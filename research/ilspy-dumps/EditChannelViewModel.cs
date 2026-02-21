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
using RootApp.Client.Avalonia.Controls.ImageUpload;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.Avalonia.UI.Community.Channels.Permissions;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.Client.Avalonia.UI.Community.Channels;

public class EditChannelViewModel : ViewModelBase<EditChannelViewModel>
{
	[CompilerGenerated]
	private string <ChannelName>k__BackingField;

	[CompilerGenerated]
	private string <ChannelDescription>k__BackingField;

	[CompilerGenerated]
	private WebApiStatus <WebApiStatus>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? saveChannelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeViewModelCommand;

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("SaveChannelCommand")]
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
				SaveChannelCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("SaveChannelCommand")]
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
				SaveChannelCommand.NotifyCanExecuteChanged();
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

	public Channel Channel { get; }

	public ImageUploaderViewModel ImageUploaderViewModel { get; }

	public AccessRuleSelectorViewModel AccessRuleSelector { get; }

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand SaveChannelCommand => saveChannelCommand ?? (saveChannelCommand = new AsyncRelayCommand(SaveChannelAsync, canEditChannel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	public EditChannelViewModel(Channel P_0, AccessRuleSelectorViewModelFactory P_1, ImageUploaderViewModelFactory P_2)
		: base((IValidator<EditChannelViewModel>?)new EditChannelViewModelValidator())
	{
		Channel = P_0;
		ChannelName = Channel.Name;
		ChannelDescription = Channel.Description;
		ImageUploaderViewModel = P_2.Create(ImageUploadType.ChannelIcon, Channel.IconAssetUri);
		ImageUploaderViewModel.UploadStarted += imageUploaderViewModelUploadStarted;
		ImageUploaderViewModel.UploadCompleted += imageUploaderViewModelUploadCompleted;
		AccessRuleSelector = P_1.Create(Channel.Community, false, Channel.PermissionsSyncedToChannelGroup, Channel.Id, Channel.Type == ChannelType.App);
	}

	[RelayCommand(CanExecute = "canEditChannel")]
	public async Task SaveChannelAsync()
	{
		try
		{
			WebApiStatus = WebApiStatus.Sending;
			bool updateIcon = false;
			string uploadToken = null;
			if (Channel.IconAssetUri != ImageUploaderViewModel.PictureUrl)
			{
				updateIcon = true;
				uploadToken = ImageUploaderViewModel.UploadToken?.ToString();
			}
			await Channel.Community.Channels.EditChannelAsync(Channel.Id, ChannelName, ChannelDescription, AccessRuleSelector.GetSyncPermissionsValue(), AccessRuleSelector.GetAccessRuleUpdateRequest(), updateIcon, uploadToken);
			WebApiStatus = WebApiStatus.Success;
		}
		catch
		{
			WebApiStatus = WebApiStatus.Failed;
		}
	}

	private bool canEditChannel()
	{
		return !ImageUploaderViewModel.IsUploading && base.HasNoErrors;
	}

	[RelayCommand]
	public void CloseViewModel()
	{
		WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this));
	}

	private void imageUploaderViewModelUploadCompleted()
	{
		SaveChannelCommand.NotifyCanExecuteChanged();
	}

	private void imageUploaderViewModelUploadStarted()
	{
		SaveChannelCommand.NotifyCanExecuteChanged();
	}

	public override void Dispose()
	{
		base.Dispose();
		ImageUploaderViewModel.UploadStarted -= imageUploaderViewModelUploadStarted;
		ImageUploaderViewModel.UploadCompleted -= imageUploaderViewModelUploadCompleted;
		AccessRuleSelector.Dispose();
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
