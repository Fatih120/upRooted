#define TRACE
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RootApp.Utility;

namespace RootApp.Hosting;

public abstract class ApplicationRestartBase : IApplicationRestart
{
	private readonly IHostApplicationLifetime _applicationLifetime;

	private readonly IFireAndForgetHost _fireAndForget;

	private readonly ILogger _logger;

	private readonly ConcurrentBag<Func<Task>> _shutdownHandlers;

	private bool _restart;

	private bool _forceShutdown;

	public bool ForceShutdown => _forceShutdown;

	protected ApplicationRestartBase(IHostApplicationLifetime P_0, IFireAndForgetHost P_1, ILoggerFactory P_2)
	{
		_applicationLifetime = P_0;
		_fireAndForget = P_1;
		_logger = P_2.CreateLogger<ApplicationRestartBase>();
		_shutdownHandlers = new ConcurrentBag<Func<Task>>();
		base._002Ector();
	}

	public void RestartIfRequested()
	{
		if (_restart)
		{
			performReset();
		}
	}

	public void RequestRestart()
	{
		_restart = true;
		RequestShutdown();
	}

	public void RequestShutdown()
	{
		if (Interlocked.Exchange(ref _forceShutdown, true))
		{
			_logger.LogWarning("Application shutdown already requested.");
			return;
		}
		Task task = Task.Run(async delegate
		{
			IEnumerable<Task> tasks = _shutdownHandlers.Select((Func<Task> handler) => Task.Run(async delegate
			{
				try
				{
					await handler();
				}
				catch (Exception ex)
				{
					Exception ex2 = ex;
					_logger.LogError(ex2, "Error during shutdown handler execution.");
				}
			}));
			await Task.WhenAll(tasks).ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
			_logger.LogInformation("Stopping application");
			_applicationLifetime.StopApplication();
		});
		_fireAndForget.AddTask(task);
	}

	public void RegisterShutdownHandler(Func<Task> P_0)
	{
		_shutdownHandlers.Add(P_0);
	}

	private void performReset()
	{
		string processPath = Environment.ProcessPath;
		bool flag = 1 == 0;
		if (string.IsNullOrEmpty(processPath))
		{
			Trace.WriteLine("Application executable path not found.");
			return;
		}
		ProcessStartInfo processStartInfo = new ProcessStartInfo
		{
			FileName = (flag ? "nohup" : processPath),
			CreateNoWindow = true,
			UseShellExecute = false
		};
		if (flag)
		{
			processStartInfo.ArgumentList.Add(processPath);
		}
		foreach (string item in Environment.GetCommandLineArgs().Skip(1))
		{
			processStartInfo.ArgumentList.Add(item);
		}
		using (Process.Start(processStartInfo))
		{
		}
	}
}
