// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.MemberView
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Controls.ContextMenus;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Resources.Converters.CommunityMembers;
using RootApp.Client.Avalonia.Resources.Converters.Roles;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Community.Members;

public class MemberView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_82
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MemberView> context = CreateContext(P_0);
			return new CommunityMemberOnlineStatusConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<MemberView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MemberView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MemberView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FCommunity_002FMembers_002FMemberView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Members/MemberView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (MemberView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MemberView> context = CreateContext(P_0);
			return new CommunityMemberFriendCutoutConverter();
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MemberView> context = CreateContext(P_0);
			return new CommunityRoleColorConverter();
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Panel MainGrid;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal StackPanel MainStackPanel;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border OnlineBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock UsernameTextBlock;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgImage OwnerImage;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Button UserProfileButton;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	private MemberViewModel? _memberViewModel => base.DataContext as MemberViewModel;

	public MemberView()
	{
		InitializeComponent();
	}

	private void onUserControlSizeChanged(object? sender, SizeChangedEventArgs e)
	{
		if (!e.WidthChanged)
		{
			return;
		}
		double width = e.NewSize.Width;
		if (width < 80.0)
		{
			MainStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
			MainGrid.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
			UsernameTextBlock.IsVisible = false;
			OwnerImage.IsVisible = false;
			if (UserProfileButton.Flyout is RootFlyout rootFlyout)
			{
				rootFlyout.HorizontalOffset = -7.0;
			}
		}
		else
		{
			MainStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
			MainGrid.Margin = new Thickness(8.0, 0.0, 15.0, 0.0);
			UsernameTextBlock.IsVisible = true;
			OwnerImage.IsVisible = _memberViewModel?.Member.IsOwner ?? false;
			if (UserProfileButton.Flyout is RootFlyout rootFlyout2)
			{
				rootFlyout2.HorizontalOffset = 8.0;
			}
		}
	}

	private void onFlyoutOpening(object? sender, EventArgs e)
	{
		if (_memberViewModel != null && _memberViewModel.ProfileOpeningCommand.CanExecute(null))
		{
			_memberViewModel.ProfileOpeningCommand.Execute(null);
		}
	}

	private void onFlyoutClosing(object? sender, CancelEventArgs e)
	{
		if (_memberViewModel != null && _memberViewModel.ProfileClosingCommand.CanExecute(null))
		{
			_memberViewModel.ProfileClosingCommand.Execute(null);
		}
	}

	private void onMainGridPointerEntered(object? sender, PointerEventArgs e)
	{
		if (_memberViewModel != null && _memberViewModel.PointerEnteredCommand.CanExecute(null))
		{
			_memberViewModel.PointerEnteredCommand.Execute(null);
		}
	}

	private void onMainGridPointerExited(object? sender, PointerEventArgs e)
	{
		if (_memberViewModel != null && _memberViewModel.PointerExitedCommand.CanExecute(null))
		{
			_memberViewModel.PointerExitedCommand.Execute(null);
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
		MainGrid = nameScope?.Find<Panel>("MainGrid");
		MainStackPanel = nameScope?.Find<StackPanel>("MainStackPanel");
		OnlineBorder = nameScope?.Find<Border>("OnlineBorder");
		UsernameTextBlock = nameScope?.Find<TextBlock>("UsernameTextBlock");
		OwnerImage = nameScope?.Find<RootSvgImage>("OwnerImage");
		UserProfileButton = nameScope?.Find<Button>("UserProfileButton");
	}

	[CompilerGenerated]
	private unsafe static void _0021XamlIlPopulate(IServiceProvider P_0, MemberView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<MemberView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MemberView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FCommunity_002FMembers_002FMemberView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Members/MemberView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.Background = new ImmutableSolidColorBrush(16777215u);
		P_1.Cursor = new Cursor(StandardCursorType.Hand);
		ToolTip.SetPlacement(P_1, PlacementMode.Right);
		ToolTip.SetVerticalOffset(P_1, 0.0);
		ToolTip.SetHorizontalOffset(P_1, 4.0);
		ToolTip.SetShowDelay(P_1, 0);
		RootToolTip rootToolTip2;
		RootToolTip rootToolTip = (rootToolTip2 = new RootToolTip());
		((ISupportInitialize)rootToolTip).BeginInit();
		ToolTip.SetTip(P_1, rootToolTip);
		RootToolTip rootToolTip4;
		RootToolTip rootToolTip3 = (rootToolTip4 = rootToolTip2);
		context.PushParent(rootToolTip4);
		ToolTip.SetPlacement(rootToolTip4, PlacementMode.Right);
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		rootToolTip4.Content = stackPanel;
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		Controls children = stackPanel4.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002EMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EUserName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, compiledBindingExtension2);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj);
		textBlock5.FontWeight = (FontWeight)450;
		textBlock5.FontSize = 14.0;
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip3).EndInit();
		ResourceDictionary resourceDictionary = new ResourceDictionary();
		if (resourceDictionary is ResourceDictionary resourceDictionary2)
		{
			resourceDictionary2.EnsureCapacity(resourceDictionary2.Count + 3);
		}
		resourceDictionary.AddDeferred("CommunityMemberOnlineStatusConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_82.Build_1), context));
		resourceDictionary.AddDeferred("CommunityMemberFriendCutoutConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_82.Build_2), context));
		resourceDictionary.AddDeferred("CommunityRoleColorConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_82.Build_3), context));
		P_1.Resources = resourceDictionary;
		UserContextMenuView userContextMenuView;
		UserContextMenuView contextFlyout = (userContextMenuView = new UserContextMenuView());
		context.PushParent(userContextMenuView);
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EUserContextMenu_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.System_002ELazy_00601_002CSystem_002ERuntime_002EValue_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = UserContextMenuView.DataContextProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_22(userContextMenuView, compiledBindingExtension4);
		context.PopParent();
		P_1.ContextFlyout = contextFlyout;
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		P_1.Content = panel;
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		Panel panel5 = panel4;
		panel5.Name = "MainGrid";
		object obj2 = panel5;
		context.AvaloniaNameScope.Register("MainGrid", obj2);
		panel5.Background = new ImmutableSolidColorBrush(16777215u);
		panel5.AddHandler(Control.SizeChangedEvent, context.RootObject.onUserControlSizeChanged);
		panel5.AddHandler(InputElement.PointerEnteredEvent, context.RootObject.onMainGridPointerEntered);
		panel5.AddHandler(InputElement.PointerExitedEvent, context.RootObject.onMainGridPointerExited);
		Controls children2 = panel5.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children2.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Border border5 = border4;
		border5.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, backgroundProperty, binding);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EIsSelectedOrHighlighted_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, isVisibleProperty, compiledBindingExtension6);
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		Controls children3 = panel5.Children;
		StackPanel stackPanel6;
		StackPanel stackPanel5 = (stackPanel6 = new StackPanel());
		((ISupportInitialize)stackPanel5).BeginInit();
		children3.Add(stackPanel5);
		StackPanel stackPanel7 = (stackPanel4 = stackPanel6);
		context.PushParent(stackPanel4);
		StackPanel stackPanel8 = stackPanel4;
		stackPanel8.Name = "MainStackPanel";
		obj2 = stackPanel8;
		context.AvaloniaNameScope.Register("MainStackPanel", obj2);
		stackPanel8.Orientation = Orientation.Horizontal;
		stackPanel8.Background = new ImmutableSolidColorBrush(16777215u);
		stackPanel8.Margin = new Thickness(8.0, 5.0, 8.0, 5.0);
		Controls children4 = stackPanel8.Children;
		Panel panel7;
		Panel panel6 = (panel7 = new Panel());
		((ISupportInitialize)panel6).BeginInit();
		children4.Add(panel6);
		Panel panel8 = (panel4 = panel7);
		context.PushParent(panel4);
		Panel panel9 = panel4;
		panel9.VerticalAlignment = VerticalAlignment.Center;
		Controls children5 = panel9.Children;
		Border border7;
		Border border6 = (border7 = new Border());
		((ISupportInitialize)border6).BeginInit();
		children5.Add(border6);
		Border border8 = (border4 = border7);
		context.PushParent(border4);
		Border border9 = border4;
		GeometryGroup geometryGroup = new GeometryGroup();
		geometryGroup.Children.Add(new RectangleGeometry
		{
			Rect = Rect.Parse("0,0,30,30")
		});
		GeometryCollection children6 = geometryGroup.Children;
		EllipseGeometry ellipseGeometry = new EllipseGeometry();
		ellipseGeometry.Center = new Point(27.0, 27.0);
		ellipseGeometry.RadiusX = 8.0;
		ellipseGeometry.RadiusY = 8.0;
		children6.Add(ellipseGeometry);
		border9.Clip = geometryGroup;
		RootImageLoader rootImageLoader2;
		RootImageLoader rootImageLoader = (rootImageLoader2 = new RootImageLoader());
		((ISupportInitialize)rootImageLoader).BeginInit();
		border9.Child = rootImageLoader;
		RootImageLoader rootImageLoader4;
		RootImageLoader rootImageLoader3 = (rootImageLoader4 = rootImageLoader2);
		context.PushParent(rootImageLoader4);
		rootImageLoader4.Width = 30.0;
		rootImageLoader4.Height = 30.0;
		rootImageLoader4.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		StyledProperty<IBrush?> backgroundProperty2 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, backgroundProperty2, binding2);
		StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EProfilePictureAsyncBitmapWrapper_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
		context.ProvideTargetProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, sourceProperty, compiledBindingExtension8);
		rootImageLoader4.LoadingPlaceholderSize = 16.0;
		rootImageLoader4.Stretch = Stretch.UniformToFill;
		context.PopParent();
		((ISupportInitialize)rootImageLoader3).EndInit();
		context.PopParent();
		((ISupportInitialize)border8).EndInit();
		Controls children7 = panel9.Children;
		Border border11;
		Border border10 = (border11 = new Border());
		((ISupportInitialize)border10).BeginInit();
		children7.Add(border10);
		Border border12 = (border4 = border11);
		context.PushParent(border4);
		Border border13 = border4;
		border13.Name = "OnlineBorder";
		obj2 = border13;
		context.AvaloniaNameScope.Register("OnlineBorder", obj2);
		border13.Width = 10.0;
		border13.Height = 10.0;
		border13.CornerRadius = new CornerRadius(10.0, 10.0, 10.0, 10.0);
		border13.Margin = new Thickness(0.0, 0.0, -2.0, -2.0);
		border13.HorizontalAlignment = HorizontalAlignment.Right;
		border13.VerticalAlignment = VerticalAlignment.Bottom;
		StyledProperty<Geometry?> clipProperty = Visual.ClipProperty;
		CompiledBindingExtension compiledBindingExtension10;
		CompiledBindingExtension compiledBindingExtension9 = (compiledBindingExtension10 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002EMember_002CRootApp_002EClient_002ECoreDomain_002ECommunityOnlineStatus_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension10);
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("CommunityMemberFriendCutoutConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj3 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension10.Converter = (IValueConverter)obj3;
		context.PopParent();
		context.ProvideTargetProperty = Visual.ClipProperty;
		CompiledBindingExtension compiledBindingExtension11 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border13, clipProperty, compiledBindingExtension11);
		StyledProperty<IBrush?> backgroundProperty3 = Border.BackgroundProperty;
		MultiBinding multiBinding2;
		MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding3 = multiBinding2;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("CommunityMemberOnlineStatusConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj4 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding3.Converter = (IMultiValueConverter)obj4;
		IList<IBinding> bindings = multiBinding3.Bindings;
		CompiledBindingExtension obj5 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002EMember_002CRootApp_002EClient_002ECoreDomain_002ECommunityOnlineStatus_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item = obj5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings.Add(item);
		IList<IBinding> bindings2 = multiBinding3.Bindings;
		CompiledBindingExtension compiledBindingExtension12 = new CompiledBindingExtension();
		compiledBindingExtension12.Path = new CompiledBindingPathBuilder(1).Property(Application.ActualThemeVariantProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
		compiledBindingExtension12.Source = Application.Current;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item2 = compiledBindingExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings2.Add(item2);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(border13, backgroundProperty3, multiBinding);
		context.PopParent();
		((ISupportInitialize)border12).EndInit();
		context.PopParent();
		((ISupportInitialize)panel8).EndInit();
		Controls children8 = stackPanel8.Children;
		StackPanel stackPanel10;
		StackPanel stackPanel9 = (stackPanel10 = new StackPanel());
		((ISupportInitialize)stackPanel9).BeginInit();
		children8.Add(stackPanel9);
		StackPanel stackPanel11 = (stackPanel4 = stackPanel10);
		context.PushParent(stackPanel4);
		StackPanel stackPanel12 = stackPanel4;
		stackPanel12.Orientation = Orientation.Horizontal;
		Controls children9 = stackPanel12.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children9.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Name = "UsernameTextBlock";
		obj2 = textBlock9;
		context.AvaloniaNameScope.Register("UsernameTextBlock", obj2);
		StyledProperty<string?> textProperty2 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002EMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EUserName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, textProperty2, compiledBindingExtension14);
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj6 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj6);
		textBlock9.FontWeight = FontWeight.Medium;
		textBlock9.FontSize = 14.0;
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock9.VerticalAlignment = VerticalAlignment.Center;
		textBlock9.Margin = new Thickness(14.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		MultiBinding multiBinding4 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding5 = multiBinding2;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("CommunityRoleColorConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj7 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding5.Converter = (IMultiValueConverter)obj7;
		IList<IBinding> bindings3 = multiBinding5.Bindings;
		CompiledBindingExtension compiledBindingExtension15 = new CompiledBindingExtension();
		compiledBindingExtension15.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002EMember_002CRootApp_002EClient_002ECoreDomain_002ERoles_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002ERepositories_002ECommunity_002EIMemberRoleService_002CRootApp_002EClient_002ECoreDomain_002EPrimaryRole_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ERole_002CRootApp_002EClient_002ECoreDomain_002ERoleColorHex_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension15.FallbackValue = null;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item3 = compiledBindingExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings3.Add(item3);
		IList<IBinding> bindings4 = multiBinding5.Bindings;
		CompiledBindingExtension compiledBindingExtension16 = new CompiledBindingExtension();
		compiledBindingExtension16.Path = new CompiledBindingPathBuilder(1).Property(Application.ActualThemeVariantProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
		compiledBindingExtension16.Source = Application.Current;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item4 = compiledBindingExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings4.Add(item4);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty, multiBinding4);
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		Controls children10 = stackPanel12.Children;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		children10.Add(rootSvgImage);
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		rootSvgImage4.Name = "OwnerImage";
		obj2 = rootSvgImage4;
		context.AvaloniaNameScope.Register("OwnerImage", obj2);
		rootSvgImage4.Margin = new Thickness(8.0, 0.0, 0.0, 0.0);
		rootSvgImage4.Width = 15.0;
		rootSvgImage4.Height = 15.0;
		StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("OwnerSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage4, svgPathProperty, binding3);
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		MultiBinding multiBinding6 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding7 = multiBinding2;
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj8 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding7.Converter = (IMultiValueConverter)obj8;
		IList<IBinding> bindings5 = multiBinding7.Bindings;
		CompiledBindingExtension obj9 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002EMember_002CRootApp_002EClient_002ECoreDomain_002EIsOwner_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item5 = obj9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings5.Add(item5);
		IList<IBinding> bindings6 = multiBinding7.Bindings;
		CompiledBindingExtension obj10 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EMenuIn_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item6 = obj10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings6.Add(item6);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgImage4, isVisibleProperty2, multiBinding6);
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		Controls children11 = stackPanel12.Children;
		Border border15;
		Border border14 = (border15 = new Border());
		((ISupportInitialize)border14).BeginInit();
		children11.Add(border14);
		Border border16 = (border4 = border15);
		context.PushParent(border4);
		Border border17 = border4;
		StyledProperty<IBrush?> backgroundProperty4 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border17, backgroundProperty4, binding4);
		border17.CornerRadius = new CornerRadius(4.0, 4.0, 4.0, 4.0);
		border17.Margin = new Thickness(8.0, 0.0, 0.0, 0.0);
		border17.Padding = new Thickness(4.0, 2.0, 4.0, 2.0);
		border17.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		MultiBinding multiBinding8 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding9 = multiBinding2;
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj11 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding9.Converter = (IMultiValueConverter)obj11;
		IList<IBinding> bindings7 = multiBinding9.Bindings;
		CompiledBindingExtension obj12 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002EMember_002CRootApp_002EClient_002ECoreDomain_002EIsApp_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item7 = obj12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings7.Add(item7);
		IList<IBinding> bindings8 = multiBinding9.Bindings;
		CompiledBindingExtension obj13 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EMenuIn_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item8 = obj13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings8.Add(item8);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(border17, isVisibleProperty3, multiBinding8);
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		border17.Child = textBlock10;
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		textBlock13.FontSize = 10.0;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, foregroundProperty2, binding5);
		StaticResourceExtension staticResourceExtension8 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj14 = staticResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock13, obj14);
		textBlock13.FontWeight = FontWeight.DemiBold;
		textBlock13.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.App;
		textBlock13.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		context.PopParent();
		((ISupportInitialize)border16).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel11).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel7).EndInit();
		Controls children12 = panel5.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children12.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		button4.Name = "UserProfileButton";
		obj2 = button4;
		context.AvaloniaNameScope.Register("UserProfileButton", obj2);
		button4.Classes.Add("TransparentButton");
		RootFlyout rootFlyout;
		RootFlyout flyout = (rootFlyout = new RootFlyout());
		context.PushParent(rootFlyout);
		rootFlyout.Placement = PlacementMode.RightEdgeAlignedTop;
		rootFlyout.VerticalOffset = -16.0;
		rootFlyout.HorizontalOffset = 8.0;
		rootFlyout.LimitSizeToWindow = false;
		StyledProperty<bool> isPopupOpenProperty = RootFlyout.IsPopupOpenProperty;
		CompiledBindingExtension compiledBindingExtension17 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EIsSelectedOrHighlighted_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootFlyout.IsPopupOpenProperty;
		CompiledBindingExtension compiledBindingExtension18 = compiledBindingExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootFlyout, isPopupOpenProperty, compiledBindingExtension18);
		rootFlyout.Opening += context.RootObject.onFlyoutOpening;
		rootFlyout.Closing += context.RootObject.onFlyoutClosing;
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		rootFlyout.Content = rootBorder;
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		StyledProperty<IBrush?> backgroundProperty5 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, backgroundProperty5, binding6);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, borderBrushProperty, binding7);
		rootBorder4.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder4.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		rootBorder4.Margin = new Thickness(12.0, 12.0, 12.0, 12.0);
		StyledProperty<BoxShadows> boxShadowProperty = Border.BoxShadowProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("PopupBoxShadow");
		context.ProvideTargetProperty = Border.BoxShadowProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, boxShadowProperty, binding8);
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		rootBorder4.Child = contentControl;
		ContentControl contentControl4;
		ContentControl contentControl3 = (contentControl4 = contentControl2);
		context.PushParent(contentControl4);
		CompiledBindingExtension compiledBindingExtension19 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EMemberProfile_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension20 = compiledBindingExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_4(contentControl4, compiledBindingExtension20);
		context.PopParent();
		((ISupportInitialize)contentControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		context.PopParent();
		button4.Flyout = flyout;
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
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
	private static void _0021XamlIlPopulateTrampoline(MemberView P_0)
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

