// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.DirectMessageTabViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Threading;
using ReactiveUI.Avalonia;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Popout;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.DirectMessages;
using RootApp.Client.CoreDomain.Models.Tabs;
using RootApp.Core.Identifiers;

public class DirectMessageTabViewModel : ViewModelBase<DirectMessageTabViewModel>, ITabViewModel, IDisposable
{
	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly ILogger<DirectMessageTabViewModel> _logger;

	private readonly DirectMessageContentViewModelFactory _directMessageContentViewModelFactory;

	private readonly LeaveDirectMessageViewModelFactory _leaveDirectMessageViewModelFactory;

	private readonly ITabPopoutService? _tabPopoutService;

	private readonly BitmapCache _bitmapCache;

	private readonly IDisposable? _nameDisposable;

	private readonly IDisposable? _badgeDisposable;

	private readonly IDisposable? _dmRemovedDisposable;

	[CompilerGenerated]
	private string _003CDirectMessageName_003Ek__BackingField = string.Empty;

	private readonly ReadOnlyObservableCollection<MemberBadgeViewModel> _memberBadges;

	private readonly DirectMessageViewModel? _directMessageViewModel;

	[CompilerGenerated]
	private DirectMessage? _003CDirectMessage_003Ek__BackingField;

	[CompilerGenerated]
	private IViewModelBase? _003CContentViewModel_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsOnCall_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CHasActivity_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeTabCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? popoutTabCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? markAsReadCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? leaveConversationCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public DirectMessage? DirectMessage
	{
		get
		{
			return _003CDirectMessage_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<RootApp.Client.CoreDomain.Models.DirectMessages.DirectMessage>.Default.Equals(_003CDirectMessage_003Ek__BackingField, directMessage))
			{
				_003CDirectMessage_003Ek__BackingField = directMessage;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.DirectMessage);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IViewModelBase? ContentViewModel
	{
		get
		{
			return _003CContentViewModel_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<IViewModelBase>.Default.Equals(_003CContentViewModel_003Ek__BackingField, viewModelBase))
			{
				_003CContentViewModel_003Ek__BackingField = viewModelBase;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ContentViewModel);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string DirectMessageName
	{
		get
		{
			return _003CDirectMessageName_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CDirectMessageName_003Ek__BackingField, text))
			{
				_003CDirectMessageName_003Ek__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.DirectMessageName);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsOnCall
	{
		get
		{
			return _003CIsOnCall_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsOnCall_003Ek__BackingField, flag))
			{
				_003CIsOnCall_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsOnCall);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool HasActivity
	{
		get
		{
			return _003CHasActivity_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CHasActivity_003Ek__BackingField, flag))
			{
				_003CHasActivity_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.HasActivity);
			}
		}
	}

	public Tab Tab { get; }

	public ReadOnlyObservableCollection<MemberBadgeViewModel> MemberBadges => _memberBadges;

	public bool CanPopoutTab => _tabPopoutService != null;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseTabCommand => closeTabCommand ?? (closeTabCommand = new RelayCommand(CloseTab));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand PopoutTabCommand => popoutTabCommand ?? (popoutTabCommand = new RelayCommand(PopoutTab, () => CanPopoutTab));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand MarkAsReadCommand => markAsReadCommand ?? (markAsReadCommand = new RelayCommand(MarkAsRead));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand LeaveConversationCommand => leaveConversationCommand ?? (leaveConversationCommand = new RelayCommand(LeaveConversation));

	public DirectMessageTabViewModel(Tab P_0, IRootSessionAccessor P_1, DirectMessageContentViewModelFactory P_2, DirectMessageViewModelFactory P_3, LeaveDirectMessageViewModelFactory P_4, ITabPopoutService? P_5, ILoggerFactory P_6, BitmapCache P_7)
		: base((IValidator<DirectMessageTabViewModel>?)null)
	{
		DirectMessageTabViewModel directMessageTabViewModel = this;
		Tab = P_0;
		_rootSessionAccessor = P_1;
		_logger = P_6.CreateLogger<DirectMessageTabViewModel>();
		_directMessageContentViewModelFactory = P_2;
		_leaveDirectMessageViewModelFactory = P_4;
		_tabPopoutService = P_5;
		_bitmapCache = P_7;
		DirectMessageGuid directMessageGuid = (DirectMessageGuid)Tab.ContainerId.Value;
		DirectMessage = _rootSessionAccessor.Session.DirectMessageService.GetDirectMessage(directMessageGuid);
		if (DirectMessage == null)
		{
			_memberBadges = new ReadOnlyObservableCollection<MemberBadgeViewModel>(new ObservableCollection<MemberBadgeViewModel>());
			Dispatcher.UIThread.Post(delegate
			{
				P_1.Session.TabService.RemoveTab(directMessageTabViewModel.Tab.ContainerId.Value);
			});
			return;
		}
		_nameDisposable = DirectMessage.ConnectDirectMessageName().ObserveOn(AvaloniaScheduler.Instance).Subscribe(delegate(string name)
		{
			directMessageTabViewModel.DirectMessageName = name;
		});
		MemberBadgeViewModelFactory memberBadgeViewModelFactory = new MemberBadgeViewModelFactory(P_7);
		_badgeDisposable = DirectMessage.ConnectMembers().Transform(memberBadgeViewModelFactory.Create).ObserveOn(AvaloniaScheduler.Instance)
			.Bind(out _memberBadges)
			.DisposeMany()
			.Subscribe();
		DirectMessage.PropertyChanged += onDirectMessagePropertyChanged;
		_directMessageViewModel = P_3.Create(DirectMessage, delegate
		{
		});
		ContentViewModel = _directMessageContentViewModelFactory.Create(_directMessageViewModel, null, delegate
		{
		}, false, false, P_5 != null);
		determineIfOnCall();
		updateHasActivity();
		_rootSessionAccessor.Session.ActiveMediaRoomService.PropertyChanged += onActiveMediaRoomServicePropertyChanged;
		_dmRemovedDisposable = _rootSessionAccessor.Session.DirectMessageService.ConnectDirectMessages().ObserveOn(AvaloniaScheduler.Instance).Subscribe(delegate(IChangeSet<DirectMessage> changeSet)
		{
			foreach (Change<DirectMessage> item in changeSet)
			{
				if (item.Reason == ListChangeReason.Remove && item.Item.Current.Id == directMessageTabViewModel.DirectMessage.Id && directMessageTabViewModel.Tab.ContainerId.HasValue)
				{
					directMessageTabViewModel._rootSessionAccessor.Session.TabService.RemoveTab(directMessageTabViewModel.Tab.ContainerId.Value);
					break;
				}
			}
		});
	}

	private void onDirectMessagePropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "HasActivity")
			{
				updateHasActivity();
			}
		});
	}

	private void updateHasActivity()
	{
		HasActivity = DirectMessage?.HasActivity ?? false;
	}

	[RelayCommand]
	public void CloseTab()
	{
		_rootSessionAccessor.Session.TabService.RemoveTab(Tab.ContainerId.Value);
	}

	[RelayCommand(CanExecute = "CanPopoutTab")]
	public void PopoutTab()
	{
		_tabPopoutService?.PopoutTab(this);
	}

	[RelayCommand]
	public void MarkAsRead()
	{
		if (DirectMessage != null)
		{
			DirectMessage.Messages?.SetViewTimeAsync().Forget();
			_rootSessionAccessor.Session.NotificationService.SetContainerAsViewedAsync(DirectMessage.Id, null).Forget();
		}
	}

	[RelayCommand]
	public void LeaveConversation()
	{
		if (DirectMessage != null)
		{
			LeaveDirectMessageViewModel leaveDirectMessageViewModel = _leaveDirectMessageViewModelFactory.Create(DirectMessage.Id);
			WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(leaveDirectMessageViewModel));
		}
	}

	private void onActiveMediaRoomServicePropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "ActiveMediaRoom")
			{
				determineIfOnCall();
			}
		});
	}

	private void determineIfOnCall()
	{
		if (_rootSessionAccessor.Session.ActiveMediaRoomService.ActiveMediaRoom != null && _rootSessionAccessor.Session.ActiveMediaRoomService.ActiveMediaRoom.MessageContainer.ContainerId == DirectMessage.ContainerId)
		{
			IsOnCall = true;
		}
		else
		{
			IsOnCall = false;
		}
	}

	public override void Dispose()
	{
		base.Dispose();
		_nameDisposable?.Dispose();
		_badgeDisposable?.Dispose();
		_dmRemovedDisposable?.Dispose();
		ContentViewModel?.Dispose();
		_directMessageViewModel?.Dispose();
		if (DirectMessage != null)
		{
			DirectMessage.PropertyChanged -= onDirectMessagePropertyChanged;
		}
		_rootSessionAccessor.Session.ActiveMediaRoomService.PropertyChanged -= onActiveMediaRoomServicePropertyChanged;
	}
}

