using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Threading;
using RootApp.Browser;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.DirectMessages;
using RootApp.Client.CoreDomain.Models.Media;
using RootApp.Client.CoreDomain.Models.Messages;
using RootApp.Core.Identifiers;

namespace RootApp.Client.Avalonia.Helpers.Calling;

public class CallingService
{
	private readonly BrowserService _browserService;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly SemaphoreSlim _joinLock = new SemaphoreSlim(1, 1);

	public CallingService(BrowserService P_0, IRootSessionAccessor P_1)
	{
		_browserService = P_0;
		_rootSessionAccessor = P_1;
	}

	public async Task JoinVoiceCallAsync(MessageContainerGuid P_0, MediaRoom P_1, bool P_2 = false)
	{
		await _joinLock.WaitAsync();
		try
		{
			await JoinVoiceCallCoreAsync(P_0, P_1, P_2);
		}
		finally
		{
			_joinLock.Release();
		}
	}

	private async Task JoinVoiceCallCoreAsync(MessageContainerGuid P_0, MediaRoom P_1, bool P_2)
	{
		MediaRoom activeMediaRoom = _rootSessionAccessor.Session.ActiveMediaRoomService.ActiveMediaRoom;
		if (activeMediaRoom != null)
		{
			if (activeMediaRoom.MessageContainer.ContainerId == P_0 && !P_2)
			{
				return;
			}
			_browserService.RemoveBrowser(activeMediaRoom.MessageContainer.ContainerId);
		}
		WebRtcBrowser newBrowser = await _browserService.CreateWebRtcBrowserAsync(P_0, P_1);
		if (newBrowser == null)
		{
			return;
		}
		string[] ringingUserIds = null;
		int? ringTimeoutMs = null;
		if (P_1.MessageContainer.CommunityId == null && !P_1.HasActiveCall)
		{
			IMessageContainer messageContainer = P_1.MessageContainer;
			if (messageContainer is DirectMessage directMessage)
			{
				ringingUserIds = (from userId in directMessage.GetMemberUserIdsExcludingSelf()
					select userId.ToString("s")).ToArray();
				ringTimeoutMs = 28000;
			}
		}
		newBrowser.WebRtc.StartCallAsync(ringingUserIds, ringTimeoutMs).Forget();
	}
}
