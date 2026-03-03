using System.Reflection;
using Xunit;

namespace Uprooted.Tests;

/// <summary>
/// Tests for MessageStore — pipe-delimited flat-file persistence.
/// Creates real temp files; flushes the write buffer via reflection.
/// </summary>
[Collection("SequentialTests")]
public class MessageStoreTests : IDisposable
{
    private readonly string _tempDir;
    private readonly MessageStore _store;

    private static readonly MethodInfo FlushMethod =
        typeof(MessageStore).GetMethod("FlushBuffer", BindingFlags.NonPublic | BindingFlags.Instance)!;

    public MessageStoreTests()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), $"uprooted-msgstore-{Guid.NewGuid():N}");
        Directory.CreateDirectory(_tempDir);
        PlatformPaths.ProfileDirOverride = _tempDir;
        _store = new MessageStore();
    }

    public void Dispose()
    {
        try { Directory.Delete(_tempDir, recursive: true); } catch { }
    }

    private string LogFilePath => Path.Combine(_tempDir, "uprooted-message-log.dat");

    // Recent timestamp for file-level tests (LoadAll has a 2h cutoff)
    private static string RecentTimestamp => DateTime.UtcNow.AddMinutes(-5).ToString("O");

    private void Flush() => FlushMethod.Invoke(_store, new object?[] { null });

    private Dictionary<string, CachedMessage> LoadAll()
    {
        var cache = new Dictionary<string, CachedMessage>();
        _store.LoadAll(cache);
        return cache;
    }

    // ── MSG record ──

    [Fact]
    public void RecordMessage_FlushThenLoad_ReturnsCorrectFields()
    {
        var ts = DateTime.UtcNow.AddMinutes(-5);
        _store.RecordMessage("msg1", "ch1", "user1", "Alice", ts, "Hello world");
        Flush();

        var cache = LoadAll();
        Assert.True(cache.ContainsKey("msg1"));
        var msg = cache["msg1"];
        Assert.Equal("msg1", msg.MessageId);
        Assert.Equal("ch1", msg.ChannelId);
        Assert.Equal("user1", msg.AuthorId);
        Assert.Equal("Alice", msg.AuthorName);
        Assert.Equal(ts, msg.Timestamp);
        Assert.Equal("Hello world", msg.Content);
        Assert.False(msg.IsDeleted);
        Assert.Empty(msg.Edits);
    }

    [Fact]
    public void RecordMessage_MultipleMessages_AllLoaded()
    {
        var ts = DateTime.UtcNow;
        _store.RecordMessage("m1", "ch1", "u1", "Alice", ts, "First");
        _store.RecordMessage("m2", "ch1", "u2", "Bob", ts, "Second");
        _store.RecordMessage("m3", "ch2", "u1", "Alice", ts, "Third");
        Flush();

        var cache = LoadAll();
        Assert.Equal(3, cache.Count);
        Assert.True(cache.ContainsKey("m1"));
        Assert.True(cache.ContainsKey("m2"));
        Assert.True(cache.ContainsKey("m3"));
        Assert.Equal("ch2", cache["m3"].ChannelId);
    }

    // ── EDIT record ──

    [Fact]
    public void RecordEdit_MatchingMsgId_PopulatesEdits()
    {
        var ts = DateTime.UtcNow;
        var editTime = ts.AddSeconds(30);
        _store.RecordMessage("msg1", "ch1", "u1", "Alice", ts, "Original");
        _store.RecordEdit("msg1", "ch1", editTime, "Original");
        Flush();

        var cache = LoadAll();
        Assert.True(cache.ContainsKey("msg1"));
        Assert.Single(cache["msg1"].Edits);
        Assert.Equal(editTime, cache["msg1"].Edits[0].EditTime);
        Assert.Equal("Original", cache["msg1"].Edits[0].PreviousContent);
    }

    [Fact]
    public void RecordEdit_UnknownMsgId_SilentlySkipped()
    {
        var ts = DateTime.UtcNow;
        _store.RecordEdit("nonexistent", "ch1", ts, "content");
        Flush();

        var cache = LoadAll(); // should not throw
        Assert.Empty(cache); // no MSG record → empty cache (EDIT for unknown is discarded)
    }

    // ── DEL record ──

    [Fact]
    public void RecordDeletion_SetsIsDeletedAndDeletedAt()
    {
        var ts = DateTime.UtcNow;
        var delTime = ts.AddMinutes(5);
        _store.RecordMessage("msg1", "ch1", "u1", "Alice", ts, "Oops");
        _store.RecordDeletion("msg1", "ch1", delTime);
        Flush();

        var cache = LoadAll();
        Assert.True(cache["msg1"].IsDeleted);
        Assert.NotNull(cache["msg1"].DeletedAt);
        Assert.Equal(delTime, cache["msg1"].DeletedAt!.Value);
    }

    [Fact]
    public void RecordDeletion_UnknownMsgId_SilentlySkipped()
    {
        var ts = DateTime.UtcNow;
        _store.RecordDeletion("ghost", "ch1", ts);
        Flush();

        var cache = LoadAll(); // should not throw
        Assert.Empty(cache);
    }

    // ── CLR record ──

    [Fact]
    public void RecordClear_MessageAbsentFromCache()
    {
        var ts = DateTime.UtcNow;
        _store.RecordMessage("msg1", "ch1", "u1", "Alice", ts, "Will be cleared");
        _store.RecordClear("msg1");
        Flush();

        var cache = LoadAll();
        Assert.DoesNotContain("msg1", cache.Keys);
    }

    [Fact]
    public void RecordClear_UnknownMsgId_SilentlySkipped()
    {
        _store.RecordClear("doesnt-exist");
        Flush();

        var cache = LoadAll(); // should not throw
        Assert.Empty(cache);
    }

    // ── URI encoding roundtrip ──

    [Fact]
    public void UriEncoding_PipeInContent_EncodedAndDecoded()
    {
        var ts = DateTime.UtcNow;
        _store.RecordMessage("msg1", "ch1", "u1", "Alice", ts, "before|after");
        Flush();

        var cache = LoadAll();
        Assert.Equal("before|after", cache["msg1"].Content);
    }

    [Fact]
    public void UriEncoding_NewlineInContent_EncodedAndDecoded()
    {
        var ts = DateTime.UtcNow;
        _store.RecordMessage("msg1", "ch1", "u1", "Alice", ts, "line1\nline2");
        Flush();

        var cache = LoadAll();
        Assert.Equal("line1\nline2", cache["msg1"].Content);
    }

    [Fact]
    public void UriEncoding_UnicodeContent_EncodedAndDecoded()
    {
        var ts = DateTime.UtcNow;
        _store.RecordMessage("msg1", "ch1", "u1", "Alice", ts, "你好世界 🌍");
        Flush();

        var cache = LoadAll();
        Assert.Equal("你好世界 🌍", cache["msg1"].Content);
    }

    // ── Malformed line handling ──

    [Fact]
    public void MalformedLine_TooFewFields_Skipped()
    {
        // Write a valid header + a malformed MSG line directly to the file
        File.WriteAllText(LogFilePath,
            "# uprooted-message-log v1\n" +
            "MSG|only-two-parts\n");  // MSG needs >= 7 parts

        var cache = LoadAll();
        Assert.Empty(cache); // malformed line silently skipped
    }

    [Fact]
    public void LineStartingWithHash_Skipped()
    {
        File.WriteAllText(LogFilePath,
            "# uprooted-message-log v1\n" +
            "# This is a comment\n" +
            $"MSG|msg1|ch1|u1|Alice|{RecentTimestamp}|Hello\n");

        var cache = LoadAll();
        Assert.Single(cache); // only the valid MSG line
    }

    [Fact]
    public void BlankLines_Skipped()
    {
        File.WriteAllText(LogFilePath,
            "# uprooted-message-log v1\n" +
            "\n" +
            "   \n" +
            $"MSG|msg1|ch1|u1|Alice|{RecentTimestamp}|Hello\n");

        var cache = LoadAll();
        Assert.Single(cache);
    }

    // ── DateTime roundtrip ──

    [Fact]
    public void DateTime_RoundtripWithRoundtripFormat()
    {
        // Use a recent timestamp with sub-second precision (LoadAll has a 2h cutoff)
        var ts = DateTime.UtcNow.AddMinutes(-5);
        _store.RecordMessage("msg1", "ch1", "u1", "Alice", ts, "Precise time");
        Flush();

        var cache = LoadAll();
        var loaded = cache["msg1"].Timestamp;
        // Compare to millisecond precision (the 'O' format preserves all sub-seconds)
        Assert.Equal(ts, loaded);
        Assert.Equal(DateTimeKind.Utc, loaded.Kind);
    }

    // ── Truncate ──

    [Fact]
    public void Truncate_WhenMoreThanMax_KeepsNewestLines()
    {
        // Write 5 MSG lines directly to the file (recent timestamps for 2h cutoff)
        var ts = RecentTimestamp;
        var lines = new List<string> { "# uprooted-message-log v1" };
        for (int i = 1; i <= 5; i++)
            lines.Add($"MSG|msg{i}|ch1|u1|Alice|{ts}|Message {i}");
        File.WriteAllText(LogFilePath, string.Join("\n", lines) + "\n");

        _store.Truncate(3);

        // Load and check only the last 3 messages remain
        var cache = LoadAll();
        Assert.Equal(3, cache.Count);
        Assert.True(cache.ContainsKey("msg3"));
        Assert.True(cache.ContainsKey("msg4"));
        Assert.True(cache.ContainsKey("msg5"));
        Assert.False(cache.ContainsKey("msg1"));
        Assert.False(cache.ContainsKey("msg2"));
    }

    [Fact]
    public void Truncate_WhenAtOrBelowMax_NoOp()
    {
        var ts = RecentTimestamp;
        var lines = new List<string> { "# uprooted-message-log v1" };
        for (int i = 1; i <= 3; i++)
            lines.Add($"MSG|msg{i}|ch1|u1|Alice|{ts}|Message {i}");
        var originalContent = string.Join("\n", lines) + "\n";
        File.WriteAllText(LogFilePath, originalContent);

        _store.Truncate(3); // exactly at limit

        var afterContent = File.ReadAllText(LogFilePath);
        Assert.Equal(originalContent, afterContent); // file unchanged
    }

    [Fact]
    public void Truncate_WhenBelowMax_NoOp()
    {
        var ts = RecentTimestamp;
        var lines = new List<string> { "# uprooted-message-log v1" };
        for (int i = 1; i <= 2; i++)
            lines.Add($"MSG|msg{i}|ch1|u1|Alice|{ts}|Message {i}");
        var originalContent = string.Join("\n", lines) + "\n";
        File.WriteAllText(LogFilePath, originalContent);

        _store.Truncate(10); // way above limit

        var afterContent = File.ReadAllText(LogFilePath);
        Assert.Equal(originalContent, afterContent); // file unchanged
    }

    [Fact]
    public void Truncate_HeaderPreservedAfterTruncation()
    {
        var ts = RecentTimestamp;
        var lines = new List<string> { "# uprooted-message-log v1" };
        for (int i = 1; i <= 5; i++)
            lines.Add($"MSG|msg{i}|ch1|u1|Alice|{ts}|Message {i}");
        File.WriteAllText(LogFilePath, string.Join("\n", lines) + "\n");

        _store.Truncate(2);

        var resultLines = File.ReadAllLines(LogFilePath);
        Assert.Equal("# uprooted-message-log v1", resultLines[0]);
    }

    [Fact]
    public void Truncate_FileDoesNotExist_NoOp()
    {
        // No file created → Truncate should not throw
        _store.Truncate(100);
        Assert.False(File.Exists(LogFilePath));
    }
}
