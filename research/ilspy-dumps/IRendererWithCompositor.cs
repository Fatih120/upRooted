// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.IRendererWithCompositor
using System;
using Avalonia.Rendering;
using Avalonia.Rendering.Composition;

internal interface IRendererWithCompositor : IRenderer, IDisposable
{
	Compositor Compositor { get; }
}

