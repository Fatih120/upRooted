using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
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
using Avalonia.Styling;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Resources.Strings;

namespace RootApp.Client.Avalonia.UI.Home.VoiceBar;

public class ScreensharePickerView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_169
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ScreensharePickerView> context = CreateContext(P_0);
			context.IntermediateRoot = new Border();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			Border border = (Border)obj;
			context.PushParent(border);
			Border border2 = border;
			border2.Name = "HighlightBorder";
			object obj2 = border2;
			context.AvaloniaNameScope.Register("HighlightBorder", obj2);
			StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
			DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("HighlightLight");
			context.ProvideTargetProperty = Border.BackgroundProperty;
			IBinding binding = dynamicResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(border2, backgroundProperty, binding);
			StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
			DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("Border");
			context.ProvideTargetProperty = Border.BorderBrushProperty;
			IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(border2, borderBrushProperty, binding2);
			border2.SetValue(Border.BorderThicknessProperty, new Thickness(1.0, 1.0, 1.0, 1.0), BindingPriority.Template);
			border2.SetValue(Border.CornerRadiusProperty, new CornerRadius(12.0, 12.0, 12.0, 12.0), BindingPriority.Template);
			border2.SetValue(Decorator.PaddingProperty, new Thickness(12.0, 12.0, 12.0, 12.0), BindingPriority.Template);
			border2.SetValue(Layoutable.MarginProperty, new Thickness(0.0, 0.0, 0.0, 0.0), BindingPriority.Template);
			Panel panel2;
			Panel panel = (panel2 = new Panel());
			((ISupportInitialize)panel).BeginInit();
			border2.Child = panel;
			Panel panel4;
			Panel panel3 = (panel4 = panel2);
			context.PushParent(panel4);
			global::Avalonia.Controls.Controls children = panel4.Children;
			ContentPresenter contentPresenter2;
			ContentPresenter contentPresenter = (contentPresenter2 = new ContentPresenter());
			((ISupportInitialize)contentPresenter).BeginInit();
			children.Add(contentPresenter);
			contentPresenter2.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Left, BindingPriority.Template);
			CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_2(contentPresenter2, BindingPriority.Template, new TemplateBinding(ContentControl.ContentProperty).ProvideValue());
			((ISupportInitialize)contentPresenter2).EndInit();
			global::Avalonia.Controls.Controls children2 = panel4.Children;
			RootBorder rootBorder2;
			RootBorder rootBorder = (rootBorder2 = new RootBorder());
			((ISupportInitialize)rootBorder).BeginInit();
			children2.Add(rootBorder);
			RootBorder rootBorder4;
			RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
			context.PushParent(rootBorder4);
			rootBorder4.Name = "CheckContainerBorder";
			obj2 = rootBorder4;
			context.AvaloniaNameScope.Register("CheckContainerBorder", obj2);
			rootBorder4.SetValue(RootBorder.DynamicBorderThicknessProperty, new Thickness(0.5, 0.5, 0.5, 0.5), BindingPriority.Template);
			rootBorder4.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Right, BindingPriority.Template);
			rootBorder4.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			rootBorder4.SetValue(Border.BackgroundProperty, new ImmutableSolidColorBrush(16777215u), BindingPriority.Template);
			StyledProperty<IBrush?> borderBrushProperty2 = Border.BorderBrushProperty;
			DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TextPrimary");
			context.ProvideTargetProperty = Border.BorderBrushProperty;
			IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(rootBorder4, borderBrushProperty2, binding3);
			rootBorder4.SetValue(Border.CornerRadiusProperty, new CornerRadius(4.0, 4.0, 4.0, 4.0), BindingPriority.Template);
			rootBorder4.SetValue(Layoutable.WidthProperty, 16.0, BindingPriority.Template);
			rootBorder4.SetValue(Layoutable.HeightProperty, 16.0, BindingPriority.Template);
			Border border4;
			Border border3 = (border4 = new Border());
			((ISupportInitialize)border3).BeginInit();
			rootBorder4.Child = border3;
			Border border5 = (border = border4);
			context.PushParent(border);
			Border border6 = border;
			border6.Name = "CheckedBorder";
			obj2 = border6;
			context.AvaloniaNameScope.Register("CheckedBorder", obj2);
			border6.SetValue(Border.BorderThicknessProperty, new Thickness(6.0, 6.0, 6.0, 6.0), BindingPriority.Template);
			border6.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			border6.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
			DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("TextPrimary");
			context.ProvideTargetProperty = Border.BackgroundProperty;
			IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(border6, backgroundProperty2, binding4);
			border6.SetValue(Border.BorderBrushProperty, new ImmutableSolidColorBrush(16777215u), BindingPriority.Template);
			border6.SetValue(Border.CornerRadiusProperty, new CornerRadius(2.0, 2.0, 2.0, 2.0), BindingPriority.Template);
			border6.SetValue(Layoutable.WidthProperty, 16.0, BindingPriority.Template);
			border6.SetValue(Layoutable.HeightProperty, 16.0, BindingPriority.Template);
			context.PopParent();
			((ISupportInitialize)border5).EndInit();
			context.PopParent();
			((ISupportInitialize)rootBorder3).EndInit();
			context.PopParent();
			((ISupportInitialize)panel3).EndInit();
			context.PopParent();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<ScreensharePickerView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ScreensharePickerView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ScreensharePickerView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Home/VoiceBar/ScreensharePickerView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Home/VoiceBar/ScreensharePickerView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (ScreensharePickerView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ScreensharePickerView> context = CreateContext(P_0);
			context.IntermediateRoot = new ScreenshareView();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border HighlightBorder;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	public ScreensharePickerView()
	{
		InitializeComponent();
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	[ExcludeFromCodeCoverage]
	public void InitializeComponent(bool P_0 = true)
	{
		if (P_0)
		{
			!XamlIlPopulateTrampoline(this);
		}
		HighlightBorder = this.FindNameScope()?.Find<Border>("HighlightBorder");
	}

	[CompilerGenerated]
	private unsafe static void !XamlIlPopulate(IServiceProvider P_0, ScreensharePickerView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<ScreensharePickerView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ScreensharePickerView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Home/VoiceBar/ScreensharePickerView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Home/VoiceBar/ScreensharePickerView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		Styles styles = P_1.Styles;
		Style style2;
		Style style = (style2 = new Style());
		context.PushParent(style2);
		Style style3 = style2;
		style3.Selector = ((Selector?)null).OfType(typeof(RadioButton));
		Setter setter = new Setter();
		setter.Property = InputElement.CursorProperty;
		setter.Value = new Cursor(StandardCursorType.Hand);
		style3.Add(setter);
		Setter setter3;
		Setter setter2 = (setter3 = new Setter());
		context.PushParent(setter3);
		Setter setter4 = setter3;
		setter4.Property = TemplatedControl.TemplateProperty;
		ControlTemplate controlTemplate;
		ControlTemplate value = (controlTemplate = new ControlTemplate());
		context.PushParent(controlTemplate);
		controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_169.Build_1), context);
		context.PopParent();
		setter4.Value = value;
		context.PopParent();
		style3.Add(setter2);
		Style style4 = (style2 = new Style());
		context.PushParent(style2);
		Style style5 = style2;
		style5.Selector = ((Selector?)null).Nesting().Class(":checked").Template()
			.OfType(typeof(Border))
			.Name("HighlightBorder");
		Setter setter5 = (setter3 = new Setter());
		context.PushParent(setter3);
		Setter setter6 = setter3;
		setter6.Property = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Styling.Setter,Avalonia.Base.Value!Property();
		IBinding value2 = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter6.Value = value2;
		context.PopParent();
		style5.Add(setter5);
		Setter setter7 = new Setter();
		setter7.Property = Border.BorderThicknessProperty;
		setter7.Value = new Thickness(1.0, 1.0, 1.0, 1.0);
		style5.Add(setter7);
		Setter setter8 = (setter3 = new Setter());
		context.PushParent(setter3);
		Setter setter9 = setter3;
		setter9.Property = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Styling.Setter,Avalonia.Base.Value!Property();
		IBinding value3 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter9.Value = value3;
		context.PopParent();
		style5.Add(setter8);
		context.PopParent();
		style3.Add(style4);
		Style style6 = (style2 = new Style());
		context.PushParent(style2);
		Style style7 = style2;
		style7.Selector = ((Selector?)null).Nesting().Not(((Selector?)null).Class(":checked")).Class(":pointerover")
			.Template()
			.OfType(typeof(Border))
			.Name("HighlightBorder");
		Setter setter10 = (setter3 = new Setter());
		context.PushParent(setter3);
		Setter setter11 = setter3;
		setter11.Property = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Styling.Setter,Avalonia.Base.Value!Property();
		IBinding value4 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter11.Value = value4;
		context.PopParent();
		style7.Add(setter10);
		context.PopParent();
		style3.Add(style6);
		Style style8 = (style2 = new Style());
		context.PushParent(style2);
		Style style9 = style2;
		style9.Selector = ((Selector?)null).Nesting().Not(((Selector?)null).Class(":checked")).Class(":pointerover")
			.Template()
			.OfType(typeof(Border))
			.Name("CheckedBorder");
		Setter setter12 = new Setter();
		setter12.Property = Visual.IsVisibleProperty;
		setter12.Value = true;
		style9.Add(setter12);
		Setter setter13 = (setter3 = new Setter());
		context.PushParent(setter3);
		Setter setter14 = setter3;
		setter14.Property = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Styling.Setter,Avalonia.Base.Value!Property();
		IBinding value5 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter14.Value = value5;
		context.PopParent();
		style9.Add(setter13);
		context.PopParent();
		style3.Add(style8);
		Style style10 = new Style();
		style10.Selector = ((Selector?)null).Nesting().Template().OfType(typeof(Border))
			.Name("CheckedBorder");
		Setter setter15 = new Setter();
		setter15.Property = Visual.IsVisibleProperty;
		setter15.Value = false;
		style10.Add(setter15);
		style3.Add(style10);
		Style style11 = (style2 = new Style());
		context.PushParent(style2);
		Style style12 = style2;
		style12.Selector = ((Selector?)null).Nesting().Class(":checked").Template()
			.OfType(typeof(Border))
			.Name("CheckedBorder");
		Setter setter16 = new Setter();
		setter16.Property = Visual.IsVisibleProperty;
		setter16.Value = true;
		style12.Add(setter16);
		Setter setter17 = (setter3 = new Setter());
		context.PushParent(setter3);
		Setter setter18 = setter3;
		setter18.Property = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Styling.Setter,Avalonia.Base.Value!Property();
		IBinding value6 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter18.Value = value6;
		context.PopParent();
		style12.Add(setter17);
		context.PopParent();
		style3.Add(style11);
		context.PopParent();
		styles.Add(style);
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		P_1.Content = panel;
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		panel4.Width = 554.0;
		panel4.Height = 685.0;
		global::Avalonia.Controls.Controls children = panel4.Children;
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		children.Add(grid);
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
			Height = new GridLength(0.5, GridUnitType.Pixel)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		global::Avalonia.Controls.Controls children2 = grid5.Children;
		RootScrollViewer rootScrollViewer2;
		RootScrollViewer rootScrollViewer = (rootScrollViewer2 = new RootScrollViewer());
		((ISupportInitialize)rootScrollViewer).BeginInit();
		children2.Add(rootScrollViewer);
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
		global::Avalonia.Controls.Controls children3 = stackPanel5.Children;
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		children3.Add(rootBorder);
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		rootBorder4.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
		rootBorder4.VerticalAlignment = VerticalAlignment.Top;
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, backgroundProperty, binding);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding2 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, borderBrushProperty, binding2);
		rootBorder4.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder4.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		rootBorder4.Padding = new Thickness(24.0, 24.0, 24.0, 24.0);
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		rootBorder4.Child = textBlock;
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ScreenSharing;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock5, obj);
		textBlock5.FontWeight = FontWeight.Bold;
		textBlock5.FontSize = 14.0;
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding3 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding3);
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		global::Avalonia.Controls.Controls children4 = stackPanel5.Children;
		ItemsRepeater itemsRepeater2;
		ItemsRepeater itemsRepeater = (itemsRepeater2 = new ItemsRepeater());
		((ISupportInitialize)itemsRepeater).BeginInit();
		children4.Add(itemsRepeater);
		ItemsRepeater itemsRepeater4;
		ItemsRepeater itemsRepeater3 = (itemsRepeater4 = itemsRepeater2);
		context.PushParent(itemsRepeater4);
		itemsRepeater4.Margin = new Thickness(0.0, 10.0, 0.0, 0.0);
		DirectProperty<ItemsRepeater, IEnumerable?> itemsSourceProperty = ItemsRepeater.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.ScreensharePickerViewModel,RootApp.Client.Avalonia.Screens!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsRepeater.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsRepeater4, itemsSourceProperty, compiledBindingExtension2);
		UniformGridLayout uniformGridLayout = new UniformGridLayout();
		uniformGridLayout.MinColumnSpacing = 8.0;
		uniformGridLayout.MinRowSpacing = 8.0;
		uniformGridLayout.MinItemWidth = 162.0;
		uniformGridLayout.Orientation = Orientation.Horizontal;
		itemsRepeater4.Layout = uniformGridLayout;
		itemsRepeater4.ItemTemplate = new DataTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_169.Build_2), context)
		};
		context.PopParent();
		((ISupportInitialize)itemsRepeater3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootScrollViewer3).EndInit();
		global::Avalonia.Controls.Controls children5 = grid5.Children;
		Rectangle rectangle2;
		Rectangle rectangle = (rectangle2 = new Rectangle());
		((ISupportInitialize)rectangle).BeginInit();
		children5.Add(rectangle);
		Rectangle rectangle4;
		Rectangle rectangle3 = (rectangle4 = rectangle2);
		context.PushParent(rectangle4);
		Grid.SetRow(rectangle4, 1);
		rectangle4.Height = 0.5;
		StyledProperty<IBrush?> fillProperty = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding4 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle4, fillProperty, binding4);
		context.PopParent();
		((ISupportInitialize)rectangle3).EndInit();
		global::Avalonia.Controls.Controls children6 = grid5.Children;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		children6.Add(stackPanel6);
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		Grid.SetRow(stackPanel9, 2);
		stackPanel9.Margin = new Thickness(24.0, 24.0, 24.0, 24.0);
		global::Avalonia.Controls.Controls children7 = stackPanel9.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children7.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.StreamQuality;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock9, obj2);
		textBlock9.FontWeight = (FontWeight)450;
		textBlock9.FontSize = 13.0;
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding5 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding5);
		textBlock9.Margin = new Thickness(0.0, 0.0, 0.0, 10.0);
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		global::Avalonia.Controls.Controls children8 = stackPanel9.Children;
		RadioButton radioButton2;
		RadioButton radioButton = (radioButton2 = new RadioButton());
		((ISupportInitialize)radioButton).BeginInit();
		children8.Add(radioButton);
		RadioButton radioButton4;
		RadioButton radioButton3 = (radioButton4 = radioButton2);
		context.PushParent(radioButton4);
		RadioButton radioButton5 = radioButton4;
		radioButton5.GroupName = "StreamQuality";
		radioButton5.Margin = new Thickness(0.0, 0.0, 0.0, 10.0);
		StyledProperty<bool?> isCheckedProperty = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension obj3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.ScreensharePickerViewModel,RootApp.Client.Avalonia.GameMode!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension3 = obj3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(radioButton5, isCheckedProperty, compiledBindingExtension3);
		StackPanel stackPanel11;
		StackPanel stackPanel10 = (stackPanel11 = new StackPanel());
		((ISupportInitialize)stackPanel10).BeginInit();
		radioButton5.Content = stackPanel10;
		StackPanel stackPanel12 = (stackPanel4 = stackPanel11);
		context.PushParent(stackPanel4);
		StackPanel stackPanel13 = stackPanel4;
		global::Avalonia.Controls.Controls children9 = stackPanel13.Children;
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		children9.Add(textBlock10);
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		textBlock13.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.GameMode;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj4 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock13, obj4);
		textBlock13.FontWeight = FontWeight.Medium;
		textBlock13.FontSize = 14.0;
		textBlock13.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding6 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, foregroundProperty3, binding6);
		textBlock13.Margin = new Thickness(0.0, 0.0, 0.0, 4.0);
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		global::Avalonia.Controls.Controls children10 = stackPanel13.Children;
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		children10.Add(textBlock14);
		TextBlock textBlock16 = (textBlock4 = textBlock15);
		context.PushParent(textBlock4);
		TextBlock textBlock17 = textBlock4;
		textBlock17.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.GameModeDescription;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj5 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock17, obj5);
		textBlock17.FontWeight = FontWeight.Normal;
		textBlock17.FontSize = 14.0;
		textBlock17.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty4 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding7 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock17, foregroundProperty4, binding7);
		context.PopParent();
		((ISupportInitialize)textBlock16).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel12).EndInit();
		context.PopParent();
		((ISupportInitialize)radioButton3).EndInit();
		global::Avalonia.Controls.Controls children11 = stackPanel9.Children;
		RadioButton radioButton7;
		RadioButton radioButton6 = (radioButton7 = new RadioButton());
		((ISupportInitialize)radioButton6).BeginInit();
		children11.Add(radioButton6);
		RadioButton radioButton8 = (radioButton4 = radioButton7);
		context.PushParent(radioButton4);
		RadioButton radioButton9 = radioButton4;
		radioButton9.GroupName = "StreamQuality";
		StyledProperty<bool?> isCheckedProperty2 = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension obj6 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.ScreensharePickerViewModel,RootApp.Client.Avalonia.ScreenshareMode!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension4 = obj6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(radioButton9, isCheckedProperty2, compiledBindingExtension4);
		StackPanel stackPanel15;
		StackPanel stackPanel14 = (stackPanel15 = new StackPanel());
		((ISupportInitialize)stackPanel14).BeginInit();
		radioButton9.Content = stackPanel14;
		StackPanel stackPanel16 = (stackPanel4 = stackPanel15);
		context.PushParent(stackPanel4);
		StackPanel stackPanel17 = stackPanel4;
		global::Avalonia.Controls.Controls children12 = stackPanel17.Children;
		TextBlock textBlock19;
		TextBlock textBlock18 = (textBlock19 = new TextBlock());
		((ISupportInitialize)textBlock18).BeginInit();
		children12.Add(textBlock18);
		TextBlock textBlock20 = (textBlock4 = textBlock19);
		context.PushParent(textBlock4);
		TextBlock textBlock21 = textBlock4;
		textBlock21.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ScreenshareMode;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj7 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock21, obj7);
		textBlock21.FontWeight = FontWeight.Medium;
		textBlock21.FontSize = 14.0;
		textBlock21.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty5 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding8 = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock21, foregroundProperty5, binding8);
		textBlock21.Margin = new Thickness(0.0, 0.0, 0.0, 4.0);
		context.PopParent();
		((ISupportInitialize)textBlock20).EndInit();
		global::Avalonia.Controls.Controls children13 = stackPanel17.Children;
		TextBlock textBlock23;
		TextBlock textBlock22 = (textBlock23 = new TextBlock());
		((ISupportInitialize)textBlock22).BeginInit();
		children13.Add(textBlock22);
		TextBlock textBlock24 = (textBlock4 = textBlock23);
		context.PushParent(textBlock4);
		TextBlock textBlock25 = textBlock4;
		textBlock25.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ScreenshareModeDescription;
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj8 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock25, obj8);
		textBlock25.FontWeight = FontWeight.Normal;
		textBlock25.FontSize = 14.0;
		textBlock25.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty6 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding9 = dynamicResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock25, foregroundProperty6, binding9);
		context.PopParent();
		((ISupportInitialize)textBlock24).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel16).EndInit();
		context.PopParent();
		((ISupportInitialize)radioButton8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		global::Avalonia.Controls.Controls children14 = grid5.Children;
		StackPanel stackPanel19;
		StackPanel stackPanel18 = (stackPanel19 = new StackPanel());
		((ISupportInitialize)stackPanel18).BeginInit();
		children14.Add(stackPanel18);
		StackPanel stackPanel20 = (stackPanel4 = stackPanel19);
		context.PushParent(stackPanel4);
		StackPanel stackPanel21 = stackPanel4;
		Grid.SetRow(stackPanel21, 3);
		stackPanel21.Margin = new Thickness(24.0, 0.0, 24.0, 24.0);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.ScreensharePickerViewModel,RootApp.Client.Avalonia.CanShareAudio!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(stackPanel21, isVisibleProperty, compiledBindingExtension6);
		global::Avalonia.Controls.Controls children15 = stackPanel21.Children;
		TextBlock textBlock27;
		TextBlock textBlock26 = (textBlock27 = new TextBlock());
		((ISupportInitialize)textBlock26).BeginInit();
		children15.Add(textBlock26);
		TextBlock textBlock28 = (textBlock4 = textBlock27);
		context.PushParent(textBlock4);
		TextBlock textBlock29 = textBlock4;
		textBlock29.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Audio;
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj9 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock29, obj9);
		textBlock29.FontWeight = (FontWeight)450;
		textBlock29.FontSize = 13.0;
		textBlock29.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty7 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension15 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding10 = dynamicResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock29, foregroundProperty7, binding10);
		textBlock29.Margin = new Thickness(0.0, 0.0, 0.0, 10.0);
		context.PopParent();
		((ISupportInitialize)textBlock28).EndInit();
		global::Avalonia.Controls.Controls children16 = stackPanel21.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children16.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		border4.Name = "HighlightBorder";
		object obj10 = border4;
		context.AvaloniaNameScope.Register("HighlightBorder", obj10);
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension16 = new DynamicResourceExtension("HighlightLight");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding11 = dynamicResourceExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border4, backgroundProperty2, binding11);
		StyledProperty<IBrush?> borderBrushProperty2 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension17 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding12 = dynamicResourceExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border4, borderBrushProperty2, binding12);
		border4.BorderThickness = new Thickness(1.0, 1.0, 1.0, 1.0);
		border4.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		border4.Padding = new Thickness(12.0, 12.0, 12.0, 12.0);
		border4.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		border4.Child = grid6;
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		ColumnDefinitions columnDefinitions = new ColumnDefinitions();
		columnDefinitions.Capacity = 2;
		columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid9.ColumnDefinitions = columnDefinitions;
		global::Avalonia.Controls.Controls children17 = grid9.Children;
		TextBlock textBlock31;
		TextBlock textBlock30 = (textBlock31 = new TextBlock());
		((ISupportInitialize)textBlock30).BeginInit();
		children17.Add(textBlock30);
		TextBlock textBlock32 = (textBlock4 = textBlock31);
		context.PushParent(textBlock4);
		TextBlock textBlock33 = textBlock4;
		textBlock33.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ShareStreamAudio;
		StaticResourceExtension staticResourceExtension8 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj11 = staticResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock33, obj11);
		textBlock33.FontWeight = FontWeight.Medium;
		textBlock33.FontSize = 14.0;
		textBlock33.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty8 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension18 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding13 = dynamicResourceExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock33, foregroundProperty8, binding13);
		textBlock33.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock32).EndInit();
		global::Avalonia.Controls.Controls children18 = grid9.Children;
		CheckBox checkBox2;
		CheckBox checkBox = (checkBox2 = new CheckBox());
		((ISupportInitialize)checkBox).BeginInit();
		children18.Add(checkBox);
		CheckBox checkBox4;
		CheckBox checkBox3 = (checkBox4 = checkBox2);
		context.PushParent(checkBox4);
		Grid.SetColumn(checkBox4, 1);
		checkBox4.Classes.Add("ToggleSwitch");
		StyledProperty<bool?> isCheckedProperty3 = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension obj12 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.ScreensharePickerViewModel,RootApp.Client.Avalonia.ScreenshareAudio!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension7 = obj12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(checkBox4, isCheckedProperty3, compiledBindingExtension7);
		checkBox4.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)checkBox3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel20).EndInit();
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
	private static void !XamlIlPopulateTrampoline(ScreensharePickerView P_0)
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
