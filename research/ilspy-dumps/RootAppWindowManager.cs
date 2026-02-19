// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.RootAppWindowManager
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.UI.Main;
using RootApp.Client.CoreDomain.Repositories;
using RootApp.Utility;

public class RootAppWindowManager(MainViewModelFactory P_0, IUserInfoRepository P_1, ILoggerFactory P_2, IApplicationRestart P_3, IHostApplicationLifetime P_4) : IRootAppWindowManager
{
	private readonly IApplicationRestart _applicationRestart = P_3;

	private readonly IHostApplicationLifetime _hostApplicationLifetime = P_4;

	private readonly ILogger<RootAppWindowManager> _logger = P_2.CreateLogger<RootAppWindowManager>();

	private readonly MainViewModel _mainViewModel = P_0.Create();

	private readonly SynchronizationContext _synchronizationContext = SynchronizationContext.Current ?? throw new InvalidOperationException("No synchronization context found.");

	private readonly TaskCompletionSource _tcs = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

	private readonly IUserInfoRepository _userInfoRepository = P_1;
}

