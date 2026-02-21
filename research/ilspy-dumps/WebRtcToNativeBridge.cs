using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Messaging;
using DotNetBrowser.Frames;
using DotNetBrowser.Js;
using DotNetBrowser.Js.Collections;
using Microsoft.VisualStudio.Threading;
using RootApp.Browser.Models;
using RootApp.Browser.WebRtc.Models;
using RootApp.Browser.WebRtc.Services;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.UI.Home.VoiceBar;
using RootApp.Client.CoreDomain.Models.Media;
using RootApp.Client.CoreDomain.Models.User;
using RootApp.Client.CoreDomain.Services;
using RootApp.Client.Domain.Helpers.Store;
using RootApp.Core.Identifiers;

namespace RootApp.Browser.WebRtc.Bridges;

public class WebRtcToNativeBridge
{
	private readonly IFrame _browserFrame;

	private readonly WebRtcService _webRtcService;

	private readonly ISoundEffectService _soundEffectService;

	private readonly ILocalDataStore _localDataStore;

	private readonly ScreenshareAudioFailedViewModelFactory _screenshareAudioFailedViewModelFactory;

	public IJsObject JsObject { get; }

	public WebRtcToNativeBridge(IFrame P_0, WebRtcService P_1, ISoundEffectService P_2, ILocalDataStore P_3, ScreenshareAudioFailedViewModelFactory P_4)
	{
		_browserFrame = P_0;
		_webRtcService = P_1;
		_soundEffectService = P_2;
		_localDataStore = P_3;
		_screenshareAudioFailedViewModelFactory = P_4;
		JsObject = P_0.ParseJsonString<IJsObject>("{}");
		JsObject.Properties["initialized"] = new Action(initialized);
		JsObject.Properties["remoteLiveMediaTrackStarted"] = new Action(remoteLiveMediaTrackStarted);
		JsObject.Properties["remoteAudioTrackStarted"] = new Action<IJsArray>(remoteAudioTrackStarted);
		JsObject.Properties["remoteLiveMediaTrackStopped"] = new Action(remoteLiveMediaTrackStopped);
		JsObject.Properties["disconnected"] = new Action(disconnected);
		JsObject.Properties["getUserProfile"] = new Func<string, IJsPromise>(getUserProfile);
		JsObject.Properties["getUserProfiles"] = new Func<IJsArray, IJsPromise>(getUserProfiles);
		JsObject.Properties["setSpeaking"] = new Action<bool, string, string>(setSpeaking);
		JsObject.Properties["setHandRaised"] = new Action<bool, string, string>(setHandRaised);
		JsObject.Properties["failed"] = new Action<IJsObject>(failed);
		JsObject.Properties["setAdminMute"] = new Action<string, bool>(setAdminMute);
		JsObject.Properties["setAdminDeafen"] = new Action<string, bool>(setAdminDeafen);
		JsObject.Properties["kickPeer"] = new Action<string>(kickPeer);
		JsObject.Properties["viewProfileMenu"] = new Action<string, IJsObject>(viewProfileMenu);
		JsObject.Properties["viewContextMenu"] = new Action<string, IJsObject, string, double>(viewContextMenu);
		JsObject.Properties["log"] = new Action<string>(log);
		JsObject.Properties["localAudioStarted"] = new Action(localAudioStarted);
		JsObject.Properties["localAudioStopped"] = new Action(localAudioStopped);
		JsObject.Properties["localMuteWasSet"] = new Action<bool>(localMuteWasSet);
		JsObject.Properties["localDeafenWasSet"] = new Action<bool>(localDeafenWasSet);
		JsObject.Properties["localVideoStarted"] = new Action(localVideoStarted);
		JsObject.Properties["localVideoStopped"] = new Action(localVideoStopped);
		JsObject.Properties["localVideoFailed"] = new Action(localVideoFailed);
		JsObject.Properties["localScreenStarted"] = new Action(localScreenStarted);
		JsObject.Properties["localScreenStopped"] = new Action(localScreenStopped);
		JsObject.Properties["localScreenFailed"] = new Action(localScreenFailed);
		JsObject.Properties["localScreenAudioFailed"] = new Action(localScreenAudioFailed);
	}

	public void localAudioStarted()
	{
	}

	public void localAudioStopped()
	{
	}

	public void localMuteWasSet(bool isMuted)
	{
		Dispatcher.UIThread.Post(delegate
		{
			_webRtcService.MediaRoom.SetLocalMuteStatus(isMuted);
		});
	}

	public void localDeafenWasSet(bool isDeafened)
	{
		Dispatcher.UIThread.Post(delegate
		{
			_webRtcService.MediaRoom.SetLocalDeafenStatus(isDeafened);
		});
	}

	public void localVideoStarted()
	{
		Dispatcher.UIThread.Post(delegate
		{
			_webRtcService.MediaRoom.SetLocalVideoStatus(true);
			WeakReferenceMessenger.Default.Send(new VideoEnabledMessage(true));
		});
	}

	public void localVideoStopped()
	{
		Dispatcher.UIThread.Post(delegate
		{
			_webRtcService.MediaRoom.SetLocalVideoStatus(false);
			WeakReferenceMessenger.Default.Send(new VideoEnabledMessage(true));
		});
	}

	public void localVideoFailed()
	{
		Dispatcher.UIThread.Post(delegate
		{
			_webRtcService.MediaRoom.SetLocalVideoStatus(false, false);
			WeakReferenceMessenger.Default.Send(new VideoEnabledMessage(true));
		});
	}

	public void localScreenStarted()
	{
		Dispatcher.UIThread.Post(delegate
		{
			_webRtcService.MediaRoom.SetLocalScreenStatus(true);
			WeakReferenceMessenger.Default.Send(new ScreenshareEnabledMessage(true));
		});
	}

	public void localScreenStopped()
	{
		Dispatcher.UIThread.Post(delegate
		{
			_webRtcService.MediaRoom.SetLocalScreenStatus(false);
			WeakReferenceMessenger.Default.Send(new ScreenshareEnabledMessage(true));
		});
	}

	public void localScreenFailed()
	{
		Dispatcher.UIThread.Post(delegate
		{
			_webRtcService.MediaRoom.SetLocalScreenStatus(false, false);
			WeakReferenceMessenger.Default.Send(new ScreenshareEnabledMessage(true));
		});
	}

	public void localScreenAudioFailed()
	{
		Dispatcher.UIThread.Post(delegate
		{
			_localDataStore.SetGlobal(DataStoreKeys.ScreenshareAudio, Convert.ToInt32(value: false));
			_webRtcService.MediaRoom.SetLocalScreenStatus(false, false);
			WeakReferenceMessenger.Default.Send(new ScreenshareEnabledMessage(true));
			ScreenshareAudioFailedViewModel screenshareAudioFailedViewModel = _screenshareAudioFailedViewModelFactory.Create();
			WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(screenshareAudioFailedViewModel));
		});
	}

	public void initialized()
	{
		_webRtcService.ReleasePackets();
		_webRtcService.Initialized();
		Dispatcher.UIThread.Post(delegate
		{
			_webRtcService.MediaRoom.SetMediaRoomConnectionStatus(MediaRoomConnectionStatus.Connected);
			_soundEffectService.PlayJoinVoice();
			_webRtcService.MediaRoom.SelfMediaMember?.SetFullyConnectedToCall(true);
			WeakReferenceMessenger.Default.Send(new ResetVoiceBarMessage());
		});
	}

	public void disconnected()
	{
		Dispatcher.UIThread.Post(delegate
		{
			_webRtcService.MediaRoom.SetMediaRoomConnectionStatus(MediaRoomConnectionStatus.Disconnected);
			WeakReferenceMessenger.Default.Send(new BrowserDisposingMessage(_webRtcService.WebRtcBrowser.Id));
			Task.Run(delegate
			{
				_webRtcService.WebRtcBrowser.DotNetBrowser.Dispose();
			}).Forget();
		});
	}

	public IJsPromise getUserProfile(string userId)
	{
		return JsPromiseHelper.FromAsync(_browserFrame, async delegate
		{
			GlobalUser globalUser = await _webRtcService.MediaRoom.GetGlobalUserAsync(RootGuid.Parse<UserGuid>(userId));
			return (globalUser != null) ? new UserResponse(globalUser).ToJsObject(_browserFrame) : null;
		});
	}

	public IJsPromise getUserProfiles(IJsArray userIds)
	{
		IReadOnlyList<string> readOnlyList = userIds.ToReadOnlyList<string>();
		IEnumerable<UserGuid> userIdsAsGuids = readOnlyList.Select(RootGuid.Parse<UserGuid>);
		return JsPromiseHelper.FromAsync(_browserFrame, async delegate
		{
			IEnumerable<GlobalUser> members = await _webRtcService.MediaRoom.GetGlobalUsersAsync(userIdsAsGuids);
			return _browserFrame.CreateJsArray(members.Select((GlobalUser member) => new UserResponse(member).ToJsObject(_browserFrame)));
		});
	}

	public void setSpeaking(bool isSpeaking, string deviceId, string userId)
	{
		UserGuid userIdAsGuid = RootGuid.Parse<UserGuid>(userId);
		Dispatcher.UIThread.Post(delegate
		{
			_webRtcService.MediaRoom.GetMemberByUserId(userIdAsGuid)?.SetSpeakingFlag(isSpeaking);
		});
	}

	public void setHandRaised(bool isHandRaised, string deviceId, string userId)
	{
	}

	public void failed(IJsObject error)
	{
		switch (int.Parse(error.Properties["clientErrorAction"].ToString() ?? "0"))
		{
		case 0:
			_webRtcService.MediaRoom.SetMediaRoomConnectionStatus(MediaRoomConnectionStatus.Error);
			break;
		case 1:
			_webRtcService.CallingService.JoinVoiceCallAsync(_webRtcService.MediaRoom.MessageContainer.ContainerId, _webRtcService.MediaRoom, true).Forget();
			WeakReferenceMessenger.Default.Send(new ResetVoiceBarMessage());
			break;
		case 2:
			Task.Run(async delegate
			{
				await Task.Delay(5000);
				await _webRtcService.CallingService.JoinVoiceCallAsync(_webRtcService.MediaRoom.MessageContainer.ContainerId, _webRtcService.MediaRoom, true);
			}).Forget();
			WeakReferenceMessenger.Default.Send(new ResetVoiceBarMessage());
			break;
		case 3:
			break;
		}
	}

	public void log(string message)
	{
	}

	public void setAdminMute(string deviceId, bool isMuted)
	{
	}

	public void setAdminDeafen(string deviceId, bool isDeafened)
	{
	}

	public void kickPeer(string userId)
	{
	}

	public void remoteLiveMediaTrackStarted()
	{
	}

	public void remoteAudioTrackStarted(IJsArray userIdsJsArray)
	{
		Dispatcher.UIThread.Post(delegate
		{
			_soundEffectService.PlayJoinVoice();
			IReadOnlyList<string> readOnlyList = userIdsJsArray.ToReadOnlyList<string>();
			IEnumerable<UserGuid> enumerable = readOnlyList.Select(RootGuid.Parse<UserGuid>);
			foreach (UserGuid item in enumerable)
			{
				_webRtcService.MediaRoom.GetMemberByUserId(item)?.SetFullyConnectedToCall(true);
			}
		});
	}

	public void remoteLiveMediaTrackStopped()
	{
	}

	public void viewProfileMenu(string userId, IJsObject coordinates)
	{
		UserGuid userIdAsGuid = RootGuid.Parse<UserGuid>(userId);
		double x = double.Parse(coordinates.Properties["x"].ToString() ?? "0");
		double y = double.Parse(coordinates.Properties["y"].ToString() ?? "0");
		Dispatcher.UIThread.Post(delegate
		{
			WeakReferenceMessenger.Default.Send(new ShowProfileFlyoutAtPositionByUserAndContainerMessage(userIdAsGuid, _webRtcService.MediaRoom.MessageContainer, x, y));
		});
	}

	public void viewContextMenu(string userId, IJsObject coordinates, string tileType, double volume)
	{
		UserGuid userIdAsGuid = RootGuid.Parse<UserGuid>(userId);
		double x = double.Parse(coordinates.Properties["x"].ToString() ?? "0");
		double y = double.Parse(coordinates.Properties["y"].ToString() ?? "0");
		Dispatcher.UIThread.Post(delegate
		{
			WeakReferenceMessenger.Default.Send(new ShowUserContextMenuAtPositionMessage(userIdAsGuid, _webRtcService.MediaRoom.MessageContainer, x, y, tileType != "primary"));
		});
	}
}
