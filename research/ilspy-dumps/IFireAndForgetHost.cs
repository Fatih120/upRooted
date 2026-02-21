using System;
using System.Threading;
using System.Threading.Tasks;

namespace RootApp.Utility;

public interface IFireAndForgetHost : IAsyncDisposable
{
	void AddTask(Task P_0);

	ValueTask WhenAllAsync(CancellationToken P_0 = default(CancellationToken));
}
