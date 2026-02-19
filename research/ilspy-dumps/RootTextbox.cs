// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootTextbox
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Svg.Skia;
using Avalonia.Threading;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Resources.Strings;

public class RootTextbox : UserControl
{
	[CompilerGenerated]
	private bool _003CHasTextChanged_003Ek__BackingField;

	public static readonly StyledProperty<string> TextProperty = AvaloniaProperty.Register<RootTextbox, string>("Text");

	public static readonly StyledProperty<string> PlaceholderTextProperty = AvaloniaProperty.Register<RootTextbox, string>("PlaceholderText");

	public static readonly StyledProperty<bool> IsReadOnlyProperty = AvaloniaProperty.Register<RootTextbox, bool>("IsReadOnly", false);

	public static readonly StyledProperty<bool> IsPasswordTextboxProperty = AvaloniaProperty.Register<RootTextbox, bool>("IsPasswordTextbox", false);

	public static readonly StyledProperty<bool> ShowPasswordProperty = AvaloniaProperty.Register<RootTextbox, bool>("ShowPassword", false);

	public static readonly StyledProperty<TextWrapping> TextWrappingProperty = AvaloniaProperty.Register<RootTextbox, TextWrapping>("TextWrapping", TextWrapping.NoWrap);

	public static readonly StyledProperty<int> MaxLengthProperty = AvaloniaProperty.Register<RootTextbox, int>("MaxLength", 0);

	public static readonly StyledProperty<string> ValidationPropertyNameProperty = AvaloniaProperty.Register<RootTextbox, string>("ValidationPropertyName");

	public static readonly StyledProperty<double> BorderHeightProperty = AvaloniaProperty.Register<RootTextbox, double>("BorderHeight", double.NaN);

	public static readonly StyledProperty<double> BorderMinHeightProperty = AvaloniaProperty.Register<RootTextbox, double>("BorderMinHeight", double.NaN);

	public static readonly StyledProperty<double> BorderWidthProperty = AvaloniaProperty.Register<RootTextbox, double>("BorderWidth", double.NaN);

	public static readonly StyledProperty<IBrush> BorderBackgroundBrushProperty = AvaloniaProperty.Register<RootTextbox, IBrush>("BorderBackgroundBrush");

	public static readonly StyledProperty<CornerRadius> BorderCornerRadiusProperty = AvaloniaProperty.Register<RootTextbox, CornerRadius>("BorderCornerRadius");

	public static readonly StyledProperty<IBrush> BorderBorderBrushProperty = AvaloniaProperty.Register<RootTextbox, IBrush>("BorderBorderBrush");

	public static readonly StyledProperty<Thickness> BorderBorderThicknessProperty = AvaloniaProperty.Register<RootTextbox, Thickness>("BorderBorderThickness");

	public static readonly StyledProperty<string> TextboxPlaceholderTextProperty = AvaloniaProperty.Register<RootTextbox, string>("TextboxPlaceholderText");

	public static readonly StyledProperty<double> TextboxFontSizeProperty = AvaloniaProperty.Register<RootTextbox, double>("TextboxFontSize", 0.0);

	public static readonly StyledProperty<Thickness> TextboxMarginProperty = AvaloniaProperty.Register<RootTextbox, Thickness>("TextboxMargin");

	public static readonly StyledProperty<VerticalAlignment> TextVerticalAlignmentProperty = AvaloniaProperty.Register<RootTextbox, VerticalAlignment>("TextVerticalAlignment", VerticalAlignment.Center);

	public static readonly StyledProperty<string> SvgPathProperty = AvaloniaProperty.Register<RootTextbox, string>("SvgPath");

	public static readonly StyledProperty<double> SvgWidthProperty = AvaloniaProperty.Register<RootTextbox, double>("SvgWidth", 0.0);

	public static readonly StyledProperty<double> SvgHeightProperty = AvaloniaProperty.Register<RootTextbox, double>("SvgHeight", 0.0);

	public static readonly StyledProperty<Thickness> SvgMarginProperty = AvaloniaProperty.Register<RootTextbox, Thickness>("SvgMargin");

	public static readonly StyledProperty<object?> InnerRightContentProperty = AvaloniaProperty.Register<RootTextbox, object>("InnerRightContent");

	public static readonly StyledProperty<bool> ValidateImmediatelyProperty = AvaloniaProperty.Register<RootTextbox, bool>("ValidateImmediately", false);

	public static readonly StyledProperty<TimeSpan> TypingDebounceProperty = AvaloniaProperty.Register<RootTextbox, TimeSpan>("TypingDebounce", TimeSpan.FromMilliseconds(500L));

	private bool _touched;

	private bool _liveValidate;

	private DispatcherTimer? _typingTimer;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal UserControl RootTextBoxUserControl;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootBorder MainBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Panel MainGrid;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock PlaceholderTextBlock;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBox MainTextBox;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton ShowPasswordButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootBorder ErrorBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock ErrorTextBlock;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	private bool HasTextChanged
	{
		[CompilerGenerated]
		set
		{
			_003CHasTextChanged_003Ek__BackingField = flag;
		}
	}

	public string ValidationPropertyName
	{
		get
		{
			return GetValue(ValidationPropertyNameProperty);
		}
		set
		{
			SetValue(ValidationPropertyNameProperty, value2);
		}
	}

	public string Text => GetValue(TextProperty);

	public string PlaceholderText
	{
		set
		{
			SetValue(PlaceholderTextProperty, value2);
		}
	}

	public bool IsReadOnly
	{
		set
		{
			SetValue(IsReadOnlyProperty, flag);
			MainTextBox.IsReadOnly = flag;
		}
	}

	public bool IsPasswordTextbox
	{
		get
		{
			return GetValue(IsPasswordTextboxProperty);
		}
		set
		{
			SetValue(IsPasswordTextboxProperty, value2);
			ShowPassword = false;
		}
	}

	public bool ShowPassword
	{
		get
		{
			return GetValue(ShowPasswordProperty);
		}
		set
		{
			SetValue(ShowPasswordProperty, flag);
			if (flag)
			{
				MainTextBox.PasswordChar = '\0';
				ShowPasswordButton[!RootSvgButton.SvgPathProperty] = new DynamicResourceExtension("HidePasswordEyeSVG");
			}
			else
			{
				MainTextBox.PasswordChar = RootApp.Client.Avalonia.Resources.Strings.Resources.PasswordCharacter[0];
				ShowPasswordButton[!RootSvgButton.SvgPathProperty] = new DynamicResourceExtension("ShowPasswordEyeSVG");
			}
		}
	}

	public TextWrapping TextWrapping
	{
		set
		{
			SetValue(TextWrappingProperty, value2);
		}
	}

	public int MaxLength
	{
		set
		{
			SetValue(MaxLengthProperty, value2);
		}
	}

	public double BorderHeight
	{
		set
		{
			SetValue(BorderHeightProperty, value2);
		}
	}

	public double BorderMinHeight
	{
		set
		{
			SetValue(BorderMinHeightProperty, value2);
		}
	}

	public CornerRadius BorderCornerRadius
	{
		set
		{
			SetValue(BorderCornerRadiusProperty, value2);
		}
	}

	public VerticalAlignment TextVerticalAlignment
	{
		set
		{
			SetValue(TextVerticalAlignmentProperty, value2);
		}
	}

	public IBrush BorderBorderBrush => GetValue(BorderBorderBrushProperty);

	public Thickness BorderBorderThickness
	{
		set
		{
			SetValue(BorderBorderThicknessProperty, value2);
		}
	}

	public double TextboxFontSize
	{
		set
		{
			SetValue(TextboxFontSizeProperty, value2);
		}
	}

	public Thickness TextboxMargin
	{
		set
		{
			SetValue(TextboxMarginProperty, value2);
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

	public Thickness SvgMargin
	{
		set
		{
			SetValue(SvgMarginProperty, value2);
		}
	}

	public object? InnerRightContent
	{
		set
		{
			SetValue(InnerRightContentProperty, value2);
		}
	}

	public bool ValidateImmediately => GetValue(ValidateImmediatelyProperty);

	public TimeSpan TypingDebounce => GetValue(TypingDebounceProperty);

	public RootTextbox()
	{
		InitializeComponent();
		IsPasswordTextboxProperty.Changed.AddClassHandler(delegate(RootTextbox tb, AvaloniaPropertyChangedEventArgs _)
		{
			tb.ShowPassword = false;
		});
	}

	public void FocusTextBox()
	{
		MainTextBox.Focus();
		MainTextBox.CaretIndex = MainTextBox.Text?.Length ?? 0;
	}

	public void SelectAll()
	{
		MainTextBox.Focus();
		MainTextBox.SelectAll();
	}

	protected override void OnLoaded(RoutedEventArgs P_0)
	{
		base.OnLoaded(P_0);
		_liveValidate = ValidateImmediately;
		if (base.DataContext is IViewModelBase viewModelBase)
		{
			viewModelBase.ErrorsChanged += onErrorsChanged;
		}
		MainTextBox.GotFocus += onGotFocus;
		MainTextBox.LostFocus += onLostFocus;
		MainTextBox.TextChanged += onTextChanged;
		MainBorder.PointerPressed += onBorderPressed;
		_typingTimer = new DispatcherTimer
		{
			Interval = TypingDebounce
		};
		_typingTimer.Tick += onTypingTimerTick;
		renderPlaceHolderVisibility();
		if (MainTextBox.IsFocused)
		{
			renderBorderColor();
		}
	}

	protected override void OnUnloaded(RoutedEventArgs P_0)
	{
		base.OnUnloaded(P_0);
		if (base.DataContext is IViewModelBase viewModelBase)
		{
			viewModelBase.ErrorsChanged -= onErrorsChanged;
		}
		MainTextBox.GotFocus -= onGotFocus;
		MainTextBox.LostFocus -= onLostFocus;
		MainTextBox.TextChanged -= onTextChanged;
		MainBorder.PointerPressed -= onBorderPressed;
		if (_typingTimer != null)
		{
			_typingTimer.Tick -= onTypingTimerTick;
			_typingTimer.Stop();
			_typingTimer = null;
		}
	}

	private void onErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
	{
		if (!_liveValidate)
		{
			ErrorBorder.IsVisible = false;
			renderBorderColor();
		}
		else
		{
			RenderErrorsFromVm();
		}
	}

	private void onGotFocus(object? sender, GotFocusEventArgs e)
	{
		renderBorderColor();
	}

	private void onLostFocus(object? sender, RoutedEventArgs e)
	{
		renderBorderColor();
	}

	private void onBorderPressed(object? sender, PointerPressedEventArgs e)
	{
		MainTextBox.Focus();
	}

	private void onTextChanged(object? sender, TextChangedEventArgs e)
	{
		renderPlaceHolderVisibility();
		HasTextChanged = true;
		_touched = true;
		if (!_liveValidate)
		{
			if (_typingTimer == null)
			{
				_typingTimer = new DispatcherTimer();
			}
			_typingTimer.Stop();
			_typingTimer.Interval = TypingDebounce;
			_typingTimer.Start();
		}
	}

	private void onTypingTimerTick(object? sender, EventArgs e)
	{
		_typingTimer?.Stop();
		if (!_liveValidate)
		{
			_liveValidate = true;
			RenderErrorsFromVm();
		}
	}

	private void RenderErrorsFromVm()
	{
		if (!(base.DataContext is IViewModelBase viewModelBase))
		{
			ErrorBorder.IsVisible = false;
			renderBorderColor();
			return;
		}
		List<string> list = ((viewModelBase.GetErrors(ValidationPropertyName) as IEnumerable<string>) ?? Array.Empty<string>()).ToList();
		if (list.Count != 0 && (_touched || ValidateImmediately))
		{
			ErrorBorder.IsVisible = true;
			ErrorTextBlock.Text = list.First();
		}
		else
		{
			ErrorBorder.IsVisible = false;
		}
		renderBorderColor();
	}

	private void renderBorderColor()
	{
		if (!ErrorBorder.IsVisible)
		{
			if (MainTextBox.IsFocused)
			{
				MainBorder.BorderBrush = (IBrush)Application.Current.FindResource(Application.Current.ActualThemeVariant, "BrandPrimary");
			}
			else
			{
				MainBorder.BorderBrush = BorderBorderBrush;
			}
		}
		else
		{
			MainBorder.BorderBrush = (IBrush)Application.Current.FindResource(Application.Current.ActualThemeVariant, "Error");
		}
	}

	private void renderPlaceHolderVisibility()
	{
		PlaceholderTextBlock.IsVisible = string.IsNullOrEmpty(MainTextBox.Text);
	}

	private void ShowPasswordButtonClicked(object? sender, RoutedEventArgs e)
	{
		if (IsPasswordTextbox)
		{
			ShowPassword = !ShowPassword;
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
		RootTextBoxUserControl = nameScope?.Find<UserControl>("RootTextBoxUserControl");
		MainBorder = nameScope?.Find<RootBorder>("MainBorder");
		MainGrid = nameScope?.Find<Panel>("MainGrid");
		PlaceholderTextBlock = nameScope?.Find<TextBlock>("PlaceholderTextBlock");
		MainTextBox = nameScope?.Find<TextBox>("MainTextBox");
		ShowPasswordButton = nameScope?.Find<RootSvgButton>("ShowPasswordButton");
		ErrorBorder = nameScope?.Find<RootBorder>("ErrorBorder");
		ErrorTextBlock = nameScope?.Find<TextBlock>("ErrorTextBlock");
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulate(IServiceProvider P_0, RootTextbox P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<RootTextbox> context = new CompiledAvaloniaXaml.XamlIlContext.Context<RootTextbox>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FControls_002FRootTextbox_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Controls/RootTextbox.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.Name = "RootTextBoxUserControl";
		object obj = P_1;
		context.AvaloniaNameScope.Register("RootTextBoxUserControl", obj);
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		P_1.Content = stackPanel;
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		stackPanel4.VerticalAlignment = VerticalAlignment.Center;
		Controls children = stackPanel4.Children;
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		children.Add(rootBorder);
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		RootBorder rootBorder5 = rootBorder4;
		rootBorder5.Name = "MainBorder";
		obj = rootBorder5;
		context.AvaloniaNameScope.Register("MainBorder", obj);
		rootBorder5.Cursor = new Cursor(StandardCursorType.Ibeam);
		StyledProperty<double> heightProperty = Layoutable.HeightProperty;
		CompiledBindingExtension obj2 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(BorderHeightProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Layoutable.HeightProperty;
		CompiledBindingExtension compiledBindingExtension = obj2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, heightProperty, compiledBindingExtension);
		StyledProperty<double> minHeightProperty = Layoutable.MinHeightProperty;
		CompiledBindingExtension obj3 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(BorderMinHeightProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Layoutable.MinHeightProperty;
		CompiledBindingExtension compiledBindingExtension2 = obj3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, minHeightProperty, compiledBindingExtension2);
		StyledProperty<double> widthProperty = Layoutable.WidthProperty;
		CompiledBindingExtension obj4 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(BorderWidthProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Layoutable.WidthProperty;
		CompiledBindingExtension compiledBindingExtension3 = obj4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, widthProperty, compiledBindingExtension3);
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		CompiledBindingExtension obj5 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(BorderBackgroundBrushProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Border.BackgroundProperty;
		CompiledBindingExtension compiledBindingExtension4 = obj5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, backgroundProperty, compiledBindingExtension4);
		StyledProperty<CornerRadius> cornerRadiusProperty = Border.CornerRadiusProperty;
		CompiledBindingExtension obj6 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(BorderCornerRadiusProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Border.CornerRadiusProperty;
		CompiledBindingExtension compiledBindingExtension5 = obj6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, cornerRadiusProperty, compiledBindingExtension5);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		CompiledBindingExtension obj7 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(BorderBorderBrushProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		CompiledBindingExtension compiledBindingExtension6 = obj7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, borderBrushProperty, compiledBindingExtension6);
		StyledProperty<Thickness> dynamicBorderThicknessProperty = RootBorder.DynamicBorderThicknessProperty;
		CompiledBindingExtension obj8 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(BorderBorderThicknessProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = RootBorder.DynamicBorderThicknessProperty;
		CompiledBindingExtension compiledBindingExtension7 = obj8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, dynamicBorderThicknessProperty, compiledBindingExtension7);
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		rootBorder5.Child = panel;
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		panel4.Name = "MainGrid";
		obj = panel4;
		context.AvaloniaNameScope.Register("MainGrid", obj);
		Controls children2 = panel4.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children2.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Name = "PlaceholderTextBlock";
		obj = textBlock5;
		context.AvaloniaNameScope.Register("PlaceholderTextBlock", obj);
		StyledProperty<double> fontSizeProperty = TextBlock.FontSizeProperty;
		CompiledBindingExtension obj9 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(TextboxFontSizeProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = TextBlock.FontSizeProperty;
		CompiledBindingExtension compiledBindingExtension8 = obj9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, fontSizeProperty, compiledBindingExtension8);
		StyledProperty<Thickness> marginProperty = Layoutable.MarginProperty;
		CompiledBindingExtension obj10 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(TextboxMarginProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Layoutable.MarginProperty;
		CompiledBindingExtension compiledBindingExtension9 = obj10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, marginProperty, compiledBindingExtension9);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj11 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj11);
		textBlock5.FontWeight = (FontWeight)450;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding);
		StyledProperty<VerticalAlignment> verticalAlignmentProperty = Layoutable.VerticalAlignmentProperty;
		CompiledBindingExtension obj12 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(TextVerticalAlignmentProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Layoutable.VerticalAlignmentProperty;
		CompiledBindingExtension compiledBindingExtension10 = obj12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, verticalAlignmentProperty, compiledBindingExtension10);
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension obj13 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(PlaceholderTextProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension11 = obj13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, compiledBindingExtension11);
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		Controls children3 = panel4.Children;
		TextBox textBox2;
		TextBox textBox = (textBox2 = new TextBox());
		((ISupportInitialize)textBox).BeginInit();
		children3.Add(textBox);
		TextBox textBox4;
		TextBox textBox3 = (textBox4 = textBox2);
		context.PushParent(textBox4);
		textBox4.Name = "MainTextBox";
		obj = textBox4;
		context.AvaloniaNameScope.Register("MainTextBox", obj);
		textBox4.Classes.Add("BorderlessTextBox");
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj14 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(textBox4, obj14);
		StyledProperty<IBrush?> caretBrushProperty = TextBox.CaretBrushProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBox.CaretBrushProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBox4, caretBrushProperty, binding2);
		StyledProperty<IBrush?> foregroundProperty2 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBox4, foregroundProperty2, binding3);
		StyledProperty<IBrush?> selectionForegroundBrushProperty = TextBox.SelectionForegroundBrushProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBox.SelectionForegroundBrushProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBox4, selectionForegroundBrushProperty, binding4);
		StyledProperty<IBrush?> selectionBrushProperty = TextBox.SelectionBrushProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = TextBox.SelectionBrushProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBox4, selectionBrushProperty, binding5);
		StyledProperty<string?> textProperty2 = TextBox.TextProperty;
		CompiledBindingExtension compiledBindingExtension12 = new CompiledBindingExtension();
		compiledBindingExtension12.Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(TextProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
		compiledBindingExtension12.Mode = BindingMode.TwoWay;
		context.ProvideTargetProperty = TextBox.TextProperty;
		CompiledBindingExtension compiledBindingExtension13 = compiledBindingExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBox4, textProperty2, compiledBindingExtension13);
		StyledProperty<VerticalAlignment> verticalAlignmentProperty2 = Layoutable.VerticalAlignmentProperty;
		CompiledBindingExtension obj15 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(TextVerticalAlignmentProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Layoutable.VerticalAlignmentProperty;
		CompiledBindingExtension compiledBindingExtension14 = obj15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBox4, verticalAlignmentProperty2, compiledBindingExtension14);
		StyledProperty<VerticalAlignment> verticalContentAlignmentProperty = TextBox.VerticalContentAlignmentProperty;
		CompiledBindingExtension obj16 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(TextVerticalAlignmentProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = TextBox.VerticalContentAlignmentProperty;
		CompiledBindingExtension compiledBindingExtension15 = obj16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBox4, verticalContentAlignmentProperty, compiledBindingExtension15);
		textBox4.FontWeight = (FontWeight)450;
		textBox4.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBox4.MinHeight = 20.0;
		StyledProperty<TextWrapping> textWrappingProperty = TextBox.TextWrappingProperty;
		CompiledBindingExtension obj17 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(TextWrappingProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = TextBox.TextWrappingProperty;
		CompiledBindingExtension compiledBindingExtension16 = obj17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBox4, textWrappingProperty, compiledBindingExtension16);
		StyledProperty<int> maxLengthProperty = TextBox.MaxLengthProperty;
		CompiledBindingExtension obj18 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(MaxLengthProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = TextBox.MaxLengthProperty;
		CompiledBindingExtension compiledBindingExtension17 = obj18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBox4, maxLengthProperty, compiledBindingExtension17);
		StyledProperty<double> fontSizeProperty2 = TemplatedControl.FontSizeProperty;
		CompiledBindingExtension obj19 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(TextboxFontSizeProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = TemplatedControl.FontSizeProperty;
		CompiledBindingExtension compiledBindingExtension18 = obj19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBox4, fontSizeProperty2, compiledBindingExtension18);
		StyledProperty<Thickness> marginProperty2 = Layoutable.MarginProperty;
		CompiledBindingExtension obj20 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(TextboxMarginProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Layoutable.MarginProperty;
		CompiledBindingExtension compiledBindingExtension19 = obj20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBox4, marginProperty2, compiledBindingExtension19);
		context.PopParent();
		((ISupportInitialize)textBox3).EndInit();
		Controls children4 = panel4.Children;
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		children4.Add(contentControl);
		ContentControl contentControl4;
		ContentControl contentControl3 = (contentControl4 = contentControl2);
		context.PushParent(contentControl4);
		CompiledBindingExtension obj21 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(InnerRightContentProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension20 = obj21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_4(contentControl4, compiledBindingExtension20);
		contentControl4.HorizontalAlignment = HorizontalAlignment.Right;
		context.PopParent();
		((ISupportInitialize)contentControl3).EndInit();
		Controls children5 = panel4.Children;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		children5.Add(rootSvgImage);
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
		CompiledBindingExtension obj22 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(SvgPathProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		CompiledBindingExtension compiledBindingExtension21 = obj22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage4, svgPathProperty, compiledBindingExtension21);
		StyledProperty<double> widthProperty2 = Layoutable.WidthProperty;
		CompiledBindingExtension obj23 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(SvgWidthProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Layoutable.WidthProperty;
		CompiledBindingExtension compiledBindingExtension22 = obj23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage4, widthProperty2, compiledBindingExtension22);
		StyledProperty<double> heightProperty2 = Layoutable.HeightProperty;
		CompiledBindingExtension obj24 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(SvgHeightProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Layoutable.HeightProperty;
		CompiledBindingExtension compiledBindingExtension23 = obj24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage4, heightProperty2, compiledBindingExtension23);
		StyledProperty<Thickness> marginProperty3 = Layoutable.MarginProperty;
		CompiledBindingExtension obj25 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(SvgMarginProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Layoutable.MarginProperty;
		CompiledBindingExtension compiledBindingExtension24 = obj25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage4, marginProperty3, compiledBindingExtension24);
		rootSvgImage4.Opacity = 0.4;
		rootSvgImage4.HorizontalAlignment = HorizontalAlignment.Right;
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		Controls children6 = panel4.Children;
		RootSvgButton rootSvgButton2;
		RootSvgButton rootSvgButton = (rootSvgButton2 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton).BeginInit();
		children6.Add(rootSvgButton);
		RootSvgButton rootSvgButton4;
		RootSvgButton rootSvgButton3 = (rootSvgButton4 = rootSvgButton2);
		context.PushParent(rootSvgButton4);
		rootSvgButton4.Name = "ShowPasswordButton";
		obj = rootSvgButton4;
		context.AvaloniaNameScope.Register("ShowPasswordButton", obj);
		rootSvgButton4.Classes.Add("Custom");
		rootSvgButton4.HorizontalAlignment = HorizontalAlignment.Right;
		rootSvgButton4.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension obj26 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootTextBoxUserControl").Property(IsPasswordTextboxProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension25 = obj26.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton4, isVisibleProperty, compiledBindingExtension25);
		rootSvgButton4.SvgWidth = 18.0;
		rootSvgButton4.SvgHeight = 18.0;
		rootSvgButton4.Width = 20.0;
		rootSvgButton4.Height = 20.0;
		rootSvgButton4.Margin = new Thickness(0.0, 0.0, 16.0, 0.0);
		rootSvgButton4.Opacity = 0.64;
		rootSvgButton4.AddHandler((RoutedEvent)Button.ClickEvent, (Delegate)new EventHandler<RoutedEventArgs>(context.RootObject.ShowPasswordButtonClicked), RoutingStrategies.Direct | RoutingStrategies.Bubble, false);
		context.PopParent();
		((ISupportInitialize)rootSvgButton3).EndInit();
		context.PopParent();
		((ISupportInitialize)panel3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		Controls children7 = stackPanel4.Children;
		RootBorder rootBorder7;
		RootBorder rootBorder6 = (rootBorder7 = new RootBorder());
		((ISupportInitialize)rootBorder6).BeginInit();
		children7.Add(rootBorder6);
		RootBorder rootBorder8 = (rootBorder4 = rootBorder7);
		context.PushParent(rootBorder4);
		RootBorder rootBorder9 = rootBorder4;
		rootBorder9.Name = "ErrorBorder";
		obj = rootBorder9;
		context.AvaloniaNameScope.Register("ErrorBorder", obj);
		rootBorder9.IsVisible = false;
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("BackgroundSecondary`");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, backgroundProperty2, binding6);
		rootBorder9.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		StyledProperty<IBrush?> borderBrushProperty2 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, borderBrushProperty2, binding7);
		rootBorder9.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		rootBorder9.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		rootBorder9.Child = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		grid4.Margin = new Thickness(18.0, 12.0, 18.0, 12.0);
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(12.0, GridUnitType.Pixel)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		Controls children8 = grid4.Children;
		Avalonia.Svg.Skia.Svg svg2;
		Avalonia.Svg.Skia.Svg svg = (svg2 = new Avalonia.Svg.Skia.Svg(context));
		((ISupportInitialize)svg).BeginInit();
		children8.Add(svg);
		Avalonia.Svg.Skia.Svg svg4;
		Avalonia.Svg.Skia.Svg svg3 = (svg4 = svg2);
		context.PushParent(svg4);
		StyledProperty<string?> pathProperty = Avalonia.Svg.Skia.Svg.PathProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("TextboxValidationErrorSVG");
		context.ProvideTargetProperty = Avalonia.Svg.Skia.Svg.PathProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(svg4, pathProperty, binding8);
		svg4.Width = 17.0;
		svg4.Height = 17.0;
		svg4.VerticalAlignment = VerticalAlignment.Top;
		context.PopParent();
		((ISupportInitialize)svg3).EndInit();
		Controls children9 = grid4.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children9.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Name = "ErrorTextBlock";
		obj = textBlock9;
		context.AvaloniaNameScope.Register("ErrorTextBlock", obj);
		Grid.SetColumn(textBlock9, 2);
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj27 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj27);
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty3, binding9);
		textBlock9.FontSize = 13.0;
		textBlock9.TextWrapping = TextWrapping.Wrap;
		textBlock9.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(RootTextbox P_0)
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

