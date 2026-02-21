using System.Threading;
using System.Threading.Tasks;

namespace RootApp.Utility.Extensions;

public static class TaskWaitExtensions
{
	public static void WaitNoThrow(this Task P_0)
	{
		ObserveExceptionsAsync(P_0).Wait();
	}

	private static Task ObserveExceptionsAsync(Task P_0)
	{
		return P_0.ContinueWith(delegate(Task t)
		{
			_ = t.Exception;
		}, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
	}
}
