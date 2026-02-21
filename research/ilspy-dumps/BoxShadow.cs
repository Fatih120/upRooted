// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.BoxShadow
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Avalonia;
using Avalonia.Media;
using Avalonia.Utilities;

public struct BoxShadow
{
	private struct ArrayReader(string[] P_0)
	{
		private int _index = 0;

		private readonly string[] _arr = P_0;

		public bool TryReadString([MaybeNullWhen(false)] out string P_0)
		{
			P_0 = null;
			if (_index >= _arr.Length)
			{
				return false;
			}
			P_0 = _arr[_index];
			_index++;
			return true;
		}

		public string ReadString()
		{
			if (!TryReadString(out string result))
			{
				throw new FormatException();
			}
			return result;
		}
	}

	private static readonly char[] s_Separator = new char[2] { ' ', '\t' };

	[CompilerGenerated]
	private double _003COffsetX_003Ek__BackingField;

	[CompilerGenerated]
	private double _003COffsetY_003Ek__BackingField;

	[CompilerGenerated]
	private double _003CBlur_003Ek__BackingField;

	[CompilerGenerated]
	private double _003CSpread_003Ek__BackingField;

	[CompilerGenerated]
	private Color _003CColor_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CIsInset_003Ek__BackingField;

	public double OffsetX
	{
		[CompilerGenerated]
		readonly get
		{
			return _003COffsetX_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003COffsetX_003Ek__BackingField = num;
		}
	}

	public double OffsetY
	{
		[CompilerGenerated]
		readonly get
		{
			return _003COffsetY_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003COffsetY_003Ek__BackingField = num;
		}
	}

	public double Blur
	{
		[CompilerGenerated]
		readonly get
		{
			return _003CBlur_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CBlur_003Ek__BackingField = num;
		}
	}

	public double Spread
	{
		[CompilerGenerated]
		readonly get
		{
			return _003CSpread_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CSpread_003Ek__BackingField = num;
		}
	}

	public Color Color
	{
		[CompilerGenerated]
		readonly get
		{
			return _003CColor_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CColor_003Ek__BackingField = color;
		}
	}

	public bool IsInset
	{
		[CompilerGenerated]
		readonly get
		{
			return _003CIsInset_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CIsInset_003Ek__BackingField = flag;
		}
	}

	public bool Equals(in BoxShadow P_0)
	{
		if (OffsetX == P_0.OffsetX && OffsetY == P_0.OffsetY && Blur == P_0.Blur && Spread == P_0.Spread && Color.Equals(P_0.Color))
		{
			return IsInset == P_0.IsInset;
		}
		return false;
	}

	public override bool Equals(object? P_0)
	{
		if (P_0 is BoxShadow boxShadow)
		{
			return Equals(in boxShadow);
		}
		return false;
	}

	public override int GetHashCode()
	{
		return (((((((((OffsetX.GetHashCode() * 397) ^ OffsetY.GetHashCode()) * 397) ^ Blur.GetHashCode()) * 397) ^ Spread.GetHashCode()) * 397) ^ Color.GetHashCode()) * 397) ^ IsInset.GetHashCode();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = StringBuilderCache.Acquire();
		ToString(stringBuilder);
		return StringBuilderCache.GetStringAndRelease(stringBuilder);
	}

	internal void ToString(StringBuilder P_0)
	{
		if (this == default(BoxShadow))
		{
			P_0.Append("none");
			return;
		}
		if (IsInset)
		{
			P_0.Append("inset ");
		}
		P_0.AppendFormat(CultureInfo.InvariantCulture, "{0} ", OffsetX);
		P_0.AppendFormat(CultureInfo.InvariantCulture, "{0} ", OffsetY);
		if (Blur != 0.0 || Spread != 0.0)
		{
			P_0.AppendFormat(CultureInfo.InvariantCulture, "{0} ", Blur);
		}
		if (Spread != 0.0)
		{
			P_0.AppendFormat(CultureInfo.InvariantCulture, "{0} ", Spread);
		}
		Color.ToString(P_0);
	}

	public static BoxShadow Parse(string P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException();
		}
		if (P_0.Length == 0)
		{
			throw new FormatException();
		}
		string[] array = StringSplitter.SplitRespectingBrackets(P_0, s_Separator, '(', ')', StringSplitOptions.RemoveEmptyEntries);
		if (array.Length == 1 && array[0] == "none")
		{
			return default(BoxShadow);
		}
		if (array.Length < 3 || array.Length > 6)
		{
			throw new FormatException();
		}
		bool isInset = false;
		ArrayReader arrayReader = new ArrayReader(array);
		string text = arrayReader.ReadString();
		if (text == "inset")
		{
			isInset = true;
			text = arrayReader.ReadString();
		}
		double offsetX = double.Parse(text, CultureInfo.InvariantCulture);
		double offsetY = double.Parse(arrayReader.ReadString(), CultureInfo.InvariantCulture);
		double blur = 0.0;
		double spread = 0.0;
		arrayReader.TryReadString(out string text2);
		arrayReader.TryReadString(out string text3);
		arrayReader.TryReadString(out string text4);
		if (text3 != null)
		{
			blur = double.Parse(text2, CultureInfo.InvariantCulture);
		}
		if (text4 != null)
		{
			spread = double.Parse(text3, CultureInfo.InvariantCulture);
		}
		Color color = Color.Parse(text4 ?? text3 ?? text2);
		return new BoxShadow
		{
			IsInset = isInset,
			OffsetX = offsetX,
			OffsetY = offsetY,
			Blur = blur,
			Spread = spread,
			Color = color
		};
	}

	public Rect TransformBounds(in Rect P_0)
	{
		if (!IsInset)
		{
			return P_0.Translate(new Vector(OffsetX, OffsetY)).Inflate(Spread + Blur);
		}
		return P_0;
	}

	public static bool operator ==(BoxShadow P_0, BoxShadow P_1)
	{
		return P_0.Equals(in P_1);
	}

	public static bool operator !=(BoxShadow P_0, BoxShadow P_1)
	{
		return !(P_0 == P_1);
	}
}

