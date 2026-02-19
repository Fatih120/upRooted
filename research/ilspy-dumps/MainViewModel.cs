// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Main.MainViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.LogicalTree;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using Microsoft.Extensions.Logging;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls.TitleBars;
using RootApp.Client.Avalonia.Helpers.Focus;
using RootApp.Client.Avalonia.Helpers.Installation;
using RootApp.Client.Avalonia.Helpers.Windows;
using RootApp.Client.Avalonia.Helpers.Zoom;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.Avalonia.UI.Login;
using RootApp.Client.Avalonia.UI.Main;
using RootApp.Client.Avalonia.UI.Messages;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Messages;
using RootApp.Client.CoreDomain.Services;
using RootApp.Client.CoreDomain.Utils.Badges;
using RootApp.Core.Identifiers;

public class MainViewModel : ViewModelBase<MainViewModel>, IOverlayStackTracker
{
	private readonly LoginViewModelFactory _loginFactory;

	private readonly HomeViewModelFactory _homeFactory;

	private readonly IRootService _rootService;

	private readonly MemberProfileViewModelFactory _memberProfileViewModelFactory;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly FocusService _focusService;

	private readonly ActivityTrackerService _activityTrackerService;

	private readonly IAppBadgeService _appBadgeService;

	private readonly IWindowRegistry _windowRegistry;

	private readonly OverlayStackService _overlayStackService;

	private readonly ILogger<MainViewModel> _logger;

	private HashSet<string> _lockedViewModels = new HashSet<string>();

	private Dictionary<string, IViewModelBase> _lockKeyToViewModel = new Dictionary<string, IViewModelBase>();

	[CompilerGenerated]
	private int _003COverlayCount_003Ek__BackingField;

	[CompilerGenerated]
	private Action<int>? m_OverlayCountChanged;

	[CompilerGenerated]
	private bool _003CIsPopupOpen_003Ek__BackingField;

	[CompilerGenerated]
	private MemberProfileViewModel? _003CMemberProfile_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? viewLoadedCommand;

	public int OverlayCount
	{
		[CompilerGenerated]
		get
		{
			return _003COverlayCount_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003COverlayCount_003Ek__BackingField = num;
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsPopupOpen
	{
		get
		{
			return _003CIsPopupOpen_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsPopupOpen_003Ek__BackingField, flag))
			{
				_003CIsPopupOpen_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsPopupOpen);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public MemberProfileViewModel? MemberProfile
	{
		get
		{
			return _003CMemberProfile_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<MemberProfileViewModel>.Default.Equals(_003CMemberProfile_003Ek__BackingField, memberProfileViewModel))
			{
				_003CMemberProfile_003Ek__BackingField = memberProfileViewModel;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.MemberProfile);
			}
		}
	}

	public ObservableCollection<IViewModelBase> ViewModels { get; }

	public IViewModelBase? TitleBarViewModel { get; }

	public ZoomService ZoomService { get; }

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand ViewLoadedCommand => viewLoadedCommand ?? (viewLoadedCommand = new AsyncRelayCommand(ViewLoadedAsync));

	public event Action<int>? OverlayCountChanged
	{
		[CompilerGenerated]
		add
		{
			Action<int> action = this.m_OverlayCountChanged;
			Action<int> action2;
			do
			{
				action2 = action;
				Action<int> action3 = (Action<int>)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_OverlayCountChanged, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action<int> action = this.m_OverlayCountChanged;
			Action<int> action2;
			do
			{
				action2 = action;
				Action<int> action3 = (Action<int>)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_OverlayCountChanged, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public MainViewModel(LoginViewModelFactory P_0, HomeViewModelFactory P_1, IRootService P_2, ILoggerFactory P_3, TitleBarViewModelFactory P_4, MemberProfileViewModelFactory P_5, IRootSessionAccessor P_6, FocusService P_7, ActivityTrackerService P_8, RootInstallationManager P_9, IAppBadgeService P_10, ZoomService P_11, IWindowRegistry P_12, OverlayStackService P_13)
		: base((IValidator<MainViewModel>?)null)
	{
		ZoomService = P_11;
		_loginFactory = P_0;
		_homeFactory = P_1;
		_rootService = P_2;
		_memberProfileViewModelFactory = P_5;
		_rootSessionAccessor = P_6;
		_focusService = P_7;
		_activityTrackerService = P_8;
		_appBadgeService = P_10;
		_windowRegistry = P_12;
		_overlayStackService = P_13;
		_logger = P_3.CreateLogger<MainViewModel>();
		TitleBarViewModel = P_4.Create();
		ViewModels = new ObservableCollection<IViewModelBase>();
		ViewModels.CollectionChanged += OnViewModelsCollectionChanged;
		_overlayStackService.RegisterMainWindowTracker(this);
		WeakReferenceMessenger.Default.Register<PushViewModelToStackMessage>(this, onPushViewModelMessageReceived);
		WeakReferenceMessenger.Default.Register<PushViewModelToMainWindowStackMessage>(this, onPushViewModelToMainWindowMessageReceived);
		WeakReferenceMessenger.Default.Register<PopViewModelFromStackMessage>(this, onPopViewModelMessageReceived);
		WeakReferenceMessenger.Default.Register<PopViewModelsFromStackMessage>(this, onPopViewModelsMessageReceived);
		WeakReferenceMessenger.Default.Register<ToggleViewModelMessage>(this, onToggleViewModelMessageReceived);
		WeakReferenceMessenger.Default.Register<ShowProfileFlyoutAtMousePositionByUrlMessage>(this, onShowProfileFlyoutAtMousePositionByUrlMessageReceived);
		P_9.Start();
	}

	private void OnViewModelsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
	{
		int num = Math.Max(0, ViewModels.Count - 1);
		if (num != OverlayCount)
		{
			OverlayCount = num;
			this.OverlayCountChanged?.Invoke(OverlayCount);
		}
	}

	[RelayCommand]
	public async Task ViewLoadedAsync()
	{
		if (!(await _rootService.ConnectAsync()))
		{
			AddViewModel(_loginFactory.Create());
			_logger.LogInformation("Failed to connect. Need to log in");
		}
		else
		{
			AddViewModel(_homeFactory.Create());
		}
		_focusService.Initialize();
		_activityTrackerService.Initialize();
		IApplicationLifetime applicationLifetime = Application.Current?.ApplicationLifetime;
		if (applicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
		{
			Window mainWindow = desktop.MainWindow;
			if (mainWindow != null)
			{
				_appBadgeService.Initialize(mainWindow);
			}
		}
	}

	public void AddViewModel(IViewModelBase P_0)
	{
		IViewModelBase viewModelBase = ViewModels.FirstOrDefault((IViewModelBase x) => x.IsTopMostViewModel);
		if (viewModelBase != null)
		{
			viewModelBase.IsTopMostViewModel = false;
		}
		P_0.IsTopMostViewModel = true;
		ViewModels.Add(P_0);
	}

	public void RemoveViewModel(IViewModelBase P_0)
	{
		if (P_0.IsTopMostViewModel)
		{
			P_0.IsTopMostViewModel = false;
		}
		ViewModels.Remove(P_0);
		P_0.Dispose();
		IViewModelBase viewModelBase = ViewModels.LastOrDefault();
		if (viewModelBase != null)
		{
			viewModelBase.IsTopMostViewModel = true;
		}
	}

	private void onPushViewModelMessageReceived(object recipient, PushViewModelToStackMessage toStackMessage)
	{
		Window activeWindow = _windowRegistry.ActiveWindow;
		Window mainWindow = _windowRegistry.MainWindow;
		if ((activeWindow == null || activeWindow == mainWindow) && (toStackMessage.LockKey == null || !_lockedViewModels.Contains(toStackMessage.LockKey)))
		{
			if (toStackMessage.LockKey != null)
			{
				_lockedViewModels.Add(toStackMessage.LockKey);
			}
			AddViewModel(toStackMessage.ViewModel);
		}
	}

	private void onPushViewModelToMainWindowMessageReceived(object recipient, PushViewModelToMainWindowStackMessage toStackMessage)
	{
		if (toStackMessage.LockKey != null && _lockedViewModels.Contains(toStackMessage.LockKey))
		{
			return;
		}
		if (toStackMessage.LockKey != null)
		{
			_lockedViewModels.Add(toStackMessage.LockKey);
		}
		Window mainWindow = _windowRegistry.MainWindow;
		if (mainWindow != null)
		{
			if (!mainWindow.IsVisible)
			{
				mainWindow.Show();
			}
			if (mainWindow.WindowState == WindowState.Minimized)
			{
				mainWindow.WindowState = WindowState.Normal;
			}
			mainWindow.Activate();
		}
		AddViewModel(toStackMessage.ViewModel);
	}

	private void onPopViewModelMessageReceived(object recipient, PopViewModelFromStackMessage fromStackMessage)
	{
		if (ViewModels.Contains(fromStackMessage.ViewModel))
		{
			if (fromStackMessage.LockKey != null)
			{
				_lockedViewModels.Remove(fromStackMessage.LockKey);
			}
			RemoveViewModel(fromStackMessage.ViewModel);
		}
	}

	private void onPopViewModelsMessageReceived(object recipient, PopViewModelsFromStackMessage fromStackMessage)
	{
		Window activeWindow = _windowRegistry.ActiveWindow;
		Window mainWindow = _windowRegistry.MainWindow;
		if ((activeWindow != null && activeWindow != mainWindow) || ViewModels.Count == 0)
		{
			return;
		}
		if (fromStackMessage.LockKeys != null)
		{
			string[] lockKeys = fromStackMessage.LockKeys;
			foreach (string text in lockKeys)
			{
				_lockedViewModels.Remove(text);
			}
		}
		List<IViewModelBase> list = ViewModels.TakeLast(fromStackMessage.NumberOfViewModels).ToList();
		foreach (IViewModelBase item in list)
		{
			RemoveViewModel(item);
		}
	}

	private void onToggleViewModelMessageReceived(object recipient, ToggleViewModelMessage message)
	{
		Window activeWindow = _windowRegistry.ActiveWindow;
		Window mainWindow = _windowRegistry.MainWindow;
		if (activeWindow == null || activeWindow == mainWindow)
		{
			if (_lockedViewModels.Contains(message.LockKey) && _lockKeyToViewModel.TryGetValue(message.LockKey, out IViewModelBase value))
			{
				_lockedViewModels.Remove(message.LockKey);
				_lockKeyToViewModel.Remove(message.LockKey);
				RemoveViewModel(value);
			}
			else
			{
				IViewModelBase viewModelBase = message.ViewModelFactory();
				_lockedViewModels.Add(message.LockKey);
				_lockKeyToViewModel[message.LockKey] = viewModelBase;
				AddViewModel(viewModelBase);
			}
		}
	}

	private async void onShowProfileFlyoutAtMousePositionByUrlMessageReceived(object recipient, ShowProfileFlyoutAtMousePositionByUrlMessage message)
	{
		Window activeWindow = _windowRegistry.ActiveWindow;
		Window mainWindow = _windowRegistry.MainWindow;
		if (activeWindow != null && activeWindow != mainWindow)
		{
			return;
		}
		try
		{
			MemberProfile?.Dispose();
			object obj = (message.Hyperlink.Parent?.GetLogicalAncestors().OfType<MessageView>().FirstOrDefault())?.DataContext;
			if (!(obj is MessageViewModel messageViewModel))
			{
				return;
			}
			Uri mentionAsUri = new Uri(message.Url);
			string userIdAsString = mentionAsUri.Segments[^1];
			if (userIdAsString == "me")
			{
				userIdAsString = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id.ToString();
			}
			UserGuid userGuid = RootGuid.Parse<UserGuid>(userIdAsString);
			IMessageContainerMember user = await messageViewModel.Message.MessageContainer.GetMemberAsync(userGuid);
			if (user != null)
			{
				MemberProfile = _memberProfileViewModelFactory.Create(user, delegate
				{
					IsPopupOpen = false;
				});
				IsPopupOpen = true;
			}
		}
		catch
		{
		}
	}
}

