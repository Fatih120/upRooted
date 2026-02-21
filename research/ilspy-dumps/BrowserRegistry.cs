using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using RootApp.Client.Avalonia.Messages;
using RootApp.Core.Identifiers;

namespace RootApp.Browser;

public sealed class BrowserRegistry : IDisposable
{
	private readonly Dictionary<RootGuid, IRootBrowser> _browsers = new Dictionary<RootGuid, IRootBrowser>();

	public void Add(RootGuid P_0, IRootBrowser P_1)
	{
		_browsers.Add(P_0, P_1);
	}

	public void AddOrReplace(RootGuid P_0, IRootBrowser P_1)
	{
		if (_browsers.Remove(P_0, out IRootBrowser rootBrowser))
		{
			rootBrowser.Dispose();
		}
		_browsers.Add(P_0, P_1);
	}

	public bool Contains(RootGuid P_0)
	{
		return _browsers.ContainsKey(P_0);
	}

	public IRootBrowser? Get(RootGuid P_0)
	{
		return _browsers.GetValueOrDefault(P_0);
	}

	public T? Get<T>(RootGuid P_0) where T : class, IRootBrowser
	{
		return _browsers.GetValueOrDefault(P_0) as T;
	}

	public T? GetFirst<T>() where T : class, IRootBrowser
	{
		return _browsers.Values.FirstOrDefault((IRootBrowser b) => b is T) as T;
	}

	public bool Remove(RootGuid P_0)
	{
		if (_browsers.Remove(P_0, out IRootBrowser rootBrowser))
		{
			if (Dispatcher.UIThread.CheckAccess())
			{
				WeakReferenceMessenger.Default.Send(new BrowserDisposingMessage(P_0));
			}
			else
			{
				Dispatcher.UIThread.Invoke(() => WeakReferenceMessenger.Default.Send(new BrowserDisposingMessage(P_0)));
			}
			rootBrowser.Dispose();
			return true;
		}
		return false;
	}

	public void RemoveAll<T>() where T : class, IRootBrowser
	{
		List<RootGuid> list = (from kvp in _browsers
			where kvp.Value is T
			select kvp.Key).ToList();
		foreach (RootGuid item in list)
		{
			Remove(item);
		}
	}

	public void RemoveAllAppBrowsersForCommunity(CommunityGuid P_0)
	{
		List<RootGuid> list = (from kvp in _browsers
			where kvp.Value is RootAppBrowser rootAppBrowser && rootAppBrowser.CommunityId == P_0
			select kvp.Key).ToList();
		foreach (RootGuid item in list)
		{
			Remove(item);
		}
	}

	public void Dispose()
	{
		RootGuid rootGuid;
		IRootBrowser rootBrowser;
		foreach (KeyValuePair<RootGuid, IRootBrowser> browser in _browsers)
		{
			browser.Deconstruct(out rootGuid, out rootBrowser);
			RootGuid id = rootGuid;
			if (Dispatcher.UIThread.CheckAccess())
			{
				WeakReferenceMessenger.Default.Send(new BrowserDisposingMessage(id));
				continue;
			}
			Dispatcher.UIThread.Invoke(() => WeakReferenceMessenger.Default.Send(new BrowserDisposingMessage(id)));
		}
		foreach (KeyValuePair<RootGuid, IRootBrowser> browser2 in _browsers)
		{
			browser2.Deconstruct(out rootGuid, out rootBrowser);
			IRootBrowser rootBrowser2 = rootBrowser;
			rootBrowser2.Dispose();
		}
		_browsers.Clear();
	}
}
