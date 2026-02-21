// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.SpanRider
using System.Runtime.CompilerServices;
using Avalonia.Utilities;

internal struct SpanRider
{
	private readonly SpanVector _spans;

	private SpanPosition _spanPosition;

	[CompilerGenerated]
	private int _003CLength_003Ek__BackingField;

	[CompilerGenerated]
	private int _003CCurrentPosition_003Ek__BackingField;

	public int Length
	{
		[CompilerGenerated]
		readonly get
		{
			return _003CLength_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CLength_003Ek__BackingField = num;
		}
	}

	private int CurrentPosition
	{
		[CompilerGenerated]
		set
		{
			_003CCurrentPosition_003Ek__BackingField = num;
		}
	}

	public object? CurrentElement
	{
		get
		{
			if (_spanPosition.Index < _spans.Count)
			{
				return _spans[_spanPosition.Index].element;
			}
			return _spans.Default;
		}
	}

	public SpanRider(SpanVector P_0, SpanPosition P_1 = default(SpanPosition), int P_2 = 0)
	{
		_spans = P_0;
		_spanPosition = default(SpanPosition);
		CurrentPosition = 0;
		Length = 0;
		At(P_1, P_2);
	}

	public bool At(SpanPosition P_0, int P_1)
	{
		bool num = _spans.FindSpan(P_1, P_0, out _spanPosition);
		if (num)
		{
			Length = _spans[_spanPosition.Index].length - (P_1 - _spanPosition.Offset);
			CurrentPosition = P_1;
			return num;
		}
		Length = int.MaxValue;
		CurrentPosition = _spanPosition.Offset;
		return num;
	}
}

