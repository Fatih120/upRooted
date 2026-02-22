// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.DrawingBrush
using System;
using Avalonia;
using Avalonia.Media;
using Avalonia.Rendering.Composition;
using Avalonia.Rendering.Composition.Drawing;
using Avalonia.Rendering.Composition.Server;
using Avalonia.Rendering.Composition.Transport;
using Avalonia.Utilities;

public sealed class DrawingBrush : TileBrush, ISceneBrush, ITileBrush, IBrush
{
	public static readonly StyledProperty<Drawing?> DrawingProperty = AvaloniaProperty.Register<DrawingBrush, Drawing>("Drawing");

	private InlineDictionary<Compositor, CompositionRenderData?> _renderDataDictionary;

	public Drawing? Drawing
	{
		get
		{
			return GetValue(DrawingProperty);
		}
		set
		{
			SetValue(DrawingProperty, value2);
		}
	}

	internal override Func<Compositor, ServerCompositionSimpleBrush> Factory => (Compositor c) => new ServerCompositionSimpleContentBrush(c.Server);

	ISceneBrushContent? ISceneBrush.CreateContent()
	{
		if (Drawing == null)
		{
			return null;
		}
		using RenderDataDrawingContext renderDataDrawingContext = new RenderDataDrawingContext(null);
		Drawing?.Draw(renderDataDrawingContext);
		return renderDataDrawingContext.GetImmediateSceneBrushContent(this, null, true);
	}

	private protected override void OnReferencedFromCompositor(Compositor P_0)
	{
		_renderDataDictionary.Add(P_0, CreateServerContent(P_0));
		base.OnReferencedFromCompositor(P_0);
	}

	protected override void OnUnreferencedFromCompositor(Compositor P_0)
	{
		if (_renderDataDictionary.TryGetAndRemoveValue(P_0, out CompositionRenderData compositionRenderData))
		{
			compositionRenderData?.Dispose();
		}
		base.OnUnreferencedFromCompositor(P_0);
	}

	private protected override void SerializeChanges(Compositor P_0, BatchStreamWriter P_1)
	{
		base.SerializeChanges(P_0, P_1);
		if (_renderDataDictionary.TryGetValue(P_0, out CompositionRenderData compositionRenderData) && compositionRenderData != null)
		{
			P_1.WriteObject(new CompositionRenderDataSceneBrushContent.Properties(compositionRenderData.Server, null, true));
		}
		else
		{
			P_1.WriteObject(null);
		}
	}

	private CompositionRenderData? CreateServerContent(Compositor P_0)
	{
		if (Drawing == null)
		{
			return null;
		}
		using RenderDataDrawingContext renderDataDrawingContext = new RenderDataDrawingContext(P_0);
		Drawing?.Draw(renderDataDrawingContext);
		return renderDataDrawingContext.GetRenderResults();
	}
}

