using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using RootApp.Client.Avalonia.Controls.Settings;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Strings;

namespace RootApp.Client.Avalonia.UI.NewTab;

public class DiscoverVerifiedCommunitiesViewModel : ViewModelBase<DiscoverVerifiedCommunitiesViewModel>
{
	private readonly VerifiedCommunitiesViewModelFactory _verifiedCommunitiesViewModelFactory;

	private MenuItemPageContainerViewModel? _verifiedContainer;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeViewModelCommand;

	public ObservableCollection<MenuItemPageContainerViewModel> MenuItemPageContainers { get; } = new ObservableCollection<MenuItemPageContainerViewModel>();

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	public DiscoverVerifiedCommunitiesViewModel(VerifiedCommunitiesViewModelFactory P_0)
		: base((IValidator<DiscoverVerifiedCommunitiesViewModel>?)null)
	{
		_verifiedCommunitiesViewModelFactory = P_0;
		GenerateMenuItems();
	}

	private void GenerateMenuItems()
	{
		MenuItemPageContainers.Add(new MenuItemPageContainerViewModel(RootApp.Client.Avalonia.Resources.Strings.Resources.Categories, true));
		_verifiedContainer = new MenuItemPageContainerViewModel(RootApp.Client.Avalonia.Resources.Strings.Resources.VerifiedCommunities);
		_verifiedContainer.MenuItemSelected += OnVerifiedContainerMenuItemSelected;
		_verifiedContainer.SetForceInitialLoad();
		MenuItemPageContainers.Add(_verifiedContainer);
	}

	private void OnVerifiedContainerMenuItemSelected(MenuItemPageContainerViewModel container)
	{
		_verifiedContainer?.Navigator.Push(_verifiedCommunitiesViewModelFactory.Create(CloseViewModel));
	}

	[RelayCommand]
	public void CloseViewModel()
	{
		WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this));
	}

	public override void Dispose()
	{
		base.Dispose();
		if (_verifiedContainer != null)
		{
			_verifiedContainer.MenuItemSelected -= OnVerifiedContainerMenuItemSelected;
		}
		foreach (MenuItemPageContainerViewModel menuItemPageContainer in MenuItemPageContainers)
		{
			menuItemPageContainer.Dispose();
		}
		MenuItemPageContainers.Clear();
	}
}
