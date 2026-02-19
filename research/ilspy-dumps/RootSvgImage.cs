// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootSvgImage
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media.Imaging;
using Avalonia.Metadata;
using Avalonia.Svg.Skia;
using Avalonia.Threading;
using Microsoft.VisualStudio.Threading;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Helpers.Zoom;
using RootApp.Client.Avalonia.UI.Main;
using SkiaSharp;

public class RootSvgImage : Image
{
	private static readonly object _cacheGate = new object();

	private static readonly Dictionary<(string Path, int W, int H), Bitmap> _cache = new Dictionary<(string, int, int), Bitmap>();

	private IDisposable? _svgPathSub;

	private ZoomService? _zoomService;

	public static readonly StyledProperty<string?> SvgPathProperty = AvaloniaProperty.Register<RootSvgImage, string>("SvgPath");

	[Content]
	public string? SvgPath => GetValue(SvgPathProperty);

	protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnAttachedToVisualTree(P_0);
		if (_zoomService == null && Application.Current?.ApplicationLifetime is ClassicDesktopStyleApplicationLifetime classicDesktopStyleApplicationLifetime && classicDesktopStyleApplicationLifetime.MainWindow?.DataContext is MainViewModel mainViewModel)
		{
			_zoomService = mainViewModel.ZoomService;
		}
		if (_zoomService != null)
		{
			_zoomService.ZoomChanged += onZoomServiceZoomChanged;
			_svgPathSub = this.GetObservable(SvgPathProperty).Subscribe(delegate
			{
				UpdateImageSourceAsync().Forget();
			});
		}
	}

	protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnDetachedFromVisualTree(P_0);
		if (_zoomService != null)
		{
			_zoomService.ZoomChanged -= onZoomServiceZoomChanged;
			_svgPathSub?.Dispose();
			_svgPathSub = null;
		}
	}

	private void onZoomServiceZoomChanged(double zoomLevel)
	{
		lock (_cacheGate)
		{
			_cache.Clear();
		}
		Dispatcher.UIThread.InvokeAsync((Func<Task>)UpdateImageSourceAsync, DispatcherPriority.Background).Forget();
	}

	private async Task UpdateImageSourceAsync()
	{
		string path = SvgPath;
		if (string.IsNullOrWhiteSpace(path))
		{
			return;
		}
		double ctlW = base.Width;
		double ctlH = base.Height;
		if (ctlW <= 0.0 || ctlH <= 0.0)
		{
			return;
		}
		double renderScale = base.VisualRoot?.RenderScaling ?? 1.0;
		double zoom = _zoomService?.Zoom ?? 1.0;
		int pxW = (int)Math.Round(ctlW * renderScale * zoom);
		int pxH = (int)Math.Round(ctlH * renderScale * zoom);
		if (pxW <= 0 || pxH <= 0)
		{
			return;
		}
		(string path, int pxW, int pxH) key = (path: path, pxW: pxW, pxH: pxH);
		lock (_cacheGate)
		{
			if (_cache.TryGetValue(key, out Bitmap bmp))
			{
				base.Source = bmp;
				return;
			}
		}
		try
		{
			SvgSource svgSource = SvgSource.Load(path, new Uri("avares://RootApp.Client.Avalonia")) ?? throw new InvalidOperationException("Failed to load SVG source.");
			if (svgSource.Picture == null)
			{
				throw new InvalidOperationException("SVG picture is null.");
			}
			Bitmap bmp = await RenderSvgToBitmapAsync(svgSource, pxW, pxH);
			lock (_cacheGate)
			{
				_cache[key] = bmp;
			}
			base.Source = bmp;
		}
		catch
		{
		}
	}

	private Task<Bitmap> RenderSvgToBitmapAsync(SvgSource P_0, int P_1, int P_2)
	{
		return Task.Run(delegate
		{
			SKImageInfo sKImageInfo = new SKImageInfo(P_1, P_2, SKColorType.Bgra8888, SKAlphaType.Premul);
			using SKSurface sKSurface = SKSurface.Create(sKImageInfo);
			SKCanvas canvas = sKSurface.Canvas;
			canvas.Clear(SKColors.Transparent);
			SKPicture picture = P_0.Picture;
			float val = (float)P_1 / picture.CullRect.Width;
			float val2 = (float)P_2 / picture.CullRect.Height;
			float num = Math.Min(val, val2);
			canvas.Scale(num);
			canvas.DrawPicture(picture);
			canvas.Flush();
			using SKImage sKImage = sKSurface.Snapshot();
			using SKData sKData = sKImage.Encode(SKEncodedImageFormat.Png, 100);
			using MemoryStream memoryStream = new MemoryStream(sKData.ToArray());
			memoryStream.Position = 0L;
			return new Bitmap(memoryStream);
		});
	}
}

