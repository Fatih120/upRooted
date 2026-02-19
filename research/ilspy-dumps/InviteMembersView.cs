// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.InviteMembersView
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Xaml.Interactions.Core;
using Avalonia.Xaml.Interactions.Custom;
using Avalonia.Xaml.Interactivity;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Community.Members;

public class InviteMembersView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_76
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<InviteMembersView> context = CreateContext(P_0);
			context.IntermediateRoot = new VirtualizingStackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<InviteMembersView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<InviteMembersView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<InviteMembersView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FCommunity_002FMembers_002FInviteMembersView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Members/InviteMembersView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (InviteMembersView)service;
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
	internal RootTextbox SearchTextBox;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public InviteMembersView()
	{
		InitializeComponent();
	}

	protected override void OnLoaded(RoutedEventArgs P_0)
	{
		base.OnLoaded(P_0);
		SearchTextBox.FocusTextBox();
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
		SearchTextBox = nameScope?.Find<RootTextbox>("SearchTextBox");
	}

	[CompilerGenerated]
	private unsafe static void _0021XamlIlPopulate(IServiceProvider P_0, InviteMembersView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<InviteMembersView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<InviteMembersView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FCommunity_002FMembers_002FInviteMembersView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Members/InviteMembersView.axaml")
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
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EInviteMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECloseViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
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
		RootBorder rootBorder5 = rootBorder4;
		rootBorder5.Name = "MainBorder";
		obj = rootBorder5;
		context.AvaloniaNameScope.Register("MainBorder", obj);
		rootBorder5.Width = 600.0;
		rootBorder5.Margin = new Thickness(0.0, 35.0, 0.0, 35.0);
		rootBorder5.MaxHeight = 800.0;
		rootBorder5.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, backgroundProperty2, binding2);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, borderBrushProperty, binding3);
		rootBorder5.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder5.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		rootBorder5.Child = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(59.0, GridUnitType.Pixel)
		});
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.5, GridUnitType.Pixel)
		});
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
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
		Grid.SetRow(textBlock5, 0);
		textBlock5.Margin = new Thickness(24.0, 0.0, 0.0, 0.0);
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		textBlock5.TextTrimming = TextTrimming.CharacterEllipsis;
		InlineCollection? inlines = textBlock5.Inlines;
		Run run = new Run();
		((ISupportInitialize)run).BeginInit();
		run.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.InviteMembers;
		((ISupportInitialize)run).EndInit();
		inlines.Add(run);
		textBlock5.Inlines.Add(" ");
		InlineCollection? inlines2 = textBlock5.Inlines;
		Run run2 = new Run();
		((ISupportInitialize)run2).BeginInit();
		Run run3 = run2;
		context.PushParent(run3);
		AttachedProperty<IBrush?> foregroundProperty2 = TextElement.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextElement.ForegroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(run3, foregroundProperty2, binding5);
		StyledProperty<string?> textProperty = Run.TextProperty;
		MultiBinding multiBinding2;
		MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("PrependStringConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj3 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding2.Converter = (IMultiValueConverter)obj3;
		IList<IBinding> bindings = multiBinding2.Bindings;
		CompiledBindingExtension obj4 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EInviteMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunityName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item = obj4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings.Add(item);
		IList<IBinding> bindings2 = multiBinding2.Bindings;
		CompiledBindingExtension obj5 = new CompiledBindingExtension
		{
			Source = RootApp.Client.Avalonia.Resources.Strings.Resources.To
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item2 = obj5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings2.Add(item2);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(run3, textProperty, multiBinding);
		context.PopParent();
		((ISupportInitialize)run2).EndInit();
		inlines2.Add(run2);
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
		Grid.SetColumn(rootSvgButton4, 1);
		Grid.SetRow(rootSvgButton4, 0);
		rootSvgButton4.Classes.Add("SvgDimmedButton");
		StyledProperty<ICommand?> commandProperty2 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EInviteMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECloseViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton4, commandProperty2, compiledBindingExtension4);
		rootSvgButton4.HorizontalAlignment = HorizontalAlignment.Right;
		rootSvgButton4.Width = 25.0;
		rootSvgButton4.Height = 25.0;
		rootSvgButton4.Margin = new Thickness(0.0, 0.0, 24.0, 0.0);
		StyledProperty<string> svgPathProperty = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("ExitThickSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton4, svgPathProperty, binding6);
		rootSvgButton4.SvgWidth = 13.0;
		rootSvgButton4.SvgHeight = 13.0;
		context.PopParent();
		((ISupportInitialize)rootSvgButton3).EndInit();
		Controls children5 = grid4.Children;
		RootBorder rootBorder7;
		RootBorder rootBorder6 = (rootBorder7 = new RootBorder());
		((ISupportInitialize)rootBorder6).BeginInit();
		children5.Add(rootBorder6);
		RootBorder rootBorder8 = (rootBorder4 = rootBorder7);
		context.PushParent(rootBorder4);
		RootBorder rootBorder9 = rootBorder4;
		rootBorder9.Margin = new Thickness(24.0, 0.0, 24.0, 16.0);
		Grid.SetRow(rootBorder9, 1);
		Grid.SetColumnSpan(rootBorder9, 2);
		Grid.SetColumn(rootBorder9, 0);
		StyledProperty<IBrush?> backgroundProperty3 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, backgroundProperty3, binding7);
		StyledProperty<IBrush?> borderBrushProperty2 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, borderBrushProperty2, binding8);
		rootBorder9.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder9.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		rootBorder9.Child = stackPanel;
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		stackPanel5.Margin = new Thickness(16.0, 16.0, 16.0, 16.0);
		stackPanel5.Spacing = 12.0;
		Controls children6 = stackPanel5.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children6.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ShareLinkToGiveAccess;
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj6 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj6);
		textBlock9.FontWeight = FontWeight.Bold;
		textBlock9.FontSize = 14.0;
		textBlock9.LineHeight = 20.0;
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty3, binding9);
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		Controls children7 = stackPanel5.Children;
		RootTextbox rootTextbox2;
		RootTextbox rootTextbox = (rootTextbox2 = new RootTextbox());
		((ISupportInitialize)rootTextbox).BeginInit();
		children7.Add(rootTextbox);
		RootTextbox rootTextbox4;
		RootTextbox rootTextbox3 = (rootTextbox4 = rootTextbox2);
		context.PushParent(rootTextbox4);
		RootTextbox rootTextbox5 = rootTextbox4;
		StyledProperty<string> textProperty2 = RootTextbox.TextProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EInviteMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EInviteLink_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootTextbox.TextProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox5, textProperty2, compiledBindingExtension6);
		rootTextbox5.IsReadOnly = true;
		rootTextbox5.BorderHeight = 52.0;
		rootTextbox5.BorderCornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		StyledProperty<IBrush> borderBackgroundBrushProperty = RootTextbox.BorderBackgroundBrushProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("Input");
		context.ProvideTargetProperty = RootTextbox.BorderBackgroundBrushProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox5, borderBackgroundBrushProperty, binding10);
		StyledProperty<IBrush> borderBorderBrushProperty = RootTextbox.BorderBorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = RootTextbox.BorderBorderBrushProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox5, borderBorderBrushProperty, binding11);
		rootTextbox5.BorderBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootTextbox5.TextboxFontSize = 16.0;
		rootTextbox5.TextboxMargin = new Thickness(16.0, 0.0, 100.0, 0.0);
		rootTextbox5.VerticalContentAlignment = VerticalAlignment.Center;
		rootTextbox5.TextVerticalAlignment = VerticalAlignment.Center;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		rootTextbox5.InnerRightContent = button;
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		button4.Classes.Add("BorderButton");
		button4.Height = 40.0;
		button4.Padding = new Thickness(16.0, 8.0, 16.0, 8.0);
		button4.IsDefault = true;
		button4.Width = 100.0;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj7 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(button4, obj7);
		button4.FontWeight = FontWeight.Medium;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EInviteMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECopyButtonText_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_4(button4, compiledBindingExtension8);
		StyledProperty<IBrush?> foregroundProperty4 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding12 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, foregroundProperty4, binding12);
		button4.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button4.FontSize = 14.0;
		button4.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		button4.Margin = new Thickness(0.0, 0.0, 6.0, 0.0);
		StyledProperty<ICommand?> commandProperty3 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EInviteMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECopyLinkCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, commandProperty3, compiledBindingExtension10);
		Styles styles = button4.Styles;
		Style style2;
		Style style = (style2 = new Style());
		context.PushParent(style2);
		Style style3 = style2;
		style3.Selector = ((Selector?)null).OfType(typeof(Button));
		Setter setter2;
		Setter setter = (setter2 = new Setter());
		context.PushParent(setter2);
		Setter setter3 = setter2;
		setter3.Property = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		IBinding value = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter3.Value = value;
		context.PopParent();
		style3.Add(setter);
		context.PopParent();
		styles.Add(style);
		Styles styles2 = button4.Styles;
		Style style4 = (style2 = new Style());
		context.PushParent(style2);
		Style style5 = style2;
		style5.Selector = ((Selector?)null).OfType(typeof(Button)).PropertyEquals(Control.TagProperty, "True");
		Setter setter4 = (setter2 = new Setter());
		context.PushParent(setter2);
		Setter setter5 = setter2;
		setter5.Property = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("BrandSecondary");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		IBinding value2 = dynamicResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter5.Value = value2;
		context.PopParent();
		style5.Add(setter4);
		context.PopParent();
		styles2.Add(style4);
		CompiledBindingExtension obj8 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EInviteMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EIsCopied_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Control.TagProperty;
		CompiledBindingExtension compiledBindingExtension11 = obj8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_23(button4, compiledBindingExtension11);
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootTextbox3).EndInit();
		Controls children8 = stackPanel5.Children;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		children8.Add(stackPanel6);
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		stackPanel9.Orientation = Orientation.Horizontal;
		stackPanel9.Margin = new Thickness(0.0, 4.0, 0.0, 0.0);
		Controls children9 = stackPanel9.Children;
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		children9.Add(textBlock10);
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		StyledProperty<string?> textProperty3 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension12 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EInviteMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EInviteDurationText_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension13 = compiledBindingExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, textProperty3, compiledBindingExtension13);
		textBlock13.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj9 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock13, obj9);
		textBlock13.FontWeight = FontWeight.Normal;
		textBlock13.FontSize = 12.0;
		StyledProperty<IBrush?> foregroundProperty5 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension15 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding13 = dynamicResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, foregroundProperty5, binding13);
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		Controls children10 = stackPanel9.Children;
		RootLinkButton rootLinkButton2;
		RootLinkButton rootLinkButton = (rootLinkButton2 = new RootLinkButton());
		((ISupportInitialize)rootLinkButton).BeginInit();
		children10.Add(rootLinkButton);
		RootLinkButton rootLinkButton4;
		RootLinkButton rootLinkButton3 = (rootLinkButton4 = rootLinkButton2);
		context.PushParent(rootLinkButton4);
		rootLinkButton4.Content = RootApp.Client.Avalonia.Resources.Strings.Resources.EditInviteLink;
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj10 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(rootLinkButton4, obj10);
		StyledProperty<IBrush?> foregroundProperty6 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension16 = new DynamicResourceExtension("Link");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding14 = dynamicResourceExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootLinkButton4, foregroundProperty6, binding14);
		rootLinkButton4.FontWeight = FontWeight.Normal;
		rootLinkButton4.FontSize = 12.0;
		rootLinkButton4.Margin = new Thickness(2.0, 0.0, 0.0, 0.0);
		StyledProperty<ICommand?> commandProperty4 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension14 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EInviteMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EEditLinkSettingsCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension15 = compiledBindingExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootLinkButton4, commandProperty4, compiledBindingExtension15);
		context.PopParent();
		((ISupportInitialize)rootLinkButton3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder8).EndInit();
		Controls children11 = grid4.Children;
		Rectangle rectangle2;
		Rectangle rectangle = (rectangle2 = new Rectangle());
		((ISupportInitialize)rectangle).BeginInit();
		children11.Add(rectangle);
		Rectangle rectangle4;
		Rectangle rectangle3 = (rectangle4 = rectangle2);
		context.PushParent(rectangle4);
		rectangle4.Height = 0.5;
		StyledProperty<IBrush?> fillProperty = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension17 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding15 = dynamicResourceExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle4, fillProperty, binding15);
		rectangle4.Margin = new Thickness(24.0, 0.0, 24.0, 0.0);
		Grid.SetRow(rectangle4, 2);
		Grid.SetColumnSpan(rectangle4, 2);
		Grid.SetColumn(rectangle4, 0);
		context.PopParent();
		((ISupportInitialize)rectangle3).EndInit();
		Controls children12 = grid4.Children;
		RootTextbox rootTextbox7;
		RootTextbox rootTextbox6 = (rootTextbox7 = new RootTextbox());
		((ISupportInitialize)rootTextbox6).BeginInit();
		children12.Add(rootTextbox6);
		RootTextbox rootTextbox8 = (rootTextbox4 = rootTextbox7);
		context.PushParent(rootTextbox4);
		RootTextbox rootTextbox9 = rootTextbox4;
		rootTextbox9.Name = "SearchTextBox";
		obj = rootTextbox9;
		context.AvaloniaNameScope.Register("SearchTextBox", obj);
		StyledProperty<string> textProperty4 = RootTextbox.TextProperty;
		CompiledBindingExtension obj11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EInviteMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ESearchTerm_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = RootTextbox.TextProperty;
		CompiledBindingExtension compiledBindingExtension16 = obj11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox9, textProperty4, compiledBindingExtension16);
		Grid.SetColumnSpan(rootTextbox9, 2);
		Grid.SetColumn(rootTextbox9, 0);
		Grid.SetRow(rootTextbox9, 3);
		rootTextbox9.PlaceholderText = RootApp.Client.Avalonia.Resources.Strings.Resources.SearchFriends;
		rootTextbox9.Margin = new Thickness(24.0, 16.0, 24.0, 16.0);
		rootTextbox9.VerticalAlignment = VerticalAlignment.Top;
		rootTextbox9.BorderHeight = 34.0;
		rootTextbox9.BorderCornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		StyledProperty<IBrush> borderBackgroundBrushProperty2 = RootTextbox.BorderBackgroundBrushProperty;
		DynamicResourceExtension dynamicResourceExtension18 = new DynamicResourceExtension("Input");
		context.ProvideTargetProperty = RootTextbox.BorderBackgroundBrushProperty;
		IBinding binding16 = dynamicResourceExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox9, borderBackgroundBrushProperty2, binding16);
		StyledProperty<IBrush> borderBorderBrushProperty2 = RootTextbox.BorderBorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension19 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = RootTextbox.BorderBorderBrushProperty;
		IBinding binding17 = dynamicResourceExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox9, borderBorderBrushProperty2, binding17);
		rootTextbox9.BorderBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootTextbox9.TextboxFontSize = 14.0;
		rootTextbox9.TextboxMargin = new Thickness(16.0, 0.0, 36.0, 0.0);
		StyledProperty<string> svgPathProperty2 = RootTextbox.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension20 = new DynamicResourceExtension("SearchSVG");
		context.ProvideTargetProperty = RootTextbox.SvgPathProperty;
		IBinding binding18 = dynamicResourceExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox9, svgPathProperty2, binding18);
		rootTextbox9.SvgWidth = 14.21;
		rootTextbox9.SvgHeight = 14.22;
		rootTextbox9.SvgMargin = new Thickness(0.0, 0.0, 16.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rootTextbox8).EndInit();
		Controls children13 = grid4.Children;
		RootScrollViewer rootScrollViewer2;
		RootScrollViewer rootScrollViewer = (rootScrollViewer2 = new RootScrollViewer());
		((ISupportInitialize)rootScrollViewer).BeginInit();
		children13.Add(rootScrollViewer);
		RootScrollViewer rootScrollViewer4;
		RootScrollViewer rootScrollViewer3 = (rootScrollViewer4 = rootScrollViewer2);
		context.PushParent(rootScrollViewer4);
		Grid.SetRow(rootScrollViewer4, 4);
		Grid.SetColumnSpan(rootScrollViewer4, 2);
		Grid.SetColumn(rootScrollViewer4, 0);
		RootScrollViewer.SetEnableDropShadowOnScroll(rootScrollViewer4, true);
		Panel panel7;
		Panel panel6 = (panel7 = new Panel());
		((ISupportInitialize)panel6).BeginInit();
		rootScrollViewer4.Content = panel6;
		Panel panel8 = (panel4 = panel7);
		context.PushParent(panel4);
		Panel panel9 = panel4;
		panel9.Margin = new Thickness(24.0, 0.0, 24.0, 0.0);
		Controls children14 = panel9.Children;
		ItemsControl itemsControl2;
		ItemsControl itemsControl = (itemsControl2 = new ItemsControl());
		((ISupportInitialize)itemsControl).BeginInit();
		children14.Add(itemsControl);
		ItemsControl itemsControl4;
		ItemsControl itemsControl3 = (itemsControl4 = itemsControl2);
		context.PushParent(itemsControl4);
		StyledProperty<IEnumerable?> itemsSourceProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension17 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EInviteMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EFriends_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension18 = compiledBindingExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl4, itemsSourceProperty, compiledBindingExtension18);
		itemsControl4.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_76.Build_1), context)
		};
		context.PopParent();
		((ISupportInitialize)itemsControl3).EndInit();
		Controls children15 = panel9.Children;
		StackPanel stackPanel11;
		StackPanel stackPanel10 = (stackPanel11 = new StackPanel());
		((ISupportInitialize)stackPanel10).BeginInit();
		children15.Add(stackPanel10);
		StackPanel stackPanel12 = (stackPanel4 = stackPanel11);
		context.PushParent(stackPanel4);
		StackPanel stackPanel13 = stackPanel4;
		stackPanel13.Margin = new Thickness(0.0, 40.0, 0.0, 60.0);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension19 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EInviteMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EIsEmptyStateVisible_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension20 = compiledBindingExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(stackPanel13, isVisibleProperty, compiledBindingExtension20);
		stackPanel13.MaxWidth = 340.0;
		Controls children16 = stackPanel13.Children;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		children16.Add(rootSvgImage);
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		rootSvgImage4.Width = 78.0;
		rootSvgImage4.Height = 78.0;
		StyledProperty<string?> svgPathProperty3 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension21 = new DynamicResourceExtension("EmptyStateSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding19 = dynamicResourceExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage4, svgPathProperty3, binding19);
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		Controls children17 = stackPanel13.Children;
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		children17.Add(textBlock14);
		TextBlock textBlock16 = (textBlock4 = textBlock15);
		context.PushParent(textBlock4);
		TextBlock textBlock17 = textBlock4;
		textBlock17.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.FriendsListEmpty;
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj12 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock17, obj12);
		textBlock17.FontWeight = FontWeight.Bold;
		textBlock17.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		textBlock17.FontSize = 24.0;
		StyledProperty<IBrush?> foregroundProperty7 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension22 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding20 = dynamicResourceExtension22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock17, foregroundProperty7, binding20);
		textBlock17.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock17.HorizontalAlignment = HorizontalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock16).EndInit();
		Controls children18 = stackPanel13.Children;
		TextBlock textBlock19;
		TextBlock textBlock18 = (textBlock19 = new TextBlock());
		((ISupportInitialize)textBlock18).BeginInit();
		children18.Add(textBlock18);
		TextBlock textBlock20 = (textBlock4 = textBlock19);
		context.PushParent(textBlock4);
		TextBlock textBlock21 = textBlock4;
		textBlock21.TextWrapping = TextWrapping.Wrap;
		textBlock21.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.FriendsListEmptyDescription;
		StaticResourceExtension staticResourceExtension8 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj13 = staticResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock21, obj13);
		textBlock21.FontWeight = FontWeight.Medium;
		textBlock21.Margin = new Thickness(0.0, 10.0, 0.0, 0.0);
		textBlock21.FontSize = 14.0;
		StyledProperty<IBrush?> foregroundProperty8 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension23 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding21 = dynamicResourceExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock21, foregroundProperty8, binding21);
		textBlock21.Padding = new Thickness(10.0, 0.0, 10.0, 0.0);
		textBlock21.HorizontalAlignment = HorizontalAlignment.Center;
		textBlock21.TextAlignment = TextAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock20).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel12).EndInit();
		context.PopParent();
		((ISupportInitialize)panel8).EndInit();
		context.PopParent();
		((ISupportInitialize)rootScrollViewer3).EndInit();
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
	private static void _0021XamlIlPopulateTrampoline(InviteMembersView P_0)
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

