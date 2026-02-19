// RootApp.Client.CoreDomain, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.CoreDomain.IRootSessionAccessor
using System.ComponentModel;
using RootApp.Client.CoreDomain;

public interface IRootSessionAccessor : INotifyPropertyChanged
{
	RootSession? Session { get; }

	void SetSession(RootSession P_0);

	void ClearSession();
}

