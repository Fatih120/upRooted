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
using Avalonia.Markup.Xaml;
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
using RootApp.Client.Avalonia.Controls.ImageUpload;
using RootApp.Client.Avalonia.Resources.Converters.Channels;
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.Avalonia.Resources.Strings;

namespace RootApp.Client.Avalonia.UI.Community.Channels;

public class EditChannelView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_68
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<EditChannelView> context = CreateContext(P_0);
			return new ChannelTypeToSvgConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<EditChannelView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<EditChannelView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<EditChannelView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Community/Channels/EditChannelView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Channels/EditChannelView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (EditChannelView)service;
				}
			}
			return context;
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border BackgroundBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootBorder MainBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootTextbox ChannelNameTextbox;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	public EditChannelView()
	{
		InitializeComponent();
	}

	protected override void OnLoaded(RoutedEventArgs P_0)
	{
		base.OnLoaded(P_0);
		ChannelNameTextbox.FocusTextBox();
		ChannelNameTextbox.MainTextBox.TextChanged += onMainTextBoxTextChanged;
	}

	protected override void OnUnloaded(RoutedEventArgs P_0)
	{
		base.OnUnloaded(P_0);
		ChannelNameTextbox.MainTextBox.TextChanged -= onMainTextBoxTextChanged;
	}

	private void onMainTextBoxTextChanged(object? sender, TextChangedEventArgs e)
	{
		if (ChannelNameTextbox.MainTextBox.Text.Contains(' '))
		{
			ChannelNameTextbox.MainTextBox.Text = ChannelNameTextbox.MainTextBox.Text.Replace(" ", "-");
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	[ExcludeFromCodeCoverage]
	public void InitializeComponent(bool P_0 = true)
	{
		if (P_0)
		{
			!XamlIlPopulateTrampoline(this);
		}
		INameScope nameScope = this.FindNameScope();
		BackgroundBorder = nameScope?.Find<Border>("BackgroundBorder");
		MainBorder = nameScope?.Find<RootBorder>("MainBorder");
		ChannelNameTextbox = nameScope?.Find<RootTextbox>("ChannelNameTextbox");
	}

	[CompilerGenerated]
	private unsafe static void !XamlIlPopulate(IServiceProvider P_0, EditChannelView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<EditChannelView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<EditChannelView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Community/Channels/EditChannelView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Channels/EditChannelView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		ResourceDictionary resourceDictionary = new ResourceDictionary();
		resourceDictionary.AddDeferred("ChannelTypeToSvgConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_68.Build_1), context));
		P_1.Resources = resourceDictionary;
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		P_1.Content = panel;
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		Panel panel5 = panel4;
		global::Avalonia.Controls.Controls children = panel5.Children;
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
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.EditChannelViewModel,RootApp.Client.Avalonia.CloseViewModelCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
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
		global::Avalonia.Controls.Controls children2 = panel5.Children;
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
		rootBorder4.Width = 700.0;
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
		global::Avalonia.Controls.Controls children3 = grid5.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children3.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.EditChannel;
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock5, obj2);
		textBlock5.FontSize = 20.0;
		textBlock5.FontWeight = (FontWeight)450;
		textBlock5.Margin = new Thickness(24.0, 0.0, 0.0, 0.0);
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding4);
		textBlock5.HorizontalAlignment = HorizontalAlignment.Left;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		global::Avalonia.Controls.Controls children4 = grid5.Children;
		RootSvgButton rootSvgButton2;
		RootSvgButton rootSvgButton = (rootSvgButton2 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton).BeginInit();
		children4.Add(rootSvgButton);
		RootSvgButton rootSvgButton4;
		RootSvgButton rootSvgButton3 = (rootSvgButton4 = rootSvgButton2);
		context.PushParent(rootSvgButton4);
		rootSvgButton4.Classes.Add("SvgDimmedButton");
		rootSvgButton4.Margin = new Thickness(0.0, 0.0, 24.0, 0.0);
		StyledProperty<ICommand?> commandProperty2 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.EditChannelViewModel,RootApp.Client.Avalonia.CloseViewModelCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
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
		global::Avalonia.Controls.Controls children5 = grid5.Children;
		TabControl tabControl2;
		TabControl tabControl = (tabControl2 = new TabControl());
		((ISupportInitialize)tabControl).BeginInit();
		children5.Add(tabControl);
		TabControl tabControl4;
		TabControl tabControl3 = (tabControl4 = tabControl2);
		context.PushParent(tabControl4);
		Grid.SetRow(tabControl4, 1);
		tabControl4.VerticalAlignment = VerticalAlignment.Top;
		tabControl4.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		tabControl4.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		ItemCollection items = tabControl4.Items;
		TabItem tabItem2;
		TabItem tabItem = (tabItem2 = new TabItem());
		((ISupportInitialize)tabItem).BeginInit();
		items.Add(tabItem);
		TabItem tabItem4;
		TabItem tabItem3 = (tabItem4 = tabItem2);
		context.PushParent(tabItem4);
		TabItem tabItem5 = tabItem4;
		tabItem5.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.General;
		tabItem5.FontSize = 14.0;
		tabItem5.Height = 48.0;
		tabItem5.Margin = new Thickness(24.0, 0.0, 24.0, 0.0);
		Panel panel7;
		Panel panel6 = (panel7 = new Panel());
		((ISupportInitialize)panel6).BeginInit();
		tabItem5.Content = panel6;
		Panel panel8 = (panel4 = panel7);
		context.PushParent(panel4);
		Panel panel9 = panel4;
		global::Avalonia.Controls.Controls children6 = panel9.Children;
		RootScrollViewer rootScrollViewer2;
		RootScrollViewer rootScrollViewer = (rootScrollViewer2 = new RootScrollViewer());
		((ISupportInitialize)rootScrollViewer).BeginInit();
		children6.Add(rootScrollViewer);
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
		global::Avalonia.Controls.Controls children7 = stackPanel5.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children7.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ChannelName;
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj3 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock9, obj3);
		textBlock9.FontWeight = (FontWeight)450;
		textBlock9.FontSize = 13.0;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding6);
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		global::Avalonia.Controls.Controls children8 = stackPanel5.Children;
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		children8.Add(grid6);
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		grid9.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(52.0, GridUnitType.Pixel)
		});
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		global::Avalonia.Controls.Controls children9 = grid9.Children;
		RootTextbox rootTextbox2;
		RootTextbox rootTextbox = (rootTextbox2 = new RootTextbox());
		((ISupportInitialize)rootTextbox).BeginInit();
		children9.Add(rootTextbox);
		RootTextbox rootTextbox4;
		RootTextbox rootTextbox3 = (rootTextbox4 = rootTextbox2);
		context.PushParent(rootTextbox4);
		RootTextbox rootTextbox5 = rootTextbox4;
		rootTextbox5.Name = "ChannelNameTextbox";
		obj = rootTextbox5;
		context.AvaloniaNameScope.Register("ChannelNameTextbox", obj);
		StyledProperty<string> textProperty = RootTextbox.TextProperty;
		CompiledBindingExtension obj4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.EditChannelViewModel,RootApp.Client.Avalonia.ChannelName!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = RootTextbox.TextProperty;
		CompiledBindingExtension compiledBindingExtension5 = obj4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox5, textProperty, compiledBindingExtension5);
		rootTextbox5.ValidationPropertyName = "ChannelName";
		Grid.SetColumn(rootTextbox5, 0);
		Grid.SetColumnSpan(rootTextbox5, 2);
		rootTextbox5.BorderHeight = 52.0;
		rootTextbox5.PlaceholderText = RootApp.Client.Avalonia.Resources.Strings.Resources.ChannelName;
		rootTextbox5.BorderCornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		StyledProperty<IBrush> borderBackgroundBrushProperty = RootTextbox.BorderBackgroundBrushProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("Input");
		context.ProvideTargetProperty = RootTextbox.BorderBackgroundBrushProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox5, borderBackgroundBrushProperty, binding7);
		StyledProperty<IBrush> borderBorderBrushProperty = RootTextbox.BorderBorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = RootTextbox.BorderBorderBrushProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox5, borderBorderBrushProperty, binding8);
		rootTextbox5.BorderBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootTextbox5.TextboxFontSize = 15.0;
		rootTextbox5.TextboxMargin = new Thickness(52.0, 0.0, 20.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rootTextbox3).EndInit();
		global::Avalonia.Controls.Controls children10 = grid9.Children;
		ImageUploaderView imageUploaderView2;
		ImageUploaderView imageUploaderView = (imageUploaderView2 = new ImageUploaderView());
		((ISupportInitialize)imageUploaderView).BeginInit();
		children10.Add(imageUploaderView);
		ImageUploaderView imageUploaderView4;
		ImageUploaderView imageUploaderView3 = (imageUploaderView4 = imageUploaderView2);
		context.PushParent(imageUploaderView4);
		Grid.SetColumn(imageUploaderView4, 0);
		imageUploaderView4.VerticalAlignment = VerticalAlignment.Top;
		imageUploaderView4.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		imageUploaderView4.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		imageUploaderView4.PlaceholderIconSize = new Size(10.0, 10.0);
		imageUploaderView4.Width = 30.0;
		imageUploaderView4.Height = 30.0;
		imageUploaderView4.ActionButtonSize = new Size(20.0, 20.0);
		imageUploaderView4.ActionButtonCornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		imageUploaderView4.ActionButtonMargin = new Thickness(0.0, 0.0, -8.0, -8.0);
		imageUploaderView4.ProgressBarSize = new Size(18.0, 18.0);
		CompiledBindingExtension compiledBindingExtension6 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.EditChannelViewModel,RootApp.Client.Avalonia.ImageUploaderViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = StyledElement.DataContextProperty;
		CompiledBindingExtension compiledBindingExtension7 = compiledBindingExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_6(imageUploaderView4, compiledBindingExtension7);
		context.PopParent();
		((ISupportInitialize)imageUploaderView3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		global::Avalonia.Controls.Controls children11 = stackPanel5.Children;
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		children11.Add(textBlock10);
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		textBlock13.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ChannelDescription;
		textBlock13.Margin = new Thickness(0.0, 28.0, 0.0, 0.0);
		textBlock13.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj5 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock13, obj5);
		textBlock13.FontWeight = (FontWeight)450;
		textBlock13.FontSize = 13.0;
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, foregroundProperty3, binding9);
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		global::Avalonia.Controls.Controls children12 = stackPanel5.Children;
		RootTextbox rootTextbox7;
		RootTextbox rootTextbox6 = (rootTextbox7 = new RootTextbox());
		((ISupportInitialize)rootTextbox6).BeginInit();
		children12.Add(rootTextbox6);
		RootTextbox rootTextbox8 = (rootTextbox4 = rootTextbox7);
		context.PushParent(rootTextbox4);
		RootTextbox rootTextbox9 = rootTextbox4;
		StyledProperty<string> textProperty2 = RootTextbox.TextProperty;
		CompiledBindingExtension obj6 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.EditChannelViewModel,RootApp.Client.Avalonia.ChannelDescription!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = RootTextbox.TextProperty;
		CompiledBindingExtension compiledBindingExtension8 = obj6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox9, textProperty2, compiledBindingExtension8);
		rootTextbox9.ValidationPropertyName = "ChannelDescription";
		rootTextbox9.BorderHeight = 52.0;
		rootTextbox9.PlaceholderText = RootApp.Client.Avalonia.Resources.Strings.Resources.ChannelDescription;
		rootTextbox9.BorderCornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		StyledProperty<IBrush> borderBackgroundBrushProperty2 = RootTextbox.BorderBackgroundBrushProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("Input");
		context.ProvideTargetProperty = RootTextbox.BorderBackgroundBrushProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox9, borderBackgroundBrushProperty2, binding10);
		StyledProperty<IBrush> borderBorderBrushProperty2 = RootTextbox.BorderBorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = RootTextbox.BorderBorderBrushProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox9, borderBorderBrushProperty2, binding11);
		rootTextbox9.BorderBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootTextbox9.TextboxFontSize = 15.0;
		rootTextbox9.TextboxMargin = new Thickness(20.0, 0.0, 20.0, 0.0);
		rootTextbox9.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rootTextbox8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootScrollViewer3).EndInit();
		global::Avalonia.Controls.Controls children13 = panel9.Children;
		Rectangle rectangle2;
		Rectangle rectangle = (rectangle2 = new Rectangle());
		((ISupportInitialize)rectangle).BeginInit();
		children13.Add(rectangle);
		Rectangle rectangle4;
		Rectangle rectangle3 = (rectangle4 = rectangle2);
		context.PushParent(rectangle4);
		Rectangle rectangle5 = rectangle4;
		rectangle5.Height = 0.5;
		StyledProperty<IBrush?> fillProperty = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding12 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle5, fillProperty, binding12);
		rectangle5.VerticalAlignment = VerticalAlignment.Top;
		rectangle5.Margin = new Thickness(24.0, 0.0, 24.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rectangle3).EndInit();
		context.PopParent();
		((ISupportInitialize)panel8).EndInit();
		context.PopParent();
		((ISupportInitialize)tabItem3).EndInit();
		ItemCollection items2 = tabControl4.Items;
		TabItem tabItem7;
		TabItem tabItem6 = (tabItem7 = new TabItem());
		((ISupportInitialize)tabItem6).BeginInit();
		items2.Add(tabItem6);
		TabItem tabItem8 = (tabItem4 = tabItem7);
		context.PushParent(tabItem4);
		TabItem tabItem9 = tabItem4;
		tabItem9.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.Permissions;
		tabItem9.FontSize = 14.0;
		tabItem9.Height = 48.0;
		Panel panel11;
		Panel panel10 = (panel11 = new Panel());
		((ISupportInitialize)panel10).BeginInit();
		tabItem9.Content = panel10;
		Panel panel12 = (panel4 = panel11);
		context.PushParent(panel4);
		Panel panel13 = panel4;
		global::Avalonia.Controls.Controls children14 = panel13.Children;
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		children14.Add(contentControl);
		ContentControl contentControl4;
		ContentControl contentControl3 = (contentControl4 = contentControl2);
		context.PushParent(contentControl4);
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.EditChannelViewModel,RootApp.Client.Avalonia.AccessRuleSelector!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl4, compiledBindingExtension10);
		context.PopParent();
		((ISupportInitialize)contentControl3).EndInit();
		global::Avalonia.Controls.Controls children15 = panel13.Children;
		Rectangle rectangle7;
		Rectangle rectangle6 = (rectangle7 = new Rectangle());
		((ISupportInitialize)rectangle6).BeginInit();
		children15.Add(rectangle6);
		Rectangle rectangle8 = (rectangle4 = rectangle7);
		context.PushParent(rectangle4);
		Rectangle rectangle9 = rectangle4;
		rectangle9.Height = 0.5;
		StyledProperty<IBrush?> fillProperty2 = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding13 = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle9, fillProperty2, binding13);
		rectangle9.VerticalAlignment = VerticalAlignment.Top;
		rectangle9.Margin = new Thickness(24.0, 0.0, 24.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rectangle8).EndInit();
		context.PopParent();
		((ISupportInitialize)panel12).EndInit();
		context.PopParent();
		((ISupportInitialize)tabItem8).EndInit();
		context.PopParent();
		((ISupportInitialize)tabControl3).EndInit();
		global::Avalonia.Controls.Controls children16 = grid5.Children;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		children16.Add(stackPanel6);
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		Grid.SetRow(stackPanel9, 2);
		stackPanel9.VerticalAlignment = VerticalAlignment.Center;
		stackPanel9.HorizontalAlignment = HorizontalAlignment.Right;
		stackPanel9.Orientation = Orientation.Horizontal;
		stackPanel9.Margin = new Thickness(0.0, 0.0, 24.0, 0.0);
		global::Avalonia.Controls.Controls children17 = stackPanel9.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children17.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		Button button5 = button4;
		button5.Classes.Add("BorderButton");
		button5.Height = 40.0;
		button5.Width = 101.0;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj7 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_3(button5, obj7);
		button5.FontWeight = FontWeight.Medium;
		button5.Content = RootApp.Client.Avalonia.Resources.Strings.Resources.Cancel;
		StyledProperty<IBrush?> borderBrushProperty2 = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding14 = dynamicResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, borderBrushProperty2, binding14);
		StyledProperty<IBrush?> foregroundProperty4 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension15 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding15 = dynamicResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, foregroundProperty4, binding15);
		button5.Background = new ImmutableSolidColorBrush(16777215u);
		button5.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		button5.FontSize = 14.0;
		button5.Margin = new Thickness(0.0, 0.0, 16.0, 0.0);
		button5.CornerRadius = new CornerRadius(20.0, 20.0, 20.0, 20.0);
		StyledProperty<ICommand?> commandProperty3 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.EditChannelViewModel,RootApp.Client.Avalonia.CloseViewModelCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, commandProperty3, compiledBindingExtension12);
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		global::Avalonia.Controls.Controls children18 = stackPanel9.Children;
		Button button7;
		Button button6 = (button7 = new Button());
		((ISupportInitialize)button6).BeginInit();
		children18.Add(button6);
		Button button8 = (button4 = button7);
		context.PushParent(button4);
		Button button9 = button4;
		button9.Classes.Add("BorderButton");
		button9.Height = 40.0;
		button9.Width = 101.0;
		button9.IsDefault = true;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj8 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_3(button9, obj8);
		button9.FontWeight = FontWeight.Medium;
		button9.Content = RootApp.Client.Avalonia.Resources.Strings.Resources.Save;
		StyledProperty<IBrush?> foregroundProperty5 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension16 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding16 = dynamicResourceExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, foregroundProperty5, binding16);
		StyledProperty<IBrush?> backgroundProperty3 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension17 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding17 = dynamicResourceExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, backgroundProperty3, binding17);
		button9.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button9.FontSize = 14.0;
		button9.CornerRadius = new CornerRadius(20.0, 20.0, 20.0, 20.0);
		StyledProperty<ICommand?> commandProperty4 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.EditChannelViewModel,RootApp.Client.Avalonia.SaveChannelCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, commandProperty4, compiledBindingExtension14);
		context.PopParent();
		((ISupportInitialize)button8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		global::Avalonia.Controls.Controls children19 = grid5.Children;
		RootWebApiStatus rootWebApiStatus2;
		RootWebApiStatus rootWebApiStatus = (rootWebApiStatus2 = new RootWebApiStatus());
		((ISupportInitialize)rootWebApiStatus).BeginInit();
		children19.Add(rootWebApiStatus);
		RootWebApiStatus rootWebApiStatus4;
		RootWebApiStatus rootWebApiStatus3 = (rootWebApiStatus4 = rootWebApiStatus2);
		context.PushParent(rootWebApiStatus4);
		Grid.SetRow(rootWebApiStatus4, 3);
		StyledProperty<WebApiStatus> webApiStatusProperty = RootWebApiStatus.WebApiStatusProperty;
		CompiledBindingExtension compiledBindingExtension15 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.EditChannelViewModel,RootApp.Client.Avalonia.WebApiStatus!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootWebApiStatus.WebApiStatusProperty;
		CompiledBindingExtension compiledBindingExtension16 = compiledBindingExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootWebApiStatus4, webApiStatusProperty, compiledBindingExtension16);
		StyledProperty<ICommand?> closeCommandProperty = RootWebApiStatus.CloseCommandProperty;
		CompiledBindingExtension compiledBindingExtension17 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.EditChannelViewModel,RootApp.Client.Avalonia.CloseViewModelCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootWebApiStatus.CloseCommandProperty;
		CompiledBindingExtension compiledBindingExtension18 = compiledBindingExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootWebApiStatus4, closeCommandProperty, compiledBindingExtension18);
		rootWebApiStatus4.SendingText = RootApp.Client.Avalonia.Resources.Strings.Resources.EditingChannel;
		rootWebApiStatus4.SuccessText = RootApp.Client.Avalonia.Resources.Strings.Resources.SuccessfullyEditedChannel;
		rootWebApiStatus4.FailedText = RootApp.Client.Avalonia.Resources.Strings.Resources.FailedToEditChannel;
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
	private static void !XamlIlPopulateTrampoline(EditChannelView P_0)
	{
		if (!XamlIlPopulateOverride != null)
		{
			!XamlIlPopulateOverride(P_0);
		}
		else
		{
			!XamlIlPopulate(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(null), P_0);
		}
	}
}
