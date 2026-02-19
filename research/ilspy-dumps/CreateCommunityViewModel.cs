// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.CreateCommunityViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls.ImageUpload;
using RootApp.Client.Avalonia.Helpers.DiscordTemplates;
using RootApp.Client.Avalonia.Helpers.DiscordTemplates.Models;
using RootApp.Client.Avalonia.Messages;
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.Resources.Styles.ColorPicker;
using RootApp.Client.Avalonia.UI.Home;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Templates;

public class CreateCommunityViewModel : ViewModelBase<CreateCommunityViewModel>
{
	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly DiscordTemplateService _discordTemplateService;

	[CompilerGenerated]
	private string _003CCommunityName_003Ek__BackingField;

	[CompilerGenerated]
	private Color _003CCommunityColor_003Ek__BackingField;

	[CompilerGenerated]
	private WebApiStatus _003CWebApiStatus_003Ek__BackingField;

	[CompilerGenerated]
	private string _003CDiscordTemplateUrl_003Ek__BackingField;

	[CompilerGenerated]
	private DiscordTemplateResponse? _003CImportedDiscordTemplate_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsImportingTemplate_003Ek__BackingField;

	[CompilerGenerated]
	private string? _003CDiscordTemplateError_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsImportSectionVisible_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? importDiscordTemplateCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? clearDiscordTemplateCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? toggleImportSectionCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? createCommunityCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeViewModelCommand;

	private List<string> _randomHexColors
	{
		get
		{
			int num = 30;
			List<string> list = new List<string>(num);
			CollectionsMarshal.SetCount(list, num);
			Span<string> span = CollectionsMarshal.AsSpan(list);
			int num2 = 0;
			span[num2] = "#D34649";
			num2++;
			span[num2] = "#E6623F";
			num2++;
			span[num2] = "#E88F3D";
			num2++;
			span[num2] = "#D6D949";
			num2++;
			span[num2] = "#DD59D0";
			num2++;
			span[num2] = "#ED6980";
			num2++;
			span[num2] = "#F88567";
			num2++;
			span[num2] = "#F2C642";
			num2++;
			span[num2] = "#F1F376";
			num2++;
			span[num2] = "#FF94F4";
			num2++;
			span[num2] = "#FC97C1";
			num2++;
			span[num2] = "#FDAB96";
			num2++;
			span[num2] = "#F8E48D";
			num2++;
			span[num2] = "#FAFCA2";
			num2++;
			span[num2] = "#F5BAFF";
			num2++;
			span[num2] = "#6F6FFB";
			num2++;
			span[num2] = "#4C6FFF";
			num2++;
			span[num2] = "#279DC8";
			num2++;
			span[num2] = "#49D6AC";
			num2++;
			span[num2] = "#58AC30";
			num2++;
			span[num2] = "#A98BFF";
			num2++;
			span[num2] = "#6993FF";
			num2++;
			span[num2] = "#3FB9E5";
			num2++;
			span[num2] = "#6CF3AB";
			num2++;
			span[num2] = "#70C038";
			num2++;
			span[num2] = "#B89AF6";
			num2++;
			span[num2] = "#87A9FF";
			num2++;
			span[num2] = "#7BD4F4";
			num2++;
			span[num2] = "#9EF8AC";
			num2++;
			span[num2] = "#99E645";
			return list;
		}
	}

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("CreateCommunityCommand")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string CommunityName
	{
		get
		{
			return _003CCommunityName_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CCommunityName_003Ek__BackingField, text))
			{
				_003CCommunityName_003Ek__BackingField = text;
				OnCommunityNameChanged(text);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.CommunityName);
				CreateCommunityCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Color CommunityColor
	{
		get
		{
			return _003CCommunityColor_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<Color>.Default.Equals(_003CCommunityColor_003Ek__BackingField, color))
			{
				_003CCommunityColor_003Ek__BackingField = color;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.CommunityColor);
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

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("ImportDiscordTemplateCommand")]
	[NotifyCanExecuteChangedFor("CreateCommunityCommand")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string DiscordTemplateUrl
	{
		get
		{
			return _003CDiscordTemplateUrl_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CDiscordTemplateUrl_003Ek__BackingField, text))
			{
				_003CDiscordTemplateUrl_003Ek__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.DiscordTemplateUrl);
				ImportDiscordTemplateCommand.NotifyCanExecuteChanged();
				CreateCommunityCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[ObservableProperty]
	[NotifyPropertyChangedFor("HasImportedTemplate")]
	[NotifyPropertyChangedFor("ImportedTemplateName")]
	[NotifyCanExecuteChangedFor("CreateCommunityCommand")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public DiscordTemplateResponse? ImportedDiscordTemplate
	{
		get
		{
			return _003CImportedDiscordTemplate_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<DiscordTemplateResponse>.Default.Equals(_003CImportedDiscordTemplate_003Ek__BackingField, discordTemplateResponse))
			{
				_003CImportedDiscordTemplate_003Ek__BackingField = discordTemplateResponse;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ImportedDiscordTemplate);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.HasImportedTemplate);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ImportedTemplateName);
				CreateCommunityCommand.NotifyCanExecuteChanged();
			}
		}
	}

	public bool HasImportedTemplate => ImportedDiscordTemplate != null;

	public string ImportedTemplateName => ImportedDiscordTemplate?.Name ?? string.Empty;

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("ImportDiscordTemplateCommand")]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsImportingTemplate
	{
		get
		{
			return _003CIsImportingTemplate_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsImportingTemplate_003Ek__BackingField, flag))
			{
				_003CIsImportingTemplate_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsImportingTemplate);
				ImportDiscordTemplateCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string? DiscordTemplateError
	{
		get
		{
			return _003CDiscordTemplateError_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CDiscordTemplateError_003Ek__BackingField, text))
			{
				_003CDiscordTemplateError_003Ek__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.DiscordTemplateError);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsImportSectionVisible
	{
		get
		{
			return _003CIsImportSectionVisible_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsImportSectionVisible_003Ek__BackingField, flag))
			{
				_003CIsImportSectionVisible_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsImportSectionVisible);
			}
		}
	}

	public ImageUploaderViewModel ImageUploaderViewModel { get; }

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand ImportDiscordTemplateCommand => importDiscordTemplateCommand ?? (importDiscordTemplateCommand = new AsyncRelayCommand(ImportDiscordTemplateAsync, canImportDiscordTemplate));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ClearDiscordTemplateCommand => clearDiscordTemplateCommand ?? (clearDiscordTemplateCommand = new RelayCommand(ClearDiscordTemplate));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ToggleImportSectionCommand => toggleImportSectionCommand ?? (toggleImportSectionCommand = new RelayCommand(ToggleImportSection));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand CreateCommunityCommand => createCommunityCommand ?? (createCommunityCommand = new AsyncRelayCommand(CreateCommunityAsync, canCreateCommunity));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseViewModelCommand => closeViewModelCommand ?? (closeViewModelCommand = new RelayCommand(CloseViewModel));

	public CreateCommunityViewModel(IRootSessionAccessor P_0, ImageUploaderViewModelFactory P_1, DiscordTemplateService P_2)
		: base((IValidator<CreateCommunityViewModel>?)new CreateCommunityViewModelValidator())
	{
		_rootSessionAccessor = P_0;
		_discordTemplateService = P_2;
		CommunityName = string.Empty;
		DiscordTemplateUrl = string.Empty;
		pickRandomColor();
		ImageUploaderViewModel = P_1.Create(ImageUploadType.CommunityImage);
		ImageUploaderViewModel.UploadStarted += imageUploaderViewModelUploadStarted;
		ImageUploaderViewModel.UploadCompleted += imageUploaderViewModelUploadCompleted;
	}

	private void imageUploaderViewModelUploadCompleted()
	{
		CreateCommunityCommand.NotifyCanExecuteChanged();
	}

	private void imageUploaderViewModelUploadStarted()
	{
		CreateCommunityCommand.NotifyCanExecuteChanged();
	}

	[RelayCommand(CanExecute = "canImportDiscordTemplate")]
	public async Task ImportDiscordTemplateAsync()
	{
		try
		{
			IsImportingTemplate = true;
			DiscordTemplateError = null;
			DiscordTemplateResponse template = await _discordTemplateService.GetTemplateFromUrlAsync(DiscordTemplateUrl);
			if (template == null)
			{
				DiscordTemplateError = Resources.FailedToImportDiscordTemplate;
				ImportedDiscordTemplate = null;
				return;
			}
			ImportedDiscordTemplate = template;
			if (string.IsNullOrWhiteSpace(CommunityName) && template.SerializedSourceGuild != null)
			{
				CommunityName = template.SerializedSourceGuild.Name;
			}
		}
		catch (Exception)
		{
			DiscordTemplateError = Resources.FailedToImportDiscordTemplate;
			ImportedDiscordTemplate = null;
		}
		finally
		{
			IsImportingTemplate = false;
		}
	}

	private bool canImportDiscordTemplate()
	{
		return !IsImportingTemplate && DiscordTemplateService.IsValidDiscordTemplateUrl(DiscordTemplateUrl);
	}

	[RelayCommand]
	public void ClearDiscordTemplate()
	{
		ImportedDiscordTemplate = null;
		DiscordTemplateUrl = string.Empty;
		DiscordTemplateError = null;
	}

	[RelayCommand]
	public void ToggleImportSection()
	{
		IsImportSectionVisible = !IsImportSectionVisible;
	}

	[RelayCommand(CanExecute = "canCreateCommunity")]
	public async Task CreateCommunityAsync()
	{
		try
		{
			WebApiStatus = WebApiStatus.Sending;
			CommunityTemplate template = null;
			if (ImportedDiscordTemplate != null)
			{
				template = DiscordTemplateConverter.ConvertToRootTemplate(ImportedDiscordTemplate);
			}
			await _rootSessionAccessor.Session.CommunityService.CreateCommunityAsync(CommunityName, CommunityColor.ToHexWithoutAlpha(), ImageUploaderViewModel.UploadToken?.ToString(), template);
			WebApiStatus = WebApiStatus.Success;
		}
		catch
		{
			WebApiStatus = WebApiStatus.Failed;
		}
	}

	private bool canCreateCommunity()
	{
		bool flag = !string.IsNullOrWhiteSpace(DiscordTemplateUrl) && !HasImportedTemplate;
		return !ImageUploaderViewModel.IsUploading && base.HasNoErrors && !flag;
	}

	[RelayCommand]
	public void CloseViewModel()
	{
		WeakReferenceMessenger.Default.Send(new PopViewModelFromStackMessage(this));
	}

	private void pickRandomColor()
	{
		Random random = new Random();
		int num = random.Next(_randomHexColors.Count);
		string text = _randomHexColors[num];
		CommunityColor = Color.Parse(text);
	}

	public override void Dispose()
	{
		base.Dispose();
		ImageUploaderViewModel.UploadStarted -= imageUploaderViewModelUploadStarted;
		ImageUploaderViewModel.UploadCompleted -= imageUploaderViewModelUploadCompleted;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnCommunityNameChanged(string P_0)
	{
		ValidateProperty("CommunityName");
	}
}

