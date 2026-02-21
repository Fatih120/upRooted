// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.IRenderTimer
using System;
using Avalonia.Metadata;

[PrivateApi]
public interface IRenderTimer
{
	bool RunsInBackground { get; }

	event Action<TimeSpan> Tick;
}

