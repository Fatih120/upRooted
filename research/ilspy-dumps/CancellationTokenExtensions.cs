using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace RootApp.Utility.Extensions;

public static class CancellationTokenExtensions
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public readonly struct Awaiter : ICriticalNotifyCompletion, INotifyCompletion
	{
		private readonly CancellationToken _cancellationToken;

		public bool IsCompleted => _cancellationToken.IsCancellationRequested;

		public Awaiter(CancellationToken P_0)
		{
			_cancellationToken = P_0;
		}

		public void GetResult()
		{
		}

		public void OnCompleted(Action P_0)
		{
			_cancellationToken.Register(P_0);
		}

		public void UnsafeOnCompleted(Action P_0)
		{
			_cancellationToken.Register(P_0);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static Awaiter GetAwaiter(this CancellationToken P_0)
	{
		return new Awaiter(P_0);
	}
}
