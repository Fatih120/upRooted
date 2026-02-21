// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKCodec
using System;
using System.IO;
using SkiaSharp;

public class SKCodec : SKObject, ISKSkipObjectRegistration
{
	public static int MinBufferedBytesNeeded => (int)SkiaApi.sk_codec_min_buffered_bytes_needed();

	public unsafe SKImageInfo Info
	{
		get
		{
			SKImageInfoNative sKImageInfoNative = default(SKImageInfoNative);
			SkiaApi.sk_codec_get_info(Handle, &sKImageInfoNative);
			return SKImageInfoNative.ToManaged(ref sKImageInfoNative);
		}
	}

	public int RepetitionCount => SkiaApi.sk_codec_get_repetition_count(Handle);

	public unsafe SKCodecFrameInfo[] FrameInfo
	{
		get
		{
			int num = SkiaApi.sk_codec_get_frame_count(Handle);
			SKCodecFrameInfo[] array = new SKCodecFrameInfo[num];
			fixed (SKCodecFrameInfo* ptr = array)
			{
				SkiaApi.sk_codec_get_frame_info(Handle, ptr);
			}
			return array;
		}
	}

	internal SKCodec(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeNative()
	{
		SkiaApi.sk_codec_destroy(Handle);
	}

	public unsafe SKSizeI GetScaledDimensions(float P_0)
	{
		SKSizeI result = default(SKSizeI);
		SkiaApi.sk_codec_get_scaled_dimensions(Handle, P_0, &result);
		return result;
	}

	public SKCodecResult GetPixels(SKImageInfo P_0, IntPtr P_1)
	{
		return GetPixels(P_0, P_1, P_0.RowBytes, SKCodecOptions.Default);
	}

	public unsafe SKCodecResult GetPixels(SKImageInfo P_0, IntPtr P_1, int P_2, SKCodecOptions P_3)
	{
		if (P_1 == IntPtr.Zero)
		{
			throw new ArgumentNullException("pixels");
		}
		SKImageInfoNative sKImageInfoNative = SKImageInfoNative.FromManaged(ref P_0);
		SKCodecOptionsInternal sKCodecOptionsInternal = new SKCodecOptionsInternal
		{
			fZeroInitialized = P_3.ZeroInitialized,
			fSubset = null,
			fFrameIndex = P_3.FrameIndex,
			fPriorFrame = P_3.PriorFrame
		};
		SKRectI sKRectI = default(SKRectI);
		if (P_3.HasSubset)
		{
			sKRectI = P_3.Subset.Value;
			sKCodecOptionsInternal.fSubset = &sKRectI;
		}
		return SkiaApi.sk_codec_get_pixels(Handle, &sKImageInfoNative, (void*)P_1, (IntPtr)P_2, &sKCodecOptionsInternal);
	}

	public static SKCodec Create(Stream P_0)
	{
		SKCodecResult sKCodecResult;
		return Create(P_0, out sKCodecResult);
	}

	public static SKCodec Create(Stream P_0, out SKCodecResult P_1)
	{
		return Create(WrapManagedStream(P_0), out P_1);
	}

	public unsafe static SKCodec Create(SKStream P_0, out SKCodecResult P_1)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("stream");
		}
		SKFileStream sKFileStream = null;
		if (sKFileStream != null && !sKFileStream.IsValid)
		{
			throw new ArgumentException("File stream was not valid.", "stream");
		}
		fixed (SKCodecResult* ptr = &P_1)
		{
			SKCodec sKCodec = GetObject(SkiaApi.sk_codec_new_from_stream(P_0.Handle, ptr));
			P_0.RevokeOwnership(sKCodec);
			return sKCodec;
		}
	}

	public static SKCodec Create(SKData P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("data");
		}
		return GetObject(SkiaApi.sk_codec_new_from_data(P_0.Handle));
	}

	internal static SKStream WrapManagedStream(Stream P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("stream");
		}
		if (P_0.CanSeek)
		{
			return new SKManagedStream(P_0, true);
		}
		return new SKFrontBufferedManagedStream(P_0, MinBufferedBytesNeeded, true);
	}

	internal static SKCodec GetObject(IntPtr P_0)
	{
		if (!(P_0 == IntPtr.Zero))
		{
			return new SKCodec(P_0, true);
		}
		return null;
	}
}

