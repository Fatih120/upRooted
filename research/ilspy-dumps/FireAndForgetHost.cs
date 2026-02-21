using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace RootApp.Utility;

public sealed class FireAndForgetHost(ILogger<FireAndForgetHost>) : IFireAndForgetHost, IAsyncDisposable
{
	private static readonly TimeSpan _updateDelay = TimeSpan.FromSeconds(1L);

	private readonly ILogger<FireAndForgetHost> _logger = P_0;

	private readonly ConcurrentDictionary<Task, bool> _tasks = new ConcurrentDictionary<Task, bool>();

	private int _disposed;

	public async ValueTask DisposeAsync()
	{
		P_0.BeginScope("Disposing");
		if (Interlocked.Exchange(ref _disposed, 1) != 0)
		{
			LogAlreadyDisposed();
			return;
		}
		if (!_tasks.IsEmpty)
		{
			LogHasTasksDuringDispose();
		}
		await WhenAllImplAsync(CancellationToken.None).ConfigureAwait(false);
	}

	public void AddTask(Task P_0)
	{
		EnsureNotDisposed();
		AddTask(P_0, P_0);
	}

	public void AddTask(Task P_0, ILogger P_1)
	{
		EnsureNotDisposed();
		Task task = P_0.ContinueWith(delegate(Task t, object? obj)
		{
			try
			{
				ILogger logger = (ILogger)obj;
				if (t.IsFaulted)
				{
					LogFireAndForgetTaskException(logger, t.Exception, t.Exception?.Message);
				}
				else if (t.IsCanceled)
				{
					LogFireAndForgetTaskCanceled(logger, t.Exception);
				}
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"Fire-and-forget task observer failed: {ex}");
			}
		}, P_1, CancellationToken.None, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.RunContinuationsAsynchronously, TaskScheduler.Default);
		_tasks[task] = false;
		task.ContinueWith(delegate(Task t)
		{
			try
			{
				if (!_tasks.TryRemove(t, out var _))
				{
					LogAddTaskFailedToRemoveTask();
				}
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"Fire-and-forget task cleanup failed: {ex}");
			}
		}, CancellationToken.None, TaskContinuationOptions.AttachedToParent, TaskScheduler.Default);
	}

	public ValueTask WhenAllAsync(CancellationToken P_0 = default(CancellationToken))
	{
		P_0.BeginScope("WhenAll");
		EnsureNotDisposed();
		return WhenAllImplAsync(P_0);
	}

	private async ValueTask WhenAllImplAsync(CancellationToken P_0)
	{
		List<Task> tasks = new List<Task>(_tasks.Count);
		int stuckTasks = 0;
		while (!_tasks.IsEmpty)
		{
			tasks.Clear();
			foreach (KeyValuePair<Task, bool> kv in _tasks)
			{
				Task task = kv.Key;
				if (!task.IsCompleted)
				{
					tasks.Add(kv.Key);
				}
			}
			if (tasks.Count == 0)
			{
				int num = stuckTasks + 1;
				stuckTasks = num;
				if (num > 10)
				{
					stuckTasks = 5;
					LogTasksCleaningUp(_tasks.Count);
					await Task.Delay(100, P_0).ConfigureAwait(continueOnCapturedContext: false);
				}
				else
				{
					await Task.Delay(10, P_0).ConfigureAwait(continueOnCapturedContext: false);
				}
				continue;
			}
			stuckTasks = 0;
			while (true)
			{
				Task<Task> whenAllTask = Task.WhenAny(tasks);
				try
				{
					Task delay = Task.Delay(_updateDelay.AddRandomFraction(), P_0);
					await Task.WhenAny(delay, whenAllTask).ConfigureAwait(continueOnCapturedContext: false);
				}
				catch (OperationCanceledException)
				{
					throw;
				}
				catch (Exception ex2)
				{
					Exception ex3 = ex2;
					LogFireAndForgetTasksFailed(ex3);
				}
				if (whenAllTask.IsCompleted)
				{
					break;
				}
				await Task.Delay(50, P_0).ConfigureAwait(continueOnCapturedContext: false);
				int count = tasks.Count((Task t) => !t.IsCompleted);
				LogTasksStillPending(count, _tasks.Count);
			}
		}
	}

	private void EnsureNotDisposed()
	{
		ObjectDisposedException.ThrowIf(Interlocked.CompareExchange(ref _disposed, 0, 0) != 0, this);
	}

	[LoggerMessage(Level = LogLevel.Critical, Message = "FireAndForgetHost.AddTask() failed to remove task.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogAddTaskFailedToRemoveTask()
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(1);
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "FireAndForgetHost.AddTask() failed to remove task.");
		P_0.Log(LogLevel.Critical, new EventId(477502524, "LogAddTaskFailedToRemoveTask"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "FireAndForgetHost.AddTask() failed to remove task.");
		threadLocalState.Clear();
	}

	[LoggerMessage(Level = LogLevel.Error, Message = "FireAndForgetHost already disposed")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogAlreadyDisposed()
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(1);
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "FireAndForgetHost already disposed");
		P_0.Log(LogLevel.Error, new EventId(157300048, "LogAlreadyDisposed"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "FireAndForgetHost already disposed");
		threadLocalState.Clear();
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Fire-and-forget task canceled.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private static void LogFireAndForgetTaskCanceled(ILogger P_0, Exception? P_1)
	{
		if (P_0.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Fire-and-forget task canceled.");
			P_0.Log(LogLevel.Information, new EventId(1493747981, "LogFireAndForgetTaskCanceled"), threadLocalState, P_1, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Fire-and-forget task canceled.");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Error, Message = "Fire-and-forget task exception: {Exception}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private static void LogFireAndForgetTaskException(ILogger P_0, Exception P_1, string? P_2)
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(2);
		threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Fire-and-forget task exception: {Exception}");
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Exception", P_2);
		P_0.Log(LogLevel.Error, new EventId(158355953, "LogFireAndForgetTaskException"), threadLocalState, P_1, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
		{
			object obj = s.TagArray[0].Value ?? "(null)";
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(32, 1, invariantCulture);
			handler.AppendLiteral("Fire-and-forget task exception: ");
			handler.AppendFormatted<object>(obj);
			return string.Create(invariantCulture, ref handler);
		});
		threadLocalState.Clear();
	}

	[LoggerMessage(Level = LogLevel.Error, Message = "Fire-and-forget tasks failed.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogFireAndForgetTasksFailed(Exception P_0)
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(1);
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Fire-and-forget tasks failed.");
		P_0.Log(LogLevel.Error, new EventId(301837382, "LogFireAndForgetTasksFailed"), threadLocalState, P_0, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Fire-and-forget tasks failed.");
		threadLocalState.Clear();
	}

	[LoggerMessage(Level = LogLevel.Warning, Message = "FireAndForgetHost still has tasks during dispose.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogHasTasksDuringDispose()
	{
		if (P_0.IsEnabled(LogLevel.Warning))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "FireAndForgetHost still has tasks during dispose.");
			P_0.Log(LogLevel.Warning, new EventId(1588688775, "LogHasTasksDuringDispose"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "FireAndForgetHost still has tasks during dispose.");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Warning, Message = "There are {Count} tasks cleaning up.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogTasksCleaningUp(int P_0)
	{
		if (P_0.IsEnabled(LogLevel.Warning))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "There are {Count} tasks cleaning up.");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Count", P_0);
			P_0.Log(LogLevel.Warning, new EventId(1409967187, "LogTasksCleaningUp"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(29, 1, invariantCulture);
				handler.AppendLiteral("There are ");
				handler.AppendFormatted<object>(value);
				handler.AppendLiteral(" tasks cleaning up.");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Warning, Message = "{Count}/{Total} tasks still pending.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogTasksStillPending(int P_0, int P_1)
	{
		if (P_0.IsEnabled(LogLevel.Warning))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "{Count}/{Total} tasks still pending.");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Count", P_0);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Total", P_1);
			P_0.Log(LogLevel.Warning, new EventId(837972028, "LogTasksStillPending"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[1].Value;
				object value2 = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(22, 2, invariantCulture);
				handler.AppendFormatted<object>(value);
				handler.AppendLiteral("/");
				handler.AppendFormatted<object>(value2);
				handler.AppendLiteral(" tasks still pending.");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}
}
