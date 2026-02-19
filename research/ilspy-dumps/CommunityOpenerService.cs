// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Helpers.Navigation.CommunityOpenerService
using System;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using RootApp.Client.Avalonia.Helpers.Navigation;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.CoreDomain;
using RootApp.Core.Identifiers;

public class CommunityOpenerService
{
	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly ILogger<CommunityOpenerService> _logger;

	public CommunityOpenerService(IRootSessionAccessor P_0, ILoggerFactory P_1)
	{
		_rootSessionAccessor = P_0;
		_logger = P_1.CreateLogger<CommunityOpenerService>();
	}

	public void OpenCommunity(CommunityGuid P_0, ChannelGuid? P_1 = null, MessageGuid? P_2 = null)
	{
		try
		{
			CheckPopoutFocusMessage checkPopoutFocusMessage = new CheckPopoutFocusMessage(P_0, P_1, P_2);
			WeakReferenceMessenger.Default.Send(checkPopoutFocusMessage);
			if (checkPopoutFocusMessage.WasHandled)
			{
				return;
			}
			if (_rootSessionAccessor.Session.TabService.ContainsTab(P_0))
			{
				WeakReferenceMessenger.Default.Send(new SelectTabMessage(P_0));
				if (P_1 != null)
				{
					WeakReferenceMessenger.Default.Send(new SelectChannelMessage(P_0, P_1.Value, P_2));
				}
				if (_rootSessionAccessor.Session.TabService.ContainsTab(null))
				{
					_rootSessionAccessor.Session.TabService.RemoveTab(null);
				}
			}
			else if (_rootSessionAccessor.Session.TabService.ContainsTab(null))
			{
				_rootSessionAccessor.Session.TabService.ReplaceNewTab(P_0, P_1, P_2);
			}
			else
			{
				_rootSessionAccessor.Session.TabService.AddCommunityTab(P_0, P_1, P_2);
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Failed to open community");
		}
	}
}

