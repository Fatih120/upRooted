using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Uprooted;

/// <summary>
/// Audit Log engine — intercepts Root's CommunityLogGrpcService/List HTTP responses to
/// extract community audit log entries (message deletions, edits, role changes, etc.)
/// and expose them via OnEntry for downstream consumers (MessageLogger).
///
/// Phase 1: HTTP interception + protobuf field logging for schema discovery.
///           Run /watch-log and open the Action Logs page; look for [AuditLog] lines.
///           Identify which field numbers map to action_type, actor, channel, message, etc.
///
/// Phase 2: Update the FieldXxx and ActionXxx constants below based on Phase 1 findings.
///           Rebuild — no structural changes required.
///
/// Phase 3: MessageLogger subscribes to OnEntry and calls ProcessAuditEntry.
/// </summary>
internal class AuditLogEngine : IDisposable
{
    private const string Tag = "AuditLog";
    private const int ScanIntervalMs = 30_000;

    // ===== Field constants — UPDATE AFTER PHASE 2 CALIBRATION =====
    // Set to the actual field numbers found in Phase 1 diagnostics.
    // Defaults are best-guess placeholders based on common gRPC audit log schemas.
    private const int FieldActionType  = 1;  // TBD: varint action enum
    private const int FieldActorId     = 2;  // TBD: actor UUID (bytes)
    private const int FieldCommunityId = 3;  // TBD: community UUID (bytes)
    private const int FieldChannelId   = 4;  // TBD: channel UUID (bytes)
    private const int FieldMessageId   = 5;  // TBD: message UUID (bytes)
    private const int FieldTimestamp   = 6;  // TBD: unix timestamp (varint or fixed64)
    private const int FieldContent     = 7;  // TBD: content string (if preserved by server)
    private const int FieldActorName   = 0;  // TBD: actor display name — set after Phase 2

    // Action type enum values — UPDATE AFTER PHASE 2 CALIBRATION.
    // Set to -1/-2 as sentinels so Phase 1 never accidentally matches anything.
    internal const int ActionMessageDelete = -1;  // TBD
    internal const int ActionMessageEdit   = -2;  // TBD

    // Outer message field containing the repeated audit log entry sub-messages.
    // Root wraps the list at field 10 (standard for repeated entries in Root's gRPC schemas).
    private const int OuterFieldEntries = 10;

    // ===== HttpClient scanning helpers =====

    private static readonly string[] CandidateKeywords =
        { "Grpc", "Api", "Service", "Client", "Http", "Channel", "Community", "Audit", "Log", "Message" };

    private readonly AvaloniaReflection _r;
    private readonly object _mainWindow;

    private readonly HashSet<int> _patched = new();
    private Timer? _scanTimer;
    private int _scanning;
    private int _patchCount;

    private static readonly FieldInfo? HandlerField =
        typeof(HttpMessageInvoker).GetField("_handler",
            BindingFlags.NonPublic | BindingFlags.Instance);

    /// <summary>Fired (on the UI thread) for each audit log entry decoded from a live response.</summary>
    internal event Action<AuditLogEntry>? OnEntry;

    internal AuditLogEngine(AvaloniaReflection resolver, object mainWindow)
    {
        _r = resolver;
        _mainWindow = mainWindow;
    }

    internal void Initialize()
    {
        if (HandlerField == null)
        {
            Logger.Log(Tag, "HttpMessageInvoker._handler not found — engine disabled");
            return;
        }
        Logger.Log(Tag, "Starting audit log engine (HttpClient interception for CommunityLogGrpcService/List)");
        _scanTimer = new Timer(OnScanTick, null, 0, ScanIntervalMs);
    }

    public void Dispose()
    {
        var t = _scanTimer;
        _scanTimer = null;
        t?.Dispose();
    }

    // ===== Timer =====

    private void OnScanTick(object? state)
    {
        if (Interlocked.CompareExchange(ref _scanning, 1, 0) != 0) return;
        try
        {
            ScanStaticFields();

            using var done = new ManualResetEventSlim(false);
            _r.RunOnUIThread(() =>
            {
                try { ScanViewModelChain(); }
                catch (Exception ex) { Logger.Log(Tag, $"ViewModel scan error: {ex.Message}"); }
                finally { done.Set(); }
            });
            done.Wait(10_000);

            if (_patchCount > 0 && _scanTimer != null)
                _scanTimer.Change(30_000, 30_000);
        }
        catch (Exception ex) { Logger.Log(Tag, $"ScanTick error: {ex.Message}"); }
        finally { Interlocked.Exchange(ref _scanning, 0); }
    }

    // ===== HttpClient scanning: static fields =====

    private void ScanStaticFields()
    {
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            try
            {
                var asmName = asm.GetName().Name ?? "";
                if (IsFrameworkAssembly(asmName)) continue;

                Type[] types;
                try { types = asm.GetTypes(); }
                catch { continue; }

                foreach (var type in types)
                {
                    if (!NameMatchesCandidates(type.Name)) continue;
                    try
                    {
                        var fields = type.GetFields(
                            BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                        foreach (var field in fields)
                        {
                            if (!IsHttpClientField(field.FieldType)) continue;
                            try
                            {
                                var client = field.GetValue(null) as HttpClient;
                                if (client != null)
                                    TryPatch(client, $"{type.Name}.{field.Name} (static)");
                            }
                            catch { }
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }
    }

    // ===== HttpClient scanning: ViewModel chain =====

    private void ScanViewModelChain()
    {
        object? dc = null;
        try
        {
            dc = _mainWindow.GetType()
                .GetProperty("DataContext",
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                ?.GetValue(_mainWindow);
        }
        catch { }

        if (dc == null) return;
        var visited = new HashSet<int>();
        WalkInstanceFields(dc, 0, visited);
    }

    private void WalkInstanceFields(object obj, int depth, HashSet<int> visited)
    {
        if (depth > 8) return;
        int id = System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
        if (!visited.Add(id)) return;

        var type = obj.GetType();
        if (IsFrameworkType(type)) return;

        var fields = type.GetFields(
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var field in fields)
        {
            try
            {
                var ft = field.FieldType;
                if (IsHttpClientField(ft))
                {
                    var client = field.GetValue(obj) as HttpClient;
                    if (client != null)
                        TryPatch(client, $"{type.Name}.{field.Name} (instance d={depth})");
                    continue;
                }
                if (ft.IsPrimitive || ft == typeof(string) || ft.IsEnum) continue;
                if (IsFrameworkType(ft)) continue;
                if (NameMatchesCandidates(type.Name) || NameMatchesCandidates(ft.Name))
                {
                    var child = field.GetValue(obj);
                    if (child != null) WalkInstanceFields(child, depth + 1, visited);
                }
            }
            catch { }
        }
    }

    // ===== Handler injection =====

    private void TryPatch(HttpClient client, string location)
    {
        if (HandlerField == null) return;
        int id = System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(client);
        if (_patched.Contains(id)) return;

        try
        {
            var existing = HandlerField.GetValue(client) as HttpMessageHandler;
            if (existing is AuditLogInterceptorHandler)
            {
                _patched.Add(id);
                return;
            }

            var handler = new AuditLogInterceptorHandler(existing!, this);
            HandlerField.SetValue(client, handler);
            _patched.Add(id);
            _patchCount++;
            Logger.Log(Tag, $"Patched HttpClient @ {location} (total: {_patchCount})");
        }
        catch (Exception ex) { Logger.Log(Tag, $"Patch failed @ {location}: {ex.Message}"); }
    }

    // ===== Response parsing =====

    internal void ParseAuditLogResponse(byte[] raw)
    {
        try
        {
            Logger.Log(Tag, $"Parsing CommunityLogGrpcService/List response ({raw.Length} bytes)");

            // Decode gRPC-web frames.
            // Format: [flag:1][length:4 big-endian][payload:length]
            // flag 0x00 = data frame, 0x80 = trailer frame
            int pos = 0;
            int frameIndex = 0;
            while (pos + 5 <= raw.Length)
            {
                byte flag = raw[pos];
                uint frameLen = ((uint)raw[pos + 1] << 24) | ((uint)raw[pos + 2] << 16)
                              | ((uint)raw[pos + 3] << 8)  |  raw[pos + 4];
                pos += 5;

                if (frameLen > raw.Length - pos) break;

                if (flag == 0x00) // data frame
                {
                    frameIndex++;
                    Logger.Log(Tag, $"gRPC data frame #{frameIndex}: {frameLen} bytes");
                    ParseDataFrame(raw, pos, (int)frameLen);
                }
                else if (flag == 0x80) // trailer frame
                {
                    // Trailers contain gRPC status/metadata as HTTP/1.1 headers.
                    // Check for grpc-status to surface permission errors.
                    try
                    {
                        var trailerText = Encoding.ASCII.GetString(raw, pos, Math.Min((int)frameLen, 512));
                        if (trailerText.Contains("grpc-status: 7") || trailerText.Contains("grpc-status:7"))
                            Logger.Log(Tag, "grpc-status:7 PERMISSION_DENIED — user does not have manage_audit_log");
                        else if (trailerText.Contains("grpc-status:"))
                            Logger.Log(Tag, $"Trailers: {trailerText.Trim()}");
                    }
                    catch { }
                }

                pos += (int)frameLen;
            }
        }
        catch (Exception ex) { Logger.Log(Tag, $"ParseAuditLogResponse: {ex.Message}"); }
    }

    private void ParseDataFrame(byte[] data, int offset, int length)
    {
        try
        {
            var outerFields = DecodeProtoFields(data, offset, length);
            int entryIndex = 0;
            foreach (var f in outerFields)
            {
                if (f.FieldNum != OuterFieldEntries || f.WireType != 2) continue;
                entryIndex++;
                LogEntryFields(f.Payload, entryIndex);
                var entry = TryParseEntry(f.Payload);
                if (entry != null)
                {
                    var captured = entry;
                    _r.RunOnUIThread(() =>
                    {
                        try { OnEntry?.Invoke(captured); }
                        catch (Exception ex) { Logger.Log(Tag, $"OnEntry handler error: {ex.Message}"); }
                    });
                }
            }
            if (entryIndex == 0)
                Logger.Log(Tag, $"No entries at field {OuterFieldEntries} — outer fields: {string.Join(", ", outerFields.Select(f => $"f{f.FieldNum}/wt{f.WireType}"))}");
        }
        catch (Exception ex) { Logger.Log(Tag, $"ParseDataFrame: {ex.Message}"); }
    }

    // ===== Phase 1: Diagnostic field logger =====

    private static void LogEntryFields(byte[] entryBytes, int entryIndex)
    {
        try
        {
            Logger.Log("AuditLog", $"[AuditLog] Entry #{entryIndex} ({entryBytes.Length} bytes):");
            var fields = DecodeProtoFields(entryBytes, 0, entryBytes.Length);
            foreach (var f in fields)
            {
                string value = DescribeField(f, entryBytes);
                Logger.Log("AuditLog", $"  f{f.FieldNum} wt{f.WireType} = {value}");
            }
        }
        catch (Exception ex) { Logger.Log("AuditLog", $"LogEntryFields: {ex.Message}"); }
    }

    private static string DescribeField(ProtoField f, byte[] _ = null!)
    {
        if (f.WireType == 0)
        {
            ulong v = BitConverter.ToUInt64(f.Payload, 0);
            return $"varint={v}";
        }

        if (f.WireType == 1)
        {
            ulong v = BitConverter.ToUInt64(f.Payload, 0);
            return $"fixed64={v}";
        }

        if (f.WireType == 5)
        {
            uint v = BitConverter.ToUInt32(f.Payload, 0);
            return $"fixed32={v}";
        }

        // Wire type 2: length-delimited
        var payload = f.Payload;
        var hexStr = BitConverter.ToString(payload).Replace("-", "").ToLowerInvariant();
        var sb = new StringBuilder();
        sb.Append($"bytes[{payload.Length}]={hexStr[..Math.Min(32, hexStr.Length)]}");

        // Attempt UUID: raw 16 bytes
        if (payload.Length == 16)
        {
            try { sb.Append($" → UUID={new Guid(payload)}"); return sb.ToString(); }
            catch { }
        }

        // Attempt UUID: sub-message with two fixed64 fields at f1/f2 (hi/lo encoding)
        if (payload.Length >= 18)
        {
            try
            {
                var sub = DecodeProtoFields(payload, 0, payload.Length);
                if (sub.Count == 2 && sub[0].WireType == 1 && sub[1].WireType == 1)
                {
                    ulong hi = BitConverter.ToUInt64(sub[0].Payload, 0);
                    ulong lo = BitConverter.ToUInt64(sub[1].Payload, 0);
                    sb.Append($" → UUID-hilo={hi:X16}{lo:X16}");
                    return sb.ToString();
                }
            }
            catch { }
        }

        // Attempt UTF-8 string
        if (payload.Length > 0)
        {
            try
            {
                bool printable = payload.All(b => b >= 32 || b == 9 || b == 10 || b == 13);
                if (printable)
                {
                    var str = Encoding.UTF8.GetString(payload);
                    sb.Append($" → str=\"{(str.Length > 60 ? str[..60] + "..." : str)}\"");
                    return sb.ToString();
                }
            }
            catch { }
        }

        // Try as nested sub-message
        try
        {
            var sub = DecodeProtoFields(payload, 0, payload.Length);
            if (sub.Count > 0)
            {
                sb.Append($" → sub-msg[{sub.Count} fields: {string.Join(", ", sub.Select(s => $"f{s.FieldNum}/wt{s.WireType}"))}]");
            }
        }
        catch { }

        return sb.ToString();
    }

    // ===== TryParseEntry: Phase 1 stub =====

    private static AuditLogEntry? TryParseEntry(byte[] entryBytes)
    {
        try
        {
            var entry = new AuditLogEntry();
            var fields = DecodeProtoFields(entryBytes, 0, entryBytes.Length);

            foreach (var f in fields)
            {
                if (f.FieldNum == FieldActionType && f.WireType == 0)
                    entry.ActionType = (int)BitConverter.ToUInt64(f.Payload, 0);
                else if (f.FieldNum == FieldActorId && f.WireType == 2)
                    entry.ActorId = TryDecodeUuid(f.Payload);
                else if (f.FieldNum == FieldCommunityId && f.WireType == 2)
                    entry.CommunityId = TryDecodeUuid(f.Payload);
                else if (f.FieldNum == FieldChannelId && f.WireType == 2)
                    entry.ChannelId = TryDecodeUuid(f.Payload);
                else if (f.FieldNum == FieldMessageId && f.WireType == 2)
                    entry.MessageId = TryDecodeUuid(f.Payload);
                else if (f.FieldNum == FieldTimestamp)
                {
                    ulong ts = f.WireType == 1
                        ? BitConverter.ToUInt64(f.Payload, 0)
                        : BitConverter.ToUInt64(f.Payload, 0); // same for varint (stored as uint64 LE)
                    entry.TimestampRaw = (long)ts;
                }
                else if (f.FieldNum == FieldContent && f.WireType == 2)
                {
                    try { entry.ExtraContent = Encoding.UTF8.GetString(f.Payload); } catch { }
                }
                else if (FieldActorName > 0 && f.FieldNum == FieldActorName && f.WireType == 2)
                {
                    try { entry.ActorName = Encoding.UTF8.GetString(f.Payload); } catch { }
                }
            }

            return entry;
        }
        catch { return null; }
    }

    private static string TryDecodeUuid(byte[] payload)
    {
        if (payload.Length == 16)
        {
            try { return new Guid(payload).ToString(); }
            catch { }
        }
        if (payload.Length >= 2)
        {
            try
            {
                var sub = DecodeProtoFields(payload, 0, payload.Length);
                if (sub.Count >= 2 && sub[0].WireType == 1 && sub[1].WireType == 1)
                {
                    ulong hi = BitConverter.ToUInt64(sub[0].Payload, 0);
                    ulong lo = BitConverter.ToUInt64(sub[1].Payload, 0);
                    return $"{hi:X16}{lo:X16}";
                }
            }
            catch { }
        }
        return BitConverter.ToString(payload).Replace("-", "").ToLowerInvariant();
    }

    // ===== Minimal protobuf wire decoder =====

    internal struct ProtoField
    {
        public int FieldNum;
        public int WireType;
        public byte[] Payload;
        public ProtoField(int fieldNum, int wireType, byte[] payload)
        { FieldNum = fieldNum; WireType = wireType; Payload = payload; }
    }

    internal static List<ProtoField> DecodeProtoFields(byte[] data, int offset, int length)
    {
        var fields = new List<ProtoField>();
        int end = offset + length;
        int pos = offset;
        bool stop = false;

        while (pos < end && !stop)
        {
            if (!TryReadVarint(data, ref pos, end, out ulong tag)) break;
            int fieldNum = (int)(tag >> 3);
            int wireType = (int)(tag & 7);
            if (fieldNum == 0) break;

            switch (wireType)
            {
                case 0: // varint
                {
                    if (!TryReadVarint(data, ref pos, end, out ulong val)) { stop = true; break; }
                    var bytes = new byte[8];
                    for (int i = 0; i < 8; i++) bytes[i] = (byte)(val >> (i * 8)); // little-endian
                    fields.Add(new ProtoField(fieldNum, wireType, bytes));
                    break;
                }
                case 1: // fixed64 — little-endian
                {
                    if (pos + 8 > end) { stop = true; break; }
                    var bytes = new byte[8];
                    Array.Copy(data, pos, bytes, 0, 8);
                    pos += 8;
                    fields.Add(new ProtoField(fieldNum, wireType, bytes));
                    break;
                }
                case 2: // length-delimited
                {
                    if (!TryReadVarint(data, ref pos, end, out ulong len)) { stop = true; break; }
                    int len32 = (int)len;
                    if (len32 < 0 || pos + len32 > end) { stop = true; break; }
                    var bytes = new byte[len32];
                    if (len32 > 0) Array.Copy(data, pos, bytes, 0, len32);
                    pos += len32;
                    fields.Add(new ProtoField(fieldNum, wireType, bytes));
                    break;
                }
                case 5: // fixed32 — little-endian
                {
                    if (pos + 4 > end) { stop = true; break; }
                    var bytes = new byte[4];
                    Array.Copy(data, pos, bytes, 0, 4);
                    pos += 4;
                    fields.Add(new ProtoField(fieldNum, wireType, bytes));
                    break;
                }
                default:
                    stop = true; // Unknown wire type — stop parsing this message
                    break;
            }
        }

        return fields;
    }

    private static bool TryReadVarint(byte[] data, ref int pos, int end, out ulong value)
    {
        value = 0;
        int shift = 0;
        while (pos < end)
        {
            byte b = data[pos++];
            value |= (ulong)(b & 0x7F) << shift;
            if ((b & 0x80) == 0) return true;
            shift += 7;
            if (shift >= 64) return false;
        }
        return false;
    }

    // ===== Type/assembly classification helpers =====

    private static bool IsHttpClientField(Type t) =>
        t == typeof(HttpClient) || typeof(HttpClient).IsAssignableFrom(t);

    private static bool NameMatchesCandidates(string name)
    {
        foreach (var kw in CandidateKeywords)
            if (name.Contains(kw, StringComparison.OrdinalIgnoreCase))
                return true;
        return false;
    }

    private static bool IsFrameworkAssembly(string name)
    {
        if (name.StartsWith("System",        StringComparison.OrdinalIgnoreCase)) return true;
        if (name.StartsWith("Microsoft",     StringComparison.OrdinalIgnoreCase)) return true;
        if (name.StartsWith("Avalonia",      StringComparison.OrdinalIgnoreCase)) return true;
        if (name.StartsWith("AvaloniaEdit",  StringComparison.OrdinalIgnoreCase)) return true;
        if (name.StartsWith("DotNetBrowser", StringComparison.OrdinalIgnoreCase)) return true;
        if (name.StartsWith("Uprooted",      StringComparison.OrdinalIgnoreCase)) return true;
        if (name.StartsWith("netstandard",   StringComparison.OrdinalIgnoreCase)) return true;
        if (name.StartsWith("mscorlib",      StringComparison.OrdinalIgnoreCase)) return true;
        return false;
    }

    private static bool IsFrameworkType(Type t)
    {
        var ns = t.Namespace ?? "";
        if (ns.StartsWith("System",    StringComparison.OrdinalIgnoreCase)) return true;
        if (ns.StartsWith("Microsoft", StringComparison.OrdinalIgnoreCase)) return true;
        if (ns.StartsWith("Avalonia",  StringComparison.OrdinalIgnoreCase)) return true;
        return false;
    }
}

/// <summary>
/// DelegatingHandler that tees CommunityLogGrpcService/List responses to AuditLogEngine
/// while forwarding them unmodified to Root's own HTTP pipeline.
/// </summary>
internal sealed class AuditLogInterceptorHandler : DelegatingHandler
{
    private readonly AuditLogEngine _engine;

    internal AuditLogInterceptorHandler(HttpMessageHandler inner, AuditLogEngine engine)
        : base(inner)
    {
        _engine = engine;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken ct)
    {
        var response = await base.SendAsync(request, ct);
        try
        {
            var path = request.RequestUri?.AbsolutePath ?? "";
            if (!path.Contains("CommunityLogGrpcService/List", StringComparison.Ordinal))
                return response;

            // Read the response body
            var body = await response.Content.ReadAsByteArrayAsync(ct);
            var contentType = response.Content.Headers.ContentType?.ToString();

            // Tee: replace response.Content with a fresh ByteArrayContent so Root
            // can still read it (HttpContent can only be read once).
            var replacement = new ByteArrayContent(body);
            if (!string.IsNullOrEmpty(contentType))
                replacement.Headers.TryAddWithoutValidation("Content-Type", contentType);
            response.Content = replacement;

            // Parse on a background thread — do not block Root's response pipeline
            var engine = _engine;
            _ = Task.Run(() =>
            {
                try { engine.ParseAuditLogResponse(body); }
                catch (Exception ex) { Logger.Log("AuditLog", $"ParseAuditLogResponse error: {ex.Message}"); }
            });
        }
        catch (Exception ex)
        {
            Logger.Log("AuditLog", $"AuditLogInterceptorHandler: {ex.Message}");
        }
        return response;
    }
}

/// <summary>Parsed audit log entry from CommunityLogGrpcService/List.</summary>
internal class AuditLogEntry
{
    /// <summary>Action type enum value — meaning TBD after Phase 2 calibration.</summary>
    public int ActionType = 0;

    /// <summary>Actor UUID string (who performed the action).</summary>
    public string ActorId = "";

    /// <summary>Actor display name — populated after Phase 2 when FieldActorName is known.</summary>
    public string ActorName = "";

    /// <summary>Community UUID string.</summary>
    public string CommunityId = "";

    /// <summary>Channel UUID string (where the action occurred).</summary>
    public string ChannelId = "";

    /// <summary>Message UUID string (the affected message, if applicable).</summary>
    public string MessageId = "";

    /// <summary>Raw timestamp value — scale (seconds/ms/µs) TBD after Phase 2.</summary>
    public long TimestampRaw = 0;

    /// <summary>Message content if preserved by server; empty otherwise.</summary>
    public string ExtraContent = "";
}
