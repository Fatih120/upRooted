// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootMemberVisibilitySwitch
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Resources.Strings;

public class RootMemberVisibilitySwitch : UserControl
{
	public static readonly StyledProperty<MemberVisibilityOption> SelectedOptionProperty = AvaloniaProperty.Register<RootMemberVisibilitySwitch, MemberVisibilityOption>("SelectedOption", MemberVisibilityOption.CommunityMembers);

	public static readonly StyledProperty<bool> IsInTallModeProperty = AvaloniaProperty.Register<RootMemberVisibilitySwitch, bool>("IsInTallMode", false);

	public static readonly StyledProperty<double> CommunitySvgOpacityProperty = AvaloniaProperty.Register<RootMemberVisibilitySwitch, double>("CommunitySvgOpacity", 0.6);

	public static readonly StyledProperty<double> ChannelSvgOpacityProperty = AvaloniaProperty.Register<RootMemberVisibilitySwitch, double>("ChannelSvgOpacity", 1.0);

	public static readonly StyledProperty<IBrush> SelectedCommunityBorderBackgroundProperty = AvaloniaProperty.Register<RootMemberVisibilitySwitch, IBrush>("SelectedCommunityBorderBackground", Brushes.Transparent);

	public static readonly StyledProperty<IBrush> SelectedChannelBorderBackgroundProperty = AvaloniaProperty.Register<RootMemberVisibilitySwitch, IBrush>("SelectedChannelBorderBackground", Brushes.Transparent);

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootBorder TallBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border TallCommunityOuterBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border ChannelOuterBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootBorder WideBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border WideCommunityOuterBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border WideChannelOuterBorder;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public MemberVisibilityOption SelectedOption
	{
		get
		{
			return GetValue(SelectedOptionProperty);
		}
		set
		{
			SetValue(SelectedOptionProperty, value2);
			UpdateVisualStates();
		}
	}

	private bool IsInTallMode
	{
		set
		{
			SetValue(IsInTallModeProperty, value2);
		}
	}

	private double CommunitySvgOpacity
	{
		set
		{
			SetValue(CommunitySvgOpacityProperty, value2);
		}
	}

	private double ChannelSvgOpacity
	{
		set
		{
			SetValue(ChannelSvgOpacityProperty, value2);
		}
	}

	private IBrush SelectedCommunityBorderBackground
	{
		set
		{
			SetValue(SelectedCommunityBorderBackgroundProperty, value2);
		}
	}

	private IBrush SelectedChannelBorderBackground
	{
		set
		{
			SetValue(SelectedChannelBorderBackgroundProperty, value2);
		}
	}

	public RootMemberVisibilitySwitch()
	{
		InitializeComponent();
	}

	protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnAttachedToVisualTree(P_0);
		DetermineTallMode();
		UpdateVisualStates();
	}

	protected override void OnSizeChanged(SizeChangedEventArgs P_0)
	{
		base.OnSizeChanged(P_0);
		DetermineTallMode();
	}

	private void DetermineTallMode()
	{
		IsInTallMode = base.Bounds.Width < 100.0;
	}

	private void UpdateVisualStates()
	{
		IBrush brush = (IBrush)Application.Current.FindResource(Application.Current.ActualThemeVariant, "HighlightNormal");
		switch (SelectedOption)
		{
		case MemberVisibilityOption.CommunityMembers:
			CommunitySvgOpacity = 1.0;
			ChannelSvgOpacity = 0.6;
			SelectedCommunityBorderBackground = brush;
			SelectedChannelBorderBackground = Brushes.Transparent;
			break;
		case MemberVisibilityOption.ChannelMembers:
			CommunitySvgOpacity = 0.6;
			ChannelSvgOpacity = 1.0;
			SelectedCommunityBorderBackground = Brushes.Transparent;
			SelectedChannelBorderBackground = brush;
			break;
		}
	}

	private void onCommunityMemberClicked(object? sender, RoutedEventArgs e)
	{
		SelectedOption = MemberVisibilityOption.CommunityMembers;
	}

	private void onChannelMemberClicked(object? sender, RoutedEventArgs e)
	{
		SelectedOption = MemberVisibilityOption.ChannelMembers;
	}

	private void onActualThemeVariantChanged(object? sender, EventArgs e)
	{
		UpdateVisualStates();
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	[ExcludeFromCodeCoverage]
	public void InitializeComponent(bool P_0 = true)
	{
		if (P_0)
		{
			_0021XamlIlPopulateTrampoline(this);
		}
		INameScope nameScope = this.FindNameScope();
		TallBorder = nameScope?.Find<RootBorder>("TallBorder");
		TallCommunityOuterBorder = nameScope?.Find<Border>("TallCommunityOuterBorder");
		ChannelOuterBorder = nameScope?.Find<Border>("ChannelOuterBorder");
		WideBorder = nameScope?.Find<RootBorder>("WideBorder");
		WideCommunityOuterBorder = nameScope?.Find<Border>("WideCommunityOuterBorder");
		WideChannelOuterBorder = nameScope?.Find<Border>("WideChannelOuterBorder");
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulate(IServiceProvider P_0, RootMemberVisibilitySwitch P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<RootMemberVisibilitySwitch> context = new CompiledAvaloniaXaml.XamlIlContext.Context<RootMemberVisibilitySwitch>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FControls_002FRootMemberVisibilitySwitch_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Controls/RootMemberVisibilitySwitch.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.ActualThemeVariantChanged += context.RootObject.onActualThemeVariantChanged;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Self().Build());
		context.ProvideTargetProperty = StyledElement.DataContextProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_6(P_1, compiledBindingExtension2);
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		P_1.Content = panel;
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		Controls children = panel4.Children;
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		children.Add(rootBorder);
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		RootBorder rootBorder5 = rootBorder4;
		rootBorder5.Name = "TallBorder";
		object obj = rootBorder5;
		context.AvaloniaNameScope.Register("TallBorder", obj);
		rootBorder5.Width = 40.0;
		rootBorder5.Height = 76.0;
		rootBorder5.CornerRadius = new CornerRadius(10.0, 10.0, 10.0, 10.0);
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, backgroundProperty, binding);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, borderBrushProperty, binding2);
		rootBorder5.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(IsInTallModeProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, isVisibleProperty, compiledBindingExtension4);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		rootBorder5.Child = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		Grid grid5 = grid4;
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		Controls children2 = grid5.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children2.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Border border5 = border4;
		Grid.SetRow(border5, 0);
		border5.Name = "TallCommunityOuterBorder";
		obj = border5;
		context.AvaloniaNameScope.Register("TallCommunityOuterBorder", obj);
		border5.CornerRadius = new CornerRadius(8.0, 8.0, 0.0, 0.0);
		border5.Margin = new Thickness(2.0, 2.0, 2.0, 0.0);
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(SelectedCommunityBorderBackgroundProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = Border.BackgroundProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, backgroundProperty2, compiledBindingExtension6);
		border5.ClipToBounds = true;
		ToolTip.SetPlacement(border5, PlacementMode.Right);
		ToolTip.SetVerticalOffset(border5, 0.0);
		ToolTip.SetHorizontalOffset(border5, 4.0);
		ToolTip.SetShowDelay(border5, 0);
		RootToolTip rootToolTip2;
		RootToolTip rootToolTip = (rootToolTip2 = new RootToolTip());
		((ISupportInitialize)rootToolTip).BeginInit();
		ToolTip.SetTip(border5, rootToolTip);
		RootToolTip rootToolTip4;
		RootToolTip rootToolTip3 = (rootToolTip4 = rootToolTip2);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip5 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip5, PlacementMode.Right);
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(IsInTallModeProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootToolTip5, isVisibleProperty2, compiledBindingExtension8);
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		rootToolTip5.Content = textBlock;
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ShowCommunityMembers;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj2);
		textBlock5.FontWeight = (FontWeight)450;
		textBlock5.FontSize = 14.0;
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip3).EndInit();
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		border5.Child = button;
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		Button button5 = button4;
		button5.AddHandler((RoutedEvent)Button.ClickEvent, (Delegate)new EventHandler<RoutedEventArgs>(context.RootObject.onCommunityMemberClicked), RoutingStrategies.Direct | RoutingStrategies.Bubble, false);
		button5.Classes.Add("BasicButton");
		button5.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button5.Background = new ImmutableSolidColorBrush(16777215u);
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		button5.Content = rootSvgImage;
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage5 = rootSvgImage4;
		StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("CommunityMembersSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage5, svgPathProperty, binding3);
		rootSvgImage5.Width = 21.0;
		rootSvgImage5.Height = 16.0;
		StyledProperty<double> opacityProperty = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CommunitySvgOpacityProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage5, opacityProperty, compiledBindingExtension10);
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		Controls children3 = grid5.Children;
		Border border7;
		Border border6 = (border7 = new Border());
		((ISupportInitialize)border6).BeginInit();
		children3.Add(border6);
		Border border8 = (border4 = border7);
		context.PushParent(border4);
		Border border9 = border4;
		Grid.SetRow(border9, 1);
		border9.Name = "ChannelOuterBorder";
		obj = border9;
		context.AvaloniaNameScope.Register("ChannelOuterBorder", obj);
		border9.CornerRadius = new CornerRadius(0.0, 0.0, 8.0, 8.0);
		border9.Margin = new Thickness(2.0, 0.0, 2.0, 2.0);
		StyledProperty<IBrush?> backgroundProperty3 = Border.BackgroundProperty;
		CompiledBindingExtension compiledBindingExtension11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(SelectedChannelBorderBackgroundProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = Border.BackgroundProperty;
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border9, backgroundProperty3, compiledBindingExtension12);
		border9.ClipToBounds = true;
		ToolTip.SetPlacement(border9, PlacementMode.Right);
		ToolTip.SetVerticalOffset(border9, 0.0);
		ToolTip.SetHorizontalOffset(border9, 4.0);
		ToolTip.SetShowDelay(border9, 0);
		RootToolTip rootToolTip7;
		RootToolTip rootToolTip6 = (rootToolTip7 = new RootToolTip());
		((ISupportInitialize)rootToolTip6).BeginInit();
		ToolTip.SetTip(border9, rootToolTip6);
		RootToolTip rootToolTip8 = (rootToolTip4 = rootToolTip7);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip9 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip9, PlacementMode.Right);
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(IsInTallModeProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootToolTip9, isVisibleProperty3, compiledBindingExtension14);
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		rootToolTip9.Content = textBlock6;
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ShowChannelMembers;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj3 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj3);
		textBlock9.FontWeight = (FontWeight)450;
		textBlock9.FontSize = 14.0;
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock9.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip8).EndInit();
		Button button7;
		Button button6 = (button7 = new Button());
		((ISupportInitialize)button6).BeginInit();
		border9.Child = button6;
		Button button8 = (button4 = button7);
		context.PushParent(button4);
		Button button9 = button4;
		button9.AddHandler((RoutedEvent)Button.ClickEvent, (Delegate)new EventHandler<RoutedEventArgs>(context.RootObject.onChannelMemberClicked), RoutingStrategies.Direct | RoutingStrategies.Bubble, false);
		button9.Classes.Add("BasicButton");
		button9.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button9.Background = new ImmutableSolidColorBrush(16777215u);
		RootSvgImage rootSvgImage7;
		RootSvgImage rootSvgImage6 = (rootSvgImage7 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage6).BeginInit();
		button9.Content = rootSvgImage6;
		RootSvgImage rootSvgImage8 = (rootSvgImage4 = rootSvgImage7);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage9 = rootSvgImage4;
		StyledProperty<string?> svgPathProperty2 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("ChannelMembersSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage9, svgPathProperty2, binding4);
		rootSvgImage9.Width = 14.0;
		rootSvgImage9.Height = 14.0;
		StyledProperty<double> opacityProperty2 = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension15 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(ChannelSvgOpacityProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension16 = compiledBindingExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage9, opacityProperty2, compiledBindingExtension16);
		context.PopParent();
		((ISupportInitialize)rootSvgImage8).EndInit();
		context.PopParent();
		((ISupportInitialize)button8).EndInit();
		context.PopParent();
		((ISupportInitialize)border8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		Controls children4 = panel4.Children;
		RootBorder rootBorder7;
		RootBorder rootBorder6 = (rootBorder7 = new RootBorder());
		((ISupportInitialize)rootBorder6).BeginInit();
		children4.Add(rootBorder6);
		RootBorder rootBorder8 = (rootBorder4 = rootBorder7);
		context.PushParent(rootBorder4);
		RootBorder rootBorder9 = rootBorder4;
		rootBorder9.Name = "WideBorder";
		obj = rootBorder9;
		context.AvaloniaNameScope.Register("WideBorder", obj);
		rootBorder9.Height = 40.0;
		rootBorder9.Padding = new Thickness(4.0, 4.0, 4.0, 4.0);
		rootBorder9.CornerRadius = new CornerRadius(16.0, 16.0, 16.0, 16.0);
		StyledProperty<IBrush?> backgroundProperty4 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, backgroundProperty4, binding5);
		StyledProperty<IBrush?> borderBrushProperty2 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, borderBrushProperty2, binding6);
		rootBorder9.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension17 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(IsInTallModeProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension18 = compiledBindingExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, isVisibleProperty4, compiledBindingExtension18);
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
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		Controls children5 = grid9.Children;
		Border border11;
		Border border10 = (border11 = new Border());
		((ISupportInitialize)border10).BeginInit();
		children5.Add(border10);
		Border border12 = (border4 = border11);
		context.PushParent(border4);
		Border border13 = border4;
		border13.Name = "WideCommunityOuterBorder";
		obj = border13;
		context.AvaloniaNameScope.Register("WideCommunityOuterBorder", obj);
		border13.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		StyledProperty<IBrush?> backgroundProperty5 = Border.BackgroundProperty;
		CompiledBindingExtension compiledBindingExtension19 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(SelectedCommunityBorderBackgroundProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = Border.BackgroundProperty;
		CompiledBindingExtension compiledBindingExtension20 = compiledBindingExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border13, backgroundProperty5, compiledBindingExtension20);
		border13.ClipToBounds = true;
		Button button11;
		Button button10 = (button11 = new Button());
		((ISupportInitialize)button10).BeginInit();
		border13.Child = button10;
		Button button12 = (button4 = button11);
		context.PushParent(button4);
		Button button13 = button4;
		button13.AddHandler((RoutedEvent)Button.ClickEvent, (Delegate)new EventHandler<RoutedEventArgs>(context.RootObject.onCommunityMemberClicked), RoutingStrategies.Direct | RoutingStrategies.Bubble, false);
		button13.Classes.Add("BasicButton");
		button13.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button13.Background = new ImmutableSolidColorBrush(16777215u);
		button13.HorizontalContentAlignment = HorizontalAlignment.Stretch;
		button13.VerticalContentAlignment = VerticalAlignment.Stretch;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		button13.Content = stackPanel;
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		stackPanel5.Orientation = Orientation.Horizontal;
		stackPanel5.HorizontalAlignment = HorizontalAlignment.Center;
		Controls children6 = stackPanel5.Children;
		RootSvgImage rootSvgImage11;
		RootSvgImage rootSvgImage10 = (rootSvgImage11 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage10).BeginInit();
		children6.Add(rootSvgImage10);
		RootSvgImage rootSvgImage12 = (rootSvgImage4 = rootSvgImage11);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage13 = rootSvgImage4;
		StyledProperty<string?> svgPathProperty3 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("CommunityMembersSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage13, svgPathProperty3, binding7);
		rootSvgImage13.Width = 18.0;
		rootSvgImage13.Height = 14.0;
		StyledProperty<double> opacityProperty3 = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension21 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CommunitySvgOpacityProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension22 = compiledBindingExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage13, opacityProperty3, compiledBindingExtension22);
		rootSvgImage13.Margin = new Thickness(0.0, 0.0, 12.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rootSvgImage12).EndInit();
		Controls children7 = stackPanel5.Children;
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		children7.Add(textBlock10);
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		textBlock13.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Community;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj4 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock13, obj4);
		textBlock13.FontWeight = FontWeight.Medium;
		textBlock13.FontSize = 12.0;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, foregroundProperty, binding8);
		textBlock13.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<double> opacityProperty4 = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension23 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CommunitySvgOpacityProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension24 = compiledBindingExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, opacityProperty4, compiledBindingExtension24);
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)button12).EndInit();
		context.PopParent();
		((ISupportInitialize)border12).EndInit();
		Controls children8 = grid9.Children;
		Border border15;
		Border border14 = (border15 = new Border());
		((ISupportInitialize)border14).BeginInit();
		children8.Add(border14);
		Border border16 = (border4 = border15);
		context.PushParent(border4);
		Border border17 = border4;
		border17.Name = "WideChannelOuterBorder";
		obj = border17;
		context.AvaloniaNameScope.Register("WideChannelOuterBorder", obj);
		Grid.SetColumn(border17, 1);
		border17.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		StyledProperty<IBrush?> backgroundProperty6 = Border.BackgroundProperty;
		CompiledBindingExtension compiledBindingExtension25 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(SelectedChannelBorderBackgroundProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = Border.BackgroundProperty;
		CompiledBindingExtension compiledBindingExtension26 = compiledBindingExtension25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border17, backgroundProperty6, compiledBindingExtension26);
		border17.ClipToBounds = true;
		Button button15;
		Button button14 = (button15 = new Button());
		((ISupportInitialize)button14).BeginInit();
		border17.Child = button14;
		Button button16 = (button4 = button15);
		context.PushParent(button4);
		Button button17 = button4;
		button17.AddHandler((RoutedEvent)Button.ClickEvent, (Delegate)new EventHandler<RoutedEventArgs>(context.RootObject.onChannelMemberClicked), RoutingStrategies.Direct | RoutingStrategies.Bubble, false);
		button17.Classes.Add("BasicButton");
		button17.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button17.Background = new ImmutableSolidColorBrush(16777215u);
		button17.HorizontalContentAlignment = HorizontalAlignment.Stretch;
		button17.VerticalContentAlignment = VerticalAlignment.Stretch;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		button17.Content = stackPanel6;
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		stackPanel9.Orientation = Orientation.Horizontal;
		stackPanel9.HorizontalAlignment = HorizontalAlignment.Center;
		Controls children9 = stackPanel9.Children;
		RootSvgImage rootSvgImage15;
		RootSvgImage rootSvgImage14 = (rootSvgImage15 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage14).BeginInit();
		children9.Add(rootSvgImage14);
		RootSvgImage rootSvgImage16 = (rootSvgImage4 = rootSvgImage15);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage17 = rootSvgImage4;
		StyledProperty<string?> svgPathProperty4 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("ChannelMembersSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage17, svgPathProperty4, binding9);
		rootSvgImage17.Width = 12.0;
		rootSvgImage17.Height = 12.0;
		StyledProperty<double> opacityProperty5 = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension27 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(ChannelSvgOpacityProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension28 = compiledBindingExtension27.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage17, opacityProperty5, compiledBindingExtension28);
		rootSvgImage17.Margin = new Thickness(0.0, 0.0, 12.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rootSvgImage16).EndInit();
		Controls children10 = stackPanel9.Children;
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		children10.Add(textBlock14);
		TextBlock textBlock16 = (textBlock4 = textBlock15);
		context.PushParent(textBlock4);
		TextBlock textBlock17 = textBlock4;
		textBlock17.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Channel;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj5 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock17, obj5);
		textBlock17.FontWeight = FontWeight.Medium;
		textBlock17.FontSize = 12.0;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock17, foregroundProperty2, binding10);
		textBlock17.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<double> opacityProperty6 = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension29 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(ChannelSvgOpacityProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension30 = compiledBindingExtension29.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock17, opacityProperty6, compiledBindingExtension30);
		context.PopParent();
		((ISupportInitialize)textBlock16).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		context.PopParent();
		((ISupportInitialize)button16).EndInit();
		context.PopParent();
		((ISupportInitialize)border16).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder8).EndInit();
		context.PopParent();
		((ISupportInitialize)panel3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(RootMemberVisibilitySwitch P_0)
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

