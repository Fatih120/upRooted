// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.IRenderLoop
using Avalonia.Metadata;
using Avalonia.Rendering;

[NotClientImplementable]
internal interface IRenderLoop
{
	bool RunsInBackground { get; }

	void Add(IRenderLoopTask P_0);
}

