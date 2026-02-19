// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootImageLoader
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Helpers.Caching;

[TemplatePart("PART_Image", typeof(Image))]
[TemplatePart("PART_Placeholder", typeof(RootSvgImage))]
public class RootImageLoader : TemplatedControl
{
	private bool _isLoading;

	private Image? _innerImage;

	private RootSvgImage? _placeholder;

	public static readonly DirectProperty<RootImageLoader, bool> IsLoadingProperty;

	public static readonly StyledProperty<BitmapWrapper?> SourceProperty;

	public static readonly StyledProperty<Bitmap?> PreviewProperty;

	public static readonly StyledProperty<Stretch> StretchProperty;

	public static readonly StyledProperty<StretchDirection> StretchDirectionProperty;

	public static readonly StyledProperty<double> LoadingPlaceholderSizeProperty;

	public bool IsLoading
	{
		get
		{
			return _isLoading;
		}
		private set
		{
			SetAndRaise(IsLoadingProperty, ref _isLoading, value2);
		}
	}

	public BitmapWrapper? Source
	{
		get
		{
			return GetValue(SourceProperty);
		}
		set
		{
			SetValue(SourceProperty, value2);
		}
	}

	public Bitmap? Preview
	{
		get
		{
			return GetValue(PreviewProperty);
		}
		set
		{
			SetValue(PreviewProperty, value2);
		}
	}

	public double LoadingPlaceholderSize
	{
		set
		{
			SetValue(LoadingPlaceholderSizeProperty, value2);
		}
	}

	public Stretch Stretch
	{
		get
		{
			return GetValue(StretchProperty);
		}
		set
		{
			SetValue(StretchProperty, value2);
		}
	}

	public StretchDirection StretchDirection
	{
		get
		{
			return GetValue(StretchDirectionProperty);
		}
		set
		{
			SetValue(StretchDirectionProperty, value2);
		}
	}

	static RootImageLoader()
	{
		IsLoadingProperty = AvaloniaProperty.RegisterDirect("IsLoading", (RootImageLoader rootImageLoader) => rootImageLoader._isLoading, delegate(RootImageLoader rootImageLoader, bool isLoading)
		{
			rootImageLoader._isLoading = isLoading;
		}, false);
		SourceProperty = AvaloniaProperty.Register<RootImageLoader, BitmapWrapper>("Source");
		PreviewProperty = AvaloniaProperty.Register<RootImageLoader, Bitmap>("Preview");
		StretchProperty = AvaloniaProperty.Register<RootImageLoader, Stretch>("Stretch", Stretch.Uniform);
		StretchDirectionProperty = AvaloniaProperty.Register<RootImageLoader, StretchDirection>("StretchDirection", StretchDirection.Both);
		LoadingPlaceholderSizeProperty = AvaloniaProperty.Register<RootImageLoader, double>("LoadingPlaceholderSize", 50.0);
		Visual.AffectsRender<RootImageLoader>(new AvaloniaProperty[5] { SourceProperty, PreviewProperty, StretchProperty, StretchDirectionProperty, LoadingPlaceholderSizeProperty });
		Layoutable.AffectsMeasure<RootImageLoader>(new AvaloniaProperty[5] { SourceProperty, PreviewProperty, StretchProperty, StretchDirectionProperty, LoadingPlaceholderSizeProperty });
	}

	public RootImageLoader()
	{
		_isLoading = true;
	}

	protected override void OnApplyTemplate(TemplateAppliedEventArgs P_0)
	{
		base.OnApplyTemplate(P_0);
		_innerImage = P_0.NameScope.Find<Image>("PART_Image");
		_placeholder = P_0.NameScope.Find<RootSvgImage>("PART_Placeholder");
		UpdateVisuals();
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		base.OnPropertyChanged(P_0);
		if (P_0.Property == SourceProperty && _isLoading)
		{
			IsLoading = false;
		}
		if (P_0.Property == SourceProperty || P_0.Property == PreviewProperty || P_0.Property == IsLoadingProperty)
		{
			UpdateVisuals();
		}
	}

	private void UpdateVisuals()
	{
		if (_innerImage != null && _placeholder != null)
		{
			Bitmap bitmap = Source?.Bitmap;
			if ((!IsLoading && bitmap != null) || (IsLoading && Preview != null))
			{
				_placeholder.IsVisible = false;
				_innerImage.IsVisible = true;
				_innerImage.Source = (IsLoading ? Preview : bitmap);
				_innerImage.Stretch = Stretch;
				_innerImage.StretchDirection = StretchDirection;
			}
			else
			{
				_innerImage.IsVisible = false;
				_placeholder.IsVisible = true;
			}
		}
	}

	protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnDetachedFromVisualTree(P_0);
		Preview = null;
		Image? innerImage = _innerImage;
		if (innerImage != null)
		{
			innerImage.Source = null;
		}
		_innerImage = null;
		Source = null;
	}
}

