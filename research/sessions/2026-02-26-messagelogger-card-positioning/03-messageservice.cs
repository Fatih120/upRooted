using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using DynamicData;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Threading;
using RootApp.Assets;
using RootApp.Client.CoreDomain.Models.Messages;
using RootApp.Client.CoreDomain.Repositories;
using RootApp.Client.CoreDomain.Utils.Badges;
using RootApp.Client.CoreDomain.Utils.Extensions;
using RootApp.Client.CoreDomain.Utils.Links;
using RootApp.Client.CoreDomain.Utils.Messages;
using RootApp.Core;
using RootApp.Core.Enums;
using RootApp.Core.Identifiers;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Grpc;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.Client.CoreDomain.Services;

public class MessageService : ObservableObject, IMessageService, IDisposable
{
	private readonly IMessageRepository _messageRepository;

	private readonly MessageFactory _messageFactory;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly ILocalNotificationService _localNotificationService;

	private readonly IAppBadgeService _appBadgeService;

	private readonly LinkHelper _linkHelper;

	private readonly AssetLinkWrapperFactory _assetLinkWrapperFactory;

	private readonly IAssetRepository _assetRepository;

	private readonly IAnalyticsService _analyticsService;

	private readonly ILogger<MessageService> _logger;

	private IMessageContainer _messageContainer;

	private bool _disposed = false;

	private bool _isResettingCache;

	private readonly List<MessagePacket> _pendingCreatedDuringReset = new List<MessagePacket>();

	private readonly SourceList<Message> _messageCache = new SourceList<Message>();

	private readonly SourceList<Message> _pinnedMessagesCache = new SourceList<Message>();

	private readonly SourceList<UserGuid> _typingUsersCache = new SourceList<UserGuid>();

	private readonly object _sourceListGate = new object();

	private readonly IDisposable _typingUsersExpirationDisposable;

	private Message? _lastMessageWithNew;

	private int _oldMessagesNeededToDownload;

	private int _newMessagesNeededToDownload;

	[CompilerGenerated]
	private bool <HasFetchedOnce>k__BackingField;

	[CompilerGenerated]
	private bool <IsVisible>k__BackingField;

	private int _virtualizationWindowStartIndex;

	private int _virtualizationWindowEndIndex;

	private bool _virtualizationWindowLocked;

	private bool _virtualizationWindowInitialized;

	private readonly BehaviorSubject<IVirtualRequest> _virtualRequest = new BehaviorSubject<IVirtualRequest>(new VirtualRequest(0, 0));

	private static readonly TimeZoneInfo ViewerTz = TimeZoneInfo.Local;

	[CompilerGenerated]
	private int <NewMessagesCount>k__BackingField;

	[CompilerGenerated]
	private bool <InFocusMode>k__BackingField;

	[CompilerGenerated]
	private Message? <FocusedMessage>k__BackingField;

	private long OlderMessageEpochBound => (_messageCache.Count > 0) ? _messageCache.Items[0].MessageEpoch : DateTimeOffset.MaxValue.ToUnixTimeMilliseconds();

	private long NewerMessageEpochBound
	{
		get
		{
			long result;
			if (_messageCache.Count <= 0)
			{
				result = 0L;
			}
			else
			{
				IReadOnlyList<Message> items = _messageCache.Items;
				result = items[items.Count - 1].MessageEpoch;
			}
			return result;
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public int NewMessagesCount
	{
		get
		{
			return <NewMessagesCount>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<int>.Default.Equals(<NewMessagesCount>k__BackingField, num))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.NewMessagesCount);
				<NewMessagesCount>k__BackingField = num;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.NewMessagesCount);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool InFocusMode
	{
		get
		{
			return <InFocusMode>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<InFocusMode>k__BackingField, flag))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.InFocusMode);
				<InFocusMode>k__BackingField = flag;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.InFocusMode);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Message? FocusedMessage
	{
		get
		{
			return <FocusedMessage>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<Message>.Default.Equals(<FocusedMessage>k__BackingField, message))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.FocusedMessage);
				<FocusedMessage>k__BackingField = message;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.FocusedMessage);
			}
		}
	}

	public bool HasMoreOlderMessages => _oldMessagesNeededToDownload > 0 || !_virtualizationWindowContainsStart;

	public bool HasMoreNewerMessages => _newMessagesNeededToDownload > 0 || !_virtualizationWindowContainsEnd;

	public bool IsContainerDividerPresent => _messageCache.Items.FirstOrDefault()?.IsStartOfContainer ?? false;

	public bool HasFetchedOnce
	{
		[CompilerGenerated]
		get
		{
			return <HasFetchedOnce>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<HasFetchedOnce>k__BackingField = flag;
		}
	}

	public bool IsVisible
	{
		[CompilerGenerated]
		get
		{
			return <IsVisible>k__BackingField;
		}
		[CompilerGenerated]
		set
		{
			<IsVisible>k__BackingField = flag;
		}
	}

	public bool HasMessageWithNewIndicator => _lastMessageWithNew != null;

	private int _lastIndexOffset => (!IsContainerDividerPresent) ? 1 : 2;

	private int _lastIndex => (_messageCache.Count - _lastIndexOffset >= 0) ? (_messageCache.Count - _lastIndexOffset) : 0;

	private int _virtualizationWindowSize => _virtualizationWindowEndIndex - _virtualizationWindowStartIndex + _lastIndexOffset;

	private bool _virtualizationWindowContainsStart => _virtualizationWindowStartIndex == 0;

	private bool _virtualizationWindowContainsEnd => _virtualizationWindowEndIndex == _lastIndex || _virtualizationWindowEndIndex == 0;

	private static DateOnly ViewerDate(DateTimeOffset P_0)
	{
		return DateOnly.FromDateTime(TimeZoneInfo.ConvertTime(P_0.UtcDateTime, ViewerTz));
	}

	private static bool DifferentViewerDays(DateTimeOffset P_0, DateTimeOffset P_1)
	{
		return ViewerDate(P_0) != ViewerDate(P_1);
	}

	private static bool Within5Min(DateTimeOffset P_0, DateTimeOffset P_1)
	{
		return P_0 - P_1 < TimeSpan.FromMinutes(5L);
	}

	public MessageService(IMessageContainer P_0, IMessageRepository P_1, MessageFactory P_2, IRootSessionAccessor P_3, ILocalNotificationService P_4, IAppBadgeService P_5, ILoggerFactory P_6, LinkHelper P_7, AssetLinkWrapperFactory P_8, IAssetRepository P_9, IAnalyticsService P_10)
	{
		_messageContainer = P_0;
		_messageRepository = P_1;
		_messageFactory = P_2;
		_rootSessionAccessor = P_3;
		_localNotificationService = P_4;
		_appBadgeService = P_5;
		_linkHelper = P_7;
		_assetLinkWrapperFactory = P_8;
		_assetRepository = P_9;
		_analyticsService = P_10;
		_logger = P_6.CreateLogger<MessageService>();
		SourceList<UserGuid> typingUsersCache = _typingUsersCache;
		Func<UserGuid, TimeSpan?> timeSelector = (UserGuid _) => TimeSpan.FromSeconds(30L);
		IScheduler scheduler = Scheduler.Default;
		_typingUsersExpirationDisposable = typingUsersCache.ExpireAfter(timeSelector, null, scheduler).Subscribe();
	}

	public IObservable<IChangeSet<Message>> ConnectMessages()
	{
		return _messageCache.Connect().Synchronize(_sourceListGate);
	}

	public IObservable<IChangeSet<Message>> ConnectVirtualizedMessages()
	{
		return _messageCache.Connect().Synchronize(_sourceListGate).Virtualise(_virtualRequest);
	}

	public IObservable<IChangeSet<Message>> ConnectPinnedMessages()
	{
		IObservable<HashSet<UserGuid>> observable = _rootSessionAccessor.Session.UserBlockService.ConnectBlockedUsers().QueryWhenChanged((IQuery<UserGuid, UserGuid> cache) => cache.Keys.ToHashSet()).StartWith(new HashSet<UserGuid>());
		IObservable<Func<Message, bool>> predicate = observable.Select((Func<HashSet<UserGuid>, Func<Message, bool>>)((HashSet<UserGuid> blocked) => delegate(Message m)
		{
			UserGuid senderUserId = m.SenderUserId;
			return senderUserId == null || !blocked.Contains(senderUserId);
		}));
		return _pinnedMessagesCache.Connect().Synchronize(_sourceListGate).Filter(predicate);
	}

	public IObservable<IChangeSet<UserGuid>> ConnectTypingUsers()
	{
		IObservable<HashSet<UserGuid>> observable = _rootSessionAccessor.Session.UserBlockService.ConnectBlockedUsers().QueryWhenChanged((IQuery<UserGuid, UserGuid> cache) => cache.Keys.ToHashSet()).StartWith(new HashSet<UserGuid>());
		IObservable<Func<UserGuid, bool>> predicate = observable.Select((Func<HashSet<UserGuid>, Func<UserGuid, bool>>)((HashSet<UserGuid> blocked) => (UserGuid userId) => userId == null || !blocked.Contains(userId)));
		return _typingUsersCache.Connect().Synchronize(_sourceListGate).Filter(predicate);
	}

	public async Task ReinitializeAsync()
	{
		bool shouldBufferPackets = IsVisible;
		try
		{
			if (shouldBufferPackets)
			{
				_isResettingCache = true;
			}
			clearMessageCache();
			clearPinnedMessageCache();
			_lastMessageWithNew = null;
			if (shouldBufferPackets)
			{
				initializePinnedMessagesAsync().Forget();
				await fetchMessagesAsync(MessageDirectionTake.Both);
				InitializeVirtualizationWindow();
			}
			else
			{
				HasFetchedOnce = false;
			}
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogApiFailure(e, "Failed to reinitialize messages");
			throw;
		}
		finally
		{
			if (shouldBufferPackets)
			{
				_isResettingCache = false;
				flushPendingCreatedPackets();
			}
		}
	}

	public async Task CreateMessageAsync(string P_0, IEnumerable<string> P_1, IEnumerable<MessageGuid>? P_2, bool P_3, IReadOnlyList<PendingAttachment>? P_4 = null)
	{
		P_0 = await MentionSanitizer.SanitizeMentionsAsync(P_0, _messageContainer, _linkHelper);
		MessageCreateRequest request = new MessageCreateRequest();
		if (_messageContainer.CommunityId != null)
		{
			request.CommunityId = _messageContainer.CommunityId;
		}
		request.ContainerId = _messageContainer.ContainerId;
		request.Content = P_0.TrimEnd();
		request.AttachmentTokenUris.AddRange(P_1);
		request.Context = new RootContext
		{
			CommandId = CommandIdempotencyGuid.NewGuid()
		};
		if (P_2 != null)
		{
			request.ParentMessageIds.AddRange(P_2.Select((Func<MessageGuid, MessageUuid>)((MessageGuid m) => m)));
			request.NeedsParentMessageNotification = P_3;
		}
		if (!HasMoreNewerMessages)
		{
			Message previousMessage = _messageCache.Items.LastOrDefault();
			bool showUserProfile = !(previousMessage?.SenderUserId == _rootSessionAccessor.Session.UserInfoService.SessionUser.Id) || previousMessage == null || !Within5Min(DateTimeOffset.UtcNow, previousMessage.SentAtUtc) || (previousMessage?.IsSystemMessage ?? false) || (previousMessage?.DeletedAt.HasValue ?? false);
			bool showDateDivider = previousMessage != null && DifferentViewerDays(previousMessage.SentAtUtc, DateTimeOffset.UtcNow);
			Message placeholderMessage = _messageFactory.CreatePlaceHolder(request.Content, showUserProfile || request.ParentMessageIds.Count > 0, showDateDivider, _messageContainer, request, retryMessageCreateAsync, P_4);
			_messageCache.Add(placeholderMessage);
			if (_virtualizationWindowLocked)
			{
				slideVirtualizationWindowNewer(1);
			}
			else
			{
				expandVirtualizationWindowNewer(1);
			}
			try
			{
				placeholderMessage.ConvertPlaceholderToRealMessage(await _messageRepository.CreateMessageAsync(request));
				_messageContainer.SetLastSentMessage(placeholderMessage);
				SetViewTimeAsync(true).Forget();
				if (_messageContainer.CommunityId != null)
				{
					_analyticsService.IncrementMessagesSentInCommunities();
				}
				else
				{
					_analyticsService.IncrementMessagesSentInDms();
				}
			}
			catch (Exception ex)
			{
				if (request.AttachmentTokenUris.Count > 0)
				{
					try
					{
						if ((await _assetRepository.GetUploadTokenStatusAsync(new AssetUploadTokenStatusRequest
						{
							Tokens = { (IEnumerable<string>)request.AttachmentTokenUris.ToList() }
						})).Status.Values.Any((UploadTokenStatus s) => s == UploadTokenStatus.Pending))
						{
							retryMessageCreateAsync(request, placeholderMessage);
							return;
						}
					}
					catch
					{
					}
				}
				placeholderMessage.FailedToSend = true;
				_logger.LogWarning(ex, "Failed to create message. Entering failed state that supports retry");
			}
			return;
		}
		try
		{
			await _messageRepository.CreateMessageAsync(request);
			_messageContainer.SetLastSentMessage(_messageFactory.CreatePlaceHolder(request.Content, false, false, _messageContainer, null, null));
			NewMessagesCount++;
			if (_messageContainer.CommunityId != null)
			{
				_analyticsService.IncrementMessagesSentInCommunities();
			}
			else
			{
				_analyticsService.IncrementMessagesSentInDms();
			}
		}
		catch (Exception ex2)
		{
			_logger.LogApiFailure(ex2, "Failed to create message");
		}
	}

	private async Task retryMessageCreateAsync(MessageCreateRequest request, Message message)
	{
		try
		{
			message.ConvertPlaceholderToRealMessage(await _messageRepository.CreateMessageAsync(request));
			_messageContainer.SetLastSentMessage(message);
			SetViewTimeAsync(true).Forget();
		}
		catch (Exception ex)
		{
			Exception e = ex;
			message.FailedToSend = true;
			_logger.LogWarning(e, "Failed to create message. Entering failed state that supports retry");
		}
	}

	public async Task EditMessageAsync(MessageGuid P_0, string P_1)
	{
		try
		{
			P_1 = await MentionSanitizer.SanitizeMentionsAsync(P_1, _messageContainer, _linkHelper);
			MessageEditRequest request = new MessageEditRequest();
			if (_messageContainer.CommunityId != null)
			{
				request.CommunityId = _messageContainer.CommunityId;
			}
			request.ContainerId = _messageContainer.ContainerId;
			request.Id = P_0;
			request.Content = P_1;
			processMessageEditedPacket(await _messageRepository.EditMessageAsync(request));
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogApiFailure(e, "Failed to edit message");
			throw;
		}
	}

	public async Task CreateReactionAsync(MessageGuid P_0, string P_1)
	{
		try
		{
			MessageReactionCreateRequest request = new MessageReactionCreateRequest();
			if (_messageContainer.CommunityId != null)
			{
				request.CommunityId = _messageContainer.CommunityId;
			}
			request.ContainerId = _messageContainer.ContainerId;
			request.MessageId = P_0;
			request.Shortcode = P_1;
			MessageReactionCreateResponse response = await _messageRepository.CreateReactionAsync(request);
			processMessageReactionPacket(response.MessageId, response.UserId, response.Shortcode, true);
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogApiFailure(e, "Failed to create reaction");
			throw;
		}
	}

	public async Task DeleteReactionAsync(MessageGuid P_0, string P_1)
	{
		try
		{
			MessageReactionDeleteRequest request = new MessageReactionDeleteRequest();
			if (_messageContainer.CommunityId != null)
			{
				request.CommunityId = _messageContainer.CommunityId;
			}
			request.ContainerId = _messageContainer.ContainerId;
			request.MessageId = P_0;
			request.Shortcode = P_1;
			await _messageRepository.DeleteReactionAsync(request);
			processMessageReactionPacket(request.MessageId, _rootSessionAccessor.Session.UserInfoService.SessionUser.Id, request.Shortcode, false);
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogApiFailure(e, "Failed to delete reaction");
			throw;
		}
	}

	public async Task SetViewTimeAsync(bool P_0 = false)
	{
		try
		{
			IMessageContainer messageContainer = _messageContainer;
			int num;
			if (messageContainer != null && messageContainer.HasActivity)
			{
				IMessageService messages = messageContainer.Messages;
				if (messages != null)
				{
					num = ((!messages.HasMoreNewerMessages) ? 1 : 0);
					goto IL_0048;
				}
			}
			num = 0;
			goto IL_0048;
			IL_0048:
			if (((uint)num | (P_0 ? 1u : 0u)) != 0)
			{
				MessageSetViewTimeRequest request = ((!(_messageContainer.CommunityId != null)) ? new MessageSetViewTimeRequest
				{
					ContainerId = _messageContainer.ContainerId
				} : new MessageSetViewTimeRequest
				{
					CommunityId = _messageContainer.CommunityId,
					ContainerId = _messageContainer.ContainerId
				});
				_newMessagesNeededToDownload = 0;
				_messageContainer.SetViewTime(DateTime.UtcNow.AddMilliseconds(3000.0));
				await _messageRepository.SetViewTimeAsync(request);
			}
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogApiFailure(e, "Failed to update view time for container");
		}
	}

	public async Task SetTypingIndicatorAsync(bool P_0)
	{
		try
		{
			MessageSetTypingIndicatorRequest request = new MessageSetTypingIndicatorRequest
			{
				CommunityId = _messageContainer.CommunityId,
				ContainerId = _messageContainer.ContainerId,
				IsTyping = P_0
			};
			await _messageRepository.SetTypingIndicatorAsync(request);
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogApiFailure(e, "Failed to set typing indicator for container");
		}
	}

	public async Task MarkContainerAsReadAsync()
	{
		try
		{
			_isResettingCache = true;
			await SetViewTimeAsync(true);
			clearMessageCache();
			await fetchMessagesAsync(MessageDirectionTake.Both);
			InitializeVirtualizationWindow();
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogApiFailure(e, "Failed to update mark container as read");
			throw;
		}
		finally
		{
			_isResettingCache = false;
			flushPendingCreatedPackets();
		}
	}

	public async Task PinMessageAsync(MessageGuid P_0)
	{
		try
		{
			MessagePinCreateRequest request = new MessagePinCreateRequest();
			if (_messageContainer.CommunityId != null)
			{
				request.CommunityId = _messageContainer.CommunityId;
			}
			request.ContainerId = _messageContainer.ContainerId;
			request.MessageId = P_0;
			await _messageRepository.PinMessageAsync(request);
			processPinCreated(request.MessageId);
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogApiFailure(e, "Failed to pin message");
			throw;
		}
	}

	public async Task UnpinMessageAsync(MessageGuid P_0)
	{
		try
		{
			MessagePinDeleteRequest request = new MessagePinDeleteRequest();
			if (_messageContainer.CommunityId != null)
			{
				request.CommunityId = _messageContainer.CommunityId;
			}
			request.ContainerId = _messageContainer.ContainerId;
			request.MessageId = P_0;
			await _messageRepository.UnpinMessageAsync(request);
			processPinDeleted(request.MessageId);
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogApiFailure(e, "Failed to unpin message");
			throw;
		}
	}

	public async Task FocusMessageAsync(MessageGuid P_0)
	{
		try
		{
			Message message = getMessageById(P_0);
			if (message == null)
			{
				clearMessageCache();
				await fetchMessagesAsync(MessageDirectionTake.Both, P_0);
				message = getMessageById(P_0);
				if (message != null)
				{
					InitializeVirtualizationWindow(message);
				}
			}
			else if (!virtualizationWindowContainsMessage(message))
			{
				ResetVirtualizationWindow();
				InitializeVirtualizationWindow(message);
			}
			FocusedMessage = message;
			setFocusMode();
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogApiFailure(e, "Failed to focus message");
		}
	}

	public async Task DeleteMessageAsync(MessageGuid P_0)
	{
		Message message = getMessageById(P_0);
		if (message?.IsPlaceholder ?? false)
		{
			_messageCache.Remove(message);
			message.Dispose();
			return;
		}
		try
		{
			MessageDeleteRequest request = new MessageDeleteRequest();
			if (_messageContainer.CommunityId != null)
			{
				request.CommunityId = _messageContainer.CommunityId;
			}
			request.ContainerId = _messageContainer.ContainerId;
			request.Id = P_0;
			await _messageRepository.DeleteMessageAsync(request);
			processMessageDeleted(P_0);
		}
		catch (Exception ex)
		{
			_logger.LogApiFailure(ex, "Failed to delete message");
			throw;
		}
	}

	public Task FetchMessagesAsync(MessageDirectionTake P_0)
	{
		switch (P_0)
		{
		case MessageDirectionTake.Older:
			if (expandVirtualizationWindowOlder(20))
			{
				return Task.CompletedTask;
			}
			break;
		case MessageDirectionTake.Newer:
			if (expandVirtualizationWindowNewer(20))
			{
				return Task.CompletedTask;
			}
			break;
		}
		if (HasFetchedOnce && P_0 == MessageDirectionTake.Both)
		{
			return Task.CompletedTask;
		}
		return fetchMessagesAsync(P_0);
	}

	private async Task fetchMessagesAsync(MessageDirectionTake P_0, MessageGuid? P_1 = null)
	{
		if (!HasFetchedOnce)
		{
			clearMessageCache();
			HasFetchedOnce = true;
			initializePinnedMessagesAsync().Forget();
		}
		MessageListRequest request = new MessageListRequest();
		if (_messageContainer.CommunityId != null)
		{
			request.CommunityId = _messageContainer.CommunityId;
		}
		request.ContainerId = _messageContainer.ContainerId;
		request.MessageDirectionTake = P_0;
		switch (P_0)
		{
		case MessageDirectionTake.Older:
			if (!HasMoreOlderMessages)
			{
				return;
			}
			request.DateAt = DateTimeOffset.FromUnixTimeMilliseconds(OlderMessageEpochBound).UtcDateTime.ToTimestamp();
			break;
		case MessageDirectionTake.Newer:
			if (!HasMoreNewerMessages)
			{
				return;
			}
			request.DateAt = DateTimeOffset.FromUnixTimeMilliseconds(NewerMessageEpochBound).UtcDateTime.ToTimestamp();
			break;
		case MessageDirectionTake.Both:
		{
			long epoch = NewerMessageEpochBound;
			if (_messageContainer.UserLastViewedAt.HasValue && P_1 == null)
			{
				epoch = ((DateTimeOffset)_messageContainer.UserLastViewedAt.Value).ToUnixTimeMilliseconds();
			}
			else if (P_1.HasValue)
			{
				epoch = P_1.Value.ToDateTimeOffset().ToUnixTimeMilliseconds();
			}
			request.DateAt = DateTimeOffset.FromUnixTimeMilliseconds(epoch).UtcDateTime.ToTimestamp();
			break;
		}
		}
		try
		{
			MessageContainerResponse response = await _messageRepository.GetMessagesAsync(request);
			if (response.Messages.Count > 0)
			{
				processMessageResponse(response, P_0);
				return;
			}
			MessageDirectionTake messageDirectionTake = P_0;
			if ((messageDirectionTake == MessageDirectionTake.Newer || messageDirectionTake == MessageDirectionTake.Both) ? true : false)
			{
				_newMessagesNeededToDownload = response.NewCount;
			}
			messageDirectionTake = P_0;
			if ((uint)(messageDirectionTake - 2) <= 1u)
			{
				_oldMessagesNeededToDownload = response.OldCount;
			}
			if (_messageCache.Count == 0)
			{
				createContainerStartDivider();
			}
		}
		catch (Exception ex)
		{
			_logger.LogApiFailure(ex, "Failed to fetch messages");
		}
	}

	private async Task initializePinnedMessagesAsync()
	{
		try
		{
			MessagePinListRequest request = new MessagePinListRequest();
			if (_messageContainer.CommunityId != null)
			{
				request.CommunityId = _messageContainer.CommunityId;
			}
			request.ContainerId = _messageContainer.ContainerId;
			List<Message> pinnedMessages = ConvertToMessages(await _messageRepository.GetPinnedMessagesAsync(request), _messageFactory, _messageContainer, _assetLinkWrapperFactory);
			if (_pinnedMessagesCache.Items.Count == 0)
			{
				_pinnedMessagesCache.AddRange(pinnedMessages);
				return;
			}
			SourceListEditConvenienceEx.InsertRange(items: pinnedMessages.ExceptBy(_pinnedMessagesCache.Items.Select((Message p) => p.MessageId), (Message m) => m.MessageId), source: _pinnedMessagesCache, index: 0);
		}
		catch (Exception ex)
		{
			Exception e = ex;
			_logger.LogApiFailure(e, "Failed to initialize pinned messages");
		}
	}

	public static List<Message> ConvertToMessages(MessageContainerResponse P_0, MessageFactory P_1, IMessageContainer P_2, AssetLinkWrapperFactory P_3)
	{
		List<Message> list = new List<Message>();
		if (P_0.Messages != null)
		{
			RepeatedField<MessagePacket> messages = P_0.Messages;
			Dictionary<Uri, AssetLinkWrapper> assetLinks = P_0.ReferenceMaps?.Assets?.ToDictionary((KeyValuePair<string, AssetInformation> pair) => new Uri(pair.Key), (KeyValuePair<string, AssetInformation> pair) => P_3.Create(pair.Key, pair.Value));
			list.AddRange(messages.Select((MessagePacket msg) => P_1.Create(msg, assetLinks, true, false, P_2)));
		}
		return list;
	}

	private Message? getMessageById(MessageGuid P_0)
	{
		return _messageCache.Items.FirstOrDefault((Message m) => m.MessageId == P_0);
	}

	private Message? getMessageAfterId(MessageGuid P_0)
	{
		return _messageCache.Items.FirstOrDefault((Message m) => m.MessageId > P_0);
	}

	private Message? getPinnedMessageById(MessageGuid P_0)
	{
		return _pinnedMessagesCache.Items.FirstOrDefault((Message m) => m.MessageId == P_0);
	}

	private IEnumerable<MessageReply> getMessageRepliesContainingMessage(MessageGuid P_0)
	{
		return _messageCache.Items.SelectMany((Message m) => m.GetReplyMessages(P_0));
	}

	private IEnumerable<MessageReply> getPinnedMessageRepliesContainingMessage(MessageGuid P_0)
	{
		return _pinnedMessagesCache.Items.SelectMany((Message m) => m.GetReplyMessages(P_0));
	}

	private bool ContainsMessageId(MessageGuid P_0)
	{
		return _messageCache.Items.Any((Message m) => m.MessageId == P_0);
	}

	private bool TryAddUnique(Message P_0)
	{
		if (!ContainsMessageId(P_0.MessageId))
		{
			_messageCache.Add(P_0);
			return true;
		}
		P_0.Dispose();
		return false;
	}

	private bool TryInsertUniqueAtStart(Message P_0)
	{
		if (!ContainsMessageId(P_0.MessageId))
		{
			_messageCache.Insert(0, P_0);
			return true;
		}
		P_0.Dispose();
		return false;
	}

	private void AddRangeDistinct(IEnumerable<Message> P_0)
	{
		List<Message> list = new List<Message>();
		foreach (Message item in P_0)
		{
			if (!ContainsMessageId(item.MessageId))
			{
				list.Add(item);
			}
			else
			{
				item.Dispose();
			}
		}
		if (list.Count > 0)
		{
			_messageCache.AddRange(list);
		}
	}

	private void processMessageResponse(MessageContainerResponse P_0, MessageDirectionTake P_1)
	{
		List<Message> list = new List<Message>();
		if (P_0.Messages == null)
		{
			return;
		}
		List<MessagePacket> list2 = P_0.Messages.OrderBy((MessagePacket m) => ((MessageGuid)m.Id).ToDateTime()).ToList();
		Dictionary<Uri, AssetLinkWrapper> dictionary = P_0.ReferenceMaps.Assets.ToDictionary((KeyValuePair<string, AssetInformation> pair) => new Uri(pair.Key), (KeyValuePair<string, AssetInformation> pair) => _assetLinkWrapperFactory.Create(pair.Key, pair.Value));
		for (int num = 0; num < list2.Count; num++)
		{
			MessagePacket messagePacket = list2[num];
			DateTime dateTime = ((MessageGuid)messagePacket.Id).ToDateTime();
			bool flag = true;
			if (num > 0)
			{
				UserGuid value = list2[num - 1].UserId;
				UserGuid value2 = messagePacket.UserId;
				DateTime dateTime2 = ((MessageGuid)list2[num - 1].Id).ToDateTime();
				if (value == value2 && Within5Min(dateTime, dateTime2))
				{
					flag = false;
				}
				if (list2[num - 1].MessageType == MessageType.System)
				{
					flag = true;
				}
				if (list2[num - 1].DeletedAt != null)
				{
					flag = true;
				}
			}
			bool flag2 = false;
			if (num > 0)
			{
				DateTime dateTime3 = ((MessageGuid)list2[num - 1].Id).ToDateTime();
				flag2 = DifferentViewerDays(dateTime3, dateTime);
			}
			Message message = _messageFactory.Create(messagePacket, dictionary, flag, flag2, _messageContainer);
			if (_messageContainer.HasActivity && _lastMessageWithNew == null)
			{
				DateTimeOffset dateTimeOffset = ((num > 0) ? ((DateTimeOffset)((MessageGuid)list2[num - 1].Id).ToDateTime()) : DateTimeOffset.MinValue);
				if (_messageContainer.UserLastViewedAt < dateTime)
				{
					DateTimeOffset? dateTimeOffset2 = _messageContainer.UserLastViewedAt;
					DateTimeOffset dateTimeOffset3 = dateTimeOffset;
					if (dateTimeOffset2.HasValue && dateTimeOffset2.GetValueOrDefault() >= dateTimeOffset3 && !message.HasBeenDeleted)
					{
						message.SetNewDividerStatus(true);
						_lastMessageWithNew = message;
					}
				}
			}
			list.Add(message);
		}
		switch (P_1)
		{
		case MessageDirectionTake.Older:
			_oldMessagesNeededToDownload = P_0.OldCount;
			if (_messageCache.Count > 0 && list.Count > 0)
			{
				Message message4 = _messageCache.Items.First();
				Message message5 = list.Last();
				if (message4.SenderUserId == message5.SenderUserId && Within5Min(message4.SentAtUtc, message5.SentAtUtc))
				{
					message4.SetShowUserProfile(false);
				}
				if (message5.MessageType == MessageType.System)
				{
					message4.SetShowUserProfile(true);
				}
				if (message5.DeletedAt.HasValue)
				{
					message4.SetShowUserProfile(true);
				}
				if (DifferentViewerDays(message4.SentAtUtc, message5.SentAtUtc))
				{
					message4.ShowDateDivider = true;
				}
			}
			if (list.Count > 0)
			{
				for (int num2 = list.Count - 1; num2 >= 0; num2--)
				{
					Message message6 = list[num2];
					if (TryInsertUniqueAtStart(message6))
					{
						slideVirtualizationWindowNewer(1);
					}
				}
			}
			if (_oldMessagesNeededToDownload <= 0)
			{
				createContainerStartDivider();
			}
			break;
		case MessageDirectionTake.Newer:
			_newMessagesNeededToDownload = P_0.NewCount;
			if (_messageCache.Count > 0 && list.Count > 0)
			{
				Message message2 = _messageCache.Items.Last();
				Message message3 = list[0];
				if (message2.SenderUserId == message3.SenderUserId && Within5Min(message3.SentAtUtc, message2.SentAtUtc))
				{
					message3.SetShowUserProfile(false);
				}
				if (message2.MessageType == MessageType.System)
				{
					message3.SetShowUserProfile(true);
				}
				if (message2.DeletedAt.HasValue)
				{
					message3.SetShowUserProfile(true);
				}
				if (DifferentViewerDays(message2.SentAtUtc, message3.SentAtUtc))
				{
					message3.ShowDateDivider = true;
				}
			}
			if (list.Count > 0)
			{
				AddRangeDistinct(list);
				expandVirtualizationWindowNewer(20);
			}
			break;
		case MessageDirectionTake.Both:
			_oldMessagesNeededToDownload = P_0.OldCount;
			_newMessagesNeededToDownload = P_0.NewCount;
			if (list.Count > 0)
			{
				AddRangeDistinct(list);
			}
			if (_oldMessagesNeededToDownload <= 0)
			{
				createContainerStartDivider();
			}
			break;
		}
	}

	private Message processMessageCreatedPacket(MessagePacket P_0)
	{
		Message message = _messageCache.Items.LastOrDefault();
		DateTime dateTime = ((MessageGuid)P_0.Id).ToDateTime();
		bool flag = true;
		if (message != null)
		{
			if (message.SenderUserId == P_0.UserId && Within5Min(dateTime, message.SentAtUtc))
			{
				flag = false;
			}
			if (message.MessageType == MessageType.System)
			{
				flag = true;
			}
			if (message.DeletedAt.HasValue)
			{
				flag = true;
			}
		}
		bool flag2 = message != null && DifferentViewerDays(message.SentAtUtc, dateTime);
		Dictionary<Uri, AssetLinkWrapper> dictionary = P_0.ReferenceMaps?.Assets.ToDictionary((KeyValuePair<string, AssetInformation> pair) => new Uri(pair.Key), (KeyValuePair<string, AssetInformation> pair) => _assetLinkWrapperFactory.Create(pair.Key, pair.Value));
		Message message2 = _messageFactory.Create(P_0, dictionary, flag, flag2, _messageContainer);
		if (_messageContainer.HasActivity && _lastMessageWithNew == null)
		{
			DateTimeOffset value = message?.SentAtUtc ?? DateTimeOffset.MinValue;
			DateTime? userLastViewedAt = _messageContainer.UserLastViewedAt;
			DateTime dateTime2 = dateTime;
			if (userLastViewedAt.HasValue && userLastViewedAt.GetValueOrDefault() < dateTime2 && _messageContainer.UserLastViewedAt >= value)
			{
				message2.SetNewDividerStatus(true);
				_lastMessageWithNew = message2;
			}
		}
		return message2;
	}

	private void processMessageEditedPacket(MessagePacket P_0)
	{
		getMessageById(P_0.Id)?.UpdateMessage(P_0);
		IEnumerable<MessageReply> messageRepliesContainingMessage = getMessageRepliesContainingMessage(P_0.Id);
		foreach (MessageReply item in messageRepliesContainingMessage)
		{
			item.UpdateMessageContent(P_0.MessageContent);
		}
		IEnumerable<MessageReply> pinnedMessageRepliesContainingMessage = getPinnedMessageRepliesContainingMessage(P_0.Id);
		foreach (MessageReply item2 in pinnedMessageRepliesContainingMessage)
		{
			item2.UpdateMessageContent(P_0.MessageContent);
		}
		getPinnedMessageById(P_0.Id)?.UpdateMessage(P_0);
	}

	private void processMessageDeleted(MessageGuid P_0)
	{
		Message messageById = getMessageById(P_0);
		if (messageById != null)
		{
			messageById.SetAsDeleted();
			if (messageById.ShowUserProfile)
			{
				Message messageAfterId = getMessageAfterId(P_0);
				if (messageAfterId != null && messageAfterId.SenderUserId == messageById.SenderUserId && !messageAfterId.ShowUserProfile)
				{
					messageAfterId.SetShowUserProfile(true);
				}
			}
			if (_lastMessageWithNew != null && _lastMessageWithNew.MessageId == P_0)
			{
				_lastMessageWithNew.SetNewDividerStatus(false);
				_lastMessageWithNew = null;
			}
		}
		IEnumerable<MessageReply> messageRepliesContainingMessage = getMessageRepliesContainingMessage(P_0);
		foreach (MessageReply item in messageRepliesContainingMessage)
		{
			item.UpdateMessageContent(null);
		}
		IEnumerable<MessageReply> pinnedMessageRepliesContainingMessage = getPinnedMessageRepliesContainingMessage(P_0);
		foreach (MessageReply item2 in pinnedMessageRepliesContainingMessage)
		{
			item2.UpdateMessageContent(null);
		}
		Message pinnedMessageById = getPinnedMessageById(P_0);
		if (pinnedMessageById != null)
		{
			_pinnedMessagesCache.Remove(pinnedMessageById);
			pinnedMessageById.Dispose();
		}
	}

	private void processMessageReactionPacket(MessageGuid P_0, UserGuid P_1, string P_2, bool P_3)
	{
		getMessageById(P_0)?.ProcessReaction(P_1, P_2, P_3);
		getPinnedMessageById(P_0)?.ProcessReaction(P_1, P_2, P_3);
	}

	private void processMessageSetTypingIndicator(UserGuid P_0, bool P_1)
	{
		UserGuid userGuid = _typingUsersCache.Items.FirstOrDefault((UserGuid id) => P_0 == id);
		if (userGuid != null)
		{
			_typingUsersCache.Remove(userGuid);
		}
		if (P_1)
		{
			_typingUsersCache.Add(P_0);
		}
	}

	private void processPinCreated(MessageGuid P_0)
	{
		Message messageById = getMessageById(P_0);
		if (messageById != null)
		{
			messageById.PinnedAt = DateTimeOffset.UtcNow;
		}
		initializePinnedMessagesAsync().Forget();
	}

	private void processPinDeleted(MessageGuid P_0)
	{
		Message messageById = getMessageById(P_0);
		if (messageById != null)
		{
			messageById.PinnedAt = null;
		}
		Message pinnedMessageById = getPinnedMessageById(P_0);
		if (pinnedMessageById != null)
		{
			_pinnedMessagesCache.Remove(pinnedMessageById);
			pinnedMessageById.Dispose();
		}
	}

	private void createContainerStartDivider()
	{
		Message item = _messageFactory.CreateContainerStartDivider(_messageContainer);
		_messageCache.Insert(0, item);
	}

	private void setFocusMode()
	{
		InFocusMode = !_virtualizationWindowContainsEnd;
		if (!InFocusMode)
		{
			FocusedMessage = null;
		}
	}

	private bool expandVirtualizationWindowOlder(int P_0, bool P_1 = false)
	{
		if (_virtualizationWindowStartIndex == 0 && !P_1)
		{
			return false;
		}
		if (_virtualizationWindowStartIndex - P_0 < 0 && !P_1)
		{
			P_0 = _virtualizationWindowStartIndex;
			_virtualizationWindowStartIndex -= P_0;
			updateVirtualizationRequest();
			return false;
		}
		_virtualizationWindowStartIndex -= P_0;
		updateVirtualizationRequest();
		return true;
	}

	private bool expandVirtualizationWindowNewer(int P_0, bool P_1 = false)
	{
		if (_virtualizationWindowEndIndex >= _lastIndex && !P_1)
		{
			return false;
		}
		if (_virtualizationWindowEndIndex + P_0 > _lastIndex && !P_1)
		{
			P_0 = _lastIndex - _virtualizationWindowEndIndex;
			_virtualizationWindowEndIndex += P_0;
			updateVirtualizationRequest();
			return false;
		}
		_virtualizationWindowEndIndex += P_0;
		updateVirtualizationRequest();
		return true;
	}

	private bool slideVirtualizationWindowNewer(int P_0, bool P_1 = false)
	{
		if (_virtualizationWindowEndIndex >= _lastIndex && !P_1)
		{
			return false;
		}
		if (_virtualizationWindowEndIndex + P_0 > _lastIndex && !P_1)
		{
			P_0 = _lastIndex - _virtualizationWindowEndIndex;
			_virtualizationWindowStartIndex += P_0;
			_virtualizationWindowEndIndex += P_0;
			updateVirtualizationRequest();
			return false;
		}
		_virtualizationWindowStartIndex += P_0;
		_virtualizationWindowEndIndex += P_0;
		updateVirtualizationRequest();
		return true;
	}

	public bool IncreaseVirtualizationWindow()
	{
		int virtualizationWindowStartIndex = _virtualizationWindowStartIndex;
		int virtualizationWindowEndIndex = _virtualizationWindowEndIndex;
		int num = (_virtualizationWindowContainsEnd ? 20 : 10);
		_virtualizationWindowStartIndex = ((_virtualizationWindowStartIndex - num >= 0) ? (_virtualizationWindowStartIndex - num) : 0);
		_virtualizationWindowEndIndex = ((_virtualizationWindowEndIndex + num > _lastIndex) ? _lastIndex : (_virtualizationWindowEndIndex + num));
		if (virtualizationWindowStartIndex == _virtualizationWindowStartIndex && virtualizationWindowEndIndex == _virtualizationWindowEndIndex)
		{
			return false;
		}
		updateVirtualizationRequest();
		return true;
	}

	public void InitializeVirtualizationWindow(Message? P_0 = null)
	{
		_virtualizationWindowInitialized = true;
		if (_virtualizationWindowEndIndex >= _lastIndex)
		{
			updateVirtualizationRequest();
		}
		else
		{
			expandVirtualizationWindowNewer(20);
		}
		if (P_0 != null)
		{
			int num = _messageCache.Items.IndexOf<Message>(P_0);
			if (num >= 0)
			{
				int num2 = num - 10;
				if (num2 > 0)
				{
					slideVirtualizationWindowNewer(num2);
				}
			}
		}
		else if (_messageContainer.HasActivity && _lastMessageWithNew != null)
		{
			int num3 = _messageCache.Items.IndexOf<Message>(_lastMessageWithNew);
			int num4 = num3 - 10;
			if (num4 > 0)
			{
				slideVirtualizationWindowNewer(num4);
			}
			NewMessagesCount = _messageCache.Count - num3 + _newMessagesNeededToDownload;
		}
		else
		{
			int num5 = _messageCache.Count - 20;
			if (num5 > 0)
			{
				slideVirtualizationWindowNewer(num5);
			}
		}
	}

	private void trimOldestMessagesFromCache()
	{
		int num = 150;
		int num2 = _messageCache.Count - num;
		if (num2 <= 0)
		{
			return;
		}
		List<Message> list = _messageCache.Items.Take(num2).ToList();
		_messageCache.RemoveMany(list);
		foreach (Message item in list)
		{
			item.Dispose();
		}
	}

	public void ResetVirtualizationWindow()
	{
		_virtualizationWindowInitialized = false;
		if (!_disposed)
		{
			trimOldestMessagesFromCache();
			_virtualizationWindowStartIndex = 0;
			_virtualizationWindowEndIndex = 0;
			_virtualRequest.OnNext(new VirtualRequest(0, 1));
			InFocusMode = false;
			FocusedMessage = null;
			if (!_messageContainer.HasActivity && _lastMessageWithNew != null)
			{
				_lastMessageWithNew.SetNewDividerStatus(false);
				_lastMessageWithNew = null;
			}
		}
	}

	public void ShrinkVirtualizationWindow()
	{
		if (!HasMoreNewerMessages)
		{
			trimOldestMessagesFromCache();
			_virtualizationWindowStartIndex = _lastIndex;
			_virtualizationWindowEndIndex = _lastIndex;
			expandVirtualizationWindowOlder(20);
		}
		setFocusMode();
	}

	public void SetVirtualizationLockMode(bool P_0)
	{
		if (!P_0 || _virtualizationWindowSize >= 20)
		{
			_virtualizationWindowLocked = P_0;
		}
	}

	private void updateVirtualizationRequest()
	{
		if (!_virtualRequest.IsDisposed)
		{
			_virtualRequest.OnNext(new VirtualRequest(_virtualizationWindowStartIndex, _virtualizationWindowSize));
		}
	}

	private bool virtualizationWindowContainsMessage(Message P_0)
	{
		int num = _messageCache.Items.IndexOf(P_0);
		return _virtualizationWindowStartIndex <= num && _virtualizationWindowEndIndex >= num;
	}

	public void OnMessageCreatedPacketReceived(MessagePacket P_0)
	{
		if (_messageContainer.CommunityId == null && P_0.UserId != (UserUuid)_rootSessionAccessor.Session.UserInfoService.SessionUser.Id)
		{
			_localNotificationService.ShowNotification(P_0);
			_appBadgeService.SetAttention(true);
		}
		if (!HasFetchedOnce)
		{
			return;
		}
		if (_isResettingCache)
		{
			_pendingCreatedDuringReset.Add(P_0);
			return;
		}
		if (!HasMoreNewerMessages)
		{
			Message message = processMessageCreatedPacket(P_0);
			if (message != null && TryAddUnique(message) && _virtualizationWindowInitialized)
			{
				if (_virtualizationWindowLocked)
				{
					slideVirtualizationWindowNewer(1);
				}
				else
				{
					expandVirtualizationWindowNewer(1);
				}
			}
		}
		else
		{
			NewMessagesCount++;
		}
		processMessageSetTypingIndicator(P_0.UserId, false);
	}

	public void OnMessageDeletedPacketReceived(MessageDeletedPacket P_0)
	{
		processMessageDeleted(P_0.Id);
	}

	public void OnMessageEditedPacketReceived(MessagePacket P_0)
	{
		if (HasFetchedOnce)
		{
			processMessageEditedPacket(P_0);
		}
	}

	public void OnMessageReactionCreatedPacketReceived(MessageReactionPacket P_0)
	{
		if (HasFetchedOnce)
		{
			processMessageReactionPacket(P_0.MessageId, P_0.UserId, P_0.Shortcode, true);
		}
	}

	public void OnMessageReactionDeletedPacketReceived(MessageReactionPacket P_0)
	{
		if (HasFetchedOnce)
		{
			processMessageReactionPacket(P_0.MessageId, P_0.UserId, P_0.Shortcode, false);
		}
	}

	public void OnMessageSetTypingIndicatorPacketPacketReceived(MessageSetTypingIndicatorPacket P_0)
	{
		processMessageSetTypingIndicator(P_0.UserId, P_0.IsTyping);
	}

	public void OnMessagePinCreatedPacketReceived(MessagePinPacket P_0)
	{
		if (HasFetchedOnce)
		{
			processPinCreated(P_0.MessageId);
		}
	}

	public void OnMessagePinDeletedPacketReceived(MessagePinPacket P_0)
	{
		if (HasFetchedOnce)
		{
			processPinDeleted(P_0.MessageId);
		}
	}

	private void flushPendingCreatedPackets()
	{
		if (_pendingCreatedDuringReset.Count == 0)
		{
			return;
		}
		List<MessagePacket> list = _pendingCreatedDuringReset.OrderBy((MessagePacket p) => ((MessageGuid)p.Id).ToDateTime()).ToList();
		_pendingCreatedDuringReset.Clear();
		foreach (MessagePacket item in list)
		{
			OnMessageCreatedPacketReceived(item);
		}
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	protected virtual void Dispose(bool P_0)
	{
		if (_disposed)
		{
			return;
		}
		if (P_0)
		{
			foreach (Message item in _messageCache.Items)
			{
				item.Dispose();
			}
			foreach (Message item2 in _pinnedMessagesCache.Items)
			{
				item2.Dispose();
			}
			_virtualRequest.Dispose();
			_messageCache.Clear();
			_messageCache.Dispose();
			_pinnedMessagesCache.Clear();
			_pinnedMessagesCache.Dispose();
			_typingUsersCache.Dispose();
			_typingUsersExpirationDisposable.Dispose();
		}
		_disposed = true;
	}

	private void clearMessageCache()
	{
		ResetVirtualizationWindow();
		foreach (Message item in _messageCache.Items)
		{
			item.Dispose();
		}
		_messageCache.Clear();
		_newMessagesNeededToDownload = 0;
		_oldMessagesNeededToDownload = 0;
		NewMessagesCount = 0;
	}

	private void clearPinnedMessageCache()
	{
		foreach (Message item in _pinnedMessagesCache.Items)
		{
			item.Dispose();
		}
		_pinnedMessagesCache.Clear();
	}
}
