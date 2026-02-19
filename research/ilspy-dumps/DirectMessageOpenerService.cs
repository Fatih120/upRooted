// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Helpers.Navigation.DirectMessageOpenerService
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using Grpc.Core;
using RootApp.Browser;
using RootApp.Client.Avalonia.Helpers.Calling;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.DirectMessages;
using RootApp.Client.CoreDomain.Models.User;
using RootApp.Core.Identifiers;

public class DirectMessageOpenerService(IRootSessionAccessor P_0, CallingServiceFactory P_1, BrowserService P_2, PrivacyBlockedActionViewModelFactory P_3)
{
	public async Task OpenDirectMessageAsync(GlobalUser P_0, string P_1 = "", bool P_2 = false)
	{
		DirectMessage directMessage = P_0.Session.DirectMessageService.GetOrCreateDraftDirectMessage(new List<GlobalUser>(1) { P_0 });
		if ((!string.IsNullOrEmpty(P_1) || P_2) && directMessage.IsDraft)
		{
			try
			{
				await P_0.Session.DirectMessageService.CreateDirectMessageAsync(directMessage);
			}
			catch (RpcException ex) when (ex.StatusCode == StatusCode.Unauthenticated)
			{
				P_3.Show(PrivacyBlockedActionType.DirectMessage);
				P_0.Session.DirectMessageService.RemoveDraftDirectMessage(directMessage);
				return;
			}
			catch
			{
				return;
			}
		}
		if (!string.IsNullOrEmpty(P_1))
		{
			await directMessage.Messages.CreateMessageAsync(P_1, Array.Empty<string>(), null, false);
		}
		if (P_2)
		{
			CallingService callingService = P_1.Create(P_2);
			await callingService.JoinVoiceCallAsync(directMessage.Id, directMessage.MediaRoom);
		}
		CheckPopoutFocusMessage checkMessage = new CheckPopoutFocusMessage(directMessage.Id, null, null, P_2);
		WeakReferenceMessenger.Default.Send(checkMessage);
		if (checkMessage.WasHandled)
		{
			if (P_2)
			{
				WeakReferenceMessenger.Default.Send(new FocusDirectMessageCallMessage(directMessage.Id));
			}
		}
		else if (P_0.Session.TabService.ContainsTab(directMessage.Id))
		{
			WeakReferenceMessenger.Default.Send(new SelectTabMessage(directMessage.Id));
			if (P_2)
			{
				WeakReferenceMessenger.Default.Send(new FocusDirectMessageCallMessage(directMessage.Id));
			}
		}
		else
		{
			WeakReferenceMessenger.Default.Send(new OpenDirectMessagePaneMessage(directMessage.Id, null, P_2));
		}
	}

	public void OpenDirectMessage(DirectMessageGuid P_0, MessageGuid? P_1 = null, bool P_2 = false)
	{
		DirectMessage directMessage = P_0.Session.DirectMessageService.GetDirectMessage(P_0);
		if (directMessage == null)
		{
			return;
		}
		CheckPopoutFocusMessage checkPopoutFocusMessage = new CheckPopoutFocusMessage(directMessage.Id, null, P_1, P_2);
		WeakReferenceMessenger.Default.Send(checkPopoutFocusMessage);
		if (checkPopoutFocusMessage.WasHandled)
		{
			if (P_1 != null)
			{
				WeakReferenceMessenger.Default.Send(new FocusDirectMessageMessage(directMessage.Id, P_1.Value));
			}
			if (P_2)
			{
				WeakReferenceMessenger.Default.Send(new FocusDirectMessageCallMessage(directMessage.Id));
			}
		}
		else if (P_0.Session.TabService.ContainsTab(directMessage.Id))
		{
			WeakReferenceMessenger.Default.Send(new SelectTabMessage(directMessage.Id));
			if (P_1 != null)
			{
				WeakReferenceMessenger.Default.Send(new FocusDirectMessageMessage(directMessage.Id, P_1.Value));
			}
			if (P_2)
			{
				WeakReferenceMessenger.Default.Send(new FocusDirectMessageCallMessage(directMessage.Id));
			}
		}
		else
		{
			WeakReferenceMessenger.Default.Send(new OpenDirectMessagePaneMessage(directMessage.Id, P_1, P_2));
		}
	}
}

