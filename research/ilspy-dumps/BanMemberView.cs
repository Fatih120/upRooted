// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.BanMemberView
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Xaml.Interactions.Core;
using Avalonia.Xaml.Interactions.Custom;
using Avalonia.Xaml.Interactivity;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Community.Members;

public class BanMemberView : UserControl
{
	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border BackgroundBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootBorder MainBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootTextbox BanReasonTextBox;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public BanMemberView()
	{
		InitializeComponent();
	}

	protected override void OnLoaded(RoutedEventArgs P_0)
	{
		base.OnLoaded(P_0);
		BanReasonTextBox.FocusTextBox();
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
		BackgroundBorder = nameScope?.Find<Border>("BackgroundBorder");
		MainBorder = nameScope?.Find<RootBorder>("MainBorder");
		BanReasonTextBox = nameScope?.Find<RootTextbox>("BanReasonTextBox");
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulate(IServiceProvider P_0, BanMemberView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<BanMemberView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<BanMemberView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FCommunity_002FMembers_002FBanMemberView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Members/BanMemberView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		P_1.Content = panel;
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		Panel panel5 = panel4;
		Controls children = panel5.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		border4.Name = "BackgroundBorder";
		object obj = border4;
		context.AvaloniaNameScope.Register("BackgroundBorder", obj);
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("DropShadow");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border4, backgroundProperty, binding);
		BehaviorCollection behaviors = Interaction.GetBehaviors(border4);
		RoutedEventTriggerBehavior routedEventTriggerBehavior = new RoutedEventTriggerBehavior();
		((ISupportInitialize)routedEventTriggerBehavior).BeginInit();
		RoutedEventTriggerBehavior routedEventTriggerBehavior2 = routedEventTriggerBehavior;
		context.PushParent(routedEventTriggerBehavior2);
		routedEventTriggerBehavior2.RoutedEvent = InputElement.PointerPressedEvent;
		ActionCollection? actions = routedEventTriggerBehavior2.Actions;
		InvokeCommandAction invokeCommandAction = new InvokeCommandAction();
		((ISupportInitialize)invokeCommandAction).BeginInit();
		InvokeCommandAction invokeCommandAction2 = invokeCommandAction;
		context.PushParent(invokeCommandAction2);
		StyledProperty<ICommand?> commandProperty = InvokeCommandActionBase.CommandProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EBanMemberViewModel_002CRootApp_002EClient_002EAvalonia_002ECloseViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = InvokeCommandActionBase.CommandProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(invokeCommandAction2, commandProperty, compiledBindingExtension2);
		context.PopParent();
		((ISupportInitialize)invokeCommandAction).EndInit();
		actions.Add(invokeCommandAction);
		context.PopParent();
		((ISupportInitialize)routedEventTriggerBehavior).EndInit();
		behaviors.Add(routedEventTriggerBehavior);
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		Controls children2 = panel5.Children;
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		children2.Add(rootBorder);
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		rootBorder4.Name = "MainBorder";
		obj = rootBorder4;
		context.AvaloniaNameScope.Register("MainBorder", obj);
		rootBorder4.Width = 480.0;
		rootBorder4.Margin = new Thickness(0.0, 35.0, 0.0, 35.0);
		rootBorder4.MaxHeight = 800.0;
		rootBorder4.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, backgroundProperty2, binding2);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, borderBrushProperty, binding3);
		rootBorder4.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder4.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		rootBorder4.Child = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		Grid grid5 = grid4;
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(59.0, GridUnitType.Pixel)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(64.0, GridUnitType.Pixel)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		Controls children3 = grid5.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children3.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		Grid.SetRow(textBlock5, 0);
		textBlock5.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.BanMember;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding4);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj2);
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock5.FontSize = 20.0;
		textBlock5.FontWeight = (FontWeight)450;
		textBlock5.Margin = new Thickness(24.0, 0.0, 0.0, 0.0);
		textBlock5.HorizontalAlignment = HorizontalAlignment.Left;
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		Controls children4 = grid5.Children;
		RootSvgButton rootSvgButton2;
		RootSvgButton rootSvgButton = (rootSvgButton2 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton).BeginInit();
		children4.Add(rootSvgButton);
		RootSvgButton rootSvgButton4;
		RootSvgButton rootSvgButton3 = (rootSvgButton4 = rootSvgButton2);
		context.PushParent(rootSvgButton4);
		Grid.SetRow(rootSvgButton4, 0);
		rootSvgButton4.Classes.Add("SvgDimmedButton");
		rootSvgButton4.Margin = new Thickness(0.0, 0.0, 24.0, 0.0);
		StyledProperty<ICommand?> commandProperty2 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EBanMemberViewModel_002CRootApp_002EClient_002EAvalonia_002ECloseViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton4, commandProperty2, compiledBindingExtension4);
		rootSvgButton4.HorizontalAlignment = HorizontalAlignment.Right;
		rootSvgButton4.Width = 25.0;
		rootSvgButton4.Height = 25.0;
		StyledProperty<string> svgPathProperty = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("ExitThickSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton4, svgPathProperty, binding5);
		rootSvgButton4.SvgWidth = 13.0;
		rootSvgButton4.SvgHeight = 13.0;
		context.PopParent();
		((ISupportInitialize)rootSvgButton3).EndInit();
		Controls children5 = grid5.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children5.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		Grid.SetRow(stackPanel5, 1);
		stackPanel5.Margin = new Thickness(32.0, 32.0, 32.0, 20.0);
		Controls children6 = stackPanel5.Children;
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		children6.Add(grid6);
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		ColumnDefinitions columnDefinitions = new ColumnDefinitions();
		columnDefinitions.Capacity = 2;
		columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		grid9.ColumnDefinitions = columnDefinitions;
		Controls children7 = grid9.Children;
		Panel panel7;
		Panel panel6 = (panel7 = new Panel());
		((ISupportInitialize)panel6).BeginInit();
		children7.Add(panel6);
		Panel panel8 = (panel4 = panel7);
		context.PushParent(panel4);
		Panel panel9 = panel4;
		panel9.VerticalAlignment = VerticalAlignment.Center;
		Controls children8 = panel9.Children;
		Ellipse ellipse2;
		Ellipse ellipse = (ellipse2 = new Ellipse());
		((ISupportInitialize)ellipse).BeginInit();
		children8.Add(ellipse);
		Ellipse ellipse4;
		Ellipse ellipse3 = (ellipse4 = ellipse2);
		context.PushParent(ellipse4);
		ellipse4.Width = 64.0;
		ellipse4.Height = 64.0;
		ellipse4.StrokeThickness = 1.0;
		StyledProperty<IBrush?> strokeProperty = Shape.StrokeProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = Shape.StrokeProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(ellipse4, strokeProperty, binding6);
		ellipse4.Opacity = 0.1;
		context.PopParent();
		((ISupportInitialize)ellipse3).EndInit();
		Controls children9 = panel9.Children;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		children9.Add(rootSvgImage);
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		StyledProperty<string?> svgPathProperty2 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("UserBannedSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage4, svgPathProperty2, binding7);
		rootSvgImage4.Width = 24.0;
		rootSvgImage4.Height = 24.0;
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		context.PopParent();
		((ISupportInitialize)panel8).EndInit();
		Controls children10 = grid9.Children;
		RootMarkdownTextBlock rootMarkdownTextBlock2;
		RootMarkdownTextBlock rootMarkdownTextBlock = (rootMarkdownTextBlock2 = new RootMarkdownTextBlock());
		((ISupportInitialize)rootMarkdownTextBlock).BeginInit();
		children10.Add(rootMarkdownTextBlock);
		RootMarkdownTextBlock rootMarkdownTextBlock4;
		RootMarkdownTextBlock rootMarkdownTextBlock3 = (rootMarkdownTextBlock4 = rootMarkdownTextBlock2);
		context.PushParent(rootMarkdownTextBlock4);
		Grid.SetColumn(rootMarkdownTextBlock4, 1);
		rootMarkdownTextBlock4.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<IMarkdownEngine?> engineProperty = RootMarkdownTextBlock.EngineProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EBanMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EMarkdownEngine_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootMarkdownTextBlock.EngineProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMarkdownTextBlock4, engineProperty, compiledBindingExtension6);
		DirectProperty<RootMarkdownTextBlock, string?> markdownProperty = RootMarkdownTextBlock.MarkdownProperty;
		CompiledBindingExtension obj3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EBanMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002EMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EUserName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			StringFormat = RootApp.Client.Avalonia.Resources.Strings.Resources.BanMemberConfirmationMessage
		};
		context.ProvideTargetProperty = RootMarkdownTextBlock.MarkdownProperty;
		CompiledBindingExtension compiledBindingExtension7 = obj3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMarkdownTextBlock4, markdownProperty, compiledBindingExtension7);
		rootMarkdownTextBlock4.HorizontalAlignment = HorizontalAlignment.Left;
		rootMarkdownTextBlock4.Margin = new Thickness(20.0, 0.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rootMarkdownTextBlock3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		Controls children11 = stackPanel5.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children11.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension obj4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EBanMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002EMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EUserName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			StringFormat = RootApp.Client.Avalonia.Resources.Strings.Resources.BanReasonTitle
		};
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension8 = obj4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, textProperty, compiledBindingExtension8);
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding8);
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj5 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj5);
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock9.Margin = new Thickness(0.0, 32.0, 0.0, 8.0);
		textBlock9.FontSize = 13.0;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		Controls children12 = stackPanel5.Children;
		RootTextbox rootTextbox2;
		RootTextbox rootTextbox = (rootTextbox2 = new RootTextbox());
		((ISupportInitialize)rootTextbox).BeginInit();
		children12.Add(rootTextbox);
		RootTextbox rootTextbox4;
		RootTextbox rootTextbox3 = (rootTextbox4 = rootTextbox2);
		context.PushParent(rootTextbox4);
		rootTextbox4.Name = "BanReasonTextBox";
		obj = rootTextbox4;
		context.AvaloniaNameScope.Register("BanReasonTextBox", obj);
		rootTextbox4.PlaceholderText = RootApp.Client.Avalonia.Resources.Strings.Resources.BanReason;
		StyledProperty<string> textProperty2 = RootTextbox.TextProperty;
		CompiledBindingExtension obj6 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EBanMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EBanReason_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = RootTextbox.TextProperty;
		CompiledBindingExtension compiledBindingExtension9 = obj6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox4, textProperty2, compiledBindingExtension9);
		rootTextbox4.ValidationPropertyName = "BanReason";
		StyledProperty<IBrush> borderBackgroundBrushProperty = RootTextbox.BorderBackgroundBrushProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("Input");
		context.ProvideTargetProperty = RootTextbox.BorderBackgroundBrushProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox4, borderBackgroundBrushProperty, binding9);
		StyledProperty<IBrush> borderBorderBrushProperty = RootTextbox.BorderBorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = RootTextbox.BorderBorderBrushProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox4, borderBorderBrushProperty, binding10);
		rootTextbox4.BorderBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj7 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(rootTextbox4, obj7);
		rootTextbox4.BorderHeight = 52.0;
		rootTextbox4.BorderCornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		rootTextbox4.TextboxFontSize = 15.0;
		rootTextbox4.TextboxMargin = new Thickness(20.0, 0.0, 20.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rootTextbox3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		Controls children13 = grid5.Children;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		children13.Add(stackPanel6);
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		Grid.SetRow(stackPanel9, 2);
		stackPanel9.VerticalAlignment = VerticalAlignment.Center;
		stackPanel9.HorizontalAlignment = HorizontalAlignment.Right;
		stackPanel9.Orientation = Orientation.Horizontal;
		stackPanel9.Margin = new Thickness(0.0, 0.0, 24.0, 0.0);
		Controls children14 = stackPanel9.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children14.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		Button button5 = button4;
		StyledProperty<ICommand?> commandProperty3 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension10 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EBanMemberViewModel_002CRootApp_002EClient_002EAvalonia_002ECloseViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension11 = compiledBindingExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, commandProperty3, compiledBindingExtension11);
		button5.Classes.Add("BorderButton");
		button5.Height = 40.0;
		button5.MinWidth = 101.0;
		button5.Padding = new Thickness(28.0, 0.0, 28.0, 0.0);
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj8 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(button5, obj8);
		button5.FontWeight = FontWeight.Medium;
		button5.Content = RootApp.Client.Avalonia.Resources.Strings.Resources.Cancel;
		StyledProperty<IBrush?> borderBrushProperty2 = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, borderBrushProperty2, binding11);
		StyledProperty<IBrush?> foregroundProperty3 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding12 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, foregroundProperty3, binding12);
		button5.Background = new ImmutableSolidColorBrush(16777215u);
		button5.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		button5.FontSize = 14.0;
		button5.Margin = new Thickness(0.0, 0.0, 16.0, 0.0);
		button5.CornerRadius = new CornerRadius(100.0, 100.0, 100.0, 100.0);
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		Controls children15 = stackPanel9.Children;
		Button button7;
		Button button6 = (button7 = new Button());
		((ISupportInitialize)button6).BeginInit();
		children15.Add(button6);
		Button button8 = (button4 = button7);
		context.PushParent(button4);
		Button button9 = button4;
		StyledProperty<ICommand?> commandProperty4 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension12 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EBanMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EBanMemberCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension13 = compiledBindingExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, commandProperty4, compiledBindingExtension13);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension14 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EBanMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EBanMemberCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension15 = compiledBindingExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, isVisibleProperty, compiledBindingExtension15);
		button9.Classes.Add("BorderButton");
		button9.Height = 40.0;
		button9.MinWidth = 101.0;
		button9.IsDefault = true;
		button9.Padding = new Thickness(28.0, 0.0, 28.0, 0.0);
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj9 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(button9, obj9);
		button9.FontWeight = FontWeight.Medium;
		button9.Content = RootApp.Client.Avalonia.Resources.Strings.Resources.Ban;
		StyledProperty<IBrush?> backgroundProperty3 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("Error");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding13 = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, backgroundProperty3, binding13);
		StyledProperty<IBrush?> foregroundProperty4 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding14 = dynamicResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, foregroundProperty4, binding14);
		button9.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button9.FontSize = 14.0;
		button9.CornerRadius = new CornerRadius(100.0, 100.0, 100.0, 100.0);
		context.PopParent();
		((ISupportInitialize)button8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		Controls children16 = grid5.Children;
		RootWebApiStatus rootWebApiStatus2;
		RootWebApiStatus rootWebApiStatus = (rootWebApiStatus2 = new RootWebApiStatus());
		((ISupportInitialize)rootWebApiStatus).BeginInit();
		children16.Add(rootWebApiStatus);
		RootWebApiStatus rootWebApiStatus4;
		RootWebApiStatus rootWebApiStatus3 = (rootWebApiStatus4 = rootWebApiStatus2);
		context.PushParent(rootWebApiStatus4);
		Grid.SetRow(rootWebApiStatus4, 3);
		StyledProperty<WebApiStatus> webApiStatusProperty = RootWebApiStatus.WebApiStatusProperty;
		CompiledBindingExtension compiledBindingExtension16 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EBanMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EWebApiStatus_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootWebApiStatus.WebApiStatusProperty;
		CompiledBindingExtension compiledBindingExtension17 = compiledBindingExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootWebApiStatus4, webApiStatusProperty, compiledBindingExtension17);
		StyledProperty<ICommand?> closeCommandProperty = RootWebApiStatus.CloseCommandProperty;
		CompiledBindingExtension compiledBindingExtension18 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EBanMemberViewModel_002CRootApp_002EClient_002EAvalonia_002ECloseViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootWebApiStatus.CloseCommandProperty;
		CompiledBindingExtension compiledBindingExtension19 = compiledBindingExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootWebApiStatus4, closeCommandProperty, compiledBindingExtension19);
		rootWebApiStatus4.SendingText = RootApp.Client.Avalonia.Resources.Strings.Resources.BanningMember;
		rootWebApiStatus4.SuccessText = RootApp.Client.Avalonia.Resources.Strings.Resources.SuccessfullyBannedMember;
		rootWebApiStatus4.FailedText = RootApp.Client.Avalonia.Resources.Strings.Resources.FailedToBanMember;
		context.PopParent();
		((ISupportInitialize)rootWebApiStatus3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
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
	private static void _0021XamlIlPopulateTrampoline(BanMemberView P_0)
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

