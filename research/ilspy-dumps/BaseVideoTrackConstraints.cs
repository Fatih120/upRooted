using System.Runtime.CompilerServices;

namespace RootApp.Browser.WebRtc.Models;

public class BaseVideoTrackConstraints
{
	[CompilerGenerated]
	private ConstrainLong? _003Cwidth_003Ek__BackingField;

	[CompilerGenerated]
	private ConstrainLong? _003Cheight_003Ek__BackingField;

	[CompilerGenerated]
	private ConstrainDouble? _003CframeRate_003Ek__BackingField;

	[CompilerGenerated]
	private ConstrainDouble? _003CaspectRatio_003Ek__BackingField;

	[CompilerGenerated]
	private ConstrainString? _003CresizeMode_003Ek__BackingField;

	public ConstrainLong? width
	{
		[CompilerGenerated]
		get
		{
			return _003Cwidth_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003Cwidth_003Ek__BackingField = constrainLong;
		}
	}

	public ConstrainLong? height
	{
		[CompilerGenerated]
		get
		{
			return _003Cheight_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003Cheight_003Ek__BackingField = constrainLong;
		}
	}

	public ConstrainDouble? frameRate
	{
		[CompilerGenerated]
		get
		{
			return _003CframeRate_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CframeRate_003Ek__BackingField = constrainDouble;
		}
	}

	public ConstrainDouble? aspectRatio
	{
		[CompilerGenerated]
		get
		{
			return _003CaspectRatio_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CaspectRatio_003Ek__BackingField = constrainDouble;
		}
	}

	public ConstrainString? resizeMode
	{
		[CompilerGenerated]
		get
		{
			return _003CresizeMode_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CresizeMode_003Ek__BackingField = constrainString;
		}
	}
}
