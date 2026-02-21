// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.VisualBrush
using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Media;
using Avalonia.Rendering;
using Avalonia.Rendering.Composition;
using Avalonia.Rendering.Composition.Drawing;
using Avalonia.Rendering.Composition.Server;
using Avalonia.Rendering.Composition.Transport;
using Avalonia.Utilities;

public sealed class VisualBrush : TileBrush, ISceneBrush, ITileBrush, IBrush
{
	private class RenderDataItem(CompositionRenderData P_0, Rect P_1) : IDisposable
	{
		public bool IsDirty;

		public CompositionRenderData Data { get; } = P_0;

		public Rect Rect { get; } = P_1;

		public void Dispose()
		{
			Data?.Dispose();
		}
	}

	public static readonly StyledProperty<Visual?> VisualProperty = AvaloniaProperty.Register<VisualBrush, Visual>("Visual");

	private InlineDictionary<Compositor, RenderDataItem?> _renderDataDictionary;

	public Visual? Visual
	{
		get
		{
			return GetValue(VisualProperty);
		}
		set
		{
			SetValue(VisualProperty, value2);
		}
	}

	internal override Func<Compositor, ServerCompositionSimpleBrush> Factory => (Compositor c) => new ServerCompositionSimpleContentBrush(c.Server);

	ISceneBrushContent? ISceneBrush.CreateContent()
	{
		if (Visual == null)
		{
			return null;
		}
		if (Visual is IVisualBrushInitialize visualBrushInitialize)
		{
			visualBrushInitialize.EnsureInitialized();
		}
		using RenderDataDrawingContext renderDataDrawingContext = new RenderDataDrawingContext(null);
		ImmediateRenderer.Render(renderDataDrawingContext, Visual);
		return renderDataDrawingContext.GetImmediateSceneBrushContent(this, new Rect(Visual.Bounds.Size), true);
	}

	protected override void OnUnreferencedFromCompositor(Compositor P_0)
	{
		if (_renderDataDictionary.TryGetAndRemoveValue(P_0, out RenderDataItem renderDataItem))
		{
			renderDataItem?.Dispose();
		}
		base.OnUnreferencedFromCompositor(P_0);
	}

	private protected override void SerializeChanges(Compositor P_0, BatchStreamWriter P_1)
	{
		base.SerializeChanges(P_0, P_1);
		CompositionRenderDataSceneBrushContent.Properties properties = null;
		if (IsOnCompositor(P_0))
		{
			_renderDataDictionary.TryGetValue(P_0, out RenderDataItem renderDataItem);
			if (renderDataItem == null || renderDataItem.IsDirty)
			{
				RenderDataItem renderDataItem2 = CreateServerContent(P_0);
				renderDataItem?.Dispose();
				renderDataItem = (_renderDataDictionary[P_0] = renderDataItem2);
			}
			if (renderDataItem != null)
			{
				properties = new CompositionRenderDataSceneBrushContent.Properties(renderDataItem.Data.Server, renderDataItem.Rect, true);
			}
		}
		P_1.WriteObject(properties);
	}

	private void InvalidateContent()
	{
		foreach (KeyValuePair<Compositor, RenderDataItem> item in _renderDataDictionary)
		{
			if (item.Value != null)
			{
				item.Value.IsDirty = true;
			}
		}
		RegisterForSerialization();
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		InvalidateContent();
		base.OnPropertyChanged(P_0);
	}

	private RenderDataItem? CreateServerContent(Compositor P_0)
	{
		if (Visual == null)
		{
			return null;
		}
		if (Visual is IVisualBrushInitialize visualBrushInitialize)
		{
			visualBrushInitialize.EnsureInitialized();
		}
		using RenderDataDrawingContext renderDataDrawingContext = new RenderDataDrawingContext(P_0);
		ImmediateRenderer.Render(renderDataDrawingContext, Visual);
		CompositionRenderData renderResults = renderDataDrawingContext.GetRenderResults();
		if (renderResults == null)
		{
			return null;
		}
		return new RenderDataItem(renderResults, new Rect(Visual.Bounds.Size));
	}
}
