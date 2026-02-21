using System.Runtime.CompilerServices;

namespace RootApp.Browser.WebRtc.Models;

public class UserMediaStreamConstraints
{
	[CompilerGenerated]
	private VideoTrackConstraints? _003Cvideo_003Ek__BackingField;

	[CompilerGenerated]
	private AudioTrackConstraints? _003Caudio_003Ek__BackingField;

	public VideoTrackConstraints? video
	{
		[CompilerGenerated]
		get
		{
			return _003Cvideo_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003Cvideo_003Ek__BackingField = videoTrackConstraints;
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
}
