// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Markdown.Components.LineInfo
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using RootApp.Client.Avalonia.Markdown.Components;
using RootApp.Client.Avalonia.Markdown.Components.Geometries;

internal class LineInfo
{
	public List<CGeometry> Metries = new List<CGeometry>();

	public double RequestBaseHeight;

	private double _baseHeight1;

	private double _baseHeight2;

	private double _height;

	private double _dheightTop;

	private double _dheightBtm;

	[CompilerGenerated]
	private double _003CTop_003Ek__BackingField;

	[CompilerGenerated]
	private double _003CWidth_003Ek__BackingField;

	public double Top
	{
		[CompilerGenerated]
		get
		{
			return _003CTop_003Ek__BackingField;
		}
		[CompilerGenerated]
		internal set
		{
			_003CTop_003Ek__BackingField = num;
		}
	}

	public double Width
	{
		[CompilerGenerated]
		get
		{
			return _003CWidth_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CWidth_003Ek__BackingField = num;
		}
	}

	public double Height => Math.Max(_height, _dheightTop + _dheightBtm);

	public double BaseHeight => Math.Max(RequestBaseHeight, (_baseHeight1 != 0.0) ? _baseHeight1 : _baseHeight2);

	public bool Add(CGeometry P_0)
	{
		Metries.Add(P_0);
		Width += P_0.Width;
		switch (P_0.TextVerticalAlignment)
		{
		case TextVerticalAlignment.Base:
			Max(ref _baseHeight1, P_0.BaseHeight);
			Max(ref _dheightTop, P_0.BaseHeight);
			Max(ref _dheightBtm, P_0.Height - P_0.BaseHeight);
			break;
		case TextVerticalAlignment.Top:
			Max(ref _baseHeight1, P_0.BaseHeight);
			Max(ref _height, P_0.Height);
			break;
		case TextVerticalAlignment.Center:
			Max(ref _baseHeight1, P_0.Height / 2.0);
			Max(ref _height, P_0.Height);
			break;
		case TextVerticalAlignment.Bottom:
			Max(ref _baseHeight2, P_0.BaseHeight);
			Max(ref _height, P_0.Height);
			break;
		default:
			throw new InvalidOperationException("Unsupported TextVerticalAlignment.");
		}
		return P_0.LineBreak;
	}

	public void OverwriteHeight(double P_0)
	{
		_height = P_0;
		_dheightBtm = (_dheightTop = 0.0);
	}

	private static void Max(ref double P_0, double P_1)
	{
		P_0 = Math.Max(P_0, P_1);
	}
}

