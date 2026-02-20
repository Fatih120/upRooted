// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.BlockedUserViewModel
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.User;
using RootApp.Client.CoreDomain.Services;
using RootApp.Core.Identifiers;

public class BlockedUserViewModel : ViewModelBase<BlockedUserViewModel>
{
	private readonly UserGuid _userId;

	private readonly BitmapCache _bitmapCache;

	private readonly IGlobalUserCacheService _globalUserCacheService;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	[CompilerGenerated]
	private string _003CUserName_003Ek__BackingField = string.Empty;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? unblockUserCommand;

	public Task<BitmapWrapper?> ProfilePictureAsyncBitmapWrapper => getProfilePicAsync();

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string UserName
	{
		get
		{
			return _003CUserName_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CUserName_003Ek__BackingField, text))
			{
				_003CUserName_003Ek__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.UserName);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand UnblockUserCommand => unblockUserCommand ?? (unblockUserCommand = new AsyncRelayCommand(UnblockUser));

	public BlockedUserViewModel(UserGuid P_0, BitmapCache P_1, IGlobalUserCacheService P_2, IRootSessionAccessor P_3)
		: base((IValidator<BlockedUserViewModel>?)null)
	{
		_userId = P_0;
		_bitmapCache = P_1;
		_globalUserCacheService = P_2;
		_rootSessionAccessor = P_3;
	}

	[RelayCommand]
	public async Task UnblockUser()
	{
		try
		{
			await _rootSessionAccessor.Session.UserBlockService.UnblockUserAsync(_userId);
		}
		catch
		{
		}
	}

	public async Task<BitmapWrapper?> getProfilePicAsync()
	{
		GlobalUser globalUser = await _globalUserCacheService.GetUserByIdAsync(_userId);
		if (globalUser != null)
		{
			UserName = globalUser.UserName;
			return await _bitmapCache.GetBitmapAsync(globalUser.ProfilePictureUri, null, 120);
		}
		return null;
	}
}

