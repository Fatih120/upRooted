// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.IGlSkiaSpecificOptionsFeature
using Avalonia.Metadata;

[PrivateApi]
public interface IGlSkiaSpecificOptionsFeature
{
	bool UseNativeSkiaGrGlInterface { get; }
}

