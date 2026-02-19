// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Messages.MessageViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using FluentValidation;
using ReactiveUI.Avalonia;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls.ContextMenus;
using RootApp.Client.Avalonia.Controls.Emojis;
using RootApp.Client.Avalonia.Controls.Messaging;
using RootApp.Client.Avalonia.Helpers.Badges;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Clipboard;
using RootApp.Client.Avalonia.Helpers.DeveloperMode;
using RootApp.Client.Avalonia.Helpers.Messages.SystemMessages;
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.Avalonia.UI.Messages;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Client.CoreDomain.Models.Messages;
using RootApp.Client.CoreDomain.Utils.Links;
using RootApp.Client.Domain.Helpers.Store;
using RootApp.WebApi.Shared.Packets;

public class MessageViewModel : ViewModelBase<MessageViewModel>
{
	private bool _subscribedToGlobalUserPropertyChanged;

	private readonly BitmapCache _bitmapCache;

	private readonly RootMessageTextboxViewModelFactory _rootMessageTextboxViewModelFactory;

	private readonly MemberProfileViewModelFactory _memberProfileViewModelFactory;

	private readonly DeleteMessageViewModelFactory _deleteMessageViewModelFactory;

	private readonly MessageRepliesViewModelFactory _messageRepliesViewModelFactory;

	private readonly ILocalDataStore _localDataStore;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly IDeveloperModeService _developerModeService;

	private readonly ClipboardService _clipboardService;

	private readonly IDisposable? _linkCacheCleanup;

	private readonly IDisposable? _mediaCacheCleanup;

	private readonly IDisposable? _fileCacheCleanup;

	private readonly IDisposable? _reactionCacheCleanup;

	[CompilerGenerated]
	private string? _003CPrimaryBadgeSvgPath_003Ek__BackingField;

	[CompilerGenerated]
	private string? _003CPrimaryBadgeName_003Ek__BackingField;

	private readonly ReadOnlyObservableCollection<LinkMessageViewModel>? _links;

	private readonly ReadOnlyObservableCollection<IViewModelBase>? _media;

	private readonly ReadOnlyObservableCollection<IViewModelBase>? _files;

	private readonly ReadOnlyObservableCollection<IViewModelBase>? _reactions;

	[CompilerGenerated]
	private bool _003CActionBarOpen_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsInEditMode_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsShiftKeyPressed_003Ek__BackingField;

	[CompilerGenerated]
	private RootMessageTextboxViewModel? _003CEditMessageTextboxViewModel_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsUsernameProfilePopupOpen_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsImageProfilePopupOpen_003Ek__BackingField;

	[CompilerGenerated]
	private MemberProfileViewModel? _003CMemberProfile_003Ek__BackingField;

	[CompilerGenerated]
	private MessageRepliesViewModel? _003CMessageRepliesViewModel_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? emojiPickerOpenedCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? emojiPickerClosedCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand<EmojiViewModel>? emojiSelectedCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? enterEditModeCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand<MessagePayload>? saveMessageCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? exitMessageCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? pinMessageCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? deleteMessageCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? directDeleteMessageCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? usernameProfileOpeningCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? usernameProfileClosingCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? imageProfileOpeningCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? imageProfileClosingCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? retrySendCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? replyToMessageCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? copyMessageIdCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ActionBarOpen
	{
		get
		{
			return _003CActionBarOpen_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CActionBarOpen_003Ek__BackingField, flag))
			{
				_003CActionBarOpen_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ActionBarOpen);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsInEditMode
	{
		get
		{
			return _003CIsInEditMode_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsInEditMode_003Ek__BackingField, flag))
			{
				_003CIsInEditMode_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsInEditMode);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsShiftKeyPressed
	{
		get
		{
			return _003CIsShiftKeyPressed_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsShiftKeyPressed_003Ek__BackingField, flag))
			{
				_003CIsShiftKeyPressed_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsShiftKeyPressed);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public RootMessageTextboxViewModel? EditMessageTextboxViewModel
	{
		get
		{
			return _003CEditMessageTextboxViewModel_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<RootMessageTextboxViewModel>.Default.Equals(_003CEditMessageTextboxViewModel_003Ek__BackingField, rootMessageTextboxViewModel))
			{
				_003CEditMessageTextboxViewModel_003Ek__BackingField = rootMessageTextboxViewModel;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.EditMessageTextboxViewModel);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsUsernameProfilePopupOpen
	{
		get
		{
			return _003CIsUsernameProfilePopupOpen_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsUsernameProfilePopupOpen_003Ek__BackingField, flag))
			{
				_003CIsUsernameProfilePopupOpen_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsUsernameProfilePopupOpen);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsImageProfilePopupOpen
	{
		get
		{
			return _003CIsImageProfilePopupOpen_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsImageProfilePopupOpen_003Ek__BackingField, flag))
			{
				_003CIsImageProfilePopupOpen_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsImageProfilePopupOpen);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public MemberProfileViewModel? MemberProfile
	{
		get
		{
			return _003CMemberProfile_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<MemberProfileViewModel>.Default.Equals(_003CMemberProfile_003Ek__BackingField, memberProfileViewModel))
			{
				_003CMemberProfile_003Ek__BackingField = memberProfileViewModel;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.MemberProfile);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public MessageRepliesViewModel? MessageRepliesViewModel
	{
		get
		{
			return _003CMessageRepliesViewModel_003Ek__BackingField;
		}
		private set
		{
			if (!EqualityComparer<RootApp.Client.Avalonia.UI.Messages.MessageRepliesViewModel>.Default.Equals(_003CMessageRepliesViewModel_003Ek__BackingField, messageRepliesViewModel))
			{
				_003CMessageRepliesViewModel_003Ek__BackingField = messageRepliesViewModel;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.MessageRepliesViewModel);
			}
		}
	}

	public Message Message { get; }

	public EmojiPickerViewModel EmojiPickerViewModel { get; }

	public IMarkdownEngine MarkdownEngine { get; }

	public Lazy<UserContextMenuViewModel>? UserContextMenu { get; }

	public string? PrimaryBadgeSvgPath
	{
		[CompilerGenerated]
		get
		{
			return _003CPrimaryBadgeSvgPath_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CPrimaryBadgeSvgPath_003Ek__BackingField = text;
		}
	}

	public string? PrimaryBadgeName
	{
		[CompilerGenerated]
		get
		{
			return _003CPrimaryBadgeName_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CPrimaryBadgeName_003Ek__BackingField = text;
		}
	}

	public bool DeveloperModeEnabled => _developerModeService.IsEnabled;

	public Task<BitmapWrapper?> ProfilePictureAsyncBitmapWrapper => _bitmapCache.GetBitmapAsync(Message.SenderMember?.GlobalUser.ProfilePictureUri, null, 120);

	public ReadOnlyObservableCollection<LinkMessageViewModel>? Links => _links;

	public ReadOnlyObservableCollection<IViewModelBase>? Media => _media;

	public ReadOnlyObservableCollection<IViewModelBase>? Files => _files;

	public ReadOnlyObservableCollection<IViewModelBase>? Reactions => _reactions;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand EmojiPickerOpenedCommand => emojiPickerOpenedCommand ?? (emojiPickerOpenedCommand = new RelayCommand(EmojiPickerOpened));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand EmojiPickerClosedCommand => emojiPickerClosedCommand ?? (emojiPickerClosedCommand = new RelayCommand(EmojiPickerClosed));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand<EmojiViewModel> EmojiSelectedCommand => emojiSelectedCommand ?? (emojiSelectedCommand = new AsyncRelayCommand<EmojiViewModel>(EmojiSelectedAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand EnterEditModeCommand => enterEditModeCommand ?? (enterEditModeCommand = new RelayCommand(EnterEditMode));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand<MessagePayload> SaveMessageCommand => saveMessageCommand ?? (saveMessageCommand = new AsyncRelayCommand<MessagePayload>(SaveMessageAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ExitMessageCommand => exitMessageCommand ?? (exitMessageCommand = new RelayCommand(ExitMessage));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand PinMessageCommand => pinMessageCommand ?? (pinMessageCommand = new AsyncRelayCommand(PinMessageAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand DeleteMessageCommand => deleteMessageCommand ?? (deleteMessageCommand = new RelayCommand(DeleteMessage));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand DirectDeleteMessageCommand => directDeleteMessageCommand ?? (directDeleteMessageCommand = new AsyncRelayCommand(DirectDeleteMessageAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand UsernameProfileOpeningCommand => usernameProfileOpeningCommand ?? (usernameProfileOpeningCommand = new RelayCommand(UsernameProfileOpening));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand UsernameProfileClosingCommand => usernameProfileClosingCommand ?? (usernameProfileClosingCommand = new RelayCommand(UsernameProfileClosing));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ImageProfileOpeningCommand => imageProfileOpeningCommand ?? (imageProfileOpeningCommand = new RelayCommand(ImageProfileOpening));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ImageProfileClosingCommand => imageProfileClosingCommand ?? (imageProfileClosingCommand = new RelayCommand(ImageProfileClosing));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand RetrySendCommand => retrySendCommand ?? (retrySendCommand = new AsyncRelayCommand(RetrySendAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ReplyToMessageCommand => replyToMessageCommand ?? (replyToMessageCommand = new RelayCommand(ReplyToMessage));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CopyMessageIdCommand => copyMessageIdCommand ?? (copyMessageIdCommand = new RelayCommand(CopyMessageId));

	public MessageViewModel(Message P_0, BitmapCache P_1, LinkMessageViewModelFactory P_2, ImageMessageViewModelFactory P_3, GifMessageViewModelFactory P_4, VideoMessageViewModelFactory P_5, ReactionViewModelFactory P_6, AddReactionViewModelFactory P_7, FileMessageViewModelFactory P_8, PendingMediaMessageViewModelFactory P_9, PendingFileMessageViewModelFactory P_10, EmojiPickerViewModel P_11, RootMessageTextboxViewModelFactory P_12, RootMarkdownEngineManager P_13, MemberProfileViewModelFactory P_14, UserContextMenuViewModelFactory P_15, DeleteMessageViewModelFactory P_16, MessageRepliesViewModelFactory P_17, ILocalDataStore P_18, IRootSessionAccessor P_19, IDeveloperModeService P_20, ClipboardService P_21)
		: base((IValidator<MessageViewModel>?)null)
	{
		MessageViewModel messageViewModel = this;
		_bitmapCache = P_1;
		_rootMessageTextboxViewModelFactory = P_12;
		_memberProfileViewModelFactory = P_14;
		_deleteMessageViewModelFactory = P_16;
		_messageRepliesViewModelFactory = P_17;
		_localDataStore = P_18;
		_rootSessionAccessor = P_19;
		_developerModeService = P_20;
		_clipboardService = P_21;
		_developerModeService.PropertyChanged += onDeveloperModePropertyChanged;
		MarkdownEngine = P_13.FullEngine;
		Message = P_0;
		EmojiPickerViewModel = P_11;
		if (Message.HasMessageReply)
		{
			MessageRepliesViewModel = _messageRepliesViewModelFactory.Create(Message);
		}
		if (Message.SenderMember != null)
		{
			IMessageContainerMember senderMember = Message.SenderMember;
			Member member = senderMember as Member;
			if (member != null)
			{
				UserContextMenu = new Lazy<UserContextMenuViewModel>(() => P_15.Create(member));
			}
			else
			{
				UserContextMenu = new Lazy<UserContextMenuViewModel>(() => P_15.Create(messageViewModel.Message.SenderMember.GlobalUser));
			}
			_linkCacheCleanup = Message.ConnectLinks().Transform((Uri uri) => P_2.Create(messageViewModel.Message.SenderMember.GlobalUser, uri)).ObserveOn(AvaloniaScheduler.Instance)
				.Bind(out _links)
				.DisposeMany()
				.Subscribe();
			IObservable<IChangeSet<IViewModelBase>> observable = Message.ConnectMedia().Transform((Func<Attachment, IViewModelBase>)delegate(Attachment attachment)
			{
				if (attachment.FileType == FileType.Gif)
				{
					return P_4.Create(messageViewModel.Message.SenderMember.GlobalUser, attachment);
				}
				if (attachment.FileType == FileType.Image)
				{
					return P_3.Create(messageViewModel.Message.SenderMember.GlobalUser, attachment);
				}
				if (attachment.FileType == FileType.Video)
				{
					return P_5.Create(messageViewModel.Message.SenderMember.GlobalUser, attachment);
				}
				return (attachment.FileType == FileType.SVG) ? P_8.Create(attachment) : null;
			}, false);
			IObservable<IChangeSet<IViewModelBase>> observable2 = Message.ConnectPendingMedia().Transform((Func<PendingAttachment, IViewModelBase>)((PendingAttachment pending) => P_9.Create(pending, messageViewModel.Message)), false);
			_mediaCacheCleanup = observable2.Merge(observable).ObserveOn(AvaloniaScheduler.Instance).Bind(out _media)
				.DisposeMany()
				.Subscribe();
			if (!_subscribedToGlobalUserPropertyChanged)
			{
				_subscribedToGlobalUserPropertyChanged = true;
				Message.SenderMember.GlobalUser.PropertyChanged += onGlobalUserPropertyChanged;
			}
			updatePrimaryBadgeSvgPath();
		}
		else
		{
			_mediaCacheCleanup = Message.ConnectPendingMedia().Transform((Func<PendingAttachment, IViewModelBase>)((PendingAttachment pending) => P_9.Create(pending, messageViewModel.Message)), false).ObserveOn(AvaloniaScheduler.Instance)
				.Bind(out _media)
				.DisposeMany()
				.Subscribe();
		}
		IObservable<IChangeSet<IViewModelBase>> observable3 = Message.ConnectFiles().Transform((Func<Attachment, IViewModelBase>)((Attachment file) => P_8.Create(file)), false);
		IObservable<IChangeSet<IViewModelBase>> observable4 = Message.ConnectPendingFiles().Transform((Func<PendingAttachment, IViewModelBase>)((PendingAttachment pending) => P_10.Create(pending, messageViewModel.Message)), false);
		_fileCacheCleanup = observable4.Merge(observable3).ObserveOn(AvaloniaScheduler.Instance).Bind(out _files)
			.DisposeMany()
			.Subscribe();
		_reactionCacheCleanup = Message.ConnectReactions().Transform((Reaction reaction) => reaction.IsPlaceHolder ? ((IViewModelBase)P_7.Create(messageViewModel.Message)) : ((IViewModelBase)P_6.Create(reaction, messageViewModel.Message))).ObserveOn(AvaloniaScheduler.Instance)
			.Bind(out _reactions)
			.DisposeMany()
			.Subscribe();
		Message.PropertyChanged += onMessagePropertyChanged;
		if (Message.IsSystemMessage)
		{
			processSystemMessage();
		}
	}

	[RelayCommand]
	public void EmojiPickerOpened()
	{
		ActionBarOpen = true;
		EmojiPickerViewModel.EmojiSelectedCommand = EmojiSelectedCommand;
	}

	[RelayCommand]
	public void EmojiPickerClosed()
	{
		ActionBarOpen = false;
	}

	[RelayCommand]
	public async Task EmojiSelectedAsync(EmojiViewModel emojiViewModel)
	{
		try
		{
			await Message.MessageContainer.Messages.CreateReactionAsync(Message.MessageId, emojiViewModel.Emoji.Shortname);
		}
		catch
		{
		}
	}

	[RelayCommand]
	public void EnterEditMode()
	{
		if (!Message.IsSystemMessage && !Message.IsPlaceholder)
		{
			EditMessageTextboxViewModel = _rootMessageTextboxViewModelFactory.Create(Message.MessageContainer, SaveMessageCommand, ExitMessageCommand, Message.MessageContent);
			IsInEditMode = true;
			WeakReferenceMessenger.Default.Send(new MessageEditModeChangedMessage(Message.MessageContainer.ContainerId, true));
		}
	}

	[RelayCommand]
	public async Task SaveMessageAsync(MessagePayload messagePayload)
	{
		try
		{
			IsInEditMode = false;
			WeakReferenceMessenger.Default.Send(new MessageEditModeChangedMessage(Message.MessageContainer.ContainerId, false));
			WeakReferenceMessenger.Default.Send(new FocusMessageContainerTextboxMessage(Message.MessageContainer.ContainerId));
			await Message.MessageContainer.Messages.EditMessageAsync(Message.MessageId, messagePayload.Message);
		}
		catch
		{
		}
		finally
		{
			EditMessageTextboxViewModel?.Dispose();
			EditMessageTextboxViewModel = null;
		}
	}

	[RelayCommand]
	public void ExitMessage()
	{
		IsInEditMode = false;
		WeakReferenceMessenger.Default.Send(new MessageEditModeChangedMessage(Message.MessageContainer.ContainerId, false));
		EditMessageTextboxViewModel?.Dispose();
		EditMessageTextboxViewModel = null;
	}

	[RelayCommand]
	public async Task PinMessageAsync()
	{
		try
		{
			if (Message.HasBeenPinned)
			{
				await Message.MessageContainer.Messages.UnpinMessageAsync(Message.MessageId);
			}
			else
			{
				await Message.MessageContainer.Messages.PinMessageAsync(Message.MessageId);
			}
		}
		catch
		{
		}
	}

	[RelayCommand]
	public void DeleteMessage()
	{
		DeleteMessageViewModel deleteMessageViewModel = _deleteMessageViewModelFactory.Create(Message);
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(deleteMessageViewModel));
	}

	[RelayCommand]
	public async Task DirectDeleteMessageAsync()
	{
		try
		{
			await Message.MessageContainer.Messages.DeleteMessageAsync(Message.MessageId);
		}
		catch
		{
		}
	}

	[RelayCommand]
	public void UsernameProfileOpening()
	{
		if (Message.SenderMember != null)
		{
			MemberProfile?.Dispose();
			MemberProfile = _memberProfileViewModelFactory.Create(Message.SenderMember, UsernameProfileClosing);
			IsUsernameProfilePopupOpen = true;
		}
	}

	[RelayCommand]
	public void UsernameProfileClosing()
	{
		IsUsernameProfilePopupOpen = false;
		MemberProfile?.Dispose();
		MemberProfile = null;
	}

	[RelayCommand]
	public void ImageProfileOpening()
	{
		if (Message.SenderMember != null)
		{
			MemberProfile?.Dispose();
			MemberProfile = _memberProfileViewModelFactory.Create(Message.SenderMember, ImageProfileClosing);
			IsImageProfilePopupOpen = true;
		}
	}

	[RelayCommand]
	public void ImageProfileClosing()
	{
		IsImageProfilePopupOpen = false;
		MemberProfile?.Dispose();
		MemberProfile = null;
	}

	[RelayCommand]
	public Task RetrySendAsync()
	{
		if (Message.FailedToSend)
		{
			return Message.RetrySendingMessageAsync();
		}
		return Task.CompletedTask;
	}

	[RelayCommand]
	public void ReplyToMessage()
	{
		WeakReferenceMessenger.Default.Send(new ReplyToMessageMessage(Message));
	}

	[RelayCommand]
	public void CopyMessageId()
	{
		_clipboardService.CopyTextToClipboard(Message.MessageId.ToString());
	}

	public bool IsTapToReplyEnabled()
	{
		ILocalDataStore localDataStore = _localDataStore;
		global::_003C_003Ey__InlineArray2<string> _003C_003Ey__InlineArray281 = default(global::_003C_003Ey__InlineArray2<string>);
		_003C_003Ey__InlineArray281[0] = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id.ToString();
		_003C_003Ey__InlineArray281[1] = "TapToReply";
		if (!localDataStore.TryGetWithPath((ReadOnlySpan<string>)_003C_003Ey__InlineArray281, out int value))
		{
			value = 1;
		}
		return Convert.ToBoolean(value);
	}

	private void onMessagePropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "SenderMember")
			{
				if (Message.SenderMember != null)
				{
					OnPropertyChanged("ProfilePictureAsyncBitmapWrapper");
					updatePrimaryBadgeSvgPath();
					if (!_subscribedToGlobalUserPropertyChanged)
					{
						_subscribedToGlobalUserPropertyChanged = true;
						Message.SenderMember.GlobalUser.PropertyChanged += onGlobalUserPropertyChanged;
					}
				}
			}
			else if (e.PropertyName == "SystemMessageLog")
			{
				processSystemMessage();
			}
			else if (e.PropertyName == "HasMessageReply" && Message.HasMessageReply)
			{
				MessageRepliesViewModel = _messageRepliesViewModelFactory.Create(Message);
			}
		});
	}

	private void onGlobalUserPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "ProfilePictureUri")
			{
				OnPropertyChanged("ProfilePictureAsyncBitmapWrapper");
			}
			else if (e.PropertyName == "Badges" || e.PropertyName == "PrimaryBadge")
			{
				updatePrimaryBadgeSvgPath();
			}
		});
	}

	private void onDeveloperModePropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "IsEnabled")
		{
			Dispatcher.UIThread.Post(delegate
			{
				OnPropertyChanged("DeveloperModeEnabled");
			});
		}
	}

	private void updatePrimaryBadgeSvgPath()
	{
		PrimaryBadgeSvgPath = null;
		PrimaryBadgeName = null;
		Application current = Application.Current;
		UserBadge userBadge = Message.SenderMember?.GlobalUser.PrimaryBadge;
		if (current == null || userBadge == null)
		{
			OnPropertyChanged("PrimaryBadgeSvgPath");
			OnPropertyChanged("PrimaryBadgeName");
			return;
		}
		if (BadgeSvgMapper.TryGetBadgeInfo(userBadge.Id, out BadgeSvgMapper.BadgeDisplayInfo badgeDisplayInfo) && badgeDisplayInfo != null && current.TryFindResource(badgeDisplayInfo.ResourceKey, current.ActualThemeVariant, out object obj) && obj is string primaryBadgeSvgPath)
		{
			PrimaryBadgeSvgPath = primaryBadgeSvgPath;
			PrimaryBadgeName = badgeDisplayInfo.DisplayName;
		}
		OnPropertyChanged("PrimaryBadgeSvgPath");
		OnPropertyChanged("PrimaryBadgeName");
	}

	private void processSystemMessage()
	{
		Message message = Message;
		if (message != null && message.IsSystemMessage && message.SystemMessageLog != null)
		{
			string text = SystemMessageFormatter.CreateFormattedSystemMessage(Message.SystemMessageLog);
			Message.UpdateMessageFromFormattedSystemMessage(text);
		}
	}

	public override void Dispose()
	{
		base.Dispose();
		Message.PropertyChanged -= onMessagePropertyChanged;
		if (Message.SenderMember != null)
		{
			Message.SenderMember.GlobalUser.PropertyChanged -= onGlobalUserPropertyChanged;
		}
		_developerModeService.PropertyChanged -= onDeveloperModePropertyChanged;
		_linkCacheCleanup?.Dispose();
		_mediaCacheCleanup?.Dispose();
		_fileCacheCleanup?.Dispose();
		_reactionCacheCleanup?.Dispose();
		MemberProfile?.Dispose();
		MessageRepliesViewModel?.Dispose();
	}
}
