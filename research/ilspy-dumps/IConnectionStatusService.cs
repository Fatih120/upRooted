using System;
using System.ComponentModel;

namespace RootApp.WebApi.Client.Shared;

public interface IConnectionStatusService : INotifyPropertyChanged
{
	bool IsConnected { get; }

	bool IsConnectedToHubServer { get; }

	event EventHandler? Reconnected;

	void SetHubServerConnectionStatus(bool isConnected);

	void SetGrpcServerConnectionStatus(bool isConnected);
}
