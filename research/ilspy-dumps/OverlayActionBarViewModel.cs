using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using RootApp.Browser;
using RootApp.Browser.WebRtc.Services;
using RootApp.Client.CoreDomain;
using RootApp.Client.CoreDomain.Models.Media;
using RootApp.Core.Identifiers;

namespace RootApp.Client.Avalonia.UI.Overlay;

public class OverlayActionBarViewModel : ObservableObject, IDisposable
{
	private readonly IRootSessionAccessor _rootSessionAccessor;

	private readonly BrowserService _browserService;

	private bool _disposed;

	private MediaMember? _selfMember;

	[CompilerGenerated]
	private Action? m_ResetPositionsRequested;

	[CompilerGenerated]
	private bool _003CIsMuted_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsDeafened_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsInVoiceChannel_003Ek__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? toggleMuteCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? toggleDeafenCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? disconnectCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsMuted
	{
		get
		{
			return _003CIsMuted_003Ek__BackingField;
		}
		private set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsMuted_003Ek__BackingField, flag))
			{
				_003CIsMuted_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsMuted);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsDeafened
	{
		get
		{
			return _003CIsDeafened_003Ek__BackingField;
		}
		private set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsDeafened_003Ek__BackingField, flag))
			{
				_003CIsDeafened_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsDeafened);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsInVoiceChannel
	{
		get
		{
			return _003CIsInVoiceChannel_003Ek__BackingField;
		}
		private set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsInVoiceChannel_003Ek__BackingField, flag))
			{
				_003CIsInVoiceChannel_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsInVoiceChannel);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ToggleMuteCommand => toggleMuteCommand ?? (toggleMuteCommand = new RelayCommand(ToggleMute));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ToggleDeafenCommand => toggleDeafenCommand ?? (toggleDeafenCommand = new RelayCommand(ToggleDeafen));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand DisconnectCommand => disconnectCommand ?? (disconnectCommand = new RelayCommand(Disconnect));

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

	public OverlayActionBarViewModel(IRootSessionAccessor P_0, BrowserService P_1)
	{
		_rootSessionAccessor = P_0;
		_browserService = P_1;
		if (_rootSessionAccessor.Session != null)
		{
			_rootSessionAccessor.Session.ActiveMediaRoomService.PropertyChanged += OnActiveMediaRoomChanged;
			SubscribeToSelfMember();
		}
	}

	private void OnActiveMediaRoomChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "ActiveMediaRoom")
		{
			Dispatcher.UIThread.Post(SubscribeToSelfMember);
		}
	}

	private void SubscribeToSelfMember()
	{
		if (_selfMember != null)
		{
			_selfMember.PropertyChanged -= OnSelfMemberPropertyChanged;
			_selfMember = null;
		}
		MediaRoom mediaRoom = _rootSessionAccessor.Session?.ActiveMediaRoomService.ActiveMediaRoom;
		_selfMember = mediaRoom?.SelfMediaMember;
		IsInVoiceChannel = mediaRoom != null;
		if (_selfMember != null)
		{
			_selfMember.PropertyChanged += OnSelfMemberPropertyChanged;
			UpdateStates();
		}
	}

	private void OnSelfMemberPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (!_disposed)
			{
				switch (e.PropertyName)
				{
				case "IsMuted":
				case "IsAdminMuted":
				case "IsDeafened":
				case "IsAdminDeafened":
					UpdateStates();
					break;
				}
			}
		});
	}

	private void UpdateStates()
	{
		if (_selfMember != null)
		{
			IsMuted = _selfMember.IsMuted || _selfMember.IsAdminMuted;
			IsDeafened = _selfMember.IsDeafened || _selfMember.IsAdminDeafened;
			OnPropertyChanged("MuteTooltip");
			OnPropertyChanged("DeafenTooltip");
		}
	}

	[RelayCommand]
	private void ToggleMute()
	{
		MediaRoom mediaRoom = _rootSessionAccessor.Session?.ActiveMediaRoomService.ActiveMediaRoom;
		if (mediaRoom == null)
		{
			return;
		}
		MessageContainerGuid containerId = mediaRoom.MessageContainer.ContainerId;
		if (!(containerId == null))
		{
			WebRtcBrowser webRtcBrowser = _browserService.GetWebRtcBrowser(containerId);
			if (webRtcBrowser != null)
			{
				WebRtcService webRtc = webRtcBrowser.WebRtc;
				MediaMember? selfMediaMember = mediaRoom.SelfMediaMember;
				webRtc.SetMute(selfMediaMember == null || !selfMediaMember.IsMuted);
			}
		}
	}

	[RelayCommand]
	private void ToggleDeafen()
	{
		MediaRoom mediaRoom = _rootSessionAccessor.Session?.ActiveMediaRoomService.ActiveMediaRoom;
		if (mediaRoom == null)
		{
			return;
		}
		MessageContainerGuid containerId = mediaRoom.MessageContainer.ContainerId;
		if (!(containerId == null))
		{
			WebRtcBrowser webRtcBrowser = _browserService.GetWebRtcBrowser(containerId);
			if (webRtcBrowser != null)
			{
				WebRtcService webRtc = webRtcBrowser.WebRtc;
				MediaMember? selfMediaMember = mediaRoom.SelfMediaMember;
				webRtc.SetDeafen(selfMediaMember == null || !selfMediaMember.IsDeafened);
			}
		}
	}

	[RelayCommand]
	private void Disconnect()
	{
		MessageContainerGuid? messageContainerGuid = _rootSessionAccessor.Session?.ActiveMediaRoomService.ActiveMediaRoom?.MessageContainer.ContainerId;
		if (messageContainerGuid != null)
		{
			_browserService.RemoveBrowser(messageContainerGuid.Value);
		}
	}

	public void Dispose()
	{
		if (!_disposed)
		{
			_disposed = true;
			if (_selfMember != null)
			{
				_selfMember.PropertyChanged -= OnSelfMemberPropertyChanged;
				_selfMember = null;
			}
			if (_rootSessionAccessor.Session != null)
			{
				_rootSessionAccessor.Session.ActiveMediaRoomService.PropertyChanged -= OnActiveMediaRoomChanged;
			}
		}
	}
}
