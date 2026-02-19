// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.MemberInviteViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.Avalonia.UI.Community.Roles.Pickers;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Client.CoreDomain.Models.Friends;
using RootApp.Client.CoreDomain.Models.User;
using RootApp.Core.Identifiers;

public class MemberInviteViewModel : ViewModelBase<MemberInviteViewModel>
{
	private readonly Community _community;

	private readonly BitmapCache _bitmapCache;

	private readonly RolePickerViewModelFactory _rolePickerViewModelFactory;

	private readonly List<Role> _selectedRoles;

	[CompilerGenerated]
	private bool _003CHasBeenInvited_003Ek__BackingField;

	[CompilerGenerated]
	private int _003CNumRoles_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsBannedMember_003Ek__BackingField;

	[CompilerGenerated]
	private RolePickerViewModel? _003CRoleSelectorViewModel_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? sendCommunityMemberInviteCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? roleSelectorFlyoutOpeningCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool HasBeenInvited
	{
		get
		{
			return _003CHasBeenInvited_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CHasBeenInvited_003Ek__BackingField, flag))
			{
				_003CHasBeenInvited_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.HasBeenInvited);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public int NumRoles
	{
		get
		{
			return _003CNumRoles_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<int>.Default.Equals(_003CNumRoles_003Ek__BackingField, num))
			{
				_003CNumRoles_003Ek__BackingField = num;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.NumRoles);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsBannedMember
	{
		get
		{
			return _003CIsBannedMember_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsBannedMember_003Ek__BackingField, flag))
			{
				_003CIsBannedMember_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsBannedMember);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public RolePickerViewModel? RoleSelectorViewModel
	{
		get
		{
			return _003CRoleSelectorViewModel_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<RolePickerViewModel>.Default.Equals(_003CRoleSelectorViewModel_003Ek__BackingField, rolePickerViewModel))
			{
				_003CRoleSelectorViewModel_003Ek__BackingField = rolePickerViewModel;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.RoleSelectorViewModel);
			}
		}
	}

	public GlobalUser GlobalUser { get; }

	public Task<BitmapWrapper?> ProfilePictureAsyncBitmapWrapper => _bitmapCache.GetBitmapAsync(GlobalUser.ProfilePictureUri, null, 120);

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand SendCommunityMemberInviteCommand => sendCommunityMemberInviteCommand ?? (sendCommunityMemberInviteCommand = new AsyncRelayCommand(SendCommunityMemberInviteAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand RoleSelectorFlyoutOpeningCommand => roleSelectorFlyoutOpeningCommand ?? (roleSelectorFlyoutOpeningCommand = new RelayCommand(RoleSelectorFlyoutOpening));

	public MemberInviteViewModel(Friend P_0, Community P_1, BitmapCache P_2, RolePickerViewModelFactory P_3)
		: base((IValidator<MemberInviteViewModel>?)null)
	{
		_bitmapCache = P_2;
		_community = P_1;
		_rolePickerViewModelFactory = P_3;
		_selectedRoles = new List<Role>();
		GlobalUser = P_0.GlobalUser;
	}

	[RelayCommand]
	public async Task SendCommunityMemberInviteAsync()
	{
		try
		{
			await _community.MemberManagement.InviteUserAsync(GlobalUser.Id, _selectedRoles.Select((Role role) => role.Id).ToList());
			HasBeenInvited = true;
		}
		catch
		{
			IsBannedMember = true;
		}
	}

	[RelayCommand]
	public void RoleSelectorFlyoutOpening()
	{
		RoleSelectorViewModel?.Dispose();
		RoleSelectorViewModel = _rolePickerViewModelFactory.Create(_community, roleSelected, roleUnselected, ((IEnumerable<Role>)_selectedRoles).Select((Func<Role, RootGuid>)((Role role) => role.Id)), false, true);
	}

	private void roleSelected(RootGuid roleId)
	{
		if (!_selectedRoles.Any((Role role2) => role2.Id == roleId))
		{
			Role role = _community.Roles.GetRole((CommunityRoleGuid)roleId);
			if (role != null)
			{
				_selectedRoles.Add(role);
				updateRoleCount();
			}
		}
	}

	private void roleUnselected(RootGuid roleId)
	{
		if (!_selectedRoles.All((Role role2) => role2.Id != roleId))
		{
			Role role = _community.Roles.GetRole((CommunityRoleGuid)roleId);
			if (role != null)
			{
				_selectedRoles.Remove(role);
				updateRoleCount();
			}
		}
	}

	private void updateRoleCount()
	{
		NumRoles = _selectedRoles.Count;
	}

	public override void Dispose()
	{
		base.Dispose();
		RoleSelectorViewModel?.Dispose();
	}
}

