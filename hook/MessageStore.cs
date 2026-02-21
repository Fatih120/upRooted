namespace Uprooted;

/// <summary>
/// In-memory representation of a logged chat message.
/// </summary>
internal class CachedMessage
{
    public string MessageId = "";
    public string ChannelId = "";
    public string AuthorId = "";
    public string AuthorName = "";
    public DateTime Timestamp;
    public string Content = "";
    public List<MessageEdit> Edits = new();
    public DateTime? DeletedAt;
    public bool IsDeleted;
    /// <summary>UUID of the user who deleted the message (from audit log). Empty if unknown.</summary>
    public string DeletedBy = "";
    /// <summary>Display name of the user who deleted the message (from audit log). Empty if unknown.</summary>
    public string DeletedByName = "";
}

internal class MessageEdit
{
    public DateTime EditTime;
    public string PreviousContent = "";
}

/// <summary>
/// Flat-file persistence for message logger data.
/// Pipe-delimited format with URI-encoded fields.
/// Append-only writes via buffered flush every 5 seconds.
///
/// File location: {profileDir}/uprooted-message-log.dat
/// Format:
///   # uprooted-message-log v1
///   MSG|msgId|channelId|authorId|authorName|timestamp|content
///   EDIT|msgId|channelId|editTimestamp|previousContent
///   DEL|msgId|channelId|deleteTimestamp[|actorId][|actorName]  ← actorId/actorName optional
/// </summary>
internal class MessageStore : IDisposable
{
    private const string Tag = "MsgStore";
    private const string FileName = "uprooted-message-log.dat";
    private const string FileHeader = "# uprooted-message-log v1";

    // Single lock for both buffer mutations AND file I/O so Truncate and FlushBuffer
    // can't interleave (Truncate reads all lines then writes; a concurrent Flush would
    // append new lines that Truncate then overwrites, silently dropping them).
    private static readonly object WriteLock = new();

    private readonly string _filePath;
    private readonly List<string> _writeBuffer = new();
    private Timer? _flushTimer;

    internal MessageStore()
    {
        var profileDir = PlatformPaths.GetProfileDir();
        _filePath = Path.Combine(profileDir, FileName);
        _flushTimer = new Timer(FlushBuffer, null, 5000, 5000);
    }

    public void Dispose()
    {
        var t = _flushTimer;
        _flushTimer = null;
        t?.Dispose();
        // Flush any remaining buffered writes before shutting down.
        FlushBuffer(null);
    }

    internal void RecordMessage(string msgId, string channelId, string authorId,
        string authorName, DateTime ts, string content)
    {
        var line = $"MSG|{Enc(msgId)}|{Enc(channelId)}|{Enc(authorId)}|{Enc(authorName)}|{ts:O}|{Enc(content)}";
        lock (WriteLock)
        {
            _writeBuffer.Add(line);
        }
    }

    internal void RecordEdit(string msgId, string channelId, DateTime editTime, string previousContent)
    {
        var line = $"EDIT|{Enc(msgId)}|{Enc(channelId)}|{editTime:O}|{Enc(previousContent)}";
        lock (WriteLock)
        {
            _writeBuffer.Add(line);
        }
    }

    internal void RecordDeletion(string msgId, string channelId, DateTime deleteTime,
        string actorId = "", string actorName = "")
    {
        // Actor fields are optional — only appended when non-empty (from audit log).
        // LoadAll reads parts[4]/parts[5] conditionally so old records without them still load.
        var line = string.IsNullOrEmpty(actorId)
            ? $"DEL|{Enc(msgId)}|{Enc(channelId)}|{deleteTime:O}"
            : $"DEL|{Enc(msgId)}|{Enc(channelId)}|{deleteTime:O}|{Enc(actorId)}|{Enc(actorName)}";
        lock (WriteLock)
        {
            _writeBuffer.Add(line);
        }
    }

    internal void RecordClear(string msgId)
    {
        var line = $"CLR|{Enc(msgId)}";
        lock (WriteLock)
        {
            _writeBuffer.Add(line);
        }
    }

    /// <summary>
    /// Load all records from disk into the in-memory cache.
    /// </summary>
    internal void LoadAll(Dictionary<string, CachedMessage> cache)
    {
        if (!File.Exists(_filePath)) return;

        try
        {
            foreach (var line in File.ReadAllLines(_filePath))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;

                var parts = line.Split('|');
                if (parts.Length < 2) continue;

                switch (parts[0])
                {
                    case "MSG" when parts.Length >= 7:
                        // Skip record if timestamp is unparseable rather than inventing UtcNow,
                        // which would make old messages falsely appear as current.
                        if (!DateTime.TryParse(parts[5], null,
                                System.Globalization.DateTimeStyles.RoundtripKind, out var ts))
                        {
                            Logger.Log(Tag, $"Skipping MSG with bad timestamp: {parts[5]}");
                            break;
                        }
                        var msg = new CachedMessage
                        {
                            MessageId = Dec(parts[1]),
                            ChannelId = Dec(parts[2]),
                            AuthorId = Dec(parts[3]),
                            AuthorName = Dec(parts[4]),
                            Timestamp = ts,
                            Content = Dec(parts[6])
                        };
                        cache[msg.MessageId] = msg;
                        break;

                    case "EDIT" when parts.Length >= 5:
                    {
                        var editMsgId = Dec(parts[1]);
                        if (cache.TryGetValue(editMsgId, out var editMsg))
                        {
                            if (!DateTime.TryParse(parts[3], null,
                                    System.Globalization.DateTimeStyles.RoundtripKind, out var et))
                            {
                                Logger.Log(Tag, $"Skipping EDIT with bad timestamp: {parts[3]}");
                                break;
                            }
                            editMsg.Edits.Add(new MessageEdit
                            {
                                EditTime = et,
                                PreviousContent = Dec(parts[4])
                            });
                        }
                        break;
                    }

                    case "DEL" when parts.Length >= 4:
                    {
                        var delMsgId = Dec(parts[1]);
                        if (cache.TryGetValue(delMsgId, out var delMsg))
                        {
                            if (!DateTime.TryParse(parts[3], null,
                                    System.Globalization.DateTimeStyles.RoundtripKind, out var dt))
                            {
                                Logger.Log(Tag, $"Skipping DEL with bad timestamp: {parts[3]}");
                                break;
                            }
                            delMsg.IsDeleted = true;
                            delMsg.DeletedAt = dt;
                            if (parts.Length >= 5) delMsg.DeletedBy = Dec(parts[4]);
                            if (parts.Length >= 6) delMsg.DeletedByName = Dec(parts[5]);
                        }
                        break;
                    }

                    case "CLR" when parts.Length >= 2:
                    {
                        var clrMsgId = Dec(parts[1]);
                        cache.Remove(clrMsgId);
                        break;
                    }
                }
            }

            Logger.Log(Tag, $"Loaded {cache.Count} messages from {_filePath}");
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"Load error: {ex.Message}");
        }
    }

    /// <summary>
    /// Truncate the log file to keep only the most recent maxMessages entries.
    /// Called on startup when cache exceeds the configured limit.
    /// </summary>
    internal void Truncate(int maxMessages)
    {
        if (!File.Exists(_filePath)) return;

        // Hold WriteLock for the full read+write cycle so FlushBuffer can't append
        // lines that would be silently overwritten by our WriteAllText at the end.
        lock (WriteLock)
        {
            try
            {
                var allLines = File.ReadAllLines(_filePath);
                var dataLines = new List<string>();
                foreach (var l in allLines)
                {
                    if (!string.IsNullOrWhiteSpace(l) && !l.StartsWith("#"))
                        dataLines.Add(l);
                }

                if (dataLines.Count <= maxMessages) return;

                // Keep only the newest entries
                var kept = new List<string> { FileHeader };
                for (int i = dataLines.Count - maxMessages; i < dataLines.Count; i++)
                    kept.Add(dataLines[i]);

                File.WriteAllText(_filePath, string.Join("\n", kept) + "\n");
                Logger.Log(Tag, $"Truncated: {dataLines.Count} -> {maxMessages} entries");
            }
            catch (Exception ex)
            {
                Logger.Log(Tag, $"Truncate error: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Flush buffered writes to disk. Called by timer every 5 seconds.
    /// WriteLock is held for the entire operation so Truncate() can't interleave.
    /// </summary>
    private void FlushBuffer(object? state)
    {
        lock (WriteLock)
        {
            if (_writeBuffer.Count == 0) return;

            var toWrite = new List<string>(_writeBuffer);
            _writeBuffer.Clear();

            try
            {
                // Ensure file exists with header
                if (!File.Exists(_filePath))
                    File.WriteAllText(_filePath, FileHeader + "\n");

                File.AppendAllText(_filePath, string.Join("\n", toWrite) + "\n");
            }
            catch (Exception ex)
            {
                Logger.Log(Tag, $"Flush error: {ex.Message}");
            }
        }
    }

    private static string Enc(string s) => Uri.EscapeDataString(s);
    private static string Dec(string s) => Uri.UnescapeDataString(s);
}
