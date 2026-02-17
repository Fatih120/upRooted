namespace Uprooted;

/// <summary>
/// Injects link-embeds JavaScript into the DotNetBrowser main frame.
/// Follows the NsfwFilter pattern: find BrowserView, get IBrowser/IFrame,
/// ExecuteJavaScript, periodic re-injection with JS self-guard.
///
/// Supports partial resolution: BrowserView may not be known at type-scan time.
/// The visual tree walk in DotNetBrowserReflection.FindBrowserView handles this
/// by searching for any control with a Browser property returning IBrowser.
/// ExecuteJavaScript supports deferred resolution from the runtime frame type.
/// </summary>
internal class LinkEmbedInjector : IDisposable
{
    private const int ReinjectionIntervalMs = 30_000;

    private readonly AvaloniaReflection _avaloniaReflection;
    private readonly DotNetBrowserReflection _browserReflection;
    private readonly UprootedSettings _settings;
    private readonly object _mainWindow;

    private Timer? _reinjectionTimer;
    private object? _lastFrame;
    private string? _embedScript;
    private bool _disposed;

    internal LinkEmbedInjector(AvaloniaReflection avaloniaReflection,
        DotNetBrowserReflection browserReflection,
        UprootedSettings settings,
        object mainWindow)
    {
        _avaloniaReflection = avaloniaReflection;
        _browserReflection = browserReflection;
        _settings = settings;
        _mainWindow = mainWindow;
    }

    internal bool Initialize()
    {
        try
        {
            _embedScript = LoadScript();
            if (_embedScript == null)
            {
                Logger.Log("LinkEmbeds", "Script not found, aborting");
                return false;
            }

            bool injected = false;
            _avaloniaReflection.RunOnUIThread(() =>
            {
                injected = TryInject();
            });

            Thread.Sleep(2000);

            _reinjectionTimer = new Timer(OnReinjectionTick, null, ReinjectionIntervalMs, ReinjectionIntervalMs);
            Logger.Log("LinkEmbeds", "Re-injection timer started (30s interval)");

            return true;
        }
        catch (Exception ex)
        {
            Logger.Log("LinkEmbeds", $"Initialize error: {ex}");
            return false;
        }
    }

    private bool TryInject()
    {
        try
        {
            // FindBrowserView handles both known-type and visual-tree-walk fallback
            var browserView = _browserReflection.FindBrowserView(_avaloniaReflection, _mainWindow);
            if (browserView == null)
            {
                Logger.Log("LinkEmbeds", "BrowserView not found in visual tree");
                return false;
            }
            Logger.Log("LinkEmbeds", $"BrowserView found: {browserView.GetType().FullName}");

            // GetBrowser handles runtime property discovery if not cached
            var browser = _browserReflection.GetBrowser(browserView);
            if (browser == null)
            {
                Logger.Log("LinkEmbeds", "IBrowser not available from BrowserView");
                return false;
            }

            var frame = _browserReflection.GetMainFrame(browser);
            if (frame == null)
            {
                Logger.Log("LinkEmbeds", "MainFrame not available from IBrowser");
                return false;
            }
            _lastFrame = frame;
            Logger.Log("LinkEmbeds", $"MainFrame acquired: {frame.GetType().FullName}");

            // ExecuteJavaScript supports deferred resolution — it will search the
            // runtime frame type if the method wasn't found at type-scan time
            if (!_browserReflection.ExecuteJavaScript(frame, _embedScript!))
            {
                Logger.Log("LinkEmbeds", "Failed to inject script (ExecuteJavaScript returned false)");
                return false;
            }
            Logger.Log("LinkEmbeds", "Script injected successfully");

            return true;
        }
        catch (Exception ex)
        {
            Logger.Log("LinkEmbeds", $"TryInject error: {ex.Message}");
            return false;
        }
    }

    private void OnReinjectionTick(object? state)
    {
        if (_disposed) return;

        // Check if plugin is still enabled
        var currentSettings = UprootedSettings.Load();
        if (currentSettings.Plugins.TryGetValue("link-embeds", out var enabled) && !enabled)
            return;

        try
        {
            _avaloniaReflection.RunOnUIThread(() =>
            {
                try
                {
                    if (_lastFrame != null)
                    {
                        // Re-inject -- JS self-guard prevents duplicates
                        if (_embedScript != null)
                        {
                            if (!_browserReflection.ExecuteJavaScript(_lastFrame, _embedScript))
                            {
                                // Frame may have been invalidated — try fresh injection next tick
                                Logger.Log("LinkEmbeds", "Re-injection failed on cached frame, clearing");
                                _lastFrame = null;
                            }
                        }
                    }
                    else
                    {
                        TryInject();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log("LinkEmbeds", $"Re-injection error: {ex.Message}");
                    _lastFrame = null;
                }
            });
        }
        catch (Exception ex)
        {
            Logger.Log("LinkEmbeds", $"OnReinjectionTick error: {ex.Message}");
        }
    }

    private static string? LoadScript()
    {
        try
        {
            var hookDir = Path.GetDirectoryName(typeof(LinkEmbedInjector).Assembly.Location);
            if (hookDir != null)
            {
                var scriptPath = Path.Combine(hookDir, "link-embeds.js");
                if (File.Exists(scriptPath))
                {
                    Logger.Log("LinkEmbeds", $"Loading script from: {scriptPath}");
                    return File.ReadAllText(scriptPath);
                }
            }

            var uprootedDir = PlatformPaths.GetUprootedDir();
            var fallbackPath = Path.Combine(uprootedDir, "link-embeds.js");
            if (File.Exists(fallbackPath))
            {
                Logger.Log("LinkEmbeds", $"Loading script from fallback: {fallbackPath}");
                return File.ReadAllText(fallbackPath);
            }

            Logger.Log("LinkEmbeds", "Script not found in hook dir or uprooted dir");
            return null;
        }
        catch (Exception ex)
        {
            Logger.Log("LinkEmbeds", $"LoadScript error: {ex.Message}");
            return null;
        }
    }

    public void Dispose()
    {
        _disposed = true;
        _reinjectionTimer?.Dispose();
        _reinjectionTimer = null;
    }
}
