// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.PlatformDrawingContext
using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Logging;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Threading;
using Avalonia.Utilities;

internal sealed class PlatformDrawingContext : DrawingContext
{
	private readonly IDrawingContextImpl _impl;

	private readonly bool _ownsImpl;

	private Stack<Matrix>? _transforms;

	private static ThreadSafeObjectPool<Stack<Matrix>> TransformStackPool { get; } = ThreadSafeObjectPool<Stack<Matrix>>.Default;

	public PlatformDrawingContext(IDrawingContextImpl P_0, bool P_1 = true)
	{
		_impl = P_0;
		_ownsImpl = P_1;
	}

	protected override void DrawLineCore(IPen P_0, Point P_1, Point P_2)
	{
		_impl.DrawLine(P_0, P_1, P_2);
	}

	protected override void DrawGeometryCore(IBrush? P_0, IPen? P_1, IGeometryImpl P_2)
	{
		_impl.DrawGeometry(P_0, P_1, P_2);
	}

	protected override void DrawRectangleCore(IBrush? P_0, IPen? P_1, RoundedRect P_2, BoxShadows P_3 = default(BoxShadows))
	{
		_impl.DrawRectangle(P_0, P_1, P_2, P_3);
	}

	internal override void DrawBitmap(IRef<IBitmapImpl> P_0, double P_1, Rect P_2, Rect P_3)
	{
		_impl.DrawBitmap(P_0.Item, P_1, P_2, P_3);
	}

	public override void Custom(ICustomDrawOperation P_0)
	{
		using ImmediateDrawingContext immediateDrawingContext = new ImmediateDrawingContext(_impl, false);
		try
		{
			P_0.Render(immediateDrawingContext);
		}
		catch (Exception ex)
		{
			Logger.TryGet(LogEventLevel.Error, "Visual")?.Log(P_0, $"Exception in {P_0.GetType().Name}.{"Render"} {{0}}", ex);
		}
	}

	public override void DrawGlyphRun(IBrush? P_0, GlyphRun P_1)
	{
		if (P_1 == null)
		{
			throw new ArgumentNullException("glyphRun");
		}
		if (P_0 != null)
		{
			_impl.DrawGlyphRun(P_0, P_1.PlatformImpl.Item);
		}
	}

	protected override void PushClipCore(RoundedRect P_0)
	{
		_impl.PushClip(P_0);
	}

	protected override void PushClipCore(Rect P_0)
	{
		_impl.PushClip(P_0);
	}

	protected override void PushGeometryClipCore(Geometry P_0)
	{
		_impl.PushGeometryClip(P_0.PlatformImpl ?? throw new ArgumentException());
	}

	protected override void PushOpacityCore(double P_0)
	{
		_impl.PushOpacity(P_0, null);
	}

	protected override void PushOpacityMaskCore(IBrush P_0, Rect P_1)
	{
		_impl.PushOpacityMask(P_0, P_1);
	}

	protected override void PushTransformCore(Matrix P_0)
	{
		if (_transforms == null)
		{
			_transforms = TransformStackPool.Get();
		}
		Matrix transform = _impl.Transform;
		_transforms.Push(transform);
		_impl.Transform = P_0 * transform;
	}

	protected override void PushRenderOptionsCore(RenderOptions P_0)
	{
		_impl.PushRenderOptions(P_0);
	}

	protected override void PopClipCore()
	{
		_impl.PopClip();
	}

	protected override void PopGeometryClipCore()
	{
		_impl.PopGeometryClip();
	}

	protected override void PopOpacityCore()
	{
		_impl.PopOpacity();
	}

	protected override void PopOpacityMaskCore()
	{
		_impl.PopOpacityMask();
	}

	protected override void PopTransformCore()
	{
		_impl.Transform = (_transforms ?? throw new ObjectDisposedException("PlatformDrawingContext")).Pop();
	}

	protected override void PopRenderOptionsCore()
	{
		_impl.PopRenderOptions();
	}

	protected override void DisposeCore()
	{
		if (_ownsImpl)
		{
			_impl.Dispose();
		}
		if (_transforms != null)
		{
			if (_transforms.Count != 0)
			{
				throw new InvalidOperationException("Not all states are disposed");
			}
			TransformStackPool.ReturnAndSetNull(ref _transforms);
		}
	}
}

