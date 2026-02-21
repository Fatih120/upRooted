// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.SkiaApplicationExtensions
using Avalonia;
using Avalonia.Skia;

public static class SkiaApplicationExtensions
{
	public static AppBuilder UseSkia(this AppBuilder P_0)
	{
		return P_0.UseRenderingSubsystem(delegate
		{
			SkiaPlatform.Initialize(AvaloniaLocator.Current.GetService<SkiaOptions>() ?? new SkiaOptions());
		}, "Skia");
	}
}

