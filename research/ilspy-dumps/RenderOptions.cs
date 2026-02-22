// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.RenderOptions
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;

public readonly record struct RenderOptions
{
	public BitmapInterpolationMode BitmapInterpolationMode
	{
		[CompilerGenerated]
		get
		{
			return _003CBitmapInterpolationMode_003Ek__BackingField;
		}
		[CompilerGenerated]
		init
		{
			_003CBitmapInterpolationMode_003Ek__BackingField = bitmapInterpolationMode;
		}
	}

	public EdgeMode EdgeMode
	{
		[CompilerGenerated]
		get
		{
			return _003CEdgeMode_003Ek__BackingField;
		}
		[CompilerGenerated]
		init
		{
			_003CEdgeMode_003Ek__BackingField = edgeMode;
		}
	}

	public TextRenderingMode TextRenderingMode
	{
		[CompilerGenerated]
		get
		{
			return _003CTextRenderingMode_003Ek__BackingField;
		}
		[CompilerGenerated]
		init
		{
			_003CTextRenderingMode_003Ek__BackingField = textRenderingMode;
		}
	}

	public BitmapBlendingMode BitmapBlendingMode
	{
		[CompilerGenerated]
		get
		{
			return _003CBitmapBlendingMode_003Ek__BackingField;
		}
		[CompilerGenerated]
		init
		{
			_003CBitmapBlendingMode_003Ek__BackingField = bitmapBlendingMode;
		}
	}

	public bool? RequiresFullOpacityHandling
	{
		[CompilerGenerated]
		get
		{
			return _003CRequiresFullOpacityHandling_003Ek__BackingField;
		}
		[CompilerGenerated]
		init
		{
			_003CRequiresFullOpacityHandling_003Ek__BackingField = flag;
		}
	}

	public static void SetBitmapInterpolationMode(Visual P_0, BitmapInterpolationMode P_1)
	{
		P_0.RenderOptions = P_0.RenderOptions with
		{
			BitmapInterpolationMode = P_1
		};
	}

	public static void SetRequiresFullOpacityHandling(Visual P_0, bool? P_1)
	{
		P_0.RenderOptions = P_0.RenderOptions with
		{
			RequiresFullOpacityHandling = P_1
		};
	}

	public RenderOptions MergeWith(RenderOptions P_0)
	{
		BitmapInterpolationMode bitmapInterpolationMode = BitmapInterpolationMode;
		if (bitmapInterpolationMode == BitmapInterpolationMode.Unspecified)
		{
			bitmapInterpolationMode = P_0.BitmapInterpolationMode;
		}
		EdgeMode edgeMode = EdgeMode;
		if (edgeMode == EdgeMode.Unspecified)
		{
			edgeMode = P_0.EdgeMode;
		}
		TextRenderingMode textRenderingMode = TextRenderingMode;
		if (textRenderingMode == TextRenderingMode.Unspecified)
		{
			textRenderingMode = P_0.TextRenderingMode;
		}
		BitmapBlendingMode bitmapBlendingMode = BitmapBlendingMode;
		if (bitmapBlendingMode == BitmapBlendingMode.Unspecified)
		{
			bitmapBlendingMode = P_0.BitmapBlendingMode;
		}
		bool? requiresFullOpacityHandling = RequiresFullOpacityHandling;
		if (!requiresFullOpacityHandling.HasValue)
		{
			requiresFullOpacityHandling = P_0.RequiresFullOpacityHandling;
		}
		return new RenderOptions
		{
			BitmapInterpolationMode = bitmapInterpolationMode,
			EdgeMode = edgeMode,
			TextRenderingMode = textRenderingMode,
			BitmapBlendingMode = bitmapBlendingMode,
			RequiresFullOpacityHandling = requiresFullOpacityHandling
		};
	}
}

