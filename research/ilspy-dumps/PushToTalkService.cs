using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using RootApp.Client.Avalonia.Helpers.LowLevelHook.KeyBinds;
using RootApp.Client.Domain.Helpers.Store;
using SharpHook.Data;

namespace RootApp.Client.Avalonia.Helpers.Calling;

public class PushToTalkService : ObservableObject
{
	private readonly ILocalDataStore _localDataStore;

	private readonly IGlobalHotkeyManager _globalHotkeyManager;

	private Action? _onPressed;

	private Action? _onReleased;

	[CompilerGenerated]
	private bool <IsPushToTalkEnabled>k__BackingField = false;

	[CompilerGenerated]
	private KeyGesture <PushToTalkKeybind>k__BackingField = KeyGesture.From(KeyCode.VcLeftShift);

	[CompilerGenerated]
	private double <PushToTalkDelay>k__BackingField = 30.0;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsPushToTalkEnabled
	{
		get
		{
			return <IsPushToTalkEnabled>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(<IsPushToTalkEnabled>k__BackingField, flag))
			{
				<IsPushToTalkEnabled>k__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsPushToTalkEnabled);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public KeyGesture PushToTalkKeybind
	{
		get
		{
			return <PushToTalkKeybind>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<KeyGesture>.Default.Equals(<PushToTalkKeybind>k__BackingField, keyGesture))
			{
				<PushToTalkKeybind>k__BackingField = keyGesture;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.PushToTalkKeybind);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double PushToTalkDelay
	{
		get
		{
			return <PushToTalkDelay>k__BackingField;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(<PushToTalkDelay>k__BackingField, num))
			{
				<PushToTalkDelay>k__BackingField = num;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.PushToTalkDelay);
			}
		}
	}

	public PushToTalkService(ILocalDataStore P_0, IGlobalHotkeyManager P_1)
	{
		_localDataStore = P_0;
		_globalHotkeyManager = P_1;
		if (_localDataStore.TryGetGlobal(DataStoreKeys.PushToTalk, out int value))
		{
			IsPushToTalkEnabled = Convert.ToBoolean(value);
		}
		if (_localDataStore.TryGetGlobal(DataStoreKeys.PushToTalkKeybind, out string text) && text != null && KeyGesture.TryParse(text, out var pushToTalkKeybind))
		{
			PushToTalkKeybind = pushToTalkKeybind;
		}
		if (_localDataStore.TryGetGlobal(DataStoreKeys.PushToTalkDelay, out double pushToTalkDelay))
		{
			PushToTalkDelay = pushToTalkDelay;
		}
		buildHookRegistrations();
	}

	public void SetPushToTalkMode(bool P_0)
	{
		_localDataStore.SetGlobal(DataStoreKeys.PushToTalk, Convert.ToInt32(P_0));
		IsPushToTalkEnabled = P_0;
		buildHookRegistrations();
	}

	public void SetPushToTalkKeybind(KeyGesture P_0)
	{
		_localDataStore.SetGlobal(DataStoreKeys.PushToTalkKeybind, P_0.ToString());
		PushToTalkKeybind = P_0;
		buildHookRegistrations();
	}

	public void SetPushToTalkDelay(double P_0)
	{
		_localDataStore.SetGlobal(DataStoreKeys.PushToTalkDelay, P_0);
		PushToTalkDelay = P_0;
	}

	public void SetPushToTalkOnPressAction(Action P_0)
	{
		_onPressed = P_0;
		buildHookRegistrations();
	}

	public void SetPushToTalkOnReleaseAction(Action P_0)
	{
		_onReleased = P_0;
		buildHookRegistrations();
	}

	public void StartListening()
	{
		_globalHotkeyManager.EnableContext("call");
	}

	public void StopListening()
	{
		_globalHotkeyManager.DisableContext("call");
	}

	private void buildHookRegistrations()
	{
		_globalHotkeyManager.Unregister("PTT");
		if (IsPushToTalkEnabled)
		{
			_globalHotkeyManager.Register(new HotkeyRegistration("PTT", PushToTalkKeybind, HotkeyMode.Hold, _onPressed, _onReleased, "call"));
		}
	}
}
