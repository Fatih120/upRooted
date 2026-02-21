// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.CombinedGeometryImpl
using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

internal class CombinedGeometryImpl : GeometryImpl
{
	public override Rect Bounds { get; }

	public override SKPath? StrokePath { get; }

	public override SKPath? FillPath { get; }

	public CombinedGeometryImpl(SKPath? P_0, SKPath? P_1)
	{
		StrokePath = P_0;
		FillPath = P_1;
		SKRect sKRect = P_0?.TightBounds ?? default(SKRect);
		if (P_1 != null)
		{
			sKRect.Union(P_1.TightBounds);
		}
		Bounds = sKRect.ToAvaloniaRect();
	}

	public static CombinedGeometryImpl ForceCreate(GeometryCombineMode P_0, IGeometryImpl P_1, IGeometryImpl P_2)
	{
		if (P_1 is GeometryImpl geometryImpl && P_2 is GeometryImpl geometryImpl2)
		{
			CombinedGeometryImpl combinedGeometryImpl = TryCreate(P_0, geometryImpl, geometryImpl2);
			if (combinedGeometryImpl != null)
			{
				return combinedGeometryImpl;
			}
		}
		return new CombinedGeometryImpl(null, null);
	}

	public static CombinedGeometryImpl? TryCreate(GeometryCombineMode P_0, GeometryImpl P_1, GeometryImpl P_2)
	{
		SKPathOp sKPathOp = P_0 switch
		{
			GeometryCombineMode.Intersect => SKPathOp.Intersect, 
			GeometryCombineMode.Xor => SKPathOp.Xor, 
			GeometryCombineMode.Exclude => SKPathOp.Difference, 
			_ => SKPathOp.Union, 
		};
		SKPath sKPath = ((P_1.StrokePath != null && P_2.StrokePath != null) ? P_1.StrokePath.Op(P_2.StrokePath, sKPathOp) : null);
		SKPath sKPath2 = null;
		if (P_1.FillPath != null && P_2.FillPath != null)
		{
			sKPath2 = ((P_1.FillPath != P_1.StrokePath || P_2.FillPath != P_2.StrokePath) ? P_1.FillPath.Op(P_2.FillPath, sKPathOp) : sKPath);
		}
		if (sKPath == null && sKPath2 == null)
		{
			return null;
		}
		return new CombinedGeometryImpl(sKPath, sKPath2);
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.DrawingContextImpl
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.Utilities;
using Avalonia.Skia;
using Avalonia.Skia.Helpers;
using Avalonia.Utilities;
using SkiaSharp;

internal class DrawingContextImpl : IDrawingContextImpl, IDisposable, IDrawingContextImplWithEffects
{
	public struct CreateInfo
	{
		public SKCanvas? Canvas;

		public SKSurface? Surface;

		public bool ScaleDrawingToDpi;

		public Vector Dpi;

		public bool DisableSubpixelTextRendering;

		public GRContext? GrContext;

		public ISkiaGpu? Gpu;

		public ISkiaGpuRenderSession? CurrentSession;
	}

	private class SkiaLeaseFeature : ISkiaSharpApiLeaseFeature
	{
		private class ApiLease : ISkiaSharpApiLease, IDisposable
		{
			private class PlatformApiLease : ISkiaSharpPlatformGraphicsApiLease, IDisposable
			{
				private readonly ApiLease _parent;

				public IPlatformGraphicsContext Context { get; }

				public PlatformApiLease(ApiLease P_0, IPlatformGraphicsContext P_1)
				{
					_parent = P_0;
					_parent.GrContext?.Flush();
					Context = P_1;
					_parent._leased = true;
				}

				public void Dispose()
				{
					_parent._leased = false;
					_parent.GrContext?.ResetContext();
				}
			}

			private readonly DrawingContextImpl _context;

			private readonly SKMatrix _revertTransform;

			private bool _isDisposed;

			private bool _leased;

			public SKCanvas SkCanvas => CheckLease(_context.Canvas);

			public GRContext? GrContext => _context.GrContext;

			public ApiLease(DrawingContextImpl P_0)
			{
				_revertTransform = P_0.Canvas.TotalMatrix;
				_context = P_0;
				_context._leased = true;
			}

			private void CheckLease()
			{
				if (_leased)
				{
					throw new InvalidOperationException("The underlying graphics API is currently leased");
				}
			}

			private T CheckLease<T>(T P_0)
			{
				CheckLease();
				return P_0;
			}

			public void Dispose()
			{
				if (!_isDisposed)
				{
					_context.Canvas.SetMatrix(_revertTransform);
					_context._leased = false;
					_isDisposed = true;
				}
			}

			public ISkiaSharpPlatformGraphicsApiLease? TryLeasePlatformGraphicsApi()
			{
				CheckLease();
				if (_context._gpu is ISkiaGpuWithPlatformGraphicsContext skiaGpuWithPlatformGraphicsContext)
				{
					IPlatformGraphicsContext platformGraphicsContext = skiaGpuWithPlatformGraphicsContext.PlatformGraphicsContext;
					if (platformGraphicsContext != null)
					{
						return new PlatformApiLease(this, platformGraphicsContext);
					}
				}
				return null;
			}
		}

		private readonly DrawingContextImpl _context;

		public SkiaLeaseFeature(DrawingContextImpl P_0)
		{
			_context = P_0;
		}

		public ISkiaSharpApiLease Lease()
		{
			_context.CheckLease();
			return new ApiLease(_context);
		}
	}

	private struct BoxShadowFilter(SKPaint P_0, SKImageFilter? P_1, SKClipOperation P_2) : IDisposable
	{
		public readonly SKPaint Paint = P_0;

		private readonly SKImageFilter? _filter = P_1;

		public readonly SKClipOperation ClipOperation = P_2;

		public static BoxShadowFilter Create(SKPaint P_0, BoxShadow P_1, double P_2)
		{
			Color color = P_1.Color;
			SKImageFilter sKImageFilter = SKImageFilter.CreateBlur(SkBlurRadiusToSigma(P_1.Blur), SkBlurRadiusToSigma(P_1.Blur));
			SKColor color2 = new SKColor(color.R, color.G, color.B, (byte)((double)(int)color.A * P_2));
			P_0.Reset();
			P_0.IsAntialias = true;
			P_0.Color = color2;
			P_0.ImageFilter = sKImageFilter;
			SKClipOperation sKClipOperation = (P_1.IsInset ? SKClipOperation.Intersect : SKClipOperation.Difference);
			return new BoxShadowFilter(P_0, sKImageFilter, sKClipOperation);
		}

		public void Dispose()
		{
			Paint?.Reset();
			_filter?.Dispose();
		}
	}

	internal struct PaintWrapper(SKPaint P_0) : IDisposable
	{
		public readonly SKPaint Paint = P_0;

		private IDisposable? _disposable1 = null;

		private IDisposable? _disposable2 = null;

		private IDisposable? _disposable3 = null;

		public void AddDisposable(IDisposable P_0)
		{
			if (_disposable1 == null)
			{
				_disposable1 = P_0;
				return;
			}
			if (_disposable2 == null)
			{
				_disposable2 = P_0;
				return;
			}
			if (_disposable3 == null)
			{
				_disposable3 = P_0;
				return;
			}
			throw new InvalidOperationException("PaintWrapper disposable object limit reached. You need to add extra struct fields to support more disposables.");
		}

		public void Dispose()
		{
			Paint?.Reset();
			_disposable1?.Dispose();
			_disposable2?.Dispose();
			_disposable3?.Dispose();
		}
	}

	private IDisposable?[]? _disposables;

	private readonly Vector _intermediateSurfaceDpi;

	private readonly Stack<(SKMatrix matrix, PaintWrapper paint)> _maskStack = new Stack<(SKMatrix, PaintWrapper)>();

	private readonly Stack<double> _opacityStack = new Stack<double>();

	private readonly Stack<RenderOptions> _renderOptionsStack = new Stack<RenderOptions>();

	private readonly Matrix? _postTransform;

	private double _currentOpacity = 1.0;

	private readonly bool _disableSubpixelTextRendering;

	private Matrix? _currentTransform;

	private bool _disposed;

	private GRContext? _grContext;

	private readonly ISkiaGpu? _gpu;

	private readonly SKPaint _strokePaint = SKCacheBase<SKPaint, SKPaintCache>.Shared.Get();

	private readonly SKPaint _fillPaint = SKCacheBase<SKPaint, SKPaintCache>.Shared.Get();

	private readonly SKPaint _boxShadowPaint = SKCacheBase<SKPaint, SKPaintCache>.Shared.Get();

	private readonly ISkiaGpuRenderSession? _session;

	private bool _leased;

	private bool _useOpacitySaveLayer;

	[CompilerGenerated]
	private readonly SKSurface? _003CSurface_003Ek__BackingField;

	[CompilerGenerated]
	private RenderOptions _003CRenderOptions_003Ek__BackingField;

	public GRContext? GrContext => _grContext;

	public SKCanvas Canvas { get; }

	public RenderOptions RenderOptions
	{
		[CompilerGenerated]
		get
		{
			return _003CRenderOptions_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CRenderOptions_003Ek__BackingField = renderOptions;
		}
	}

	public Matrix Transform
	{
		get
		{
			Matrix valueOrDefault = _currentTransform.GetValueOrDefault();
			if (!_currentTransform.HasValue)
			{
				valueOrDefault = Canvas.TotalMatrix.ToAvaloniaMatrix();
				_currentTransform = valueOrDefault;
				return valueOrDefault;
			}
			return valueOrDefault;
		}
		set
		{
			CheckLease();
			if (!(_currentTransform == matrix))
			{
				_currentTransform = matrix;
				Matrix matrix2 = matrix;
				if (_postTransform.HasValue)
				{
					matrix2 *= _postTransform.Value;
				}
				Canvas.SetMatrix(matrix2.ToSKMatrix());
			}
		}
	}

	public DrawingContextImpl(CreateInfo P_0, params IDisposable?[]? P_1)
	{
		Canvas = P_0.Canvas ?? P_0.Surface?.Canvas ?? throw new ArgumentException("Invalid create info - no Canvas provided", "createInfo");
		_intermediateSurfaceDpi = P_0.Dpi;
		_disposables = P_1;
		_disableSubpixelTextRendering = P_0.DisableSubpixelTextRendering;
		_grContext = P_0.GrContext;
		_gpu = P_0.Gpu;
		if (_grContext != null)
		{
			Monitor.Enter(_grContext);
		}
		_003CSurface_003Ek__BackingField = P_0.Surface;
		_session = P_0.CurrentSession;
		if (P_0.ScaleDrawingToDpi && !P_0.Dpi.NearlyEquals(SkiaPlatform.DefaultDpi))
		{
			_postTransform = Matrix.CreateScale(P_0.Dpi.X / SkiaPlatform.DefaultDpi.X, P_0.Dpi.Y / SkiaPlatform.DefaultDpi.Y);
		}
		Transform = Matrix.Identity;
		SkiaOptions service = AvaloniaLocator.Current.GetService<SkiaOptions>();
		if (service != null)
		{
			_useOpacitySaveLayer = service.UseOpacitySaveLayer;
		}
	}

	private void CheckLease()
	{
		if (_leased)
		{
			throw new InvalidOperationException("The underlying graphics API is currently leased");
		}
	}

	public void Clear(Color P_0)
	{
		CheckLease();
		Canvas.Clear(P_0.ToSKColor());
	}

	public void DrawBitmap(IBitmapImpl P_0, double P_1, Rect P_2, Rect P_3)
	{
		CheckLease();
		IDrawableBitmapImpl obj = (IDrawableBitmapImpl)P_0;
		SKRect sKRect = P_2.ToSKRect();
		SKRect sKRect2 = P_3.ToSKRect();
		SKPaint sKPaint = SKCacheBase<SKPaint, SKPaintCache>.Shared.Get();
		sKPaint.Color = new SKColor(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)(255.0 * P_1 * _currentOpacity));
		sKPaint.FilterQuality = RenderOptions.BitmapInterpolationMode.ToSKFilterQuality();
		sKPaint.BlendMode = RenderOptions.BitmapBlendingMode.ToSKBlendMode();
		sKPaint.IsAntialias = RenderOptions.EdgeMode != EdgeMode.Aliased;
		obj.Draw(this, sKRect, sKRect2, sKPaint);
		SKCacheBase<SKPaint, SKPaintCache>.Shared.ReturnReset(sKPaint);
	}

	public void DrawLine(IPen? P_0, Point P_1, Point P_2)
	{
		CheckLease();
		if (P_0 == null)
		{
			return;
		}
		PaintWrapper? paintWrapper = TryCreatePaint(_strokePaint, P_0, new Rect(P_1, P_2).Normalize());
		if (paintWrapper.HasValue)
		{
			PaintWrapper valueOrDefault = paintWrapper.GetValueOrDefault();
			using (valueOrDefault)
			{
				Canvas.DrawLine((float)P_1.X, (float)P_1.Y, (float)P_2.X, (float)P_2.Y, valueOrDefault.Paint);
			}
		}
	}

	public void DrawGeometry(IBrush? P_0, IPen? P_1, IGeometryImpl P_2)
	{
		CheckLease();
		GeometryImpl geometryImpl = (GeometryImpl)P_2;
		Rect bounds = P_2.Bounds;
		if (P_0 != null && geometryImpl.FillPath != null)
		{
			PaintWrapper paintWrapper = CreatePaint(_fillPaint, P_0, bounds);
			try
			{
				Canvas.DrawPath(geometryImpl.FillPath, paintWrapper.Paint);
			}
			finally
			{
				((IDisposable)paintWrapper/*cast due to .constrained prefix*/).Dispose();
			}
		}
		if (P_1 == null || geometryImpl.StrokePath == null)
		{
			return;
		}
		PaintWrapper? paintWrapper2 = TryCreatePaint(_strokePaint, P_1, bounds.Inflate(new Thickness(P_1.Thickness / 2.0)));
		if (paintWrapper2.HasValue)
		{
			PaintWrapper valueOrDefault = paintWrapper2.GetValueOrDefault();
			using (valueOrDefault)
			{
				Canvas.DrawPath(geometryImpl.StrokePath, valueOrDefault.Paint);
			}
		}
	}

	private static float SkBlurRadiusToSigma(double P_0)
	{
		if (P_0 <= 0.0)
		{
			return 0f;
		}
		return 0.288675f * (float)P_0 + 0.5f;
	}

	private static SKRect AreaCastingShadowInHole(SKRect P_0, float P_1, float P_2, float P_3, float P_4)
	{
		SKRect sKRect = P_0;
		sKRect.Inflate(P_1, P_1);
		if (P_2 < 0f)
		{
			sKRect.Inflate(0f - P_2, 0f - P_2);
		}
		SKRect sKRect2 = sKRect;
		sKRect2.Offset(0f - P_3, 0f - P_4);
		sKRect.Union(sKRect2);
		return sKRect;
	}

	public void DrawRectangle(IBrush? P_0, IPen? P_1, RoundedRect P_2, BoxShadows P_3 = default(BoxShadows))
	{
		if (P_2.Rect.Height <= 0.0 || P_2.Rect.Width <= 0.0)
		{
			return;
		}
		CheckLease();
		if (P_2.Rect.Height > 8192.0 || P_2.Rect.Width > 8192.0)
		{
			P_3 = default(BoxShadows);
		}
		SKRect sKRect = P_2.Rect.ToSKRect();
		bool isRounded = P_2.IsRounded;
		bool num = P_2.IsRounded || P_3.HasInsetShadows;
		SKRoundRect sKRoundRect = null;
		if (num)
		{
			sKRoundRect = SKCacheBase<SKRoundRect, SKRoundRectCache>.Shared.GetAndSetRadii(in sKRect, in P_2);
		}
		BoxShadows.BoxShadowsEnumerator enumerator = P_3.GetEnumerator();
		while (enumerator.MoveNext())
		{
			BoxShadow current = enumerator.Current;
			if (!(current != default(BoxShadow)) || current.IsInset)
			{
				continue;
			}
			BoxShadowFilter boxShadowFilter = BoxShadowFilter.Create(_boxShadowPaint, current, _currentOpacity);
			try
			{
				float num2 = (float)current.Spread;
				if (current.IsInset)
				{
					num2 = 0f - num2;
				}
				Canvas.Save();
				if (isRounded)
				{
					SKRoundRect andSetRadii = SKCacheBase<SKRoundRect, SKRoundRectCache>.Shared.GetAndSetRadii(sKRoundRect.Rect, sKRoundRect.Radii);
					if (num2 != 0f)
					{
						andSetRadii.Inflate(num2, num2);
					}
					Canvas.ClipRoundRect(sKRoundRect, boxShadowFilter.ClipOperation, true);
					Matrix transform = Transform;
					Transform = transform * Matrix.CreateTranslation(current.OffsetX, current.OffsetY);
					Canvas.DrawRoundRect(andSetRadii, boxShadowFilter.Paint);
					Transform = transform;
					SKCacheBase<SKRoundRect, SKRoundRectCache>.Shared.Return(andSetRadii);
				}
				else
				{
					SKRect sKRect2 = sKRect;
					if (num2 != 0f)
					{
						sKRect2.Inflate(num2, num2);
					}
					Canvas.ClipRect(sKRect, boxShadowFilter.ClipOperation);
					Matrix transform2 = Transform;
					Transform = transform2 * Matrix.CreateTranslation(current.OffsetX, current.OffsetY);
					Canvas.DrawRect(sKRect2, boxShadowFilter.Paint);
					Transform = transform2;
				}
				RestoreCanvas();
			}
			finally
			{
				((IDisposable)boxShadowFilter/*cast due to .constrained prefix*/).Dispose();
			}
		}
		if (P_0 != null)
		{
			PaintWrapper paintWrapper = CreatePaint(_fillPaint, P_0, P_2.Rect);
			try
			{
				if (isRounded)
				{
					Canvas.DrawRoundRect(sKRoundRect, paintWrapper.Paint);
				}
				else
				{
					Canvas.DrawRect(sKRect, paintWrapper.Paint);
				}
			}
			finally
			{
				((IDisposable)paintWrapper/*cast due to .constrained prefix*/).Dispose();
			}
		}
		enumerator = P_3.GetEnumerator();
		while (enumerator.MoveNext())
		{
			BoxShadow current2 = enumerator.Current;
			if (!(current2 != default(BoxShadow)) || !current2.IsInset)
			{
				continue;
			}
			BoxShadowFilter boxShadowFilter2 = BoxShadowFilter.Create(_boxShadowPaint, current2, _currentOpacity);
			try
			{
				float num3 = (float)current2.Spread;
				float num4 = (float)current2.OffsetX;
				float num5 = (float)current2.OffsetY;
				SKRect sKRect3 = AreaCastingShadowInHole(sKRect, (float)current2.Blur, num3, num4, num5);
				Canvas.Save();
				SKRoundRect andSetRadii2 = SKCacheBase<SKRoundRect, SKRoundRectCache>.Shared.GetAndSetRadii(sKRoundRect.Rect, sKRoundRect.Radii);
				if (num3 != 0f)
				{
					andSetRadii2.Deflate(num3, num3);
				}
				Canvas.ClipRoundRect(sKRoundRect, boxShadowFilter2.ClipOperation, true);
				Matrix transform3 = Transform;
				Transform = transform3 * Matrix.CreateTranslation(current2.OffsetX, current2.OffsetY);
				using (SKRoundRect sKRoundRect2 = new SKRoundRect(sKRect3))
				{
					Canvas.DrawRoundRectDifference(sKRoundRect2, andSetRadii2, boxShadowFilter2.Paint);
				}
				Transform = transform3;
				RestoreCanvas();
				SKCacheBase<SKRoundRect, SKRoundRectCache>.Shared.Return(andSetRadii2);
			}
			finally
			{
				((IDisposable)boxShadowFilter2/*cast due to .constrained prefix*/).Dispose();
			}
		}
		if (P_1 != null)
		{
			PaintWrapper? paintWrapper2 = TryCreatePaint(_strokePaint, P_1, P_2.Rect.Inflate(new Thickness(P_1.Thickness / 2.0)));
			if (paintWrapper2.HasValue)
			{
				PaintWrapper valueOrDefault = paintWrapper2.GetValueOrDefault();
				using (valueOrDefault)
				{
					if (isRounded)
					{
						Canvas.DrawRoundRect(sKRoundRect, valueOrDefault.Paint);
					}
					else
					{
						Canvas.DrawRect(sKRect, valueOrDefault.Paint);
					}
				}
			}
		}
		if (sKRoundRect != null)
		{
			SKCacheBase<SKRoundRect, SKRoundRectCache>.Shared.Return(sKRoundRect);
		}
	}

	public void DrawRegion(IBrush? P_0, IPen? P_1, IPlatformRenderInterfaceRegion P_2)
	{
		SkiaRegionImpl skiaRegionImpl = (SkiaRegionImpl)P_2;
		if (skiaRegionImpl.IsEmpty)
		{
			return;
		}
		CheckLease();
		if (P_0 != null)
		{
			PaintWrapper paintWrapper = CreatePaint(_fillPaint, P_0, skiaRegionImpl.Bounds.ToRectUnscaled());
			try
			{
				Canvas.DrawRegion(skiaRegionImpl.Region, paintWrapper.Paint);
			}
			finally
			{
				((IDisposable)paintWrapper/*cast due to .constrained prefix*/).Dispose();
			}
		}
		if (P_1 == null)
		{
			return;
		}
		PaintWrapper? paintWrapper2 = TryCreatePaint(_strokePaint, P_1, skiaRegionImpl.Bounds.ToRectUnscaled().Inflate(new Thickness(P_1.Thickness / 2.0)));
		if (!paintWrapper2.HasValue)
		{
			return;
		}
		PaintWrapper valueOrDefault = paintWrapper2.GetValueOrDefault();
		using (valueOrDefault)
		{
			Canvas.DrawRegion(skiaRegionImpl.Region, valueOrDefault.Paint);
		}
	}

	public void DrawGlyphRun(IBrush? P_0, IGlyphRunImpl P_1)
	{
		CheckLease();
		if (P_0 == null)
		{
			return;
		}
		PaintWrapper paintWrapper = CreatePaint(_fillPaint, P_0, P_1.Bounds);
		try
		{
			GlyphRunImpl glyphRunImpl = (GlyphRunImpl)P_1;
			RenderOptions renderOptions = RenderOptions;
			if (_disableSubpixelTextRendering)
			{
				TextRenderingMode textRenderingMode = renderOptions.TextRenderingMode;
				if (textRenderingMode != TextRenderingMode.Unspecified)
				{
					if (textRenderingMode == TextRenderingMode.SubpixelAntialias)
					{
						goto IL_005b;
					}
				}
				else if (renderOptions.EdgeMode == EdgeMode.Antialias || renderOptions.EdgeMode == EdgeMode.Unspecified)
				{
					goto IL_005b;
				}
			}
			goto IL_0069;
			IL_0069:
			SKTextBlob textBlob = glyphRunImpl.GetTextBlob(renderOptions);
			Canvas.DrawText(textBlob, (float)P_1.BaselineOrigin.X, (float)P_1.BaselineOrigin.Y, paintWrapper.Paint);
			return;
			IL_005b:
			renderOptions = renderOptions with
			{
				TextRenderingMode = TextRenderingMode.Antialias
			};
			goto IL_0069;
		}
		finally
		{
			((IDisposable)paintWrapper/*cast due to .constrained prefix*/).Dispose();
		}
	}

	public IDrawingContextLayerImpl CreateLayer(PixelSize P_0)
	{
		CheckLease();
		return CreateRenderTarget(P_0, true);
	}

	public void PushClip(Rect P_0)
	{
		CheckLease();
		Canvas.Save();
		Canvas.ClipRect(P_0.ToSKRect());
	}

	public void PushClip(RoundedRect P_0)
	{
		CheckLease();
		Canvas.Save();
		SKRect sKRect = P_0.Rect.ToSKRect();
		SKRoundRect sKRoundRect = SKCacheBase<SKRoundRect, SKRoundRectCache>.Shared.Get();
		sKRoundRect.SetRectRadii(sKRect, new SKPoint[4]
		{
			P_0.RadiiTopLeft.ToSKPoint(),
			P_0.RadiiTopRight.ToSKPoint(),
			P_0.RadiiBottomRight.ToSKPoint(),
			P_0.RadiiBottomLeft.ToSKPoint()
		});
		Canvas.ClipRoundRect(sKRoundRect, SKClipOperation.Intersect, true);
		SKCacheBase<SKRoundRect, SKRoundRectCache>.Shared.Return(sKRoundRect);
	}

	public void PushClip(IPlatformRenderInterfaceRegion P_0)
	{
		SKRegion region = ((SkiaRegionImpl)P_0).Region;
		CheckLease();
		Canvas.Save();
		Canvas.ClipRegion(region);
	}

	private void RestoreCanvas()
	{
		_currentTransform = null;
		Canvas.Restore();
	}

	public void PopClip()
	{
		CheckLease();
		RestoreCanvas();
	}

	public void PushLayer(Rect P_0)
	{
		CheckLease();
		Canvas.SaveLayer(P_0.ToSKRect(), null);
	}

	public void PopLayer()
	{
		CheckLease();
		RestoreCanvas();
	}

	public void PushOpacity(double P_0, Rect? P_1)
	{
		CheckLease();
		_opacityStack.Push(_currentOpacity);
		if (_useOpacitySaveLayer || RenderOptions.RequiresFullOpacityHandling == true)
		{
			P_0 = _currentOpacity * P_0;
			_currentOpacity = 1.0;
			if (P_1.HasValue)
			{
				SKRect sKRect = P_1.Value.ToSKRect();
				Canvas.SaveLayer(sKRect, new SKPaint
				{
					ColorF = new SKColorF(0f, 0f, 0f, (float)P_0)
				});
			}
			else
			{
				Canvas.SaveLayer(new SKPaint
				{
					ColorF = new SKColorF(0f, 0f, 0f, (float)P_0)
				});
			}
		}
		else
		{
			_currentOpacity *= P_0;
		}
	}

	public void PopOpacity()
	{
		CheckLease();
		if (_useOpacitySaveLayer || RenderOptions.RequiresFullOpacityHandling == true)
		{
			RestoreCanvas();
		}
		_currentOpacity = _opacityStack.Pop();
	}

	public void PushRenderOptions(RenderOptions P_0)
	{
		CheckLease();
		_renderOptionsStack.Push(RenderOptions);
		RenderOptions = RenderOptions.MergeWith(P_0);
	}

	public void PopRenderOptions()
	{
		RenderOptions = _renderOptionsStack.Pop();
	}

	public virtual void Dispose()
	{
		if (_disposed)
		{
			return;
		}
		CheckLease();
		try
		{
			SKCacheBase<SKPaint, SKPaintCache>.Shared.ReturnReset(_strokePaint);
			SKCacheBase<SKPaint, SKPaintCache>.Shared.ReturnReset(_fillPaint);
			SKCacheBase<SKPaint, SKPaintCache>.Shared.ReturnReset(_boxShadowPaint);
			if (_grContext != null)
			{
				Monitor.Exit(_grContext);
				_grContext = null;
			}
			if (_disposables != null)
			{
				IDisposable[] disposables = _disposables;
				for (int i = 0; i < disposables.Length; i++)
				{
					disposables[i]?.Dispose();
				}
				_disposables = null;
			}
		}
		finally
		{
			_disposed = true;
		}
	}

	public void PushGeometryClip(IGeometryImpl P_0)
	{
		CheckLease();
		Canvas.Save();
		Canvas.ClipPath(((GeometryImpl)P_0).FillPath, SKClipOperation.Intersect, true);
	}

	public void PopGeometryClip()
	{
		CheckLease();
		RestoreCanvas();
	}

	public void PushOpacityMask(IBrush P_0, Rect P_1)
	{
		CheckLease();
		SKPaint sKPaint = SKCacheBase<SKPaint, SKPaintCache>.Shared.Get();
		Canvas.SaveLayer(P_1.ToSKRect(), sKPaint);
		_maskStack.Push((Canvas.TotalMatrix, CreatePaint(sKPaint, P_0, P_1)));
	}

	public void PopOpacityMask()
	{
		CheckLease();
		SKPaint sKPaint = SKCacheBase<SKPaint, SKPaintCache>.Shared.Get();
		sKPaint.BlendMode = SKBlendMode.DstIn;
		Canvas.SaveLayer(sKPaint);
		SKCacheBase<SKPaint, SKPaintCache>.Shared.ReturnReset(sKPaint);
		var (matrix, paintWrapper) = _maskStack.Pop();
		Canvas.SetMatrix(matrix);
		using (paintWrapper)
		{
			Canvas.DrawPaint(paintWrapper.Paint);
		}
		SKCacheBase<SKPaint, SKPaintCache>.Shared.Return(paintWrapper.Paint);
		RestoreCanvas();
		RestoreCanvas();
	}

	public object? GetFeature(Type P_0)
	{
		if (P_0 == typeof(ISkiaSharpApiLeaseFeature))
		{
			return new SkiaLeaseFeature(this);
		}
		return null;
	}

	private static void ConfigureGradientBrush(ref PaintWrapper P_0, Rect P_1, IGradientBrush P_2)
	{
		SKShaderTileMode sKShaderTileMode = P_2.SpreadMethod.ToSKShaderTileMode();
		SKColor[] array = P_2.GradientStops.Select((IGradientStop s) => s.Color.ToSKColor()).ToArray();
		float[] array2 = P_2.GradientStops.Select((IGradientStop s) => (float)s.Offset).ToArray();
		if (!(P_2 is ILinearGradientBrush { StartPoint: var startPoint } linearGradientBrush))
		{
			if (!(P_2 is IRadialGradientBrush { Center: var center } radialGradientBrush))
			{
				if (!(P_2 is IConicGradientBrush { Center: var center2 } conicGradientBrush))
				{
					return;
				}
				SKPoint sKPoint = center2.ToPixels(P_1).ToSKPoint();
				SKMatrix sKMatrix = SKMatrix.CreateRotationDegrees((float)(conicGradientBrush.Angle - 90.0), sKPoint.X, sKPoint.Y);
				if (conicGradientBrush.Transform != null)
				{
					Matrix matrix = Matrix.CreateTranslation(conicGradientBrush.TransformOrigin.ToPixels(P_1));
					Matrix matrix2 = -matrix * conicGradientBrush.Transform.Value * matrix;
					sKMatrix = sKMatrix.PreConcat(matrix2.ToSKMatrix());
				}
				using SKShader shader = SKShader.CreateSweepGradient(sKPoint, array, array2, sKMatrix);
				P_0.Paint.Shader = shader;
				return;
			}
			Point point = center.ToPixels(P_1);
			SKPoint sKPoint2 = point.ToSKPoint();
			double num = radialGradientBrush.RadiusX.ToValue(P_1.Width);
			double num2 = radialGradientBrush.RadiusY.ToValue(P_1.Height);
			Point point2 = radialGradientBrush.GradientOrigin.ToPixels(P_1);
			Matrix? matrix3 = null;
			if (num != num2)
			{
				matrix3 = Matrix.CreateTranslation(-point) * Matrix.CreateScale(1.0, num2 / num) * Matrix.CreateTranslation(point);
			}
			if (radialGradientBrush.Transform != null)
			{
				Matrix matrix4 = Matrix.CreateTranslation(radialGradientBrush.TransformOrigin.ToPixels(P_1));
				Matrix value = -matrix4 * radialGradientBrush.Transform.Value * matrix4;
				matrix3 = ((!matrix3.HasValue) ? new Matrix?(value) : (matrix3 * value));
			}
			if (point2.Equals(point))
			{
				using (SKShader shader2 = (matrix3.HasValue ? SKShader.CreateRadialGradient(sKPoint2, (float)num, array, array2, sKShaderTileMode, matrix3.Value.ToSKMatrix()) : SKShader.CreateRadialGradient(sKPoint2, (float)num, array, array2, sKShaderTileMode)))
				{
					P_0.Paint.Shader = shader2;
					return;
				}
			}
			if (num != num2)
			{
				point2 = point2.WithY((point2.Y - point.Y) * num / num2 + point.Y);
			}
			SKPoint sKPoint3 = point2.ToSKPoint();
			float num3 = array2[^1];
			SKPoint sKPoint4 = sKPoint3;
			float num4 = 0f;
			SKPoint sKPoint5 = sKPoint2;
			float num5 = (float)num;
			if ((point.X != point2.X || point.Y != point2.Y) && num3 == 1f)
			{
				SKPoint sKPoint6 = sKPoint5;
				float num6 = num5;
				SKPoint sKPoint7 = sKPoint4;
				num5 = num4;
				sKPoint5 = sKPoint7;
				num4 = num6;
				sKPoint4 = sKPoint6;
				int num7 = array2.Length;
				SKColor[] array3 = new SKColor[array.Length];
				float[] array4 = new float[num7];
				for (int num8 = 0; num8 < num7; num8++)
				{
					double offset = radialGradientBrush.GradientStops[num8].Offset;
					offset = 1.0 - offset;
					if (MathUtilities.IsZero(offset))
					{
						offset = 0.0;
					}
					int num9 = num7 - 1 - num8;
					array4[num9] = (float)offset;
					array3[num9] = array[num8];
				}
				array = array3;
				array2 = array4;
			}
			using SKShader shader3 = SKShader.CreateCompose(SKShader.CreateColor(array[0]), matrix3.HasValue ? SKShader.CreateTwoPointConicalGradient(sKPoint4, num4, sKPoint5, num5, array, array2, sKShaderTileMode, matrix3.Value.ToSKMatrix()) : SKShader.CreateTwoPointConicalGradient(sKPoint4, num4, sKPoint5, num5, array, array2, sKShaderTileMode));
			P_0.Paint.Shader = shader3;
			return;
		}
		SKPoint sKPoint8 = startPoint.ToPixels(P_1).ToSKPoint();
		SKPoint sKPoint9 = linearGradientBrush.EndPoint.ToPixels(P_1).ToSKPoint();
		if (linearGradientBrush.Transform == null)
		{
			using (SKShader shader4 = SKShader.CreateLinearGradient(sKPoint8, sKPoint9, array, array2, sKShaderTileMode))
			{
				P_0.Paint.Shader = shader4;
				return;
			}
		}
		Matrix matrix5 = Matrix.CreateTranslation(linearGradientBrush.TransformOrigin.ToPixels(P_1));
		Matrix matrix6 = -matrix5 * linearGradientBrush.Transform.Value * matrix5;
		using SKShader shader5 = SKShader.CreateLinearGradient(sKPoint8, sKPoint9, array, array2, sKShaderTileMode, matrix6.ToSKMatrix());
		P_0.Paint.Shader = shader5;
	}

	private void ConfigureTileBrush(ref PaintWrapper P_0, Rect P_1, ITileBrush P_2, IDrawableBitmapImpl P_3)
	{
		TileBrushCalculator tileBrushCalculator = new TileBrushCalculator(P_2, P_3.PixelSize.ToSizeWithDpi(_intermediateSurfaceDpi), P_1.Size);
		SurfaceRenderTarget surfaceRenderTarget = CreateRenderTarget(PixelSize.FromSizeWithDpi(tileBrushCalculator.IntermediateSize, _intermediateSurfaceDpi), false);
		P_0.AddDisposable(surfaceRenderTarget);
		using (IDrawingContextImpl drawingContextImpl = surfaceRenderTarget.CreateDrawingContext(true))
		{
			Rect rect = new Rect(P_3.PixelSize.ToSizeWithDpi(96.0));
			Rect rect2 = new Rect(P_3.PixelSize.ToSizeWithDpi(_intermediateSurfaceDpi));
			drawingContextImpl.Clear(Colors.Transparent);
			drawingContextImpl.PushClip(tileBrushCalculator.IntermediateClip);
			drawingContextImpl.PushRenderOptions(RenderOptions);
			drawingContextImpl.Transform = tileBrushCalculator.IntermediateTransform;
			drawingContextImpl.DrawBitmap(P_3, 1.0, rect, rect2);
			drawingContextImpl.PopRenderOptions();
			drawingContextImpl.PopClip();
		}
		SKMatrix sKMatrix = ((P_2.TileMode != TileMode.None) ? SKMatrix.CreateTranslation(0f - (float)tileBrushCalculator.DestinationRect.X, 0f - (float)tileBrushCalculator.DestinationRect.Y) : SKMatrix.CreateIdentity());
		SKShaderTileMode sKShaderTileMode = ((P_2.TileMode == TileMode.None) ? SKShaderTileMode.Decal : ((P_2.TileMode != TileMode.FlipX && P_2.TileMode != TileMode.FlipXY) ? SKShaderTileMode.Repeat : SKShaderTileMode.Mirror));
		SKShaderTileMode sKShaderTileMode2 = ((P_2.TileMode == TileMode.None) ? SKShaderTileMode.Decal : ((P_2.TileMode != TileMode.FlipY && P_2.TileMode != TileMode.FlipXY) ? SKShaderTileMode.Repeat : SKShaderTileMode.Mirror));
		SKImage sKImage = surfaceRenderTarget.SnapshotImage();
		P_0.AddDisposable(sKImage);
		SKMatrix sKMatrix2 = default(SKMatrix);
		SKMatrix.Concat(ref sKMatrix2, sKMatrix, SKMatrix.CreateScale((float)(96.0 / _intermediateSurfaceDpi.X), (float)(96.0 / _intermediateSurfaceDpi.Y)));
		if (P_2.Transform != null)
		{
			Matrix matrix = Matrix.CreateTranslation(P_2.TransformOrigin.ToPixels(P_1));
			Matrix matrix2 = -matrix * P_2.Transform.Value * matrix;
			sKMatrix2 = sKMatrix2.PreConcat(matrix2.ToSKMatrix());
		}
		if (P_2.DestinationRect.Unit == RelativeUnit.Relative)
		{
			sKMatrix2 = sKMatrix2.PreConcat(SKMatrix.CreateTranslation((float)P_1.X, (float)P_1.Y));
		}
		using SKShader shader = sKImage.ToShader(sKShaderTileMode, sKShaderTileMode2, sKMatrix2);
		P_0.Paint.Shader = shader;
	}

	private void ConfigureSceneBrushContent(ref PaintWrapper P_0, ISceneBrushContent P_1, Rect P_2)
	{
		if (P_1.UseScalableRasterization)
		{
			ConfigureSceneBrushContentWithPicture(ref P_0, P_1, P_2);
		}
		else
		{
			ConfigureSceneBrushContentWithSurface(ref P_0, P_1, P_2);
		}
	}

	private void ConfigureSceneBrushContentWithSurface(ref PaintWrapper P_0, ISceneBrushContent P_1, Rect P_2)
	{
		Rect rect = P_1.Rect;
		Size size = rect.Size;
		if (!(size.Width >= 1.0) || !(size.Height >= 1.0))
		{
			return;
		}
		using SurfaceRenderTarget surfaceRenderTarget = CreateRenderTarget(PixelSize.FromSizeWithDpi(size, _intermediateSurfaceDpi), false);
		using (IDrawingContextImpl drawingContextImpl = surfaceRenderTarget.CreateDrawingContext(true))
		{
			drawingContextImpl.PushRenderOptions(RenderOptions);
			drawingContextImpl.Clear(Colors.Transparent);
			P_1.Render(drawingContextImpl, (rect.TopLeft == default(Point)) ? ((Matrix?)null) : new Matrix?(Matrix.CreateTranslation(0.0 - rect.X, 0.0 - rect.Y)));
			drawingContextImpl.PopRenderOptions();
		}
		ConfigureTileBrush(ref P_0, P_2, P_1.Brush, surfaceRenderTarget);
	}

	private void ConfigureSceneBrushContentWithPicture(ref PaintWrapper P_0, ISceneBrushContent P_1, Rect P_2)
	{
		Rect rect = P_1.Rect;
		Rect rect2 = P_1.Brush.SourceRect.ToPixels(rect);
		if (rect.Size.Width <= 0.0 || rect.Size.Height <= 0.0 || rect2.Size.Width <= 0.0 || rect2.Size.Height <= 0.0)
		{
			P_0.Paint.Color = SKColor.Empty;
			return;
		}
		Matrix matrix = Matrix.CreateTranslation(0.0 - rect2.X, 0.0 - rect2.Y);
		Rect rect3 = P_1.Brush.DestinationRect.ToPixels(P_2);
		Size size = rect3.Size;
		if (rect2.Size != size)
		{
			Vector vector = P_1.Brush.Stretch.CalculateScaling(size, rect2.Size);
			Vector vector2 = TileBrushCalculator.CalculateTranslate(P_1.Brush.AlignmentX, P_1.Brush.AlignmentY, rect2.Size * vector, size);
			matrix = matrix * Matrix.CreateScale(vector) * Matrix.CreateTranslation(vector2);
		}
		using PictureRenderTarget pictureRenderTarget = new PictureRenderTarget(_gpu, _grContext, _intermediateSurfaceDpi);
		using (IDrawingContextImpl drawingContextImpl = pictureRenderTarget.CreateDrawingContext(size, false))
		{
			drawingContextImpl.PushRenderOptions(RenderOptions);
			P_1.Render(drawingContextImpl, matrix);
			drawingContextImpl.PopRenderOptions();
		}
		using SKPicture sKPicture = pictureRenderTarget.GetPicture();
		Matrix matrix2 = Matrix.Identity;
		if (P_1.Transform != null)
		{
			Matrix matrix3 = Matrix.CreateTranslation(P_1.TransformOrigin.ToPixels(P_2));
			matrix2 = -matrix3 * P_1.Transform.Value * matrix3;
		}
		if (rect3.Position != default(Point))
		{
			matrix2 *= Matrix.CreateTranslation(rect3.X, rect3.Y);
		}
		var (sKShaderTileMode, sKShaderTileMode2) = GetTileModes(P_1.Brush.TileMode);
		using SKShader shader = sKPicture.ToShader(sKShaderTileMode, sKShaderTileMode2, matrix2.ToSKMatrix(), new SKRect(0f, 0f, sKPicture.CullRect.Width, sKPicture.CullRect.Height));
		P_0.Paint.FilterQuality = SKFilterQuality.None;
		P_0.Paint.Shader = shader;
	}

	private (SKShaderTileMode x, SKShaderTileMode y) GetTileModes(TileMode P_0)
	{
		int num;
		switch (P_0)
		{
		default:
			num = 1;
			break;
		case TileMode.FlipX:
		case TileMode.FlipXY:
			num = 2;
			break;
		case TileMode.None:
			num = 3;
			break;
		}
		int num2;
		switch (P_0)
		{
		default:
			num2 = 1;
			break;
		case TileMode.FlipY:
		case TileMode.FlipXY:
			num2 = 2;
			break;
		case TileMode.None:
			num2 = 3;
			break;
		}
		return (x: (SKShaderTileMode)num, y: (SKShaderTileMode)num2);
	}

	internal PaintWrapper CreatePaint(SKPaint P_0, IBrush P_1, Rect P_2)
	{
		PaintWrapper result = new PaintWrapper(P_0);
		P_0.IsAntialias = RenderOptions.EdgeMode != EdgeMode.Aliased;
		double num = P_1.Opacity * (_useOpacitySaveLayer ? 1.0 : _currentOpacity);
		if (P_1 is ISolidColorBrush solidColorBrush)
		{
			P_0.Color = new SKColor(solidColorBrush.Color.R, solidColorBrush.Color.G, solidColorBrush.Color.B, (byte)((double)(int)solidColorBrush.Color.A * num));
			return result;
		}
		P_0.Color = new SKColor(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)(255.0 * num));
		if (P_1 is IGradientBrush gradientBrush)
		{
			ConfigureGradientBrush(ref result, P_2, gradientBrush);
			return result;
		}
		ITileBrush tileBrush = P_1 as ITileBrush;
		IDrawableBitmapImpl drawableBitmapImpl = null;
		if (P_1 is ISceneBrush sceneBrush)
		{
			using ISceneBrushContent sceneBrushContent = sceneBrush.CreateContent();
			if (sceneBrushContent != null)
			{
				ConfigureSceneBrushContent(ref result, sceneBrushContent, P_2);
				return result;
			}
			P_0.Color = default(SKColor);
		}
		else
		{
			if (P_1 is ISceneBrushContent sceneBrushContent2)
			{
				ConfigureSceneBrushContent(ref result, sceneBrushContent2, P_2);
				return result;
			}
			drawableBitmapImpl = (tileBrush as IImageBrush)?.Source?.Bitmap?.Item as IDrawableBitmapImpl;
		}
		if (tileBrush != null && drawableBitmapImpl != null)
		{
			ConfigureTileBrush(ref result, P_2, tileBrush, drawableBitmapImpl);
		}
		else
		{
			P_0.Color = new SKColor(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);
		}
		return result;
	}

	private PaintWrapper? TryCreatePaint(SKPaint P_0, IPen P_1, Rect P_2)
	{
		IBrush brush = P_1.Brush;
		if (brush == null || P_1.Thickness == 0.0)
		{
			return null;
		}
		PaintWrapper value = CreatePaint(P_0, brush, P_2);
		P_0.IsStroke = true;
		P_0.StrokeWidth = (float)P_1.Thickness;
		P_0.StrokeCap = P_1.LineCap.ToSKStrokeCap();
		P_0.StrokeJoin = P_1.LineJoin.ToSKStrokeJoin();
		P_0.StrokeMiter = (float)P_1.MiterLimit;
		if (DrawingContextHelper.TryCreateDashEffect(P_1, out SKPathEffect sKPathEffect))
		{
			P_0.PathEffect = sKPathEffect;
			value.AddDisposable(sKPathEffect);
		}
		return value;
	}

	private SurfaceRenderTarget CreateRenderTarget(PixelSize P_0, bool P_1, PixelFormat? P_2 = null)
	{
		return new SurfaceRenderTarget(new SurfaceRenderTarget.CreateInfo
		{
			Width = P_0.Width,
			Height = P_0.Height,
			Dpi = _intermediateSurfaceDpi,
			Format = P_2,
			DisableTextLcdRendering = (!P_1 || _disableSubpixelTextRendering),
			GrContext = _grContext,
			Gpu = _gpu,
			Session = _session,
			DisableManualFbo = !P_1
		});
	}

	public void PushEffect(Rect? P_0, IEffect P_1)
	{
		CheckLease();
		using SKImageFilter imageFilter = CreateEffect(P_1);
		SKPaint sKPaint = SKCacheBase<SKPaint, SKPaintCache>.Shared.Get();
		sKPaint.ImageFilter = imageFilter;
		if (P_0.HasValue)
		{
			Canvas.SaveLayer(P_0.Value.ToSKRect(), sKPaint);
		}
		else
		{
			Canvas.SaveLayer(sKPaint);
		}
		SKCacheBase<SKPaint, SKPaintCache>.Shared.ReturnReset(sKPaint);
	}

	public void PopEffect()
	{
		CheckLease();
		Canvas.Restore();
	}

	private SKImageFilter? CreateEffect(IEffect P_0)
	{
		if (P_0 is IBlurEffect blurEffect)
		{
			if (blurEffect.Radius <= 0.0)
			{
				return null;
			}
			float num = SkBlurRadiusToSigma(blurEffect.Radius);
			return SKImageFilter.CreateBlur(num, num);
		}
		if (P_0 is IDropShadowEffect dropShadowEffect)
		{
			float num2 = ((dropShadowEffect.BlurRadius > 0.0) ? SkBlurRadiusToSigma(dropShadowEffect.BlurRadius) : 0f);
			double num3 = (double)(int)dropShadowEffect.Color.A * dropShadowEffect.Opacity;
			if (!_useOpacitySaveLayer)
			{
				num3 *= _currentOpacity;
			}
			SKColor sKColor = new SKColor(dropShadowEffect.Color.R, dropShadowEffect.Color.G, dropShadowEffect.Color.B, (byte)Math.Max(0.0, Math.Min(255.0, num3)));
			return SKImageFilter.CreateDropShadow((float)dropShadowEffect.OffsetX, (float)dropShadowEffect.OffsetY, num2, num2, sKColor);
		}
		return null;
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.EllipseGeometryImpl
using Avalonia;
using Avalonia.Skia;
using SkiaSharp;

internal class EllipseGeometryImpl : GeometryImpl
{
	public override Rect Bounds { get; }

	public override SKPath StrokePath { get; }

	public override SKPath FillPath => StrokePath;

	public EllipseGeometryImpl(Rect P_0)
	{
		SKPath sKPath = new SKPath();
		sKPath.AddOval(P_0.ToSKRect());
		StrokePath = sKPath;
		Bounds = P_0;
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.FboSkiaSurface
using System;
using Avalonia;
using Avalonia.OpenGL;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

internal class FboSkiaSurface : ISkiaSurface, IDisposable
{
	private readonly GlSkiaGpu _gpu;

	private readonly GRContext _grContext;

	private readonly IGlContext _glContext;

	private readonly PixelSize _pixelSize;

	private int _fbo;

	private int _depthStencil;

	private int _texture;

	private SKSurface? _surface;

	private static readonly bool[] TrueFalse = new bool[2] { true, false };

	public SKSurface Surface => _surface ?? throw new ObjectDisposedException("FboSkiaSurface");

	public bool CanBlit { get; }

	public FboSkiaSurface(GlSkiaGpu P_0, GRContext P_1, IGlContext P_2, PixelSize P_3, GRSurfaceOrigin P_4)
	{
		_gpu = P_0;
		_grContext = P_1;
		_glContext = P_2;
		_pixelSize = P_3;
		int num = ((P_2.Version.Type == GlProfileType.OpenGLES) ? 6408 : 32856);
		GlInterface glInterface = P_2.GlInterface;
		glInterface.GetIntegerv(36006, out var num2);
		glInterface.GetIntegerv(36007, out var num3);
		glInterface.GetIntegerv(32873, out var num4);
		_fbo = glInterface.GenFramebuffer();
		glInterface.BindFramebuffer(36160, _fbo);
		_texture = glInterface.GenTexture();
		glInterface.BindTexture(3553, _texture);
		glInterface.TexImage2D(3553, 0, num, P_3.Width, P_3.Height, 0, 6408, 5121, IntPtr.Zero);
		glInterface.TexParameteri(3553, 10240, 9728);
		glInterface.TexParameteri(3553, 10241, 9728);
		glInterface.FramebufferTexture2D(36160, 36064, 3553, _texture, 0);
		bool flag = false;
		bool[] trueFalse = TrueFalse;
		foreach (bool num5 in trueFalse)
		{
			_depthStencil = glInterface.GenRenderbuffer();
			glInterface.BindRenderbuffer(36161, _depthStencil);
			if (num5)
			{
				glInterface.RenderbufferStorage(36161, 36168, P_3.Width, P_3.Height);
				glInterface.FramebufferRenderbuffer(36160, 36128, 36161, _depthStencil);
			}
			else
			{
				glInterface.RenderbufferStorage(36161, 35056, P_3.Width, P_3.Height);
				glInterface.FramebufferRenderbuffer(36160, 36096, 36161, _depthStencil);
				glInterface.FramebufferRenderbuffer(36160, 36128, 36161, _depthStencil);
			}
			if (glInterface.CheckFramebufferStatus(36160) == 36053)
			{
				flag = true;
				break;
			}
			glInterface.BindRenderbuffer(36161, num3);
			glInterface.DeleteRenderbuffer(_depthStencil);
		}
		glInterface.BindRenderbuffer(36161, num3);
		glInterface.BindTexture(3553, num4);
		glInterface.BindFramebuffer(36160, num2);
		if (!flag)
		{
			glInterface.DeleteFramebuffer(_fbo);
			glInterface.DeleteTexture(_texture);
			throw new OpenGlException("Unable to create FBO with stencil");
		}
		using GRBackendRenderTarget gRBackendRenderTarget = new GRBackendRenderTarget(P_3.Width, P_3.Height, 0, 8, new GRGlFramebufferInfo((uint)_fbo, SKColorType.Rgba8888.ToGlSizedFormat()));
		using SKSurfaceProperties sKSurfaceProperties = new SKSurfaceProperties(SKPixelGeometry.RgbHorizontal);
		_surface = SKSurface.Create(_grContext, gRBackendRenderTarget, P_4, SKColorType.Rgba8888, sKSurfaceProperties);
		CanBlit = glInterface.IsBlitFramebufferAvailable;
	}

	public void Dispose()
	{
		try
		{
			using (_glContext.EnsureCurrent())
			{
				_surface?.Dispose();
				_surface = null;
				GlInterface glInterface = _glContext.GlInterface;
				if (_fbo != 0)
				{
					glInterface.DeleteFramebuffer(_fbo);
					glInterface.DeleteTexture(_texture);
					glInterface.DeleteRenderbuffer(_depthStencil);
				}
			}
		}
		catch (PlatformGraphicsContextLostException)
		{
			if (_surface != null)
			{
				_gpu.AddPostDispose(_surface.Dispose);
			}
			_surface = null;
		}
		finally
		{
			_fbo = (_texture = (_depthStencil = 0));
		}
	}

	public void Blit(SKCanvas P_0)
	{
		P_0.Clear();
		P_0.Flush();
		GlInterface glInterface = _glContext.GlInterface;
		glInterface.GetIntegerv(36010, out var num);
		glInterface.BindFramebuffer(36008, _fbo);
		glInterface.BlitFramebuffer(0, 0, _pixelSize.Width, _pixelSize.Height, 0, 0, _pixelSize.Width, _pixelSize.Height, 16384, 9729);
		glInterface.BindFramebuffer(36008, num);
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.FontManagerImpl
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

internal class FontManagerImpl : IFontManagerImpl, IFontManagerImpl2
{
	private SKFontManager _skFontManager = SKFontManager.Default;

	[ThreadStatic]
	private static string[]? t_languageTagBuffer;

	public string GetDefaultFontFamilyName()
	{
		return SKTypeface.Default.FamilyName;
	}

	public string[] GetInstalledFontFamilyNames(bool P_0 = false)
	{
		if (P_0)
		{
			_skFontManager = SKFontManager.CreateDefault();
		}
		return _skFontManager.GetFontFamilies();
	}

	public bool TryMatchCharacter(int P_0, FontStyle P_1, FontWeight P_2, FontStretch P_3, CultureInfo? P_4, out Typeface P_5)
	{
		if (!TryMatchCharacter(P_0, P_1, P_2, P_3, P_4, out SKTypeface sKTypeface))
		{
			P_5 = default(Typeface);
			return false;
		}
		P_5 = new Typeface(sKTypeface.FamilyName, sKTypeface.FontStyle.Slant.ToAvalonia(), (FontWeight)sKTypeface.FontStyle.Weight, (FontStretch)sKTypeface.FontStyle.Width);
		sKTypeface.Dispose();
		return true;
	}

	public bool TryMatchCharacter(int P_0, FontStyle P_1, FontWeight P_2, FontStretch P_3, CultureInfo? P_4, [NotNullWhen(true)] out IGlyphTypeface? P_5)
	{
		if (!TryMatchCharacter(P_0, P_1, P_2, P_3, P_4, out SKTypeface sKTypeface))
		{
			P_5 = null;
			return false;
		}
		P_5 = new GlyphTypefaceImpl(sKTypeface, FontSimulations.None);
		return true;
	}

	private bool TryMatchCharacter(int P_0, FontStyle P_1, FontWeight P_2, FontStretch P_3, CultureInfo? P_4, [NotNullWhen(true)] out SKTypeface? P_5)
	{
		SKFontStyle sKFontStyle;
		if (P_2 != FontWeight.Normal)
		{
			if (P_2 != FontWeight.Bold)
			{
				goto IL_0056;
			}
			if (P_1 == FontStyle.Normal && P_3 == FontStretch.Normal)
			{
				sKFontStyle = SKFontStyle.Bold;
			}
			else
			{
				if (P_1 != FontStyle.Italic || P_3 != FontStretch.Normal)
				{
					goto IL_0056;
				}
				sKFontStyle = SKFontStyle.BoldItalic;
			}
		}
		else if (P_1 == FontStyle.Normal && P_3 == FontStretch.Normal)
		{
			sKFontStyle = SKFontStyle.Normal;
		}
		else
		{
			if (P_1 != FontStyle.Italic || P_3 != FontStretch.Normal)
			{
				goto IL_0056;
			}
			sKFontStyle = SKFontStyle.Italic;
		}
		goto IL_0065;
		IL_0056:
		sKFontStyle = new SKFontStyle((SKFontStyleWeight)P_2, (SKFontStyleWidth)P_3, P_1.ToSkia());
		goto IL_0065;
		IL_0065:
		if (P_4 == null)
		{
			P_4 = CultureInfo.CurrentUICulture;
		}
		if (t_languageTagBuffer == null)
		{
			t_languageTagBuffer = new string[1];
		}
		t_languageTagBuffer[0] = P_4.Name;
		P_5 = _skFontManager.MatchCharacter(null, sKFontStyle, t_languageTagBuffer, P_0);
		return P_5 != null;
	}

	public bool TryCreateGlyphTypeface(string P_0, FontStyle P_1, FontWeight P_2, FontStretch P_3, [NotNullWhen(true)] out IGlyphTypeface? P_4)
	{
		P_4 = null;
		SKFontStyle sKFontStyle = new SKFontStyle((SKFontStyleWeight)P_2, (SKFontStyleWidth)P_3, P_1.ToSkia());
		SKTypeface sKTypeface = _skFontManager.MatchFamily(P_0, sKFontStyle);
		if (false)
		{
		}
		if (sKTypeface == null)
		{
			return false;
		}
		FontSimulations fontSimulations = FontSimulations.None;
		if (P_2 >= FontWeight.DemiBold && !sKTypeface.IsBold)
		{
			fontSimulations |= FontSimulations.Bold;
		}
		if (P_1 == FontStyle.Italic && !sKTypeface.IsItalic)
		{
			fontSimulations |= FontSimulations.Oblique;
		}
		P_4 = new GlyphTypefaceImpl(sKTypeface, fontSimulations);
		return true;
	}

	public bool TryCreateGlyphTypeface(Stream P_0, FontSimulations P_1, [NotNullWhen(true)] out IGlyphTypeface? P_2)
	{
		SKTypeface sKTypeface = SKTypeface.FromStream(P_0);
		if (sKTypeface != null)
		{
			P_2 = new GlyphTypefaceImpl(sKTypeface, P_1);
			return true;
		}
		P_2 = null;
		return false;
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.FramebufferRenderTarget
using System;
using System.Diagnostics.CodeAnalysis;
using Avalonia;
using Avalonia.Controls.Platform.Surfaces;
using Avalonia.Platform;
using Avalonia.Reactive;
using Avalonia.Skia;
using SkiaSharp;

internal class FramebufferRenderTarget : IRenderTarget2, IRenderTarget, IDisposable
{
	private class PixelFormatConversionShim : IDisposable
	{
		private readonly SKBitmap _bitmap;

		private readonly SKImageInfo _destinationInfo;

		private readonly nint _framebufferAddress;

		public SKSurface Surface { get; }

		public IDisposable SurfaceCopyHandler { get; }

		public PixelFormatConversionShim(SKImageInfo P_0, nint P_1)
		{
			_destinationInfo = P_0;
			_framebufferAddress = P_1;
			_bitmap = new SKBitmap(P_0.Width, P_0.Height);
			if (!_bitmap.CanCopyTo(P_0.ColorType))
			{
				SKColorType colorType = _bitmap.ColorType;
				_bitmap.Dispose();
				throw new Exception($"Unable to create pixel format shim for conversion from {colorType} to {P_0.ColorType}");
			}
			Surface = SKSurface.Create(_bitmap.Info, _bitmap.GetPixels(), _bitmap.RowBytes, new SKSurfaceProperties(SKPixelGeometry.RgbHorizontal));
			if (Surface == null)
			{
				SKColorType colorType = _bitmap.ColorType;
				_bitmap.Dispose();
				throw new Exception($"Unable to create pixel format shim surface for conversion from {colorType} to {P_0.ColorType}");
			}
			SurfaceCopyHandler = Disposable.Create(CopySurface);
		}

		public void Dispose()
		{
			Surface.Dispose();
			_bitmap.Dispose();
		}

		private void CopySurface()
		{
			using SKImage sKImage = Surface.Snapshot();
			sKImage.ReadPixels(_destinationInfo, _framebufferAddress, _destinationInfo.RowBytes, 0, 0, SKImageCachingHint.Disallow);
		}
	}

	private SKImageInfo _currentImageInfo;

	private nint _currentFramebufferAddress;

	private SKSurface? _framebufferSurface;

	private PixelFormatConversionShim? _conversionShim;

	private IDisposable? _preFramebufferCopyHandler;

	private IFramebufferRenderTarget? _renderTarget;

	private IFramebufferRenderTargetWithProperties? _renderTargetWithProperties;

	private bool _hadConversionShim;

	public RenderTargetProperties Properties => new RenderTargetProperties
	{
		RetainsPreviousFrameContents = (!_hadConversionShim && (_renderTargetWithProperties?.RetainsFrameContents ?? false)),
		IsSuitableForDirectRendering = true
	};

	public bool IsCorrupted => false;

	public FramebufferRenderTarget(IFramebufferPlatformSurface P_0)
	{
		_renderTarget = P_0.CreateFramebufferRenderTarget();
		_renderTargetWithProperties = _renderTarget as IFramebufferRenderTargetWithProperties;
	}

	public void Dispose()
	{
		_renderTarget?.Dispose();
		_renderTarget = null;
		_renderTargetWithProperties = null;
		FreeSurface();
	}

	public IDrawingContextImpl CreateDrawingContext(bool P_0)
	{
		RenderTargetDrawingContextProperties renderTargetDrawingContextProperties;
		return CreateDrawingContextCore(P_0, out renderTargetDrawingContextProperties);
	}

	public IDrawingContextImpl CreateDrawingContext(PixelSize P_0, out RenderTargetDrawingContextProperties P_1)
	{
		return CreateDrawingContextCore(false, out P_1);
	}

	private IDrawingContextImpl CreateDrawingContextCore(bool P_0, out RenderTargetDrawingContextProperties P_1)
	{
		if (_renderTarget == null)
		{
			throw new ObjectDisposedException("FramebufferRenderTarget");
		}
		FramebufferLockProperties framebufferLockProperties = default(FramebufferLockProperties);
		ILockedFramebuffer lockedFramebuffer = _renderTargetWithProperties?.Lock(out framebufferLockProperties) ?? _renderTarget.Lock();
		SKImageInfo sKImageInfo = new SKImageInfo(lockedFramebuffer.Size.Width, lockedFramebuffer.Size.Height, lockedFramebuffer.Format.ToSkColorType(), (lockedFramebuffer.Format == PixelFormat.Rgb565) ? SKAlphaType.Opaque : SKAlphaType.Premul);
		CreateSurface(sKImageInfo, lockedFramebuffer);
		_hadConversionShim |= _conversionShim != null;
		SKCanvas canvas = _framebufferSurface.Canvas;
		canvas.RestoreToCount(-1);
		canvas.Save();
		canvas.ResetMatrix();
		DrawingContextImpl.CreateInfo obj = new DrawingContextImpl.CreateInfo
		{
			Surface = _framebufferSurface,
			Dpi = lockedFramebuffer.Dpi,
			ScaleDrawingToDpi = P_0
		};
		P_1 = new RenderTargetDrawingContextProperties
		{
			PreviousFrameIsRetained = (!_hadConversionShim && framebufferLockProperties.PreviousFrameIsRetained)
		};
		return new DrawingContextImpl(obj, _preFramebufferCopyHandler, canvas, lockedFramebuffer);
	}

	private static bool AreImageInfosCompatible(SKImageInfo P_0, SKImageInfo P_1)
	{
		if (P_0.Width == P_1.Width && P_0.Height == P_1.Height)
		{
			return P_0.ColorType == P_1.ColorType;
		}
		return false;
	}

	[MemberNotNull("_framebufferSurface")]
	private void CreateSurface(SKImageInfo P_0, ILockedFramebuffer P_1)
	{
		if (_framebufferSurface == null || !AreImageInfosCompatible(_currentImageInfo, P_0) || _currentFramebufferAddress != P_1.Address)
		{
			FreeSurface();
			_currentFramebufferAddress = P_1.Address;
			SKSurface sKSurface = SKSurface.Create(P_0, _currentFramebufferAddress, P_1.RowBytes, new SKSurfaceProperties(SKPixelGeometry.RgbHorizontal));
			if (sKSurface == null)
			{
				_conversionShim = new PixelFormatConversionShim(P_0, P_1.Address);
				_preFramebufferCopyHandler = _conversionShim.SurfaceCopyHandler;
				sKSurface = _conversionShim.Surface;
			}
			_framebufferSurface = sKSurface ?? throw new Exception("Unable to create a surface for pixel format " + P_1.Format.ToString() + " or pixel format translator");
			_currentImageInfo = P_0;
		}
	}

	private void FreeSurface()
	{
		_conversionShim?.Dispose();
		_conversionShim = null;
		_preFramebufferCopyHandler = null;
		_framebufferSurface?.Dispose();
		_framebufferSurface = null;
		_currentFramebufferAddress = IntPtr.Zero;
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.GeometryGroupImpl
using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

internal class GeometryGroupImpl : GeometryImpl
{
	public override Rect Bounds { get; }

	public override SKPath StrokePath { get; }

	public override SKPath FillPath { get; }

	public GeometryGroupImpl(FillRule P_0, IReadOnlyList<IGeometryImpl> P_1)
	{
		SKPathFillType fillType = ((P_0 != FillRule.NonZero) ? SKPathFillType.EvenOdd : SKPathFillType.Winding);
		int count = P_1.Count;
		SKPath sKPath = new SKPath
		{
			FillType = fillType
		};
		bool flag = false;
		for (int i = 0; i < count; i++)
		{
			if (P_1[i] is GeometryImpl geometryImpl)
			{
				if (geometryImpl.StrokePath != null)
				{
					sKPath.AddPath(geometryImpl.StrokePath);
				}
				if (geometryImpl.StrokePath != geometryImpl.FillPath)
				{
					flag = true;
				}
			}
		}
		StrokePath = sKPath;
		if (flag)
		{
			SKPath sKPath2 = new SKPath
			{
				FillType = fillType
			};
			for (int j = 0; j < count; j++)
			{
				if (P_1[j] is GeometryImpl geometryImpl2)
				{
					SKPath fillPath = geometryImpl2.FillPath;
					if (fillPath != null)
					{
						sKPath2.AddPath(fillPath);
					}
				}
			}
			FillPath = sKPath2;
		}
		else
		{
			FillPath = sKPath;
		}
		Bounds = sKPath.TightBounds.ToAvaloniaRect();
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.GeometryImpl
using System;
using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Skia;
using Avalonia.Skia.Helpers;
using SkiaSharp;

internal abstract class GeometryImpl : IGeometryImpl
{
	private struct PathCache : IDisposable
	{
		private int _penHash;

		private SKPath? _path;

		private SKPath? _cachedFor;

		private Rect? _renderBounds;

		private static readonly SKPath s_emptyPath = new SKPath();

		public Rect RenderBounds
		{
			get
			{
				Rect valueOrDefault = _renderBounds.GetValueOrDefault();
				if (!_renderBounds.HasValue)
				{
					valueOrDefault = (_path ?? _cachedFor ?? s_emptyPath).TightBounds.ToAvaloniaRect();
					_renderBounds = valueOrDefault;
					return valueOrDefault;
				}
				return valueOrDefault;
			}
		}

		public SKPath ExpandedPath => _path ?? s_emptyPath;

		public void UpdateIfNeeded(SKPath? P_0, IPen? P_1)
		{
			int hashCode = PenHelper.GetHashCode(P_1, false);
			if (hashCode != _penHash || P_0 != _cachedFor)
			{
				_renderBounds = null;
				_cachedFor = P_0;
				_penHash = hashCode;
				_path?.Dispose();
				if (P_0 != null && P_1 != null)
				{
					_path = SKPathHelper.CreateStrokedPath(P_0, P_1);
				}
				else
				{
					_path = null;
				}
			}
		}

		public void Dispose()
		{
			_path?.Dispose();
			_path = null;
		}
	}

	private readonly object _lock = new object();

	private PathCache _pathCache;

	public abstract Rect Bounds { get; }

	public abstract SKPath? StrokePath { get; }

	public abstract SKPath? FillPath { get; }

	public bool FillContains(Point P_0)
	{
		return PathContainsCore(FillPath, P_0);
	}

	public bool StrokeContains(IPen? P_0, Point P_1)
	{
		lock (_lock)
		{
			_pathCache.UpdateIfNeeded(StrokePath, P_0);
			return PathContainsCore(_pathCache.ExpandedPath, P_1);
		}
	}

	private static bool PathContainsCore(SKPath? P_0, Point P_1)
	{
		return P_0?.Contains((float)P_1.X, (float)P_1.Y) ?? false;
	}

	public Rect GetRenderBounds(IPen? P_0)
	{
		lock (_lock)
		{
			_pathCache.UpdateIfNeeded(StrokePath, P_0);
			Rect result = _pathCache.RenderBounds;
			if (StrokePath != FillPath && FillPath != null)
			{
				result = result.Union(FillPath.TightBounds.ToAvaloniaRect());
			}
			return result;
		}
	}

	public ITransformedGeometryImpl WithTransform(Matrix P_0)
	{
		return new TransformedGeometryImpl(this, P_0);
	}

	protected void InvalidateCaches()
	{
		lock (_lock)
		{
			_pathCache.Dispose();
			_pathCache = default(PathCache);
		}
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.GlRenderTarget
using System;
using Avalonia;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Surfaces;
using Avalonia.Skia;
using SkiaSharp;

internal class GlRenderTarget : ISkiaGpuRenderTarget2, ISkiaGpuRenderTarget, IDisposable
{
	private class GlGpuSession : ISkiaGpuRenderSession, IDisposable
	{
		private readonly GRBackendRenderTarget _backendRenderTarget;

		private readonly SKSurface _surface;

		private readonly IGlPlatformSurfaceRenderingSession _glSession;

		public GRSurfaceOrigin SurfaceOrigin { get; }

		public GRContext GrContext { get; }

		public SKSurface SkSurface => _surface;

		public double ScaleFactor => _glSession.Scaling;

		public GlGpuSession(GRContext P_0, GRBackendRenderTarget P_1, SKSurface P_2, IGlPlatformSurfaceRenderingSession P_3)
		{
			GrContext = P_0;
			_backendRenderTarget = P_1;
			_surface = P_2;
			_glSession = P_3;
			SurfaceOrigin = ((!P_3.IsYFlipped) ? GRSurfaceOrigin.BottomLeft : GRSurfaceOrigin.TopLeft);
		}

		public void Dispose()
		{
			_surface.Canvas.Flush();
			_surface.Dispose();
			_backendRenderTarget.Dispose();
			GrContext.Flush();
			_glSession.Dispose();
		}
	}

	private readonly GRContext _grContext;

	private IGlPlatformSurfaceRenderTarget _surface;

	private static readonly SKSurfaceProperties _surfaceProperties = new SKSurfaceProperties(SKPixelGeometry.RgbHorizontal);

	public bool IsCorrupted => (_surface as IGlPlatformSurfaceRenderTargetWithCorruptionInfo)?.IsCorrupted ?? false;

	public GlRenderTarget(GRContext P_0, IGlContext P_1, IGlPlatformSurface P_2)
	{
		_grContext = P_0;
		using (P_1.EnsureCurrent())
		{
			_surface = P_2.CreateGlRenderTarget(P_1);
		}
	}

	public void Dispose()
	{
		_surface.Dispose();
	}

	public ISkiaGpuRenderSession BeginRenderingSession(PixelSize P_0)
	{
		return BeginRenderingSessionCore(P_0);
	}

	public ISkiaGpuRenderSession BeginRenderingSession()
	{
		return BeginRenderingSessionCore(null);
	}

	private ISkiaGpuRenderSession BeginRenderingSessionCore(PixelSize? P_0)
	{
		IGlPlatformSurfaceRenderingSession glPlatformSurfaceRenderingSession = ((P_0.HasValue && _surface is IGlPlatformSurfaceRenderTarget2 glPlatformSurfaceRenderTarget) ? glPlatformSurfaceRenderTarget.BeginDraw(P_0.Value) : _surface.BeginDraw());
		bool flag = false;
		try
		{
			IGlContext context = glPlatformSurfaceRenderingSession.Context;
			context.GlInterface.GetIntegerv(36006, out var num);
			PixelSize size = glPlatformSurfaceRenderingSession.Size;
			SKColorType sKColorType = SKColorType.Rgba8888;
			double scaling = glPlatformSurfaceRenderingSession.Scaling;
			if (size.Width <= 0 || size.Height <= 0 || scaling < 0.0)
			{
				glPlatformSurfaceRenderingSession.Dispose();
				throw new InvalidOperationException($"Can't create drawing context for surface with {size} size and {scaling} scaling");
			}
			lock (_grContext)
			{
				_grContext.ResetContext();
				int num2 = context.SampleCount;
				int maxSurfaceSampleCount = _grContext.GetMaxSurfaceSampleCount(sKColorType);
				if (num2 > maxSurfaceSampleCount)
				{
					num2 = maxSurfaceSampleCount;
				}
				GRBackendRenderTarget gRBackendRenderTarget = new GRBackendRenderTarget(glInfo: new GRGlFramebufferInfo((uint)num, sKColorType.ToGlSizedFormat()), width: size.Width, height: size.Height, sampleCount: num2, stencilBits: context.StencilSize);
				SKSurface sKSurface = SKSurface.Create(_grContext, gRBackendRenderTarget, (!glPlatformSurfaceRenderingSession.IsYFlipped) ? GRSurfaceOrigin.BottomLeft : GRSurfaceOrigin.TopLeft, sKColorType, _surfaceProperties);
				flag = true;
				return new GlGpuSession(_grContext, gRBackendRenderTarget, sKSurface, glPlatformSurfaceRenderingSession);
			}
		}
		finally
		{
			if (!flag)
			{
				glPlatformSurfaceRenderingSession.Dispose();
			}
		}
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.GlSkiaExternalObjectsFeature
using Avalonia.OpenGL;
using Avalonia.Platform;
using Avalonia.Skia;

internal class GlSkiaExternalObjectsFeature : IExternalObjectsRenderInterfaceContextFeature
{
	private readonly GlSkiaGpu _gpu;

	private readonly IGlContextExternalObjectsFeature? _feature;

	public GlSkiaExternalObjectsFeature(GlSkiaGpu P_0, IGlContextExternalObjectsFeature? P_1)
	{
		_gpu = P_0;
		_feature = P_1;
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.GlSkiaGpu
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Logging;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Surfaces;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

internal class GlSkiaGpu : ISkiaGpu, IPlatformGraphicsContext, IDisposable, IOptionalFeatureProvider, IOpenGlTextureSharingRenderInterfaceContextFeature, ISkiaGpuWithPlatformGraphicsContext
{
	private class SurfaceWrapper : IGlPlatformSurface
	{
		private readonly object _surface;

		public SurfaceWrapper(object P_0)
		{
			_surface = P_0;
		}

		public IGlPlatformSurfaceRenderTarget CreateGlRenderTarget(IGlContext P_0)
		{
			return P_0.TryGetFeature<IGlPlatformSurfaceRenderTargetFactory>().CreateRenderTarget(P_0, _surface);
		}
	}

	private readonly GRContext _grContext;

	private readonly IGlContext _glContext;

	private readonly List<Action> _postDisposeCallbacks = new List<Action>();

	private bool? _canCreateSurfaces;

	private readonly IExternalObjectsRenderInterfaceContextFeature? _externalObjectsFeature;

	public bool IsLost => _glContext.IsLost;

	public IPlatformGraphicsContext? PlatformGraphicsContext => _glContext;

	public GlSkiaGpu(IGlContext P_0, long? P_1)
	{
		_glContext = P_0;
		using (_glContext.EnsureCurrent())
		{
			IGlSkiaSpecificOptionsFeature glSkiaSpecificOptionsFeature = P_0.TryGetFeature<IGlSkiaSpecificOptionsFeature>();
			GRGlInterface gRGlInterface = ((glSkiaSpecificOptionsFeature == null || !glSkiaSpecificOptionsFeature.UseNativeSkiaGrGlInterface) ? ((P_0.Version.Type == GlProfileType.OpenGL) ? GRGlInterface.CreateOpenGl((string proc) => P_0.GlInterface.GetProcAddress(proc)) : GRGlInterface.CreateGles((string proc) => P_0.GlInterface.GetProcAddress(proc))) : GRGlInterface.Create());
			using (gRGlInterface)
			{
				_grContext = GRContext.CreateGl(gRGlInterface, new GRContextOptions
				{
					AvoidStencilBuffers = true
				});
				if (P_1.HasValue)
				{
					_grContext.SetResourceCacheLimit(P_1.Value);
				}
			}
			P_0.TryGetFeature<IGlContextExternalObjectsFeature>(out var glContextExternalObjectsFeature);
			_externalObjectsFeature = new GlSkiaExternalObjectsFeature(this, glContextExternalObjectsFeature);
		}
	}

	public ISkiaGpuRenderTarget? TryCreateRenderTarget(IEnumerable<object> P_0)
	{
		IGlPlatformSurfaceRenderTargetFactory glPlatformSurfaceRenderTargetFactory = _glContext.TryGetFeature<IGlPlatformSurfaceRenderTargetFactory>();
		foreach (object item in P_0)
		{
			if (glPlatformSurfaceRenderTargetFactory != null && glPlatformSurfaceRenderTargetFactory.CanRenderToSurface(_glContext, item))
			{
				return new GlRenderTarget(_grContext, _glContext, new SurfaceWrapper(item));
			}
			if (item is IGlPlatformSurface glPlatformSurface)
			{
				return new GlRenderTarget(_grContext, _glContext, glPlatformSurface);
			}
		}
		return null;
	}

	public ISkiaSurface? TryCreateSurface(PixelSize P_0, ISkiaGpuRenderSession? P_1)
	{
		if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
		{
			return null;
		}
		if (!_glContext.GlInterface.IsBlitFramebufferAvailable)
		{
			return null;
		}
		P_0 = new PixelSize(Math.Max(P_0.Width, 1), Math.Max(P_0.Height, 1));
		if (_canCreateSurfaces == false)
		{
			return null;
		}
		try
		{
			FboSkiaSurface result = new FboSkiaSurface(this, _grContext, _glContext, P_0, P_1?.SurfaceOrigin ?? GRSurfaceOrigin.TopLeft);
			_canCreateSurfaces = true;
			return result;
		}
		catch (Exception)
		{
			Logger.TryGet(LogEventLevel.Error, "OpenGL")?.Log(this, "Unable to create a Skia-compatible FBO manually");
			bool valueOrDefault = _canCreateSurfaces == true;
			if (!_canCreateSurfaces.HasValue)
			{
				valueOrDefault = false;
				_canCreateSurfaces = valueOrDefault;
			}
			return null;
		}
	}

	public void Dispose()
	{
		if (_glContext.IsLost)
		{
			_grContext.AbandonContext();
		}
		else
		{
			_grContext.AbandonContext(true);
		}
		_grContext.Dispose();
		lock (_postDisposeCallbacks)
		{
			foreach (Action postDisposeCallback in _postDisposeCallbacks)
			{
				postDisposeCallback();
			}
		}
	}

	public IDisposable EnsureCurrent()
	{
		return _glContext.EnsureCurrent();
	}

	public object? TryGetFeature(Type P_0)
	{
		if (P_0 == typeof(IOpenGlTextureSharingRenderInterfaceContextFeature))
		{
			return this;
		}
		if (P_0 == typeof(IExternalObjectsRenderInterfaceContextFeature))
		{
			return _externalObjectsFeature;
		}
		if (P_0 == typeof(IExternalObjectsHandleWrapRenderInterfaceContextFeature))
		{
			return _glContext.TryGetFeature(P_0);
		}
		return null;
	}

	public void AddPostDispose(Action P_0)
	{
		lock (_postDisposeCallbacks)
		{
			_postDisposeCallbacks.Add(P_0);
		}
	}
}

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

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.GlyphTypefaceImpl
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Avalonia.Media;
using Avalonia.Media.Fonts.Tables;
using Avalonia.Media.Fonts.Tables.Name;
using HarfBuzzSharp;
using SkiaSharp;

internal class GlyphTypefaceImpl : IGlyphTypeface2, IGlyphTypeface, IDisposable
{
	private bool _isDisposed;

	private readonly NameTable? _nameTable;

	private readonly OS2Table? _os2Table;

	private readonly HorizontalHeadTable? _hhTable;

	[CompilerGenerated]
	private readonly IReadOnlyDictionary<ushort, string> _003CFaceNames_003Ek__BackingField;

	[CompilerGenerated]
	private readonly int _003CGlyphCount_003Ek__BackingField;

	public string TypographicFamilyName { get; }

	public IReadOnlyDictionary<ushort, string> FamilyNames { get; }

	public SKTypeface SKTypeface { get; }

	public Face Face { get; }

	public Font Font { get; }

	public FontSimulations FontSimulations { get; }

	public FontMetrics Metrics { get; }

	public string FamilyName { get; }

	public FontWeight Weight { get; }

	public FontStyle Style { get; }

	public FontStretch Stretch { get; }

	public GlyphTypefaceImpl(SKTypeface P_0, FontSimulations P_1)
	{
		SKTypeface = P_0 ?? throw new ArgumentNullException("typeface");
		Face = new Face(GetTable)
		{
			UnitsPerEm = P_0.UnitsPerEm
		};
		Font = new Font(Face);
		Font.SetFunctionsOpenType();
		Font.OpenTypeMetrics.TryGetPosition(OpenTypeMetricsTag.UnderlineOffset, out var num);
		Font.OpenTypeMetrics.TryGetPosition(OpenTypeMetricsTag.UnderlineSize, out var underlineThickness);
		_os2Table = OS2Table.Load(this);
		_hhTable = HorizontalHeadTable.Load(this);
		int num2 = 0;
		int num3 = 0;
		int lineGap = 0;
		if (_os2Table != null && (_os2Table.FontStyle & OS2Table.FontStyleSelection.USE_TYPO_METRICS) != 0)
		{
			num2 = -_os2Table.TypoAscender;
			num3 = -_os2Table.TypoDescender;
			lineGap = _os2Table.TypoLineGap;
		}
		else if (_hhTable != null)
		{
			num2 = -_hhTable.Ascender;
			num3 = -_hhTable.Descender;
			lineGap = _hhTable.LineGap;
		}
		if (_os2Table != null && (num2 == 0 || num3 == 0))
		{
			if (_os2Table.TypoAscender != 0 || _os2Table.TypoDescender != 0)
			{
				num2 = -_os2Table.TypoAscender;
				num3 = -_os2Table.TypoDescender;
				lineGap = _os2Table.TypoLineGap;
			}
			else
			{
				num2 = -_os2Table.WinAscent;
				num3 = _os2Table.WinDescent;
			}
		}
		Metrics = new FontMetrics
		{
			DesignEmHeight = (short)Face.UnitsPerEm,
			Ascent = num2,
			Descent = num3,
			LineGap = lineGap,
			UnderlinePosition = -num,
			UnderlineThickness = underlineThickness,
			StrikethroughPosition = (-(_os2Table?.StrikeoutPosition)).GetValueOrDefault(),
			StrikethroughThickness = (_os2Table?.StrikeoutSize ?? 0),
			IsFixedPitch = P_0.IsFixedPitch
		};
		_003CGlyphCount_003Ek__BackingField = P_0.GlyphCount;
		FontSimulations = P_1;
		FontWeight fontWeight = (FontWeight)((_os2Table != null) ? _os2Table.WeightClass : 400);
		Weight = (((P_1 & FontSimulations.Bold) != FontSimulations.None) ? FontWeight.Bold : fontWeight);
		FontStyle fontStyle = ((_os2Table != null) ? GetFontStyle(_os2Table.FontStyle) : FontStyle.Normal);
		if (P_0.FontStyle.Slant == SKFontStyleSlant.Oblique)
		{
			fontStyle = FontStyle.Oblique;
		}
		Style = (((P_1 & FontSimulations.Oblique) != FontSimulations.None) ? FontStyle.Italic : fontStyle);
		Stretch = (FontStretch)((_os2Table != null) ? _os2Table.WidthClass : 5);
		_nameTable = NameTable.Load(this);
		FamilyName = _nameTable?.FontFamilyName((ushort)CultureInfo.InvariantCulture.LCID) ?? P_0.FamilyName;
		TypographicFamilyName = _nameTable?.GetNameById((ushort)CultureInfo.InvariantCulture.LCID, KnownNameIds.TypographicFamilyName) ?? FamilyName;
		if (_nameTable != null)
		{
			Dictionary<ushort, string> dictionary = new Dictionary<ushort, string>(1);
			Dictionary<ushort, string> dictionary2 = new Dictionary<ushort, string>(1);
			foreach (NameRecord item in _nameTable)
			{
				if (item.NameID == KnownNameIds.FontFamilyName)
				{
					if (item.Platform != PlatformIDs.Windows || item.LanguageID == 0)
					{
						continue;
					}
					if (!dictionary.ContainsKey(item.LanguageID))
					{
						dictionary[item.LanguageID] = item.Value;
					}
				}
				if (item.NameID == KnownNameIds.FontSubfamilyName && item.Platform == PlatformIDs.Windows && item.LanguageID != 0 && !dictionary2.ContainsKey(item.LanguageID))
				{
					dictionary2[item.LanguageID] = item.Value;
				}
			}
			FamilyNames = dictionary;
			_003CFaceNames_003Ek__BackingField = dictionary2;
		}
		else
		{
			FamilyNames = new Dictionary<ushort, string> { 
			{
				(ushort)CultureInfo.InvariantCulture.LCID,
				FamilyName
			} };
			_003CFaceNames_003Ek__BackingField = new Dictionary<ushort, string> { 
			{
				(ushort)CultureInfo.InvariantCulture.LCID,
				Weight.ToString()
			} };
		}
	}

	public ushort GetGlyph(uint P_0)
	{
		if (Font.TryGetGlyph(P_0, out var num))
		{
			return (ushort)num;
		}
		return 0;
	}

	public bool TryGetGlyph(uint P_0, out ushort P_1)
	{
		P_1 = GetGlyph(P_0);
		return P_1 != 0;
	}

	public int GetGlyphAdvance(ushort P_0)
	{
		return Font.GetHorizontalGlyphAdvance(P_0);
	}

	public int[] GetGlyphAdvances(ReadOnlySpan<ushort> P_0)
	{
		uint[] array = new uint[P_0.Length];
		for (int i = 0; i < P_0.Length; i++)
		{
			array[i] = P_0[i];
		}
		return Font.GetHorizontalGlyphAdvances(array);
	}

	private static FontStyle GetFontStyle(OS2Table.FontStyleSelection P_0)
	{
		if ((P_0 & OS2Table.FontStyleSelection.ITALIC) != 0)
		{
			return FontStyle.Italic;
		}
		if ((P_0 & OS2Table.FontStyleSelection.OBLIQUE) != 0)
		{
			return FontStyle.Oblique;
		}
		return FontStyle.Normal;
	}

	private Blob? GetTable(Face face, Tag tag)
	{
		int tableSize = SKTypeface.GetTableSize(tag);
		nint data = Marshal.AllocCoTaskMem(tableSize);
		ReleaseDelegate releaseDelegate = delegate
		{
			Marshal.FreeCoTaskMem(data);
		};
		if (!SKTypeface.TryGetTableData(tag, 0, tableSize, data))
		{
			return null;
		}
		return new Blob(data, tableSize, MemoryMode.ReadOnly, releaseDelegate);
	}

	public SKFont CreateSKFont(float P_0)
	{
		return new SKFont(SKTypeface, P_0, 1f, ((FontSimulations & FontSimulations.Oblique) != FontSimulations.None) ? (-0.3f) : 0f)
		{
			LinearMetrics = true,
			Embolden = ((FontSimulations & FontSimulations.Bold) != 0)
		};
	}

	private void Dispose(bool P_0)
	{
		if (!_isDisposed)
		{
			_isDisposed = true;
			if (P_0)
			{
				Font.Dispose();
				Face.Dispose();
				SKTypeface.Dispose();
			}
		}
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	public bool TryGetTable(uint P_0, out byte[] P_1)
	{
		return SKTypeface.TryGetTableData(P_0, out P_1);
	}

	public bool TryGetStream([NotNullWhen(true)] out Stream? P_0)
	{
		try
		{
			SKStreamAsset sKStreamAsset = SKTypeface.OpenStream();
			int length = sKStreamAsset.Length;
			byte[] array = new byte[length];
			sKStreamAsset.Read(array, length);
			P_0 = new MemoryStream(array);
			return true;
		}
		catch
		{
			P_0 = null;
			return false;
		}
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.IDrawableBitmapImpl
using System;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

internal interface IDrawableBitmapImpl : IBitmapImpl, IDisposable
{
	void Draw(DrawingContextImpl P_0, SKRect P_1, SKRect P_2, SKPaint P_3);
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.IGlSkiaSpecificOptionsFeature
using Avalonia.Metadata;

[PrivateApi]
public interface IGlSkiaSpecificOptionsFeature
{
	bool UseNativeSkiaGrGlInterface { get; }
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.ImmutableBitmap
using System;
using System.IO;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Skia;
using Avalonia.Skia.Helpers;
using SkiaSharp;

internal class ImmutableBitmap : IDrawableBitmapImpl, IBitmapImpl, IDisposable, IReadableBitmapWithAlphaImpl, IReadableBitmapImpl
{
	private readonly SKImage _image;

	private readonly SKBitmap? _bitmap;

	private readonly Action? _customImageDispose;

	[CompilerGenerated]
	private readonly int _003CVersion_003Ek__BackingField = 1;

	public Vector Dpi { get; }

	public PixelSize PixelSize { get; }

	public PixelFormat? Format => _bitmap?.ColorType.ToAvalonia();

	public AlphaFormat? AlphaFormat => _bitmap?.AlphaType.ToAlphaFormat();

	public ImmutableBitmap(Stream P_0)
	{
		using SKManagedStream sKManagedStream = new SKManagedStream(P_0);
		using (SKData sKData = SKData.Create(sKManagedStream))
		{
			_bitmap = SKBitmap.Decode(sKData);
		}
		if (_bitmap == null)
		{
			throw new ArgumentException("Unable to load bitmap from provided data");
		}
		_bitmap.SetImmutable();
		_image = SKImage.FromBitmap(_bitmap);
		PixelSize = new PixelSize(_image.Width, _image.Height);
		Dpi = new Vector(96.0, 96.0);
	}

	public ImmutableBitmap(ImmutableBitmap P_0, PixelSize P_1, BitmapInterpolationMode P_2)
	{
		SKImageInfo sKImageInfo = new SKImageInfo(P_1.Width, P_1.Height, SKColorType.Bgra8888);
		_bitmap = new SKBitmap(sKImageInfo);
		P_0._image.ScalePixels(_bitmap.PeekPixels(), P_2.ToSKFilterQuality());
		_bitmap.SetImmutable();
		_image = SKImage.FromBitmap(_bitmap);
		PixelSize = new PixelSize(_image.Width, _image.Height);
		Dpi = new Vector(96.0, 96.0);
	}

	public ImmutableBitmap(Stream P_0, int P_1, bool P_2, BitmapInterpolationMode P_3)
	{
		using SKManagedStream sKManagedStream = new SKManagedStream(P_0);
		using SKData sKData = SKData.Create(sKManagedStream);
		using SKCodec sKCodec = SKCodec.Create(sKData);
		SKImageInfo info = sKCodec.Info;
		SKSizeI scaledDimensions = sKCodec.GetScaledDimensions(P_2 ? ((float)P_1 / (float)info.Width) : ((float)P_1 / (float)info.Height));
		SKImageInfo sKImageInfo = new SKImageInfo(scaledDimensions.Width, scaledDimensions.Height);
		_bitmap = SKBitmap.Decode(sKCodec, sKImageInfo);
		if (_bitmap == null)
		{
			throw new ArgumentException("Unable to load bitmap from provided data");
		}
		double num = (P_2 ? ((double)info.Height / (double)info.Width) : ((double)info.Width / (double)info.Height));
		SKImageInfo sKImageInfo2 = ((!P_2) ? new SKImageInfo((int)(num * (double)P_1), P_1) : new SKImageInfo(P_1, (int)(num * (double)P_1)));
		if (_bitmap.Width != sKImageInfo2.Width || _bitmap.Height != sKImageInfo2.Height)
		{
			SKBitmap bitmap = _bitmap.Resize(sKImageInfo2, P_3.ToSKFilterQuality());
			_bitmap.Dispose();
			_bitmap = bitmap;
		}
		_bitmap.SetImmutable();
		_image = SKImage.FromBitmap(_bitmap);
		if (_image == null)
		{
			throw new ArgumentException("Unable to load bitmap from provided data");
		}
		PixelSize = new PixelSize(_image.Width, _image.Height);
		Dpi = new Vector(96.0, 96.0);
	}

	public ImmutableBitmap(PixelSize P_0, Vector P_1, int P_2, PixelFormat P_3, AlphaFormat P_4, nint P_5)
	{
		using (SKBitmap sKBitmap = new SKBitmap())
		{
			sKBitmap.InstallPixels(new SKImageInfo(P_0.Width, P_0.Height, P_3.ToSkColorType(), P_4.ToSkAlphaType()), P_5, P_2);
			_bitmap = sKBitmap.Copy();
		}
		_bitmap.SetImmutable();
		_image = SKImage.FromBitmap(_bitmap);
		if (_image == null)
		{
			throw new ArgumentException("Unable to create bitmap from provided data");
		}
		PixelSize = P_0;
		Dpi = P_1;
	}

	public void Dispose()
	{
		if (_customImageDispose != null)
		{
			_customImageDispose();
		}
		else
		{
			_image.Dispose();
		}
		_bitmap?.Dispose();
	}

	public void Save(string P_0, int? P_1 = null)
	{
		ImageSavingHelper.SaveImage(_image, P_0, P_1);
	}

	public void Save(Stream P_0, int? P_1 = null)
	{
		ImageSavingHelper.SaveImage(_image, P_0, P_1);
	}

	public void Draw(DrawingContextImpl P_0, SKRect P_1, SKRect P_2, SKPaint P_3)
	{
		P_0.Canvas.DrawImage(_image, P_1, P_2, P_3);
	}

	public ILockedFramebuffer Lock()
	{
		if (_bitmap == null)
		{
			throw new NotSupportedException("A bitmap is needed for locking");
		}
		PixelFormat pixelFormat = _bitmap.ColorType.ToAvalonia() ?? throw new NotSupportedException($"Unsupported format {_bitmap.ColorType}");
		return new LockedFramebuffer(_bitmap.GetPixels(), PixelSize, _bitmap.RowBytes, Dpi, pixelFormat, null);
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.ISkiaGpu
using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Skia;

public interface ISkiaGpu : IPlatformGraphicsContext, IDisposable, IOptionalFeatureProvider
{
	ISkiaGpuRenderTarget? TryCreateRenderTarget(IEnumerable<object> P_0);

	ISkiaSurface? TryCreateSurface(PixelSize P_0, ISkiaGpuRenderSession? P_1);
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.ISkiaGpuRenderSession
using System;
using SkiaSharp;

public interface ISkiaGpuRenderSession : IDisposable
{
	GRContext GrContext { get; }

	SKSurface SkSurface { get; }

	double ScaleFactor { get; }

	GRSurfaceOrigin SurfaceOrigin { get; }
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.ISkiaGpuRenderTarget
using System;
using Avalonia.Skia;

public interface ISkiaGpuRenderTarget : IDisposable
{
	bool IsCorrupted { get; }

	ISkiaGpuRenderSession BeginRenderingSession();
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.ISkiaGpuRenderTarget2
using System;
using Avalonia;
using Avalonia.Metadata;
using Avalonia.Skia;

[PrivateApi]
public interface ISkiaGpuRenderTarget2 : ISkiaGpuRenderTarget, IDisposable
{
	ISkiaGpuRenderSession BeginRenderingSession(PixelSize P_0);
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.ISkiaGpuWithPlatformGraphicsContext
using System;
using Avalonia.Metadata;
using Avalonia.Platform;
using Avalonia.Skia;

[Unstable]
public interface ISkiaGpuWithPlatformGraphicsContext : ISkiaGpu, IPlatformGraphicsContext, IDisposable, IOptionalFeatureProvider
{
	IPlatformGraphicsContext? PlatformGraphicsContext { get; }
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.ISkiaSharpApiLease
using System;
using Avalonia.Metadata;
using Avalonia.Skia;
using SkiaSharp;

[Unstable]
public interface ISkiaSharpApiLease : IDisposable
{
	SKCanvas SkCanvas { get; }

	GRContext? GrContext { get; }

	ISkiaSharpPlatformGraphicsApiLease? TryLeasePlatformGraphicsApi();
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.ISkiaSharpApiLeaseFeature
using Avalonia.Metadata;
using Avalonia.Skia;

[Unstable]
public interface ISkiaSharpApiLeaseFeature
{
	ISkiaSharpApiLease Lease();
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.ISkiaSharpPlatformGraphicsApiLease
using System;
using Avalonia.Metadata;
using Avalonia.Platform;

[Unstable]
public interface ISkiaSharpPlatformGraphicsApiLease : IDisposable
{
	IPlatformGraphicsContext Context { get; }
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.ISkiaSurface
using System;
using SkiaSharp;

public interface ISkiaSurface : IDisposable
{
	SKSurface Surface { get; }

	bool CanBlit { get; }

	void Blit(SKCanvas P_0);
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.LineGeometryImpl
using System;
using Avalonia;
using Avalonia.Skia;
using SkiaSharp;

internal class LineGeometryImpl : GeometryImpl
{
	public override Rect Bounds { get; }

	public override SKPath StrokePath { get; }

	public override SKPath? FillPath => null;

	public LineGeometryImpl(Point P_0, Point P_1)
	{
		SKPath sKPath = new SKPath();
		sKPath.MoveTo(P_0.ToSKPoint());
		sKPath.LineTo(P_1.ToSKPoint());
		StrokePath = sKPath;
		Bounds = new Rect(new Point(Math.Min(P_0.X, P_1.X), Math.Min(P_0.Y, P_1.Y)), new Point(Math.Max(P_0.X, P_1.X), Math.Max(P_0.Y, P_1.Y)));
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.PictureRenderTarget
using System;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Reactive;
using Avalonia.Skia;
using SkiaSharp;

internal class PictureRenderTarget : IDisposable
{
	private readonly ISkiaGpu? _gpu;

	private readonly GRContext? _grContext;

	private readonly Vector _dpi;

	private SKPicture? _picture;

	public PictureRenderTarget(ISkiaGpu? P_0, GRContext? P_1, Vector P_2)
	{
		_gpu = P_0;
		_grContext = P_1;
		_dpi = P_2;
	}

	public SKPicture GetPicture()
	{
		SKPicture? result = _picture ?? throw new InvalidOperationException();
		_picture = null;
		return result;
	}

	public IDrawingContextImpl CreateDrawingContext(Size P_0, bool P_1 = true)
	{
		if (P_1)
		{
			P_0 *= _dpi / 96.0;
		}
		SKPictureRecorder recorder = new SKPictureRecorder();
		SKCanvas canvas = recorder.BeginRecording(new SKRect(0f, 0f, (float)P_0.Width, (float)P_0.Height));
		canvas.RestoreToCount(-1);
		canvas.ResetMatrix();
		return new DrawingContextImpl(new DrawingContextImpl.CreateInfo
		{
			Canvas = canvas,
			ScaleDrawingToDpi = P_1,
			Dpi = _dpi,
			DisableSubpixelTextRendering = true,
			GrContext = _grContext,
			Gpu = _gpu
		}, Disposable.Create(delegate
		{
			_picture = recorder.EndRecording();
			canvas.Dispose();
			recorder.Dispose();
		}));
	}

	public void Dispose()
	{
		_picture?.Dispose();
	}
}

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

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.RectangleGeometryImpl
using Avalonia;
using Avalonia.Skia;
using SkiaSharp;

internal class RectangleGeometryImpl : GeometryImpl
{
	public override Rect Bounds { get; }

	public override SKPath StrokePath { get; }

	public override SKPath? FillPath => StrokePath;

	public RectangleGeometryImpl(Rect P_0)
	{
		SKPath sKPath = new SKPath();
		sKPath.AddRect(P_0.ToSKRect());
		StrokePath = sKPath;
		Bounds = P_0;
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.RenderTargetBitmapImpl
using System;
using Avalonia;
using Avalonia.Controls.Platform.Surfaces;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

internal class RenderTargetBitmapImpl : WriteableBitmapImpl, IRenderTargetBitmapImpl, IBitmapImpl, IDisposable, IRenderTarget, IFramebufferPlatformSurface
{
	private readonly FramebufferRenderTarget _renderTarget;

	public bool IsCorrupted => false;

	public RenderTargetBitmapImpl(PixelSize P_0, Vector P_1)
		: base(P_0, P_1, (SKImageInfo.PlatformColorType == SKColorType.Rgba8888) ? PixelFormats.Rgba8888 : PixelFormat.Bgra8888, Avalonia.Platform.AlphaFormat.Premul)
	{
		_renderTarget = new FramebufferRenderTarget(this);
	}

	IDrawingContextImpl IRenderTarget.CreateDrawingContext(bool P_0)
	{
		return _renderTarget.CreateDrawingContext(P_0);
	}

	public override void Dispose()
	{
		_renderTarget.Dispose();
		base.Dispose();
	}

	public IFramebufferRenderTarget CreateFramebufferRenderTarget()
	{
		return new FuncFramebufferRenderTarget(Lock);
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.SKCacheBase<TCachedItem,TCache>
using System;
using System.Collections.Concurrent;

internal abstract class SKCacheBase<TCachedItem, TCache> where TCachedItem : IDisposable, new() where TCache : new()
{
	protected readonly ConcurrentBag<TCachedItem> Cache;

	public static readonly TCache Shared = new TCache();

	protected SKCacheBase()
	{
		Cache = new ConcurrentBag<TCachedItem>();
	}

	public TCachedItem Get()
	{
		if (!Cache.TryTake(out var result))
		{
			return new TCachedItem();
		}
		return result;
	}

	public void Return(TCachedItem P_0)
	{
		Cache.Add(P_0);
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.SkiaContext
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls.Platform.Surfaces;
using Avalonia.OpenGL;
using Avalonia.Platform;
using Avalonia.Skia;

internal class SkiaContext : IPlatformRenderInterfaceContext, IOptionalFeatureProvider, IDisposable
{
	private ISkiaGpu? _gpu;

	public IReadOnlyDictionary<Type, object> PublicFeatures { get; }

	public SkiaContext(ISkiaGpu? P_0)
	{
		_gpu = P_0;
		Dictionary<Type, object> features = new Dictionary<Type, object>();
		if (P_0 != null)
		{
			TryFeature<IOpenGlTextureSharingRenderInterfaceContextFeature>();
			TryFeature<IExternalObjectsRenderInterfaceContextFeature>();
		}
		PublicFeatures = features;
		void TryFeature<T>() where T : class
		{
			T val = P_0.TryGetFeature<T>();
			if (val != null)
			{
				features.Add(typeof(T), val);
			}
		}
	}

	public void Dispose()
	{
		_gpu?.Dispose();
		_gpu = null;
	}

	public IRenderTarget CreateRenderTarget(IEnumerable<object> P_0)
	{
		if (!(P_0 is IList))
		{
			P_0 = P_0.ToList();
		}
		ISkiaGpuRenderTarget skiaGpuRenderTarget = _gpu?.TryCreateRenderTarget(P_0);
		if (skiaGpuRenderTarget != null)
		{
			return new SkiaGpuRenderTarget(_gpu, skiaGpuRenderTarget);
		}
		foreach (object item in P_0)
		{
			if (item is IFramebufferPlatformSurface framebufferPlatformSurface)
			{
				return new FramebufferRenderTarget(framebufferPlatformSurface);
			}
		}
		throw new NotSupportedException("Don't know how to create a Skia render target from any of provided surfaces");
	}

	public object? TryGetFeature(Type P_0)
	{
		return _gpu?.TryGetFeature(P_0);
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.SkiaGpuRenderTarget
using System;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Skia;

internal class SkiaGpuRenderTarget : IRenderTarget2, IRenderTarget, IDisposable
{
	private readonly ISkiaGpu _skiaGpu;

	private readonly ISkiaGpuRenderTarget _renderTarget;

	public bool IsCorrupted => _renderTarget.IsCorrupted;

	public RenderTargetProperties Properties { get; }

	public SkiaGpuRenderTarget(ISkiaGpu P_0, ISkiaGpuRenderTarget P_1)
	{
		_skiaGpu = P_0;
		_renderTarget = P_1;
	}

	public void Dispose()
	{
		_renderTarget.Dispose();
	}

	public IDrawingContextImpl CreateDrawingContext(PixelSize P_0, out RenderTargetDrawingContextProperties P_1)
	{
		return CreateDrawingContextCore(P_0, false, out P_1);
	}

	public IDrawingContextImpl CreateDrawingContext(bool P_0)
	{
		RenderTargetDrawingContextProperties renderTargetDrawingContextProperties;
		return CreateDrawingContextCore(null, P_0, out renderTargetDrawingContextProperties);
	}

	private IDrawingContextImpl CreateDrawingContextCore(PixelSize? P_0, bool P_1, out RenderTargetDrawingContextProperties P_2)
	{
		P_2 = default(RenderTargetDrawingContextProperties);
		ISkiaGpuRenderSession skiaGpuRenderSession = ((P_0.HasValue && _renderTarget is ISkiaGpuRenderTarget2 skiaGpuRenderTarget) ? skiaGpuRenderTarget.BeginRenderingSession(P_0.Value) : _renderTarget.BeginRenderingSession());
		return new DrawingContextImpl(new DrawingContextImpl.CreateInfo
		{
			GrContext = skiaGpuRenderSession.GrContext,
			Surface = skiaGpuRenderSession.SkSurface,
			Dpi = SkiaPlatform.DefaultDpi * skiaGpuRenderSession.ScaleFactor,
			ScaleDrawingToDpi = P_1,
			Gpu = _skiaGpu,
			CurrentSession = skiaGpuRenderSession
		}, skiaGpuRenderSession);
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.SkiaPlatform
using Avalonia;
using Avalonia.Platform;
using Avalonia.Skia;

public static class SkiaPlatform
{
	public static Vector DefaultDpi => new Vector(96.0, 96.0);

	public static void Initialize(SkiaOptions P_0)
	{
		PlatformRenderInterface platformRenderInterface = new PlatformRenderInterface(P_0.MaxGpuResourceSizeBytes);
		AvaloniaLocator.CurrentMutable.Bind<IPlatformRenderInterface>().ToConstant(platformRenderInterface).Bind<IFontManagerImpl>()
			.ToConstant(new FontManagerImpl())
			.Bind<ITextShaperImpl>()
			.ToConstant(new TextShaperImpl());
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.SkiaRegionImpl
using System;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

internal class SkiaRegionImpl : IPlatformRenderInterfaceRegion, IDisposable
{
	private SKRegion? _region = new SKRegion();

	private bool _rectsValid;

	public SKRegion Region => _region ?? throw new ObjectDisposedException("SkiaRegionImpl");

	public bool IsEmpty => Region.IsEmpty;

	public LtrbPixelRect Bounds => Region.Bounds.ToAvaloniaLtrbPixelRect();

	public void Dispose()
	{
		_region?.Dispose();
		_region = null;
	}

	public void AddRect(LtrbPixelRect P_0)
	{
		_rectsValid = false;
		Region.Op(P_0.Left, P_0.Top, P_0.Right, P_0.Bottom, SKRegionOperation.Union);
	}

	public void Reset()
	{
		_rectsValid = false;
		Region.SetEmpty();
	}

	public bool Intersects(LtrbRect P_0)
	{
		return Region.Intersects(new SKRectI((int)P_0.Left, (int)P_0.Top, (int)Math.Ceiling(P_0.Right), (int)Math.Ceiling(P_0.Bottom)));
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.SkiaSharpExtensions
using System;
using System.Diagnostics.CodeAnalysis;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using SkiaSharp;

public static class SkiaSharpExtensions
{
	public static SKFilterQuality ToSKFilterQuality(this BitmapInterpolationMode P_0)
	{
		switch (P_0)
		{
		case BitmapInterpolationMode.Unspecified:
		case BitmapInterpolationMode.LowQuality:
			return SKFilterQuality.Low;
		case BitmapInterpolationMode.MediumQuality:
			return SKFilterQuality.Medium;
		case BitmapInterpolationMode.HighQuality:
			return SKFilterQuality.High;
		case BitmapInterpolationMode.None:
			return SKFilterQuality.None;
		default:
			throw new ArgumentOutOfRangeException("interpolationMode", P_0, null);
		}
	}

	public static SKBlendMode ToSKBlendMode(this BitmapBlendingMode P_0)
	{
		return P_0 switch
		{
			BitmapBlendingMode.Unspecified => SKBlendMode.SrcOver, 
			BitmapBlendingMode.SourceOver => SKBlendMode.SrcOver, 
			BitmapBlendingMode.Source => SKBlendMode.Src, 
			BitmapBlendingMode.SourceIn => SKBlendMode.SrcIn, 
			BitmapBlendingMode.SourceOut => SKBlendMode.SrcOut, 
			BitmapBlendingMode.SourceAtop => SKBlendMode.SrcATop, 
			BitmapBlendingMode.Destination => SKBlendMode.Dst, 
			BitmapBlendingMode.DestinationIn => SKBlendMode.DstIn, 
			BitmapBlendingMode.DestinationOut => SKBlendMode.DstOut, 
			BitmapBlendingMode.DestinationOver => SKBlendMode.DstOver, 
			BitmapBlendingMode.DestinationAtop => SKBlendMode.DstATop, 
			BitmapBlendingMode.Xor => SKBlendMode.Xor, 
			BitmapBlendingMode.Plus => SKBlendMode.Plus, 
			BitmapBlendingMode.Screen => SKBlendMode.Screen, 
			BitmapBlendingMode.Overlay => SKBlendMode.Overlay, 
			BitmapBlendingMode.Darken => SKBlendMode.Darken, 
			BitmapBlendingMode.Lighten => SKBlendMode.Lighten, 
			BitmapBlendingMode.ColorDodge => SKBlendMode.ColorDodge, 
			BitmapBlendingMode.ColorBurn => SKBlendMode.ColorBurn, 
			BitmapBlendingMode.HardLight => SKBlendMode.HardLight, 
			BitmapBlendingMode.SoftLight => SKBlendMode.SoftLight, 
			BitmapBlendingMode.Difference => SKBlendMode.Difference, 
			BitmapBlendingMode.Exclusion => SKBlendMode.Exclusion, 
			BitmapBlendingMode.Multiply => SKBlendMode.Multiply, 
			BitmapBlendingMode.Hue => SKBlendMode.Hue, 
			BitmapBlendingMode.Saturation => SKBlendMode.Saturation, 
			BitmapBlendingMode.Color => SKBlendMode.Color, 
			BitmapBlendingMode.Luminosity => SKBlendMode.Luminosity, 
			_ => throw new ArgumentOutOfRangeException("blendingMode", P_0, null), 
		};
	}

	public static SKPoint ToSKPoint(this Point P_0)
	{
		return new SKPoint((float)P_0.X, (float)P_0.Y);
	}

	public static SKPoint ToSKPoint(this Vector P_0)
	{
		return new SKPoint((float)P_0.X, (float)P_0.Y);
	}

	public static SKRect ToSKRect(this Rect P_0)
	{
		return new SKRect((float)P_0.X, (float)P_0.Y, (float)P_0.Right, (float)P_0.Bottom);
	}

	public static Rect ToAvaloniaRect(this SKRect P_0)
	{
		return new Rect(P_0.Left, P_0.Top, P_0.Right - P_0.Left, P_0.Bottom - P_0.Top);
	}

	internal static LtrbPixelRect ToAvaloniaLtrbPixelRect(this SKRectI P_0)
	{
		return new LtrbPixelRect(P_0.Left, P_0.Top, P_0.Right, P_0.Bottom);
	}

	public static SKMatrix ToSKMatrix(this Matrix P_0)
	{
		return new SKMatrix
		{
			ScaleX = (float)P_0.M11,
			SkewX = (float)P_0.M21,
			TransX = (float)P_0.M31,
			SkewY = (float)P_0.M12,
			ScaleY = (float)P_0.M22,
			TransY = (float)P_0.M32,
			Persp0 = (float)P_0.M13,
			Persp1 = (float)P_0.M23,
			Persp2 = (float)P_0.M33
		};
	}

	internal static Matrix ToAvaloniaMatrix(this SKMatrix P_0)
	{
		return new Matrix(P_0.ScaleX, P_0.SkewY, P_0.Persp0, P_0.SkewX, P_0.ScaleY, P_0.Persp1, P_0.TransX, P_0.TransY, P_0.Persp2);
	}

	public static SKColor ToSKColor(this Color P_0)
	{
		return new SKColor(P_0.R, P_0.G, P_0.B, P_0.A);
	}

	public static SKColorType ToSkColorType(this PixelFormat P_0)
	{
		if (P_0 == PixelFormat.Rgb565)
		{
			return SKColorType.Rgb565;
		}
		if (P_0 == PixelFormat.Bgra8888)
		{
			return SKColorType.Bgra8888;
		}
		if (P_0 == PixelFormat.Rgba8888)
		{
			return SKColorType.Rgba8888;
		}
		if (P_0 == PixelFormat.Rgb32)
		{
			return SKColorType.Rgb888x;
		}
		PixelFormat pixelFormat = P_0;
		throw new ArgumentException("Unknown pixel format: " + pixelFormat.ToString());
	}

	public static PixelFormat? ToAvalonia(this SKColorType P_0)
	{
		return P_0 switch
		{
			SKColorType.Rgb565 => PixelFormats.Rgb565, 
			SKColorType.Bgra8888 => PixelFormats.Bgra8888, 
			SKColorType.Rgba8888 => PixelFormats.Rgba8888, 
			SKColorType.Rgb888x => PixelFormats.Rgb32, 
			_ => null, 
		};
	}

	public static PixelFormat ToPixelFormat(this SKColorType P_0)
	{
		return P_0 switch
		{
			SKColorType.Rgb565 => PixelFormat.Rgb565, 
			SKColorType.Bgra8888 => PixelFormat.Bgra8888, 
			SKColorType.Rgba8888 => PixelFormat.Rgba8888, 
			_ => throw new ArgumentException("Unknown pixel format: " + P_0), 
		};
	}

	public static SKAlphaType ToSkAlphaType(this AlphaFormat P_0)
	{
		return P_0 switch
		{
			AlphaFormat.Premul => SKAlphaType.Premul, 
			AlphaFormat.Unpremul => SKAlphaType.Unpremul, 
			AlphaFormat.Opaque => SKAlphaType.Opaque, 
			_ => throw new ArgumentException($"Unknown alpha format: {P_0}"), 
		};
	}

	public static AlphaFormat ToAlphaFormat(this SKAlphaType P_0)
	{
		return P_0 switch
		{
			SKAlphaType.Premul => AlphaFormat.Premul, 
			SKAlphaType.Unpremul => AlphaFormat.Unpremul, 
			SKAlphaType.Opaque => AlphaFormat.Opaque, 
			_ => throw new ArgumentException($"Unknown alpha format: {P_0}"), 
		};
	}

	public static SKShaderTileMode ToSKShaderTileMode(this GradientSpreadMethod P_0)
	{
		return P_0 switch
		{
			GradientSpreadMethod.Reflect => SKShaderTileMode.Mirror, 
			GradientSpreadMethod.Repeat => SKShaderTileMode.Repeat, 
			_ => SKShaderTileMode.Clamp, 
		};
	}

	public static SKStrokeCap ToSKStrokeCap(this PenLineCap P_0)
	{
		return P_0 switch
		{
			PenLineCap.Round => SKStrokeCap.Round, 
			PenLineCap.Square => SKStrokeCap.Square, 
			_ => SKStrokeCap.Butt, 
		};
	}

	public static SKStrokeJoin ToSKStrokeJoin(this PenLineJoin P_0)
	{
		return P_0 switch
		{
			PenLineJoin.Bevel => SKStrokeJoin.Bevel, 
			PenLineJoin.Round => SKStrokeJoin.Round, 
			_ => SKStrokeJoin.Miter, 
		};
	}

	public static FontStyle ToAvalonia(this SKFontStyleSlant P_0)
	{
		return P_0 switch
		{
			SKFontStyleSlant.Upright => FontStyle.Normal, 
			SKFontStyleSlant.Italic => FontStyle.Italic, 
			SKFontStyleSlant.Oblique => FontStyle.Oblique, 
			_ => throw new ArgumentOutOfRangeException("slant", P_0, null), 
		};
	}

	public static SKFontStyleSlant ToSkia(this FontStyle P_0)
	{
		return P_0 switch
		{
			FontStyle.Normal => SKFontStyleSlant.Upright, 
			FontStyle.Italic => SKFontStyleSlant.Italic, 
			FontStyle.Oblique => SKFontStyleSlant.Oblique, 
			_ => throw new ArgumentOutOfRangeException("style", P_0, null), 
		};
	}

	[return: NotNullIfNotNull("src")]
	public static SKPath? Clone(this SKPath? P_0)
	{
		if (P_0 == null)
		{
			return null;
		}
		return new SKPath(P_0);
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.SKPaintCache
using Avalonia.Skia;
using SkiaSharp;

internal class SKPaintCache : SKCacheBase<SKPaint, SKPaintCache>
{
	public void ReturnReset(SKPaint P_0)
	{
		P_0.Reset();
		Cache.Add(P_0);
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.SKRoundRectCache
using System.Collections.Concurrent;
using Avalonia;
using Avalonia.Skia;
using SkiaSharp;

internal class SKRoundRectCache : SKCacheBase<SKRoundRect, SKRoundRectCache>
{
	private readonly ConcurrentBag<SKPoint[]> _radiiCache = new ConcurrentBag<SKPoint[]>();

	public SKRoundRect GetAndSetRadii(in SKRect P_0, in RoundedRect P_1)
	{
		if (!Cache.TryTake(out SKRoundRect sKRoundRect))
		{
			sKRoundRect = new SKRoundRect();
		}
		if (!_radiiCache.TryTake(out SKPoint[] array))
		{
			array = new SKPoint[4];
		}
		array[0].X = (float)P_1.RadiiTopLeft.X;
		array[0].Y = (float)P_1.RadiiTopLeft.Y;
		array[1].X = (float)P_1.RadiiTopRight.X;
		array[1].Y = (float)P_1.RadiiTopRight.Y;
		array[2].X = (float)P_1.RadiiBottomRight.X;
		array[2].Y = (float)P_1.RadiiBottomRight.Y;
		array[3].X = (float)P_1.RadiiBottomLeft.X;
		array[3].Y = (float)P_1.RadiiBottomLeft.Y;
		sKRoundRect.SetRectRadii(P_0, array);
		_radiiCache.Add(array);
		return sKRoundRect;
	}

	public SKRoundRect GetAndSetRadii(in SKRect P_0, in SKPoint[] P_1)
	{
		if (!Cache.TryTake(out SKRoundRect sKRoundRect))
		{
			sKRoundRect = new SKRoundRect();
		}
		sKRoundRect.SetRectRadii(P_0, P_1);
		return sKRoundRect;
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.SKTextBlobBuilderCache
using Avalonia.Skia;
using SkiaSharp;

internal class SKTextBlobBuilderCache : SKCacheBase<SKTextBlobBuilder, SKTextBlobBuilderCache>
{
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.StreamGeometryImpl
using System;
using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

internal class StreamGeometryImpl : GeometryImpl, IStreamGeometryImpl, IGeometryImpl
{
	private class StreamContext : IStreamGeometryContextImpl, IGeometryContext, IDisposable, IGeometryContext2
	{
		private readonly StreamGeometryImpl _geometryImpl;

		private bool _isFilled;

		private Point _startPoint;

		private bool _isFigureBroken;

		private SKPath Stroke => _geometryImpl._strokePath;

		private SKPath Fill
		{
			get
			{
				StreamGeometryImpl geometryImpl = _geometryImpl;
				return geometryImpl._fillPath ?? (geometryImpl._fillPath = new SKPath());
			}
		}

		private bool Duplicate
		{
			get
			{
				if (_isFilled)
				{
					return _geometryImpl._fillPath != Stroke;
				}
				return false;
			}
		}

		private void EnsureSeparateFillPath()
		{
			if (Stroke == Fill)
			{
				_geometryImpl._fillPath = Stroke.Clone();
			}
		}

		private void BreakFigure()
		{
			if (!_isFigureBroken)
			{
				_isFigureBroken = true;
				EnsureSeparateFillPath();
			}
		}

		public StreamContext(StreamGeometryImpl P_0)
		{
			_geometryImpl = P_0;
		}

		public void Dispose()
		{
			_geometryImpl._bounds = Stroke.TightBounds.ToAvaloniaRect();
			_geometryImpl.InvalidateCaches();
		}

		public void ArcTo(Point P_0, Size P_1, double P_2, bool P_3, SweepDirection P_4)
		{
			SKPathArcSize sKPathArcSize = (P_3 ? SKPathArcSize.Large : SKPathArcSize.Small);
			SKPathDirection sKPathDirection = ((P_4 != SweepDirection.Clockwise) ? SKPathDirection.CounterClockwise : SKPathDirection.Clockwise);
			Stroke.ArcTo((float)P_1.Width, (float)P_1.Height, (float)P_2, sKPathArcSize, sKPathDirection, (float)P_0.X, (float)P_0.Y);
			if (Duplicate)
			{
				Fill.ArcTo((float)P_1.Width, (float)P_1.Height, (float)P_2, sKPathArcSize, sKPathDirection, (float)P_0.X, (float)P_0.Y);
			}
		}

		public void BeginFigure(Point P_0, bool P_1)
		{
			if (!P_1)
			{
				EnsureSeparateFillPath();
			}
			_isFilled = P_1;
			_startPoint = P_0;
			_isFigureBroken = false;
			Stroke.MoveTo((float)P_0.X, (float)P_0.Y);
			if (Duplicate)
			{
				Fill.MoveTo((float)P_0.X, (float)P_0.Y);
			}
		}

		public void CubicBezierTo(Point P_0, Point P_1, Point P_2)
		{
			Stroke.CubicTo((float)P_0.X, (float)P_0.Y, (float)P_1.X, (float)P_1.Y, (float)P_2.X, (float)P_2.Y);
			if (Duplicate)
			{
				Fill.CubicTo((float)P_0.X, (float)P_0.Y, (float)P_1.X, (float)P_1.Y, (float)P_2.X, (float)P_2.Y);
			}
		}

		public void QuadraticBezierTo(Point P_0, Point P_1)
		{
			Stroke.QuadTo((float)P_0.X, (float)P_0.Y, (float)P_1.X, (float)P_1.Y);
			if (Duplicate)
			{
				Fill.QuadTo((float)P_0.X, (float)P_0.Y, (float)P_1.X, (float)P_1.Y);
			}
		}

		public void LineTo(Point P_0)
		{
			Stroke.LineTo((float)P_0.X, (float)P_0.Y);
			if (Duplicate)
			{
				Fill.LineTo((float)P_0.X, (float)P_0.Y);
			}
		}

		public void EndFigure(bool P_0)
		{
			if (P_0)
			{
				if (_isFigureBroken)
				{
					Stroke.LineTo(_startPoint.ToSKPoint());
					_isFigureBroken = false;
				}
				else
				{
					Stroke.Close();
				}
				if (Duplicate)
				{
					Fill.Close();
				}
			}
		}

		public void SetFillRule(FillRule P_0)
		{
			Fill.FillType = ((P_0 == FillRule.EvenOdd) ? SKPathFillType.EvenOdd : SKPathFillType.Winding);
		}

		public void LineTo(Point P_0, bool P_1)
		{
			if (P_1)
			{
				Stroke.LineTo((float)P_0.X, (float)P_0.Y);
			}
			else
			{
				BreakFigure();
				Stroke.MoveTo((float)P_0.X, (float)P_0.Y);
			}
			if (Duplicate)
			{
				Fill.LineTo((float)P_0.X, (float)P_0.Y);
			}
		}

		public void ArcTo(Point P_0, Size P_1, double P_2, bool P_3, SweepDirection P_4, bool P_5)
		{
			SKPathArcSize sKPathArcSize = (P_3 ? SKPathArcSize.Large : SKPathArcSize.Small);
			SKPathDirection sKPathDirection = ((P_4 != SweepDirection.Clockwise) ? SKPathDirection.CounterClockwise : SKPathDirection.Clockwise);
			if (P_5)
			{
				Stroke.ArcTo((float)P_1.Width, (float)P_1.Height, (float)P_2, sKPathArcSize, sKPathDirection, (float)P_0.X, (float)P_0.Y);
			}
			else
			{
				BreakFigure();
				Stroke.MoveTo((float)P_0.X, (float)P_0.Y);
			}
			if (Duplicate)
			{
				Fill.ArcTo((float)P_1.Width, (float)P_1.Height, (float)P_2, sKPathArcSize, sKPathDirection, (float)P_0.X, (float)P_0.Y);
			}
		}

		public void CubicBezierTo(Point P_0, Point P_1, Point P_2, bool P_3)
		{
			if (P_3)
			{
				Stroke.CubicTo((float)P_0.X, (float)P_0.Y, (float)P_1.X, (float)P_1.Y, (float)P_2.X, (float)P_2.Y);
			}
			else
			{
				BreakFigure();
				Stroke.MoveTo((float)P_2.X, (float)P_2.Y);
			}
			if (Duplicate)
			{
				Fill.CubicTo((float)P_0.X, (float)P_0.Y, (float)P_1.X, (float)P_1.Y, (float)P_2.X, (float)P_2.Y);
			}
		}

		public void QuadraticBezierTo(Point P_0, Point P_1, bool P_2)
		{
			if (P_2)
			{
				Stroke.QuadTo((float)P_0.X, (float)P_0.Y, (float)P_1.X, (float)P_1.Y);
			}
			else
			{
				BreakFigure();
				Stroke.MoveTo((float)P_1.X, (float)P_1.Y);
			}
			if (Duplicate)
			{
				Fill.QuadTo((float)P_0.X, (float)P_0.Y, (float)P_1.X, (float)P_1.Y);
			}
		}
	}

	private Rect _bounds;

	private readonly SKPath _strokePath;

	private SKPath? _fillPath;

	public override SKPath? StrokePath => _strokePath;

	public override SKPath? FillPath => _fillPath;

	public override Rect Bounds => _bounds;

	public StreamGeometryImpl(SKPath P_0, SKPath? P_1, Rect? P_2 = null)
	{
		_strokePath = P_0;
		_fillPath = P_1;
		_bounds = P_2 ?? P_0.TightBounds.ToAvaloniaRect();
	}

	private StreamGeometryImpl(SKPath P_0)
		: this(P_0, P_0, default(Rect))
	{
	}

	public StreamGeometryImpl()
		: this(CreateEmptyPath())
	{
	}

	public IStreamGeometryImpl Clone()
	{
		SKPath sKPath = _strokePath.Clone();
		SKPath sKPath2 = ((_fillPath == _strokePath) ? sKPath : _fillPath.Clone());
		return new StreamGeometryImpl(sKPath, sKPath2, Bounds);
	}

	public IStreamGeometryContextImpl Open()
	{
		return new StreamContext(this);
	}

	private static SKPath CreateEmptyPath()
	{
		return new SKPath
		{
			FillType = SKPathFillType.EvenOdd
		};
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.SurfaceRenderTarget
using System;
using System.IO;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Reactive;
using Avalonia.Skia;
using Avalonia.Skia.Helpers;
using SkiaSharp;

internal class SurfaceRenderTarget : IDrawingContextLayerImpl, IRenderTargetBitmapImpl, IBitmapImpl, IDisposable, IRenderTarget, IDrawableBitmapImpl
{
	private class SkiaSurfaceWrapper : ISkiaSurface, IDisposable
	{
		private SKSurface? _surface;

		public SKSurface Surface => _surface ?? throw new ObjectDisposedException("SkiaSurfaceWrapper");

		public bool CanBlit => false;

		public void Blit(SKCanvas P_0)
		{
			throw new NotSupportedException();
		}

		public SkiaSurfaceWrapper(SKSurface P_0)
		{
			_surface = P_0;
		}

		public void Dispose()
		{
			_surface?.Dispose();
			_surface = null;
		}
	}

	public struct CreateInfo
	{
		public int Width;

		public int Height;

		public Vector Dpi;

		public PixelFormat? Format;

		public bool DisableTextLcdRendering;

		public GRContext? GrContext;

		public ISkiaGpu? Gpu;

		public ISkiaGpuRenderSession? Session;

		public bool DisableManualFbo;
	}

	private readonly ISkiaSurface _surface;

	private readonly SKCanvas _canvas;

	private readonly bool _disableLcdRendering;

	private readonly GRContext? _grContext;

	private readonly ISkiaGpu? _gpu;

	[CompilerGenerated]
	private int _003CVersion_003Ek__BackingField;

	public bool IsCorrupted => _gpu?.IsLost ?? false;

	public Vector Dpi { get; }

	public PixelSize PixelSize { get; }

	public int Version
	{
		[CompilerGenerated]
		get
		{
			return _003CVersion_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CVersion_003Ek__BackingField = num;
		}
	}

	public bool CanBlit => true;

	public SurfaceRenderTarget(CreateInfo P_0)
	{
		Version = 1;
		base._002Ector();
		PixelSize = new PixelSize(P_0.Width, P_0.Height);
		Dpi = P_0.Dpi;
		_disableLcdRendering = P_0.DisableTextLcdRendering;
		_grContext = P_0.GrContext;
		_gpu = P_0.Gpu;
		ISkiaSurface skiaSurface = null;
		if (!P_0.DisableManualFbo)
		{
			skiaSurface = _gpu?.TryCreateSurface(PixelSize, P_0.Session);
		}
		if (skiaSurface == null)
		{
			SKSurface sKSurface = CreateSurface(P_0.GrContext, PixelSize.Width, PixelSize.Height, P_0.Format);
			if (sKSurface != null)
			{
				skiaSurface = new SkiaSurfaceWrapper(sKSurface);
			}
		}
		SKCanvas sKCanvas = skiaSurface?.Surface.Canvas;
		if (sKCanvas == null)
		{
			throw new InvalidOperationException("Failed to create Skia render target surface");
		}
		_surface = skiaSurface;
		_canvas = sKCanvas;
	}

	private static SKSurface? CreateSurface(GRContext? P_0, int P_1, int P_2, PixelFormat? P_3)
	{
		SKImageInfo sKImageInfo = MakeImageInfo(P_1, P_2, P_3);
		if (P_0 != null)
		{
			return SKSurface.Create(P_0, false, sKImageInfo, new SKSurfaceProperties(SKPixelGeometry.RgbHorizontal));
		}
		return SKSurface.Create(sKImageInfo, new SKSurfaceProperties(SKPixelGeometry.RgbHorizontal));
	}

	public void Dispose()
	{
		_canvas.Dispose();
		_surface.Dispose();
	}

	public IDrawingContextImpl CreateDrawingContext(bool P_0)
	{
		_canvas.RestoreToCount(-1);
		_canvas.ResetMatrix();
		return new DrawingContextImpl(new DrawingContextImpl.CreateInfo
		{
			Surface = _surface.Surface,
			Dpi = Dpi,
			ScaleDrawingToDpi = P_0,
			DisableSubpixelTextRendering = _disableLcdRendering,
			GrContext = _grContext,
			Gpu = _gpu
		}, Disposable.Create(delegate
		{
			Version++;
		}));
	}

	public void Save(string P_0, int? P_1 = null)
	{
		using SKImage sKImage = SnapshotImage();
		ImageSavingHelper.SaveImage(sKImage, P_0, P_1);
	}

	public void Save(Stream P_0, int? P_1 = null)
	{
		using SKImage sKImage = SnapshotImage();
		ImageSavingHelper.SaveImage(sKImage, P_0, P_1);
	}

	public void Blit(IDrawingContextImpl P_0)
	{
		DrawingContextImpl drawingContextImpl = (DrawingContextImpl)P_0;
		if (_surface.CanBlit)
		{
			_surface.Surface.Canvas.Flush();
			_surface.Blit(drawingContextImpl.Canvas);
			return;
		}
		SKMatrix totalMatrix = drawingContextImpl.Canvas.TotalMatrix;
		drawingContextImpl.Canvas.ResetMatrix();
		_surface.Surface.Draw(drawingContextImpl.Canvas, 0f, 0f, null);
		drawingContextImpl.Canvas.SetMatrix(totalMatrix);
	}

	public void Draw(DrawingContextImpl P_0, SKRect P_1, SKRect P_2, SKPaint P_3)
	{
		using SKImage sKImage = SnapshotImage();
		P_0.Canvas.DrawImage(sKImage, P_1, P_2, P_3);
	}

	public SKImage SnapshotImage()
	{
		return _surface.Surface.Snapshot();
	}

	private static SKImageInfo MakeImageInfo(int P_0, int P_1, PixelFormat? P_2)
	{
		SKColorType sKColorType = PixelFormatHelper.ResolveColorType(P_2);
		return new SKImageInfo(Math.Max(P_0, 1), Math.Max(P_1, 1), sKColorType, SKAlphaType.Premul);
	}
}

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

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.TransformedGeometryImpl
using Avalonia;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

internal class TransformedGeometryImpl : GeometryImpl, ITransformedGeometryImpl, IGeometryImpl
{
	public override SKPath? StrokePath { get; }

	public override SKPath? FillPath { get; }

	public IGeometryImpl SourceGeometry { get; }

	public Matrix Transform { get; }

	public override Rect Bounds { get; }

	public TransformedGeometryImpl(GeometryImpl P_0, Matrix P_1)
	{
		SourceGeometry = P_0;
		Transform = P_1;
		SKMatrix sKMatrix = P_1.ToSKMatrix();
		SKPath sKPath = (StrokePath = P_0.StrokePath.Clone());
		sKPath?.Transform(sKMatrix);
		Bounds = sKPath?.TightBounds.ToAvaloniaRect() ?? default(Rect);
		if (P_0.StrokePath == P_0.FillPath)
		{
			FillPath = sKPath;
		}
		else if (P_0.FillPath != null)
		{
			(FillPath = P_0.FillPath.Clone()).Transform(sKMatrix);
		}
	}
}

// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.WriteableBitmapImpl
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Platform.Internal;
using Avalonia.Skia;
using Avalonia.Skia.Helpers;
using SkiaSharp;

internal class WriteableBitmapImpl : IWriteableBitmapImpl, IBitmapImpl, IDisposable, IReadableBitmapWithAlphaImpl, IReadableBitmapImpl, IDrawableBitmapImpl
{
	private class BitmapFramebuffer : ILockedFramebuffer, IDisposable
	{
		private WriteableBitmapImpl _parent;

		private SKBitmap _bitmap;

		public nint Address => _bitmap.GetPixels();

		public PixelSize Size => new PixelSize(_bitmap.Width, _bitmap.Height);

		public int RowBytes => _bitmap.RowBytes;

		public Vector Dpi => _parent.Dpi;

		public PixelFormat Format => _bitmap.ColorType.ToPixelFormat();

		public BitmapFramebuffer(WriteableBitmapImpl P_0, SKBitmap P_1)
		{
			_parent = P_0;
			_bitmap = P_1;
			Monitor.Enter(P_0._lock);
		}

		public void Dispose()
		{
			_bitmap.NotifyPixelsChanged();
			_parent.Version++;
			_parent._imageValid = false;
			Monitor.Exit(_parent._lock);
			_bitmap = null;
			_parent = null;
		}
	}

	private static readonly SKBitmapReleaseDelegate s_releaseDelegate = ReleaseProc;

	private SKBitmap _bitmap;

	private SKImage? _image;

	private bool _imageValid;

	private readonly object _lock = new object();

	[CompilerGenerated]
	private int _003CVersion_003Ek__BackingField;

	public Vector Dpi { get; }

	public PixelSize PixelSize { get; }

	public int Version
	{
		[CompilerGenerated]
		get
		{
			return _003CVersion_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CVersion_003Ek__BackingField = num;
		}
	}

	public PixelFormat? Format => _bitmap.ColorType.ToAvalonia();

	public AlphaFormat? AlphaFormat => _bitmap.AlphaType.ToAlphaFormat();

	public WriteableBitmapImpl(PixelSize P_0, Vector P_1, PixelFormat P_2, AlphaFormat P_3)
	{
		Version = 1;
		base._002Ector();
		PixelSize = P_0;
		Dpi = P_1;
		SKColorType sKColorType = P_2.ToSkColorType();
		SKAlphaType sKAlphaType = P_3.ToSkAlphaType();
		_bitmap = new SKBitmap();
		SKImageInfo sKImageInfo = new SKImageInfo(P_0.Width, P_0.Height, sKColorType, sKAlphaType);
		UnmanagedBlob unmanagedBlob = new UnmanagedBlob(sKImageInfo.BytesSize);
		_bitmap.InstallPixels(sKImageInfo, unmanagedBlob.Address, sKImageInfo.RowBytes, s_releaseDelegate, unmanagedBlob);
		_bitmap.Erase(SKColor.Empty);
	}

	public void Draw(DrawingContextImpl P_0, SKRect P_1, SKRect P_2, SKPaint P_3)
	{
		lock (_lock)
		{
			if (_image == null || !_imageValid)
			{
				_image?.Dispose();
				_image = null;
				_image = GetSnapshot();
				_imageValid = true;
			}
			P_0.Canvas.DrawImage(_image, P_1, P_2, P_3);
		}
	}

	public virtual void Dispose()
	{
		lock (_lock)
		{
			_image?.Dispose();
			_image = null;
			_bitmap.Dispose();
			_bitmap = null;
		}
	}

	public void Save(Stream P_0, int? P_1 = null)
	{
		using SKImage sKImage = GetSnapshot();
		ImageSavingHelper.SaveImage(sKImage, P_0, P_1);
	}

	public void Save(string P_0, int? P_1 = null)
	{
		using SKImage sKImage = GetSnapshot();
		ImageSavingHelper.SaveImage(sKImage, P_0, P_1);
	}

	public ILockedFramebuffer Lock()
	{
		return new BitmapFramebuffer(this, _bitmap);
	}

	public SKImage GetSnapshot()
	{
		lock (_lock)
		{
			return SKImage.FromPixels(_bitmap.Info, _bitmap.GetPixels(), _bitmap.RowBytes);
		}
	}

	private static void ReleaseProc(nint address, object ctx)
	{
		((UnmanagedBlob)ctx).Dispose();
	}
}
