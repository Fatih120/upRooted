using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using Polly.Contrib.WaitAndRetry;
using RootApp.App.Messaging.Grpc;
using RootApp.Utility.Extensions;

namespace RootApp.AppHub.Client.Implementations;

public sealed class AppConnection : IAsyncDisposable
{
	private readonly CancellationTokenSource _cts;

	private readonly HttpClient _httpClient;

	private readonly ILogger<AppConnection> _logger;

	private readonly ILoggerFactory _loggerFactory;

	private readonly Action _onConnectedToAppHubServer;

	private readonly Action _onDisconnectedFromAppHubServer;

	private bool _disposed;

	private static readonly TimeSpan _maxRetryDelay = TimeSpan.FromSeconds(60L);

	private static readonly IEnumerable<TimeSpan> _retryDelays = Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(0.5), 15, null, fastFirst: true);

	private readonly Channel<AppRpcMessageToClient> _channel;

	private readonly BoundedChannelOptions _packetChannelOptions = new BoundedChannelOptions(250)
	{
		AllowSynchronousContinuations = false,
		FullMode = BoundedChannelFullMode.Wait,
		SingleWriter = true,
		SingleReader = true
	};

	private readonly Task _packetReader;

	private AppHubConnection? _hubConnection;

	private long _sequenceNumber = -1L;

	private readonly ConcurrentBag<TaskCompletionSource> _connectionWaiters = new ConcurrentBag<TaskCompletionSource>();

	private static readonly TimeSpan _offlineThreshold = TimeSpan.FromSeconds(15L);

	public Uri? HubServerUrl => _hubConnection?.Url;

	public AppConnection(HttpClient P_0, string P_1, Action P_2, Action P_3, ILoggerFactory P_4, CancellationToken P_5)
	{
		_httpClient = P_0;
		_onConnectedToAppHubServer = P_2;
		_onDisconnectedFromAppHubServer = P_3;
		_loggerFactory = P_4;
		_logger = P_4.CreateLogger<AppConnection>();
		_cts = CancellationTokenSource.CreateLinkedTokenSource(P_5);
		_channel = CreateNotificationChannel();
		_packetReader = StartPacketReaderAsync(new Uri(P_1));
	}

	public async ValueTask DisposeAsync()
	{
		if (!_disposed)
		{
			_disposed = true;
			await _cts.CancelAsync().ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
			await _packetReader.ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
			_cts.Dispose();
		}
	}

	public IAsyncEnumerable<AppRpcMessageToClient> ReadMessagesAsync(CancellationToken P_0)
	{
		return _channel.Reader.ReadAllAsync(P_0);
	}

	private void WakeConnectionWaiters()
	{
		TaskCompletionSource taskCompletionSource;
		while (_connectionWaiters.TryTake(out taskCompletionSource))
		{
			taskCompletionSource.TrySetResult();
		}
	}

	private Channel<AppRpcMessageToClient> CreateNotificationChannel()
	{
		return Channel.CreateBounded<AppRpcMessageToClient>(_packetChannelOptions);
	}

	private Task StartPacketReaderAsync(Uri P_0)
	{
		return Task.Run(() => PacketReaderAsync(P_0), _cts.Token);
	}

	public async ValueTask SendMessageAsync(AppRpcMessageToHost P_0)
	{
		if (_hubConnection == null)
		{
			return;
		}
		try
		{
			await _hubConnection.SendMessageAsync(P_0, CancellationToken.None).ConfigureAwait(false);
		}
		catch (ChannelClosedException)
		{
		}
		catch (OperationCanceledException)
		{
		}
	}

	private async Task PacketReaderAsync(Uri P_0)
	{
		ILogger<AppConnection> logger = _loggerFactory.CreateLogger<AppConnection>();
		using (_logger.BeginScope("PacketReader"))
		{
			CancellationToken cancellationToken = _cts.Token;
			try
			{
				while (!cancellationToken.IsCancellationRequested)
				{
					Stopwatch clock = Stopwatch.StartNew();
					foreach (TimeSpan delay in _retryDelays.ClampForever(_maxRetryDelay))
					{
						if (delay > TimeSpan.Zero)
						{
							LogDelayFor(logger, delay);
							await Task.Delay(delay, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
						}
						bool wasHealthy;
						try
						{
							wasHealthy = await ConnectAsync(P_0, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
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
			LogPacketReaderDone(logger);
		}
	}

	private async Task<bool> ConnectAsync(Uri P_0, CancellationToken P_1)
	{
		ILogger<AppConnection> logger = _loggerFactory.CreateLogger<AppConnection>();
		Uri hubFullUrl = BuildHubUrl(P_0);
		LogConnectingToAppHub(logger, P_0);
		Stopwatch.StartNew();
		AppHubConnectionHandler hubHandler = new AppHubConnectionHandler(_channel.Writer, _onConnectedToAppHubServer, _onDisconnectedFromAppHubServer, _loggerFactory, _sequenceNumber);
		bool gotData = false;
		try
		{
			await using AppHubConnection connection = new AppHubConnection(hubFullUrl, _httpClient, hubHandler, _loggerFactory, WakeConnectionWaiters, P_1);
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
			LogConnectionToUrlFailed(_logger, ex3, P_0);
			_sequenceNumber = hubHandler.SequenceNumber;
		}
		finally
		{
			if (_sequenceNumber == -1)
			{
				await SetHubOfflineAsync(P_1);
			}
		}
		_hubConnection = null;
		return gotData;
	}

	public async Task SetHubOfflineAsync(CancellationToken P_0)
	{
		_sequenceNumber = -1L;
		_onDisconnectedFromAppHubServer?.Invoke();
	}

	private Uri BuildHubUrl(Uri P_0)
	{
		if (_sequenceNumber >= 0)
		{
			UriBuilder uriBuilder = new UriBuilder(P_0);
			NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(P_0.Query);
			nameValueCollection["sequence_number"] = (_sequenceNumber + 1).ToString(CultureInfo.InvariantCulture);
			uriBuilder.Query = nameValueCollection.ToString();
			return uriBuilder.Uri;
		}
		return P_0;
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Connecting to App Hub \"{Url}\".")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private static void LogConnectingToAppHub(ILogger P_0, Uri P_1)
	{
		if (P_0.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Connecting to App Hub \"{Url}\".");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Url", P_1);
			P_0.Log(LogLevel.Information, new EventId(1852670812, "LogConnectingToAppHub"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(25, 1, invariantCulture);
				handler.AppendLiteral("Connecting to App Hub \"");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral("\".");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Error, Message = "Connection to \"{Url}\" failed")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private static void LogConnectionToUrlFailed(ILogger P_0, Exception P_1, Uri P_2)
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(2);
		threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Connection to \"{Url}\" failed");
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Url", P_2);
		P_0.Log(LogLevel.Error, new EventId(1760356188, "LogConnectionToUrlFailed"), threadLocalState, P_1, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
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

	[LoggerMessage(Level = LogLevel.Debug, Message = "Delay for {Delay}.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private static void LogDelayFor(ILogger P_0, TimeSpan P_1)
	{
		if (P_0.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Delay for {Delay}.");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Delay", P_1);
			P_0.Log(LogLevel.Debug, new EventId(1463543515, "LogDelayFor"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
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

	[LoggerMessage(Level = LogLevel.Information, Message = "PacketReader done.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private static void LogPacketReaderDone(ILogger P_0)
	{
		if (P_0.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "PacketReader done.");
			P_0.Log(LogLevel.Information, new EventId(471826074, "LogPacketReaderDone"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "PacketReader done.");
			threadLocalState.Clear();
		}
	}
}
