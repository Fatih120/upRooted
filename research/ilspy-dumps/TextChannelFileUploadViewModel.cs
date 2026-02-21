using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.CoreDomain.Models.Messages;

namespace RootApp.Client.Avalonia.UI.Community.Content;

public class TextChannelFileUploadViewModel : ViewModelBase<TextChannelFileUploadViewModel>
{
	private readonly Action _closeCallBack;

	private readonly Action<IEnumerable<Uri>> _attachmentsAddedCallback;

	private readonly List<Uri> _fileUris = new List<Uri>();

	[CompilerGenerated]
	private IMessageContainer <MessageContainer>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<IEnumerable<IStorageItem>>? processFilesCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? uploadFilesCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeViewCommand;

	public IMessageContainer MessageContainer
	{
		[CompilerGenerated]
		get
		{
			return <MessageContainer>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<MessageContainer>k__BackingField = messageContainer;
		}
	}

	public string Name => (MessageContainer.CommunityId == null) ? ("@" + MessageContainer.Name) : ("#" + MessageContainer.Name);

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<IEnumerable<IStorageItem>> ProcessFilesCommand => processFilesCommand ?? (processFilesCommand = new RelayCommand<IEnumerable<IStorageItem>>(ProcessFiles));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand UploadFilesCommand => uploadFilesCommand ?? (uploadFilesCommand = new RelayCommand(UploadFiles));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewCommand => closeViewCommand ?? (closeViewCommand = new RelayCommand(CloseView));

	public TextChannelFileUploadViewModel(IMessageContainer P_0, Action P_1, Action<IEnumerable<Uri>> P_2)
		: base((IValidator<TextChannelFileUploadViewModel>?)null)
	{
		MessageContainer = P_0;
		_closeCallBack = P_1;
		_attachmentsAddedCallback = P_2;
	}

	[RelayCommand]
	public void ProcessFiles(IEnumerable<IStorageItem> files)
	{
		IStorageItem[] array = files.ToArray();
		if (array.Length == 0)
		{
			return;
		}
		foreach (IStorageItem item in array.Take(10))
		{
			_fileUris.Add(item.Path);
		}
	}

	[RelayCommand]
	public void UploadFiles()
	{
		_attachmentsAddedCallback(_fileUris);
	}

	[RelayCommand]
	public void CloseView()
	{
		WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this));
		_closeCallBack();
	}
}
