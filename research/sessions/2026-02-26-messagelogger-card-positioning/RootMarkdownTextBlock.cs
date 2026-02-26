using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.Markdown.DocumentElements;

namespace RootApp.Client.Avalonia.Controls;

public sealed class RootMarkdownTextBlock : ContentControl
{
	public static readonly DirectProperty<RootMarkdownTextBlock, string?> MarkdownProperty = AvaloniaProperty.RegisterDirect("Markdown", (RootMarkdownTextBlock o) => o.Markdown, delegate(RootMarkdownTextBlock o, string? v)
	{
		o.Markdown = v;
	});

	public static readonly StyledProperty<IMarkdownEngine?> EngineProperty = AvaloniaProperty.Register<RootMarkdownTextBlock, IMarkdownEngine>("Engine");

	private string? _markdown;

	[CompilerGenerated]
	private DocumentElement? <Document>k__BackingField;

	public DocumentElement? Document
	{
		[CompilerGenerated]
		get
		{
			return <Document>k__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			<Document>k__BackingField = documentElement;
		}
	}

	public string? Markdown
	{
		get
		{
			return _markdown;
		}
		set
		{
			if (SetAndRaise(MarkdownProperty, ref _markdown, value2))
			{
				UpdateMarkdown();
			}
		}
	}

	public IMarkdownEngine? Engine => GetValue(EngineProperty);

	protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnDetachedFromVisualTree(P_0);
		SetDocument(null);
	}

	private void UpdateMarkdown()
	{
		if (Engine == null || string.IsNullOrEmpty(Markdown))
		{
			SetDocument(null);
			return;
		}
		DocumentElement document = Engine.TransformElement(Markdown);
		SetDocument(document);
	}

	private void SetDocument(DocumentElement? P_0)
	{
		if (Document != P_0)
		{
			(Document?.Control)?.Classes.Remove("RootMarkdown");
			Document = P_0;
			Control control = Document?.Control;
			if (control != null)
			{
				control.Classes.Add("RootMarkdown");
				base.Content = control;
			}
			else
			{
				base.Content = null;
			}
		}
	}
}
