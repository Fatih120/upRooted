using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Threading;
using DotNetBrowser.Browser;
using DotNetBrowser.Frames;
using DotNetBrowser.Js;
using DotNetBrowser.Navigation;
using Microsoft.VisualStudio.Threading;
using RootApp.Browser.RootApps.Services;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Core.Identifiers;

namespace RootApp.Browser;

public class RootAppBrowser : IRootBrowser, IDisposable
{
	private readonly RootAppServiceFactory _rootAppServiceFactory;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly IJsObject? _prewarmedWindow;

	private readonly IFrame? _prewarmedIFrameEnv;

	[CompilerGenerated]
	private readonly string <VersionId>k__BackingField;

	[CompilerGenerated]
	private RootAppService <RootApp>k__BackingField;

	public AppGuid AppId { get; }

	public RootGuid Id { get; }

	public CommunityGuid CommunityId { get; }

	public IBrowser DotNetBrowser { get; }

	public RootAppService RootApp
	{
		[CompilerGenerated]
		get
		{
			return <RootApp>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<RootApp>k__BackingField = rootAppService;
		}
	} = null;

	public RootAppBrowser(AppGuid P_0, string P_1, CommunityAppGuid P_2, CommunityGuid P_3, IBrowser P_4, RootAppServiceFactory P_5, IRootSessionAccessor P_6)
	{
		_rootAppServiceFactory = P_5;
		_rootSessionAccessor = P_6;
		AppId = P_0;
		<VersionId>k__BackingField = P_1;
		Id = P_2;
		CommunityId = P_3;
		DotNetBrowser = P_4;
		DotNetBrowser.UserAgent += $" AppId={P_0} AppVersionId={P_1}";
	}

	public RootAppBrowser(AppGuid P_0, string P_1, CommunityAppGuid P_2, CommunityGuid P_3, WarmBrowser P_4, RootAppServiceFactory P_5, IRootSessionAccessor P_6)
	{
		_rootAppServiceFactory = P_5;
		_rootSessionAccessor = P_6;
		_prewarmedWindow = P_4.Window;
		_prewarmedIFrameEnv = P_4.IFrameExecutionEnvironment;
		AppId = P_0;
		<VersionId>k__BackingField = P_1;
		Id = P_2;
		CommunityId = P_3;
		DotNetBrowser = P_4.DotNetBrowser;
		DotNetBrowser.UserAgent += $" AppId={P_0} AppVersionId={P_1}";
	}

	public async Task InitializeAsync()
	{
		Community community = _rootSessionAccessor.Session.CommunityService.GetCommunity(CommunityId);
		if (!(community?.IsFullyLoaded ?? false))
		{
			throw new Exception("Community is not fully loaded.");
		}
		IJsObject window;
		IFrame iFrameExecutionEnvironment;
		if (_prewarmedWindow != null && _prewarmedIFrameEnv != null)
		{
			window = _prewarmedWindow;
			iFrameExecutionEnvironment = _prewarmedIFrameEnv;
		}
		else
		{
			string filePath = "rootapp://app/__index.html";
			string url = new Uri(filePath).ToString();
			if ((await DotNetBrowser.Navigation.LoadUrl(url)).LoadResult != LoadResult.Completed)
			{
				throw new Exception("Failed to load RootApp browser.");
			}
			await DotNetBrowser.MainFrame.ExecuteJavaScript("\r\n                document.addEventListener('dragover', function(e) {\r\n                    e.preventDefault();\r\n                }, false);\r\n                document.addEventListener('drop', function(e) {\r\n                    e.preventDefault();\r\n                }, false);\r\n            ");
			window = await DotNetBrowser.MainFrame.ExecuteJavaScript<IJsObject>("window", userGesture: true);
			iFrameExecutionEnvironment = DotNetBrowser.AllFrames.First((IFrame f) => !f.IsMain);
		}
		RootApp = _rootAppServiceFactory.Create(community, window, this);
		await RootApp.WaitForClientAttachAsync();
		string iFramePath = "rootapp://app/";
		if ((await iFrameExecutionEnvironment.LoadUrl(iFramePath)).LoadResult == LoadResult.Completed)
		{
			await iFrameExecutionEnvironment.ExecuteJavaScript("\r\n                document.addEventListener('dragover', function(e) {\r\n                    e.preventDefault();\r\n                }, false);\r\n                document.addEventListener('drop', function(e) {\r\n                    e.preventDefault();\r\n                }, false);\r\n            ");
			IJsObject iFrameWindow = await iFrameExecutionEnvironment.ExecuteJavaScript<IJsObject>("window", userGesture: true);
			RootApp.InitializeIFrame(iFrameWindow);
		}
	}

	public void Dispose()
	{
		if (Dispatcher.UIThread.CheckAccess())
		{
			RootApp.Dispose();
		}
		else
		{
			Dispatcher.UIThread.Invoke(delegate
			{
				RootApp.Dispose();
			});
		}
		Task.Run(delegate
		{
			try
			{
				DotNetBrowser.Dispose();
			}
			catch
			{
			}
		}).Forget();
	}
}
