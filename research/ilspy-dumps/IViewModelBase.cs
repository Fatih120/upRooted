// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.IViewModelBase
using System;
using System.ComponentModel;

public interface IViewModelBase : INotifyDataErrorInfo, IDisposable
{
	bool IsTopMostViewModel { get; set; }

	void ValidateProperty(string propertyName);
}

