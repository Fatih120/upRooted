// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.NewTabViewModel
using System;
using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.Avalonia.UI.NewTab;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Tabs;

public class NewTabViewModel : ViewModelBase<NewTabViewModel>, ITabViewModel, IDisposable
{
	private readonly IRootSessionAccessor _rootSessionAccessor;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeTabCommand;

	public Tab Tab { get; }

	public IViewModelBase ContentViewModel { get; }

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseTabCommand => closeTabCommand ?? (closeTabCommand = new RelayCommand(CloseTab));

	public NewTabViewModel(Tab P_0, NewTabContentViewModelFactory P_1, IRootSessionAccessor P_2)
		: base((IValidator<NewTabViewModel>?)null)
	{
		_rootSessionAccessor = P_2;
		Tab = P_0;
		ContentViewModel = P_1.Create();
	}

	[RelayCommand]
	public void CloseTab()
	{
		_rootSessionAccessor.Session.TabService.RemoveTab(null);
	}

	public override void Dispose()
	{
		base.Dispose();
		ContentViewModel.Dispose();
	}
}

