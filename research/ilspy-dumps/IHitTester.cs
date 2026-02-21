// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.IHitTester
using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Metadata;

[PrivateApi]
public interface IHitTester
{
	IEnumerable<Visual> HitTest(Point P_0, Visual P_1, Func<Visual, bool>? P_2);

	Visual? HitTestFirst(Point P_0, Visual P_1, Func<Visual, bool>? P_2);
}

