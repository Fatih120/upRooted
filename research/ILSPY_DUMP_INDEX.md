# ILSpy Dump Index

> Master index for all decompiled files in `research/ilspy-dumps/`.
> Source: ILSpy decompilation of Root v0.9.92 and its Avalonia dependencies.
> Each file preserves original ILSpy comments (assembly version, full namespace path).

**Total:** 112 files, 122,174 lines, 7.7 MB

**Already analyzed into docs:** Files marked with a checkmark have been distilled into [ROOT_CONTROL_REFERENCE.md](../docs/framework/ROOT_CONTROL_REFERENCE.md) or [ROOT_THEME_SYSTEM_FINDINGS.md](ROOT_THEME_SYSTEM_FINDINGS.md). The raw dumps remain the authoritative source — the docs are curated summaries.

**Context warning:** Three files exceed 30k lines (`-AvaloniaResources.cs`, `XamlIlHelpers.cs`, `StylesAll.cs`). Do not read these in full — use targeted line ranges or grep.

---

## Source Assemblies

| Assembly | Version | Files | Description |
|----------|---------|-------|-------------|
| `Root` | 0.9.92.0 | 1 | Root.exe entry point |
| `RootApp.Client.Avalonia` | 0.9.92.0 | 99 | Main UI assembly — views, styles, themes, controls, settings |
| `RootApp.Client.Domain` | 0.9.92.0 | 5 | Domain layer (DataStoreKeys, ILocalDataStore, LocalDataStore, extensions, secure storage) |
| `RootApp.Client.CoreDomain` | 0.9.92.0 | 3 | Core domain (IRootSessionAccessor, RootSessionAccessor, RootSession) |
| `Avalonia.Controls` | 11.3.12.0 | 3 | Framework controls (Application, CheckBox, ToggleSwitch) |
| `Avalonia.Themes.Simple` | 11.3.12.0 | 1 | SimpleTheme base class |
| `AvaloniaEdit` | 11.3.0.0 | 3 | Text editor theme resources |

---

## Namespace Map

Namespaces discovered from ILSpy comment headers (line 2 of each file):

```
Root (exe)
├── Program                                          → Program.cs

RootApp.Client.Avalonia
├── App                                              → App.cs
├── Helpers
│   └── Navigation
│       ├── Navigator                                → Navigator.cs
│       └── DirectMessageOpenerService               → DirectMessageOpenerService.cs
├── IViewModelBase                                   → IViewModelBase.cs
├── Controls
│   ├── HexInputBorder                               → HexInputBorder.cs
│   ├── MemberVisibilityOption                       → MemberVisibilityOption.cs
│   ├── RootBorder                                   → RootBorder.cs
│   ├── RootChannelTypeRadioButton                   → RootChannelTypeRadioButton.cs
│   ├── RootCircleProgressBar                        → RootCircleProgressBar.cs
│   ├── RootCircularPanel                            → RootCircularPanel.cs
│   ├── RootConfirmationControl                      → RootConfirmationControl.cs
│   ├── RootFlyout                                   → RootFlyout.cs
│   ├── RootImageLoader                              → RootImageLoader.cs
│   ├── RootLinkButton                               → RootLinkButton.cs
│   ├── RootMarkdownTextBlock                        → RootMarkdownTextBlock.cs
│   ├── RootMemberVisibilitySwitch                   → RootMemberVisibilitySwitch.cs
│   ├── RootMenuFlyout                               → RootMenuFlyout.cs
│   ├── RootMessageItemsControl                      → RootMessageItemsControl.cs
│   ├── RootMessageScrollViewer                      → RootMessageScrollViewer.cs
│   ├── RootMultiCheckBox                            → RootMultiCheckBox.cs
│   ├── RootPercentageSlider                         → RootPercentageSlider.cs
│   ├── RootScrollBarThumb                           → RootScrollBarThumb.cs
│   ├── RootScrollViewer                             → RootScrollViewer.cs
│   ├── RootSplitView                                → RootSplitView.cs
│   ├── RootSvgButton                                → RootSvgButton.cs
│   ├── RootSvgCheckBox                              → RootSvgCheckBox.cs
│   ├── RootSvgImage                                 → RootSvgImage.cs
│   ├── RootTextbox                                  → RootTextbox.cs
│   ├── RootToolTip                                  → RootToolTip.cs
│   ├── RootTrimTooltipTextBlock                     → RootTrimTooltipTextBlock.cs
│   ├── RootWebApiStatus                             → RootWebApiStatus.cs
│   ├── RootZoomContainer                            → RootZoomContainer.cs
│   ├── SaveChangesView                              → SaveChangesView.cs
│   ├── StreamerModeBanner                           → StreamerModeBanner.cs
│   ├── StreamerModeBannerViewModel                  → StreamerModeBannerViewModel.cs
│   ├── TextWithBadgePanel                           → TextWithBadgePanel.cs
│   └── Settings
│       ├── IPage                                    → IPage.cs
│       ├── MenuItemPageContainerView                → MenuItemPageContainerView.cs
│       ├── MenuItemPageContainerViewModel           → MenuItemPageContainerViewModel.cs
│       └── RootSettingsContainer                    → RootSettingsContainer.cs
├── Resources
│   ├── Converters
│   │   └── ThemeToBoolConverter                     → ThemeToBoolConverter.cs
│   └── Themes
│       ├── RootThemeEnum                            → RootThemeEnum.cs
│       ├── ThemeMapper                              → ThemeMapper.cs
│       └── ThemeService                             → ThemeService.cs
├── UI
│   ├── Community
│   │   ├── Members
│   │   │   └── MembersViewModel                    (referenced in XamlIlTrampolines.cs)
│   │   └── Settings
│   │       └── CommunityLogViewModel               (referenced in XamlIlTrampolines.cs)
│   ├── Home
│   │   ├── CommunityTabViewModel                   (referenced in XamlIlTrampolines.cs)
│   │   └── SystemTray.Profile.Settings
│   │       ├── ChangeThemeView                      → ChangeThemeView.cs
│   │       ├── ChangeThemeViewModel                 → ChangeThemeViewModel.cs
│   │       ├── ChangeThemeViewModelFactory          → ChangeThemeViewModelFactory.cs
│   │       ├── ChatView                             → ChatView.cs
│   │       └── ChatViewModel                        → ChatViewModel.cs
│   ├── Main
│   │   ├── ConnectionBlockingView                   → ConnectionBlockingView.cs
│   │   ├── ConnectionBlockingViewModel              → ConnectionBlockingViewModel.cs
│   │   ├── ConnectionBlockingViewModelFactory       → ConnectionBlockingViewModelFactory.cs
│   │   ├── MainView                                 → MainView.cs
│   │   ├── MainViewModel                            → MainViewModel.cs
│   │   ├── MainViewModelFactory                     → MainViewModelFactory.cs
│   │   └── MainWindow                               → MainWindow.cs
│   └── Messages
│       ├── ChannelStartMessageView                  → ChannelStartMessageView.cs
│       ├── ChannelStartMessageViewModel             → ChannelStartMessageViewModel.cs
│       ├── MessageView                              → MessageView.cs
│       └── MessageViewModel                         → MessageViewModel.cs

RootApp.Client.CoreDomain
├── IRootSessionAccessor                                 → IRootSessionAccessor.cs
├── RootSessionAccessor                                  → RootSessionAccessor.cs
└── RootSession                                          → RootSession.cs

RootApp.Client.Domain
└── Helpers.Store
    ├── DataStoreKeys                                → DataStoreKeys.cs
    ├── ILocalDataStore                              → ILocalDataStore.cs
    ├── LocalDataStore                               → LocalDataStore.cs
    ├── LocalDataStoreExtensions                     → LocalDataStoreExtensions.cs
    └── SecureStorageImplementation                   → SecureStorageImplementation.cs

CompiledAvaloniaXaml (XAML-to-C# generated code)
├── !AvaloniaResources                               → -AvaloniaResources.cs (55k lines)
│   ├── XamlClosure_53                               → XamlClosure_53.cs
│   ├── XamlClosure_54                               → XamlClosure_54.cs
│   └── XamlClosure_55                               → XamlClosure_55.cs
├── XamlDynamicSetters                               → XamlDynamicSetters.cs
├── XamlIlContext                                    → XamlIlContext.cs
├── XamlIlHelpers                                    → XamlIlHelpers.cs (33k lines)
└── XamlIlTrampolines                                → XamlIlTrampolines.cs

Avalonia.Controls
├── Application                                      → Application.cs
├── CheckBox                                         → CheckBox.cs
└── ToggleSwitch                                     → ToggleSwitch.cs

Avalonia.Themes.Simple
└── SimpleTheme                                      → SimpleTheme.cs

AvaloniaEdit (CompiledAvaloniaXaml.!AvaloniaResources.NamespaceInfo)
├── /Themes/Base.xaml                                → NamespaceInfo_Themes_Base.cs
├── /Themes/Fluent/AvaloniaEdit.xaml                 → NamespaceInfo_Themes_Fluent.cs
└── /Themes/Simple/AvaloniaEdit.xaml                 → NamespaceInfo_Themes_Simple.cs
```

---

## File Inventory

### Views (code-behind)

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `MessageView.cs` | 3,602 | `UI.Messages.MessageView` | Y | Chat message control — named controls, updateBackgroundColor(), action bar, context menu, mention/hover highlights, role colors |
| `ChatView.cs` | 424 | `UI.Home.SystemTray.Profile.Settings.ChatView` | partial | Settings > Chat page — emoji toggle, tap-to-reply toggle |
| `ChangeThemeView.cs` | 815 | `UI.Home.SystemTray.Profile.Settings.ChangeThemeView` | Y | Settings > Theme picker — RadioButton per theme, preview SVGs |
| `ChannelStartMessageView.cs` | 352 | `UI.Messages.ChannelStartMessageView` | N | "Welcome to #channel" start message |
| `App.cs` | 635 | `RootApp.Client.Avalonia.App` | partial | Application subclass — Initialize(), resource/style registration |
| `MainWindow.cs` | 317 | `UI.Main.MainWindow` | Y | Top-level window — title bar, tray icon, close/minimize behavior |
| `MainView.cs` | 259 | `UI.Main.MainView` | Y | Main content shell — hosts RootSplitView, navigation |
| `ConnectionBlockingView.cs` | 91 | `UI.Main.ConnectionBlockingView` | N | Connection blocked / offline banner |
| `SaveChangesView.cs` | 396 | `Controls.SaveChangesView` | Y | Settings save/revert bar UI |
| `StreamerModeBanner.cs` | 202 | `Controls.StreamerModeBanner` | N | Streamer mode notification banner |
| `MenuItemPageContainerView.cs` | 281 | `Controls.Settings.MenuItemPageContainerView` | N | Settings page container with menu item navigation |

### ViewModels

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `MessageViewModel.cs` | 786 | `UI.Messages.MessageViewModel` | partial | Message data: HasSelfMention, HasLocalPendingReply, content, timestamps, edit state |
| `ChatViewModel.cs` | 116 | `UI.Home.SystemTray.Profile.Settings.ChatViewModel` | Y | Settings > Chat VM: AutoConvertEmojis, TapToReply (IPage, DataStore) |
| `ChangeThemeViewModel.cs` | 55 | `UI.Home.SystemTray.Profile.Settings.ChangeThemeViewModel` | Y | Theme picker VM: Theme property, ThemeToBoolConverter binding |
| `ChangeThemeViewModelFactory.cs` | 10 | `UI.Home.SystemTray.Profile.Settings.ChangeThemeViewModelFactory` | Y | DI factory for ChangeThemeViewModel |
| `ChannelStartMessageViewModel.cs` | 17 | `UI.Messages.ChannelStartMessageViewModel` | Y | Thin wrapper: exposes Message property |
| `MainViewModel.cs` | 400 | `UI.Main.MainViewModel` | Y | Top-level VM — DataContext chain root, navigation, DI container |
| `MainViewModelFactory.cs` | 24 | `UI.Main.MainViewModelFactory` | N | DI factory for MainViewModel |
| `ConnectionBlockingViewModel.cs` | 14 | `UI.Main.ConnectionBlockingViewModel` | N | Connection blocking state |
| `ConnectionBlockingViewModelFactory.cs` | 12 | `UI.Main.ConnectionBlockingViewModelFactory` | N | DI factory for ConnectionBlockingViewModel |
| `StreamerModeBannerViewModel.cs` | 67 | `Controls.StreamerModeBannerViewModel` | N | Streamer mode toggle state |
| `MenuItemPageContainerViewModel.cs` | 135 | `Controls.Settings.MenuItemPageContainerViewModel` | Y | Settings page container VM — page navigation, menu item selection |
| `IViewModelBase.cs` | 12 | `RootApp.Client.Avalonia.IViewModelBase` | Y | ViewModel base interface — IsTopMostViewModel, ValidateProperty, IDisposable |
| `DirectMessageOpenerService.cs` | 109 | `Helpers.Navigation.DirectMessageOpenerService` | Y | DM opener — DotNetBrowser discovery chain link, WeakReferenceMessenger tab navigation |

### Custom Controls

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `RootBorder.cs` | 61 | `Controls.RootBorder` | Y | Border subclass with DPI-aware DynamicBorderThickness |
| `RootMenuFlyout.cs` | 97 | `Controls.RootMenuFlyout` | Y | Context menu flyout — Translate plugin prerequisite |
| `RootMessageItemsControl.cs` | 378 | `Controls.RootMessageItemsControl` | Y | Virtualized message list — critical for MessageLogger |
| `RootMessageScrollViewer.cs` | 645 | `Controls.RootMessageScrollViewer` | N | Message list scroll container with load-more behavior |
| `RootMarkdownTextBlock.cs` | 91 | `Controls.RootMarkdownTextBlock` | N | Markdown renderer entry point (CTextBlock, CSpan, etc.) |
| `RootScrollViewer.cs` | 431 | `Controls.RootScrollViewer` | N | Custom scroll viewer with opacity-based thumb show/hide |
| `RootScrollBarThumb.cs` | 61 | `Controls.RootScrollBarThumb` | N | Custom scrollbar thumb |
| `RootSvgImage.cs` | 144 | `Controls.RootSvgImage` | N | SVG image with per-theme path binding |
| `RootSvgButton.cs` | 35 | `Controls.RootSvgButton` | N | Icon button (styled in Style_SvgButton.cs) |
| `RootSvgCheckBox.cs` | 35 | `Controls.RootSvgCheckBox` | N | Checkbox with SVG check mark |
| `RootImageLoader.cs` | 178 | `Controls.RootImageLoader` | N | Image loading control with placeholder |
| `RootLinkButton.cs` | 30 | `Controls.RootLinkButton` | N | Username display button |
| `RootFlyout.cs` | 172 | `Controls.RootFlyout` | N | Flyout popup base control |
| `RootConfirmationControl.cs` | 901 | `Controls.RootConfirmationControl` | N | Confirmation dialog (delete/leave/ban flows) |
| `RootTextbox.cs` | 996 | `Controls.RootTextbox` | N | Custom textbox with validation, character limit, helper text |
| `RootMultiCheckBox.cs` | 478 | `Controls.RootMultiCheckBox` | N | Multi-option checkbox group |
| `RootMemberVisibilitySwitch.cs` | 747 | `Controls.RootMemberVisibilitySwitch` | N | Member visibility toggle (complex multi-state control) |
| `RootCircularPanel.cs` | 290 | `Controls.RootCircularPanel` | N | Circular layout panel |
| `RootCircleProgressBar.cs` | 277 | `Controls.RootCircleProgressBar` | N | Circular progress indicator |
| `RootColorPicker.cs` | — | `Controls.RootColorPicker` | N | Color picker (styled in Style_RootColorPicker.cs, code-behind not yet split) |
| `RootWebApiStatus.cs` | 282 | `Controls.RootWebApiStatus` | N | Web API status indicator |
| `RootZoomContainer.cs` | 79 | `Controls.RootZoomContainer` | N | Zoom/scale container |
| `RootToolTip.cs` | 70 | `Controls.RootToolTip` | N | Custom tooltip |
| `RootTrimTooltipTextBlock.cs` | 51 | `Controls.RootTrimTooltipTextBlock` | N | TextBlock with tooltip on text trim |
| `RootPercentageSlider.cs` | 35 | `Controls.RootPercentageSlider` | N | Percentage slider |
| `RootChannelTypeRadioButton.cs` | 61 | `Controls.RootChannelTypeRadioButton` | N | Channel type radio button |
| `RootSplitView.cs` | 13 | `Controls.RootSplitView` | N | Split view (thin wrapper — template in Style_RootSplitView.cs) |
| `HexInputBorder.cs` | 123 | `Controls.HexInputBorder` | N | Hex color input with border |
| `TextWithBadgePanel.cs` | 66 | `Controls.TextWithBadgePanel` | N | Text + badge layout panel |
| `MemberVisibilityOption.cs` | 8 | `Controls.MemberVisibilityOption` | N | Enum for member visibility options |

### Markdown Components

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `CInline.cs` | 195 | `Controls.Markdown.CInline` | Y | Abstract inline base — StyledElement, text properties, MeasureOverride/AsString abstract methods, invalidation chain |
| `CRun.cs` | 75 | `Controls.Markdown.CRun` | Y | Leaf inline — plain text, TextFormatter line breaking |
| `CSpan.cs` | 306 | `Controls.Markdown.CSpan` | Y | Container inline — child inlines, border/padding support, DecoratorGeometry wrapping |
| `CHyperlink.cs` | 155 | `Controls.Markdown.CHyperlink` | Y | Clickable link — extends CSpan, Command/CommandParameter, hover/press handlers |
| `CCode.cs` | 13 | `Controls.Markdown.CCode` | Y | Code span — trivial CSpan subclass, styling from Style_MessageMarkdown |
| `CImage.cs` | 174 | `Controls.Markdown.CImage` | Y | Inline image — async BitmapWrapper loading, aspect ratio, shrink-to-fit |
| `CTextBlock.cs` | 988 | `Controls.Markdown.CTextBlock` | Y | Rich text renderer — AvaloniaList<CInline> content, CGeometry layout, text selection, pointer hit-testing |

### Settings Infrastructure

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `RootSettingsContainer.cs` | 990 | `Controls.Settings.RootSettingsContainer` | Y | Settings page host — save bar, page switching, revert logic |
| `IPage.cs` | 7 | `Controls.Settings.IPage` | Y | Settings page interface |
| `MenuItemPageContainerView.cs` | 281 | `Controls.Settings.MenuItemPageContainerView` | N | Settings page container with menu navigation |
| `MenuItemPageContainerViewModel.cs` | 135 | `Controls.Settings.MenuItemPageContainerViewModel` | Y | Settings page container VM |
| `Navigator.cs` | 287 | `Helpers.Navigation.Navigator` | Y | Settings page navigation stack — push/pop, HasPendingChanges, save/revert events |

### Data Store / Persistence

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `DataStoreKeys.cs` | 69 | `RootApp.Client.Domain.Helpers.Store.DataStoreKeys` | Y | All 68 settings keys (theme, audio, overlay, analytics, streamer mode) |
| `ILocalDataStore.cs` | 13 | `RootApp.Client.Domain.Helpers.Store.ILocalDataStore` | Y | Settings persistence interface |
| `LocalDataStore.cs` | 271 | `RootApp.Client.Domain.Helpers.Store.LocalDataStore` | Y | Settings persistence implementation |
| `LocalDataStoreExtensions.cs` | 232 | `RootApp.Client.Domain.Helpers.Store.LocalDataStoreExtensions` | Y | Extension methods for ILocalDataStore |
| `SecureStorageImplementation.cs` | 86 | `RootApp.Client.Domain.Helpers.Store.SecureStorageImplementation` | Y | Encrypted credential storage |

### Session / Core Domain

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `IRootSessionAccessor.cs` | 14 | `RootApp.Client.CoreDomain.IRootSessionAccessor` | Y | Session accessor interface — INotifyPropertyChanged, SetSession/ClearSession |
| `RootSessionAccessor.cs` | 47 | `RootApp.Client.CoreDomain.RootSessionAccessor` | Y | ObservableObject wrapper around Session [ObservableProperty] |
| `RootSession.cs` | 505 | `RootApp.Client.CoreDomain.RootSession` | Y | Core session object — all service refs, packet dispatch, reconnect logic |

### Theme System

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `ThemeService.cs` | 64 | `Resources.Themes.ThemeService` | Y | SetTheme(), InitializeTheme(), IsDefaultColor(), GetInvertedDefaultColorHex() |
| `ThemeMapper.cs` | 75 | `Resources.Themes.ThemeMapper` | Y | PureDark ThemeVariant, enum/variant conversion, WebRTC/AppBridge theme strings |
| `ThemeToBoolConverter.cs` | 24 | `Resources.Converters.ThemeToBoolConverter` | Y | RadioButton binding converter for theme picker |
| `RootThemeEnum.cs` | 9 | `Resources.Themes.RootThemeEnum` | Y | Enum: Default, Dark, Light, PureDark |
| `ThemesDarkAxaml.cs` | 285 | compiled XAML | Y | Dark theme: 32 color keys + ~220 SVG paths |
| `ThemesLightAxaml.cs` | 284 | compiled XAML | Y | Light theme: 32 color keys + ~220 SVG paths |
| `ThemesPureDarkAxaml.cs` | 283 | compiled XAML | Y | PureDark theme: overrides bg/border keys only (inherits Dark) |

### Style Files (compiled XAML — control templates and visual states)

All from `RootApp.Client.Avalonia` / `CompiledAvaloniaXaml.!AvaloniaResources`.

| File | Lines | Analyzed | Controls Styled |
|------|------:|:--------:|-----------------|
| `Style_BorderButton.cs` | 356 | Y | Button classes: BorderButton, ListBorderButton, BasicButton, BasicButtonNeverOpaque |
| `Style_TransparentButton.cs` | 227 | Y | TransparentButton, TransparentButtonWithHighlight/Opacity/ClickEffect |
| `Style_SvgButton.cs` | 341 | Y | RootSvgButton: standard, SvgDimmedButton, Custom, CustomSvgDimmedButton |
| `Style_CheckBox.cs` | 220 | Y | CheckBox default + `.ToggleSwitch` class (45x25 toggle pill) |
| `Style_ComboBox.cs` | 521 | partial | ComboBox: template, dropdown, input background |
| `Style_ComboBoxItem.cs` | 313 | Y | ComboBoxItem: hover/pressed/selected states with HighlightNormal/HighlightStrong |
| `Style_ListBox.cs` | 80 | Y | ListBox: transparent background/border |
| `Style_ListBoxItem.cs` | 111 | Y | ListBoxItem: all states transparent (selection highlight is per-template) |
| `Style_MenuItem.cs` | 193 | partial | MenuItem: hover = HighlightNormal, DeleteMenuItem class |
| `Style_ScrollViewer.cs` | 242 | Y | RootScrollViewer + RootScrollBarThumb: opacity-based show/hide |
| `Style_Slider.cs` | 238 | partial | Slider: BrandPrimary foreground, Border track background |
| `Style_TabItem.cs` | 103 | Y | TabItem: selected pipe, TextPrimary foreground |
| `Style_MessageMarkdown.cs` | 1,087 | Y | RootMarkdown + SimpleMessage: CTextBlock, mention pills, code blocks, blockquotes, headings |
| `Style_RootSplitView.cs` | 626 | partial | RootSplitView: main app shell layout (sidebar + content split) |
| `Style_LinkButton.cs` | 85 | partial | RootLinkButton: username display button |
| `Style_Separator.cs` | 83 | partial | Separator: Border color, 0.5px thickness |
| `Style_DropDownButton.cs` | 136 | N | DropDownButton: BackgroundTertiary bg, Border border |
| `Style_DragTabItem.cs` | 54 | N | DragTabItem: draggable tab styling |
| `Style_FlyoutPresenter.cs` | 55 | N | FlyoutPresenter: popup container |
| `Style_MenuFlyoutPresenter.cs` | 71 | N | MenuFlyoutPresenter: context menu container |
| `Style_RootColorPicker.cs` | 106 | N | RootColorPicker: color picker control |
| `Style_RootImageLoader.cs` | 76 | N | RootImageLoader: image loading/placeholder |
| `Style_BorderlessTextbox.cs` | 75 | N | BorderlessTextbox: no-border text input |
| `Style_TextButton.cs` | 73 | N | TextButton: text-only button variant |
| `Style_ToolTip.cs` | 71 | N | ToolTip: tooltip popup styling |
| `Style_TabsControl.cs` | 48 | N | TabsControl: tab container |
| `Style_TabsTheme.cs` | 53 | N | TabsTheme: tab visual theme |
| `StylesAll.cs` | 5,228 | N | Aggregate: all 27 style registrations in one file |

### App Infrastructure

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `Program.cs` | 65 | `Root.Program` | Y | Entry point: STA thread, Velopack, RootLauncher.Run(), BuildAvaloniaApp() |
| `App.cs` | 635 | `RootApp.Client.Avalonia.App` | partial | App.Initialize(): XAML trampoline, theme init, menu delay |
| `DataStoreKeys.cs` | 69 | `RootApp.Client.Domain.Helpers.Store.DataStoreKeys` | Y | All 68 settings keys (theme, audio, overlay, analytics, streamer mode) |

### Avalonia Framework Internals

| File | Lines | Namespace | Analyzed | Description |
|------|------:|-----------|:--------:|-------------|
| `Application.cs` | 394 | `Avalonia.Application` | partial | Application base: Resources, Styles, ActualThemeVariantChanged, TryGetResource() |
| `CheckBox.cs` | 14 | `Avalonia.Controls.CheckBox` | Y | CheckBox : ToggleButton (trivial — just AutomationControlType override) |
| `ToggleSwitch.cs` | 88 | `Avalonia.Controls.ToggleSwitch` | partial | ToggleSwitch : ToggleButton — template parts, knob transitions |
| `SimpleTheme.cs` | 1,022 | `Avalonia.Themes.Simple.SimpleTheme` | partial | SimpleTheme base: ThemeControlHighlightLowBrush and other base keys |
| `AppBuilder.cs` | 325 | `Avalonia.AppBuilder` | N | Avalonia AppBuilder fluent API |

### AvaloniaEdit Theme Resources

| File | Lines | Namespace | Description |
|------|------:|-----------|-------------|
| `NamespaceInfo_Themes_Base.cs` | 97 | `AvaloniaEdit` `/Themes/Base.xaml` | Base text editor theme |
| `NamespaceInfo_Themes_Fluent.cs` | 100 | `AvaloniaEdit` `/Themes/Fluent/AvaloniaEdit.xaml` | Fluent-themed editor |
| `NamespaceInfo_Themes_Simple.cs` | 100 | `AvaloniaEdit` `/Themes/Simple/AvaloniaEdit.xaml` | Simple-themed editor |

### Resource / Font / Sound Definitions

| File | Lines | Namespace | Description |
|------|------:|-----------|-------------|
| `FontsAxaml.cs` | 26 | compiled XAML | Font family resource definitions |
| `SoundsAxaml.cs` | 39 | compiled XAML | Sound resource URI definitions |

### XAML Compiler Infrastructure

These are generated by Avalonia's XAML-to-IL compiler. Mostly boilerplate, but contain useful type resolution info.

| File | Lines | Description |
|------|------:|-------------|
| `-AvaloniaResources.cs` | 55,296 | **HUGE.** Master resource population — all views, styles, themes, resources compiled into one file. Contains every `Populate_*` method. |
| `XamlIlHelpers.cs` | 33,645 | **HUGE.** XAML IL helper methods — type resolution, property setters, binding infrastructure |
| `StylesAll.cs` | 5,228 | All 27 style file registrations aggregated |
| `XamlDynamicSetters.cs` | 584 | Dynamic property setter delegates for compiled bindings |
| `XamlIlContext.cs` | 274 | XAML IL compilation context — type mapping, namespace URIs |
| `XamlClosure_53.cs` | 233 | XAML closure: control template builder (theme preview) |
| `XamlClosure_54.cs` | 233 | XAML closure: control template builder (theme preview) |
| `XamlClosure_55.cs` | 233 | XAML closure: control template builder (theme preview) |
| `XamlIlTrampolines.cs` | 36 | Command trampolines — reveals RootSettingsContainer, MembersViewModel, CommunityLogViewModel, CommunityTabViewModel |

---

## Not Yet Decompiled

Classes referenced in the dumps but not present as standalone files. Candidates for future decompilation:

### Views
- Channel list view(s) — channel sidebar
- Members panel view(s) — `MembersViewModel` referenced
- DM list view
- Voice/call UI views
- Profile popup views — would refine ProfileBadgeInjector heuristics
- Community settings views — `CommunityLogViewModel` referenced
- `CommunityTabViewModel` / view — community tab

### ViewModels
- `MembersViewModel` — members panel
- `CommunityLogViewModel` — community audit log
- `CommunityTabViewModel` — community tab

### Services
- Navigation service (how Root switches between pages)

---

## Cross-Reference: Dumps → Docs

| Document | Source Dumps |
|----------|-------------|
| [ROOT_CONTROL_REFERENCE.md](../docs/framework/ROOT_CONTROL_REFERENCE.md) | MessageView, MessageViewModel, ChatView, ChatViewModel, ChangeThemeView, ChangeThemeViewModel, RootBorder, ThemeService, ThemeMapper, RootThemeEnum, ThemeToBoolConverter, DataStoreKeys, Program, App, Style_CheckBox, Style_ComboBoxItem, Style_ListBoxItem, Style_SvgButton, Style_BorderButton, Style_TransparentButton, Style_ScrollViewer, Style_TabItem, Style_MessageMarkdown, Style_RootSplitView (partial), MainWindow, MainView, MainViewModel, RootSettingsContainer, SaveChangesView, IPage, MenuItemPageContainerViewModel, RootMessageItemsControl, RootMenuFlyout, ILocalDataStore, LocalDataStore, LocalDataStoreExtensions, SecureStorageImplementation, Navigator, IRootSessionAccessor, RootSessionAccessor, RootSession, IViewModelBase, DirectMessageOpenerService, **CInline, CRun, CSpan, CHyperlink, CCode, CImage, CTextBlock** |
| [ROOT_THEME_SYSTEM_FINDINGS.md](ROOT_THEME_SYSTEM_FINDINGS.md) | ThemesDarkAxaml, ThemesLightAxaml, ThemesPureDarkAxaml |
| [THEME_ENGINE_DEEP_DIVE.md](../docs/framework/THEME_ENGINE_DEEP_DIVE.md) | ThemeService, ThemeMapper, SimpleTheme (partial) |

---

*Last updated: 2026-02-19 — 112 files from Root v0.9.92, Avalonia 11.3.12, AvaloniaEdit 11.3.0. Batch split: Controls (32), Controls.Settings (4), UI.Main (7), Domain.Helpers.Store (5).*
