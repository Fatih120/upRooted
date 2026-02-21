// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKCropRectFlags
using System;

[Flags]
public enum SKCropRectFlags
{
	HasNone = 0,
	HasLeft = 1,
	HasTop = 2,
	HasWidth = 4,
	HasHeight = 8,
	HasAll = 0xF
}

