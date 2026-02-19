// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootToolTip
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Threading;
using RootApp.Client.Avalonia.UI.Main;

public class RootToolTip : ToolTip
{
	private LayoutTransformControl? _zoomWrapper;

	private readonly ScaleTransform _scale = new ScaleTransform(1.0, 1.0);

	private object? _zoomServiceRef;

	protected override void OnApplyTemplate(TemplateAppliedEventArgs P_0)
	{
		base.OnApplyTemplate(P_0);
		_zoomWrapper = P_0.NameScope.Find<LayoutTransformControl>("PART_ZoomWrapper");
		if (_zoomWrapper != null)
		{
			_zoomWrapper.LayoutTransform = _scale;
		}
	}

	protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnAttachedToVisualTree(P_0);
		double num = 1.0;
		if (Application.Current?.ApplicationLifetime is ClassicDesktopStyleApplicationLifetime classicDesktopStyleApplicationLifetime && classicDesktopStyleApplicationLifetime.MainWindow?.DataContext is MainViewModel mainViewModel)
		{
			num = mainViewModel.ZoomService.Zoom;
			ApplyZoom(num);
			_zoomServiceRef = mainViewModel.ZoomService;
			mainViewModel.ZoomService.ZoomChanged += OnZoomChanged;
		}
		else
		{
			ApplyZoom(num);
		}
	}

	protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		if (_zoomServiceRef != null && Application.Current?.ApplicationLifetime is ClassicDesktopStyleApplicationLifetime classicDesktopStyleApplicationLifetime && classicDesktopStyleApplicationLifetime.MainWindow?.DataContext is MainViewModel mainViewModel && mainViewModel.ZoomService == _zoomServiceRef)
		{
			mainViewModel.ZoomService.ZoomChanged -= OnZoomChanged;
		}
		_zoomServiceRef = null;
		base.OnDetachedFromVisualTree(P_0);
	}

	private void OnZoomChanged(double z)
	{
		Dispatcher.UIThread.Post(delegate
		{
			ApplyZoom(z);
		});
	}

	private void ApplyZoom(double P_0)
	{
		_scale.ScaleX = P_0;
		_scale.ScaleY = P_0;
	}
}

