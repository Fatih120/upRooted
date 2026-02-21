using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using DynamicData.Alias;
using DynamicData.Binding;
using FluentValidation;
using Microsoft.VisualStudio.Threading;
using ReactiveUI.Avalonia;
using RootApp.Client.Avalonia.Helpers.Panes;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Core.Identifiers;

namespace RootApp.Client.Avalonia.UI.NewTab;

public class NewTabContentViewModel : ViewModelBase<NewTabContentViewModel>
{
	private readonly CreateCommunityViewModelFactory _createCommunityViewModelFactory;

	private readonly DiscoverVerifiedCommunitiesViewModelFactory _discoverVerifiedCommunitiesViewModelFactory;

	private readonly PaneDisplayService _paneDisplayService;

	private readonly IDisposable? _cacheCleanup;

	private readonly IDisposable? _favoriteCacheCleanup;

	private readonly ReadOnlyObservableCollection<NewTabFavoriteCommunityViewModel> _favoriteCommunities;

	private readonly ReadOnlyObservableCollection<NewTabCommunityViewModel> _communities;

	[CompilerGenerated]
	private string? _003CSearchTerm_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? createCommunityCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<SplitViewDisplayMode>? setPaneDisplayModeCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? discoverVerifiedCommunitiesCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string? SearchTerm
	{
		get
		{
			return _003CSearchTerm_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CSearchTerm_003Ek__BackingField, text))
			{
				_003CSearchTerm_003Ek__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SearchTerm);
			}
		}
	}

	public ReadOnlyObservableCollection<NewTabFavoriteCommunityViewModel> FavoriteCommunities => _favoriteCommunities;

	public ReadOnlyObservableCollection<NewTabCommunityViewModel> Communities => _communities;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CreateCommunityCommand => createCommunityCommand ?? (createCommunityCommand = new RelayCommand(CreateCommunity));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<SplitViewDisplayMode> SetPaneDisplayModeCommand => setPaneDisplayModeCommand ?? (setPaneDisplayModeCommand = new RelayCommand<SplitViewDisplayMode>(SetPaneDisplayMode));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand DiscoverVerifiedCommunitiesCommand => discoverVerifiedCommunitiesCommand ?? (discoverVerifiedCommunitiesCommand = new RelayCommand(DiscoverVerifiedCommunities));

	public NewTabContentViewModel(IRootSessionAccessor P_0, NewTabCommunityViewModelFactory P_1, CreateCommunityViewModelFactory P_2, NewTabFavoriteCommunityViewModelFactory P_3, DiscoverVerifiedCommunitiesViewModelFactory P_4, PaneDisplayService P_5)
		: base((IValidator<NewTabContentViewModel>?)null)
	{
		_createCommunityViewModelFactory = P_2;
		_discoverVerifiedCommunitiesViewModelFactory = P_4;
		_paneDisplayService = P_5;
		IObservable<Func<RootApp.Client.CoreDomain.Models.Community.Community, bool>> predicate = (from _ in Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(delegate(PropertyChangedEventHandler h)
			{
				base.PropertyChanged += h;
			}, delegate(PropertyChangedEventHandler h)
			{
				base.PropertyChanged -= h;
			})
			where _.EventArgs.PropertyName == "SearchTerm"
			select buildFilterPredicate(SearchTerm)).StartWith(buildFilterPredicate(SearchTerm));
		_cacheCleanup = (from c in P_0.Session.CommunityService.ConnectCommunities().AutoRefresh((RootApp.Client.CoreDomain.Models.Community.Community c) => c.IsFavorite)
			where !c.IsFavorite
			select c).Filter(predicate).Sort(SortExpressionComparer<RootApp.Client.CoreDomain.Models.Community.Community>.Descending((RootApp.Client.CoreDomain.Models.Community.Community n) => n.Id.ToEpoch())).Transform(P_1.Create)
			.ObserveOn(AvaloniaScheduler.Instance)
			.Bind(out _communities)
			.DisposeMany()
			.Subscribe();
		_favoriteCacheCleanup = (from c in P_0.Session.CommunityService.ConnectCommunities().AutoRefresh((RootApp.Client.CoreDomain.Models.Community.Community c) => c.IsFavorite)
			where c.IsFavorite
			select c).Filter(predicate).Transform(P_3.Create).ObserveOn(AvaloniaScheduler.Instance)
			.Bind(out _favoriteCommunities)
			.DisposeMany()
			.Subscribe();
		try
		{
			P_0.Session.CommunityService.InitializeLightCommunitiesAsync().Forget();
		}
		catch
		{
		}
	}

	private Func<RootApp.Client.CoreDomain.Models.Community.Community, bool> buildFilterPredicate(string? P_0)
	{
		return (RootApp.Client.CoreDomain.Models.Community.Community community) => string.IsNullOrEmpty(P_0) || community.Name.Contains(P_0, StringComparison.OrdinalIgnoreCase);
	}

	[RelayCommand]
	public void CreateCommunity()
	{
		CreateCommunityViewModel createCommunityViewModel = _createCommunityViewModelFactory.Create();
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(createCommunityViewModel));
	}

	[RelayCommand]
	public void SetPaneDisplayMode(SplitViewDisplayMode displayMode)
	{
		_paneDisplayService.SetGlobalPaneDisplayMode(displayMode);
	}

	[RelayCommand]
	public void DiscoverVerifiedCommunities()
	{
		DiscoverVerifiedCommunitiesViewModel discoverVerifiedCommunitiesViewModel = _discoverVerifiedCommunitiesViewModelFactory.Create();
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(discoverVerifiedCommunitiesViewModel));
	}

	public override void Dispose()
	{
		base.Dispose();
		_cacheCleanup?.Dispose();
		_favoriteCacheCleanup?.Dispose();
	}
}
