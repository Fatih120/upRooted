// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.SaveChangesView
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
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Skia.Lottie;
using Avalonia.Styling;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.Avalonia.Resources.Strings;

public class SaveChangesView : UserControl
{
	public static readonly StyledProperty<bool> CanSaveProperty = AvaloniaProperty.Register<Button, bool>("CanSave", false, false, BindingMode.OneWay, null, null, true);

	public static readonly StyledProperty<ICommand?> SaveChangesCommandProperty = AvaloniaProperty.Register<Button, ICommand>("SaveChangesCommand", null, false, BindingMode.OneWay, null, null, true);

	public static readonly StyledProperty<ICommand?> RevertChangesCommandProperty = AvaloniaProperty.Register<Button, ICommand>("RevertChangesCommand", null, false, BindingMode.OneWay, null, null, true);

	public static readonly StyledProperty<WebApiStatus> WebApiStatusProperty = AvaloniaProperty.Register<RootWebApiStatus, WebApiStatus>("WebApiStatus", WebApiStatus.Default);

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal UserControl RootSaveChangesControl;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Button RevertChangesButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Button SaveChangesButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock SaveChangesButtonTextBlock;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Lottie LoadingSpinner;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public WebApiStatus WebApiStatus => GetValue(WebApiStatusProperty);

	public SaveChangesView()
	{
		InitializeComponent();
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		base.OnPropertyChanged(P_0);
		if (P_0.Property == WebApiStatusProperty)
		{
			switch (WebApiStatus)
			{
			case WebApiStatus.Sending:
				LoadingSpinner.Path = Application.Current.FindResource(Application.Current.ActualThemeVariant, "LoadingSpinnerPath").ToString();
				LoadingSpinner.IsVisible = true;
				SaveChangesButtonTextBlock.IsVisible = false;
				base.IsEnabled = false;
				break;
			case WebApiStatus.Default:
			case WebApiStatus.Success:
			case WebApiStatus.Failed:
				LoadingSpinner.IsVisible = false;
				LoadingSpinner.Stop();
				SaveChangesButtonTextBlock.IsVisible = true;
				base.IsEnabled = true;
				break;
			}
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
		RootSaveChangesControl = nameScope?.Find<UserControl>("RootSaveChangesControl");
		RevertChangesButton = nameScope?.Find<Button>("RevertChangesButton");
		SaveChangesButton = nameScope?.Find<Button>("SaveChangesButton");
		SaveChangesButtonTextBlock = nameScope?.Find<TextBlock>("SaveChangesButtonTextBlock");
		LoadingSpinner = nameScope?.Find<Lottie>("LoadingSpinner");
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulate(IServiceProvider P_0, SaveChangesView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<SaveChangesView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<SaveChangesView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FControls_002FSaveChangesView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Controls/SaveChangesView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.Name = "RootSaveChangesControl";
		object obj = P_1;
		context.AvaloniaNameScope.Register("RootSaveChangesControl", obj);
		P_1.IsVisible = false;
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		P_1.Content = panel;
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		Panel panel5 = panel4;
		panel5.Margin = new Thickness(24.0, 24.0, 24.0, 24.0);
		Controls children = panel5.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		border4.Height = 56.0;
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border4, backgroundProperty, binding);
		border4.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		Controls children2 = panel5.Children;
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		children2.Add(grid);
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		ColumnDefinitions columnDefinitions = new ColumnDefinitions();
		columnDefinitions.Capacity = 3;
		columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid4.ColumnDefinitions = columnDefinitions;
		Controls children3 = grid4.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children3.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.YouHaveUnsavedChanges;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj2);
		textBlock5.FontWeight = FontWeight.Bold;
		textBlock5.FontSize = 14.0;
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock5.Margin = new Thickness(20.0, 0.0, 0.0, 0.0);
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding2);
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		Controls children4 = grid4.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children4.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		Grid.SetColumn(stackPanel4, 2);
		stackPanel4.Orientation = Orientation.Horizontal;
		stackPanel4.HorizontalAlignment = HorizontalAlignment.Right;
		Controls children5 = stackPanel4.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children5.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		Button button5 = button4;
		button5.Name = "RevertChangesButton";
		obj = button5;
		context.AvaloniaNameScope.Register("RevertChangesButton", obj);
		button5.Height = 35.0;
		button5.Margin = new Thickness(16.0, 0.0, 0.0, 0.0);
		button5.HorizontalAlignment = HorizontalAlignment.Right;
		button5.Classes.Add("TextButton");
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj3 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(button5, obj3);
		button5.FontWeight = FontWeight.Medium;
		button5.Content = RootApp.Client.Avalonia.Resources.Strings.Resources.Revert;
		button5.CornerRadius = new CornerRadius(16.0, 16.0, 16.0, 16.0);
		StyledProperty<IBrush?> foregroundProperty2 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, foregroundProperty2, binding3);
		button5.Background = new ImmutableSolidColorBrush(16777215u);
		button5.BorderBrush = new ImmutableSolidColorBrush(16777215u);
		button5.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button5.FontSize = 13.0;
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension obj4 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSaveChangesControl").Property(RevertChangesCommandProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension = obj4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, commandProperty, compiledBindingExtension);
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		Controls children6 = stackPanel4.Children;
		Panel panel7;
		Panel panel6 = (panel7 = new Panel());
		((ISupportInitialize)panel6).BeginInit();
		children6.Add(panel6);
		Panel panel8 = (panel4 = panel7);
		context.PushParent(panel4);
		Panel panel9 = panel4;
		panel9.Margin = new Thickness(0.0, 0.0, 16.0, 0.0);
		Controls children7 = panel9.Children;
		ThemeVariantScope themeVariantScope2;
		ThemeVariantScope themeVariantScope = (themeVariantScope2 = new ThemeVariantScope());
		((ISupportInitialize)themeVariantScope).BeginInit();
		children7.Add(themeVariantScope);
		ThemeVariantScope themeVariantScope4;
		ThemeVariantScope themeVariantScope3 = (themeVariantScope4 = themeVariantScope2);
		context.PushParent(themeVariantScope4);
		ThemeVariantScope themeVariantScope5 = themeVariantScope4;
		themeVariantScope5.RequestedThemeVariant = ThemeVariant.Light;
		Button button7;
		Button button6 = (button7 = new Button());
		((ISupportInitialize)button6).BeginInit();
		themeVariantScope5.Child = button6;
		Button button8 = (button4 = button7);
		context.PushParent(button4);
		Button button9 = button4;
		button9.Name = "SaveChangesButton";
		obj = button9;
		context.AvaloniaNameScope.Register("SaveChangesButton", obj);
		StyledProperty<bool> isEnabledProperty = InputElement.IsEnabledProperty;
		CompiledBindingExtension obj5 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSaveChangesControl").Property(CanSaveProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension2 = obj5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, isEnabledProperty, compiledBindingExtension2);
		button9.Classes.Add("BorderButton");
		button9.Margin = new Thickness(16.0, 0.0, 0.0, 0.0);
		button9.Height = 32.0;
		button9.Width = 122.0;
		button9.IsDefault = true;
		StyledProperty<ICommand?> commandProperty2 = Button.CommandProperty;
		CompiledBindingExtension obj6 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSaveChangesControl").Property(SaveChangesCommandProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension3 = obj6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, commandProperty2, compiledBindingExtension3);
		StyledProperty<IBrush?> backgroundProperty2 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, backgroundProperty2, binding4);
		button9.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button9.CornerRadius = new CornerRadius(16.0, 16.0, 16.0, 16.0);
		Panel panel11;
		Panel panel10 = (panel11 = new Panel());
		((ISupportInitialize)panel10).BeginInit();
		button9.Content = panel10;
		Panel panel12 = (panel4 = panel11);
		context.PushParent(panel4);
		Panel panel13 = panel4;
		Controls children8 = panel13.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children8.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Name = "SaveChangesButtonTextBlock";
		obj = textBlock9;
		context.AvaloniaNameScope.Register("SaveChangesButtonTextBlock", obj);
		textBlock9.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.SaveChanges;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj7 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj7);
		textBlock9.FontWeight = FontWeight.Medium;
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty3, binding5);
		textBlock9.FontSize = 13.0;
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock9.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		Controls children9 = panel13.Children;
		ThemeVariantScope themeVariantScope7;
		ThemeVariantScope themeVariantScope6 = (themeVariantScope7 = new ThemeVariantScope());
		((ISupportInitialize)themeVariantScope6).BeginInit();
		children9.Add(themeVariantScope6);
		ThemeVariantScope themeVariantScope8 = (themeVariantScope4 = themeVariantScope7);
		context.PushParent(themeVariantScope4);
		ThemeVariantScope themeVariantScope9 = themeVariantScope4;
		themeVariantScope9.RequestedThemeVariant = ThemeVariant.Dark;
		Lottie lottie2;
		Lottie lottie = (lottie2 = new Lottie(context));
		((ISupportInitialize)lottie).BeginInit();
		themeVariantScope9.Child = lottie;
		lottie2.Name = "LoadingSpinner";
		obj = lottie2;
		context.AvaloniaNameScope.Register("LoadingSpinner", obj);
		lottie2.IsVisible = false;
		lottie2.Width = 17.0;
		lottie2.Height = 17.0;
		((ISupportInitialize)lottie2).EndInit();
		context.PopParent();
		((ISupportInitialize)themeVariantScope8).EndInit();
		context.PopParent();
		((ISupportInitialize)panel12).EndInit();
		context.PopParent();
		((ISupportInitialize)button8).EndInit();
		context.PopParent();
		((ISupportInitialize)themeVariantScope3).EndInit();
		context.PopParent();
		((ISupportInitialize)panel8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
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
	private static void _0021XamlIlPopulateTrampoline(SaveChangesView P_0)
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

