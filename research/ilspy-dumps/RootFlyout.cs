// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootFlyout
using System;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.VisualTree;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.UI.Main;

public class RootFlyout : Flyout
{
	private IDisposable? _isOpenSubscription;

	private LightDismissOverlayLayer? _lightDismissOverlayLayer;

	private LayoutTransformControl? _zoomWrapper;

	private Control? _presenter;

	private double _currentZoom = 1.0;

	public static readonly StyledProperty<bool> IsPopupOpenProperty = AvaloniaProperty.Register<RootFlyout, bool>("IsPopupOpen", false);

	public static readonly StyledProperty<bool> LimitSizeToWindowProperty = AvaloniaProperty.Register<RootFlyout, bool>("LimitSizeToWindow", false);

	public static readonly StyledProperty<bool> DismissOnClickAwayProperty = AvaloniaProperty.Register<RootFlyout, bool>("DismissOnClickAway", true);

	public bool IsPopupOpen
	{
		set
		{
			SetValue(IsPopupOpenProperty, value2);
		}
	}

	public bool LimitSizeToWindow
	{
		get
		{
			return GetValue(LimitSizeToWindowProperty);
		}
		set
		{
			SetValue(LimitSizeToWindowProperty, value2);
		}
	}

	public bool DismissOnClickAway => GetValue(DismissOnClickAwayProperty);

	public Popup FlyoutPopup => base.Popup;

	protected override Control CreatePresenter()
	{
		Control control = (_presenter = base.CreatePresenter());
		if (control is ContentControl contentControl && contentControl.Content is Control)
		{
			EnsureChildWrapped(contentControl);
		}
		return control;
	}

	private void EnsureChildWrapped(ContentControl P_0)
	{
		if (_zoomWrapper == null)
		{
			if (P_0.Content is LayoutTransformControl zoomWrapper)
			{
				_zoomWrapper = zoomWrapper;
			}
			else if (P_0.Content is Control control)
			{
				P_0.Content = null;
				_zoomWrapper = new LayoutTransformControl
				{
					Child = control
				};
				P_0.Content = _zoomWrapper;
				RenderOptions.SetBitmapInterpolationMode(_zoomWrapper, BitmapInterpolationMode.MediumQuality);
				RenderOptions.SetBitmapInterpolationMode(control, BitmapInterpolationMode.MediumQuality);
			}
		}
	}

	protected override void OnOpening(CancelEventArgs P_0)
	{
		base.OnOpening(P_0);
		_currentZoom = 1.0;
		if (Application.Current?.ApplicationLifetime is ClassicDesktopStyleApplicationLifetime classicDesktopStyleApplicationLifetime && classicDesktopStyleApplicationLifetime.MainWindow?.DataContext is MainViewModel mainViewModel)
		{
			_currentZoom = mainViewModel.ZoomService.Zoom;
		}
		if (_zoomWrapper != null)
		{
			ApplyZoom(_currentZoom);
		}
	}

	protected override void OnOpened()
	{
		base.OnOpened();
		IsPopupOpen = true;
		if (LimitSizeToWindow)
		{
			DeterminePopupMaxHeightAdjustedForZoom(_currentZoom);
		}
		_isOpenSubscription = this.GetObservable(IsPopupOpenProperty).Subscribe(delegate(bool isOpen)
		{
			if (isOpen && !base.IsOpen)
			{
				base.Popup.Open();
			}
			else if (!isOpen && base.IsOpen)
			{
				base.Popup.Close();
			}
		});
		if (base.Popup.PlacementTarget != null && DismissOnClickAway)
		{
			_lightDismissOverlayLayer = LightDismissOverlayLayer.GetLightDismissOverlayLayer(base.Popup.PlacementTarget);
			if (_lightDismissOverlayLayer != null)
			{
				_lightDismissOverlayLayer.IsVisible = true;
				_lightDismissOverlayLayer.PointerPressed += onPointerPressed;
			}
		}
	}

	protected override void OnClosed()
	{
		base.OnClosed();
		IsPopupOpen = false;
		_isOpenSubscription?.Dispose();
		_isOpenSubscription = null;
		if (_lightDismissOverlayLayer != null)
		{
			_lightDismissOverlayLayer.IsVisible = false;
			_lightDismissOverlayLayer.PointerPressed -= onPointerPressed;
			_lightDismissOverlayLayer = null;
		}
	}

	private void ApplyZoom(double P_0)
	{
		if (_zoomWrapper != null)
		{
			_zoomWrapper.LayoutTransform = new ScaleTransform(P_0, P_0);
		}
	}

	private void onPointerPressed(object? sender, PointerPressedEventArgs e)
	{
		base.Popup.Close();
	}

	private void DeterminePopupMaxHeightAdjustedForZoom(double P_0)
	{
		if (base.Popup.PlacementTarget != null && base.Popup.PlacementTarget.GetVisualRoot() is Window window)
		{
			Rect bounds = base.Popup.PlacementTarget.Bounds;
			Point point = base.Popup.PlacementTarget.TranslatePoint(bounds.Position, window) ?? new Point(0.0, 0.0);
			double num = window.Bounds.Height - (point.Y + bounds.Height);
			base.Popup.MaxHeight = num / Math.Max(P_0, 0.0001);
		}
	}
}

