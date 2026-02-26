using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using DynamicData;
using RootApp.Assets;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Client.CoreDomain.Models.DirectMessages;
using RootApp.Client.CoreDomain.Models.Messages.SystemMessages;
using RootApp.Client.CoreDomain.Models.User;
using RootApp.Client.CoreDomain.Services;
using RootApp.Client.CoreDomain.Utils.Files;
using RootApp.Client.CoreDomain.Utils.Links;
using RootApp.Core;
using RootApp.Core.Identifiers;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Packets;
using RootApp.WebApi.Shared.Payloads.Message;

namespace RootApp.Client.CoreDomain.Models.Messages;

public class Message : ObservableObject, IDisposable
{
	private bool _disposed = false;

	private Dictionary<Uri, AssetLinkWrapper>? _assetLinks;

	private List<MessageUri>? _messageUris;

	private readonly MessageCreateRequest? _createRequest;

	private readonly Func<MessageCreateRequest, Message, Task>? _failedMessageRetryCallback;

	private readonly LinkHelper _linkHelper;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly ReactionFactory _reactionFactory;

	private readonly FileNameFormatter _fileNameFormatter;

	private readonly IGlobalUserCacheService _globalUserCacheService;

	private readonly AssetLinkWrapperFactory _assetLinkWrapperFactory;

	[CompilerGenerated]
	private MessageGuid <MessageId>k__BackingField;

	[CompilerGenerated]
	private bool <IsStartOfContainer>k__BackingField;

	[CompilerGenerated]
	private List<MessageReply>? <MessageReplies>k__BackingField;

	[CompilerGenerated]
	private string <MessageContent>k__BackingField = string.Empty;

	private readonly SourceList<Uri> _linkCache = new SourceList<Uri>();

	private readonly SourceList<Attachment> _fileCache = new SourceList<Attachment>();

	private readonly SourceList<Attachment> _mediaCache = new SourceList<Attachment>();

	private readonly SourceList<Reaction> _reactions = new SourceList<Reaction>();

	private readonly SourceList<PendingAttachment> _pendingFileCache = new SourceList<PendingAttachment>();

	private readonly SourceList<PendingAttachment> _pendingMediaCache = new SourceList<PendingAttachment>();

	[CompilerGenerated]
	private long <MessageEpoch>k__BackingField;

	[CompilerGenerated]
	private DateTimeOffset <SentAtUtc>k__BackingField;

	[CompilerGenerated]
	private DateTimeOffset? <DeletedAt>k__BackingField;

	[CompilerGenerated]
	private DateTimeOffset? <EditedAt>k__BackingField;

	[CompilerGenerated]
	private DateTimeOffset? <PinnedAt>k__BackingField;

	[CompilerGenerated]
	private bool <IsPlaceholder>k__BackingField;

	[CompilerGenerated]
	private bool <FailedToSend>k__BackingField;

	[CompilerGenerated]
	private bool <ShowUserProfile>k__BackingField;

	[CompilerGenerated]
	private bool <ShowDateDivider>k__BackingField;

	[CompilerGenerated]
	private bool <ShowNewDivider>k__BackingField;

	[CompilerGenerated]
	private IMessageContainerMember? <SenderMember>k__BackingField;

	[CompilerGenerated]
	private bool <HasSelfMention>k__BackingField;

	[CompilerGenerated]
	private SystemMessageLog? <SystemMessageLog>k__BackingField;

	[CompilerGenerated]
	private bool <HasMessageReply>k__BackingField;

	[CompilerGenerated]
	private bool <HasLocalPendingReply>k__BackingField;

	public IMessageContainer MessageContainer { get; }

	public MessageGuid MessageId
	{
		[CompilerGenerated]
		get
		{
			return <MessageId>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<MessageId>k__BackingField = messageGuid;
		}
	}

	public MessageType MessageType { get; }

	public UserGuid SenderUserId { get; }

	public bool IsMyMessage { get; }

	public bool IsSystemMessage => MessageType == MessageType.System;

	public bool IsStartOfContainer
	{
		[CompilerGenerated]
		get
		{
			return <IsStartOfContainer>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<IsStartOfContainer>k__BackingField = flag;
		}
	} = false;

	public MessagePayload? SystemMessagePayload { get; }

	public List<MessageReply>? MessageReplies
	{
		[CompilerGenerated]
		get
		{
			return <MessageReplies>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<MessageReplies>k__BackingField = list;
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public long MessageEpoch
	{
		get
		{
			return <MessageEpoch>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<long>.Default.Equals(<MessageEpoch>k__BackingField, num))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.MessageEpoch);
				<MessageEpoch>k__BackingField = num;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.MessageEpoch);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public DateTimeOffset SentAtUtc
	{
		get
		{
			return <SentAtUtc>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<DateTimeOffset>.Default.Equals(<SentAtUtc>k__BackingField, dateTimeOffset))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.SentAtUtc);
				<SentAtUtc>k__BackingField = dateTimeOffset;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.SentAtUtc);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public DateTimeOffset? DeletedAt
	{
		get
		{
			return <DeletedAt>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<DateTimeOffset?>.Default.Equals(<DeletedAt>k__BackingField, dateTimeOffset))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.DeletedAt);
				<DeletedAt>k__BackingField = dateTimeOffset;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.DeletedAt);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public DateTimeOffset? EditedAt
	{
		get
		{
			return <EditedAt>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<DateTimeOffset?>.Default.Equals(<EditedAt>k__BackingField, dateTimeOffset))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.EditedAt);
				<EditedAt>k__BackingField = dateTimeOffset;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.EditedAt);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public DateTimeOffset? PinnedAt
	{
		get
		{
			return <PinnedAt>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<DateTimeOffset?>.Default.Equals(<PinnedAt>k__BackingField, dateTimeOffset))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.PinnedAt);
				<PinnedAt>k__BackingField = dateTimeOffset;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.PinnedAt);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsPlaceholder
	{
		get
		{
			return <IsPlaceholder>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsPlaceholder>k__BackingField, flag))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsPlaceholder);
				<IsPlaceholder>k__BackingField = flag;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsPlaceholder);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool FailedToSend
	{
		get
		{
			return <FailedToSend>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<FailedToSend>k__BackingField, flag))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.FailedToSend);
				<FailedToSend>k__BackingField = flag;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.FailedToSend);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string MessageContent
	{
		get
		{
			return <MessageContent>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<MessageContent>k__BackingField, text))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.MessageContent);
				<MessageContent>k__BackingField = text;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.MessageContent);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ShowUserProfile
	{
		get
		{
			return <ShowUserProfile>k__BackingField;
		}
		private set
		{
			if (!EqualityComparer<bool>.Default.Equals(<ShowUserProfile>k__BackingField, flag))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.ShowUserProfile);
				<ShowUserProfile>k__BackingField = flag;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.ShowUserProfile);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ShowDateDivider
	{
		get
		{
			return <ShowDateDivider>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<ShowDateDivider>k__BackingField, flag))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.ShowDateDivider);
				<ShowDateDivider>k__BackingField = flag;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.ShowDateDivider);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ShowNewDivider
	{
		get
		{
			return <ShowNewDivider>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<ShowNewDivider>k__BackingField, flag))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.ShowNewDivider);
				<ShowNewDivider>k__BackingField = flag;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.ShowNewDivider);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IMessageContainerMember? SenderMember
	{
		get
		{
			return <SenderMember>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<IMessageContainerMember>.Default.Equals(<SenderMember>k__BackingField, messageContainerMember))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.SenderMember);
				<SenderMember>k__BackingField = messageContainerMember;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.SenderMember);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool HasSelfMention
	{
		get
		{
			return <HasSelfMention>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<HasSelfMention>k__BackingField, flag))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.HasSelfMention);
				<HasSelfMention>k__BackingField = flag;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.HasSelfMention);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public SystemMessageLog? SystemMessageLog
	{
		get
		{
			return <SystemMessageLog>k__BackingField;
		}
		private set
		{
			if (!EqualityComparer<RootApp.Client.CoreDomain.Models.Messages.SystemMessages.SystemMessageLog>.Default.Equals(<SystemMessageLog>k__BackingField, systemMessageLog))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.SystemMessageLog);
				<SystemMessageLog>k__BackingField = systemMessageLog;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.SystemMessageLog);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool HasMessageReply
	{
		get
		{
			return <HasMessageReply>k__BackingField;
		}
		private set
		{
			if (!EqualityComparer<bool>.Default.Equals(<HasMessageReply>k__BackingField, flag))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.HasMessageReply);
				<HasMessageReply>k__BackingField = flag;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.HasMessageReply);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool HasLocalPendingReply
	{
		get
		{
			return <HasLocalPendingReply>k__BackingField;
		}
		private set
		{
			if (!EqualityComparer<bool>.Default.Equals(<HasLocalPendingReply>k__BackingField, flag))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.HasLocalPendingReply);
				<HasLocalPendingReply>k__BackingField = flag;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.HasLocalPendingReply);
			}
		}
	}

	public bool HasBeenEdited => EditedAt.HasValue;

	public bool HasBeenDeleted => DeletedAt.HasValue;

	public bool HasBeenPinned => PinnedAt.HasValue;

	public Message(MessagePacket P_0, Dictionary<Uri, AssetLinkWrapper>? P_1, bool P_2, bool P_3, IMessageContainer P_4, LinkHelper P_5, IRootSessionAccessor P_6, ReactionFactory P_7, FileNameFormatter P_8, IGlobalUserCacheService P_9, AssetLinkWrapperFactory P_10)
	{
		IsPlaceholder = false;
		MessageId = P_0.Id;
		DateTime dateTime = MessageId.ToDateTime();
		SentAtUtc = dateTime;
		MessageType = P_0.MessageType;
		DeletedAt = P_0.DeletedAt?.ToDateTimeOffset();
		EditedAt = P_0.EditedAt?.ToDateTimeOffset();
		PinnedAt = P_0.PinnedAt?.ToDateTimeOffset();
		MessageContent = P_0.MessageContent;
		SenderUserId = P_0.UserId;
		_messageUris = P_0.MessageUris.ToList();
		MessageReplies = (from reply in P_0.ParentMessages
			orderby reply.Id
			select new MessageReply(reply)).ToList();
		HasMessageReply = MessageReplies.Count > 0;
		MessageEpoch = MessageId.ToEpoch();
		ShowDateDivider = P_3;
		ShowUserProfile = P_3 || P_2 || HasMessageReply;
		_assetLinks = P_1;
		_linkHelper = P_5;
		_rootSessionAccessor = P_6;
		MessageContainer = P_4;
		_reactionFactory = P_7;
		_fileNameFormatter = P_8;
		_globalUserCacheService = P_9;
		_assetLinkWrapperFactory = P_10;
		if (MessageReplies.Any((MessageReply m) => m.UserId == _rootSessionAccessor.Session.UserInfoService.SessionUser.Id))
		{
			HasSelfMention = true;
		}
		IsMyMessage = SenderUserId == _rootSessionAccessor.Session.UserInfoService.SessionUser.Id;
		if (MessageType == MessageType.System)
		{
			ShowUserProfile = false;
			SystemMessagePayload = P_0.Payload;
		}
		processInitialReactions(P_0.Reactions);
		getSenderMemberAsync().ConfigureAwait(continueOnCapturedContext: false);
		processInitialUrisAsync().ConfigureAwait(continueOnCapturedContext: false);
	}

	public Message(string P_0, bool P_1, bool P_2, IMessageContainer P_3, MessageCreateRequest? P_4, Func<MessageCreateRequest, Message, Task>? P_5, IReadOnlyList<PendingAttachment>? P_6, LinkHelper P_7, IRootSessionAccessor P_8, ReactionFactory P_9, FileNameFormatter P_10, IGlobalUserCacheService P_11, AssetLinkWrapperFactory P_12)
	{
		IsPlaceholder = true;
		MessageContent = P_0;
		ShowDateDivider = P_2;
		ShowUserProfile = P_2 || P_1;
		_createRequest = P_4;
		_failedMessageRetryCallback = P_5;
		_linkHelper = P_7;
		_rootSessionAccessor = P_8;
		MessageContainer = P_3;
		_reactionFactory = P_9;
		_fileNameFormatter = P_10;
		_globalUserCacheService = P_11;
		_assetLinkWrapperFactory = P_12;
		MessageId = MessageGuid.Empty;
		SentAtUtc = DateTimeOffset.UtcNow;
		MessageType = MessageType.UserMessage;
		SenderUserId = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id;
		IsMyMessage = true;
		if (P_6 != null)
		{
			foreach (PendingAttachment item in P_6)
			{
				if (item.IsMedia)
				{
					_pendingMediaCache.Add(item);
				}
				else
				{
					_pendingFileCache.Add(item);
				}
			}
		}
		getSenderMemberAsync().ConfigureAwait(continueOnCapturedContext: false);
	}

	public Message(IMessageContainer P_0, LinkHelper P_1, IRootSessionAccessor P_2, ReactionFactory P_3, FileNameFormatter P_4, IGlobalUserCacheService P_5, AssetLinkWrapperFactory P_6)
	{
		IsStartOfContainer = true;
		DateTime dateTime = P_0.ContainerId.ToDateTime();
		SentAtUtc = dateTime;
		MessageContent = string.Empty;
		_linkHelper = P_1;
		_rootSessionAccessor = P_2;
		MessageContainer = P_0;
		_reactionFactory = P_3;
		_fileNameFormatter = P_4;
		_globalUserCacheService = P_5;
		_assetLinkWrapperFactory = P_6;
	}

	public void ConvertPlaceholderToRealMessage(MessagePacket P_0)
	{
		bool flag = _pendingMediaCache.Items.Any((PendingAttachment p) => p.FileType == FileType.Video);
		bool flag2 = _pendingMediaCache.Items.Any((PendingAttachment p) => p.FileType != FileType.Video);
		bool flag3 = _pendingFileCache.Count > 0;
		IsPlaceholder = false;
		FailedToSend = false;
		MessageId = P_0.Id;
		SentAtUtc = MessageId.ToDateTime();
		MessageEpoch = MessageId.ToEpoch();
		MessageContent = P_0.MessageContent;
		_messageUris = P_0.MessageUris.ToList();
		MessageReplies = (from reply in P_0.ParentMessages
			orderby reply.Id
			select new MessageReply(reply)).ToList();
		HasMessageReply = MessageReplies.Count > 0;
		if (MessageReplies.Any((MessageReply m) => m.UserId == _rootSessionAccessor.Session.UserInfoService.SessionUser.Id))
		{
			HasSelfMention = true;
		}
		ShowUserProfile = ShowDateDivider || ShowUserProfile || HasMessageReply;
		_assetLinks = P_0.ReferenceMaps?.Assets.ToDictionary((KeyValuePair<string, AssetInformation> pair) => new Uri(pair.Key), (KeyValuePair<string, AssetInformation> pair) => _assetLinkWrapperFactory.Create(pair.Key, pair.Value));
		if (flag)
		{
			_pendingMediaCache.Clear();
			_pendingFileCache.Clear();
			processInitialUrisAsync().ConfigureAwait(continueOnCapturedContext: false);
		}
		else if (!flag2 && !flag3)
		{
			processInitialUrisAsync().ConfigureAwait(continueOnCapturedContext: false);
		}
	}

	public void UpdateMessage(MessagePacket P_0)
	{
		DeletedAt = P_0.DeletedAt?.ToDateTimeOffset();
		EditedAt = P_0.EditedAt?.ToDateTimeOffset();
		PinnedAt = P_0.PinnedAt?.ToDateTimeOffset();
		MessageContent = P_0.MessageContent;
		_messageUris = P_0.MessageUris.ToList();
		_assetLinks = P_0.ReferenceMaps?.Assets.ToDictionary((KeyValuePair<string, AssetInformation> pair) => new Uri(pair.Key), (KeyValuePair<string, AssetInformation> pair) => _assetLinkWrapperFactory.Create(pair.Key, pair.Value));
		_linkCache.Clear();
		_fileCache.Clear();
		_mediaCache.Clear();
		_pendingFileCache.Clear();
		_pendingMediaCache.Clear();
		processInitialUrisAsync().ConfigureAwait(continueOnCapturedContext: false);
	}

	public void UpdateMessageFromFormattedSystemMessage(string P_0)
	{
		MessageContent = P_0;
	}

	public void SetAsDeleted()
	{
		DeletedAt = DateTimeOffset.UtcNow;
		MessageContent = string.Empty;
		_linkCache.Clear();
		_linkCache.Dispose();
		_fileCache.Clear();
		_fileCache.Dispose();
		_mediaCache.Clear();
		_mediaCache.Dispose();
		_reactions.Clear();
		_reactions.Dispose();
	}

	public async Task RetrySendingMessageAsync()
	{
		if (_failedMessageRetryCallback != null && _createRequest != null)
		{
			await _failedMessageRetryCallback(_createRequest, this).ConfigureAwait(continueOnCapturedContext: false);
		}
	}

	public IObservable<IChangeSet<Uri>> ConnectLinks()
	{
		return _linkCache.Connect();
	}

	public IObservable<IChangeSet<Attachment>> ConnectFiles()
	{
		return _fileCache.Connect();
	}

	public IObservable<IChangeSet<Attachment>> ConnectMedia()
	{
		return _mediaCache.Connect();
	}

	public IObservable<IChangeSet<Reaction>> ConnectReactions()
	{
		return _reactions.Connect();
	}

	public IObservable<IChangeSet<PendingAttachment>> ConnectPendingFiles()
	{
		return _pendingFileCache.Connect();
	}

	public IObservable<IChangeSet<PendingAttachment>> ConnectPendingMedia()
	{
		return _pendingMediaCache.Connect();
	}

	public void SetNewDividerStatus(bool P_0)
	{
		ShowNewDivider = P_0;
		if (ShowNewDivider)
		{
			SetShowUserProfile(true);
		}
	}

	public void SetShowUserProfile(bool P_0)
	{
		if (MessageType == MessageType.System)
		{
			ShowUserProfile = false;
		}
		else
		{
			ShowUserProfile = P_0;
		}
	}

	public void ProcessReaction(UserGuid P_0, string P_1, bool P_2)
	{
		if (P_2)
		{
			addReaction(P_0, P_1);
		}
		else
		{
			removeReaction(P_0, P_1);
		}
	}

	public IEnumerable<MessageReply> GetReplyMessages(MessageGuid P_0)
	{
		if (MessageReplies == null)
		{
			return Array.Empty<MessageReply>();
		}
		return MessageReplies.Where((MessageReply m) => m.Id == P_0);
	}

	public void SetLocalPendingReplyStatus(bool P_0)
	{
		HasLocalPendingReply = P_0;
	}

	private async Task processInitialUrisAsync()
	{
		if (_messageUris == null)
		{
			return;
		}
		for (int i = 0; i < _messageUris.Count; i++)
		{
			MessageUri messageUri = _messageUris[i];
			Uri uri = new Uri(messageUri.Uri);
			AssetLinkWrapper internalAssetLinkList;
			switch (_linkHelper.GetLinkType(uri))
			{
			case LinkType.InternalAsset:
				if (_assetLinks != null && _assetLinks.TryGetValue(uri, out internalAssetLinkList))
				{
					if (internalAssetLinkList.LinkType == AssetInformation.LinkOneofCase.Url)
					{
						Uri externalUri = new Uri(internalAssetLinkList.Url);
						MessageContent = MessageContent.Replace(_linkHelper.CreateRootExternalLinkFromIndex(i), externalUri.ToString());
						_linkCache.Add(externalUri);
					}
					else if (internalAssetLinkList.LinkType == AssetInformation.LinkOneofCase.File)
					{
						string newFileName = _fileNameFormatter.GetFileNameWithProperExtension(messageUri);
						SourceListEditConvenienceEx.Add(item: new Attachment(internalAssetLinkList, newFileName, messageUri.Attachment.Length, messageUri.Attachment.Modified?.ToDateTime(), _linkHelper.GetFileTypeFromMimeType(messageUri.Attachment.MimeType)), source: _fileCache);
					}
					else
					{
						string newFileName2 = _fileNameFormatter.GetFileNameWithProperExtension(messageUri);
						SourceListEditConvenienceEx.Add(item: new Attachment(internalAssetLinkList, newFileName2, messageUri.Attachment.Length, messageUri.Attachment.Modified?.ToDateTime(), _linkHelper.GetFileTypeFromMimeType(messageUri.Attachment.MimeType)), source: _mediaCache);
					}
				}
				break;
			case LinkType.ExternalLink:
				MessageContent = MessageContent.Replace(_linkHelper.CreateRootExternalLinkFromIndex(i), uri.ToString());
				_linkCache.Add(uri);
				break;
			case LinkType.UserMention:
			{
				RootGuid? userId = _linkHelper.GetMentionId(LinkType.UserMention, uri);
				if (userId != null && userId == _rootSessionAccessor.Session.UserInfoService.SessionUser.Id)
				{
					HasSelfMention = true;
					string s2 = uri.ToString();
					MessageContent = MessageContent.Replace(s2, s2 + "/me");
				}
				break;
			}
			case LinkType.RoleMention:
			{
				RootGuid? roleId = _linkHelper.GetMentionId(LinkType.RoleMention, uri);
				if (roleId != null && await MessageContainer.GetMemberAsync(_rootSessionAccessor.Session.UserInfoService.SessionUser.Id).ConfigureAwait(continueOnCapturedContext: false) is Member { Roles: not null } member && member.Roles.IsInRole((CommunityRoleGuid)roleId.Value))
				{
					HasSelfMention = true;
					string s = uri.ToString();
					MessageContent = MessageContent.Replace(s, s + "/me");
				}
				break;
			}
			case LinkType.AllRoleMention:
			case LinkType.HereRoleMention:
				HasSelfMention = true;
				break;
			}
			internalAssetLinkList = null;
		}
	}

	private void processInitialReactions(IEnumerable<MessageReaction> P_0)
	{
		foreach (MessageReaction item in P_0)
		{
			addReaction(item.UserId, item.Shortcode);
		}
	}

	private void addReaction(UserGuid P_0, string P_1)
	{
		Reaction reaction = _reactions.Items.FirstOrDefault((Reaction x) => x.ShortCode == P_1);
		if (reaction == null)
		{
			reaction = _reactionFactory.Create(MessageId, P_1, MessageContainer);
			_reactions.Add(reaction);
			Reaction reaction2 = _reactions.Items.FirstOrDefault((Reaction x) => x.IsPlaceHolder);
			if (reaction2 != null)
			{
				_reactions.Remove(reaction2);
			}
			else
			{
				reaction2 = _reactionFactory.Create(MessageGuid.Empty, null, MessageContainer);
			}
			_reactions.Add(reaction2);
		}
		reaction.Increment(P_0);
	}

	private void removeReaction(UserGuid P_0, string P_1)
	{
		Reaction reaction = _reactions.Items.FirstOrDefault((Reaction x) => x.ShortCode == P_1);
		if (reaction == null)
		{
			return;
		}
		reaction.Decrement(P_0);
		if (reaction.Count <= 0)
		{
			_reactions.Remove(reaction);
		}
		if (_reactions.Count == 1)
		{
			Reaction reaction2 = _reactions.Items.FirstOrDefault((Reaction x) => x.IsPlaceHolder);
			if (reaction2 != null)
			{
				_reactions.Remove(reaction2);
			}
		}
	}

	private async Task getSenderMemberAsync()
	{
		SenderMember = await MessageContainer.GetMemberAsync(SenderUserId).ConfigureAwait(continueOnCapturedContext: false);
		if (SenderMember == null)
		{
			GlobalUser globalUser = await _globalUserCacheService.GetUserByIdAsync(SenderUserId).ConfigureAwait(false);
			if (globalUser != null)
			{
				SenderMember = new DirectMessageMember(globalUser);
			}
		}
		if (IsSystemMessage && SenderMember != null && SystemMessagePayload != null)
		{
			SystemMessageLog = new SystemMessageLog(SystemMessagePayload, SenderMember, MessageContainer.ContainerId, MessageId, MessageContainer.CommunityId);
		}
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool P_0)
	{
		if (!_disposed)
		{
			if (P_0)
			{
				_linkCache.Clear();
				_linkCache.Dispose();
				_fileCache.Clear();
				_fileCache.Dispose();
				_mediaCache.Clear();
				_mediaCache.Dispose();
				_reactions.Clear();
				_reactions.Dispose();
				_pendingFileCache.Clear();
				_pendingFileCache.Dispose();
				_pendingMediaCache.Clear();
				_pendingMediaCache.Dispose();
			}
			_disposed = true;
		}
	}
}
