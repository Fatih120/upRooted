using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using DotNetBrowser.Capture;
using DotNetBrowser.Ui;
using FluentValidation;
using Microsoft.VisualStudio.Threading;

namespace RootApp.Client.Avalonia.UI.Home.VoiceBar;

public class ScreenshareViewModel : ViewModelBase<ScreenshareViewModel>
{
	private readonly Source _screenSource;

	private readonly bool _isWindow;

	[CompilerGenerated]
	private Action<ScreenSelectedEventArgs>? m_ScreenSelected;

	[CompilerGenerated]
	private WriteableBitmap? <ScreenBitmap>k__BackingField;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? screenClickedCommand;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public WriteableBitmap? ScreenBitmap
	{
		get
		{
			return <ScreenBitmap>k__BackingField;
		}
		private set
		{
			if (!EqualityComparer<WriteableBitmap>.Default.Equals(<ScreenBitmap>k__BackingField, writeableBitmap))
			{
				<ScreenBitmap>k__BackingField = writeableBitmap;
				OnPropertyChanged(CommunityToolkit.Mvvm.ComponentModel.__Internals.__KnownINotifyPropertyChangedArgs.ScreenBitmap);
			}
		}
	}

	public string ScreenName => _screenSource.Name;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ScreenClickedCommand => screenClickedCommand ?? (screenClickedCommand = new RelayCommand(ScreenClicked));

	public event Action<ScreenSelectedEventArgs>? ScreenSelected
	{
		[CompilerGenerated]
		add
		{
			Action<ScreenSelectedEventArgs> action = this.m_ScreenSelected;
			Action<ScreenSelectedEventArgs> action2;
			do
			{
				action2 = action;
				Action<ScreenSelectedEventArgs> action3 = (Action<ScreenSelectedEventArgs>)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_ScreenSelected, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action<ScreenSelectedEventArgs> action = this.m_ScreenSelected;
			Action<ScreenSelectedEventArgs> action2;
			do
			{
				action2 = action;
				Action<ScreenSelectedEventArgs> action3 = (Action<ScreenSelectedEventArgs>)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_ScreenSelected, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public ScreenshareViewModel(Source P_0, bool P_1)
		: base((IValidator<ScreenshareViewModel>?)null)
	{
		_screenSource = P_0;
		_isWindow = P_1;
		if (_screenSource.Thumbnail.Pixels != null)
		{
			Task.Run(delegate
			{
				convertDotNetBrowserBitmapToAvaloniaBitmap(_screenSource.Thumbnail);
			}).Forget();
		}
	}

	private void convertDotNetBrowserBitmapToAvaloniaBitmap(DotNetBrowser.Ui.Bitmap P_0)
	{
		int width = (int)P_0.Size.Width;
		int height = (int)P_0.Size.Height;
		PixelSize pixelSize = new PixelSize(width, height);
		Vector vector = new Vector(96.0, 96.0);
		WriteableBitmap writeableBitmap = new WriteableBitmap(pixelSize, vector, PixelFormat.Bgra8888, AlphaFormat.Premul);
		byte[] array = P_0.Pixels.ToArray();
		using (ILockedFramebuffer lockedFramebuffer = writeableBitmap.Lock())
		{
			if (array.Length != lockedFramebuffer.RowBytes * height)
			{
				throw new InvalidOperationException("Pixel data size does not match the expected buffer size.");
			}
			Marshal.Copy(array, 0, lockedFramebuffer.Address, array.Length);
		}
		ScreenBitmap = writeableBitmap;
	}

	[RelayCommand]
	public void ScreenClicked()
	{
		this.ScreenSelected?.Invoke(new ScreenSelectedEventArgs(_screenSource, _isWindow));
	}

	public override void Dispose()
	{
		base.Dispose();
		ScreenBitmap?.Dispose();
	}
}
