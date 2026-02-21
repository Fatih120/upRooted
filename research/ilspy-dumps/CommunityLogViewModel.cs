using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.CommunityLogs;
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.CoreDomain.Models.Community;

namespace RootApp.Client.Avalonia.UI.Community.Settings;

public class CommunityLogViewModel : ViewModelBase<CommunityLogViewModel>
{
	private readonly BitmapCache _bitmapCache;

	private readonly Member _member;

	private readonly CommunityLogDetailsViewModelFactory _communityLogDetailsViewModelFactory;

	public FormattedCommunityLog FormattedCommunityLog { get; }

	public bool HasDetails { get; }

	public IMarkdownEngine MarkdownEngine { get; }

	public string ActionDateString { get; }

	public string ActionLogIconPath { get; }

	public Task<BitmapWrapper?> ProfilePictureAsyncBitmapWrapper => _bitmapCache.GetBitmapAsync(_member.GlobalUser.ProfilePictureUri, null, 120);

	public CommunityLogViewModel(FormattedCommunityLog P_0, string P_1, string P_2, Member P_3, CommunityLogDetailsViewModelFactory P_4, RootMarkdownEngineManager P_5, BitmapCache P_6)
		: base((IValidator<CommunityLogViewModel>?)null)
	{
		FormattedCommunityLog = P_0;
		HasDetails = P_0.LogItems.Any();
		ActionDateString = P_1;
		ActionLogIconPath = P_2;
		MarkdownEngine = P_5.SimpleEngine;
		_bitmapCache = P_6;
		_member = P_3;
		_communityLogDetailsViewModelFactory = P_4;
	}

	[RelayCommand]
	public void ShowActionLogDetailsCommand()
	{
		if (HasDetails)
		{
			CommunityLogDetailsViewModel communityLogDetailsViewModel = _communityLogDetailsViewModelFactory.Create(FormattedCommunityLog, ActionDateString, ActionLogIconPath, _member);
			WeakReferenceMessenger.Default.Send(new PushViewModelToStackMessage(communityLogDetailsViewModel));
		}
	}
}
