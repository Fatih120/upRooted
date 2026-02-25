using System;

namespace Uprooted;

/// <summary>
/// MessageDrafts+: auto-save unsent message drafts per channel, DM, and thread.
/// Status: Planned (stub). Toggle available in developer channel for testing.
///
/// TODO: Future implementation phases:
/// 1. Per-conversation draft persistence (key by channel/DM/thread ID)
/// 2. Auto-save on composer text changes (debounced writes)
/// 3. Restore draft on composer mount (optional confirmation toast)
/// 4. Draft history (last N snapshots per conversation)
/// 5. Cleanup/expiration (remove stale drafts after configurable days)
/// 6. Import/export (JSON backup)
/// 7. Privacy: local storage only, no network sync
/// </summary>
internal sealed class MessageDraftsEngine
{
    private const string Tag = "MessageDrafts";

    internal static MessageDraftsEngine? Instance { get; set; }

    private readonly AvaloniaReflection _r;
    private readonly object _mainWindow;

    internal MessageDraftsEngine(AvaloniaReflection resolver, object mainWindow)
    {
        _r = resolver;
        _mainWindow = mainWindow;
    }

    internal void Initialize()
    {
        Logger.Log(Tag, "MessageDrafts+ initialized (planned stub, no active behavior)");
    }
}
