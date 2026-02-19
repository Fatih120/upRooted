// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.BanMemberViewModel
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
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.Avalonia.UI.Community.Members;
using RootApp.Client.CoreDomain.Models.Community;

public class BanMemberViewModel : ViewModelBase<BanMemberViewModel>
{
	[CompilerGenerated]
	private string? _003CBanReason_003Ek__BackingField;

	[CompilerGenerated]
	private WebApiStatus _003CWebApiStatus_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? banMemberCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeViewModelCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string? BanReason
	{
		get
		{
			return _003CBanReason_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CBanReason_003Ek__BackingField, text))
			{
				_003CBanReason_003Ek__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.BanReason);
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
			return _003CWebApiStatus_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<WebApiStatus>.Default.Equals(_003CWebApiStatus_003Ek__BackingField, webApiStatus))
			{
				_003CWebApiStatus_003Ek__BackingField = webApiStatus;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.WebApiStatus);
			}
		}
	}

	public Member Member { get; }

	public Community Community { get; }

	public IMarkdownEngine MarkdownEngine { get; }

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand BanMemberCommand => banMemberCommand ?? (banMemberCommand = new AsyncRelayCommand(BanMemberAsync));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	public BanMemberViewModel(Member P_0, Community P_1, RootMarkdownEngineManager P_2)
		: base((IValidator<BanMemberViewModel>?)null)
	{
		Member = P_0;
		Community = P_1;
		MarkdownEngine = P_2.SimpleEngine;
	}

	[RelayCommand]
	public async Task BanMemberAsync()
	{
		try
		{
			WebApiStatus = WebApiStatus.Sending;
			await Community.MemberManagement.BanMemberAsync(Member.UserId, BanReason);
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

