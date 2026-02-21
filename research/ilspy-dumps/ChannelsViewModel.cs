using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI.Avalonia;
using RootApp.Client.Avalonia.Controls.ReorderableList;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Core.Identifiers;

namespace RootApp.Client.Avalonia.UI.Community.Channels;

public class ChannelsViewModel : CachedViewModelBase
{
	private readonly IDisposable? _cacheCleanup;

	private readonly CreateChannelGroupViewModelFactory _createChannelGroupViewModelFactory;

	private readonly ReadOnlyObservableCollection<IViewModelBase>? _channels;

	private List<ChannelGroupViewModel>? _channelGroupCapture;

	[CompilerGenerated]
	private RootApp.Client.CoreDomain.Models.Community.Community <Community>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? showCreateChannelGroupViewModelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<DragInitiatedData>? dragInitiatedCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<DragCancelledData>? dragCancelledCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand<ReorderRequestedData>? reorderRequestedCommand;

	public ReadOnlyObservableCollection<IViewModelBase>? Channels => _channels;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public RootApp.Client.CoreDomain.Models.Community.Community Community
	{
		get
		{
			return <Community>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<RootApp.Client.CoreDomain.Models.Community.Community>.Default.Equals(<Community>k__BackingField, community))
			{
				<Community>k__BackingField = community;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Community);
			}
		}
	}

	public Func<ReorderRequestedData, bool> CanReorderChannelItem => CanReorder;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ShowCreateChannelGroupViewModelCommand => showCreateChannelGroupViewModelCommand ?? (showCreateChannelGroupViewModelCommand = new RelayCommand(ShowCreateChannelGroupViewModel));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<DragInitiatedData> DragInitiatedCommand => dragInitiatedCommand ?? (dragInitiatedCommand = new RelayCommand<DragInitiatedData>(DragInitiated));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<DragCancelledData> DragCancelledCommand => dragCancelledCommand ?? (dragCancelledCommand = new RelayCommand<DragCancelledData>(DragCancelled));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand<ReorderRequestedData> ReorderRequestedCommand => reorderRequestedCommand ?? (reorderRequestedCommand = new AsyncRelayCommand<ReorderRequestedData>(ReorderRequestedAsync));

	public ChannelsViewModel(RootApp.Client.CoreDomain.Models.Community.Community P_0, ChannelGroupViewModelFactory P_1, ChannelViewModelFactory P_2, CreateChannelGroupViewModelFactory P_3)
	{
		Community = P_0;
		_createChannelGroupViewModelFactory = P_3;
		if (Community.Channels == null)
		{
			return;
		}
		_cacheCleanup = Community.Channels.Connect().AutoRefresh((IChannelListItem cli) => cli.IsCollapsed).Filter((IChannelListItem cli) => !(cli is Channel { IsCollapsed: not false }))
			.Transform((Func<IChannelListItem, IViewModelBase>)delegate(IChannelListItem channelItem)
			{
				if (channelItem is ChannelGroup channelGroup)
				{
					return P_1.Create(channelGroup);
				}
				Channel channel = channelItem as Channel;
				return P_2.Create(channel);
			}, (IObservable<Func<IChannelListItem, bool>>?)null)
			.ObserveOn(AvaloniaScheduler.Instance)
			.SortAndBind(out _channels, SortExpressionComparer<IViewModelBase>.Ascending(delegate(IViewModelBase vm)
			{
				if (vm is ChannelGroupViewModel channelGroupViewModel)
				{
					return channelGroupViewModel.ChannelGroup.Position;
				}
				ChannelViewModel channelViewModel = vm as ChannelViewModel;
				return channelViewModel.Channel.Position;
			}))
			.DisposeMany()
			.Subscribe();
	}

	[RelayCommand]
	public void ShowCreateChannelGroupViewModel()
	{
		CreateChannelGroupViewModel createChannelGroupViewModel = _createChannelGroupViewModelFactory.Create(Community);
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(createChannelGroupViewModel));
	}

	[RelayCommand]
	public void DragInitiated(DragInitiatedData data)
	{
		if (Channels == null || !(data.DraggedItem is ChannelGroupViewModel))
		{
			return;
		}
		_channelGroupCapture = new List<ChannelGroupViewModel>();
		IViewModelBase[] array = Channels.ToArray();
		foreach (IViewModelBase viewModelBase in array)
		{
			if (viewModelBase is ChannelGroupViewModel channelGroupViewModel)
			{
				_channelGroupCapture.Add(channelGroupViewModel);
				channelGroupViewModel.ChannelGroup.SetForcedCollapsed(true);
			}
		}
	}

	[RelayCommand]
	public void DragCancelled(DragCancelledData data)
	{
		if (_channelGroupCapture == null)
		{
			return;
		}
		foreach (ChannelGroupViewModel item in _channelGroupCapture)
		{
			item.ChannelGroup.SetForcedCollapsed(false);
		}
		_channelGroupCapture.Clear();
		_channelGroupCapture = null;
	}

	[RelayCommand]
	public async Task ReorderRequestedAsync(ReorderRequestedData data)
	{
		try
		{
			if (_channelGroupCapture != null)
			{
				foreach (ChannelGroupViewModel channelGroupViewModel in _channelGroupCapture)
				{
					channelGroupViewModel.ChannelGroup.SetForcedCollapsed(false);
				}
				_channelGroupCapture.Clear();
				_channelGroupCapture = null;
			}
			object draggedItem = data.DraggedItem;
			if (draggedItem is ChannelGroupViewModel draggedChannelGroupViewModel)
			{
				draggedItem = data.DestinationBeforeItem;
				ChannelGroupGuid beforeChannelGroupId = ((draggedItem is ChannelGroupViewModel destinationChannelGroupViewModel) ? destinationChannelGroupViewModel.ChannelGroup.Id : ChannelGroupGuid.Empty);
				await Community.Channels.MoveChannelGroupAsync(draggedChannelGroupViewModel.ChannelGroup.Id, beforeChannelGroupId);
				return;
			}
			draggedItem = data.DraggedItem;
			if (!(draggedItem is ChannelViewModel draggedChannelViewModel))
			{
				return;
			}
			draggedItem = data.DestinationBeforeItem;
			if (!(draggedItem is ChannelViewModel destinationChannelViewModel))
			{
				draggedItem = data.DestinationBeforeItem;
				if (draggedItem is ChannelGroupViewModel destinationChannelGroupViewModel2)
				{
					await Community.Channels.MoveChannelAsync(draggedChannelViewModel.Channel.Id, ChannelGuid.Empty, draggedChannelViewModel.Channel.ChannelGroup.Id, destinationChannelGroupViewModel2.ChannelGroup.Id);
				}
			}
			else
			{
				await Community.Channels.MoveChannelAsync(draggedChannelViewModel.Channel.Id, destinationChannelViewModel.Channel.Id, draggedChannelViewModel.Channel.ChannelGroup.Id, destinationChannelViewModel.Channel.ChannelGroup.Id);
			}
		}
		catch
		{
		}
	}

	public bool CanReorder(ReorderRequestedData data)
	{
		if (data.DraggedItem is ChannelGroupViewModel channelGroupViewModel)
		{
			return channelGroupViewModel.ChannelGroup.LocalChannelPermission.ChannelFullControl;
		}
		if (data.DraggedItem is ChannelViewModel channelViewModel)
		{
			if (data.DestinationBeforeItem == null)
			{
				return false;
			}
			bool flag = false;
			if (data.DestinationBeforeItem is ChannelGroupViewModel channelGroupViewModel2)
			{
				flag = channelGroupViewModel2.ChannelGroup.LocalChannelPermission.ChannelFullControl;
			}
			else if (data.DestinationBeforeItem is ChannelViewModel channelViewModel2)
			{
				flag = channelViewModel2.Channel.ChannelGroup.LocalChannelPermission.ChannelFullControl;
			}
			return channelViewModel.Channel.LocalChannelPermission.ChannelFullControl && flag;
		}
		return false;
	}

	public override void Dispose()
	{
		base.Dispose();
		_cacheCleanup?.Dispose();
	}
}
