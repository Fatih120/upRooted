// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Markdown.Components.CHyperlink
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using RootApp.Client.Avalonia.Markdown.Components;
using RootApp.Client.Avalonia.Markdown.Components.Geometries;

public class CHyperlink : CSpan
{
	public static readonly StyledProperty<IBrush?> HoverBackgroundProperty = AvaloniaProperty.Register<CHyperlink, IBrush>("HoverBackground");

	public static readonly StyledProperty<IBrush?> HoverForegroundProperty = AvaloniaProperty.Register<CHyperlink, IBrush>("HoverForeground");

	[CompilerGenerated]
	private Action<string>? _003CCommand_003Ek__BackingField;

	[CompilerGenerated]
	private string? _003CCommandParameter_003Ek__BackingField;

	private Cursor? _previousCursor;

	public IBrush? HoverBackground => GetValue(HoverBackgroundProperty);

	public IBrush? HoverForeground => GetValue(HoverForegroundProperty);

	public Action<string>? Command
	{
		[CompilerGenerated]
		get
		{
			return _003CCommand_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCommand_003Ek__BackingField = action;
		}
	}

	public string? CommandParameter
	{
		[CompilerGenerated]
		get
		{
			return _003CCommandParameter_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CCommandParameter_003Ek__BackingField = text;
		}
	}

	public CHyperlink(IEnumerable<CInline> P_0)
		: base(P_0)
	{
	}

	protected override IEnumerable<CGeometry> MeasureOverride(double P_0, double P_1)
	{
		IEnumerable<CGeometry> metrics = base.MeasureOverride(P_0, P_1);
		foreach (CGeometry metry in metrics)
		{
			metry.OnClick = delegate
			{
				Command?.Invoke(CommandParameter ?? string.Empty);
			};
			metry.OnMousePressed = delegate
			{
				base.PseudoClasses.Add(":pressed");
				base.IsPressed = true;
				if (base.PressedScale != 1.0)
				{
					RequestRender();
				}
			};
			metry.OnMouseReleased = delegate
			{
				base.PseudoClasses.Remove(":pressed");
				base.IsPressed = false;
				if (base.PressedScale != 1.0)
				{
					RequestRender();
				}
			};
			metry.OnMouseEnter = delegate(Control ctrl)
			{
				base.PseudoClasses.Add(":pointerover");
				base.PseudoClasses.Add(":hover");
				try
				{
					_previousCursor = ctrl.Cursor;
					ctrl.Cursor = new Cursor(StandardCursorType.Hand);
				}
				catch
				{
				}
				IEnumerable<TextGeometry> enumerable2;
				if (!(metry is DecoratorGeometry decoratorGeometry))
				{
					IEnumerable<TextGeometry> enumerable = ((!(metry is TextGeometry textGeometry)) ? new TextGeometry[0] : new TextGeometry[1] { textGeometry });
					enumerable2 = enumerable;
				}
				else
				{
					enumerable2 = decoratorGeometry.Targets.OfType<TextGeometry>();
				}
				IEnumerable<TextGeometry> enumerable3 = enumerable2;
				if (enumerable3 != null)
				{
					foreach (TextGeometry item in enumerable3)
					{
						item.TemporaryForeground = HoverForeground;
						item.TemporaryBackground = HoverBackground;
					}
					RequestRender();
				}
			};
			metry.OnMouseLeave = delegate(Control ctrl)
			{
				base.PseudoClasses.Remove(":pointerover");
				base.PseudoClasses.Remove(":hover");
				ctrl.Cursor = _previousCursor;
				IEnumerable<TextGeometry> enumerable2;
				if (!(metry is DecoratorGeometry decoratorGeometry))
				{
					IEnumerable<TextGeometry> enumerable = ((!(metry is TextGeometry textGeometry)) ? new TextGeometry[0] : new TextGeometry[1] { textGeometry });
					enumerable2 = enumerable;
				}
				else
				{
					enumerable2 = decoratorGeometry.Targets.OfType<TextGeometry>();
				}
				IEnumerable<TextGeometry> enumerable3 = enumerable2;
				if (enumerable3 != null)
				{
					foreach (TextGeometry item2 in enumerable3)
					{
						item2.TemporaryForeground = null;
						item2.TemporaryBackground = null;
					}
					RequestRender();
				}
			};
			yield return metry;
		}
	}
}

