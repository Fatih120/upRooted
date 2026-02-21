// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.IRenderer
using System;
using Avalonia;
using Avalonia.Metadata;
using Avalonia.Rendering;

[PrivateApi]
public interface IRenderer : IDisposable
{
	RendererDiagnostics Diagnostics { get; }

	void AddDirty(Visual P_0);

	void RecalculateChildren(Visual P_0);
}

