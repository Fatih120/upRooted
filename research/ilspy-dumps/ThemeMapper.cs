// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Resources.Themes.ThemeMapper
using Avalonia.Styling;
using RootApp.Client.Avalonia.Resources.Themes;

public static class ThemeMapper
{
	public static readonly ThemeVariant PureDark = new ThemeVariant("PureDark", ThemeVariant.Dark);

	public static ThemeVariant ToThemeVariant(RootThemeEnum P_0)
	{
		if (1 == 0)
		{
		}
		ThemeVariant result = P_0 switch
		{
			RootThemeEnum.Default => ThemeVariant.Default, 
			RootThemeEnum.Dark => ThemeVariant.Dark, 
			RootThemeEnum.Light => ThemeVariant.Light, 
			RootThemeEnum.PureDark => PureDark, 
			_ => ThemeVariant.Default, 
		};
		if (1 == 0)
		{
		}
		return result;
	}

	public static RootThemeEnum ToRootThemeEnum(ThemeVariant P_0)
	{
		if (P_0 == ThemeVariant.Dark)
		{
			return RootThemeEnum.Dark;
		}
		if (P_0 == ThemeVariant.Light)
		{
			return RootThemeEnum.Light;
		}
		if (P_0 == PureDark)
		{
			return RootThemeEnum.PureDark;
		}
		return RootThemeEnum.Default;
	}

	public static string ToWebRtcThemeString(ThemeVariant? P_0)
	{
		if (P_0 == null || P_0 == ThemeVariant.Dark)
		{
			return "dark";
		}
		if (P_0 == ThemeVariant.Light)
		{
			return "light";
		}
		if (P_0 == PureDark)
		{
			return "pure-dark";
		}
		return "dark";
	}

	public static string ToAppBridgeThemeString(ThemeVariant? P_0)
	{
		if (P_0 == null || P_0 == ThemeVariant.Dark || P_0.InheritVariant == ThemeVariant.Dark)
		{
			return "dark";
		}
		if (P_0 == ThemeVariant.Light || P_0.InheritVariant == ThemeVariant.Light)
		{
			return "light";
		}
		return "dark";
	}
}
