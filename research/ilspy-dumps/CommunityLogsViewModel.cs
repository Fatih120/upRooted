using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using RootApp.Client.Avalonia.Controls.Settings;
using RootApp.Client.Avalonia.Helpers.CommunityLogs;
using RootApp.Client.Avalonia.Helpers.Navigation;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.CoreDomain.Models.Community;
using RootApp.Client.CoreDomain.Models.Community.CommunityLogs;
using RootApp.Client.CoreDomain.Utils.Time;
using RootApp.Core.Identifiers;

namespace RootApp.Client.Avalonia.UI.Community.Settings;

public class CommunityLogsViewModel : ViewModelBase<CommunityLogsViewModel>, IPage
{
	private readonly Navigator _navigator;

	private readonly RootApp.Client.CoreDomain.Models.Community.Community _community;

	private readonly CommunityLogViewModelFactory _communityLogViewModelFactory;

	private readonly CommunityLogDateBreakViewModelFactory _communityLogDateBreakViewModelFactory;

	private readonly HashSet<string> _dateGroupings = new HashSet<string>();

	private CommunityLogGuid? _lastCommunityLogGuid;

	private bool _hasMoreLogsToDownload = true;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? resetStateCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? fetchCommunityLogsCommand;

	private bool HasMoreLogsToDownload
	{
		get
		{
			return _hasMoreLogsToDownload;
		}
		set
		{
			if (_hasMoreLogsToDownload != flag)
			{
				_hasMoreLogsToDownload = flag;
				FetchCommunityLogsCommand.NotifyCanExecuteChanged();
			}
		}
	}

	public ObservableCollection<IViewModelBase> CommunityLogs { get; } = new ObservableCollection<IViewModelBase>();

	public string PageTitle => RootApp.Client.Avalonia.Resources.Strings.Resources.ActionLogs;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ResetStateCommand => resetStateCommand ?? (resetStateCommand = new RelayCommand(ResetState));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand FetchCommunityLogsCommand => fetchCommunityLogsCommand ?? (fetchCommunityLogsCommand = new AsyncRelayCommand(FetchCommunityLogsAsync, () => HasMoreLogsToDownload));

	public CommunityLogsViewModel(Navigator P_0, RootApp.Client.CoreDomain.Models.Community.Community P_1, CommunityLogViewModelFactory P_2, CommunityLogDateBreakViewModelFactory P_3)
		: base((IValidator<CommunityLogsViewModel>?)null)
	{
		_navigator = P_0;
		_community = P_1;
		_communityLogViewModelFactory = P_2;
		_communityLogDateBreakViewModelFactory = P_3;
	}

	[RelayCommand]
	public void ResetState()
	{
		CommunityLogs.Clear();
		_dateGroupings.Clear();
		_lastCommunityLogGuid = null;
		HasMoreLogsToDownload = true;
	}

	[RelayCommand(CanExecute = "HasMoreLogsToDownload")]
	public async Task FetchCommunityLogsAsync()
	{
		try
		{
			List<CommunityLog> logs = (await _community.CommunityLogs.FetchCommunityLogsAsync(_lastCommunityLogGuid)).ToList();
			if (logs.Count == 0)
			{
				HasMoreLogsToDownload = false;
			}
			else
			{
				processCommunityLogs(logs);
			}
		}
		catch
		{
		}
	}

	private void processCommunityLogs(List<CommunityLog> P_0)
	{
		_lastCommunityLogGuid = P_0.Last().Id;
		foreach (CommunityLog item in P_0)
		{
			addDateGroupingIfNecessary(item.Id);
			addCommunityLog(item);
		}
	}

	private void addDateGroupingIfNecessary(CommunityLogGuid P_0)
	{
		string text = P_0.ToDateTime().ToLocalTime().ToTimeContainer();
		if (_dateGroupings.Add(text))
		{
			CommunityLogs.Add(_communityLogDateBreakViewModelFactory.Create(text));
		}
	}

	private void addCommunityLog(CommunityLog P_0)
	{
		BaseCommunityLogItem baseCommunityLogItem = P_0.LogItems.First();
		string logIconName = IconLookup.GetLogIconName(baseCommunityLogItem, baseCommunityLogItem.Action);
		CommunityLogViewModel communityLogViewModel = _communityLogViewModelFactory.Create(CommunityLogFormatter.CreateFormattedCommunityLog(P_0), P_0.Id.ToDateTime().ToLocalTime().ToMessageTimeString(), logIconName, P_0.ActorMember);
		CommunityLogs.Add(communityLogViewModel);
	}
}
