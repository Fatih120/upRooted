// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.GlyphRunImpl
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Threading;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

internal class GlyphRunImpl : IGlyphRunImpl, IDisposable
{
	private readonly GlyphTypefaceImpl _glyphTypefaceImpl;

	private readonly ushort[] _glyphIndices;

	private readonly SKPoint[] _glyphPositions;

	private readonly SKTextBlob?[] _textBlobCache = new SKTextBlob[3];

	public double FontRenderingEmSize { get; }

	public Point BaselineOrigin { get; }

	public Rect Bounds { get; }

	public GlyphRunImpl(IGlyphTypeface P_0, double P_1, IReadOnlyList<GlyphInfo> P_2, Point P_3)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("glyphTypeface");
		}
		if (P_2 == null)
		{
			throw new ArgumentNullException("glyphInfos");
		}
		_glyphTypefaceImpl = (GlyphTypefaceImpl)P_0;
		FontRenderingEmSize = P_1;
		int count = P_2.Count;
		_glyphIndices = new ushort[count];
		_glyphPositions = new SKPoint[count];
		double num = 0.0;
		for (int i = 0; i < count; i++)
		{
			GlyphInfo glyphInfo = P_2[i];
			Vector glyphOffset = glyphInfo.GlyphOffset;
			_glyphIndices[i] = glyphInfo.GlyphIndex;
			_glyphPositions[i] = new SKPoint((float)(num + glyphOffset.X), (float)glyphOffset.Y);
			num += P_2[i].GlyphAdvance;
		}
		using SKFont sKFont = CreateFont(SKFontEdging.SubpixelAntialias);
		Rect rect = default(Rect);
		SKRect[] array = ArrayPool<SKRect>.Shared.Rent(count);
		sKFont.GetGlyphWidths(_glyphIndices, null, array.AsSpan(0, count));
		num = 0.0;
		for (int j = 0; j < count; j++)
		{
			SKRect sKRect = array[j];
			double glyphAdvance = P_2[j].GlyphAdvance;
			rect = rect.Union(new Rect(num + (double)sKRect.Left, sKRect.Top, sKRect.Width, sKRect.Height));
			num += glyphAdvance;
		}
		ArrayPool<SKRect>.Shared.Return(array);
		BaselineOrigin = P_3;
		Bounds = rect.Translate(new Vector(P_3.X, P_3.Y));
	}

	public SKTextBlob GetTextBlob(RenderOptions P_0)
	{
		SKFontEdging sKFontEdging = SKFontEdging.SubpixelAntialias;
		switch (P_0.TextRenderingMode)
		{
		case TextRenderingMode.Alias:
			sKFontEdging = SKFontEdging.Alias;
			break;
		case TextRenderingMode.Antialias:
			sKFontEdging = SKFontEdging.Antialias;
			break;
		case TextRenderingMode.Unspecified:
			sKFontEdging = ((P_0.EdgeMode != EdgeMode.Aliased) ? SKFontEdging.SubpixelAntialias : SKFontEdging.Alias);
			break;
		}
		if (_textBlobCache[(int)sKFontEdging] == null)
		{
			using SKFont sKFont = CreateFont(sKFontEdging);
			SKTextBlobBuilder sKTextBlobBuilder = SKCacheBase<SKTextBlobBuilder, SKTextBlobBuilderCache>.Shared.Get();
			SKPositionedRunBuffer sKPositionedRunBuffer = sKTextBlobBuilder.AllocatePositionedRun(sKFont, _glyphIndices.Length);
			sKPositionedRunBuffer.SetPositions(_glyphPositions);
			sKPositionedRunBuffer.SetGlyphs(_glyphIndices);
			SKTextBlob sKTextBlob = sKTextBlobBuilder.Build();
			SKCacheBase<SKTextBlobBuilder, SKTextBlobBuilderCache>.Shared.Return(sKTextBlobBuilder);
			Interlocked.CompareExchange(ref _textBlobCache[(int)sKFontEdging], sKTextBlob, null);
		}
		return _textBlobCache[(int)sKFontEdging];
	}

	private SKFont CreateFont(SKFontEdging P_0)
	{
		SKFont sKFont = _glyphTypefaceImpl.CreateSKFont((float)FontRenderingEmSize);
		sKFont.Hinting = SKFontHinting.Full;
		sKFont.Subpixel = P_0 != SKFontEdging.Alias;
		sKFont.Edging = P_0;
		return sKFont;
	}

	public void Dispose()
	{
		SKTextBlob[] textBlobCache = _textBlobCache;
		for (int i = 0; i < textBlobCache.Length; i++)
		{
			textBlobCache[i]?.Dispose();
		}
	}

	public IReadOnlyList<float> GetIntersections(float P_0, float P_1)
	{
		return GetTextBlob(default(RenderOptions)).GetIntercepts(P_0, P_1);
	}
}

