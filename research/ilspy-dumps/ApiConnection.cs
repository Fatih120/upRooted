using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Web;
using Google.Protobuf.Collections;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using Polly.Contrib.WaitAndRetry;
using RootApp.Assets;
using RootApp.Core;
using RootApp.HubServer.Client;
using RootApp.HubServer.Client.Packets;
using RootApp.Utility;
using RootApp.Utility.Extensions;
using RootApp.Utility.Normalization;
using RootApp.WebApi.Shared.Blob;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;
using RootApp.WebApi.Shared.Grpc.Services;

namespace RootApp.WebApi.Client.Shared.Implementations;

public sealed class ApiConnection : IApiConnection, IAsyncDisposable
{
	[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
	[JsonSerializable(typeof(Uri))]
	[GeneratedCode("System.Text.Json.SourceGeneration", "10.0.14.7603")]
	private sealed class UrlSerializerContext : JsonSerializerContext, IJsonTypeInfoResolver
	{
		private JsonTypeInfo<Uri>? _Uri;

		private static readonly JsonSerializerOptions s_defaultOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

		private const BindingFlags InstanceMemberBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		public JsonTypeInfo<Uri> Uri => _Uri ?? (_Uri = (JsonTypeInfo<Uri>)base.Options.GetTypeInfo(typeof(Uri)));

		public static UrlSerializerContext Default { get; } = new UrlSerializerContext(new JsonSerializerOptions(s_defaultOptions));

		protected override JsonSerializerOptions? GeneratedSerializerOptions { get; } = s_defaultOptions;

		private JsonTypeInfo<Uri> Create_Uri(JsonSerializerOptions options)
		{
			if (!TryGetTypeInfoForRuntimeCustomConverter(options, out JsonTypeInfo<Uri> jsonTypeInfo))
			{
				jsonTypeInfo = JsonMetadataServices.CreateValueInfo<Uri>(options, JsonMetadataServices.UriConverter);
			}
			jsonTypeInfo.OriginatingResolver = this;
			return jsonTypeInfo;
		}

		public UrlSerializerContext()
			: base(null)
		{
		}

		public UrlSerializerContext(JsonSerializerOptions options)
			: base(options)
		{
		}

		private static bool TryGetTypeInfoForRuntimeCustomConverter<TJsonMetadataType>(JsonSerializerOptions options, out JsonTypeInfo<TJsonMetadataType> jsonTypeInfo)
		{
			JsonConverter runtimeConverterForType = GetRuntimeConverterForType(typeof(TJsonMetadataType), options);
			if (runtimeConverterForType != null)
			{
				jsonTypeInfo = JsonMetadataServices.CreateValueInfo<TJsonMetadataType>(options, runtimeConverterForType);
				return true;
			}
			jsonTypeInfo = null;
			return false;
		}

		private static JsonConverter? GetRuntimeConverterForType(Type type, JsonSerializerOptions options)
		{
			for (int i = 0; i < options.Converters.Count; i++)
			{
				JsonConverter jsonConverter = options.Converters[i];
				if (jsonConverter != null && jsonConverter.CanConvert(type))
				{
					return ExpandConverter(type, jsonConverter, options, validateCanConvert: false);
				}
			}
			return null;
		}

		private static JsonConverter ExpandConverter(Type type, JsonConverter converter, JsonSerializerOptions options, bool validateCanConvert = true)
		{
			if (validateCanConvert && !converter.CanConvert(type))
			{
				throw new InvalidOperationException($"The converter '{converter.GetType()}' is not compatible with the type '{type}'.");
			}
			if (converter is JsonConverterFactory jsonConverterFactory)
			{
				converter = jsonConverterFactory.CreateConverter(type, options);
				if (converter == null || converter is JsonConverterFactory)
				{
					throw new InvalidOperationException($"The converter '{jsonConverterFactory.GetType()}' cannot return null or a JsonConverterFactory instance.");
				}
			}
			return converter;
		}

		public override JsonTypeInfo? GetTypeInfo(Type type)
		{
			base.Options.TryGetTypeInfo(type, out JsonTypeInfo result);
			return result;
		}

		JsonTypeInfo? IJsonTypeInfoResolver.GetTypeInfo(Type type, JsonSerializerOptions options)
		{
			if (type == typeof(Uri))
			{
				return Create_Uri(options);
			}
			return null;
		}
	}

	private static readonly TimeSpan _httpClientTimeout = TimeSpan.FromHours(1);

	private readonly CancellationTokenSource _cts;

	private readonly GrpcChannel _grpcChannel;

	private readonly ILogger<ApiConnection> _logger;

	private readonly ILoggerFactory _loggerFactory;

	private bool _disposed;

	private static readonly string _fileDownloadUri = "";

	private static readonly TimeSpan _defaultTimeout = TimeSpan.FromSeconds(10L);

	private static readonly TimeSpan _maxRetryDelay = TimeSpan.FromSeconds(60L);

	private static readonly IEnumerable<TimeSpan> _retryDelays = Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(0.5), 15, null, fastFirst: true);

	private static readonly TimeSpan _resourceExhaustedDelay = TimeSpan.FromSeconds(10L);

	private readonly Channel<ClientHubPacket> _channel;

	private static readonly BoundedChannelOptions _packetChannelOptions = new BoundedChannelOptions(250)
	{
		AllowSynchronousContinuations = false,
		FullMode = BoundedChannelFullMode.Wait,
		SingleWriter = true,
		SingleReader = true
	};

	private readonly Task _packetReader;

	private HubConnection? _hubConnection;

	private long _sequenceNumber = -1L;

	private readonly ConcurrentBag<TaskCompletionSource> _connectionWaiters = new ConcurrentBag<TaskCompletionSource>();

	private static readonly TimeSpan _healthyThreshold = TimeSpan.FromMinutes(2L);

	private static readonly TimeSpan _offlineThreshold = TimeSpan.FromSeconds(15L);

	private const double _ticksPerByte = 800.0;

	private static readonly TimeSpan _maxUploadTimeout = TimeSpan.FromMinutes(30L);

	private static readonly TimeSpan _minUploadTimeout = TimeSpan.FromSeconds(30L);

	private static readonly Uri _uploadUrl = new Uri("asset/upload", UriKind.Relative);

	public IApiAuthorization ApiAuthorization { get; }

	public HttpClient HttpClient { get; }

	public ClientToken ClientToken { get; }

	public AssetGrpcService.AssetGrpcServiceClient Asset { get; }

	public AppStoreGrpcService.AppStoreGrpcServiceClient AppStore { get; }

	public UserGrpcService.UserGrpcServiceClient User { get; }

	public ChannelGrpcService.ChannelGrpcServiceClient Channel { get; }

	public AccessRuleGrpcService.AccessRuleGrpcServiceClient AccessRule { get; }

	public ChannelGroupGrpcService.ChannelGroupGrpcServiceClient ChannelGroup { get; }

	public MessageGrpcService.MessageGrpcServiceClient Message { get; }

	public CommunityGrpcService.CommunityGrpcServiceClient Community { get; }

	public CommunityAppGrpcService.CommunityAppGrpcServiceClient CommunityApp { get; }

	public CommunityAppLogGrpcService.CommunityAppLogGrpcServiceClient CommunityAppLog { get; }

	public CommunityMemberBanGrpcService.CommunityMemberBanGrpcServiceClient CommunityMemberBan { get; }

	public CommunityMemberInviteGrpcService.CommunityMemberInviteGrpcServiceClient CommunityMemberInvite { get; }

	public CommunityRoleGrpcService.CommunityRoleGrpcServiceClient CommunityRole { get; }

	public CommunityMemberGrpcService.CommunityMemberGrpcServiceClient CommunityMember { get; }

	public CommunityMemberRoleGrpcService.CommunityMemberRoleGrpcServiceClient CommunityMemberRole { get; }

	public DirectMessageGrpcService.DirectMessageGrpcServiceClient DirectMessage { get; }

	public DirectoryGrpcService.DirectoryGrpcServiceClient Directory { get; }

	public FileGrpcService.FileGrpcServiceClient File { get; }

	public FriendshipGrpcService.FriendshipGrpcServiceClient Friendship { get; }

	public FriendshipGroupGrpcService.FriendshipGroupGrpcServiceClient FriendshipGroup { get; }

	public FriendshipInviteGrpcService.FriendshipInviteGrpcServiceClient FriendshipInvite { get; }

	public NotificationGrpcService.NotificationGrpcServiceClient Notification { get; }

	public WebRtcGrpcService.WebRtcGrpcServiceClient WebRtc { get; }

	public LinkGrpcService.LinkGrpcServiceClient Link { get; }

	public CommunityLogGrpcService.CommunityLogGrpcServiceClient CommunityLog { get; }

	public SupportGrpcService.SupportGrpcServiceClient Support { get; }

	public Uri? HubServerUrl => _hubConnection?.Url;

	public bool IsHubConnected => _hubConnection?.IsConnectedToSocketServer ?? false;

	public ApiConnection(HttpClient httpClient, GrpcChannel grpcChannel, IApiAuthorization apiAuthorization, TokenResponse token, ILoggerFactory loggerFactory, CancellationToken cancellationToken)
	{
		httpClient.Timeout = _httpClientTimeout;
		ApiAuthorization = apiAuthorization;
		HttpClient = httpClient;
		_loggerFactory = loggerFactory;
		_logger = loggerFactory.CreateLogger<ApiConnection>();
		_cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
		_grpcChannel = grpcChannel;
		ClientToken = token.ClientToken;
		User = CreateGrpcClient<UserGrpcService.UserGrpcServiceClient>();
		AccessRule = CreateGrpcClient<AccessRuleGrpcService.AccessRuleGrpcServiceClient>();
		AppStore = CreateGrpcClient<AppStoreGrpcService.AppStoreGrpcServiceClient>();
		Asset = CreateGrpcClient<AssetGrpcService.AssetGrpcServiceClient>();
		Channel = CreateGrpcClient<ChannelGrpcService.ChannelGrpcServiceClient>();
		ChannelGroup = CreateGrpcClient<ChannelGroupGrpcService.ChannelGroupGrpcServiceClient>();
		Message = CreateGrpcClient<MessageGrpcService.MessageGrpcServiceClient>();
		Community = CreateGrpcClient<CommunityGrpcService.CommunityGrpcServiceClient>();
		CommunityApp = CreateGrpcClient<CommunityAppGrpcService.CommunityAppGrpcServiceClient>();
		CommunityMemberBan = CreateGrpcClient<CommunityMemberBanGrpcService.CommunityMemberBanGrpcServiceClient>();
		FriendshipGroup = CreateGrpcClient<FriendshipGroupGrpcService.FriendshipGroupGrpcServiceClient>();
		FriendshipInvite = CreateGrpcClient<FriendshipInviteGrpcService.FriendshipInviteGrpcServiceClient>();
		Support = CreateGrpcClient<SupportGrpcService.SupportGrpcServiceClient>();
		Notification = CreateGrpcClient<NotificationGrpcService.NotificationGrpcServiceClient>();
		CommunityAppLog = CreateGrpcClient<CommunityAppLogGrpcService.CommunityAppLogGrpcServiceClient>();
		CommunityLog = CreateGrpcClient<CommunityLogGrpcService.CommunityLogGrpcServiceClient>();
		CommunityMemberInvite = CreateGrpcClient<CommunityMemberInviteGrpcService.CommunityMemberInviteGrpcServiceClient>();
		CommunityRole = CreateGrpcClient<CommunityRoleGrpcService.CommunityRoleGrpcServiceClient>();
		CommunityMember = CreateGrpcClient<CommunityMemberGrpcService.CommunityMemberGrpcServiceClient>();
		CommunityMemberRole = CreateGrpcClient<CommunityMemberRoleGrpcService.CommunityMemberRoleGrpcServiceClient>();
		DirectMessage = CreateGrpcClient<DirectMessageGrpcService.DirectMessageGrpcServiceClient>();
		Directory = CreateGrpcClient<DirectoryGrpcService.DirectoryGrpcServiceClient>();
		File = CreateGrpcClient<FileGrpcService.FileGrpcServiceClient>();
		Friendship = CreateGrpcClient<FriendshipGrpcService.FriendshipGrpcServiceClient>();
		WebRtc = CreateGrpcClient<WebRtcGrpcService.WebRtcGrpcServiceClient>();
		Link = CreateGrpcClient<LinkGrpcService.LinkGrpcServiceClient>();
		_channel = CreateNotificationChannel();
		_packetReader = StartPacketReaderAsync(new Uri(token.HubServerInfo));
	}

	public async ValueTask DisposeAsync()
	{
		if (!_disposed)
		{
			_disposed = true;
			_channel.Writer.TryComplete();
			await _cts.CancelAsync().ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
			await _grpcChannel.ShutdownAsync().ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
			await _packetReader.ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
			_grpcChannel.Dispose();
			HttpClient.Dispose();
			_cts.Dispose();
		}
	}

	private T CreateGrpcClient<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicMethods)] T>() where T : ClientBase<T>
	{
		ThrowIfDisposed();
		LogCreateGrpcClient(typeof(T).Name);
		return ((T)Activator.CreateInstance(typeof(T), _grpcChannel)) ?? throw new InvalidOperationException("Unable to create instance of type " + typeof(T).Name);
	}

	private void ThrowIfDisposed()
	{
		ObjectDisposedException.ThrowIf(_disposed, this);
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "ApiConnection.CreateService({Name})")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogCreateGrpcClient(string name)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "ApiConnection.CreateService({Name})");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Name", name);
			_logger.Log(LogLevel.Debug, new EventId(1797223774, "LogCreateGrpcClient"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(29, 1, invariantCulture);
				handler.AppendLiteral("ApiConnection.CreateService(");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(")");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	public async Task<HttpResponseMessage> DownloadAssetAsync(Uri assetUri, string? eTag, DateTimeOffset? lastModified, CancellationToken cancellationToken)
	{
		ThrowIfDisposed();
		LogDownloadAsset(assetUri);
		if (!assetUri.IsRootScheme())
		{
			throw new ArgumentException($"Invalid asset URI \"{assetUri}\"", "assetUri");
		}
		Uri url;
		if (assetUri.IsRootAuthority("file"))
		{
			ReadOnlyMemory<char> path = assetUri.AbsolutePath.AsMemory();
			if (path.Length < 20 || path.Span[0] != '/')
			{
				throw new ArgumentException($"Invalid asset URI \"{assetUri}\"", "assetUri");
			}
			url = new Uri($"{_fileDownloadUri}{path}", UriKind.Relative);
			path = default(ReadOnlyMemory<char>);
		}
		else
		{
			AssetGetRequest assetGetRequest = new AssetGetRequest();
			string assetUriString = assetUri.ToString();
			assetGetRequest.Uris.Add(assetUriString);
			if (!(await Asset.GetAsync(assetGetRequest, null, DateTime.UtcNow.Add(_defaultTimeout), cancellationToken).ConfigureAwait(continueOnCapturedContext: false)).Assets.TryGetValue(assetUriString, out var assetLink))
			{
				throw new FileNotFoundException("Unable to find URLs");
			}
			if (assetLink == null)
			{
				throw new InvalidOperationException("The server did not return any links.");
			}
			switch (assetLink.LinkCase)
			{
			case AssetInformation.LinkOneofCase.Url:
			{
				string link2 = assetLink.Url;
				if (link2 == null)
				{
					throw new InvalidOperationException("The server did not return a download link.");
				}
				url = new Uri(link2);
				break;
			}
			case AssetInformation.LinkOneofCase.Image:
			{
				RepeatedField<AssetImageLink> links = assetLink.Image.AssetLinks;
				if (links == null || links.Count <= 0)
				{
					throw new InvalidOperationException("The server did not return any links.");
				}
				string urlString = links.FirstOrDefault((AssetImageLink l) => l.Url?.Contains("/small", StringComparison.OrdinalIgnoreCase) ?? false)?.Url ?? links[0].Url;
				url = new Uri(urlString);
				break;
			}
			case AssetInformation.LinkOneofCase.Video:
			{
				string link3 = assetLink.Video.DownloadUrl;
				if (link3 == null)
				{
					throw new InvalidOperationException("The server did not return a download link.");
				}
				url = new Uri(link3);
				break;
			}
			case AssetInformation.LinkOneofCase.File:
			{
				string link = assetLink.File.Url;
				if (link == null)
				{
					throw new InvalidOperationException("The server did not return a download link.");
				}
				url = new Uri(link);
				break;
			}
			default:
				throw new ArgumentOutOfRangeException("assetUri");
			}
		}
		using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
		request.Version = HttpClient.DefaultRequestVersion;
		request.VersionPolicy = HttpClient.DefaultVersionPolicy;
		if (eTag != null)
		{
			request.Headers.IfNoneMatch.ParseAdd(eTag);
		}
		else if (lastModified.HasValue)
		{
			request.Headers.IfModifiedSince = lastModified.Value;
		}
		return await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Downloading asset from {AssetUri}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogDownloadAsset(Uri assetUri)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Downloading asset from {AssetUri}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("AssetUri", assetUri);
			_logger.Log(LogLevel.Debug, new EventId(2113755517, "LogDownloadAsset"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(23, 1, invariantCulture);
				handler.AppendLiteral("Downloading asset from ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	public IAsyncEnumerable<ClientHubPacket> ReadPacketsAsync(CancellationToken cancellationToken)
	{
		ThrowIfDisposed();
		return _channel.Reader.ReadAllAsync(cancellationToken);
	}

	public async ValueTask WaitForHubConnectionAsync(CancellationToken cancellationToken)
	{
		ThrowIfDisposed();
		if (!IsHubConnected)
		{
			TaskCompletionSource tcs = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
			_connectionWaiters.Add(tcs);
			if (!IsHubConnected)
			{
				await tcs.Task.WaitAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			}
		}
	}

	private void WakeConnectionWaiters()
	{
		TaskCompletionSource taskCompletionSource;
		while (_connectionWaiters.TryTake(out taskCompletionSource))
		{
			taskCompletionSource.TrySetResult();
		}
	}

	private static Channel<ClientHubPacket> CreateNotificationChannel()
	{
		return System.Threading.Channels.Channel.CreateBounded<ClientHubPacket>(_packetChannelOptions);
	}

	private Task StartPacketReaderAsync(Uri hubUrl)
	{
		return Task.Run(() => PacketReaderAsync(hubUrl), _cts.Token);
	}

	public async Task SetHubOfflineAsync(CancellationToken cancellationToken)
	{
		ThrowIfDisposed();
		_sequenceNumber = -1L;
		await _channel.Writer.WriteAsync(new ClientHubPacket(new HubDisconnectedPacket(), null), cancellationToken);
	}

	private Uri BuildHubUrl(Uri hubUrl)
	{
		if (_sequenceNumber >= 0)
		{
			UriBuilder uriBuilder = new UriBuilder(hubUrl);
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(hubUrl.Query);
			nameValueCollection["sequence_number"] = (_sequenceNumber + 1).ToString(CultureInfo.InvariantCulture);
			uriBuilder.Query = nameValueCollection.ToString();
			return uriBuilder.Uri;
		}
		return hubUrl;
	}

	private async Task PacketReaderAsync(Uri hubUrl)
	{
		using (_logger.BeginScope("PacketReader"))
		{
			CancellationToken cancellationToken = _cts.Token;
			try
			{
				while (!cancellationToken.IsCancellationRequested)
				{
					Stopwatch clock = Stopwatch.StartNew();
					bool first = true;
					foreach (TimeSpan delay in _retryDelays.ClampForever(_maxRetryDelay))
					{
						if (delay > TimeSpan.Zero)
						{
							LogDelayFor(delay);
							await Task.Delay(delay, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						}
						if (first)
						{
							first = false;
						}
						else
						{
							hubUrl = await GetHubUrlAsync(hubUrl, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						}
						bool wasHealthy;
						try
						{
							wasHealthy = await ConnectAsync(hubUrl, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						}
						catch (OperationCanceledException)
						{
							if (!cancellationToken.IsCancellationRequested)
							{
								continue;
							}
							throw;
						}
						if (wasHealthy)
						{
							break;
						}
						if (clock.Elapsed > _offlineThreshold)
						{
							await SetHubOfflineAsync(cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						}
					}
				}
			}
			catch (OperationCanceledException)
			{
			}
			catch (Exception ex3)
			{
				Exception ex4 = ex3;
				LogPacketReaderFailed(ex4);
			}
			LogPacketReaderDone();
		}
	}

	private async Task<Uri> GetHubUrlAsync(Uri hubUrl, CancellationToken cancellationToken)
	{
		try
		{
			HubServerEndpointResponse response = await User.GetNewHubserverEndpointAsync(new UserGetNewHubserverRequest(), null, DateTime.UtcNow.Add(_defaultTimeout), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			if (Uri.TryCreate(response.HubServerInfo, UriKind.Absolute, out Uri url) && url.IsWellFormedOriginalString())
			{
				if (hubUrl != url)
				{
					await SetHubOfflineAsync(cancellationToken);
				}
				return url;
			}
			LogInvalidHubAddress(response.HubServerInfo);
		}
		catch (OperationCanceledException)
		{
			throw;
		}
		catch (RpcException ex2)
		{
			StatusCode statusCode = ex2.StatusCode;
			LogUnableToFetchHubAddressStatus(ex2, statusCode);
			switch (statusCode)
			{
			case StatusCode.ResourceExhausted:
			{
				TimeSpan exhaustedDelay = _resourceExhaustedDelay.AddRandomFraction();
				LogResourceExhaustedDelay(exhaustedDelay);
				await Task.Delay(exhaustedDelay, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
				break;
			}
			case StatusCode.Cancelled:
			case StatusCode.Unknown:
			case StatusCode.DeadlineExceeded:
			case StatusCode.Aborted:
			case StatusCode.Internal:
			case StatusCode.Unavailable:
			case StatusCode.DataLoss:
				break;
			default:
				LogUnableToFetchHubAddressGivingUp(ex2);
				await SetHubOfflineAsync(cancellationToken);
				return hubUrl;
			}
		}
		catch (Exception exception)
		{
			LogUnableToFetchHubAddress(exception);
			await SetHubOfflineAsync(cancellationToken);
		}
		return hubUrl;
	}

	private async Task<bool> ConnectAsync(Uri hubUrl, CancellationToken cancellationToken)
	{
		Uri hubFullUrl = BuildHubUrl(hubUrl);
		LogConnectingToHub(hubFullUrl);
		Stopwatch.StartNew();
		WebApiHubConnectionHandler hubHandler = new WebApiHubConnectionHandler(hubFullUrl, _channel.Writer, _loggerFactory, _sequenceNumber);
		bool gotData = false;
		try
		{
			await using HubConnection connection = new HubConnection(hubFullUrl, HttpClient, hubHandler, _loggerFactory, WakeConnectionWaiters, cancellationToken);
			_hubConnection = connection;
			gotData = await connection.WaitForShutdownAsync().ConfigureAwait(continueOnCapturedContext: false);
			_sequenceNumber = hubHandler.SequenceNumber;
		}
		catch (OperationCanceledException)
		{
			throw;
		}
		catch (Exception ex2)
		{
			Exception ex3 = ex2;
			LogConnectionFailed(ex3, hubFullUrl);
			_sequenceNumber = hubHandler.SequenceNumber;
		}
		finally
		{
			if (_sequenceNumber == -1)
			{
				await SetHubOfflineAsync(cancellationToken);
			}
		}
		_hubConnection = null;
		return gotData;
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Connecting to Hub \"{Url}\".")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogConnectingToHub(Uri url)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Connecting to Hub \"{Url}\".");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Url", url);
			_logger.Log(LogLevel.Information, new EventId(357190739, "LogConnectingToHub"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(21, 1, invariantCulture);
				handler.AppendLiteral("Connecting to Hub \"");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral("\".");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Connection to \"{Url}\" failed")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogConnectionFailed(Exception exception, Uri url)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Connection to \"{Url}\" failed");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Url", url);
			_logger.Log(LogLevel.Information, new EventId(1055688132, "LogConnectionFailed"), threadLocalState, exception, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(23, 1, invariantCulture);
				handler.AppendLiteral("Connection to \"");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral("\" failed");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Delay for {Delay}.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogDelayFor(TimeSpan delay)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Delay for {Delay}.");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Delay", delay);
			_logger.Log(LogLevel.Debug, new EventId(1463543515, "LogDelayFor"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(11, 1, invariantCulture);
				handler.AppendLiteral("Delay for ");
				handler.AppendFormatted<object>(value);
				handler.AppendLiteral(".");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Invalid Hub address \"{Url}\".")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogInvalidHubAddress(string url)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Invalid Hub address \"{Url}\".");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Url", url);
			_logger.Log(LogLevel.Information, new EventId(1274492687, "LogInvalidHubAddress"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(23, 1, invariantCulture);
				handler.AppendLiteral("Invalid Hub address \"");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral("\".");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Unable to fetch Hub address due to {StatusCode}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogUnableToFetchHubAddressStatus(Exception exception, StatusCode statusCode)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Unable to fetch Hub address due to {StatusCode}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("StatusCode", statusCode);
			_logger.Log(LogLevel.Information, new EventId(1128219822, "LogUnableToFetchHubAddressStatus"), threadLocalState, exception, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(35, 1, invariantCulture);
				handler.AppendLiteral("Unable to fetch Hub address due to ");
				handler.AppendFormatted<object>(value);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Resource exhausted delay {Delay}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogResourceExhaustedDelay(TimeSpan delay)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Resource exhausted delay {Delay}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Delay", delay);
			_logger.Log(LogLevel.Debug, new EventId(627541271, "LogResourceExhaustedDelay"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(25, 1, invariantCulture);
				handler.AppendLiteral("Resource exhausted delay ");
				handler.AppendFormatted<object>(value);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Unable to fetch Hub address, giving up.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogUnableToFetchHubAddressGivingUp(Exception exception)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Unable to fetch Hub address, giving up.");
			_logger.Log(LogLevel.Information, new EventId(2121798361, "LogUnableToFetchHubAddressGivingUp"), threadLocalState, exception, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Unable to fetch Hub address, giving up.");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Unable to fetch Hub address")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogUnableToFetchHubAddress(Exception exception)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Unable to fetch Hub address");
			_logger.Log(LogLevel.Information, new EventId(2081539312, "LogUnableToFetchHubAddress"), threadLocalState, exception, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Unable to fetch Hub address");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "PacketReader failed.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogPacketReaderFailed(Exception exception)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "PacketReader failed.");
			_logger.Log(LogLevel.Information, new EventId(498500223, "LogPacketReaderFailed"), threadLocalState, exception, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "PacketReader failed.");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "PacketReader done.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogPacketReaderDone()
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "PacketReader done.");
			_logger.Log(LogLevel.Information, new EventId(471826074, "LogPacketReaderDone"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "PacketReader done.");
			threadLocalState.Clear();
		}
	}

	public async Task<Uri> UploadFileAsync(FileInfo fileInfo, IProgress<int>? percentComplete, CancellationToken cancellationToken)
	{
		ThrowIfDisposed();
		Uri result;
		await using (FileStream file = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan))
		{
			BlobUploadInformation uploadInformation = await BlobUploadInformation.CreateAsync(file.Name, file, fileInfo.LastWriteTimeUtc, cancellationToken).ConfigureAwait(false);
			file.Position = 0L;
			using StreamContent content = new StreamContent((percentComplete == null) ? ((Stream)file) : ((Stream)new ReadOnlyProgressStream(file, percentComplete)));
			HttpContentHeaders headers = content.Headers;
			headers.ContentMD5 = uploadInformation.Md5Bytes.ToArray();
			headers.ContentLength = file.Length;
			headers.Add("x-amz-meta-root-content-sha256", Convert.ToBase64String(uploadInformation.Sha256Bytes.Span));
			AddFilename(headers, fileInfo.Name);
			headers.Add("x-amz-meta-root-file-modification", fileInfo.LastWriteTimeUtc.ToString("u", CultureInfo.InvariantCulture));
			TimeSpan timeout = ComputeTimeout(file.Length);
			using CancellationTokenSource timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			timeoutCts.CancelAfter(timeout);
			using HttpResponseMessage response = await HttpClient.PostAsync(_uploadUrl, content, timeoutCts.Token).ConfigureAwait(continueOnCapturedContext: false);
			response.EnsureSuccessStatusCode();
			Uri uploadUri;
			await using (Stream contentStream = await response.Content.ReadAsStreamAsync(timeoutCts.Token).ConfigureAwait(continueOnCapturedContext: false))
			{
				uploadUri = await JsonSerializer.DeserializeAsync(contentStream, UrlSerializerContext.Default.Uri, timeoutCts.Token).ConfigureAwait(false);
			}
			if ((object)uploadUri == null || !uploadUri.IsUploadToken())
			{
				throw new InvalidOperationException("Invalid response from upload.");
			}
			result = uploadUri;
		}
		return result;
	}

	private static void AddFilename(HttpContentHeaders headers, string name)
	{
		string text = FilenameUtility.NormalizeOrEmpty(name);
		if (Ascii.IsValid(text.AsSpan()))
		{
			headers.Add("x-amz-meta-root-file-name", text);
		}
		string text2 = text.ToUtf8Base64();
		headers.Add("x-amz-meta-root-file-name-base64", text2);
	}

	private static TimeSpan ComputeTimeout(long length)
	{
		double num = Math.Clamp((double)length * 800.0, _minUploadTimeout.Ticks, _maxUploadTimeout.Ticks);
		return TimeSpan.FromTicks(checked((long)num));
	}
}
