using System.Runtime.CompilerServices;

namespace RootApp.Browser.WebRtc.Models;

public class UserTileVolume
{
	[CompilerGenerated]
	private string _003CuserId_003Ek__BackingField;

	[CompilerGenerated]
	private string _003CtileType_003Ek__BackingField;

	[CompilerGenerated]
	private double _003Cvolume_003Ek__BackingField;

	public string userId
	{
		[CompilerGenerated]
		get
		{
			return _003CuserId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CuserId_003Ek__BackingField = text;
		}
	} = string.Empty;

	public string tileType
	{
		[CompilerGenerated]
		get
		{
			return _003CtileType_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CtileType_003Ek__BackingField = text;
		}
	} = string.Empty;

	public double volume
	{
		[CompilerGenerated]
		get
		{
			return _003Cvolume_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003Cvolume_003Ek__BackingField = num;
		}
	} = 1.0;
}
