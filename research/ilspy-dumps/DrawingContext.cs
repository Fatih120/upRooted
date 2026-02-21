// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.DrawingContext
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Threading;
using Avalonia.Utilities;

public abstract class DrawingContext : IDisposable
{
	public record struct PushedState : IDisposable
	{
		private readonly DrawingContext _context;

		private readonly int _level;

		public PushedState(DrawingContext P_0)
		{
			_context = P_0;
			_level = _context._states.Count;
		}

		public void Dispose()
		{
			if (_context?._states != null)
			{
				if (_context._states.Count != _level)
				{
					throw new InvalidOperationException("Wrong Push/Pop state order");
				}
				_context._states.Pop().Dispose();
			}
		}

		[CompilerGenerated]
		public override readonly string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PushedState");
			stringBuilder.Append(" { ");
			if (PrintMembers(stringBuilder))
			{
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}
	}

	private readonly record struct RestoreState : IDisposable
	{
		public enum PushedStateType
		{
			None,
			Transform,
			Opacity,
			Clip,
			GeometryClip,
			OpacityMask,
			RenderOptions
		}

		private readonly DrawingContext _context;

		private readonly PushedStateType _type;

		public RestoreState(DrawingContext P_0, PushedStateType P_1)
		{
			_context = P_0;
			_type = P_1;
		}

		public void Dispose()
		{
			if (_type != PushedStateType.None)
			{
				if (_context._states == null)
				{
					throw new ObjectDisposedException("DrawingContext");
				}
				if (_type == PushedStateType.Transform)
				{
					_context.PopTransformCore();
				}
				else if (_type == PushedStateType.Clip)
				{
					_context.PopClipCore();
				}
				else if (_type == PushedStateType.Opacity)
				{
					_context.PopOpacityCore();
				}
				else if (_type == PushedStateType.GeometryClip)
				{
					_context.PopGeometryClipCore();
				}
				else if (_type == PushedStateType.OpacityMask)
				{
					_context.PopOpacityMaskCore();
				}
				else if (_type == PushedStateType.RenderOptions)
				{
					_context.PopRenderOptionsCore();
				}
			}
		}

		[CompilerGenerated]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("RestoreState");
			stringBuilder.Append(" { ");
			if (PrintMembers(stringBuilder))
			{
			}
			stringBuilder.Append('}');
			return stringBuilder.ToString();
		}
	}

	private Stack<RestoreState>? _states;

	private static ThreadSafeObjectPool<Stack<RestoreState>> StateStackPool { get; } = ThreadSafeObjectPool<Stack<RestoreState>>.Default;

	internal DrawingContext()
	{
	}

	public void Dispose()
	{
		if (_states != null)
		{
			while (_states.Count > 0)
			{
				_states.Pop().Dispose();
			}
			StateStackPool.ReturnAndSetNull(ref _states);
		}
		DisposeCore();
	}

	protected abstract void DisposeCore();

	public virtual void DrawImage(IImage P_0, Rect P_1)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("source");
		}
		DrawImage(P_0, new Rect(P_0.Size), P_1);
	}

	public virtual void DrawImage(IImage P_0, Rect P_1, Rect P_2)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("source");
		}
		P_0.Draw(this, P_1, P_2);
	}

	internal abstract void DrawBitmap(IRef<IBitmapImpl> P_0, double P_1, Rect P_2, Rect P_3);

	public void DrawLine(IPen P_0, Point P_1, Point P_2)
	{
		if (PenIsVisible(P_0))
		{
			DrawLineCore(P_0, P_1, P_2);
		}
	}

	protected abstract void DrawLineCore(IPen P_0, Point P_1, Point P_2);

	public void DrawGeometry(IBrush? P_0, IPen? P_1, Geometry P_2)
	{
		if ((P_0 != null || PenIsVisible(P_1)) && P_2.PlatformImpl != null)
		{
			DrawGeometryCore(P_0, P_1, P_2.PlatformImpl);
		}
	}

	protected abstract void DrawGeometryCore(IBrush? P_0, IPen? P_1, IGeometryImpl P_2);

	public void DrawRectangle(IBrush? P_0, IPen? P_1, Rect P_2, double P_3 = 0.0, double P_4 = 0.0, BoxShadows P_5 = default(BoxShadows))
	{
		if (P_0 != null || PenIsVisible(P_1) || P_5.Count != 0)
		{
			if (!MathUtilities.IsZero(P_3))
			{
				P_3 = Math.Min(P_3, P_2.Width / 2.0);
			}
			if (!MathUtilities.IsZero(P_4))
			{
				P_4 = Math.Min(P_4, P_2.Height / 2.0);
			}
			DrawRectangleCore(P_0, P_1, new RoundedRect(P_2, P_3, P_4), P_5);
		}
	}

	public void DrawRectangle(IBrush? P_0, IPen? P_1, RoundedRect P_2, BoxShadows P_3 = default(BoxShadows))
	{
		if (P_0 != null || PenIsVisible(P_1) || P_3.Count != 0)
		{
			DrawRectangleCore(P_0, P_1, P_2, P_3);
		}
	}

	protected abstract void DrawRectangleCore(IBrush? P_0, IPen? P_1, RoundedRect P_2, BoxShadows P_3 = default(BoxShadows));

	public void DrawRectangle(IPen P_0, Rect P_1, float P_2 = 0f)
	{
		DrawRectangle(null, P_0, P_1, P_2, P_2);
	}

	public void FillRectangle(IBrush P_0, Rect P_1, float P_2 = 0f)
	{
		DrawRectangle(P_0, null, P_1, P_2, P_2);
	}

	public abstract void Custom(ICustomDrawOperation P_0);

	public virtual void DrawText(FormattedText P_0, Point P_1)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("text");
		}
		P_0.Draw(this, P_1);
	}

	public abstract void DrawGlyphRun(IBrush? P_0, GlyphRun P_1);

	public PushedState PushClip(RoundedRect P_0)
	{
		PushClipCore(P_0);
		if (_states == null)
		{
			_states = StateStackPool.Get();
		}
		_states.Push(new RestoreState(this, RestoreState.PushedStateType.Clip));
		return new PushedState(this);
	}

	protected abstract void PushClipCore(RoundedRect P_0);

	public PushedState PushClip(Rect P_0)
	{
		PushClipCore(P_0);
		if (_states == null)
		{
			_states = StateStackPool.Get();
		}
		_states.Push(new RestoreState(this, RestoreState.PushedStateType.Clip));
		return new PushedState(this);
	}

	protected abstract void PushClipCore(Rect P_0);

	public PushedState PushGeometryClip(Geometry P_0)
	{
		PushGeometryClipCore(P_0);
		if (_states == null)
		{
			_states = StateStackPool.Get();
		}
		_states.Push(new RestoreState(this, RestoreState.PushedStateType.GeometryClip));
		return new PushedState(this);
	}

	protected abstract void PushGeometryClipCore(Geometry P_0);

	public PushedState PushOpacity(double P_0)
	{
		PushOpacityCore(P_0);
		if (_states == null)
		{
			_states = StateStackPool.Get();
		}
		_states.Push(new RestoreState(this, RestoreState.PushedStateType.Opacity));
		return new PushedState(this);
	}

	protected abstract void PushOpacityCore(double P_0);

	public PushedState PushOpacityMask(IBrush P_0, Rect P_1)
	{
		PushOpacityMaskCore(P_0, P_1);
		if (_states == null)
		{
			_states = StateStackPool.Get();
		}
		_states.Push(new RestoreState(this, RestoreState.PushedStateType.OpacityMask));
		return new PushedState(this);
	}

	protected abstract void PushOpacityMaskCore(IBrush P_0, Rect P_1);

	public PushedState PushTransform(Matrix P_0)
	{
		PushTransformCore(P_0);
		if (_states == null)
		{
			_states = StateStackPool.Get();
		}
		_states.Push(new RestoreState(this, RestoreState.PushedStateType.Transform));
		return new PushedState(this);
	}

	public PushedState PushRenderOptions(RenderOptions P_0)
	{
		PushRenderOptionsCore(P_0);
		if (_states == null)
		{
			_states = StateStackPool.Get();
		}
		_states.Push(new RestoreState(this, RestoreState.PushedStateType.RenderOptions));
		return new PushedState(this);
	}

	protected abstract void PushRenderOptionsCore(RenderOptions P_0);

	protected abstract void PushTransformCore(Matrix P_0);

	protected abstract void PopClipCore();

	protected abstract void PopGeometryClipCore();

	protected abstract void PopOpacityCore();

	protected abstract void PopOpacityMaskCore();

	protected abstract void PopTransformCore();

	protected abstract void PopRenderOptionsCore();

	private static bool PenIsVisible(IPen? P_0)
	{
		if (P_0?.Brush != null)
		{
			return P_0.Thickness > 0.0;
		}
		return false;
	}
}

