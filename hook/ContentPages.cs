namespace Uprooted;

/// <summary>
/// Builds Avalonia control trees for Uprooted settings pages.
/// All controls created via reflection through AvaloniaReflection.
/// Styling matches Root's native settings pages forensically:
///   - Cards: BG=#081408, CornerRadius=12, BorderThickness=0.5, inner padding 24px
///   - Card titles: FontSize=14, Bold, #fff2f2f2
///   - Field labels: FontSize=13, Weight=450, #a3f2f2f2
///   - Body text: FontSize=13, Normal, #a3f2f2f2
///   - Page container: StackPanel(Margin=24,24,24,0)
/// </summary>
internal static class ContentPages
{
    // Root's exact colors from diagnostic dump
    private const string CardBg = "#081408";         // RootBorder card background
    private const string InputBg = "#090e13";        // Input field background
    private const string CardBorder = "#19ffffff";    // Thin border color
    private const string TextWhite = "#fff2f2f2";     // Primary text (full opacity)
    private const string TextMuted = "#a3f2f2f2";     // Labels/muted text
    private const string TextDim = "#66f2f2f2";       // Placeholder/dim text
    private const string AccentGreen = "#2D7D46";     // Uprooted brand green
    private const string AccentBlue = "#3b6af8";      // Root brand blue (dots)
    private const string SubtleOverlay = "#19ffffff";  // Subtle white overlay

    public static object? BuildPage(string pageName, AvaloniaReflection r, UprootedSettings settings)
    {
        return pageName switch
        {
            "uprooted" => BuildUprootedPage(r, settings),
            "plugins" => BuildPluginsPage(r, settings),
            "themes" => BuildThemesPage(r, settings),
            _ => null
        };
    }

    private static object? BuildUprootedPage(AvaloniaReflection r, UprootedSettings settings)
    {
        var page = r.CreateStackPanel(vertical: true, spacing: 0);
        if (page == null) return null;
        r.SetMargin(page, 24, 24, 24, 0);

        // Card 1: Uprooted identity
        var identityCard = CreateCard(r);
        if (identityCard != null)
        {
            var cardContent = r.CreateStackPanel(vertical: true, spacing: 0);
            r.SetMargin(cardContent, 24, 24, 24, 24);

            // Title row: "UPROOTED" + version badge
            var titleRow = r.CreateStackPanel(vertical: false, spacing: 12);
            var title = r.CreateTextBlock("UPROOTED", 14, TextWhite, "Bold");
            r.AddChild(titleRow, title);

            var versionText = r.CreateTextBlock($"v{settings.Version}", 11, TextWhite);
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

            var statusTitle = r.CreateTextBlock("STATUS", 14, TextWhite, "Bold");
            r.AddChild(cardContent, statusTitle);

            AddStatusField(r, cardContent, "Hook", "Loaded", AccentGreen, true);
            AddStatusField(r, cardContent, "Settings Injection", "Active", AccentGreen, false);
            AddStatusField(r, cardContent, "Plugins", "0 loaded", TextDim, false);
            AddStatusField(r, cardContent, "Custom CSS", "Not active", TextDim, false);

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

            var linksTitle = r.CreateTextBlock("LINKS", 14, TextWhite, "Bold");
            r.AddChild(cardContent, linksTitle);

            AddLinkField(r, cardContent, "GitHub", "github.com/watchthelight/uprooted", true);
            AddLinkField(r, cardContent, "Website", "uprooted.sh", false);

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

            var hookTitle = r.CreateTextBlock("HOOK INFO", 14, TextWhite, "Bold");
            r.AddChild(cardContent, hookTitle);

            var hookText = r.CreateTextBlock(
                "Uprooted is loaded via .NET CLR Profiler into Root's process. " +
                "It persists across restarts via environment variables. " +
                "The profiler hooks into Root's .NET runtime to inject the Uprooted module.",
                13, TextMuted);
            if (hookText != null)
            {
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

    private static object? BuildPluginsPage(AvaloniaReflection r, UprootedSettings settings)
    {
        var page = r.CreateStackPanel(vertical: true, spacing: 0);
        if (page == null) return null;
        r.SetMargin(page, 24, 24, 24, 0);

        // Card: Installed Plugins
        var pluginsCard = CreateCard(r);
        if (pluginsCard != null)
        {
            var cardContent = r.CreateStackPanel(vertical: true, spacing: 0);
            r.SetMargin(cardContent, 24, 24, 24, 24);

            var pluginsTitle = r.CreateTextBlock("INSTALLED PLUGINS", 14, TextWhite, "Bold");
            r.AddChild(cardContent, pluginsTitle);

            if (settings.Plugins.Count == 0)
            {
                var emptyText = r.CreateTextBlock(
                    "No plugins installed. Plugins extend Root with custom features, " +
                    "commands, and UI modifications.",
                    13, TextMuted);
                if (emptyText != null)
                {
                    r.SetTextWrapping(emptyText, "Wrap");
                    r.SetMargin(emptyText, 0, 16, 0, 0);
                }
                r.AddChild(cardContent, emptyText);
            }
            else
            {
                foreach (var (name, enabled) in settings.Plugins)
                {
                    AddPluginField(r, cardContent, name, enabled);
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

    private static object? BuildThemesPage(AvaloniaReflection r, UprootedSettings settings)
    {
        var page = r.CreateStackPanel(vertical: true, spacing: 0);
        if (page == null) return null;
        r.SetMargin(page, 24, 24, 24, 0);

        // Card: Available Themes
        var themesCard = CreateCard(r);
        if (themesCard != null)
        {
            var cardContent = r.CreateStackPanel(vertical: true, spacing: 0);
            r.SetMargin(cardContent, 24, 24, 24, 24);

            var themesTitle = r.CreateTextBlock("AVAILABLE THEMES", 14, TextWhite, "Bold");
            r.AddChild(cardContent, themesTitle);

            var themes = new[]
            {
                ("Default Dark", "#0D1521", "The default Root dark theme"),
                ("Pure Dark",    "#161617", "OLED-friendly pure dark theme"),
                ("Light",        "#FBFBFB", "Light theme for daytime use"),
            };

            bool first = true;
            foreach (var (name, colorHex, description) in themes)
            {
                AddThemeField(r, cardContent, name, colorHex, description, settings.ActiveTheme, first);
                first = false;
            }

            r.SetBorderChild(themesCard, cardContent);
            r.AddChild(page, themesCard);
        }

        // Custom CSS card
        var cssCard = CreateCard(r);
        if (cssCard != null)
        {
            r.SetMargin(cssCard, 0, 12, 0, 0);
            var cardContent = r.CreateStackPanel(vertical: true, spacing: 0);
            r.SetMargin(cardContent, 24, 24, 24, 24);

            var cssTitle = r.CreateTextBlock("CUSTOM CSS", 14, TextWhite, "Bold");
            r.AddChild(cardContent, cssTitle);

            var cssNote = r.CreateTextBlock(
                "Custom CSS injection will be available in a future update. " +
                "This will allow you to override Root's styling with your own CSS rules.",
                13, TextMuted);
            if (cssNote != null)
            {
                r.SetTextWrapping(cssNote, "Wrap");
                r.SetMargin(cssNote, 0, 16, 0, 0);
            }
            r.AddChild(cardContent, cssNote);

            r.SetBorderChild(cssCard, cardContent);
            r.AddChild(page, cssCard);
        }

        var spacer = r.CreateStackPanel(vertical: true, spacing: 0);
        if (spacer != null)
        {
            spacer.GetType().GetProperty("Height")?.SetValue(spacer, 24.0);
            r.AddChild(page, spacer);
        }

        return r.CreateScrollViewer(page);
    }

    // ===== Card and field builders matching Root's exact style =====

    /// <summary>
    /// Create a card matching Root's RootBorder: BG=#081408, CornerRadius=12, BorderThickness=0.5
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
    /// Label: FontSize=13, Weight=450, Fg=#a3f2f2f2
    /// Value: FontSize=13, Fg=colored
    /// </summary>
    private static void AddStatusField(AvaloniaReflection r, object? panel,
        string label, string value, string valueColor, bool first)
    {
        var row = r.CreateStackPanel(vertical: false, spacing: 0);
        if (row == null) return;
        r.SetMargin(row, 0, first ? 16 : 12, 0, 0);

        var labelText = r.CreateTextBlock(label, 13, TextMuted);
        r.SetFontWeightNumeric(labelText, 450);
        r.AddChild(row, labelText);

        var separator = r.CreateTextBlock(" \u2022 ", 13, TextDim);
        r.AddChild(row, separator);

        var valueText = r.CreateTextBlock(value, 13, valueColor);
        r.SetFontWeightNumeric(valueText, 450);
        r.AddChild(row, valueText);

        r.AddChild(panel, row);
    }

    /// <summary>
    /// Link field matching Root's label pattern.
    /// </summary>
    private static void AddLinkField(AvaloniaReflection r, object? panel,
        string label, string url, bool first)
    {
        var row = r.CreateStackPanel(vertical: false, spacing: 0);
        if (row == null) return;
        r.SetMargin(row, 0, first ? 16 : 12, 0, 0);

        var labelText = r.CreateTextBlock(label, 13, TextMuted);
        r.SetFontWeightNumeric(labelText, 450);
        r.SetMargin(labelText, 0, 0, 12, 0);
        r.AddChild(row, labelText);

        var urlText = r.CreateTextBlock(url, 13, AccentGreen);
        r.SetFontWeightNumeric(urlText, 450);
        r.AddChild(row, urlText);

        r.AddChild(panel, row);
    }

    /// <summary>
    /// Plugin status field.
    /// </summary>
    private static void AddPluginField(AvaloniaReflection r, object? panel, string name, bool enabled)
    {
        var row = r.CreateStackPanel(vertical: false, spacing: 0);
        if (row == null) return;
        r.SetMargin(row, 0, 16, 0, 0);

        var nameText = r.CreateTextBlock(name, 13, TextWhite);
        r.SetFontWeightNumeric(nameText, 450);
        r.AddChild(row, nameText);

        var separator = r.CreateTextBlock(" \u2022 ", 13, TextDim);
        r.AddChild(row, separator);

        var statusText = r.CreateTextBlock(
            enabled ? "Enabled" : "Disabled",
            13,
            enabled ? AccentGreen : TextDim);
        r.SetFontWeightNumeric(statusText, 450);
        r.AddChild(row, statusText);

        r.AddChild(panel, row);
    }

    /// <summary>
    /// Theme card with swatch preview.
    /// </summary>
    private static void AddThemeField(AvaloniaReflection r, object? panel, string name,
        string colorHex, string description, string activeTheme, bool first)
    {
        var row = r.CreateStackPanel(vertical: false, spacing: 12);
        if (row == null) return;
        r.SetMargin(row, 0, first ? 20 : 12, 0, 0);

        // Color swatch (matching Root's Ellipse icon style: Fill=#19ffffff, 40x40)
        var swatchPanel = r.CreatePanel();
        if (swatchPanel != null)
        {
            var swatchBg = r.CreateEllipse(40, 40, SubtleOverlay);
            r.AddChild(swatchPanel, swatchBg);

            var swatchInner = r.CreateEllipse(24, 24, colorHex);
            if (swatchInner != null)
            {
                r.SetMargin(swatchInner, 8, 8, 8, 8);
                r.AddChild(swatchPanel, swatchInner);
            }

            r.SetVerticalAlignment(swatchPanel, "Top");
            r.AddChild(row, swatchPanel);
        }

        // Name + description
        var textPanel = r.CreateStackPanel(vertical: true, spacing: 4);

        var nameRow = r.CreateStackPanel(vertical: false, spacing: 8);
        var nameText = r.CreateTextBlock(name, 16, TextWhite);
        r.SetFontWeightNumeric(nameText, 450);
        r.AddChild(nameRow, nameText);

        bool isActive = name.ToLower().Replace(" ", "-") == activeTheme;
        if (isActive)
        {
            var badgeText = r.CreateTextBlock("ACTIVE", 10, TextWhite, "Bold");
            var badge = r.CreateBorder(AccentGreen, 6, badgeText);
            r.SetPadding(badge, 6, 2, 6, 2);
            r.SetVerticalAlignment(badge, "Center");
            r.AddChild(nameRow, badge);
        }

        r.AddChild(textPanel, nameRow);

        var descText = r.CreateTextBlock(description, 13, TextMuted);
        r.AddChild(textPanel, descText);

        r.SetVerticalAlignment(textPanel, "Center");
        r.AddChild(row, textPanel);

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
