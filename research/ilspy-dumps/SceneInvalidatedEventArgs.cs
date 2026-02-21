// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.SceneInvalidatedEventArgs
using System;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Metadata;
using Avalonia.Rendering;

[PrivateApi]
public class SceneInvalidatedEventArgs : EventArgs
{
	[CompilerGenerated]
	private readonly IRenderRoot _003CRenderRoot_003Ek__BackingField;

	public Rect DirtyRect { get; }

	public SceneInvalidatedEventArgs(IRenderRoot P_0, Rect P_1)
	{
		_003CRenderRoot_003Ek__BackingField = P_0;
		DirtyRect = P_1;
	}
}

