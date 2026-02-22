// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.PathMarkupParser
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;

public class PathMarkupParser : IDisposable
{
	private enum Command
	{
		None,
		FillRule,
		Move,
		Line,
		HorizontalLine,
		VerticalLine,
		CubicBezierCurve,
		QuadraticBezierCurve,
		SmoothCubicBezierCurve,
		SmoothQuadraticBezierCurve,
		Arc,
		Close
	}

	private static readonly Dictionary<char, Command> s_commands = new Dictionary<char, Command>
	{
		{
			'F',
			Command.FillRule
		},
		{
			'M',
			Command.Move
		},
		{
			'L',
			Command.Line
		},
		{
			'H',
			Command.HorizontalLine
		},
		{
			'V',
			Command.VerticalLine
		},
		{
			'Q',
			Command.QuadraticBezierCurve
		},
		{
			'T',
			Command.SmoothQuadraticBezierCurve
		},
		{
			'C',
			Command.CubicBezierCurve
		},
		{
			'S',
			Command.SmoothCubicBezierCurve
		},
		{
			'A',
			Command.Arc
		},
		{
			'Z',
			Command.Close
		}
	};

	private IGeometryContext? _geometryContext;

	private Point _currentPoint;

	private Point? _beginFigurePoint;

	private Point? _previousControlPoint;

	private bool _isOpen;

	private bool _isDisposed;

	public PathMarkupParser(IGeometryContext P_0)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("geometryContext");
		}
		_geometryContext = P_0;
	}

	void IDisposable.Dispose()
	{
		Dispose(true);
	}

	protected virtual void Dispose(bool P_0)
	{
		if (!_isDisposed)
		{
			if (P_0)
			{
				_geometryContext = null;
			}
			_isDisposed = true;
		}
	}

	private static Point MirrorControlPoint(Point P_0, Point P_1)
	{
		Point point = P_0 - P_1;
		return P_1 + -point;
	}

	public void Parse(string P_0)
	{
		ThrowIfDisposed();
		ReadOnlySpan<char> readOnlySpan = P_0.AsSpan();
		_currentPoint = default(Point);
		Command command;
		bool flag;
		while (!readOnlySpan.IsEmpty && ReadCommand(ref readOnlySpan, out command, out flag))
		{
			bool flag2 = true;
			do
			{
				if (!flag2)
				{
					readOnlySpan = ReadSeparator(readOnlySpan);
				}
				switch (command)
				{
				case Command.FillRule:
					SetFillRule(ref readOnlySpan);
					break;
				case Command.Move:
					AddMove(ref readOnlySpan, flag);
					break;
				case Command.Line:
					AddLine(ref readOnlySpan, flag);
					break;
				case Command.HorizontalLine:
					AddHorizontalLine(ref readOnlySpan, flag);
					break;
				case Command.VerticalLine:
					AddVerticalLine(ref readOnlySpan, flag);
					break;
				case Command.CubicBezierCurve:
					AddCubicBezierCurve(ref readOnlySpan, flag);
					break;
				case Command.QuadraticBezierCurve:
					AddQuadraticBezierCurve(ref readOnlySpan, flag);
					break;
				case Command.SmoothCubicBezierCurve:
					AddSmoothCubicBezierCurve(ref readOnlySpan, flag);
					break;
				case Command.SmoothQuadraticBezierCurve:
					AddSmoothQuadraticBezierCurve(ref readOnlySpan, flag);
					break;
				case Command.Arc:
					AddArc(ref readOnlySpan, flag);
					break;
				case Command.Close:
					CloseFigure();
					break;
				default:
					throw new NotSupportedException("Unsupported command");
				case Command.None:
					break;
				}
				flag2 = false;
			}
			while (PeekArgument(readOnlySpan));
		}
		if (_isOpen)
		{
			_geometryContext.EndFigure(false);
		}
	}

	private void CreateFigure()
	{
		ThrowIfDisposed();
		if (_isOpen)
		{
			_geometryContext.EndFigure(false);
		}
		_geometryContext.BeginFigure(_currentPoint);
		_beginFigurePoint = _currentPoint;
		_isOpen = true;
	}

	private void SetFillRule(scoped ref ReadOnlySpan<char> P_0)
	{
		ThrowIfDisposed();
		if (!ReadArgument(ref P_0, out var readOnlySpan) || readOnlySpan.Length != 1)
		{
			throw new InvalidDataException("Invalid fill rule.");
		}
		FillRule fillRule = readOnlySpan[0] switch
		{
			'0' => FillRule.EvenOdd, 
			'1' => FillRule.NonZero, 
			_ => throw new InvalidDataException("Invalid fill rule"), 
		};
		_geometryContext.SetFillRule(fillRule);
	}

	private void CloseFigure()
	{
		ThrowIfDisposed();
		if (_isOpen)
		{
			_geometryContext.EndFigure(true);
			if (_beginFigurePoint.HasValue)
			{
				_currentPoint = _beginFigurePoint.Value;
				_beginFigurePoint = null;
			}
		}
		_previousControlPoint = null;
		_isOpen = false;
	}

	private void AddMove(ref ReadOnlySpan<char> P_0, bool P_1)
	{
		Point currentPoint = (P_1 ? ReadRelativePoint(ref P_0, _currentPoint) : ReadPoint(ref P_0));
		_currentPoint = currentPoint;
		CreateFigure();
		while (PeekArgument(P_0))
		{
			P_0 = ReadSeparator(P_0);
			AddLine(ref P_0, P_1);
		}
	}

	private void AddLine(ref ReadOnlySpan<char> P_0, bool P_1)
	{
		ThrowIfDisposed();
		Point point = (P_1 ? ReadRelativePoint(ref P_0, _currentPoint) : ReadPoint(ref P_0));
		if (!_isOpen)
		{
			CreateFigure();
		}
		_geometryContext.LineTo(point);
		_currentPoint = point;
	}

	private void AddHorizontalLine(ref ReadOnlySpan<char> P_0, bool P_1)
	{
		ThrowIfDisposed();
		Point point = (P_1 ? new Point(_currentPoint.X + ReadDouble(ref P_0), _currentPoint.Y) : _currentPoint.WithX(ReadDouble(ref P_0)));
		if (!_isOpen)
		{
			CreateFigure();
		}
		_geometryContext.LineTo(point);
		_currentPoint = point;
	}

	private void AddVerticalLine(ref ReadOnlySpan<char> P_0, bool P_1)
	{
		ThrowIfDisposed();
		Point point = (P_1 ? new Point(_currentPoint.X, _currentPoint.Y + ReadDouble(ref P_0)) : _currentPoint.WithY(ReadDouble(ref P_0)));
		if (!_isOpen)
		{
			CreateFigure();
		}
		_geometryContext.LineTo(point);
		_currentPoint = point;
	}

	private void AddCubicBezierCurve(ref ReadOnlySpan<char> P_0, bool P_1)
	{
		ThrowIfDisposed();
		Point point = (P_1 ? ReadRelativePoint(ref P_0, _currentPoint) : ReadPoint(ref P_0));
		P_0 = ReadSeparator(P_0);
		Point point2 = (P_1 ? ReadRelativePoint(ref P_0, _currentPoint) : ReadPoint(ref P_0));
		_previousControlPoint = point2;
		P_0 = ReadSeparator(P_0);
		Point point3 = (P_1 ? ReadRelativePoint(ref P_0, _currentPoint) : ReadPoint(ref P_0));
		if (!_isOpen)
		{
			CreateFigure();
		}
		_geometryContext.CubicBezierTo(point, point2, point3);
		_currentPoint = point3;
	}

	private void AddQuadraticBezierCurve(ref ReadOnlySpan<char> P_0, bool P_1)
	{
		ThrowIfDisposed();
		Point point = (P_1 ? ReadRelativePoint(ref P_0, _currentPoint) : ReadPoint(ref P_0));
		_previousControlPoint = point;
		P_0 = ReadSeparator(P_0);
		Point point2 = (P_1 ? ReadRelativePoint(ref P_0, _currentPoint) : ReadPoint(ref P_0));
		if (!_isOpen)
		{
			CreateFigure();
		}
		_geometryContext.QuadraticBezierTo(point, point2);
		_currentPoint = point2;
	}

	private void AddSmoothCubicBezierCurve(ref ReadOnlySpan<char> P_0, bool P_1)
	{
		ThrowIfDisposed();
		Point point = (P_1 ? ReadRelativePoint(ref P_0, _currentPoint) : ReadPoint(ref P_0));
		P_0 = ReadSeparator(P_0);
		Point point2 = (P_1 ? ReadRelativePoint(ref P_0, _currentPoint) : ReadPoint(ref P_0));
		if (_previousControlPoint.HasValue)
		{
			_previousControlPoint = MirrorControlPoint(_previousControlPoint.Value, _currentPoint);
		}
		if (!_isOpen)
		{
			CreateFigure();
		}
		_geometryContext.CubicBezierTo(_previousControlPoint ?? _currentPoint, point, point2);
		_previousControlPoint = point;
		_currentPoint = point2;
	}

	private void AddSmoothQuadraticBezierCurve(ref ReadOnlySpan<char> P_0, bool P_1)
	{
		ThrowIfDisposed();
		Point point = (P_1 ? ReadRelativePoint(ref P_0, _currentPoint) : ReadPoint(ref P_0));
		if (_previousControlPoint.HasValue)
		{
			_previousControlPoint = MirrorControlPoint(_previousControlPoint.Value, _currentPoint);
		}
		if (!_isOpen)
		{
			CreateFigure();
		}
		_geometryContext.QuadraticBezierTo(_previousControlPoint ?? _currentPoint, point);
		_currentPoint = point;
	}

	private void AddArc(ref ReadOnlySpan<char> P_0, bool P_1)
	{
		ThrowIfDisposed();
		Size size = ReadSize(ref P_0);
		P_0 = ReadSeparator(P_0);
		double num = ReadDouble(ref P_0);
		P_0 = ReadSeparator(P_0);
		bool flag = ReadBool(ref P_0);
		P_0 = ReadSeparator(P_0);
		SweepDirection sweepDirection = (ReadBool(ref P_0) ? SweepDirection.Clockwise : SweepDirection.CounterClockwise);
		P_0 = ReadSeparator(P_0);
		Point point = (P_1 ? ReadRelativePoint(ref P_0, _currentPoint) : ReadPoint(ref P_0));
		if (!_isOpen)
		{
			CreateFigure();
		}
		_geometryContext.ArcTo(point, size, num, flag, sweepDirection);
		_currentPoint = point;
		_previousControlPoint = null;
	}

	private static bool PeekArgument(ReadOnlySpan<char> P_0)
	{
		P_0 = SkipWhitespace(P_0);
		if (!P_0.IsEmpty)
		{
			if (P_0[0] != ',' && P_0[0] != '-' && P_0[0] != '.')
			{
				return char.IsDigit(P_0[0]);
			}
			return true;
		}
		return false;
	}

	private static bool ReadArgument(scoped ref ReadOnlySpan<char> P_0, out ReadOnlySpan<char> P_1)
	{
		P_0 = SkipWhitespace(P_0);
		if (P_0.IsEmpty)
		{
			P_1 = ReadOnlySpan<char>.Empty;
			return false;
		}
		bool flag = false;
		int i = 0;
		if (P_0[i] == '-')
		{
			i++;
		}
		for (; i < P_0.Length && char.IsNumber(P_0[i]); i++)
		{
			flag = true;
		}
		if (i < P_0.Length && P_0[i] == '.')
		{
			flag = false;
			i++;
		}
		for (; i < P_0.Length && char.IsNumber(P_0[i]); i++)
		{
			flag = true;
		}
		if (i < P_0.Length && (P_0[i] == 'E' || P_0[i] == 'e'))
		{
			flag = false;
			i++;
			if (P_0[i] == '-' || P_0[i] == '+')
			{
				for (i++; i < P_0.Length && char.IsNumber(P_0[i]); i++)
				{
					flag = true;
				}
			}
		}
		if (!flag)
		{
			P_1 = ReadOnlySpan<char>.Empty;
			return false;
		}
		P_1 = P_0.Slice(0, i);
		P_0 = P_0.Slice(i);
		return true;
	}

	private static ReadOnlySpan<char> ReadSeparator(ReadOnlySpan<char> P_0)
	{
		P_0 = SkipWhitespace(P_0);
		if (!P_0.IsEmpty && P_0[0] == ',')
		{
			P_0 = P_0.Slice(1);
		}
		return P_0;
	}

	private static ReadOnlySpan<char> SkipWhitespace(ReadOnlySpan<char> P_0)
	{
		int i;
		for (i = 0; i < P_0.Length && char.IsWhiteSpace(P_0[i]); i++)
		{
		}
		return P_0.Slice(i);
	}

	private static bool ReadBool(ref ReadOnlySpan<char> P_0)
	{
		P_0 = SkipWhitespace(P_0);
		if (P_0.IsEmpty)
		{
			throw new InvalidDataException("Invalid bool rule.");
		}
		char c = P_0[0];
		P_0 = P_0.Slice(1);
		return c switch
		{
			'0' => false, 
			'1' => true, 
			_ => throw new InvalidDataException("Invalid bool rule"), 
		};
	}

	private static double ReadDouble(ref ReadOnlySpan<char> P_0)
	{
		if (!ReadArgument(ref P_0, out var readOnlySpan))
		{
			throw new InvalidDataException("Invalid double value");
		}
		return double.Parse(readOnlySpan.ToString(), CultureInfo.InvariantCulture);
	}

	private static Size ReadSize(ref ReadOnlySpan<char> P_0)
	{
		double num = ReadDouble(ref P_0);
		P_0 = ReadSeparator(P_0);
		double num2 = ReadDouble(ref P_0);
		return new Size(num, num2);
	}

	private static Point ReadPoint(ref ReadOnlySpan<char> P_0)
	{
		double num = ReadDouble(ref P_0);
		P_0 = ReadSeparator(P_0);
		double num2 = ReadDouble(ref P_0);
		return new Point(num, num2);
	}

	private static Point ReadRelativePoint(ref ReadOnlySpan<char> P_0, Point P_1)
	{
		double num = ReadDouble(ref P_0);
		P_0 = ReadSeparator(P_0);
		double num2 = ReadDouble(ref P_0);
		return new Point(P_1.X + num, P_1.Y + num2);
	}

	private static bool ReadCommand(ref ReadOnlySpan<char> P_0, out Command P_1, out bool P_2)
	{
		P_0 = SkipWhitespace(P_0);
		if (P_0.IsEmpty)
		{
			P_1 = Command.None;
			P_2 = false;
			return false;
		}
		char c = P_0[0];
		if (!s_commands.TryGetValue(char.ToUpperInvariant(c), out P_1))
		{
			throw new InvalidDataException("Unexpected path command '" + c + "'.");
		}
		P_2 = char.IsLower(c);
		P_0 = P_0.Slice(1);
		return true;
	}

	[MemberNotNull("_geometryContext")]
	private void ThrowIfDisposed()
	{
		if (_isDisposed || _geometryContext == null)
		{
			throw new ObjectDisposedException("PathMarkupParser");
		}
	}
}

