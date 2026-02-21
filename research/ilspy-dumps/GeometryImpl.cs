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

