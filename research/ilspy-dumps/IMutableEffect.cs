// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.IMutableEffect
using Avalonia.Media;

public interface IMutableEffect : IEffect
{
	internal IImmutableEffect ToImmutable();
}

