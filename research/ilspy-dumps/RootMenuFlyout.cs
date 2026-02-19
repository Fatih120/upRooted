// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootMenuFlyout
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using RootApp.Client.Avalonia.UI.Main;

public class RootMenuFlyout : MenuFlyout
{
	private LayoutTransformControl? _zoomWrapper;

	private Control? _innerPresenter;

	private readonly ScaleTransform _popupScale = new ScaleTransform(1.0, 1.0);

	private object? _zoomServiceRef;

	protected override Control CreatePresenter()
	{
		Control child = (_innerPresenter = base.CreatePresenter());
		_zoomWrapper = new LayoutTransformControl
		{
			LayoutTransform = _popupScale,
			Child = child
		};
		return _zoomWrapper;
	}

	protected override void OnOpened()
	{
		if (base.Popup.HorizontalOffset != 0.0)
		{
			base.Popup.HorizontalOffset += -12.0;
		}
		else
		{
			base.Popup.HorizontalOffset = -12.0;
		}
		if (base.Popup.VerticalOffset != 0.0)
		{
			base.Popup.VerticalOffset += -12.0;
		}
		else
		{
			base.Popup.VerticalOffset = -12.0;
		}
		if (_zoomWrapper != null)
		{
			RenderOptions.SetBitmapInterpolationMode(_zoomWrapper, BitmapInterpolationMode.MediumQuality);
		}
		if (_innerPresenter != null)
		{
			RenderOptions.SetBitmapInterpolationMode(_innerPresenter, BitmapInterpolationMode.MediumQuality);
		}
		double num = 1.0;
		if (Application.Current?.ApplicationLifetime is ClassicDesktopStyleApplicationLifetime classicDesktopStyleApplicationLifetime && classicDesktopStyleApplicationLifetime.MainWindow?.DataContext is MainViewModel mainViewModel)
		{
			num = mainViewModel.ZoomService.Zoom;
			ApplyZoom(num);
			_zoomServiceRef = mainViewModel.ZoomService;
			mainViewModel.ZoomService.ZoomChanged += OnGlobalZoomChanged;
		}
		else
		{
			ApplyZoom(num);
		}
		base.OnOpened();
	}

	protected override void OnClosed()
	{
		if (_zoomServiceRef != null && Application.Current?.ApplicationLifetime is ClassicDesktopStyleApplicationLifetime classicDesktopStyleApplicationLifetime && classicDesktopStyleApplicationLifetime.MainWindow?.DataContext is MainViewModel mainViewModel && mainViewModel.ZoomService == _zoomServiceRef)
		{
			mainViewModel.ZoomService.ZoomChanged -= OnGlobalZoomChanged;
		}
		_zoomServiceRef = null;
		base.OnClosed();
	}

	private void OnGlobalZoomChanged(double z)
	{
		Dispatcher.UIThread.Post(delegate
		{
			ApplyZoom(z);
		});
	}

	private void ApplyZoom(double P_0)
	{
		_popupScale.ScaleX = P_0;
		_popupScale.ScaleY = P_0;
	}
}

