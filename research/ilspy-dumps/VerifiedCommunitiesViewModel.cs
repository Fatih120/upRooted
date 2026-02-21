using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using Microsoft.VisualStudio.Threading;
using RootApp.Client.Avalonia.Controls.Settings;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.LinkJoining;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.CoreDomain;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.Client.Avalonia.UI.NewTab;

public class VerifiedCommunitiesViewModel : ViewModelBase<VerifiedCommunitiesViewModel>, IPage
{
	private readonly LinkJoiningService _linkJoiningService;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly BitmapCache _bitmapCache;

	private readonly Action? _onCommunityOpened;

	[CompilerGenerated]
	private CommunityInviteLinkGetInfoResponse? _003CGeneralCommunityInfo_003Ek__BackingField;

	[CompilerGenerated]
	private CommunityInviteLinkGetInfoResponse? _003CDeveloperCommunityInfo_003Ek__BackingField;

	[CompilerGenerated]
	private CommunityInviteLinkGetInfoResponse? _003CHytaleFoundryCommunityInfo_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsLoading_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? joinGeneralCommunityCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? joinDeveloperCommunityCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? joinHytaleFoundryCommunityCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public CommunityInviteLinkGetInfoResponse? GeneralCommunityInfo
	{
		get
		{
			return _003CGeneralCommunityInfo_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<CommunityInviteLinkGetInfoResponse>.Default.Equals(_003CGeneralCommunityInfo_003Ek__BackingField, communityInviteLinkGetInfoResponse))
			{
				_003CGeneralCommunityInfo_003Ek__BackingField = communityInviteLinkGetInfoResponse;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.GeneralCommunityInfo);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public CommunityInviteLinkGetInfoResponse? DeveloperCommunityInfo
	{
		get
		{
			return _003CDeveloperCommunityInfo_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<CommunityInviteLinkGetInfoResponse>.Default.Equals(_003CDeveloperCommunityInfo_003Ek__BackingField, communityInviteLinkGetInfoResponse))
			{
				_003CDeveloperCommunityInfo_003Ek__BackingField = communityInviteLinkGetInfoResponse;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.DeveloperCommunityInfo);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public CommunityInviteLinkGetInfoResponse? HytaleFoundryCommunityInfo
	{
		get
		{
			return _003CHytaleFoundryCommunityInfo_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<CommunityInviteLinkGetInfoResponse>.Default.Equals(_003CHytaleFoundryCommunityInfo_003Ek__BackingField, communityInviteLinkGetInfoResponse))
			{
				_003CHytaleFoundryCommunityInfo_003Ek__BackingField = communityInviteLinkGetInfoResponse;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.HytaleFoundryCommunityInfo);
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

	public Task<BitmapWrapper?> GeneralCommunityPicture => _bitmapCache.GetBitmapAsync(GeneralCommunityInfo?.PictureAssetUri, null, 128);

	public Task<BitmapWrapper?> DeveloperCommunityPicture => _bitmapCache.GetBitmapAsync(DeveloperCommunityInfo?.PictureAssetUri, null, 128);

	public Task<BitmapWrapper?> HytaleFoundryCommunityPicture => _bitmapCache.GetBitmapAsync(HytaleFoundryCommunityInfo?.PictureAssetUri, null, 128);

	public string PageTitle => RootApp.Client.Avalonia.Resources.Strings.Resources.VerifiedCommunities;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand JoinGeneralCommunityCommand => joinGeneralCommunityCommand ?? (joinGeneralCommunityCommand = new AsyncRelayCommand(JoinGeneralCommunityAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand JoinDeveloperCommunityCommand => joinDeveloperCommunityCommand ?? (joinDeveloperCommunityCommand = new AsyncRelayCommand(JoinDeveloperCommunityAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand JoinHytaleFoundryCommunityCommand => joinHytaleFoundryCommunityCommand ?? (joinHytaleFoundryCommunityCommand = new AsyncRelayCommand(JoinHytaleFoundryCommunityAsync));

	public VerifiedCommunitiesViewModel(LinkJoiningService P_0, IRootSessionAccessor P_1, BitmapCache P_2, Action? P_3 = null)
		: base((IValidator<VerifiedCommunitiesViewModel>?)null)
	{
		_linkJoiningService = P_0;
		_rootSessionAccessor = P_1;
		_bitmapCache = P_2;
		_onCommunityOpened = P_3;
		LoadCommunityInfoAsync().Forget();
	}

	private async Task LoadCommunityInfoAsync()
	{
		IsLoading = true;
		try
		{
			GeneralCommunityInfo = await _rootSessionAccessor.Session.LinkService.GetCommunityInviteLinkInfoAsync("root");
			OnPropertyChanged("GeneralCommunityPicture");
		}
		catch
		{
		}
		try
		{
			DeveloperCommunityInfo = await _rootSessionAccessor.Session.LinkService.GetCommunityInviteLinkInfoAsync("developer");
			OnPropertyChanged("DeveloperCommunityPicture");
		}
		catch
		{
		}
		try
		{
			HytaleFoundryCommunityInfo = await _rootSessionAccessor.Session.LinkService.GetCommunityInviteLinkInfoAsync("ACuxLK5shQqYlXqJeaT8Hw");
			OnPropertyChanged("HytaleFoundryCommunityPicture");
		}
		catch
		{
		}
		IsLoading = false;
	}

	[RelayCommand]
	public async Task JoinGeneralCommunityAsync()
	{
		if (await _linkJoiningService.OpenLinkAsync("root", _onCommunityOpened) == LinkJoinResult.AlreadyMember)
		{
			_onCommunityOpened?.Invoke();
		}
	}

	[RelayCommand]
	public async Task JoinDeveloperCommunityAsync()
	{
		if (await _linkJoiningService.OpenLinkAsync("developer", _onCommunityOpened) == LinkJoinResult.AlreadyMember)
		{
			_onCommunityOpened?.Invoke();
		}
	}

	[RelayCommand]
	public async Task JoinHytaleFoundryCommunityAsync()
	{
		if (await _linkJoiningService.OpenLinkAsync("ACuxLK5shQqYlXqJeaT8Hw", _onCommunityOpened) == LinkJoinResult.AlreadyMember)
		{
			_onCommunityOpened?.Invoke();
		}
	}
}
