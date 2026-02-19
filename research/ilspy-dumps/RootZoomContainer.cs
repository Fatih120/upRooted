// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootZoomContainer
using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using RootApp.Client.Avalonia.Helpers.Zoom;

public class RootZoomContainer : LayoutTransformControl
{
	private ScaleTransform _scale = new ScaleTransform(1.0, 1.0);

	private ZoomService? _zoomService;

	public double Zoom
	{
		get
		{
			return _scale.ScaleX;
		}
		set
		{
			double num = Math.Clamp(num2, 0.6, 1.4);
			_scale = new ScaleTransform(num, num);
			base.LayoutTransform = _scale;
			if (_zoomService != null)
			{
				_zoomService.SetZoom(num);
			}
		}
	}

	public RootZoomContainer()
	{
		base.LayoutTransform = _scale;
		base.Focusable = true;
		base.AttachedToVisualTree += delegate
		{
			AddHandler(InputElement.KeyDownEvent, OnKeyDown, RoutingStrategies.Tunnel);
		};
		base.DetachedFromVisualTree += delegate
		{
			RemoveHandler(InputElement.KeyDownEvent, OnKeyDown);
		};
	}

	public void SetZoomService(ZoomService P_0)
	{
		_zoomService = P_0;
		_scale = new ScaleTransform(P_0.Zoom, P_0.Zoom);
		base.LayoutTransform = _scale;
	}

	private void OnKeyDown(object? _, KeyEventArgs e)
	{
		if (((e.KeyModifiers & KeyModifiers.Control) != KeyModifiers.None || (e.KeyModifiers & KeyModifiers.Meta) != KeyModifiers.None) && (e.KeyModifiers & KeyModifiers.Shift) == 0 && (e.KeyModifiers & KeyModifiers.Alt) == 0)
		{
			switch (e.Key)
			{
			case Key.Add:
			case Key.OemPlus:
				Zoom += 0.1;
				e.Handled = true;
				break;
			case Key.Subtract:
			case Key.OemMinus:
				Zoom -= 0.1;
				e.Handled = true;
				break;
			case Key.D0:
			case Key.NumPad0:
				Zoom = 1.0;
				e.Handled = true;
				break;
			}
		}
	}
}
