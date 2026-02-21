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

