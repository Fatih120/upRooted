using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RootApp.Core;
using RootApp.Information;
using RootApp.Utility.Extensions;

namespace RootApp.Utility;

public class RootAppService : IHostedService
{
	private readonly IFireAndForgetHost _fireAndForget;

	private readonly ILogger<RootAppService> _logger;

	private readonly string _userAgent;

	public IConfigurationRoot Configuration { get; }

	public RootAppService(IHostEnvironment P_0, RootAppVersion P_1, IConfiguration P_2, ILoggerFactory P_3, IFireAndForgetHost P_4, IReadOnlyCollection<RootBuildInformation> P_5)
	{
		_userAgent = P_1.UserAgent;
		Configuration = (IConfigurationRoot)P_2;
		_logger = P_3.CreateLogger<RootAppService>();
		_fireAndForget = P_4;
		LogInitializingRootAppService(_userAgent, P_0.EnvironmentName);
		LogRunningAs(Environment.UserName, Environment.MachineName);
		LogRuntimeVersion(Environment.Version);
		LogFrameworkRuntime(".NET 10.0.3", RuntimeInformation.OSDescription, RuntimeInformation.RuntimeIdentifier);
		if (1 == 0)
		{
		}
		LogProcessBitsAndCores(64, Environment.OSVersion, Environment.ProcessorCount);
		LogTargetFramework(AppContext.TargetFrameworkName);
		LogDynamicCode(true, true);
		LogBuiltFrom(P_1.Commit, P_1.Branch);
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			string baseDirectory = AppContext.BaseDirectory;
			if (!string.IsNullOrWhiteSpace(baseDirectory))
			{
				LogBaseDirectory(baseDirectory, Directory.GetLastWriteTimeUtc(baseDirectory));
			}
		}
		bool? flag = null;
		if (AppContext.TryGetSwitch("System.Globalization.Invariant", out var value))
		{
			flag = value;
		}
		if (CanNormalize())
		{
			LogNormalizeWorks(flag);
		}
		else
		{
			LogNormalizeBroken(flag);
		}
		LogGcServer(GCSettings.IsServerGC, GCSettings.LatencyMode, GCSettings.LargeObjectHeapCompactionMode);
		GCMemoryInfo gCMemoryInfo = GC.GetGCMemoryInfo();
		LogGcConcurrent(gCMemoryInfo.Concurrent, gCMemoryInfo.TotalAvailableMemoryBytes.ToMiB());
		if (_logger.IsEnabled(LogLevel.Trace))
		{
			foreach (var (text2, obj2) in GC.GetConfigurationVariables())
			{
				LogGcConfigurationVariable(text2, obj2?.ToString());
			}
		}
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			foreach (NetworkInterface networkInterface in allNetworkInterfaces)
			{
				if (networkInterface.OperationalStatus != OperationalStatus.Up)
				{
					continue;
				}
				IPInterfaceProperties iPProperties = networkInterface.GetIPProperties();
				foreach (UnicastIPAddressInformation unicastAddress in iPProperties.UnicastAddresses)
				{
					IPAddress address = unicastAddress.Address;
					if (!IPAddress.IsLoopback(address) && !address.IsIPv6LinkLocal)
					{
						LogNicAddress(networkInterface.Name, address.ToString());
					}
				}
			}
		}
		foreach (RootBuildInformation item in P_5)
		{
			LogVersionInformation(item.Name, item.Describe, item.CommitDateTimeOffset?.ToUniversalTime(), item.Branch, item.CommitHash);
			if (!string.IsNullOrEmpty(item.GitHubSha))
			{
				LogGitHubInformation(item.Name, item.GitHubBuild, item.GitHubRefName, item.GitHubSha, item.GitHubWorkflow, item.GitRunNumber, item.GitRunId, item.GitRunAttempt);
			}
			RootBuildInformation gitWorkspace = item.GitWorkspace;
			if ((object)gitWorkspace != null)
			{
				LogVersionInformation(gitWorkspace.Name, gitWorkspace.Describe, gitWorkspace.CommitDateTimeOffset?.ToUniversalTime(), gitWorkspace.Branch, gitWorkspace.CommitHash);
			}
		}
		try
		{
			_ = 0;
			_ = 0;
			_ = 0;
			if (0 == 0)
			{
				Console.Title = _userAgent + " (" + P_1.Instance + ")";
			}
		}
		catch (Exception ex)
		{
			LogUnableToSetConsoleTitle(ex);
		}
		if (!_logger.IsEnabled(LogLevel.Debug))
		{
			return;
		}
		foreach (IConfigurationProvider provider in Configuration.Providers)
		{
			LogProvider(provider);
		}
		if (_logger.IsEnabled(LogLevel.Trace))
		{
			LogConfigurationDebugView(Configuration.GetDebugView(DebugViewRedactor));
		}
		void LogProvider(IConfigurationProvider configurationProvider)
		{
			if (configurationProvider is ChainedConfigurationProvider chainedConfigurationProvider)
			{
				IConfiguration configuration = chainedConfigurationProvider.Configuration;
				if (configuration is IConfigurationRoot configurationRoot)
				{
					{
						foreach (IConfigurationProvider provider2 in configurationRoot.Providers)
						{
							LogProvider(provider2);
						}
						return;
					}
				}
			}
			else if (configurationProvider is FileConfigurationProvider fileConfigurationProvider)
			{
				FileConfigurationSource source = fileConfigurationProvider.Source;
				if (source.FileProvider != null && !string.IsNullOrWhiteSpace(source.Path))
				{
					IFileInfo fileInfo = source.FileProvider.GetFileInfo(source.Path);
					LogConfigurationProviderWithPath(fileConfigurationProvider.ToString(), fileInfo.PhysicalPath ?? fileInfo.Name, fileInfo.Exists, fileInfo.Exists ? new DateTimeOffset?(fileInfo.LastModified.ToLocalTime()) : ((DateTimeOffset?)null));
					return;
				}
			}
			LogConfigurationProvider(configurationProvider.ToString());
		}
	}

	public Task StartAsync(CancellationToken P_0)
	{
		LogStarting(_userAgent);
		AppDomain.CurrentDomain.UnhandledException -= ConsoleUnhandledException;
		AppDomain.CurrentDomain.UnhandledException += LoggingUnhandledException;
		TaskScheduler.UnobservedTaskException -= ConsoleUnobservedTaskException;
		TaskScheduler.UnobservedTaskException += LoggingUnobservedTaskException;
		LogStarted();
		return Task.CompletedTask;
	}

	public async Task StopAsync(CancellationToken P_0)
	{
		LogStopping();
		await _fireAndForget.WhenAllAsync(P_0).ConfigureAwait(false);
		TaskScheduler.UnobservedTaskException -= LoggingUnobservedTaskException;
		TaskScheduler.UnobservedTaskException += ConsoleUnobservedTaskException;
		AppDomain.CurrentDomain.UnhandledException -= LoggingUnhandledException;
		AppDomain.CurrentDomain.UnhandledException += ConsoleUnhandledException;
		Process p = Process.GetCurrentProcess();
		LogGcTotals(GC.GetTotalAllocatedBytes().ToMiB(), p.PeakWorkingSet64.ToMiB(), p.PeakVirtualMemorySize64.ToGiB());
		GCMemoryInfo info = GC.GetGCMemoryInfo();
		LogGcHeap(info.HeapSizeBytes.ToMiB(), info.PauseTimePercentage, info.FinalizationPendingCount);
		LogJitInfo(JitInfo.GetCompilationTime(), JitInfo.GetCompiledILBytes().ToMiB(), JitInfo.GetCompiledMethodCount());
		LogCpuTimes(p.TotalProcessorTime, p.UserProcessorTime, p.PrivilegedProcessorTime);
		LogStartedAndUptime(p.StartTime, DateTime.Now - p.StartTime);
		LogStopped(_userAgent);
	}

	private static bool CanNormalize()
	{
		string a = "àáâãäå".Normalize(NormalizationForm.FormC);
		string b = "àáâãäå".Normalize(NormalizationForm.FormD);
		return !string.Equals(a, b, StringComparison.Ordinal);
	}

	private static string DebugViewRedactor(ConfigurationDebugViewContext context)
	{
		string value = context.Value;
		if (string.IsNullOrWhiteSpace(value))
		{
			return string.Empty;
		}
		if (context.Path.EndsWith("Password", StringComparison.OrdinalIgnoreCase))
		{
			return "[REDACTED]";
		}
		if (!context.Path.StartsWith("ConnectionStrings:", StringComparison.OrdinalIgnoreCase))
		{
			return value;
		}
		if (Uri.TryCreate(value, UriKind.Absolute, out Uri result))
		{
			UriBuilder uriBuilder = new UriBuilder(result);
			if (!string.IsNullOrEmpty(uriBuilder.Password))
			{
				uriBuilder.Password = "[REDACTED]";
				return uriBuilder.ToString();
			}
		}
		if (!value.Contains(';'))
		{
			return (value.Contains("password", StringComparison.OrdinalIgnoreCase) || value.Contains("pwd", StringComparison.OrdinalIgnoreCase)) ? "[REDACTED]" : value;
		}
		try
		{
			DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder
			{
				ConnectionString = value
			};
			if (dbConnectionStringBuilder.TryGetValue("password", out object value2))
			{
				dbConnectionStringBuilder["password"] = "[REDACTED]";
			}
			if (dbConnectionStringBuilder.TryGetValue("pwd", out value2))
			{
				dbConnectionStringBuilder["pwd"] = "[REDACTED]";
			}
			return dbConnectionStringBuilder.ToString();
		}
		catch (ArgumentException)
		{
			return "[INVALID]";
		}
	}

	public static void SetDefaults()
	{
		AppDomain.CurrentDomain.SetData("REGEX_DEFAULT_MATCH_TIMEOUT", TimeSpan.FromSeconds(1L));
		AppDomain.CurrentDomain.UnhandledException += ConsoleUnhandledException;
		TaskScheduler.UnobservedTaskException += ConsoleUnobservedTaskException;
	}

	private static void ConsoleUnhandledException(object sender, UnhandledExceptionEventArgs e)
	{
		Console.Error.WriteLine($"*** Unhandled exception: {e.ExceptionObject}");
	}

	private static void ConsoleUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
	{
		e.SetObserved();
		Console.Error.WriteLine($"*** Unobserved task exception: {e.Exception}");
	}

	private void LoggingUnhandledException(object sender, UnhandledExceptionEventArgs e)
	{
		LogUnhandledException(e.ExceptionObject as Exception);
	}

	private void LoggingUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
	{
		e.SetObserved();
		LogUnobservedTaskException(e.Exception);
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Initializing RootAppService {Application} ({Environment})...")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogInitializingRootAppService(string P_0, string P_1)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "Initializing RootAppService {Application} ({Environment})...");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Application", P_0);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Environment", P_1);
			_logger.Log(LogLevel.Information, new EventId(1246790230, "LogInitializingRootAppService"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[1].Value ?? "(null)";
				object obj2 = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(34, 2, invariantCulture);
				handler.AppendLiteral("Initializing RootAppService ");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(" (");
				handler.AppendFormatted<object>(obj2);
				handler.AppendLiteral(")...");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Running as {User} on {Host}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogRunningAs(string P_0, string P_1)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "Running as {User} on {Host}");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("User", P_0);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Host", P_1);
			_logger.Log(LogLevel.Debug, new EventId(1858139032, "LogRunningAs"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[1].Value ?? "(null)";
				object obj2 = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(15, 2, invariantCulture);
				handler.AppendLiteral("Running as ");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(" on ");
				handler.AppendFormatted<object>(obj2);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Runtime version {Version}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogRuntimeVersion(Version P_0)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Runtime version {Version}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Version", P_0);
			_logger.Log(LogLevel.Information, new EventId(364646695, "LogRuntimeVersion"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(16, 1, invariantCulture);
				handler.AppendLiteral("Runtime version ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Framework \"{Framework}\" OS \"{OS}\" Runtime \"{Runtime}\"")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogFrameworkRuntime(string P_0, string P_1, string P_2)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(4);
			threadLocalState.TagArray[3] = new KeyValuePair<string, object>("{OriginalFormat}", "Framework \"{Framework}\" OS \"{OS}\" Runtime \"{Runtime}\"");
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("Framework", P_0);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("OS", P_1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Runtime", P_2);
			_logger.Log(LogLevel.Debug, new EventId(1853925927, "LogFrameworkRuntime"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[2].Value ?? "(null)";
				object obj2 = s.TagArray[1].Value ?? "(null)";
				object obj3 = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(29, 3, invariantCulture);
				handler.AppendLiteral("Framework \"");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral("\" OS \"");
				handler.AppendFormatted<object>(obj2);
				handler.AppendLiteral("\" Runtime \"");
				handler.AppendFormatted<object>(obj3);
				handler.AppendLiteral("\"");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Running in a {Bit}-bit process on \"{Os}\" with {Cores} cores")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogProcessBitsAndCores(int P_0, OperatingSystem P_1, int P_2)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(4);
			threadLocalState.TagArray[3] = new KeyValuePair<string, object>("{OriginalFormat}", "Running in a {Bit}-bit process on \"{Os}\" with {Cores} cores");
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("Bit", P_0);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Os", P_1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Cores", P_2);
			_logger.Log(LogLevel.Debug, new EventId(649580461, "LogProcessBitsAndCores"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[2].Value;
				object obj = s.TagArray[1].Value ?? "(null)";
				object value2 = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(43, 3, invariantCulture);
				handler.AppendLiteral("Running in a ");
				handler.AppendFormatted<object>(value);
				handler.AppendLiteral("-bit process on \"");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral("\" with ");
				handler.AppendFormatted<object>(value2);
				handler.AppendLiteral(" cores");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Target framework \"{TargetFramework}\"")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogTargetFramework(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Target framework \"{TargetFramework}\"");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("TargetFramework", P_0);
			_logger.Log(LogLevel.Debug, new EventId(1182518724, "LogTargetFramework"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(19, 1, invariantCulture);
				handler.AppendLiteral("Target framework \"");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral("\"");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Trace, Message = "Dynamic code supported: {Dynamic} ({Compiled})")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogDynamicCode(bool P_0, bool P_1)
	{
		if (_logger.IsEnabled(LogLevel.Trace))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "Dynamic code supported: {Dynamic} ({Compiled})");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Dynamic", P_0);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Compiled", P_1);
			_logger.Log(LogLevel.Trace, new EventId(1168637113, "LogDynamicCode"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[1].Value;
				object value2 = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(27, 2, invariantCulture);
				handler.AppendLiteral("Dynamic code supported: ");
				handler.AppendFormatted<object>(value);
				handler.AppendLiteral(" (");
				handler.AppendFormatted<object>(value2);
				handler.AppendLiteral(")");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Built from {Commit} on {Branch}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogBuiltFrom(string P_0, string P_1)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "Built from {Commit} on {Branch}");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Commit", P_0);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Branch", P_1);
			_logger.Log(LogLevel.Information, new EventId(773475469, "LogBuiltFrom"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[1].Value ?? "(null)";
				object obj2 = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(15, 2, invariantCulture);
				handler.AppendLiteral("Built from ");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(" on ");
				handler.AppendFormatted<object>(obj2);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Base directory: {BaseDirectory} Modified: {Modified}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogBaseDirectory(string P_0, DateTime P_1)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "Base directory: {BaseDirectory} Modified: {Modified}");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("BaseDirectory", P_0);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Modified", P_1);
			_logger.Log(LogLevel.Debug, new EventId(883911559, "LogBaseDirectory"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[1].Value ?? "(null)";
				object value = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(27, 2, invariantCulture);
				handler.AppendLiteral("Base directory: ");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(" Modified: ");
				handler.AppendFormatted<object>(value);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "String normalization works (InvariantGlobalization switch: {InvariantGlobalization}).")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogNormalizeWorks(bool? P_0)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "String normalization works (InvariantGlobalization switch: {InvariantGlobalization}).");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("InvariantGlobalization", P_0);
			_logger.Log(LogLevel.Debug, new EventId(1631659758, "LogNormalizeWorks"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(61, 1, invariantCulture);
				handler.AppendLiteral("String normalization works (InvariantGlobalization switch: ");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(").");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Critical, Message = "***** String normalization is broken. (InvariantGlobalization switch: {InvariantGlobalization}) THIS CAN HAVE SECURITY IMPLICATIONS! *****")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogNormalizeBroken(bool? P_0)
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(2);
		threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "***** String normalization is broken. (InvariantGlobalization switch: {InvariantGlobalization}) THIS CAN HAVE SECURITY IMPLICATIONS! *****");
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("InvariantGlobalization", P_0);
		_logger.Log(LogLevel.Critical, new EventId(2104313715, "LogNormalizeBroken"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
		{
			object obj = s.TagArray[0].Value ?? "(null)";
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(114, 1, invariantCulture);
			handler.AppendLiteral("***** String normalization is broken. (InvariantGlobalization switch: ");
			handler.AppendFormatted<object>(obj);
			handler.AppendLiteral(") THIS CAN HAVE SECURITY IMPLICATIONS! *****");
			return string.Create(invariantCulture, ref handler);
		});
		threadLocalState.Clear();
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "GC Is Server: {Server} Latency Mode: {LatencyMode} LOH Compaction Mode: {CompactionMode}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogGcServer(bool P_0, GCLatencyMode P_1, GCLargeObjectHeapCompactionMode P_2)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(4);
			threadLocalState.TagArray[3] = new KeyValuePair<string, object>("{OriginalFormat}", "GC Is Server: {Server} Latency Mode: {LatencyMode} LOH Compaction Mode: {CompactionMode}");
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("Server", P_0);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("LatencyMode", P_1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("CompactionMode", P_2);
			_logger.Log(LogLevel.Debug, new EventId(2048640592, "LogGcServer"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[2].Value;
				object value2 = s.TagArray[1].Value;
				object value3 = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(51, 3, invariantCulture);
				handler.AppendLiteral("GC Is Server: ");
				handler.AppendFormatted<object>(value);
				handler.AppendLiteral(" Latency Mode: ");
				handler.AppendFormatted<object>(value2);
				handler.AppendLiteral(" LOH Compaction Mode: ");
				handler.AppendFormatted<object>(value3);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "GC Is Concurrent: {IsConcurrent} Total Memory: {TotalMemory:F1} MiB")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogGcConcurrent(bool P_0, double P_1)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "GC Is Concurrent: {IsConcurrent} Total Memory: {TotalMemory:F1} MiB");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("IsConcurrent", P_0);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("TotalMemory", P_1);
			_logger.Log(LogLevel.Debug, new EventId(1218209334, "LogGcConcurrent"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[1].Value;
				object value2 = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(37, 2, invariantCulture);
				handler.AppendLiteral("GC Is Concurrent: ");
				handler.AppendFormatted<object>(value);
				handler.AppendLiteral(" Total Memory: ");
				handler.AppendFormatted(value2, "F1");
				handler.AppendLiteral(" MiB");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Trace, Message = "GC {Key} = {Value}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogGcConfigurationVariable(string P_0, string? P_1)
	{
		if (_logger.IsEnabled(LogLevel.Trace))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "GC {Key} = {Value}");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Key", P_0);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Value", P_1);
			_logger.Log(LogLevel.Trace, new EventId(1738476233, "LogGcConfigurationVariable"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[1].Value ?? "(null)";
				object obj2 = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(6, 2, invariantCulture);
				handler.AppendLiteral("GC ");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(" = ");
				handler.AppendFormatted<object>(obj2);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "NIC \"{Name}\" {Address}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogNicAddress(string P_0, string P_1)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "NIC \"{Name}\" {Address}");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Name", P_0);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Address", P_1);
			_logger.Log(LogLevel.Debug, new EventId(289408197, "LogNicAddress"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[1].Value ?? "(null)";
				object obj2 = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(7, 2, invariantCulture);
				handler.AppendLiteral("NIC \"");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral("\" ");
				handler.AppendFormatted<object>(obj2);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "{Name}: {Describe} on {Committed:O} branch \"{Branch}\" ({Commit})")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogVersionInformation(string P_0, string P_1, DateTimeOffset? P_2, string P_3, string P_4)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(6);
			threadLocalState.TagArray[5] = new KeyValuePair<string, object>("{OriginalFormat}", "{Name}: {Describe} on {Committed:O} branch \"{Branch}\" ({Commit})");
			threadLocalState.TagArray[4] = new KeyValuePair<string, object>("Name", P_0);
			threadLocalState.TagArray[3] = new KeyValuePair<string, object>("Describe", P_1);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("Committed", P_2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Branch", P_3);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Commit", P_4);
			_logger.Log(LogLevel.Information, new EventId(644309983, "LogVersionInformation"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[4].Value ?? "(null)";
				object obj2 = s.TagArray[3].Value ?? "(null)";
				object obj3 = s.TagArray[2].Value ?? "(null)";
				object obj4 = s.TagArray[1].Value ?? "(null)";
				object obj5 = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(19, 5, invariantCulture);
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(": ");
				handler.AppendFormatted<object>(obj2);
				handler.AppendLiteral(" on ");
				handler.AppendFormatted(obj3, "O");
				handler.AppendLiteral(" branch \"");
				handler.AppendFormatted<object>(obj4);
				handler.AppendLiteral("\" (");
				handler.AppendFormatted<object>(obj5);
				handler.AppendLiteral(")");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "{Name} GitHub: Deployment {Deployment} Ref \"{Ref}\" SHA {SHA} Workflow \"{Workflow}\"/{WorkflowNumber} Run {RunId}/{RunAttempt}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogGitHubInformation(string P_0, bool P_1, string P_2, string P_3, string P_4, int P_5, long P_6, int P_7)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(9);
			threadLocalState.TagArray[8] = new KeyValuePair<string, object>("{OriginalFormat}", "{Name} GitHub: Deployment {Deployment} Ref \"{Ref}\" SHA {SHA} Workflow \"{Workflow}\"/{WorkflowNumber} Run {RunId}/{RunAttempt}");
			threadLocalState.TagArray[7] = new KeyValuePair<string, object>("Name", P_0);
			threadLocalState.TagArray[6] = new KeyValuePair<string, object>("Deployment", P_1);
			threadLocalState.TagArray[5] = new KeyValuePair<string, object>("Ref", P_2);
			threadLocalState.TagArray[4] = new KeyValuePair<string, object>("SHA", P_3);
			threadLocalState.TagArray[3] = new KeyValuePair<string, object>("Workflow", P_4);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("WorkflowNumber", P_5);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("RunId", P_6);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("RunAttempt", P_7);
			_logger.Log(LogLevel.Information, new EventId(456225608, "LogGitHubInformation"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[7].Value ?? "(null)";
				object value = s.TagArray[6].Value;
				object obj2 = s.TagArray[5].Value ?? "(null)";
				object obj3 = s.TagArray[4].Value ?? "(null)";
				object obj4 = s.TagArray[3].Value ?? "(null)";
				object value2 = s.TagArray[2].Value;
				object value3 = s.TagArray[1].Value;
				object value4 = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(51, 8, invariantCulture);
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(" GitHub: Deployment ");
				handler.AppendFormatted<object>(value);
				handler.AppendLiteral(" Ref \"");
				handler.AppendFormatted<object>(obj2);
				handler.AppendLiteral("\" SHA ");
				handler.AppendFormatted<object>(obj3);
				handler.AppendLiteral(" Workflow \"");
				handler.AppendFormatted<object>(obj4);
				handler.AppendLiteral("\"/");
				handler.AppendFormatted<object>(value2);
				handler.AppendLiteral(" Run ");
				handler.AppendFormatted<object>(value3);
				handler.AppendLiteral("/");
				handler.AppendFormatted<object>(value4);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Unable to set console title")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogUnableToSetConsoleTitle(Exception P_0)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Unable to set console title");
			_logger.Log(LogLevel.Debug, new EventId(1039446956, "LogUnableToSetConsoleTitle"), threadLocalState, P_0, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Unable to set console title");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Configuration: {Description} at {Path} exists: {Exists} modified: {Modified}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogConfigurationProviderWithPath(string P_0, string P_1, bool P_2, DateTimeOffset? P_3)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(5);
			threadLocalState.TagArray[4] = new KeyValuePair<string, object>("{OriginalFormat}", "Configuration: {Description} at {Path} exists: {Exists} modified: {Modified}");
			threadLocalState.TagArray[3] = new KeyValuePair<string, object>("Description", P_0);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("Path", P_1);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Exists", P_2);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Modified", P_3);
			_logger.Log(LogLevel.Debug, new EventId(606042021, "LogConfigurationProviderWithPath"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[3].Value ?? "(null)";
				object obj2 = s.TagArray[2].Value ?? "(null)";
				object value = s.TagArray[1].Value;
				object obj3 = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(39, 4, invariantCulture);
				handler.AppendLiteral("Configuration: ");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(" at ");
				handler.AppendFormatted<object>(obj2);
				handler.AppendLiteral(" exists: ");
				handler.AppendFormatted<object>(value);
				handler.AppendLiteral(" modified: ");
				handler.AppendFormatted<object>(obj3);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Configuration: {Description}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogConfigurationProvider(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Configuration: {Description}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Description", P_0);
			_logger.Log(LogLevel.Debug, new EventId(1002805308, "LogConfigurationProvider"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(15, 1, invariantCulture);
				handler.AppendLiteral("Configuration: ");
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Trace, Message = "{Configuration}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogConfigurationDebugView(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Trace))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "{Configuration}");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Configuration", P_0);
			_logger.Log(LogLevel.Trace, new EventId(798809025, "LogConfigurationDebugView"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(0, 1, invariantCulture);
				handler.AppendFormatted<object>(obj);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Starting RootAppService {UserAgent}...")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogStarting(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "Starting RootAppService {UserAgent}...");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("UserAgent", P_0);
			_logger.Log(LogLevel.Debug, new EventId(1280267637, "LogStarting"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(27, 1, invariantCulture);
				handler.AppendLiteral("Starting RootAppService ");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral("...");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "RootAppService started.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogStarted()
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "RootAppService started.");
			_logger.Log(LogLevel.Debug, new EventId(1929458206, "LogStarted"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "RootAppService started.");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "Stopping RootAppService...")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogStopping()
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "Stopping RootAppService...");
			_logger.Log(LogLevel.Debug, new EventId(1694195323, "LogStopping"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "Stopping RootAppService...");
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "GC Total Allocated: {Allocated:F2} MiB Peak working set: {WorkingSet:F2} MiB Peak VM: {VM:F2} GiB")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogGcTotals(double P_0, double P_1, double P_2)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(4);
			threadLocalState.TagArray[3] = new KeyValuePair<string, object>("{OriginalFormat}", "GC Total Allocated: {Allocated:F2} MiB Peak working set: {WorkingSet:F2} MiB Peak VM: {VM:F2} GiB");
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("Allocated", P_0);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("WorkingSet", P_1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("VM", P_2);
			_logger.Log(LogLevel.Debug, new EventId(1027441472, "LogGcTotals"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[2].Value;
				object value2 = s.TagArray[1].Value;
				object value3 = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(61, 3, invariantCulture);
				handler.AppendLiteral("GC Total Allocated: ");
				handler.AppendFormatted(value, "F2");
				handler.AppendLiteral(" MiB Peak working set: ");
				handler.AppendFormatted(value2, "F2");
				handler.AppendLiteral(" MiB Peak VM: ");
				handler.AppendFormatted(value3, "F2");
				handler.AppendLiteral(" GiB");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "GC Heap: {HeapSize:F1} MiB Pause: {Pause:F2}% Pending Finalization: {PendingFinalization}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogGcHeap(double P_0, double P_1, long P_2)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(4);
			threadLocalState.TagArray[3] = new KeyValuePair<string, object>("{OriginalFormat}", "GC Heap: {HeapSize:F1} MiB Pause: {Pause:F2}% Pending Finalization: {PendingFinalization}");
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("HeapSize", P_0);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Pause", P_1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("PendingFinalization", P_2);
			_logger.Log(LogLevel.Debug, new EventId(1933094693, "LogGcHeap"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[2].Value;
				object value2 = s.TagArray[1].Value;
				object value3 = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(45, 3, invariantCulture);
				handler.AppendLiteral("GC Heap: ");
				handler.AppendFormatted(value, "F1");
				handler.AppendLiteral(" MiB Pause: ");
				handler.AppendFormatted(value2, "F2");
				handler.AppendLiteral("% Pending Finalization: ");
				handler.AppendFormatted<object>(value3);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "JIT time: {Time} Size: {Size:F2} MiB Methods: {Methods}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogJitInfo(TimeSpan P_0, double P_1, long P_2)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(4);
			threadLocalState.TagArray[3] = new KeyValuePair<string, object>("{OriginalFormat}", "JIT time: {Time} Size: {Size:F2} MiB Methods: {Methods}");
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("Time", P_0);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Size", P_1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Methods", P_2);
			_logger.Log(LogLevel.Debug, new EventId(2011910648, "LogJitInfo"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[2].Value;
				object value2 = s.TagArray[1].Value;
				object value3 = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(31, 3, invariantCulture);
				handler.AppendLiteral("JIT time: ");
				handler.AppendFormatted<object>(value);
				handler.AppendLiteral(" Size: ");
				handler.AppendFormatted(value2, "F2");
				handler.AppendLiteral(" MiB Methods: ");
				handler.AppendFormatted<object>(value3);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "CPU Time: {CpuTime} User Time: {UserTime} Privileged Time: {PrivilegedTime}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogCpuTimes(TimeSpan P_0, TimeSpan P_1, TimeSpan P_2)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(4);
			threadLocalState.TagArray[3] = new KeyValuePair<string, object>("{OriginalFormat}", "CPU Time: {CpuTime} User Time: {UserTime} Privileged Time: {PrivilegedTime}");
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("CpuTime", P_0);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("UserTime", P_1);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("PrivilegedTime", P_2);
			_logger.Log(LogLevel.Information, new EventId(1770782293, "LogCpuTimes"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[2].Value;
				object value2 = s.TagArray[1].Value;
				object value3 = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(40, 3, invariantCulture);
				handler.AppendLiteral("CPU Time: ");
				handler.AppendFormatted<object>(value);
				handler.AppendLiteral(" User Time: ");
				handler.AppendFormatted<object>(value2);
				handler.AppendLiteral(" Privileged Time: ");
				handler.AppendFormatted<object>(value3);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Information, Message = "Started: {Started} Uptime: {Uptime}")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogStartedAndUptime(DateTime P_0, TimeSpan P_1)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(3);
			threadLocalState.TagArray[2] = new KeyValuePair<string, object>("{OriginalFormat}", "Started: {Started} Uptime: {Uptime}");
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("Started", P_0);
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Uptime", P_1);
			_logger.Log(LogLevel.Information, new EventId(1248607761, "LogStartedAndUptime"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object value = s.TagArray[1].Value;
				object value2 = s.TagArray[0].Value;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(18, 2, invariantCulture);
				handler.AppendLiteral("Started: ");
				handler.AppendFormatted<object>(value);
				handler.AppendLiteral(" Uptime: ");
				handler.AppendFormatted<object>(value2);
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Debug, Message = "RootAppService {Application} stopped.")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogStopped(string P_0)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
			threadLocalState.ReserveTagSpace(2);
			threadLocalState.TagArray[1] = new KeyValuePair<string, object>("{OriginalFormat}", "RootAppService {Application} stopped.");
			threadLocalState.TagArray[0] = new KeyValuePair<string, object>("Application", P_0);
			_logger.Log(LogLevel.Debug, new EventId(112729106, "LogStopped"), threadLocalState, null, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) =>
			{
				object obj = s.TagArray[0].Value ?? "(null)";
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				DefaultInterpolatedStringHandler handler = new DefaultInterpolatedStringHandler(24, 1, invariantCulture);
				handler.AppendLiteral("RootAppService ");
				handler.AppendFormatted<object>(obj);
				handler.AppendLiteral(" stopped.");
				return string.Create(invariantCulture, ref handler);
			});
			threadLocalState.Clear();
		}
	}

	[LoggerMessage(Level = LogLevel.Critical, Message = "*** Unhandled exception")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogUnhandledException(Exception? P_0)
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(1);
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "*** Unhandled exception");
		_logger.Log(LogLevel.Critical, new EventId(233252029, "LogUnhandledException"), threadLocalState, P_0, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "*** Unhandled exception");
		threadLocalState.Clear();
	}

	[LoggerMessage(Level = LogLevel.Critical, Message = "*** Unobserved task exception")]
	[GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")]
	private void LogUnobservedTaskException(Exception? P_0)
	{
		LoggerMessageState threadLocalState = LoggerMessageHelper.ThreadLocalState;
		threadLocalState.ReserveTagSpace(1);
		threadLocalState.TagArray[0] = new KeyValuePair<string, object>("{OriginalFormat}", "*** Unobserved task exception");
		_logger.Log(LogLevel.Critical, new EventId(1080745056, "LogUnobservedTaskException"), threadLocalState, P_0, [GeneratedCode("Microsoft.Gen.Logging", "10.3.0.0")] (LoggerMessageState s, Exception? _) => "*** Unobserved task exception");
		threadLocalState.Clear();
	}
}
