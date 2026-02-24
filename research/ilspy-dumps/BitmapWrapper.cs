using System.Runtime.CompilerServices;
using Avalonia.Media.Imaging;

namespace RootApp.Client.Avalonia.Helpers.Caching;

public class BitmapWrapper
{
	[CompilerGenerated]
	private Bitmap <Bitmap>k__BackingField;

	[CompilerGenerated]
	private long <Size>k__BackingField;

	public Bitmap Bitmap
	{
		[CompilerGenerated]
		get
		{
			return <Bitmap>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<Bitmap>k__BackingField = bitmap;
		}
	}

	private long Size
	{
		[CompilerGenerated]
		set
		{
			<Size>k__BackingField = num;
		}
	}

	public BitmapWrapper(Bitmap P_0)
	{
		Bitmap = P_0;
		Size = CalculateBitmapSize(P_0);
	}

	private static long CalculateBitmapSize(Bitmap P_0)
	{
		return P_0.PixelSize.Width * P_0.PixelSize.Height * 4;
	}
}
