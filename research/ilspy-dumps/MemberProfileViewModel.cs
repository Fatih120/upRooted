// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.MemberProfileViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using FluentValidation;
using Grpc.Core;
using Microsoft.VisualStudio.Threading;
using ReactiveUI.Avalonia;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls.ContextMenus;
using RootApp.Client.Avalonia.Helpers.Badges;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.DeveloperMode;
using RootApp.Client.Avalonia.Helpers.Navigation;
using RootApp.Client.Avalonia.Helpers.StreamerMode;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.Avalonia.UI.Community.Roles;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Client.CoreDomain.Models.Messages;
using RootApp.WebApi.Shared.Packets;

public class MemberProfileViewModel : ViewModelBase<MemberProfileViewModel>
{
	private readonly IDisposable? _cacheCleanup;

	private readonly Action _closeProfileCallback;

	private readonly BitmapCache _bitmapCache;

	private readonly DirectMessageOpenerService _directMessageOpenerService;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly PrivacyBlockedActionViewModelFactory _privacyBlockedActionViewModelFactory;

	private readonly IStreamerModeService _streamerModeService;

	private readonly IDeveloperModeService _developerModeService;

	private bool _firstNoteLoaded;

	private bool _noteChanged;

	private readonly Subject<IChangeSet<IViewModelBase>> _addRoleSubject = new Subject<IChangeSet<IViewModelBase>>();

	private readonly AddRoleButtonViewModel? _addRoleButtonViewModel;

	private bool _addRoleButtonIsVisible;

	private readonly ReadOnlyObservableCollection<IViewModelBase>? _roles;

	[CompilerGenerated]
	private bool _003CFriendRequestSent_003Ek__BackingField;

	[CompilerGenerated]
	private string? _003CNote_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CShouldHideNotes_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<string>? sendMessageCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? sendFriendRequestCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<string>? closeProfileCallbackCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? callCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? unloadedCommand;

	public ObservableCollection<MemberBadgeDisplay> BadgeDisplays { get; } = new ObservableCollection<MemberBadgeDisplay>();

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool FriendRequestSent
	{
		get
		{
			return _003CFriendRequestSent_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CFriendRequestSent_003Ek__BackingField, flag))
			{
				_003CFriendRequestSent_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.FriendRequestSent);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string? Note
	{
		get
		{
			return _003CNote_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CNote_003Ek__BackingField, text))
			{
				_003CNote_003Ek__BackingField = text;
				OnNoteChanged(text);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Note);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ShouldHideNotes
	{
		get
		{
			return _003CShouldHideNotes_003Ek__BackingField;
		}
		private set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CShouldHideNotes_003Ek__BackingField, flag))
			{
				_003CShouldHideNotes_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShouldHideNotes);
			}
		}
	}

	public IMessageContainerMember MessageContainerMember { get; }

	public Member? CommunityMember { get; }

	public bool IsSelf { get; }

	public Lazy<UserContextMenuViewModel> UserContextMenu { get; }

	public bool DeveloperModeEnabled => _developerModeService.IsEnabled;

	public bool ShowContextMenuButton => (!IsSelf && !MessageContainerMember.IsApp) || (IsSelf && DeveloperModeEnabled);

	public ReadOnlyObservableCollection<IViewModelBase>? Roles => _roles;

	public Task<BitmapWrapper?> ProfilePictureAsyncBitmapWrapper => _bitmapCache.GetBitmapAsync(MessageContainerMember.GlobalUser.ProfilePictureUri, null, 120);

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<string> SendMessageCommand => sendMessageCommand ?? (sendMessageCommand = new RelayCommand<string>(SendMessage));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand SendFriendRequestCommand => sendFriendRequestCommand ?? (sendFriendRequestCommand = new AsyncRelayCommand(SendFriendRequestAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<string> CloseProfileCallbackCommand => closeProfileCallbackCommand ?? (closeProfileCallbackCommand = new RelayCommand<string>(CloseProfileCallback));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CallCommand => callCommand ?? (callCommand = new RelayCommand(Call));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand UnloadedCommand => unloadedCommand ?? (unloadedCommand = new RelayCommand(Unloaded));

	public MemberProfileViewModel(IMessageContainerMember P_0, Action P_1, RoleTagViewModelFactory P_2, BitmapCache P_3, DirectMessageOpenerService P_4, AddRoleButtonViewModelFactory P_5, IRootSessionAccessor P_6, UserContextMenuViewModelFactory P_7, PrivacyBlockedActionViewModelFactory P_8, IStreamerModeService P_9, IDeveloperModeService P_10)
		: base((IValidator<MemberProfileViewModel>?)null)
	{
		MemberProfileViewModel memberProfileViewModel = this;
		MessageContainerMember = P_0;
		MessageContainerMember.GlobalUser.PropertyChanged += onGlobalUserPropertyChanged;
		_closeProfileCallback = P_1;
		_bitmapCache = P_3;
		_directMessageOpenerService = P_4;
		_rootSessionAccessor = P_6;
		_privacyBlockedActionViewModelFactory = P_8;
		_streamerModeService = P_9;
		_developerModeService = P_10;
		_streamerModeService.PropertyChanged += onStreamerModeServicePropertyChanged;
		_developerModeService.PropertyChanged += onDeveloperModeServicePropertyChanged;
		ShouldHideNotes = _streamerModeService.ShouldHidePersonalInfo;
		IsSelf = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id == MessageContainerMember.GlobalUser.Id;
		if (MessageContainerMember is Member member)
		{
			CommunityMember = member;
			UserContextMenu = new Lazy<UserContextMenuViewModel>(() => P_7.Create(memberProfileViewModel.CommunityMember));
		}
		else
		{
			UserContextMenu = new Lazy<UserContextMenuViewModel>(() => P_7.Create(memberProfileViewModel.MessageContainerMember.GlobalUser));
		}
		if (CommunityMember != null)
		{
			CommunityMember.Community.LocalCommunityPermission.PropertyChanged += onLocalCommunityPermissionPropertyChanged;
			_addRoleButtonViewModel = P_5.Create(CommunityMember);
			_cacheCleanup = CommunityMember.Roles.ConnectRoles().Transform((Func<Role, IViewModelBase>)((Role role) => P_2.Create(role, memberProfileViewModel.CommunityMember)), false).Merge<IChangeSet<IViewModelBase>>(_addRoleSubject)
				.ObserveOn(AvaloniaScheduler.Instance)
				.Bind(out _roles)
				.DisposeMany()
				.Subscribe();
			if (CommunityMember.Community.LocalCommunityPermission.CommunityManageRoles)
			{
				showAddRoleButton();
			}
		}
		getNoteAsync().Forget();
		loadBadges();
	}

	private void onLocalCommunityPermissionPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "CommunityManageRoles")
			{
				if (_addRoleButtonIsVisible)
				{
					hideAddRoleButton();
				}
				else
				{
					showAddRoleButton();
				}
			}
		});
	}

	private void onStreamerModeServicePropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "ShouldHidePersonalInfo" || e.PropertyName == "IsEnabled")
		{
			Dispatcher.UIThread.Post(delegate
			{
				ShouldHideNotes = _streamerModeService.ShouldHidePersonalInfo;
			});
		}
	}

	private void onDeveloperModeServicePropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "IsEnabled")
		{
			Dispatcher.UIThread.Post(delegate
			{
				OnPropertyChanged("DeveloperModeEnabled");
				OnPropertyChanged("ShowContextMenuButton");
			});
		}
	}

	private void showAddRoleButton()
	{
		if (_addRoleButtonViewModel != null)
		{
			_addRoleButtonIsVisible = true;
			if (!_addRoleSubject.IsDisposed)
			{
				_addRoleSubject.OnNext(new ChangeSet<IViewModelBase>(new global::_003C_003Ez__ReadOnlySingleElementList<Change<IViewModelBase>>(new Change<IViewModelBase>(ListChangeReason.Add, _addRoleButtonViewModel))));
			}
		}
	}

	private void hideAddRoleButton()
	{
		if (_addRoleButtonViewModel != null)
		{
			_addRoleButtonIsVisible = false;
			if (!_addRoleSubject.IsDisposed)
			{
				_addRoleSubject.OnNext(new ChangeSet<IViewModelBase>(new global::_003C_003Ez__ReadOnlySingleElementList<Change<IViewModelBase>>(new Change<IViewModelBase>(ListChangeReason.Remove, _addRoleButtonViewModel))));
			}
		}
	}

	private void loadBadges()
	{
		BadgeDisplays.Clear();
		if (CommunityMember == null)
		{
			return;
		}
		Application current = Application.Current;
		if (current == null)
		{
			return;
		}
		foreach (UserBadge badge in CommunityMember.GlobalUser.Badges)
		{
			if (BadgeSvgMapper.TryGetBadgeInfo(badge.Id, out BadgeSvgMapper.BadgeDisplayInfo badgeDisplayInfo) && !(badgeDisplayInfo == null) && current.TryFindResource(badgeDisplayInfo.ResourceKey, current.ActualThemeVariant, out object obj) && obj is string text)
			{
				BadgeDisplays.Add(new MemberBadgeDisplay(badgeDisplayInfo.DisplayName, text));
			}
		}
	}

	[RelayCommand]
	public void SendMessage(string message)
	{
		_directMessageOpenerService.OpenDirectMessageAsync(MessageContainerMember.GlobalUser, message);
		_closeProfileCallback();
	}

	[RelayCommand]
	public async Task SendFriendRequestAsync()
	{
		try
		{
			await _rootSessionAccessor.Session.FriendService.CreateFriendshipRequestAsync(MessageContainerMember.GlobalUser.UserName);
			FriendRequestSent = true;
		}
		catch (RpcException ex) when (ex.StatusCode == StatusCode.Unauthenticated)
		{
			_privacyBlockedActionViewModelFactory.Show(PrivacyBlockedActionType.FriendshipInvite);
		}
		catch
		{
		}
	}

	[RelayCommand]
	public void CloseProfileCallback(string message)
	{
		_closeProfileCallback();
	}

	[RelayCommand]
	public void Call()
	{
		_directMessageOpenerService.OpenDirectMessageAsync(MessageContainerMember.GlobalUser, "", true).Forget();
	}

	private void onGlobalUserPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "ProfilePictureUri")
			{
				OnPropertyChanged("ProfilePictureAsyncBitmapWrapper");
			}
			else if (e.PropertyName == "Badges")
			{
				loadBadges();
			}
		});
	}

	[RelayCommand]
	public void Unloaded()
	{
		if (_noteChanged && Note != null)
		{
			_rootSessionAccessor.Session.UserInfoService.SetUserNoteAsync(MessageContainerMember.GlobalUser.Id, Note).Forget();
		}
	}

	private async Task getNoteAsync()
	{
		Note = await _rootSessionAccessor.Session.UserInfoService.GetUserNoteAsync(MessageContainerMember.GlobalUser.Id);
		_firstNoteLoaded = true;
	}

	public override void Dispose()
	{
		base.Dispose();
		_cacheCleanup?.Dispose();
		_addRoleSubject.Dispose();
		MessageContainerMember.GlobalUser.PropertyChanged -= onGlobalUserPropertyChanged;
		_streamerModeService.PropertyChanged -= onStreamerModeServicePropertyChanged;
		_developerModeService.PropertyChanged -= onDeveloperModeServicePropertyChanged;
		if (CommunityMember != null)
		{
			CommunityMember.Community.LocalCommunityPermission.PropertyChanged -= onLocalCommunityPermissionPropertyChanged;
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnNoteChanged(string? P_0)
	{
		if (_firstNoteLoaded)
		{
			_noteChanged = true;
		}
	}
}

