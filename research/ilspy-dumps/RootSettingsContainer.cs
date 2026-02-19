// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.Settings.RootSettingsContainer
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
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
using Avalonia.Media.Immutable;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.Input;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Controls.Settings;
using RootApp.Client.Avalonia.Resources.Converters.Settings;
using RootApp.Client.Avalonia.Resources.Enums;

public class RootSettingsContainer : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_18
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<RootSettingsContainer> context = CreateContext(P_0);
			return new MenuItemFontWeightConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<RootSettingsContainer> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<RootSettingsContainer> context = new CompiledAvaloniaXaml.XamlIlContext.Context<RootSettingsContainer>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FControls_002FSettings_002FRootSettingsContainer_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Controls/Settings/RootSettingsContainer.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (RootSettingsContainer)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<RootSettingsContainer> context = CreateContext(P_0);
			return new SaveChangesMarginConverter();
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<RootSettingsContainer> context = CreateContext(P_0);
			context.IntermediateRoot = new Panel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((AvaloniaObject)obj).SetValue(InputElement.CursorProperty, new Cursor(StandardCursorType.Hand), BindingPriority.Template);
			((AvaloniaObject)obj).SetValue(Layoutable.MarginProperty, new Thickness(0.0, 2.0, 0.0, 2.0), BindingPriority.Template);
			Controls children = ((Panel)obj).Children;
			ContentPresenter contentPresenter2;
			ContentPresenter contentPresenter = (contentPresenter2 = new ContentPresenter());
			((ISupportInitialize)contentPresenter).BeginInit();
			children.Add(contentPresenter);
			contentPresenter2.Name = "PART_ContentPresenter";
			object obj2 = contentPresenter2;
			context.AvaloniaNameScope.Register("PART_ContentPresenter", obj2);
			AvaloniaObjectExtensions.Bind(contentPresenter2, ContentPresenter.HorizontalContentAlignmentProperty, new TemplateBinding(ContentControl.HorizontalContentAlignmentProperty).ProvideValue());
			AvaloniaObjectExtensions.Bind(contentPresenter2, ContentPresenter.VerticalContentAlignmentProperty, new TemplateBinding(ContentControl.VerticalContentAlignmentProperty).ProvideValue());
			AvaloniaObjectExtensions.Bind(contentPresenter2, ContentPresenter.BackgroundProperty, new TemplateBinding(TemplatedControl.BackgroundProperty).ProvideValue());
			AvaloniaObjectExtensions.Bind(contentPresenter2, ContentPresenter.BorderBrushProperty, new TemplateBinding(TemplatedControl.BorderBrushProperty).ProvideValue());
			AvaloniaObjectExtensions.Bind(contentPresenter2, ContentPresenter.BorderThicknessProperty, new TemplateBinding(TemplatedControl.BorderThicknessProperty).ProvideValue());
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_2(contentPresenter2, BindingPriority.Template, new TemplateBinding(ContentControl.ContentProperty).ProvideValue());
			AvaloniaObjectExtensions.Bind(contentPresenter2, ContentPresenter.ContentTemplateProperty, new TemplateBinding(ContentControl.ContentTemplateProperty).ProvideValue());
			AvaloniaObjectExtensions.Bind(contentPresenter2, ContentPresenter.CornerRadiusProperty, new TemplateBinding(TemplatedControl.CornerRadiusProperty).ProvideValue());
			((ISupportInitialize)contentPresenter2).EndInit();
			Controls children2 = ((Panel)obj).Children;
			Border border2;
			Border border = (border2 = new Border());
			((ISupportInitialize)border).BeginInit();
			children2.Add(border);
			border2.SetValue(Border.BackgroundProperty, new ImmutableSolidColorBrush(16777215u), BindingPriority.Template);
			border2.SetValue(Layoutable.HeightProperty, 36.0, BindingPriority.Template);
			border2.SetValue(Border.CornerRadiusProperty, new CornerRadius(12.0, 12.0, 12.0, 12.0), BindingPriority.Template);
			((ISupportInitialize)border2).EndInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}
	}

	public static readonly StyledProperty<double> MenuWidthProperty = AvaloniaProperty.Register<RootSettingsContainer, double>("MenuWidth", double.NaN);

	public static readonly StyledProperty<ObservableCollection<MenuItemPageContainerViewModel>> MenuItemPageContainersProperty = AvaloniaProperty.Register<RootSettingsContainer, ObservableCollection<MenuItemPageContainerViewModel>>("MenuItemPageContainers", new ObservableCollection<MenuItemPageContainerViewModel>());

	public static readonly StyledProperty<MenuItemPageContainerViewModel> SelectedMenuItemPageContainerProperty = AvaloniaProperty.Register<RootSettingsContainer, MenuItemPageContainerViewModel>("SelectedMenuItemPageContainer");

	public static readonly StyledProperty<ICommand?> CloseViewModelCommandProperty = AvaloniaProperty.Register<RootSettingsContainer, ICommand>("CloseViewModelCommand", null, false, BindingMode.OneWay, null, null, true);

	public static readonly StyledProperty<WebApiStatus> WebApiStatusProperty = AvaloniaProperty.Register<RootWebApiStatus, WebApiStatus>("WebApiStatus", WebApiStatus.Default);

	public static readonly StyledProperty<ContentControl> ListHeaderProperty = AvaloniaProperty.Register<RootSettingsContainer, ContentControl>("ListHeader");

	public static readonly StyledProperty<ContentControl> ListFooterProperty = AvaloniaProperty.Register<RootSettingsContainer, ContentControl>("ListFooter");

	public static readonly StyledProperty<ContentControl> SidePanelFooterProperty = AvaloniaProperty.Register<RootSettingsContainer, ContentControl>("SidePanelFooter");

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal UserControl RootSettingsContainerUserControl;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal ListBox MainListBox;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public ContentControl ListHeader
	{
		set
		{
			SetValue(ListHeaderProperty, value2);
		}
	}

	public ContentControl ListFooter
	{
		set
		{
			SetValue(ListFooterProperty, value2);
		}
	}

	public ContentControl SidePanelFooter
	{
		set
		{
			SetValue(SidePanelFooterProperty, value2);
		}
	}

	public double MenuWidth
	{
		set
		{
			SetValue(MenuWidthProperty, value2);
		}
	}

	public ObservableCollection<MenuItemPageContainerViewModel> MenuItemPageContainers => GetValue(MenuItemPageContainersProperty);

	public MenuItemPageContainerViewModel SelectedMenuItemPageContainer => GetValue(SelectedMenuItemPageContainerProperty);

	public RootSettingsContainer()
	{
		InitializeComponent();
	}

	protected override void OnLoaded(RoutedEventArgs P_0)
	{
		base.OnLoaded(P_0);
		MenuItemPageContainerViewModel menuItemPageContainerViewModel = MenuItemPageContainers.FirstOrDefault((MenuItemPageContainerViewModel item) => item.ForceInitialLoad);
		if (menuItemPageContainerViewModel != null)
		{
			MainListBox.SelectedItem = menuItemPageContainerViewModel;
			return;
		}
		MainListBox.SelectedItem = MainListBox.Items.OfType<MenuItemPageContainerViewModel>().FirstOrDefault((MenuItemPageContainerViewModel item) => !item.IsHeaderItem);
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		if (P_0.Property == SelectedMenuItemPageContainerProperty)
		{
			MenuItemPageContainerViewModel selectedMenuItemPageContainer = SelectedMenuItemPageContainer;
			if (selectedMenuItemPageContainer != null && selectedMenuItemPageContainer.Navigator.Count == 0)
			{
				SelectedMenuItemPageContainer.SelectMenuItem();
			}
		}
		base.OnPropertyChanged(P_0);
	}

	public void onBackButtonClicked(object sender, RoutedEventArgs args)
	{
		SelectedMenuItemPageContainer.Navigator.Pop();
	}

	[RelayCommand]
	public void SaveChangesCommand()
	{
		SelectedMenuItemPageContainer.Navigator.SaveChanges();
	}

	[RelayCommand]
	public void RevertChangesCommand()
	{
		SelectedMenuItemPageContainer.Navigator.RevertChanges();
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
		RootSettingsContainerUserControl = nameScope?.Find<UserControl>("RootSettingsContainerUserControl");
		MainListBox = nameScope?.Find<ListBox>("MainListBox");
	}

	[CompilerGenerated]
	private unsafe static void _0021XamlIlPopulate(IServiceProvider P_0, RootSettingsContainer P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<RootSettingsContainer> context = new CompiledAvaloniaXaml.XamlIlContext.Context<RootSettingsContainer>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FControls_002FSettings_002FRootSettingsContainer_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Controls/Settings/RootSettingsContainer.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		if (P_1.Resources is ResourceDictionary resourceDictionary)
		{
			resourceDictionary.EnsureCapacity(resourceDictionary.Count + 2);
		}
		P_1.Name = "RootSettingsContainerUserControl";
		object obj = P_1;
		context.AvaloniaNameScope.Register("RootSettingsContainerUserControl", obj);
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"MenuItemFontWeightConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_18.Build_1), context));
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"SaveChangesMarginConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_18.Build_2), context));
		Styles styles = P_1.Styles;
		Style style2;
		Style style = (style2 = new Style());
		context.PushParent(style2);
		Style style3 = style2;
		style3.Selector = ((Selector?)null).OfType(typeof(ListBox)).Class("SettingsContainerListBox").Descendant()
			.OfType(typeof(ListBoxItem));
		Setter setter2;
		Setter setter = (setter2 = new Setter());
		context.PushParent(setter2);
		Setter setter3 = setter2;
		setter3.Property = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension2;
		CompiledBindingExtension compiledBindingExtension = (compiledBindingExtension2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EControls_002ESettings_002EMenuItemPageContainerViewModel_002CRootApp_002EClient_002EAvalonia_002EIsHeaderItem_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension2);
		CompiledBindingExtension compiledBindingExtension3 = compiledBindingExtension2;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("BoolInverterConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj2 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension3.Converter = (IValueConverter)obj2;
		context.PopParent();
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		CompiledBindingExtension value = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter3.Value = value;
		context.PopParent();
		style3.Add(setter);
		Setter setter4 = (setter2 = new Setter());
		context.PushParent(setter2);
		Setter setter5 = setter2;
		setter5.Property = TemplatedControl.FontWeightProperty;
		CompiledBindingExtension compiledBindingExtension4 = (compiledBindingExtension2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EControls_002ESettings_002EMenuItemPageContainerViewModel_002CRootApp_002EClient_002EAvalonia_002EIsHeaderItem_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension2);
		CompiledBindingExtension compiledBindingExtension5 = compiledBindingExtension2;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("MenuItemFontWeightConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj3 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension5.Converter = (IValueConverter)obj3;
		context.PopParent();
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		CompiledBindingExtension value2 = compiledBindingExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter5.Value = value2;
		context.PopParent();
		style3.Add(setter4);
		Setter setter6 = new Setter();
		setter6.Property = TemplatedControl.BackgroundProperty;
		setter6.Value = new ImmutableSolidColorBrush(16777215u);
		style3.Add(setter6);
		Setter setter7 = new Setter();
		setter7.Property = TemplatedControl.BorderBrushProperty;
		setter7.Value = new ImmutableSolidColorBrush(16777215u);
		style3.Add(setter7);
		Setter setter8 = new Setter();
		setter8.Property = TemplatedControl.BorderThicknessProperty;
		setter8.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
		style3.Add(setter8);
		Setter setter9 = new Setter();
		setter9.Property = TemplatedControl.TemplateProperty;
		setter9.Value = new ControlTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_18.Build_3), context)
		};
		style3.Add(setter9);
		context.PopParent();
		styles.Add(style);
		Styles styles2 = P_1.Styles;
		Style style4 = (style2 = new Style());
		context.PushParent(style2);
		Style style5 = style2;
		style5.Selector = ((Selector?)null).OfType(typeof(ListBoxItem)).Class(":pointerover").Template()
			.OfType(typeof(Border));
		Setter setter10 = (setter2 = new Setter());
		context.PushParent(setter2);
		Setter setter11 = setter2;
		setter11.Property = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("HighlightLight");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		IBinding value3 = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter11.Value = value3;
		context.PopParent();
		style5.Add(setter10);
		context.PopParent();
		styles2.Add(style4);
		Styles styles3 = P_1.Styles;
		Style style6 = (style2 = new Style());
		context.PushParent(style2);
		Style style7 = style2;
		style7.Selector = ((Selector?)null).OfType(typeof(ListBoxItem)).Class(":selected").Template()
			.OfType(typeof(Border));
		Setter setter12 = (setter2 = new Setter());
		context.PushParent(setter2);
		Setter setter13 = setter2;
		setter13.Property = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EStyling_002ESetter_002CAvalonia_002EBase_002EValue_0021Property();
		IBinding value4 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter13.Value = value4;
		context.PopParent();
		style7.Add(setter12);
		context.PopParent();
		styles3.Add(style6);
		Styles styles4 = P_1.Styles;
		Style style8 = new Style();
		style8.Selector = ((Selector?)null).OfType(typeof(ListBoxItem)).Class(":pointerover").Template()
			.OfType(typeof(ContentPresenter));
		Setter setter14 = new Setter();
		setter14.Property = ContentPresenter.BackgroundProperty;
		setter14.Value = new ImmutableSolidColorBrush(16777215u);
		style8.Add(setter14);
		styles4.Add(style8);
		Styles styles5 = P_1.Styles;
		Style style9 = new Style();
		style9.Selector = ((Selector?)null).OfType(typeof(ListBoxItem)).Class(":selected").Template()
			.OfType(typeof(ContentPresenter));
		Setter setter15 = new Setter();
		setter15.Property = ContentPresenter.BackgroundProperty;
		setter15.Value = new ImmutableSolidColorBrush(16777215u);
		style9.Add(setter15);
		styles5.Add(style9);
		Styles styles6 = P_1.Styles;
		Style style10 = new Style();
		style10.Selector = ((Selector?)null).OfType(typeof(ListBoxItem)).Class(":selected").Class(":focus")
			.Template()
			.OfType(typeof(ContentPresenter));
		Setter setter16 = new Setter();
		setter16.Property = ContentPresenter.BackgroundProperty;
		setter16.Value = new ImmutableSolidColorBrush(16777215u);
		style10.Add(setter16);
		styles6.Add(style10);
		Styles styles7 = P_1.Styles;
		Style style11 = new Style();
		style11.Selector = ((Selector?)null).OfType(typeof(ListBoxItem)).Class(":selected").Class(":pointerover")
			.Template()
			.OfType(typeof(ContentPresenter));
		Setter setter17 = new Setter();
		setter17.Property = ContentPresenter.BackgroundProperty;
		setter17.Value = new ImmutableSolidColorBrush(16777215u);
		style11.Add(setter17);
		styles7.Add(style11);
		Styles styles8 = P_1.Styles;
		Style style12 = new Style();
		style12.Selector = ((Selector?)null).OfType(typeof(ListBoxItem)).Class(":selected").Class(":focus")
			.Class(":pointerover")
			.Template()
			.OfType(typeof(ContentPresenter));
		Setter setter18 = new Setter();
		setter18.Property = ContentPresenter.BackgroundProperty;
		setter18.Value = new ImmutableSolidColorBrush(16777215u);
		style12.Add(setter18);
		styles8.Add(style12);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		P_1.Content = grid;
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
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		ColumnDefinitions columnDefinitions = grid5.ColumnDefinitions;
		ColumnDefinition columnDefinition = new ColumnDefinition();
		columnDefinition.Width = new GridLength(20.0, GridUnitType.Star);
		columnDefinition.MaxWidth = 900.0;
		columnDefinitions.Add(columnDefinition);
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(85.0, GridUnitType.Pixel)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		Controls children = grid5.Children;
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		children.Add(panel);
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		Panel panel5 = panel4;
		Grid.SetColumn(panel5, 0);
		Grid.SetRowSpan(panel5, 3);
		StyledProperty<IBrush?> backgroundProperty = Panel.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = Panel.BackgroundProperty;
		IBinding binding = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(panel5, backgroundProperty, binding);
		context.PopParent();
		((ISupportInitialize)panel3).EndInit();
		Controls children2 = grid5.Children;
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		children2.Add(grid6);
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		Grid.SetColumn(grid9, 2);
		Grid.SetRow(grid9, 0);
		ColumnDefinitions columnDefinitions2 = new ColumnDefinitions();
		columnDefinitions2.Capacity = 3;
		columnDefinitions2.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		columnDefinitions2.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		columnDefinitions2.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid9.ColumnDefinitions = columnDefinitions2;
		Controls children3 = grid9.Children;
		RootSvgButton rootSvgButton2;
		RootSvgButton rootSvgButton = (rootSvgButton2 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton).BeginInit();
		children3.Add(rootSvgButton);
		RootSvgButton rootSvgButton4;
		RootSvgButton rootSvgButton3 = (rootSvgButton4 = rootSvgButton2);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton5 = rootSvgButton4;
		StyledProperty<string> svgPathProperty = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("DownArrowSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding2 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton5, svgPathProperty, binding2);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension6 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSettingsContainerUserControl").Property(SelectedMenuItemPageContainerProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EControls_002ESettings_002EMenuItemPageContainerViewModel_002CRootApp_002EClient_002EAvalonia_002ENavigator_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EHelpers_002ENavigation_002ENavigator_002CRootApp_002EClient_002EAvalonia_002ECanGoBack_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension7 = compiledBindingExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton5, isVisibleProperty, compiledBindingExtension7);
		rootSvgButton5.Classes.Add("Custom");
		rootSvgButton5.Width = 40.0;
		rootSvgButton5.Height = 40.0;
		rootSvgButton5.SvgWidth = 12.31;
		rootSvgButton5.SvgHeight = 8.49;
		StyledProperty<IBrush?> borderBrushProperty = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding3 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton5, borderBrushProperty, binding3);
		rootSvgButton5.BorderThickness = new Thickness(1.0, 1.0, 1.0, 1.0);
		rootSvgButton5.CornerRadius = new CornerRadius(20.0, 20.0, 20.0, 20.0);
		rootSvgButton5.Margin = new Thickness(24.0, 0.0, 0.0, 0.0);
		rootSvgButton5.HorizontalAlignment = HorizontalAlignment.Center;
		rootSvgButton5.VerticalAlignment = VerticalAlignment.Center;
		rootSvgButton5.AddHandler((RoutedEvent)Button.ClickEvent, (Delegate)new EventHandler<RoutedEventArgs>(context.RootObject.onBackButtonClicked), RoutingStrategies.Direct | RoutingStrategies.Bubble, false);
		rootSvgButton5.RenderTransform = new RotateTransform
		{
			Angle = 90.0
		};
		context.PopParent();
		((ISupportInitialize)rootSvgButton3).EndInit();
		Controls children4 = grid9.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children4.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		Grid.SetColumn(textBlock4, 1);
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension obj4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSettingsContainerUserControl").Property(SelectedMenuItemPageContainerProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EControls_002ESettings_002EMenuItemPageContainerViewModel_002CRootApp_002EClient_002EAvalonia_002ENavigator_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EHelpers_002ENavigation_002ENavigator_002CRootApp_002EClient_002EAvalonia_002ECurrentViewModel_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.TypeCast<IPage>()
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EControls_002ESettings_002EIPage_002CRootApp_002EClient_002EAvalonia_002EPageTitle_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = null
		};
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension8 = obj4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock4, textProperty, compiledBindingExtension8);
		textBlock4.Margin = new Thickness(24.0, 0.0, 24.0, 0.0);
		textBlock4.VerticalAlignment = VerticalAlignment.Center;
		textBlock4.HorizontalAlignment = HorizontalAlignment.Left;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj5 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock4, obj5);
		textBlock4.FontWeight = FontWeight.Medium;
		textBlock4.FontSize = 24.0;
		textBlock4.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock4.TextTrimming = TextTrimming.CharacterEllipsis;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding4 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock4, foregroundProperty, binding4);
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		Controls children5 = grid9.Children;
		RootSvgButton rootSvgButton7;
		RootSvgButton rootSvgButton6 = (rootSvgButton7 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton6).BeginInit();
		children5.Add(rootSvgButton6);
		RootSvgButton rootSvgButton8 = (rootSvgButton4 = rootSvgButton7);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton9 = rootSvgButton4;
		Grid.SetColumn(rootSvgButton9, 2);
		StyledProperty<string> svgPathProperty2 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("ExitThickSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding5 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, svgPathProperty2, binding5);
		rootSvgButton9.Classes.Add("Custom");
		rootSvgButton9.Width = 40.0;
		rootSvgButton9.Height = 40.0;
		rootSvgButton9.SvgWidth = 13.18;
		rootSvgButton9.SvgHeight = 13.18;
		StyledProperty<IBrush?> backgroundProperty2 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding6 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, backgroundProperty2, binding6);
		rootSvgButton9.CornerRadius = new CornerRadius(20.0, 20.0, 20.0, 20.0);
		rootSvgButton9.Margin = new Thickness(0.0, 0.0, 24.0, 0.0);
		rootSvgButton9.HorizontalAlignment = HorizontalAlignment.Right;
		rootSvgButton9.VerticalAlignment = VerticalAlignment.Center;
		rootSvgButton9.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSettingsContainerUserControl").Property(CloseViewModelCommandProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, commandProperty, compiledBindingExtension10);
		context.PopParent();
		((ISupportInitialize)rootSvgButton8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		Controls children6 = grid5.Children;
		Panel panel7;
		Panel panel6 = (panel7 = new Panel());
		((ISupportInitialize)panel6).BeginInit();
		children6.Add(panel6);
		Panel panel8 = (panel4 = panel7);
		context.PushParent(panel4);
		Panel panel9 = panel4;
		Grid.SetColumn(panel9, 1);
		Grid.SetRow(panel9, 0);
		StyledProperty<IBrush?> backgroundProperty3 = Panel.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = Panel.BackgroundProperty;
		IBinding binding7 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(panel9, backgroundProperty3, binding7);
		Controls children7 = panel9.Children;
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		children7.Add(contentControl);
		ContentControl contentControl4;
		ContentControl contentControl3 = (contentControl4 = contentControl2);
		context.PushParent(contentControl4);
		ContentControl contentControl5 = contentControl4;
		contentControl5.Margin = new Thickness(24.0, 0.0, 24.0, 0.0);
		contentControl5.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj6 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSettingsContainerUserControl").Property(ListHeaderProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build())
		{
			Converter = ObjectConverters.IsNotNull
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension11 = obj6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(contentControl5, isVisibleProperty2, compiledBindingExtension11);
		CompiledBindingExtension compiledBindingExtension12 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSettingsContainerUserControl").Property(ListHeaderProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension13 = compiledBindingExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_4(contentControl5, compiledBindingExtension13);
		context.PopParent();
		((ISupportInitialize)contentControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)panel8).EndInit();
		Controls children8 = grid5.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children8.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Border border5 = border4;
		Grid.SetColumn(border5, 1);
		Grid.SetRow(border5, 0);
		border5.Height = 0.5;
		border5.Margin = new Thickness(24.0, 0.0, 24.0, 0.0);
		border5.VerticalAlignment = VerticalAlignment.Bottom;
		StyledProperty<IBrush?> backgroundProperty4 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding8 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, backgroundProperty4, binding8);
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		Controls children9 = grid5.Children;
		Border border7;
		Border border6 = (border7 = new Border());
		((ISupportInitialize)border6).BeginInit();
		children9.Add(border6);
		Border border8 = (border4 = border7);
		context.PushParent(border4);
		Border border9 = border4;
		Grid.SetColumn(border9, 1);
		Grid.SetRow(border9, 1);
		StyledProperty<IBrush?> backgroundProperty5 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding9 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border9, backgroundProperty5, binding9);
		Grid grid11;
		Grid grid10 = (grid11 = new Grid());
		((ISupportInitialize)grid10).BeginInit();
		border9.Child = grid10;
		Grid grid12 = (grid4 = grid11);
		context.PushParent(grid4);
		Grid grid13 = grid4;
		RowDefinitions rowDefinitions = new RowDefinitions();
		rowDefinitions.Capacity = 2;
		rowDefinitions.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
		rowDefinitions.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid13.RowDefinitions = rowDefinitions;
		Controls children10 = grid13.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children10.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		stackPanel4.Margin = new Thickness(12.0, 0.0, 24.0, 0.0);
		Controls children11 = stackPanel4.Children;
		ListBox listBox2;
		ListBox listBox = (listBox2 = new ListBox());
		((ISupportInitialize)listBox).BeginInit();
		children11.Add(listBox);
		ListBox listBox4;
		ListBox listBox3 = (listBox4 = listBox2);
		context.PushParent(listBox4);
		listBox4.Name = "MainListBox";
		obj = listBox4;
		context.AvaloniaNameScope.Register("MainListBox", obj);
		listBox4.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		listBox4.HorizontalAlignment = HorizontalAlignment.Right;
		listBox4.Classes.Add("SettingsContainerListBox");
		StyledProperty<double> widthProperty = Layoutable.WidthProperty;
		CompiledBindingExtension compiledBindingExtension14 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSettingsContainerUserControl").Property(MenuWidthProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = Layoutable.WidthProperty;
		CompiledBindingExtension compiledBindingExtension15 = compiledBindingExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(listBox4, widthProperty, compiledBindingExtension15);
		listBox4.Background = new ImmutableSolidColorBrush(16777215u);
		StyledProperty<IEnumerable?> itemsSourceProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension16 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSettingsContainerUserControl").Property(MenuItemPageContainersProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension17 = compiledBindingExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(listBox4, itemsSourceProperty, compiledBindingExtension17);
		CompiledBindingExtension obj7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSettingsContainerUserControl").Property(SelectedMenuItemPageContainerProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = SelectingItemsControl.SelectedItemProperty;
		CompiledBindingExtension compiledBindingExtension18 = obj7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_7(listBox4, compiledBindingExtension18);
		context.PopParent();
		((ISupportInitialize)listBox3).EndInit();
		Controls children12 = stackPanel4.Children;
		Border border11;
		Border border10 = (border11 = new Border());
		((ISupportInitialize)border10).BeginInit();
		children12.Add(border10);
		Border border12 = (border4 = border11);
		context.PushParent(border4);
		Border border13 = border4;
		border13.Height = 0.5;
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj8 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSettingsContainerUserControl").Property(ListFooterProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build())
		{
			Converter = ObjectConverters.IsNotNull
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension19 = obj8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border13, isVisibleProperty3, compiledBindingExtension19);
		border13.Margin = new Thickness(12.0, 12.0, 0.0, 0.0);
		StyledProperty<IBrush?> backgroundProperty6 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding10 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border13, backgroundProperty6, binding10);
		context.PopParent();
		((ISupportInitialize)border12).EndInit();
		Controls children13 = stackPanel4.Children;
		ContentControl contentControl7;
		ContentControl contentControl6 = (contentControl7 = new ContentControl());
		((ISupportInitialize)contentControl6).BeginInit();
		children13.Add(contentControl6);
		ContentControl contentControl8 = (contentControl4 = contentControl7);
		context.PushParent(contentControl4);
		ContentControl contentControl9 = contentControl4;
		contentControl9.Margin = new Thickness(12.0, 24.0, 0.0, 24.0);
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSettingsContainerUserControl").Property(ListFooterProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build())
		{
			Converter = ObjectConverters.IsNotNull
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension20 = obj9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(contentControl9, isVisibleProperty4, compiledBindingExtension20);
		CompiledBindingExtension compiledBindingExtension21 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSettingsContainerUserControl").Property(ListFooterProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension22 = compiledBindingExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_4(contentControl9, compiledBindingExtension22);
		context.PopParent();
		((ISupportInitialize)contentControl8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		Controls children14 = grid13.Children;
		ContentControl contentControl11;
		ContentControl contentControl10 = (contentControl11 = new ContentControl());
		((ISupportInitialize)contentControl10).BeginInit();
		children14.Add(contentControl10);
		ContentControl contentControl12 = (contentControl4 = contentControl11);
		context.PushParent(contentControl4);
		ContentControl contentControl13 = contentControl4;
		Grid.SetRow(contentControl13, 1);
		contentControl13.Margin = new Thickness(12.0, 12.0, 24.0, 12.0);
		StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj10 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSettingsContainerUserControl").Property(SidePanelFooterProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build())
		{
			Converter = ObjectConverters.IsNotNull
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension23 = obj10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(contentControl13, isVisibleProperty5, compiledBindingExtension23);
		CompiledBindingExtension compiledBindingExtension24 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSettingsContainerUserControl").Property(SidePanelFooterProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension25 = compiledBindingExtension24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_4(contentControl13, compiledBindingExtension25);
		context.PopParent();
		((ISupportInitialize)contentControl12).EndInit();
		context.PopParent();
		((ISupportInitialize)grid12).EndInit();
		context.PopParent();
		((ISupportInitialize)border8).EndInit();
		Controls children15 = grid5.Children;
		Panel panel11;
		Panel panel10 = (panel11 = new Panel());
		((ISupportInitialize)panel10).BeginInit();
		children15.Add(panel10);
		Panel panel12 = (panel4 = panel11);
		context.PushParent(panel4);
		Panel panel13 = panel4;
		Grid.SetColumn(panel13, 2);
		Grid.SetRow(panel13, 1);
		Grid.SetRowSpan(panel13, 2);
		StyledProperty<Thickness> marginProperty = Layoutable.MarginProperty;
		CompiledBindingExtension compiledBindingExtension26 = (compiledBindingExtension2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSettingsContainerUserControl").Property(SelectedMenuItemPageContainerProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EControls_002ESettings_002EMenuItemPageContainerViewModel_002CRootApp_002EClient_002EAvalonia_002ENavigator_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EHelpers_002ENavigation_002ENavigator_002CRootApp_002EClient_002EAvalonia_002EHasPendingChanges_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build()));
		context.PushParent(compiledBindingExtension2);
		CompiledBindingExtension compiledBindingExtension27 = compiledBindingExtension2;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("SaveChangesMarginConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj11 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension27.Converter = (IValueConverter)obj11;
		context.PopParent();
		context.ProvideTargetProperty = Layoutable.MarginProperty;
		CompiledBindingExtension compiledBindingExtension28 = compiledBindingExtension26.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(panel13, marginProperty, compiledBindingExtension28);
		Controls children16 = panel13.Children;
		ContentControl contentControl15;
		ContentControl contentControl14 = (contentControl15 = new ContentControl());
		((ISupportInitialize)contentControl14).BeginInit();
		children16.Add(contentControl14);
		ContentControl contentControl16 = (contentControl4 = contentControl15);
		context.PushParent(contentControl4);
		ContentControl contentControl17 = contentControl4;
		CompiledBindingExtension compiledBindingExtension29 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSettingsContainerUserControl").Property(SelectedMenuItemPageContainerProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EControls_002ESettings_002EMenuItemPageContainerViewModel_002CRootApp_002EClient_002EAvalonia_002ENavigator_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EHelpers_002ENavigation_002ENavigator_002CRootApp_002EClient_002EAvalonia_002ECurrentViewModel_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension30 = compiledBindingExtension29.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_4(contentControl17, compiledBindingExtension30);
		context.PopParent();
		((ISupportInitialize)contentControl16).EndInit();
		context.PopParent();
		((ISupportInitialize)panel12).EndInit();
		Controls children17 = grid5.Children;
		Border border15;
		Border border14 = (border15 = new Border());
		((ISupportInitialize)border14).BeginInit();
		children17.Add(border14);
		Border border16 = (border4 = border15);
		context.PushParent(border4);
		Border border17 = border4;
		Grid.SetColumn(border17, 2);
		Grid.SetRow(border17, 0);
		border17.Height = 0.5;
		border17.VerticalAlignment = VerticalAlignment.Bottom;
		StyledProperty<IBrush?> backgroundProperty7 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding11 = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border17, backgroundProperty7, binding11);
		context.PopParent();
		((ISupportInitialize)border16).EndInit();
		Controls children18 = grid5.Children;
		SaveChangesView saveChangesView2;
		SaveChangesView saveChangesView = (saveChangesView2 = new SaveChangesView());
		((ISupportInitialize)saveChangesView).BeginInit();
		children18.Add(saveChangesView);
		SaveChangesView saveChangesView4;
		SaveChangesView saveChangesView3 = (saveChangesView4 = saveChangesView2);
		context.PushParent(saveChangesView4);
		Grid.SetColumn(saveChangesView4, 2);
		Grid.SetRow(saveChangesView4, 1);
		Grid.SetRowSpan(saveChangesView4, 2);
		saveChangesView4.Margin = new Thickness(0.0, 0.0, 0.0, 12.0);
		saveChangesView4.VerticalAlignment = VerticalAlignment.Bottom;
		StyledProperty<bool> isVisibleProperty6 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension31 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSettingsContainerUserControl").Property(SelectedMenuItemPageContainerProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EControls_002ESettings_002EMenuItemPageContainerViewModel_002CRootApp_002EClient_002EAvalonia_002ENavigator_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EHelpers_002ENavigation_002ENavigator_002CRootApp_002EClient_002EAvalonia_002EHasPendingChanges_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension32 = compiledBindingExtension31.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(saveChangesView4, isVisibleProperty6, compiledBindingExtension32);
		StyledProperty<bool> canSaveProperty = SaveChangesView.CanSaveProperty;
		CompiledBindingExtension compiledBindingExtension33 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSettingsContainerUserControl").Property(SelectedMenuItemPageContainerProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EControls_002ESettings_002EMenuItemPageContainerViewModel_002CRootApp_002EClient_002EAvalonia_002ENavigator_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EHelpers_002ENavigation_002ENavigator_002CRootApp_002EClient_002EAvalonia_002ECanSave_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = SaveChangesView.CanSaveProperty;
		CompiledBindingExtension compiledBindingExtension34 = compiledBindingExtension33.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(saveChangesView4, canSaveProperty, compiledBindingExtension34);
		StyledProperty<ICommand?> revertChangesCommandProperty = SaveChangesView.RevertChangesCommandProperty;
		CompiledBindingExtension compiledBindingExtension35 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSettingsContainerUserControl").Command("RevertChangesCommand", CompiledAvaloniaXaml.XamlIlTrampolines.RootApp_002EClient_002EAvalonia_003ARootApp_002EClient_002EAvalonia_002EControls_002ESettings_002ERootSettingsContainer_002BRevertChangesCommand_0_0021CommandExecuteTrampoline, null, null).Build());
		context.ProvideTargetProperty = SaveChangesView.RevertChangesCommandProperty;
		CompiledBindingExtension compiledBindingExtension36 = compiledBindingExtension35.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(saveChangesView4, revertChangesCommandProperty, compiledBindingExtension36);
		StyledProperty<ICommand?> saveChangesCommandProperty = SaveChangesView.SaveChangesCommandProperty;
		CompiledBindingExtension compiledBindingExtension37 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSettingsContainerUserControl").Command("SaveChangesCommand", CompiledAvaloniaXaml.XamlIlTrampolines.RootApp_002EClient_002EAvalonia_003ARootApp_002EClient_002EAvalonia_002EControls_002ESettings_002ERootSettingsContainer_002BSaveChangesCommand_0_0021CommandExecuteTrampoline, null, null).Build());
		context.ProvideTargetProperty = SaveChangesView.SaveChangesCommandProperty;
		CompiledBindingExtension compiledBindingExtension38 = compiledBindingExtension37.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(saveChangesView4, saveChangesCommandProperty, compiledBindingExtension38);
		StyledProperty<WebApiStatus> webApiStatusProperty = SaveChangesView.WebApiStatusProperty;
		CompiledBindingExtension compiledBindingExtension39 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootSettingsContainerUserControl").Property(SelectedMenuItemPageContainerProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EControls_002ESettings_002EMenuItemPageContainerViewModel_002CRootApp_002EClient_002EAvalonia_002ENavigator_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EHelpers_002ENavigation_002ENavigator_002CRootApp_002EClient_002EAvalonia_002EWebApiStatus_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = SaveChangesView.WebApiStatusProperty;
		CompiledBindingExtension compiledBindingExtension40 = compiledBindingExtension39.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(saveChangesView4, webApiStatusProperty, compiledBindingExtension40);
		context.PopParent();
		((ISupportInitialize)saveChangesView3).EndInit();
		Controls children19 = grid5.Children;
		Rectangle rectangle2;
		Rectangle rectangle = (rectangle2 = new Rectangle());
		((ISupportInitialize)rectangle).BeginInit();
		children19.Add(rectangle);
		Rectangle rectangle4;
		Rectangle rectangle3 = (rectangle4 = rectangle2);
		context.PushParent(rectangle4);
		Grid.SetColumn(rectangle4, 1);
		Grid.SetRow(rectangle4, 0);
		Grid.SetRowSpan(rectangle4, 3);
		rectangle4.Width = 0.5;
		StyledProperty<IBrush?> fillProperty = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding12 = dynamicResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle4, fillProperty, binding12);
		rectangle4.HorizontalAlignment = HorizontalAlignment.Right;
		context.PopParent();
		((ISupportInitialize)rectangle3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(RootSettingsContainer P_0)
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
