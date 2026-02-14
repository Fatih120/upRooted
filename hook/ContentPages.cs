namespace Uprooted;

/// <summary>
/// Builds Avalonia control trees for Uprooted settings pages.
/// All controls created via reflection through AvaloniaReflection.
/// Styling matches Root's native settings pages:
///   - Cards: BG=#0f1923, CornerRadius=12, BorderThickness=0.5, inner padding 24px
///   - Section headers: FontSize=12, Medium(500), #66f2f2f2 (matches Root's "APP SETTINGS")
///   - Field labels: FontSize=13, Weight=450, #a3f2f2f2
///   - Body text: FontSize=13, Normal, #a3f2f2f2
///   - Page title: FontSize=20, SemiBold(600), #fff2f2f2
///   - Font: CircularXX TT (passed from native controls)
///   - Page container: StackPanel(Margin=24,24,24,0)
/// </summary>
internal static class ContentPages
{
    // Root's colors matching native UI
    private const string CardBg = "#0f1923";         // Card background (slightly lighter than Root's #0d1521)
    private const string CardBorder = "#19ffffff";    // Thin border color
    private const string TextWhite = "#fff2f2f2";     // Primary text (full opacity)
    private const string TextMuted = "#a3f2f2f2";     // Labels/muted text
    private const string TextDim = "#66f2f2f2";       // Placeholder/dim text (also section headers)
    private const string AccentGreen = "#2A5A40";     // Uprooted brand green (Moss)
    private const string SubtleOverlay = "#19ffffff";  // Subtle white overlay

    public static object? BuildPage(string pageName, AvaloniaReflection r,
        UprootedSettings settings, object? nativeFontFamily = null,
        ThemeEngine? themeEngine = null, Action? onThemeChanged = null)
    {
        return pageName switch
        {
            "uprooted" => BuildUprootedPage(r, settings, nativeFontFamily, themeEngine),
            "plugins" => BuildPluginsPage(r, settings, nativeFontFamily),
            "themes" => BuildThemesPage(r, settings, nativeFontFamily, themeEngine, onThemeChanged),
            _ => null
        };
    }

    private static void ApplyFont(AvaloniaReflection r, object? control, object? fontFamily)
    {
        if (control != null && fontFamily != null)
            r.SetFontFamily(control, fontFamily);
    }

    private static object? BuildUprootedPage(AvaloniaReflection r, UprootedSettings settings, object? font, ThemeEngine? themeEngine = null)
    {
        var page = r.CreateStackPanel(vertical: true, spacing: 0);
        if (page == null) return null;
        r.SetMargin(page, 24, 24, 24, 0);

        // Page title
        var pageTitle = r.CreateTextBlock("Uprooted", 20, TextWhite);
        r.SetFontWeightNumeric(pageTitle, 600);
        ApplyFont(r, pageTitle, font);
        r.AddChild(page, pageTitle);

        // Card 1: Uprooted identity
        var identityCard = CreateCard(r);
        if (identityCard != null)
        {
            r.SetMargin(identityCard, 0, 20, 0, 0);
            var cardContent = r.CreateStackPanel(vertical: true, spacing: 0);
            r.SetMargin(cardContent, 24, 24, 24, 24);

            // Section header + version badge
            var titleRow = r.CreateStackPanel(vertical: false, spacing: 12);
            var title = CreateSectionHeader(r, "UPROOTED", font);
            r.AddChild(titleRow, title);

            var versionText = r.CreateTextBlock($"v{settings.Version}", 11, TextWhite);
            ApplyFont(r, versionText, font);
            var versionBadge = r.CreateBorder(AccentGreen, 8, versionText);
            r.SetPadding(versionBadge, 8, 2, 8, 2);
            r.SetVerticalAlignment(versionBadge, "Center");
            r.AddChild(titleRow, versionBadge);
            r.AddChild(cardContent, titleRow);

            // About text
            var aboutText = r.CreateTextBlock(
                "A client modification framework for Root Communications. " +
                "Customize your Root experience with plugins and themes.",
                13, TextMuted);
            if (aboutText != null)
            {
                ApplyFont(r, aboutText, font);
                r.SetTextWrapping(aboutText, "Wrap");
                r.SetMargin(aboutText, 0, 16, 0, 0);
            }
            r.AddChild(cardContent, aboutText);

            r.SetBorderChild(identityCard, cardContent);
            r.AddChild(page, identityCard);
        }

        // Card 2: Status
        var statusCard = CreateCard(r);
        if (statusCard != null)
        {
            r.SetMargin(statusCard, 0, 12, 0, 0);
            var cardContent = r.CreateStackPanel(vertical: true, spacing: 0);
            r.SetMargin(cardContent, 24, 24, 24, 24);

            var statusTitle = CreateSectionHeader(r, "STATUS", font);
            r.AddChild(cardContent, statusTitle);

            AddStatusField(r, cardContent, "Hook", "Loaded", AccentGreen, true, font);
            AddStatusField(r, cardContent, "Settings Injection", "Active", AccentGreen, false, font);
            AddStatusField(r, cardContent, "Plugins", "0 loaded", TextDim, false, font);
            var activeTheme = themeEngine?.ActiveThemeName;
            var hasTheme = activeTheme != null && activeTheme != "default-dark";
            var themeStatus = hasTheme ? "Active (" + activeTheme + ")" : "Not active";
            var themeColor = hasTheme ? AccentGreen : TextDim;
            AddStatusField(r, cardContent, "Theme Override", themeStatus, themeColor, false, font);

            r.SetBorderChild(statusCard, cardContent);
            r.AddChild(page, statusCard);
        }

        // Card 3: Links
        var linksCard = CreateCard(r);
        if (linksCard != null)
        {
            r.SetMargin(linksCard, 0, 12, 0, 0);
            var cardContent = r.CreateStackPanel(vertical: true, spacing: 0);
            r.SetMargin(cardContent, 24, 24, 24, 24);

            var linksTitle = CreateSectionHeader(r, "LINKS", font);
            r.AddChild(cardContent, linksTitle);

            AddLinkField(r, cardContent, "GitHub", "github.com/watchthelight/uprooted", true, font);
            AddLinkField(r, cardContent, "Website", "uprooted.sh", false, font);

            r.SetBorderChild(linksCard, cardContent);
            r.AddChild(page, linksCard);
        }

        // Card 4: Hook Info (technical details)
        var hookCard = CreateCard(r);
        if (hookCard != null)
        {
            r.SetMargin(hookCard, 0, 12, 0, 0);
            var cardContent = r.CreateStackPanel(vertical: true, spacing: 0);
            r.SetMargin(cardContent, 24, 24, 24, 24);

            var hookTitle = CreateSectionHeader(r, "HOOK INFO", font);
            r.AddChild(cardContent, hookTitle);

            var hookText = r.CreateTextBlock(
                "Uprooted is loaded via .NET CLR Profiler into Root's process. " +
                "It persists across restarts via environment variables. " +
                "The profiler hooks into Root's .NET runtime to inject the Uprooted module.",
                13, TextMuted);
            if (hookText != null)
            {
                ApplyFont(r, hookText, font);
                r.SetTextWrapping(hookText, "Wrap");
                r.SetMargin(hookText, 0, 16, 0, 0);
            }
            r.AddChild(cardContent, hookText);

            r.SetBorderChild(hookCard, cardContent);
            r.AddChild(page, hookCard);
        }

        // Bottom padding
        var spacer = r.CreateStackPanel(vertical: true, spacing: 0);
        if (spacer != null)
        {
            spacer.GetType().GetProperty("Height")?.SetValue(spacer, 24.0);
            r.AddChild(page, spacer);
        }

        return r.CreateScrollViewer(page);
    }

    private static object? BuildPluginsPage(AvaloniaReflection r, UprootedSettings settings, object? font)
    {
        var page = r.CreateStackPanel(vertical: true, spacing: 0);
        if (page == null) return null;
        r.SetMargin(page, 24, 24, 24, 0);

        // Page title
        var pageTitle = r.CreateTextBlock("Plugins", 20, TextWhite);
        r.SetFontWeightNumeric(pageTitle, 600);
        ApplyFont(r, pageTitle, font);
        r.AddChild(page, pageTitle);

        // Card: Installed Plugins
        var pluginsCard = CreateCard(r);
        if (pluginsCard != null)
        {
            r.SetMargin(pluginsCard, 0, 20, 0, 0);
            var cardContent = r.CreateStackPanel(vertical: true, spacing: 0);
            r.SetMargin(cardContent, 24, 24, 24, 24);

            var pluginsTitle = CreateSectionHeader(r, "INSTALLED PLUGINS", font);
            r.AddChild(cardContent, pluginsTitle);

            if (settings.Plugins.Count == 0)
            {
                var emptyText = r.CreateTextBlock(
                    "No plugins installed. Plugins extend Root with custom features, " +
                    "commands, and UI modifications.",
                    13, TextMuted);
                if (emptyText != null)
                {
                    ApplyFont(r, emptyText, font);
                    r.SetTextWrapping(emptyText, "Wrap");
                    r.SetMargin(emptyText, 0, 16, 0, 0);
                }
                r.AddChild(cardContent, emptyText);
            }
            else
            {
                foreach (var (name, enabled) in settings.Plugins)
                {
                    AddPluginField(r, cardContent, name, enabled, font);
                }
            }

            r.SetBorderChild(pluginsCard, cardContent);
            r.AddChild(page, pluginsCard);
        }

        // Note
        var noteText = r.CreateTextBlock(
            "Plugin management, including install, enable/disable, and configuration, " +
            "will be available in a future update.",
            13, TextDim);
        if (noteText != null)
        {
            ApplyFont(r, noteText, font);
            r.SetTextWrapping(noteText, "Wrap");
            r.SetMargin(noteText, 0, 16, 0, 0);
        }
        r.AddChild(page, noteText);

        var spacer = r.CreateStackPanel(vertical: true, spacing: 0);
        if (spacer != null)
        {
            spacer.GetType().GetProperty("Height")?.SetValue(spacer, 24.0);
            r.AddChild(page, spacer);
        }

        return r.CreateScrollViewer(page);
    }

    private static object? BuildThemesPage(AvaloniaReflection r, UprootedSettings settings,
        object? font, ThemeEngine? themeEngine = null, Action? onThemeChanged = null)
    {
        var page = r.CreateStackPanel(vertical: true, spacing: 0);
        if (page == null) return null;
        r.SetMargin(page, 24, 24, 24, 0);

        // Page title
        var pageTitle = r.CreateTextBlock("Themes", 20, TextWhite);
        r.SetFontWeightNumeric(pageTitle, 600);
        ApplyFont(r, pageTitle, font);
        r.AddChild(page, pageTitle);

        // Section header
        var sectionHeader = CreateSectionHeader(r, "UPROOTED THEMES", font);
        if (sectionHeader != null)
        {
            r.SetMargin(sectionHeader, 0, 20, 0, 12);
            r.AddChild(page, sectionHeader);
        }

        // Theme cards - matching Root's native theme page layout
        var themes = new[]
        {
            ("Default",  "default-dark", "#0D1521", "#3B6AF8", "Root's default dark theme"),
            ("Crimson",  "crimson",      "#1A0A0A", "#C42B1C", "Deep red accent theme"),
            ("Loki",     "loki",         "#0F1210", "#2A5A40", "Moss green and gold, trestle palette"),
            ("Onyx",     "onyx",         "#000000", "#A0A8B0", "OLED black with silver accent"),
        };

        foreach (var (displayName, themeId, bgColor, accentColor, description) in themes)
        {
            bool isActive = settings.ActiveTheme == themeId;
            var card = BuildThemeCard(r, displayName, themeId, bgColor, accentColor,
                description, isActive, font, themeEngine, settings, onThemeChanged);
            if (card != null)
            {
                r.SetMargin(card, 0, 0, 0, 8);
                r.AddChild(page, card);
            }
        }

        // About themes card
        var aboutCard = CreateCard(r);
        if (aboutCard != null)
        {
            r.SetMargin(aboutCard, 0, 12, 0, 0);
            var cardContent = r.CreateStackPanel(vertical: true, spacing: 0);
            r.SetMargin(cardContent, 24, 24, 24, 24);

            var aboutTitle = CreateSectionHeader(r, "ABOUT THEMES", font);
            r.AddChild(cardContent, aboutTitle);

            var aboutText = r.CreateTextBlock(
                "Themes modify Avalonia's FluentTheme resource dictionary at runtime. " +
                "They change accent colors, backgrounds, control fills, and text colors " +
                "across the entire native UI. Your theme choice persists across restarts.",
                13, TextMuted);
            if (aboutText != null)
            {
                ApplyFont(r, aboutText, font);
                r.SetTextWrapping(aboutText, "Wrap");
                r.SetMargin(aboutText, 0, 16, 0, 0);
            }
            r.AddChild(cardContent, aboutText);

            r.SetBorderChild(aboutCard, cardContent);
            r.AddChild(page, aboutCard);
        }

        var spacer = r.CreateStackPanel(vertical: true, spacing: 0);
        if (spacer != null)
        {
            spacer.GetType().GetProperty("Height")?.SetValue(spacer, 24.0);
            r.AddChild(page, spacer);
        }

        return r.CreateScrollViewer(page);
    }

    /// <summary>
    /// Build a full-width theme card matching Root's native theme page cards.
    /// Structure: Border (full-width, rounded) containing horizontal layout with
    /// radio indicator + name on left, preview swatch on right.
    /// </summary>
    private static object? BuildThemeCard(AvaloniaReflection r, string displayName,
        string themeId, string bgColor, string accentColor, string description,
        bool isActive, object? font, ThemeEngine? themeEngine,
        UprootedSettings settings, Action? onThemeChanged)
    {
        // Outer border: full-width card with subtle or accent border
        var borderColor = isActive ? accentColor : "#242C36";
        var card = r.CreateBorder(CardBg, 12);
        if (card == null) return null;
        SetBorderStroke(r, card, borderColor, isActive ? 1.5 : 1.0);
        r.SetCursorHand(card);

        // Main horizontal layout
        var outerLayout = r.CreateStackPanel(vertical: false, spacing: 0);
        if (outerLayout == null) return null;
        r.SetMargin(outerLayout, 20, 16, 20, 16);

        // Left side: radio indicator + text
        var leftPanel = r.CreateStackPanel(vertical: false, spacing: 12);
        if (leftPanel != null)
        {
            r.SetVerticalAlignment(leftPanel, "Center");

            // Radio indicator: rounded rectangle border with fill when active
            var radioOuter = r.CreateBorder(null, 4);
            if (radioOuter != null)
            {
                r.SetWidth(radioOuter, 20);
                r.SetHeight(radioOuter, 20);
                SetBorderStroke(r, radioOuter, isActive ? accentColor : "#555555", 2.0);
                r.SetVerticalAlignment(radioOuter, "Center");

                if (isActive)
                {
                    // Inner fill dot
                    var innerDot = r.CreateBorder(accentColor, 2);
                    if (innerDot != null)
                    {
                        r.SetWidth(innerDot, 10);
                        r.SetHeight(innerDot, 10);
                        r.SetMargin(innerDot, 3, 3, 3, 3);
                    }
                    r.SetBorderChild(radioOuter, innerDot);
                }

                r.AddChild(leftPanel, radioOuter);
            }

            // Text stack: name + description
            var textStack = r.CreateStackPanel(vertical: true, spacing: 2);
            if (textStack != null)
            {
                r.SetVerticalAlignment(textStack, "Center");

                var nameText = r.CreateTextBlock(displayName, 14, TextWhite);
                r.SetFontWeightNumeric(nameText, 450);
                ApplyFont(r, nameText, font);
                r.AddChild(textStack, nameText);

                var descText = r.CreateTextBlock(description, 12, TextMuted);
                ApplyFont(r, descText, font);
                r.AddChild(textStack, descText);

                r.AddChild(leftPanel, textStack);
            }

            r.AddChild(outerLayout, leftPanel);
        }

        // Right side: preview swatch
        var preview = BuildThemePreview(r, bgColor, accentColor);
        if (preview != null)
        {
            r.SetHorizontalAlignment(preview, "Right");
            r.SetVerticalAlignment(preview, "Center");
            r.SetMargin(preview, 24, 0, 0, 0);
            r.AddChild(outerLayout, preview);
        }

        r.SetBorderChild(card, outerLayout);

        // Click handler for theme switching
        r.SubscribeEvent(card, "PointerPressed", () =>
        {
            try
            {
                Logger.Log("Theme", "Theme card clicked: " + themeId);
                if (themeEngine == null) return;

                if (themeId == "default-dark")
                {
                    themeEngine.RevertTheme();
                    settings.ActiveTheme = "default-dark";
                }
                else
                {
                    themeEngine.ApplyTheme(themeId);
                    settings.ActiveTheme = themeId;
                }

                // Save in background to avoid MissingMethodException in event context
                try { settings.Save(); }
                catch (Exception sx) { Logger.Log("Theme", "Save error: " + sx.Message); }

                // Defer page rebuild via dispatcher to avoid modifying visual tree during event
                if (onThemeChanged != null)
                {
                    r.RunOnUIThread(() =>
                    {
                        try { onThemeChanged.Invoke(); }
                        catch (Exception rx) { Logger.Log("Theme", "Rebuild error: " + rx.Message); }
                    });
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Theme", "Theme switch error: " + ex.Message);
            }
        });

        // Hover effect
        r.SubscribeEvent(card, "PointerEntered", () =>
        {
            if (!isActive)
                r.SetBackground(card, "#14FFFFFF");
        });
        r.SubscribeEvent(card, "PointerExited", () =>
        {
            if (!isActive)
                r.SetBackground(card, CardBg);
        });

        return card;
    }

    /// <summary>
    /// Build a mini preview swatch showing a simplified mockup of the theme colors.
    /// </summary>
    private static object? BuildThemePreview(AvaloniaReflection r, string bgColor, string accentColor)
    {
        // Outer frame
        var frame = r.CreateBorder("#00000000", 6);
        if (frame == null) return null;
        SetBorderStroke(r, frame, "#30ffffff", 0.5);
        r.SetWidth(frame, 100);
        r.SetHeight(frame, 56);

        var inner = r.CreateStackPanel(vertical: true, spacing: 0);
        if (inner == null) return frame;

        // Accent bar at top
        var accentBar = r.CreateBorder(accentColor, 0);
        if (accentBar != null)
        {
            r.SetHeight(accentBar, 6);
            // Top corners only
            if (r.CornerRadiusType != null)
            {
                var cr = Activator.CreateInstance(r.CornerRadiusType, 5.0, 5.0, 0.0, 0.0);
                accentBar.GetType().GetProperty("CornerRadius")?.SetValue(accentBar, cr);
            }
            r.AddChild(inner, accentBar);
        }

        // Body with bg color
        var body = r.CreateBorder(bgColor, 0);
        if (body != null)
        {
            r.SetHeight(body, 50);
            if (r.CornerRadiusType != null)
            {
                var cr = Activator.CreateInstance(r.CornerRadiusType, 0.0, 0.0, 5.0, 5.0);
                body.GetType().GetProperty("CornerRadius")?.SetValue(body, cr);
            }

            // Mini layout inside: sidebar + content area
            var bodyLayout = r.CreateStackPanel(vertical: false, spacing: 2);
            if (bodyLayout != null)
            {
                r.SetMargin(bodyLayout, 4, 4, 4, 4);

                var sidebar = r.CreateBorder("#15ffffff", 2);
                if (sidebar != null)
                {
                    r.SetWidth(sidebar, 22);
                    r.SetHeight(sidebar, 38);
                    r.AddChild(bodyLayout, sidebar);
                }

                var content = r.CreateBorder("#0Bffffff", 2);
                if (content != null)
                {
                    r.SetWidth(content, 64);
                    r.SetHeight(content, 38);
                    r.AddChild(bodyLayout, content);
                }

                r.SetBorderChild(body, bodyLayout);
            }

            r.AddChild(inner, body);
        }

        r.SetBorderChild(frame, inner);
        return frame;
    }

    // ===== Card and field builders matching Root's native style =====

    /// <summary>
    /// Create a section header matching Root's "APP SETTINGS" style:
    /// FontSize=12, FontWeight=Medium(500), Fg=#66f2f2f2
    /// </summary>
    private static object? CreateSectionHeader(AvaloniaReflection r, string text, object? font)
    {
        var header = r.CreateTextBlock(text, 12, TextDim);
        r.SetFontWeightNumeric(header, 500);
        ApplyFont(r, header, font);
        return header;
    }

    /// <summary>
    /// Create a card: BG=#0f1923, CornerRadius=12, BorderThickness=0.5
    /// </summary>
    private static object? CreateCard(AvaloniaReflection r)
    {
        var card = r.CreateBorder(CardBg, 12);
        if (card == null) return null;
        SetBorderStroke(r, card, CardBorder, 0.5);
        return card;
    }

    /// <summary>
    /// Status field matching Root's label-value pattern.
    /// </summary>
    private static void AddStatusField(AvaloniaReflection r, object? panel,
        string label, string value, string valueColor, bool first, object? font)
    {
        var row = r.CreateStackPanel(vertical: false, spacing: 0);
        if (row == null) return;
        r.SetMargin(row, 0, first ? 16 : 12, 0, 0);

        var labelText = r.CreateTextBlock(label, 13, TextMuted);
        r.SetFontWeightNumeric(labelText, 450);
        ApplyFont(r, labelText, font);
        r.AddChild(row, labelText);

        var separator = r.CreateTextBlock(" \u2022 ", 13, TextDim);
        ApplyFont(r, separator, font);
        r.AddChild(row, separator);

        var valueText = r.CreateTextBlock(value, 13, valueColor);
        r.SetFontWeightNumeric(valueText, 450);
        ApplyFont(r, valueText, font);
        r.AddChild(row, valueText);

        r.AddChild(panel, row);
    }

    /// <summary>
    /// Link field matching Root's label pattern.
    /// </summary>
    private static void AddLinkField(AvaloniaReflection r, object? panel,
        string label, string url, bool first, object? font)
    {
        var row = r.CreateStackPanel(vertical: false, spacing: 0);
        if (row == null) return;
        r.SetMargin(row, 0, first ? 16 : 12, 0, 0);

        var labelText = r.CreateTextBlock(label, 13, TextMuted);
        r.SetFontWeightNumeric(labelText, 450);
        ApplyFont(r, labelText, font);
        r.SetMargin(labelText, 0, 0, 12, 0);
        r.AddChild(row, labelText);

        var urlText = r.CreateTextBlock(url, 13, AccentGreen);
        r.SetFontWeightNumeric(urlText, 450);
        ApplyFont(r, urlText, font);
        r.AddChild(row, urlText);

        r.AddChild(panel, row);
    }

    /// <summary>
    /// Plugin status field.
    /// </summary>
    private static void AddPluginField(AvaloniaReflection r, object? panel,
        string name, bool enabled, object? font)
    {
        var row = r.CreateStackPanel(vertical: false, spacing: 0);
        if (row == null) return;
        r.SetMargin(row, 0, 16, 0, 0);

        var nameText = r.CreateTextBlock(name, 13, TextWhite);
        r.SetFontWeightNumeric(nameText, 450);
        ApplyFont(r, nameText, font);
        r.AddChild(row, nameText);

        var separator = r.CreateTextBlock(" \u2022 ", 13, TextDim);
        ApplyFont(r, separator, font);
        r.AddChild(row, separator);

        var statusText = r.CreateTextBlock(
            enabled ? "Enabled" : "Disabled",
            13,
            enabled ? AccentGreen : TextDim);
        r.SetFontWeightNumeric(statusText, 450);
        ApplyFont(r, statusText, font);
        r.AddChild(row, statusText);

        r.AddChild(panel, row);
    }

    private static void SetBorderStroke(AvaloniaReflection r, object? border, string hex, double width)
    {
        if (border == null) return;
        var brush = r.CreateBrush(hex);
        border.GetType().GetProperty("BorderBrush")?.SetValue(border, brush);

        if (r.ThicknessType != null)
        {
            var thickness = Activator.CreateInstance(r.ThicknessType, width, width, width, width);
            border.GetType().GetProperty("BorderThickness")?.SetValue(border, thickness);
        }
    }
}
