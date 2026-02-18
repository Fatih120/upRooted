using System.Reflection;

namespace Uprooted;

/// <summary>
/// SkiaSharp-based animated GIF/WebP decoder and playback controller.
/// Resolves SKCodec, SKBitmap, SKImage, etc. via reflection from assemblies already
/// loaded in Root's process (Avalonia ships SkiaSharp as its rendering backend).
///
/// Frame extraction: SKCodec decodes each frame → SKImage.Encode(PNG) → byte[] → Avalonia Bitmap(Stream).
/// Playback: per-embed System.Threading.Timer cycles frames on the UI thread.
///
/// Graceful fallback: if any SkiaSharp type/method is missing or trimmed, all public methods
/// return null/false, and the caller falls back to static first-frame rendering.
/// </summary>
internal class AnimatedImage
{
    private const int MaxFrames = 100;
    private const int MinDelayMs = 20;   // Floor for per-frame delay
    private const int DefaultDelayMs = 100; // Fallback if metadata says 0
    private static readonly bool Verbose = Environment.GetEnvironmentVariable("UPROOTED_VERBOSE") == "1";

    // ===== SkiaSharp type cache (one-time resolution) =====

    private static bool s_resolved;
    private static bool s_available; // true if all required types found

    private static Type? s_skCodecType;
    private static Type? s_skStreamType;        // SKStream (base) or SKManagedStream
    private static Type? s_skBitmapType;
    private static Type? s_skImageInfoType;
    private static Type? s_skImageType;
    private static Type? s_skDataType;
    private static Type? s_skEncodedImageFormatType;
    private static Type? s_skCodecOptionsType;

    // Cached method/property handles
    private static MethodInfo? s_codecCreate;         // SKCodec.Create(SKStream) or Create(SKData)
    private static bool s_codecCreateTakesData;       // true if Create param is SKData (not stream)
    private static PropertyInfo? s_codecFrameCount;   // SKCodec.FrameCount (may be absent in some versions)
    private static PropertyInfo? s_codecFrameInfo;    // SKCodec.FrameInfo (SKCodecFrameInfo[]) — fallback for frame count via .Length
    private static PropertyInfo? s_codecInfo;          // SKCodec.Info (SKImageInfo)
    private static MethodInfo? s_codecGetPixels;      // SKCodec.GetPixels(SKImageInfo, IntPtr, SKCodecOptions)
    private static ConstructorInfo? s_streamCtor;     // SKManagedStream(Stream, bool)
    private static ConstructorInfo? s_bitmapCtor;     // SKBitmap(SKImageInfo)
    private static MethodInfo? s_bitmapGetPixels;     // SKBitmap.GetPixels()
    private static PropertyInfo? s_bitmapInfo;         // SKBitmap.Info
    private static MethodInfo? s_imageFromBitmap;     // SKImage.FromBitmap(SKBitmap)
    private static MethodInfo? s_imageEncode;         // SKImage.Encode(SKEncodedImageFormat, int)
    private static MethodInfo? s_dataToArray;         // SKData.ToArray()
    private static MethodInfo? s_dataCreateFromBytes; // SKData.CreateCopy(byte[]) or Create(byte[])
    private static object? s_pngFormat;               // SKEncodedImageFormat.Png enum value

    /// <summary>
    /// Quick magic-byte check for potentially animated image formats.
    /// For GIF: checks GIF87a/GIF89a magic bytes (all GIFs might be animated).
    /// For WebP: checks RIFF+WEBP header — delegates to SKCodec for frame count
    /// since manually parsing VP8X flags is fragile across WebP variants.
    /// </summary>
    internal static bool MightBeAnimated(byte[] data)
    {
        if (data.Length < 12) return false;

        // GIF: starts with GIF87a or GIF89a
        if (data[0] == 0x47 && data[1] == 0x49 && data[2] == 0x46) // "GIF"
            return true;

        // WebP: RIFF....WEBP header — any WebP could be animated
        // Let SKCodec.FrameCount determine actual animation (avoids fragile VP8X flag parsing)
        if (data[0] == 0x52 && data[1] == 0x49 && data[2] == 0x46 && data[3] == 0x46 && // "RIFF"
            data[8] == 0x57 && data[9] == 0x45 && data[10] == 0x42 && data[11] == 0x50)  // "WEBP"
            return true;

        return false;
    }

    /// <summary>
    /// Check if image bytes represent a multi-frame animation.
    /// Returns false if SkiaSharp is unavailable or image has ≤1 frame.
    /// </summary>
    internal static bool IsAnimated(byte[] imageBytes)
    {
        if (!MightBeAnimated(imageBytes)) return false;

        EnsureResolved();
        if (!s_available) return false;

        try
        {
            var codec = CreateCodecFromBytes(imageBytes);
            if (codec == null)
            {
                Logger.Log("AnimatedImage", "IsAnimated: SKCodec.Create returned null");
                return false;
            }

            try
            {
                var frameCount = GetFrameCount(codec);
                Logger.Log("AnimatedImage", $"IsAnimated: frameCount={frameCount}");
                return frameCount > 1;
            }
            finally
            {
                (codec as IDisposable)?.Dispose();
            }
        }
        catch (Exception ex)
        {
            var inner = ex is TargetInvocationException tie && tie.InnerException != null
                ? tie.InnerException : ex;
            Logger.Log("AnimatedImage", $"IsAnimated error: {inner.Message}");
            return false;
        }
    }

    /// <summary>
    /// Decode all frames from animated image bytes.
    /// Returns list of (Avalonia Bitmap, delay in ms) or null on failure.
    /// Each Bitmap is created via Avalonia's Bitmap(Stream) from PNG-encoded frame data.
    /// </summary>
    internal static List<(object bitmap, int delayMs)>? DecodeFrames(byte[] imageBytes, AvaloniaReflection resolver)
    {
        EnsureResolved();
        if (!s_available) return null;

        try
        {
            var codec = CreateCodecFromBytes(imageBytes);
            if (codec == null)
            {
                Logger.Log("AnimatedImage", "DecodeFrames: SKCodec.Create returned null");
                return null;
            }

            try
            {
                return DecodeFramesFromCodec(codec, resolver);
            }
            finally
            {
                (codec as IDisposable)?.Dispose();
            }
        }
        catch (Exception ex)
        {
            var inner = ex is TargetInvocationException tie && tie.InnerException != null
                ? tie.InnerException : ex;
            Logger.Log("AnimatedImage", $"DecodeFrames error: {inner.Message}");
            return null;
        }
    }

    /// <summary>
    /// Get frame count from codec. Tries FrameCount property first (some versions),
    /// falls back to FrameInfo[].Length (always available when FrameInfo resolves).
    /// </summary>
    private static int GetFrameCount(object codec)
    {
        // Try FrameCount property (not present in all SkiaSharp versions)
        if (s_codecFrameCount != null)
        {
            var val = s_codecFrameCount.GetValue(codec);
            if (val is int count) return count;
        }

        // Fallback: FrameInfo array length
        if (s_codecFrameInfo != null)
        {
            var arr = s_codecFrameInfo.GetValue(codec) as Array;
            if (arr != null) return arr.Length;
        }

        return 0;
    }

    /// <summary>
    /// Create SKCodec from raw bytes. Tries SKData path first (more reliable),
    /// falls back to SKManagedStream path.
    /// </summary>
    private static object? CreateCodecFromBytes(byte[] imageBytes)
    {
        // Path 1: SKCodec.Create(SKData) — preferred, avoids stream lifecycle issues
        if (s_codecCreateTakesData && s_dataCreateFromBytes != null)
        {
            try
            {
                var skData = s_dataCreateFromBytes.Invoke(null, new object[] { imageBytes });
                if (skData != null)
                {
                    var codec = s_codecCreate!.Invoke(null, new[] { skData });
                    // Note: codec holds a ref to skData, don't dispose skData here
                    if (codec != null) return codec;
                }
            }
            catch (Exception ex)
            {
                if (Verbose) Logger.Log("AnimatedImage", $"SKData codec path failed: {ex.Message}");
            }
        }

        // Path 2: SKCodec.Create(SKManagedStream) — fallback
        if (!s_codecCreateTakesData && s_streamCtor != null)
        {
            var ms = new System.IO.MemoryStream(imageBytes);
            object?[] streamArgs = s_streamCtor.GetParameters().Length == 2
                ? new object[] { ms, false }
                : new object[] { ms };
            var skStream = s_streamCtor.Invoke(streamArgs);
            if (skStream != null)
            {
                var codec = s_codecCreate!.Invoke(null, new[] { skStream });
                // Note: codec holds a ref to the stream, don't dispose here
                if (codec != null) return codec;
                (skStream as IDisposable)?.Dispose();
            }
            ms.Dispose();
        }

        return null;
    }

    private static List<(object bitmap, int delayMs)>? DecodeFramesFromCodec(object codec, AvaloniaReflection resolver)
    {
        var frameCount = GetFrameCount(codec);
        if (frameCount <= 1) return null;

        var cappedCount = Math.Min(frameCount, MaxFrames);
        Logger.Log("AnimatedImage", $"Decoding {cappedCount} frames (total={frameCount})");

        // Get frame info array for per-frame durations
        var frameInfoArray = s_codecFrameInfo!.GetValue(codec) as Array;

        // Get codec's image info (dimensions, color type, etc.)
        var codecInfo = s_codecInfo!.GetValue(codec);
        if (codecInfo == null)
        {
            Logger.Log("AnimatedImage", "DecodeFrames: codec.Info is null");
            return null;
        }

        var frames = new List<(object, int)>(cappedCount);

        for (int i = 0; i < cappedCount; i++)
        {
            try
            {
                var frameBitmap = DecodeFrame(codec, codecInfo, i);
                if (frameBitmap == null)
                {
                    if (Verbose) Logger.Log("AnimatedImage", $"Frame {i}: DecodeFrame returned null");
                    continue;
                }

                try
                {
                    // Convert SKBitmap → SKImage → Encode(PNG) → byte[] → Avalonia Bitmap
                    var avBitmap = ConvertToAvBitmap(frameBitmap, resolver);
                    if (avBitmap == null)
                    {
                        if (Verbose) Logger.Log("AnimatedImage", $"Frame {i}: ConvertToAvBitmap returned null");
                        continue;
                    }

                    // Get frame delay from FrameInfo
                    int delayMs = DefaultDelayMs;
                    if (frameInfoArray != null && i < frameInfoArray.Length)
                    {
                        var frameInfo = frameInfoArray.GetValue(i);
                        if (frameInfo != null)
                        {
                            var durProp = frameInfo.GetType().GetProperty("Duration");
                            if (durProp != null)
                            {
                                var dur = durProp.GetValue(frameInfo);
                                if (dur is int d) delayMs = d;
                            }
                        }
                    }

                    // Enforce minimum delay (some GIFs specify 0 or 10ms)
                    if (delayMs < MinDelayMs) delayMs = DefaultDelayMs;

                    frames.Add((avBitmap, delayMs));
                }
                finally
                {
                    (frameBitmap as IDisposable)?.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Log("AnimatedImage", $"Frame {i} decode error: {ex.Message}");
            }
        }

        if (frames.Count == 0) return null;

        Logger.Log("AnimatedImage", $"Decoded {frames.Count} frames successfully");
        return frames;
    }

    /// <summary>
    /// Decode a single frame from the codec into an SKBitmap.
    /// Uses SKCodec.GetPixels with SKCodecOptions.FrameIndex.
    /// </summary>
    private static object? DecodeFrame(object codec, object codecInfo, int frameIndex)
    {
        // Create SKBitmap with the codec's image info
        var bitmap = s_bitmapCtor!.Invoke(new[] { codecInfo });
        if (bitmap == null) return null;

        try
        {
            // Get pixel buffer pointer
            var pixels = s_bitmapGetPixels!.Invoke(bitmap, null);
            if (pixels == null || pixels.Equals(IntPtr.Zero))
            {
                (bitmap as IDisposable)?.Dispose();
                return null;
            }

            // Create SKCodecOptions with FrameIndex set
            var options = Activator.CreateInstance(s_skCodecOptionsType!);
            if (options == null)
            {
                (bitmap as IDisposable)?.Dispose();
                return null;
            }

            // Set FrameIndex property
            var frameIndexProp = s_skCodecOptionsType!.GetProperty("FrameIndex");
            frameIndexProp?.SetValue(options, frameIndex);

            // For frames after the first, set PriorFrame to previous frame
            // This ensures proper rendering of frames that depend on previous frame state
            if (frameIndex > 0)
            {
                var priorFrameProp = s_skCodecOptionsType.GetProperty("PriorFrame");
                priorFrameProp?.SetValue(options, frameIndex - 1);
            }

            // Get the bitmap's info (may differ from codec info in some edge cases)
            var bitmapInfo = s_bitmapInfo!.GetValue(bitmap);
            if (bitmapInfo == null) bitmapInfo = codecInfo;

            // SKCodec.GetPixels — signature varies by SkiaSharp version
            // 3-param: GetPixels(SKImageInfo, IntPtr, SKCodecOptions)
            // 4-param: GetPixels(SKImageInfo, IntPtr, int rowBytes, SKCodecOptions)
            object? result;
            var getPixelsParams = s_codecGetPixels!.GetParameters();
            if (getPixelsParams.Length == 4)
            {
                // 4-param overload: need rowBytes — get from bitmap's RowBytes property
                int rowBytes = 0;
                var rowBytesProp = bitmap.GetType().GetProperty("RowBytes",
                    BindingFlags.Public | BindingFlags.Instance);
                if (rowBytesProp != null)
                    rowBytes = (int)rowBytesProp.GetValue(bitmap)!;

                result = s_codecGetPixels.Invoke(codec, new[] { bitmapInfo, pixels, (object)rowBytes, options });
            }
            else
            {
                result = s_codecGetPixels.Invoke(codec, new[] { bitmapInfo, pixels, options });
            }

            // Result is SKCodecResult enum — Success = 0
            if (result != null)
            {
                var resultStr = result.ToString();
                if (resultStr != "Success" && resultStr != "IncompleteInput")
                {
                    Logger.Log("AnimatedImage", $"GetPixels frame {frameIndex}: {resultStr}");
                    (bitmap as IDisposable)?.Dispose();
                    return null;
                }
            }

            return bitmap;
        }
        catch
        {
            (bitmap as IDisposable)?.Dispose();
            throw;
        }
    }

    /// <summary>
    /// Convert SKBitmap → SKImage → Encode(PNG) → SKData → byte[] → Avalonia Bitmap(Stream).
    /// </summary>
    private static object? ConvertToAvBitmap(object skBitmap, AvaloniaReflection resolver)
    {
        // SKImage.FromBitmap(SKBitmap)
        var skImage = s_imageFromBitmap!.Invoke(null, new[] { skBitmap });
        if (skImage == null) return null;

        try
        {
            // SKImage.Encode(SKEncodedImageFormat.Png, 100)
            var skData = s_imageEncode!.Invoke(skImage, new[] { s_pngFormat!, (object)100 });
            if (skData == null) return null;

            try
            {
                // SKData.ToArray() → byte[]
                var pngBytes = s_dataToArray!.Invoke(skData, null) as byte[];
                if (pngBytes == null || pngBytes.Length == 0) return null;

                // Avalonia Bitmap(Stream) from PNG bytes
                using var pngStream = new System.IO.MemoryStream(pngBytes);
                return resolver.CreateBitmapFromStream(pngStream);
            }
            finally
            {
                (skData as IDisposable)?.Dispose();
            }
        }
        finally
        {
            (skImage as IDisposable)?.Dispose();
        }
    }

    /// <summary>
    /// Create an animated Image control with timer-based frame cycling.
    /// Returns (imageControl, disposeAction) or null on failure.
    /// The dispose action stops the timer and releases frame references.
    /// </summary>
    internal static (object control, Action dispose)? CreateAnimatedControl(
        List<(object bitmap, int delayMs)> frames,
        AvaloniaReflection resolver)
    {
        if (frames.Count == 0) return null;

        var img = resolver.CreateImage("Uniform");
        if (img == null) return null;

        // Set first frame
        resolver.SetImageSource(img, frames[0].bitmap);

        if (frames.Count == 1)
        {
            // Single frame — no timer needed
            return (img, () => { });
        }

        int currentFrame = 0;
        Timer? timer = null;
        var capturedFrames = frames;
        var capturedImg = img;
        var capturedResolver = resolver;

        timer = new Timer(_ =>
        {
            try
            {
                var localFrames = capturedFrames;
                if (localFrames == null || localFrames.Count == 0) return;

                var nextFrame = (currentFrame + 1) % localFrames.Count;
                currentFrame = nextFrame;
                var nextBitmap = localFrames[nextFrame].bitmap;

                capturedResolver.RunOnUIThread(() =>
                {
                    try
                    {
                        capturedResolver.SetImageSource(capturedImg, nextBitmap);
                    }
                    catch { }
                });

                // Schedule next tick with per-frame delay
                var nextDelay = localFrames[nextFrame].delayMs;
                if (nextDelay < MinDelayMs) nextDelay = DefaultDelayMs;
                timer?.Change(nextDelay, Timeout.Infinite);
            }
            catch { }
        }, null, frames[0].delayMs, Timeout.Infinite);

        Action dispose = () =>
        {
            try
            {
                timer?.Dispose();
                timer = null;
                capturedFrames = null!;
            }
            catch { }
        };

        return (img, dispose);
    }

    // ===== SkiaSharp type resolution =====

    private static void EnsureResolved()
    {
        if (s_resolved) return;
        s_resolved = true;

        try
        {
            ResolveSkiaTypes();
            ResolveSkiaMethods();

            // FrameCount is optional — we fall back to FrameInfo[].Length
            s_available = s_codecCreate != null &&
                          (s_codecFrameCount != null || s_codecFrameInfo != null) &&
                          s_codecInfo != null &&
                          s_codecGetPixels != null &&
                          (s_streamCtor != null || s_dataCreateFromBytes != null) &&
                          s_bitmapCtor != null &&
                          s_bitmapGetPixels != null &&
                          s_bitmapInfo != null &&
                          s_imageFromBitmap != null &&
                          s_imageEncode != null &&
                          s_dataToArray != null &&
                          s_pngFormat != null;

            Logger.Log("AnimatedImage", $"SkiaSharp resolved: available={s_available}");

            // Always log which methods are missing — critical for diagnosing failures
            if (!s_available)
            {
                var missing = new List<string>();
                if (s_codecCreate == null) missing.Add("SKCodec.Create");
                if (s_codecFrameCount == null && s_codecFrameInfo == null) missing.Add("SKCodec.FrameCount/FrameInfo");
                if (s_codecInfo == null) missing.Add("SKCodec.Info");
                if (s_codecGetPixels == null) missing.Add("SKCodec.GetPixels");
                if (s_streamCtor == null && s_dataCreateFromBytes == null) missing.Add("SKManagedStream/SKData.Create");
                if (s_bitmapCtor == null) missing.Add("SKBitmap(SKImageInfo)");
                if (s_bitmapGetPixels == null) missing.Add("SKBitmap.GetPixels");
                if (s_bitmapInfo == null) missing.Add("SKBitmap.Info");
                if (s_imageFromBitmap == null) missing.Add("SKImage.FromBitmap");
                if (s_imageEncode == null) missing.Add("SKImage.Encode");
                if (s_dataToArray == null) missing.Add("SKData.ToArray");
                if (s_pngFormat == null) missing.Add("SKEncodedImageFormat.Png");
                Logger.Log("AnimatedImage", $"Missing: {string.Join(", ", missing)}");
            }

            if (Verbose)
            {
                Logger.Log("AnimatedImage", $"  Types: SKCodec={s_skCodecType != null}, " +
                    $"SKStream={s_skStreamType != null}, SKBitmap={s_skBitmapType != null}, " +
                    $"SKImageInfo={s_skImageInfoType != null}, SKImage={s_skImageType != null}, " +
                    $"SKData={s_skDataType != null}, SKCodecOptions={s_skCodecOptionsType != null}");
                Logger.Log("AnimatedImage", $"  Methods: Create={s_codecCreate != null} (data={s_codecCreateTakesData}), " +
                    $"DataCreate={s_dataCreateFromBytes != null}, " +
                    $"GetPixels={s_codecGetPixels != null}" +
                    (s_codecGetPixels != null ? $" ({s_codecGetPixels.GetParameters().Length} params)" : ""));
            }
        }
        catch (Exception ex)
        {
            Logger.Log("AnimatedImage", $"SkiaSharp resolve error: {ex.Message}");
            s_available = false;
        }
    }

    private static void ResolveSkiaTypes()
    {
        var typeMap = new Dictionary<string, Type>();

        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                var name = asm.GetName().Name ?? "";
                if (!name.StartsWith("SkiaSharp", StringComparison.OrdinalIgnoreCase)) continue;

                foreach (var type in asm.GetTypes())
                {
                    var fn = type.FullName;
                    if (fn != null) typeMap[fn] = type;
                }
            }
            catch { }
        }

        Logger.Log("AnimatedImage", $"Scanned SkiaSharp assemblies, found {typeMap.Count} types");

        Type? Find(string fullName) => typeMap.TryGetValue(fullName, out var t) ? t : null;

        s_skCodecType = Find("SkiaSharp.SKCodec");
        s_skBitmapType = Find("SkiaSharp.SKBitmap");
        s_skImageInfoType = Find("SkiaSharp.SKImageInfo");
        s_skImageType = Find("SkiaSharp.SKImage");
        s_skDataType = Find("SkiaSharp.SKData");
        s_skEncodedImageFormatType = Find("SkiaSharp.SKEncodedImageFormat");
        s_skCodecOptionsType = Find("SkiaSharp.SKCodecOptions");

        // SKManagedStream preferred, fall back to SKStream
        s_skStreamType = Find("SkiaSharp.SKManagedStream");
        s_skStreamType ??= Find("SkiaSharp.SKStream");

        if (Verbose)
        {
            foreach (var kv in typeMap)
            {
                if (kv.Key.Contains("Codec") || kv.Key.Contains("Stream") || kv.Key.Contains("Bitmap") ||
                    kv.Key.Contains("Image") || kv.Key.Contains("Data") || kv.Key.Contains("Format"))
                    Logger.Log("AnimatedImage", $"  Type: {kv.Key}");
            }
        }
    }

    private static void ResolveSkiaMethods()
    {
        var pub = BindingFlags.Public | BindingFlags.Instance;
        var pubStatic = BindingFlags.Public | BindingFlags.Static;

        // SKManagedStream(Stream, bool disposeManagedStream)
        if (s_skStreamType != null)
        {
            s_streamCtor = s_skStreamType.GetConstructor(new[] { typeof(System.IO.Stream), typeof(bool) });
            // Fallback: SKManagedStream(Stream)
            s_streamCtor ??= s_skStreamType.GetConstructor(new[] { typeof(System.IO.Stream) });
        }

        // SKData.CreateCopy(byte[]) or SKData.Create(byte[], int) — for SKData-based codec creation
        if (s_skDataType != null)
        {
            // Try CreateCopy(byte[]) first — makes an independent copy (safest)
            s_dataCreateFromBytes = s_skDataType.GetMethod("CreateCopy", pubStatic, null,
                new[] { typeof(byte[]) }, null);
            // Fallback: Create(byte[])
            s_dataCreateFromBytes ??= s_skDataType.GetMethod("Create", pubStatic, null,
                new[] { typeof(byte[]) }, null);
            // Fallback: Create(byte[], int)
            s_dataCreateFromBytes ??= s_skDataType.GetMethods(pubStatic)
                .FirstOrDefault(m => m.Name == "CreateCopy" &&
                    m.GetParameters().Length == 1 &&
                    m.GetParameters()[0].ParameterType == typeof(byte[]));
        }

        // SKCodec.Create — try SKData overload first (more reliable), fall back to SKStream
        if (s_skCodecType != null)
        {
            // Prefer: SKCodec.Create(SKData)
            if (s_skDataType != null)
            {
                s_codecCreate = s_skCodecType.GetMethod("Create", pubStatic, null,
                    new[] { s_skDataType }, null);
                if (s_codecCreate != null)
                {
                    s_codecCreateTakesData = true;
                    if (Verbose) Logger.Log("AnimatedImage", "Using SKCodec.Create(SKData) path");
                }
            }

            // Fallback: SKCodec.Create(SKStream)
            if (s_codecCreate == null && s_skStreamType != null)
            {
                s_codecCreate = s_skCodecType.GetMethod("Create", pubStatic, null,
                    new[] { s_skStreamType }, null);

                // Try base SKStream type if SKManagedStream didn't match
                if (s_codecCreate == null)
                {
                    var baseStreamType = s_skStreamType.BaseType;
                    while (baseStreamType != null && baseStreamType != typeof(object))
                    {
                        s_codecCreate = s_skCodecType.GetMethod("Create", pubStatic, null,
                            new[] { baseStreamType }, null);
                        if (s_codecCreate != null) break;
                        baseStreamType = baseStreamType.BaseType;
                    }
                }

                // Last resort: find any single-param Create overload compatible with our stream type
                if (s_codecCreate == null)
                {
                    s_codecCreate = s_skCodecType.GetMethods(pubStatic)
                        .FirstOrDefault(m => m.Name == "Create" &&
                            m.GetParameters().Length == 1 &&
                            m.GetParameters()[0].ParameterType.IsAssignableFrom(s_skStreamType));
                }

                if (s_codecCreate != null)
                {
                    s_codecCreateTakesData = false;
                    if (Verbose) Logger.Log("AnimatedImage", "Using SKCodec.Create(SKStream) path");
                }
            }

            // Dump all Create overloads for diagnostics if we failed
            if (s_codecCreate == null)
            {
                Logger.Log("AnimatedImage", "SKCodec.Create not found. Available overloads:");
                foreach (var m in s_skCodecType.GetMethods(pubStatic))
                {
                    if (m.Name == "Create")
                    {
                        var parms = string.Join(", ", m.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
                        Logger.Log("AnimatedImage", $"  Create({parms})");
                    }
                }
            }

            s_codecFrameCount = s_skCodecType.GetProperty("FrameCount", pub);
            s_codecFrameInfo = s_skCodecType.GetProperty("FrameInfo", pub);
            s_codecInfo = s_skCodecType.GetProperty("Info", pub);

            // Log frame count strategy
            if (s_codecFrameCount != null)
                Logger.Log("AnimatedImage", "Frame count: via FrameCount property");
            else if (s_codecFrameInfo != null)
                Logger.Log("AnimatedImage", "Frame count: via FrameInfo[].Length (no FrameCount property)");

            // Diagnostic dump if critical methods are missing
            if (s_codecGetPixels == null || s_codecInfo == null ||
                (s_codecFrameCount == null && s_codecFrameInfo == null))
            {
                Logger.Log("AnimatedImage", $"--- SKCodec members ({s_skCodecType.FullName}) ---");
                foreach (var p in s_skCodecType.GetProperties(pub))
                    Logger.Log("AnimatedImage", $"  Prop: {p.Name} : {p.PropertyType.Name}");
                foreach (var m in s_skCodecType.GetMethods(pub))
                {
                    if (m.DeclaringType == typeof(object)) continue;
                    var parms = string.Join(", ", m.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
                    Logger.Log("AnimatedImage", $"  Method: {m.Name}({parms}) -> {m.ReturnType.Name}");
                }
            }

            // SKCodec.GetPixels — try multiple overload signatures
            if (s_skImageInfoType != null && s_skCodecOptionsType != null)
            {
                // 3-param: GetPixels(SKImageInfo, IntPtr, SKCodecOptions)
                s_codecGetPixels = s_skCodecType.GetMethod("GetPixels", pub, null,
                    new[] { s_skImageInfoType, typeof(IntPtr), s_skCodecOptionsType }, null);

                // 4-param: GetPixels(SKImageInfo, IntPtr, int rowBytes, SKCodecOptions)
                s_codecGetPixels ??= s_skCodecType.GetMethod("GetPixels", pub, null,
                    new[] { s_skImageInfoType, typeof(IntPtr), typeof(int), s_skCodecOptionsType }, null);

                // Broad search: any GetPixels overload with SKCodecOptions as last param
                s_codecGetPixels ??= s_skCodecType.GetMethods(pub)
                    .FirstOrDefault(m => m.Name == "GetPixels" &&
                        m.GetParameters().Length >= 3 &&
                        m.GetParameters().Last().ParameterType == s_skCodecOptionsType);

                if (s_codecGetPixels == null)
                {
                    // Dump all GetPixels overloads for diagnostics
                    Logger.Log("AnimatedImage", "SKCodec.GetPixels not found. Available overloads:");
                    foreach (var m in s_skCodecType.GetMethods(pub))
                    {
                        if (m.Name == "GetPixels")
                        {
                            var parms = string.Join(", ", m.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
                            Logger.Log("AnimatedImage", $"  GetPixels({parms})");
                        }
                    }
                }
            }
        }

        // SKBitmap(SKImageInfo)
        if (s_skBitmapType != null && s_skImageInfoType != null)
        {
            s_bitmapCtor = s_skBitmapType.GetConstructor(new[] { s_skImageInfoType });
            s_bitmapGetPixels = s_skBitmapType.GetMethod("GetPixels", pub, null, Type.EmptyTypes, null);
            s_bitmapInfo = s_skBitmapType.GetProperty("Info", pub);
        }

        // SKImage.FromBitmap(SKBitmap)
        if (s_skImageType != null && s_skBitmapType != null)
        {
            s_imageFromBitmap = s_skImageType.GetMethod("FromBitmap", pubStatic, null,
                new[] { s_skBitmapType }, null);
        }

        // SKImage.Encode(SKEncodedImageFormat, int)
        if (s_skImageType != null && s_skEncodedImageFormatType != null)
        {
            s_imageEncode = s_skImageType.GetMethod("Encode", pub, null,
                new[] { s_skEncodedImageFormatType, typeof(int) }, null);

            // Resolve SKEncodedImageFormat.Png
            try
            {
                s_pngFormat = Enum.Parse(s_skEncodedImageFormatType, "Png");
            }
            catch { }
        }

        // SKData.ToArray()
        if (s_skDataType != null)
        {
            s_dataToArray = s_skDataType.GetMethod("ToArray", pub, null, Type.EmptyTypes, null);
        }
    }
}
