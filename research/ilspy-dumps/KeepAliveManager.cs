using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Threading;
using RootApp.App.Messaging.Grpc;
using RootApp.Utility;

namespace RootApp.AppHub.Client;

public sealed class KeepAliveManager : System.IAsyncDisposable
{
	private static readonly TimeSpan _nominalDelay = TimeSpan.FromSeconds(10L);

	private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

	private readonly AppHubConnection _connection;

	private readonly ILogger<KeepAliveManager> _logger;

	private readonly AppRpcMessageToHost _pingPacket;

	private readonly TaskCompletionSource _startTcs = new TaskCompletionSource();

	private readonly Task _worker;

	private static TimeSpan FuzzyDelay => _nominalDelay.AddRandomFraction();

	public KeepAliveManager(AppHubConnection P_0, AppRpcMessageToHost P_1, ILogger<KeepAliveManager> P_2)
	{
		_connection = P_0;
		_logger = P_2;
		_pingPacket = P_1;
		_worker = Task.Run((Func<Task?>)RunAsync, _cancellationTokenSource.Token);
	}

	public async ValueTask DisposeAsync()
	{
		Stop();
		await _worker.ConfigureAwait(continueOnCapturedContext: false);
		_cancellationTokenSource.Dispose();
	}

	private async Task RunAsync()
	{
		try
		{
			await _startTcs.Task.WithCancellation(_cancellationTokenSource.Token).ConfigureAwait(continueOnCapturedContext: false);
			while (!_cancellationTokenSource.IsCancellationRequested)
			{
				await Task.Delay(FuzzyDelay, _cancellationTokenSource.Token).ConfigureAwait(continueOnCapturedContext: false);
				try
				{
					await _connection.SendMessageAsync(_pingPacket, _cancellationTokenSource.Token).ConfigureAwait(false);
				}
				catch (ChannelClosedException)
				{
					break;
				}
				catch (OperationCanceledException)
				{
					break;
				}
				catch (Exception ex3)
				{
					LogErrorSendingPingPacket(_logger, ex3);
				}
			}
		}
		catch (OperationCanceledException)
		{
		}
		catch (Exception ex5)
		{
			Exception ex6 = ex5;
			LogErrorRunningKeepAliveManager(_logger, ex6);
		}
	}

	public void Start()
	{
		_startTcs.TrySetResult();
	}

	public void Stop()
	{
		_cancellationTokenSource.Cancel();
	}

	[LoggerMessage(Level = LogLevel.Error, Message = "Error while running KeepAliveManager")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private static void LogErrorRunningKeepAliveManager(ILogger P_0, Exception P_1)
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(1);
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Error while running KeepAliveManager");
		P_0.Log(LogLevel.Error, new EventId(1390619149, "LogErrorRunningKeepAliveManager"), threadLocalState, P_1, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Error while running KeepAliveManager");
		threadLocalState.Clear();
	}

	[LoggerMessage(Level = LogLevel.Error, Message = "Error while sending ping packet")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private static void LogErrorSendingPingPacket(ILogger P_0, Exception P_1)
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(1);
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Error while sending ping packet");
		P_0.Log(LogLevel.Error, new EventId(1154013769, "LogErrorSendingPingPacket"), threadLocalState, P_1, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Error while sending ping packet");
		threadLocalState.Clear();
	}
}
