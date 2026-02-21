using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using FluentValidation;
using Grpc.Core;
using Microsoft.VisualStudio.Threading;
using ReactiveUI.Avalonia;
using RootApp.Browser;
using RootApp.Client.Avalonia.Controls.Messaging;
using RootApp.Client.Avalonia.Helpers.Calling;
using RootApp.Client.Avalonia.Helpers.Focus;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.UI.Community.Content;
using RootApp.Client.Avalonia.UI.Messages;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Media;
using RootApp.Client.CoreDomain.Models.Messages;
using RootApp.Client.CoreDomain.Models.User;
using RootApp.Client.CoreDomain.Services;
using RootApp.Core.Enums;
using RootApp.Core.Identifiers;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages;

public class DirectMessageContentViewModel : ViewModelBase<DirectMessageContentViewModel>, IMessageContainerViewModel
{
	private readonly IDisposable? _messageCacheCleanup;

	private readonly IDisposable? _virtualizedMessagesCleanup;

	private readonly IDisposable? _typingUsersCacheCleanup;

	private readonly Dictionary<Message, IViewModelBase> _messageViewModelCache = new Dictionary<Message, IViewModelBase>();

	private IDisposable? _hasOtherActivityDisposable;

	private bool _isTyping;

	private bool _isAutoScroll = false;

	private bool _isMessageInEditMode;

	private readonly Action _closeConversationCallBack;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly TextChannelFileUploadViewModelFactory _fileUploadViewModelFactory;

	private readonly DirectMessageDetailsViewModelFactory _directMessageDetailsViewModelFactory;

	private readonly AddDirectMessageMemberViewModelFactory _addDirectMessageMemberViewModelFactory;

	private readonly FocusService _focusService;

	private readonly DirectMessageCallContentViewModelFactory _directMessageCallContentViewModelFactory;

	private readonly CallPopoutService _callPopoutService;

	private readonly CallingService _callingService;

	private readonly PrivacyBlockedActionViewModelFactory _privacyBlockedActionViewModelFactory;

	private readonly MessageViewModelFactory _messageViewModelFactory;

	private readonly DirectMessageStartMessageViewModelFactory _directMessageStartMessageViewModelFactory;

	private readonly ReadOnlyObservableCollection<IViewModelBase> _messages;

	private readonly ReadOnlyObservableCollection<TypingIndicatorViewModel> _typingUsers;

	[CompilerGenerated]
	private bool <IsFileUploaderOpen>k__BackingField;

	[CompilerGenerated]
	private MessageGuid? <StartupMessageId>k__BackingField;

	[CompilerGenerated]
	private Action<MessageGuid>? m_FocusMessageRequested;

	[CompilerGenerated]
	private Action? m_ExpandMessageAreaRequested;

	[CompilerGenerated]
	private bool <ShowNewMessagesBanner>k__BackingField;

	[CompilerGenerated]
	private bool <ShowJumpToPresent>k__BackingField;

	[CompilerGenerated]
	private bool <InitialMessagesBeganRendering>k__BackingField;

	[CompilerGenerated]
	private IViewModelBase? <SecondaryContentViewModel>k__BackingField;

	[CompilerGenerated]
	private IViewModelBase? <CallContentViewModel>k__BackingField;

	[CompilerGenerated]
	private bool <FocusedOnCall>k__BackingField;

	[CompilerGenerated]
	private int <OtherDmActivityCount>k__BackingField;

	[CompilerGenerated]
	private bool <ShowCloseButton>k__BackingField;

	[CompilerGenerated]
	private bool <ShowPinToTab>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? viewLoadedCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? viewUnloadedCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showDetailsCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showAddMembersCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? pinToTabCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeConversationCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? markAsReadCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? downloadNewerMessagesCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? downloadOlderMessagesCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<bool>? setNewMessagesBannerStatusCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<bool>? setAutoScrollStatusCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<bool>? setShowJumpToPresentCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? messagesBeganRenderingCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand<MessagePayload>? sendMessageCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? startCallCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? popoutCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? focusMessagesCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? focusCallCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ShowNewMessagesBanner
	{
		get
		{
			return <ShowNewMessagesBanner>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<ShowNewMessagesBanner>k__BackingField, flag))
			{
				<ShowNewMessagesBanner>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShowNewMessagesBanner);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ShowJumpToPresent
	{
		get
		{
			return <ShowJumpToPresent>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<ShowJumpToPresent>k__BackingField, flag))
			{
				<ShowJumpToPresent>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShowJumpToPresent);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool InitialMessagesBeganRendering
	{
		get
		{
			return <InitialMessagesBeganRendering>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<InitialMessagesBeganRendering>k__BackingField, flag))
			{
				<InitialMessagesBeganRendering>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.InitialMessagesBeganRendering);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IViewModelBase? SecondaryContentViewModel
	{
		get
		{
			return <SecondaryContentViewModel>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<IViewModelBase>.Default.Equals(<SecondaryContentViewModel>k__BackingField, viewModelBase))
			{
				<SecondaryContentViewModel>k__BackingField = viewModelBase;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SecondaryContentViewModel);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IViewModelBase? CallContentViewModel
	{
		get
		{
			return <CallContentViewModel>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<IViewModelBase>.Default.Equals(<CallContentViewModel>k__BackingField, viewModelBase))
			{
				<CallContentViewModel>k__BackingField = viewModelBase;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.CallContentViewModel);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool FocusedOnCall
	{
		get
		{
			return <FocusedOnCall>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<FocusedOnCall>k__BackingField, flag))
			{
				<FocusedOnCall>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.FocusedOnCall);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public int OtherDmActivityCount
	{
		get
		{
			return <OtherDmActivityCount>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<int>.Default.Equals(<OtherDmActivityCount>k__BackingField, num))
			{
				<OtherDmActivityCount>k__BackingField = num;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.OtherDmActivityCount);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ShowCloseButton
	{
		get
		{
			return <ShowCloseButton>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<ShowCloseButton>k__BackingField, flag))
			{
				<ShowCloseButton>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShowCloseButton);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ShowPinToTab
	{
		get
		{
			return <ShowPinToTab>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<ShowPinToTab>k__BackingField, flag))
			{
				<ShowPinToTab>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShowPinToTab);
			}
		}
	}

	public ReadOnlyObservableCollection<IViewModelBase> Messages => _messages;

	public ReadOnlyObservableCollection<TypingIndicatorViewModel> TypingUsers => _typingUsers;

	public DirectMessageViewModel DirectMessageViewModel { get; }

	public bool IsFileUploaderOpen
	{
		[CompilerGenerated]
		get
		{
			return <IsFileUploaderOpen>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<IsFileUploaderOpen>k__BackingField = flag;
		}
	}

	public RootMessageTextboxViewModel RootMessageTextboxViewModel { get; }

	public MessageGuid? StartupMessageId
	{
		[CompilerGenerated]
		get
		{
			return <StartupMessageId>k__BackingField;
		}
		[CompilerGenerated]
		set
		{
			<StartupMessageId>k__BackingField = messageGuid;
		}
	}

	public IMessageContainer MessageContainer => DirectMessageViewModel.DirectMessage;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand ViewLoadedCommand => viewLoadedCommand ?? (viewLoadedCommand = new AsyncRelayCommand(ViewLoadedAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ViewUnloadedCommand => viewUnloadedCommand ?? (viewUnloadedCommand = new RelayCommand(ViewUnloaded));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowDetailsCommand => showDetailsCommand ?? (showDetailsCommand = new RelayCommand(ShowDetails));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowAddMembersCommand => showAddMembersCommand ?? (showAddMembersCommand = new RelayCommand(ShowAddMembers));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand PinToTabCommand => pinToTabCommand ?? (pinToTabCommand = new RelayCommand(PinToTab));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseConversationCommand => closeConversationCommand ?? (closeConversationCommand = new RelayCommand(CloseConversation));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand MarkAsReadCommand => markAsReadCommand ?? (markAsReadCommand = new AsyncRelayCommand(MarkAsReadAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand DownloadNewerMessagesCommand => downloadNewerMessagesCommand ?? (downloadNewerMessagesCommand = new AsyncRelayCommand(DownloadNewerMessagesAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand DownloadOlderMessagesCommand => downloadOlderMessagesCommand ?? (downloadOlderMessagesCommand = new AsyncRelayCommand(DownloadOlderMessagesAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<bool> SetNewMessagesBannerStatusCommand => setNewMessagesBannerStatusCommand ?? (setNewMessagesBannerStatusCommand = new RelayCommand<bool>(SetNewMessagesBannerStatus));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<bool> SetAutoScrollStatusCommand => setAutoScrollStatusCommand ?? (setAutoScrollStatusCommand = new RelayCommand<bool>(SetAutoScrollStatus));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<bool> SetShowJumpToPresentCommand => setShowJumpToPresentCommand ?? (setShowJumpToPresentCommand = new RelayCommand<bool>(SetShowJumpToPresent));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand MessagesBeganRenderingCommand => messagesBeganRenderingCommand ?? (messagesBeganRenderingCommand = new RelayCommand(MessagesBeganRendering));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand<MessagePayload> SendMessageCommand => sendMessageCommand ?? (sendMessageCommand = new AsyncRelayCommand<MessagePayload>(SendMessageAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand StartCallCommand => startCallCommand ?? (startCallCommand = new AsyncRelayCommand(StartCallAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand PopoutCommand => popoutCommand ?? (popoutCommand = new RelayCommand(Popout));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand FocusMessagesCommand => focusMessagesCommand ?? (focusMessagesCommand = new RelayCommand(FocusMessages));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand FocusCallCommand => focusCallCommand ?? (focusCallCommand = new RelayCommand(FocusCall));

	public event Action<MessageGuid>? FocusMessageRequested
	{
		[CompilerGenerated]
		add
		{
			Action<MessageGuid> action = this.m_FocusMessageRequested;
			Action<MessageGuid> action2;
			do
			{
				action2 = action;
				Action<MessageGuid> action3 = (Action<MessageGuid>)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_FocusMessageRequested, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action<MessageGuid> action = this.m_FocusMessageRequested;
			Action<MessageGuid> action2;
			do
			{
				action2 = action;
				Action<MessageGuid> action3 = (Action<MessageGuid>)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_FocusMessageRequested, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public event Action? ExpandMessageAreaRequested
	{
		[CompilerGenerated]
		add
		{
			Action action = this.m_ExpandMessageAreaRequested;
			Action action2;
			do
			{
				action2 = action;
				Action action3 = (Action)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_ExpandMessageAreaRequested, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action action = this.m_ExpandMessageAreaRequested;
			Action action2;
			do
			{
				action2 = action;
				Action action3 = (Action)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_ExpandMessageAreaRequested, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public DirectMessageContentViewModel(DirectMessageViewModel P_0, MessageGuid? P_1, Action P_2, bool P_3, bool P_4, bool P_5, MessageViewModelFactory P_6, DirectMessageStartMessageViewModelFactory P_7, IRootSessionAccessor P_8, RootMessageTextboxViewModelFactory P_9, TypingIndicatorViewModelFactory P_10, IGlobalUserCacheService P_11, TextChannelFileUploadViewModelFactory P_12, DirectMessageDetailsViewModelFactory P_13, AddDirectMessageMemberViewModelFactory P_14, FocusService P_15, BrowserService P_16, CallingServiceFactory P_17, DirectMessageCallContentViewModelFactory P_18, CallPopoutService P_19, PrivacyBlockedActionViewModelFactory P_20)
		: base((IValidator<DirectMessageContentViewModel>?)null)
	{
		DirectMessageContentViewModel directMessageContentViewModel = this;
		DirectMessageViewModel = P_0;
		StartupMessageId = P_1;
		_closeConversationCallBack = P_2;
		ShowCloseButton = P_4;
		ShowPinToTab = P_5;
		_rootSessionAccessor = P_8;
		_fileUploadViewModelFactory = P_12;
		_directMessageDetailsViewModelFactory = P_13;
		_addDirectMessageMemberViewModelFactory = P_14;
		_focusService = P_15;
		_callingService = P_17.Create(P_16);
		_directMessageCallContentViewModelFactory = P_18;
		_callPopoutService = P_19;
		_privacyBlockedActionViewModelFactory = P_20;
		_messageViewModelFactory = P_6;
		_directMessageStartMessageViewModelFactory = P_7;
		_rootSessionAccessor.Session.ActiveMediaRoomService.MediaRoomClosed += onActiveMediaRoomServiceMediaRoomClosed;
		MediaRoom mediaRoom = DirectMessageViewModel.DirectMessage.MediaRoom;
		if ((mediaRoom != null && mediaRoom.HasActiveCall && mediaRoom.SelfMediaMember != null) || P_3)
		{
			CallContentViewModel = _directMessageCallContentViewModelFactory.Create(DirectMessageViewModel.DirectMessage);
		}
		if (P_3)
		{
			FocusedOnCall = true;
		}
		RootMessageTextboxViewModel = P_9.Create(DirectMessageViewModel.DirectMessage, SendMessageCommand, null, string.Empty);
		RootMessageTextboxViewModel.PropertyChanged += onRootMessageTextboxViewModelPropertyChanged;
		RootMessageTextboxViewModel.EditLastMessageRequested += onRootMessageTextboxViewModelEditLastMessageRequested;
		updatePlaceholderText();
		_messageCacheCleanup = DirectMessageViewModel.DirectMessage.Messages.ConnectMessages().Transform((Message message) => directMessageContentViewModel.getOrCreateViewModelForMessage(message)).DisposeMany()
			.Subscribe();
		_virtualizedMessagesCleanup = DirectMessageViewModel.DirectMessage.Messages.ConnectVirtualizedMessages().Transform((Message message) => directMessageContentViewModel.getOrCreateViewModelForMessage(message)).ObserveOn(AvaloniaScheduler.Instance)
			.Do(delegate(IChangeSet<IViewModelBase> changeSet)
			{
				if (directMessageContentViewModel.DirectMessageViewModel.DirectMessage.Messages.IsVisible)
				{
					foreach (Change<IViewModelBase> item in changeSet)
					{
						if (item.Reason == ListChangeReason.Add)
						{
							if (item.Item.Current is MessageViewModel messageViewModel)
							{
								Message message = messageViewModel.Message;
								if (message != null && message.HasSelfMention)
								{
									directMessageContentViewModel.setNotificationsAsViewed();
								}
							}
							if (directMessageContentViewModel._isAutoScroll)
							{
								directMessageContentViewModel.MessageContainer.Messages.SetViewTimeAsync().Forget();
							}
						}
					}
				}
			})
			.Bind(out _messages)
			.Subscribe();
		_typingUsersCacheCleanup = DirectMessageViewModel.DirectMessage.Messages.ConnectTypingUsers().TransformAsync(async delegate(UserGuid userId)
		{
			IMessageContainerMember member = await directMessageContentViewModel.DirectMessageViewModel.DirectMessage.GetMemberAsync(userId);
			if (member != null)
			{
				return P_10.Create(member);
			}
			GlobalUser globalUser = await P_11.GetUserByIdAsync(userId);
			return P_10.Create(globalUser);
		}).ObserveOn(AvaloniaScheduler.Instance)
			.Bind(out _typingUsers)
			.DisposeMany()
			.Subscribe();
		DirectMessageViewModel.PropertyChanged += onDirectMessageViewModelPropertyChanged;
		WeakReferenceMessenger.Default.Register<ReplyToMessageMessage>(this, onReplyToMessageMessageReceived);
		WeakReferenceMessenger.Default.Register<MessageEditModeChangedMessage>(this, onMessageEditModeChanged);
		WeakReferenceMessenger.Default.Register<FocusDirectMessageCallMessage>(this, onFocusDirectMessageCallMessageReceived);
		WeakReferenceMessenger.Default.Register<FocusDirectMessageMessage>(this, onFocusDirectMessageMessageReceived);
	}

	private void onFocusDirectMessageCallMessageReceived(object recipient, FocusDirectMessageCallMessage message)
	{
		if (message.DirectMessageId == DirectMessageViewModel.DirectMessage.Id)
		{
			if (CallContentViewModel == null)
			{
				CallContentViewModel = _directMessageCallContentViewModelFactory.Create(DirectMessageViewModel.DirectMessage);
			}
			FocusedOnCall = true;
		}
	}

	private void onFocusDirectMessageMessageReceived(object recipient, FocusDirectMessageMessage message)
	{
		if (message.DirectMessageId == DirectMessageViewModel.DirectMessage.Id)
		{
			FocusMessageAsync(message.MessageId).Forget();
		}
	}

	private async Task FocusMessageAsync(MessageGuid P_0)
	{
		StartupMessageId = P_0;
		await DirectMessageViewModel.DirectMessage.Messages.FocusMessageAsync(P_0);
		FocusMessage(P_0);
	}

	public void FocusMessage(MessageGuid P_0)
	{
		this.FocusMessageRequested?.Invoke(P_0);
	}

	private void onRootMessageTextboxViewModelEditLastMessageRequested()
	{
		Messages.OfType<MessageViewModel>().TakeLast(10).LastOrDefault((MessageViewModel x) => x.Message.SenderUserId == _rootSessionAccessor.Session.UserInfoService.SessionUser.Id)?.EnterEditMode();
	}

	private void onRootMessageTextboxViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "Text")
			{
				processMessageTextboxChange();
			}
		});
	}

	private void onDirectMessageViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "DirectMessageName")
			{
				updatePlaceholderText();
			}
		});
	}

	private void updatePlaceholderText()
	{
		RootMessageTextboxViewModel.PlaceholderText = "Message @" + DirectMessageViewModel.DirectMessageName;
	}

	private void processMessageTextboxChange()
	{
		if (DirectMessageViewModel.DirectMessage.Id != DirectMessageGuid.Empty)
		{
			bool flag = RootMessageTextboxViewModel.Text.Length > 0;
			if (_isTyping != flag)
			{
				DirectMessageViewModel.DirectMessage.Messages.SetTypingIndicatorAsync(flag);
			}
			_isTyping = flag;
		}
	}

	[RelayCommand]
	public async Task ViewLoadedAsync()
	{
		SetFocusTrackingState(true);
		if (DirectMessageViewModel.DirectMessage.Id != DirectMessageGuid.Empty)
		{
			_hasOtherActivityDisposable = _rootSessionAccessor.Session.DirectMessageService.ConnectHasAnyActivityBesides(DirectMessageViewModel.DirectMessage.Id).Subscribe(delegate(int count)
			{
				OtherDmActivityCount = count;
			});
			if (!DirectMessageViewModel.DirectMessage.Messages.HasFetchedOnce && StartupMessageId == null)
			{
				await DirectMessageViewModel.DirectMessage.Messages.FetchMessagesAsync(MessageDirectionTake.Both);
			}
			if (StartupMessageId == null)
			{
				DirectMessageViewModel.DirectMessage.Messages.InitializeVirtualizationWindow();
			}
			setNotificationsAsViewed();
		}
	}

	[RelayCommand]
	public void ViewUnloaded()
	{
		_hasOtherActivityDisposable?.Dispose();
		SetFocusTrackingState(false);
		InitialMessagesBeganRendering = false;
		SetNewMessagesBannerStatus(isVisible: false);
		ShowJumpToPresent = false;
		if (DirectMessageViewModel.DirectMessage.Messages.HasMoreNewerMessages)
		{
			DirectMessageViewModel.DirectMessage.Messages.ReinitializeAsync().Forget();
			StartupMessageId = null;
		}
		else
		{
			DirectMessageViewModel.DirectMessage.Messages.ResetVirtualizationWindow();
		}
	}

	public void SetFocusTrackingState(bool P_0)
	{
		DirectMessageViewModel.DirectMessage.Messages.IsVisible = P_0;
		if (P_0)
		{
			_focusService.TrackView(DirectMessageViewModel.DirectMessage.Id);
		}
		else
		{
			_focusService.UntrackView(DirectMessageViewModel.DirectMessage.Id);
		}
	}

	[RelayCommand]
	public void ShowDetails()
	{
		SecondaryContentViewModel = _directMessageDetailsViewModelFactory.Create(DirectMessageViewModel, closeSecondaryContentViewModel, _closeConversationCallBack);
	}

	[RelayCommand]
	public void ShowAddMembers()
	{
		SecondaryContentViewModel = _addDirectMessageMemberViewModelFactory.Create(DirectMessageViewModel, closeSecondaryContentViewModel);
	}

	[RelayCommand]
	public void PinToTab()
	{
		CheckPopoutFocusMessage checkPopoutFocusMessage = new CheckPopoutFocusMessage(DirectMessageViewModel.DirectMessage.Id, null, null);
		WeakReferenceMessenger.Default.Send(checkPopoutFocusMessage);
		if (checkPopoutFocusMessage.WasHandled)
		{
			_closeConversationCallBack();
			return;
		}
		if (DirectMessageViewModel.IsPinned)
		{
			_rootSessionAccessor.Session.TabService.RemoveTab(DirectMessageViewModel.DirectMessage.Id);
			return;
		}
		_rootSessionAccessor.Session.TabService.AddDirectMessageTab(DirectMessageViewModel.DirectMessage.Id);
		_closeConversationCallBack();
		WeakReferenceMessenger.Default.Send(new SelectTabMessage(DirectMessageViewModel.DirectMessage.Id));
	}

	private void closeSecondaryContentViewModel()
	{
		SecondaryContentViewModel?.Dispose();
		SecondaryContentViewModel = null;
	}

	[RelayCommand]
	public void CloseConversation()
	{
		_closeConversationCallBack();
	}

	[RelayCommand]
	public async Task MarkAsReadAsync()
	{
		try
		{
			ShowNewMessagesBanner = false;
			await DirectMessageViewModel.DirectMessage.Messages.MarkContainerAsReadAsync();
			this.ExpandMessageAreaRequested?.Invoke();
		}
		catch (Exception)
		{
			ShowNewMessagesBanner = true;
		}
	}

	[RelayCommand]
	public Task DownloadNewerMessagesAsync()
	{
		return DirectMessageViewModel.DirectMessage.Messages.FetchMessagesAsync(MessageDirectionTake.Newer);
	}

	[RelayCommand]
	public Task DownloadOlderMessagesAsync()
	{
		return DirectMessageViewModel.DirectMessage.Messages.FetchMessagesAsync(MessageDirectionTake.Older);
	}

	[RelayCommand]
	public void SetNewMessagesBannerStatus(bool isVisible)
	{
		ShowNewMessagesBanner = isVisible;
	}

	[RelayCommand]
	public void SetAutoScrollStatus(bool isAutoScroll)
	{
		_isAutoScroll = isAutoScroll;
	}

	[RelayCommand]
	public void SetShowJumpToPresent(bool isVisible)
	{
		ShowJumpToPresent = isVisible;
	}

	[RelayCommand]
	public void MessagesBeganRendering()
	{
		InitialMessagesBeganRendering = true;
	}

	[RelayCommand]
	public async Task SendMessageAsync(MessagePayload messagePayload)
	{
		if (DirectMessageViewModel.DirectMessage.IsDraft)
		{
			try
			{
				await _rootSessionAccessor.Session.DirectMessageService.CreateDirectMessageAsync(DirectMessageViewModel.DirectMessage);
				await DirectMessageViewModel.DirectMessage.Messages.FetchMessagesAsync(MessageDirectionTake.Both);
				DirectMessageViewModel.DirectMessage.Messages.InitializeVirtualizationWindow();
				setNotificationsAsViewed();
			}
			catch (RpcException ex) when (ex.StatusCode == StatusCode.Unauthenticated)
			{
				_privacyBlockedActionViewModelFactory.Show(PrivacyBlockedActionType.DirectMessage);
				_rootSessionAccessor.Session.DirectMessageService.RemoveDraftDirectMessage(DirectMessageViewModel.DirectMessage);
				_closeConversationCallBack();
				return;
			}
			catch
			{
			}
		}
		await DirectMessageViewModel.DirectMessage.Messages.CreateMessageAsync(messagePayload.Message, messagePayload.AttachmentTokenUris, messagePayload.MessageIds, messagePayload.ShouldReplySendNotification, messagePayload.PendingAttachments);
	}

	private void onReplyToMessageMessageReceived(object recipient, ReplyToMessageMessage message)
	{
		if (!_isMessageInEditMode && message.Message.MessageContainer.CommunityId == null && (MessageContainerGuid?)message.Message.MessageContainer.ContainerId == (MessageContainerGuid?)(MessageContainerGuid)DirectMessageViewModel.DirectMessage.Id)
		{
			RootMessageTextboxViewModel.SetReplyingToMessage(message.Message);
		}
	}

	private void onMessageEditModeChanged(object recipient, MessageEditModeChangedMessage message)
	{
		if ((MessageContainerGuid?)message.ContainerId == (MessageContainerGuid?)(MessageContainerGuid)DirectMessageViewModel.DirectMessage.Id)
		{
			_isMessageInEditMode = message.IsInEditMode;
		}
	}

	[RelayCommand]
	public async Task StartCallAsync()
	{
		if (DirectMessageViewModel.DirectMessage.IsDraft)
		{
			try
			{
				await _rootSessionAccessor.Session.DirectMessageService.CreateDirectMessageAsync(DirectMessageViewModel.DirectMessage);
			}
			catch (RpcException ex) when (ex.StatusCode == StatusCode.Unauthenticated)
			{
				_privacyBlockedActionViewModelFactory.Show(PrivacyBlockedActionType.DirectMessage);
				_rootSessionAccessor.Session.DirectMessageService.RemoveDraftDirectMessage(DirectMessageViewModel.DirectMessage);
				_closeConversationCallBack();
				return;
			}
			catch
			{
				return;
			}
		}
		await _callingService.JoinVoiceCallAsync(DirectMessageViewModel.DirectMessage.Id, DirectMessageViewModel.DirectMessage.MediaRoom);
		CallContentViewModel = _directMessageCallContentViewModelFactory.Create(DirectMessageViewModel.DirectMessage);
		FocusedOnCall = true;
	}

	[RelayCommand]
	public void Popout()
	{
		_callPopoutService.PopoutCall(DirectMessageViewModel.DirectMessage.MediaRoom);
	}

	[RelayCommand]
	public void FocusMessages()
	{
		FocusedOnCall = false;
	}

	[RelayCommand]
	public void FocusCall()
	{
		MediaRoom mediaRoom = DirectMessageViewModel.DirectMessage.MediaRoom;
		if (mediaRoom != null && mediaRoom.HasActiveCall && mediaRoom.SelfMediaMember != null && CallContentViewModel == null)
		{
			CallContentViewModel = _directMessageCallContentViewModelFactory.Create(DirectMessageViewModel.DirectMessage);
		}
		FocusedOnCall = true;
	}

	private void onActiveMediaRoomServiceMediaRoomClosed(MessageContainerGuid containerId)
	{
		if ((RootGuidType)containerId == RootGuidType.DirectMessage && RootGuid.TryParse<DirectMessageGuid>(containerId, out var value) && DirectMessageViewModel.DirectMessage.Id == value)
		{
			CallContentViewModel?.Dispose();
			CallContentViewModel = null;
			FocusedOnCall = false;
		}
	}

	public void ShowFileUploadView()
	{
		IsFileUploaderOpen = true;
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(_fileUploadViewModelFactory.Create(DirectMessageViewModel.DirectMessage, delegate
		{
			IsFileUploaderOpen = false;
		}, RootMessageTextboxViewModel.AddAttachments)));
	}

	private void setNotificationsAsViewed()
	{
		_rootSessionAccessor.Session.NotificationService.SetContainerAsViewedAsync(DirectMessageViewModel.DirectMessage.Id, null).Forget();
	}

	private IViewModelBase getOrCreateViewModelForMessage(Message P_0)
	{
		if (_messageViewModelCache.TryGetValue(P_0, out IViewModelBase value))
		{
			return value;
		}
		IViewModelBase viewModelBase = ((!P_0.IsStartOfContainer) ? ((IViewModelBase)_messageViewModelFactory.Create(P_0)) : ((IViewModelBase)_directMessageStartMessageViewModelFactory.Create(P_0)));
		_messageViewModelCache[P_0] = viewModelBase;
		return viewModelBase;
	}

	public override void Dispose()
	{
		base.Dispose();
		_messageCacheCleanup?.Dispose();
		_virtualizedMessagesCleanup?.Dispose();
		_typingUsersCacheCleanup?.Dispose();
		_messageViewModelCache.Clear();
		SecondaryContentViewModel?.Dispose();
		DirectMessageViewModel.PropertyChanged -= onDirectMessageViewModelPropertyChanged;
		RootMessageTextboxViewModel.PropertyChanged -= onRootMessageTextboxViewModelPropertyChanged;
		RootMessageTextboxViewModel.EditLastMessageRequested -= onRootMessageTextboxViewModelEditLastMessageRequested;
		_rootSessionAccessor.Session.ActiveMediaRoomService.MediaRoomClosed -= onActiveMediaRoomServiceMediaRoomClosed;
		RootMessageTextboxViewModel.Dispose();
		CallContentViewModel?.Dispose();
	}
}
