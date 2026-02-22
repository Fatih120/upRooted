// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.OnlineTextStatusConverter
using System;
using System.Globalization;
using Avalonia.Data.Converters;
using RootApp.WebApi.Shared.Enums;

internal class OnlineTextStatusConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is UserOnlineStatus userOnlineStatus)
		{
			return userOnlineStatus switch
			{
				UserOnlineStatus.Active => "Online", 
				UserOnlineStatus.Disconnected => "Offline", 
				UserOnlineStatus.Inactive => "Away", 
				_ => "Unknown status", 
			};
		}
		return "Invalid status";
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		throw new NotImplementedException();
	}
}

