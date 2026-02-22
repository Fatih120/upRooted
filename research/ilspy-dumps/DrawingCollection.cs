// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.DrawingCollection
using Avalonia.Collections;
using Avalonia.Media;

public sealed class DrawingCollection : AvaloniaList<Drawing>
{
	public DrawingCollection()
	{
		base.ResetBehavior = ResetBehavior.Remove;
	}
}

