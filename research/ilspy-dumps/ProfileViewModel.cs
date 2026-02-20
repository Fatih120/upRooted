// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.ProfileViewModel
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using Microsoft.Extensions.Logging;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls.Support;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Installation;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.User;
using RootApp.Client.CoreDomain.Services;
using RootApp.WebApi.Shared.Enums;

public class ProfileViewModel : ViewModelBase<ProfileViewModel>
{
	private readonly IRootService _rootService;

	private readonly BitmapCache _bitmapCache;

	private readonly ProfileSettingsViewModelFactory _profileSettingsViewModelFactory;

	private readonly SignOutConfirmationViewModelFactory _signOutConfirmationViewModelFactory;

	private readonly SupportViewModelFactory _supportViewModelFactory;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly ILogger<ProfileViewModel> _logger;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showProfileSettingsCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showSupportFormCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showSignOutConfirmationViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? restartClientCommand;

	public SessionUser SessionUser { get; }

	public RootInstallationManager InstallationManager { get; }

	public Task<BitmapWrapper?> ProfilePictureAsyncBitmapWrapper => _bitmapCache.GetBitmapAsync(SessionUser.ProfilePictureUri, null, 120);

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowProfileSettingsCommand => showProfileSettingsCommand ?? (showProfileSettingsCommand = new RelayCommand(ShowProfileSettings));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowSupportFormCommand => showSupportFormCommand ?? (showSupportFormCommand = new RelayCommand(ShowSupportForm));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowSignOutConfirmationViewModelCommand => showSignOutConfirmationViewModelCommand ?? (showSignOutConfirmationViewModelCommand = new RelayCommand(ShowSignOutConfirmationViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand RestartClientCommand => restartClientCommand ?? (restartClientCommand = new RelayCommand(RestartClient));

	public ProfileViewModel(IRootSessionAccessor P_0, BitmapCache P_1, ILoggerFactory P_2, ProfileSettingsViewModelFactory P_3, SignOutConfirmationViewModelFactory P_4, SupportViewModelFactory P_5, IRootService P_6, RootInstallationManager P_7)
		: base((IValidator<ProfileViewModel>?)null)
	{
		InstallationManager = P_7;
		_bitmapCache = P_1;
		_profileSettingsViewModelFactory = P_3;
		_signOutConfirmationViewModelFactory = P_4;
		_supportViewModelFactory = P_5;
		_rootSessionAccessor = P_0;
		_logger = P_2.CreateLogger<ProfileViewModel>();
		_rootService = P_6;
		SessionUser = _rootSessionAccessor.Session.UserInfoService.SessionUser;
		SessionUser.PropertyChanged += onSessionUserPropertyChanged;
	}

	private void onSessionUserPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "ProfilePictureUri")
			{
				OnPropertyChanged("ProfilePictureAsyncBitmapWrapper");
			}
		});
	}

	[RelayCommand]
	public void ShowProfileSettings()
	{
		ProfileSettingsViewModel profileSettingsViewModel = _profileSettingsViewModelFactory.Create();
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(profileSettingsViewModel, "ProfileSettingsViewModel"));
	}

	[RelayCommand]
	public void ShowSupportForm()
	{
		SupportViewModel supportViewModel = _supportViewModelFactory.Create();
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(supportViewModel));
	}

	[RelayCommand]
	public void ShowSignOutConfirmationViewModel()
	{
		SignOutConfirmationViewModel signOutConfirmationViewModel = _signOutConfirmationViewModelFactory.Create();
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(signOutConfirmationViewModel));
	}

	[RelayCommand]
	public void RestartClient()
	{
		InstallationManager.PublishUpdates();
	}

	public async Task SetStatusAsync(UserOnlineStatus P_0)
	{
		try
		{
			await _rootSessionAccessor.Session.UserInfoService.SetMaxOnlineStatusAsync(P_0);
		}
		catch
		{
		}
	}

	public override void Dispose()
	{
		base.Dispose();
		SessionUser.PropertyChanged -= onSessionUserPropertyChanged;
	}
}

