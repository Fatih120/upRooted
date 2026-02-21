using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.UI.Messages;
using RootApp.Client.CoreDomain.Models.Messages;
using RootApp.Client.CoreDomain.Services;
using RootApp.Core.Identifiers;

namespace RootApp.Client.Avalonia.UI.Overlay;

public class OverlayMessageViewModel : ObservableObject, IDisposable
{
	private readonly BitmapCache _bitmapCache;

	private readonly string? _avatarUrl;

	private bool _disposed;

	[CompilerGenerated]
	private readonly Guid _003CInstanceId_003Ek__BackingField = Guid.NewGuid();

	[CompilerGenerated]
	private readonly MessageGuid _003CMessageId_003Ek__BackingField;

	[CompilerGenerated]
	private double _003COpacity_003Ek__BackingField = 0.0;

	[CompilerGenerated]
	private double _003CScale_003Ek__BackingField = 1.0;

	[CompilerGenerated]
	private DateTimeOffset? _003CFadeStartedAt_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsFadingOut_003Ek__BackingField;

	public string SenderName { get; }

	public string ContainerName { get; }

	public string? CommunityName { get; }

	public bool IsDirectMessage { get; }

	public DateTimeOffset ReceivedAt { get; } = DateTimeOffset.UtcNow;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsFadingOut
	{
		get
		{
			return _003CIsFadingOut_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsFadingOut_003Ek__BackingField, flag))
			{
				_003CIsFadingOut_003Ek__BackingField = flag;
				OnIsFadingOutChanged(flag);
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsFadingOut);
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
			return _003COpacity_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(_003COpacity_003Ek__BackingField, num))
			{
				_003COpacity_003Ek__BackingField = num;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.Opacity);
			}
		}
	}

	public DateTimeOffset? FadeStartedAt
	{
		[CompilerGenerated]
		get
		{
			return _003CFadeStartedAt_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CFadeStartedAt_003Ek__BackingField = dateTimeOffset;
		}
	}

	public Message Message { get; }

	public MessageViewModel MessageViewModel { get; }

	public Task<BitmapWrapper?> AvatarBitmap => _bitmapCache.GetBitmapAsync(_avatarUrl, null, 120);

	public string ContextDisplay => IsDirectMessage ? "DM" : (CommunityName + " | #" + ContainerName);

	public OverlayMessageViewModel(GlobalMessageEvent P_0, string P_1, string? P_2, Message P_3, MessageViewModel P_4, BitmapCache P_5)
	{
		_bitmapCache = P_5;
		_avatarUrl = P_2;
		_003CMessageId_003Ek__BackingField = P_0.Packet.Id;
		SenderName = P_1;
		ContainerName = P_0.ContainerName;
		CommunityName = P_0.CommunityName;
		IsDirectMessage = P_0.IsDirectMessage;
		Message = P_3;
		MessageViewModel = P_4;
	}

	public void StartFadeOut()
	{
		IsFadingOut = true;
	}

	public void Dispose()
	{
		if (!_disposed)
		{
			_disposed = true;
			MessageViewModel.Dispose();
			Message.Dispose();
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnIsFadingOutChanged(bool P_0)
	{
		if (P_0)
		{
			Opacity = 0.0;
		}
	}
}
