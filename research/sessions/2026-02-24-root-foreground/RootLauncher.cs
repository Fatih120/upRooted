#define TRACE
using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RootApp.Client.Avalonia.DI;
using RootApp.Client.Avalonia.Helpers.Activation;
using RootApp.Client.Domain.Helpers.ApplicationConfiguration;
using RootApp.Utility;

namespace RootApp.Client.Avalonia;

public static class RootLauncher
{
	public static void Run<T>(string[] P_0, Func<IHost, T> P_1, Action<T> P_2) where T : IServiceProvider, IDisposable, IAsyncDisposable
	{
		HostApplicationBuilder hostApplicationBuilder = Host.CreateApplicationBuilder(P_0);
		IApplicationRestart applicationRestart = null;
		Mutex mutex = null;
		bool flag = false;
		try
		{
			using IHost arg = hostApplicationBuilder.Build();
			using T val = P_1(arg);
			bool flag2 = 0 == 0;
			IOptions<RootApplicationOptions> requiredService = val.GetRequiredService<IOptions<RootApplicationOptions>>();
			string userProfile = requiredService.Value.UserProfile;
			string text = "RootApp-" + userProfile;
			string text2 = ActivationPipe.SanitizeForOsIdentifier(text);
			string text3 = "Global\\" + text2 + "-SingleInstance";
			string text4 = ActivationPipe.PipeName(text2);
			mutex = new Mutex(true, text3, out var flag3);
			flag = flag3;
			if (!flag3)
			{
				string text5 = ExtractLaunchIdFromArgs(P_0);
				ActivationPipe.SignalFirstInstance(text4, text5, 700);
				return;
			}
			applicationRestart = val.GetRequiredService<IApplicationRestart>();
			ILogger logger = val.GetRequiredService<ILoggerFactory>().CreateLogger("Program");
			logger.LogInformation("Starting application");
			HostedServicesRunner requiredService2 = val.GetRequiredService<HostedServicesRunner>();
			requiredService2.Start(val);
			try
			{
				P_2(val);
			}
			catch (OperationCanceledException)
			{
			}
			catch (Exception ex2)
			{
				logger.LogError(ex2, "Application failed");
			}
			requiredService2.Stop();
			if (!requiredService2.WaitForDoneAsync().Wait(TimeSpan.FromSeconds(30L)))
			{
				logger.LogWarning("Hosted services did not stop in time");
			}
			logger.LogInformation("Application done.");
		}
		catch (Exception ex3)
		{
			Trace.WriteLine($"Application failed: {ex3}");
		}
		finally
		{
			if (flag)
			{
				mutex?.ReleaseMutex();
			}
			applicationRestart?.RestartIfRequested();
		}
	}

	private static string? ExtractLaunchIdFromArgs(string[] P_0)
	{
		if (P_0 == null || P_0.Length == 0)
		{
			return null;
		}
		foreach (string text in P_0)
		{
			int num = text.IndexOf("/launch/", StringComparison.OrdinalIgnoreCase);
			if (num >= 0)
			{
				string text2 = text;
				int num2 = num + "/launch/".Length;
				string text3 = text2.Substring(num2, text2.Length - num2);
				if (!string.IsNullOrWhiteSpace(text3))
				{
					return text3;
				}
			}
		}
		return null;
	}
}
