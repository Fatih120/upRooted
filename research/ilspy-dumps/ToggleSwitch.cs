// Avalonia.Controls, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Controls.ToggleSwitch
using System;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;

[TemplatePart("PART_MovingKnobs", typeof(Panel), IsRequired = true)]
[TemplatePart("PART_OffContentPresenter", typeof(ContentPresenter))]
[TemplatePart("PART_OnContentPresenter", typeof(ContentPresenter))]
[TemplatePart("PART_SwitchKnob", typeof(Panel))]
[PseudoClasses(new string[] { ":dragging" })]
public class ToggleSwitch : ToggleButton
{
	public static readonly StyledProperty<object?> OffContentProperty;

	public static readonly StyledProperty<IDataTemplate?> OffContentTemplateProperty;

	public static readonly StyledProperty<object?> OnContentProperty;

	public static readonly StyledProperty<IDataTemplate?> OnContentTemplateProperty;

	public static readonly StyledProperty<Transitions> KnobTransitionsProperty;

	static ToggleSwitch()
	{
		OffContentProperty = AvaloniaProperty.Register<ToggleSwitch, object>("OffContent", "Off");
		OffContentTemplateProperty = AvaloniaProperty.Register<ToggleSwitch, IDataTemplate>("OffContentTemplate");
		OnContentProperty = AvaloniaProperty.Register<ToggleSwitch, object>("OnContent", "On");
		OnContentTemplateProperty = AvaloniaProperty.Register<ToggleSwitch, IDataTemplate>("OnContentTemplate");
		KnobTransitionsProperty = AvaloniaProperty.Register<ToggleSwitch, Transitions>("KnobTransitions");
		OffContentProperty.Changed.AddClassHandler(delegate(ToggleSwitch x, AvaloniaPropertyChangedEventArgs e)
		{
			x.OffContentChanged(e);
		});
		OnContentProperty.Changed.AddClassHandler(delegate(ToggleSwitch x, AvaloniaPropertyChangedEventArgs e)
		{
			x.OnContentChanged(e);
		});
		ToggleButton.IsCheckedProperty.Changed.AddClassHandler(delegate(ToggleSwitch x, AvaloniaPropertyChangedEventArgs e)
		{
			if (e.NewValue != null && e.NewValue is bool flag)
			{
				x.UpdateKnobPos(flag);
			}
		});
		Visual.BoundsProperty.Changed.AddClassHandler(delegate(ToggleSwitch x, AvaloniaPropertyChangedEventArgs e)
		{
			if (x.IsChecked.HasValue)
			{
				x.UpdateKnobPos(x.IsChecked.Value);
			}
		});
		KnobTransitionsProperty.Changed.AddClassHandler(delegate(ToggleSwitch x, AvaloniaPropertyChangedEventArgs e)
		{
			x.UpdateKnobTransitions();
		});
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private void OffContentChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		throw new NotSupportedException("Linked away");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private void OnContentChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		throw new NotSupportedException("Linked away");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private void UpdateKnobTransitions()
	{
		throw new NotSupportedException("Linked away");
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private void UpdateKnobPos(bool P_0)
	{
		throw new NotSupportedException("Linked away");
	}
}
