// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Themes.ThemeService
using System;
using Avalonia;
using Avalonia.Styling;
using RootApp.Client.Avalonia.Resources.Themes;
using RootApp.Client.Domain.Helpers.Store;

public class ThemeService(ILocalDataStore P_0)
{
	public void InitializeTheme()
	{
		if (P_0.TryGetGlobal(DataStoreKeys.Theme, out int num) && Enum.IsDefined(typeof(RootThemeEnum), num))
		{
			SetTheme(ThemeMapper.ToThemeVariant((RootThemeEnum)num));
		}
		else
		{
			SetTheme(ThemeVariant.Dark);
		}
	}

	public void SetTheme(ThemeVariant P_0, bool P_1 = false)
	{
		if (P_1)
		{
			P_0.SetGlobal(DataStoreKeys.Theme, (int)ThemeMapper.ToRootThemeEnum(P_0));
		}
		Application.Current.RequestedThemeVariant = P_0;
	}

	public static bool IsDefaultColor(string P_0)
	{
		if (sanitizeHexString(P_0) == "#FFFFFF" || sanitizeHexString(P_0) == "#000000")
		{
			return true;
		}
		return false;
	}

	public static string GetInvertedDefaultColorHex(string P_0)
	{
		ThemeVariant actualThemeVariant = Application.Current.ActualThemeVariant;
		string text = sanitizeHexString(P_0);
		if (actualThemeVariant == ThemeVariant.Light)
		{
			return "#000000";
		}
		return "#FFFFFF";
	}

	private static string sanitizeHexString(string P_0)
	{
		if (string.IsNullOrEmpty(P_0))
		{
			return "#FFFFFF";
		}
		if (P_0.Length == 9)
		{
			return "#" + P_0.Substring(3);
		}
		return P_0.ToUpper();
	}
}
