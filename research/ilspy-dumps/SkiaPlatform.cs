// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.SkiaPlatform
using Avalonia;
using Avalonia.Platform;
using Avalonia.Skia;

public static class SkiaPlatform
{
	public static Vector DefaultDpi => new Vector(96.0, 96.0);

	public static void Initialize(SkiaOptions P_0)
	{
		PlatformRenderInterface platformRenderInterface = new PlatformRenderInterface(P_0.MaxGpuResourceSizeBytes);
		AvaloniaLocator.CurrentMutable.Bind<IPlatformRenderInterface>().ToConstant(platformRenderInterface).Bind<IFontManagerImpl>()
			.ToConstant(new FontManagerImpl())
			.Bind<ITextShaperImpl>()
			.ToConstant(new TextShaperImpl());
	}
}

