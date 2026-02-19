// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.ViewLocator
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using CommunityToolkit.Mvvm.Messaging;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Messages;

public class ViewLocator : IDataTemplate, ITemplate<object?, Control?>
{
	private readonly ConcurrentDictionary<CachedViewModelBase, Control> _viewCache = new ConcurrentDictionary<CachedViewModelBase, Control>();

	public ViewLocator()
	{
		WeakReferenceMessenger.Default.Register<RemoveViewFromCacheMessage>(this, onRemoveViewFromCacheMessageReceived);
	}

	public Control Build(object? P_0)
	{
		if (!(P_0 is IViewModelBase viewModelBase))
		{
			throw new InvalidOperationException("Failed to create view from object that does not derive from ViewModelBase");
		}
		if (P_0 is CachedViewModelBase cachedViewModelBase)
		{
			Control orCreateCachedView = getOrCreateCachedView(cachedViewModelBase);
			if (orCreateCachedView != null)
			{
				return orCreateCachedView;
			}
		}
		return ViewFactory.CreateView(viewModelBase);
	}

	public bool Match(object? P_0)
	{
		return P_0 is IViewModelBase;
	}

	private Control? getOrCreateCachedView(CachedViewModelBase P_0)
	{
		if (_viewCache.TryGetValue(P_0, out Control result))
		{
			return result;
		}
		Control control = ViewFactory.CreateView(P_0);
		_viewCache[P_0] = control;
		return control;
	}

	private void removeViewFromCache(CachedViewModelBase P_0)
	{
		_viewCache.Remove(P_0, out var _);
	}

	private void onRemoveViewFromCacheMessageReceived(object recipient, RemoveViewFromCacheMessage message)
	{
		removeViewFromCache(message.ViewModel);
	}
}

