// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.StreamGeometryContext
using System;
using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;

public class StreamGeometryContext : IGeometryContext, IDisposable, IGeometryContext2
{
	private readonly IStreamGeometryContextImpl _impl;

	private Point _currentPoint;

	public StreamGeometryContext(IStreamGeometryContextImpl P_0)
	{
		_impl = P_0;
	}

	public void SetFillRule(FillRule P_0)
	{
		_impl.SetFillRule(P_0);
	}

	public void ArcTo(Point P_0, Size P_1, double P_2, bool P_3, SweepDirection P_4)
	{
		_impl.ArcTo(P_0, P_1, P_2, P_3, P_4);
		_currentPoint = P_0;
	}

	public void BeginFigure(Point P_0, bool P_1)
	{
		_impl.BeginFigure(P_0, P_1);
		_currentPoint = P_0;
	}

	public void CubicBezierTo(Point P_0, Point P_1, Point P_2)
	{
		_impl.CubicBezierTo(P_0, P_1, P_2);
		_currentPoint = P_2;
	}

	public void QuadraticBezierTo(Point P_0, Point P_1)
	{
		_impl.QuadraticBezierTo(P_0, P_1);
		_currentPoint = P_1;
	}

	public void LineTo(Point P_0)
	{
		_impl.LineTo(P_0);
		_currentPoint = P_0;
	}

	public void EndFigure(bool P_0)
	{
		_impl.EndFigure(P_0);
	}

	public void Dispose()
	{
		_impl.Dispose();
	}

	public void LineTo(Point P_0, bool P_1)
	{
		if (_impl is IGeometryContext2 geometryContext)
		{
			geometryContext.LineTo(P_0, P_1);
		}
		else
		{
			_impl.LineTo(P_0);
		}
		_currentPoint = P_0;
	}

	public void ArcTo(Point P_0, Size P_1, double P_2, bool P_3, SweepDirection P_4, bool P_5)
	{
		if (_impl is IGeometryContext2 geometryContext)
		{
			geometryContext.ArcTo(P_0, P_1, P_2, P_3, P_4, P_5);
		}
		else
		{
			_impl.ArcTo(P_0, P_1, P_2, P_3, P_4);
		}
		_currentPoint = P_0;
	}

	public void CubicBezierTo(Point P_0, Point P_1, Point P_2, bool P_3)
	{
		if (_impl is IGeometryContext2 geometryContext)
		{
			geometryContext.CubicBezierTo(P_0, P_1, P_2, P_3);
		}
		else
		{
			_impl.CubicBezierTo(P_0, P_1, P_2);
		}
		_currentPoint = P_2;
	}

	public void QuadraticBezierTo(Point P_0, Point P_1, bool P_2)
	{
		if (_impl is IGeometryContext2 geometryContext)
		{
			geometryContext.QuadraticBezierTo(P_0, P_1, P_2);
		}
		else
		{
			_impl.QuadraticBezierTo(P_0, P_1);
		}
		_currentPoint = P_1;
	}
}

