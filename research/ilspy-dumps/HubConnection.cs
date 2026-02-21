using System;
using System.Buffers;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Pipelines;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using CommunityToolkit.HighPerformance.Buffers;
using Google.Protobuf;
using Microsoft.Extensions.Logging;
using RootApp.Utility;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.HubServer.Client;

public sealed class HubConnection : IAsyncDisposable
{
	private static readonly TimeSpan _hubConnectionTimeout = TimeSpan.FromSeconds(10L);

	private static readonly BoundedChannelOptions _packetChannelOptions = new BoundedChannelOptions(100)
	{
		SingleReader = true,
		SingleWriter = false
	};

	private static readonly PipeOptions _receivePipeOptions = new PipeOptions(null, null, null, 0L, -1L, 4096, false);

	private static readonly TimeSpan _txDoneShutdownDelay = TimeSpan.FromSeconds(7L);

	private static readonly TimeSpan _rxDoneDelay = TimeSpan.FromSeconds(5L);

	private readonly Action _connected;

	private readonly IHubConnectionHandler _connectionHandler;

	private readonly CancellationTokenRegistration _hardCancellationTokenRegistration;

	private readonly CancellationTokenSource _hardShutdownCancellationTokenSource;

	private readonly HttpClient _httpClient;

	private readonly ILogger<HubConnection> _logger;

	private readonly ILoggerFactory _loggerFactory;

	private readonly Channel<IMessage> _packetChannel = Channel.CreateBounded<IMessage>(_packetChannelOptions);

	private readonly ClientWebSocket _rootWebSocketClient;

	private readonly CancellationTokenSource _softShutdownCancellationTokenSource;

	private readonly Task<bool> _workerTask;

	private int _shutdownRequested;

	public bool IsConnectedToSocketServer => _rootWebSocketClient.State == WebSocketState.Open;

	public bool IsClosed
	{
		get
		{
			WebSocketState state = _rootWebSocketClient.State;
			if ((uint)(state - 5) <= 1u)
			{
				return true;
			}
			return false;
		}
	}

	private bool NeedsClose
	{
		get
		{
			WebSocketState state = _rootWebSocketClient.State;
			if (state == WebSocketState.Open || state == WebSocketState.CloseReceived)
			{
				return true;
			}
			return false;
		}
	}

	private Guid Id { get; }

	public Uri Url { get; }

	public HubConnection(Uri P_0, HttpClient P_1, IHubConnectionHandler P_2, ILoggerFactory P_3, Action P_4, CancellationToken P_5)
	{
		ClientWebSocket clientWebSocket = new ClientWebSocket();
		clientWebSocket.Options.HttpVersion = HttpVersion.Version11;
		clientWebSocket.Options.HttpVersionPolicy = HttpVersionPolicy.RequestVersionExact;
		clientWebSocket.Options.KeepAliveInterval = TimeSpan.FromSeconds(45L);
		_rootWebSocketClient = clientWebSocket;
		Id = Guid.NewGuid();
		base._002Ector();
		Url = P_0;
		_httpClient = P_1;
		_loggerFactory = P_3;
		_connectionHandler = P_2;
		_connected = P_4;
		_logger = _loggerFactory.CreateLogger<HubConnection>();
		_hardShutdownCancellationTokenSource = new CancellationTokenSource();
		_softShutdownCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(P_5, _hardShutdownCancellationTokenSource.Token);
		_hardCancellationTokenRegistration = _softShutdownCancellationTokenSource.Token.Register(delegate(object? obj)
		{
			((CancellationTokenSource)obj)?.CancelAfter(TimeSpan.FromSeconds(5L));
		}, _hardShutdownCancellationTokenSource);
		_workerTask = Task.Run((Func<Task<bool>?>)RunAsync, _hardShutdownCancellationTokenSource.Token);
	}

	public async ValueTask DisposeAsync()
	{
		await _hardCancellationTokenRegistration.DisposeAsync();
		await _hardShutdownCancellationTokenSource.CancelAsync();
		_rootWebSocketClient.Abort();
		await _workerTask.ConfigureAwait(continueOnCapturedContext: false);
		_rootWebSocketClient.Dispose();
		_softShutdownCancellationTokenSource.Dispose();
		_hardShutdownCancellationTokenSource.Dispose();
	}

	public void RequestShutdown(TimeSpan P_0)
	{
		_packetChannel.Writer.TryComplete();
		if (Interlocked.CompareExchange(ref _shutdownRequested, 1, 0) == 0)
		{
			if (!_softShutdownCancellationTokenSource.IsCancellationRequested)
			{
				_softShutdownCancellationTokenSource.CancelAfter(P_0);
			}
			if (!_hardShutdownCancellationTokenSource.IsCancellationRequested)
			{
				_hardShutdownCancellationTokenSource.CancelAfter(2.0 * P_0);
			}
		}
	}

	public Task<bool> WaitForShutdownAsync()
	{
		return _workerTask;
	}

	private async Task<bool> RunAsync()
	{
		Pipe pipe = new Pipe(_receivePipeOptions);
		WebSocketCloseStatus webSocketCloseStatus = WebSocketCloseStatus.InternalServerError;
		bool gotData = false;
		try
		{
			LogConnecting(Url);
			using (CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(_softShutdownCancellationTokenSource.Token))
			{
				cts.CancelAfter(_hubConnectionTimeout);
				await _rootWebSocketClient.ConnectAsync(Url, _httpClient, cts.Token).ConfigureAwait(continueOnCapturedContext: false);
			}
			await _connectionHandler.SignalHubServerOnlineAsync(_softShutdownCancellationTokenSource.Token);
			_connected();
			ConfiguredAsyncDisposable I_3 = TaskHandle.Run(PacketWriterAsync, _softShutdownCancellationTokenSource.Token).ConfigureAwait(false);
			try
			{
				await using KeepAliveManager keepAliveManager = new KeepAliveManager(this, _connectionHandler.PingMessage, _loggerFactory.CreateLogger<KeepAliveManager>());
				keepAliveManager.Start();
				try
				{
					if (await ReceivePacketsAsync(pipe).ConfigureAwait(continueOnCapturedContext: false))
					{
						gotData = true;
					}
					if (_rootWebSocketClient.CloseStatus == (WebSocketCloseStatus)4016)
					{
						_connectionHandler.SequenceNumber = -1L;
					}
					webSocketCloseStatus = WebSocketCloseStatus.NormalClosure;
				}
				finally
				{
					keepAliveManager.Stop();
					_packetChannel.Writer.TryComplete();
					RequestShutdown(_rxDoneDelay);
				}
			}
			finally
			{
				IAsyncDisposable asyncDisposable = I_3 as IAsyncDisposable;
				if (asyncDisposable != null)
				{
					await asyncDisposable.DisposeAsync();
				}
			}
		}
		catch (OperationCanceledException)
		{
			webSocketCloseStatus = WebSocketCloseStatus.NormalClosure;
			if (NeedsClose && !_hardShutdownCancellationTokenSource.IsCancellationRequested)
			{
				await _rootWebSocketClient.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", _hardShutdownCancellationTokenSource.Token).ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
			}
			throw;
		}
		catch (Exception ex2)
		{
			Exception exception = ex2;
			LogHubConnectionFailed(exception);
			_connectionHandler.SequenceNumber = -1L;
			throw;
		}
		finally
		{
			if (NeedsClose && !_hardShutdownCancellationTokenSource.Token.IsCancellationRequested)
			{
				await _rootWebSocketClient.CloseAsync(webSocketCloseStatus, null, _hardShutdownCancellationTokenSource.Token).ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
			}
			if (!IsClosed)
			{
				_rootWebSocketClient.Abort();
			}
		}
		return gotData;
	}

	private async Task<bool> ReceivePacketsAsync(Pipe P_0)
	{
		LogReceiveStarting(Id, Url);
		bool gotData = false;
		try
		{
			while (!_softShutdownCancellationTokenSource.IsCancellationRequested && _rootWebSocketClient.State == WebSocketState.Open)
			{
				PipeWriter pipeWriter = P_0.Writer;
				Memory<byte> memory = pipeWriter.GetMemory();
				ValueWebSocketReceiveResult result = await _rootWebSocketClient.ReceiveAsync(memory, _hardShutdownCancellationTokenSource.Token).ConfigureAwait(false);
				WebSocketMessageType messageType = result.MessageType;
				pipeWriter.Advance((messageType == WebSocketMessageType.Binary) ? result.Count : 0);
				switch (messageType)
				{
				case WebSocketMessageType.Text:
					LogUnexpectedTextMessage();
					break;
				case WebSocketMessageType.Binary:
					if (result.Count > 0)
					{
						gotData = true;
					}
					if (pipeWriter.UnflushedBytes > _connectionHandler.MaxPacketSize)
					{
						await _rootWebSocketClient.CloseAsync(WebSocketCloseStatus.MessageTooBig, "Packet too big", _hardShutdownCancellationTokenSource.Token);
						LogPendingBytesTooLarge();
						return gotData;
					}
					if (result.EndOfMessage)
					{
						await pipeWriter.FlushAsync(_hardShutdownCancellationTokenSource.Token).ConfigureAwait(false);
						await ConsumePacketAsync(P_0.Reader).ConfigureAwait(false);
					}
					break;
				case WebSocketMessageType.Close:
					LogCloseReceived();
					return gotData;
				default:
					LogUnexpectedMessageType(messageType);
					break;
				}
			}
		}
		catch (OperationCanceledException)
		{
		}
		catch (Exception ex2)
		{
			Exception ex3 = ex2;
			LogReceiveFailed(ex3, Id);
		}
		finally
		{
			LogReceiveFinished(Id, Url);
		}
		return gotData;
	}

	private async ValueTask ConsumePacketAsync(PipeReader P_0)
	{
		if (P_0.TryRead(out var readResult))
		{
			ReadOnlySequence<byte> buffer = readResult.Buffer;
			if (!buffer.IsEmpty)
			{
				await _connectionHandler.ConsumeMessageAsync(buffer, _softShutdownCancellationTokenSource.Token).ConfigureAwait(false);
			}
			P_0.AdvanceTo(readResult.Buffer.End);
		}
	}

	public async ValueTask SendMessageAsync(HubNotification P_0, CancellationToken P_1)
	{
		using CancellationTokenSource linkedCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(_hardShutdownCancellationTokenSource.Token, P_1);
		P_0.SequenceNumber = _connectionHandler.SequenceNumber;
		await _packetChannel.Writer.WriteAsync(P_0, linkedCancellationTokenSource.Token).ConfigureAwait(false);
	}

	private async Task PacketWriterAsync()
	{
		LogPacketWriterStarting(Id, Url);
		byte[] buffer = ArrayPool<byte>.Shared.Rent(262144);
		MemoryBufferWriter<byte> writer = new MemoryBufferWriter<byte>(buffer);
		WebSocketCloseStatus closeReason = WebSocketCloseStatus.InternalServerError;
		try
		{
			await foreach (IMessage packet in _packetChannel.Reader.ReadAllAsync(_softShutdownCancellationTokenSource.Token).ConfigureAwait(false))
			{
				if (_rootWebSocketClient.State != WebSocketState.Open)
				{
					break;
				}
				LogPacketSending(packet.GetType().Name);
				writer.Clear();
				packet.WriteTo(writer);
				await _rootWebSocketClient.SendAsync(writer.WrittenMemory, WebSocketMessageType.Binary, true, _hardShutdownCancellationTokenSource.Token).ConfigureAwait(false);
			}
			closeReason = WebSocketCloseStatus.NormalClosure;
			LogPacketWriterCompleted();
		}
		catch (OperationCanceledException)
		{
			LogPacketWriterCanceled();
			closeReason = WebSocketCloseStatus.NormalClosure;
		}
		catch (Exception ex2)
		{
			Exception ex3 = ex2;
			LogPacketWriterFailed(ex3, Id);
		}
		ArrayPool<byte>.Shared.Return(buffer);
		LogPacketWriterFinished(Id, Url);
		RequestShutdown(_txDoneShutdownDelay);
		if (IsConnectedToSocketServer && !_hardShutdownCancellationTokenSource.Token.IsCancellationRequested)
		{
			await _rootWebSocketClient.CloseOutputAsync(closeReason, null, _hardShutdownCancellationTokenSource.Token).ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Connecting to {Url}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogConnecting(Uri P_0)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Connecting to {Url}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Url", P_0);
			_logger.Log(LogLevel.Information, new EventId(1150959493, "LogConnecting"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(14, 1, invariantCulture);
				handler.AppendLiteral("Connecting to ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "HubConnection failed.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogHubConnectionFailed(Exception P_0)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "HubConnection failed.");
			_logger.Log(LogLevel.Information, new EventId(39464955, "LogHubConnectionFailed"), threadLocalState, P_0, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "HubConnection failed.");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "ReceivePacketsAsync {Id} for {Url} starting.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogReceiveStarting(Guid P_0, Uri P_1)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "ReceivePacketsAsync {Id} for {Url} starting.");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Id", P_0);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Url", P_1);
			_logger.Log(LogLevel.Information, new EventId(1992524326, "LogReceiveStarting"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[1].Value;
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(35, 2, invariantCulture);
				handler.AppendLiteral("ReceivePacketsAsync ");
				handler.AppendFormatted<object>(value);
				handler.AppendLiteral(" for ");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(" starting.");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "ReceivePacketsAsync {Id} for {Url} finished.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogReceiveFinished(Guid P_0, Uri P_1)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "ReceivePacketsAsync {Id} for {Url} finished.");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Id", P_0);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Url", P_1);
			_logger.Log(LogLevel.Information, new EventId(625057064, "LogReceiveFinished"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[1].Value;
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(35, 2, invariantCulture);
				handler.AppendLiteral("ReceivePacketsAsync ");
				handler.AppendFormatted<object>(value);
				handler.AppendLiteral(" for ");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(" finished.");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "ReceivePacketsAsync {Id} failed.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogReceiveFailed(Exception P_0, Guid P_1)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "ReceivePacketsAsync {Id} failed.");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Id", P_1);
			_logger.Log(LogLevel.Information, new EventId(345761385, "LogReceiveFailed"), threadLocalState, P_0, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(28, 1, invariantCulture);
				handler.AppendLiteral("ReceivePacketsAsync ");
				handler.AppendFormatted<object>(value);
				handler.AppendLiteral(" failed.");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Error, Message = "Unexpected text message received.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogUnexpectedTextMessage()
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(1);
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Unexpected text message received.");
		_logger.Log(LogLevel.Error, new EventId(399214320, "LogUnexpectedTextMessage"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Unexpected text message received.");
		threadLocalState.Clear();
	}

	[LoggerMessage(Level = LogLevel.Error, Message = "Unexpected message type \"{MessageType}\" received.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogUnexpectedMessageType(WebSocketMessageType P_0)
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(2);
		threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Unexpected message type \"{MessageType}\" received.");
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("MessageType", P_0);
		_logger.Log(LogLevel.Error, new EventId(330280337, "LogUnexpectedMessageType"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
		{
			object value = s.TagArray[0].Value;
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(36, 1, invariantCulture);
			handler.AppendLiteral("Unexpected message type \"");
			handler.AppendFormatted<object>(value);
			handler.AppendLiteral("\" received.");
			return string.Create(invariantCulture, ref handler);
		});
		threadLocalState.Clear();
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Close message received.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogCloseReceived()
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Close message received.");
			_logger.Log(LogLevel.Debug, new EventId(788571280, "LogCloseReceived"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Close message received.");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Error, Message = "Pending bytes is greater than max packet size.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogPendingBytesTooLarge()
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(1);
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Pending bytes is greater than max packet size.");
		_logger.Log(LogLevel.Error, new EventId(531104190, "LogPendingBytesTooLarge"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Pending bytes is greater than max packet size.");
		threadLocalState.Clear();
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "PacketWriterAsync {Id} for {Url} starting.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogPacketWriterStarting(Guid P_0, Uri P_1)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "PacketWriterAsync {Id} for {Url} starting.");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Id", P_0);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Url", P_1);
			_logger.Log(LogLevel.Information, new EventId(1148497882, "LogPacketWriterStarting"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[1].Value;
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(33, 2, invariantCulture);
				handler.AppendLiteral("PacketWriterAsync ");
				handler.AppendFormatted<object>(value);
				handler.AppendLiteral(" for ");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(" starting.");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "PacketWriterAsync {Id} for {Url} finished.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogPacketWriterFinished(Guid P_0, Uri P_1)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "PacketWriterAsync {Id} for {Url} finished.");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Id", P_0);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Url", P_1);
			_logger.Log(LogLevel.Information, new EventId(1839136940, "LogPacketWriterFinished"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[1].Value;
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(33, 2, invariantCulture);
				handler.AppendLiteral("PacketWriterAsync ");
				handler.AppendFormatted<object>(value);
				handler.AppendLiteral(" for ");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(" finished.");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Trace, Message = "Sending packet: {PacketType}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogPacketSending(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Trace))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Sending packet: {PacketType}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("PacketType", P_0);
			_logger.Log(LogLevel.Trace, new EventId(670439121, "LogPacketSending"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(16, 1, invariantCulture);
				handler.AppendLiteral("Sending packet: ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Packet writer completed normally")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogPacketWriterCompleted()
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Packet writer completed normally");
			_logger.Log(LogLevel.Debug, new EventId(558656969, "LogPacketWriterCompleted"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Packet writer completed normally");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Packet writer canceled")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogPacketWriterCanceled()
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Packet writer canceled");
			_logger.Log(LogLevel.Debug, new EventId(1845434321, "LogPacketWriterCanceled"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Packet writer canceled");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "PacketWriterAsync {Id} failed.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogPacketWriterFailed(Exception P_0, Guid P_1)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "PacketWriterAsync {Id} failed.");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Id", P_1);
			_logger.Log(LogLevel.Information, new EventId(1931815795, "LogPacketWriterFailed"), threadLocalState, P_0, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(26, 1, invariantCulture);
				handler.AppendLiteral("PacketWriterAsync ");
				handler.AppendFormatted<object>(value);
				handler.AppendLiteral(" failed.");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}
}
