// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.MemberCheckBoxView
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Data.Converters;
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
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.UI.Community.Members;

public class MemberCheckBoxView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_77
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MemberCheckBoxView> context = CreateContext(P_0);
			context.IntermediateRoot = new Border();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			Border border = (Border)obj;
			context.PushParent(border);
			Border border2 = border;
			border2.Name = "ContainerBorder";
			object obj2 = border2;
			context.AvaloniaNameScope.Register("ContainerBorder", obj2);
			border2.SetValue(Decorator.PaddingProperty, new Thickness(10.0, 10.0, 10.0, 10.0), BindingPriority.Template);
			border2.SetValue(Border.CornerRadiusProperty, new CornerRadius(6.0, 6.0, 6.0, 6.0), BindingPriority.Template);
			border2.SetValue(Border.BackgroundProperty, new ImmutableSolidColorBrush(16777215u), BindingPriority.Template);
			border2.SetValue(InputElement.CursorProperty, new Cursor(StandardCursorType.Hand), BindingPriority.Template);
			Grid grid2;
			Grid grid = (grid2 = new Grid());
			((ISupportInitialize)grid).BeginInit();
			border2.Child = grid;
			Grid grid4;
			Grid grid3 = (grid4 = grid2);
			context.PushParent(grid4);
			ColumnDefinitions columnDefinitions = new ColumnDefinitions();
			columnDefinitions.Capacity = 2;
			columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
			columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
			grid4.ColumnDefinitions = columnDefinitions;
			Controls children = grid4.Children;
			ContentPresenter contentPresenter2;
			ContentPresenter contentPresenter = (contentPresenter2 = new ContentPresenter());
			((ISupportInitialize)contentPresenter).BeginInit();
			children.Add(contentPresenter);
			contentPresenter2.SetValue(Layoutable.MarginProperty, new Thickness(0.0, 0.0, 8.0, 0.0), BindingPriority.Template);
			AvaloniaObjectExtensions.Bind(contentPresenter2, Layoutable.HorizontalAlignmentProperty, new TemplateBinding(ContentControl.HorizontalContentAlignmentProperty).ProvideValue());
			AvaloniaObjectExtensions.Bind(contentPresenter2, Layoutable.VerticalAlignmentProperty, new TemplateBinding(ContentControl.VerticalContentAlignmentProperty).ProvideValue());
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_2(contentPresenter2, BindingPriority.Template, new TemplateBinding(ContentControl.ContentProperty).ProvideValue());
			AvaloniaObjectExtensions.Bind(contentPresenter2, ContentPresenter.ContentTemplateProperty, new TemplateBinding(ContentControl.ContentTemplateProperty).ProvideValue());
			AvaloniaObjectExtensions.Bind(contentPresenter2, Visual.IsVisibleProperty, new TemplateBinding(ContentControl.ContentProperty)
			{
				Converter = ObjectConverters.IsNotNull
			}.ProvideValue());
			contentPresenter2.SetValue(ContentPresenter.RecognizesAccessKeyProperty, value: true, BindingPriority.Template);
			AvaloniaObjectExtensions.Bind(contentPresenter2, TextElement.ForegroundProperty, new TemplateBinding(TemplatedControl.ForegroundProperty).ProvideValue());
			((ISupportInitialize)contentPresenter2).EndInit();
			Controls children2 = grid4.Children;
			Border border4;
			Border border3 = (border4 = new Border());
			((ISupportInitialize)border3).BeginInit();
			children2.Add(border3);
			Border border5 = (border = border4);
			context.PushParent(border);
			Border border6 = border;
			border6.SetValue(Grid.ColumnProperty, 1, BindingPriority.Template);
			border6.Name = "CheckContainerBorder";
			obj2 = border6;
			context.AvaloniaNameScope.Register("CheckContainerBorder", obj2);
			border6.SetValue(Layoutable.WidthProperty, 16.0, BindingPriority.Template);
			border6.SetValue(Layoutable.HeightProperty, 16.0, BindingPriority.Template);
			border6.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			AvaloniaObjectExtensions.Bind(border6, Border.BackgroundProperty, new TemplateBinding(TemplatedControl.BackgroundProperty).ProvideValue());
			AvaloniaObjectExtensions.Bind(border6, Border.BorderBrushProperty, new TemplateBinding(TemplatedControl.BorderBrushProperty).ProvideValue());
			AvaloniaObjectExtensions.Bind(border6, Border.BorderThicknessProperty, new TemplateBinding(TemplatedControl.BorderThicknessProperty).ProvideValue());
			border6.SetValue(Border.CornerRadiusProperty, new CornerRadius(4.0, 4.0, 4.0, 4.0), BindingPriority.Template);
			Border border8;
			Border border7 = (border8 = new Border());
			((ISupportInitialize)border7).BeginInit();
			border6.Child = border7;
			Border border9 = (border = border8);
			context.PushParent(border);
			Border border10 = border;
			border10.Name = "CheckedBorder";
			obj2 = border10;
			context.AvaloniaNameScope.Register("CheckedBorder", obj2);
			border10.SetValue(Layoutable.WidthProperty, 16.0, BindingPriority.Template);
			border10.SetValue(Layoutable.HeightProperty, 16.0, BindingPriority.Template);
			border10.SetValue(Border.CornerRadiusProperty, new CornerRadius(2.0, 2.0, 2.0, 2.0), BindingPriority.Template);
			border10.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			border10.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			border10.SetValue(Border.BorderThicknessProperty, new Thickness(6.0, 6.0, 6.0, 6.0), BindingPriority.Template);
			border10.SetValue(Border.BorderBrushProperty, new ImmutableSolidColorBrush(16777215u), BindingPriority.Template);
			StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
			DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
			context.ProvideTargetProperty = Border.BackgroundProperty;
			IBinding binding = dynamicResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(border10, backgroundProperty, binding);
			context.PopParent();
			((ISupportInitialize)border9).EndInit();
			context.PopParent();
			((ISupportInitialize)border5).EndInit();
			context.PopParent();
			((ISupportInitialize)grid3).EndInit();
			context.PopParent();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<MemberCheckBoxView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MemberCheckBoxView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MemberCheckBoxView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FCommunity_002FMembers_002FMemberCheckBoxView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Members/MemberCheckBoxView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (MemberCheckBoxView)service;
				}
			}
			return context;
		}
	}

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public MemberCheckBoxView()
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
	}

	[CompilerGenerated]
	private unsafe static void _0021XamlIlPopulate(IServiceProvider P_0, MemberCheckBoxView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<MemberCheckBoxView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MemberCheckBoxView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FCommunity_002FMembers_002FMemberCheckBoxView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Members/MemberCheckBoxView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.Margin = new Thickness(0.0, 2.0, 0.0, 2.0);
		Styles styles = P_1.Styles;
		Style style2;
		Style style = (style2 = new Style());
		context.PushParent(style2);
		Style style3 = style2;
		style3.Selector = ((Selector?)null).OfType(typeof(CheckBox));
		Setter setter2;
		Setter setter = (setter2 = new Setter());
		context.PushParent(setter2);
		Setter setter3 = setter2;
		setter3.Property = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		IBinding value = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter3.Value = value;
		context.PopParent();
		style3.Add(setter);
		Setter setter4 = new Setter();
		setter4.Property = TemplatedControl.BackgroundProperty;
		setter4.Value = new ImmutableSolidColorBrush(16777215u);
		style3.Add(setter4);
		Setter setter5 = (setter2 = new Setter());
		context.PushParent(setter2);
		Setter setter6 = setter2;
		setter6.Property = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		IBinding value2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter6.Value = value2;
		context.PopParent();
		style3.Add(setter5);
		Setter setter7 = new Setter();
		setter7.Property = TemplatedControl.BorderThicknessProperty;
		setter7.Value = new Thickness(1.0, 1.0, 1.0, 1.0);
		style3.Add(setter7);
		Setter setter8 = new Setter();
		setter8.Property = ContentControl.VerticalContentAlignmentProperty;
		setter8.Value = VerticalAlignment.Center;
		style3.Add(setter8);
		Setter setter9 = new Setter();
		setter9.Property = ContentControl.HorizontalContentAlignmentProperty;
		setter9.Value = HorizontalAlignment.Left;
		style3.Add(setter9);
		Setter setter10 = (setter2 = new Setter());
		context.PushParent(setter2);
		Setter setter11 = setter2;
		setter11.Property = TemplatedControl.TemplateProperty;
		ControlTemplate controlTemplate;
		ControlTemplate value3 = (controlTemplate = new ControlTemplate());
		context.PushParent(controlTemplate);
		controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_77.Build_1), context);
		context.PopParent();
		setter11.Value = value3;
		context.PopParent();
		style3.Add(setter10);
		Style style4 = (style2 = new Style());
		context.PushParent(style2);
		Style style5 = style2;
		style5.Selector = ((Selector?)null).Nesting().Class(":pointerover").Template()
			.OfType(typeof(Border))
			.Name("ContainerBorder");
		Setter setter12 = (setter2 = new Setter());
		context.PushParent(setter2);
		Setter setter13 = setter2;
		setter13.Property = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		IBinding value4 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter13.Value = value4;
		context.PopParent();
		style5.Add(setter12);
		context.PopParent();
		style3.Add(style4);
		Style style6 = new Style();
		style6.Selector = ((Selector?)null).Nesting().Class(":pressed").Template()
			.OfType(typeof(Border))
			.Name("ContainerBorder");
		Setter setter14 = new Setter();
		setter14.Property = Visual.OpacityProperty;
		setter14.Value = 0.7;
		style6.Add(setter14);
		style3.Add(style6);
		Style style7 = new Style();
		style7.Selector = ((Selector?)null).Nesting().Template().OfType(typeof(Border))
			.Name("CheckedBorder");
		Setter setter15 = new Setter();
		setter15.Property = Visual.IsVisibleProperty;
		setter15.Value = false;
		style7.Add(setter15);
		style3.Add(style7);
		Style style8 = new Style();
		style8.Selector = ((Selector?)null).Nesting().Class(":checked").Template()
			.OfType(typeof(Border))
			.Name("CheckedBorder");
		Setter setter16 = new Setter();
		setter16.Property = Visual.IsVisibleProperty;
		setter16.Value = true;
		style8.Add(setter16);
		style3.Add(style8);
		Style style9 = (style2 = new Style());
		context.PushParent(style2);
		Style style10 = style2;
		style10.Selector = ((Selector?)null).Nesting().Class(":disabled").Template()
			.OfType(typeof(Border))
			.Name("ContainerBorder");
		Setter setter17 = (setter2 = new Setter());
		context.PushParent(setter2);
		Setter setter18 = setter2;
		setter18.Property = Visual.OpacityProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("ThemeDisabledOpacity");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		IBinding value5 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter18.Value = value5;
		context.PopParent();
		style10.Add(setter17);
		context.PopParent();
		style3.Add(style9);
		context.PopParent();
		styles.Add(style);
		CheckBox checkBox2;
		CheckBox checkBox = (checkBox2 = new CheckBox());
		((ISupportInitialize)checkBox).BeginInit();
		P_1.Content = checkBox;
		CheckBox checkBox4;
		CheckBox checkBox3 = (checkBox4 = checkBox2);
		context.PushParent(checkBox4);
		checkBox4.MinWidth = 100.0;
		StyledProperty<bool?> isCheckedProperty = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension obj = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberCheckBoxViewModel_002CRootApp_002EClient_002EAvalonia_002EIsChecked_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension = obj.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(checkBox4, isCheckedProperty, compiledBindingExtension);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberCheckBoxViewModel_002CRootApp_002EClient_002EAvalonia_002ESetMemberCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension3 = compiledBindingExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(checkBox4, commandProperty, compiledBindingExtension3);
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		checkBox4.Content = stackPanel;
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		stackPanel4.Orientation = Orientation.Horizontal;
		stackPanel4.Background = new ImmutableSolidColorBrush(16777215u);
		Controls children = stackPanel4.Children;
		RootImageLoader rootImageLoader2;
		RootImageLoader rootImageLoader = (rootImageLoader2 = new RootImageLoader());
		((ISupportInitialize)rootImageLoader).BeginInit();
		children.Add(rootImageLoader);
		RootImageLoader rootImageLoader4;
		RootImageLoader rootImageLoader3 = (rootImageLoader4 = rootImageLoader2);
		context.PushParent(rootImageLoader4);
		rootImageLoader4.Width = 18.0;
		rootImageLoader4.Height = 18.0;
		rootImageLoader4.Margin = new Thickness(0.0, 0.0, 8.0, 0.0);
		rootImageLoader4.CornerRadius = new CornerRadius(4.0, 4.0, 4.0, 4.0);
		StyledProperty<IBrush?> backgroundProperty = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, backgroundProperty, binding);
		StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberCheckBoxViewModel_002CRootApp_002EClient_002EAvalonia_002EProfilePictureAsyncBitmapWrapper_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
		context.ProvideTargetProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension5 = compiledBindingExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, sourceProperty, compiledBindingExtension5);
		rootImageLoader4.LoadingPlaceholderSize = 16.0;
		rootImageLoader4.Stretch = Stretch.UniformToFill;
		context.PopParent();
		((ISupportInitialize)rootImageLoader3).EndInit();
		Controls children2 = stackPanel4.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children2.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock4, obj2);
		textBlock4.FontSize = 14.0;
		textBlock4.FontWeight = (FontWeight)450;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding2 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock4, foregroundProperty, binding2);
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension6 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberCheckBoxViewModel_002CRootApp_002EClient_002EAvalonia_002EMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002EMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EUserName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension7 = compiledBindingExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock4, textProperty, compiledBindingExtension7);
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)checkBox3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(MemberCheckBoxView P_0)
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

