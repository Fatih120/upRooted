// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootMultiCheckBox
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;

public class RootMultiCheckBox : UserControl
{
	public static readonly StyledProperty<bool?> IsCheckedProperty = AvaloniaProperty.Register<RootMultiCheckBox, bool?>("IsChecked");

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Grid DisabledGrid;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootBorder DisabledBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootBorder DisabledBackgroundBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Grid NeutralGrid;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootBorder NeutralBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Grid EnabledGrid;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootBorder EnabledBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border EnabledBackgroundBorder;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public bool? IsChecked
	{
		get
		{
			return GetValue(IsCheckedProperty);
		}
		set
		{
			SetValue(IsCheckedProperty, value2);
		}
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		if (P_0.Property == IsCheckedProperty)
		{
			renderIsChecked();
		}
		base.OnPropertyChanged(P_0);
	}

	public RootMultiCheckBox()
	{
		InitializeComponent();
		renderIsChecked();
	}

	private void renderIsChecked()
	{
		DisabledBorder.IsVisible = false;
		NeutralBorder.IsVisible = false;
		EnabledBorder.IsVisible = false;
		EnabledBackgroundBorder.IsVisible = false;
		bool? isChecked = IsChecked;
		bool? flag = isChecked;
		if (flag.HasValue)
		{
			if (flag != true)
			{
				DisabledBorder.IsVisible = true;
				return;
			}
			EnabledBorder.IsVisible = true;
			EnabledBackgroundBorder.IsVisible = true;
		}
		else
		{
			NeutralBorder.IsVisible = true;
		}
	}

	private void onDisabledGridPointerPressed(object? sender, PointerPressedEventArgs e)
	{
		IsChecked = false;
	}

	private void onNeutralGridPointerPressed(object? sender, PointerPressedEventArgs e)
	{
		IsChecked = null;
	}

	private void onEnabledGridPointerPressed(object? sender, PointerPressedEventArgs e)
	{
		IsChecked = true;
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
		DisabledGrid = nameScope?.Find<Grid>("DisabledGrid");
		DisabledBorder = nameScope?.Find<RootBorder>("DisabledBorder");
		DisabledBackgroundBorder = nameScope?.Find<RootBorder>("DisabledBackgroundBorder");
		NeutralGrid = nameScope?.Find<Grid>("NeutralGrid");
		NeutralBorder = nameScope?.Find<RootBorder>("NeutralBorder");
		EnabledGrid = nameScope?.Find<Grid>("EnabledGrid");
		EnabledBorder = nameScope?.Find<RootBorder>("EnabledBorder");
		EnabledBackgroundBorder = nameScope?.Find<Border>("EnabledBackgroundBorder");
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulate(IServiceProvider P_0, RootMultiCheckBox P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<RootMultiCheckBox> context = new CompiledAvaloniaXaml.XamlIlContext.Context<RootMultiCheckBox>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FControls_002FRootMultiCheckBox_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Controls/RootMultiCheckBox.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.Width = 108.0;
		P_1.Height = 28.0;
		P_1.Cursor = new Cursor(StandardCursorType.Hand);
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		P_1.Content = rootBorder;
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		RootBorder rootBorder5 = rootBorder4;
		rootBorder5.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		rootBorder5.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, borderBrushProperty, binding);
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
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Pixel)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Pixel)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		Controls children = grid5.Children;
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		children.Add(grid6);
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		grid9.Name = "DisabledGrid";
		object obj = grid9;
		context.AvaloniaNameScope.Register("DisabledGrid", obj);
		grid9.AddHandler(InputElement.PointerPressedEvent, context.RootObject.onDisabledGridPointerPressed);
		grid9.Background = new ImmutableSolidColorBrush(16777215u);
		Controls children2 = grid9.Children;
		RootBorder rootBorder7;
		RootBorder rootBorder6 = (rootBorder7 = new RootBorder());
		((ISupportInitialize)rootBorder6).BeginInit();
		children2.Add(rootBorder6);
		RootBorder rootBorder8 = (rootBorder4 = rootBorder7);
		context.PushParent(rootBorder4);
		RootBorder rootBorder9 = rootBorder4;
		rootBorder9.Name = "DisabledBorder";
		obj = rootBorder9;
		context.AvaloniaNameScope.Register("DisabledBorder", obj);
		rootBorder9.CornerRadius = new CornerRadius(8.0, 0.0, 0.0, 8.0);
		StyledProperty<IBrush?> borderBrushProperty2 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("Error");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, borderBrushProperty2, binding2);
		rootBorder9.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder9.Background = new ImmutableSolidColorBrush(16777215u);
		rootBorder9.IsVisible = false;
		context.PopParent();
		((ISupportInitialize)rootBorder8).EndInit();
		Controls children3 = grid9.Children;
		RootBorder rootBorder11;
		RootBorder rootBorder10 = (rootBorder11 = new RootBorder());
		((ISupportInitialize)rootBorder10).BeginInit();
		children3.Add(rootBorder10);
		RootBorder rootBorder12 = (rootBorder4 = rootBorder11);
		context.PushParent(rootBorder4);
		RootBorder rootBorder13 = rootBorder4;
		rootBorder13.Name = "DisabledBackgroundBorder";
		obj = rootBorder13;
		context.AvaloniaNameScope.Register("DisabledBackgroundBorder", obj);
		rootBorder13.CornerRadius = new CornerRadius(8.0, 0.0, 0.0, 8.0);
		rootBorder13.BorderBrush = new ImmutableSolidColorBrush(16777215u);
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("Error");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder13, backgroundProperty, binding3);
		rootBorder13.Opacity = 0.15;
		rootBorder13.IsVisible = false;
		context.PopParent();
		((ISupportInitialize)rootBorder12).EndInit();
		Controls children4 = grid9.Children;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		children4.Add(rootSvgImage);
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage5 = rootSvgImage4;
		StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("DisabledSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage5, svgPathProperty, binding4);
		rootSvgImage5.Width = 10.99;
		rootSvgImage5.Height = 10.99;
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		Controls children5 = grid5.Children;
		Rectangle rectangle2;
		Rectangle rectangle = (rectangle2 = new Rectangle());
		((ISupportInitialize)rectangle).BeginInit();
		children5.Add(rectangle);
		Rectangle rectangle4;
		Rectangle rectangle3 = (rectangle4 = rectangle2);
		context.PushParent(rectangle4);
		Rectangle rectangle5 = rectangle4;
		rectangle5.Width = 0.5;
		StyledProperty<IBrush?> fillProperty = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle5, fillProperty, binding5);
		Grid.SetColumn(rectangle5, 1);
		context.PopParent();
		((ISupportInitialize)rectangle3).EndInit();
		Controls children6 = grid5.Children;
		Grid grid11;
		Grid grid10 = (grid11 = new Grid());
		((ISupportInitialize)grid10).BeginInit();
		children6.Add(grid10);
		Grid grid12 = (grid4 = grid11);
		context.PushParent(grid4);
		Grid grid13 = grid4;
		grid13.Name = "NeutralGrid";
		obj = grid13;
		context.AvaloniaNameScope.Register("NeutralGrid", obj);
		grid13.AddHandler(InputElement.PointerPressedEvent, context.RootObject.onNeutralGridPointerPressed);
		Grid.SetColumn(grid13, 2);
		grid13.Background = new ImmutableSolidColorBrush(16777215u);
		Controls children7 = grid13.Children;
		RootBorder rootBorder15;
		RootBorder rootBorder14 = (rootBorder15 = new RootBorder());
		((ISupportInitialize)rootBorder14).BeginInit();
		children7.Add(rootBorder14);
		RootBorder rootBorder16 = (rootBorder4 = rootBorder15);
		context.PushParent(rootBorder4);
		RootBorder rootBorder17 = rootBorder4;
		rootBorder17.Name = "NeutralBorder";
		obj = rootBorder17;
		context.AvaloniaNameScope.Register("NeutralBorder", obj);
		rootBorder17.CornerRadius = new CornerRadius(2.0, 2.0, 2.0, 2.0);
		StyledProperty<IBrush?> borderBrushProperty3 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder17, borderBrushProperty3, binding6);
		rootBorder17.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder17, backgroundProperty2, binding7);
		rootBorder17.IsVisible = false;
		context.PopParent();
		((ISupportInitialize)rootBorder16).EndInit();
		Controls children8 = grid13.Children;
		RootSvgImage rootSvgImage7;
		RootSvgImage rootSvgImage6 = (rootSvgImage7 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage6).BeginInit();
		children8.Add(rootSvgImage6);
		RootSvgImage rootSvgImage8 = (rootSvgImage4 = rootSvgImage7);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage9 = rootSvgImage4;
		StyledProperty<string?> svgPathProperty2 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("NeutralSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage9, svgPathProperty2, binding8);
		rootSvgImage9.Width = 11.67;
		rootSvgImage9.Height = 11.67;
		context.PopParent();
		((ISupportInitialize)rootSvgImage8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid12).EndInit();
		Controls children9 = grid5.Children;
		Rectangle rectangle7;
		Rectangle rectangle6 = (rectangle7 = new Rectangle());
		((ISupportInitialize)rectangle6).BeginInit();
		children9.Add(rectangle6);
		Rectangle rectangle8 = (rectangle4 = rectangle7);
		context.PushParent(rectangle4);
		Rectangle rectangle9 = rectangle4;
		rectangle9.Width = 0.5;
		StyledProperty<IBrush?> fillProperty2 = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle9, fillProperty2, binding9);
		Grid.SetColumn(rectangle9, 3);
		context.PopParent();
		((ISupportInitialize)rectangle8).EndInit();
		Controls children10 = grid5.Children;
		Grid grid15;
		Grid grid14 = (grid15 = new Grid());
		((ISupportInitialize)grid14).BeginInit();
		children10.Add(grid14);
		Grid grid16 = (grid4 = grid15);
		context.PushParent(grid4);
		Grid grid17 = grid4;
		grid17.Name = "EnabledGrid";
		obj = grid17;
		context.AvaloniaNameScope.Register("EnabledGrid", obj);
		grid17.AddHandler(InputElement.PointerPressedEvent, context.RootObject.onEnabledGridPointerPressed);
		Grid.SetColumn(grid17, 4);
		grid17.Background = new ImmutableSolidColorBrush(16777215u);
		Controls children11 = grid17.Children;
		RootBorder rootBorder19;
		RootBorder rootBorder18 = (rootBorder19 = new RootBorder());
		((ISupportInitialize)rootBorder18).BeginInit();
		children11.Add(rootBorder18);
		RootBorder rootBorder20 = (rootBorder4 = rootBorder19);
		context.PushParent(rootBorder4);
		RootBorder rootBorder21 = rootBorder4;
		rootBorder21.Name = "EnabledBorder";
		obj = rootBorder21;
		context.AvaloniaNameScope.Register("EnabledBorder", obj);
		rootBorder21.CornerRadius = new CornerRadius(0.0, 8.0, 8.0, 0.0);
		StyledProperty<IBrush?> borderBrushProperty4 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("BrandTertiary");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder21, borderBrushProperty4, binding10);
		rootBorder21.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder21.Background = new ImmutableSolidColorBrush(16777215u);
		rootBorder21.IsVisible = false;
		context.PopParent();
		((ISupportInitialize)rootBorder20).EndInit();
		Controls children12 = grid17.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children12.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		border4.Name = "EnabledBackgroundBorder";
		obj = border4;
		context.AvaloniaNameScope.Register("EnabledBackgroundBorder", obj);
		border4.CornerRadius = new CornerRadius(0.0, 8.0, 8.0, 0.0);
		border4.BorderBrush = new ImmutableSolidColorBrush(16777215u);
		StyledProperty<IBrush?> backgroundProperty3 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("BrandTertiary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border4, backgroundProperty3, binding11);
		border4.Opacity = 0.15;
		border4.IsVisible = false;
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		Controls children13 = grid17.Children;
		RootSvgImage rootSvgImage11;
		RootSvgImage rootSvgImage10 = (rootSvgImage11 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage10).BeginInit();
		children13.Add(rootSvgImage10);
		RootSvgImage rootSvgImage12 = (rootSvgImage4 = rootSvgImage11);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage13 = rootSvgImage4;
		StyledProperty<string?> svgPathProperty3 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("EnabledSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding12 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage13, svgPathProperty3, binding12);
		rootSvgImage13.Width = 13.99;
		rootSvgImage13.Height = 10.48;
		context.PopParent();
		((ISupportInitialize)rootSvgImage12).EndInit();
		context.PopParent();
		((ISupportInitialize)grid16).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(RootMultiCheckBox P_0)
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

