# Next Release

> Changes since v0.4.1. This file is replaced each release.

## SilentTyping: DiagnosticListener rewrite (by Kurumi Nanase)

- **Replaced** the 482-line HttpClient/GrpcChannel handler injection with ~90-line DiagnosticListener approach.
- Subscribes to .NET's built-in HTTP diagnostics (`DiagnosticListener.AllListeners`) — fires for ALL outbound requests regardless of which HttpClient or GrpcChannel makes the call.
- Intercepts `System.Net.Http.HttpRequestOut.Start` events, redirects SetTypingIndicator to `localhost:0` (silently fails).
- No discovery, no field walking, no handler patching — clean and maintainable.
- Respects `UprootedSettings` enable/disable toggle (checked per-request).
- Case-insensitive URL matching.

## Plugin descriptions: remove em dashes

- Stripped em dashes from SentryBlocker, Themes, ClearURLs, and ContentFilter plugin descriptions in ContentPages.cs for cleaner copy.

## MessageLogger: major reliability overhaul

- **Property resolution fix**: Root's Message model uses `DeletedAt` (DateTimeOffset?) and `EditedAt` (DateTimeOffset?), not `HasBeenDeleted`/`IsDeleted` (bool). Updated `FindProp` search order and all type checks to handle nullable DateTimeOffset (non-null = deleted/edited).
- **Author name resolution**: Replaced hardcoded "Unknown" with deep property chain traversal: `Message.SenderMember` → `.GlobalUser` → `.UserName` (with `DisplayName`/`Name` fallbacks).
- **Event-driven detection (INotifyPropertyChanged)**: Subscribes to `Message.PropertyChanged` for instant `DeletedAt`/`EditedAt` change detection via `Expression.Lambda` delegate. Primary detection method; polling retained as fallback.
- **Self-delete fallback**: Root doesn't set `DeletedAt` for client-initiated deletes (only removes from collection). After 3s polling timeout, checks if message is absent from live collection — if gone, marks as self-deleted.
- **Dedup already-deleted messages**: `HandleRemoved` skips spawning new pollers for messages already marked as deleted in cache (prevents 20+ duplicate poller spam).
- **Improved edit detection**: Dual-strategy — primary: check `EditedAt` non-null on Replace event; fallback: 5s grace period gating. Re-subscribes PropertyChanged on Replace.
- **Scan interval reduced**: 1500ms → 500ms for faster VirtualizingStackPanel recycling recovery.
- **Expanded diagnostics**: `DumpBooleanProperties` now also dumps `DateTimeOffset?` properties (discovers DeletedAt/EditedAt at runtime).
- **Epoch cleanup**: `_boolDumpCount` reset on channel switch; `UnsubscribeAllPropertyChanged()` on epoch change prevents memory leaks.
