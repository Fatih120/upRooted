// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.TextShaperImpl
using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Globalization;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;
using Avalonia.Media.TextFormatting.Unicode;
using Avalonia.Platform;
using Avalonia.Skia;
using HarfBuzzSharp;

internal class TextShaperImpl : ITextShaperImpl
{
	[ThreadStatic]
	private static HarfBuzzSharp.Buffer? s_buffer;

	private static readonly ConcurrentDictionary<int, Language> s_cachedLanguage = new ConcurrentDictionary<int, Language>();

	public ShapedBuffer ShapeText(ReadOnlyMemory<char> P_0, TextShaperOptions P_1)
	{
		_ = P_0.Span;
		IGlyphTypeface typeface = P_1.Typeface;
		double fontRenderingEmSize = P_1.FontRenderingEmSize;
		sbyte bidiLevel = P_1.BidiLevel;
		CultureInfo cultureInfo = P_1.Culture;
		HarfBuzzSharp.Buffer buffer = s_buffer ?? (s_buffer = new HarfBuzzSharp.Buffer());
		buffer.Reset();
		int num;
		int num2;
		ReadOnlySpan<char> span = GetContainingMemory(P_0, out num, out num2).Span;
		buffer.AddUtf16(span, num, num2);
		MergeBreakPair(buffer);
		buffer.GuessSegmentProperties();
		buffer.Direction = (((bidiLevel & 1) == 0) ? Direction.LeftToRight : Direction.RightToLeft);
		if (cultureInfo == null)
		{
			cultureInfo = CultureInfo.CurrentCulture;
		}
		CultureInfo cultureInfo2 = cultureInfo;
		buffer.Language = s_cachedLanguage.GetOrAdd(cultureInfo2.LCID, (int _, CultureInfo culture) => new Language(culture), cultureInfo2);
		Font font = ((GlyphTypefaceImpl)typeface).Font;
		font.Shape(buffer, GetFeatures(P_1));
		if (buffer.Direction == Direction.RightToLeft)
		{
			buffer.Reverse();
		}
		font.GetScale(out var num3, out var _);
		double num5 = fontRenderingEmSize / (double)num3;
		int length = buffer.Length;
		ShapedBuffer shapedBuffer = new ShapedBuffer(P_0, length, typeface, fontRenderingEmSize, bidiLevel);
		ReadOnlySpan<HarfBuzzSharp.GlyphInfo> glyphInfoSpan = buffer.GetGlyphInfoSpan();
		ReadOnlySpan<GlyphPosition> glyphPositionSpan = buffer.GetGlyphPositionSpan();
		for (int num6 = 0; num6 < length; num6++)
		{
			HarfBuzzSharp.GlyphInfo glyphInfo = glyphInfoSpan[num6];
			ushort num7 = (ushort)glyphInfo.Codepoint;
			int cluster = (int)glyphInfo.Cluster;
			double num8 = GetGlyphAdvance(glyphPositionSpan, num6, num5) + P_1.LetterSpacing;
			Vector glyphOffset = GetGlyphOffset(glyphPositionSpan, num6, num5);
			if (cluster < span.Length && span[cluster] == '\t')
			{
				num7 = typeface.GetGlyph(32u);
				num8 = ((P_1.IncrementalTabWidth > 0.0) ? P_1.IncrementalTabWidth : ((double)(4 * typeface.GetGlyphAdvance(num7)) * num5));
			}
			shapedBuffer[num6] = new Avalonia.Media.TextFormatting.GlyphInfo(num7, cluster, num8, glyphOffset);
		}
		return shapedBuffer;
	}

	private unsafe static void MergeBreakPair(HarfBuzzSharp.Buffer P_0)
	{
		int length = P_0.Length;
		ReadOnlySpan<HarfBuzzSharp.GlyphInfo> glyphInfoSpan = P_0.GetGlyphInfoSpan();
		HarfBuzzSharp.GlyphInfo glyphInfo = glyphInfoSpan[length - 1];
		if (!new Codepoint(glyphInfo.Codepoint).IsBreakChar)
		{
			return;
		}
		if (length > 1 && glyphInfoSpan[length - 2].Codepoint == 13 && glyphInfo.Codepoint == 10)
		{
			HarfBuzzSharp.GlyphInfo glyphInfo2 = glyphInfoSpan[length - 2];
			glyphInfo2.Codepoint = 8204u;
			glyphInfo.Codepoint = 8204u;
			glyphInfo.Cluster = glyphInfo2.Cluster;
			fixed (HarfBuzzSharp.GlyphInfo* ptr = &glyphInfoSpan[length - 2])
			{
				*ptr = glyphInfo2;
			}
			fixed (HarfBuzzSharp.GlyphInfo* ptr = &glyphInfoSpan[length - 1])
			{
				*ptr = glyphInfo;
			}
		}
		else
		{
			glyphInfo.Codepoint = 8204u;
			fixed (HarfBuzzSharp.GlyphInfo* ptr = &glyphInfoSpan[length - 1])
			{
				*ptr = glyphInfo;
			}
		}
	}

	private static Vector GetGlyphOffset(ReadOnlySpan<GlyphPosition> P_0, int P_1, double P_2)
	{
		GlyphPosition glyphPosition = P_0[P_1];
		double num = (double)glyphPosition.XOffset * P_2;
		double num2 = (double)(-glyphPosition.YOffset) * P_2;
		return new Vector(num, num2);
	}

	private static double GetGlyphAdvance(ReadOnlySpan<GlyphPosition> P_0, int P_1, double P_2)
	{
		return (double)P_0[P_1].XAdvance * P_2;
	}

	private static ReadOnlyMemory<char> GetContainingMemory(ReadOnlyMemory<char> P_0, out int P_1, out int P_2)
	{
		if (MemoryMarshal.TryGetString(P_0, out string text, out P_1, out P_2))
		{
			return text.AsMemory();
		}
		if (MemoryMarshal.TryGetArray(P_0, out var arraySegment))
		{
			P_1 = arraySegment.Offset;
			P_2 = arraySegment.Count;
			return arraySegment.Array.AsMemory();
		}
		if (MemoryMarshal.TryGetMemoryManager<char, MemoryManager<char>>(P_0, out MemoryManager<char> memoryManager, out P_1, out P_2))
		{
			return memoryManager.Memory;
		}
		throw new InvalidOperationException("Memory not backed by string, array or manager");
	}

	private static Feature[] GetFeatures(TextShaperOptions P_0)
	{
		if (P_0.FontFeatures == null || P_0.FontFeatures.Count == 0)
		{
			return Array.Empty<Feature>();
		}
		Feature[] array = new Feature[P_0.FontFeatures.Count];
		for (int i = 0; i < P_0.FontFeatures.Count; i++)
		{
			FontFeature fontFeature = P_0.FontFeatures[i];
			array[i] = new Feature(Tag.Parse(fontFeature.Tag), (uint)fontFeature.Value, (uint)fontFeature.Start, (uint)fontFeature.End);
		}
		return array;
	}
}

