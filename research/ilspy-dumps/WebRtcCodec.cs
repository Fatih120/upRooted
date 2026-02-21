using System.Runtime.CompilerServices;

namespace RootApp.Browser.WebRtc.Models;

public class WebRtcCodec
{
	[CompilerGenerated]
	private string _003Cname_003Ek__BackingField;

	[CompilerGenerated]
	private string? _003CprofileLevelId_003Ek__BackingField;

	[CompilerGenerated]
	private int? _003CpacketizationMode_003Ek__BackingField;

	public string name
	{
		[CompilerGenerated]
		get
		{
			return _003Cname_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003Cname_003Ek__BackingField = text;
		}
	} = string.Empty;

	public string? profileLevelId
	{
		[CompilerGenerated]
		get
		{
			return _003CprofileLevelId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CprofileLevelId_003Ek__BackingField = text;
		}
	} = string.Empty;

	public int? packetizationMode
	{
		[CompilerGenerated]
		get
		{
			return _003CpacketizationMode_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CpacketizationMode_003Ek__BackingField = num;
		}
	} = 1;
}
