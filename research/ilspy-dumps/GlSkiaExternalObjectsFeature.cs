// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.GlSkiaExternalObjectsFeature
using Avalonia.OpenGL;
using Avalonia.Platform;
using Avalonia.Skia;

internal class GlSkiaExternalObjectsFeature : IExternalObjectsRenderInterfaceContextFeature
{
	private readonly GlSkiaGpu _gpu;

	private readonly IGlContextExternalObjectsFeature? _feature;

	public GlSkiaExternalObjectsFeature(GlSkiaGpu P_0, IGlContextExternalObjectsFeature? P_1)
	{
		_gpu = P_0;
		_feature = P_1;
	}
}

