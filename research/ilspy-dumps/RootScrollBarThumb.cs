// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootScrollBarThumb
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.VisualTree;
using RootApp.Client.Avalonia.Controls;

public class RootScrollBarThumb : Thumb
{
	private RootScrollViewer? _rootScrollViewer;

	public RootScrollBarThumb()
	{
		base.AttachedToVisualTree += onAttachedToVisualTree;
		base.DetachedFromVisualTree += onDetachedFromVisualTree;
	}

	private void onAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
	{
		_rootScrollViewer = this.GetVisualAncestors().OfType<RootScrollViewer>().FirstOrDefault();
		if (_rootScrollViewer != null)
		{
			_rootScrollViewer.PropertyChanged += onRootScrollViewerPropertyChanged;
		}
		base.DragStarted += onDragStarted;
		base.DragCompleted += onDragCompleted;
	}

	private void onDetachedFromVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
	{
		base.DragStarted -= onDragStarted;
		base.DragCompleted -= onDragCompleted;
		if (_rootScrollViewer != null)
		{
			_rootScrollViewer.PropertyChanged -= onRootScrollViewerPropertyChanged;
			_rootScrollViewer = null;
		}
	}

	private void onRootScrollViewerPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
	{
		if (e.Property == RootScrollViewer.IsMouseOverProperty && e.Property == RootScrollViewer.IsMouseOverProperty)
		{
			base.PseudoClasses.Set(":parentIsMouseOver", e.NewValue is bool flag && flag);
		}
	}

	private void onDragStarted(object? sender, VectorEventArgs e)
	{
		_rootScrollViewer?.NotifyThumbDragStarted();
	}

	private void onDragCompleted(object? sender, VectorEventArgs e)
	{
		_rootScrollViewer?.NotifyThumbDragCompleted();
	}
}

