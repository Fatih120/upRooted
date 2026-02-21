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
using Microsoft.VisualStudio.Threading;
using ReactiveUI.Avalonia;
using RootApp.Client.Avalonia.Controls.Messaging;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Focus;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.UI.Community.Channels;
using RootApp.Client.Avalonia.UI.Messages;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Client.CoreDomain.Models.Messages;
using RootApp.Client.CoreDomain.Models.User;
using RootApp.Client.CoreDomain.Services;
using RootApp.Core.Identifiers;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.Client.Avalonia.UI.Community.Content;

public class TextChannelContentViewModel : ViewModelBase<TextChannelContentViewModel>, IMessageContainerViewModel
{
	private readonly IDisposable? _messageCacheCleanup;

	private readonly IDisposable? _virtualizedMessagesCleanup;

	private readonly IDisposable? _typingUsersCacheCleanup;

	private readonly IDisposable? _pinnedMessagesCountCleanup;

	private readonly Dictionary<Message, IViewModelBase> _messageViewModelCache = new Dictionary<Message, IViewModelBase>();

	private bool _isTyping;

	private bool _isAutoScroll = false;

	private bool _isMessageInEditMode;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly FocusService _focusService;

	private readonly TextChannelFileUploadViewModelFactory _fileUploadViewModelFactory;

	private readonly EditChannelViewModelFactory _editChannelViewModelFactory;

	private readonly DeleteChannelViewModelFactory _deleteChannelViewModelFactory;

	private readonly MessageViewModelFactory _messageViewModelFactory;

	private readonly ChannelStartMessageViewModelFactory _channelStartMessageViewModelFactory;

	private readonly ReadOnlyObservableCollection<TypingIndicatorViewModel> _typingUsers;

	private readonly ReadOnlyObservableCollection<IViewModelBase> _messages;

	[CompilerGenerated]
	private CommunityViewModel <CommunityViewModel>k__BackingField;

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
	private int <PinnedMessagesCount>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? viewLoadedCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? viewUnloadedCommand;

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
	private RelayCommand? showEditChannelViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showDeleteChannelViewModelCommand;

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
	[NotifyPropertyChangedFor("HasPinnedMessages")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public int PinnedMessagesCount
	{
		get
		{
			return <PinnedMessagesCount>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<int>.Default.Equals(<PinnedMessagesCount>k__BackingField, num))
			{
				<PinnedMessagesCount>k__BackingField = num;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.PinnedMessagesCount);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.HasPinnedMessages);
			}
		}
	}

	public bool HasPinnedMessages => PinnedMessagesCount > 0;

	public ReadOnlyObservableCollection<TypingIndicatorViewModel> TypingUsers => _typingUsers;

	public ReadOnlyObservableCollection<IViewModelBase> Messages => _messages;

	public BitmapCache BitmapCache { get; }

	public Channel Channel { get; }

	public CommunityViewModel CommunityViewModel
	{
		[CompilerGenerated]
		get
		{
			return <CommunityViewModel>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<CommunityViewModel>k__BackingField = communityViewModel;
		}
	}

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

	public Task<BitmapWrapper?> ChannelIconAsyncBitmapWrapper => BitmapCache.GetBitmapAsync(Channel.IconAssetUri, null, 120);

	public IMessageContainer MessageContainer => Channel;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand ViewLoadedCommand => viewLoadedCommand ?? (viewLoadedCommand = new AsyncRelayCommand(ViewLoadedAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ViewUnloadedCommand => viewUnloadedCommand ?? (viewUnloadedCommand = new RelayCommand(ViewUnloaded));

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
	public IRelayCommand ShowEditChannelViewModelCommand => showEditChannelViewModelCommand ?? (showEditChannelViewModelCommand = new RelayCommand(ShowEditChannelViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowDeleteChannelViewModelCommand => showDeleteChannelViewModelCommand ?? (showDeleteChannelViewModelCommand = new RelayCommand(ShowDeleteChannelViewModel));

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

	public TextChannelContentViewModel(Channel P_0, CommunityViewModel P_1, MessageGuid? P_2, MessageViewModelFactory P_3, ChannelStartMessageViewModelFactory P_4, BitmapCache P_5, TextChannelFileUploadViewModelFactory P_6, RootMessageTextboxViewModelFactory P_7, TypingIndicatorViewModelFactory P_8, EditChannelViewModelFactory P_9, DeleteChannelViewModelFactory P_10, IGlobalUserCacheService P_11, IRootSessionAccessor P_12, FocusService P_13)
		: base((IValidator<TextChannelContentViewModel>?)null)
	{
		TextChannelContentViewModel textChannelContentViewModel = this;
		Channel = P_0;
		CommunityViewModel = P_1;
		BitmapCache = P_5;
		StartupMessageId = P_2;
		_fileUploadViewModelFactory = P_6;
		_editChannelViewModelFactory = P_9;
		_deleteChannelViewModelFactory = P_10;
		_rootSessionAccessor = P_12;
		_focusService = P_13;
		_messageViewModelFactory = P_3;
		_channelStartMessageViewModelFactory = P_4;
		RootMessageTextboxViewModel = P_7.Create(Channel, SendMessageCommand, null, string.Empty);
		RootMessageTextboxViewModel.PropertyChanged += onRootMessageTextboxViewModelPropertyChanged;
		RootMessageTextboxViewModel.EditLastMessageRequested += onRootMessageTextboxViewModelEditLastMessageRequested;
		updatePlaceholderText();
		_messageCacheCleanup = Channel.Messages.ConnectMessages().Transform((Message message) => textChannelContentViewModel.getOrCreateViewModelForMessage(message)).DisposeMany()
			.Subscribe();
		_virtualizedMessagesCleanup = Channel.Messages.ConnectVirtualizedMessages().Transform((Message message) => textChannelContentViewModel.getOrCreateViewModelForMessage(message)).ObserveOn(AvaloniaScheduler.Instance)
			.Do(delegate(IChangeSet<IViewModelBase> changeSet)
			{
				if (textChannelContentViewModel.Channel.Messages.IsVisible)
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
									textChannelContentViewModel.setNotificationsAsViewed();
								}
							}
							if (textChannelContentViewModel._isAutoScroll)
							{
								textChannelContentViewModel.MessageContainer.Messages.SetViewTimeAsync().Forget();
							}
						}
					}
				}
			})
			.Bind(out _messages)
			.Subscribe();
		_typingUsersCacheCleanup = Channel.Messages.ConnectTypingUsers().TransformAsync(async delegate(UserGuid userId)
		{
			Member communityMember = await textChannelContentViewModel.Channel.Community.Members.GetMemberAsync(userId);
			if (communityMember != null)
			{
				return P_8.Create(communityMember);
			}
			GlobalUser globalUser = await P_11.GetUserByIdAsync(userId);
			return P_8.Create(globalUser);
		}).ObserveOn(AvaloniaScheduler.Instance)
			.Bind(out _typingUsers)
			.DisposeMany()
			.Subscribe();
		_pinnedMessagesCountCleanup = Channel.Messages.ConnectPinnedMessages().QueryWhenChanged((IReadOnlyCollection<Message> items) => items.Count).ObserveOn(AvaloniaScheduler.Instance)
			.Subscribe(delegate(int count)
			{
				textChannelContentViewModel.PinnedMessagesCount = count;
			});
		Channel.PropertyChanged += onChannelPropertyChanged;
		Channel.LocalChannelPermission.PropertyChanged += onLocalChannelPermissionPropertyChanged;
		WeakReferenceMessenger.Default.Register<ReplyToMessageMessage>(this, onReplyToMessageMessageReceived);
		WeakReferenceMessenger.Default.Register<MessageEditModeChangedMessage>(this, onMessageEditModeChanged);
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

	private void onChannelPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "IconAssetUri")
			{
				OnPropertyChanged("ChannelIconAsyncBitmapWrapper");
			}
			else if (e.PropertyName == "Name")
			{
				updatePlaceholderText();
			}
		});
	}

	private void onLocalChannelPermissionPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "ChannelViewFile" && CommunityViewModel.IsDirectoriesChecked && !Channel.LocalChannelPermission.ChannelViewFile)
			{
				CommunityViewModel.CloseAllPanes();
			}
		});
	}

	private void updatePlaceholderText()
	{
		RootMessageTextboxViewModel.PlaceholderText = "Message #" + Channel.Name;
	}

	private void processMessageTextboxChange()
	{
		bool flag = RootMessageTextboxViewModel.Text.Length > 0;
		if (_isTyping != flag)
		{
			Channel.Messages.SetTypingIndicatorAsync(flag).Forget();
		}
		_isTyping = flag;
	}

	[RelayCommand]
	public async Task ViewLoadedAsync()
	{
		try
		{
			Channel.Messages.IsVisible = true;
			_focusService.TrackView(Channel.Id);
			if (!Channel.Messages.HasFetchedOnce && StartupMessageId == null)
			{
				await Channel.Messages.FetchMessagesAsync(MessageDirectionTake.Both);
			}
			if (StartupMessageId == null)
			{
				Channel.Messages.InitializeVirtualizationWindow();
			}
			setNotificationsAsViewed();
		}
		catch
		{
		}
	}

	[RelayCommand]
	public void ViewUnloaded()
	{
		Channel.Messages.IsVisible = false;
		_focusService.UntrackView(Channel.Id);
		InitialMessagesBeganRendering = false;
		SetNewMessagesBannerStatus(isVisible: false);
		ShowJumpToPresent = false;
		if (Channel.Messages.HasMoreNewerMessages)
		{
			Channel.Messages.ReinitializeAsync().Forget();
			StartupMessageId = null;
		}
		else
		{
			Channel.Messages.ResetVirtualizationWindow();
		}
	}

	[RelayCommand]
	public async Task MarkAsReadAsync()
	{
		try
		{
			ShowNewMessagesBanner = false;
			await Channel.Messages.MarkContainerAsReadAsync();
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
		return Channel.Messages.FetchMessagesAsync(MessageDirectionTake.Newer);
	}

	[RelayCommand]
	public Task DownloadOlderMessagesAsync()
	{
		return Channel.Messages.FetchMessagesAsync(MessageDirectionTake.Older);
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
		await Channel.Messages.CreateMessageAsync(messagePayload.Message, messagePayload.AttachmentTokenUris, messagePayload.MessageIds, messagePayload.ShouldReplySendNotification, messagePayload.PendingAttachments);
	}

	public void FocusMessage(MessageGuid P_0)
	{
		this.FocusMessageRequested?.Invoke(P_0);
	}

	[RelayCommand]
	public void ShowEditChannelViewModel()
	{
		EditChannelViewModel editChannelViewModel = _editChannelViewModelFactory.Create(Channel);
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(editChannelViewModel));
	}

	[RelayCommand]
	public void ShowDeleteChannelViewModel()
	{
		DeleteChannelViewModel deleteChannelViewModel = _deleteChannelViewModelFactory.Create(Channel);
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(deleteChannelViewModel));
	}

	private void onReplyToMessageMessageReceived(object recipient, ReplyToMessageMessage message)
	{
		if (!_isMessageInEditMode && message.Message.MessageContainer.CommunityId == Channel.CommunityId && (MessageContainerGuid?)message.Message.MessageContainer.ContainerId == (MessageContainerGuid?)(MessageContainerGuid)Channel.Id)
		{
			RootMessageTextboxViewModel.SetReplyingToMessage(message.Message);
		}
	}

	private void onMessageEditModeChanged(object recipient, MessageEditModeChangedMessage message)
	{
		if ((MessageContainerGuid?)message.ContainerId == (MessageContainerGuid?)(MessageContainerGuid)Channel.Id)
		{
			_isMessageInEditMode = message.IsInEditMode;
		}
	}

	public void ShowFileUploadView()
	{
		IsFileUploaderOpen = true;
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(_fileUploadViewModelFactory.Create(Channel, delegate
		{
			IsFileUploaderOpen = false;
		}, RootMessageTextboxViewModel.AddAttachments)));
	}

	private void setNotificationsAsViewed()
	{
		_rootSessionAccessor.Session.NotificationService.SetContainerAsViewedAsync(Channel.Community.Id, Channel.Id).Forget();
	}

	private IViewModelBase getOrCreateViewModelForMessage(Message P_0)
	{
		if (_messageViewModelCache.TryGetValue(P_0, out IViewModelBase value))
		{
			return value;
		}
		IViewModelBase viewModelBase = ((!P_0.IsStartOfContainer) ? ((IViewModelBase)_messageViewModelFactory.Create(P_0)) : ((IViewModelBase)_channelStartMessageViewModelFactory.Create(P_0)));
		_messageViewModelCache[P_0] = viewModelBase;
		return viewModelBase;
	}

	public override void Dispose()
	{
		base.Dispose();
		_messageCacheCleanup?.Dispose();
		_virtualizedMessagesCleanup?.Dispose();
		_typingUsersCacheCleanup?.Dispose();
		_pinnedMessagesCountCleanup?.Dispose();
		_messageViewModelCache.Clear();
		Channel.PropertyChanged -= onChannelPropertyChanged;
		Channel.LocalChannelPermission.PropertyChanged -= onLocalChannelPermissionPropertyChanged;
		RootMessageTextboxViewModel.PropertyChanged -= onRootMessageTextboxViewModelPropertyChanged;
		RootMessageTextboxViewModel.EditLastMessageRequested -= onRootMessageTextboxViewModelEditLastMessageRequested;
		RootMessageTextboxViewModel.Dispose();
	}
}
