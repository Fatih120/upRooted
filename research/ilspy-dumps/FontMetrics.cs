// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.FontMetrics
using System.Runtime.CompilerServices;

public readonly record struct FontMetrics
{
	public short DesignEmHeight
	{
		[CompilerGenerated]
		get
		{
			return _003CDesignEmHeight_003Ek__BackingField;
		}
		[CompilerGenerated]
		init
		{
			_003CDesignEmHeight_003Ek__BackingField = num;
		}
	}

	public bool IsFixedPitch
	{
		[CompilerGenerated]
		get
		{
			return _003CIsFixedPitch_003Ek__BackingField;
		}
		[CompilerGenerated]
		init
		{
			_003CIsFixedPitch_003Ek__BackingField = flag;
		}
	}

	public int Ascent
	{
		[CompilerGenerated]
		get
		{
			return _003CAscent_003Ek__BackingField;
		}
		[CompilerGenerated]
		init
		{
			_003CAscent_003Ek__BackingField = num;
		}
	}

	public int Descent
	{
		[CompilerGenerated]
		get
		{
			return _003CDescent_003Ek__BackingField;
		}
		[CompilerGenerated]
		init
		{
			_003CDescent_003Ek__BackingField = num;
		}
	}

	public int LineGap
	{
		[CompilerGenerated]
		get
		{
			return _003CLineGap_003Ek__BackingField;
		}
		[CompilerGenerated]
		init
		{
			_003CLineGap_003Ek__BackingField = num;
		}
	}

	public int LineSpacing => Descent - Ascent + LineGap;

	public int UnderlinePosition
	{
		[CompilerGenerated]
		get
		{
			return _003CUnderlinePosition_003Ek__BackingField;
		}
		[CompilerGenerated]
		init
		{
			_003CUnderlinePosition_003Ek__BackingField = num;
		}
	}

	public int UnderlineThickness
	{
		[CompilerGenerated]
		get
		{
			return _003CUnderlineThickness_003Ek__BackingField;
		}
		[CompilerGenerated]
		init
		{
			_003CUnderlineThickness_003Ek__BackingField = num;
		}
	}

	public int StrikethroughPosition
	{
		[CompilerGenerated]
		get
		{
			return _003CStrikethroughPosition_003Ek__BackingField;
		}
		[CompilerGenerated]
		init
		{
			_003CStrikethroughPosition_003Ek__BackingField = num;
		}
	}

	public int StrikethroughThickness
	{
		[CompilerGenerated]
		get
		{
			return _003CStrikethroughThickness_003Ek__BackingField;
		}
		[CompilerGenerated]
		init
		{
			_003CStrikethroughThickness_003Ek__BackingField = num;
		}
	}
}

