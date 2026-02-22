// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.DrawingImage
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Avalonia;
using Avalonia.Media;
using Avalonia.Metadata;

public class DrawingImage : AvaloniaObject, IImage, IAffectsRender
{
	public static readonly StyledProperty<Drawing?> DrawingProperty = AvaloniaProperty.Register<DrawingImage, Drawing>("Drawing");

	[CompilerGenerated]
	private EventHandler? m_Invalidated;

	[Content]
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

	public Size Size => Drawing?.GetBounds().Size ?? default(Size);

	public event EventHandler? Invalidated
	{
		[CompilerGenerated]
		add
		{
			EventHandler eventHandler = this.m_Invalidated;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler eventHandler3 = (EventHandler)Delegate.Combine(eventHandler2, b);
				eventHandler = Interlocked.CompareExchange(ref this.m_Invalidated, eventHandler3, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
		[CompilerGenerated]
		remove
		{
			EventHandler eventHandler = this.m_Invalidated;
			EventHandler eventHandler2;
			do
			{
				eventHandler2 = eventHandler;
				EventHandler eventHandler3 = (EventHandler)Delegate.Remove(eventHandler2, value2);
				eventHandler = Interlocked.CompareExchange(ref this.m_Invalidated, eventHandler3, eventHandler2);
			}
			while ((object)eventHandler != eventHandler2);
		}
	}

	void IImage.Draw(DrawingContext P_0, Rect P_1, Rect P_2)
	{
		Drawing drawing = Drawing;
		if (drawing == null)
		{
			return;
		}
		Rect bounds = drawing.GetBounds();
		Matrix matrix = Matrix.CreateScale(P_2.Width / P_1.Width, P_2.Height / P_1.Height);
		Matrix matrix2 = Matrix.CreateTranslation(0.0 - P_1.X + P_2.X - bounds.X, 0.0 - P_1.Y + P_2.Y - bounds.Y);
		using (P_0.PushClip(P_2))
		{
			using (P_0.PushTransform(matrix2 * matrix))
			{
				Drawing?.Draw(P_0);
			}
		}
	}

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		base.OnPropertyChanged(P_0);
		if (P_0.Property == DrawingProperty)
		{
			RaiseInvalidated(EventArgs.Empty);
		}
	}

	protected void RaiseInvalidated(EventArgs P_0)
	{
		this.Invalidated?.Invoke(this, P_0);
	}
}

