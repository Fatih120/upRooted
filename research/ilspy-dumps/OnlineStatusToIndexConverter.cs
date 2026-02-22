// RootApp.Client.Avalonia, Version=0.9.93.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Converters.OnlineStatusToIndexConverter
using System;
using System.Globalization;
using Avalonia.Data.Converters;
using RootApp.WebApi.Shared.Enums;

internal class OnlineStatusToIndexConverter : IValueConverter
{
	public object Convert(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is UserOnlineStatus userOnlineStatus)
		{
			return userOnlineStatus switch
			{
				UserOnlineStatus.Active => 0, 
				UserOnlineStatus.Inactive => 1, 
				_ => 2, 
			};
		}
		return 2;
	}

	public object ConvertBack(object? P_0, Type P_1, object? P_2, CultureInfo P_3)
	{
		if (P_0 is int num)
		{
			return num switch
			{
				0 => UserOnlineStatus.Active, 
				1 => UserOnlineStatus.Inactive, 
				_ => UserOnlineStatus.Disconnected, 
			};
		}
		return UserOnlineStatus.Disconnected;
	}
}

