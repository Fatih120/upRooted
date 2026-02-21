using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Navigation;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Client.CoreDomain.Models.Notifications;

namespace RootApp.Client.Avalonia.UI.NewTab;

public class NewTabFavoriteCommunityViewModel : ViewModelBase<NewTabFavoriteCommunityViewModel>
{
	private readonly BitmapCache _bitmapCache;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly CommunityOpenerService _communityOpenerService;

	private readonly LeaveCommunityViewModelFactory _leaveCommunityViewModelFactory;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? communitySelectedCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? favoriteSelectedCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? leaveCommunityCommand;

	public RootApp.Client.CoreDomain.Models.Community.Community Community { get; }

	public NotificationContainer? Notifications { get; }

	public Task<BitmapWrapper?> CommunityPictureAsyncBitmapWrapper => _bitmapCache.GetBitmapAsync(Community.PictureUrl, null, 560);

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CommunitySelectedCommand => communitySelectedCommand ?? (communitySelectedCommand = new RelayCommand(CommunitySelected));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand FavoriteSelectedCommand => favoriteSelectedCommand ?? (favoriteSelectedCommand = new AsyncRelayCommand(FavoriteSelectedAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand LeaveCommunityCommand => leaveCommunityCommand ?? (leaveCommunityCommand = new RelayCommand(LeaveCommunity));

	public NewTabFavoriteCommunityViewModel(RootApp.Client.CoreDomain.Models.Community.Community P_0, BitmapCache P_1, IRootSessionAccessor P_2, CommunityOpenerService P_3, LeaveCommunityViewModelFactory P_4)
		: base((IValidator<NewTabFavoriteCommunityViewModel>?)null)
	{
		Community = P_0;
		_bitmapCache = P_1;
		_rootSessionAccessor = P_2;
		_communityOpenerService = P_3;
		_leaveCommunityViewModelFactory = P_4;
		Notifications = _rootSessionAccessor.Session.NotificationService.GetNotificationContainer(Community.Id);
	}

	[RelayCommand]
	public void CommunitySelected()
	{
		_communityOpenerService.OpenCommunity(Community.Id);
	}

	[RelayCommand]
	public async Task FavoriteSelectedAsync()
	{
		try
		{
			await Community.SetFavoriteAsync(false);
		}
		catch
		{
		}
	}

	[RelayCommand]
	public void LeaveCommunity()
	{
		LeaveCommunityViewModel leaveCommunityViewModel = _leaveCommunityViewModelFactory.Create(Community);
		WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(leaveCommunityViewModel));
	}
}
