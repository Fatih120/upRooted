// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.PathToBitmapConverter
using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;

public class PathToBitmapConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is string text)
		{
			return new Bitmap(text);
		}
		return null;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

