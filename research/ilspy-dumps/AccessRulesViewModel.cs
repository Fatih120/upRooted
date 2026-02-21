using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using Microsoft.VisualStudio.Threading;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.Avalonia.UI.Community.Roles.Pickers;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Core.Enums;
using RootApp.Core.Identifiers;

namespace RootApp.Client.Avalonia.UI.Community.Channels.Permissions;

public class AccessRulesViewModel : ViewModelBase<AccessRulesViewModel>
{
	private readonly Action<RoleOrMemberGuid, string> _accessRuleAddedCallback;

	private readonly Action<RoleOrMemberGuid> _accessRuleRemovedCallback;

	private readonly Action<RoleOrMemberGuid> _accessRuleSelectedCallback;

	private readonly RolePickerViewModelFactory _rolePickerViewModelFactory;

	private readonly MemberPickerViewModelFactory _memberPickerViewModelFactory;

	private readonly RootApp.Client.CoreDomain.Models.Community.Community _community;

	private readonly RoleAccessRuleTagViewModelFactory _roleAccessRuleTagViewModelFactory;

	private readonly MemberAccessRuleTagViewModelFactory _memberAccessRuleTagViewModelFactory;

	private readonly AppAccessRuleTagViewModelFactory _appAccessRuleTagViewModelFactory;

	[CompilerGenerated]
	private RolePickerViewModel? <RoleSelectorViewModel>k__BackingField;

	[CompilerGenerated]
	private MemberPickerViewModel? <MemberPickerViewModel>k__BackingField;

	[CompilerGenerated]
	private bool? <SyncPermissions>k__BackingField;

	[CompilerGenerated]
	private bool <IsAppChannel>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? roleSelectorFlyoutOpeningCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? memberPickerFlyoutOpeningCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public RolePickerViewModel? RoleSelectorViewModel
	{
		get
		{
			return <RoleSelectorViewModel>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<RolePickerViewModel>.Default.Equals(<RoleSelectorViewModel>k__BackingField, rolePickerViewModel))
			{
				<RoleSelectorViewModel>k__BackingField = rolePickerViewModel;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.RoleSelectorViewModel);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public MemberPickerViewModel? MemberPickerViewModel
	{
		get
		{
			return <MemberPickerViewModel>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<RootApp.Client.Avalonia.UI.Community.Members.MemberPickerViewModel>.Default.Equals(<MemberPickerViewModel>k__BackingField, memberPickerViewModel))
			{
				<MemberPickerViewModel>k__BackingField = memberPickerViewModel;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.MemberPickerViewModel);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool? SyncPermissions
	{
		get
		{
			return <SyncPermissions>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool?>.Default.Equals(<SyncPermissions>k__BackingField, flag))
			{
				<SyncPermissions>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SyncPermissions);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsAppChannel
	{
		get
		{
			return <IsAppChannel>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsAppChannel>k__BackingField, flag))
			{
				<IsAppChannel>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsAppChannel);
			}
		}
	}

	public ObservableCollection<IViewModelBase> AccessRuleTags { get; } = new ObservableCollection<IViewModelBase>();

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand RoleSelectorFlyoutOpeningCommand => roleSelectorFlyoutOpeningCommand ?? (roleSelectorFlyoutOpeningCommand = new RelayCommand(RoleSelectorFlyoutOpening));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand MemberPickerFlyoutOpeningCommand => memberPickerFlyoutOpeningCommand ?? (memberPickerFlyoutOpeningCommand = new RelayCommand(MemberPickerFlyoutOpening));

	public AccessRulesViewModel(RootApp.Client.CoreDomain.Models.Community.Community P_0, bool P_1, bool? P_2, RoleAccessRuleTagViewModelFactory P_3, MemberAccessRuleTagViewModelFactory P_4, AppAccessRuleTagViewModelFactory P_5, Action<RoleOrMemberGuid, string> P_6, Action<RoleOrMemberGuid> P_7, Action<RoleOrMemberGuid> P_8, RolePickerViewModelFactory P_9, MemberPickerViewModelFactory P_10, bool P_11 = false)
		: base((IValidator<AccessRulesViewModel>?)null)
	{
		SyncPermissions = P_2;
		IsAppChannel = P_11;
		_community = P_0;
		_roleAccessRuleTagViewModelFactory = P_3;
		_memberAccessRuleTagViewModelFactory = P_4;
		_appAccessRuleTagViewModelFactory = P_5;
		_accessRuleAddedCallback = P_6;
		_accessRuleRemovedCallback = P_7;
		_accessRuleSelectedCallback = P_8;
		_rolePickerViewModelFactory = P_9;
		_memberPickerViewModelFactory = P_10;
		if (P_1)
		{
			Role defaultRole = _community.Roles.GetDefaultRole();
			AccessRuleTags.Add(_roleAccessRuleTagViewModelFactory.Create(defaultRole, roleAccessRuleTagNavigationRequested, roleAccessRuleTagNavigationGoBack));
			_accessRuleAddedCallback(((RoleOrMemberGuid?)defaultRole.Id).Value, defaultRole.RoleName);
		}
	}

	public async Task AddRoleOrMemberAccessRuleAsync(RootGuid P_0)
	{
		if ((RootGuidType)P_0 == RootGuidType.CommunityRole)
		{
			if (!AccessRuleTags.OfType<RoleAccessRuleTagViewModel>().Any((RoleAccessRuleTagViewModel r) => r.Role.Id == P_0))
			{
				Role role = _community.Roles.GetRole((CommunityRoleGuid)P_0);
				if (role != null)
				{
					AccessRuleTags.Add(_roleAccessRuleTagViewModelFactory.Create(role, roleAccessRuleTagNavigationRequested, roleAccessRuleTagNavigationGoBack));
					_accessRuleAddedCallback((RoleOrMemberGuid)P_0, role.RoleName);
				}
			}
		}
		else if ((RootGuidType)P_0 == RootGuidType.Person)
		{
			if (!AccessRuleTags.OfType<MemberAccessRuleTagViewModel>().Any((MemberAccessRuleTagViewModel m) => m.Member.GlobalUser.Id == P_0))
			{
				Member member = await _community.Members.GetMemberAsync((UserGuid)P_0);
				if (member != null)
				{
					AccessRuleTags.Add(_memberAccessRuleTagViewModelFactory.Create(member, memberAccessRuleTagNavigationRequested, memberAccessRuleTagNavigationGoBack));
					_accessRuleAddedCallback((RoleOrMemberGuid)P_0, member.GlobalUser.UserName);
				}
			}
		}
		else if ((RootGuidType)P_0 == RootGuidType.App && !AccessRuleTags.OfType<AppAccessRuleTagViewModel>().Any((AppAccessRuleTagViewModel a) => a.App.AppId == P_0))
		{
			CommunityApp app = _community.Apps.GetAppByAppId((AppGuid)P_0);
			if (app != null)
			{
				AccessRuleTags.Add(_appAccessRuleTagViewModelFactory.Create(app, _community, appAccessRuleTagNavigationRequested, appAccessRuleTagNavigationGoBack));
				_accessRuleAddedCallback((RoleOrMemberGuid)P_0, app.Name);
			}
		}
	}

	private void roleAccessRuleTagNavigationRequested(RoleAccessRuleTagViewModel roleAccessRuleTagViewModel)
	{
		_accessRuleSelectedCallback(((RoleOrMemberGuid?)roleAccessRuleTagViewModel.Role.Id).Value);
	}

	private void roleAccessRuleTagNavigationGoBack(RoleAccessRuleTagViewModel roleAccessRuleTagViewModel)
	{
		roleAccessRuleRemoved(roleAccessRuleTagViewModel.Role.Id);
	}

	private void newRoleAccessRuleAdded(RootGuid roleId)
	{
		AddRoleOrMemberAccessRuleAsync(roleId).Forget();
	}

	private void roleAccessRuleRemoved(RootGuid roleId)
	{
		RoleAccessRuleTagViewModel roleAccessRuleTagViewModel = AccessRuleTags.OfType<RoleAccessRuleTagViewModel>().FirstOrDefault((RoleAccessRuleTagViewModel r) => r.Role.Id == roleId);
		if (roleAccessRuleTagViewModel != null)
		{
			AccessRuleTags.Remove(roleAccessRuleTagViewModel);
			_accessRuleRemovedCallback((RoleOrMemberGuid)roleId);
		}
	}

	private void memberAccessRuleTagNavigationRequested(MemberAccessRuleTagViewModel memberAccessRuleTagViewModel)
	{
		_accessRuleSelectedCallback(((RoleOrMemberGuid?)memberAccessRuleTagViewModel.Member.GlobalUser.Id).Value);
	}

	private void memberAccessRuleTagNavigationGoBack(MemberAccessRuleTagViewModel memberAccessRuleTagViewModel)
	{
		memberAccessRuleRemoved(memberAccessRuleTagViewModel.Member.GlobalUser.Id);
	}

	private void memberAccessRuleRemoved(RootGuid P_0)
	{
		MemberAccessRuleTagViewModel memberAccessRuleTagViewModel = AccessRuleTags.OfType<MemberAccessRuleTagViewModel>().FirstOrDefault((MemberAccessRuleTagViewModel m) => m.Member.GlobalUser.Id == P_0);
		if (memberAccessRuleTagViewModel != null)
		{
			AccessRuleTags.Remove(memberAccessRuleTagViewModel);
			_accessRuleRemovedCallback((RoleOrMemberGuid)P_0);
		}
	}

	private void newMemberOrAppAccessRuleAdded(RootGuid userId)
	{
		Member member = _community.Members?.GetMemberFromCache((UserGuid)userId);
		if (member != null && member.IsApp)
		{
			AddRoleOrMemberAccessRuleAsync((AppGuid)userId).Forget();
		}
		else
		{
			AddRoleOrMemberAccessRuleAsync(userId).Forget();
		}
	}

	private void memberOrAppAccessRuleRemoved(RootGuid userId)
	{
		AppAccessRuleTagViewModel appAccessRuleTagViewModel = AccessRuleTags.OfType<AppAccessRuleTagViewModel>().FirstOrDefault((AppAccessRuleTagViewModel a) => ((UserGuid?)a.App.AppId).Value == userId);
		if (appAccessRuleTagViewModel != null)
		{
			AccessRuleTags.Remove(appAccessRuleTagViewModel);
			_accessRuleRemovedCallback(((RoleOrMemberGuid?)appAccessRuleTagViewModel.App.AppId).Value);
			return;
		}
		MemberAccessRuleTagViewModel memberAccessRuleTagViewModel = AccessRuleTags.OfType<MemberAccessRuleTagViewModel>().FirstOrDefault((MemberAccessRuleTagViewModel m) => m.Member.GlobalUser.Id == userId);
		if (memberAccessRuleTagViewModel != null)
		{
			AccessRuleTags.Remove(memberAccessRuleTagViewModel);
			_accessRuleRemovedCallback((RoleOrMemberGuid)userId);
		}
	}

	private void appAccessRuleTagNavigationRequested(AppAccessRuleTagViewModel appAccessRuleTagViewModel)
	{
		_accessRuleSelectedCallback(((RoleOrMemberGuid?)appAccessRuleTagViewModel.App.AppId).Value);
	}

	private void appAccessRuleTagNavigationGoBack(AppAccessRuleTagViewModel appAccessRuleTagViewModel)
	{
		appAccessRuleRemoved(appAccessRuleTagViewModel.App.AppId);
	}

	private void appAccessRuleRemoved(RootGuid P_0)
	{
		AppAccessRuleTagViewModel appAccessRuleTagViewModel = AccessRuleTags.OfType<AppAccessRuleTagViewModel>().FirstOrDefault((AppAccessRuleTagViewModel a) => a.App.AppId == P_0);
		if (appAccessRuleTagViewModel != null)
		{
			AccessRuleTags.Remove(appAccessRuleTagViewModel);
			_accessRuleRemovedCallback((RoleOrMemberGuid)P_0);
		}
	}

	[RelayCommand]
	public void RoleSelectorFlyoutOpening()
	{
		RoleSelectorViewModel?.Dispose();
		RoleSelectorViewModel = _rolePickerViewModelFactory.Create(_community, newRoleAccessRuleAdded, roleAccessRuleRemoved, AccessRuleTags.OfType<RoleAccessRuleTagViewModel>().Select((Func<RoleAccessRuleTagViewModel, RootGuid>)((RoleAccessRuleTagViewModel r) => r.Role.Id)), true, false);
	}

	[RelayCommand]
	public void MemberPickerFlyoutOpening()
	{
		MemberPickerViewModel?.Dispose();
		IEnumerable<RootGuid> enumerable = AccessRuleTags.OfType<MemberAccessRuleTagViewModel>().Select((Func<MemberAccessRuleTagViewModel, RootGuid>)((MemberAccessRuleTagViewModel u) => u.Member.GlobalUser.Id));
		IEnumerable<RootGuid> enumerable2 = AccessRuleTags.OfType<AppAccessRuleTagViewModel>().Select((Func<AppAccessRuleTagViewModel, RootGuid>)((AppAccessRuleTagViewModel a) => ((UserGuid?)a.App.AppId).Value));
		MemberPickerViewModel = _memberPickerViewModelFactory.Create(_community, newMemberOrAppAccessRuleAdded, memberOrAppAccessRuleRemoved, enumerable.Concat(enumerable2));
	}

	public override void Dispose()
	{
		base.Dispose();
		RoleSelectorViewModel?.Dispose();
	}
}
