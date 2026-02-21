using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using FluentValidation;
using Microsoft.VisualStudio.Threading;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Core.Identifiers;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.Client.Avalonia.UI.Community.Channels.Permissions;

public class AccessRuleSelectorViewModel : ViewModelBase<AccessRuleSelectorViewModel>
{
	private readonly AccessRuleViewModelFactory _accessRuleViewModelFactory;

	private readonly ChannelOrChannelGroupGuid? _existingChannelOrChannelGroupId;

	private readonly RootApp.Client.CoreDomain.Models.Community.Community _community;

	private readonly AccessRulesViewModel _accessRulesViewModel;

	private bool _isAppChannel;

	private readonly List<AccessRuleResponse> _initialAccessRuleResponses = new List<AccessRuleResponse>();

	private readonly Dictionary<RoleOrMemberGuid, AccessRuleViewModel> _accessRuleViewModels = new Dictionary<RoleOrMemberGuid, AccessRuleViewModel>();

	[CompilerGenerated]
	private IViewModelBase <MainContent>k__BackingField;

	public bool IsAppChannel
	{
		set
		{
			if (_isAppChannel == flag)
			{
				return;
			}
			_isAppChannel = flag;
			foreach (AccessRuleViewModel value in _accessRuleViewModels.Values)
			{
				value.IsAppChannel = flag;
			}
			_accessRulesViewModel.IsAppChannel = flag;
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IViewModelBase MainContent
	{
		get
		{
			return <MainContent>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<IViewModelBase>.Default.Equals(<MainContent>k__BackingField, viewModelBase))
			{
				<MainContent>k__BackingField = viewModelBase;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.MainContent);
			}
		}
	}

	public AccessRuleSelectorViewModel(RootApp.Client.CoreDomain.Models.Community.Community P_0, bool P_1, bool? P_2, ChannelOrChannelGroupGuid? P_3, bool P_4, AccessRulesViewModelFactory P_5, AccessRuleViewModelFactory P_6)
		: base((IValidator<AccessRuleSelectorViewModel>?)null)
	{
		_community = P_0;
		_existingChannelOrChannelGroupId = P_3;
		_isAppChannel = P_4;
		_accessRuleViewModelFactory = P_6;
		_accessRulesViewModel = P_5.Create(P_0, P_1, P_2, accessRuleAdded, accessRuleRemoved, accessRuleSelected, P_4);
		MainContent = _accessRulesViewModel;
		if (_existingChannelOrChannelGroupId != null)
		{
			loadExistingAccessRulesAsync().Forget();
		}
	}

	private async Task loadExistingAccessRulesAsync()
	{
		try
		{
			foreach (AccessRuleResponse accessRule in (await _community.Roles.GetAccessRulesByChannelOrChannelGroupAsync(_existingChannelOrChannelGroupId.Value)).AccessRules)
			{
				_initialAccessRuleResponses.Add(accessRule);
				_accessRulesViewModel.AddRoleOrMemberAccessRuleAsync(accessRule.RoleOrMemberId).Forget();
				_accessRuleViewModels.GetValueOrDefault(accessRule.RoleOrMemberId)?.UpdateFromAccessRuleResponse(accessRule);
			}
		}
		catch
		{
		}
	}

	public IEnumerable<AccessRuleCreateRoleOrMemberRequest> GetAccessRuleCreateRoleOrMemberRequests()
	{
		return _accessRuleViewModels.Values.Select((AccessRuleViewModel p) => p.GetAccessRuleCreateRoleOrMemberRequest());
	}

	public AccessRuleUpdateRequest GetAccessRuleUpdateRequest()
	{
		AccessRuleUpdateRequest accessRuleUpdateRequest = new AccessRuleUpdateRequest();
		accessRuleUpdateRequest.CommunityId = _community.Id;
		IEnumerable<AccessRuleUpdateRequest> enumerable = _accessRuleViewModels.Values.Select((AccessRuleViewModel o) => o.GetAccessRuleUpdateRequest());
		foreach (AccessRuleUpdateRequest item in enumerable)
		{
			accessRuleUpdateRequest.Creates.AddRange(item.Creates);
			accessRuleUpdateRequest.Edits.AddRange(item.Edits);
		}
		foreach (AccessRuleResponse initialAccessRuleResponse in _initialAccessRuleResponses)
		{
			if (!_accessRuleViewModels.ContainsKey(initialAccessRuleResponse.RoleOrMemberId))
			{
				accessRuleUpdateRequest.Deletes.Add(new AccessRuleDeleteRequest
				{
					CommunityId = _community.Id,
					ChannelOrChannelGroupId = initialAccessRuleResponse.ChannelOrChannelGroupId,
					RoleOrMemberId = initialAccessRuleResponse.RoleOrMemberId
				});
			}
		}
		foreach (AccessRuleCreateRequest create in accessRuleUpdateRequest.Creates)
		{
			create.ChannelOrChannelGroupId = _existingChannelOrChannelGroupId;
		}
		return accessRuleUpdateRequest;
	}

	public bool GetSyncPermissionsValue()
	{
		if (!_accessRulesViewModel.SyncPermissions.HasValue)
		{
			return true;
		}
		return _accessRulesViewModel.SyncPermissions.Value;
	}

	private void accessRuleAdded(RoleOrMemberGuid roleOrMemberId, string name)
	{
		AccessRuleViewModel valueOrDefault = _accessRuleViewModels.GetValueOrDefault(roleOrMemberId);
		if (valueOrDefault == null)
		{
			valueOrDefault = _accessRuleViewModelFactory.Create(roleOrMemberId, name, _community, navigateBack, _isAppChannel);
			_accessRuleViewModels.Add(roleOrMemberId, valueOrDefault);
		}
	}

	private void accessRuleRemoved(RoleOrMemberGuid roleOrMemberId)
	{
		AccessRuleViewModel valueOrDefault = _accessRuleViewModels.GetValueOrDefault(roleOrMemberId);
		if (valueOrDefault != null)
		{
			_accessRuleViewModels.Remove(roleOrMemberId);
		}
	}

	private void accessRuleSelected(RoleOrMemberGuid roleOrMemberId)
	{
		AccessRuleViewModel valueOrDefault = _accessRuleViewModels.GetValueOrDefault(roleOrMemberId);
		if (valueOrDefault != null)
		{
			MainContent = valueOrDefault;
		}
	}

	private void navigateBack()
	{
		MainContent = _accessRulesViewModel;
	}

	public override void Dispose()
	{
		base.Dispose();
		_accessRulesViewModel.Dispose();
	}
}
