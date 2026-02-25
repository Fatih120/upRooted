using System;
using System.Collections.Generic;
using System.Threading;

namespace Uprooted;

/// <summary>
/// Focus Mode: readability-first plugin that hides visual clutter (media, embeds,
/// reactions, typing indicators, notification badges) for a clean reading experience.
///
/// Architecture: timer-based visual tree scan (500ms) with non-destructive suppression.
/// All changes are reversible via SetIsVisible(true) on disable.
///
/// Future enhancements:
/// - Per-channel presets (v1.1)
/// - Quick toggle hotkey (when hotkey infra exists)
/// - Compact mode variants (denser spacing)
/// - Allowlist for specific media types (e.g., keep images, hide videos)
/// </summary>
internal sealed class FocusModeEngine : IDisposable
{
    private const string Tag = "FocusMode";
    private const int ScanIntervalMs = 500;

    // Tag prefix for all Focus Mode suppression markers
    private const string TagPrefix = "uprooted-focus-";
    private const string HiddenTag = "uprooted-focus-hidden";
    private const string PlaceholderTag = "uprooted-focus-placeholder";

    internal static FocusModeEngine? Instance { get; set; }

    private readonly AvaloniaReflection _r;
    private readonly object _mainWindow;

    private Timer? _scanTimer;
    private int _scanPosted;  // Interlocked: 1 if a scan is already queued on the UI thread
    private bool _disposed;

    // Track hidden controls for clean revert: (original control, original tag, placeholder if any, parent)
    private readonly List<(object control, string? originalTag, object? placeholder, object? parent)> _hiddenControls = new();
    private readonly object _lock = new();

    internal FocusModeEngine(AvaloniaReflection resolver, object mainWindow)
    {
        _r = resolver;
        _mainWindow = mainWindow;
    }

    internal void Initialize()
    {
        var settings = UprootedSettings.Load();
        if (!settings.Plugins.TryGetValue("focus-mode", out var enabled) || !enabled)
        {
            Logger.Log(Tag, "Focus Mode initialized (disabled, dormant)");
            return;
        }

        StartScanning();
        Logger.Log(Tag, "Focus Mode initialized and active");
    }

    /// <summary>
    /// Called when the plugin toggle changes or settings change.
    /// Applies or reverts focus mode immediately.
    /// </summary>
    internal void UpdateConfig()
    {
        var settings = UprootedSettings.Load();
        if (!settings.Plugins.TryGetValue("focus-mode", out var enabled) || !enabled)
        {
            StopScanning();
            _r.RunOnUIThread(RevertAll);
            Logger.Log(Tag, "Focus Mode disabled, reverted all suppressions");
        }
        else
        {
            // Revert all first, then re-apply with new settings.
            // This handles category toggles correctly: if HideMedia was ON and is now OFF,
            // the revert unhides media, and the re-apply skips them.
            _r.RunOnUIThread(() =>
            {
                RevertAll();
                ScanAndApply();
            });
            StartScanning();
            Logger.Log(Tag, "Focus Mode settings updated, re-applying");
        }
    }

    /// <summary>
    /// Apply focus mode suppressions. Called from UI thread.
    /// </summary>
    internal void Apply()
    {
        StartScanning();
        ScanAndApply();
    }

    /// <summary>
    /// Revert all focus mode suppressions. Called from UI thread.
    /// </summary>
    internal void Revert()
    {
        StopScanning();
        RevertAll();
    }

    private void StartScanning()
    {
        if (_scanTimer != null) return;
        _scanTimer = new Timer(OnScanTick, null, ScanIntervalMs, ScanIntervalMs);
    }

    private void StopScanning()
    {
        var timer = _scanTimer;
        _scanTimer = null;
        timer?.Dispose();
    }

    private void OnScanTick(object? state)
    {
        if (_disposed) return;

        var settings = UprootedSettings.Load();
        if (!settings.Plugins.TryGetValue("focus-mode", out var enabled) || !enabled) return;

        // Only queue one scan at a time
        if (Interlocked.CompareExchange(ref _scanPosted, 1, 0) != 0) return;

        try
        {
            _r.RunOnUIThread(() =>
            {
                try { ScanAndApply(); }
                finally { Interlocked.Exchange(ref _scanPosted, 0); }
            });
        }
        catch (Exception ex)
        {
            Interlocked.Exchange(ref _scanPosted, 0);
            Logger.Log(Tag, $"OnScanTick error: {ex.Message}");
        }
    }

    /// <summary>
    /// DFS walk from mainWindow; suppress visual clutter based on current settings.
    /// Must run on the UI thread. Idempotent: safe to call multiple times.
    /// </summary>
    private void ScanAndApply()
    {
        try
        {
            var settings = UprootedSettings.Load();
            if (!settings.Plugins.TryGetValue("focus-mode", out var enabled) || !enabled) return;

            var walker = new VisualTreeWalker(_r);

            foreach (var node in walker.DescendantsDepthFirst(_mainWindow))
            {
                // Skip nodes already processed by Focus Mode
                var tag = _r.GetTag(node);
                if (tag != null && tag.StartsWith(TagPrefix)) continue;

                var typeName = node.GetType().Name;

                // === Category 1: Media previews ===
                if (settings.FocusModeHideMedia)
                {
                    if (IsMediaControl(typeName))
                    {
                        SuppressControl(node, settings.FocusModeShowPlaceholders, "[Media hidden by Focus Mode]");
                        continue;
                    }
                }

                // === Category 2: Embeds (LinkEmbed cards + native link previews) ===
                if (settings.FocusModeHideEmbeds)
                {
                    // Uprooted LinkEmbed cards (tagged)
                    if (tag != null && tag.StartsWith("uprooted-link-embed:"))
                    {
                        SuppressControl(node, settings.FocusModeShowPlaceholders, "[Embed hidden by Focus Mode]");
                        continue;
                    }

                    // Native link message views
                    if (typeName == "LinkMessageView")
                    {
                        SuppressControl(node, settings.FocusModeShowPlaceholders, "[Embed hidden by Focus Mode]");
                        continue;
                    }
                }

                // === Category 3: Reactions ===
                if (settings.FocusModeHideReactions)
                {
                    if (IsReactionControl(node, typeName, tag))
                    {
                        SuppressControl(node, false, null); // Reactions: just hide, no placeholder
                        continue;
                    }
                }

                // === Category 4: Typing indicators ===
                if (settings.FocusModeHideTyping)
                {
                    if (typeName == "TypingIndicatorView")
                    {
                        SuppressControl(node, false, null); // Just hide, no placeholder
                        continue;
                    }
                }

                // === Category 5: Notification badges (visual only) ===
                if (settings.FocusModeHideBadges)
                {
                    if (IsNotificationBadge(node, typeName))
                    {
                        SuppressControl(node, false, null);
                        continue;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"ScanAndApply error: {ex.Message}");
        }
    }

    /// <summary>
    /// Suppress a control by hiding it and optionally injecting a placeholder.
    /// Non-destructive: the control is hidden via IsVisible=false, not removed.
    /// </summary>
    private void SuppressControl(object control, bool showPlaceholder, string? placeholderText)
    {
        try
        {
            // Already hidden by us? Skip.
            if (!_r.GetIsVisible(control)) return;

            // Preserve original tag before overwriting
            var originalTag = _r.GetTag(control);

            _r.SetIsVisible(control, false);
            _r.SetTag(control, HiddenTag);

            object? placeholder = null;
            var parent = GetVisualParent(control);

            if (showPlaceholder && !string.IsNullOrEmpty(placeholderText))
            {
                placeholder = CreatePlaceholder(placeholderText);
                if (placeholder != null && parent != null && IsPanelOrGrid(parent))
                {
                    try
                    {
                        _r.AddChild(parent, placeholder);
                    }
                    catch
                    {
                        placeholder = null; // Failed to insert, don't track
                    }
                }
                else
                {
                    placeholder = null;
                }
            }

            lock (_lock)
            {
                _hiddenControls.Add((control, originalTag, placeholder, parent));
            }
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"SuppressControl error: {ex.Message}");
        }
    }

    /// <summary>
    /// Create a compact placeholder TextBlock for hidden content.
    /// Small, muted, no extra padding, consistent with placeholder examples.
    /// </summary>
    private object? CreatePlaceholder(string text)
    {
        try
        {
            var placeholder = _r.CreateTextBlock(text, 11, "#70808090");
            if (placeholder != null)
            {
                _r.SetTag(placeholder, PlaceholderTag);
                _r.SetMargin(placeholder, 0, 2, 0, 2);
            }
            return placeholder;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Revert all focus mode suppressions: unhide controls, remove placeholders.
    /// Must run on UI thread.
    /// </summary>
    private void RevertAll()
    {
        try
        {
            List<(object control, string? originalTag, object? placeholder, object? parent)> toRevert;
            lock (_lock)
            {
                toRevert = new List<(object, string?, object?, object?)>(_hiddenControls);
                _hiddenControls.Clear();
            }

            foreach (var (control, originalTag, placeholder, parent) in toRevert)
            {
                try
                {
                    // Restore visibility
                    _r.SetIsVisible(control, true);

                    // Restore original tag (or clear if there was none)
                    var currentTag = _r.GetTag(control);
                    if (currentTag == HiddenTag)
                        _r.SetTag(control, originalTag!);

                    // Remove placeholder
                    if (placeholder != null && parent != null)
                    {
                        try { _r.RemoveChild(parent, placeholder); }
                        catch { /* Parent may have changed */ }
                    }
                }
                catch { /* Control may have been GC'd or removed from tree */ }
            }

            // Also do a sweep to catch any controls we might have missed in tracking
            // (e.g., from VirtualizingStackPanel recycling)
            SweepOrphanedTags();
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"RevertAll error: {ex.Message}");
        }
    }

    /// <summary>
    /// Walk the visual tree and restore any controls that still have our hidden tag
    /// but weren't in the tracking list (e.g., recycled by virtualization).
    /// </summary>
    private void SweepOrphanedTags()
    {
        try
        {
            var walker = new VisualTreeWalker(_r);
            var toRestore = new List<object>();
            var toRemove = new List<(object parent, object child)>();

            foreach (var node in walker.DescendantsDepthFirst(_mainWindow))
            {
                var tag = _r.GetTag(node);
                if (tag == HiddenTag)
                    toRestore.Add(node);
                else if (tag == PlaceholderTag)
                {
                    var parent = GetVisualParent(node);
                    if (parent != null)
                        toRemove.Add((parent, node));
                }
            }

            foreach (var node in toRestore)
            {
                _r.SetIsVisible(node, true);
                _r.SetTag(node, null!);
            }

            foreach (var (parent, child) in toRemove)
            {
                try { _r.RemoveChild(parent, child); }
                catch { }
            }
        }
        catch (Exception ex)
        {
            Logger.Log(Tag, $"SweepOrphanedTags error: {ex.Message}");
        }
    }

    // === Control identification helpers ===

    /// <summary>
    /// Check if a control is a media preview (image, video, GIF).
    /// Based on Root's message view structure: Row 3 contains media ItemsControl.
    /// </summary>
    private static bool IsMediaControl(string typeName)
    {
        return typeName == "GifMessageView"
            || typeName == "ImageMessageView"
            || typeName == "VideoMessageView";
    }

    /// <summary>
    /// Check if a control is a reaction row or reaction-related control.
    /// Reactions are in Row 6 of the message Grid as an ItemsControl.
    /// We target AddReactionView and its parent containers.
    /// </summary>
    private bool IsReactionControl(object node, string typeName, string? tag)
    {
        // AddReactionView is the "add reaction" button
        if (typeName == "AddReactionView") return true;

        // WhoReacted engine output
        if (tag != null && tag.StartsWith("uprooted-whoreacted:")) return true;

        // ReactionView is the individual reaction pill
        if (typeName == "ReactionView") return true;

        return false;
    }

    /// <summary>
    /// Check if a control is a notification badge (dot badge, count badge, unread indicator).
    /// Conservative approach: only target well-known badge patterns.
    /// </summary>
    private bool IsNotificationBadge(object node, string typeName)
    {
        // Root uses Ellipse controls for dot badges in channel list
        // and small Border controls for count badges.
        // Be conservative: only hide if the control is small and looks like a badge.

        if (_r.EllipseType != null && node.GetType() == _r.EllipseType)
        {
            // Small ellipses (< 16px) in channel-related containers are likely badges
            try
            {
                var width = node.GetType().GetProperty("Width")?.GetValue(node);
                if (width is double w && w > 0 && w <= 14)
                    return true;
            }
            catch { }
        }

        // "New messages" divider bar in chat
        if (typeName == "NewMessagesDividerView")
            return true;

        return false;
    }

    /// <summary>
    /// Get the visual parent of a control.
    /// </summary>
    private object? GetVisualParent(object control)
    {
        try
        {
            return control.GetType().GetProperty("Parent")?.GetValue(control)
                ?? control.GetType().GetProperty("VisualParent")?.GetValue(control);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Check if a control is a Panel or Grid (can have children added/removed).
    /// </summary>
    private bool IsPanelOrGrid(object control)
    {
        if (_r.PanelType != null && _r.PanelType.IsAssignableFrom(control.GetType())) return true;
        if (_r.GridType != null && _r.GridType.IsAssignableFrom(control.GetType())) return true;
        return false;
    }

    public void Dispose()
    {
        _disposed = true;
        StopScanning();
    }
}
