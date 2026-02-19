// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.CommunityTabView
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
using Avalonia.Media.Imaging;
using Avalonia.Styling;
using Avalonia.Threading;
using Avalonia.VisualTree;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home;
using Tabalonia.Controls;

public class CommunityTabView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_133
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<CommunityTabView> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((AvaloniaObject)obj).SetValue(StackPanel.OrientationProperty, Orientation.Horizontal, BindingPriority.Template);
			((AvaloniaObject)obj).SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<CommunityTabView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<CommunityTabView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<CommunityTabView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FHome_002FCommunityTabView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Home/CommunityTabView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (CommunityTabView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<CommunityTabView> context = CreateContext(P_0);
			context.IntermediateRoot = new RootImageLoader();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			RootImageLoader rootImageLoader = (RootImageLoader)obj;
			context.PushParent(rootImageLoader);
			rootImageLoader.Width = 12.0;
			rootImageLoader.Height = 12.0;
			rootImageLoader.CornerRadius = new CornerRadius(3.0, 3.0, 3.0, 3.0);
			StyledProperty<IBrush?> backgroundProperty = TemplatedControl.BackgroundProperty;
			DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BackgroundSecondary");
			context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
			IBinding binding = dynamicResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(rootImageLoader, backgroundProperty, binding);
			StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
			CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EVoiceCallMemberAvatarViewModel_002CRootApp_002EClient_002EAvalonia_002EProfilePictureAsyncBitmapWrapper_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
			context.ProvideTargetProperty = RootImageLoader.SourceProperty;
			CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(rootImageLoader, sourceProperty, compiledBindingExtension2);
			rootImageLoader.LoadingPlaceholderSize = 6.0;
			rootImageLoader.Stretch = Stretch.UniformToFill;
			StyledProperty<Thickness> marginProperty = Layoutable.MarginProperty;
			CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EVoiceCallMemberAvatarViewModel_002CRootApp_002EClient_002EAvalonia_002EAvatarMargin_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
			context.ProvideTargetProperty = Layoutable.MarginProperty;
			CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(rootImageLoader, marginProperty, compiledBindingExtension4);
			context.PopParent();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}
	}

	private DragTabItem? _dragTabItem;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal MenuItem InviteMembersMenuItem;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal MenuItem CommunitySettingsMenuItem;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal MenuItem LeaveCommunityMenuItem;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock CommunityNameTextBlock;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock MemberCountTextBlock;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border VoiceCallIndicator;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock NotificationTextBlock;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	private CommunityTabViewModel _communityTabViewModel => (CommunityTabViewModel)base.DataContext;

	public CommunityTabView()
	{
		InitializeComponent();
	}

	protected override void OnLoaded(RoutedEventArgs P_0)
	{
		base.OnLoaded(P_0);
		updateCommunityNameActivity();
		_communityTabViewModel.PropertyChanged += onCommunityTabViewModelPropertyChanged;
		_dragTabItem = this.FindAncestorOfType<DragTabItem>();
		if (_dragTabItem != null)
		{
			_dragTabItem.ContextFlyout = base.ContextFlyout;
			_dragTabItem.PointerPressed += onDragItemPointerPressed;
		}
		base.PropertyChanged += onPropertyChanged;
		updateAvailableWidth();
	}

	protected override void OnUnloaded(RoutedEventArgs P_0)
	{
		base.OnUnloaded(P_0);
		_communityTabViewModel.PropertyChanged -= onCommunityTabViewModelPropertyChanged;
		base.PropertyChanged -= onPropertyChanged;
		if (_dragTabItem != null)
		{
			_dragTabItem.PointerPressed -= onDragItemPointerPressed;
			_dragTabItem = null;
		}
	}

	private void onCommunityTabViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "HasAnyActivity")
			{
				updateCommunityNameActivity();
			}
		});
	}

	private void onDragItemPointerPressed(object? sender, PointerPressedEventArgs e)
	{
		if (e.GetCurrentPoint(this).Properties.IsMiddleButtonPressed && _communityTabViewModel.CloseTabCommand.CanExecute(null))
		{
			_communityTabViewModel.CloseTabCommand.Execute(null);
		}
	}

	private void updateCommunityNameActivity()
	{
		if (_communityTabViewModel.HasAnyActivity)
		{
			CommunityNameTextBlock[!TemplatedControl.ForegroundProperty] = new DynamicResourceExtension("TextPrimary");
			CommunityNameTextBlock.FontWeight = FontWeight.DemiBold;
		}
		else
		{
			CommunityNameTextBlock[!TemplatedControl.ForegroundProperty] = new DynamicResourceExtension("TextSecondary");
			CommunityNameTextBlock.FontWeight = (FontWeight)450;
		}
	}

	private void onPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
	{
		if (e.Property == Visual.BoundsProperty)
		{
			updateAvailableWidth();
		}
	}

	private void updateAvailableWidth()
	{
		_communityTabViewModel.AvailableWidth = base.Bounds.Width;
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
		InviteMembersMenuItem = nameScope?.Find<MenuItem>("InviteMembersMenuItem");
		CommunitySettingsMenuItem = nameScope?.Find<MenuItem>("CommunitySettingsMenuItem");
		LeaveCommunityMenuItem = nameScope?.Find<MenuItem>("LeaveCommunityMenuItem");
		CommunityNameTextBlock = nameScope?.Find<TextBlock>("CommunityNameTextBlock");
		MemberCountTextBlock = nameScope?.Find<TextBlock>("MemberCountTextBlock");
		VoiceCallIndicator = nameScope?.Find<Border>("VoiceCallIndicator");
		NotificationTextBlock = nameScope?.Find<TextBlock>("NotificationTextBlock");
	}

	[CompilerGenerated]
	private unsafe static void _0021XamlIlPopulate(IServiceProvider P_0, CommunityTabView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<CommunityTabView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<CommunityTabView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FHome_002FCommunityTabView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Home/CommunityTabView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		RenderOptions.SetBitmapInterpolationMode(P_1, BitmapInterpolationMode.MediumQuality);
		RootMenuFlyout rootMenuFlyout;
		RootMenuFlyout contextFlyout = (rootMenuFlyout = new RootMenuFlyout());
		context.PushParent(rootMenuFlyout);
		ItemCollection items = rootMenuFlyout.Items;
		MenuItem menuItem2;
		MenuItem menuItem = (menuItem2 = new MenuItem());
		((ISupportInitialize)menuItem).BeginInit();
		items.Add(menuItem);
		MenuItem menuItem4;
		MenuItem menuItem3 = (menuItem4 = menuItem2);
		context.PushParent(menuItem4);
		MenuItem menuItem5 = menuItem4;
		menuItem5.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.CloseTab;
		StyledProperty<ICommand?> commandProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002ECloseTabCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem5, commandProperty, compiledBindingExtension2);
		context.PopParent();
		((ISupportInitialize)menuItem3).EndInit();
		ItemCollection items2 = rootMenuFlyout.Items;
		MenuItem menuItem7;
		MenuItem menuItem6 = (menuItem7 = new MenuItem());
		((ISupportInitialize)menuItem6).BeginInit();
		items2.Add(menuItem6);
		MenuItem menuItem8 = (menuItem4 = menuItem7);
		context.PushParent(menuItem4);
		MenuItem menuItem9 = menuItem4;
		menuItem9.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.OpenInNewWindow;
		StyledProperty<ICommand?> commandProperty2 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002EPopoutTabCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem9, commandProperty2, compiledBindingExtension4);
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension();
		context.ProvideTargetProperty = MenuItem.CommandParameterProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_27(menuItem9, compiledBindingExtension6);
		context.PopParent();
		((ISupportInitialize)menuItem8).EndInit();
		ItemCollection items3 = rootMenuFlyout.Items;
		MenuItem menuItem11;
		MenuItem menuItem10 = (menuItem11 = new MenuItem());
		((ISupportInitialize)menuItem10).BeginInit();
		items3.Add(menuItem10);
		MenuItem menuItem12 = (menuItem4 = menuItem11);
		context.PushParent(menuItem4);
		MenuItem menuItem13 = menuItem4;
		menuItem13.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.MarkAllChannelsAsRead;
		StyledProperty<ICommand?> commandProperty3 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002EMarkAllChannelsAsReadCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem13, commandProperty3, compiledBindingExtension8);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002EHasAnyActivity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem13, isVisibleProperty, compiledBindingExtension10);
		context.PopParent();
		((ISupportInitialize)menuItem12).EndInit();
		ItemCollection items4 = rootMenuFlyout.Items;
		Separator separator2;
		Separator separator = (separator2 = new Separator());
		((ISupportInitialize)separator).BeginInit();
		items4.Add(separator);
		Separator separator4;
		Separator separator3 = (separator4 = separator2);
		context.PushParent(separator4);
		Separator separator5 = separator4;
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		MultiBinding multiBinding2;
		MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding3 = multiBinding2;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("OrConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding3.Converter = (IMultiValueConverter)obj;
		IList<IBinding> bindings = multiBinding3.Bindings;
		CompiledBindingExtension obj2 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "InviteMembersMenuItem").Property(Visual.IsVisibleProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item = obj2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings.Add(item);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(separator5, isVisibleProperty2, multiBinding);
		context.PopParent();
		((ISupportInitialize)separator3).EndInit();
		ItemCollection items5 = rootMenuFlyout.Items;
		MenuItem menuItem15;
		MenuItem menuItem14 = (menuItem15 = new MenuItem());
		((ISupportInitialize)menuItem14).BeginInit();
		items5.Add(menuItem14);
		MenuItem menuItem16 = (menuItem4 = menuItem15);
		context.PushParent(menuItem4);
		MenuItem menuItem17 = menuItem4;
		menuItem17.Name = "InviteMembersMenuItem";
		object obj3 = menuItem17;
		context.AvaloniaNameScope.Register("InviteMembersMenuItem", obj3);
		menuItem17.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.InviteMembers;
		StyledProperty<ICommand?> commandProperty4 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002EShowInviteMembersViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem17, commandProperty4, compiledBindingExtension12);
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002ELocalCommunityPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalCommunityPermission_002CRootApp_002EClient_002ECoreDomain_002ECommunityCreateInvite_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "False"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension13 = obj4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem17, isVisibleProperty3, compiledBindingExtension13);
		context.PopParent();
		((ISupportInitialize)menuItem16).EndInit();
		ItemCollection items6 = rootMenuFlyout.Items;
		MenuItem menuItem19;
		MenuItem menuItem18 = (menuItem19 = new MenuItem());
		((ISupportInitialize)menuItem18).BeginInit();
		items6.Add(menuItem18);
		MenuItem menuItem20 = (menuItem4 = menuItem19);
		context.PushParent(menuItem4);
		MenuItem menuItem21 = menuItem4;
		menuItem21.Name = "CommunitySettingsMenuItem";
		obj3 = menuItem21;
		context.AvaloniaNameScope.Register("CommunitySettingsMenuItem", obj3);
		menuItem21.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.CommunitySettings;
		StyledProperty<ICommand?> commandProperty5 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension14 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002EShowCommunitySettingsViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension15 = compiledBindingExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem21, commandProperty5, compiledBindingExtension15);
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		MultiBinding multiBinding4 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding5 = multiBinding2;
		multiBinding5.Converter = BoolConverters.Or;
		IList<IBinding> bindings2 = multiBinding5.Bindings;
		CompiledBindingExtension compiledBindingExtension16 = new CompiledBindingExtension();
		compiledBindingExtension16.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002ELocalCommunityPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalCommunityPermission_002CRootApp_002EClient_002ECoreDomain_002ECommunityManageApps_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension16.FallbackValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item2 = compiledBindingExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings2.Add(item2);
		IList<IBinding> bindings3 = multiBinding5.Bindings;
		CompiledBindingExtension compiledBindingExtension17 = new CompiledBindingExtension();
		compiledBindingExtension17.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002ELocalCommunityPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalCommunityPermission_002CRootApp_002EClient_002ECoreDomain_002ECommunityManageAuditLog_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension17.FallbackValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item3 = compiledBindingExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings3.Add(item3);
		IList<IBinding> bindings4 = multiBinding5.Bindings;
		CompiledBindingExtension compiledBindingExtension18 = new CompiledBindingExtension();
		compiledBindingExtension18.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002ELocalCommunityPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalCommunityPermission_002CRootApp_002EClient_002ECoreDomain_002ECommunityManageBans_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension18.FallbackValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item4 = compiledBindingExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings4.Add(item4);
		IList<IBinding> bindings5 = multiBinding5.Bindings;
		CompiledBindingExtension compiledBindingExtension19 = new CompiledBindingExtension();
		compiledBindingExtension19.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002ELocalCommunityPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalCommunityPermission_002CRootApp_002EClient_002ECoreDomain_002ECommunityManageCommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension19.FallbackValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item5 = compiledBindingExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings5.Add(item5);
		IList<IBinding> bindings6 = multiBinding5.Bindings;
		CompiledBindingExtension compiledBindingExtension20 = new CompiledBindingExtension();
		compiledBindingExtension20.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002ELocalCommunityPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalCommunityPermission_002CRootApp_002EClient_002ECoreDomain_002ECommunityManageRoles_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension20.FallbackValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item6 = compiledBindingExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings6.Add(item6);
		IList<IBinding> bindings7 = multiBinding5.Bindings;
		CompiledBindingExtension compiledBindingExtension21 = new CompiledBindingExtension();
		compiledBindingExtension21.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002ELocalCommunityPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalCommunityPermission_002CRootApp_002EClient_002ECoreDomain_002ECommunityManageEmojis_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension21.FallbackValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item7 = compiledBindingExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings7.Add(item7);
		IList<IBinding> bindings8 = multiBinding5.Bindings;
		CompiledBindingExtension compiledBindingExtension22 = new CompiledBindingExtension();
		compiledBindingExtension22.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002ELocalCommunityPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalCommunityPermission_002CRootApp_002EClient_002ECoreDomain_002ECommunityManageInvites_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension22.FallbackValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item8 = compiledBindingExtension22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings8.Add(item8);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(menuItem21, isVisibleProperty4, multiBinding4);
		context.PopParent();
		((ISupportInitialize)menuItem20).EndInit();
		ItemCollection items7 = rootMenuFlyout.Items;
		Separator separator7;
		Separator separator6 = (separator7 = new Separator());
		((ISupportInitialize)separator6).BeginInit();
		items7.Add(separator6);
		Separator separator8 = (separator4 = separator7);
		context.PushParent(separator4);
		Separator separator9 = separator4;
		StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
		MultiBinding multiBinding6 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding7 = multiBinding2;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("OrConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj5 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding7.Converter = (IMultiValueConverter)obj5;
		IList<IBinding> bindings9 = multiBinding7.Bindings;
		CompiledBindingExtension obj6 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "CommunitySettingsMenuItem").Property(Visual.IsVisibleProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item9 = obj6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings9.Add(item9);
		IList<IBinding> bindings10 = multiBinding7.Bindings;
		CompiledBindingExtension obj7 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "LeaveCommunityMenuItem").Property(Visual.IsVisibleProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item10 = obj7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings10.Add(item10);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(separator9, isVisibleProperty5, multiBinding6);
		context.PopParent();
		((ISupportInitialize)separator8).EndInit();
		ItemCollection items8 = rootMenuFlyout.Items;
		MenuItem menuItem23;
		MenuItem menuItem22 = (menuItem23 = new MenuItem());
		((ISupportInitialize)menuItem22).BeginInit();
		items8.Add(menuItem22);
		MenuItem menuItem24 = (menuItem4 = menuItem23);
		context.PushParent(menuItem4);
		MenuItem menuItem25 = menuItem4;
		menuItem25.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.DeleteCommunity;
		menuItem25.Classes.Add("DeleteMenuItem");
		StyledProperty<ICommand?> commandProperty6 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension23 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002EShowDeleteCommunityViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension24 = compiledBindingExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem25, commandProperty6, compiledBindingExtension24);
		StyledProperty<bool> isVisibleProperty6 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension25 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002EIsCommunityOwner_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension26 = compiledBindingExtension25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem25, isVisibleProperty6, compiledBindingExtension26);
		context.PopParent();
		((ISupportInitialize)menuItem24).EndInit();
		ItemCollection items9 = rootMenuFlyout.Items;
		MenuItem menuItem27;
		MenuItem menuItem26 = (menuItem27 = new MenuItem());
		((ISupportInitialize)menuItem26).BeginInit();
		items9.Add(menuItem26);
		MenuItem menuItem28 = (menuItem4 = menuItem27);
		context.PushParent(menuItem4);
		MenuItem menuItem29 = menuItem4;
		menuItem29.Name = "LeaveCommunityMenuItem";
		obj3 = menuItem29;
		context.AvaloniaNameScope.Register("LeaveCommunityMenuItem", obj3);
		menuItem29.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.LeaveCommunity;
		menuItem29.Classes.Add("DeleteMenuItem");
		StyledProperty<ICommand?> commandProperty7 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension27 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Command("ShowLeaveCommunityViewModel", CompiledAvaloniaXaml.XamlIlTrampolines.RootApp_002EClient_002EAvalonia_003ARootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002BShowLeaveCommunityViewModel_0_0021CommandExecuteTrampoline, null, null).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension28 = compiledBindingExtension27.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem29, commandProperty7, compiledBindingExtension28);
		StyledProperty<bool> isVisibleProperty7 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension29 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002EIsCommunityOwner_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension30 = compiledBindingExtension29.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem29, isVisibleProperty7, compiledBindingExtension30);
		context.PopParent();
		((ISupportInitialize)menuItem28).EndInit();
		ItemCollection items10 = rootMenuFlyout.Items;
		Separator separator11;
		Separator separator10 = (separator11 = new Separator());
		((ISupportInitialize)separator10).BeginInit();
		items10.Add(separator10);
		Separator separator12 = (separator4 = separator11);
		context.PushParent(separator4);
		Separator separator13 = separator4;
		StyledProperty<bool> isVisibleProperty8 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension31 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002EDeveloperModeEnabled_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension32 = compiledBindingExtension31.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(separator13, isVisibleProperty8, compiledBindingExtension32);
		context.PopParent();
		((ISupportInitialize)separator12).EndInit();
		ItemCollection items11 = rootMenuFlyout.Items;
		MenuItem menuItem31;
		MenuItem menuItem30 = (menuItem31 = new MenuItem());
		((ISupportInitialize)menuItem30).BeginInit();
		items11.Add(menuItem30);
		MenuItem menuItem32 = (menuItem4 = menuItem31);
		context.PushParent(menuItem4);
		MenuItem menuItem33 = menuItem4;
		menuItem33.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.CopyCommunityId;
		StyledProperty<ICommand?> commandProperty8 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension33 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002ECopyCommunityIdCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension34 = compiledBindingExtension33.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem33, commandProperty8, compiledBindingExtension34);
		StyledProperty<bool> isVisibleProperty9 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension35 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002EDeveloperModeEnabled_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension36 = compiledBindingExtension35.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem33, isVisibleProperty9, compiledBindingExtension36);
		context.PopParent();
		((ISupportInitialize)menuItem32).EndInit();
		context.PopParent();
		P_1.ContextFlyout = contextFlyout;
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		P_1.Content = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		Grid grid5 = grid4;
		grid5.Margin = new Thickness(6.0, 0.0, 6.0, 0.0);
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(6.0, GridUnitType.Pixel)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		Controls children = grid5.Children;
		Ellipse ellipse2;
		Ellipse ellipse = (ellipse2 = new Ellipse());
		((ISupportInitialize)ellipse).BeginInit();
		children.Add(ellipse);
		Ellipse ellipse4;
		Ellipse ellipse3 = (ellipse4 = ellipse2);
		context.PushParent(ellipse4);
		Ellipse ellipse5 = ellipse4;
		StyledProperty<IBrush?> fillProperty = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(ellipse5, fillProperty, binding);
		ellipse5.Width = 6.0;
		ellipse5.Height = 6.0;
		ellipse5.Margin = new Thickness(-9.0, 0.0, 0.0, 0.0);
		Grid.SetColumn(ellipse5, 0);
		ellipse5.HorizontalAlignment = HorizontalAlignment.Left;
		StyledProperty<bool> isVisibleProperty10 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension37 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002EHasAnyActivity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension38 = compiledBindingExtension37.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(ellipse5, isVisibleProperty10, compiledBindingExtension38);
		context.PopParent();
		((ISupportInitialize)ellipse3).EndInit();
		Controls children2 = grid5.Children;
		RootImageLoader rootImageLoader2;
		RootImageLoader rootImageLoader = (rootImageLoader2 = new RootImageLoader());
		((ISupportInitialize)rootImageLoader).BeginInit();
		children2.Add(rootImageLoader);
		RootImageLoader rootImageLoader4;
		RootImageLoader rootImageLoader3 = (rootImageLoader4 = rootImageLoader2);
		context.PushParent(rootImageLoader4);
		Grid.SetColumn(rootImageLoader4, 0);
		rootImageLoader4.Width = 28.0;
		rootImageLoader4.Height = 28.0;
		rootImageLoader4.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		StyledProperty<IBrush?> backgroundProperty = TemplatedControl.BackgroundProperty;
		CompiledBindingExtension obj8 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002EPictureHex_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			FallbackValue = "#FFFFFF"
		};
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		CompiledBindingExtension compiledBindingExtension39 = obj8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, backgroundProperty, compiledBindingExtension39);
		StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension40 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunityPictureAsyncBitmapWrapper_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
		context.ProvideTargetProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension41 = compiledBindingExtension40.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, sourceProperty, compiledBindingExtension41);
		rootImageLoader4.LoadingPlaceholderSize = 0.0;
		rootImageLoader4.Stretch = Stretch.UniformToFill;
		context.PopParent();
		((ISupportInitialize)rootImageLoader3).EndInit();
		Controls children3 = grid5.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children3.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		Grid.SetColumn(stackPanel5, 2);
		stackPanel5.VerticalAlignment = VerticalAlignment.Center;
		Controls children4 = stackPanel5.Children;
		TextWithBadgePanel textWithBadgePanel2;
		TextWithBadgePanel textWithBadgePanel = (textWithBadgePanel2 = new TextWithBadgePanel());
		((ISupportInitialize)textWithBadgePanel).BeginInit();
		children4.Add(textWithBadgePanel);
		TextWithBadgePanel textWithBadgePanel4;
		TextWithBadgePanel textWithBadgePanel3 = (textWithBadgePanel4 = textWithBadgePanel2);
		context.PushParent(textWithBadgePanel4);
		Controls children5 = textWithBadgePanel4.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children5.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension obj9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002EName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			FallbackValue = "Unknown Community"
		};
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension42 = obj9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, compiledBindingExtension42);
		textBlock5.Name = "CommunityNameTextBlock";
		obj3 = textBlock5;
		context.AvaloniaNameScope.Register("CommunityNameTextBlock", obj3);
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj10 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj10);
		textBlock5.FontWeight = (FontWeight)450;
		textBlock5.FontSize = 13.0;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding2);
		textBlock5.TextTrimming = TextTrimming.CharacterEllipsis;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		Controls children6 = textWithBadgePanel4.Children;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		children6.Add(rootSvgImage);
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage5 = rootSvgImage4;
		StyledProperty<bool> isVisibleProperty11 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002EIsVerified_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			FallbackValue = "False"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension43 = obj11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage5, isVisibleProperty11, compiledBindingExtension43);
		StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("VerifiedCommunitySVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage5, svgPathProperty, binding3);
		rootSvgImage5.Width = 14.0;
		rootSvgImage5.Height = 14.0;
		rootSvgImage5.Margin = new Thickness(2.0, 0.0, 0.0, 0.0);
		rootSvgImage5.VerticalAlignment = VerticalAlignment.Center;
		ToolTip.SetPlacement(rootSvgImage5, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgImage5, -2.0);
		ToolTip.SetShowDelay(rootSvgImage5, 300);
		RootToolTip rootToolTip2;
		RootToolTip rootToolTip = (rootToolTip2 = new RootToolTip());
		((ISupportInitialize)rootToolTip).BeginInit();
		ToolTip.SetTip(rootSvgImage5, rootToolTip);
		RootToolTip rootToolTip4;
		RootToolTip rootToolTip3 = (rootToolTip4 = rootToolTip2);
		context.PushParent(rootToolTip4);
		ToolTip.SetPlacement(rootToolTip4, PlacementMode.Top);
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		rootToolTip4.Content = grid6;
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		grid9.MaxWidth = 300.0;
		ColumnDefinitions columnDefinitions = new ColumnDefinitions();
		columnDefinitions.Capacity = 3;
		columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(12.0, GridUnitType.Pixel)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		grid9.ColumnDefinitions = columnDefinitions;
		Controls children7 = grid9.Children;
		RootSvgImage rootSvgImage7;
		RootSvgImage rootSvgImage6 = (rootSvgImage7 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage6).BeginInit();
		children7.Add(rootSvgImage6);
		RootSvgImage rootSvgImage8 = (rootSvgImage4 = rootSvgImage7);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage9 = rootSvgImage4;
		rootSvgImage9.Width = 36.0;
		rootSvgImage9.Height = 36.0;
		StyledProperty<string?> svgPathProperty2 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("VerifiedCommunitySVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage9, svgPathProperty2, binding4);
		context.PopParent();
		((ISupportInitialize)rootSvgImage8).EndInit();
		Controls children8 = grid9.Children;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		children8.Add(stackPanel6);
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		Grid.SetColumn(stackPanel9, 2);
		stackPanel9.Spacing = 2.0;
		stackPanel9.VerticalAlignment = VerticalAlignment.Center;
		Controls children9 = stackPanel9.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children9.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.VerifiedCommunity;
		textBlock9.HorizontalAlignment = HorizontalAlignment.Center;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj12 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj12);
		textBlock9.FontWeight = FontWeight.DemiBold;
		textBlock9.FontSize = 14.0;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding5);
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		context.PopParent();
		((ISupportInitialize)textWithBadgePanel3).EndInit();
		Controls children10 = stackPanel5.Children;
		StackPanel stackPanel11;
		StackPanel stackPanel10 = (stackPanel11 = new StackPanel());
		((ISupportInitialize)stackPanel10).BeginInit();
		children10.Add(stackPanel10);
		StackPanel stackPanel12 = (stackPanel4 = stackPanel11);
		context.PushParent(stackPanel4);
		StackPanel stackPanel13 = stackPanel4;
		stackPanel13.Orientation = Orientation.Horizontal;
		stackPanel13.Margin = new Thickness(0.0, 1.0, 0.0, 0.0);
		stackPanel13.ClipToBounds = true;
		StyledProperty<bool> isVisibleProperty12 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension44 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002EShowBottomRow_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension45 = compiledBindingExtension44.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(stackPanel13, isVisibleProperty12, compiledBindingExtension45);
		Controls children11 = stackPanel13.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children11.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Border border5 = border4;
		border5.CornerRadius = new CornerRadius(4.0, 4.0, 4.0, 4.0);
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("HighlightLight");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, backgroundProperty2, binding6);
		border5.HorizontalAlignment = HorizontalAlignment.Left;
		border5.Height = 16.0;
		border5.MinWidth = 16.0;
		border5.Padding = new Thickness(3.0, 3.0, 3.0, 3.0);
		StyledProperty<bool> isVisibleProperty13 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension46 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002EShowMemberCount_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension47 = compiledBindingExtension46.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, isVisibleProperty13, compiledBindingExtension47);
		StackPanel stackPanel15;
		StackPanel stackPanel14 = (stackPanel15 = new StackPanel());
		((ISupportInitialize)stackPanel14).BeginInit();
		border5.Child = stackPanel14;
		StackPanel stackPanel16 = (stackPanel4 = stackPanel15);
		context.PushParent(stackPanel4);
		StackPanel stackPanel17 = stackPanel4;
		stackPanel17.Orientation = Orientation.Horizontal;
		Controls children12 = stackPanel17.Children;
		Ellipse ellipse7;
		Ellipse ellipse6 = (ellipse7 = new Ellipse());
		((ISupportInitialize)ellipse6).BeginInit();
		children12.Add(ellipse6);
		Ellipse ellipse8 = (ellipse4 = ellipse7);
		context.PushParent(ellipse4);
		Ellipse ellipse9 = ellipse4;
		StyledProperty<IBrush?> fillProperty2 = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("BrandSecondary");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(ellipse9, fillProperty2, binding7);
		ellipse9.Width = 5.0;
		ellipse9.Height = 5.0;
		ellipse9.VerticalAlignment = VerticalAlignment.Center;
		ellipse9.Margin = new Thickness(0.0, 0.0, 4.0, 0.0);
		context.PopParent();
		((ISupportInitialize)ellipse8).EndInit();
		Controls children13 = stackPanel17.Children;
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		children13.Add(textBlock10);
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		textBlock13.Name = "MemberCountTextBlock";
		obj3 = textBlock13;
		context.AvaloniaNameScope.Register("MemberCountTextBlock", obj3);
		StyledProperty<string?> textProperty2 = TextBlock.TextProperty;
		CompiledBindingExtension obj13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002EMembers_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EServices_002EIMemberService_002CRootApp_002EClient_002ECoreDomain_002EAttachedMemberCount_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			FallbackValue = "0"
		};
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension48 = obj13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, textProperty2, compiledBindingExtension48);
		textBlock13.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock13.VerticalAlignment = VerticalAlignment.Center;
		textBlock13.HorizontalAlignment = HorizontalAlignment.Center;
		textBlock13.FontSize = 11.0;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj14 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock13, obj14);
		textBlock13.FontWeight = FontWeight.Medium;
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, foregroundProperty3, binding8);
		textBlock13.TextTrimming = TextTrimming.CharacterEllipsis;
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel16).EndInit();
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		Controls children14 = stackPanel13.Children;
		Border border7;
		Border border6 = (border7 = new Border());
		((ISupportInitialize)border6).BeginInit();
		children14.Add(border6);
		Border border8 = (border4 = border7);
		context.PushParent(border4);
		Border border9 = border4;
		border9.Name = "VoiceCallIndicator";
		obj3 = border9;
		context.AvaloniaNameScope.Register("VoiceCallIndicator", obj3);
		border9.CornerRadius = new CornerRadius(4.0, 4.0, 4.0, 4.0);
		StyledProperty<IBrush?> backgroundProperty3 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("HighlightLight");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border9, backgroundProperty3, binding9);
		border9.Height = 16.0;
		border9.Margin = new Thickness(4.0, 0.0, 0.0, 0.0);
		border9.Padding = new Thickness(2.0, 1.0, 2.0, 1.0);
		border9.IsHitTestVisible = false;
		StyledProperty<bool> isVisibleProperty14 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension49 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002EShowVoiceCallIndicator_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension50 = compiledBindingExtension49.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border9, isVisibleProperty14, compiledBindingExtension50);
		StackPanel stackPanel19;
		StackPanel stackPanel18 = (stackPanel19 = new StackPanel());
		((ISupportInitialize)stackPanel18).BeginInit();
		border9.Child = stackPanel18;
		StackPanel stackPanel20 = (stackPanel4 = stackPanel19);
		context.PushParent(stackPanel4);
		StackPanel stackPanel21 = stackPanel4;
		stackPanel21.Orientation = Orientation.Horizontal;
		stackPanel21.VerticalAlignment = VerticalAlignment.Center;
		Controls children15 = stackPanel21.Children;
		RootSvgImage rootSvgImage11;
		RootSvgImage rootSvgImage10 = (rootSvgImage11 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage10).BeginInit();
		children15.Add(rootSvgImage10);
		RootSvgImage rootSvgImage12 = (rootSvgImage4 = rootSvgImage11);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage13 = rootSvgImage4;
		StyledProperty<string?> svgPathProperty3 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("VoiceSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage13, svgPathProperty3, binding10);
		rootSvgImage13.Width = 12.0;
		rootSvgImage13.Height = 12.0;
		rootSvgImage13.Opacity = 0.7;
		rootSvgImage13.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)rootSvgImage12).EndInit();
		Controls children16 = stackPanel21.Children;
		ItemsControl itemsControl2;
		ItemsControl itemsControl = (itemsControl2 = new ItemsControl());
		((ISupportInitialize)itemsControl).BeginInit();
		children16.Add(itemsControl);
		ItemsControl itemsControl4;
		ItemsControl itemsControl3 = (itemsControl4 = itemsControl2);
		context.PushParent(itemsControl4);
		StyledProperty<IEnumerable?> itemsSourceProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension51 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002EVoiceCallMembers_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension52 = compiledBindingExtension51.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl4, itemsSourceProperty, compiledBindingExtension52);
		itemsControl4.VerticalAlignment = VerticalAlignment.Center;
		itemsControl4.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_133.Build_1), context)
		};
		DataTemplate dataTemplate;
		DataTemplate itemTemplate = (dataTemplate = new DataTemplate());
		context.PushParent(dataTemplate);
		dataTemplate.DataType = typeof(VoiceCallMemberAvatarViewModel);
		dataTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_133.Build_2), context);
		context.PopParent();
		itemsControl4.ItemTemplate = itemTemplate;
		context.PopParent();
		((ISupportInitialize)itemsControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel20).EndInit();
		context.PopParent();
		((ISupportInitialize)border8).EndInit();
		Controls children17 = stackPanel13.Children;
		Border border11;
		Border border10 = (border11 = new Border());
		((ISupportInitialize)border10).BeginInit();
		children17.Add(border10);
		Border border12 = (border4 = border11);
		context.PushParent(border4);
		Border border13 = border4;
		border13.Margin = new Thickness(4.0, 0.0, 0.0, 0.0);
		border13.Height = 16.0;
		border13.MinWidth = 16.0;
		border13.IsHitTestVisible = false;
		StyledProperty<IBrush?> backgroundProperty4 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("Error");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border13, backgroundProperty4, binding11);
		border13.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<bool> isVisibleProperty15 = Visual.IsVisibleProperty;
		MultiBinding multiBinding8 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding9 = multiBinding2;
		multiBinding9.Converter = BoolConverters.And;
		IList<IBinding> bindings11 = multiBinding9.Bindings;
		CompiledBindingExtension obj15 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002EShowNotifications_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item11 = obj15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings11.Add(item11);
		IList<IBinding> bindings12 = multiBinding9.Bindings;
		CompiledBindingExtension obj16 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002ETab_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ETabs_002ETab_002CRootApp_002EClient_002ECoreDomain_002ENotifications_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ENotifications_002ENotificationContainer_002CRootApp_002EClient_002ECoreDomain_002EContainerUnviewedNotificationCount_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item12 = obj16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings12.Add(item12);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(border13, isVisibleProperty15, multiBinding8);
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		border13.Child = textBlock14;
		TextBlock textBlock16 = (textBlock4 = textBlock15);
		context.PushParent(textBlock4);
		TextBlock textBlock17 = textBlock4;
		textBlock17.Name = "NotificationTextBlock";
		obj3 = textBlock17;
		context.AvaloniaNameScope.Register("NotificationTextBlock", obj3);
		textBlock17.FontSize = 11.0;
		StyledProperty<IBrush?> foregroundProperty4 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding12 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock17, foregroundProperty4, binding12);
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj17 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock17, obj17);
		textBlock17.FontWeight = FontWeight.Bold;
		StyledProperty<string?> textProperty3 = TextBlock.TextProperty;
		CompiledBindingExtension obj18 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002ETab_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ETabs_002ETab_002CRootApp_002EClient_002ECoreDomain_002ENotifications_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ENotifications_002ENotificationContainer_002CRootApp_002EClient_002ECoreDomain_002EContainerUnviewedNotificationCount_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "0"
		};
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension53 = obj18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock17, textProperty3, compiledBindingExtension53);
		textBlock17.Margin = new Thickness(2.0, 0.0, 2.0, 0.0);
		textBlock17.HorizontalAlignment = HorizontalAlignment.Center;
		textBlock17.VerticalAlignment = VerticalAlignment.Center;
		textBlock17.TextTrimming = TextTrimming.CharacterEllipsis;
		context.PopParent();
		((ISupportInitialize)textBlock16).EndInit();
		context.PopParent();
		((ISupportInitialize)border12).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel12).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		Controls children18 = grid5.Children;
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		children18.Add(panel);
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		Grid.SetColumn(panel4, 3);
		panel4.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<bool> isVisibleProperty16 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension54 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ECommunityTabViewModel_002CRootApp_002EClient_002EAvalonia_002EIsOnCall_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension55 = compiledBindingExtension54.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(panel4, isVisibleProperty16, compiledBindingExtension55);
		Controls children19 = panel4.Children;
		ThemeVariantScope themeVariantScope2;
		ThemeVariantScope themeVariantScope = (themeVariantScope2 = new ThemeVariantScope());
		((ISupportInitialize)themeVariantScope).BeginInit();
		children19.Add(themeVariantScope);
		ThemeVariantScope themeVariantScope4;
		ThemeVariantScope themeVariantScope3 = (themeVariantScope4 = themeVariantScope2);
		context.PushParent(themeVariantScope4);
		themeVariantScope4.RequestedThemeVariant = ThemeVariant.Light;
		Ellipse ellipse11;
		Ellipse ellipse10 = (ellipse11 = new Ellipse());
		((ISupportInitialize)ellipse10).BeginInit();
		themeVariantScope4.Child = ellipse10;
		Ellipse ellipse12 = (ellipse4 = ellipse11);
		context.PushParent(ellipse4);
		Ellipse ellipse13 = ellipse4;
		StyledProperty<IBrush?> fillProperty3 = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("BrandTertiary");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding13 = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(ellipse13, fillProperty3, binding13);
		ellipse13.Width = 18.0;
		ellipse13.Height = 18.0;
		context.PopParent();
		((ISupportInitialize)ellipse12).EndInit();
		context.PopParent();
		((ISupportInitialize)themeVariantScope3).EndInit();
		Controls children20 = panel4.Children;
		RootSvgImage rootSvgImage15;
		RootSvgImage rootSvgImage14 = (rootSvgImage15 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage14).BeginInit();
		children20.Add(rootSvgImage14);
		RootSvgImage rootSvgImage16 = (rootSvgImage4 = rootSvgImage15);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage17 = rootSvgImage4;
		StyledProperty<string?> svgPathProperty4 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("DMCallSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding14 = dynamicResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage17, svgPathProperty4, binding14);
		rootSvgImage17.Width = 10.0;
		rootSvgImage17.Height = 10.0;
		context.PopParent();
		((ISupportInitialize)rootSvgImage16).EndInit();
		context.PopParent();
		((ISupportInitialize)panel3).EndInit();
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
	private static void _0021XamlIlPopulateTrampoline(CommunityTabView P_0)
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

