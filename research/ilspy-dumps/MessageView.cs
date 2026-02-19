// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Messages.MessageView
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Media.Immutable;
using Avalonia.Threading;
using Avalonia.VisualTree;
using CommunityToolkit.Mvvm.Input;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Controls.ContextMenus;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Messages;
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.Markdown.Components;
using RootApp.Client.Avalonia.Markdown.DocumentElements;
using RootApp.Client.Avalonia.Resources.Converters.Messages;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.Resources.Themes;
using RootApp.Client.Avalonia.UI.Messages;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Client.CoreDomain.Models.Messages;
using RootApp.Client.CoreDomain.Models.Messages.SystemMessages;
using RootApp.Client.CoreDomain.Models.Messages.SystemMessages.LogItems;
using RootApp.Client.CoreDomain.Models.User;
using RootApp.Core.Identifiers;

public class MessageView : UserControl, ISelectableMessage
{
	[CompilerGenerated]
	private class XamlClosure_177
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MessageView> context = CreateContext(P_0);
			return new DateDividerConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<MessageView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MessageView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MessageView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FMessages_002FMessageView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Messages/MessageView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (MessageView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MessageView> context = CreateContext(P_0);
			return new MessageDateConverter();
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MessageView> context = CreateContext(P_0);
			return new MessageTimeConverter();
		}

		public static object Build_4(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MessageView> context = CreateContext(P_0);
			return new PlaceholderOpacityConverter();
		}

		public static object Build_5(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MessageView> context = CreateContext(P_0);
			return new FullDateTimeConverter();
		}

		public static object Build_6(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MessageView> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static object Build_7(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MessageView> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static object Build_8(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MessageView> context = CreateContext(P_0);
			context.IntermediateRoot = new WrapPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static object Build_9(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MessageView> context = CreateContext(P_0);
			context.IntermediateRoot = new WrapPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}
	}

	[CompilerGenerated]
	private DateTimeOffset? _003CDateTime_003Ek__BackingField;

	private Role? _primaryRole;

	private Member? _communityMember;

	private bool _hasSetSystemMessageIcon;

	private RootMenuFlyout? _contextMenu;

	private readonly List<object> _tempContextMenuItems = new List<object>();

	private RootMessageScrollViewer? _parentScrollViewer;

	private TopLevel? _topLevel;

	private Point? _replyPressPoint;

	private bool _replyHadTextSelectionOnPressDown;

	private bool _isHooked;

	private MessageViewModel? _hookedVm;

	private Message? _hookedMessage;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal UserControl MainView;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal MenuItem PinnedMessageMenuItem;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal MenuItem DeleteMenuItem;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border MessageBackgroundBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border MessageBackgroundHighlightBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgImage SystemMessageSvgImage;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootLinkButton UsernameTextBlock;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootMarkdownTextBlock MessageTextBlock;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootBorder ActionBarBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton AddReactionActionButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton ReplyActionButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton EditMessageActionButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton PinMessageActionButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock PinMessageToolTip;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton MoreOptionsActionButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton ShiftDeleteActionButton;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	private MessageViewModel? _messageViewModel => base.DataContext as MessageViewModel;

	private DateTimeOffset? DateTime
	{
		[CompilerGenerated]
		set
		{
			_003CDateTime_003Ek__BackingField = dateTimeOffset;
		}
	}

	public DocumentElement? Document => MessageTextBlock.Document;

	public MessageView()
	{
		InitializeComponent();
	}

	protected override void OnDataContextChanged(EventArgs P_0)
	{
		MessageViewModel hookedVm = _hookedVm;
		base.OnDataContextChanged(P_0);
		MessageViewModel messageViewModel = _messageViewModel;
		if (messageViewModel != null)
		{
			DateTime = messageViewModel.Message.MessageId.ToDateTimeOffset();
			if (messageViewModel.Message.SenderMember is Member communityMember)
			{
				_communityMember = communityMember;
				_primaryRole = _communityMember.Roles.PrimaryRole;
			}
			else
			{
				_communityMember = null;
				_primaryRole = null;
			}
			_hasSetSystemMessageIcon = false;
		}
		else
		{
			DateTime = null;
			_communityMember = null;
			_primaryRole = null;
			_hasSetSystemMessageIcon = false;
		}
		if (_isHooked && hookedVm != messageViewModel)
		{
			Unhook();
			Hook();
		}
	}

	protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnAttachedToVisualTree(P_0);
		if (!_isHooked)
		{
			_isHooked = true;
			Hook();
		}
	}

	protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnDetachedFromVisualTree(P_0);
		if (_isHooked)
		{
			_isHooked = false;
			Unhook();
		}
	}

	private void Hook()
	{
		MessageViewModel messageViewModel = _messageViewModel;
		if (messageViewModel == null)
		{
			return;
		}
		_hookedVm = messageViewModel;
		_hookedMessage = messageViewModel.Message;
		_contextMenu = MessageBackgroundBorder.ContextFlyout as RootMenuFlyout;
		if (_contextMenu != null)
		{
			_contextMenu.Closing += onContextMenuClosing;
		}
		_parentScrollViewer = this.FindAncestorOfType<RootMessageScrollViewer>();
		messageViewModel.Message.PropertyChanged += onMessagePropertyChanged;
		messageViewModel.PropertyChanged += onMessageViewModelPropertyChanged;
		if (MoreOptionsActionButton.ContextFlyout != null)
		{
			MoreOptionsActionButton.ContextFlyout.Opened += onMessageOptionsContextFlyoutOpened;
			MoreOptionsActionButton.ContextFlyout.Closed += onMessageOptionsContextFlyoutClosed;
		}
		if (Application.Current != null)
		{
			Application.Current.ActualThemeVariantChanged += onActualThemeVariantChanged;
		}
		_topLevel = TopLevel.GetTopLevel(this);
		if (_topLevel != null)
		{
			_topLevel.KeyDown += onTopLevelKeyDown;
			_topLevel.KeyUp += onTopLevelKeyUp;
		}
		if (_communityMember != null)
		{
			_communityMember.Roles.PropertyChanged += onRolesCollectionPropertyChanged;
			if (_primaryRole != null)
			{
				_primaryRole.PropertyChanged += onPrimaryRolePropertyChanged;
			}
		}
		updateSystemMessageIcon();
		updateMessageBorderMargin();
		updateBackgroundColor();
		updatePinnedMessageStates();
		base.Cursor = (messageViewModel.IsTapToReplyEnabled() ? new Cursor(StandardCursorType.Hand) : Avalonia.Input.Cursor.Default);
		if (_communityMember != null)
		{
			updateMemberColor();
		}
	}

	private void Unhook()
	{
		if (_tempContextMenuItems.Count > 0 && _contextMenu != null)
		{
			foreach (object tempContextMenuItem in _tempContextMenuItems)
			{
				_contextMenu.Items.Remove(tempContextMenuItem);
			}
			_tempContextMenuItems.Clear();
		}
		if (_contextMenu != null)
		{
			_contextMenu.Closing -= onContextMenuClosing;
			_contextMenu = null;
		}
		if (_hookedVm != null)
		{
			_hookedVm.Message.PropertyChanged -= onMessagePropertyChanged;
			_hookedVm.PropertyChanged -= onMessageViewModelPropertyChanged;
		}
		if (MoreOptionsActionButton.ContextFlyout != null)
		{
			MoreOptionsActionButton.ContextFlyout.Opened -= onMessageOptionsContextFlyoutOpened;
			MoreOptionsActionButton.ContextFlyout.Closed -= onMessageOptionsContextFlyoutClosed;
		}
		if (Application.Current != null)
		{
			Application.Current.ActualThemeVariantChanged -= onActualThemeVariantChanged;
		}
		if (_topLevel != null)
		{
			_topLevel.KeyDown -= onTopLevelKeyDown;
			_topLevel.KeyUp -= onTopLevelKeyUp;
			_topLevel = null;
		}
		if (_communityMember != null)
		{
			_communityMember.Roles.PropertyChanged -= onRolesCollectionPropertyChanged;
			if (_primaryRole != null)
			{
				_primaryRole.PropertyChanged -= onPrimaryRolePropertyChanged;
			}
		}
		_parentScrollViewer = null;
		_hookedVm = null;
		_hookedMessage = null;
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

	private void onMessagePropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (_messageViewModel != null)
			{
				if (e.PropertyName == "HasSelfMention" || e.PropertyName == "HasLocalPendingReply")
				{
					updateBackgroundColor();
				}
				else if (e.PropertyName == "PinnedAt")
				{
					updatePinnedMessageStates();
				}
				else if (e.PropertyName == "SystemMessageLog")
				{
					updateSystemMessageIcon();
				}
			}
		});
	}

	private void onMessageViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		string propertyName = e.PropertyName;
		if ((propertyName == "ActionBarOpen" || propertyName == "IsInEditMode") ? true : false)
		{
			updateActionBarVisibility();
			updateBackgroundColor();
		}
	}

	private void onMessageBackgroundBorderPointerEntered(object? sender, PointerEventArgs e)
	{
		updateActionBarVisibility();
		updateBackgroundColor();
	}

	private void onMessageBackgroundBorderPointerExited(object? sender, PointerEventArgs e)
	{
		updateActionBarVisibility();
		updateBackgroundColor();
	}

	private void updateBackgroundColor()
	{
		if (_messageViewModel == null)
		{
			return;
		}
		if (_messageViewModel.Message.HasSelfMention && !_messageViewModel.Message.HasLocalPendingReply)
		{
			MessageBackgroundBorder[!TemplatedControl.BorderBrushProperty] = new DynamicResourceExtension("Error");
			MessageBackgroundHighlightBorder[!TemplatedControl.BackgroundProperty] = new DynamicResourceExtension("SelfMention");
			return;
		}
		if (_messageViewModel.Message.HasLocalPendingReply)
		{
			MessageBackgroundBorder.BorderBrush = Application.Current.FindResource(Application.Current.ActualThemeVariant, "BrandPrimary") as IBrush;
			MessageBackgroundHighlightBorder.Background = Application.Current.FindResource(Application.Current.ActualThemeVariant, "BrandPrimary") as IBrush;
			return;
		}
		MessageBackgroundHighlightBorder.Background = Brushes.Transparent;
		if ((MessageBackgroundBorder.IsPointerOver || _messageViewModel.ActionBarOpen) && !_messageViewModel.IsInEditMode)
		{
			MessageBackgroundBorder.Background = Application.Current.FindResource(Application.Current.ActualThemeVariant, "HighlightLight") as IBrush;
			MessageBackgroundBorder.BorderBrush = Application.Current.FindResource(Application.Current.ActualThemeVariant, "HighlightLight") as IBrush;
		}
		else if (_messageViewModel.IsInEditMode)
		{
			MessageBackgroundBorder.Background = Application.Current.FindResource(Application.Current.ActualThemeVariant, "HighlightLight") as IBrush;
			MessageBackgroundBorder.BorderBrush = Application.Current.FindResource(Application.Current.ActualThemeVariant, "HighlightLight") as IBrush;
		}
		else
		{
			MessageBackgroundBorder.Background = Brushes.Transparent;
			MessageBackgroundBorder.BorderBrush = Brushes.Transparent;
		}
	}

	private void updateActionBarVisibility()
	{
		if (_messageViewModel != null)
		{
			ActionBarBorder.IsVisible = (MessageBackgroundBorder.IsPointerOver || _messageViewModel.ActionBarOpen) && !_messageViewModel.IsInEditMode;
		}
	}

	private void updatePinnedMessageStates()
	{
		if (_messageViewModel != null)
		{
			if (_messageViewModel.Message.HasBeenPinned)
			{
				PinMessageActionButton[!RootSvgButton.SvgPathProperty] = new DynamicResourceExtension("PinFilledSVG");
				PinMessageToolTip.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.UnpinMessage;
				PinnedMessageMenuItem.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.UnpinMessage;
			}
			else
			{
				PinMessageActionButton[!RootSvgButton.SvgPathProperty] = new DynamicResourceExtension("PinSVG");
				PinMessageToolTip.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.PinMessage;
				PinnedMessageMenuItem.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.PinMessage;
			}
		}
	}

	private void updateMessageBorderMargin()
	{
		if (_messageViewModel != null)
		{
			MessageBackgroundBorder.Margin = ((_messageViewModel.Message.ShowUserProfile || _messageViewModel.Message.IsSystemMessage) ? new Thickness(0.0, 15.0, 0.0, 0.0) : new Thickness(0.0, 4.0, 0.0, 0.0));
		}
	}

	private void updateSystemMessageIcon()
	{
		if (_messageViewModel == null)
		{
			return;
		}
		Message message = _messageViewModel.Message;
		if (message == null || !message.IsSystemMessage || message.SystemMessageLog == null || _hasSetSystemMessageIcon)
		{
			return;
		}
		_hasSetSystemMessageIcon = true;
		BaseSystemMessageLogItem baseSystemMessageLogItem = _messageViewModel.Message.SystemMessageLog.LogItems.FirstOrDefault();
		if (baseSystemMessageLogItem != null)
		{
			if (1 == 0)
			{
			}
			string text = ((baseSystemMessageLogItem is ChannelLogItem) ? "ChannelEditedSVG" : ((baseSystemMessageLogItem is CommunityLogItem) ? "CommunityEditedSVG" : ((baseSystemMessageLogItem is MessagePinnedLogItem) ? "MessagePinnedSVG" : ((baseSystemMessageLogItem is MessageUnpinnedLogItem) ? "MessageUnpinnedSVG" : ((!(baseSystemMessageLogItem is CommunityMemberJoinedLogItem) && !(baseSystemMessageLogItem is DirectMessageUsersJoinedLogItem)) ? ((baseSystemMessageLogItem is DirectMessageUserLeftLogItem) ? "UserLeftSVG" : ((baseSystemMessageLogItem is DirectMessageUserCallStartedLogItem) ? "CallStartedSVG" : ((baseSystemMessageLogItem is DirectMessageUserCallEndedLogItem) ? "CallEndedSVG" : ((baseSystemMessageLogItem is CommunityMemberBannedLogItem) ? "UserBannedSVG" : ((!(baseSystemMessageLogItem is CommunityMemberKickedLogItem)) ? "InfoSVG" : "UserKickedSVG"))))) : "UserJoinedSVG")))));
			if (1 == 0)
			{
			}
			string text2 = text;
			SystemMessageSvgImage[!RootSvgImage.SvgPathProperty] = new DynamicResourceExtension(text2);
		}
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

	private void onActualThemeVariantChanged(object? sender, EventArgs e)
	{
		updateMemberColor();
	}

	private void onTopLevelKeyDown(object? sender, KeyEventArgs e)
	{
		if (_messageViewModel != null && (e.Key == Key.LeftShift || e.Key == Key.RightShift))
		{
			_messageViewModel.IsShiftKeyPressed = true;
		}
	}

	private void onTopLevelKeyUp(object? sender, KeyEventArgs e)
	{
		if (_messageViewModel != null && (e.Key == Key.LeftShift || e.Key == Key.RightShift))
		{
			_messageViewModel.IsShiftKeyPressed = false;
		}
	}

	private void onFlyoutOpening(object? sender, EventArgs e)
	{
		if (_messageViewModel != null && _messageViewModel.EmojiPickerOpenedCommand.CanExecute(null))
		{
			_messageViewModel.EmojiPickerOpenedCommand.Execute(null);
		}
	}

	private void onFlyoutClosing(object? sender, CancelEventArgs e)
	{
		if (_messageViewModel != null && _messageViewModel.EmojiPickerClosedCommand.CanExecute(null))
		{
			_messageViewModel.EmojiPickerClosedCommand.Execute(null);
		}
	}

	private void onUsernameProfilePopupOpening(object? sender, EventArgs e)
	{
		if (_messageViewModel != null && _messageViewModel.UsernameProfileOpeningCommand.CanExecute(null))
		{
			_messageViewModel.UsernameProfileOpeningCommand.Execute(null);
		}
	}

	private void onUsernameProfilePopupClosing(object? sender, CancelEventArgs e)
	{
		if (_messageViewModel != null && _messageViewModel.UsernameProfileClosingCommand.CanExecute(null))
		{
			_messageViewModel.UsernameProfileClosingCommand.Execute(null);
		}
	}

	private void onImageProfilePopupOpening(object? sender, EventArgs e)
	{
		if (_messageViewModel != null && _messageViewModel.ImageProfileOpeningCommand.CanExecute(null))
		{
			_messageViewModel.ImageProfileOpeningCommand.Execute(null);
		}
	}

	private void onImageProfilePopupClosing(object? sender, CancelEventArgs e)
	{
		if (_messageViewModel != null && _messageViewModel.ImageProfileClosingCommand.CanExecute(null))
		{
			_messageViewModel.ImageProfileClosingCommand.Execute(null);
		}
	}

	private void onCopyMessageMenuItemClick(object? sender, RoutedEventArgs e)
	{
		if (Document != null)
		{
			TopLevel.GetTopLevel(this)?.Clipboard?.SetTextAsync(Document.GetText());
		}
	}

	private void onAddReactionMenuItemClick(object? sender, RoutedEventArgs e)
	{
		FlyoutBase flyout = AddReactionActionButton.Flyout;
		Flyout flyout2 = flyout as Flyout;
		if (flyout2 != null)
		{
			Dispatcher.UIThread.Post(delegate
			{
				flyout2.ShowAt(AddReactionActionButton);
			});
		}
	}

	private void onMoreOptionsActionButtonClick(object? sender, RoutedEventArgs e)
	{
		MoreOptionsActionButton.ContextFlyout?.ShowAt(MoreOptionsActionButton);
	}

	private void onMessageOptionsContextFlyoutOpened(object? sender, EventArgs e)
	{
		if (_messageViewModel != null)
		{
			_messageViewModel.ActionBarOpen = true;
		}
	}

	private void onMessageOptionsContextFlyoutClosed(object? sender, EventArgs e)
	{
		if (_messageViewModel != null)
		{
			_messageViewModel.ActionBarOpen = false;
		}
	}

	private void onMessageBackgroundBorderContextRequested(object? sender, ContextRequestedEventArgs e)
	{
		_parentScrollViewer?.SetBlockSelection(true);
		if (!e.TryGetPosition(this, out var point))
		{
			return;
		}
		_tempContextMenuItems.Add(new Separator());
		string selectedText = _parentScrollViewer?.ItemsControl?.GetSelectedText();
		if (!string.IsNullOrEmpty(selectedText))
		{
			MenuItem item = new MenuItem
			{
				Header = RootApp.Client.Avalonia.Resources.Strings.Resources.CopySelectedText,
				Command = new RelayCommand(delegate
				{
					TopLevel.GetTopLevel(this)?.Clipboard?.SetTextAsync(selectedText);
				})
			};
			_tempContextMenuItems.Add(item);
		}
		IInputElement inputElement = this.InputHitTest(point);
		if (inputElement is CTextBlock cTextBlock)
		{
			CHyperlink link = FindDescendants<CHyperlink>(cTextBlock).FirstOrDefault((CHyperlink cHyperlink) => cHyperlink.Classes.Contains(":pointerover"));
			if (link != null && link.Classes.Contains("externalLink"))
			{
				MenuItem item2 = new MenuItem
				{
					Header = RootApp.Client.Avalonia.Resources.Strings.Resources.CopyLink,
					Command = new RelayCommand(delegate
					{
						TopLevel.GetTopLevel(this)?.Clipboard?.SetTextAsync(link.CommandParameter ?? string.Empty);
					})
				};
				_tempContextMenuItems.Add(item2);
			}
		}
		if (_tempContextMenuItems.Count > 1 && _contextMenu != null)
		{
			foreach (object tempContextMenuItem in _tempContextMenuItems)
			{
				_contextMenu.Items.Insert(0, tempContextMenuItem);
			}
			return;
		}
		_tempContextMenuItems.Clear();
	}

	private void onContextMenuClosing(object? sender, CancelEventArgs e)
	{
		_parentScrollViewer?.SetBlockSelection(false);
		_parentScrollViewer?.ItemsControl?.UnSelect();
		if (_contextMenu != null)
		{
			foreach (object tempContextMenuItem in _tempContextMenuItems)
			{
				_contextMenu.Items.Remove(tempContextMenuItem);
			}
		}
		_tempContextMenuItems.Clear();
	}

	protected override void OnPointerPressed(PointerPressedEventArgs P_0)
	{
		base.OnPointerPressed(P_0);
		if (P_0.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
		{
			_replyPressPoint = P_0.GetPosition(P_0.Source as Visual);
			_replyHadTextSelectionOnPressDown = !string.IsNullOrEmpty(MessageTextBlock.Document?.GetSelectedText());
		}
	}

	protected override void OnPointerReleased(PointerReleasedEventArgs P_0)
	{
		base.OnPointerReleased(P_0);
		if (!_replyPressPoint.HasValue || _messageViewModel == null)
		{
			_replyHadTextSelectionOnPressDown = false;
			return;
		}
		bool flag = _messageViewModel.Message.IsSystemMessage || !_messageViewModel.Message.MessageContainer.LocalChannelPermission.ChannelCreateMessage;
		bool flag2 = flag;
		bool flag3;
		if (!flag2)
		{
			IMessageContainerMember senderMember = _messageViewModel.Message.SenderMember;
			if (senderMember != null)
			{
				GlobalUser globalUser = senderMember.GlobalUser;
				if (globalUser == null || !globalUser.IsBlocked)
				{
					flag3 = false;
					goto IL_00a5;
				}
			}
			flag3 = true;
			goto IL_00a5;
		}
		goto IL_00a8;
		IL_00a5:
		flag2 = flag3;
		goto IL_00a8;
		IL_00a8:
		if (!flag2)
		{
			Point position = P_0.GetPosition(P_0.Source as Visual);
			if (Math.Abs(position.X - _replyPressPoint.Value.X) < 4.0 && Math.Abs(position.Y - _replyPressPoint.Value.Y) < 4.0 && !_replyHadTextSelectionOnPressDown && _messageViewModel.IsTapToReplyEnabled() && _messageViewModel.ReplyToMessageCommand.CanExecute(null))
			{
				_messageViewModel.ReplyToMessageCommand.Execute(null);
			}
			_replyPressPoint = null;
			_replyHadTextSelectionOnPressDown = false;
		}
	}

	private static IEnumerable<T> FindDescendants<T>(ILogical P_0) where T : class, ILogical
	{
		foreach (ILogical child in P_0.LogicalChildren.OfType<ILogical>())
		{
			if (child is T t)
			{
				yield return t;
			}
			foreach (T item in FindDescendants<T>(child))
			{
				yield return item;
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
		MainView = nameScope?.Find<UserControl>("MainView");
		PinnedMessageMenuItem = nameScope?.Find<MenuItem>("PinnedMessageMenuItem");
		DeleteMenuItem = nameScope?.Find<MenuItem>("DeleteMenuItem");
		MessageBackgroundBorder = nameScope?.Find<Border>("MessageBackgroundBorder");
		MessageBackgroundHighlightBorder = nameScope?.Find<Border>("MessageBackgroundHighlightBorder");
		SystemMessageSvgImage = nameScope?.Find<RootSvgImage>("SystemMessageSvgImage");
		UsernameTextBlock = nameScope?.Find<RootLinkButton>("UsernameTextBlock");
		MessageTextBlock = nameScope?.Find<RootMarkdownTextBlock>("MessageTextBlock");
		ActionBarBorder = nameScope?.Find<RootBorder>("ActionBarBorder");
		AddReactionActionButton = nameScope?.Find<RootSvgButton>("AddReactionActionButton");
		ReplyActionButton = nameScope?.Find<RootSvgButton>("ReplyActionButton");
		EditMessageActionButton = nameScope?.Find<RootSvgButton>("EditMessageActionButton");
		PinMessageActionButton = nameScope?.Find<RootSvgButton>("PinMessageActionButton");
		PinMessageToolTip = nameScope?.Find<TextBlock>("PinMessageToolTip");
		MoreOptionsActionButton = nameScope?.Find<RootSvgButton>("MoreOptionsActionButton");
		ShiftDeleteActionButton = nameScope?.Find<RootSvgButton>("ShiftDeleteActionButton");
	}

	[CompilerGenerated]
	private unsafe static void _0021XamlIlPopulate(IServiceProvider P_0, MessageView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<MessageView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MessageView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FMessages_002FMessageView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Messages/MessageView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		if (P_1.Resources is ResourceDictionary resourceDictionary)
		{
			resourceDictionary.EnsureCapacity(resourceDictionary.Count + 6);
		}
		P_1.Name = "MainView";
		object obj = P_1;
		context.AvaloniaNameScope.Register("MainView", obj);
		P_1.ClipToBounds = false;
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension obj2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EDeletedAt_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Converter = ObjectConverters.IsNull
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension = obj2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(P_1, isVisibleProperty, compiledBindingExtension);
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"DateDividerConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_177.Build_1), context));
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"MessageDateConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_177.Build_2), context));
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"MessageTimeConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_177.Build_3), context));
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"PlaceholderOpacityConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_177.Build_4), context));
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"FullDateTimeConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_177.Build_5), context));
		IResourceDictionary resources = P_1.Resources;
		RootMenuFlyout rootMenuFlyout;
		RootMenuFlyout value = (rootMenuFlyout = new RootMenuFlyout());
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
		menuItem5.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.CopyMessage;
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002ESenderMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainerMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EIsBlocked_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "True"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension2 = obj3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem5, isVisibleProperty2, compiledBindingExtension2);
		menuItem5.AddHandler((RoutedEvent)MenuItem.ClickEvent, (Delegate)new EventHandler<RoutedEventArgs>(context.RootObject.onCopyMessageMenuItemClick), RoutingStrategies.Direct | RoutingStrategies.Bubble, false);
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
		menuItem9.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.EditMessage;
		StyledProperty<ICommand?> commandProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EEnterEditModeCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem9, commandProperty, compiledBindingExtension4);
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		MultiBinding multiBinding2;
		MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding3 = multiBinding2;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj4 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding3.Converter = (IMultiValueConverter)obj4;
		IList<IBinding> bindings = multiBinding3.Bindings;
		CompiledBindingExtension obj5 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EIsMyMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item = obj5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings.Add(item);
		IList<IBinding> bindings2 = multiBinding3.Bindings;
		CompiledBindingExtension obj6 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EIsSystemMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item2 = obj6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings2.Add(item2);
		IList<IBinding> bindings3 = multiBinding3.Bindings;
		CompiledBindingExtension obj7 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EIsPlaceholder_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item3 = obj7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings3.Add(item3);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(menuItem9, isVisibleProperty3, multiBinding);
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
		menuItem13.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.Reply;
		StyledProperty<ICommand?> commandProperty2 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EReplyToMessageCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem13, commandProperty2, compiledBindingExtension6);
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		MultiBinding multiBinding4 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding5 = multiBinding2;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj8 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding5.Converter = (IMultiValueConverter)obj8;
		IList<IBinding> bindings4 = multiBinding5.Bindings;
		CompiledBindingExtension obj9 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EIsSystemMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item4 = obj9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings4.Add(item4);
		IList<IBinding> bindings5 = multiBinding5.Bindings;
		CompiledBindingExtension obj10 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EMessageContainer_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainer_002CRootApp_002EClient_002ECoreDomain_002ELocalChannelPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalChannelPermission_002CRootApp_002EClient_002ECoreDomain_002EChannelCreateMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item5 = obj10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings5.Add(item5);
		IList<IBinding> bindings6 = multiBinding5.Bindings;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension();
		compiledBindingExtension7.Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002ESenderMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainerMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EIsBlocked_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension7.FallbackValue = "True";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item6 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings6.Add(item6);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(menuItem13, isVisibleProperty4, multiBinding4);
		context.PopParent();
		((ISupportInitialize)menuItem12).EndInit();
		ItemCollection items4 = rootMenuFlyout.Items;
		MenuItem menuItem15;
		MenuItem menuItem14 = (menuItem15 = new MenuItem());
		((ISupportInitialize)menuItem14).BeginInit();
		items4.Add(menuItem14);
		MenuItem menuItem16 = (menuItem4 = menuItem15);
		context.PushParent(menuItem4);
		MenuItem menuItem17 = menuItem4;
		menuItem17.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.AddReaction;
		StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension8 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EMessageContainer_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainer_002CRootApp_002EClient_002ECoreDomain_002ELocalChannelPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalChannelPermission_002CRootApp_002EClient_002ECoreDomain_002EChannelCreateMessageReaction_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension9 = compiledBindingExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem17, isVisibleProperty5, compiledBindingExtension9);
		menuItem17.AddHandler((RoutedEvent)MenuItem.ClickEvent, (Delegate)new EventHandler<RoutedEventArgs>(context.RootObject.onAddReactionMenuItemClick), RoutingStrategies.Direct | RoutingStrategies.Bubble, false);
		context.PopParent();
		((ISupportInitialize)menuItem16).EndInit();
		ItemCollection items5 = rootMenuFlyout.Items;
		MenuItem menuItem19;
		MenuItem menuItem18 = (menuItem19 = new MenuItem());
		((ISupportInitialize)menuItem18).BeginInit();
		items5.Add(menuItem18);
		MenuItem menuItem20 = (menuItem4 = menuItem19);
		context.PushParent(menuItem4);
		MenuItem menuItem21 = menuItem4;
		menuItem21.Name = "PinnedMessageMenuItem";
		obj = menuItem21;
		context.AvaloniaNameScope.Register("PinnedMessageMenuItem", obj);
		StyledProperty<ICommand?> commandProperty3 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension10 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EPinMessageCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension11 = compiledBindingExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem21, commandProperty3, compiledBindingExtension11);
		StyledProperty<bool> isVisibleProperty6 = Visual.IsVisibleProperty;
		MultiBinding multiBinding6 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding7 = multiBinding2;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj11 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding7.Converter = (IMultiValueConverter)obj11;
		IList<IBinding> bindings7 = multiBinding7.Bindings;
		CompiledBindingExtension compiledBindingExtension12 = new CompiledBindingExtension();
		compiledBindingExtension12.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EMessageContainer_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainer_002CRootApp_002EClient_002ECoreDomain_002ELocalChannelPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalChannelPermission_002CRootApp_002EClient_002ECoreDomain_002EChannelManagePinnedMessages_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension12.FallbackValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item7 = compiledBindingExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings7.Add(item7);
		IList<IBinding> bindings8 = multiBinding7.Bindings;
		CompiledBindingExtension obj12 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EIsSystemMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item8 = obj12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings8.Add(item8);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(menuItem21, isVisibleProperty6, multiBinding6);
		context.PopParent();
		((ISupportInitialize)menuItem20).EndInit();
		ItemCollection items6 = rootMenuFlyout.Items;
		Separator separator2;
		Separator separator = (separator2 = new Separator());
		((ISupportInitialize)separator).BeginInit();
		items6.Add(separator);
		Separator separator4;
		Separator separator3 = (separator4 = separator2);
		context.PushParent(separator4);
		Separator separator5 = separator4;
		StyledProperty<bool> isVisibleProperty7 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "DeleteMenuItem").Property(Visual.IsVisibleProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(separator5, isVisibleProperty7, compiledBindingExtension14);
		context.PopParent();
		((ISupportInitialize)separator3).EndInit();
		ItemCollection items7 = rootMenuFlyout.Items;
		MenuItem menuItem23;
		MenuItem menuItem22 = (menuItem23 = new MenuItem());
		((ISupportInitialize)menuItem22).BeginInit();
		items7.Add(menuItem22);
		MenuItem menuItem24 = (menuItem4 = menuItem23);
		context.PushParent(menuItem4);
		MenuItem menuItem25 = menuItem4;
		menuItem25.Name = "DeleteMenuItem";
		obj = menuItem25;
		context.AvaloniaNameScope.Register("DeleteMenuItem", obj);
		menuItem25.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.DeleteMessage;
		menuItem25.Classes.Add("DeleteMenuItem");
		StyledProperty<ICommand?> commandProperty4 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension15 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EDeleteMessageCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension16 = compiledBindingExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem25, commandProperty4, compiledBindingExtension16);
		StyledProperty<bool> isVisibleProperty8 = Visual.IsVisibleProperty;
		MultiBinding multiBinding8 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding9 = multiBinding2;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("OrConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj13 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding9.Converter = (IMultiValueConverter)obj13;
		IList<IBinding> bindings9 = multiBinding9.Bindings;
		CompiledBindingExtension obj14 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EIsMyMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item9 = obj14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings9.Add(item9);
		IList<IBinding> bindings10 = multiBinding9.Bindings;
		CompiledBindingExtension obj15 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EMessageContainer_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainer_002CRootApp_002EClient_002ECoreDomain_002ELocalChannelPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalChannelPermission_002CRootApp_002EClient_002ECoreDomain_002EChannelDeleteMessageOther_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item10 = obj15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings10.Add(item10);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(menuItem25, isVisibleProperty8, multiBinding8);
		context.PopParent();
		((ISupportInitialize)menuItem24).EndInit();
		ItemCollection items8 = rootMenuFlyout.Items;
		Separator separator7;
		Separator separator6 = (separator7 = new Separator());
		((ISupportInitialize)separator6).BeginInit();
		items8.Add(separator6);
		Separator separator8 = (separator4 = separator7);
		context.PushParent(separator4);
		Separator separator9 = separator4;
		StyledProperty<bool> isVisibleProperty9 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension17 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EDeveloperModeEnabled_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension18 = compiledBindingExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(separator9, isVisibleProperty9, compiledBindingExtension18);
		context.PopParent();
		((ISupportInitialize)separator8).EndInit();
		ItemCollection items9 = rootMenuFlyout.Items;
		MenuItem menuItem27;
		MenuItem menuItem26 = (menuItem27 = new MenuItem());
		((ISupportInitialize)menuItem26).BeginInit();
		items9.Add(menuItem26);
		MenuItem menuItem28 = (menuItem4 = menuItem27);
		context.PushParent(menuItem4);
		MenuItem menuItem29 = menuItem4;
		menuItem29.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.CopyMessageId;
		StyledProperty<ICommand?> commandProperty5 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension19 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002ECopyMessageIdCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension20 = compiledBindingExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem29, commandProperty5, compiledBindingExtension20);
		StyledProperty<bool> isVisibleProperty10 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension21 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EDeveloperModeEnabled_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension22 = compiledBindingExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem29, isVisibleProperty10, compiledBindingExtension22);
		context.PopParent();
		((ISupportInitialize)menuItem28).EndInit();
		context.PopParent();
		resources.Add("MessageContextFlyout", value);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		P_1.Content = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		Grid grid5 = grid4;
		StyledProperty<double> opacityProperty = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension24;
		CompiledBindingExtension compiledBindingExtension23 = (compiledBindingExtension24 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EIsPlaceholder_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension24);
		CompiledBindingExtension compiledBindingExtension25 = compiledBindingExtension24;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("PlaceholderOpacityConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj16 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension25.Converter = (IValueConverter)obj16;
		context.PopParent();
		context.ProvideTargetProperty = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension26 = compiledBindingExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(grid5, opacityProperty, compiledBindingExtension26);
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		Controls children = grid5.Children;
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		children.Add(grid6);
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		Grid.SetRow(grid9, 0);
		grid9.Margin = new Thickness(24.0, 15.0, 24.0, 0.0);
		StyledProperty<bool> isVisibleProperty11 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension27 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EShowNewDivider_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension28 = compiledBindingExtension27.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(grid9, isVisibleProperty11, compiledBindingExtension28);
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		Controls children2 = grid9.Children;
		Rectangle rectangle2;
		Rectangle rectangle = (rectangle2 = new Rectangle());
		((ISupportInitialize)rectangle).BeginInit();
		children2.Add(rectangle);
		Rectangle rectangle4;
		Rectangle rectangle3 = (rectangle4 = rectangle2);
		context.PushParent(rectangle4);
		Rectangle rectangle5 = rectangle4;
		Grid.SetColumn(rectangle5, 0);
		rectangle5.Height = 0.5;
		StyledProperty<IBrush?> fillProperty = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("Error");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle5, fillProperty, binding);
		rectangle5.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)rectangle3).EndInit();
		Controls children3 = grid9.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children3.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		Grid.SetColumn(textBlock5, 1);
		textBlock5.FontSize = 12.0;
		textBlock5.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.New;
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj17 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj17);
		textBlock5.FontWeight = FontWeight.Bold;
		textBlock5.Margin = new Thickness(25.0, 0.0, 25.0, 0.0);
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("Error");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding2);
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		Controls children4 = grid9.Children;
		Rectangle rectangle7;
		Rectangle rectangle6 = (rectangle7 = new Rectangle());
		((ISupportInitialize)rectangle6).BeginInit();
		children4.Add(rectangle6);
		Rectangle rectangle8 = (rectangle4 = rectangle7);
		context.PushParent(rectangle4);
		Rectangle rectangle9 = rectangle4;
		Grid.SetColumn(rectangle9, 2);
		rectangle9.Height = 0.5;
		StyledProperty<IBrush?> fillProperty2 = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("Error");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle9, fillProperty2, binding3);
		rectangle9.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)rectangle8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		Controls children5 = grid5.Children;
		Grid grid11;
		Grid grid10 = (grid11 = new Grid());
		((ISupportInitialize)grid10).BeginInit();
		children5.Add(grid10);
		Grid grid12 = (grid4 = grid11);
		context.PushParent(grid4);
		Grid grid13 = grid4;
		Grid.SetRow(grid13, 1);
		grid13.Margin = new Thickness(24.0, 15.0, 24.0, 0.0);
		StyledProperty<bool> isVisibleProperty12 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension29 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EShowDateDivider_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension30 = compiledBindingExtension29.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(grid13, isVisibleProperty12, compiledBindingExtension30);
		grid13.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid13.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid13.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		Controls children6 = grid13.Children;
		Rectangle rectangle11;
		Rectangle rectangle10 = (rectangle11 = new Rectangle());
		((ISupportInitialize)rectangle10).BeginInit();
		children6.Add(rectangle10);
		Rectangle rectangle12 = (rectangle4 = rectangle11);
		context.PushParent(rectangle4);
		Rectangle rectangle13 = rectangle4;
		Grid.SetColumn(rectangle13, 0);
		rectangle13.Height = 0.5;
		StyledProperty<IBrush?> fillProperty3 = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle13, fillProperty3, binding4);
		rectangle13.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)rectangle12).EndInit();
		Controls children7 = grid13.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children7.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		Grid.SetColumn(textBlock9, 1);
		textBlock9.FontSize = 12.0;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension31 = (compiledBindingExtension24 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002ESentAtUtc_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension24);
		CompiledBindingExtension compiledBindingExtension32 = compiledBindingExtension24;
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("DateDividerConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj18 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension32.Converter = (IValueConverter)obj18;
		context.PopParent();
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension33 = compiledBindingExtension31.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, textProperty, compiledBindingExtension33);
		StaticResourceExtension staticResourceExtension8 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj19 = staticResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj19);
		textBlock9.FontWeight = (FontWeight)450;
		textBlock9.Margin = new Thickness(25.0, 0.0, 25.0, 0.0);
		textBlock9.Opacity = 0.5;
		textBlock9.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding5);
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		Controls children8 = grid13.Children;
		Rectangle rectangle15;
		Rectangle rectangle14 = (rectangle15 = new Rectangle());
		((ISupportInitialize)rectangle14).BeginInit();
		children8.Add(rectangle14);
		Rectangle rectangle16 = (rectangle4 = rectangle15);
		context.PushParent(rectangle4);
		Rectangle rectangle17 = rectangle4;
		Grid.SetColumn(rectangle17, 2);
		rectangle17.Height = 0.5;
		StyledProperty<IBrush?> fillProperty4 = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle17, fillProperty4, binding6);
		rectangle17.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)rectangle16).EndInit();
		context.PopParent();
		((ISupportInitialize)grid12).EndInit();
		Controls children9 = grid5.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children9.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Border border5 = border4;
		border5.Name = "MessageBackgroundBorder";
		obj = border5;
		context.AvaloniaNameScope.Register("MessageBackgroundBorder", obj);
		Grid.SetRow(border5, 2);
		border5.BorderThickness = new Thickness(2.0, 0.0, 0.0, 0.0);
		border5.Background = new ImmutableSolidColorBrush(16777215u);
		border5.AddHandler(InputElement.PointerEnteredEvent, context.RootObject.onMessageBackgroundBorderPointerEntered);
		border5.AddHandler(InputElement.PointerExitedEvent, context.RootObject.onMessageBackgroundBorderPointerExited);
		border5.AddHandler(Control.ContextRequestedEvent, context.RootObject.onMessageBackgroundBorderContextRequested);
		StaticResourceExtension staticResourceExtension9 = new StaticResourceExtension("MessageContextFlyout");
		context.ProvideTargetProperty = Control.ContextFlyoutProperty;
		object? obj20 = staticResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_25(border5, obj20);
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		border5.Child = panel;
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		Controls children10 = panel4.Children;
		Border border7;
		Border border6 = (border7 = new Border());
		((ISupportInitialize)border6).BeginInit();
		children10.Add(border6);
		border7.Name = "MessageBackgroundHighlightBorder";
		obj = border7;
		context.AvaloniaNameScope.Register("MessageBackgroundHighlightBorder", obj);
		border7.Opacity = 0.25;
		((ISupportInitialize)border7).EndInit();
		Controls children11 = panel4.Children;
		Grid grid15;
		Grid grid14 = (grid15 = new Grid());
		((ISupportInitialize)grid14).BeginInit();
		children11.Add(grid14);
		Grid grid16 = (grid4 = grid15);
		context.PushParent(grid4);
		Grid grid17 = grid4;
		grid17.Margin = new Thickness(26.0, 3.0, 26.0, 3.0);
		grid17.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(34.0, GridUnitType.Pixel)
		});
		grid17.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(16.0, GridUnitType.Pixel)
		});
		grid17.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid17.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid17.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		Controls children12 = grid17.Children;
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		children12.Add(contentControl);
		ContentControl contentControl4;
		ContentControl contentControl3 = (contentControl4 = contentControl2);
		context.PushParent(contentControl4);
		ContentControl contentControl5 = contentControl4;
		Grid.SetRow(contentControl5, 0);
		Grid.SetColumn(contentControl5, 0);
		Grid.SetColumnSpan(contentControl5, 3);
		CompiledBindingExtension compiledBindingExtension34 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessageRepliesViewModel_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension35 = compiledBindingExtension34.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_4(contentControl5, compiledBindingExtension35);
		StyledProperty<bool> isVisibleProperty13 = Visual.IsVisibleProperty;
		MultiBinding multiBinding10 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding11 = multiBinding2;
		StaticResourceExtension staticResourceExtension10 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj21 = staticResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding11.Converter = (IMultiValueConverter)obj21;
		IList<IBinding> bindings11 = multiBinding11.Bindings;
		CompiledBindingExtension obj22 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EHasMessageReply_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item11 = obj22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings11.Add(item11);
		IList<IBinding> bindings12 = multiBinding11.Bindings;
		CompiledBindingExtension compiledBindingExtension36 = new CompiledBindingExtension();
		compiledBindingExtension36.Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002ESenderMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainerMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EIsBlocked_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension36.FallbackValue = "True";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item12 = compiledBindingExtension36.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings12.Add(item12);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(contentControl5, isVisibleProperty13, multiBinding10);
		context.PopParent();
		((ISupportInitialize)contentControl3).EndInit();
		Controls children13 = grid17.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children13.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		button4.Classes.Add("BasicButton");
		Grid.SetRow(button4, 1);
		Grid.SetColumn(button4, 0);
		button4.HorizontalAlignment = HorizontalAlignment.Left;
		button4.HorizontalContentAlignment = HorizontalAlignment.Left;
		button4.Background = new ImmutableSolidColorBrush(16777215u);
		button4.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button4.VerticalAlignment = VerticalAlignment.Top;
		StyledProperty<bool> isVisibleProperty14 = Visual.IsVisibleProperty;
		MultiBinding multiBinding12 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding13 = multiBinding2;
		StaticResourceExtension staticResourceExtension11 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj23 = staticResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding13.Converter = (IMultiValueConverter)obj23;
		IList<IBinding> bindings13 = multiBinding13.Bindings;
		CompiledBindingExtension obj24 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EShowUserProfile_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item13 = obj24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings13.Add(item13);
		IList<IBinding> bindings14 = multiBinding13.Bindings;
		CompiledBindingExtension compiledBindingExtension37 = new CompiledBindingExtension();
		compiledBindingExtension37.Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002ESenderMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainerMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EIsBlocked_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension37.FallbackValue = "True";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item14 = compiledBindingExtension37.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings14.Add(item14);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(button4, isVisibleProperty14, multiBinding12);
		UserContextMenuView userContextMenuView;
		UserContextMenuView contextFlyout = (userContextMenuView = new UserContextMenuView());
		context.PushParent(userContextMenuView);
		UserContextMenuView userContextMenuView2 = userContextMenuView;
		CompiledBindingExtension obj25 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EUserContextMenu_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.System_002ELazy_00601_002CSystem_002ERuntime_002EValue_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true).Build())
		{
			FallbackValue = null
		};
		context.ProvideTargetProperty = UserContextMenuView.DataContextProperty;
		CompiledBindingExtension compiledBindingExtension38 = obj25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_22(userContextMenuView2, compiledBindingExtension38);
		context.PopParent();
		button4.ContextFlyout = contextFlyout;
		RootFlyout rootFlyout;
		RootFlyout flyout = (rootFlyout = new RootFlyout());
		context.PushParent(rootFlyout);
		RootFlyout rootFlyout2 = rootFlyout;
		rootFlyout2.Placement = PlacementMode.RightEdgeAlignedTop;
		rootFlyout2.VerticalOffset = -16.0;
		rootFlyout2.LimitSizeToWindow = false;
		StyledProperty<bool> isPopupOpenProperty = RootFlyout.IsPopupOpenProperty;
		CompiledBindingExtension compiledBindingExtension39 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EIsImageProfilePopupOpen_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootFlyout.IsPopupOpenProperty;
		CompiledBindingExtension compiledBindingExtension40 = compiledBindingExtension39.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootFlyout2, isPopupOpenProperty, compiledBindingExtension40);
		rootFlyout2.Opening += context.RootObject.onImageProfilePopupOpening;
		rootFlyout2.Closing += context.RootObject.onImageProfilePopupClosing;
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		rootFlyout2.Content = rootBorder;
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		RootBorder rootBorder5 = rootBorder4;
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, backgroundProperty, binding7);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, borderBrushProperty, binding8);
		rootBorder5.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder5.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		rootBorder5.Margin = new Thickness(12.0, 12.0, 12.0, 12.0);
		StyledProperty<BoxShadows> boxShadowProperty = Border.BoxShadowProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("PopupBoxShadow");
		context.ProvideTargetProperty = Border.BoxShadowProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, boxShadowProperty, binding9);
		ContentControl contentControl7;
		ContentControl contentControl6 = (contentControl7 = new ContentControl());
		((ISupportInitialize)contentControl6).BeginInit();
		rootBorder5.Child = contentControl6;
		ContentControl contentControl8 = (contentControl4 = contentControl7);
		context.PushParent(contentControl4);
		ContentControl contentControl9 = contentControl4;
		CompiledBindingExtension compiledBindingExtension41 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMemberProfile_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension42 = compiledBindingExtension41.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_4(contentControl9, compiledBindingExtension42);
		context.PopParent();
		((ISupportInitialize)contentControl8).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		context.PopParent();
		button4.Flyout = flyout;
		RootImageLoader rootImageLoader2;
		RootImageLoader rootImageLoader = (rootImageLoader2 = new RootImageLoader());
		((ISupportInitialize)rootImageLoader).BeginInit();
		button4.Content = rootImageLoader;
		RootImageLoader rootImageLoader4;
		RootImageLoader rootImageLoader3 = (rootImageLoader4 = rootImageLoader2);
		context.PushParent(rootImageLoader4);
		rootImageLoader4.Width = 34.0;
		rootImageLoader4.Height = 34.0;
		rootImageLoader4.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<IBrush?> backgroundProperty2 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, backgroundProperty2, binding10);
		StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension43 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EProfilePictureAsyncBitmapWrapper_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
		context.ProvideTargetProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension44 = compiledBindingExtension43.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, sourceProperty, compiledBindingExtension44);
		rootImageLoader4.LoadingPlaceholderSize = 16.0;
		rootImageLoader4.Stretch = Stretch.UniformToFill;
		context.PopParent();
		((ISupportInitialize)rootImageLoader3).EndInit();
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		Controls children14 = grid17.Children;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		children14.Add(rootSvgImage);
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage5 = rootSvgImage4;
		rootSvgImage5.Name = "SystemMessageSvgImage";
		obj = rootSvgImage5;
		context.AvaloniaNameScope.Register("SystemMessageSvgImage", obj);
		Grid.SetColumn(rootSvgImage5, 0);
		Grid.SetRow(rootSvgImage5, 1);
		rootSvgImage5.VerticalAlignment = VerticalAlignment.Top;
		rootSvgImage5.HorizontalAlignment = HorizontalAlignment.Center;
		rootSvgImage5.Width = 20.0;
		rootSvgImage5.Height = 20.0;
		StyledProperty<bool> isVisibleProperty15 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension45 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EIsSystemMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension46 = compiledBindingExtension45.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage5, isVisibleProperty15, compiledBindingExtension46);
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		Controls children15 = grid17.Children;
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		children15.Add(textBlock10);
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		Grid.SetColumn(textBlock13, 0);
		Grid.SetRow(textBlock13, 1);
		textBlock13.HorizontalAlignment = HorizontalAlignment.Center;
		textBlock13.FontSize = 10.0;
		StyledProperty<string?> textProperty2 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension47 = (compiledBindingExtension24 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002ESentAtUtc_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension24);
		CompiledBindingExtension compiledBindingExtension48 = compiledBindingExtension24;
		StaticResourceExtension staticResourceExtension12 = new StaticResourceExtension("MessageTimeConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj26 = staticResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension48.Converter = (IValueConverter)obj26;
		context.PopParent();
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension49 = compiledBindingExtension47.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, textProperty2, compiledBindingExtension49);
		StaticResourceExtension staticResourceExtension13 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj27 = staticResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock13, obj27);
		textBlock13.FontWeight = FontWeight.Normal;
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, foregroundProperty3, binding11);
		textBlock13.VerticalAlignment = VerticalAlignment.Center;
		textBlock13.Margin = new Thickness(-26.0, 0.0, -26.0, 0.0);
		StyledProperty<bool> isVisibleProperty16 = Visual.IsVisibleProperty;
		MultiBinding multiBinding14 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding15 = multiBinding2;
		StaticResourceExtension staticResourceExtension14 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj28 = staticResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding15.Converter = (IMultiValueConverter)obj28;
		IList<IBinding> bindings15 = multiBinding15.Bindings;
		CompiledBindingExtension obj29 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EShowUserProfile_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item15 = obj29.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings15.Add(item15);
		IList<IBinding> bindings16 = multiBinding15.Bindings;
		CompiledBindingExtension obj30 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EIsSystemMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item16 = obj30.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings16.Add(item16);
		IList<IBinding> bindings17 = multiBinding15.Bindings;
		CompiledBindingExtension obj31 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "MessageBackgroundBorder").Property(InputElement.IsPointerOverProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item17 = obj31.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings17.Add(item17);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(textBlock13, isVisibleProperty16, multiBinding14);
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		Controls children16 = grid17.Children;
		ContentControl contentControl11;
		ContentControl contentControl10 = (contentControl11 = new ContentControl());
		((ISupportInitialize)contentControl10).BeginInit();
		children16.Add(contentControl10);
		ContentControl contentControl12 = (contentControl4 = contentControl11);
		context.PushParent(contentControl4);
		ContentControl contentControl13 = contentControl4;
		Grid.SetColumn(contentControl13, 2);
		Grid.SetRow(contentControl13, 1);
		contentControl13.Margin = new Thickness(0.0, 8.0, 0.0, 8.0);
		CompiledBindingExtension compiledBindingExtension50 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EEditMessageTextboxViewModel_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension51 = compiledBindingExtension50.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_4(contentControl13, compiledBindingExtension51);
		context.PopParent();
		((ISupportInitialize)contentControl12).EndInit();
		Controls children17 = grid17.Children;
		Grid grid19;
		Grid grid18 = (grid19 = new Grid());
		((ISupportInitialize)grid18).BeginInit();
		children17.Add(grid18);
		Grid grid20 = (grid4 = grid19);
		context.PushParent(grid4);
		Grid grid21 = grid4;
		Grid.SetColumn(grid21, 2);
		Grid.SetRow(grid21, 1);
		StyledProperty<bool> isVisibleProperty17 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension52 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EIsInEditMode_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension53 = compiledBindingExtension52.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(grid21, isVisibleProperty17, compiledBindingExtension53);
		grid21.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid21.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid21.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(8.0, GridUnitType.Pixel)
		});
		grid21.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid21.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid21.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		grid21.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid21.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid21.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid21.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid21.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid21.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		Controls children18 = grid21.Children;
		RootLinkButton rootLinkButton2;
		RootLinkButton rootLinkButton = (rootLinkButton2 = new RootLinkButton());
		((ISupportInitialize)rootLinkButton).BeginInit();
		children18.Add(rootLinkButton);
		RootLinkButton rootLinkButton4;
		RootLinkButton rootLinkButton3 = (rootLinkButton4 = rootLinkButton2);
		context.PushParent(rootLinkButton4);
		RootLinkButton rootLinkButton5 = rootLinkButton4;
		Grid.SetColumn(rootLinkButton5, 0);
		Grid.SetRow(rootLinkButton5, 0);
		rootLinkButton5.Margin = new Thickness(0.0, 0.0, 0.0, 4.0);
		rootLinkButton5.Name = "UsernameTextBlock";
		obj = rootLinkButton5;
		context.AvaloniaNameScope.Register("UsernameTextBlock", obj);
		rootLinkButton5.FontSize = 15.0;
		CompiledBindingExtension obj32 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002ESenderMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainerMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EUserName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "[Username]"
		};
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension54 = obj32.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_4(rootLinkButton5, compiledBindingExtension54);
		StaticResourceExtension staticResourceExtension15 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj33 = staticResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(rootLinkButton5, obj33);
		rootLinkButton5.FontWeight = FontWeight.Medium;
		StyledProperty<bool> isVisibleProperty18 = Visual.IsVisibleProperty;
		MultiBinding multiBinding16 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding17 = multiBinding2;
		StaticResourceExtension staticResourceExtension16 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj34 = staticResourceExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding17.Converter = (IMultiValueConverter)obj34;
		IList<IBinding> bindings18 = multiBinding17.Bindings;
		CompiledBindingExtension obj35 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EShowUserProfile_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item18 = obj35.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings18.Add(item18);
		IList<IBinding> bindings19 = multiBinding17.Bindings;
		CompiledBindingExtension compiledBindingExtension55 = new CompiledBindingExtension();
		compiledBindingExtension55.Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002ESenderMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainerMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EIsBlocked_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension55.FallbackValue = "True";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item19 = compiledBindingExtension55.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings19.Add(item19);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootLinkButton5, isVisibleProperty18, multiBinding16);
		UserContextMenuView contextFlyout2 = (userContextMenuView = new UserContextMenuView());
		context.PushParent(userContextMenuView);
		UserContextMenuView userContextMenuView3 = userContextMenuView;
		CompiledBindingExtension obj36 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EUserContextMenu_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.System_002ELazy_00601_002CSystem_002ERuntime_002EValue_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true).Build())
		{
			FallbackValue = null
		};
		context.ProvideTargetProperty = UserContextMenuView.DataContextProperty;
		CompiledBindingExtension compiledBindingExtension56 = obj36.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_22(userContextMenuView3, compiledBindingExtension56);
		context.PopParent();
		rootLinkButton5.ContextFlyout = contextFlyout2;
		RootFlyout flyout2 = (rootFlyout = new RootFlyout());
		context.PushParent(rootFlyout);
		RootFlyout rootFlyout3 = rootFlyout;
		rootFlyout3.Placement = PlacementMode.RightEdgeAlignedTop;
		rootFlyout3.VerticalOffset = -16.0;
		rootFlyout3.LimitSizeToWindow = false;
		StyledProperty<bool> isPopupOpenProperty2 = RootFlyout.IsPopupOpenProperty;
		CompiledBindingExtension compiledBindingExtension57 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EIsUsernameProfilePopupOpen_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootFlyout.IsPopupOpenProperty;
		CompiledBindingExtension compiledBindingExtension58 = compiledBindingExtension57.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootFlyout3, isPopupOpenProperty2, compiledBindingExtension58);
		rootFlyout3.Opening += context.RootObject.onUsernameProfilePopupOpening;
		rootFlyout3.Closing += context.RootObject.onUsernameProfilePopupClosing;
		RootBorder rootBorder7;
		RootBorder rootBorder6 = (rootBorder7 = new RootBorder());
		((ISupportInitialize)rootBorder6).BeginInit();
		rootFlyout3.Content = rootBorder6;
		RootBorder rootBorder8 = (rootBorder4 = rootBorder7);
		context.PushParent(rootBorder4);
		RootBorder rootBorder9 = rootBorder4;
		StyledProperty<IBrush?> backgroundProperty3 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding12 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, backgroundProperty3, binding12);
		StyledProperty<IBrush?> borderBrushProperty2 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding13 = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, borderBrushProperty2, binding13);
		rootBorder9.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder9.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		rootBorder9.Margin = new Thickness(12.0, 12.0, 12.0, 12.0);
		StyledProperty<BoxShadows> boxShadowProperty2 = Border.BoxShadowProperty;
		DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("PopupBoxShadow");
		context.ProvideTargetProperty = Border.BoxShadowProperty;
		IBinding binding14 = dynamicResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, boxShadowProperty2, binding14);
		ContentControl contentControl15;
		ContentControl contentControl14 = (contentControl15 = new ContentControl());
		((ISupportInitialize)contentControl14).BeginInit();
		rootBorder9.Child = contentControl14;
		ContentControl contentControl16 = (contentControl4 = contentControl15);
		context.PushParent(contentControl4);
		ContentControl contentControl17 = contentControl4;
		CompiledBindingExtension compiledBindingExtension59 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMemberProfile_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension60 = compiledBindingExtension59.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_4(contentControl17, compiledBindingExtension60);
		context.PopParent();
		((ISupportInitialize)contentControl16).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder8).EndInit();
		context.PopParent();
		rootLinkButton5.Flyout = flyout2;
		context.PopParent();
		((ISupportInitialize)rootLinkButton3).EndInit();
		Controls children19 = grid21.Children;
		RootSvgImage rootSvgImage7;
		RootSvgImage rootSvgImage6 = (rootSvgImage7 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage6).BeginInit();
		children19.Add(rootSvgImage6);
		RootSvgImage rootSvgImage8 = (rootSvgImage4 = rootSvgImage7);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage9 = rootSvgImage4;
		Grid.SetColumn(rootSvgImage9, 1);
		Grid.SetRow(rootSvgImage9, 0);
		rootSvgImage9.Width = 16.0;
		rootSvgImage9.Height = 16.0;
		rootSvgImage9.VerticalAlignment = VerticalAlignment.Center;
		rootSvgImage9.Margin = new Thickness(2.0, 0.0, 0.0, 4.0);
		StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
		CompiledBindingExtension compiledBindingExtension61 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EPrimaryBadgeSvgPath_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		CompiledBindingExtension compiledBindingExtension62 = compiledBindingExtension61.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage9, svgPathProperty, compiledBindingExtension62);
		ToolTip.SetPlacement(rootSvgImage9, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgImage9, -2.0);
		ToolTip.SetShowDelay(rootSvgImage9, 300);
		StyledProperty<bool> isVisibleProperty19 = Visual.IsVisibleProperty;
		MultiBinding multiBinding18 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding19 = multiBinding2;
		StaticResourceExtension staticResourceExtension17 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj37 = staticResourceExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding19.Converter = (IMultiValueConverter)obj37;
		IList<IBinding> bindings20 = multiBinding19.Bindings;
		CompiledBindingExtension obj38 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EShowUserProfile_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item20 = obj38.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings20.Add(item20);
		IList<IBinding> bindings21 = multiBinding19.Bindings;
		CompiledBindingExtension compiledBindingExtension63 = new CompiledBindingExtension();
		compiledBindingExtension63.Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002ESenderMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainerMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EIsBlocked_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension63.FallbackValue = "True";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item21 = compiledBindingExtension63.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings21.Add(item21);
		IList<IBinding> bindings22 = multiBinding19.Bindings;
		CompiledBindingExtension compiledBindingExtension64 = new CompiledBindingExtension();
		compiledBindingExtension64.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EPrimaryBadgeSvgPath_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build();
		compiledBindingExtension64.Converter = StringConverters.IsNotNullOrEmpty;
		compiledBindingExtension64.FallbackValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item22 = compiledBindingExtension64.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings22.Add(item22);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgImage9, isVisibleProperty19, multiBinding18);
		RootToolTip rootToolTip2;
		RootToolTip rootToolTip = (rootToolTip2 = new RootToolTip());
		((ISupportInitialize)rootToolTip).BeginInit();
		ToolTip.SetTip(rootSvgImage9, rootToolTip);
		RootToolTip rootToolTip4;
		RootToolTip rootToolTip3 = (rootToolTip4 = rootToolTip2);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip5 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip5, PlacementMode.Top);
		Grid grid23;
		Grid grid22 = (grid23 = new Grid());
		((ISupportInitialize)grid22).BeginInit();
		rootToolTip5.Content = grid22;
		Grid grid24 = (grid4 = grid23);
		context.PushParent(grid4);
		Grid grid25 = grid4;
		grid25.MaxWidth = 300.0;
		ColumnDefinitions columnDefinitions = new ColumnDefinitions();
		columnDefinitions.Capacity = 3;
		columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(12.0, GridUnitType.Pixel)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		grid25.ColumnDefinitions = columnDefinitions;
		Controls children20 = grid25.Children;
		RootSvgImage rootSvgImage11;
		RootSvgImage rootSvgImage10 = (rootSvgImage11 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage10).BeginInit();
		children20.Add(rootSvgImage10);
		RootSvgImage rootSvgImage12 = (rootSvgImage4 = rootSvgImage11);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage13 = rootSvgImage4;
		rootSvgImage13.Width = 36.0;
		rootSvgImage13.Height = 36.0;
		StyledProperty<string?> svgPathProperty2 = RootSvgImage.SvgPathProperty;
		CompiledBindingExtension compiledBindingExtension65 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EPrimaryBadgeSvgPath_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		CompiledBindingExtension compiledBindingExtension66 = compiledBindingExtension65.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage13, svgPathProperty2, compiledBindingExtension66);
		context.PopParent();
		((ISupportInitialize)rootSvgImage12).EndInit();
		Controls children21 = grid25.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children21.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		Grid.SetColumn(stackPanel5, 2);
		stackPanel5.Spacing = 2.0;
		stackPanel5.VerticalAlignment = VerticalAlignment.Center;
		Controls children22 = stackPanel5.Children;
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		children22.Add(textBlock14);
		TextBlock textBlock16 = (textBlock4 = textBlock15);
		context.PushParent(textBlock4);
		TextBlock textBlock17 = textBlock4;
		StyledProperty<string?> textProperty3 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension67 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EPrimaryBadgeName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension68 = compiledBindingExtension67.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock17, textProperty3, compiledBindingExtension68);
		textBlock17.HorizontalAlignment = HorizontalAlignment.Center;
		StaticResourceExtension staticResourceExtension18 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj39 = staticResourceExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock17, obj39);
		textBlock17.FontWeight = FontWeight.DemiBold;
		textBlock17.FontSize = 14.0;
		StyledProperty<IBrush?> foregroundProperty4 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension15 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding15 = dynamicResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock17, foregroundProperty4, binding15);
		textBlock17.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)textBlock16).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid24).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgImage8).EndInit();
		Controls children23 = grid21.Children;
		Border border9;
		Border border8 = (border9 = new Border());
		((ISupportInitialize)border8).BeginInit();
		children23.Add(border8);
		Border border10 = (border4 = border9);
		context.PushParent(border4);
		Border border11 = border4;
		Grid.SetColumn(border11, 1);
		Grid.SetRow(border11, 0);
		StyledProperty<IBrush?> backgroundProperty4 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension16 = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding16 = dynamicResourceExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border11, backgroundProperty4, binding16);
		border11.CornerRadius = new CornerRadius(4.0, 4.0, 4.0, 4.0);
		border11.Margin = new Thickness(6.0, 0.0, 0.0, 4.0);
		border11.Padding = new Thickness(4.0, 2.0, 4.0, 2.0);
		border11.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<bool> isVisibleProperty20 = Visual.IsVisibleProperty;
		MultiBinding multiBinding20 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding21 = multiBinding2;
		StaticResourceExtension staticResourceExtension19 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj40 = staticResourceExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding21.Converter = (IMultiValueConverter)obj40;
		IList<IBinding> bindings23 = multiBinding21.Bindings;
		CompiledBindingExtension obj41 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EShowUserProfile_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item23 = obj41.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings23.Add(item23);
		IList<IBinding> bindings24 = multiBinding21.Bindings;
		CompiledBindingExtension compiledBindingExtension69 = new CompiledBindingExtension();
		compiledBindingExtension69.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002ESenderMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainerMember_002CRootApp_002EClient_002ECoreDomain_002EIsApp_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension69.FallbackValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item24 = compiledBindingExtension69.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings24.Add(item24);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(border11, isVisibleProperty20, multiBinding20);
		TextBlock textBlock19;
		TextBlock textBlock18 = (textBlock19 = new TextBlock());
		((ISupportInitialize)textBlock18).BeginInit();
		border11.Child = textBlock18;
		TextBlock textBlock20 = (textBlock4 = textBlock19);
		context.PushParent(textBlock4);
		TextBlock textBlock21 = textBlock4;
		textBlock21.FontSize = 10.0;
		StyledProperty<IBrush?> foregroundProperty5 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension17 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding17 = dynamicResourceExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock21, foregroundProperty5, binding17);
		StaticResourceExtension staticResourceExtension20 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj42 = staticResourceExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock21, obj42);
		textBlock21.FontWeight = FontWeight.DemiBold;
		textBlock21.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.App;
		textBlock21.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock20).EndInit();
		context.PopParent();
		((ISupportInitialize)border10).EndInit();
		Controls children24 = grid21.Children;
		TextBlock textBlock23;
		TextBlock textBlock22 = (textBlock23 = new TextBlock());
		((ISupportInitialize)textBlock22).BeginInit();
		children24.Add(textBlock22);
		TextBlock textBlock24 = (textBlock4 = textBlock23);
		context.PushParent(textBlock4);
		TextBlock textBlock25 = textBlock4;
		Grid.SetColumn(textBlock25, 3);
		Grid.SetRow(textBlock25, 0);
		textBlock25.HorizontalAlignment = HorizontalAlignment.Left;
		textBlock25.FontSize = 12.0;
		StyledProperty<string?> textProperty4 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension70 = (compiledBindingExtension24 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002ESentAtUtc_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension24);
		CompiledBindingExtension compiledBindingExtension71 = compiledBindingExtension24;
		StaticResourceExtension staticResourceExtension21 = new StaticResourceExtension("MessageDateConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj43 = staticResourceExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension71.Converter = (IValueConverter)obj43;
		context.PopParent();
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension72 = compiledBindingExtension70.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock25, textProperty4, compiledBindingExtension72);
		StaticResourceExtension staticResourceExtension22 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj44 = staticResourceExtension22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock25, obj44);
		textBlock25.FontWeight = (FontWeight)450;
		textBlock25.Margin = new Thickness(0.0, 0.0, 0.0, 1.0);
		StyledProperty<IBrush?> foregroundProperty6 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension18 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding18 = dynamicResourceExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock25, foregroundProperty6, binding18);
		textBlock25.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<bool> isVisibleProperty21 = Visual.IsVisibleProperty;
		MultiBinding multiBinding22 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding23 = multiBinding2;
		StaticResourceExtension staticResourceExtension23 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj45 = staticResourceExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding23.Converter = (IMultiValueConverter)obj45;
		IList<IBinding> bindings25 = multiBinding23.Bindings;
		CompiledBindingExtension obj46 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EShowUserProfile_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item25 = obj46.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings25.Add(item25);
		IList<IBinding> bindings26 = multiBinding23.Bindings;
		CompiledBindingExtension compiledBindingExtension73 = new CompiledBindingExtension();
		compiledBindingExtension73.Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002ESenderMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainerMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EIsBlocked_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension73.FallbackValue = "True";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item26 = compiledBindingExtension73.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings26.Add(item26);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(textBlock25, isVisibleProperty21, multiBinding22);
		context.PopParent();
		((ISupportInitialize)textBlock24).EndInit();
		Controls children25 = grid21.Children;
		RootMarkdownTextBlock rootMarkdownTextBlock2;
		RootMarkdownTextBlock rootMarkdownTextBlock = (rootMarkdownTextBlock2 = new RootMarkdownTextBlock());
		((ISupportInitialize)rootMarkdownTextBlock).BeginInit();
		children25.Add(rootMarkdownTextBlock);
		RootMarkdownTextBlock rootMarkdownTextBlock4;
		RootMarkdownTextBlock rootMarkdownTextBlock3 = (rootMarkdownTextBlock4 = rootMarkdownTextBlock2);
		context.PushParent(rootMarkdownTextBlock4);
		rootMarkdownTextBlock4.Name = "MessageTextBlock";
		obj = rootMarkdownTextBlock4;
		context.AvaloniaNameScope.Register("MessageTextBlock", obj);
		Grid.SetColumn(rootMarkdownTextBlock4, 0);
		Grid.SetColumnSpan(rootMarkdownTextBlock4, 4);
		Grid.SetRow(rootMarkdownTextBlock4, 1);
		StyledProperty<IMarkdownEngine?> engineProperty = RootMarkdownTextBlock.EngineProperty;
		CompiledBindingExtension compiledBindingExtension74 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMarkdownEngine_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootMarkdownTextBlock.EngineProperty;
		CompiledBindingExtension compiledBindingExtension75 = compiledBindingExtension74.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMarkdownTextBlock4, engineProperty, compiledBindingExtension75);
		DirectProperty<RootMarkdownTextBlock, string?> markdownProperty = RootMarkdownTextBlock.MarkdownProperty;
		CompiledBindingExtension compiledBindingExtension76 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EMessageContent_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootMarkdownTextBlock.MarkdownProperty;
		CompiledBindingExtension compiledBindingExtension77 = compiledBindingExtension76.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMarkdownTextBlock4, markdownProperty, compiledBindingExtension77);
		rootMarkdownTextBlock4.Cursor = new Cursor(StandardCursorType.Ibeam);
		RenderOptions.SetBitmapInterpolationMode(rootMarkdownTextBlock4, BitmapInterpolationMode.MediumQuality);
		StyledProperty<bool> isVisibleProperty22 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj47 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002ESenderMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainerMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EIsBlocked_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "True"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension78 = obj47.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMarkdownTextBlock4, isVisibleProperty22, compiledBindingExtension78);
		context.PopParent();
		((ISupportInitialize)rootMarkdownTextBlock3).EndInit();
		Controls children26 = grid21.Children;
		TextBlock textBlock27;
		TextBlock textBlock26 = (textBlock27 = new TextBlock());
		((ISupportInitialize)textBlock26).BeginInit();
		children26.Add(textBlock26);
		TextBlock textBlock28 = (textBlock4 = textBlock27);
		context.PushParent(textBlock4);
		TextBlock textBlock29 = textBlock4;
		Grid.SetColumn(textBlock29, 0);
		Grid.SetColumnSpan(textBlock29, 4);
		Grid.SetRow(textBlock29, 1);
		textBlock29.FontSize = 15.0;
		StaticResourceExtension staticResourceExtension24 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj48 = staticResourceExtension24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock29, obj48);
		textBlock29.FontWeight = FontWeight.Normal;
		textBlock29.FontStyle = FontStyle.Italic;
		StyledProperty<IBrush?> foregroundProperty7 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension19 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding19 = dynamicResourceExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock29, foregroundProperty7, binding19);
		textBlock29.LineSpacing = 2.0;
		textBlock29.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.BlockedMessage;
		StyledProperty<bool> isVisibleProperty23 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj49 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002ESenderMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainerMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EIsBlocked_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "False"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension79 = obj49.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock29, isVisibleProperty23, compiledBindingExtension79);
		context.PopParent();
		((ISupportInitialize)textBlock28).EndInit();
		Controls children27 = grid21.Children;
		ItemsControl itemsControl2;
		ItemsControl itemsControl = (itemsControl2 = new ItemsControl());
		((ISupportInitialize)itemsControl).BeginInit();
		children27.Add(itemsControl);
		ItemsControl itemsControl4;
		ItemsControl itemsControl3 = (itemsControl4 = itemsControl2);
		context.PushParent(itemsControl4);
		ItemsControl itemsControl5 = itemsControl4;
		StyledProperty<IEnumerable?> itemsSourceProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension80 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002ELinks_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension81 = compiledBindingExtension80.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl5, itemsSourceProperty, compiledBindingExtension81);
		Grid.SetColumn(itemsControl5, 0);
		Grid.SetColumnSpan(itemsControl5, 4);
		Grid.SetRow(itemsControl5, 2);
		StyledProperty<bool> isVisibleProperty24 = Visual.IsVisibleProperty;
		MultiBinding multiBinding24 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding25 = multiBinding2;
		StaticResourceExtension staticResourceExtension25 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj50 = staticResourceExtension25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding25.Converter = (IMultiValueConverter)obj50;
		IList<IBinding> bindings27 = multiBinding25.Bindings;
		CompiledBindingExtension compiledBindingExtension82 = new CompiledBindingExtension();
		compiledBindingExtension82.Path = new CompiledBindingPathBuilder(1).Not().Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002ELinks_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.System_002ECollections_002EObjectModel_002EReadOnlyCollection_00601_002CSystem_002ERuntime_002ECount_91_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
			.Build();
		compiledBindingExtension82.FallbackValue = "False";
		compiledBindingExtension82.TargetNullValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item27 = compiledBindingExtension82.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings27.Add(item27);
		IList<IBinding> bindings28 = multiBinding25.Bindings;
		CompiledBindingExtension compiledBindingExtension83 = new CompiledBindingExtension();
		compiledBindingExtension83.Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002ESenderMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainerMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EIsBlocked_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension83.FallbackValue = "True";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item28 = compiledBindingExtension83.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings28.Add(item28);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(itemsControl5, isVisibleProperty24, multiBinding24);
		itemsControl5.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_177.Build_6), context)
		};
		context.PopParent();
		((ISupportInitialize)itemsControl3).EndInit();
		Controls children28 = grid21.Children;
		ItemsControl itemsControl7;
		ItemsControl itemsControl6 = (itemsControl7 = new ItemsControl());
		((ISupportInitialize)itemsControl6).BeginInit();
		children28.Add(itemsControl6);
		ItemsControl itemsControl8 = (itemsControl4 = itemsControl7);
		context.PushParent(itemsControl4);
		ItemsControl itemsControl9 = itemsControl4;
		StyledProperty<IEnumerable?> itemsSourceProperty2 = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension84 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMedia_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension85 = compiledBindingExtension84.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl9, itemsSourceProperty2, compiledBindingExtension85);
		Grid.SetColumn(itemsControl9, 0);
		Grid.SetColumnSpan(itemsControl9, 4);
		Grid.SetRow(itemsControl9, 3);
		StyledProperty<bool> isVisibleProperty25 = Visual.IsVisibleProperty;
		MultiBinding multiBinding26 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding27 = multiBinding2;
		StaticResourceExtension staticResourceExtension26 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj51 = staticResourceExtension26.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding27.Converter = (IMultiValueConverter)obj51;
		IList<IBinding> bindings29 = multiBinding27.Bindings;
		CompiledBindingExtension compiledBindingExtension86 = new CompiledBindingExtension();
		compiledBindingExtension86.Path = new CompiledBindingPathBuilder(1).Not().Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMedia_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.System_002ECollections_002EObjectModel_002EReadOnlyCollection_00601_002CSystem_002ERuntime_002ECount_84_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
			.Build();
		compiledBindingExtension86.FallbackValue = "False";
		compiledBindingExtension86.TargetNullValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item29 = compiledBindingExtension86.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings29.Add(item29);
		IList<IBinding> bindings30 = multiBinding27.Bindings;
		CompiledBindingExtension compiledBindingExtension87 = new CompiledBindingExtension();
		compiledBindingExtension87.Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002ESenderMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainerMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EIsBlocked_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension87.FallbackValue = "True";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item30 = compiledBindingExtension87.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings30.Add(item30);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(itemsControl9, isVisibleProperty25, multiBinding26);
		itemsControl9.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_177.Build_7), context)
		};
		context.PopParent();
		((ISupportInitialize)itemsControl8).EndInit();
		Controls children29 = grid21.Children;
		ItemsControl itemsControl11;
		ItemsControl itemsControl10 = (itemsControl11 = new ItemsControl());
		((ISupportInitialize)itemsControl10).BeginInit();
		children29.Add(itemsControl10);
		ItemsControl itemsControl12 = (itemsControl4 = itemsControl11);
		context.PushParent(itemsControl4);
		ItemsControl itemsControl13 = itemsControl4;
		StyledProperty<IEnumerable?> itemsSourceProperty3 = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension88 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EFiles_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension89 = compiledBindingExtension88.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl13, itemsSourceProperty3, compiledBindingExtension89);
		Grid.SetColumn(itemsControl13, 0);
		Grid.SetColumnSpan(itemsControl13, 4);
		Grid.SetRow(itemsControl13, 4);
		StyledProperty<bool> isVisibleProperty26 = Visual.IsVisibleProperty;
		MultiBinding multiBinding28 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding29 = multiBinding2;
		StaticResourceExtension staticResourceExtension27 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj52 = staticResourceExtension27.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding29.Converter = (IMultiValueConverter)obj52;
		IList<IBinding> bindings31 = multiBinding29.Bindings;
		CompiledBindingExtension compiledBindingExtension90 = new CompiledBindingExtension();
		compiledBindingExtension90.Path = new CompiledBindingPathBuilder(1).Not().Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EFiles_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.System_002ECollections_002EObjectModel_002EReadOnlyCollection_00601_002CSystem_002ERuntime_002ECount_84_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
			.Build();
		compiledBindingExtension90.FallbackValue = "False";
		compiledBindingExtension90.TargetNullValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item31 = compiledBindingExtension90.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings31.Add(item31);
		IList<IBinding> bindings32 = multiBinding29.Bindings;
		CompiledBindingExtension compiledBindingExtension91 = new CompiledBindingExtension();
		compiledBindingExtension91.Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002ESenderMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainerMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EIsBlocked_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension91.FallbackValue = "True";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item32 = compiledBindingExtension91.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings32.Add(item32);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(itemsControl13, isVisibleProperty26, multiBinding28);
		itemsControl13.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_177.Build_8), context)
		};
		context.PopParent();
		((ISupportInitialize)itemsControl12).EndInit();
		Controls children30 = grid21.Children;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		children30.Add(stackPanel6);
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		Grid.SetColumn(stackPanel9, 0);
		Grid.SetColumnSpan(stackPanel9, 4);
		Grid.SetRow(stackPanel9, 5);
		stackPanel9.Orientation = Orientation.Horizontal;
		stackPanel9.Spacing = 4.0;
		stackPanel9.Margin = new Thickness(0.0, 4.0, 0.0, 0.0);
		StyledProperty<bool> isVisibleProperty27 = Visual.IsVisibleProperty;
		MultiBinding multiBinding30 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding31 = multiBinding2;
		StaticResourceExtension staticResourceExtension28 = new StaticResourceExtension("OrConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj53 = staticResourceExtension28.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding31.Converter = (IMultiValueConverter)obj53;
		IList<IBinding> bindings33 = multiBinding31.Bindings;
		CompiledBindingExtension compiledBindingExtension92 = new CompiledBindingExtension();
		compiledBindingExtension92.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EEditedAt_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build();
		compiledBindingExtension92.Converter = ObjectConverters.IsNotNull;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item33 = compiledBindingExtension92.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings33.Add(item33);
		IList<IBinding> bindings34 = multiBinding31.Bindings;
		CompiledBindingExtension compiledBindingExtension93 = new CompiledBindingExtension();
		compiledBindingExtension93.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EPinnedAt_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build();
		compiledBindingExtension93.Converter = ObjectConverters.IsNotNull;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item34 = compiledBindingExtension93.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings34.Add(item34);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(stackPanel9, isVisibleProperty27, multiBinding30);
		Controls children31 = stackPanel9.Children;
		TextBlock textBlock31;
		TextBlock textBlock30 = (textBlock31 = new TextBlock());
		((ISupportInitialize)textBlock30).BeginInit();
		children31.Add(textBlock30);
		TextBlock textBlock32 = (textBlock4 = textBlock31);
		context.PushParent(textBlock4);
		TextBlock textBlock33 = textBlock4;
		textBlock33.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Edited;
		textBlock33.FontSize = 12.0;
		StaticResourceExtension staticResourceExtension29 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj54 = staticResourceExtension29.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock33, obj54);
		textBlock33.FontWeight = (FontWeight)450;
		StyledProperty<bool> isVisibleProperty28 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj55 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EEditedAt_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Converter = ObjectConverters.IsNotNull
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension94 = obj55.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock33, isVisibleProperty28, compiledBindingExtension94);
		StyledProperty<IBrush?> foregroundProperty8 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension20 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding20 = dynamicResourceExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock33, foregroundProperty8, binding20);
		ToolTip.SetPlacement(textBlock33, PlacementMode.Top);
		ToolTip.SetVerticalOffset(textBlock33, -2.0);
		ToolTip.SetShowDelay(textBlock33, 300);
		RootToolTip rootToolTip7;
		RootToolTip rootToolTip6 = (rootToolTip7 = new RootToolTip());
		((ISupportInitialize)rootToolTip6).BeginInit();
		ToolTip.SetTip(textBlock33, rootToolTip6);
		RootToolTip rootToolTip8 = (rootToolTip4 = rootToolTip7);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip9 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip9, PlacementMode.Top);
		TextBlock textBlock35;
		TextBlock textBlock34 = (textBlock35 = new TextBlock());
		((ISupportInitialize)textBlock34).BeginInit();
		rootToolTip9.Content = textBlock34;
		TextBlock textBlock36 = (textBlock4 = textBlock35);
		context.PushParent(textBlock4);
		TextBlock textBlock37 = textBlock4;
		StyledProperty<string?> textProperty5 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension95 = (compiledBindingExtension24 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EEditedAt_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension24);
		CompiledBindingExtension compiledBindingExtension96 = compiledBindingExtension24;
		StaticResourceExtension staticResourceExtension30 = new StaticResourceExtension("FullDateTimeConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj56 = staticResourceExtension30.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension96.Converter = (IValueConverter)obj56;
		context.PopParent();
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension97 = compiledBindingExtension95.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock37, textProperty5, compiledBindingExtension97);
		StaticResourceExtension staticResourceExtension31 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj57 = staticResourceExtension31.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock37, obj57);
		textBlock37.FontWeight = (FontWeight)450;
		textBlock37.FontSize = 14.0;
		textBlock37.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock37.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock36).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip8).EndInit();
		context.PopParent();
		((ISupportInitialize)textBlock32).EndInit();
		Controls children32 = stackPanel9.Children;
		TextBlock textBlock39;
		TextBlock textBlock38 = (textBlock39 = new TextBlock());
		((ISupportInitialize)textBlock38).BeginInit();
		children32.Add(textBlock38);
		TextBlock textBlock40 = (textBlock4 = textBlock39);
		context.PushParent(textBlock4);
		TextBlock textBlock41 = textBlock4;
		textBlock41.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Pinned;
		textBlock41.FontSize = 12.0;
		StyledProperty<bool> isVisibleProperty29 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj58 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EPinnedAt_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Converter = ObjectConverters.IsNotNull
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension98 = obj58.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock41, isVisibleProperty29, compiledBindingExtension98);
		StaticResourceExtension staticResourceExtension32 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj59 = staticResourceExtension32.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock41, obj59);
		textBlock41.FontWeight = (FontWeight)450;
		StyledProperty<IBrush?> foregroundProperty9 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension21 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding21 = dynamicResourceExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock41, foregroundProperty9, binding21);
		ToolTip.SetPlacement(textBlock41, PlacementMode.Top);
		ToolTip.SetVerticalOffset(textBlock41, -2.0);
		ToolTip.SetShowDelay(textBlock41, 300);
		RootToolTip rootToolTip11;
		RootToolTip rootToolTip10 = (rootToolTip11 = new RootToolTip());
		((ISupportInitialize)rootToolTip10).BeginInit();
		ToolTip.SetTip(textBlock41, rootToolTip10);
		RootToolTip rootToolTip12 = (rootToolTip4 = rootToolTip11);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip13 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip13, PlacementMode.Top);
		TextBlock textBlock43;
		TextBlock textBlock42 = (textBlock43 = new TextBlock());
		((ISupportInitialize)textBlock42).BeginInit();
		rootToolTip13.Content = textBlock42;
		TextBlock textBlock44 = (textBlock4 = textBlock43);
		context.PushParent(textBlock4);
		TextBlock textBlock45 = textBlock4;
		StyledProperty<string?> textProperty6 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension99 = (compiledBindingExtension24 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EPinnedAt_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension24);
		CompiledBindingExtension compiledBindingExtension100 = compiledBindingExtension24;
		StaticResourceExtension staticResourceExtension33 = new StaticResourceExtension("FullDateTimeConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj60 = staticResourceExtension33.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension100.Converter = (IValueConverter)obj60;
		context.PopParent();
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension101 = compiledBindingExtension99.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock45, textProperty6, compiledBindingExtension101);
		StaticResourceExtension staticResourceExtension34 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj61 = staticResourceExtension34.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock45, obj61);
		textBlock45.FontWeight = (FontWeight)450;
		textBlock45.FontSize = 14.0;
		textBlock45.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock45.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock44).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip12).EndInit();
		context.PopParent();
		((ISupportInitialize)textBlock40).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		Controls children33 = grid21.Children;
		ItemsControl itemsControl15;
		ItemsControl itemsControl14 = (itemsControl15 = new ItemsControl());
		((ISupportInitialize)itemsControl14).BeginInit();
		children33.Add(itemsControl14);
		ItemsControl itemsControl16 = (itemsControl4 = itemsControl15);
		context.PushParent(itemsControl4);
		ItemsControl itemsControl17 = itemsControl4;
		StyledProperty<IEnumerable?> itemsSourceProperty4 = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension102 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EReactions_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension103 = compiledBindingExtension102.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl17, itemsSourceProperty4, compiledBindingExtension103);
		Grid.SetColumn(itemsControl17, 0);
		Grid.SetColumnSpan(itemsControl17, 4);
		Grid.SetRow(itemsControl17, 6);
		StyledProperty<bool> isVisibleProperty30 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension104 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EReactions_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.System_002ECollections_002EObjectModel_002EReadOnlyCollection_00601_002CSystem_002ERuntime_002ECount_84_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
			.Build());
		compiledBindingExtension104.FallbackValue = "False";
		compiledBindingExtension104.TargetNullValue = "False";
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension105 = compiledBindingExtension104.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl17, isVisibleProperty30, compiledBindingExtension105);
		itemsControl17.Margin = new Thickness(0.0, 4.0, 0.0, 0.0);
		itemsControl17.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_177.Build_9), context)
		};
		context.PopParent();
		((ISupportInitialize)itemsControl16).EndInit();
		Controls children34 = grid21.Children;
		StackPanel stackPanel11;
		StackPanel stackPanel10 = (stackPanel11 = new StackPanel());
		((ISupportInitialize)stackPanel10).BeginInit();
		children34.Add(stackPanel10);
		StackPanel stackPanel12 = (stackPanel4 = stackPanel11);
		context.PushParent(stackPanel4);
		StackPanel stackPanel13 = stackPanel4;
		stackPanel13.Orientation = Orientation.Horizontal;
		Grid.SetRow(stackPanel13, 7);
		Grid.SetColumn(stackPanel13, 0);
		Grid.SetColumnSpan(stackPanel13, 4);
		stackPanel13.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		StyledProperty<bool> isVisibleProperty31 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension106 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EFailedToSend_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension107 = compiledBindingExtension106.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(stackPanel13, isVisibleProperty31, compiledBindingExtension107);
		Controls children35 = stackPanel13.Children;
		TextBlock textBlock47;
		TextBlock textBlock46 = (textBlock47 = new TextBlock());
		((ISupportInitialize)textBlock46).BeginInit();
		children35.Add(textBlock46);
		TextBlock textBlock48 = (textBlock4 = textBlock47);
		context.PushParent(textBlock4);
		TextBlock textBlock49 = textBlock4;
		textBlock49.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.FailedToSend;
		textBlock49.Margin = new Thickness(0.0, 0.0, 4.0, 0.0);
		StaticResourceExtension staticResourceExtension35 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj62 = staticResourceExtension35.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock49, obj62);
		textBlock49.FontSize = 12.0;
		textBlock49.FontWeight = (FontWeight)450;
		StyledProperty<IBrush?> foregroundProperty10 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension22 = new DynamicResourceExtension("Error");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding22 = dynamicResourceExtension22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock49, foregroundProperty10, binding22);
		textBlock49.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock49.FontStyle = FontStyle.Italic;
		context.PopParent();
		((ISupportInitialize)textBlock48).EndInit();
		Controls children36 = stackPanel13.Children;
		RootLinkButton rootLinkButton7;
		RootLinkButton rootLinkButton6 = (rootLinkButton7 = new RootLinkButton());
		((ISupportInitialize)rootLinkButton6).BeginInit();
		children36.Add(rootLinkButton6);
		RootLinkButton rootLinkButton8 = (rootLinkButton4 = rootLinkButton7);
		context.PushParent(rootLinkButton4);
		RootLinkButton rootLinkButton9 = rootLinkButton4;
		rootLinkButton9.Content = RootApp.Client.Avalonia.Resources.Strings.Resources.TryAgain;
		StyledProperty<IBrush?> foregroundProperty11 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension23 = new DynamicResourceExtension("Link");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding23 = dynamicResourceExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootLinkButton9, foregroundProperty11, binding23);
		rootLinkButton9.FontSize = 12.0;
		StaticResourceExtension staticResourceExtension36 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj63 = staticResourceExtension36.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(rootLinkButton9, obj63);
		rootLinkButton9.FontWeight = (FontWeight)450;
		rootLinkButton9.FontStyle = FontStyle.Italic;
		StyledProperty<ICommand?> commandProperty6 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension108 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002ERetrySendCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension109 = compiledBindingExtension108.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootLinkButton9, commandProperty6, compiledBindingExtension109);
		context.PopParent();
		((ISupportInitialize)rootLinkButton8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel12).EndInit();
		context.PopParent();
		((ISupportInitialize)grid20).EndInit();
		Controls children37 = grid17.Children;
		RootBorder rootBorder11;
		RootBorder rootBorder10 = (rootBorder11 = new RootBorder());
		((ISupportInitialize)rootBorder10).BeginInit();
		children37.Add(rootBorder10);
		RootBorder rootBorder12 = (rootBorder4 = rootBorder11);
		context.PushParent(rootBorder4);
		RootBorder rootBorder13 = rootBorder4;
		rootBorder13.Name = "ActionBarBorder";
		obj = rootBorder13;
		context.AvaloniaNameScope.Register("ActionBarBorder", obj);
		StyledProperty<IBrush?> backgroundProperty5 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension24 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding24 = dynamicResourceExtension24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder13, backgroundProperty5, binding24);
		rootBorder13.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		Grid.SetColumn(rootBorder13, 0);
		Grid.SetColumnSpan(rootBorder13, 4);
		Grid.SetRow(rootBorder13, 0);
		Grid.SetRowSpan(rootBorder13, 2);
		rootBorder13.IsVisible = false;
		StyledProperty<IBrush?> borderBrushProperty3 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension25 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding25 = dynamicResourceExtension25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder13, borderBrushProperty3, binding25);
		rootBorder13.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		rootBorder13.HorizontalAlignment = HorizontalAlignment.Right;
		rootBorder13.VerticalAlignment = VerticalAlignment.Top;
		rootBorder13.Margin = new Thickness(0.0, -24.0, -8.0, 0.0);
		WrapPanel wrapPanel2;
		WrapPanel wrapPanel = (wrapPanel2 = new WrapPanel());
		((ISupportInitialize)wrapPanel).BeginInit();
		rootBorder13.Child = wrapPanel;
		WrapPanel wrapPanel4;
		WrapPanel wrapPanel3 = (wrapPanel4 = wrapPanel2);
		context.PushParent(wrapPanel4);
		wrapPanel4.Margin = new Thickness(3.0, 3.0, 3.0, 3.0);
		wrapPanel4.VerticalAlignment = VerticalAlignment.Center;
		wrapPanel4.ItemSpacing = 3.0;
		Controls children38 = wrapPanel4.Children;
		RootSvgButton rootSvgButton2;
		RootSvgButton rootSvgButton = (rootSvgButton2 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton).BeginInit();
		children38.Add(rootSvgButton);
		RootSvgButton rootSvgButton4;
		RootSvgButton rootSvgButton3 = (rootSvgButton4 = rootSvgButton2);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton5 = rootSvgButton4;
		rootSvgButton5.Classes.Add("Custom");
		rootSvgButton5.Name = "AddReactionActionButton";
		obj = rootSvgButton5;
		context.AvaloniaNameScope.Register("AddReactionActionButton", obj);
		rootSvgButton5.Height = 24.0;
		rootSvgButton5.Width = 24.0;
		rootSvgButton5.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<string> svgPathProperty3 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension26 = new DynamicResourceExtension("AddReactionSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding26 = dynamicResourceExtension26.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton5, svgPathProperty3, binding26);
		rootSvgButton5.SvgWidth = 16.0;
		rootSvgButton5.SvgHeight = 16.0;
		StyledProperty<bool> isVisibleProperty32 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension110 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EMessageContainer_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainer_002CRootApp_002EClient_002ECoreDomain_002ELocalChannelPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalChannelPermission_002CRootApp_002EClient_002ECoreDomain_002EChannelCreateMessageReaction_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension111 = compiledBindingExtension110.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton5, isVisibleProperty32, compiledBindingExtension111);
		ToolTip.SetPlacement(rootSvgButton5, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgButton5, -1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton5, 0.0);
		ToolTip.SetShowDelay(rootSvgButton5, 0);
		RootToolTip rootToolTip15;
		RootToolTip rootToolTip14 = (rootToolTip15 = new RootToolTip());
		((ISupportInitialize)rootToolTip14).BeginInit();
		ToolTip.SetTip(rootSvgButton5, rootToolTip14);
		RootToolTip rootToolTip16 = (rootToolTip4 = rootToolTip15);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip17 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip17, PlacementMode.Top);
		TextBlock textBlock51;
		TextBlock textBlock50 = (textBlock51 = new TextBlock());
		((ISupportInitialize)textBlock50).BeginInit();
		rootToolTip17.Content = textBlock50;
		TextBlock textBlock52 = (textBlock4 = textBlock51);
		context.PushParent(textBlock4);
		TextBlock textBlock53 = textBlock4;
		textBlock53.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.AddReactions;
		StaticResourceExtension staticResourceExtension37 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj64 = staticResourceExtension37.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock53, obj64);
		textBlock53.FontWeight = (FontWeight)450;
		textBlock53.FontSize = 14.0;
		textBlock53.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock53.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock52).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip16).EndInit();
		RootFlyout flyout3 = (rootFlyout = new RootFlyout());
		context.PushParent(rootFlyout);
		RootFlyout rootFlyout4 = rootFlyout;
		rootFlyout4.Placement = PlacementMode.LeftEdgeAlignedTop;
		rootFlyout4.LimitSizeToWindow = false;
		rootFlyout4.Opening += context.RootObject.onFlyoutOpening;
		rootFlyout4.Closing += context.RootObject.onFlyoutClosing;
		RootBorder rootBorder15;
		RootBorder rootBorder14 = (rootBorder15 = new RootBorder());
		((ISupportInitialize)rootBorder14).BeginInit();
		rootFlyout4.Content = rootBorder14;
		RootBorder rootBorder16 = (rootBorder4 = rootBorder15);
		context.PushParent(rootBorder4);
		RootBorder rootBorder17 = rootBorder4;
		StyledProperty<IBrush?> backgroundProperty6 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension27 = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding27 = dynamicResourceExtension27.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder17, backgroundProperty6, binding27);
		StyledProperty<IBrush?> borderBrushProperty4 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension28 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding28 = dynamicResourceExtension28.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder17, borderBrushProperty4, binding28);
		rootBorder17.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder17.Height = 500.0;
		rootBorder17.Width = 450.0;
		rootBorder17.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<BoxShadows> boxShadowProperty3 = Border.BoxShadowProperty;
		DynamicResourceExtension dynamicResourceExtension29 = new DynamicResourceExtension("PopupBoxShadow");
		context.ProvideTargetProperty = Border.BoxShadowProperty;
		IBinding binding29 = dynamicResourceExtension29.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder17, boxShadowProperty3, binding29);
		rootBorder17.Margin = new Thickness(12.0, 12.0, 12.0, 12.0);
		ContentControl contentControl19;
		ContentControl contentControl18 = (contentControl19 = new ContentControl());
		((ISupportInitialize)contentControl18).BeginInit();
		rootBorder17.Child = contentControl18;
		ContentControl contentControl20 = (contentControl4 = contentControl19);
		context.PushParent(contentControl4);
		ContentControl contentControl21 = contentControl4;
		CompiledBindingExtension compiledBindingExtension112 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EEmojiPickerViewModel_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension113 = compiledBindingExtension112.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_4(contentControl21, compiledBindingExtension113);
		context.PopParent();
		((ISupportInitialize)contentControl20).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder16).EndInit();
		context.PopParent();
		rootSvgButton5.Flyout = flyout3;
		context.PopParent();
		((ISupportInitialize)rootSvgButton3).EndInit();
		Controls children39 = wrapPanel4.Children;
		RootSvgButton rootSvgButton7;
		RootSvgButton rootSvgButton6 = (rootSvgButton7 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton6).BeginInit();
		children39.Add(rootSvgButton6);
		RootSvgButton rootSvgButton8 = (rootSvgButton4 = rootSvgButton7);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton9 = rootSvgButton4;
		rootSvgButton9.Classes.Add("Custom");
		rootSvgButton9.Name = "ReplyActionButton";
		obj = rootSvgButton9;
		context.AvaloniaNameScope.Register("ReplyActionButton", obj);
		rootSvgButton9.Height = 24.0;
		rootSvgButton9.Width = 24.0;
		rootSvgButton9.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<string> svgPathProperty4 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension30 = new DynamicResourceExtension("ReplySVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding30 = dynamicResourceExtension30.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, svgPathProperty4, binding30);
		rootSvgButton9.SvgWidth = 13.0;
		rootSvgButton9.SvgHeight = 13.0;
		StyledProperty<ICommand?> commandProperty7 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension114 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EReplyToMessageCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension115 = compiledBindingExtension114.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, commandProperty7, compiledBindingExtension115);
		ToolTip.SetPlacement(rootSvgButton9, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgButton9, -1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton9, 0.0);
		ToolTip.SetShowDelay(rootSvgButton9, 0);
		RootToolTip rootToolTip19;
		RootToolTip rootToolTip18 = (rootToolTip19 = new RootToolTip());
		((ISupportInitialize)rootToolTip18).BeginInit();
		ToolTip.SetTip(rootSvgButton9, rootToolTip18);
		RootToolTip rootToolTip20 = (rootToolTip4 = rootToolTip19);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip21 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip21, PlacementMode.Top);
		TextBlock textBlock55;
		TextBlock textBlock54 = (textBlock55 = new TextBlock());
		((ISupportInitialize)textBlock54).BeginInit();
		rootToolTip21.Content = textBlock54;
		TextBlock textBlock56 = (textBlock4 = textBlock55);
		context.PushParent(textBlock4);
		TextBlock textBlock57 = textBlock4;
		textBlock57.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Reply;
		StaticResourceExtension staticResourceExtension38 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj65 = staticResourceExtension38.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock57, obj65);
		textBlock57.FontWeight = (FontWeight)450;
		textBlock57.FontSize = 14.0;
		textBlock57.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock57.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock56).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip20).EndInit();
		StyledProperty<bool> isVisibleProperty33 = Visual.IsVisibleProperty;
		MultiBinding multiBinding32 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding33 = multiBinding2;
		StaticResourceExtension staticResourceExtension39 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj66 = staticResourceExtension39.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding33.Converter = (IMultiValueConverter)obj66;
		IList<IBinding> bindings35 = multiBinding33.Bindings;
		CompiledBindingExtension obj67 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EIsSystemMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item35 = obj67.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings35.Add(item35);
		IList<IBinding> bindings36 = multiBinding33.Bindings;
		CompiledBindingExtension obj68 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EMessageContainer_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainer_002CRootApp_002EClient_002ECoreDomain_002ELocalChannelPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalChannelPermission_002CRootApp_002EClient_002ECoreDomain_002EChannelCreateMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item36 = obj68.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings36.Add(item36);
		IList<IBinding> bindings37 = multiBinding33.Bindings;
		CompiledBindingExtension compiledBindingExtension116 = new CompiledBindingExtension();
		compiledBindingExtension116.Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002ESenderMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainerMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EIsBlocked_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension116.FallbackValue = "True";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item37 = compiledBindingExtension116.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings37.Add(item37);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgButton9, isVisibleProperty33, multiBinding32);
		context.PopParent();
		((ISupportInitialize)rootSvgButton8).EndInit();
		Controls children40 = wrapPanel4.Children;
		RootSvgButton rootSvgButton11;
		RootSvgButton rootSvgButton10 = (rootSvgButton11 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton10).BeginInit();
		children40.Add(rootSvgButton10);
		RootSvgButton rootSvgButton12 = (rootSvgButton4 = rootSvgButton11);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton13 = rootSvgButton4;
		rootSvgButton13.Name = "EditMessageActionButton";
		obj = rootSvgButton13;
		context.AvaloniaNameScope.Register("EditMessageActionButton", obj);
		rootSvgButton13.Classes.Add("Custom");
		rootSvgButton13.Height = 24.0;
		rootSvgButton13.Width = 24.0;
		rootSvgButton13.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<string> svgPathProperty5 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension31 = new DynamicResourceExtension("PencilSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding31 = dynamicResourceExtension31.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton13, svgPathProperty5, binding31);
		rootSvgButton13.SvgWidth = 20.0;
		rootSvgButton13.SvgHeight = 20.0;
		StyledProperty<ICommand?> commandProperty8 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension117 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EEnterEditModeCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension118 = compiledBindingExtension117.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton13, commandProperty8, compiledBindingExtension118);
		ToolTip.SetPlacement(rootSvgButton13, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgButton13, -1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton13, 0.0);
		ToolTip.SetShowDelay(rootSvgButton13, 0);
		RootToolTip rootToolTip23;
		RootToolTip rootToolTip22 = (rootToolTip23 = new RootToolTip());
		((ISupportInitialize)rootToolTip22).BeginInit();
		ToolTip.SetTip(rootSvgButton13, rootToolTip22);
		RootToolTip rootToolTip24 = (rootToolTip4 = rootToolTip23);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip25 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip25, PlacementMode.Top);
		TextBlock textBlock59;
		TextBlock textBlock58 = (textBlock59 = new TextBlock());
		((ISupportInitialize)textBlock58).BeginInit();
		rootToolTip25.Content = textBlock58;
		TextBlock textBlock60 = (textBlock4 = textBlock59);
		context.PushParent(textBlock4);
		TextBlock textBlock61 = textBlock4;
		textBlock61.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.EditMessage;
		StaticResourceExtension staticResourceExtension40 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj69 = staticResourceExtension40.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock61, obj69);
		textBlock61.FontWeight = (FontWeight)450;
		textBlock61.FontSize = 14.0;
		textBlock61.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock61.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock60).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip24).EndInit();
		StyledProperty<bool> isVisibleProperty34 = Visual.IsVisibleProperty;
		MultiBinding multiBinding34 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding35 = multiBinding2;
		StaticResourceExtension staticResourceExtension41 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj70 = staticResourceExtension41.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding35.Converter = (IMultiValueConverter)obj70;
		IList<IBinding> bindings38 = multiBinding35.Bindings;
		CompiledBindingExtension obj71 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EIsMyMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item38 = obj71.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings38.Add(item38);
		IList<IBinding> bindings39 = multiBinding35.Bindings;
		CompiledBindingExtension obj72 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EIsSystemMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item39 = obj72.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings39.Add(item39);
		IList<IBinding> bindings40 = multiBinding35.Bindings;
		CompiledBindingExtension obj73 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EIsPlaceholder_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item40 = obj73.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings40.Add(item40);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgButton13, isVisibleProperty34, multiBinding34);
		context.PopParent();
		((ISupportInitialize)rootSvgButton12).EndInit();
		Controls children41 = wrapPanel4.Children;
		RootSvgButton rootSvgButton15;
		RootSvgButton rootSvgButton14 = (rootSvgButton15 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton14).BeginInit();
		children41.Add(rootSvgButton14);
		RootSvgButton rootSvgButton16 = (rootSvgButton4 = rootSvgButton15);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton17 = rootSvgButton4;
		rootSvgButton17.Name = "PinMessageActionButton";
		obj = rootSvgButton17;
		context.AvaloniaNameScope.Register("PinMessageActionButton", obj);
		rootSvgButton17.Classes.Add("Custom");
		rootSvgButton17.Height = 24.0;
		rootSvgButton17.Width = 24.0;
		rootSvgButton17.VerticalAlignment = VerticalAlignment.Center;
		rootSvgButton17.SvgWidth = 16.0;
		rootSvgButton17.SvgHeight = 16.0;
		StyledProperty<ICommand?> commandProperty9 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension119 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EPinMessageCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension120 = compiledBindingExtension119.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton17, commandProperty9, compiledBindingExtension120);
		ToolTip.SetPlacement(rootSvgButton17, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgButton17, -1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton17, 0.0);
		ToolTip.SetShowDelay(rootSvgButton17, 0);
		RootToolTip rootToolTip27;
		RootToolTip rootToolTip26 = (rootToolTip27 = new RootToolTip());
		((ISupportInitialize)rootToolTip26).BeginInit();
		ToolTip.SetTip(rootSvgButton17, rootToolTip26);
		RootToolTip rootToolTip28 = (rootToolTip4 = rootToolTip27);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip29 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip29, PlacementMode.Top);
		TextBlock textBlock63;
		TextBlock textBlock62 = (textBlock63 = new TextBlock());
		((ISupportInitialize)textBlock62).BeginInit();
		rootToolTip29.Content = textBlock62;
		TextBlock textBlock64 = (textBlock4 = textBlock63);
		context.PushParent(textBlock4);
		TextBlock textBlock65 = textBlock4;
		textBlock65.Name = "PinMessageToolTip";
		obj = textBlock65;
		context.AvaloniaNameScope.Register("PinMessageToolTip", obj);
		StaticResourceExtension staticResourceExtension42 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj74 = staticResourceExtension42.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock65, obj74);
		textBlock65.FontWeight = (FontWeight)450;
		textBlock65.FontSize = 14.0;
		textBlock65.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock65.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock64).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip28).EndInit();
		StyledProperty<bool> isVisibleProperty35 = Visual.IsVisibleProperty;
		MultiBinding multiBinding36 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding37 = multiBinding2;
		StaticResourceExtension staticResourceExtension43 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj75 = staticResourceExtension43.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding37.Converter = (IMultiValueConverter)obj75;
		IList<IBinding> bindings41 = multiBinding37.Bindings;
		CompiledBindingExtension compiledBindingExtension121 = new CompiledBindingExtension();
		compiledBindingExtension121.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EMessageContainer_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainer_002CRootApp_002EClient_002ECoreDomain_002ELocalChannelPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalChannelPermission_002CRootApp_002EClient_002ECoreDomain_002EChannelManagePinnedMessages_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension121.FallbackValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item41 = compiledBindingExtension121.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings41.Add(item41);
		IList<IBinding> bindings42 = multiBinding37.Bindings;
		CompiledBindingExtension obj76 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EIsSystemMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item42 = obj76.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings42.Add(item42);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgButton17, isVisibleProperty35, multiBinding36);
		context.PopParent();
		((ISupportInitialize)rootSvgButton16).EndInit();
		Controls children42 = wrapPanel4.Children;
		RootSvgButton rootSvgButton19;
		RootSvgButton rootSvgButton18 = (rootSvgButton19 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton18).BeginInit();
		children42.Add(rootSvgButton18);
		RootSvgButton rootSvgButton20 = (rootSvgButton4 = rootSvgButton19);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton21 = rootSvgButton4;
		rootSvgButton21.Name = "MoreOptionsActionButton";
		obj = rootSvgButton21;
		context.AvaloniaNameScope.Register("MoreOptionsActionButton", obj);
		rootSvgButton21.Classes.Add("Custom");
		rootSvgButton21.Height = 24.0;
		rootSvgButton21.Width = 24.0;
		rootSvgButton21.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<string> svgPathProperty6 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension32 = new DynamicResourceExtension("EllipsisHorizontalSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding32 = dynamicResourceExtension32.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton21, svgPathProperty6, binding32);
		rootSvgButton21.SvgWidth = 16.0;
		rootSvgButton21.SvgHeight = 4.0;
		StaticResourceExtension staticResourceExtension44 = new StaticResourceExtension("MessageContextFlyout");
		context.ProvideTargetProperty = Control.ContextFlyoutProperty;
		object? obj77 = staticResourceExtension44.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_25(rootSvgButton21, obj77);
		rootSvgButton21.AddHandler((RoutedEvent)Button.ClickEvent, (Delegate)new EventHandler<RoutedEventArgs>(context.RootObject.onMoreOptionsActionButtonClick), RoutingStrategies.Direct | RoutingStrategies.Bubble, false);
		ToolTip.SetPlacement(rootSvgButton21, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgButton21, -1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton21, 0.0);
		ToolTip.SetShowDelay(rootSvgButton21, 0);
		RootToolTip rootToolTip31;
		RootToolTip rootToolTip30 = (rootToolTip31 = new RootToolTip());
		((ISupportInitialize)rootToolTip30).BeginInit();
		ToolTip.SetTip(rootSvgButton21, rootToolTip30);
		RootToolTip rootToolTip32 = (rootToolTip4 = rootToolTip31);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip33 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip33, PlacementMode.Top);
		TextBlock textBlock67;
		TextBlock textBlock66 = (textBlock67 = new TextBlock());
		((ISupportInitialize)textBlock66).BeginInit();
		rootToolTip33.Content = textBlock66;
		TextBlock textBlock68 = (textBlock4 = textBlock67);
		context.PushParent(textBlock4);
		TextBlock textBlock69 = textBlock4;
		textBlock69.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.MoreOptions;
		StaticResourceExtension staticResourceExtension45 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj78 = staticResourceExtension45.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock69, obj78);
		textBlock69.FontWeight = (FontWeight)450;
		textBlock69.FontSize = 14.0;
		textBlock69.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock69.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock68).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip32).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton20).EndInit();
		Controls children43 = wrapPanel4.Children;
		RootSvgButton rootSvgButton23;
		RootSvgButton rootSvgButton22 = (rootSvgButton23 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton22).BeginInit();
		children43.Add(rootSvgButton22);
		RootSvgButton rootSvgButton24 = (rootSvgButton4 = rootSvgButton23);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton25 = rootSvgButton4;
		rootSvgButton25.Name = "ShiftDeleteActionButton";
		obj = rootSvgButton25;
		context.AvaloniaNameScope.Register("ShiftDeleteActionButton", obj);
		rootSvgButton25.Classes.Add("Custom");
		rootSvgButton25.Height = 24.0;
		rootSvgButton25.Width = 24.0;
		rootSvgButton25.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<string> svgPathProperty7 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension33 = new DynamicResourceExtension("RemoveSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding33 = dynamicResourceExtension33.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton25, svgPathProperty7, binding33);
		rootSvgButton25.SvgWidth = 14.0;
		rootSvgButton25.SvgHeight = 14.0;
		StyledProperty<ICommand?> commandProperty10 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension122 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EDirectDeleteMessageCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension123 = compiledBindingExtension122.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton25, commandProperty10, compiledBindingExtension123);
		ToolTip.SetPlacement(rootSvgButton25, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgButton25, -1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton25, 0.0);
		ToolTip.SetShowDelay(rootSvgButton25, 0);
		StyledProperty<bool> isVisibleProperty36 = Visual.IsVisibleProperty;
		MultiBinding multiBinding38 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding39 = multiBinding2;
		StaticResourceExtension staticResourceExtension46 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj79 = staticResourceExtension46.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding39.Converter = (IMultiValueConverter)obj79;
		IList<IBinding> bindings43 = multiBinding39.Bindings;
		CompiledBindingExtension obj80 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EIsShiftKeyPressed_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item43 = obj80.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings43.Add(item43);
		IList<IBinding> bindings44 = multiBinding39.Bindings;
		MultiBinding item44 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding40 = multiBinding2;
		StaticResourceExtension staticResourceExtension47 = new StaticResourceExtension("OrConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj81 = staticResourceExtension47.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding40.Converter = (IMultiValueConverter)obj81;
		IList<IBinding> bindings45 = multiBinding40.Bindings;
		CompiledBindingExtension obj82 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EIsMyMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item45 = obj82.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings45.Add(item45);
		IList<IBinding> bindings46 = multiBinding40.Bindings;
		CompiledBindingExtension obj83 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMessages_002EMessageViewModel_002CRootApp_002EClient_002EAvalonia_002EMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EMessage_002CRootApp_002EClient_002ECoreDomain_002EMessageContainer_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EMessages_002EIMessageContainer_002CRootApp_002EClient_002ECoreDomain_002ELocalChannelPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalChannelPermission_002CRootApp_002EClient_002ECoreDomain_002EChannelDeleteMessageOther_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item46 = obj83.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings46.Add(item46);
		context.PopParent();
		bindings44.Add(item44);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgButton25, isVisibleProperty36, multiBinding38);
		RootToolTip rootToolTip35;
		RootToolTip rootToolTip34 = (rootToolTip35 = new RootToolTip());
		((ISupportInitialize)rootToolTip34).BeginInit();
		ToolTip.SetTip(rootSvgButton25, rootToolTip34);
		RootToolTip rootToolTip36 = (rootToolTip4 = rootToolTip35);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip37 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip37, PlacementMode.Top);
		TextBlock textBlock71;
		TextBlock textBlock70 = (textBlock71 = new TextBlock());
		((ISupportInitialize)textBlock70).BeginInit();
		rootToolTip37.Content = textBlock70;
		TextBlock textBlock72 = (textBlock4 = textBlock71);
		context.PushParent(textBlock4);
		TextBlock textBlock73 = textBlock4;
		textBlock73.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.DeleteMessage;
		StaticResourceExtension staticResourceExtension48 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj84 = staticResourceExtension48.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock73, obj84);
		textBlock73.FontWeight = (FontWeight)450;
		textBlock73.FontSize = 14.0;
		textBlock73.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock73.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock72).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip36).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton24).EndInit();
		context.PopParent();
		((ISupportInitialize)wrapPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder12).EndInit();
		context.PopParent();
		((ISupportInitialize)grid16).EndInit();
		context.PopParent();
		((ISupportInitialize)panel3).EndInit();
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
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
	private static void _0021XamlIlPopulateTrampoline(MessageView P_0)
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
