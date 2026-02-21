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
using RootApp.HubServer.Client;
using RootApp.HubServer.Client.Packets;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Packets;

namespace RootApp.WebApi.Client.Shared;

public sealed class WebApiHubConnectionHandler : IHubConnectionHandler
{
	private readonly ILogger<WebApiHubConnectionHandler> _logger = loggerFactory.CreateLogger<WebApiHubConnectionHandler>();

	private readonly ChannelWriter<ClientHubPacket> _notificationWriter;

	private readonly Uri _url;

	public int MaxPacketSize => 262144;

	public HubNotification PingMessage { get; }

	public long SequenceNumber { get; set; }

	public WebApiHubConnectionHandler(Uri url, ChannelWriter<ClientHubPacket> notificationWriter, ILoggerFactory loggerFactory, long sequenceNumber)
	{
		_notificationWriter = notificationWriter;
		_url = url;
		PingMessage = new HubNotification
		{
			Ping = new PingPacket
			{
				PacketType = PacketType.Ping
			}
		};
		SequenceNumber = sequenceNumber;
		base._002Ector();
	}

	public async ValueTask ConsumeMessageAsync(ReadOnlySequence<byte> buffer, CancellationToken cancellationToken)
	{
		ClientNotification packet = ClientNotification.Parser.ParseFrom(buffer);
		if (packet == null)
		{
			LogInvalidPacket(buffer.Length);
			return;
		}
		LogPacketReceived(packet.Packet.PacketCase.ToString());
		if (SequenceNumber >= packet.SequenceNumber)
		{
			LogAlreadySeenPacket(packet.SequenceNumber);
			return;
		}
		SequenceNumber = packet.SequenceNumber;
		if (packet.Packet.PacketCase != PacketContainer.PacketOneofCase.Ping)
		{
			PingMessage.Ping.SequenceNumber = packet.SequenceNumber;
			IPacket iPacket = packet.Packet.AsIPacket();
			if (iPacket != null)
			{
				if (_logger.IsEnabled(LogLevel.Trace))
				{
					LogPacketTrace(iPacket.ToString());
				}
				await _notificationWriter.WriteAsync(new ClientHubPacket(iPacket, packet.Packet), cancellationToken).ConfigureAwait(false);
			}
			else if (_logger.IsEnabled(LogLevel.Information))
			{
				LogNotIPacket(packet.Packet.ToString());
			}
		}
		else
		{
			LogPongReceived(DateTime.Now.ToLongTimeString());
		}
	}

	public ValueTask SignalHubServerOnlineAsync(CancellationToken cancellationToken)
	{
		if (SequenceNumber >= 0)
		{
			return ValueTask.CompletedTask;
		}
		return _notificationWriter.WriteAsync(new ClientHubPacket(new HubConnectedPacket(_url), null), cancellationToken);
	}

	[LoggerMessage(Level = LogLevel.Error, Message = "Invalid packet of length {Length} received.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogInvalidPacket(long length)
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(2);
		threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Invalid packet of length {Length} received.");
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Length", length);
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

	[LoggerMessage(Level = LogLevel.Trace, Message = "Packet received: {PacketType}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogPacketReceived(string packetType)
	{
		if (_logger.IsEnabled(LogLevel.Trace))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Packet received: {PacketType}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("PacketType", packetType);
			_logger.Log(LogLevel.Trace, new EventId(1044490026, "LogPacketReceived"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(17, 1, invariantCulture);
				handler.AppendLiteral("Packet received: ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Trace, Message = "[{Timestamp}] Pong Received")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogPongReceived(string timestamp)
	{
		if (_logger.IsEnabled(LogLevel.Trace))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "[{Timestamp}] Pong Received");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Timestamp", timestamp);
			_logger.Log(LogLevel.Trace, new EventId(1940326100, "LogPongReceived"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(16, 1, invariantCulture);
				handler.AppendLiteral("[");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral("] Pong Received");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Already seen packet {Number}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogAlreadySeenPacket(long number)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Already seen packet {Number}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Number", number);
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

	[LoggerMessage(Level = LogLevel.Trace, Message = "Packet received: {Packet}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogPacketTrace(string? packet)
	{
		if (_logger.IsEnabled(LogLevel.Trace))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Packet received: {Packet}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Packet", packet);
			_logger.Log(LogLevel.Trace, new EventId(14869594, "LogPacketTrace"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(17, 1, invariantCulture);
				handler.AppendLiteral("Packet received: ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Packet type was not an IPacket {Packet}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogNotIPacket(string? packet)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Packet type was not an IPacket {Packet}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Packet", packet);
			_logger.Log(LogLevel.Information, new EventId(1297851159, "LogNotIPacket"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(31, 1, invariantCulture);
				handler.AppendLiteral("Packet type was not an IPacket ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}
}
