using Uprooted;

/// <summary>
/// .NET Startup Hook entry point for Uprooted.
/// Must be: internal class StartupHook (no namespace) with public static void Initialize().
/// Loaded via DOTNET_STARTUP_HOOKS env var before Root's Main() runs.
/// </summary>
[System.Reflection.Obfuscation(Exclude = true)]
internal class StartupHook
{
    private const string CurrentVersion = "0.5.1-rc";

    // Version migration: plugins to force-disable when upgrading to (or through) a given version.
    // Users who skip versions get all intermediate entries applied cumulatively.
    // Omit a version entirely to leave all user plugin toggles untouched for that release.
    // NOTE: Experimental plugins are blanket-disabled on EVERY upgrade (see ExperimentalPlugins).
    private static readonly Dictionary<string, string[]> ForceDisableOnUpgrade = new()
    {
        { "0.4.0", new[] { "message-logger", "content-filter" } },
        { "0.5.0", new[] { "translate", "who-reacted", "user-bio" } },
    };

    // All experimental plugin IDs. Blanket-disabled on every version upgrade to prevent
    // hangs/crashes from unstable plugins surviving across versions. Users re-enable manually.
    private static readonly string[] ExperimentalPlugins = new[]
    {
        "rootcord", "who-reacted", "message-logger", "content-filter",
        "translate", "user-bio", "recon-logger",
    };

    // Static reference keeps FileSystemWatcher alive for process lifetime
    private static HtmlPatchVerifier? s_patchVerifier;

    // Guard against double initialization (assembly loaded into multiple contexts)
    private static int s_initGuard;

    public static void Initialize()
    {
        // Process guard: only inject into Root.exe
        var processName = Path.GetFileNameWithoutExtension(Environment.ProcessPath ?? "");
        if (!processName.Equals("Root", StringComparison.OrdinalIgnoreCase))
            return;

        // One-shot guard: prevent double init if assembly loaded twice
        if (Interlocked.CompareExchange(ref s_initGuard, 1, 0) != 0)
        {
            Logger.Log("Startup", "Initialize() already called, skipping duplicate");
            return;
        }

        var thread = new Thread(InjectorLoop)
        {
            IsBackground = true,
            Name = "Uprooted-Injector"
        };
        thread.Start();
    }

    private static void InjectorLoop()
    {
        // Parent event spans the synchronous startup sequence (Phases 1-4).
        // Async phases (0, 4.5a-j, 5) emit their own child events from ThreadPool threads.
        using var startup = WideEvent.Begin("Startup", "init");
        startup.Set("version", CurrentVersion);
        startup.Set("pid", Environment.ProcessId);
        startup.Set("dotnet", Environment.Version.ToString());
        startup.Set("process", Environment.ProcessPath ?? "unknown");
        startup.Set("log_file", Logger.GetLogPath());

        try
        {
            // Phase 0: Verify HTML patches (filesystem only — no Avalonia needed).
            // Runs in background so Phase 1 polling can start immediately.
            var parentId = startup.Id;
            ThreadPool.QueueUserWorkItem(_ =>
            {
                using var ev = WideEvent.Begin("Startup", "phase0");
                ev.Set("parent_op", parentId);
                try
                {
                    var verifier = new HtmlPatchVerifier();
                    var repaired = verifier.VerifyAndRepair();
                    ev.Set("files_repaired", repaired);
                    verifier.StartWatching();
                    s_patchVerifier = verifier; // prevent GC
                    ev.Set("result", "ok");
                }
                catch (Exception ex)
                {
                    ev.SetError(ex);
                    ev.Set("result", "error");
                }
            });

            // Version migration: force-disable unstable plugins on upgrade
            {
                var migrationSettings = UprootedSettings.Load();
                var cmp = AutoUpdater.CompareVersions(CurrentVersion, migrationSettings.Version);
                if (cmp > 0)
                {
                    using var ev = WideEvent.Begin("Startup", "version_migration", startup);
                    ev.Set("from", migrationSettings.Version);
                    ev.Set("to", CurrentVersion);
                    var disabled = new List<string>();

                    // 1. Per-version targeted disables (for non-experimental plugins)
                    foreach (var (version, disableList) in ForceDisableOnUpgrade)
                    {
                        if (AutoUpdater.CompareVersions(version, migrationSettings.Version) > 0 &&
                            AutoUpdater.CompareVersions(version, CurrentVersion) <= 0)
                        {
                            foreach (var pluginId in disableList)
                            {
                                migrationSettings.Plugins[pluginId] = false;
                                if (pluginId == "content-filter")
                                    migrationSettings.NsfwFilterEnabled = false;
                                disabled.Add(pluginId);
                            }
                        }
                    }

                    // 2. Experimental plugins: preserve user's toggle state across upgrades.
                    // Only the per-version targeted disables above (step 1) force-disable
                    // specific plugins known to break on a given version. Blanket-disable
                    // was removed: users shouldn't have to re-enable plugins after every update.

                    ev.Set("disabled_plugins", string.Join(",", disabled));
                    ev.Set("experimental_reset", true);
                    migrationSettings.Version = CurrentVersion;
                    migrationSettings.Save();
                    ev.Set("result", "ok");
                }
                else if (cmp < 0)
                {
                    Logger.Log("Startup", $"Version downgrade detected: {migrationSettings.Version} -> {CurrentVersion}, updating stamp only");
                    migrationSettings.Version = CurrentVersion;
                    migrationSettings.Save();
                }
            }

            // Phase 1: Wait for Avalonia assemblies to load
            int resolvedAssemblies;
            using (var ev = WideEvent.Begin("Startup", "phase1", startup))
            {
                if (!WaitForAvaloniaAssemblies(TimeSpan.FromSeconds(30)))
                {
                    ev.Set("result", "timeout");
                    startup.Set("result", "phase1_timeout");
                    return;
                }
                resolvedAssemblies = AppDomain.CurrentDomain.GetAssemblies().Length;
                ev.Set("assemblies", resolvedAssemblies);
                ev.Set("result", "ok");
            }

            // Resolve all Avalonia types via reflection
            var resolver = new AvaloniaReflection();
            if (!resolver.Resolve())
            {
                startup.Set("result", "type_resolution_failed");
                return;
            }

            // Phase 2: Wait for Application.Current to be set
            using (var ev = WideEvent.Begin("Startup", "phase2", startup))
            {
                if (!WaitFor(() => resolver.GetAppCurrent() != null, TimeSpan.FromSeconds(30)))
                {
                    ev.Set("result", "timeout");
                    startup.Set("result", "phase2_timeout");
                    return;
                }
                ev.Set("result", "ok");
            }

            // Phase 3: Wait for MainWindow
            object? mainWindow = null;
            using (var ev = WideEvent.Begin("Startup", "phase3", startup))
            {
                if (!WaitFor(() =>
                {
                    mainWindow = resolver.GetMainWindow();
                    return mainWindow != null;
                }, TimeSpan.FromSeconds(60)))
                {
                    ev.Set("result", "timeout");
                    startup.Set("result", "phase3_timeout");
                    return;
                }
                ev.Set("window_type", mainWindow!.GetType().Name);
                ev.Set("result", "ok");
            }

            // Phase 3.5: Initialize theme engine (actual theme apply deferred to UI thread)
            var themeEngine = new ThemeEngine(resolver);
            var savedSettings = UprootedSettings.Load();

            // Apply saved theme on UI thread (ResourceDictionary requires Dispatcher access)
            resolver.RunOnUIThread(() =>
            {
                using var ev = WideEvent.Begin("Startup", "phase3_5_theme");
                try
                {
                    if (savedSettings.ActiveTheme == "custom")
                    {
                        var textHex = !string.IsNullOrEmpty(savedSettings.CustomText) && ColorUtils.IsValidHex(savedSettings.CustomText)
                            ? savedSettings.CustomText : null;
                        themeEngine.ApplyCustomTheme(savedSettings.CustomAccent, savedSettings.CustomBackground, textHex);
                        ev.Set("theme", "custom").Set("accent", savedSettings.CustomAccent).Set("bg", savedSettings.CustomBackground);
                    }
                    else if (savedSettings.ActiveTheme != "default-dark")
                    {
                        themeEngine.ApplyTheme(savedSettings.ActiveTheme);
                        ev.Set("theme", savedSettings.ActiveTheme);
                    }
                    else
                    {
                        ev.Set("theme", "default-dark");
                    }

                    themeEngine.EnsureVariantChangeSubscribed();
                    themeEngine.ReconcileStartupVectorAssets();

                    if (!string.IsNullOrEmpty(savedSettings.CustomPingColor) && ColorUtils.IsValidHex(savedSettings.CustomPingColor))
                    {
                        themeEngine.SetCustomPingColor(savedSettings.CustomPingColor);
                        ev.Set("ping_color", savedSettings.CustomPingColor);
                    }

                    // Ping color diagnostic: dev channel only, deferred to avoid UI contention.
                    if (savedSettings.AutoUpdateChannel.Equals("developer", StringComparison.OrdinalIgnoreCase))
                    {
                        var te = themeEngine;
                        System.Threading.ThreadPool.QueueUserWorkItem(_ => {
                            Thread.Sleep(30_000);
                            resolver.RunOnUIThread(() => {
                                try { te.DumpVisualTreeColors(); }
                                catch (Exception dumpEx) { Logger.Log("Startup", "DumpVisualTreeColors error: " + dumpEx.Message); }
                            });
                        });
                    }

                    ev.Set("result", "ok");
                }
                catch (Exception ex)
                {
                    ev.SetError(ex);
                    ev.Set("result", "error");
                }
            });

            // Phase 4: Start the settings page monitor
            using (var ev = WideEvent.Begin("Startup", "phase4", startup))
            {
                var injector = new SidebarInjector(resolver, mainWindow!, themeEngine);
                injector.StartMonitoring();
                ev.Set("result", "ok");
            }

            startup.Set("result", "ready");
            int pluginsStarted = 0;

            // ===== Async plugin phases (fire-and-forget on ThreadPool) =====

            // Phase 4.5: Browser discovery (diagnostic scan, dev channel only, deferred)
            if (savedSettings.AutoUpdateChannel.Equals("developer", StringComparison.OrdinalIgnoreCase))
            {
                var discoveryWindow = mainWindow!;
                var discoveryResolver = resolver;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    try
                    {
                        Thread.Sleep(30_000); // deferred to avoid UI thread contention during startup
                        discoveryResolver.RunOnUIThread(() =>
                        {
                            try { new BrowserDiscovery(discoveryResolver, discoveryWindow).DumpAllFindings(); }
                            catch (Exception ex) { Logger.Log("Startup", $"Phase 4.5 error (UI): {ex.Message}"); }
                        });
                    }
                    catch (Exception ex) { Logger.Log("Startup", $"Phase 4.5 error: {ex.Message}"); }
                });
            }

            // Phase 4.5a: ClearURLs (needs TextArea in compose box)
            StartPluginPhase("phase4_5a_clearurls", parentId, resolver, mainWindow!,
                savedSettings.Plugins.TryGetValue("clear-urls", out var cuEnabled) && cuEnabled,
                3_000, (ev, r, w) =>
                {
                    var engine = new ClearUrlsEngine(r, w);
                    engine.Initialize();
                });
            if (savedSettings.Plugins.TryGetValue("clear-urls", out var cuE) && cuE) pluginsStarted++;

            // Phase 4.5b: Native link embeds (needs message panel)
            StartPluginPhase("phase4_5b_link_embeds", parentId, resolver, mainWindow!,
                savedSettings.Plugins.TryGetValue("link-embeds", out var leEnabled) && leEnabled,
                5_000, (ev, r, w) =>
                {
                    var engine = new LinkEmbedEngine(r, w, themeEngine);
                    LinkEmbedEngine.Instance = engine;
                    engine.Initialize();
                });
            if (savedSettings.Plugins.TryGetValue("link-embeds", out var leE) && leE) pluginsStarted++;

            // Phase 4.5c: Message Logger + Audit Log (needs chat populated)
            StartPluginPhase("phase4_5c_msg_logger", parentId, resolver, mainWindow!,
                savedSettings.Plugins.TryGetValue("message-logger", out var mlEnabled) && mlEnabled,
                5_000, (ev, r, w) =>
                {
                    var logger = new MessageLogger(r, w);
                    logger.Initialize();
                    ev.Set("audit_log", true);
                    var auditEngine = new AuditLogEngine(r, w);
                    auditEngine.Initialize();
                    logger.SetAuditLogEngine(auditEngine);
                });
            if (savedSettings.Plugins.TryGetValue("message-logger", out var mlE) && mlE) pluginsStarted++;

            // Phase 4.5d: Auto-updater
            var autoUpdater = new AutoUpdater();
            AutoUpdater.Instance = autoUpdater;

            var notifyResolver = resolver;
            AutoUpdater.BackgroundUpdateApplied += (version) =>
            {
                var s = UprootedSettings.Load();
                if (!s.AutoUpdateNotify) return;
                DesktopNotification.Show("Uprooted", $"Updated to v{version} — restart Root to apply");
                notifyResolver.RunOnUIThread(() =>
                {
                    try { ContentPages.ShowUpdateNotification(notifyResolver, version); }
                    catch (Exception ex) { Logger.Log("Startup", $"Update notification error: {ex.Message}"); }
                });
            };

            StartPluginPhase("phase4_5d_auto_update", parentId, resolver, mainWindow!,
                true, 100, (ev, r, w) =>
                {
                    autoUpdater.Initialize();
                });
            pluginsStarted++;

            // Phase 4.5e: Presence beacon + profile badge + user bio (no visual tree dependency)
            StartPluginPhase("phase4_5e_presence_badge", parentId, resolver, mainWindow!,
                true, 100, (ev, r, w) =>
                {
                    var beacon = new UprootedPresenceBeacon(r, w);
                    beacon.Initialize();
                    var bioEngine = new UserBioEngine(r, w);
                    UserBioEngine.Instance = bioEngine;
                    bioEngine.Initialize(beacon);
                    var badge = new ProfileBadgeInjector(r, w, beacon, bioEngine);
                    badge.Initialize();
                });
            pluginsStarted++;

            // Phase 4.5f: Silent typing (needs HttpClient/gRPC discovery)
            StartPluginPhase("phase4_5f_silent_typing", parentId, resolver, mainWindow!,
                savedSettings.Plugins.TryGetValue("silent-typing", out var stEnabled) && stEnabled,
                3_000, (ev, r, w) =>
                {
                    var engine = new SilentTypingEngine(r, w);
                    engine.Initialize();
                });
            if (savedSettings.Plugins.TryGetValue("silent-typing", out var stE) && stE) pluginsStarted++;

            // Phase 4.5g: NSFW content filter
            var wantNsfw = savedSettings.NsfwFilterEnabled && !string.IsNullOrEmpty(savedSettings.NsfwApiKey);
            if (wantNsfw)
            {
                var nsfwSettings = savedSettings;
                StartPluginPhase("phase4_5g_nsfw_filter", parentId, resolver, mainWindow!,
                    true, 5_000, (ev, r, w) =>
                    {
                        var filter = new NsfwFilter(r, nsfwSettings, w);
                        ContentPages.NsfwFilterInstance = filter;
                        filter.Initialize();
                    });
                pluginsStarted++;
            }
            else
            {
                Logger.Log("Startup", "Phase 4.5g: NSFW filter disabled or no API key, skipping");
            }

            // Phase 4.5h: Rootcord
            // Respect Plugin.rootcord toggle directly: ShowExperimentalPlugins only gates UI visibility,
            // not runtime behavior. Version migration may reset ShowExperimental while leaving the plugin enabled.
            var wantRootcord = savedSettings.Plugins.TryGetValue("rootcord", out var rcEnabled) && rcEnabled;
            {
                var rcEngine = new RootcordEngine(resolver, mainWindow!, themeEngine);
                RootcordEngine.Instance = rcEngine;

                if (wantRootcord)
                {
                    bool applied = false;
                    int rcAttempts = 0;
                    const int maxRcAttempts = 50; // ~5-10 seconds of layout passes
                    resolver.RunOnUIThread(() =>
                    {
                        resolver.SubscribeEvent(mainWindow!, "LayoutUpdated", () =>
                        {
                            if (applied || rcEngine.IsApplied) { applied = true; return; }
                            if (rcAttempts >= maxRcAttempts) return; // give up silently
                            rcAttempts++;
                            try
                            {
                                rcEngine.Apply();
                                if (rcEngine.IsApplied)
                                {
                                    applied = true;
                                    Logger.Log("Startup", $"Phase 4.5h OK: Rootcord active (attempt {rcAttempts})");
                                }
                            }
                            catch (Exception ex)
                            {
                                if (rcAttempts == maxRcAttempts)
                                    Logger.Log("Startup", $"Phase 4.5h: Rootcord gave up after {maxRcAttempts} attempts: {ex.Message}");
                            }
                        });
                    });
                    pluginsStarted++;
                }
                else
                {
                    Logger.Log("Startup", "Phase 4.5h: Rootcord disabled, dormant instance created");
                }
            }

            // Phase 4.5i: Recon Logger (dev channel only)
            {
                if (savedSettings.AutoUpdateChannel.Equals("developer", StringComparison.OrdinalIgnoreCase))
                {
                    var reconResolver = resolver;
                    var reconWindow   = mainWindow!;
                    resolver.RunOnUIThread(() =>
                    {
                        using var ev = WideEvent.Begin("Startup", "phase4_5i_recon");
                        try
                        {
                            ReconLogger.Init(reconResolver, reconWindow);
                            bool autoStart = savedSettings.Plugins.TryGetValue("recon-logger", out var rlEnabled) && rlEnabled;
                            if (autoStart) ReconLogger.Enable();
                            ev.Set("auto_start", autoStart);
                            ev.Set("result", "ok");
                        }
                        catch (Exception ex)
                        {
                            ev.SetError(ex);
                            ev.Set("result", "error");
                        }
                    });
                }
            }

            // Phase 4.5i2: Dev Console dropdown (titlebar button, developer channel only)
            {
                if (savedSettings.AutoUpdateChannel.Equals("developer", StringComparison.OrdinalIgnoreCase))
                {
                    var dcResolver = resolver;
                    var dcWindow = mainWindow!;
                    var dcTheme = themeEngine;
                    dcResolver.RunOnUIThread(() =>
                    {
                        using var ev = WideEvent.Begin("Startup", "phase4_5i2_dev_console");
                        try
                        {
                            DevConsoleDropdown.Init(dcResolver, dcWindow, dcTheme);
                            ev.Set("result", "ok");
                        }
                        catch (Exception ex) { ev.SetError(ex); ev.Set("result", "error"); }
                    });
                }
            }

            // Phase 4.5j: Translate engine (always-on, self-gated, no visual tree dependency)
            StartPluginPhase("phase4_5j_translate", parentId, resolver, mainWindow!,
                true, 100, (ev, r, w) =>
                {
                    var engine = new TranslateEngine(r, w, themeEngine);
                    TranslateEngine.Instance = engine;
                    engine.Initialize();
                });
            pluginsStarted++;

            // Phase 4.5k: WhoReacted (needs reactions in chat)
            StartPluginPhase("phase4_5k_who_reacted", parentId, resolver, mainWindow!,
                savedSettings.ShowExperimentalPlugins
                    && savedSettings.Plugins.TryGetValue("who-reacted", out var wrEnabled) && wrEnabled,
                5_000, (ev, r, w) =>
                {
                    var engine = new WhoReactedEngine(r, w);
                    engine.Initialize();
                });
            if (savedSettings.ShowExperimentalPlugins
                && savedSettings.Plugins.TryGetValue("who-reacted", out var wrE) && wrE) pluginsStarted++;

            // Phase 5: DotNetBrowser discovery
            {
                var capturedWindow = mainWindow!;
                var capturedResolver = resolver;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    using var ev = WideEvent.Begin("Startup", "phase5_dotnetbrowser");
                    try
                    {
                        if (!DotNetBrowserReflection.AreDotNetBrowserAssembliesLoaded())
                        {
                            Func<string?, bool> dnbMatch = name =>
                                (name ?? "").StartsWith("DotNetBrowser", StringComparison.OrdinalIgnoreCase);
                            var dnbTimeout = TimeSpan.FromSeconds(90);

                            bool loaded;
                            try
                            {
                                loaded = WaitForAssemblyViaEvent(dnbMatch, dnbTimeout);
                            }
                            catch (Exception)
                            {
                                Logger.Log("Startup", "Phase 5: AssemblyLoad event unavailable (trimmed host) — polling for DotNetBrowser");
                                loaded = WaitForAssemblyViaPolling(dnbMatch, dnbTimeout, intervalMs: 500);
                            }

                            if (!loaded)
                            {
                                ev.Set("result", "timeout");
                                return;
                            }
                            Thread.Sleep(200); // brief settle for remaining DotNetBrowser assemblies
                        }

                        var browserReflection = new DotNetBrowserReflection();
                        if (!browserReflection.Resolve())
                        {
                            ev.Set("result", "resolve_failed");
                            return;
                        }

                        DotNetBrowserReflection.SharedInstance = browserReflection;
                        object? browser = null;
                        int attempts = 0;
                        for (int attempt = 1; attempt <= 6; attempt++)
                        {
                            attempts = attempt;
                            // FindBrowserDirect uses reflection on services/static fields —
                            // does NOT need the UI thread. Running it on the UI thread caused
                            // permanent freezes: DI resolution can trigger synchronous Chromium
                            // initialization which blocks the UI thread indefinitely.
                            try { browser = browserReflection.FindBrowserDirect(); }
                            catch (Exception ex) { Logger.Log("Startup", $"Phase 5: FindBrowserDirect error: {ex.Message}"); }
                            if (browser != null) break;
                            Thread.Sleep(5_000);
                        }

                        ev.Set("attempts", attempts);
                        if (browser != null)
                        {
                            DotNetBrowserReflection.SharedBrowser = browser;
                            DotNetBrowserReflection.SharedFrame = browserReflection.GetMainFrame(browser);
                            DotNetBrowserReflection.IsReady = true;
                            ev.Set("result", "ok");
                        }
                        else
                        {
                            ev.Set("result", "browser_not_found");
                        }
                    }
                    catch (Exception ex)
                    {
                        ev.SetError(ex);
                        ev.Set("result", "error");
                    }
                });
            }

            startup.Set("plugins_started", pluginsStarted);
        }
        catch (Exception ex)
        {
            startup.SetError(ex);
            startup.Set("result", "fatal");
        }
    }

    /// <summary>
    /// Helper to start an async plugin phase with consistent wide event structure.
    /// Fires on ThreadPool with a delay, emits a child wide event.
    /// </summary>
    private static void StartPluginPhase(string operation, string parentId,
        AvaloniaReflection resolver, object mainWindow, bool enabled, int delayMs,
        Action<WideEvent, AvaloniaReflection, object> initAction)
    {
        if (!enabled)
        {
            Logger.Log("Startup", $"{operation}: disabled, skipping");
            return;
        }

        var capturedResolver = resolver;
        var capturedWindow = mainWindow;
        ThreadPool.QueueUserWorkItem(_ =>
        {
            using var ev = WideEvent.Begin("Startup", operation);
            ev.Set("parent_op", parentId);
            ev.Set("delay_ms", delayMs);
            try
            {
                Thread.Sleep(delayMs);
                initAction(ev, capturedResolver, capturedWindow);
                ev.Set("result", "ok");
            }
            catch (Exception ex)
            {
                ev.SetError(ex);
                ev.Set("result", "error");
            }
        });
    }

    private static bool WaitForAvaloniaAssemblies(TimeSpan timeout)
    {
        // Check if already loaded (profiler injection fires after module loads)
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            if ((asm.GetName().Name ?? "").Equals("Avalonia.Controls", StringComparison.OrdinalIgnoreCase))
                return true;
        }

        Func<string?, bool> match = name =>
            (name ?? "").Equals("Avalonia.Controls", StringComparison.OrdinalIgnoreCase);

        // Try event-driven first; fall back to polling if AssemblyLoadEventHandler
        // is trimmed from the host app's System.Runtime (Root publishes with trimming).
        try
        {
            return WaitForAssemblyViaEvent(match, timeout);
        }
        catch (Exception)
        {
            Logger.Log("Startup", "AssemblyLoad event unavailable (trimmed host) — falling back to polling");
            return WaitForAssemblyViaPolling(match, timeout);
        }
    }

    /// <summary>
    /// Wait for an assembly matching the predicate using AppDomain.AssemblyLoad event.
    /// Isolated in its own [NoInlining] method so that if AssemblyLoadEventHandler is
    /// trimmed from the host app's System.Runtime, the exception from JIT is
    /// catchable by the caller (WaitForAvaloniaAssemblies / Phase 5).
    /// </summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
    private static bool WaitForAssemblyViaEvent(Func<string?, bool> match, TimeSpan timeout)
    {
        using var signal = new ManualResetEventSlim(false);
        void OnAssemblyLoad(object? sender, AssemblyLoadEventArgs args)
        {
            if (match(args.LoadedAssembly.GetName().Name))
                signal.Set();
        }
        AppDomain.CurrentDomain.AssemblyLoad += OnAssemblyLoad;
        try
        {
            // Double-check after subscribing (race window)
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (match(asm.GetName().Name))
                    return true;
            }
            return signal.Wait(timeout);
        }
        finally
        {
            AppDomain.CurrentDomain.AssemblyLoad -= OnAssemblyLoad;
        }
    }

    /// <summary>
    /// Polling fallback for when AssemblyLoadEventHandler is trimmed from the host app.
    /// Checks every <paramref name="intervalMs"/> — typically only needed on the startup
    /// hook injection path where Avalonia assemblies haven't loaded yet.
    /// </summary>
    private static bool WaitForAssemblyViaPolling(Func<string?, bool> match, TimeSpan timeout, int intervalMs = 50)
    {
        var deadline = DateTime.UtcNow + timeout;
        while (DateTime.UtcNow < deadline)
        {
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (match(asm.GetName().Name))
                    return true;
            }
            Thread.Sleep(intervalMs);
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
            Thread.Sleep(200);
        }
        return false;
    }
}
