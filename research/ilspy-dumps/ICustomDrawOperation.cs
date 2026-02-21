// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.SceneGraph.ICustomDrawOperation
using System;
using Avalonia;
using Avalonia.Media;
using Avalonia.Rendering.SceneGraph;

public interface ICustomDrawOperation : IEquatable<ICustomDrawOperation>, IDisposable
{
	Rect Bounds { get; }

	bool HitTest(Point P_0);

	void Render(ImmediateDrawingContext P_0);
}

