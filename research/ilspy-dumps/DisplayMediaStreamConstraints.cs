using System.Runtime.CompilerServices;

namespace RootApp.Browser.WebRtc.Models;

public class DisplayMediaStreamConstraints
{
	[CompilerGenerated]
	private ScreenTrackConstraints? _003Cvideo_003Ek__BackingField;

	[CompilerGenerated]
	private AudioTrackConstraints? _003Caudio_003Ek__BackingField;

	[CompilerGenerated]
	private string? _003CselfBrowserSurface_003Ek__BackingField;

	[CompilerGenerated]
	private string? _003CsystemAudio_003Ek__BackingField;

	[CompilerGenerated]
	private string? _003CwindowAudio_003Ek__BackingField;

	[CompilerGenerated]
	private string? _003CsurfaceSwitching_003Ek__BackingField;

	[CompilerGenerated]
	private string? _003CmonitorTypeSurfaces_003Ek__BackingField;

	public ScreenTrackConstraints? video
	{
		[CompilerGenerated]
		get
		{
			return _003Cvideo_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003Cvideo_003Ek__BackingField = screenTrackConstraints;
		}
	}

	public AudioTrackConstraints? audio
	{
		[CompilerGenerated]
		get
		{
			return _003Caudio_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003Caudio_003Ek__BackingField = audioTrackConstraints;
		}
	}

	public string? selfBrowserSurface
	{
		[CompilerGenerated]
		get
		{
			return _003CselfBrowserSurface_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CselfBrowserSurface_003Ek__BackingField = text;
		}
	}

	public string? systemAudio
	{
		[CompilerGenerated]
		get
		{
			return _003CsystemAudio_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CsystemAudio_003Ek__BackingField = text;
		}
	}

	public string? windowAudio
	{
		[CompilerGenerated]
		get
		{
			return _003CwindowAudio_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CwindowAudio_003Ek__BackingField = text;
		}
	}

	public string? surfaceSwitching
	{
		[CompilerGenerated]
		get
		{
			return _003CsurfaceSwitching_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CsurfaceSwitching_003Ek__BackingField = text;
		}
	}

	public string? monitorTypeSurfaces
	{
		[CompilerGenerated]
		get
		{
			return _003CmonitorTypeSurfaces_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CmonitorTypeSurfaces_003Ek__BackingField = text;
		}
	}
}
