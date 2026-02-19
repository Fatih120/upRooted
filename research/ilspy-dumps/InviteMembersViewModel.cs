// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.InviteMembersViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using DynamicData.Binding;
using FluentValidation;
using Google.Protobuf.WellKnownTypes;
using Microsoft.VisualStudio.Threading;
using ReactiveUI.Avalonia;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Helpers.Clipboard;
using RootApp.Client.Avalonia.Helpers.StreamerMode;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Client.CoreDomain.Models.Friends;
using RootApp.Client.CoreDomain.Utils.Links;
using RootApp.WebApi.Shared.Grpc.Responses;

public class InviteMembersViewModel : ViewModelBase<InviteMembersViewModel>
{
	private readonly Community _community;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly InviteMembersLinkSettingsViewModelFactory _inviteMembersLinkSettingsViewModelFactory;

	private readonly ClipboardService _clipboardService;

	private readonly IStreamerModeService _streamerModeService;

	private readonly Action? _onCloseCallBack;

	private readonly CompositeDisposable _disposables = new CompositeDisposable();

	private readonly List<Member> _members;

	private string _actualInviteLink = string.Empty;

	[CompilerGenerated]
	private string _003CCommunityName_003Ek__BackingField = string.Empty;

	[CompilerGenerated]
	private bool _003CIsEmptyStateVisible_003Ek__BackingField = true;

	[CompilerGenerated]
	private string _003CInviteLink_003Ek__BackingField = string.Empty;

	[CompilerGenerated]
	private string _003CInviteDurationText_003Ek__BackingField = string.Empty;

	[CompilerGenerated]
	private string _003CCopyButtonText_003Ek__BackingField = Resources.Copy;

	private readonly ReadOnlyObservableCollection<MemberInviteViewModel> _friends;

	[CompilerGenerated]
	private string? _003CSearchTerm_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsCopied_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? editLinkSettingsCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? copyLinkCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string CommunityName
	{
		get
		{
			return _003CCommunityName_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CCommunityName_003Ek__BackingField, text))
			{
				_003CCommunityName_003Ek__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.CommunityName);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string? SearchTerm
	{
		get
		{
			return _003CSearchTerm_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CSearchTerm_003Ek__BackingField, text))
			{
				_003CSearchTerm_003Ek__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SearchTerm);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsEmptyStateVisible
	{
		get
		{
			return _003CIsEmptyStateVisible_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsEmptyStateVisible_003Ek__BackingField, flag))
			{
				_003CIsEmptyStateVisible_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsEmptyStateVisible);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string InviteLink
	{
		get
		{
			return _003CInviteLink_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CInviteLink_003Ek__BackingField, text))
			{
				_003CInviteLink_003Ek__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.InviteLink);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string InviteDurationText
	{
		get
		{
			return _003CInviteDurationText_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CInviteDurationText_003Ek__BackingField, text))
			{
				_003CInviteDurationText_003Ek__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.InviteDurationText);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string CopyButtonText
	{
		get
		{
			return _003CCopyButtonText_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CCopyButtonText_003Ek__BackingField, text))
			{
				_003CCopyButtonText_003Ek__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.CopyButtonText);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsCopied
	{
		get
		{
			return _003CIsCopied_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsCopied_003Ek__BackingField, flag))
			{
				_003CIsCopied_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsCopied);
			}
		}
	}

	public ReadOnlyObservableCollection<MemberInviteViewModel> Friends => _friends;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand EditLinkSettingsCommand => editLinkSettingsCommand ?? (editLinkSettingsCommand = new RelayCommand(EditLinkSettings));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CopyLinkCommand => copyLinkCommand ?? (copyLinkCommand = new RelayCommand(CopyLink));

	public InviteMembersViewModel(Community P_0, IRootSessionAccessor P_1, MemberInviteViewModelFactory P_2, InviteMembersLinkSettingsViewModelFactory P_3, ClipboardService P_4, IStreamerModeService P_5, Action? P_6)
		: base((IValidator<InviteMembersViewModel>?)null)
	{
		InviteMembersViewModel inviteMembersViewModel = this;
		_community = P_0;
		_rootSessionAccessor = P_1;
		_inviteMembersLinkSettingsViewModelFactory = P_3;
		_clipboardService = P_4;
		_streamerModeService = P_5;
		_onCloseCallBack = P_6;
		CommunityName = _community.Name;
		_members = _community.Members.GetAllMembers();
		IObservable<Func<Friend, bool>> predicateChanged = (from _ in Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(delegate(PropertyChangedEventHandler h)
			{
				inviteMembersViewModel.PropertyChanged += h;
			}, delegate(PropertyChangedEventHandler h)
			{
				inviteMembersViewModel.PropertyChanged -= h;
			})
			where _.EventArgs.PropertyName == "SearchTerm"
			select inviteMembersViewModel.buildFilterPredicate(inviteMembersViewModel.SearchTerm)).StartWith(buildFilterPredicate(SearchTerm));
		_rootSessionAccessor.Session.FriendService.ConnectAllFriends().Filter(predicateChanged).Transform((Friend f) => P_2.Create(f, inviteMembersViewModel._community))
			.ObserveOn(AvaloniaScheduler.Instance)
			.Bind(out _friends)
			.DisposeMany()
			.Subscribe()
			.DisposeWith(_disposables);
		(from _ in _friends.ToObservableChangeSet()
			select inviteMembersViewModel.Friends.Count).Subscribe(friendCountChanged).DisposeWith(_disposables);
		generateInviteLinkAsync().Forget();
	}

	[RelayCommand]
	public void EditLinkSettings()
	{
		InviteMembersLinkSettingsViewModel inviteMembersLinkSettingsViewModel = _inviteMembersLinkSettingsViewModelFactory.Create(_community, generateInviteLinkCallback);
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(inviteMembersLinkSettingsViewModel));
	}

	[RelayCommand]
	public void CloseViewModel()
	{
		WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this));
		_onCloseCallBack?.Invoke();
	}

	private void friendCountChanged(int friendCount)
	{
		IsEmptyStateVisible = friendCount == 0;
	}

	private Func<Friend, bool> buildFilterPredicate(string? P_0)
	{
		return (Friend friend) => canInviteFriend(friend, P_0);
	}

	private bool canInviteFriend(Friend P_0, string? P_1)
	{
		if (string.IsNullOrEmpty(P_1) || P_0.GlobalUser.UserName.Contains(P_1, StringComparison.OrdinalIgnoreCase))
		{
			Member member = _members.FirstOrDefault((Member member2) => member2.UserId == P_0.UserId);
			return member == null || !member.IsMemberOfCommunity;
		}
		return false;
	}

	private async Task generateInviteLinkAsync()
	{
		try
		{
			CommunityInviteLinkListMineResponse myInvites = await _rootSessionAccessor.Session.LinkService.GetMyCommunityInviteLinksAsync(_community.Id);
			CommunityInviteLinkResponse myInvite = myInvites.CommunityInviteLinks.FirstOrDefault((CommunityInviteLinkResponse i) => i.ExpiresAt == null);
			if (myInvite == null)
			{
				myInvite = myInvites.CommunityInviteLinks.OrderByDescending((CommunityInviteLinkResponse i) => i.ExpiresAt).FirstOrDefault();
			}
			if (myInvite == null || (myInvite.ExpiresAt != null && myInvite.ExpiresAt < Timestamp.FromDateTime(DateTime.UtcNow)))
			{
				CommunityInviteLinkCreateResponse link = await _rootSessionAccessor.Session.LinkService.CreateCommunityInviteLinkAsync(_community.Id, Timestamp.FromDateTime(DateTime.UtcNow.AddDays(7.0)));
				_actualInviteLink = link.Link;
				InviteLink = getDisplayInviteLink(link.Link);
				InviteDurationText = InviteLinkFormatter.GetInviteExpirationMessage(link.ExpiresAt, link.MaxUses);
			}
			else
			{
				_actualInviteLink = myInvite.Link;
				InviteLink = getDisplayInviteLink(myInvite.Link);
				InviteDurationText = InviteLinkFormatter.GetInviteExpirationMessage(myInvite.ExpiresAt, myInvite.MaxUses);
			}
		}
		catch
		{
		}
	}

	private void generateInviteLinkCallback(CommunityInviteLinkCreateResponse response)
	{
		_actualInviteLink = response.Link;
		InviteLink = getDisplayInviteLink(response.Link);
		InviteDurationText = InviteLinkFormatter.GetInviteExpirationMessage(response.ExpiresAt, response.MaxUses);
	}

	private string getDisplayInviteLink(string P_0)
	{
		return _streamerModeService.ShouldHideInviteLinks ? Resources.InviteLinkHidden : P_0;
	}

	[RelayCommand]
	public void CopyLink()
	{
		_clipboardService.CopyTextToClipboard(_actualInviteLink);
		CopyButtonText = Resources.Copied;
		IsCopied = true;
		Task.Delay(2000).ContinueWith(delegate
		{
			CopyButtonText = Resources.Copy;
			IsCopied = false;
		}, TaskScheduler.FromCurrentSynchronizationContext());
	}

	public override void Dispose()
	{
		_disposables.Dispose();
		base.Dispose();
	}
}

