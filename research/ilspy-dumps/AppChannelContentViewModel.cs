using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using RootApp.Browser;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.RootApps;
using RootApp.Client.Avalonia.Helpers.RootApps.Browser;
using RootApp.Client.Avalonia.Helpers.Windows;
using RootApp.Client.Avalonia.Helpers.Zoom;
using RootApp.Client.CoreDomain.Models.Community;

namespace RootApp.Client.Avalonia.UI.Community.Content;

public class AppChannelContentViewModel : CachedViewModelBase
{
	private readonly BrowserService _browserService;

	private readonly IBrowserVisibilityTracker _visibilityTracker;

	private readonly IAppChannelService _appChannelService;

	private readonly BitmapCache _bitmapCache;

	private readonly OverlayStackService _overlayStackService;

	private IDisposable? _statusSubscription;

	private IOverlayStackTracker? _overlayTracker;

	private bool _isDisposed;

	[CompilerGenerated]
	private bool <ShouldRenderBrowser>k__BackingField = true;

	[CompilerGenerated]
	private IRootBrowser? <Browser>k__BackingField;

	[CompilerGenerated]
	private Action? m_BrowserRecreated;

	[CompilerGenerated]
	private Channel <Channel>k__BackingField;

	[CompilerGenerated]
	private bool <IsBrowserDisposed>k__BackingField;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Channel Channel
	{
		get
		{
			return <Channel>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<RootApp.Client.CoreDomain.Models.Community.Channel>.Default.Equals(<Channel>k__BackingField, channel))
			{
				<Channel>k__BackingField = channel;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Channel);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ShouldRenderBrowser
	{
		get
		{
			return <ShouldRenderBrowser>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<ShouldRenderBrowser>k__BackingField, flag))
			{
				<ShouldRenderBrowser>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShouldRenderBrowser);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsBrowserDisposed
	{
		get
		{
			return <IsBrowserDisposed>k__BackingField;
		}
		private set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsBrowserDisposed>k__BackingField, flag))
			{
				<IsBrowserDisposed>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsBrowserDisposed);
			}
		}
	}

	public IRootBrowser? Browser
	{
		[CompilerGenerated]
		get
		{
			return <Browser>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<Browser>k__BackingField = rootBrowser;
		}
	}

	public ZoomService ZoomService { get; }

	public event Action? BrowserRecreated
	{
		[CompilerGenerated]
		add
		{
			Action action = this.m_BrowserRecreated;
			Action action2;
			do
			{
				action2 = action;
				Action action3 = (Action)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_BrowserRecreated, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action action = this.m_BrowserRecreated;
			Action action2;
			do
			{
				action2 = action;
				Action action3 = (Action)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_BrowserRecreated, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public AppChannelContentViewModel(Channel P_0, BrowserService P_1, IBrowserVisibilityTracker P_2, IAppChannelService P_3, BitmapCache P_4, ZoomService P_5, OverlayStackService P_6)
	{
		_browserService = P_1;
		_visibilityTracker = P_2;
		_appChannelService = P_3;
		_bitmapCache = P_4;
		_overlayStackService = P_6;
		Channel = P_0;
		ZoomService = P_5;
		Channel.PropertyChanged += onChannelPropertyChanged;
		Browser = _browserService.GetBrowser(Channel.Id);
		_statusSubscription = _appChannelService.ObserveStatus(P_0.Id).Subscribe(OnChannelStatusChanged);
		SubscribeToOverlayTracker();
	}

	private void SubscribeToOverlayTracker()
	{
		_overlayTracker = _overlayStackService.GetTrackerForContainer(Channel.Community.Id);
		if (_overlayTracker != null)
		{
			_overlayTracker.OverlayCountChanged += OnOverlayCountChanged;
			ShouldRenderBrowser = _overlayTracker.OverlayCount == 0;
		}
	}

	private void OnOverlayCountChanged(int overlayCount)
	{
		Dispatcher.UIThread.Post(delegate
		{
			ShouldRenderBrowser = overlayCount == 0;
		});
	}

	private void OnChannelStatusChanged(AppChannelStatus status)
	{
		Dispatcher.UIThread.Post(delegate
		{
			IsBrowserDisposed = status.IsBrowserDisposed;
			if (status.IsBrowserDisposed)
			{
				Browser = null;
				OnPropertyChanged("Browser");
			}
			else if (status.IsReady && Browser == null)
			{
				Browser = _browserService.GetBrowser(Channel.Id);
				OnPropertyChanged("Browser");
				this.BrowserRecreated?.Invoke();
			}
		});
	}

	public void OnBrowserBecameVisible()
	{
		_visibilityTracker.OnBrowserVisible(Channel.Id);
		if (IsBrowserDisposed)
		{
			RecreateBrowserAsync();
		}
	}

	public void OnBrowserBecameInvisible()
	{
		if (!_isDisposed && Browser != null && !IsBrowserDisposed)
		{
			_visibilityTracker.OnBrowserInvisible(Channel.Id);
		}
	}

	private async Task RecreateBrowserAsync()
	{
		IRootBrowser newBrowser = await _appChannelService.RecreateBrowserAsync(Channel);
		if (newBrowser != null)
		{
			Dispatcher.UIThread.Post(delegate
			{
				Browser = newBrowser;
				IsBrowserDisposed = false;
				OnPropertyChanged("Browser");
				this.BrowserRecreated?.Invoke();
			});
		}
	}

	private void onChannelPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "IconAssetUri")
			{
				OnPropertyChanged("ChannelIconAsyncBitmapWrapper");
			}
		});
	}

	public override void Dispose()
	{
		_isDisposed = true;
		base.Dispose();
		if (_overlayTracker != null)
		{
			_overlayTracker.OverlayCountChanged -= OnOverlayCountChanged;
			_overlayTracker = null;
		}
		_visibilityTracker.CancelCleanup(Channel.Id);
		_statusSubscription?.Dispose();
		_statusSubscription = null;
		Channel.PropertyChanged -= onChannelPropertyChanged;
	}
}
