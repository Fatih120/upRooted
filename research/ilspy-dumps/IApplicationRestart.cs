using System;
using System.Threading.Tasks;

namespace RootApp.Utility;

public interface IApplicationRestart
{
	bool ForceShutdown { get; }

	void RestartIfRequested();

	void RequestRestart();

	void RequestShutdown();

	void RegisterShutdownHandler(Func<Task> P_0);
}
