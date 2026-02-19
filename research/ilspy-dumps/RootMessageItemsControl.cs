// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootMessageItemsControl
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Input;
using Avalonia.Media;
using RootApp.Client.Avalonia.Helpers.Messages;
using RootApp.Client.Avalonia.Markdown.Components;
using RootApp.Client.Avalonia.Markdown.DocumentElements;
using RootApp.Client.Avalonia.Markdown.DocumentElements.Elements;
using RootApp.Client.Avalonia.Markdown.Utils;

public class RootMessageItemsControl : ItemsControl
{
	private SelectionList? _prevSelection;

	private EnumerableEx<DocumentElement>? _children;

	private bool _isLeftButtonPressed;

	private Point _startPoint;

	protected override Type StyleKeyOverride => typeof(ItemsControl);

	public RootMessageItemsControl()
	{
		base.Background = Brushes.Transparent;
	}

	protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnAttachedToVisualTree(P_0);
		base.PointerPressed += pointerPressed;
		base.PointerMoved += pointerMoved;
		base.PointerReleased += pointerReleased;
	}

	protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnDetachedFromVisualTree(P_0);
		base.PointerPressed -= pointerPressed;
		base.PointerMoved -= pointerMoved;
		base.PointerReleased -= pointerReleased;
	}

	public void Select(Point P_0, Point P_1)
	{
		if (_prevSelection != null)
		{
			foreach (DocumentElement item in _prevSelection)
			{
				item.UnSelect();
				_prevSelection = null;
			}
		}
		_children = (from selectableMessage in (from c in base.ItemsPanelRoot?.Children.OfType<ContentPresenter>()
				select c.Child).OfType<ISelectableMessage>()
			where selectableMessage.Document != null
			select selectableMessage.Document).ToEnumerable();
		EnumerableEx<DocumentElement> children = _children;
		if (children != null && children.Count > 0)
		{
			SelectionList prevSelection = SelectionUtil.SelectVertical(this, _children, P_0, P_1);
			_prevSelection = prevSelection;
		}
	}

	private void pointerPressed(object? sender, PointerPressedEventArgs e)
	{
		try
		{
			PointerPoint currentPoint = e.GetCurrentPoint(this);
			if (currentPoint.Properties.IsLeftButtonPressed)
			{
				if (e.ClickCount == 2)
				{
					SelectWordAt(currentPoint.Position);
					return;
				}
				if (e.ClickCount == 3)
				{
					SelectLineAt(currentPoint.Position);
					return;
				}
				_isLeftButtonPressed = true;
				_startPoint = currentPoint.Position;
				Select(_startPoint, currentPoint.Position);
			}
		}
		catch
		{
		}
	}

	private void pointerMoved(object? sender, PointerEventArgs e)
	{
		try
		{
			PointerPoint currentPoint = e.GetCurrentPoint(this);
			if (_isLeftButtonPressed && currentPoint.Properties.IsLeftButtonPressed)
			{
				Select(_startPoint, currentPoint.Position);
			}
		}
		catch
		{
		}
	}

	private void pointerReleased(object? sender, PointerReleasedEventArgs e)
	{
		try
		{
			PointerPoint currentPoint = e.GetCurrentPoint(this);
			if (_isLeftButtonPressed && !currentPoint.Properties.IsLeftButtonPressed)
			{
				_isLeftButtonPressed = false;
			}
		}
		catch
		{
			_isLeftButtonPressed = false;
		}
	}

	private DocumentElement? HitTestDocumentElement(Point P_0)
	{
		if (base.ItemsPanelRoot == null)
		{
			return null;
		}
		foreach (Control child2 in base.ItemsPanelRoot.Children)
		{
			if (!(child2 is ContentPresenter { Child: ISelectableMessage child }))
			{
				continue;
			}
			if (child.Document is DocumentRootElement documentRootElement)
			{
				foreach (DocumentElement child3 in documentRootElement.Children)
				{
					if (child3.GetRect(this).Contains(P_0))
					{
						return child3;
					}
				}
			}
			else if (child.Document is ListBlockElement listBlockElement)
			{
				foreach (DocumentElement child4 in listBlockElement.Children)
				{
					if (child4.GetRect(this).Contains(P_0))
					{
						return child4;
					}
				}
			}
			else if (child.Document != null && child.Document.GetRect(this).Contains(P_0))
			{
				return child.Document;
			}
		}
		return null;
	}

	private TextPointer? GetTextPointerAt(Point P_0, out DocumentElement? P_1)
	{
		P_1 = HitTestDocumentElement(P_0);
		if (P_1 == null)
		{
			return null;
		}
		DocumentElement documentElement = FindLeafDocumentElement(P_1, P_0);
		if (documentElement == null)
		{
			return null;
		}
		if (documentElement is ListItemElement listItemElement)
		{
			foreach (DocumentElement child in listItemElement.Children)
			{
				if (child.Control is ITextPointerHandleable)
				{
					documentElement = child;
					break;
				}
			}
		}
		P_1 = documentElement;
		Point point = P_0 - documentElement.GetRect(this).Position;
		if (documentElement.Control is ITextPointerHandleable textPointerHandleable)
		{
			return textPointerHandleable.CalcuatePointerFrom(point.X, point.Y);
		}
		return null;
	}

	private DocumentElement? FindLeafDocumentElement(DocumentElement P_0, Point P_1)
	{
		if (!P_0.GetRect(this).Contains(P_1))
		{
			return null;
		}
		List<DocumentElement> list = P_0.Children?.ToList() ?? new List<DocumentElement>();
		if (list.Count == 0)
		{
			return P_0;
		}
		foreach (DocumentElement item in list)
		{
			DocumentElement documentElement = FindLeafDocumentElement(item, P_1);
			if (documentElement != null)
			{
				return documentElement;
			}
		}
		return P_0;
	}

	private List<(CInline inline, int start, int length)> FlattenInlines(IEnumerable<CInline> P_0)
	{
		List<(CInline, int, int)> list = new List<(CInline, int, int)>();
		int num = 0;
		foreach (CInline item in P_0)
		{
			string text = item.AsString();
			int length = text.Length;
			list.Add((item, num, length));
			num += length;
		}
		return list;
	}

	private (TextPointer wordStart, TextPointer wordEnd)? FindWordBoundaries(TextPointer P_0, CTextBlockElement P_1)
	{
		if (!(P_1.Control is CTextBlock cTextBlock))
		{
			return null;
		}
		string text = P_1.Text;
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		int index = cTextBlock.GetBegin().Index;
		int index2 = cTextBlock.GetEnd().Index;
		int num = Math.Clamp(P_0.Index, index, index2);
		List<(CInline, int, int)> list = FlattenInlines(cTextBlock.Content);
		(CInline, int, int)? tuple = null;
		foreach (var item2 in list)
		{
			if (num >= item2.Item2 && num < item2.Item2 + item2.Item3)
			{
				tuple = item2;
				break;
			}
		}
		if (!tuple.HasValue)
		{
			tuple = list.LastOrDefault();
		}
		if (!tuple.HasValue)
		{
			return null;
		}
		int item = tuple.Value.Item2;
		int num2 = tuple.Value.Item2 + tuple.Value.Item3;
		int num3 = num - item;
		int num4;
		int val;
		if (tuple.Value.Item1 is CImage || tuple.Value.Item1 is CHyperlink || tuple.Value.Item1 is CSpan { HasBorder: not false })
		{
			num4 = item;
			val = num2;
		}
		else
		{
			string text2 = tuple.Value.Item1.AsString();
			int num5 = num3;
			int i = num3;
			while (num5 > 0 && !char.IsWhiteSpace(text2[num5 - 1]))
			{
				num5--;
			}
			for (; i < text2.Length && !char.IsWhiteSpace(text2[i]); i++)
			{
			}
			num4 = item + num5;
			val = item + i;
		}
		num4 = Math.Max(num4, index);
		val = Math.Min(val, index2);
		TextPointer textPointer = cTextBlock.CalcuatePointerFrom(num4);
		TextPointer textPointer2 = ((val == index2) ? cTextBlock.GetEnd() : cTextBlock.CalcuatePointerFrom(val));
		return (textPointer, textPointer2);
	}

	private (TextPointer lineStart, TextPointer lineEnd)? FindLineBoundaries(TextPointer P_0, CTextBlockElement P_1)
	{
		if (P_1.Control is CTextBlock cTextBlock)
		{
			TextPointer begin = cTextBlock.GetBegin();
			TextPointer end = cTextBlock.GetEnd();
			return (begin, end);
		}
		return null;
	}

	public void SelectWordAt(Point P_0)
	{
		DocumentElement documentElement;
		TextPointer textPointerAt = GetTextPointerAt(P_0, out documentElement);
		if (textPointerAt != null && documentElement != null && documentElement is CTextBlockElement cTextBlockElement)
		{
			(TextPointer, TextPointer)? tuple = FindWordBoundaries(textPointerAt, cTextBlockElement);
			if (tuple.HasValue && cTextBlockElement.Control is CTextBlock cTextBlock)
			{
				cTextBlock.Select(tuple.Value.Item1, tuple.Value.Item2);
				_prevSelection = new SelectionList(SelectDirection.Forward, SelectRange.Part, new CTextBlockElement[1] { cTextBlockElement });
			}
		}
	}

	public void SelectLineAt(Point P_0)
	{
		DocumentElement documentElement;
		TextPointer textPointerAt = GetTextPointerAt(P_0, out documentElement);
		if (textPointerAt != null && documentElement != null && documentElement is CTextBlockElement cTextBlockElement)
		{
			(TextPointer, TextPointer)? tuple = FindLineBoundaries(textPointerAt, cTextBlockElement);
			if (tuple.HasValue && cTextBlockElement.Control is CTextBlock cTextBlock)
			{
				cTextBlock.Select(tuple.Value.Item1, tuple.Value.Item2);
				_prevSelection = new SelectionList(SelectDirection.Forward, SelectRange.Part, new CTextBlockElement[1] { cTextBlockElement });
			}
		}
	}

	public void UnSelect()
	{
		if (_children == null)
		{
			return;
		}
		foreach (DocumentElement child in _children)
		{
			child.UnSelect();
		}
		_children = null;
		_prevSelection = null;
	}

	public string GetSelectedText()
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (_prevSelection != null)
		{
			foreach (DocumentElement item in _prevSelection)
			{
				item.ConstructSelectedText(stringBuilder);
				if (stringBuilder.Length > 0)
				{
					if (stringBuilder[stringBuilder.Length - 1] != '\n')
					{
						stringBuilder.Append('\n');
					}
				}
			}
		}
		return stringBuilder.ToString().TrimEnd('\n');
	}
}

