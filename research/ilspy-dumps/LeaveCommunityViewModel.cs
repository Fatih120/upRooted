// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.LeaveCommunityViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Community;

public class LeaveCommunityViewModel : ViewModelBase<LeaveCommunityViewModel>
{
	private readonly Community _community;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly Action? _onCloseCallBack;

	[ObservableProperty]
	private WebApiStatus _webApiStatus;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? leaveCommunityCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeViewModelCommand;

	public string CommunityName { get; }

	public IMarkdownEngine MarkdownEngine { get; }

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public WebApiStatus WebApiStatus
	{
		get
		{
			return _webApiStatus;
		}
		set
		{
			if (!EqualityComparer<WebApiStatus>.Default.Equals(_webApiStatus, webApiStatus))
			{
				_webApiStatus = webApiStatus;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.WebApiStatus);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand LeaveCommunityCommand => leaveCommunityCommand ?? (leaveCommunityCommand = new AsyncRelayCommand(LeaveCommunityAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	public LeaveCommunityViewModel(Community P_0, RootMarkdownEngineManager P_1, IRootSessionAccessor P_2, Action? P_3 = null)
		: base((IValidator<LeaveCommunityViewModel>?)null)
	{
		_community = P_0;
		_rootSessionAccessor = P_2;
		_onCloseCallBack = P_3;
		CommunityName = P_0.Name;
		MarkdownEngine = P_1.SimpleEngine;
	}

	[RelayCommand]
	public async Task LeaveCommunityAsync()
	{
		try
		{
			WebApiStatus = WebApiStatus.Sending;
			await _rootSessionAccessor.Session.CommunityService.LeaveCommunityAsync(_community.Id);
			_onCloseCallBack?.Invoke();
			WebApiStatus = WebApiStatus.Success;
		}
		catch
		{
			WebApiStatus = WebApiStatus.Failed;
		}
	}

	[RelayCommand]
	public void CloseViewModel()
	{
		WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this));
	}
}

