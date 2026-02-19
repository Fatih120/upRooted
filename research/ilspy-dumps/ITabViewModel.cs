// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.ITabViewModel
using System;
using RootApp.Client.Avalonia;
using RootApp.Client.CoreDomain.Models.Tabs;

public interface ITabViewModel : IDisposable
{
	Tab Tab { get; }

	IViewModelBase? ContentViewModel { get; }
}

