// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.ImmediateDrawingContext
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Media.Immutable;
using Avalonia.Platform;
using Avalonia.Threading;
using Avalonia.Utilities;

public sealed class ImmediateDrawingContext : IDisposable, IOptionalFeatureProvider
{
	private readonly struct TransformContainer
	{
		public readonly Matrix LocalTransform;

		public readonly Matrix ContainerTransform;
	}

	public readonly record struct PushedState : IDisposable
	{
		public enum PushedStateType
		{
			None,
			Matrix,
			Opacity,
			Clip,
			MatrixContainer,
			GeometryClip,
			OpacityMask
		}

		private readonly int _level;

		private readonly ImmediateDrawingContext _context;

		private readonly Matrix _matrix;

		private readonly PushedStateType _type;

		internal PushedState(ImmediateDrawingContext P_0, PushedStateType P_1, Matrix P_2 = default(Matrix))
		{
			if (P_0._states == null)
			{
				throw new ObjectDisposedException("ImmediateDrawingContext");
			}
			_context = P_0;
			_type = P_1;
			_matrix = P_2;
			_level = ++P_0._currentLevel;
			P_0._states.Push(this);
		}

		public void Dispose()
		{
			if (_type != PushedStateType.None)
			{
				if (_context._states == null || _context._transformContainers == null)
				{
					throw new ObjectDisposedException("DrawingContext");
				}
				if (_context._currentLevel != _level)
				{
					throw new InvalidOperationException("Wrong Push/Pop state order");
				}
				_context._currentLevel--;
				_context._states.Pop();
				if (_type == PushedStateType.Matrix)
				{
					_context.CurrentTransform = _matrix;
				}
				else if (_type == PushedStateType.Clip)
				{
					_context.PlatformImpl.PopClip();
				}
				else if (_type == PushedStateType.Opacity)
				{
					_context.PlatformImpl.PopOpacity();
				}
				else if (_type == PushedStateType.GeometryClip)
				{
					_context.PlatformImpl.PopGeometryClip();
				}
				else if (_type == PushedStateType.OpacityMask)
				{
					_context.PlatformImpl.PopOpacityMask();
				}
				else if (_type == PushedStateType.MatrixContainer)
				{
					TransformContainer transformContainer = _context._transformContainers.Pop();
					_context._currentContainerTransform = transformContainer.ContainerTransform;
					_context.CurrentTransform = transformContainer.LocalTransform;
				}
			}
		}

		[CompilerGenerated]
		public override string ToString()
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

	private readonly bool _ownsImpl;

	private int _currentLevel;

	private Stack<PushedState>? _states = StateStackPool.Get();

	private Stack<TransformContainer>? _transformContainers = TransformStackPool.Get();

	private Matrix _currentTransform = Matrix.Identity;

	private Matrix _currentContainerTransform;

	private static ThreadSafeObjectPool<Stack<PushedState>> StateStackPool { get; } = ThreadSafeObjectPool<Stack<PushedState>>.Default;

	private static ThreadSafeObjectPool<Stack<TransformContainer>> TransformStackPool { get; } = ThreadSafeObjectPool<Stack<TransformContainer>>.Default;

	public IDrawingContextImpl PlatformImpl { get; }

	public Matrix CurrentTransform
	{
		get
		{
			return _currentTransform;
		}
		private set
		{
			_currentTransform = currentTransform;
			Matrix transform = _currentTransform * _currentContainerTransform;
			PlatformImpl.Transform = transform;
		}
	}

	internal ImmediateDrawingContext(IDrawingContextImpl P_0, bool P_1)
		: this(P_0, P_0.Transform, P_1)
	{
	}

	internal ImmediateDrawingContext(IDrawingContextImpl P_0, Matrix P_1, bool P_2)
	{
		_ownsImpl = P_2;
		PlatformImpl = P_0;
		_currentContainerTransform = P_1;
	}

	public void DrawBitmap(Bitmap P_0, Rect P_1, Rect P_2)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("source");
		}
		PlatformImpl.DrawBitmap(P_0.PlatformImpl.Item, 1.0, P_1, P_2);
	}

	public void DrawRectangle(IImmutableBrush? P_0, ImmutablePen? P_1, Rect P_2, double P_3 = 0.0, double P_4 = 0.0, BoxShadows P_5 = default(BoxShadows))
	{
		if (P_0 != null || PenIsVisible(P_1))
		{
			if (!MathUtilities.IsZero(P_3))
			{
				P_3 = Math.Min(P_3, P_2.Width / 2.0);
			}
			if (!MathUtilities.IsZero(P_4))
			{
				P_4 = Math.Min(P_4, P_2.Height / 2.0);
			}
			PlatformImpl.DrawRectangle(P_0, P_1, new RoundedRect(P_2, P_3, P_4), P_5);
		}
	}

	public void FillRectangle(IImmutableBrush P_0, Rect P_1, float P_2 = 0f)
	{
		DrawRectangle(P_0, null, P_1, P_2, P_2);
	}

	public PushedState PushClip(Rect P_0)
	{
		PlatformImpl.PushClip(P_0);
		return new PushedState(this, PushedState.PushedStateType.Clip);
	}

	public PushedState PushPreTransform(Matrix P_0)
	{
		return PushSetTransform(P_0 * CurrentTransform);
	}

	public PushedState PushSetTransform(Matrix P_0)
	{
		Matrix currentTransform = CurrentTransform;
		CurrentTransform = P_0;
		return new PushedState(this, PushedState.PushedStateType.Matrix, currentTransform);
	}

	public void Dispose()
	{
		if (_states == null || _transformContainers == null)
		{
			throw new ObjectDisposedException("DrawingContext");
		}
		while (_states.Count != 0)
		{
			_states.Peek().Dispose();
		}
		StateStackPool.ReturnAndSetNull(ref _states);
		if (_transformContainers.Count != 0)
		{
			throw new InvalidOperationException("Transform container stack is non-empty");
		}
		TransformStackPool.ReturnAndSetNull(ref _transformContainers);
		if (_ownsImpl)
		{
			PlatformImpl.Dispose();
		}
	}

	private static bool PenIsVisible(IPen? P_0)
	{
		if (P_0?.Brush != null)
		{
			return P_0.Thickness > 0.0;
		}
		return false;
	}

	public object? TryGetFeature(Type P_0)
	{
		return PlatformImpl.GetFeature(P_0);
	}
}

