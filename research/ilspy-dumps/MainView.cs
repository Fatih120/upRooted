// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Main.MainView
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Threading;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.UI.Main;

public class MainView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_171
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MainView> context = CreateContext(P_0);
			context.IntermediateRoot = new Grid();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<MainView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MainView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MainView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FMain_002FMainView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Main/MainView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (MainView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MainView> context = CreateContext(P_0);
			context.IntermediateRoot = new ContentControl();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			ContentControl contentControl = (ContentControl)obj;
			context.PushParent(contentControl);
			Grid.SetRow(contentControl, 0);
			Grid.SetColumn(contentControl, 0);
			CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension();
			context.ProvideTargetProperty = ContentControl.ContentProperty;
			CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_4(contentControl, compiledBindingExtension2);
			context.PopParent();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}
	}

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	private MainViewModel _mainViewModel => (MainViewModel)base.DataContext;

	public MainView()
	{
		InitializeComponent();
	}

	protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnAttachedToVisualTree(P_0);
		if (_mainViewModel.ViewLoadedCommand.CanExecute(null))
		{
			_mainViewModel.ViewLoadedCommand.Execute(null);
		}
		_mainViewModel.PropertyChanged += onViewModelPropertyChanged;
	}

	protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnDetachedFromVisualTree(P_0);
		_mainViewModel.PropertyChanged -= onViewModelPropertyChanged;
	}

	private void onViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "IsPopupOpen" && FlyoutBase.GetAttachedFlyout(this) is RootFlyout rootFlyout)
			{
				rootFlyout.Placement = PlacementMode.Pointer;
				Popup flyoutPopup = rootFlyout.FlyoutPopup;
				if (_mainViewModel.IsPopupOpen)
				{
					FlyoutBase.ShowAttachedFlyout(this);
					flyoutPopup.MinHeight = (flyoutPopup.Host as PopupRoot).Bounds.Height;
					flyoutPopup.MaxHeight = (flyoutPopup.Host as PopupRoot).Bounds.Height;
				}
				else
				{
					flyoutPopup.MinHeight = 0.0;
					flyoutPopup.MaxHeight = double.PositiveInfinity;
				}
			}
		});
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
	private unsafe static void _0021XamlIlPopulate(IServiceProvider P_0, MainView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<MainView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MainView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FMain_002FMainView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Main/MainView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		RootFlyout rootFlyout2;
		RootFlyout rootFlyout = (rootFlyout2 = new RootFlyout());
		context.PushParent(rootFlyout2);
		rootFlyout2.LimitSizeToWindow = false;
		StyledProperty<bool> isPopupOpenProperty = RootFlyout.IsPopupOpenProperty;
		CompiledBindingExtension obj = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMain_002EMainViewModel_002CRootApp_002EClient_002EAvalonia_002EIsPopupOpen_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = RootFlyout.IsPopupOpenProperty;
		CompiledBindingExtension compiledBindingExtension = obj.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootFlyout2, isPopupOpenProperty, compiledBindingExtension);
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		rootFlyout2.Content = rootBorder;
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, backgroundProperty, binding);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, borderBrushProperty, binding2);
		rootBorder4.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder4.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		rootBorder4.Margin = new Thickness(12.0, 12.0, 12.0, 12.0);
		StyledProperty<BoxShadows> boxShadowProperty = Border.BoxShadowProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("PopupBoxShadow");
		context.ProvideTargetProperty = Border.BoxShadowProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, boxShadowProperty, binding3);
		RootScrollViewer rootScrollViewer2;
		RootScrollViewer rootScrollViewer = (rootScrollViewer2 = new RootScrollViewer());
		((ISupportInitialize)rootScrollViewer).BeginInit();
		rootBorder4.Child = rootScrollViewer;
		RootScrollViewer rootScrollViewer4;
		RootScrollViewer rootScrollViewer3 = (rootScrollViewer4 = rootScrollViewer2);
		context.PushParent(rootScrollViewer4);
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		rootScrollViewer4.Content = contentControl;
		ContentControl contentControl4;
		ContentControl contentControl3 = (contentControl4 = contentControl2);
		context.PushParent(contentControl4);
		CompiledBindingExtension compiledBindingExtension2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMain_002EMainViewModel_002CRootApp_002EClient_002EAvalonia_002EMemberProfile_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension3 = compiledBindingExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_4(contentControl4, compiledBindingExtension3);
		context.PopParent();
		((ISupportInitialize)contentControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootScrollViewer3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		context.PopParent();
		FlyoutBase.SetAttachedFlyout(P_1, rootFlyout);
		ItemsControl itemsControl2;
		ItemsControl itemsControl = (itemsControl2 = new ItemsControl());
		((ISupportInitialize)itemsControl).BeginInit();
		P_1.Content = itemsControl;
		ItemsControl itemsControl4;
		ItemsControl itemsControl3 = (itemsControl4 = itemsControl2);
		context.PushParent(itemsControl4);
		StyledProperty<IEnumerable?> itemsSourceProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMain_002EMainViewModel_002CRootApp_002EClient_002EAvalonia_002EViewModels_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension5 = compiledBindingExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl4, itemsSourceProperty, compiledBindingExtension5);
		itemsControl4.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_171.Build_1), context)
		};
		DataTemplate dataTemplate;
		DataTemplate itemTemplate = (dataTemplate = new DataTemplate());
		context.PushParent(dataTemplate);
		dataTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_171.Build_2), context);
		context.PopParent();
		itemsControl4.ItemTemplate = itemTemplate;
		context.PopParent();
		((ISupportInitialize)itemsControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(MainView P_0)
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

