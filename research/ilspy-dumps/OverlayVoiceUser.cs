using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.CoreDomain.Models.Media;
using RootApp.Core.Identifiers;

namespace RootApp.Client.Avalonia.UI.Overlay;

public class OverlayVoiceUser : ObservableObject
{
	private readonly BitmapCache _bitmapCache;

	private string? _avatarUrl;

	[CompilerGenerated]
	private string _003CDisplayName_003Ek__BackingField = string.Empty;

	[CompilerGenerated]
	private readonly UserGuid _003CUserId_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsSpeaking_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsMuted_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsDeafened_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsSelf_003Ek__BackingField;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string DisplayName
	{
		get
		{
			return _003CDisplayName_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(_003CDisplayName_003Ek__BackingField, text))
			{
				_003CDisplayName_003Ek__BackingField = text;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.DisplayName);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsSpeaking
	{
		get
		{
			return _003CIsSpeaking_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsSpeaking_003Ek__BackingField, flag))
			{
				_003CIsSpeaking_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsSpeaking);
			}
		}
	}

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsMuted
	{
		get
		{
			return _003CIsMuted_003Ek__BackingField;
		}
		set
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
		set
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
	public bool IsSelf
	{
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(_003CIsSelf_003Ek__BackingField, flag))
			{
				_003CIsSelf_003Ek__BackingField = flag;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.IsSelf);
			}
		}
	}

	public RootGuid DeviceId { get; }

	public Task<BitmapWrapper?> AvatarBitmap => _bitmapCache.GetBitmapAsync(_avatarUrl, null, 120);

	public OverlayVoiceUser(UserGuid P_0, RootGuid P_1, BitmapCache P_2)
	{
		_003CUserId_003Ek__BackingField = P_0;
		DeviceId = P_1;
		_bitmapCache = P_2;
	}

	public static OverlayVoiceUser FromMediaMember(MediaMember P_0, bool P_1, BitmapCache P_2)
	{
		return new OverlayVoiceUser(P_0.UserId, P_0.DeviceId, P_2)
		{
			DisplayName = (P_0.GlobalUser.UserName ?? "Unknown"),
			_avatarUrl = P_0.GlobalUser.ProfilePictureUri,
			IsSpeaking = P_0.IsSpeaking,
			IsMuted = (P_0.IsMuted || P_0.IsAdminMuted),
			IsDeafened = (P_0.IsDeafened || P_0.IsAdminDeafened),
			IsSelf = P_1
		};
	}

	public void UpdateFrom(MediaMember P_0)
	{
		DisplayName = P_0.GlobalUser.UserName ?? "Unknown";
		if (_avatarUrl != P_0.GlobalUser.ProfilePictureUri)
		{
			_avatarUrl = P_0.GlobalUser.ProfilePictureUri;
			OnPropertyChanged("AvatarBitmap");
		}
		IsSpeaking = P_0.IsSpeaking;
		IsMuted = P_0.IsMuted || P_0.IsAdminMuted;
		IsDeafened = P_0.IsDeafened || P_0.IsAdminDeafened;
	}
}
