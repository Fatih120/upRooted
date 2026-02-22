// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.IDirectionDropShadowEffect
using Avalonia.Media;

internal interface IDirectionDropShadowEffect : IDropShadowEffect, IEffect
{
	double Direction { get; }

	double ShadowDepth { get; }
}

