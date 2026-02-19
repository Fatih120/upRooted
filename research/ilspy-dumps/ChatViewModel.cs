// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.ChatViewModel
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using FluentValidation;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Controls.Settings;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;
using RootApp.Client.CoreDomain;
using RootApp.Client.Domain.Helpers.Store;

public class ChatViewModel : ViewModelBase<ChatViewModel>, IPage
{
	private readonly ILocalDataStore _localDataStore;

	private readonly IRootSessionAccessor _rootSessionAccessor;

	[CompilerGenerated]
	private bool _003CAutomaticallyConvertEmojis_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CTapToReply_003Ek__BackingField;

	public string PageTitle => Resources.Chat;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool AutomaticallyConvertEmojis
	{
		get
		{
			return _003CAutomaticallyConvertEmojis_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CAutomaticallyConvertEmojis_003Ek__BackingField, flag))
			{
				_003CAutomaticallyConvertEmojis_003Ek__BackingField = flag;
				OnAutomaticallyConvertEmojisChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.AutomaticallyConvertEmojis);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool TapToReply
	{
		get
		{
			return _003CTapToReply_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CTapToReply_003Ek__BackingField, flag))
			{
				_003CTapToReply_003Ek__BackingField = flag;
				OnTapToReplyChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.TapToReply);
			}
		}
	}

	public ChatViewModel(ILocalDataStore P_0, IRootSessionAccessor P_1)
		: base((IValidator<ChatViewModel>?)null)
	{
		_localDataStore = P_0;
		_rootSessionAccessor = P_1;
		ILocalDataStore localDataStore = _localDataStore;
		global::_003C_003Ey__InlineArray2<string> _003C_003Ey__InlineArray281 = default(global::_003C_003Ey__InlineArray2<string>);
		_003C_003Ey__InlineArray281[0] = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id.ToString();
		_003C_003Ey__InlineArray281[1] = "AutoConvertEmojis";
		if (!localDataStore.TryGetWithPath((ReadOnlySpan<string>)_003C_003Ey__InlineArray281, out int value))
		{
			value = 0;
		}
		AutomaticallyConvertEmojis = Convert.ToBoolean(value);
		ILocalDataStore localDataStore2 = _localDataStore;
		global::_003C_003Ey__InlineArray2<string> _003C_003Ey__InlineArray282 = default(global::_003C_003Ey__InlineArray2<string>);
		_003C_003Ey__InlineArray282[0] = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id.ToString();
		_003C_003Ey__InlineArray282[1] = "TapToReply";
		if (!localDataStore2.TryGetWithPath((ReadOnlySpan<string>)_003C_003Ey__InlineArray282, out int value2))
		{
			value2 = 1;
		}
		TapToReply = Convert.ToBoolean(value2);
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnAutomaticallyConvertEmojisChanged(bool P_0)
	{
		ILocalDataStore localDataStore = _localDataStore;
		global::_003C_003Ey__InlineArray2<string> _003C_003Ey__InlineArray281 = default(global::_003C_003Ey__InlineArray2<string>);
		_003C_003Ey__InlineArray281[0] = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id.ToString();
		_003C_003Ey__InlineArray281[1] = "AutoConvertEmojis";
		localDataStore.SetWithPath(_003C_003Ey__InlineArray281, Convert.ToInt32(AutomaticallyConvertEmojis));
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnTapToReplyChanged(bool P_0)
	{
		ILocalDataStore localDataStore = _localDataStore;
		global::_003C_003Ey__InlineArray2<string> _003C_003Ey__InlineArray281 = default(global::_003C_003Ey__InlineArray2<string>);
		_003C_003Ey__InlineArray281[0] = _rootSessionAccessor.Session.UserInfoService.SessionUser.Id.ToString();
		_003C_003Ey__InlineArray281[1] = "TapToReply";
		localDataStore.SetWithPath(_003C_003Ey__InlineArray281, Convert.ToInt32(TapToReply));
	}
}
