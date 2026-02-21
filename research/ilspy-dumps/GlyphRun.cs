// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.GlyphRun
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;
using Avalonia.Media.TextFormatting.Unicode;
using Avalonia.Platform;
using Avalonia.Utilities;

public sealed class GlyphRun : IDisposable
{
	private static readonly IPlatformRenderInterface s_renderInterface;

	private IRef<IGlyphRunImpl>? _platformImpl;

	private double _fontRenderingEmSize;

	private int _biDiLevel;

	private GlyphRunMetrics? _glyphRunMetrics;

	private ReadOnlyMemory<char> _characters;

	private IReadOnlyList<GlyphInfo> _glyphInfos;

	private Point? _baselineOrigin;

	private bool _hasOneCharPerCluster;

	public IGlyphTypeface GlyphTypeface { get; }

	public double FontRenderingEmSize => _fontRenderingEmSize;

	public Rect Bounds => new Rect(new Point(BaselineOrigin.X, 0.0), new Size(Metrics.WidthIncludingTrailingWhitespace, Metrics.Height));

	public Rect InkBounds => PlatformImpl.Item.Bounds;

	public GlyphRunMetrics Metrics
	{
		get
		{
			GlyphRunMetrics valueOrDefault = _glyphRunMetrics.GetValueOrDefault();
			if (!_glyphRunMetrics.HasValue)
			{
				valueOrDefault = CreateGlyphRunMetrics();
				_glyphRunMetrics = valueOrDefault;
				return valueOrDefault;
			}
			return valueOrDefault;
		}
	}

	public Point BaselineOrigin => _baselineOrigin ?? new Point(0.0, Metrics.Baseline);

	public ReadOnlyMemory<char> Characters => _characters;

	public IReadOnlyList<GlyphInfo> GlyphInfos
	{
		get
		{
			return _glyphInfos;
		}
		set
		{
			Set(ref _glyphInfos, readOnlyList);
			_hasOneCharPerCluster = false;
		}
	}

	public int BiDiLevel => _biDiLevel;

	internal double Scale => FontRenderingEmSize / (double)GlyphTypeface.Metrics.DesignEmHeight;

	public bool IsLeftToRight => (BiDiLevel & 1) == 0;

	internal IRef<IGlyphRunImpl> PlatformImpl => _platformImpl ?? (_platformImpl = CreateGlyphRunImpl());

	static GlyphRun()
	{
		s_renderInterface = AvaloniaLocator.Current.GetRequiredService<IPlatformRenderInterface>();
	}

	public GlyphRun(IGlyphTypeface P_0, double P_1, ReadOnlyMemory<char> P_2, IReadOnlyList<ushort> P_3, Point? P_4 = null, int P_5 = 0)
		: this(P_0, P_1, P_2, CreateGlyphInfos(P_3, P_1, P_0), P_4, P_5)
	{
		_hasOneCharPerCluster = true;
	}

	public GlyphRun(IGlyphTypeface P_0, double P_1, ReadOnlyMemory<char> P_2, IReadOnlyList<GlyphInfo> P_3, Point? P_4 = null, int P_5 = 0)
	{
		GlyphTypeface = P_0;
		_fontRenderingEmSize = P_1;
		_characters = P_2;
		_glyphInfos = P_3;
		_baselineOrigin = P_4;
		_biDiLevel = P_5;
	}

	private static IReadOnlyList<GlyphInfo> CreateGlyphInfos(IReadOnlyList<ushort> P_0, double P_1, IGlyphTypeface P_2)
	{
		ReadOnlySpan<ushort> readOnlySpan = ListToSpan(P_0);
		int[] glyphAdvances = P_2.GetGlyphAdvances(readOnlySpan);
		GlyphInfo[] array = new GlyphInfo[readOnlySpan.Length];
		double num = P_1 / (double)P_2.Metrics.DesignEmHeight;
		for (int i = 0; i < readOnlySpan.Length; i++)
		{
			array[i] = new GlyphInfo(readOnlySpan[i], i, (double)glyphAdvances[i] * num);
		}
		return array;
	}

	private static ReadOnlySpan<ushort> ListToSpan(IReadOnlyList<ushort> P_0)
	{
		int count = P_0.Count;
		if (count == 0)
		{
			return default(ReadOnlySpan<ushort>);
		}
		if (P_0 is ushort[] array)
		{
			return array.AsSpan();
		}
		if (P_0 is List<ushort> list)
		{
			return CollectionsMarshal.AsSpan(list);
		}
		ushort[] array2 = new ushort[count];
		for (int i = 0; i < count; i++)
		{
			array2[i] = P_0[i];
		}
		return array2.AsSpan();
	}

	public double GetDistanceFromCharacterHit(CharacterHit P_0)
	{
		int num = P_0.FirstCharacterIndex + P_0.TrailingLength;
		bool flag = P_0.TrailingLength > 0;
		double num2 = 0.0;
		if (IsLeftToRight)
		{
			if (num < Metrics.FirstCluster)
			{
				return 0.0;
			}
			if (num > Metrics.LastCluster)
			{
				return Bounds.Width;
			}
			int i = FindGlyphIndex(num);
			int glyphCluster = _glyphInfos[i].GlyphCluster;
			if (glyphCluster < num)
			{
				for (; i < _glyphInfos.Count && _glyphInfos[i].GlyphCluster <= num; i++)
				{
				}
				flag = false;
			}
			if (flag)
			{
				for (; i + 1 < _glyphInfos.Count && _glyphInfos[i + 1].GlyphCluster == glyphCluster; i++)
				{
				}
			}
			for (int j = 0; j < i; j++)
			{
				num2 += _glyphInfos[j].GlyphAdvance;
			}
			return num2;
		}
		int num3 = FindGlyphIndex(num);
		if (num > Metrics.LastCluster)
		{
			return 0.0;
		}
		if (num <= Metrics.FirstCluster)
		{
			return Bounds.Width;
		}
		for (int k = num3 + 1; k < _glyphInfos.Count; k++)
		{
			num2 += _glyphInfos[k].GlyphAdvance;
		}
		return Bounds.Width - num2;
	}

	public CharacterHit GetCharacterHitFromDistance(double P_0, out bool P_1)
	{
		double num;
		if (P_0 <= 0.0)
		{
			P_1 = false;
			CharacterHit result = FindNearestCharacterHit(IsLeftToRight ? Metrics.FirstCluster : Metrics.LastCluster, out num);
			if (!IsLeftToRight)
			{
				return result;
			}
			return new CharacterHit(result.FirstCharacterIndex);
		}
		if (P_0 >= Bounds.Width)
		{
			P_1 = false;
			CharacterHit result2 = FindNearestCharacterHit(IsLeftToRight ? Metrics.LastCluster : Metrics.FirstCluster, out num);
			if (!IsLeftToRight)
			{
				return new CharacterHit(result2.FirstCharacterIndex);
			}
			return result2;
		}
		int num2 = 0;
		double num3 = 0.0;
		if (IsLeftToRight)
		{
			for (int i = 0; i < _glyphInfos.Count; i++)
			{
				GlyphInfo glyphInfo = _glyphInfos[i];
				double glyphAdvance = glyphInfo.GlyphAdvance;
				num2 = glyphInfo.GlyphCluster;
				if (num3 + glyphAdvance > P_0)
				{
					break;
				}
				num3 += glyphAdvance;
			}
		}
		else
		{
			num3 = Bounds.Width;
			for (int num4 = _glyphInfos.Count - 1; num4 >= 0; num4--)
			{
				GlyphInfo glyphInfo2 = _glyphInfos[num4];
				double glyphAdvance2 = glyphInfo2.GlyphAdvance;
				num2 = glyphInfo2.GlyphCluster;
				if (num3 - glyphAdvance2 < P_0)
				{
					break;
				}
				num3 -= glyphAdvance2;
			}
		}
		P_1 = true;
		double num5;
		CharacterHit result3 = FindNearestCharacterHit(num2, out num5);
		double num6 = num5 / 2.0;
		if (!((IsLeftToRight ? Math.Round(P_0 - num3, 3) : Math.Round(num3 - P_0, 3)) > num6))
		{
			return new CharacterHit(result3.FirstCharacterIndex);
		}
		return result3;
	}

	public CharacterHit GetNextCaretCharacterHit(CharacterHit P_0)
	{
		double num;
		if (P_0.TrailingLength == 0)
		{
			P_0 = FindNearestCharacterHit(P_0.FirstCharacterIndex, out num);
			if (P_0.FirstCharacterIndex == Metrics.LastCluster)
			{
				return P_0;
			}
			return new CharacterHit(P_0.FirstCharacterIndex + P_0.TrailingLength);
		}
		return FindNearestCharacterHit(P_0.FirstCharacterIndex + P_0.TrailingLength, out num);
	}

	public CharacterHit GetPreviousCaretCharacterHit(CharacterHit P_0)
	{
		double num;
		if (P_0.TrailingLength > 0)
		{
			return new CharacterHit(FindNearestCharacterHit(P_0.FirstCharacterIndex, out num).FirstCharacterIndex);
		}
		return new CharacterHit(FindNearestCharacterHit(P_0.FirstCharacterIndex - 1, out num).FirstCharacterIndex);
	}

	public int FindGlyphIndex(int P_0)
	{
		if (_hasOneCharPerCluster)
		{
			return P_0;
		}
		if (P_0 > Metrics.LastCluster)
		{
			if (IsLeftToRight)
			{
				return _glyphInfos.Count - 1;
			}
			return 0;
		}
		if (P_0 < Metrics.FirstCluster)
		{
			if (IsLeftToRight)
			{
				return 0;
			}
			return _glyphInfos.Count - 1;
		}
		Comparer<GlyphInfo> comparer = (IsLeftToRight ? GlyphInfo.ClusterAscendingComparer : GlyphInfo.ClusterDescendingComparer);
		int i = _glyphInfos.BinarySearch(new GlyphInfo(0, P_0, 0.0), comparer);
		if (i < 0)
		{
			while (P_0 > 0 && i < 0)
			{
				P_0--;
				i = _glyphInfos.BinarySearch(new GlyphInfo(0, P_0, 0.0), comparer);
			}
			if (i < 0)
			{
				return 0;
			}
		}
		if (IsLeftToRight)
		{
			while (i > 0 && _glyphInfos[i - 1].GlyphCluster == _glyphInfos[i].GlyphCluster)
			{
				i--;
			}
		}
		else
		{
			for (; i + 1 < _glyphInfos.Count && _glyphInfos[i + 1].GlyphCluster == _glyphInfos[i].GlyphCluster; i++)
			{
			}
		}
		if (i < 0)
		{
			return 0;
		}
		if (i > _glyphInfos.Count - 1)
		{
			return _glyphInfos.Count - 1;
		}
		return i;
	}

	public CharacterHit FindNearestCharacterHit(int P_0, out double P_1)
	{
		P_1 = 0.0;
		int num = FindGlyphIndex(P_0);
		if (_hasOneCharPerCluster)
		{
			P_1 = _glyphInfos[P_0].GlyphAdvance;
			return new CharacterHit(num, 1);
		}
		int glyphCluster = _glyphInfos[num].GlyphCluster;
		int num2 = glyphCluster;
		int num3 = num;
		while (num2 == glyphCluster)
		{
			P_1 += _glyphInfos[num3].GlyphAdvance;
			if (IsLeftToRight)
			{
				num3++;
				if (num3 == _glyphInfos.Count)
				{
					break;
				}
			}
			else
			{
				num3--;
				if (num3 < 0)
				{
					break;
				}
			}
			if (_glyphInfos.Count > 0 && num3 <= _glyphInfos.Count)
			{
				num2 = _glyphInfos[num3].GlyphCluster;
			}
		}
		int num4 = Math.Max(0, num2 - glyphCluster);
		if (glyphCluster == Metrics.LastCluster && num4 == 0)
		{
			int num5 = 0;
			int num6 = Metrics.FirstCluster;
			if (IsLeftToRight)
			{
				for (int i = 1; i < _glyphInfos.Count; i++)
				{
					num2 = _glyphInfos[i].GlyphCluster;
					if (num6 > glyphCluster)
					{
						break;
					}
					int num7 = num2 - num6;
					num5 += num7;
					num6 = num2;
				}
			}
			else
			{
				for (int num8 = _glyphInfos.Count - 1; num8 >= 0; num8--)
				{
					num2 = _glyphInfos[num8].GlyphCluster;
					if (num6 > glyphCluster)
					{
						break;
					}
					int num9 = num2 - num6;
					num5 += num9;
					num6 = num2;
				}
			}
			num4 = (Characters.IsEmpty ? 1 : (Characters.Length - num5));
		}
		return new CharacterHit(glyphCluster, num4);
	}

	private GlyphRunMetrics CreateGlyphRunMetrics()
	{
		int num;
		int num2;
		if (Characters.IsEmpty || _glyphInfos.Count == 0)
		{
			num = 0;
			num2 = 0;
		}
		else
		{
			num = _glyphInfos[0].GlyphCluster;
			num2 = _glyphInfos[_glyphInfos.Count - 1].GlyphCluster;
		}
		bool flag = num > num2;
		if (!IsLeftToRight)
		{
			int num3 = num;
			num = num2;
			num2 = num3;
		}
		double height = (double)GlyphTypeface.Metrics.LineSpacing * Scale;
		double num4 = 0.0;
		int newLineLength;
		int num5;
		int trailingWhitespaceLength = GetTrailingWhitespaceLength(flag, out newLineLength, out num5);
		for (int i = 0; i < _glyphInfos.Count; i++)
		{
			double glyphAdvance = _glyphInfos[i].GlyphAdvance;
			num4 += glyphAdvance;
		}
		double num6 = num4;
		if (flag)
		{
			for (int j = 0; j < num5; j++)
			{
				num6 -= _glyphInfos[j].GlyphAdvance;
			}
		}
		else
		{
			for (int k = _glyphInfos.Count - num5; k < _glyphInfos.Count; k++)
			{
				num6 -= _glyphInfos[k].GlyphAdvance;
			}
		}
		double num7 = (double)GlyphTypeface.Metrics.Ascent * Scale;
		double num8 = (double)GlyphTypeface.Metrics.LineGap * Scale;
		double baseline = 0.0 - num7 + num8 * 0.5;
		return new GlyphRunMetrics
		{
			Baseline = baseline,
			Width = num6,
			WidthIncludingTrailingWhitespace = num4,
			Height = height,
			NewLineLength = newLineLength,
			TrailingWhitespaceLength = trailingWhitespaceLength,
			FirstCluster = num,
			LastCluster = num2
		};
	}

	private int GetTrailingWhitespaceLength(bool P_0, out int P_1, out int P_2)
	{
		if (P_0)
		{
			return GetTrailingWhitespaceLengthRightToLeft(out P_1, out P_2);
		}
		P_2 = 0;
		P_1 = 0;
		int num = 0;
		ReadOnlySpan<char> span = _characters.Span;
		if (!span.IsEmpty)
		{
			int num2 = span.Length - 1;
			for (int num3 = _glyphInfos.Count - 1; num3 >= 0; num3--)
			{
				int glyphCluster = _glyphInfos[num3].GlyphCluster;
				int num4;
				Codepoint codepoint = Codepoint.ReadAt(span, num2, out num4);
				num2 -= num4;
				if (!codepoint.IsWhiteSpace)
				{
					break;
				}
				int num5 = 1;
				while (num3 - 1 >= 0)
				{
					int glyphCluster2 = _glyphInfos[num3 - 1].GlyphCluster;
					if (glyphCluster != glyphCluster2)
					{
						break;
					}
					num5++;
					num3--;
					if (num2 >= 0)
					{
						codepoint = Codepoint.ReadAt(span, num2, out num4);
						num2 -= num4;
					}
				}
				if (codepoint.IsBreakChar)
				{
					P_1 += num5;
				}
				num += num5;
				P_2++;
			}
		}
		return num;
	}

	private int GetTrailingWhitespaceLengthRightToLeft(out int P_0, out int P_1)
	{
		P_1 = 0;
		P_0 = 0;
		int num = 0;
		ReadOnlySpan<char> span = Characters.Span;
		if (!span.IsEmpty)
		{
			int num2 = span.Length - 1;
			for (int i = 0; i < _glyphInfos.Count; i++)
			{
				int glyphCluster = _glyphInfos[i].GlyphCluster;
				int num3;
				Codepoint codepoint = Codepoint.ReadAt(span, num2, out num3);
				if (!codepoint.IsWhiteSpace)
				{
					break;
				}
				int num4 = 1;
				int num5 = i;
				while (num5 + 1 < _glyphInfos.Count)
				{
					int glyphCluster2 = _glyphInfos[++num5].GlyphCluster;
					if (glyphCluster != glyphCluster2)
					{
						break;
					}
					num4++;
				}
				num2 -= num4;
				if (codepoint.IsBreakChar)
				{
					P_0 += num4;
				}
				num += num4;
				P_1 += num4;
			}
		}
		return num;
	}

	private void Set<T>(ref T P_0, T P_1)
	{
		_platformImpl?.Dispose();
		_platformImpl = null;
		_glyphRunMetrics = null;
		P_0 = P_1;
	}

	private IRef<IGlyphRunImpl> CreateGlyphRunImpl()
	{
		IGlyphRunImpl glyphRunImpl = s_renderInterface.CreateGlyphRun(GlyphTypeface, FontRenderingEmSize, GlyphInfos, BaselineOrigin);
		_platformImpl = RefCountable.Create(glyphRunImpl);
		return _platformImpl;
	}

	public void Dispose()
	{
		_platformImpl?.Dispose();
		_platformImpl = null;
	}

	public IReadOnlyList<float> GetIntersections(float P_0, float P_1)
	{
		return PlatformImpl.Item.GetIntersections(P_0, P_1);
	}
}

