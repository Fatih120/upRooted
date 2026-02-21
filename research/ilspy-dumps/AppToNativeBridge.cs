using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Threading;
using DotNetBrowser.Frames;
using DotNetBrowser.Js;
using DotNetBrowser.Js.Collections;
using Microsoft.VisualStudio.Threading;
using RootApp.Browser.Models;
using RootApp.Browser.RootApps.Models;
using RootApp.Browser.RootApps.Services;
using RootApp.Client.Avalonia.Resources.Themes;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Core.Identifiers;

namespace RootApp.Browser.RootApps.Bridges;

public class AppToNativeBridge
{
	private readonly IFrame _browserFrame;

	private readonly RootAppService _rootAppService;

	public IJsObject JsObject { get; }

	public AppToNativeBridge(IFrame P_0, RootAppService P_1)
	{
		_browserFrame = P_0;
		_rootAppService = P_1;
		JsObject = P_0.ParseJsonString<IJsObject>("{}");
		JsObject.Properties["getTheme"] = new Func<string>(getTheme);
		JsObject.Properties["getCurrentUserId"] = new Func<string>(getCurrentUserId);
		JsObject.Properties["getUserProfile"] = new Func<string, IJsPromise>(getUserProfile);
		JsObject.Properties["getUserProfiles"] = new Func<IJsArray, IJsPromise>(getUserProfiles);
		JsObject.Properties["fileUpload"] = new Func<IJsObject, IJsPromise>(fileUpload);
	}

	public string getTheme()
	{
		return Dispatcher.UIThread.Invoke(() => ThemeMapper.ToAppBridgeThemeString(Application.Current.ActualThemeVariant));
	}

	public string getCurrentUserId()
	{
		Member member = _rootAppService.Community.Members?.GetSelfMember();
		if (member != null)
		{
			return member.UserId.ToString("s");
		}
		return string.Empty;
	}

	public IJsPromise getUserProfile(string userId)
	{
		if (_rootAppService.Community.Members == null)
		{
			return null;
		}
		return JsPromiseHelper.FromAsync(_browserFrame, async delegate
		{
			Member member = await _rootAppService.Community.Members.GetMemberAsync(RootGuid.Parse<UserGuid>(userId));
			return (member != null) ? new UserProfile(member).ToJsObject(_browserFrame) : null;
		});
	}

	public IJsPromise getUserProfiles(IJsArray userIds)
	{
		if (_rootAppService.Community.Members == null)
		{
			return null;
		}
		IReadOnlyList<string> readOnlyList = userIds.ToReadOnlyList<string>();
		IEnumerable<UserGuid> userIdsAsGuids = readOnlyList.Select(RootGuid.Parse<UserGuid>);
		return JsPromiseHelper.FromAsync(_browserFrame, async delegate
		{
			IEnumerable<Member> members = await _rootAppService.Community.Members.GetMembersAsync(userIdsAsGuids);
			return _browserFrame.CreateJsArray(members.Select((Member member) => new UserProfile(member).ToJsObject(_browserFrame)));
		});
	}

	public IJsPromise fileUpload(IJsObject request)
	{
		string fileType = (request.Properties.Contains("fileType") ? request.Properties["fileType"].ToString() : null);
		TaskCompletionSource<IJsObject> tcs;
		if (fileType != null)
		{
			bool multiple = request.Properties.Contains("multiple") && (bool)request.Properties["multiple"];
			string windowTitle = (request.Properties.Contains("windowTitle") ? request.Properties["windowTitle"].ToString() : string.Empty);
			tcs = new TaskCompletionSource<IJsObject>();
			Dispatcher.UIThread.InvokeAsync(() => _rootAppService.AppAssetFileUploader.OpenFilePickerAsync(fileType, windowTitle ?? string.Empty, multiple, completedCallback)).Forget();
			return JsPromiseHelper.FromAsync(_browserFrame, () => tcs.Task);
		}
		return null;
		void completedCallback(IEnumerable<string>? tokens)
		{
			if (tokens == null)
			{
				tcs.SetResult(null);
			}
			else
			{
				FileUploadResponse fileUploadResponse = new FileUploadResponse
				{
					tokens = tokens.ToArray()
				};
				tcs.SetResult(fileUploadResponse.ToJsObject(_browserFrame));
			}
		}
	}
}
