using System;
using System.Collections.Generic;
using System.Linq;
using DotNetBrowser.Frames;
using DotNetBrowser.Js;
using DotNetBrowser.Js.Collections;
using RootApp.Browser.Models;
using RootApp.Browser.RootApps.Models;
using RootApp.Browser.RootApps.Services;
using RootApp.Client.CoreDomain.Models.Community;

namespace RootApp.Browser.RootApps.Bridges;

public class AppToNativePrivateBridge
{
	private readonly IFrame _browserFrame;

	private readonly RootAppService _rootAppService;

	public IJsObject JsObject { get; }

	public AppToNativePrivateBridge(IFrame P_0, RootAppService P_1)
	{
		_browserFrame = P_0;
		_rootAppService = P_1;
		JsObject = P_0.ParseJsonString<IJsObject>("{}");
		JsObject.Properties["send"] = new Action<IJsArrayBuffer>(send);
		JsObject.Properties["listSuggestedUserProfiles"] = new Func<IJsPromise>(listSuggestedUserProfiles);
		JsObject.Properties["searchUserProfiles"] = new Func<string, IJsPromise>(searchUserProfiles);
		JsObject.Properties["listCommunityRoles"] = new Func<IJsPromise>(listCommunityRoles);
		JsObject.Properties["uriToUrl"] = new Func<string, string>(uriToUrl);
		JsObject.Properties["uriToImageUrl"] = new Func<string, string, string>(uriToImageUrl);
		JsObject.Properties["uploadTokenToPreviewImageUrl"] = new Func<string, string>(uploadTokenToPreviewImageUrl);
	}

	public void send(IJsArrayBuffer buffer)
	{
		byte[] array = buffer.ToByteArray();
		_rootAppService.SendMessage(array);
	}

	public IJsPromise listSuggestedUserProfiles()
	{
		IEnumerable<Member> obj = _rootAppService.Community.Members?.FindMembers("").Take(10);
		return JsPromiseHelper.FromResult(_browserFrame, _browserFrame.CreateJsArray(obj?.Select((Member member) => new UserProfile(member).ToJsObject(_browserFrame))));
	}

	public IJsPromise searchUserProfiles(string partialNickname)
	{
		IEnumerable<Member> obj = _rootAppService.Community.Members?.FindMembers(partialNickname).Take(10);
		return JsPromiseHelper.FromResult(_browserFrame, _browserFrame.CreateJsArray(obj?.Select((Member member) => new UserProfile(member).ToJsObject(_browserFrame))));
	}

	public IJsPromise listCommunityRoles()
	{
		IEnumerable<Role> obj = _rootAppService.Community.Roles?.FindRoles("");
		return JsPromiseHelper.FromResult(_browserFrame, _browserFrame.CreateJsArray(obj?.Select((Role role) => new CommunityRole(role).ToJsObject(_browserFrame))));
	}

	public string uriToUrl(string uri)
	{
		return uri;
	}

	public string uriToImageUrl(string uri, string imageOptions)
	{
		return uri + "?imageOptions=" + imageOptions;
	}

	public string uploadTokenToPreviewImageUrl(string token)
	{
		_rootAppService.AppImagePreviewCache.TryGet(token, out string text);
		if (!string.IsNullOrEmpty(text))
		{
			Guid guid = Guid.NewGuid();
			_rootAppService.AppImagePreviewCache.Set(guid.ToString(), text);
			return $"rootapp://previewimage/{guid}";
		}
		return string.Empty;
	}
}
