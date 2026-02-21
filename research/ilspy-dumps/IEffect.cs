// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.IEffect
using System.ComponentModel;
using Avalonia.Media;
using Avalonia.Metadata;

[TypeConverter(typeof(EffectConverter))]
[NotClientImplementable]
public interface IEffect
{
}

