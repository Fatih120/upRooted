// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.ITransform
using System.ComponentModel;
using Avalonia;
using Avalonia.Media;
using Avalonia.Metadata;

[TypeConverter(typeof(TransformConverter))]
[NotClientImplementable]
public interface ITransform
{
	Matrix Value { get; }
}

