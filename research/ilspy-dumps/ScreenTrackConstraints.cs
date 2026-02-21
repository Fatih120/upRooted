using System.Runtime.CompilerServices;

namespace RootApp.Browser.WebRtc.Models;

public class ScreenTrackConstraints : BaseVideoTrackConstraints
{
	[CompilerGenerated]
	private ConstrainString? _003CdisplaySurface_003Ek__BackingField;

	[CompilerGenerated]
	private ConstrainBoolean? _003ClogicalSurface_003Ek__BackingField;

	[CompilerGenerated]
	private ConstrainString? _003Ccursor_003Ek__BackingField;

	[CompilerGenerated]
	private ConstrainDouble? _003CscreenPixelRatio_003Ek__BackingField;

	[CompilerGenerated]
	private ConstrainBoolean? _003CrestrictOwnAudio_003Ek__BackingField;

	[CompilerGenerated]
	private ConstrainBoolean? _003CsuppressLocalAudioPlayback_003Ek__BackingField;

	public ConstrainString? displaySurface
	{
		[CompilerGenerated]
		get
		{
			return _003CdisplaySurface_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CdisplaySurface_003Ek__BackingField = constrainString;
		}
	}

	public ConstrainBoolean? logicalSurface
	{
		[CompilerGenerated]
		get
		{
			return _003ClogicalSurface_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003ClogicalSurface_003Ek__BackingField = constrainBoolean;
		}
	}

	public ConstrainString? cursor
	{
		[CompilerGenerated]
		get
		{
			return _003Ccursor_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003Ccursor_003Ek__BackingField = constrainString;
		}
	}

	public ConstrainDouble? screenPixelRatio
	{
		[CompilerGenerated]
		get
		{
			return _003CscreenPixelRatio_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CscreenPixelRatio_003Ek__BackingField = constrainDouble;
		}
	}

	public ConstrainBoolean? restrictOwnAudio
	{
		[CompilerGenerated]
		get
		{
			return _003CrestrictOwnAudio_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CrestrictOwnAudio_003Ek__BackingField = constrainBoolean;
		}
	}

	public ConstrainBoolean? suppressLocalAudioPlayback
	{
		[CompilerGenerated]
		get
		{
			return _003CsuppressLocalAudioPlayback_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CsuppressLocalAudioPlayback_003Ek__BackingField = constrainBoolean;
		}
	}
}
