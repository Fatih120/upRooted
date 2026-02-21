// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.PlatformRenderInterface
using System;
using System.Collections.Generic;
using System.IO;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Media.TextFormatting;
using Avalonia.Metal;
using Avalonia.OpenGL;
using Avalonia.Platform;
using Avalonia.Skia;
using Avalonia.Skia.Metal;
using Avalonia.Skia.Vulkan;
using Avalonia.Vulkan;
using SkiaSharp;

internal class PlatformRenderInterface : IPlatformRenderInterface
{
	private readonly long? _maxResourceBytes;

	public bool SupportsIndividualRoundRects => true;

	public AlphaFormat DefaultAlphaFormat => AlphaFormat.Premul;

	public PixelFormat DefaultPixelFormat { get; }

	public bool SupportsRegions => true;

	public PlatformRenderInterface(long? P_0 = null)
	{
		_maxResourceBytes = P_0;
		DefaultPixelFormat = SKImageInfo.PlatformColorType.ToPixelFormat();
	}

	public IPlatformRenderInterfaceContext CreateBackendContext(IPlatformGraphicsContext? P_0)
	{
		if (P_0 == null)
		{
			return new SkiaContext(null);
		}
		if (P_0 is ISkiaGpu skiaGpu)
		{
			return new SkiaContext(skiaGpu);
		}
		if (P_0 is IGlContext glContext)
		{
			return new SkiaContext(new GlSkiaGpu(glContext, _maxResourceBytes));
		}
		if (P_0 is IMetalDevice metalDevice)
		{
			return new SkiaContext(new SkiaMetalGpu(metalDevice, _maxResourceBytes));
		}
		if (P_0 is IVulkanPlatformGraphicsContext vulkanPlatformGraphicsContext)
		{
			return new SkiaContext(new VulkanSkiaGpu(vulkanPlatformGraphicsContext, _maxResourceBytes));
		}
		throw new ArgumentException("Graphics context of type is not supported");
	}

	public bool IsSupportedBitmapPixelFormat(PixelFormat P_0)
	{
		if (!(P_0 == PixelFormats.Rgb565) && !(P_0 == PixelFormats.Bgra8888))
		{
			return P_0 == PixelFormats.Rgba8888;
		}
		return true;
	}

	public IPlatformRenderInterfaceRegion CreateRegion()
	{
		return new SkiaRegionImpl();
	}

	public IGeometryImpl CreateEllipseGeometry(Rect P_0)
	{
		return new EllipseGeometryImpl(P_0);
	}

	public IGeometryImpl CreateLineGeometry(Point P_0, Point P_1)
	{
		return new LineGeometryImpl(P_0, P_1);
	}

	public IGeometryImpl CreateRectangleGeometry(Rect P_0)
	{
		return new RectangleGeometryImpl(P_0);
	}

	public IStreamGeometryImpl CreateStreamGeometry()
	{
		return new StreamGeometryImpl();
	}

	public IGeometryImpl CreateGeometryGroup(FillRule P_0, IReadOnlyList<IGeometryImpl> P_1)
	{
		return new GeometryGroupImpl(P_0, P_1);
	}

	public IGeometryImpl CreateCombinedGeometry(GeometryCombineMode P_0, IGeometryImpl P_1, IGeometryImpl P_2)
	{
		return CombinedGeometryImpl.ForceCreate(P_0, P_1, P_2);
	}

	public IBitmapImpl LoadBitmap(string P_0)
	{
		using FileStream fileStream = File.OpenRead(P_0);
		return LoadBitmap(fileStream);
	}

	public IBitmapImpl LoadBitmap(Stream P_0)
	{
		return new ImmutableBitmap(P_0);
	}

	public IBitmapImpl LoadBitmap(PixelFormat P_0, AlphaFormat P_1, nint P_2, PixelSize P_3, Vector P_4, int P_5)
	{
		return new ImmutableBitmap(P_3, P_4, P_5, P_0, P_1, P_2);
	}

	public IBitmapImpl LoadBitmapToWidth(Stream P_0, int P_1, BitmapInterpolationMode P_2 = BitmapInterpolationMode.HighQuality)
	{
		return new ImmutableBitmap(P_0, P_1, true, P_2);
	}

	public IBitmapImpl LoadBitmapToHeight(Stream P_0, int P_1, BitmapInterpolationMode P_2 = BitmapInterpolationMode.HighQuality)
	{
		return new ImmutableBitmap(P_0, P_1, false, P_2);
	}

	public IBitmapImpl ResizeBitmap(IBitmapImpl P_0, PixelSize P_1, BitmapInterpolationMode P_2 = BitmapInterpolationMode.HighQuality)
	{
		if (P_0 is ImmutableBitmap immutableBitmap)
		{
			return new ImmutableBitmap(immutableBitmap, P_1, P_2);
		}
		throw new Exception("Invalid source bitmap type.");
	}

	public IRenderTargetBitmapImpl CreateRenderTargetBitmap(PixelSize P_0, Vector P_1)
	{
		if (P_0.Width < 1)
		{
			throw new ArgumentException("Width can't be less than 1", "size");
		}
		if (P_0.Height < 1)
		{
			throw new ArgumentException("Height can't be less than 1", "size");
		}
		return new RenderTargetBitmapImpl(P_0, P_1);
	}

	public IWriteableBitmapImpl CreateWriteableBitmap(PixelSize P_0, Vector P_1, PixelFormat P_2, AlphaFormat P_3)
	{
		return new WriteableBitmapImpl(P_0, P_1, P_2, P_3);
	}

	public IGlyphRunImpl CreateGlyphRun(IGlyphTypeface P_0, double P_1, IReadOnlyList<GlyphInfo> P_2, Point P_3)
	{
		return new GlyphRunImpl(P_0, P_1, P_2, P_3);
	}
}

