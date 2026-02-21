using System.Runtime.CompilerServices;

namespace RootApp.Browser.WebRtc.Models;

public class VideoTrackConstraints : BaseVideoTrackConstraints
{
	[CompilerGenerated]
	private ConstrainString? _003CfacingMode_003Ek__BackingField;

	[CompilerGenerated]
	private ConstrainBoolean? _003CbackgroundBlur_003Ek__BackingField;

	[CompilerGenerated]
	private ConstrainString? _003CgroupId_003Ek__BackingField;

	public ConstrainString? facingMode
	{
		[CompilerGenerated]
		get
		{
			return _003CfacingMode_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CfacingMode_003Ek__BackingField = constrainString;
		}
	}

	public ConstrainBoolean? backgroundBlur
	{
		[CompilerGenerated]
		get
		{
			return _003CbackgroundBlur_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CbackgroundBlur_003Ek__BackingField = constrainBoolean;
		}
	}

	public ConstrainString? groupId
	{
		[CompilerGenerated]
		get
		{
			return _003CgroupId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CgroupId_003Ek__BackingField = constrainString;
		}
	}
}
