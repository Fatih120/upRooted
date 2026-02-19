// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.MemberProfileView
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
using Avalonia.Media.Immutable;
using Avalonia.Threading;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Controls.ContextMenus;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.Resources.Themes;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Client.CoreDomain.Models.Messages;
using RootApp.WebApi.Shared.Enums;

public class MemberProfileView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_80
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MemberProfileView> context = CreateContext(P_0);
			context.IntermediateRoot = new WrapPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((AvaloniaObject)obj).SetValue(WrapPanel.ItemSpacingProperty, 4.0, BindingPriority.Template);
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<MemberProfileView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MemberProfileView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MemberProfileView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FCommunity_002FMembers_002FMemberProfileView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Members/MemberProfileView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (MemberProfileView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MemberProfileView> context = CreateContext(P_0);
			context.IntermediateRoot = new RootSvgImage();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			RootSvgImage rootSvgImage = (RootSvgImage)obj;
			context.PushParent(rootSvgImage);
			RootSvgImage rootSvgImage2 = rootSvgImage;
			rootSvgImage2.Width = 24.0;
			rootSvgImage2.Height = 24.0;
			StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
			CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberBadgeDisplay_002CRootApp_002EClient_002EAvalonia_002ESvgPath_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
			context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
			CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(rootSvgImage2, svgPathProperty, compiledBindingExtension2);
			ToolTip.SetPlacement(rootSvgImage2, PlacementMode.Top);
			ToolTip.SetVerticalOffset(rootSvgImage2, -2.0);
			ToolTip.SetShowDelay(rootSvgImage2, 300);
			RootToolTip rootToolTip2;
			RootToolTip rootToolTip = (rootToolTip2 = new RootToolTip());
			((ISupportInitialize)rootToolTip).BeginInit();
			ToolTip.SetTip(rootSvgImage2, rootToolTip);
			RootToolTip rootToolTip4;
			RootToolTip rootToolTip3 = (rootToolTip4 = rootToolTip2);
			context.PushParent(rootToolTip4);
			ToolTip.SetPlacement(rootToolTip4, PlacementMode.Top);
			Grid grid2;
			Grid grid = (grid2 = new Grid());
			((ISupportInitialize)grid).BeginInit();
			rootToolTip4.Content = grid;
			Grid grid4;
			Grid grid3 = (grid4 = grid2);
			context.PushParent(grid4);
			grid4.MaxWidth = 300.0;
			ColumnDefinitions columnDefinitions = new ColumnDefinitions();
			columnDefinitions.Capacity = 3;
			columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
			columnDefinitions.Add(new ColumnDefinition(new GridLength(12.0, GridUnitType.Pixel)));
			columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
			grid4.ColumnDefinitions = columnDefinitions;
			Controls children = grid4.Children;
			RootSvgImage rootSvgImage4;
			RootSvgImage rootSvgImage3 = (rootSvgImage4 = new RootSvgImage());
			((ISupportInitialize)rootSvgImage3).BeginInit();
			children.Add(rootSvgImage3);
			RootSvgImage rootSvgImage5 = (rootSvgImage = rootSvgImage4);
			context.PushParent(rootSvgImage);
			RootSvgImage rootSvgImage6 = rootSvgImage;
			rootSvgImage6.Width = 36.0;
			rootSvgImage6.Height = 36.0;
			StyledProperty<string?> svgPathProperty2 = RootSvgImage.SvgPathProperty;
			CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberBadgeDisplay_002CRootApp_002EClient_002EAvalonia_002ESvgPath_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
			context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
			CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(rootSvgImage6, svgPathProperty2, compiledBindingExtension4);
			context.PopParent();
			((ISupportInitialize)rootSvgImage5).EndInit();
			Controls children2 = grid4.Children;
			StackPanel stackPanel2;
			StackPanel stackPanel = (stackPanel2 = new StackPanel());
			((ISupportInitialize)stackPanel).BeginInit();
			children2.Add(stackPanel);
			StackPanel stackPanel4;
			StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
			context.PushParent(stackPanel4);
			Grid.SetColumn(stackPanel4, 2);
			stackPanel4.Spacing = 2.0;
			stackPanel4.VerticalAlignment = VerticalAlignment.Center;
			Controls children3 = stackPanel4.Children;
			TextBlock textBlock2;
			TextBlock textBlock = (textBlock2 = new TextBlock());
			((ISupportInitialize)textBlock).BeginInit();
			children3.Add(textBlock);
			TextBlock textBlock4;
			TextBlock textBlock3 = (textBlock4 = textBlock2);
			context.PushParent(textBlock4);
			StyledProperty<string?> textProperty = TextBlock.TextProperty;
			CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberBadgeDisplay_002CRootApp_002EClient_002EAvalonia_002EName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
			context.ProvideTargetProperty = TextBlock.TextProperty;
			CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(textBlock4, textProperty, compiledBindingExtension6);
			textBlock4.HorizontalAlignment = HorizontalAlignment.Center;
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
			context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
			object? obj2 = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock4, obj2);
			textBlock4.FontWeight = FontWeight.DemiBold;
			textBlock4.FontSize = 14.0;
			StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
			DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
			context.ProvideTargetProperty = TextBlock.ForegroundProperty;
			IBinding binding = dynamicResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(textBlock4, foregroundProperty, binding);
			textBlock4.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
			context.PopParent();
			((ISupportInitialize)textBlock3).EndInit();
			context.PopParent();
			((ISupportInitialize)stackPanel3).EndInit();
			context.PopParent();
			((ISupportInitialize)grid3).EndInit();
			context.PopParent();
			((ISupportInitialize)rootToolTip3).EndInit();
			context.PopParent();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MemberProfileView> context = CreateContext(P_0);
			context.IntermediateRoot = new WrapPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((AvaloniaObject)obj).SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			((AvaloniaObject)obj).SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			((AvaloniaObject)obj).SetValue(WrapPanel.OrientationProperty, Orientation.Horizontal, BindingPriority.Template);
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static object Build_4(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MemberProfileView> context = CreateContext(P_0);
			context.IntermediateRoot = new WrapPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}
	}

	private Role? _primaryRole;

	private Member? _communityMember;

	private IMessageContainerMember? _messageContainerMember;

	private bool _isLoaded = false;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton MemberContextMenuButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border OnlineBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock UsernameTextBlock;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootTextbox NoteTextbox;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootTextbox MessageTextbox;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	private MemberProfileViewModel? _memberProfileViewModel => base.DataContext as MemberProfileViewModel;

	public MemberProfileView()
	{
		InitializeComponent();
	}

	protected override void OnDataContextChanged(EventArgs P_0)
	{
		base.OnDataContextChanged(P_0);
		unhookEventHandlers();
		if (_memberProfileViewModel != null)
		{
			if (_memberProfileViewModel.MessageContainerMember is Member communityMember)
			{
				_communityMember = communityMember;
				_messageContainerMember = null;
				_primaryRole = _communityMember.Roles.PrimaryRole;
			}
			else
			{
				_messageContainerMember = _memberProfileViewModel.MessageContainerMember;
				_communityMember = null;
				_primaryRole = null;
			}
		}
		else
		{
			_communityMember = null;
			_messageContainerMember = null;
			_primaryRole = null;
		}
		if (_isLoaded)
		{
			hookEventHandlers();
			if (_communityMember != null)
			{
				updateCommunityMemberOnlineStatusColor();
			}
			else if (_messageContainerMember != null)
			{
				updateDirectMessageMemberOnlineStatusColor();
				removeOnlineStatusCutout();
			}
			updateMemberColor();
		}
	}

	protected override void OnLoaded(RoutedEventArgs P_0)
	{
		base.OnLoaded(P_0);
		_isLoaded = true;
		MessageTextbox.FocusTextBox();
		MessageTextbox.MainTextBox.KeyDown += onMainTextBoxKeyDown;
		hookEventHandlers();
		if (_communityMember != null)
		{
			updateCommunityMemberOnlineStatusColor();
		}
		else if (_messageContainerMember != null)
		{
			updateDirectMessageMemberOnlineStatusColor();
			removeOnlineStatusCutout();
		}
		updateMemberColor();
	}

	protected override void OnUnloaded(RoutedEventArgs P_0)
	{
		base.OnUnloaded(P_0);
		_isLoaded = false;
		MessageTextbox.MainTextBox.KeyDown -= onMainTextBoxKeyDown;
		unhookEventHandlers();
		if (_memberProfileViewModel != null && _memberProfileViewModel.UnloadedCommand.CanExecute(null))
		{
			_memberProfileViewModel.UnloadedCommand.Execute(null);
		}
	}

	private void hookEventHandlers()
	{
		if (_memberProfileViewModel == null)
		{
			return;
		}
		if (_communityMember != null)
		{
			_communityMember.PropertyChanged += onCommunityMemberPropertyChanged;
			_communityMember.Roles.PropertyChanged += onRolesCollectionPropertyChanged;
			if (_primaryRole != null)
			{
				_primaryRole.PropertyChanged += onPrimaryRolePropertyChanged;
			}
		}
		else if (_messageContainerMember != null)
		{
			_messageContainerMember.GlobalUser.PropertyChanged += onDirectMessageMemberPropertyChanged;
		}
	}

	private void unhookEventHandlers()
	{
		if (_memberProfileViewModel == null)
		{
			return;
		}
		if (_communityMember != null)
		{
			_communityMember.PropertyChanged -= onCommunityMemberPropertyChanged;
			_communityMember.Roles.PropertyChanged -= onRolesCollectionPropertyChanged;
			if (_primaryRole != null)
			{
				_primaryRole.PropertyChanged -= onPrimaryRolePropertyChanged;
			}
		}
		else if (_messageContainerMember != null)
		{
			_messageContainerMember.GlobalUser.PropertyChanged -= onDirectMessageMemberPropertyChanged;
		}
	}

	private void onCommunityMemberPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "CommunityOnlineStatus")
			{
				updateCommunityMemberOnlineStatusColor();
			}
		});
	}

	private void onDirectMessageMemberPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "OnlineStatus")
			{
				updateDirectMessageMemberOnlineStatusColor();
			}
		});
	}

	private void onRolesCollectionPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (_communityMember != null && e.PropertyName == "PrimaryRole")
			{
				if (_primaryRole != null)
				{
					_primaryRole.PropertyChanged -= onPrimaryRolePropertyChanged;
				}
				_primaryRole = _communityMember.Roles.PrimaryRole;
				if (_primaryRole != null)
				{
					_primaryRole.PropertyChanged += onPrimaryRolePropertyChanged;
				}
				updateMemberColor();
			}
		});
	}

	private void onPrimaryRolePropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "RoleColorHex")
			{
				updateMemberColor();
			}
		});
	}

	private void updateMemberColor()
	{
		if (_primaryRole != null)
		{
			if (string.IsNullOrEmpty(_primaryRole.RoleColorHex))
			{
				UsernameTextBlock[!TemplatedControl.ForegroundProperty] = new DynamicResourceExtension("TextPrimary");
			}
			else if (ThemeService.IsDefaultColor(_primaryRole.RoleColorHex))
			{
				UsernameTextBlock.Foreground = new SolidColorBrush(Color.Parse(ThemeService.GetInvertedDefaultColorHex(_primaryRole.RoleColorHex)));
			}
			else
			{
				UsernameTextBlock.Foreground = new SolidColorBrush(Color.Parse(_primaryRole.RoleColorHex));
			}
		}
		else
		{
			UsernameTextBlock[!TemplatedControl.ForegroundProperty] = new DynamicResourceExtension("TextPrimary");
		}
	}

	private void updateCommunityMemberOnlineStatusColor()
	{
		switch (_communityMember?.CommunityOnlineStatus)
		{
		case CommunityMemberOnlineStatus.OnlineAndAttached:
			OnlineBorder[!TemplatedControl.BackgroundProperty] = new DynamicResourceExtension("BrandSecondary");
			removeOnlineStatusCutout();
			break;
		case CommunityMemberOnlineStatus.Online:
			OnlineBorder[!TemplatedControl.BackgroundProperty] = new DynamicResourceExtension("BrandSecondary");
			createOnlineStatusCutout();
			break;
		case CommunityMemberOnlineStatus.AwayAndAttached:
			OnlineBorder[!TemplatedControl.BackgroundProperty] = new DynamicResourceExtension("Warning");
			removeOnlineStatusCutout();
			break;
		case CommunityMemberOnlineStatus.Away:
			OnlineBorder[!TemplatedControl.BackgroundProperty] = new DynamicResourceExtension("Warning");
			createOnlineStatusCutout();
			break;
		case null:
		case CommunityMemberOnlineStatus.Offline:
			OnlineBorder[!TemplatedControl.BackgroundProperty] = new DynamicResourceExtension("Muted");
			removeOnlineStatusCutout();
			break;
		}
	}

	private void updateDirectMessageMemberOnlineStatusColor()
	{
		switch (_messageContainerMember?.GlobalUser.OnlineStatus)
		{
		case UserOnlineStatus.Active:
			OnlineBorder[!TemplatedControl.BackgroundProperty] = new DynamicResourceExtension("BrandSecondary");
			break;
		case UserOnlineStatus.Inactive:
			OnlineBorder[!TemplatedControl.BackgroundProperty] = new DynamicResourceExtension("Warning");
			break;
		case null:
		case UserOnlineStatus.Disconnected:
			OnlineBorder[!TemplatedControl.BackgroundProperty] = new DynamicResourceExtension("Muted");
			break;
		}
	}

	private void createOnlineStatusCutout()
	{
		if (OnlineBorder.Clip == null)
		{
			GeometryGroup geometryGroup = new GeometryGroup();
			RectangleGeometry rectangleGeometry = new RectangleGeometry(new Rect(0.0, 0.0, 10.0, 10.0));
			EllipseGeometry ellipseGeometry = new EllipseGeometry
			{
				Center = new Point(5.0, 5.0),
				RadiusX = 2.0,
				RadiusY = 2.0
			};
			geometryGroup.Children.Add(rectangleGeometry);
			geometryGroup.Children.Add(ellipseGeometry);
			OnlineBorder.Clip = geometryGroup;
		}
	}

	private void removeOnlineStatusCutout()
	{
		if (OnlineBorder.Clip != null)
		{
			OnlineBorder.Clip = null;
		}
	}

	private void onMainTextBoxKeyDown(object? sender, KeyEventArgs e)
	{
		if (e.Key == Key.Return && _memberProfileViewModel != null && _memberProfileViewModel.SendMessageCommand.CanExecute(MessageTextbox.Text))
		{
			_memberProfileViewModel.SendMessageCommand.Execute(MessageTextbox.Text);
		}
	}

	private void memberContextMenuClosing(object? sender, CancelEventArgs e)
	{
		if (_memberProfileViewModel != null && MemberContextMenuButton.Flyout is UserContextMenuView { DataContext: UserContextMenuViewModel { CommandExecuted: not false } } && _memberProfileViewModel.CloseProfileCallbackCommand.CanExecute(null))
		{
			_memberProfileViewModel.CloseProfileCallbackCommand.Execute(null);
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
		MemberContextMenuButton = nameScope?.Find<RootSvgButton>("MemberContextMenuButton");
		OnlineBorder = nameScope?.Find<Border>("OnlineBorder");
		UsernameTextBlock = nameScope?.Find<TextBlock>("UsernameTextBlock");
		NoteTextbox = nameScope?.Find<RootTextbox>("NoteTextbox");
		MessageTextbox = nameScope?.Find<RootTextbox>("MessageTextbox");
	}

	[CompilerGenerated]
	private unsafe static void _0021XamlIlPopulate(IServiceProvider P_0, MemberProfileView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<MemberProfileView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MemberProfileView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FCommunity_002FMembers_002FMemberProfileView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Members/MemberProfileView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		P_1.Content = panel;
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		panel4.Width = 305.0;
		panel4.ClipToBounds = true;
		Controls children = panel4.Children;
		RootSvgButton rootSvgButton2;
		RootSvgButton rootSvgButton = (rootSvgButton2 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton).BeginInit();
		children.Add(rootSvgButton);
		RootSvgButton rootSvgButton4;
		RootSvgButton rootSvgButton3 = (rootSvgButton4 = rootSvgButton2);
		context.PushParent(rootSvgButton4);
		rootSvgButton4.Name = "MemberContextMenuButton";
		object obj = rootSvgButton4;
		context.AvaloniaNameScope.Register("MemberContextMenuButton", obj);
		rootSvgButton4.HorizontalAlignment = HorizontalAlignment.Right;
		rootSvgButton4.VerticalAlignment = VerticalAlignment.Top;
		rootSvgButton4.Classes.Add("SvgDimmedButton");
		rootSvgButton4.Margin = new Thickness(0.0, 12.0, 12.0, 0.0);
		rootSvgButton4.Width = 32.0;
		rootSvgButton4.Height = 32.0;
		rootSvgButton4.SvgWidth = 4.0;
		rootSvgButton4.SvgHeight = 16.0;
		StyledProperty<string> svgPathProperty = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("EllipsisVerticalSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton4, svgPathProperty, binding);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002EShowContextMenuButton_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton4, isVisibleProperty, compiledBindingExtension2);
		UserContextMenuView userContextMenuView;
		UserContextMenuView flyout = (userContextMenuView = new UserContextMenuView());
		context.PushParent(userContextMenuView);
		userContextMenuView.Placement = PlacementMode.RightEdgeAlignedTop;
		userContextMenuView.Closing += context.RootObject.memberContextMenuClosing;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002EUserContextMenu_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.System_002ELazy_00601_002CSystem_002ERuntime_002EValue_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = UserContextMenuView.DataContextProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_22(userContextMenuView, compiledBindingExtension4);
		context.PopParent();
		rootSvgButton4.Flyout = flyout;
		context.PopParent();
		((ISupportInitialize)rootSvgButton3).EndInit();
		Controls children2 = panel4.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children2.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		stackPanel5.Margin = new Thickness(24.0, 28.0, 24.0, 20.0);
		Controls children3 = stackPanel5.Children;
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		children3.Add(grid);
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		Grid grid5 = grid4;
		grid5.VerticalAlignment = VerticalAlignment.Center;
		grid5.HorizontalAlignment = HorizontalAlignment.Center;
		Controls children4 = grid5.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children4.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Border border5 = border4;
		GeometryGroup geometryGroup = new GeometryGroup();
		geometryGroup.Children.Add(new RectangleGeometry
		{
			Rect = Rect.Parse("0,0,60,60")
		});
		GeometryCollection children5 = geometryGroup.Children;
		EllipseGeometry ellipseGeometry = new EllipseGeometry();
		ellipseGeometry.Center = new Point(57.0, 57.0);
		ellipseGeometry.RadiusX = 8.0;
		ellipseGeometry.RadiusY = 8.0;
		children5.Add(ellipseGeometry);
		border5.Clip = geometryGroup;
		RootImageLoader rootImageLoader2;
		RootImageLoader rootImageLoader = (rootImageLoader2 = new RootImageLoader());
		((ISupportInitialize)rootImageLoader).BeginInit();
		border5.Child = rootImageLoader;
		RootImageLoader rootImageLoader4;
		RootImageLoader rootImageLoader3 = (rootImageLoader4 = rootImageLoader2);
		context.PushParent(rootImageLoader4);
		rootImageLoader4.Width = 60.0;
		rootImageLoader4.Height = 60.0;
		rootImageLoader4.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<IBrush?> backgroundProperty = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, backgroundProperty, binding2);
		StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002EProfilePictureAsyncBitmapWrapper_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
		context.ProvideTargetProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, sourceProperty, compiledBindingExtension6);
		rootImageLoader4.LoadingPlaceholderSize = 16.0;
		rootImageLoader4.Stretch = Stretch.UniformToFill;
		context.PopParent();
		((ISupportInitialize)rootImageLoader3).EndInit();
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		Controls children6 = grid5.Children;
		Border border7;
		Border border6 = (border7 = new Border());
		((ISupportInitialize)border6).BeginInit();
		children6.Add(border6);
		border7.Name = "OnlineBorder";
		obj = border7;
		context.AvaloniaNameScope.Register("OnlineBorder", obj);
		border7.Width = 10.0;
		border7.Height = 10.0;
		border7.CornerRadius = new CornerRadius(10.0, 10.0, 10.0, 10.0);
		border7.Margin = new Thickness(0.0, 0.0, -2.0, -2.0);
		border7.HorizontalAlignment = HorizontalAlignment.Right;
		border7.VerticalAlignment = VerticalAlignment.Bottom;
		GeometryGroup geometryGroup2 = new GeometryGroup();
		geometryGroup2.Children.Add(new RectangleGeometry
		{
			Rect = Rect.Parse("0,0,10,10")
		});
		GeometryCollection children7 = geometryGroup2.Children;
		EllipseGeometry ellipseGeometry2 = new EllipseGeometry();
		ellipseGeometry2.Center = new Point(5.0, 5.0);
		ellipseGeometry2.RadiusX = 2.0;
		ellipseGeometry2.RadiusY = 2.0;
		children7.Add(ellipseGeometry2);
		border7.Clip = geometryGroup2;
		((ISupportInitialize)border7).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		Controls children8 = stackPanel5.Children;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		children8.Add(stackPanel6);
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		stackPanel9.Orientation = Orientation.Horizontal;
		stackPanel9.Margin = new Thickness(0.0, 12.0, 0.0, 4.0);
		stackPanel9.HorizontalAlignment = HorizontalAlignment.Center;
		Controls children9 = stackPanel9.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children9.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Name = "UsernameTextBlock";
		obj = textBlock5;
		context.AvaloniaNameScope.Register("UsernameTextBlock", obj);
		textBlock5.FontSize = 16.0;
		textBlock5.Foreground = new ImmutableSolidColorBrush(16777215u);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj2);
		textBlock5.FontWeight = FontWeight.Medium;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002EMessageContainerMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainerMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EUserName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, compiledBindingExtension8);
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		Controls children10 = stackPanel9.Children;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		children10.Add(rootSvgImage);
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage5 = rootSvgImage4;
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunityMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002EMember_002CRootApp_002EClient_002ECoreDomain_002EIsOwner_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			FallbackValue = "False"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension9 = obj3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage5, isVisibleProperty2, compiledBindingExtension9);
		rootSvgImage5.Margin = new Thickness(8.0, 0.0, 0.0, 0.0);
		rootSvgImage5.Width = 15.0;
		rootSvgImage5.Height = 15.0;
		StyledProperty<string?> svgPathProperty2 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("OwnerSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage5, svgPathProperty2, binding3);
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		Controls children11 = stackPanel9.Children;
		Border border9;
		Border border8 = (border9 = new Border());
		((ISupportInitialize)border8).BeginInit();
		children11.Add(border8);
		Border border10 = (border4 = border9);
		context.PushParent(border4);
		Border border11 = border4;
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunityMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002EMember_002CRootApp_002EClient_002ECoreDomain_002EIsApp_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			FallbackValue = "False"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension10 = obj4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border11, isVisibleProperty3, compiledBindingExtension10);
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border11, backgroundProperty2, binding4);
		border11.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		border11.Margin = new Thickness(8.0, 0.0, 0.0, 0.0);
		border11.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		border11.Child = textBlock6;
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.FontSize = 13.0;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty, binding5);
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj5 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj5);
		textBlock9.FontWeight = FontWeight.Medium;
		textBlock9.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.App;
		textBlock9.VerticalAlignment = VerticalAlignment.Center;
		textBlock9.Margin = new Thickness(4.0, 3.0, 4.0, 3.0);
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		context.PopParent();
		((ISupportInitialize)border10).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		Controls children12 = stackPanel5.Children;
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		children12.Add(rootBorder);
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		rootBorder4.VerticalAlignment = VerticalAlignment.Center;
		rootBorder4.HorizontalAlignment = HorizontalAlignment.Center;
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension12;
		CompiledBindingExtension compiledBindingExtension11 = (compiledBindingExtension12 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002EBadgeDisplays_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.System_002ECollections_002EObjectModel_002ECollection_00601_002CSystem_002ERuntime_002ECount_79_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension12);
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("GreaterThanZeroToTrueConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj6 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension12.Converter = (IValueConverter)obj6;
		context.PopParent();
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension13 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, isVisibleProperty4, compiledBindingExtension13);
		StyledProperty<IBrush?> backgroundProperty3 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("HighlightLight");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, backgroundProperty3, binding6);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, borderBrushProperty, binding7);
		rootBorder4.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder4.Padding = new Thickness(4.0, 4.0, 4.0, 4.0);
		rootBorder4.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		ItemsControl itemsControl2;
		ItemsControl itemsControl = (itemsControl2 = new ItemsControl());
		((ISupportInitialize)itemsControl).BeginInit();
		rootBorder4.Child = itemsControl;
		ItemsControl itemsControl4;
		ItemsControl itemsControl3 = (itemsControl4 = itemsControl2);
		context.PushParent(itemsControl4);
		ItemsControl itemsControl5 = itemsControl4;
		StyledProperty<IEnumerable?> itemsSourceProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension14 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002EBadgeDisplays_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension15 = compiledBindingExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl5, itemsSourceProperty, compiledBindingExtension15);
		itemsControl5.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_80.Build_1), context)
		};
		DataTemplate dataTemplate;
		DataTemplate itemTemplate = (dataTemplate = new DataTemplate());
		context.PushParent(dataTemplate);
		dataTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_80.Build_2), context);
		context.PopParent();
		itemsControl5.ItemTemplate = itemTemplate;
		context.PopParent();
		((ISupportInitialize)itemsControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		Controls children13 = stackPanel5.Children;
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		children13.Add(grid6);
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		grid9.HorizontalAlignment = HorizontalAlignment.Center;
		grid9.VerticalAlignment = VerticalAlignment.Center;
		grid9.Margin = new Thickness(0.0, 6.0, 0.0, 0.0);
		StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
		MultiBinding multiBinding2;
		MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding3 = multiBinding2;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj7 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding3.Converter = (IMultiValueConverter)obj7;
		IList<IBinding> bindings = multiBinding3.Bindings;
		CompiledBindingExtension obj8 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002EIsSelf_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item = obj8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings.Add(item);
		IList<IBinding> bindings2 = multiBinding3.Bindings;
		CompiledBindingExtension compiledBindingExtension16 = new CompiledBindingExtension();
		compiledBindingExtension16.Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunityMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002EMember_002CRootApp_002EClient_002ECoreDomain_002EIsApp_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
			.Build();
		compiledBindingExtension16.FallbackValue = "True";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item2 = compiledBindingExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings2.Add(item2);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(grid9, isVisibleProperty5, multiBinding);
		Controls children14 = grid9.Children;
		ItemsControl itemsControl7;
		ItemsControl itemsControl6 = (itemsControl7 = new ItemsControl());
		((ISupportInitialize)itemsControl6).BeginInit();
		children14.Add(itemsControl6);
		ItemsControl itemsControl8 = (itemsControl4 = itemsControl7);
		context.PushParent(itemsControl4);
		ItemsControl itemsControl9 = itemsControl4;
		itemsControl9.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_80.Build_3), context)
		};
		ItemCollection items = itemsControl9.Items;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		items.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		Button button5 = button4;
		button5.Classes.Add("BasicButton");
		StyledProperty<IBrush?> backgroundProperty4 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, backgroundProperty4, binding8);
		button5.BorderBrush = new ImmutableSolidColorBrush(16777215u);
		button5.Padding = new Thickness(14.0, 6.0, 14.0, 6.0);
		button5.Margin = new Thickness(6.0, 6.0, 0.0, 0.0);
		button5.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button5.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension17 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002ECallCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension18 = compiledBindingExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, commandProperty, compiledBindingExtension18);
		StyledProperty<bool> isVisibleProperty6 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunityMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002EMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EIsBlocked_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "True"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension19 = obj9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, isVisibleProperty6, compiledBindingExtension19);
		ToolTip.SetPlacement(button5, PlacementMode.Top);
		ToolTip.SetVerticalOffset(button5, -1.0);
		ToolTip.SetHorizontalOffset(button5, 0.0);
		ToolTip.SetShowDelay(button5, 0);
		RootToolTip rootToolTip2;
		RootToolTip rootToolTip = (rootToolTip2 = new RootToolTip());
		((ISupportInitialize)rootToolTip).BeginInit();
		ToolTip.SetTip(button5, rootToolTip);
		RootToolTip rootToolTip4;
		RootToolTip rootToolTip3 = (rootToolTip4 = rootToolTip2);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip5 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip5, PlacementMode.Top);
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		rootToolTip5.Content = textBlock10;
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		textBlock13.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Call;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj10 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock13, obj10);
		textBlock13.FontWeight = (FontWeight)450;
		textBlock13.FontSize = 14.0;
		textBlock13.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock13.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip3).EndInit();
		RootSvgImage rootSvgImage7;
		RootSvgImage rootSvgImage6 = (rootSvgImage7 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage6).BeginInit();
		button5.Content = rootSvgImage6;
		RootSvgImage rootSvgImage8 = (rootSvgImage4 = rootSvgImage7);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage9 = rootSvgImage4;
		rootSvgImage9.Width = 16.0;
		rootSvgImage9.Height = 16.0;
		rootSvgImage9.Opacity = 0.64;
		StyledProperty<string?> svgPathProperty3 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("CallSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage9, svgPathProperty3, binding9);
		context.PopParent();
		((ISupportInitialize)rootSvgImage8).EndInit();
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		ItemCollection items2 = itemsControl9.Items;
		Button button7;
		Button button6 = (button7 = new Button());
		((ISupportInitialize)button6).BeginInit();
		items2.Add(button6);
		Button button8 = (button4 = button7);
		context.PushParent(button4);
		Button button9 = button4;
		button9.Classes.Add("BasicButton");
		StyledProperty<IBrush?> backgroundProperty5 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, backgroundProperty5, binding10);
		button9.BorderBrush = new ImmutableSolidColorBrush(16777215u);
		button9.Padding = new Thickness(14.0, 6.0, 14.0, 6.0);
		button9.Margin = new Thickness(6.0, 6.0, 0.0, 0.0);
		button9.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button9.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		StyledProperty<ICommand?> commandProperty2 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension20 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002ESendMessageCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension21 = compiledBindingExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, commandProperty2, compiledBindingExtension21);
		StyledProperty<bool> isVisibleProperty7 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunityMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002EMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EIsBlocked_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "True"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension22 = obj11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, isVisibleProperty7, compiledBindingExtension22);
		ToolTip.SetPlacement(button9, PlacementMode.Top);
		ToolTip.SetVerticalOffset(button9, -1.0);
		ToolTip.SetHorizontalOffset(button9, 0.0);
		ToolTip.SetShowDelay(button9, 0);
		RootToolTip rootToolTip7;
		RootToolTip rootToolTip6 = (rootToolTip7 = new RootToolTip());
		((ISupportInitialize)rootToolTip6).BeginInit();
		ToolTip.SetTip(button9, rootToolTip6);
		RootToolTip rootToolTip8 = (rootToolTip4 = rootToolTip7);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip9 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip9, PlacementMode.Top);
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		rootToolTip9.Content = textBlock14;
		TextBlock textBlock16 = (textBlock4 = textBlock15);
		context.PushParent(textBlock4);
		TextBlock textBlock17 = textBlock4;
		textBlock17.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.SendMessage;
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj12 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock17, obj12);
		textBlock17.FontWeight = (FontWeight)450;
		textBlock17.FontSize = 14.0;
		textBlock17.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock17.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock16).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip8).EndInit();
		RootSvgImage rootSvgImage11;
		RootSvgImage rootSvgImage10 = (rootSvgImage11 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage10).BeginInit();
		button9.Content = rootSvgImage10;
		RootSvgImage rootSvgImage12 = (rootSvgImage4 = rootSvgImage11);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage13 = rootSvgImage4;
		rootSvgImage13.Width = 16.0;
		rootSvgImage13.Height = 16.0;
		rootSvgImage13.Opacity = 0.64;
		StyledProperty<string?> svgPathProperty4 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("SystemTrayDirectMessagesSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage13, svgPathProperty4, binding11);
		context.PopParent();
		((ISupportInitialize)rootSvgImage12).EndInit();
		context.PopParent();
		((ISupportInitialize)button8).EndInit();
		ItemCollection items3 = itemsControl9.Items;
		Button button11;
		Button button10 = (button11 = new Button());
		((ISupportInitialize)button10).BeginInit();
		items3.Add(button10);
		Button button12 = (button4 = button11);
		context.PushParent(button4);
		Button button13 = button4;
		button13.Classes.Add("BasicButton");
		StyledProperty<IBrush?> backgroundProperty6 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding12 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button13, backgroundProperty6, binding12);
		button13.BorderBrush = new ImmutableSolidColorBrush(16777215u);
		button13.Padding = new Thickness(14.0, 6.0, 14.0, 6.0);
		button13.Margin = new Thickness(6.0, 6.0, 0.0, 0.0);
		button13.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button13.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		StyledProperty<ICommand?> commandProperty3 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension23 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002ESendFriendRequestCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension24 = compiledBindingExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button13, commandProperty3, compiledBindingExtension24);
		ToolTip.SetPlacement(button13, PlacementMode.Top);
		ToolTip.SetVerticalOffset(button13, -1.0);
		ToolTip.SetHorizontalOffset(button13, 0.0);
		ToolTip.SetShowDelay(button13, 0);
		RootToolTip rootToolTip11;
		RootToolTip rootToolTip10 = (rootToolTip11 = new RootToolTip());
		((ISupportInitialize)rootToolTip10).BeginInit();
		ToolTip.SetTip(button13, rootToolTip10);
		RootToolTip rootToolTip12 = (rootToolTip4 = rootToolTip11);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip13 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip13, PlacementMode.Top);
		TextBlock textBlock19;
		TextBlock textBlock18 = (textBlock19 = new TextBlock());
		((ISupportInitialize)textBlock18).BeginInit();
		rootToolTip13.Content = textBlock18;
		TextBlock textBlock20 = (textBlock4 = textBlock19);
		context.PushParent(textBlock4);
		TextBlock textBlock21 = textBlock4;
		textBlock21.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.SendFriendRequest;
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj13 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock21, obj13);
		textBlock21.FontWeight = (FontWeight)450;
		textBlock21.FontSize = 14.0;
		textBlock21.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock21.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock20).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip12).EndInit();
		StyledProperty<bool> isVisibleProperty8 = Visual.IsVisibleProperty;
		MultiBinding multiBinding4 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding5 = multiBinding2;
		StaticResourceExtension staticResourceExtension8 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj14 = staticResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding5.Converter = (IMultiValueConverter)obj14;
		IList<IBinding> bindings3 = multiBinding5.Bindings;
		CompiledBindingExtension obj15 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunityMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002EMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EIsFriend_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item3 = obj15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings3.Add(item3);
		IList<IBinding> bindings4 = multiBinding5.Bindings;
		CompiledBindingExtension obj16 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002EFriendRequestSent_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item4 = obj16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings4.Add(item4);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(button13, isVisibleProperty8, multiBinding4);
		RootSvgImage rootSvgImage15;
		RootSvgImage rootSvgImage14 = (rootSvgImage15 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage14).BeginInit();
		button13.Content = rootSvgImage14;
		RootSvgImage rootSvgImage16 = (rootSvgImage4 = rootSvgImage15);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage17 = rootSvgImage4;
		rootSvgImage17.Width = 18.0;
		rootSvgImage17.Height = 18.0;
		rootSvgImage17.Opacity = 0.64;
		StyledProperty<string?> svgPathProperty5 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("AddUserSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding13 = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage17, svgPathProperty5, binding13);
		context.PopParent();
		((ISupportInitialize)rootSvgImage16).EndInit();
		context.PopParent();
		((ISupportInitialize)button12).EndInit();
		context.PopParent();
		((ISupportInitialize)itemsControl8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		Controls children15 = stackPanel5.Children;
		Rectangle rectangle2;
		Rectangle rectangle = (rectangle2 = new Rectangle());
		((ISupportInitialize)rectangle).BeginInit();
		children15.Add(rectangle);
		Rectangle rectangle4;
		Rectangle rectangle3 = (rectangle4 = rectangle2);
		context.PushParent(rectangle4);
		rectangle4.Height = 0.5;
		StyledProperty<IBrush?> fillProperty = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding14 = dynamicResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle4, fillProperty, binding14);
		rectangle4.Margin = new Thickness(0.0, 12.0, 0.0, 12.0);
		context.PopParent();
		((ISupportInitialize)rectangle3).EndInit();
		Controls children16 = stackPanel5.Children;
		StackPanel stackPanel11;
		StackPanel stackPanel10 = (stackPanel11 = new StackPanel());
		((ISupportInitialize)stackPanel10).BeginInit();
		children16.Add(stackPanel10);
		StackPanel stackPanel12 = (stackPanel4 = stackPanel11);
		context.PushParent(stackPanel4);
		StackPanel stackPanel13 = stackPanel4;
		StyledProperty<bool> isVisibleProperty9 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj17 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunityMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002EMember_002CRootApp_002EClient_002ECoreDomain_002EIsMemberOfCommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			FallbackValue = "False"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension25 = obj17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(stackPanel13, isVisibleProperty9, compiledBindingExtension25);
		Controls children17 = stackPanel13.Children;
		StackPanel stackPanel15;
		StackPanel stackPanel14 = (stackPanel15 = new StackPanel());
		((ISupportInitialize)stackPanel14).BeginInit();
		children17.Add(stackPanel14);
		StackPanel stackPanel16 = (stackPanel4 = stackPanel15);
		context.PushParent(stackPanel4);
		StackPanel stackPanel17 = stackPanel4;
		stackPanel17.Orientation = Orientation.Horizontal;
		stackPanel17.Margin = new Thickness(0.0, 0.0, 0.0, 8.0);
		Controls children18 = stackPanel17.Children;
		TextBlock textBlock23;
		TextBlock textBlock22 = (textBlock23 = new TextBlock());
		((ISupportInitialize)textBlock22).BeginInit();
		children18.Add(textBlock22);
		TextBlock textBlock24 = (textBlock4 = textBlock23);
		context.PushParent(textBlock4);
		TextBlock textBlock25 = textBlock4;
		textBlock25.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Roles;
		StaticResourceExtension staticResourceExtension9 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj18 = staticResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock25, obj18);
		textBlock25.FontWeight = (FontWeight)450;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension15 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding15 = dynamicResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock25, foregroundProperty2, binding15);
		textBlock25.FontSize = 13.0;
		textBlock25.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock25.VerticalAlignment = VerticalAlignment.Center;
		textBlock25.HorizontalAlignment = HorizontalAlignment.Left;
		context.PopParent();
		((ISupportInitialize)textBlock24).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel16).EndInit();
		Controls children19 = stackPanel13.Children;
		ItemsControl itemsControl11;
		ItemsControl itemsControl10 = (itemsControl11 = new ItemsControl());
		((ISupportInitialize)itemsControl10).BeginInit();
		children19.Add(itemsControl10);
		ItemsControl itemsControl12 = (itemsControl4 = itemsControl11);
		context.PushParent(itemsControl4);
		ItemsControl itemsControl13 = itemsControl4;
		StyledProperty<IEnumerable?> itemsSourceProperty2 = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension26 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002ERoles_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension27 = compiledBindingExtension26.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl13, itemsSourceProperty2, compiledBindingExtension27);
		itemsControl13.Margin = new Thickness(0.0, 0.0, 0.0, 12.0);
		itemsControl13.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_80.Build_4), context)
		};
		context.PopParent();
		((ISupportInitialize)itemsControl12).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel12).EndInit();
		Controls children20 = stackPanel5.Children;
		StackPanel stackPanel19;
		StackPanel stackPanel18 = (stackPanel19 = new StackPanel());
		((ISupportInitialize)stackPanel18).BeginInit();
		children20.Add(stackPanel18);
		StackPanel stackPanel20 = (stackPanel4 = stackPanel19);
		context.PushParent(stackPanel4);
		StackPanel stackPanel21 = stackPanel4;
		StyledProperty<bool> isVisibleProperty10 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension28 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002EShouldHideNotes_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension29 = compiledBindingExtension28.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(stackPanel21, isVisibleProperty10, compiledBindingExtension29);
		Controls children21 = stackPanel21.Children;
		TextBlock textBlock27;
		TextBlock textBlock26 = (textBlock27 = new TextBlock());
		((ISupportInitialize)textBlock26).BeginInit();
		children21.Add(textBlock26);
		TextBlock textBlock28 = (textBlock4 = textBlock27);
		context.PushParent(textBlock4);
		TextBlock textBlock29 = textBlock4;
		textBlock29.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Notes;
		StaticResourceExtension staticResourceExtension10 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj19 = staticResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock29, obj19);
		textBlock29.FontWeight = (FontWeight)450;
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension16 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding16 = dynamicResourceExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock29, foregroundProperty3, binding16);
		textBlock29.FontSize = 13.0;
		textBlock29.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock29.VerticalAlignment = VerticalAlignment.Center;
		textBlock29.HorizontalAlignment = HorizontalAlignment.Left;
		textBlock29.Margin = new Thickness(0.0, 0.0, 0.0, 8.0);
		context.PopParent();
		((ISupportInitialize)textBlock28).EndInit();
		Controls children22 = stackPanel21.Children;
		RootTextbox rootTextbox2;
		RootTextbox rootTextbox = (rootTextbox2 = new RootTextbox());
		((ISupportInitialize)rootTextbox).BeginInit();
		children22.Add(rootTextbox);
		RootTextbox rootTextbox4;
		RootTextbox rootTextbox3 = (rootTextbox4 = rootTextbox2);
		context.PushParent(rootTextbox4);
		RootTextbox rootTextbox5 = rootTextbox4;
		rootTextbox5.Name = "NoteTextbox";
		obj = rootTextbox5;
		context.AvaloniaNameScope.Register("NoteTextbox", obj);
		rootTextbox5.PlaceholderText = RootApp.Client.Avalonia.Resources.Strings.Resources.ClickHereToAddANote;
		StyledProperty<string> textProperty2 = RootTextbox.TextProperty;
		CompiledBindingExtension obj20 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002ENote_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = RootTextbox.TextProperty;
		CompiledBindingExtension compiledBindingExtension30 = obj20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox5, textProperty2, compiledBindingExtension30);
		rootTextbox5.TextWrapping = TextWrapping.Wrap;
		rootTextbox5.TextboxFontSize = 13.0;
		rootTextbox5.MaxLength = 256;
		context.PopParent();
		((ISupportInitialize)rootTextbox3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel20).EndInit();
		Controls children23 = stackPanel5.Children;
		StackPanel stackPanel23;
		StackPanel stackPanel22 = (stackPanel23 = new StackPanel());
		((ISupportInitialize)stackPanel22).BeginInit();
		children23.Add(stackPanel22);
		StackPanel stackPanel24 = (stackPanel4 = stackPanel23);
		context.PushParent(stackPanel4);
		StackPanel stackPanel25 = stackPanel4;
		StyledProperty<bool> isVisibleProperty11 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension31 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002EShouldHideNotes_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension32 = compiledBindingExtension31.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(stackPanel25, isVisibleProperty11, compiledBindingExtension32);
		Controls children24 = stackPanel25.Children;
		TextBlock textBlock31;
		TextBlock textBlock30 = (textBlock31 = new TextBlock());
		((ISupportInitialize)textBlock30).BeginInit();
		children24.Add(textBlock30);
		TextBlock textBlock32 = (textBlock4 = textBlock31);
		context.PushParent(textBlock4);
		TextBlock textBlock33 = textBlock4;
		textBlock33.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Notes;
		StaticResourceExtension staticResourceExtension11 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj21 = staticResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock33, obj21);
		textBlock33.FontWeight = (FontWeight)450;
		StyledProperty<IBrush?> foregroundProperty4 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension17 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding17 = dynamicResourceExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock33, foregroundProperty4, binding17);
		textBlock33.FontSize = 13.0;
		textBlock33.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock33.VerticalAlignment = VerticalAlignment.Center;
		textBlock33.HorizontalAlignment = HorizontalAlignment.Left;
		textBlock33.Margin = new Thickness(0.0, 0.0, 0.0, 8.0);
		context.PopParent();
		((ISupportInitialize)textBlock32).EndInit();
		Controls children25 = stackPanel25.Children;
		TextBlock textBlock35;
		TextBlock textBlock34 = (textBlock35 = new TextBlock());
		((ISupportInitialize)textBlock34).BeginInit();
		children25.Add(textBlock34);
		TextBlock textBlock36 = (textBlock4 = textBlock35);
		context.PushParent(textBlock4);
		TextBlock textBlock37 = textBlock4;
		textBlock37.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.NotesHiddenStreamerMode;
		StaticResourceExtension staticResourceExtension12 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj22 = staticResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock37, obj22);
		textBlock37.FontWeight = (FontWeight)450;
		textBlock37.FontStyle = FontStyle.Italic;
		StyledProperty<IBrush?> foregroundProperty5 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension18 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding18 = dynamicResourceExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock37, foregroundProperty5, binding18);
		textBlock37.FontSize = 13.0;
		textBlock37.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)textBlock36).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel24).EndInit();
		Controls children26 = stackPanel5.Children;
		RootTextbox rootTextbox7;
		RootTextbox rootTextbox6 = (rootTextbox7 = new RootTextbox());
		((ISupportInitialize)rootTextbox6).BeginInit();
		children26.Add(rootTextbox6);
		RootTextbox rootTextbox8 = (rootTextbox4 = rootTextbox7);
		context.PushParent(rootTextbox4);
		RootTextbox rootTextbox9 = rootTextbox4;
		rootTextbox9.Name = "MessageTextbox";
		obj = rootTextbox9;
		context.AvaloniaNameScope.Register("MessageTextbox", obj);
		StyledProperty<string> placeholderTextProperty = RootTextbox.PlaceholderTextProperty;
		CompiledBindingExtension obj23 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002EMessageContainerMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainerMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EUserName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			StringFormat = RootApp.Client.Avalonia.Resources.Strings.Resources.DirectMessagePlaceholder
		};
		context.ProvideTargetProperty = RootTextbox.PlaceholderTextProperty;
		CompiledBindingExtension compiledBindingExtension33 = obj23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox9, placeholderTextProperty, compiledBindingExtension33);
		StyledProperty<IBrush> borderBackgroundBrushProperty = RootTextbox.BorderBackgroundBrushProperty;
		DynamicResourceExtension dynamicResourceExtension19 = new DynamicResourceExtension("Input");
		context.ProvideTargetProperty = RootTextbox.BorderBackgroundBrushProperty;
		IBinding binding19 = dynamicResourceExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox9, borderBackgroundBrushProperty, binding19);
		StyledProperty<IBrush> borderBorderBrushProperty = RootTextbox.BorderBorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension20 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = RootTextbox.BorderBorderBrushProperty;
		IBinding binding20 = dynamicResourceExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox9, borderBorderBrushProperty, binding20);
		rootTextbox9.TextboxFontSize = 14.0;
		rootTextbox9.BorderHeight = 40.0;
		rootTextbox9.Margin = new Thickness(0.0, 28.0, 0.0, 0.0);
		rootTextbox9.BorderCornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		rootTextbox9.BorderBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootTextbox9.TextboxMargin = new Thickness(20.0, 0.0, 20.0, 0.0);
		StyledProperty<bool> isVisibleProperty12 = Visual.IsVisibleProperty;
		MultiBinding multiBinding6 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding7 = multiBinding2;
		StaticResourceExtension staticResourceExtension13 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj24 = staticResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding7.Converter = (IMultiValueConverter)obj24;
		IList<IBinding> bindings5 = multiBinding7.Bindings;
		CompiledBindingExtension obj25 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002EIsSelf_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item5 = obj25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings5.Add(item5);
		IList<IBinding> bindings6 = multiBinding7.Bindings;
		CompiledBindingExtension compiledBindingExtension34 = new CompiledBindingExtension();
		compiledBindingExtension34.Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002EMessageContainerMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainerMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EIsBlocked_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension34.FallbackValue = "True";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item6 = compiledBindingExtension34.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings6.Add(item6);
		IList<IBinding> bindings7 = multiBinding7.Bindings;
		CompiledBindingExtension compiledBindingExtension35 = new CompiledBindingExtension();
		compiledBindingExtension35.Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberProfileViewModel_002CRootApp_002EClient_002EAvalonia_002EMessageContainerMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainerMember_002CRootApp_002EClient_002ECoreDomain_002EIsApp_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension35.FallbackValue = "True";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item7 = compiledBindingExtension35.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings7.Add(item7);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootTextbox9, isVisibleProperty12, multiBinding6);
		context.PopParent();
		((ISupportInitialize)rootTextbox8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
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
	private static void _0021XamlIlPopulateTrampoline(MemberProfileView P_0)
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

