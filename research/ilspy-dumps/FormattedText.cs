// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.FormattedText
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;
using Avalonia.Utilities;

public class FormattedText
{
	private struct LineEnumerator : IEnumerator, IDisposable
	{
		private int _lineCount = 0;

		private double _totalHeight = 0.0;

		private TextLine? _nextLine;

		private readonly TextFormatter _formatter;

		private readonly FormattedText _that;

		private readonly ITextSource _textSource;

		private double _previousHeight = 0.0;

		private TextLineBreak? _previousLineBreak = null;

		private int _position = 0;

		private int _length = 0;

		[CompilerGenerated]
		private TextLine? _003CCurrent_003Ek__BackingField;

		public int Position
		{
			get
			{
				return _position;
			}
			private set
			{
				_position = position;
			}
		}

		public int Length
		{
			get
			{
				return _length;
			}
			private set
			{
				_length = length;
			}
		}

		public TextLine? Current
		{
			[CompilerGenerated]
			readonly get
			{
				return _003CCurrent_003Ek__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				_003CCurrent_003Ek__BackingField = textLine;
			}
		}

		object? IEnumerator.Current => Current;

		internal LineEnumerator(FormattedText P_0)
		{
			Current = null;
			_nextLine = null;
			_formatter = TextFormatter.Current;
			_that = P_0;
			FormattedText that = _that;
			_textSource = that._textSourceImpl ?? (that._textSourceImpl = new TextSourceImplementation(_that));
		}

		public void Dispose()
		{
			Current = null;
			_nextLine = null;
		}

		private double MaxLineLength(int P_0)
		{
			if (_that._maxTextWidths == null)
			{
				return _that._maxTextWidth;
			}
			return _that._maxTextWidths[Math.Min(P_0, _that._maxTextWidths.Length - 1)];
		}

		public bool MoveNext()
		{
			if (Current == null)
			{
				if (_that._text.Length == 0)
				{
					return false;
				}
				Current = FormatLine(_textSource, Position, MaxLineLength(_lineCount), _that._defaultParaProps, null);
				if (Current == null)
				{
					return false;
				}
				if (_totalHeight + Current.Height > _that._maxTextHeight)
				{
					Current = null;
					return false;
				}
			}
			else
			{
				if (_nextLine == null)
				{
					return false;
				}
				_totalHeight += _previousHeight;
				Position += Length;
				_lineCount++;
				Current = _nextLine;
				_nextLine = null;
			}
			TextLineBreak textLineBreak = Current.TextLineBreak;
			if (Position + Current.Length < _that._text.Length)
			{
				bool flag = false;
				if (_lineCount + 1 >= _that._maxLineCount)
				{
					flag = false;
				}
				else
				{
					_nextLine = FormatLine(_textSource, Position + Current.Length, MaxLineLength(_lineCount + 1), _that._defaultParaProps, textLineBreak);
					if (_nextLine != null)
					{
						flag = _totalHeight + Current.Height + _nextLine.Height <= _that._maxTextHeight;
					}
				}
				if (!flag)
				{
					_nextLine = null;
					if (_that._trimming != TextTrimming.None && !Current.HasCollapsed)
					{
						TextWrapping textWrapping = _that._defaultParaProps.TextWrapping;
						_that._defaultParaProps.SetTextWrapping(TextWrapping.NoWrap);
						Current = FormatLine(_that._textSourceImpl, Position, MaxLineLength(_lineCount), _that._defaultParaProps, _previousLineBreak);
						if (Current != null)
						{
							textLineBreak = Current.TextLineBreak;
						}
						_that._defaultParaProps.SetTextWrapping(textWrapping);
					}
				}
			}
			if (Current != null)
			{
				_previousHeight = Current.Height;
				Length = Current.Length;
			}
			_previousLineBreak = textLineBreak;
			return true;
		}

		private TextLine? FormatLine(ITextSource P_0, int P_1, double P_2, TextParagraphProperties P_3, TextLineBreak? P_4)
		{
			TextLine textLine = _formatter.FormatLine(P_0, P_1, P_2, P_3, P_4);
			if (textLine != null && _that._trimming != TextTrimming.None && textLine.HasOverflowed && textLine.Length > 0)
			{
				GenericTextRunProperties genericTextRunProperties = (GenericTextRunProperties)new SpanRider(_that._formatRuns, _that._latestPosition, Math.Min(P_1 + textLine.Length - 1, _that._text.Length - 1)).CurrentElement;
				TextCollapsingProperties textCollapsingProperties = _that._trimming.CreateCollapsingProperties(new TextCollapsingCreateInfo(P_2, genericTextRunProperties, P_3.FlowDirection));
				textLine = textLine.Collapse(textCollapsingProperties);
			}
			return textLine;
		}

		public void Reset()
		{
			Position = 0;
			_lineCount = 0;
			_totalHeight = 0.0;
			Current = null;
			_nextLine = null;
		}
	}

	private class CachedMetrics
	{
		public double Height;

		public double Baseline;

		public double Width;

		public double WidthIncludingTrailingWhitespace;

		public double Extent;

		public double OverhangAfter;

		public double OverhangLeading;

		public double OverhangTrailing;
	}

	private class TextSourceImplementation : ITextSource
	{
		private readonly FormattedText _that;

		public TextSourceImplementation(FormattedText P_0)
		{
			_that = P_0;
		}

		public TextRun GetTextRun(int P_0)
		{
			if (P_0 >= _that._text.Length)
			{
				return new TextEndOfParagraph();
			}
			SpanRider spanRider = new SpanRider(_that._formatRuns, _that._latestPosition, P_0);
			ReadOnlyMemory<char> readOnlyMemory = _that._text.AsMemory(P_0, spanRider.Length);
			TextRunProperties textRunProperties = (GenericTextRunProperties)spanRider.CurrentElement;
			return new TextCharacters(readOnlyMemory, textRunProperties);
		}
	}

	private readonly string _text;

	private readonly SpanVector _formatRuns = new SpanVector(null);

	private SpanPosition _latestPosition;

	private readonly GenericTextParagraphProperties _defaultParaProps;

	private double _maxTextWidth = double.PositiveInfinity;

	private double[]? _maxTextWidths;

	private double _maxTextHeight = double.PositiveInfinity;

	private int _maxLineCount = int.MaxValue;

	private TextTrimming _trimming = TextTrimming.WordEllipsis;

	private TextSourceImplementation? _textSourceImpl;

	private CachedMetrics? _metrics;

	private CachedMetrics Metrics => _metrics ?? (_metrics = DrawAndCalculateMetrics(null, default(Point), false));

	public double Height => Metrics.Height;

	public double Baseline => Metrics.Baseline;

	public double Width => Metrics.Width;

	public double WidthIncludingTrailingWhitespace => Metrics.WidthIncludingTrailingWhitespace;

	public FormattedText(string P_0, CultureInfo P_1, FlowDirection P_2, Typeface P_3, double P_4, IBrush? P_5)
	{
		if (P_1 == null)
		{
			throw new ArgumentNullException("culture");
		}
		ValidateFlowDirection(P_2, "flowDirection");
		ValidateFontSize(P_4);
		_text = P_0;
		GenericTextRunProperties genericTextRunProperties = new GenericTextRunProperties(P_3, P_4, null, P_5, null, BaselineAlignment.Baseline, P_1);
		_latestPosition = _formatRuns.SetValue(0, _text.Length, genericTextRunProperties, _latestPosition);
		_defaultParaProps = new GenericTextParagraphProperties(P_2, TextAlignment.Left, false, false, genericTextRunProperties, TextWrapping.WrapWithOverflow, 0.0, 0.0, 0.0);
		InvalidateMetrics();
	}

	private static void ValidateFontSize(double P_0)
	{
		if (P_0 <= 0.0)
		{
			throw new ArgumentOutOfRangeException("emSize", "The parameter value must be greater than zero.");
		}
		if (P_0 > 35791.39406666667)
		{
			throw new ArgumentOutOfRangeException("emSize", $"The parameter value cannot be greater than '{35791.39406666667}'");
		}
		if (double.IsNaN(P_0))
		{
			throw new ArgumentOutOfRangeException("emSize", "The parameter value must be a number.");
		}
	}

	private static void ValidateFlowDirection(FlowDirection P_0, string P_1)
	{
		if (P_0 < FlowDirection.LeftToRight || P_0 > FlowDirection.RightToLeft)
		{
			throw new InvalidEnumArgumentException(P_1, (int)P_0, typeof(FlowDirection));
		}
	}

	private void InvalidateMetrics()
	{
		_metrics = null;
	}

	private LineEnumerator GetEnumerator()
	{
		return new LineEnumerator(this);
	}

	private void AdvanceLineOrigin(ref Point P_0, TextLine P_1)
	{
		double height = P_1.Height;
		FlowDirection flowDirection = _defaultParaProps.FlowDirection;
		if ((uint)flowDirection <= 1u)
		{
			P_0 = P_0.WithY(P_0.Y + height);
		}
	}

	internal void Draw(DrawingContext P_0, Point P_1)
	{
		Point point = P_1;
		if (_metrics != null && !double.IsNaN(_metrics.Extent))
		{
			using (LineEnumerator lineEnumerator = GetEnumerator())
			{
				while (lineEnumerator.MoveNext())
				{
					TextLine current = lineEnumerator.Current;
					current.Draw(P_0, point);
					AdvanceLineOrigin(ref point, current);
				}
				return;
			}
		}
		_metrics = DrawAndCalculateMetrics(P_0, P_1, true);
	}

	private CachedMetrics DrawAndCalculateMetrics(DrawingContext? P_0, Point P_1, bool P_2)
	{
		CachedMetrics cachedMetrics = new CachedMetrics();
		if (_text.Length == 0)
		{
			return cachedMetrics;
		}
		using (LineEnumerator lineEnumerator = GetEnumerator())
		{
			bool flag = true;
			double num2;
			double num = (num2 = double.MaxValue);
			double num4;
			double num3 = (num4 = double.MinValue);
			Point point = new Point(0.0, 0.0);
			double num5 = double.MaxValue;
			while (lineEnumerator.MoveNext())
			{
				TextLine current = lineEnumerator.Current;
				if (P_0 != null)
				{
					current.Draw(P_0, new Point(point.X + P_1.X, point.Y + P_1.Y));
				}
				if (P_2)
				{
					double val = point.X + current.Start + current.OverhangLeading;
					double num6 = point.X + current.Start + current.Width - current.OverhangTrailing;
					double num7 = point.Y + current.Height + current.OverhangAfter;
					double val2 = num7 - current.Extent;
					num = Math.Min(num, val);
					num3 = Math.Max(num3, num6);
					num4 = Math.Max(num4, num7);
					num2 = Math.Min(num2, val2);
					cachedMetrics.OverhangAfter = current.OverhangAfter;
				}
				cachedMetrics.Height += current.Height;
				cachedMetrics.Width = Math.Max(cachedMetrics.Width, current.Width);
				cachedMetrics.WidthIncludingTrailingWhitespace = Math.Max(cachedMetrics.WidthIncludingTrailingWhitespace, current.WidthIncludingTrailingWhitespace);
				num5 = Math.Min(num5, current.Start);
				if (flag)
				{
					cachedMetrics.Baseline = current.Baseline;
					flag = false;
				}
				AdvanceLineOrigin(ref point, current);
			}
			if (P_2)
			{
				cachedMetrics.Extent = num4 - num2;
				cachedMetrics.OverhangLeading = num - num5;
				cachedMetrics.OverhangTrailing = cachedMetrics.Width - (num3 - num5);
			}
			else
			{
				cachedMetrics.Extent = double.NaN;
			}
		}
		return cachedMetrics;
	}
}

