using System;
using System.Buffers;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RootApp.App.Messaging.Grpc;

namespace RootApp.AppHub.Client;

public sealed class AppHubConnectionHandler : IAppHubConnectionHandler
{
	private readonly Action _connectedToHubCallback;

	private readonly Action _disconnectedFromHubCallback;

	private readonly ILogger<AppHubConnectionHandler> _logger;

	private readonly ChannelWriter<AppRpcMessageToClient> _notificationWriter;

	[CompilerGenerated]
	private long _003CSequenceNumber_003Ek__BackingField;

	public int MaxPacketSize => 262144;

	public AppRpcMessageToHost PingMessage { get; }

	public long SequenceNumber
	{
		[CompilerGenerated]
		get
		{
			return _003CSequenceNumber_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSequenceNumber_003Ek__BackingField = num;
		}
	}

	public AppHubConnectionHandler(ChannelWriter<AppRpcMessageToClient> P_0, Action P_1, Action P_2, ILoggerFactory P_3, long P_4)
	{
		_connectedToHubCallback = P_1;
		_disconnectedFromHubCallback = P_2;
		_logger = P_3.CreateLogger<AppHubConnectionHandler>();
		_notificationWriter = P_0;
		PingMessage = new AppRpcMessageToHost
		{
			MessageType = AppRpcMessageType.Ping
		};
		SequenceNumber = P_4;
		base._002Ector();
	}

	public ValueTask SignalHubServerOnlineAsync(CancellationToken P_0)
	{
		_connectedToHubCallback();
		return ValueTask.CompletedTask;
	}

	public async ValueTask ConsumeMessageAsync(ReadOnlySequence<byte> P_0, CancellationToken P_1)
	{
		AppRpcMessageToClient packet = AppRpcMessageToClient.Parser.ParseFrom(P_0);
		if (packet == null)
		{
			LogInvalidPacket(P_0.Length);
			return;
		}
		LogPacketReceived(packet.SequenceNumber, packet.GetType().Name);
		if (SequenceNumber >= packet.SequenceNumber)
		{
			LogAlreadySeenPacket(packet.SequenceNumber);
			return;
		}
		if (packet.MessageType == AppRpcMessageType.ClientDetach)
		{
			SequenceNumber = -1L;
			return;
		}
		SequenceNumber = packet.SequenceNumber;
		if (packet.MessageType != AppRpcMessageType.Ping)
		{
			await _notificationWriter.WriteAsync(packet, P_1).ConfigureAwait(false);
		}
	}

	[LoggerMessage(Level = LogLevel.Error, Message = "Invalid packet of length {Length} received.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogInvalidPacket(long P_0)
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(2);
		threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Invalid packet of length {Length} received.");
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Length", P_0);
		_logger.Log(LogLevel.Error, new EventId(1155034508, "LogInvalidPacket"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
		{
			object value = s.TagArray[0].Value;
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(35, 1, invariantCulture);
			handler.AppendLiteral("Invalid packet of length ");
			handler.AppendFormatted<object>(value);
			handler.AppendLiteral(" received.");
			return string.Create(invariantCulture, ref handler);
		});
		threadLocalState.Clear();
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Already seen packet {Number}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogAlreadySeenPacket(long P_0)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Already seen packet {Number}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Number", P_0);
			_logger.Log(LogLevel.Information, new EventId(1056268248, "LogAlreadySeenPacket"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(20, 1, invariantCulture);
				handler.AppendLiteral("Already seen packet ");
				handler.AppendFormatted<object>(value);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Packet received: {SequenceNumber} - {PacketType}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogPacketReceived(long P_0, string P_1)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "Packet received: {SequenceNumber} - {PacketType}");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("SequenceNumber", P_0);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("PacketType", P_1);
			_logger.Log(LogLevel.Information, new EventId(1044490026, "LogPacketReceived"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[1].Value;
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(20, 2, invariantCulture);
				handler.AppendLiteral("Packet received: ");
				handler.AppendFormatted<object>(value);
				handler.AppendLiteral(" - ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}
}
