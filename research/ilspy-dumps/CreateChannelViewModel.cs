using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using Google.Protobuf.Collections;
using RootApp.App.Settings;
using RootApp.Client.Avalonia.Controls.ImageUpload;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.Avalonia.UI.Community.Apps.AppStore;
using RootApp.Client.Avalonia.UI.Community.Apps.GlobalSettings;
using RootApp.Client.Avalonia.UI.Community.Channels.Permissions;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.WebApi.Shared.Enums;
using RootApp.WebApi.Shared.Grpc.Requests;
using RootApp.WebApi.Shared.Grpc.Responses;

namespace RootApp.Client.Avalonia.UI.Community.Channels;

public class CreateChannelViewModel : ViewModelBase<CreateChannelViewModel>
{
	private readonly AppStoreViewModelFactory _appStoreViewModelFactory;

	private readonly GlobalSettingsSelectorViewModelFactory _globalSettingsSelectorViewModelFactory;

	private readonly AppAlreadyInCommunityViewModelFactory _appAlreadyInCommunityViewModelFactory;

	private readonly BitmapCache _bitmapCache;

	[CompilerGenerated]
	private Action? m_NavigateToGlobalSettingsRequested;

	[CompilerGenerated]
	private string <ChannelName>k__BackingField;

	[CompilerGenerated]
	private string <ChannelDescription>k__BackingField;

	[CompilerGenerated]
	private ChannelType <SelectedChannelType>k__BackingField;

	[CompilerGenerated]
	private WebApiStatus <WebApiStatus>k__BackingField;

	[CompilerGenerated]
	private AppStoreGetResponse? <SelectedApp>k__BackingField;

	[CompilerGenerated]
	private bool <ShowSelectAppButton>k__BackingField;

	[CompilerGenerated]
	private GlobalSettingsSelectorViewModel? <GlobalSettingsSelectorViewModel>k__BackingField;

	[CompilerGenerated]
	private bool <HasGlobalSettings>k__BackingField;

	[CompilerGenerated]
	private bool <NeedsRequiredSettings>k__BackingField;

	[CompilerGenerated]
	private bool <HasRequiredGlobalSettings>k__BackingField;

	[CompilerGenerated]
	private bool <IsOnGlobalSettingsTab>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? createChannelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? selectAppCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeViewModelCommand;

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("CreateChannelCommand")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string ChannelName
	{
		get
		{
			return <ChannelName>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<ChannelName>k__BackingField, text))
			{
				<ChannelName>k__BackingField = text;
				OnChannelNameChanged(text);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ChannelName);
				CreateChannelCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("CreateChannelCommand")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string ChannelDescription
	{
		get
		{
			return <ChannelDescription>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<ChannelDescription>k__BackingField, text))
			{
				<ChannelDescription>k__BackingField = text;
				OnChannelDescriptionChanged(text);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ChannelDescription);
				CreateChannelCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public ChannelType SelectedChannelType
	{
		get
		{
			return <SelectedChannelType>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<ChannelType>.Default.Equals(<SelectedChannelType>k__BackingField, channelType))
			{
				<SelectedChannelType>k__BackingField = channelType;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SelectedChannelType);
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
			return <WebApiStatus>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<WebApiStatus>.Default.Equals(<WebApiStatus>k__BackingField, webApiStatus))
			{
				<WebApiStatus>k__BackingField = webApiStatus;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.WebApiStatus);
			}
		}
	}

	[ObservableProperty]
	[NotifyPropertyChangedFor("SelectedAppIconAsyncBitmapWrapper")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public AppStoreGetResponse? SelectedApp
	{
		get
		{
			return <SelectedApp>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<AppStoreGetResponse>.Default.Equals(<SelectedApp>k__BackingField, appStoreGetResponse))
			{
				<SelectedApp>k__BackingField = appStoreGetResponse;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SelectedApp);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SelectedAppIconAsyncBitmapWrapper);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ShowSelectAppButton
	{
		get
		{
			return <ShowSelectAppButton>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<ShowSelectAppButton>k__BackingField, flag))
			{
				<ShowSelectAppButton>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShowSelectAppButton);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public GlobalSettingsSelectorViewModel? GlobalSettingsSelectorViewModel
	{
		get
		{
			return <GlobalSettingsSelectorViewModel>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<RootApp.Client.Avalonia.UI.Community.Apps.GlobalSettings.GlobalSettingsSelectorViewModel>.Default.Equals(<GlobalSettingsSelectorViewModel>k__BackingField, globalSettingsSelectorViewModel))
			{
				<GlobalSettingsSelectorViewModel>k__BackingField = globalSettingsSelectorViewModel;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.GlobalSettingsSelectorViewModel);
			}
		}
	}

	public Task<BitmapWrapper?>? SelectedAppIconAsyncBitmapWrapper => (SelectedApp != null) ? _bitmapCache.GetBitmapAsync(SelectedApp.IconAssetUri, null, 60) : null;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool HasGlobalSettings
	{
		get
		{
			return <HasGlobalSettings>k__BackingField;
		}
		private set
		{
			if (!EqualityComparer<bool>.Default.Equals(<HasGlobalSettings>k__BackingField, flag))
			{
				<HasGlobalSettings>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.HasGlobalSettings);
			}
		}
	}

	[ObservableProperty]
	[NotifyPropertyChangedFor("CreateButtonText")]
	[NotifyPropertyChangedFor("CanClickCreateButton")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool NeedsRequiredSettings
	{
		get
		{
			return <NeedsRequiredSettings>k__BackingField;
		}
		private set
		{
			if (!EqualityComparer<bool>.Default.Equals(<NeedsRequiredSettings>k__BackingField, flag))
			{
				<NeedsRequiredSettings>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.NeedsRequiredSettings);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.CreateButtonText);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.CanClickCreateButton);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	private bool HasRequiredGlobalSettings
	{
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<HasRequiredGlobalSettings>k__BackingField, flag))
			{
				<HasRequiredGlobalSettings>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.HasRequiredGlobalSettings);
			}
		}
	}

	[ObservableProperty]
	[NotifyPropertyChangedFor("CreateButtonText")]
	[NotifyPropertyChangedFor("CanClickCreateButton")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsOnGlobalSettingsTab
	{
		get
		{
			return <IsOnGlobalSettingsTab>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsOnGlobalSettingsTab>k__BackingField, flag))
			{
				<IsOnGlobalSettingsTab>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsOnGlobalSettingsTab);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.CreateButtonText);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.CanClickCreateButton);
			}
		}
	}

	public string CreateButtonText => (NeedsRequiredSettings && !IsOnGlobalSettingsTab) ? "Next" : "Create";

	public bool CanClickCreateButton => !IsOnGlobalSettingsTab || !NeedsRequiredSettings;

	public AccessRuleSelectorViewModel AccessRuleSelector { get; }

	public ChannelGroup ChannelGroup { get; }

	public ImageUploaderViewModel ImageUploaderViewModel { get; }

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand CreateChannelCommand => createChannelCommand ?? (createChannelCommand = new AsyncRelayCommand(CreateChannelAsync, canCreateChannel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand SelectAppCommand => selectAppCommand ?? (selectAppCommand = new RelayCommand(SelectApp));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	public event Action? NavigateToGlobalSettingsRequested
	{
		[CompilerGenerated]
		add
		{
			Action action = this.m_NavigateToGlobalSettingsRequested;
			Action action2;
			do
			{
				action2 = action;
				Action action3 = (Action)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_NavigateToGlobalSettingsRequested, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action action = this.m_NavigateToGlobalSettingsRequested;
			Action action2;
			do
			{
				action2 = action;
				Action action3 = (Action)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_NavigateToGlobalSettingsRequested, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public CreateChannelViewModel(ChannelGroup P_0, AccessRuleSelectorViewModelFactory P_1, ImageUploaderViewModelFactory P_2, AppStoreViewModelFactory P_3, GlobalSettingsSelectorViewModelFactory P_4, AppAlreadyInCommunityViewModelFactory P_5, BitmapCache P_6)
		: base((IValidator<CreateChannelViewModel>?)new CreateChannelViewModelValidator())
	{
		_appStoreViewModelFactory = P_3;
		_globalSettingsSelectorViewModelFactory = P_4;
		_appAlreadyInCommunityViewModelFactory = P_5;
		_bitmapCache = P_6;
		ChannelGroup = P_0;
		ChannelName = string.Empty;
		ChannelDescription = string.Empty;
		SelectedChannelType = ChannelType.Text;
		ImageUploaderViewModel = P_2.Create(ImageUploadType.ChannelIcon);
		ImageUploaderViewModel.UploadStarted += imageUploaderViewModelUploadStarted;
		ImageUploaderViewModel.UploadCompleted += imageUploaderViewModelUploadCompleted;
		AccessRuleSelector = P_1.Create(ChannelGroup.Community, true, true, null);
		WeakReferenceMessenger.Default.Register<SelectAppForCreateChannelMessage>(this, onSelectAppForCreateChannelMessageReceived);
	}

	[RelayCommand(CanExecute = "canCreateChannel")]
	public async Task CreateChannelAsync()
	{
		if (NeedsRequiredSettings && !IsOnGlobalSettingsTab)
		{
			IsOnGlobalSettingsTab = true;
			this.NavigateToGlobalSettingsRequested?.Invoke();
			return;
		}
		try
		{
			WebApiStatus = WebApiStatus.Sending;
			if (SelectedApp == null)
			{
				await ChannelGroup.Community.Channels.CreateChannelAsync(ChannelGroup.Id, SelectedChannelType, ChannelName, ChannelDescription, AccessRuleSelector.GetSyncPermissionsValue(), AccessRuleSelector.GetAccessRuleCreateRoleOrMemberRequests(), ImageUploaderViewModel.UploadToken?.ToString());
			}
			else
			{
				await ChannelGroup.Community.Apps.AddAsync(new ChannelCreateRequest
				{
					ChannelGroupId = ChannelGroup.Id,
					CommunityId = ChannelGroup.Community.Id,
					ChannelType = 8,
					Name = ChannelName,
					Description = ChannelDescription,
					UseChannelGroupPermission = AccessRuleSelector.GetSyncPermissionsValue(),
					AccessRuleCreates = { AccessRuleSelector.GetAccessRuleCreateRoleOrMemberRequests() },
					IconTokenUri = ImageUploaderViewModel.UploadToken?.ToString()
				}, SelectedApp.Id, SelectedApp.GlobalSettings);
			}
			WebApiStatus = WebApiStatus.Success;
		}
		catch
		{
			WebApiStatus = WebApiStatus.Failed;
		}
	}

	[RelayCommand]
	public void SelectApp()
	{
		AppStoreViewModel appStoreViewModel = _appStoreViewModelFactory.Create(ChannelGroup.Community);
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(appStoreViewModel));
	}

	protected override void OnPropertyChanged(PropertyChangedEventArgs P_0)
	{
		base.OnPropertyChanged(P_0);
		if (P_0.PropertyName == "SelectedChannelType" && SelectedChannelType != ChannelType.Unspecified)
		{
			ShowSelectAppButton = SelectedChannelType == ChannelType.App;
			if (AccessRuleSelector != null)
			{
				AccessRuleSelector.IsAppChannel = SelectedChannelType == ChannelType.App;
			}
		}
	}

	[RelayCommand]
	public void CloseViewModel()
	{
		WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this));
	}

	private void onSelectAppForCreateChannelMessageReceived(object recipient, SelectAppForCreateChannelMessage message)
	{
		CommunityApp communityApp = ChannelGroup.Community.Apps?.GetAppByAppId(message.SelectedApp.Id);
		if (communityApp != null)
		{
			AppAlreadyInCommunityViewModel appAlreadyInCommunityViewModel = _appAlreadyInCommunityViewModelFactory.Create(message.SelectedApp.Name);
			WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(appAlreadyInCommunityViewModel));
			return;
		}
		SelectedApp = message.SelectedApp;
		ShowSelectAppButton = false;
		RepeatedField<GlobalSettingGroup> repeatedField = SelectedApp.GlobalSettings?.Groups;
		HasGlobalSettings = repeatedField != null && repeatedField.Count > 0;
		if (HasGlobalSettings)
		{
			GlobalSettingsSelectorViewModel = _globalSettingsSelectorViewModelFactory.Create(SelectedApp.GlobalSettings, ChannelGroup.Community);
			HasRequiredGlobalSettings = GlobalSettingsSelectorViewModel.HasRequiredSettings;
			updateNeedsRequiredSettings();
			GlobalSettingsSelectorViewModel.PropertyChanged += onGlobalSettingsSelectorViewModelPropertyChanged;
		}
		else
		{
			GlobalSettingsSelectorViewModel = null;
			HasRequiredGlobalSettings = false;
			NeedsRequiredSettings = false;
		}
	}

	private void onGlobalSettingsSelectorViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "AreRequiredSettingsFilled")
		{
			updateNeedsRequiredSettings();
		}
	}

	private void updateNeedsRequiredSettings()
	{
		NeedsRequiredSettings = GlobalSettingsSelectorViewModel != null && GlobalSettingsSelectorViewModel.HasRequiredSettings && !GlobalSettingsSelectorViewModel.AreRequiredSettingsFilled;
	}

	private bool canCreateChannel()
	{
		return !ImageUploaderViewModel.IsUploading && base.HasNoErrors;
	}

	private void imageUploaderViewModelUploadCompleted()
	{
		CreateChannelCommand.NotifyCanExecuteChanged();
	}

	private void imageUploaderViewModelUploadStarted()
	{
		CreateChannelCommand.NotifyCanExecuteChanged();
	}

	public override void Dispose()
	{
		base.Dispose();
		ImageUploaderViewModel.UploadStarted -= imageUploaderViewModelUploadStarted;
		ImageUploaderViewModel.UploadCompleted -= imageUploaderViewModelUploadCompleted;
		AccessRuleSelector.Dispose();
		if (GlobalSettingsSelectorViewModel != null)
		{
			GlobalSettingsSelectorViewModel.PropertyChanged -= onGlobalSettingsSelectorViewModelPropertyChanged;
			GlobalSettingsSelectorViewModel.Dispose();
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnChannelNameChanged(string P_0)
	{
		ValidateProperty("ChannelName");
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnChannelDescriptionChanged(string P_0)
	{
		ValidateProperty("ChannelDescription");
	}
}
