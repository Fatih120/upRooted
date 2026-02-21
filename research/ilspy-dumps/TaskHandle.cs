using System;
using System.Threading;
using System.Threading.Tasks;

namespace RootApp.Utility;

public readonly struct TaskHandle : IAsyncDisposable
{
	private readonly Task? _task;

	public TaskHandle(Task P_0)
	{
		_task = P_0 ?? throw new ArgumentNullException("backgroundTask");
	}

	public static TaskHandle Run(Func<Task> P_0, CancellationToken P_1)
	{
		return new TaskHandle(Task.Run(P_0, P_1));
	}

	public Task WaitAsync()
	{
		return _task ?? Task.CompletedTask;
	}

	public ValueTask DisposeAsync()
	{
		return (_task == null) ? ValueTask.CompletedTask : new ValueTask(_task);
	}
}
