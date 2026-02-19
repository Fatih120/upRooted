// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootMarkdownTextBlock
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.Markdown.DocumentElements;

public sealed class RootMarkdownTextBlock : ContentControl
{
	public static readonly DirectProperty<RootMarkdownTextBlock, string?> MarkdownProperty = AvaloniaProperty.RegisterDirect("Markdown", (RootMarkdownTextBlock rootMarkdownTextBlock) => rootMarkdownTextBlock.Markdown, delegate(RootMarkdownTextBlock rootMarkdownTextBlock, string? markdown)
	{
		rootMarkdownTextBlock.Markdown = markdown;
	});

	public static readonly StyledProperty<IMarkdownEngine?> EngineProperty = AvaloniaProperty.Register<RootMarkdownTextBlock, IMarkdownEngine>("Engine");

	private string? _markdown;

	[CompilerGenerated]
	private DocumentElement? _003CDocument_003Ek__BackingField;

	public DocumentElement? Document
	{
		[CompilerGenerated]
		get
		{
			return _003CDocument_003Ek__BackingField;
		}
		[CompilerGenerated]
		private set
		{
			_003CDocument_003Ek__BackingField = documentElement;
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

