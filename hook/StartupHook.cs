using Uprooted;

/// <summary>
/// .NET Startup Hook entry point for Uprooted.
/// Must be: internal class StartupHook (no namespace) with public static void Initialize().
/// Loaded via DOTNET_STARTUP_HOOKS env var before Root's Main() runs.
/// </summary>
internal class StartupHook
{
    private const string CurrentVersion = "0.3.6-rc";

    // Version migration: plugins to force-disable when upgrading to (or through) a given version.
    // Users who skip versions get all intermediate entries applied cumulatively.
    // Omit a version entirely to leave all user plugin toggles untouched for that release.
    private static readonly Dictionary<string, string[]> ForceDisableOnUpgrade = new()
    {
        // { "0.4.0", new[] { "message-logger", "silent-typing" } },
    };

    // Static reference keeps FileSystemWatcher alive for process lifetime
    private static HtmlPatchVerifier? s_patchVerifier;

    public static void Initialize()
    {
        // Process guard: only inject into Root.exe
        var processName = Path.GetFileNameWithoutExtension(Environment.ProcessPath ?? "");
        if (!processName.Equals("Root", StringComparison.OrdinalIgnoreCase))
            return;

        var thread = new Thread(InjectorLoop)
        {
            IsBackground = true,
            Name = "Uprooted-Injector"
        };
        thread.Start();
    }

    private static void InjectorLoop()
    {
        try
        {
            // Gate logging to developer channel only — stable users get no log file.
            // Must check before the startup banner to avoid creating a log on stable.
            try
            {
                var channelSettings = UprootedSettings.Load();
                if (!channelSettings.AutoUpdateChannel.Equals("developer", StringComparison.OrdinalIgnoreCase))
                    Logger.Disable();
            }
            catch { /* Settings load failed — keep logging enabled for diagnostics */ }

            Logger.Log("Startup", "========================================");
            Logger.Log("Startup", $"=== Uprooted Hook v{CurrentVersion} Loaded ===");
            Logger.Log("Startup", "========================================");
            Logger.Log("Startup", $"Process: {Environment.ProcessPath}");
            Logger.Log("Startup", $"PID: {Environment.ProcessId}");
            Logger.Log("Startup", $".NET: {Environment.Version}");
            Logger.Log("Startup", $"Log file: {Logger.GetLogPath()}");

            // Phase 0: Verify HTML patches (filesystem only -- no Avalonia needed)
            Logger.Log("Startup", "Phase 0: Verifying HTML patches...");
            try
            {
                var verifier = new HtmlPatchVerifier();
                var repaired = verifier.VerifyAndRepair();
                Logger.Log("Startup", $"Phase 0 OK: {repaired} file(s) repaired");
                verifier.StartWatching();
                s_patchVerifier = verifier; // prevent GC
            }
            catch (Exception ex)
            {
                Logger.Log("Startup", $"Phase 0 non-fatal error: {ex.Message}");
            }

            // Version migration: force-disable unstable plugins on upgrade
            {
                var migrationSettings = UprootedSettings.Load();
                var cmp = AutoUpdater.CompareVersions(CurrentVersion, migrationSettings.Version);
                if (cmp > 0)
                {
                    Logger.Log("Startup", $"Version upgrade detected: {migrationSettings.Version} -> {CurrentVersion}");
                    var oldVersion = migrationSettings.Version;
                    foreach (var (version, disableList) in ForceDisableOnUpgrade)
                    {
                        // Apply entries for versions between old (exclusive) and current (inclusive)
                        if (AutoUpdater.CompareVersions(version, oldVersion) > 0 &&
                            AutoUpdater.CompareVersions(version, CurrentVersion) <= 0)
                        {
                            foreach (var pluginId in disableList)
                            {
                                migrationSettings.Plugins[pluginId] = false;
                                Logger.Log("Startup", $"  Force-disabled plugin '{pluginId}' (v{version} policy)");
                            }
                        }
                    }
                    migrationSettings.Version = CurrentVersion;
                    migrationSettings.Save();
                    Logger.Log("Startup", "Version migration complete");
                }
                else if (cmp < 0)
                {
                    // Downgrade detected — stamp version but don't apply any disable policies
                    Logger.Log("Startup", $"Version downgrade detected: {migrationSettings.Version} -> {CurrentVersion}, updating stamp only");
                    migrationSettings.Version = CurrentVersion;
                    migrationSettings.Save();
                }
            }

            // Phase 1: Wait for Avalonia assemblies to load
            Logger.Log("Startup", "Phase 1: Waiting for Avalonia assemblies...");
            if (!WaitForAvaloniaAssemblies(TimeSpan.FromSeconds(30)))
            {
                Logger.Log("Startup", "Phase 1 FAILED: Avalonia assemblies not found after 30s");
                return;
            }
            Logger.Log("Startup", "Phase 1 OK: Avalonia assemblies loaded");

            // Resolve all Avalonia types via reflection
            var resolver = new AvaloniaReflection();
            if (!resolver.Resolve())
            {
                Logger.Log("Startup", "Type resolution failed, aborting");
                return;
            }

            // Phase 2: Wait for Application.Current to be set
            Logger.Log("Startup", "Phase 2: Waiting for Application.Current...");
            if (!WaitFor(() => resolver.GetAppCurrent() != null, TimeSpan.FromSeconds(30)))
            {
                Logger.Log("Startup", "Phase 2 FAILED: Application.Current not available after 30s");
                return;
            }
            Logger.Log("Startup", "Phase 2 OK: Application.Current is set");

            // Phase 3: Wait for MainWindow
            Logger.Log("Startup", "Phase 3: Waiting for MainWindow...");
            object? mainWindow = null;
            if (!WaitFor(() =>
            {
                mainWindow = resolver.GetMainWindow();
                return mainWindow != null;
            }, TimeSpan.FromSeconds(60)))
            {
                Logger.Log("Startup", "Phase 3 FAILED: MainWindow not available after 60s");
                return;
            }
            Logger.Log("Startup", $"Phase 3 OK: MainWindow = {mainWindow!.GetType().FullName}");

            // Phase 3.5: Initialize theme engine (actual theme apply deferred to UI thread)
            Logger.Log("Startup", "Phase 3.5: Initializing theme engine");
            var themeEngine = new ThemeEngine(resolver);
            var savedSettings = UprootedSettings.Load();

            // Apply saved theme on UI thread (ResourceDictionary requires Dispatcher access)
            resolver.RunOnUIThread(() =>
            {
                try
                {
                    if (savedSettings.ActiveTheme == "custom")
                    {
                        Logger.Log("Startup", "Applying saved custom theme: accent=" + savedSettings.CustomAccent + " bg=" + savedSettings.CustomBackground);
                        themeEngine.ApplyCustomTheme(savedSettings.CustomAccent, savedSettings.CustomBackground);
                    }
                    else if (savedSettings.ActiveTheme != "default-dark")
                    {
                        Logger.Log("Startup", "Applying saved theme: " + savedSettings.ActiveTheme);
                        themeEngine.ApplyTheme(savedSettings.ActiveTheme);
                    }
                    else
                    {
                        Logger.Log("Startup", "Using default theme (no override)");
                    }

                    // Apply saved custom ping color override (persists across theme switches)
                    if (!string.IsNullOrEmpty(savedSettings.CustomPingColor) && ColorUtils.IsValidHex(savedSettings.CustomPingColor))
                    {
                        Logger.Log("Startup", "Applying saved custom ping color: " + savedSettings.CustomPingColor);
                        themeEngine.SetCustomPingColor(savedSettings.CustomPingColor);
                    }

                    // Ping color diagnostic: dumps visual tree colors 10s after startup
                    // to identify the exact color used by mention/reply highlight borders.
                    // Remove after confirming the source hex in Step 2.
                    var te = themeEngine;
                    System.Threading.ThreadPool.QueueUserWorkItem(_ => {
                        Thread.Sleep(10_000);
                        resolver.RunOnUIThread(() => {
                            try { te.DumpVisualTreeColors(); }
                            catch { }
                        });
                    });
                }
                catch (Exception ex)
                {
                    Logger.Log("Startup", "Theme init error: " + ex.Message);
                }
            });

            // Phase 4: Start the settings page monitor
            Logger.Log("Startup", "Phase 4: Starting settings page monitor");
            var injector = new SidebarInjector(resolver, mainWindow!, themeEngine);
            injector.StartMonitoring();

            Logger.Log("Startup", "========================================");
            Logger.Log("Startup", "=== Uprooted Hook Ready ===");
            Logger.Log("Startup", "========================================");

            // Phase 4.5: Browser discovery (runs after delay to let Root finish loading)
            {
                var discoveryWindow = mainWindow!;
                var discoveryResolver = resolver;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    try
                    {
                        Thread.Sleep(10_000); // Wait 10s for Root to fully initialize
                        Logger.Log("Startup", "Phase 4.5: Running browser discovery scan...");
                        discoveryResolver.RunOnUIThread(() =>
                        {
                            try
                            {
                                new BrowserDiscovery(discoveryResolver, discoveryWindow).DumpAllFindings();
                            }
                            catch (Exception ex)
                            {
                                Logger.Log("Startup", $"Phase 4.5 error (UI): {ex.Message}");
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Startup", $"Phase 4.5 error: {ex.Message}");
                    }
                });
            }

            // Phase 4.5a: ClearURLs (strip tracking params from chat URLs)
            var clearUrlsSettings = UprootedSettings.Load();
            var wantClearUrls = clearUrlsSettings.Plugins.TryGetValue("clear-urls", out var cuEnabled) && cuEnabled;
            if (wantClearUrls)
            {
                var cuWindow = mainWindow!;
                var cuResolver = resolver;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    try
                    {
                        Thread.Sleep(14_000);
                        Logger.Log("Startup", "Phase 4.5a: Starting ClearURLs engine...");
                        var engine = new ClearUrlsEngine(cuResolver, cuWindow);
                        engine.Initialize();
                        Logger.Log("Startup", "Phase 4.5a OK: ClearURLs active");
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Startup", $"Phase 4.5a error: {ex.Message}");
                    }
                });
            }
            else
            {
                Logger.Log("Startup", "Phase 4.5a: ClearURLs plugin disabled, skipping");
            }

            // Phase 4.5b: Native link embeds (Avalonia-only, no DotNetBrowser)
            var embedSettings = UprootedSettings.Load();
            var wantLinkEmbeds = embedSettings.Plugins.TryGetValue("link-embeds", out var leEnabled) && leEnabled;

            if (wantLinkEmbeds)
            {
                var embedWindow = mainWindow!;
                var embedResolver = resolver;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    try
                    {
                        Thread.Sleep(15_000); // Wait 15s for chat to populate
                        Logger.Log("Startup", "Phase 4.5b: Starting native link embed engine...");
                        var engine = new LinkEmbedEngine(embedResolver, embedWindow, themeEngine);
                        LinkEmbedEngine.Instance = engine;
                        engine.Initialize();
                        Logger.Log("Startup", "Phase 4.5b OK: Native link embeds active");
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Startup", $"Phase 4.5b error: {ex.Message}");
                    }
                });
            }
            else
            {
                Logger.Log("Startup", "Phase 4.5b: Link embeds plugin disabled, skipping");
            }

            // Phase 4.5c: Message Logger (discovery + logging + visual indicators)
            var msgLogSettings = UprootedSettings.Load();
            var wantMsgLogger = msgLogSettings.Plugins.TryGetValue("message-logger", out var mlEnabled) && mlEnabled;

            if (wantMsgLogger)
            {
                var mlWindow = mainWindow!;
                var mlResolver = resolver;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    try
                    {
                        Thread.Sleep(20_000); // Wait 20s for chat to populate
                        Logger.Log("Startup", "Phase 4.5c: Starting message logger...");
                        var logger = new MessageLogger(mlResolver, mlWindow);
                        logger.Initialize();
                        Logger.Log("Startup", "Phase 4.5c OK: Message logger active");
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Startup", $"Phase 4.5c error: {ex.Message}");
                    }
                });
            }
            else
            {
                Logger.Log("Startup", "Phase 4.5c: Message logger disabled, skipping");
            }

            // Phase 4.5d: Auto-updater (background update check)
            // Instance is set immediately so the manual "Check for Updates" button is live
            // from the moment the settings page opens. The actual check is deferred 5s to
            // let the app finish initializing before making network requests.
            var autoUpdater = new AutoUpdater();
            AutoUpdater.Instance = autoUpdater;

            // Subscribe to background update event — shows a popup when the auto-updater
            // applies an update without the user pressing the button.
            var notifyResolver = resolver;
            AutoUpdater.BackgroundUpdateApplied += (version) =>
            {
                Logger.Log("Startup", $"Phase 4.5d: Background update applied (v{version}) — showing notification");
                notifyResolver.RunOnUIThread(() =>
                {
                    try { ContentPages.ShowUpdateNotification(notifyResolver, version); }
                    catch (Exception ex) { Logger.Log("Startup", $"Update notification error: {ex.Message}"); }
                });
            };

            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    Thread.Sleep(5_000);
                    Logger.Log("Startup", "Phase 4.5d: Starting auto-updater...");
                    autoUpdater.Initialize();
                    Logger.Log("Startup", "Phase 4.5d OK: Auto-updater initialized");
                }
                catch (Exception ex)
                {
                    Logger.Log("Startup", $"Phase 4.5d error: {ex.Message}");
                }
            });

            // Phase 4.5e: Profile badge injector (dev channel only)
            var badgeSettings = UprootedSettings.Load();
            if (badgeSettings.AutoUpdateChannel.Equals("developer", StringComparison.OrdinalIgnoreCase))
            {
                var badgeWindow = mainWindow!;
                var badgeResolver = resolver;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    try
                    {
                        Thread.Sleep(5_000); // Wait 5s for app to settle
                        Logger.Log("Startup", "Phase 4.5e: Starting profile badge injector (dev channel)...");
                        var badge = new ProfileBadgeInjector(badgeResolver, badgeWindow);
                        badge.Initialize();
                        Logger.Log("Startup", "Phase 4.5e OK: Profile badge injector active");
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Startup", $"Phase 4.5e error: {ex.Message}");
                    }
                });
            }
            else
            {
                Logger.Log("Startup", "Phase 4.5e: Profile badge skipped (not on developer channel)");
            }

            // Phase 4.5f: Silent typing (HttpClient handler injection)
            var stSettings = UprootedSettings.Load();
            var wantSilentTyping = stSettings.Plugins.TryGetValue("silent-typing", out var stEnabled) && stEnabled;
            if (wantSilentTyping)
            {
                var stWindow = mainWindow!;
                var stResolver = resolver;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    try
                    {
                        Thread.Sleep(12_000); // Wait for Root's gRPC clients to initialize
                        Logger.Log("Startup", "Phase 4.5f: Starting silent typing engine...");
                        var engine = new SilentTypingEngine(stResolver, stWindow);
                        engine.Initialize();
                        Logger.Log("Startup", "Phase 4.5f OK: Silent typing engine started");
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Startup", $"Phase 4.5f error: {ex.Message}");
                    }
                });
            }
            else
            {
                Logger.Log("Startup", "Phase 4.5f: Silent typing disabled, skipping");
            }

            // Phase 4.5g: NSFW content filter (Avalonia-native visual tree scan)
            var nsfwSettings = UprootedSettings.Load();
            var wantNsfw = nsfwSettings.NsfwFilterEnabled && !string.IsNullOrEmpty(nsfwSettings.NsfwApiKey);
            if (wantNsfw)
            {
                var nsfwWindow   = mainWindow!;
                var nsfwResolver = resolver;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    try
                    {
                        Thread.Sleep(20_000); // Wait for chat to populate
                        Logger.Log("Startup", "Phase 4.5g: Starting NSFW content filter...");
                        var filter = new NsfwFilter(nsfwResolver, nsfwSettings, nsfwWindow);
                        ContentPages.NsfwFilterInstance = filter;
                        filter.Initialize();
                        Logger.Log("Startup", "Phase 4.5g OK: NSFW filter active (Avalonia-native)");
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Startup", $"Phase 4.5g error: {ex.Message}");
                    }
                });
            }
            else
            {
                Logger.Log("Startup", "Phase 4.5g: NSFW filter disabled or no API key, skipping");
            }

            // Phase 5: DotNetBrowser discovery (needed for video thumbnail extraction in LinkEmbedEngine)
            {
                var capturedWindow = mainWindow!;
                var capturedResolver = resolver;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    try
                    {
                        Logger.Log("Startup", "Phase 5: Waiting for DotNetBrowser assemblies...");

                        // Event-driven detection: react immediately when DotNetBrowser loads
                        if (!DotNetBrowserReflection.AreDotNetBrowserAssembliesLoaded())
                        {
                            using var assemblyEvent = new ManualResetEventSlim(false);
                            AssemblyLoadEventHandler handler = (_, args) =>
                            {
                                var name = args.LoadedAssembly.GetName().Name ?? "";
                                if (name.StartsWith("DotNetBrowser", StringComparison.OrdinalIgnoreCase))
                                {
                                    Logger.Log("Startup", $"Phase 5: DotNetBrowser assembly loaded via event: {name}");
                                    assemblyEvent.Set();
                                }
                            };
                            AppDomain.CurrentDomain.AssemblyLoad += handler;
                            try
                            {
                                // Double-check after subscribing (assembly may have loaded between check and subscribe)
                                if (!DotNetBrowserReflection.AreDotNetBrowserAssembliesLoaded())
                                {
                                    if (!assemblyEvent.Wait(TimeSpan.FromSeconds(90)))
                                    {
                                        Logger.Log("Startup", "Phase 5: DotNetBrowser assemblies not found after 90s, skipping");
                                        return;
                                    }
                                }
                            }
                            finally
                            {
                                AppDomain.CurrentDomain.AssemblyLoad -= handler;
                            }
                            // Brief pause for any companion assemblies to finish loading
                            Thread.Sleep(1000);
                        }
                        Logger.Log("Startup", "Phase 5: DotNetBrowser assemblies loaded");

                        // Resolve DotNetBrowser types
                        var browserReflection = new DotNetBrowserReflection();
                        if (!browserReflection.Resolve())
                        {
                            Logger.Log("Startup", "Phase 5: DotNetBrowser type resolution failed, skipping");
                            return;
                        }

                        // Discover browser instance — must dispatch to UI thread since
                        // ViewModel/ApplicationLifetime properties require UI thread access.
                        // Retry with delays since Root may not have created the IBrowser yet.
                        DotNetBrowserReflection.SharedInstance = browserReflection;
                        object? browser = null;
                        for (int attempt = 1; attempt <= 6; attempt++)
                        {
                            using var found = new ManualResetEventSlim(false);
                            object? result = null;
                            capturedResolver.RunOnUIThread(() =>
                            {
                                try { result = browserReflection.FindBrowserDirect(); }
                                catch (Exception ex) { Logger.Log("Startup", $"Phase 5: FindBrowserDirect UI error: {ex.Message}"); }
                                found.Set();
                            });
                            found.Wait(TimeSpan.FromSeconds(15));
                            browser = result;
                            if (browser != null) break;
                            Logger.Log("Startup", $"Phase 5: IBrowser not found (attempt {attempt}/6), retrying in 5s...");
                            Thread.Sleep(5_000);
                        }

                        if (browser != null)
                        {
                            DotNetBrowserReflection.SharedBrowser = browser;
                            DotNetBrowserReflection.SharedFrame = browserReflection.GetMainFrame(browser);
                            DotNetBrowserReflection.IsReady = true;
                            Logger.Log("Startup", "Phase 5: DotNetBrowser shared references ready (browser + frame)");
                        }
                        else
                        {
                            Logger.Log("Startup", "Phase 5: IBrowser not found after 6 attempts, video thumbnails unavailable");
                        }

                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Startup", $"Phase 5 error: {ex.Message}");
                    }
                });
            }
        }
        catch (Exception ex)
        {
            Logger.Log("Startup", $"Fatal error in injector: {ex}");
        }
    }

    private static bool WaitForAvaloniaAssemblies(TimeSpan timeout)
    {
        var deadline = DateTime.UtcNow + timeout;
        while (DateTime.UtcNow < deadline)
        {
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                var name = asm.GetName().Name ?? "";
                if (name.Equals("Avalonia.Controls", StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            Thread.Sleep(250);
        }
        return false;
    }

    private static bool WaitFor(Func<bool> condition, TimeSpan timeout)
    {
        var deadline = DateTime.UtcNow + timeout;
        while (DateTime.UtcNow < deadline)
        {
            try
            {
                if (condition()) return true;
            }
            catch { }
            Thread.Sleep(500);
        }
        return false;
    }
}
