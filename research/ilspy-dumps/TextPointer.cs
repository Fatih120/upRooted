// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Markdown.Components.TextPointer
using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Media;
using RootApp.Client.Avalonia.Markdown.Components;
using RootApp.Client.Avalonia.Markdown.Components.Geometries;

public class TextPointer : IEquatable<TextPointer>, IComparable<TextPointer>
{
	private readonly CInline[] _path;

	public int Index { get; }

	internal int InternalIndex { get; }

	internal int TrailingLength { get; }

	internal double Distance { get; }

	internal CGeometry Geometry { get; }

	internal int PathDepth => _path.Length;

	internal CInline this[int P_0] => _path[P_0];

	private TextPointer(CInline[] P_0, CGeometry P_1, int P_2, int P_3, int P_4, double P_5)
	{
		_path = P_0;
		Geometry = P_1;
		Index = P_2;
		InternalIndex = P_3;
		TrailingLength = P_4;
		Distance = P_5;
	}

	internal TextPointer(CRun P_0, TextLineGeometry P_1, CharacterHit P_2, bool P_3)
	{
		CInline[] path = new CRun[1] { P_0 };
		_path = path;
		Geometry = P_1;
		if (P_3)
		{
			int num = P_2.FirstCharacterIndex + P_2.TrailingLength;
			Index = num - P_1.Line.FirstTextSourceIndex;
			InternalIndex = num;
			TrailingLength = 0;
		}
		else
		{
			Index = P_2.FirstCharacterIndex - P_1.Line.FirstTextSourceIndex;
			InternalIndex = P_2.FirstCharacterIndex;
			TrailingLength = P_2.TrailingLength;
		}
	}

	internal TextPointer(CRun P_0, TextLineGeometry P_1, CharacterHit P_2, double P_3, bool P_4)
		: this(P_0, P_1, P_2, P_4)
	{
		Distance = P_3;
	}

	internal TextPointer(CGeometry P_0)
	{
		_path = new CInline[1] { P_0.Owner };
		Geometry = P_0;
		Index = 0;
		InternalIndex = 0;
		TrailingLength = 0;
	}

	internal TextPointer(CGeometry P_0, int P_1, double P_2)
	{
		_path = new CInline[1] { P_0.Owner };
		Geometry = P_0;
		Index = P_1;
		InternalIndex = 0;
		TrailingLength = 0;
		Distance = P_2;
	}

	internal TextPointer(CTextBlock P_0, int P_1)
	{
		_path = Array.Empty<CInline>();
		Geometry = null;
		Index = P_1;
		InternalIndex = 0;
		TrailingLength = 0;
	}

	internal TextPointer Wrap(CInline P_0, int P_1)
	{
		List<CInline> list = new List<CInline>(_path.Length + 1);
		list.Add(P_0);
		list.AddRange(_path);
		return new TextPointer(list.ToArray(), Geometry, Index + P_1, InternalIndex, TrailingLength, Distance);
	}

	internal TextPointer Wrap(CTextBlock P_0, int P_1)
	{
		return new TextPointer(_path, Geometry, Index + P_1, InternalIndex, TrailingLength, Distance);
	}

	public override int GetHashCode()
	{
		return _path.Sum((CInline e) => e.GetHashCode()) + Index.GetHashCode() + InternalIndex.GetHashCode() + TrailingLength.GetHashCode();
	}

	public bool Equals(TextPointer? P_0)
	{
		return PathDepth == P_0?.PathDepth && Enumerable.Range(0, PathDepth).All((int i) => _path[i] == P_0[i]) && Index == P_0.Index && InternalIndex == P_0.InternalIndex && TrailingLength == P_0.TrailingLength;
	}

	public int CompareTo(TextPointer? P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("other");
		}
		return Index.CompareTo(P_0.Index);
	}

	public static bool operator <(TextPointer P_0, TextPointer P_1)
	{
		return P_0.CompareTo(P_1) < 0;
	}

	public static bool operator >(TextPointer P_0, TextPointer P_1)
	{
		return P_0.CompareTo(P_1) > 0;
	}

	public static bool operator <=(TextPointer P_0, TextPointer P_1)
	{
		return P_0.CompareTo(P_1) <= 0;
	}

	public static bool operator >=(TextPointer P_0, TextPointer P_1)
	{
		return P_0.CompareTo(P_1) >= 0;
	}
}

