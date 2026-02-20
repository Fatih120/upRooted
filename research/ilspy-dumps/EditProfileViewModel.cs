// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.EditProfileViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls.ImageUpload;
using RootApp.Client.Avalonia.Controls.Settings;
using RootApp.Client.Avalonia.Helpers.Navigation;
using RootApp.Client.Avalonia.Helpers.StreamerMode;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.User;

public class EditProfileViewModel : ViewModelBase<EditProfileViewModel>, IPage
{
	private readonly Navigator _navigator;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly ChangePasswordViewModelFactory _changePasswordViewModelFactory;

	private readonly IStreamerModeService _streamerModeService;

	private readonly SessionUser _sessionUser;

	private SessionUserMirror _originalSessionUser;

	[CompilerGenerated]
	private string _003CUsername_003Ek__BackingField;

	[CompilerGenerated]
	private string _003CEmail_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showChangePasswordViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? saveChangesCommand;

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("SaveChangesCommand")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string Username
	{
		get
		{
			return _003CUsername_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CUsername_003Ek__BackingField, text))
			{
				_003CUsername_003Ek__BackingField = text;
				OnUsernameChanged(text);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Username);
				SaveChangesCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("SaveChangesCommand")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string Email
	{
		get
		{
			return _003CEmail_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CEmail_003Ek__BackingField, text))
			{
				_003CEmail_003Ek__BackingField = text;
				OnEmailChanged(text);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Email);
				SaveChangesCommand.NotifyCanExecuteChanged();
			}
		}
	}

	public ImageUploaderViewModel ImageUploaderViewModel { get; }

	public string PageTitle => Resources.UserProfile;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowChangePasswordViewModelCommand => showChangePasswordViewModelCommand ?? (showChangePasswordViewModelCommand = new RelayCommand(ShowChangePasswordViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand SaveChangesCommand => saveChangesCommand ?? (saveChangesCommand = new AsyncRelayCommand(SaveChangesAsync, () => base.HasNoErrors));

	public EditProfileViewModel(Navigator P_0, ImageUploaderViewModelFactory P_1, IRootSessionAccessor P_2, ChangePasswordViewModelFactory P_3, IStreamerModeService P_4)
		: base((IValidator<EditProfileViewModel>?)new EditProfileViewModelValidator())
	{
		_sessionUser = P_2.Session.UserInfoService.SessionUser;
		_originalSessionUser = new SessionUserMirror(_sessionUser);
		_navigator = P_0;
		_rootSessionAccessor = P_2;
		_changePasswordViewModelFactory = P_3;
		_streamerModeService = P_4;
		ImageUploaderViewModel = P_1.Create(ImageUploadType.ProfilePicture, _sessionUser.ProfilePictureUri);
		Username = _sessionUser.UserName;
		Email = getDisplayEmail();
		_navigator.SaveChangesRequested += onSaveChangesRequested;
		_navigator.RevertChangesRequested += onRevertChangesRequested;
		ImageUploaderViewModel.ImageRemoved += imageUploaderViewModelImageRemoved;
		ImageUploaderViewModel.UploadStarted += imageUploaderViewModelUploadStarted;
		ImageUploaderViewModel.UploadCompleted += imageUploaderViewModelUploadCompleted;
		_streamerModeService.PropertyChanged += onStreamerModeServicePropertyChanged;
	}

	private string getDisplayEmail()
	{
		return _streamerModeService.ShouldHidePersonalInfo ? Resources.EmailHidden : _sessionUser.Email;
	}

	private void onStreamerModeServicePropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "ShouldHidePersonalInfo" || e.PropertyName == "IsEnabled")
		{
			Email = getDisplayEmail();
		}
	}

	[RelayCommand]
	public void ShowChangePasswordViewModel()
	{
		ChangePasswordViewModel changePasswordViewModel = _changePasswordViewModelFactory.Create();
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(changePasswordViewModel));
	}

	private void onSaveChangesRequested()
	{
		if (SaveChangesCommand.CanExecute(null))
		{
			SaveChangesCommand.Execute(null);
		}
	}

	[RelayCommand(CanExecute = "HasNoErrors")]
	public async Task SaveChangesAsync()
	{
		try
		{
			_navigator.WebApiStatus = WebApiStatus.Sending;
			List<Task> tasks = new List<Task>();
			if (hasUsernameChanges())
			{
				tasks.Add(_rootSessionAccessor.Session.UserInfoService.UpdateUsernameAsync(Username));
			}
			if (hasProfilePictureChanges())
			{
				tasks.Add(_rootSessionAccessor.Session.UserInfoService.UpdateProfilePictureAsync(ImageUploaderViewModel.UploadToken?.ToString()));
			}
			await Task.WhenAll(tasks);
			reInitializeFields();
			_navigator.WebApiStatus = WebApiStatus.Success;
		}
		catch
		{
			_navigator.WebApiStatus = WebApiStatus.Failed;
		}
	}

	private void checkForChanges()
	{
		_navigator.HasPendingChanges = hasUsernameChanges() || hasProfilePictureChanges();
	}

	private bool hasUsernameChanges()
	{
		return !string.Equals(_originalSessionUser.Username, Username, StringComparison.Ordinal);
	}

	private bool hasProfilePictureChanges()
	{
		return !string.Equals(_originalSessionUser.PictureUrl, ImageUploaderViewModel.PictureUrl, StringComparison.OrdinalIgnoreCase);
	}

	private void imageUploaderViewModelImageRemoved()
	{
		_navigator.CanSave = true;
		checkForChanges();
	}

	private void imageUploaderViewModelUploadStarted()
	{
		_navigator.CanSave = false;
		checkForChanges();
	}

	private void imageUploaderViewModelUploadCompleted()
	{
		_navigator.CanSave = true;
		checkForChanges();
	}

	private void onRevertChangesRequested()
	{
		reInitializeFields();
	}

	private void reInitializeFields()
	{
		_originalSessionUser = new SessionUserMirror(_sessionUser);
		Username = _sessionUser.UserName;
		Email = getDisplayEmail();
		ImageUploaderViewModel.PictureUrl = _sessionUser.ProfilePictureUri;
		checkForChanges();
	}

	public override void Dispose()
	{
		base.Dispose();
		_navigator.SaveChangesRequested -= onSaveChangesRequested;
		_navigator.RevertChangesRequested -= onRevertChangesRequested;
		ImageUploaderViewModel.ImageRemoved -= imageUploaderViewModelImageRemoved;
		ImageUploaderViewModel.UploadStarted -= imageUploaderViewModelUploadStarted;
		ImageUploaderViewModel.UploadCompleted -= imageUploaderViewModelUploadCompleted;
		_streamerModeService.PropertyChanged -= onStreamerModeServicePropertyChanged;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnUsernameChanged(string P_0)
	{
		ValidateProperty("Username");
		checkForChanges();
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnEmailChanged(string P_0)
	{
		ValidateProperty("Username");
		checkForChanges();
	}
}

