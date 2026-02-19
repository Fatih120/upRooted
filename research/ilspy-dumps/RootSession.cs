// RootApp.Client.CoreDomain, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.CoreDomain.RootSession
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.User;
using RootApp.Client.CoreDomain.Services;
using RootApp.Core.Identifiers;
using RootApp.HubServer.Client.Packets;
using RootApp.WebApi.Client.Shared;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Helpers;
using RootApp.WebApi.Shared.Packets;

public class RootSession : IAsyncDisposable
{
	private readonly IConnectionStatusService _connectionStatusService;

	private readonly IAnalyticsService _analyticsService;

	private readonly CancellationTokenSource _cts;

	private readonly IGlobalUserCacheService _globalUserCacheService;

	private readonly ILogger<RootSession> _logger;

	private readonly Task _reader;

	private readonly IRootService _rootService;

	private readonly SemaphoreSlim _reconnectGate = new SemaphoreSlim(1, 1);

	private int _reconnectEpoch;

	[CompilerGenerated]
	private IFileUploadService _003CFileUploadService_003Ek__BackingField;

	public IUserInfoService UserInfoService { get; }

	public IUserBlockService UserBlockService { get; }

	public IFriendService FriendService { get; }

	public ITabService TabService { get; }

	public ICommunityService CommunityService { get; }

	public IFileUploadService FileUploadService
	{
		[CompilerGenerated]
		get
		{
			return _003CFileUploadService_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CFileUploadService_003Ek__BackingField = fileUploadService;
		}
	}

	public INotificationService NotificationService { get; }

	public IDirectMessageService DirectMessageService { get; }

	public IActiveMediaRoomService ActiveMediaRoomService { get; }

	public ISupportService SupportService { get; }

	public AllNotificationCountService NotificationCountService { get; }

	public IAssetService AssetService { get; }

	public ILinkService LinkService { get; }

	public UserGuid UserId => UserInfoService.SessionUser.Id;

	public DeviceGuid DeviceId => _rootService.Connection?.ClientToken.DeviceId ?? DeviceGuid.Empty;

	public RootSession(SessionUser sessionUser, IRootService rootService, IHostApplicationLifetime applicationLifetime, ILoggerFactory loggerFactory, UserInfoServiceFactory userInfoServiceFactory, UserBlockServiceFactory userBlockServiceFactory, FriendServiceFactory friendServiceFactory, TabServiceFactory tabServiceFactory, CommunityServiceFactory communityServiceFactory, IGlobalUserCacheService globalUserCacheService, FileUploadServiceFactory fileUploadServiceFactory, NotificationServiceFactory notificationServiceFactory, DirectMessageServiceFactory directMessageServiceFactory, IActiveMediaRoomService activeMediaRoomService, IConnectionStatusService connectionStatusService, SupportServiceFactory supportServiceFactory, AllNotificationCountServiceFactory allNotificationCountServiceFactory, AssetServiceFactory assetServiceFactory, LinkServiceFactory linkServiceFactory, IAnalyticsService analyticsService)
	{
		_logger = loggerFactory.CreateLogger<RootSession>();
		_cts = CancellationTokenSource.CreateLinkedTokenSource(applicationLifetime.ApplicationStopping);
		_rootService = rootService;
		_globalUserCacheService = globalUserCacheService;
		_connectionStatusService = connectionStatusService;
		_analyticsService = analyticsService;
		_globalUserCacheService.AddSessionUser(sessionUser);
		_reader = Task.Factory.StartNew((Func<Task>)packetReaderAsync, _cts.Token, TaskCreationOptions.DenyChildAttach, TaskScheduler.FromCurrentSynchronizationContext()).Unwrap();
		_connectionStatusService.Reconnected += onReconnected;
		UserInfoService = userInfoServiceFactory.Create(sessionUser);
		FriendService = friendServiceFactory.Create();
		UserBlockService = userBlockServiceFactory.Create(sessionUser, FriendService);
		CommunityService = communityServiceFactory.Create();
		FileUploadService = fileUploadServiceFactory.Create();
		NotificationService = notificationServiceFactory.Create();
		DirectMessageService = directMessageServiceFactory.Create();
		ActiveMediaRoomService = activeMediaRoomService;
		TabService = tabServiceFactory.Create();
		SupportService = supportServiceFactory.Create();
		NotificationCountService = allNotificationCountServiceFactory.Create(DirectMessageService, NotificationService);
		AssetService = assetServiceFactory.Create();
		LinkService = linkServiceFactory.Create();
		_globalUserCacheService.SetUserBlockService(UserBlockService);
	}

	public async ValueTask DisposeAsync()
	{
		await _cts.CancelAsync().ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
		await _reader.ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
		_connectionStatusService.Reconnected -= onReconnected;
		_cts.Dispose();
		_reconnectGate.Dispose();
	}

	public Task InitializeSessionAsync()
	{
		return initializeStartupServicesAsync();
	}

	private async Task packetReaderAsync()
	{
		CancellationToken cancellationToken = _cts.Token;
		try
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				try
				{
					IApiConnection connection;
					while (true)
					{
						connection = _rootService.Connection;
						if (connection != null)
						{
							break;
						}
						await Task.Delay(500, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
					}
					await connection.WaitForHubConnectionAsync(cancellationToken).ConfigureAwait(false);
					_logger.LogTrace("Starting package reader for connection");
					await foreach (ClientHubPacket hubPacket in connection.ReadPacketsAsync(cancellationToken).ConfigureAwait(true))
					{
						_logger.LogTrace("Received packet: {PacketType}", hubPacket.Packet.PacketType);
						handlePacket(hubPacket);
					}
				}
				catch (OperationCanceledException)
				{
					break;
				}
				catch (Exception ex2)
				{
					LogErrorPacketReader(ex2);
					break;
				}
			}
		}
		finally
		{
			LogInfoPacketReaderCompleted();
		}
	}

	private void handlePacket(ClientHubPacket P_0)
	{
		IPacket packet = P_0.Packet;
		if (_logger.IsEnabled(LogLevel.Trace) && packet is IMessage message)
		{
			LogTracePacket(message.ToString() ?? "<invalid>");
		}
		IPacket packet2 = packet;
		IPacket packet3 = packet2;
		if (packet3 is InternalPacket)
		{
			if (!(packet3 is HubConnectedPacket hubConnectedPacket))
			{
				if (packet3 is HubDisconnectedPacket)
				{
					_connectionStatusService.SetHubServerConnectionStatus(isConnected: false);
					LogInfoDisconnectedFromHub();
				}
			}
			else
			{
				_connectionStatusService.SetHubServerConnectionStatus(isConnected: true);
				LogInfoConnectedToHub(hubConnectedPacket.HubUrl.ToString());
			}
			return;
		}
		LogInfoPacketType(packet.GetType().Name, packet.PacketType);
		try
		{
			if (packet.PacketType.IsAssetPacket())
			{
				AssetService.HandlePacket(packet);
			}
			else if (packet.PacketType.IsCommunityPacket() || packet.PacketType.IsExternalCommunityPacket())
			{
				CommunityService.HandlePacket(packet);
			}
			else if (packet.PacketType.IsDirectMessagePacket() || packet.PacketType.IsDirectMessageWebRtcPacket())
			{
				DirectMessageService.HandlePacket(packet);
			}
			else if (packet.PacketType.IsNotificationPacket())
			{
				NotificationService.HandlePacket(packet);
			}
			else if (packet.PacketType.IsUserStatusPacket())
			{
				PacketType packetType = packet.PacketType;
				if ((uint)(packetType - 707) <= 1u)
				{
					UserInfoService.HandlePacket(packet);
				}
				else
				{
					_globalUserCacheService.HandlePacket(packet);
				}
			}
			else if (packet.PacketType.IsUserBlockPacket())
			{
				UserBlockService.HandlePacket(packet);
			}
			else if (packet.PacketType.IsFriendshipGroupPacket() || packet.PacketType.IsFriendshipPacket())
			{
				FriendService.HandlePacket(packet);
			}
		}
		catch (Exception ex)
		{
			LogErrorPacketHandler(ex);
		}
	}

	private void onReconnected(object? sender, EventArgs e)
	{
		int num = Interlocked.Increment(ref _reconnectEpoch);
		initializeReconnectServicesAsync(num);
	}

	private async Task initializeStartupServicesAsync()
	{
		try
		{
			global::_003C_003Ey__InlineArray6<Task> _003C_003Ey__InlineArray281 = default(global::_003C_003Ey__InlineArray6<Task>);
			_003C_003Ey__InlineArray281[0] = _globalUserCacheService.InitializeAsync();
			_003C_003Ey__InlineArray281[1] = UserBlockService.InitializeAsync();
			_003C_003Ey__InlineArray281[2] = FriendService.InitializeAsync();
			_003C_003Ey__InlineArray281[3] = CommunityService.InitializeLightCommunitiesAsync();
			_003C_003Ey__InlineArray281[4] = NotificationService.InitializeAsync();
			_003C_003Ey__InlineArray281[5] = DirectMessageService.InitializeAsync();
			await Task.WhenAll(_003C_003Ey__InlineArray281).ConfigureAwait(continueOnCapturedContext: false);
			await UserInfoService.SetDeviceOnlineStatusAsync(UserDeviceOnlineStatus.Active).ConfigureAwait(continueOnCapturedContext: false);
			_analyticsService.TryCaptureActiveUserAsync().ConfigureAwait(continueOnCapturedContext: false);
			TabService.Initialize();
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			LogErrorInitializeStartupServices(ex2);
		}
	}

	private async Task initializeReconnectServicesAsync(int P_0)
	{
		CancellationToken cancellationToken = _cts.Token;
		await _reconnectGate.WaitAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
		try
		{
			if (P_0 == Volatile.Read(in _reconnectEpoch))
			{
				await warmupGrpcAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				global::_003C_003Ey__InlineArray4<Task> _003C_003Ey__InlineArray281 = default(global::_003C_003Ey__InlineArray4<Task>);
				_003C_003Ey__InlineArray281[0] = FriendService.InitializeAsync();
				_003C_003Ey__InlineArray281[1] = CommunityService.InitializeLightCommunitiesAsync();
				_003C_003Ey__InlineArray281[2] = NotificationService.InitializeAsync();
				_003C_003Ey__InlineArray281[3] = DirectMessageService.InitializeAsync();
				await Task.WhenAll(_003C_003Ey__InlineArray281).ConfigureAwait(continueOnCapturedContext: false);
				await CommunityService.InitializeFullCommunitiesAsync().ConfigureAwait(continueOnCapturedContext: false);
				global::_003C_003Ey__InlineArray3<Task> _003C_003Ey__InlineArray282 = default(global::_003C_003Ey__InlineArray3<Task>);
				_003C_003Ey__InlineArray282[0] = _globalUserCacheService.InitializeAsync();
				_003C_003Ey__InlineArray282[1] = UserBlockService.InitializeAsync();
				_003C_003Ey__InlineArray282[2] = UserInfoService.SetDeviceOnlineStatusAsync(UserDeviceOnlineStatus.Active, cancellationToken);
				await Task.WhenAll(_003C_003Ey__InlineArray282).ConfigureAwait(continueOnCapturedContext: false);
				_analyticsService.TryCaptureActiveUserAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
		}
		catch (Exception ex)
		{
			Exception ex2 = ex;
			LogErrorInitializeReconnectServices(ex2);
		}
		finally
		{
			_reconnectGate.Release();
		}
	}

	private async Task warmupGrpcAsync(CancellationToken P_0)
	{
		IApiConnection connection = null;
		for (int i = 0; i < 20; i++)
		{
			P_0.ThrowIfCancellationRequested();
			connection = _rootService.Connection;
			if (connection != null)
			{
				break;
			}
			await Task.Delay(100, P_0).ConfigureAwait(continueOnCapturedContext: false);
		}
		if (connection == null)
		{
			throw new InvalidOperationException("Reconnect warmup failed: no API connection available.");
		}
		DateTime deadline = DateTime.UtcNow.AddSeconds(5.0);
		try
		{
			await connection.User.GetSelfAsync(new UserGetSelfRequest(), null, deadline, P_0).ConfigureAwait(continueOnCapturedContext: false);
		}
		catch (RpcException ex) when (isTransportishGrpcFailure(ex))
		{
			throw;
		}
	}

	private static bool isTransportishGrpcFailure(RpcException P_0)
	{
		StatusCode statusCode = P_0.StatusCode;
		if ((statusCode == StatusCode.DeadlineExceeded || statusCode == StatusCode.Unavailable) ? true : false)
		{
			return true;
		}
		for (Exception innerException = P_0.InnerException; innerException != null; innerException = innerException.InnerException)
		{
			if (innerException is HttpRequestException)
			{
				return true;
			}
		}
		return false;
	}

	[LoggerMessage(Level = LogLevel.Trace, Message = "Packet {Packet}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogTracePacket(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Trace))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Packet {Packet}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Packet", P_0);
			_logger.Log(LogLevel.Trace, new EventId(554468614, "LogTracePacket"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(7, 1, invariantCulture);
				handler.AppendLiteral("Packet ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Connected to hub {URL}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogInfoConnectedToHub(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Connected to hub {URL}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("URL", P_0);
			_logger.Log(LogLevel.Information, new EventId(1828536236, "LogInfoConnectedToHub"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(17, 1, invariantCulture);
				handler.AppendLiteral("Connected to hub ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Disconnected from hub")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogInfoDisconnectedFromHub()
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Disconnected from hub");
			_logger.Log(LogLevel.Information, new EventId(354329727, "LogInfoDisconnectedFromHub"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Disconnected from hub");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Packet of type {Type}/{PacketType}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogInfoPacketType(string P_0, PacketType P_1)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "Packet of type {Type}/{PacketType}");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Type", P_0);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("PacketType", P_1);
			_logger.Log(LogLevel.Information, new EventId(840748349, "LogInfoPacketType"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[1].Value ?? "(null)";
				object value = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(16, 2, invariantCulture);
				handler.AppendLiteral("Packet of type ");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral("/");
				handler.AppendFormatted<object>(value);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Error, Message = "Error in packet handler")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogErrorPacketHandler(Exception P_0)
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(1);
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Error in packet handler");
		_logger.Log(LogLevel.Error, new EventId(578873479, "LogErrorPacketHandler"), threadLocalState, P_0, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Error in packet handler");
		threadLocalState.Clear();
	}

	[LoggerMessage(Level = LogLevel.Error, Message = "Error in packet reader")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogErrorPacketReader(Exception P_0)
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(1);
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Error in packet reader");
		_logger.Log(LogLevel.Error, new EventId(2122530854, "LogErrorPacketReader"), threadLocalState, P_0, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Error in packet reader");
		threadLocalState.Clear();
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Packet reader completed.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogInfoPacketReaderCompleted()
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Packet reader completed.");
			_logger.Log(LogLevel.Information, new EventId(684217595, "LogInfoPacketReaderCompleted"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Packet reader completed.");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Error, Message = "Failed to initialize startup services")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogErrorInitializeStartupServices(Exception P_0)
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(1);
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Failed to initialize startup services");
		_logger.Log(LogLevel.Error, new EventId(1339909652, "LogErrorInitializeStartupServices"), threadLocalState, P_0, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Failed to initialize startup services");
		threadLocalState.Clear();
	}

	[LoggerMessage(Level = LogLevel.Warning, Message = "Failed to initialize reconnect services")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogErrorInitializeReconnectServices(Exception P_0)
	{
		if (_logger.IsEnabled(LogLevel.Warning))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Failed to initialize reconnect services");
			_logger.Log(LogLevel.Warning, new EventId(1431162168, "LogErrorInitializeReconnectServices"), threadLocalState, P_0, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Failed to initialize reconnect services");
			threadLocalState.Clear();
		}
	}
}

