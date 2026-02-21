using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using RootApp.Client.Avalonia.Helpers.Clipboard;
using RootApp.Client.Avalonia.Helpers.DeveloperMode;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.WebApi.Shared.Enums;

namespace RootApp.Client.Avalonia.UI.Community.Channels;

public class ChannelGroupViewModel : ViewModelBase<ChannelGroupViewModel>
{
	private readonly CreateChannelViewModelFactory _createChannelViewModelFactory;

	private readonly CreateChannelGroupViewModelFactory _createChannelGroupViewModelFactory;

	private readonly EditChannelGroupViewModelFactory _editChannelGroupViewModelFactory;

	private readonly DeleteChannelGroupViewModelFactory _deleteChannelGroupViewModelFactory;

	private readonly ChannelGroupContainsAppViewModelFactory _channelGroupContainsAppViewModelFactory;

	private readonly IDeveloperModeService _developerModeService;

	private readonly ClipboardService _clipboardService;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? copyChannelGroupIdCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showCreateChannelViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showCreateChannelGroupViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showEditChannelGroupViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showDeleteChannelGroupViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? toggleCollapseCommand;

	public ChannelGroup ChannelGroup { get; }

	public bool DeveloperModeEnabled => _developerModeService.IsEnabled;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CopyChannelGroupIdCommand => copyChannelGroupIdCommand ?? (copyChannelGroupIdCommand = new RelayCommand(CopyChannelGroupId));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowCreateChannelViewModelCommand => showCreateChannelViewModelCommand ?? (showCreateChannelViewModelCommand = new RelayCommand(ShowCreateChannelViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowCreateChannelGroupViewModelCommand => showCreateChannelGroupViewModelCommand ?? (showCreateChannelGroupViewModelCommand = new RelayCommand(ShowCreateChannelGroupViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowEditChannelGroupViewModelCommand => showEditChannelGroupViewModelCommand ?? (showEditChannelGroupViewModelCommand = new RelayCommand(ShowEditChannelGroupViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowDeleteChannelGroupViewModelCommand => showDeleteChannelGroupViewModelCommand ?? (showDeleteChannelGroupViewModelCommand = new RelayCommand(ShowDeleteChannelGroupViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ToggleCollapseCommand => toggleCollapseCommand ?? (toggleCollapseCommand = new RelayCommand(ToggleCollapse));

	public ChannelGroupViewModel(ChannelGroup P_0, CreateChannelViewModelFactory P_1, CreateChannelGroupViewModelFactory P_2, EditChannelGroupViewModelFactory P_3, DeleteChannelGroupViewModelFactory P_4, ChannelGroupContainsAppViewModelFactory P_5, IDeveloperModeService P_6, ClipboardService P_7)
		: base((IValidator<ChannelGroupViewModel>?)null)
	{
		ChannelGroup = P_0;
		_createChannelViewModelFactory = P_1;
		_createChannelGroupViewModelFactory = P_2;
		_editChannelGroupViewModelFactory = P_3;
		_deleteChannelGroupViewModelFactory = P_4;
		_channelGroupContainsAppViewModelFactory = P_5;
		_developerModeService = P_6;
		_clipboardService = P_7;
		_developerModeService.PropertyChanged += onDeveloperModePropertyChanged;
	}

	private void onDeveloperModePropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "IsEnabled")
		{
			Dispatcher.UIThread.Post(delegate
			{
				OnPropertyChanged("DeveloperModeEnabled");
			});
		}
	}

	[RelayCommand]
	public void CopyChannelGroupId()
	{
		_clipboardService.CopyTextToClipboard(ChannelGroup.Id.ToString());
	}

	[RelayCommand]
	public void ShowCreateChannelViewModel()
	{
		CreateChannelViewModel createChannelViewModel = _createChannelViewModelFactory.Create(ChannelGroup);
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(createChannelViewModel));
	}

	[RelayCommand]
	public void ShowCreateChannelGroupViewModel()
	{
		CreateChannelGroupViewModel createChannelGroupViewModel = _createChannelGroupViewModelFactory.Create(ChannelGroup.Community);
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(createChannelGroupViewModel));
	}

	[RelayCommand]
	public void ShowEditChannelGroupViewModel()
	{
		EditChannelGroupViewModel editChannelGroupViewModel = _editChannelGroupViewModelFactory.Create(ChannelGroup);
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(editChannelGroupViewModel));
	}

	[RelayCommand]
	public void ShowDeleteChannelGroupViewModel()
	{
		if (ChannelGroup.Community.Channels.FindChannels(string.Empty).Any((Channel c) => c.ChannelGroup.Id == ChannelGroup.Id && c.Type == ChannelType.App))
		{
			ChannelGroupContainsAppViewModel channelGroupContainsAppViewModel = _channelGroupContainsAppViewModelFactory.Create(ChannelGroup);
			WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(channelGroupContainsAppViewModel));
		}
		else
		{
			DeleteChannelGroupViewModel deleteChannelGroupViewModel = _deleteChannelGroupViewModelFactory.Create(ChannelGroup);
			WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(deleteChannelGroupViewModel));
		}
	}

	[RelayCommand]
	public void ToggleCollapse()
	{
		ChannelGroup.SetCollapsed(!ChannelGroup.IsCollapsed);
	}

	public override void Dispose()
	{
		base.Dispose();
		_developerModeService.PropertyChanged -= onDeveloperModePropertyChanged;
	}
}
