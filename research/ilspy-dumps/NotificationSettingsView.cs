// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.NotificationSettingsView
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;

public class NotificationSettingsView : UserControl
{
	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public NotificationSettingsView()
	{
		InitializeComponent();
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	[ExcludeFromCodeCoverage]
	public void InitializeComponent(bool P_0 = true)
	{
		if (P_0)
		{
			_0021XamlIlPopulateTrampoline(this);
		}
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulate(IServiceProvider P_0, NotificationSettingsView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<NotificationSettingsView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<NotificationSettingsView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FHome_002FSystemTray_002FProfile_002FSettings_002FNotificationSettingsView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Home/SystemTray/Profile/Settings/NotificationSettingsView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		RootScrollViewer rootScrollViewer2;
		RootScrollViewer rootScrollViewer = (rootScrollViewer2 = new RootScrollViewer());
		((ISupportInitialize)rootScrollViewer).BeginInit();
		P_1.Content = rootScrollViewer;
		RootScrollViewer rootScrollViewer4;
		RootScrollViewer rootScrollViewer3 = (rootScrollViewer4 = rootScrollViewer2);
		context.PushParent(rootScrollViewer4);
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		rootScrollViewer4.Content = stackPanel;
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		stackPanel5.Margin = new Thickness(24.0, 24.0, 24.0, 24.0);
		Controls children = stackPanel5.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.NotificationSettings;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj);
		textBlock5.FontWeight = FontWeight.Medium;
		textBlock5.FontSize = 20.0;
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding);
		textBlock5.Margin = new Thickness(0.0, 0.0, 0.0, 28.0);
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		Controls children2 = stackPanel5.Children;
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		children2.Add(rootBorder);
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		RootBorder rootBorder5 = rootBorder4;
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, backgroundProperty, binding2);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, borderBrushProperty, binding3);
		rootBorder5.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder5.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		rootBorder5.Padding = new Thickness(24.0, 24.0, 24.0, 24.0);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		rootBorder5.Child = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		Grid grid5 = grid4;
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		ColumnDefinitions columnDefinitions = grid5.ColumnDefinitions;
		ColumnDefinition columnDefinition = new ColumnDefinition();
		columnDefinition.MinWidth = 104.0;
		columnDefinition.Width = new GridLength(0.0, GridUnitType.Auto);
		columnDefinitions.Add(columnDefinition);
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		Controls children3 = grid5.Children;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		children3.Add(stackPanel6);
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		Controls children4 = stackPanel9.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children4.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock9.FontSize = 14.0;
		textBlock9.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.DesktopNotificationsTitle;
		textBlock9.TextWrapping = TextWrapping.Wrap;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding4);
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj2);
		textBlock9.FontWeight = FontWeight.Bold;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		Controls children5 = stackPanel9.Children;
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		children5.Add(textBlock10);
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		textBlock13.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		textBlock13.LineHeight = 20.0;
		textBlock13.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.DesktopNotificationsDescription;
		textBlock13.TextWrapping = TextWrapping.Wrap;
		textBlock13.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, foregroundProperty3, binding5);
		textBlock13.FontSize = 14.0;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj3 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock13, obj3);
		textBlock13.FontWeight = (FontWeight)450;
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		Controls children6 = grid5.Children;
		CheckBox checkBox2;
		CheckBox checkBox = (checkBox2 = new CheckBox());
		((ISupportInitialize)checkBox).BeginInit();
		children6.Add(checkBox);
		CheckBox checkBox4;
		CheckBox checkBox3 = (checkBox4 = checkBox2);
		context.PushParent(checkBox4);
		CheckBox checkBox5 = checkBox4;
		Grid.SetColumn(checkBox5, 2);
		checkBox5.Classes.Add("ToggleSwitch");
		StyledProperty<bool?> isCheckedProperty = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension obj4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002ESettings_002ENotificationSettingsViewModel_002CRootApp_002EClient_002EAvalonia_002EDesktopNotifications_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension = obj4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(checkBox5, isCheckedProperty, compiledBindingExtension);
		checkBox5.HorizontalAlignment = HorizontalAlignment.Right;
		checkBox5.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)checkBox3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		Controls children7 = stackPanel5.Children;
		RootBorder rootBorder7;
		RootBorder rootBorder6 = (rootBorder7 = new RootBorder());
		((ISupportInitialize)rootBorder6).BeginInit();
		children7.Add(rootBorder6);
		RootBorder rootBorder8 = (rootBorder4 = rootBorder7);
		context.PushParent(rootBorder4);
		RootBorder rootBorder9 = rootBorder4;
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, backgroundProperty2, binding6);
		StyledProperty<IBrush?> borderBrushProperty2 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, borderBrushProperty2, binding7);
		rootBorder9.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder9.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		rootBorder9.Padding = new Thickness(24.0, 24.0, 24.0, 24.0);
		rootBorder9.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		rootBorder9.Child = grid6;
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		ColumnDefinitions columnDefinitions2 = grid9.ColumnDefinitions;
		ColumnDefinition columnDefinition2 = new ColumnDefinition();
		columnDefinition2.MinWidth = 104.0;
		columnDefinition2.Width = new GridLength(0.0, GridUnitType.Auto);
		columnDefinitions2.Add(columnDefinition2);
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		Controls children8 = grid9.Children;
		StackPanel stackPanel11;
		StackPanel stackPanel10 = (stackPanel11 = new StackPanel());
		((ISupportInitialize)stackPanel10).BeginInit();
		children8.Add(stackPanel10);
		StackPanel stackPanel12 = (stackPanel4 = stackPanel11);
		context.PushParent(stackPanel4);
		StackPanel stackPanel13 = stackPanel4;
		Controls children9 = stackPanel13.Children;
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		children9.Add(textBlock14);
		TextBlock textBlock16 = (textBlock4 = textBlock15);
		context.PushParent(textBlock4);
		TextBlock textBlock17 = textBlock4;
		textBlock17.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock17.FontSize = 14.0;
		textBlock17.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.NotificationSoundsTitle;
		textBlock17.TextWrapping = TextWrapping.Wrap;
		StyledProperty<IBrush?> foregroundProperty4 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock17, foregroundProperty4, binding8);
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj5 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock17, obj5);
		textBlock17.FontWeight = FontWeight.Bold;
		context.PopParent();
		((ISupportInitialize)textBlock16).EndInit();
		Controls children10 = stackPanel13.Children;
		TextBlock textBlock19;
		TextBlock textBlock18 = (textBlock19 = new TextBlock());
		((ISupportInitialize)textBlock18).BeginInit();
		children10.Add(textBlock18);
		TextBlock textBlock20 = (textBlock4 = textBlock19);
		context.PushParent(textBlock4);
		TextBlock textBlock21 = textBlock4;
		textBlock21.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		textBlock21.LineHeight = 20.0;
		textBlock21.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.NotificationSoundsDescription;
		textBlock21.TextWrapping = TextWrapping.Wrap;
		textBlock21.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty5 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock21, foregroundProperty5, binding9);
		textBlock21.FontSize = 14.0;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj6 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock21, obj6);
		textBlock21.FontWeight = (FontWeight)450;
		context.PopParent();
		((ISupportInitialize)textBlock20).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel12).EndInit();
		Controls children11 = grid9.Children;
		CheckBox checkBox7;
		CheckBox checkBox6 = (checkBox7 = new CheckBox());
		((ISupportInitialize)checkBox6).BeginInit();
		children11.Add(checkBox6);
		CheckBox checkBox8 = (checkBox4 = checkBox7);
		context.PushParent(checkBox4);
		CheckBox checkBox9 = checkBox4;
		Grid.SetColumn(checkBox9, 2);
		checkBox9.Classes.Add("ToggleSwitch");
		StyledProperty<bool?> isCheckedProperty2 = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension obj7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002ESettings_002ENotificationSettingsViewModel_002CRootApp_002EClient_002EAvalonia_002ENotificationSounds_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension2 = obj7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(checkBox9, isCheckedProperty2, compiledBindingExtension2);
		checkBox9.HorizontalAlignment = HorizontalAlignment.Right;
		checkBox9.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)checkBox8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder8).EndInit();
		Controls children12 = stackPanel5.Children;
		RootBorder rootBorder11;
		RootBorder rootBorder10 = (rootBorder11 = new RootBorder());
		((ISupportInitialize)rootBorder10).BeginInit();
		children12.Add(rootBorder10);
		RootBorder rootBorder12 = (rootBorder4 = rootBorder11);
		context.PushParent(rootBorder4);
		RootBorder rootBorder13 = rootBorder4;
		StyledProperty<IBrush?> backgroundProperty3 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder13, backgroundProperty3, binding10);
		StyledProperty<IBrush?> borderBrushProperty3 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder13, borderBrushProperty3, binding11);
		rootBorder13.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder13.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		rootBorder13.Padding = new Thickness(24.0, 24.0, 24.0, 24.0);
		rootBorder13.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002ESettings_002ENotificationSettingsViewModel_002CRootApp_002EClient_002EAvalonia_002EShouldShowTaskbarAttention_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder13, isVisibleProperty, compiledBindingExtension4);
		Grid grid11;
		Grid grid10 = (grid11 = new Grid());
		((ISupportInitialize)grid10).BeginInit();
		rootBorder13.Child = grid10;
		Grid grid12 = (grid4 = grid11);
		context.PushParent(grid4);
		Grid grid13 = grid4;
		grid13.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		ColumnDefinitions columnDefinitions3 = grid13.ColumnDefinitions;
		ColumnDefinition columnDefinition3 = new ColumnDefinition();
		columnDefinition3.MinWidth = 104.0;
		columnDefinition3.Width = new GridLength(0.0, GridUnitType.Auto);
		columnDefinitions3.Add(columnDefinition3);
		grid13.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		Controls children13 = grid13.Children;
		StackPanel stackPanel15;
		StackPanel stackPanel14 = (stackPanel15 = new StackPanel());
		((ISupportInitialize)stackPanel14).BeginInit();
		children13.Add(stackPanel14);
		StackPanel stackPanel16 = (stackPanel4 = stackPanel15);
		context.PushParent(stackPanel4);
		StackPanel stackPanel17 = stackPanel4;
		Controls children14 = stackPanel17.Children;
		TextBlock textBlock23;
		TextBlock textBlock22 = (textBlock23 = new TextBlock());
		((ISupportInitialize)textBlock22).BeginInit();
		children14.Add(textBlock22);
		TextBlock textBlock24 = (textBlock4 = textBlock23);
		context.PushParent(textBlock4);
		TextBlock textBlock25 = textBlock4;
		textBlock25.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock25.FontSize = 14.0;
		textBlock25.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.TaskbarAttentionTitle;
		textBlock25.TextWrapping = TextWrapping.Wrap;
		StyledProperty<IBrush?> foregroundProperty6 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding12 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock25, foregroundProperty6, binding12);
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj8 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock25, obj8);
		textBlock25.FontWeight = FontWeight.Bold;
		context.PopParent();
		((ISupportInitialize)textBlock24).EndInit();
		Controls children15 = stackPanel17.Children;
		TextBlock textBlock27;
		TextBlock textBlock26 = (textBlock27 = new TextBlock());
		((ISupportInitialize)textBlock26).BeginInit();
		children15.Add(textBlock26);
		TextBlock textBlock28 = (textBlock4 = textBlock27);
		context.PushParent(textBlock4);
		TextBlock textBlock29 = textBlock4;
		textBlock29.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		textBlock29.LineHeight = 20.0;
		textBlock29.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.TaskbarAttentionDescription;
		textBlock29.TextWrapping = TextWrapping.Wrap;
		textBlock29.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty7 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding13 = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock29, foregroundProperty7, binding13);
		textBlock29.FontSize = 14.0;
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj9 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock29, obj9);
		textBlock29.FontWeight = (FontWeight)450;
		context.PopParent();
		((ISupportInitialize)textBlock28).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel16).EndInit();
		Controls children16 = grid13.Children;
		CheckBox checkBox11;
		CheckBox checkBox10 = (checkBox11 = new CheckBox());
		((ISupportInitialize)checkBox10).BeginInit();
		children16.Add(checkBox10);
		CheckBox checkBox12 = (checkBox4 = checkBox11);
		context.PushParent(checkBox4);
		CheckBox checkBox13 = checkBox4;
		Grid.SetColumn(checkBox13, 2);
		checkBox13.Classes.Add("ToggleSwitch");
		StyledProperty<bool?> isCheckedProperty3 = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension obj10 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002ESettings_002ENotificationSettingsViewModel_002CRootApp_002EClient_002EAvalonia_002ETaskbarAttention_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension5 = obj10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(checkBox13, isCheckedProperty3, compiledBindingExtension5);
		checkBox13.HorizontalAlignment = HorizontalAlignment.Right;
		checkBox13.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)checkBox12).EndInit();
		context.PopParent();
		((ISupportInitialize)grid12).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder12).EndInit();
		Controls children17 = stackPanel5.Children;
		RootBorder rootBorder15;
		RootBorder rootBorder14 = (rootBorder15 = new RootBorder());
		((ISupportInitialize)rootBorder14).BeginInit();
		children17.Add(rootBorder14);
		RootBorder rootBorder16 = (rootBorder4 = rootBorder15);
		context.PushParent(rootBorder4);
		RootBorder rootBorder17 = rootBorder4;
		StyledProperty<IBrush?> backgroundProperty4 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding14 = dynamicResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder17, backgroundProperty4, binding14);
		StyledProperty<IBrush?> borderBrushProperty4 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension15 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding15 = dynamicResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder17, borderBrushProperty4, binding15);
		rootBorder17.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder17.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		rootBorder17.Padding = new Thickness(24.0, 24.0, 24.0, 24.0);
		rootBorder17.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension6 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002ESettings_002ENotificationSettingsViewModel_002CRootApp_002EClient_002EAvalonia_002EShouldShowUseNativeNotifications_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension7 = compiledBindingExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder17, isVisibleProperty2, compiledBindingExtension7);
		Grid grid15;
		Grid grid14 = (grid15 = new Grid());
		((ISupportInitialize)grid14).BeginInit();
		rootBorder17.Child = grid14;
		Grid grid16 = (grid4 = grid15);
		context.PushParent(grid4);
		Grid grid17 = grid4;
		grid17.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		ColumnDefinitions columnDefinitions4 = grid17.ColumnDefinitions;
		ColumnDefinition columnDefinition4 = new ColumnDefinition();
		columnDefinition4.MinWidth = 104.0;
		columnDefinition4.Width = new GridLength(0.0, GridUnitType.Auto);
		columnDefinitions4.Add(columnDefinition4);
		grid17.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		Controls children18 = grid17.Children;
		StackPanel stackPanel19;
		StackPanel stackPanel18 = (stackPanel19 = new StackPanel());
		((ISupportInitialize)stackPanel18).BeginInit();
		children18.Add(stackPanel18);
		StackPanel stackPanel20 = (stackPanel4 = stackPanel19);
		context.PushParent(stackPanel4);
		StackPanel stackPanel21 = stackPanel4;
		Controls children19 = stackPanel21.Children;
		TextBlock textBlock31;
		TextBlock textBlock30 = (textBlock31 = new TextBlock());
		((ISupportInitialize)textBlock30).BeginInit();
		children19.Add(textBlock30);
		TextBlock textBlock32 = (textBlock4 = textBlock31);
		context.PushParent(textBlock4);
		TextBlock textBlock33 = textBlock4;
		textBlock33.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock33.FontSize = 14.0;
		textBlock33.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.NativeNotificationsTitle;
		textBlock33.TextWrapping = TextWrapping.Wrap;
		StyledProperty<IBrush?> foregroundProperty8 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension16 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding16 = dynamicResourceExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock33, foregroundProperty8, binding16);
		StaticResourceExtension staticResourceExtension8 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj11 = staticResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock33, obj11);
		textBlock33.FontWeight = FontWeight.Bold;
		context.PopParent();
		((ISupportInitialize)textBlock32).EndInit();
		Controls children20 = stackPanel21.Children;
		TextBlock textBlock35;
		TextBlock textBlock34 = (textBlock35 = new TextBlock());
		((ISupportInitialize)textBlock34).BeginInit();
		children20.Add(textBlock34);
		TextBlock textBlock36 = (textBlock4 = textBlock35);
		context.PushParent(textBlock4);
		TextBlock textBlock37 = textBlock4;
		textBlock37.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		textBlock37.LineHeight = 20.0;
		textBlock37.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.NativeNotificationsDescription;
		textBlock37.TextWrapping = TextWrapping.Wrap;
		textBlock37.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty9 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension17 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding17 = dynamicResourceExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock37, foregroundProperty9, binding17);
		textBlock37.FontSize = 14.0;
		StaticResourceExtension staticResourceExtension9 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj12 = staticResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock37, obj12);
		textBlock37.FontWeight = (FontWeight)450;
		context.PopParent();
		((ISupportInitialize)textBlock36).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel20).EndInit();
		Controls children21 = grid17.Children;
		CheckBox checkBox15;
		CheckBox checkBox14 = (checkBox15 = new CheckBox());
		((ISupportInitialize)checkBox14).BeginInit();
		children21.Add(checkBox14);
		CheckBox checkBox16 = (checkBox4 = checkBox15);
		context.PushParent(checkBox4);
		CheckBox checkBox17 = checkBox4;
		Grid.SetColumn(checkBox17, 2);
		checkBox17.Classes.Add("ToggleSwitch");
		StyledProperty<bool?> isCheckedProperty4 = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension obj13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002ESettings_002ENotificationSettingsViewModel_002CRootApp_002EClient_002EAvalonia_002EUseNativeNotifications_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension8 = obj13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(checkBox17, isCheckedProperty4, compiledBindingExtension8);
		checkBox17.HorizontalAlignment = HorizontalAlignment.Right;
		checkBox17.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)checkBox16).EndInit();
		context.PopParent();
		((ISupportInitialize)grid16).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder16).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootScrollViewer3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(NotificationSettingsView P_0)
	{
		if (_0021XamlIlPopulateOverride != null)
		{
			_0021XamlIlPopulateOverride(P_0);
		}
		else
		{
			_0021XamlIlPopulate(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(null), P_0);
		}
	}
}

