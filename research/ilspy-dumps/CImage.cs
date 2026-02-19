// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Markdown.Components.CImage
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Markdown.Components;
using RootApp.Client.Avalonia.Markdown.Components.Geometries;

public class CImage : CInline
{
	private readonly string _name;

	public static readonly StyledProperty<double?> LayoutWidthProperty = AvaloniaProperty.Register<CImage, double?>("LayoutWidth");

	public static readonly StyledProperty<double?> LayoutHeightProperty = AvaloniaProperty.Register<CImage, double?>("LayoutHeight");

	public static readonly StyledProperty<double?> RelativeWidthProperty = AvaloniaProperty.Register<CImage, double?>("RelativeWidth");

	public static readonly StyledProperty<bool> FittingWhenProtrudeProperty = AvaloniaProperty.Register<CImage, bool>("FittingWhenProtrude", true);

	public static readonly StyledProperty<bool> SaveAspectRatioProperty = AvaloniaProperty.Register<CImage, bool>("SaveAspectRatio", false);

	[CompilerGenerated]
	private IImage? _003CImage_003Ek__BackingField;

	public double? LayoutWidth
	{
		get
		{
			return GetValue(LayoutWidthProperty);
		}
		set
		{
			SetValue(LayoutWidthProperty, value2);
		}
	}

	public double? LayoutHeight
	{
		get
		{
			return GetValue(LayoutHeightProperty);
		}
		set
		{
			SetValue(LayoutHeightProperty, value2);
		}
	}

	public double? RelativeWidth => GetValue(RelativeWidthProperty);

	public bool FittingWhenProtrude => GetValue(FittingWhenProtrudeProperty);

	public bool SaveAspectRatio
	{
		get
		{
			return GetValue(SaveAspectRatioProperty);
		}
		set
		{
			SetValue(SaveAspectRatioProperty, value2);
		}
	}

	public Task<BitmapWrapper?>? Task { get; }

	public IImage? Image
	{
		[CompilerGenerated]
		get
		{
			return _003CImage_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CImage_003Ek__BackingField = image;
		}
	}

	public CImage(Task<BitmapWrapper?> P_0, string P_1)
	{
		if (P_0 == null)
		{
			throw new NullReferenceException("task");
		}
		_name = P_1;
		Task = P_0;
	}

	protected override IEnumerable<CGeometry> MeasureOverride(double P_0, double P_1)
	{
		bool flag;
		if (Image == null)
		{
			Task<BitmapWrapper> task = Task;
			if (task != null)
			{
				TaskStatus status = task.Status;
				if ((uint)(status - 5) <= 2u)
				{
					flag = true;
					goto IL_006a;
				}
			}
			flag = false;
			goto IL_006a;
		}
		goto IL_00a4;
		IL_00a4:
		if (Image == null)
		{
			yield break;
		}
		double imageWidth = Image.Size.Width;
		double imageHeight = Image.Size.Height;
		if (RelativeWidth.HasValue)
		{
			double aspect = imageHeight / imageWidth;
			imageWidth = RelativeWidth.Value * P_0;
			imageHeight = aspect * imageWidth;
		}
		if (LayoutWidth.HasValue)
		{
			imageWidth = LayoutWidth.Value;
			if (SaveAspectRatio && !LayoutHeight.HasValue)
			{
				double aspect2 = Image.Size.Height / Image.Size.Width;
				imageHeight = aspect2 * imageWidth;
			}
		}
		if (LayoutHeight.HasValue)
		{
			imageHeight = LayoutHeight.Value;
			if (SaveAspectRatio && !LayoutWidth.HasValue)
			{
				double aspect3 = Image.Size.Width / Image.Size.Height;
				imageWidth = aspect3 * imageHeight;
			}
		}
		if (imageWidth > P_1)
		{
			if (P_0 != P_1)
			{
				yield return new LineBreakMarkGeometry(this);
			}
			if (FittingWhenProtrude && imageWidth > P_0)
			{
				double aspect4 = imageHeight / imageWidth;
				imageWidth = P_0;
				imageHeight = aspect4 * imageWidth;
			}
		}
		yield return new ImageGeometry(this, Image, imageWidth, imageHeight, base.TextVerticalAlignment, _name);
		yield break;
		IL_006a:
		if (flag)
		{
			Image = Task?.Result?.Bitmap;
		}
		goto IL_00a4;
	}

	public override string AsString()
	{
		return _name;
	}
}

