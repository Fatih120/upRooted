// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.InviteMembersLinkSettingsViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using Google.Protobuf.WellKnownTypes;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.WebApi.Shared.Grpc.Responses;

public class InviteMembersLinkSettingsViewModel : ViewModelBase<InviteMembersLinkSettingsViewModel>
{
	private readonly Community _community;

	private readonly Action<CommunityInviteLinkCreateResponse> _generateInviteLinkCallback;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	[CompilerGenerated]
	private int _003CExpirationSelectionIndex_003Ek__BackingField = 4;

	[CompilerGenerated]
	private int _003CMaxUsesSelectionIndex_003Ek__BackingField = 0;

	[CompilerGenerated]
	private WebApiStatus _003CWebApiStatus_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? generateInviteLinkCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeViewModelCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public int ExpirationSelectionIndex
	{
		get
		{
			return _003CExpirationSelectionIndex_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<int>.Default.Equals(_003CExpirationSelectionIndex_003Ek__BackingField, num))
			{
				_003CExpirationSelectionIndex_003Ek__BackingField = num;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ExpirationSelectionIndex);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public int MaxUsesSelectionIndex
	{
		get
		{
			return _003CMaxUsesSelectionIndex_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<int>.Default.Equals(_003CMaxUsesSelectionIndex_003Ek__BackingField, num))
			{
				_003CMaxUsesSelectionIndex_003Ek__BackingField = num;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.MaxUsesSelectionIndex);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public WebApiStatus WebApiStatus
	{
		get
		{
			return _003CWebApiStatus_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<WebApiStatus>.Default.Equals(_003CWebApiStatus_003Ek__BackingField, webApiStatus))
			{
				_003CWebApiStatus_003Ek__BackingField = webApiStatus;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.WebApiStatus);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand GenerateInviteLinkCommand => generateInviteLinkCommand ?? (generateInviteLinkCommand = new AsyncRelayCommand(GenerateInviteLinkAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	public InviteMembersLinkSettingsViewModel(Community P_0, Action<CommunityInviteLinkCreateResponse> P_1, IRootSessionAccessor P_2)
		: base((IValidator<InviteMembersLinkSettingsViewModel>?)null)
	{
		_community = P_0;
		_generateInviteLinkCallback = P_1;
		_rootSessionAccessor = P_2;
	}

	[RelayCommand]
	public async Task GenerateInviteLinkAsync()
	{
		try
		{
			Timestamp expiresAt = null;
			switch (ExpirationSelectionIndex)
			{
			case 0:
				expiresAt = Timestamp.FromDateTime(DateTime.UtcNow.AddHours(1.0));
				break;
			case 1:
				expiresAt = Timestamp.FromDateTime(DateTime.UtcNow.AddHours(6.0));
				break;
			case 2:
				expiresAt = Timestamp.FromDateTime(DateTime.UtcNow.AddHours(12.0));
				break;
			case 3:
				expiresAt = Timestamp.FromDateTime(DateTime.UtcNow.AddDays(1.0));
				break;
			case 4:
				expiresAt = Timestamp.FromDateTime(DateTime.UtcNow.AddDays(7.0));
				break;
			}
			int? maxUses = null;
			switch (MaxUsesSelectionIndex)
			{
			case 1:
				maxUses = 1;
				break;
			case 2:
				maxUses = 5;
				break;
			case 3:
				maxUses = 10;
				break;
			case 4:
				maxUses = 25;
				break;
			case 5:
				maxUses = 50;
				break;
			case 6:
				maxUses = 100;
				break;
			}
			CommunityInviteLinkCreateResponse linkResponse = await _rootSessionAccessor.Session.LinkService.CreateCommunityInviteLinkAsync(_community.Id, expiresAt, maxUses);
			_generateInviteLinkCallback(linkResponse);
		}
		catch
		{
		}
		finally
		{
			CloseViewModel();
		}
	}

	[RelayCommand]
	public void CloseViewModel()
	{
		WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this));
	}
}

