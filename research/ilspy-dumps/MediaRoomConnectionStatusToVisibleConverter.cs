// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.MediaRoomConnectionStatusToVisibleConverter
using System;
using System.Globalization;
using Avalonia.Data.Converters;
using RootApp.Client.CoreDomain.Models.Media;

public class MediaRoomConnectionStatusToVisibleConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is MediaRoomConnectionStatus mediaRoomConnectionStatus)
		{
			switch (mediaRoomConnectionStatus)
			{
			case MediaRoomConnectionStatus.Disconnecting:
			case MediaRoomConnectionStatus.Connecting:
			case MediaRoomConnectionStatus.Connected:
			case MediaRoomConnectionStatus.Error:
				return true;
			default:
				return false;
			}
		}
		return false;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

