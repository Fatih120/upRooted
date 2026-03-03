using System;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using Microsoft.Extensions.Logging;
using RootApp.App.Messaging.Grpc;
using RootApp.AppHub.Client.Implementations;
using RootApp.Core.Identifiers;

namespace RootApp.Client.Avalonia.Helpers.RootApps.Connection;

public sealed class AppConnectionEntry : ObservableObject, IDisposable, IAsyncDisposable
{
	private readonly CancellationTokenSource _cts;

	private readonly Task _readerTask;

	private readonly ILogger _logger;

	private readonly ConcurrentDictionary<CommunityAppGuid, Action<AppRpcMessageToClient>> _subscriptions = new ConcurrentDictionary<CommunityAppGuid, Action<AppRpcMessageToClient>>();

	[CompilerGenerated]
	private bool <IsConnected>k__BackingField;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsConnected
	{
		get
		{
			return <IsConnected>k__BackingField;
		}
		private set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsConnected>k__BackingField, flag))
			{
				<IsConnected>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsConnected);
			}
		}
	}

	public AppConnection AppConnection { get; }

	public AppConnectionEntry(AppConnectionFactory P_0, HttpClient P_1, string P_2, ILogger P_3, CancellationToken P_4)
	{
		_logger = P_3;
		_cts = CancellationTokenSource.CreateLinkedTokenSource(P_4);
		AppConnection = P_0.Create(P_1, P_2, onConnectedToHubServer, onDisconnectFromHubServer, _cts.Token);
		_readerTask = Task.Run((Func<Task?>)PacketReaderAsync, _cts.Token);
	}

	public void AddSubscription(CommunityAppGuid P_0, Action<AppRpcMessageToClient> P_1)
	{
		_subscriptions[P_0] = P_1;
	}

	public void RemoveSubscription(CommunityAppGuid P_0)
	{
		_subscriptions.TryRemove(P_0, out Action<AppRpcMessageToClient> _);
	}

	private async Task PacketReaderAsync()
	{
		CancellationToken token = _cts.Token;
		try
		{
			await foreach (AppRpcMessageToClient msg in AppConnection.ReadMessagesAsync(token).ConfigureAwait(false))
			{
				_logger.LogTrace("App msg received: Type={Type} AppId={AppId}", msg.MessageType, msg.CommunityAppId);
				if (_subscriptions.TryGetValue(msg.CommunityAppId, out Action<AppRpcMessageToClient> cb))
				{
					try
					{
						cb?.Invoke(msg);
					}
					catch (Exception ex)
					{
						Exception ex2 = ex;
						_logger.LogError(ex2, "Callback threw for CommunityAppId {CommunityAppId}", msg.CommunityAppId);
					}
				}
				else
				{
					_logger.LogDebug("No subscriber for CommunityAppId {CommunityAppId}", msg.CommunityAppId);
				}
				cb = null;
			}
		}
		catch (OperationCanceledException)
		{
		}
		catch (Exception ex)
		{
			Exception ex4 = ex;
			_logger.LogError(ex4, "Packet reader error for connection {Url}", AppConnection.HubServerUrl);
		}
	}

	private void onConnectedToHubServer()
	{
		IsConnected = true;
		_logger.LogInformation("Connected to app hub server: {Url}", AppConnection.HubServerUrl);
	}

	private void onDisconnectFromHubServer()
	{
		IsConnected = false;
		_logger.LogInformation("Disconnected from app hub server: {Url}", AppConnection.HubServerUrl);
	}

	public void Dispose()
	{
		DisposeAsync().AsTask().GetAwaiter().GetResult();
	}

	public async ValueTask DisposeAsync()
	{
		IsConnected = false;
		try
		{
			await _cts.CancelAsync();
		}
		catch
		{
		}
		try
		{
			await _readerTask.ConfigureAwait(continueOnCapturedContext: false);
		}
		catch (OperationCanceledException)
		{
		}
		catch (Exception ex2)
		{
			Exception ex3 = ex2;
			_logger.LogError(ex3, "Reader task terminated with error during dispose.");
		}
		AppConnection appConnection = AppConnection;
		IAsyncDisposable asyncDisposable = appConnection;
		if (asyncDisposable != null)
		{
			try
			{
				await asyncDisposable.DisposeAsync().ConfigureAwait(false);
			}
			catch (Exception ex2)
			{
				Exception ex4 = ex2;
				_logger.LogError(ex4, "AppConnection.DisposeAsync failed.");
			}
		}
		_cts.Dispose();
	}
}
