// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.ImageBrush
using System;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Platform;
using Avalonia.Rendering.Composition;
using Avalonia.Rendering.Composition.Server;
using Avalonia.Rendering.Composition.Transport;
using Avalonia.Utilities;

public sealed class ImageBrush : TileBrush, IImageBrush, ITileBrush, IBrush, IMutableBrush
{
	public static readonly StyledProperty<IImageBrushSource?> SourceProperty = AvaloniaProperty.Register<ImageBrush, IImageBrushSource>("Source");

	public IImageBrushSource? Source
	{
		get
		{
			return GetValue(SourceProperty);
		}
		set
		{
			SetValue(SourceProperty, value2);
		}
	}

	internal override Func<Compositor, ServerCompositionSimpleBrush> Factory => (Compositor c) => new ServerCompositionSimpleImageBrush(c.Server);

	public ImageBrush()
	{
	}

	public ImageBrush(IImageBrushSource? P_0)
	{
		Source = P_0;
	}

	public IImmutableBrush ToImmutable()
	{
		return new ImmutableImageBrush(this);
	}

	private protected override void SerializeChanges(Compositor P_0, BatchStreamWriter P_1)
	{
		base.SerializeChanges(P_0, P_1);
		IRef<IBitmapImpl> obj = Source?.Bitmap?.Clone();
		P_1.WriteObject(obj);
	}
}

