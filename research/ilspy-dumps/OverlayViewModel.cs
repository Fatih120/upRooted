using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using RootApp.Browser;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.StreamerMode;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Media;
using RootApp.Client.CoreDomain.Models.User;
using RootApp.Client.CoreDomain.Services;
using RootApp.Core;
using RootApp.Core.Identifiers;

namespace RootApp.Client.Avalonia.UI.Overlay;

public class OverlayViewModel : ObservableObject, IDisposable
{
	private static readonly TimeSpan MessageDisplayDuration = TimeSpan.FromSeconds(5L);

	private static readonly TimeSpan FadeOutDuration = TimeSpan.FromMilliseconds(500L);

	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly IGlobalMessageStreamService _globalMessageStreamService;

	private readonly IGlobalUserCacheService _globalUserCacheService;

	private readonly OverlayMessageViewModelFactory _overlayMessageViewModelFactory;

	private readonly BitmapCache _bitmapCache;

	private readonly IStreamerModeService _streamerModeService;

	private readonly BrowserService _browserService;

	private IDisposable? _memberSubscription;

	private IDisposable? _messageStreamSubscription;

	private MediaRoom? _currentMediaRoom;

	private bool _disposed;

	private OverlayActionBarViewModel? _actionBarViewModel;

	private readonly Dictionary<RootGuid, (MediaMember Member, PropertyChangedEventHandler Handler)> _memberSubscriptions = new Dictionary<RootGuid, (MediaMember, PropertyChangedEventHandler)>();

	private DispatcherTimer? _messageLifecycleTimer;

	[CompilerGenerated]
	private bool <IsVoiceOverlayEnabled>k__BackingField = true;

	[CompilerGenerated]
	private Action<bool>? m_VoiceOverlayEnabledChanged;

	[CompilerGenerated]
	private Action<bool>? m_ChatOverlayEnabledChanged;

	[CompilerGenerated]
	private Action<double>? m_OpacityChanged;

	[CompilerGenerated]
	private Action<double>? m_ScaleChanged;

	[CompilerGenerated]
	private Action<bool>? m_ShowAvatarsChanged;

	[CompilerGenerated]
	private Action<bool>? m_ShowNamesChanged;

	[CompilerGenerated]
	private Action<bool>? m_OnlyShowSpeakingChanged;

	[CompilerGenerated]
	private double <Opacity>k__BackingField = 1.0;

	[CompilerGenerated]
	private double <Scale>k__BackingField = 1.0;

	[CompilerGenerated]
	private bool <ShowAvatars>k__BackingField = true;

	[CompilerGenerated]
	private bool <ShowNames>k__BackingField = true;

	[CompilerGenerated]
	private Action<bool, double, double>? m_VoicePanelPositionChanged;

	[CompilerGenerated]
	private Action<bool, double, double>? m_ChatPanelPositionChanged;

	[CompilerGenerated]
	private Action? m_ResetPositionsRequested;

	private DispatcherTimer? _welcomeNotificationTimer;

	[CompilerGenerated]
	private bool <IsInVoiceChannel>k__BackingField;

	[CompilerGenerated]
	private string? <ChannelName>k__BackingField;

	[CompilerGenerated]
	private bool <IsChatOverlayEnabled>k__BackingField;

	[CompilerGenerated]
	private bool <ShowWelcomeNotification>k__BackingField;

	[CompilerGenerated]
	private string? <WelcomeKeybindingText>k__BackingField;

	[CompilerGenerated]
	private bool <IsInteractiveMode>k__BackingField;

	[CompilerGenerated]
	private bool <OnlyShowSpeaking>k__BackingField;

	[CompilerGenerated]
	private bool <IsVoicePanelDragging>k__BackingField;

	[CompilerGenerated]
	private double <VoicePanelX>k__BackingField;

	[CompilerGenerated]
	private double <VoicePanelY>k__BackingField;

	[CompilerGenerated]
	private bool <UseCustomVoicePosition>k__BackingField;

	[CompilerGenerated]
	private bool <IsChatPanelDragging>k__BackingField;

	[CompilerGenerated]
	private double <ChatPanelX>k__BackingField;

	[CompilerGenerated]
	private double <ChatPanelY>k__BackingField;

	[CompilerGenerated]
	private bool <UseCustomChatPosition>k__BackingField;

	[CompilerGenerated]
	private bool <ShowSnapIndicator>k__BackingField;

	[CompilerGenerated]
	private double <SnapIndicatorX>k__BackingField;

	[CompilerGenerated]
	private double <SnapIndicatorY>k__BackingField;

	[CompilerGenerated]
	private double <SnapIndicatorWidth>k__BackingField;

	[CompilerGenerated]
	private double <SnapIndicatorHeight>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? dismissWelcomeNotificationCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? toggleVoiceOverlayEnabledCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? toggleChatOverlayEnabledCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsInVoiceChannel
	{
		get
		{
			return <IsInVoiceChannel>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsInVoiceChannel>k__BackingField, flag))
			{
				<IsInVoiceChannel>k__BackingField = flag;
				OnIsInVoiceChannelChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsInVoiceChannel);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string? ChannelName
	{
		get
		{
			return <ChannelName>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<ChannelName>k__BackingField, text))
			{
				<ChannelName>k__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ChannelName);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsChatOverlayEnabled
	{
		get
		{
			return <IsChatOverlayEnabled>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsChatOverlayEnabled>k__BackingField, flag))
			{
				<IsChatOverlayEnabled>k__BackingField = flag;
				OnIsChatOverlayEnabledChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsChatOverlayEnabled);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsVoiceOverlayEnabled
	{
		get
		{
			return <IsVoiceOverlayEnabled>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsVoiceOverlayEnabled>k__BackingField, flag))
			{
				<IsVoiceOverlayEnabled>k__BackingField = flag;
				OnIsVoiceOverlayEnabledChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsVoiceOverlayEnabled);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ShowWelcomeNotification
	{
		get
		{
			return <ShowWelcomeNotification>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<ShowWelcomeNotification>k__BackingField, flag))
			{
				<ShowWelcomeNotification>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShowWelcomeNotification);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string? WelcomeKeybindingText
	{
		get
		{
			return <WelcomeKeybindingText>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(<WelcomeKeybindingText>k__BackingField, text))
			{
				<WelcomeKeybindingText>k__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.WelcomeKeybindingText);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsInteractiveMode
	{
		get
		{
			return <IsInteractiveMode>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsInteractiveMode>k__BackingField, flag))
			{
				<IsInteractiveMode>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsInteractiveMode);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double Opacity
	{
		get
		{
			return <Opacity>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(<Opacity>k__BackingField, num))
			{
				<Opacity>k__BackingField = num;
				OnOpacityChanged(num);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Opacity);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double Scale
	{
		get
		{
			return <Scale>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(<Scale>k__BackingField, num))
			{
				<Scale>k__BackingField = num;
				OnScaleChanged(num);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Scale);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ShowAvatars
	{
		get
		{
			return <ShowAvatars>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<ShowAvatars>k__BackingField, flag))
			{
				<ShowAvatars>k__BackingField = flag;
				OnShowAvatarsChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShowAvatars);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ShowNames
	{
		get
		{
			return <ShowNames>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<ShowNames>k__BackingField, flag))
			{
				<ShowNames>k__BackingField = flag;
				OnShowNamesChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShowNames);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool OnlyShowSpeaking
	{
		get
		{
			return <OnlyShowSpeaking>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<OnlyShowSpeaking>k__BackingField, flag))
			{
				<OnlyShowSpeaking>k__BackingField = flag;
				OnOnlyShowSpeakingChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.OnlyShowSpeaking);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsVoicePanelDragging
	{
		get
		{
			return <IsVoicePanelDragging>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsVoicePanelDragging>k__BackingField, flag))
			{
				<IsVoicePanelDragging>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsVoicePanelDragging);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double VoicePanelX
	{
		get
		{
			return <VoicePanelX>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(<VoicePanelX>k__BackingField, num))
			{
				<VoicePanelX>k__BackingField = num;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.VoicePanelX);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double VoicePanelY
	{
		get
		{
			return <VoicePanelY>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(<VoicePanelY>k__BackingField, num))
			{
				<VoicePanelY>k__BackingField = num;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.VoicePanelY);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool UseCustomVoicePosition
	{
		get
		{
			return <UseCustomVoicePosition>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<UseCustomVoicePosition>k__BackingField, flag))
			{
				<UseCustomVoicePosition>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.UseCustomVoicePosition);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsChatPanelDragging
	{
		get
		{
			return <IsChatPanelDragging>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsChatPanelDragging>k__BackingField, flag))
			{
				<IsChatPanelDragging>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsChatPanelDragging);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double ChatPanelX
	{
		get
		{
			return <ChatPanelX>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(<ChatPanelX>k__BackingField, num))
			{
				<ChatPanelX>k__BackingField = num;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ChatPanelX);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double ChatPanelY
	{
		get
		{
			return <ChatPanelY>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(<ChatPanelY>k__BackingField, num))
			{
				<ChatPanelY>k__BackingField = num;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ChatPanelY);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool UseCustomChatPosition
	{
		get
		{
			return <UseCustomChatPosition>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<UseCustomChatPosition>k__BackingField, flag))
			{
				<UseCustomChatPosition>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.UseCustomChatPosition);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ShowSnapIndicator
	{
		get
		{
			return <ShowSnapIndicator>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<ShowSnapIndicator>k__BackingField, flag))
			{
				<ShowSnapIndicator>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ShowSnapIndicator);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double SnapIndicatorX
	{
		get
		{
			return <SnapIndicatorX>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(<SnapIndicatorX>k__BackingField, num))
			{
				<SnapIndicatorX>k__BackingField = num;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SnapIndicatorX);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double SnapIndicatorY
	{
		get
		{
			return <SnapIndicatorY>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(<SnapIndicatorY>k__BackingField, num))
			{
				<SnapIndicatorY>k__BackingField = num;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SnapIndicatorY);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double SnapIndicatorWidth
	{
		get
		{
			return <SnapIndicatorWidth>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(<SnapIndicatorWidth>k__BackingField, num))
			{
				<SnapIndicatorWidth>k__BackingField = num;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SnapIndicatorWidth);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double SnapIndicatorHeight
	{
		get
		{
			return <SnapIndicatorHeight>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(<SnapIndicatorHeight>k__BackingField, num))
			{
				<SnapIndicatorHeight>k__BackingField = num;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.SnapIndicatorHeight);
			}
		}
	}

	public OverlayActionBarViewModel? ActionBar => _actionBarViewModel;

	public bool IsChatOverlayVisible => IsChatOverlayEnabled && ChatMessages.Count > 0;

	public bool IsVoiceOverlayVisible => IsVoiceOverlayEnabled && IsInVoiceChannel;

	public ObservableCollection<OverlayVoiceUser> VoiceUsers { get; } = new ObservableCollection<OverlayVoiceUser>();

	public ObservableCollection<OverlayMessageViewModel> ChatMessages { get; } = new ObservableCollection<OverlayMessageViewModel>();

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand DismissWelcomeNotificationCommand => dismissWelcomeNotificationCommand ?? (dismissWelcomeNotificationCommand = new RelayCommand(DismissWelcomeNotification));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ToggleVoiceOverlayEnabledCommand => toggleVoiceOverlayEnabledCommand ?? (toggleVoiceOverlayEnabledCommand = new RelayCommand(ToggleVoiceOverlayEnabled));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ToggleChatOverlayEnabledCommand => toggleChatOverlayEnabledCommand ?? (toggleChatOverlayEnabledCommand = new RelayCommand(ToggleChatOverlayEnabled));

	public event Action<bool>? VoiceOverlayEnabledChanged
	{
		[CompilerGenerated]
		add
		{
			Action<bool> action = this.m_VoiceOverlayEnabledChanged;
			Action<bool> action2;
			do
			{
				action2 = action;
				Action<bool> action3 = (Action<bool>)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_VoiceOverlayEnabledChanged, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action<bool> action = this.m_VoiceOverlayEnabledChanged;
			Action<bool> action2;
			do
			{
				action2 = action;
				Action<bool> action3 = (Action<bool>)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_VoiceOverlayEnabledChanged, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public event Action<bool>? ChatOverlayEnabledChanged
	{
		[CompilerGenerated]
		add
		{
			Action<bool> action = this.m_ChatOverlayEnabledChanged;
			Action<bool> action2;
			do
			{
				action2 = action;
				Action<bool> action3 = (Action<bool>)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_ChatOverlayEnabledChanged, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action<bool> action = this.m_ChatOverlayEnabledChanged;
			Action<bool> action2;
			do
			{
				action2 = action;
				Action<bool> action3 = (Action<bool>)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_ChatOverlayEnabledChanged, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public event Action<double>? OpacityChanged
	{
		[CompilerGenerated]
		add
		{
			Action<double> action = this.m_OpacityChanged;
			Action<double> action2;
			do
			{
				action2 = action;
				Action<double> action3 = (Action<double>)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_OpacityChanged, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action<double> action = this.m_OpacityChanged;
			Action<double> action2;
			do
			{
				action2 = action;
				Action<double> action3 = (Action<double>)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_OpacityChanged, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public event Action<double>? ScaleChanged
	{
		[CompilerGenerated]
		add
		{
			Action<double> action = this.m_ScaleChanged;
			Action<double> action2;
			do
			{
				action2 = action;
				Action<double> action3 = (Action<double>)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_ScaleChanged, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action<double> action = this.m_ScaleChanged;
			Action<double> action2;
			do
			{
				action2 = action;
				Action<double> action3 = (Action<double>)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_ScaleChanged, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public event Action<bool>? ShowAvatarsChanged
	{
		[CompilerGenerated]
		add
		{
			Action<bool> action = this.m_ShowAvatarsChanged;
			Action<bool> action2;
			do
			{
				action2 = action;
				Action<bool> action3 = (Action<bool>)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_ShowAvatarsChanged, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action<bool> action = this.m_ShowAvatarsChanged;
			Action<bool> action2;
			do
			{
				action2 = action;
				Action<bool> action3 = (Action<bool>)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_ShowAvatarsChanged, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public event Action<bool>? ShowNamesChanged
	{
		[CompilerGenerated]
		add
		{
			Action<bool> action = this.m_ShowNamesChanged;
			Action<bool> action2;
			do
			{
				action2 = action;
				Action<bool> action3 = (Action<bool>)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_ShowNamesChanged, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action<bool> action = this.m_ShowNamesChanged;
			Action<bool> action2;
			do
			{
				action2 = action;
				Action<bool> action3 = (Action<bool>)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_ShowNamesChanged, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public event Action<bool>? OnlyShowSpeakingChanged
	{
		[CompilerGenerated]
		add
		{
			Action<bool> action = this.m_OnlyShowSpeakingChanged;
			Action<bool> action2;
			do
			{
				action2 = action;
				Action<bool> action3 = (Action<bool>)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_OnlyShowSpeakingChanged, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action<bool> action = this.m_OnlyShowSpeakingChanged;
			Action<bool> action2;
			do
			{
				action2 = action;
				Action<bool> action3 = (Action<bool>)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_OnlyShowSpeakingChanged, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public event Action<bool, double, double>? VoicePanelPositionChanged
	{
		[CompilerGenerated]
		add
		{
			Action<bool, double, double> action = this.m_VoicePanelPositionChanged;
			Action<bool, double, double> action2;
			do
			{
				action2 = action;
				Action<bool, double, double> action3 = (Action<bool, double, double>)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_VoicePanelPositionChanged, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action<bool, double, double> action = this.m_VoicePanelPositionChanged;
			Action<bool, double, double> action2;
			do
			{
				action2 = action;
				Action<bool, double, double> action3 = (Action<bool, double, double>)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_VoicePanelPositionChanged, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public event Action<bool, double, double>? ChatPanelPositionChanged
	{
		[CompilerGenerated]
		add
		{
			Action<bool, double, double> action = this.m_ChatPanelPositionChanged;
			Action<bool, double, double> action2;
			do
			{
				action2 = action;
				Action<bool, double, double> action3 = (Action<bool, double, double>)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_ChatPanelPositionChanged, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action<bool, double, double> action = this.m_ChatPanelPositionChanged;
			Action<bool, double, double> action2;
			do
			{
				action2 = action;
				Action<bool, double, double> action3 = (Action<bool, double, double>)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_ChatPanelPositionChanged, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public event Action? ResetPositionsRequested
	{
		[CompilerGenerated]
		add
		{
			Action action = this.m_ResetPositionsRequested;
			Action action2;
			do
			{
				action2 = action;
				Action action3 = (Action)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_ResetPositionsRequested, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action action = this.m_ResetPositionsRequested;
			Action action2;
			do
			{
				action2 = action;
				Action action3 = (Action)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_ResetPositionsRequested, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public void NotifyVoicePanelPositionChanged()
	{
		this.VoicePanelPositionChanged?.Invoke(UseCustomVoicePosition, VoicePanelX, VoicePanelY);
	}

	public void NotifyChatPanelPositionChanged()
	{
		this.ChatPanelPositionChanged?.Invoke(UseCustomChatPosition, ChatPanelX, ChatPanelY);
	}

	public void RequestResetPositions()
	{
		this.ResetPositionsRequested?.Invoke();
	}

	public void ShowWelcomeNotificationWithKeybinding(string P_0)
	{
		WelcomeKeybindingText = P_0;
		ShowWelcomeNotification = true;
		_welcomeNotificationTimer?.Stop();
		_welcomeNotificationTimer = new DispatcherTimer
		{
			Interval = TimeSpan.FromSeconds(5L)
		};
		_welcomeNotificationTimer.Tick += delegate
		{
			ShowWelcomeNotification = false;
			_welcomeNotificationTimer?.Stop();
		};
		_welcomeNotificationTimer.Start();
	}

	[RelayCommand]
	private void DismissWelcomeNotification()
	{
		ShowWelcomeNotification = false;
		_welcomeNotificationTimer?.Stop();
	}

	[RelayCommand]
	private void ToggleVoiceOverlayEnabled()
	{
		IsVoiceOverlayEnabled = !IsVoiceOverlayEnabled;
	}

	[RelayCommand]
	private void ToggleChatOverlayEnabled()
	{
		IsChatOverlayEnabled = !IsChatOverlayEnabled;
	}

	private void NotifyChatOverlayVisibilityChanged()
	{
		OnPropertyChanged("IsChatOverlayVisible");
	}

	public OverlayViewModel(IRootSessionAccessor P_0, IGlobalMessageStreamService P_1, IGlobalUserCacheService P_2, OverlayMessageViewModelFactory P_3, BitmapCache P_4, IStreamerModeService P_5, BrowserService P_6)
	{
		_rootSessionAccessor = P_0;
		_globalMessageStreamService = P_1;
		_globalUserCacheService = P_2;
		_overlayMessageViewModelFactory = P_3;
		_bitmapCache = P_4;
		_streamerModeService = P_5;
		_browserService = P_6;
		_actionBarViewModel = new OverlayActionBarViewModel(P_0, P_6);
		_actionBarViewModel.ResetPositionsRequested += RequestResetPositions;
		if (_rootSessionAccessor.Session != null)
		{
			_rootSessionAccessor.Session.ActiveMediaRoomService.PropertyChanged += OnActiveMediaRoomServicePropertyChanged;
			UpdateFromActiveMediaRoom();
		}
	}

	private void SubscribeToMessageStream()
	{
		if (_messageStreamSubscription == null)
		{
			_messageStreamSubscription = _globalMessageStreamService.MessageStream.Subscribe(OnMessageReceived);
		}
	}

	private void UnsubscribeFromMessageStream()
	{
		_messageStreamSubscription?.Dispose();
		_messageStreamSubscription = null;
	}

	private async void OnMessageReceived(GlobalMessageEvent messageEvent)
	{
		if (_disposed || !IsChatOverlayEnabled || _streamerModeService.ShouldDisableNotifications)
		{
			return;
		}
		UserGuid? selfUserId = _rootSessionAccessor.Session?.UserInfoService.SessionUser.Id;
		if (selfUserId.HasValue && messageEvent.Packet.UserId == (UserUuid)selfUserId.Value)
		{
			return;
		}
		GlobalUser senderUser = await _globalUserCacheService.GetUserByIdAsync(messageEvent.Packet.UserId);
		string senderName = senderUser?.UserName ?? "Unknown";
		string avatarUrl = senderUser?.ProfilePictureUri;
		await Dispatcher.UIThread.InvokeAsync(delegate
		{
			if (!_disposed)
			{
				OverlayMessageViewModel overlayMessageViewModel = _overlayMessageViewModelFactory.Create(messageEvent, senderName, avatarUrl);
				ChatMessages.Insert(0, overlayMessageViewModel);
				NotifyChatOverlayVisibilityChanged();
				EnforceMessageLimit();
				EnsureLifecycleTimerRunning();
			}
		});
	}

	private void EnsureLifecycleTimerRunning()
	{
		if (_messageLifecycleTimer == null)
		{
			_messageLifecycleTimer = new DispatcherTimer
			{
				Interval = TimeSpan.FromMilliseconds(500L)
			};
			_messageLifecycleTimer.Tick += OnLifecycleTimerTick;
			_messageLifecycleTimer.Start();
		}
	}

	private void StopLifecycleTimer()
	{
		if (_messageLifecycleTimer != null)
		{
			_messageLifecycleTimer.Tick -= OnLifecycleTimerTick;
			_messageLifecycleTimer.Stop();
			_messageLifecycleTimer = null;
		}
	}

	private void OnLifecycleTimerTick(object? sender, EventArgs e)
	{
		if (_disposed || ChatMessages.Count == 0)
		{
			StopLifecycleTimer();
			return;
		}
		DateTimeOffset utcNow = DateTimeOffset.UtcNow;
		List<OverlayMessageViewModel> list = new List<OverlayMessageViewModel>();
		foreach (OverlayMessageViewModel chatMessage in ChatMessages)
		{
			if (chatMessage.IsFadingOut)
			{
				if (chatMessage.FadeStartedAt.HasValue && utcNow - chatMessage.FadeStartedAt.Value >= FadeOutDuration)
				{
					list.Add(chatMessage);
				}
			}
			else if (utcNow - chatMessage.ReceivedAt >= MessageDisplayDuration)
			{
				chatMessage.FadeStartedAt = utcNow;
				chatMessage.StartFadeOut();
			}
		}
		foreach (OverlayMessageViewModel item in list)
		{
			ChatMessages.Remove(item);
			item.Dispose();
		}
		if (list.Count > 0)
		{
			NotifyChatOverlayVisibilityChanged();
		}
		if (ChatMessages.Count == 0)
		{
			StopLifecycleTimer();
		}
	}

	private void EnforceMessageLimit()
	{
		int num = ChatMessages.Count((OverlayMessageViewModel m) => !m.IsFadingOut);
		int num2 = num - 5;
		if (num2 <= 0)
		{
			return;
		}
		DateTimeOffset utcNow = DateTimeOffset.UtcNow;
		List<OverlayMessageViewModel> list = ChatMessages.Where((OverlayMessageViewModel m) => !m.IsFadingOut).Reverse().Take(num2)
			.ToList();
		foreach (OverlayMessageViewModel item in list)
		{
			item.FadeStartedAt = utcNow;
			item.StartFadeOut();
		}
	}

	private void ClearAllMessages()
	{
		StopLifecycleTimer();
		foreach (OverlayMessageViewModel chatMessage in ChatMessages)
		{
			chatMessage.Dispose();
		}
		ChatMessages.Clear();
		NotifyChatOverlayVisibilityChanged();
	}

	private void OnActiveMediaRoomServicePropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "ActiveMediaRoom")
		{
			Dispatcher.UIThread.Post(UpdateFromActiveMediaRoom);
		}
	}

	private void UpdateFromActiveMediaRoom()
	{
		if (!_disposed)
		{
			MediaRoom mediaRoom = _rootSessionAccessor.Session?.ActiveMediaRoomService.ActiveMediaRoom;
			if (_currentMediaRoom != null && _currentMediaRoom != mediaRoom)
			{
				CleanupMemberSubscriptions();
				_memberSubscription?.Dispose();
				_memberSubscription = null;
				_currentMediaRoom = null;
				VoiceUsers.Clear();
			}
			if (mediaRoom == null)
			{
				IsInVoiceChannel = false;
				ChannelName = null;
				return;
			}
			_currentMediaRoom = mediaRoom;
			IsInVoiceChannel = true;
			ChannelName = mediaRoom.MessageContainer.Name;
			_memberSubscription = mediaRoom.ConnectMembers().Subscribe(OnMemberChanges);
			RefreshVoiceUsers();
		}
	}

	private void OnMemberChanges(IChangeSet<MediaMember> changes)
	{
		if (_disposed)
		{
			return;
		}
		Dispatcher.UIThread.Post(delegate
		{
			if (_disposed)
			{
				return;
			}
			foreach (Change<MediaMember> change in changes)
			{
				switch (change.Reason)
				{
				case ListChangeReason.Add:
					AddOrUpdateUser(change.Item.Current);
					break;
				case ListChangeReason.Remove:
					RemoveUser(change.Item.Current);
					break;
				case ListChangeReason.Replace:
				case ListChangeReason.Refresh:
					AddOrUpdateUser(change.Item.Current);
					break;
				case ListChangeReason.Clear:
					CleanupMemberSubscriptions();
					VoiceUsers.Clear();
					break;
				}
			}
		});
	}

	private void AddOrUpdateUser(MediaMember P_0)
	{
		RootGuid? rootGuid = _currentMediaRoom?.SelfMediaMember?.DeviceId;
		bool flag = rootGuid != null && P_0.DeviceId == rootGuid;
		OverlayVoiceUser overlayVoiceUser = VoiceUsers.FirstOrDefault((OverlayVoiceUser u) => u.DeviceId == P_0.DeviceId);
		if (overlayVoiceUser != null)
		{
			overlayVoiceUser.UpdateFrom(P_0);
			return;
		}
		OverlayVoiceUser overlayVoiceUser2 = OverlayVoiceUser.FromMediaMember(P_0, flag, _bitmapCache);
		VoiceUsers.Add(overlayVoiceUser2);
		SubscribeToMemberChanges(P_0, overlayVoiceUser2);
	}

	private void SubscribeToMemberChanges(MediaMember P_0, OverlayVoiceUser P_1)
	{
		UnsubscribeFromMember(P_0.DeviceId);
		PropertyChangedEventHandler propertyChangedEventHandler = delegate(object? sender, PropertyChangedEventArgs e)
		{
			if (!_disposed)
			{
				MediaMember m = sender as MediaMember;
				if (m != null)
				{
					Dispatcher.UIThread.Post(delegate
					{
						if (!_disposed)
						{
							switch (e.PropertyName)
							{
							case "IsSpeaking":
								P_1.IsSpeaking = m.IsSpeaking;
								break;
							case "IsMuted":
							case "IsAdminMuted":
								P_1.IsMuted = m.IsMuted || m.IsAdminMuted;
								break;
							case "IsDeafened":
							case "IsAdminDeafened":
								P_1.IsDeafened = m.IsDeafened || m.IsAdminDeafened;
								break;
							}
						}
					});
				}
			}
		};
		P_0.PropertyChanged += propertyChangedEventHandler;
		_memberSubscriptions[P_0.DeviceId] = (P_0, propertyChangedEventHandler);
	}

	private void UnsubscribeFromMember(RootGuid P_0)
	{
		if (_memberSubscriptions.TryGetValue(P_0, out (MediaMember, PropertyChangedEventHandler) value))
		{
			value.Item1.PropertyChanged -= value.Item2;
			_memberSubscriptions.Remove(P_0);
		}
	}

	private void CleanupMemberSubscriptions()
	{
		foreach (var value in _memberSubscriptions.Values)
		{
			value.Member.PropertyChanged -= value.Handler;
		}
		_memberSubscriptions.Clear();
	}

	private void RemoveUser(MediaMember P_0)
	{
		UnsubscribeFromMember(P_0.DeviceId);
		OverlayVoiceUser overlayVoiceUser = VoiceUsers.FirstOrDefault((OverlayVoiceUser u) => u.DeviceId == P_0.DeviceId);
		if (overlayVoiceUser != null)
		{
			VoiceUsers.Remove(overlayVoiceUser);
		}
	}

	private void RefreshVoiceUsers()
	{
		if (_currentMediaRoom == null || _disposed)
		{
			return;
		}
		CleanupMemberSubscriptions();
		VoiceUsers.Clear();
		RootGuid? rootGuid = _currentMediaRoom.SelfMediaMember?.DeviceId;
		foreach (MediaMember member in _currentMediaRoom.GetMembers())
		{
			bool flag = rootGuid != null && member.DeviceId == rootGuid;
			OverlayVoiceUser overlayVoiceUser = OverlayVoiceUser.FromMediaMember(member, flag, _bitmapCache);
			VoiceUsers.Add(overlayVoiceUser);
			SubscribeToMemberChanges(member, overlayVoiceUser);
		}
	}

	public void Dispose()
	{
		if (!_disposed)
		{
			_disposed = true;
			_welcomeNotificationTimer?.Stop();
			_welcomeNotificationTimer = null;
			CleanupMemberSubscriptions();
			_memberSubscription?.Dispose();
			_memberSubscription = null;
			UnsubscribeFromMessageStream();
			ClearAllMessages();
			if (_actionBarViewModel != null)
			{
				_actionBarViewModel.ResetPositionsRequested -= RequestResetPositions;
				_actionBarViewModel.Dispose();
			}
			_actionBarViewModel = null;
			if (_rootSessionAccessor.Session != null)
			{
				_rootSessionAccessor.Session.ActiveMediaRoomService.PropertyChanged -= OnActiveMediaRoomServicePropertyChanged;
			}
			VoiceUsers.Clear();
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnIsInVoiceChannelChanged(bool P_0)
	{
		OnPropertyChanged("IsVoiceOverlayVisible");
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnIsChatOverlayEnabledChanged(bool P_0)
	{
		if (P_0)
		{
			SubscribeToMessageStream();
		}
		else
		{
			UnsubscribeFromMessageStream();
			ClearAllMessages();
		}
		OnPropertyChanged("IsChatOverlayVisible");
		this.ChatOverlayEnabledChanged?.Invoke(P_0);
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnIsVoiceOverlayEnabledChanged(bool P_0)
	{
		OnPropertyChanged("IsVoiceOverlayVisible");
		this.VoiceOverlayEnabledChanged?.Invoke(P_0);
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnOpacityChanged(double P_0)
	{
		this.OpacityChanged?.Invoke(P_0);
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnScaleChanged(double P_0)
	{
		this.ScaleChanged?.Invoke(P_0);
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnShowAvatarsChanged(bool P_0)
	{
		this.ShowAvatarsChanged?.Invoke(P_0);
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnShowNamesChanged(bool P_0)
	{
		this.ShowNamesChanged?.Invoke(P_0);
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnOnlyShowSpeakingChanged(bool P_0)
	{
		this.OnlyShowSpeakingChanged?.Invoke(P_0);
	}
}
