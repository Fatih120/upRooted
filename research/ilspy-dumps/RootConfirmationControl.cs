// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootConfirmationControl
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
using Avalonia.Data.Converters;
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

public class RootConfirmationControl : UserControl
{
	public static readonly StyledProperty<string> TitleTextProperty = AvaloniaProperty.Register<RootConfirmationControl, string>("TitleText");

	public static readonly StyledProperty<string> ConfirmationMessageProperty = AvaloniaProperty.Register<RootConfirmationControl, string>("ConfirmationMessage");

	public static readonly StyledProperty<string> ConfirmationButtonTextProperty = AvaloniaProperty.Register<RootConfirmationControl, string>("ConfirmationButtonText", RootApp.Client.Avalonia.Resources.Strings.Resources.Delete);

	public static readonly StyledProperty<IBrush> ConfirmationButtonBackgroundColorProperty = AvaloniaProperty.Register<RootConfirmationControl, IBrush>("ConfirmationButtonBackgroundColor", Application.Current.FindResource(Application.Current.ActualThemeVariant, "Error") as IBrush);

	public static readonly StyledProperty<IBrush> ConfirmationButtonForegroundColorProperty = AvaloniaProperty.Register<RootConfirmationControl, IBrush>("ConfirmationButtonForegroundColor", Application.Current.FindResource(Application.Current.ActualThemeVariant, "TextWhite") as IBrush);

	public static readonly StyledProperty<string> CancelButtonTextProperty = AvaloniaProperty.Register<RootConfirmationControl, string>("CancelButtonText");

	public static readonly StyledProperty<string> SvgPathProperty = AvaloniaProperty.Register<RootConfirmationControl, string>("SvgPath");

	public static readonly StyledProperty<double> SvgWidthProperty = AvaloniaProperty.Register<RootConfirmationControl, double>("SvgWidth", double.NaN);

	public static readonly StyledProperty<double> SvgHeightProperty = AvaloniaProperty.Register<RootConfirmationControl, double>("SvgHeight", double.NaN);

	public static readonly StyledProperty<string> TypedConfirmationTitleProperty = AvaloniaProperty.Register<RootConfirmationControl, string>("TypedConfirmationTitle");

	public static readonly StyledProperty<string> TypedConfirmationProperty = AvaloniaProperty.Register<RootConfirmationControl, string>("TypedConfirmation");

	public static readonly StyledProperty<string> TypedConfirmationPlaceholderTextProperty = AvaloniaProperty.Register<RootConfirmationControl, string>("TypedConfirmationPlaceholderText");

	public static readonly StyledProperty<string> TypedConfirmationMatchingTextProperty = AvaloniaProperty.Register<RootConfirmationControl, string>("TypedConfirmationMatchingText");

	public static readonly StyledProperty<string> ConfirmationTextBoxTextProperty = AvaloniaProperty.Register<RootConfirmationControl, string>("ConfirmationTextBoxText");

	public static readonly StyledProperty<string> WebApiSendingTextProperty = AvaloniaProperty.Register<RootConfirmationControl, string>("WebApiSendingText");

	public static readonly StyledProperty<string> WebApiSuccessTextProperty = AvaloniaProperty.Register<RootConfirmationControl, string>("WebApiSuccessText");

	public static readonly StyledProperty<string> WebApiFailedTextProperty = AvaloniaProperty.Register<RootConfirmationControl, string>("WebApiFailedText");

	public static readonly StyledProperty<WebApiStatus> WebApiStatusProperty = AvaloniaProperty.Register<RootConfirmationControl, WebApiStatus>("WebApiStatus", WebApiStatus.Default);

	public static readonly StyledProperty<ICommand?> CloseViewModelCommandProperty = AvaloniaProperty.Register<RootConfirmationControl, ICommand>("CloseViewModelCommand", null, false, BindingMode.OneWay, null, null, true);

	public static readonly StyledProperty<ICommand?> ConfirmationCommandProperty = AvaloniaProperty.Register<RootConfirmationControl, ICommand>("ConfirmationCommand", null, false, BindingMode.OneWay, null, null, true);

	public static readonly StyledProperty<TextBlock> MessageTextBlockProperty = AvaloniaProperty.Register<RootConfirmationControl, TextBlock>("MessageTextBlock");

	public static readonly StyledProperty<IMarkdownEngine> MarkdownEngineProperty = AvaloniaProperty.Register<RootConfirmationControl, IMarkdownEngine>("MarkdownEngine");

	public static readonly StyledProperty<bool> IsPasswordTextboxProperty = AvaloniaProperty.Register<RootConfirmationControl, bool>("IsPasswordTextbox", false);

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal UserControl RootConfirmationUserControl;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border BackgroundBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootBorder MainBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootTextbox TypedConfirmationTextBox;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Button ConfirmButton;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public string TitleText
	{
		set
		{
			SetValue(TitleTextProperty, value2);
		}
	}

	public string ConfirmationButtonText
	{
		set
		{
			SetValue(ConfirmationButtonTextProperty, value2);
		}
	}

	public string CancelButtonText
	{
		set
		{
			SetValue(CancelButtonTextProperty, value2);
		}
	}

	public double SvgWidth
	{
		set
		{
			SetValue(SvgWidthProperty, value2);
		}
	}

	public double SvgHeight
	{
		set
		{
			SetValue(SvgHeightProperty, value2);
		}
	}

	public string TypedConfirmationTitle
	{
		set
		{
			SetValue(TypedConfirmationTitleProperty, value2);
		}
	}

	public string TypedConfirmationPlaceholderText
	{
		set
		{
			SetValue(TypedConfirmationPlaceholderTextProperty, value2);
		}
	}

	public string WebApiSendingText
	{
		set
		{
			SetValue(WebApiSendingTextProperty, value2);
		}
	}

	public string WebApiSuccessText
	{
		set
		{
			SetValue(WebApiSuccessTextProperty, value2);
		}
	}

	public string WebApiFailedText
	{
		set
		{
			SetValue(WebApiFailedTextProperty, value2);
		}
	}

	public bool IsPasswordTextbox
	{
		set
		{
			SetValue(IsPasswordTextboxProperty, value2);
		}
	}

	public RootConfirmationControl()
	{
		InitializeComponent();
	}

	protected override void OnLoaded(RoutedEventArgs P_0)
	{
		base.OnLoaded(P_0);
		if (TypedConfirmationTextBox.IsVisible)
		{
			TypedConfirmationTextBox.FocusTextBox();
		}
		else
		{
			ConfirmButton.Focus();
		}
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
		RootConfirmationUserControl = nameScope?.Find<UserControl>("RootConfirmationUserControl");
		BackgroundBorder = nameScope?.Find<Border>("BackgroundBorder");
		MainBorder = nameScope?.Find<RootBorder>("MainBorder");
		TypedConfirmationTextBox = nameScope?.Find<RootTextbox>("TypedConfirmationTextBox");
		ConfirmButton = nameScope?.Find<Button>("ConfirmButton");
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulate(IServiceProvider P_0, RootConfirmationControl P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<RootConfirmationControl> context = new CompiledAvaloniaXaml.XamlIlContext.Context<RootConfirmationControl>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FControls_002FRootConfirmationControl_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Controls/RootConfirmationControl.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.Name = "RootConfirmationUserControl";
		object obj = P_1;
		context.AvaloniaNameScope.Register("RootConfirmationUserControl", obj);
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
		obj = border4;
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
		CompiledBindingExtension obj2 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(CloseViewModelCommandProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = InvokeCommandActionBase.CommandProperty;
		CompiledBindingExtension compiledBindingExtension = obj2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(invokeCommandAction2, commandProperty, compiledBindingExtension);
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
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension obj3 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(TitleTextProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension2 = obj3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, compiledBindingExtension2);
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding4);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj4 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj4);
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
		rootSvgButton4.Classes.Add("SvgDimmedButton");
		rootSvgButton4.Margin = new Thickness(0.0, 0.0, 24.0, 0.0);
		StyledProperty<ICommand?> commandProperty2 = Button.CommandProperty;
		CompiledBindingExtension obj5 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(CloseViewModelCommandProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension3 = obj5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton4, commandProperty2, compiledBindingExtension3);
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
		stackPanel5.Margin = new Thickness(24.0, 24.0, 24.0, 24.0);
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
		CompiledBindingExtension obj6 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(SvgPathProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		CompiledBindingExtension compiledBindingExtension4 = obj6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage4, svgPathProperty2, compiledBindingExtension4);
		StyledProperty<double> widthProperty = Layoutable.WidthProperty;
		CompiledBindingExtension obj7 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(SvgWidthProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Layoutable.WidthProperty;
		CompiledBindingExtension compiledBindingExtension5 = obj7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage4, widthProperty, compiledBindingExtension5);
		StyledProperty<double> heightProperty = Layoutable.HeightProperty;
		CompiledBindingExtension obj8 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(SvgHeightProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Layoutable.HeightProperty;
		CompiledBindingExtension compiledBindingExtension6 = obj8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage4, heightProperty, compiledBindingExtension6);
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
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(MarkdownEngineProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = RootMarkdownTextBlock.EngineProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMarkdownTextBlock4, engineProperty, compiledBindingExtension8);
		DirectProperty<RootMarkdownTextBlock, string?> markdownProperty = RootMarkdownTextBlock.MarkdownProperty;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(ConfirmationMessageProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = RootMarkdownTextBlock.MarkdownProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMarkdownTextBlock4, markdownProperty, compiledBindingExtension10);
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
		StyledProperty<string?> textProperty2 = TextBlock.TextProperty;
		CompiledBindingExtension obj9 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(TypedConfirmationTitleProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension11 = obj9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, textProperty2, compiledBindingExtension11);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension13;
		CompiledBindingExtension compiledBindingExtension12 = (compiledBindingExtension13 = new CompiledBindingExtension());
		context.PushParent(compiledBindingExtension13);
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension13;
		compiledBindingExtension14.Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(TypedConfirmationTitleProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("StringNullOrEmptyToVisibilityConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj10 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension14.Converter = (IValueConverter)obj10;
		context.PopParent();
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension15 = compiledBindingExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, isVisibleProperty, compiledBindingExtension15);
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding7);
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj11 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj11);
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
		rootTextbox4.Name = "TypedConfirmationTextBox";
		obj = rootTextbox4;
		context.AvaloniaNameScope.Register("TypedConfirmationTextBox", obj);
		StyledProperty<string> placeholderTextProperty = RootTextbox.PlaceholderTextProperty;
		CompiledBindingExtension obj12 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(TypedConfirmationPlaceholderTextProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = RootTextbox.PlaceholderTextProperty;
		CompiledBindingExtension compiledBindingExtension16 = obj12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox4, placeholderTextProperty, compiledBindingExtension16);
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension17 = (compiledBindingExtension13 = new CompiledBindingExtension());
		context.PushParent(compiledBindingExtension13);
		CompiledBindingExtension compiledBindingExtension18 = compiledBindingExtension13;
		compiledBindingExtension18.Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(TypedConfirmationTitleProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("StringNullOrEmptyToVisibilityConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj13 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension18.Converter = (IValueConverter)obj13;
		context.PopParent();
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension19 = compiledBindingExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox4, isVisibleProperty2, compiledBindingExtension19);
		StyledProperty<bool> isPasswordTextboxProperty = RootTextbox.IsPasswordTextboxProperty;
		CompiledBindingExtension obj14 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(IsPasswordTextboxProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = RootTextbox.IsPasswordTextboxProperty;
		CompiledBindingExtension compiledBindingExtension20 = obj14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox4, isPasswordTextboxProperty, compiledBindingExtension20);
		StyledProperty<string> textProperty3 = RootTextbox.TextProperty;
		CompiledBindingExtension compiledBindingExtension21 = new CompiledBindingExtension();
		compiledBindingExtension21.Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(ConfirmationTextBoxTextProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
		compiledBindingExtension21.Mode = BindingMode.TwoWay;
		context.ProvideTargetProperty = RootTextbox.TextProperty;
		CompiledBindingExtension compiledBindingExtension22 = compiledBindingExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox4, textProperty3, compiledBindingExtension22);
		rootTextbox4.ValidationPropertyName = "ConfirmationTextBoxText";
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj15 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(rootTextbox4, obj15);
		rootTextbox4.BorderHeight = 52.0;
		rootTextbox4.BorderCornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		StyledProperty<IBrush> borderBackgroundBrushProperty = RootTextbox.BorderBackgroundBrushProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("Input");
		context.ProvideTargetProperty = RootTextbox.BorderBackgroundBrushProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox4, borderBackgroundBrushProperty, binding8);
		StyledProperty<IBrush> borderBorderBrushProperty = RootTextbox.BorderBorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = RootTextbox.BorderBorderBrushProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox4, borderBorderBrushProperty, binding9);
		rootTextbox4.BorderBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
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
		CompiledBindingExtension obj16 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(CloseViewModelCommandProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension23 = obj16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, commandProperty3, compiledBindingExtension23);
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension24 = (compiledBindingExtension13 = new CompiledBindingExtension());
		context.PushParent(compiledBindingExtension13);
		CompiledBindingExtension compiledBindingExtension25 = compiledBindingExtension13;
		compiledBindingExtension25.Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(CancelButtonTextProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("StringNullOrEmptyToVisibilityConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj17 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension25.Converter = (IValueConverter)obj17;
		context.PopParent();
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension26 = compiledBindingExtension24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, isVisibleProperty3, compiledBindingExtension26);
		button5.Classes.Add("BorderButton");
		button5.Height = 40.0;
		button5.MinWidth = 101.0;
		button5.Padding = new Thickness(28.0, 0.0, 28.0, 0.0);
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj18 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(button5, obj18);
		button5.FontWeight = FontWeight.Medium;
		CompiledBindingExtension obj19 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(CancelButtonTextProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension27 = obj19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_4(button5, compiledBindingExtension27);
		StyledProperty<IBrush?> borderBrushProperty2 = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, borderBrushProperty2, binding10);
		StyledProperty<IBrush?> foregroundProperty3 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, foregroundProperty3, binding11);
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
		button9.Name = "ConfirmButton";
		obj = button9;
		context.AvaloniaNameScope.Register("ConfirmButton", obj);
		StyledProperty<ICommand?> commandProperty4 = Button.CommandProperty;
		CompiledBindingExtension obj20 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(ConfirmationCommandProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension28 = obj20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, commandProperty4, compiledBindingExtension28);
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension29 = (compiledBindingExtension13 = new CompiledBindingExtension());
		context.PushParent(compiledBindingExtension13);
		CompiledBindingExtension compiledBindingExtension30 = compiledBindingExtension13;
		compiledBindingExtension30.Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(ConfirmationButtonTextProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
		StaticResourceExtension staticResourceExtension8 = new StaticResourceExtension("StringNullOrEmptyToVisibilityConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj21 = staticResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension30.Converter = (IValueConverter)obj21;
		context.PopParent();
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension31 = compiledBindingExtension29.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, isVisibleProperty4, compiledBindingExtension31);
		button9.Classes.Add("BorderButton");
		button9.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button9.Height = 40.0;
		button9.MinWidth = 101.0;
		button9.IsDefault = true;
		button9.Padding = new Thickness(28.0, 0.0, 28.0, 0.0);
		StaticResourceExtension staticResourceExtension9 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj22 = staticResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(button9, obj22);
		button9.FontWeight = FontWeight.Medium;
		CompiledBindingExtension obj23 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(ConfirmationButtonTextProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension32 = obj23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_4(button9, compiledBindingExtension32);
		StyledProperty<IBrush?> backgroundProperty3 = TemplatedControl.BackgroundProperty;
		CompiledBindingExtension obj24 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(ConfirmationButtonBackgroundColorProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		CompiledBindingExtension compiledBindingExtension33 = obj24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, backgroundProperty3, compiledBindingExtension33);
		StyledProperty<IBrush?> foregroundProperty4 = TemplatedControl.ForegroundProperty;
		CompiledBindingExtension obj25 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(ConfirmationButtonForegroundColorProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		CompiledBindingExtension compiledBindingExtension34 = obj25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, foregroundProperty4, compiledBindingExtension34);
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
		CompiledBindingExtension obj26 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(WebApiStatusProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = RootWebApiStatus.WebApiStatusProperty;
		CompiledBindingExtension compiledBindingExtension35 = obj26.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootWebApiStatus4, webApiStatusProperty, compiledBindingExtension35);
		StyledProperty<ICommand?> closeCommandProperty = RootWebApiStatus.CloseCommandProperty;
		CompiledBindingExtension obj27 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(CloseViewModelCommandProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = RootWebApiStatus.CloseCommandProperty;
		CompiledBindingExtension compiledBindingExtension36 = obj27.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootWebApiStatus4, closeCommandProperty, compiledBindingExtension36);
		StyledProperty<string> sendingTextProperty = RootWebApiStatus.SendingTextProperty;
		CompiledBindingExtension obj28 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(WebApiSendingTextProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = RootWebApiStatus.SendingTextProperty;
		CompiledBindingExtension compiledBindingExtension37 = obj28.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootWebApiStatus4, sendingTextProperty, compiledBindingExtension37);
		StyledProperty<string> successTextProperty = RootWebApiStatus.SuccessTextProperty;
		CompiledBindingExtension obj29 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(WebApiSuccessTextProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = RootWebApiStatus.SuccessTextProperty;
		CompiledBindingExtension compiledBindingExtension38 = obj29.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootWebApiStatus4, successTextProperty, compiledBindingExtension38);
		StyledProperty<string> failedTextProperty = RootWebApiStatus.FailedTextProperty;
		CompiledBindingExtension obj30 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootConfirmationUserControl").Property(WebApiFailedTextProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = RootWebApiStatus.FailedTextProperty;
		CompiledBindingExtension compiledBindingExtension39 = obj30.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootWebApiStatus4, failedTextProperty, compiledBindingExtension39);
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
	private static void _0021XamlIlPopulateTrampoline(RootConfirmationControl P_0)
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

