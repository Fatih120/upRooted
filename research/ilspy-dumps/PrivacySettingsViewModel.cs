// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.PrivacySettingsViewModel
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls.Settings;
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.User;

public class PrivacySettingsViewModel : ViewModelBase<PrivacySettingsViewModel>, IPage
{
	private readonly IRootSessionAccessor _rootSessionAccessor;

	[CompilerGenerated]
	private PrivacyOption _003CFriendshipPrivacy_003Ek__BackingField = PrivacyOption.Any;

	[CompilerGenerated]
	private PrivacyOption _003CDirectMessagePrivacy_003Ek__BackingField = PrivacyOption.Any;

	[CompilerGenerated]
	private bool _003CFriendshipRequiresVerified_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CDirectMessageRequiresVerified_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsLoading_003Ek__BackingField;

	public string PageTitle => Resources.Privacy;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public PrivacyOption FriendshipPrivacy
	{
		get
		{
			return _003CFriendshipPrivacy_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<PrivacyOption>.Default.Equals(_003CFriendshipPrivacy_003Ek__BackingField, privacyOption))
			{
				_003CFriendshipPrivacy_003Ek__BackingField = privacyOption;
				OnFriendshipPrivacyChanged(privacyOption);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.FriendshipPrivacy);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool FriendshipRequiresVerified
	{
		get
		{
			return _003CFriendshipRequiresVerified_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CFriendshipRequiresVerified_003Ek__BackingField, flag))
			{
				_003CFriendshipRequiresVerified_003Ek__BackingField = flag;
				OnFriendshipRequiresVerifiedChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.FriendshipRequiresVerified);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public PrivacyOption DirectMessagePrivacy
	{
		get
		{
			return _003CDirectMessagePrivacy_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<PrivacyOption>.Default.Equals(_003CDirectMessagePrivacy_003Ek__BackingField, privacyOption))
			{
				_003CDirectMessagePrivacy_003Ek__BackingField = privacyOption;
				OnDirectMessagePrivacyChanged(privacyOption);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.DirectMessagePrivacy);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool DirectMessageRequiresVerified
	{
		get
		{
			return _003CDirectMessageRequiresVerified_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CDirectMessageRequiresVerified_003Ek__BackingField, flag))
			{
				_003CDirectMessageRequiresVerified_003Ek__BackingField = flag;
				OnDirectMessageRequiresVerifiedChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.DirectMessageRequiresVerified);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsLoading
	{
		get
		{
			return _003CIsLoading_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsLoading_003Ek__BackingField, flag))
			{
				_003CIsLoading_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsLoading);
			}
		}
	}

	public PrivacySettingsViewModel(IRootSessionAccessor P_0)
		: base((IValidator<PrivacySettingsViewModel>?)null)
	{
		_rootSessionAccessor = P_0;
		LoadPrivacySettingsAsync();
	}

	private Task LoadPrivacySettingsAsync()
	{
		IsLoading = true;
		try
		{
			SessionUser sessionUser = _rootSessionAccessor.Session.UserInfoService.SessionUser;
			FriendshipPrivacy = sessionUser.FriendshipInviteConnection.ToPrivacyOption();
			FriendshipRequiresVerified = sessionUser.FriendshipInviteRequiresVerifiedUser;
			DirectMessagePrivacy = sessionUser.DirectMessageInviteConnection.ToPrivacyOption();
			DirectMessageRequiresVerified = sessionUser.DirectMessageInviteRequiresVerifiedUser;
		}
		finally
		{
			IsLoading = false;
		}
		return Task.CompletedTask;
	}

	private async Task SaveFriendshipPrivacyAsync()
	{
		if (!IsLoading)
		{
			await _rootSessionAccessor.Session.UserInfoService.SetFriendshipInviteRequirementAsync(FriendshipPrivacy.ToFriendshipConnection(), FriendshipRequiresVerified);
		}
	}

	private async Task SaveDirectMessagePrivacyAsync()
	{
		if (!IsLoading)
		{
			await _rootSessionAccessor.Session.UserInfoService.SetDirectMessageInviteRequirementAsync(DirectMessagePrivacy.ToDirectMessageConnection(), DirectMessageRequiresVerified);
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnFriendshipPrivacyChanged(PrivacyOption P_0)
	{
		SaveFriendshipPrivacyAsync();
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnFriendshipRequiresVerifiedChanged(bool P_0)
	{
		SaveFriendshipPrivacyAsync();
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnDirectMessagePrivacyChanged(PrivacyOption P_0)
	{
		SaveDirectMessagePrivacyAsync();
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnDirectMessageRequiresVerifiedChanged(bool P_0)
	{
		SaveDirectMessagePrivacyAsync();
	}
}

