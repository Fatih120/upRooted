// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.InviteMembersLinkSettingsView
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Xaml.Interactions.Core;
using Avalonia.Xaml.Interactions.Custom;
using Avalonia.Xaml.Interactivity;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Community.Members;

public class InviteMembersLinkSettingsView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_75
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<InviteMembersLinkSettingsView> context = CreateContext(P_0);
			context.IntermediateRoot = new TextBlock();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			TextBlock textBlock = (TextBlock)obj;
			context.PushParent(textBlock);
			StyledProperty<string?> textProperty = TextBlock.TextProperty;
			CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension();
			context.ProvideTargetProperty = TextBlock.TextProperty;
			CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(textBlock, textProperty, compiledBindingExtension2);
			textBlock.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
			StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
			DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
			context.ProvideTargetProperty = TextBlock.ForegroundProperty;
			IBinding binding = dynamicResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(textBlock, foregroundProperty, binding);
			textBlock.FontSize = 14.0;
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
			context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
			object? obj2 = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock, obj2);
			textBlock.FontWeight = (FontWeight)450;
			context.PopParent();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<InviteMembersLinkSettingsView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<InviteMembersLinkSettingsView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<InviteMembersLinkSettingsView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FCommunity_002FMembers_002FInviteMembersLinkSettingsView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Members/InviteMembersLinkSettingsView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (InviteMembersLinkSettingsView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<InviteMembersLinkSettingsView> context = CreateContext(P_0);
			context.IntermediateRoot = new TextBlock();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			TextBlock textBlock = (TextBlock)obj;
			context.PushParent(textBlock);
			StyledProperty<string?> textProperty = TextBlock.TextProperty;
			CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension();
			context.ProvideTargetProperty = TextBlock.TextProperty;
			CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(textBlock, textProperty, compiledBindingExtension2);
			textBlock.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
			StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
			DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
			context.ProvideTargetProperty = TextBlock.ForegroundProperty;
			IBinding binding = dynamicResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(textBlock, foregroundProperty, binding);
			textBlock.FontSize = 14.0;
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
			context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
			object? obj2 = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock, obj2);
			textBlock.FontWeight = (FontWeight)450;
			context.PopParent();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border BackgroundBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootBorder MainBorder;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public InviteMembersLinkSettingsView()
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
		INameScope nameScope = this.FindNameScope();
		BackgroundBorder = nameScope?.Find<Border>("BackgroundBorder");
		MainBorder = nameScope?.Find<RootBorder>("MainBorder");
	}

	[CompilerGenerated]
	private unsafe static void _0021XamlIlPopulate(IServiceProvider P_0, InviteMembersLinkSettingsView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<InviteMembersLinkSettingsView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<InviteMembersLinkSettingsView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FCommunity_002FMembers_002FInviteMembersLinkSettingsView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Members/InviteMembersLinkSettingsView.axaml")
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
		Controls children = panel4.Children;
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
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EInviteMembersLinkSettingsViewModel_002CRootApp_002EClient_002EAvalonia_002ECloseViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
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
		Controls children2 = panel4.Children;
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
		rootBorder4.Width = 500.0;
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
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(59.0, GridUnitType.Pixel)
		});
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(64.0, GridUnitType.Pixel)
		});
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		Controls children3 = grid4.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children3.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Margin = new Thickness(24.0, 0.0, 0.0, 0.0);
		textBlock5.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.InviteLinkSettings;
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj2);
		textBlock5.FontSize = 20.0;
		textBlock5.FontWeight = (FontWeight)450;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding4);
		textBlock5.HorizontalAlignment = HorizontalAlignment.Left;
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		Controls children4 = grid4.Children;
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
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EInviteMembersLinkSettingsViewModel_002CRootApp_002EClient_002EAvalonia_002ECloseViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
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
		Controls children5 = grid4.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children5.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		Grid.SetRow(stackPanel5, 1);
		stackPanel5.Margin = new Thickness(24.0, 8.0, 24.0, 0.0);
		Controls children6 = stackPanel5.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children6.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Text = "Expire after";
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj3 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj3);
		textBlock9.FontWeight = (FontWeight)450;
		textBlock9.FontSize = 13.0;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding6);
		textBlock9.Margin = new Thickness(0.0, 0.0, 0.0, 8.0);
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		Controls children7 = stackPanel5.Children;
		ComboBox comboBox2;
		ComboBox comboBox = (comboBox2 = new ComboBox());
		((ISupportInitialize)comboBox).BeginInit();
		children7.Add(comboBox);
		ComboBox comboBox4;
		ComboBox comboBox3 = (comboBox4 = comboBox2);
		context.PushParent(comboBox4);
		ComboBox comboBox5 = comboBox4;
		comboBox5.Classes.Add("RootComboBox");
		comboBox5.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		comboBox5.Height = 52.0;
		comboBox5.FontSize = 16.0;
		StyledProperty<IBrush?> backgroundProperty3 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("Input");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(comboBox5, backgroundProperty3, binding7);
		StyledProperty<IBrush?> borderBrushProperty2 = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(comboBox5, borderBrushProperty2, binding8);
		comboBox5.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		comboBox5.HorizontalContentAlignment = HorizontalAlignment.Left;
		DirectProperty<SelectingItemsControl, int> selectedIndexProperty = SelectingItemsControl.SelectedIndexProperty;
		CompiledBindingExtension obj4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EInviteMembersLinkSettingsViewModel_002CRootApp_002EClient_002EAvalonia_002EExpirationSelectionIndex_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = SelectingItemsControl.SelectedIndexProperty;
		CompiledBindingExtension compiledBindingExtension5 = obj4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(comboBox5, selectedIndexProperty, compiledBindingExtension5);
		DataTemplate dataTemplate;
		DataTemplate itemTemplate = (dataTemplate = new DataTemplate());
		context.PushParent(dataTemplate);
		dataTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_75.Build_1), context);
		context.PopParent();
		comboBox5.ItemTemplate = itemTemplate;
		comboBox5.Items.Add("1 hour");
		comboBox5.Items.Add("6 hours");
		comboBox5.Items.Add("12 hours");
		comboBox5.Items.Add("1 day");
		comboBox5.Items.Add("7 days");
		comboBox5.Items.Add("Never");
		context.PopParent();
		((ISupportInitialize)comboBox3).EndInit();
		Controls children8 = stackPanel5.Children;
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		children8.Add(textBlock10);
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		textBlock13.Text = "Max number of uses";
		textBlock13.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj5 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock13, obj5);
		textBlock13.FontWeight = (FontWeight)450;
		textBlock13.FontSize = 13.0;
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, foregroundProperty3, binding9);
		textBlock13.Margin = new Thickness(0.0, 16.0, 0.0, 8.0);
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		Controls children9 = stackPanel5.Children;
		ComboBox comboBox7;
		ComboBox comboBox6 = (comboBox7 = new ComboBox());
		((ISupportInitialize)comboBox6).BeginInit();
		children9.Add(comboBox6);
		ComboBox comboBox8 = (comboBox4 = comboBox7);
		context.PushParent(comboBox4);
		ComboBox comboBox9 = comboBox4;
		comboBox9.Classes.Add("RootComboBox");
		comboBox9.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		comboBox9.Height = 52.0;
		comboBox9.FontSize = 16.0;
		StyledProperty<IBrush?> backgroundProperty4 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("Input");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(comboBox9, backgroundProperty4, binding10);
		StyledProperty<IBrush?> borderBrushProperty3 = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(comboBox9, borderBrushProperty3, binding11);
		comboBox9.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		comboBox9.HorizontalContentAlignment = HorizontalAlignment.Left;
		DirectProperty<SelectingItemsControl, int> selectedIndexProperty2 = SelectingItemsControl.SelectedIndexProperty;
		CompiledBindingExtension obj6 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EInviteMembersLinkSettingsViewModel_002CRootApp_002EClient_002EAvalonia_002EMaxUsesSelectionIndex_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = SelectingItemsControl.SelectedIndexProperty;
		CompiledBindingExtension compiledBindingExtension6 = obj6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(comboBox9, selectedIndexProperty2, compiledBindingExtension6);
		DataTemplate itemTemplate2 = (dataTemplate = new DataTemplate());
		context.PushParent(dataTemplate);
		dataTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_75.Build_2), context);
		context.PopParent();
		comboBox9.ItemTemplate = itemTemplate2;
		comboBox9.Items.Add("No limit");
		comboBox9.Items.Add("1 use");
		comboBox9.Items.Add("5 uses");
		comboBox9.Items.Add("10 uses");
		comboBox9.Items.Add("25 uses");
		comboBox9.Items.Add("50 uses");
		comboBox9.Items.Add("100 uses");
		context.PopParent();
		((ISupportInitialize)comboBox8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		Controls children10 = grid4.Children;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		children10.Add(stackPanel6);
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		Grid.SetRow(stackPanel9, 2);
		stackPanel9.VerticalAlignment = VerticalAlignment.Center;
		stackPanel9.HorizontalAlignment = HorizontalAlignment.Right;
		stackPanel9.Orientation = Orientation.Horizontal;
		stackPanel9.Margin = new Thickness(0.0, 0.0, 24.0, 0.0);
		Controls children11 = stackPanel9.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children11.Add(button);
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
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(button5, obj7);
		button5.FontWeight = FontWeight.Medium;
		button5.Content = RootApp.Client.Avalonia.Resources.Strings.Resources.Cancel;
		StyledProperty<IBrush?> borderBrushProperty4 = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding12 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, borderBrushProperty4, binding12);
		StyledProperty<IBrush?> foregroundProperty4 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding13 = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, foregroundProperty4, binding13);
		button5.Background = new ImmutableSolidColorBrush(16777215u);
		button5.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		button5.FontSize = 14.0;
		button5.Margin = new Thickness(0.0, 0.0, 16.0, 0.0);
		button5.CornerRadius = new CornerRadius(20.0, 20.0, 20.0, 20.0);
		StyledProperty<ICommand?> commandProperty3 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EInviteMembersLinkSettingsViewModel_002CRootApp_002EClient_002EAvalonia_002ECloseViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, commandProperty3, compiledBindingExtension8);
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		Controls children12 = stackPanel9.Children;
		Button button7;
		Button button6 = (button7 = new Button());
		((ISupportInitialize)button6).BeginInit();
		children12.Add(button6);
		Button button8 = (button4 = button7);
		context.PushParent(button4);
		Button button9 = button4;
		button9.Classes.Add("BorderButton");
		button9.Height = 40.0;
		button9.Width = 170.0;
		button9.IsDefault = true;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj8 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(button9, obj8);
		button9.FontWeight = FontWeight.Medium;
		button9.Content = RootApp.Client.Avalonia.Resources.Strings.Resources.GenerateInviteLink;
		StyledProperty<IBrush?> foregroundProperty5 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding14 = dynamicResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, foregroundProperty5, binding14);
		StyledProperty<IBrush?> backgroundProperty5 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension15 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding15 = dynamicResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, backgroundProperty5, binding15);
		button9.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button9.FontSize = 14.0;
		button9.CornerRadius = new CornerRadius(20.0, 20.0, 20.0, 20.0);
		StyledProperty<ICommand?> commandProperty4 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EInviteMembersLinkSettingsViewModel_002CRootApp_002EClient_002EAvalonia_002EGenerateInviteLinkCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, commandProperty4, compiledBindingExtension10);
		context.PopParent();
		((ISupportInitialize)button8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		Controls children13 = grid4.Children;
		RootWebApiStatus rootWebApiStatus2;
		RootWebApiStatus rootWebApiStatus = (rootWebApiStatus2 = new RootWebApiStatus());
		((ISupportInitialize)rootWebApiStatus).BeginInit();
		children13.Add(rootWebApiStatus);
		RootWebApiStatus rootWebApiStatus4;
		RootWebApiStatus rootWebApiStatus3 = (rootWebApiStatus4 = rootWebApiStatus2);
		context.PushParent(rootWebApiStatus4);
		Grid.SetRow(rootWebApiStatus4, 3);
		StyledProperty<WebApiStatus> webApiStatusProperty = RootWebApiStatus.WebApiStatusProperty;
		CompiledBindingExtension compiledBindingExtension11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EInviteMembersLinkSettingsViewModel_002CRootApp_002EClient_002EAvalonia_002EWebApiStatus_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootWebApiStatus.WebApiStatusProperty;
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootWebApiStatus4, webApiStatusProperty, compiledBindingExtension12);
		StyledProperty<ICommand?> closeCommandProperty = RootWebApiStatus.CloseCommandProperty;
		CompiledBindingExtension compiledBindingExtension13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EInviteMembersLinkSettingsViewModel_002CRootApp_002EClient_002EAvalonia_002ECloseViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootWebApiStatus.CloseCommandProperty;
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootWebApiStatus4, closeCommandProperty, compiledBindingExtension14);
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
	private static void _0021XamlIlPopulateTrampoline(InviteMembersLinkSettingsView P_0)
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

