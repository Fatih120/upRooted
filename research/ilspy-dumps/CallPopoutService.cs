using System;
using Avalonia.Controls;
using RootApp.Client.Avalonia.UI.Call;
using RootApp.Client.CoreDomain.Models.Media;

namespace RootApp.Client.Avalonia.Helpers.Calling;

public class CallPopoutService
{
	private readonly CallPopoutViewModelFactory _callPopoutViewModelFactory;

	private CallPopoutWindow? _callPopoutWindow;

	public CallPopoutService(CallPopoutViewModelFactory P_0)
	{
		_callPopoutViewModelFactory = P_0;
	}

	public void PopoutCall(MediaRoom P_0)
	{
		if (_callPopoutWindow == null)
		{
			CallPopoutViewModel callPopoutViewModel = _callPopoutViewModelFactory.Create(P_0);
			_callPopoutWindow = new CallPopoutWindow(callPopoutViewModel);
			_callPopoutWindow.Closed += onCallWindowClosed;
			_callPopoutWindow.Show();
		}
	}

	public void FocusPopoutWindow()
	{
		if (_callPopoutWindow != null)
		{
			if (!_callPopoutWindow.IsVisible)
			{
				_callPopoutWindow.Show();
			}
			if (_callPopoutWindow.WindowState == WindowState.Minimized)
			{
				_callPopoutWindow.WindowState = WindowState.Normal;
			}
			_callPopoutWindow.Activate();
		}
	}

	private void onCallWindowClosed(object? sender, EventArgs e)
	{
		if (_callPopoutWindow != null)
		{
			_callPopoutWindow.Closed -= onCallWindowClosed;
			_callPopoutWindow = null;
		}
	}
}
