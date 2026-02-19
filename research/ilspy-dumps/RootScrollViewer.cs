// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootScrollViewer
using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using RootApp.Client.Avalonia.Controls;

public class RootScrollViewer : ScrollViewer
{
	public static readonly AttachedProperty<bool> IsMouseOverProperty = AvaloniaProperty.RegisterAttached<RootScrollViewer, Control, bool>("IsMouseOver", false);

	public static readonly AttachedProperty<bool> EnableDropShadowOnScrollProperty = AvaloniaProperty.RegisterAttached<RootScrollViewer, Control, bool>("EnableDropShadowOnScroll", false);

	private bool _isRenderLoopActive;

	private TimeSpan _lastRenderTime;

	private double _vy;

	private double _lastOffsetY = double.NaN;

	private double _y;

	private double _cachedOffsetX;

	private bool _isThumbDragging;

	private bool _isMiddleClickScrolling;

	private bool _isMiddleButtonHeld;

	private bool _isToggleMode;

	private Point _middleClickOrigin;

	private double _middleClickVelocity;

	private TopLevel? _cachedTopLevel;

	private DateTime _middleClickStartTime;

	public static void SetIsMouseOver(AvaloniaObject P_0, bool P_1)
	{
		P_0.SetValue(IsMouseOverProperty, P_1);
	}

	public static void SetEnableDropShadowOnScroll(AvaloniaObject P_0, bool P_1)
	{
		P_0.SetValue(EnableDropShadowOnScrollProperty, P_1);
	}

	protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnAttachedToVisualTree(P_0);
		AddHandler(InputElement.PointerWheelChangedEvent, OnWheel, RoutingStrategies.Tunnel, true);
		Vector offset = base.Offset;
		_y = offset.Y;
		_cachedOffsetX = offset.X;
		_lastOffsetY = offset.Y;
		StopRenderLoop();
		ResetMotion();
	}

	protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		RemoveHandler(InputElement.PointerWheelChangedEvent, OnWheel);
		StopRenderLoop();
		if (_isMiddleClickScrolling)
		{
			StopMiddleClickScrolling();
		}
		base.OnDetachedFromVisualTree(P_0);
	}

	protected override void OnPointerEntered(PointerEventArgs P_0)
	{
		SetIsMouseOver(this, true);
		base.OnPointerEntered(P_0);
	}

	protected override void OnPointerExited(PointerEventArgs P_0)
	{
		SetIsMouseOver(this, false);
		base.OnPointerExited(P_0);
	}

	protected override void OnPointerPressed(PointerPressedEventArgs P_0)
	{
		base.OnPointerPressed(P_0);
		PointerPointProperties properties = P_0.GetCurrentPoint(this).Properties;
		if (_isMiddleClickScrolling && _isToggleMode)
		{
			StopMiddleClickScrolling(P_0.Pointer);
			P_0.Handled = true;
		}
		else if (properties.IsMiddleButtonPressed && IsScrollable())
		{
			_isMiddleClickScrolling = true;
			_isMiddleButtonHeld = true;
			_isToggleMode = false;
			_middleClickOrigin = P_0.GetPosition(this);
			_middleClickVelocity = 0.0;
			_middleClickStartTime = DateTime.UtcNow;
			ResetMotion();
			_lastOffsetY = (_y = base.Offset.Y);
			P_0.Pointer.Capture(this);
			P_0.Handled = true;
			_cachedTopLevel = TopLevel.GetTopLevel(this);
			_cachedTopLevel?.AddHandler(InputElement.KeyDownEvent, OnTopLevelKeyDownForAutoScroll, RoutingStrategies.Tunnel, true);
			if (_cachedTopLevel is Window window)
			{
				window.Deactivated += OnWindowDeactivated;
			}
			StartRenderLoop();
		}
	}

	protected override void OnPointerMoved(PointerEventArgs P_0)
	{
		base.OnPointerMoved(P_0);
		if (_isMiddleClickScrolling)
		{
			double num = P_0.GetPosition(this).Y - _middleClickOrigin.Y;
			if (Math.Abs(num) < 15.0)
			{
				_middleClickVelocity = 0.0;
			}
			else
			{
				double num2 = num - (double)Math.Sign(num) * 15.0;
				_middleClickVelocity = Math.Clamp(num2 * 0.12 * 60.0, -2000.0, 2000.0);
			}
			P_0.Handled = true;
		}
	}

	protected override void OnPointerReleased(PointerReleasedEventArgs P_0)
	{
		base.OnPointerReleased(P_0);
		if (_isMiddleClickScrolling && _isMiddleButtonHeld && P_0.InitialPressMouseButton == MouseButton.Middle)
		{
			_isMiddleButtonHeld = false;
			double totalMilliseconds = (DateTime.UtcNow - _middleClickStartTime).TotalMilliseconds;
			if (totalMilliseconds < 200.0)
			{
				_isToggleMode = true;
				_cachedTopLevel?.AddHandler(InputElement.PointerPressedEvent, OnTopLevelPointerPressedForAutoScroll, RoutingStrategies.Tunnel, true);
				P_0.Handled = true;
			}
			else
			{
				StopMiddleClickScrolling(P_0.Pointer);
				P_0.Handled = true;
			}
		}
	}

	private void StopMiddleClickScrolling(IPointer? P_0 = null)
	{
		if (!_isMiddleClickScrolling)
		{
			return;
		}
		_isMiddleClickScrolling = false;
		_isMiddleButtonHeld = false;
		_isToggleMode = false;
		_middleClickVelocity = 0.0;
		if (_cachedTopLevel != null)
		{
			_cachedTopLevel.RemoveHandler(InputElement.KeyDownEvent, OnTopLevelKeyDownForAutoScroll);
			_cachedTopLevel.RemoveHandler(InputElement.PointerPressedEvent, OnTopLevelPointerPressedForAutoScroll);
			if (_cachedTopLevel is Window window)
			{
				window.Deactivated -= OnWindowDeactivated;
			}
			_cachedTopLevel = null;
		}
		P_0?.Capture(null);
		if (Math.Abs(_vy) < 10.0)
		{
			StopRenderLoop();
		}
	}

	private void OnTopLevelKeyDownForAutoScroll(object? sender, KeyEventArgs e)
	{
		if (_isMiddleClickScrolling)
		{
			StopMiddleClickScrolling();
			e.Handled = true;
		}
	}

	private void OnTopLevelPointerPressedForAutoScroll(object? sender, PointerPressedEventArgs e)
	{
		if (_isMiddleClickScrolling && _isToggleMode)
		{
			StopMiddleClickScrolling(e.Pointer);
			e.Handled = true;
		}
	}

	private void OnWindowDeactivated(object? sender, EventArgs e)
	{
		if (_isMiddleClickScrolling)
		{
			StopMiddleClickScrolling();
		}
	}

	protected override void OnScrollChanged(ScrollChangedEventArgs P_0)
	{
		base.OnScrollChanged(P_0);
		Vector offset = base.Offset;
		_cachedOffsetX = offset.X;
		if (_isThumbDragging)
		{
			_lastOffsetY = offset.Y;
			return;
		}
		double num = (_y = offset.Y);
		if (!double.IsNaN(_lastOffsetY))
		{
			double num2 = Math.Abs(num - _lastOffsetY);
			double num3 = Math.Abs(P_0.ExtentDelta.Y);
			if (!(num3 <= 50.0))
			{
				if (!IsScrollable())
				{
					_y = offset.Y;
					_vy = 0.0;
					_lastOffsetY = _y;
					StopRenderLoop();
					return;
				}
				double num4 = Math.Max(0.0, base.Extent.Height - base.Viewport.Height);
				double num5 = Math.Clamp(num, 0.0, num4);
				if (Math.Abs(num5 - num) > 0.01)
				{
					num = num5;
					_y = num5;
					base.Offset = new Vector(_cachedOffsetX, num5);
				}
				_vy *= 1.0;
				_lastOffsetY = num;
				return;
			}
			if (num2 > 200.0)
			{
				ResetMotion();
				StopRenderLoop();
			}
		}
		_lastOffsetY = num;
	}

	private static bool IsTrackpad(PointerWheelEventArgs P_0)
	{
		Vector delta = P_0.Delta;
		return Math.Abs(delta.Y) < 1.0 && Math.Abs(delta.X) < 1.0;
	}

	private void OnWheel(object? sender, PointerWheelEventArgs e)
	{
		if (!IsTrackpad(e) && IsScrollable() && !_isThumbDragging)
		{
			_lastOffsetY = (_y = base.Offset.Y);
			double num = 0.0 - e.Delta.Y;
			double num2 = num * 20.0 * 60.0;
			if (Math.Sign(num2) != 0 && Math.Sign(_vy) != 0 && Math.Sign(num2) != Math.Sign(_vy))
			{
				_vy *= 0.35;
			}
			_vy += num2;
			_vy = Math.Clamp(_vy, -4000.0, 4000.0);
			bool flag = true;
			ApplyImmediateKick(0.0125);
			e.Handled = true;
			StartRenderLoop();
		}
	}

	private void StartRenderLoop()
	{
		if (!_isRenderLoopActive)
		{
			_isRenderLoopActive = true;
			_lastRenderTime = TimeSpan.Zero;
			TopLevel.GetTopLevel(this)?.RequestAnimationFrame(OnAnimationFrame);
		}
	}

	private void StopRenderLoop()
	{
		_isRenderLoopActive = false;
	}

	private void OnAnimationFrame(TimeSpan currentTime)
	{
		if (!_isRenderLoopActive)
		{
			return;
		}
		double num;
		if (_lastRenderTime == TimeSpan.Zero)
		{
			num = 1.0 / 120.0;
		}
		else
		{
			num = (currentTime - _lastRenderTime).TotalSeconds;
			if (num <= 0.0 || num > 0.1)
			{
				num = 1.0 / 120.0;
			}
		}
		_lastRenderTime = currentTime;
		int num2 = Math.Min(6, (int)Math.Round(num / (1.0 / 120.0)));
		if (num2 <= 0)
		{
			num2 = 1;
		}
		if (Math.Abs(_vy) < 10.0 && !_isMiddleClickScrolling)
		{
			_y = base.Offset.Y;
		}
		if (!IsScrollable())
		{
			_y = base.Offset.Y;
			_vy = 0.0;
			_lastOffsetY = _y;
			_isRenderLoopActive = false;
			if (_isMiddleClickScrolling)
			{
				StopMiddleClickScrolling();
			}
			return;
		}
		for (int i = 0; i < num2; i++)
		{
			Step(1.0 / 120.0, _isMiddleClickScrolling ? _middleClickVelocity : 0.0);
		}
		double num3 = Math.Max(0.0, base.Extent.Height - base.Viewport.Height);
		if (_y < 0.0)
		{
			_y = 0.0;
			_vy = 0.0;
		}
		else if (_y > num3)
		{
			_y = num3;
			_vy = 0.0;
		}
		base.Offset = new Vector(_cachedOffsetX, _y);
		_lastOffsetY = _y;
		if (!_isMiddleClickScrolling && Math.Abs(_vy) < 10.0)
		{
			_isRenderLoopActive = false;
		}
		else
		{
			TopLevel.GetTopLevel(this)?.RequestAnimationFrame(OnAnimationFrame);
		}
	}

	private void Step(double P_0, double P_1)
	{
		if (P_1 != 0.0)
		{
			_y += P_1 * P_0;
			return;
		}
		_vy *= Math.Exp(-25.0 * P_0);
		_y += _vy * P_0;
	}

	private void ApplyImmediateKick(double P_0)
	{
		double y = (_y = base.Offset.Y);
		_y += _vy * P_0;
		if (!IsScrollable())
		{
			_y = y;
			_vy = 0.0;
			_lastOffsetY = _y;
			return;
		}
		double num = Math.Max(0.0, base.Extent.Height - base.Viewport.Height);
		_y = Math.Clamp(_y, 0.0, num);
		if (_y <= 0.0 || _y >= num)
		{
			_vy = 0.0;
		}
		base.Offset = new Vector(_cachedOffsetX, _y);
		_lastOffsetY = _y;
	}

	private void ResetMotion()
	{
		_vy = 0.0;
	}

	private bool IsScrollable()
	{
		if (double.IsNaN(base.Extent.Height) || double.IsNaN(base.Viewport.Height))
		{
			return false;
		}
		if (base.Extent.Height <= 0.0 || base.Viewport.Height <= 0.0)
		{
			return false;
		}
		return base.Extent.Height > base.Viewport.Height + 0.5;
	}

	internal void NotifyThumbDragStarted()
	{
		_isThumbDragging = true;
		ResetMotion();
		StopRenderLoop();
	}

	internal void NotifyThumbDragCompleted()
	{
		_isThumbDragging = false;
		_lastOffsetY = (_y = base.Offset.Y);
	}
}

