using Uprooted;

/// <summary>
/// .NET Startup Hook entry point for Uprooted.
/// Must be: internal class StartupHook (no namespace) with public static void Initialize().
/// Loaded via DOTNET_STARTUP_HOOKS env var before Root's Main() runs.
/// </summary>
internal class StartupHook
{
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
            Logger.Log("Startup", "========================================");
            Logger.Log("Startup", "=== Uprooted Hook v0.3.6rc Loaded ===");
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

                    // Diagnostics disabled (uncomment for debugging)
                    // var te = themeEngine;
                    // System.Threading.ThreadPool.QueueUserWorkItem(_ => {
                    //     Thread.Sleep(25000);
                    //     resolver.RunOnUIThread(() => {
                    //         try { te.DumpVisualTreeColors(); }
                    //         catch { }
                    //     });
                    // });
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
            var wantClearUrls = !clearUrlsSettings.Plugins.TryGetValue("clear-urls", out var cuEnabled) || cuEnabled;
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
            var wantLinkEmbeds = !embedSettings.Plugins.TryGetValue("link-embeds", out var leEnabled) || leEnabled;

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
                        var engine = new LinkEmbedEngine(embedResolver, embedWindow);
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
            var wantMsgLogger = !msgLogSettings.Plugins.TryGetValue("message-logger", out var mlEnabled) || mlEnabled;

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
            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    Thread.Sleep(30_000); // Wait 30s for app to fully settle
                    Logger.Log("Startup", "Phase 4.5d: Starting auto-updater...");
                    var updater = new AutoUpdater();
                    AutoUpdater.Instance = updater;
                    updater.Initialize();
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
                        Thread.Sleep(25_000); // Wait 25s — profile popups won't be used immediately
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

            // Phase 5: DotNetBrowser features (NSFW filter)
            var phase5Settings = UprootedSettings.Load();
            var wantNsfw = phase5Settings.NsfwFilterEnabled && !string.IsNullOrEmpty(phase5Settings.NsfwApiKey);

            if (wantNsfw)
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
                                if (name.Equals("DotNetBrowser", StringComparison.OrdinalIgnoreCase) ||
                                    name.Equals("DotNetBrowser.Core", StringComparison.OrdinalIgnoreCase) ||
                                    name.Equals("DotNetBrowser.Chromium", StringComparison.OrdinalIgnoreCase))
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

                        // NSFW filter
                        Logger.Log("Startup", "Phase 5: Starting NSFW content filter...");
                        var nsfwFilter = new NsfwFilter(capturedResolver, browserReflection,
                            phase5Settings, capturedWindow);
                        ContentPages.NsfwFilterInstance = nsfwFilter;

                        if (nsfwFilter.Initialize())
                            Logger.Log("Startup", "Phase 5 OK: NSFW content filter active");
                        else
                            Logger.Log("Startup", "Phase 5: NSFW filter init returned false (will retry via timer)");
                    }
                    catch (Exception ex)
                    {
                        Logger.Log("Startup", $"Phase 5 error: {ex.Message}");
                    }
                });
            }
            else
            {
                Logger.Log("Startup", "Phase 5: NSFW filter disabled or no API key, skipping");
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
