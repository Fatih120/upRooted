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

