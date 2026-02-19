// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// CompiledAvaloniaXaml.XamlDynamicSetters
using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Media;
using Avalonia.Styling;
using RootApp.Client.Avalonia.Controls.ContextMenus;
using RootApp.SkiaImageLoader;

[CompilerGenerated]
internal class XamlDynamicSetters
{
	public static void _003C_003EXamlDynamicSetter_1(TextBlock P_0, object P_1)
	{
		if (P_1 is UnsetValueType)
		{
			P_0.SetValue(TextBlock.FontFamilyProperty, AvaloniaProperty.UnsetValue);
			return;
		}
		if (P_1 is IBinding)
		{
			IBinding binding = (IBinding)P_1;
			AvaloniaObjectExtensions.Bind(P_0, TextBlock.FontFamilyProperty, binding);
			return;
		}
		if (P_1 is FontFamily)
		{
			P_0.FontFamily = (FontFamily)P_1;
			return;
		}
		if (P_1 == null)
		{
			P_0.FontFamily = (FontFamily)P_1;
			return;
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_2(ContentPresenter P_0, BindingPriority P_1, IBinding P_2)
	{
		if (P_2 != null)
		{
			IBinding binding = P_2;
			AvaloniaObjectExtensions.Bind(P_0, ContentPresenter.ContentProperty, binding);
		}
		else
		{
			object value = P_2;
			int priority = (int)P_1;
			P_0.SetValue(ContentPresenter.ContentProperty, value, (BindingPriority)priority);
		}
	}

	public static void _003C_003EXamlDynamicSetter_3(TemplatedControl P_0, object P_1)
	{
		if (P_1 is UnsetValueType)
		{
			P_0.SetValue(TemplatedControl.FontFamilyProperty, AvaloniaProperty.UnsetValue);
			return;
		}
		if (P_1 is IBinding)
		{
			IBinding binding = (IBinding)P_1;
			AvaloniaObjectExtensions.Bind(P_0, TemplatedControl.FontFamilyProperty, binding);
			return;
		}
		if (P_1 is FontFamily)
		{
			P_0.FontFamily = (FontFamily)P_1;
			return;
		}
		if (P_1 == null)
		{
			P_0.FontFamily = (FontFamily)P_1;
			return;
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_4(ContentControl P_0, CompiledBindingExtension P_1)
	{
		if (P_1 != null)
		{
			IBinding binding = P_1;
			AvaloniaObjectExtensions.Bind(P_0, ContentControl.ContentProperty, binding);
		}
		else
		{
			P_0.Content = P_1;
		}
	}

	public static void _003C_003EXamlDynamicSetter_5(ContentControl P_0, BindingPriority P_1, IBinding P_2)
	{
		if (P_2 != null)
		{
			IBinding binding = P_2;
			AvaloniaObjectExtensions.Bind(P_0, ContentControl.ContentProperty, binding);
		}
		else
		{
			object value = P_2;
			int priority = (int)P_1;
			P_0.SetValue(ContentControl.ContentProperty, value, (BindingPriority)priority);
		}
	}

	public static void _003C_003EXamlDynamicSetter_6(StyledElement P_0, CompiledBindingExtension P_1)
	{
		if (P_1 != null)
		{
			IBinding binding = P_1;
			AvaloniaObjectExtensions.Bind(P_0, StyledElement.DataContextProperty, binding);
		}
		else
		{
			P_0.DataContext = P_1;
		}
	}

	public static void _003C_003EXamlDynamicSetter_7(SelectingItemsControl P_0, CompiledBindingExtension P_1)
	{
		if (P_1 != null)
		{
			IBinding binding = P_1;
			AvaloniaObjectExtensions.Bind(P_0, SelectingItemsControl.SelectedItemProperty, binding);
		}
		else
		{
			P_0.SelectedItem = P_1;
		}
	}

	public static void _003C_003EXamlDynamicSetter_8(SelectingItemsControl P_0, CompiledBindingExtension P_1)
	{
		if (P_1 != null)
		{
			IBinding binding = P_1;
			AvaloniaObjectExtensions.Bind(P_0, SelectingItemsControl.SelectedValueProperty, binding);
		}
		else
		{
			P_0.SelectedValue = P_1;
		}
	}

	public static void _003C_003EXamlDynamicSetter_9(Layoutable P_0, BindingPriority P_1, object P_2)
	{
		if (P_2 is IBinding)
		{
			IBinding binding = (IBinding)P_2;
			AvaloniaObjectExtensions.Bind(P_0, Layoutable.HeightProperty, binding);
			return;
		}
		if (P_2 is double value)
		{
			int priority = (int)P_1;
			P_0.SetValue(Layoutable.HeightProperty, value, (BindingPriority)priority);
			return;
		}
		if (P_2 == null)
		{
			throw new NullReferenceException();
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_10(Layoutable P_0, BindingPriority P_1, object P_2)
	{
		if (P_2 is IBinding)
		{
			IBinding binding = (IBinding)P_2;
			AvaloniaObjectExtensions.Bind(P_0, Layoutable.WidthProperty, binding);
			return;
		}
		if (P_2 is double value)
		{
			int priority = (int)P_1;
			P_0.SetValue(Layoutable.WidthProperty, value, (BindingPriority)priority);
			return;
		}
		if (P_2 == null)
		{
			throw new NullReferenceException();
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_11(Border P_0, BindingPriority P_1, object P_2)
	{
		if (P_2 is IBinding)
		{
			IBinding binding = (IBinding)P_2;
			AvaloniaObjectExtensions.Bind(P_0, Border.BackgroundProperty, binding);
			return;
		}
		if (P_2 is IBrush)
		{
			IBrush value = (IBrush)P_2;
			int priority = (int)P_1;
			P_0.SetValue(Border.BackgroundProperty, value, (BindingPriority)priority);
			return;
		}
		if (P_2 == null)
		{
			IBrush value = (IBrush)P_2;
			int priority = (int)P_1;
			P_0.SetValue(Border.BackgroundProperty, value, (BindingPriority)priority);
			return;
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_12(StyledElement P_0, BindingPriority P_1, IBinding P_2)
	{
		if (P_2 != null)
		{
			IBinding binding = P_2;
			AvaloniaObjectExtensions.Bind(P_0, StyledElement.DataContextProperty, binding);
		}
		else
		{
			object value = P_2;
			int priority = (int)P_1;
			P_0.SetValue(StyledElement.DataContextProperty, value, (BindingPriority)priority);
		}
	}

	public static void _003C_003EXamlDynamicSetter_13(TextBlock P_0, BindingPriority P_1, object P_2)
	{
		if (P_2 is IBinding)
		{
			IBinding binding = (IBinding)P_2;
			AvaloniaObjectExtensions.Bind(P_0, TextBlock.FontFamilyProperty, binding);
			return;
		}
		if (P_2 is FontFamily)
		{
			FontFamily value = (FontFamily)P_2;
			int priority = (int)P_1;
			P_0.SetValue(TextBlock.FontFamilyProperty, value, (BindingPriority)priority);
			return;
		}
		if (P_2 == null)
		{
			FontFamily value = (FontFamily)P_2;
			int priority = (int)P_1;
			P_0.SetValue(TextBlock.FontFamilyProperty, value, (BindingPriority)priority);
			return;
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_14(StyledElement P_0, BindingPriority P_1, object P_2)
	{
		if (P_2 is IBinding)
		{
			IBinding binding = (IBinding)P_2;
			AvaloniaObjectExtensions.Bind(P_0, StyledElement.ThemeProperty, binding);
			return;
		}
		if (P_2 is ControlTheme)
		{
			ControlTheme value = (ControlTheme)P_2;
			int priority = (int)P_1;
			P_0.SetValue(StyledElement.ThemeProperty, value, (BindingPriority)priority);
			return;
		}
		if (P_2 == null)
		{
			ControlTheme value = (ControlTheme)P_2;
			int priority = (int)P_1;
			P_0.SetValue(StyledElement.ThemeProperty, value, (BindingPriority)priority);
			return;
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_15(ItemsControl P_0, BindingPriority P_1, object P_2)
	{
		if (P_2 is IBinding)
		{
			IBinding binding = (IBinding)P_2;
			AvaloniaObjectExtensions.Bind(P_0, ItemsControl.ItemContainerThemeProperty, binding);
			return;
		}
		if (P_2 is ControlTheme)
		{
			ControlTheme value = (ControlTheme)P_2;
			int priority = (int)P_1;
			P_0.SetValue(ItemsControl.ItemContainerThemeProperty, value, (BindingPriority)priority);
			return;
		}
		if (P_2 == null)
		{
			ControlTheme value = (ControlTheme)P_2;
			int priority = (int)P_1;
			P_0.SetValue(ItemsControl.ItemContainerThemeProperty, value, (BindingPriority)priority);
			return;
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_16(ToolTip P_0, BindingPriority P_1, CompiledBindingExtension P_2)
	{
		if (P_2 != null)
		{
			IBinding binding = P_2;
			AvaloniaObjectExtensions.Bind(P_0, ToolTip.TipProperty, binding);
		}
		else
		{
			object value = P_2;
			int priority = (int)P_1;
			P_0.SetValue(ToolTip.TipProperty, value, (BindingPriority)priority);
		}
	}

	public static void _003C_003EXamlDynamicSetter_17(TemplatedControl P_0, BindingPriority P_1, object P_2)
	{
		if (P_2 is IBinding)
		{
			IBinding binding = (IBinding)P_2;
			AvaloniaObjectExtensions.Bind(P_0, TemplatedControl.FontFamilyProperty, binding);
			return;
		}
		if (P_2 is FontFamily)
		{
			FontFamily value = (FontFamily)P_2;
			int priority = (int)P_1;
			P_0.SetValue(TemplatedControl.FontFamilyProperty, value, (BindingPriority)priority);
			return;
		}
		if (P_2 == null)
		{
			FontFamily value = (FontFamily)P_2;
			int priority = (int)P_1;
			P_0.SetValue(TemplatedControl.FontFamilyProperty, value, (BindingPriority)priority);
			return;
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_18(NumericUpDown P_0, BindingPriority P_1, object P_2)
	{
		if (P_2 is IBinding)
		{
			IBinding binding = (IBinding)P_2;
			AvaloniaObjectExtensions.Bind(P_0, NumericUpDown.NumberFormatProperty, binding);
			return;
		}
		if (P_2 is NumberFormatInfo)
		{
			NumberFormatInfo value = (NumberFormatInfo)P_2;
			int priority = (int)P_1;
			P_0.SetValue(NumericUpDown.NumberFormatProperty, value, (BindingPriority)priority);
			return;
		}
		if (P_2 == null)
		{
			NumberFormatInfo value = (NumberFormatInfo)P_2;
			int priority = (int)P_1;
			P_0.SetValue(NumericUpDown.NumberFormatProperty, value, (BindingPriority)priority);
			return;
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_19(Popup P_0, BindingPriority P_1, object P_2)
	{
		if (P_2 is IBinding)
		{
			IBinding binding = (IBinding)P_2;
			AvaloniaObjectExtensions.Bind(P_0, Popup.PlacementTargetProperty, binding);
			return;
		}
		if (P_2 is Control)
		{
			Control value = (Control)P_2;
			int priority = (int)P_1;
			P_0.SetValue(Popup.PlacementTargetProperty, value, (BindingPriority)priority);
			return;
		}
		if (P_2 == null)
		{
			Control value = (Control)P_2;
			int priority = (int)P_1;
			P_0.SetValue(Popup.PlacementTargetProperty, value, (BindingPriority)priority);
			return;
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_20(Button P_0, BindingPriority P_1, CompiledBindingExtension P_2)
	{
		if (P_2 != null)
		{
			IBinding binding = P_2;
			AvaloniaObjectExtensions.Bind(P_0, Button.CommandParameterProperty, binding);
		}
		else
		{
			object value = P_2;
			int priority = (int)P_1;
			P_0.SetValue(Button.CommandParameterProperty, value, (BindingPriority)priority);
		}
	}

	public static void _003C_003EXamlDynamicSetter_21(ContentPresenter P_0, BindingPriority P_1, object P_2)
	{
		if (P_2 is IBinding)
		{
			IBinding binding = (IBinding)P_2;
			AvaloniaObjectExtensions.Bind(P_0, ContentPresenter.FontFamilyProperty, binding);
			return;
		}
		if (P_2 is FontFamily)
		{
			FontFamily value = (FontFamily)P_2;
			int priority = (int)P_1;
			P_0.SetValue(ContentPresenter.FontFamilyProperty, value, (BindingPriority)priority);
			return;
		}
		if (P_2 == null)
		{
			FontFamily value = (FontFamily)P_2;
			int priority = (int)P_1;
			P_0.SetValue(ContentPresenter.FontFamilyProperty, value, (BindingPriority)priority);
			return;
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_22(UserContextMenuView P_0, CompiledBindingExtension P_1)
	{
		if (P_1 != null)
		{
			IBinding binding = P_1;
			AvaloniaObjectExtensions.Bind(P_0, UserContextMenuView.DataContextProperty, binding);
		}
		else
		{
			P_0.DataContext = P_1;
		}
	}

	public static void _003C_003EXamlDynamicSetter_23(Control P_0, CompiledBindingExtension P_1)
	{
		if (P_1 != null)
		{
			IBinding binding = P_1;
			AvaloniaObjectExtensions.Bind(P_0, Control.TagProperty, binding);
		}
		else
		{
			P_0.Tag = P_1;
		}
	}

	public static void _003C_003EXamlDynamicSetter_24(Button P_0, object P_1)
	{
		if (P_1 is UnsetValueType)
		{
			P_0.SetValue(Button.FlyoutProperty, AvaloniaProperty.UnsetValue);
			return;
		}
		if (P_1 is IBinding)
		{
			IBinding binding = (IBinding)P_1;
			AvaloniaObjectExtensions.Bind(P_0, Button.FlyoutProperty, binding);
			return;
		}
		if (P_1 is FlyoutBase)
		{
			P_0.Flyout = (FlyoutBase?)P_1;
			return;
		}
		if (P_1 == null)
		{
			P_0.Flyout = (FlyoutBase?)P_1;
			return;
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_25(Control P_0, object P_1)
	{
		if (P_1 is UnsetValueType)
		{
			P_0.SetValue(Control.ContextFlyoutProperty, AvaloniaProperty.UnsetValue);
			return;
		}
		if (P_1 is IBinding)
		{
			IBinding binding = (IBinding)P_1;
			AvaloniaObjectExtensions.Bind(P_0, Control.ContextFlyoutProperty, binding);
			return;
		}
		if (P_1 is FlyoutBase)
		{
			P_0.ContextFlyout = (FlyoutBase?)P_1;
			return;
		}
		if (P_1 == null)
		{
			P_0.ContextFlyout = (FlyoutBase?)P_1;
			return;
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_26(HeaderedContentControl P_0, CompiledBindingExtension P_1)
	{
		if (P_1 != null)
		{
			IBinding binding = P_1;
			AvaloniaObjectExtensions.Bind(P_0, HeaderedContentControl.HeaderProperty, binding);
		}
		else
		{
			P_0.Header = P_1;
		}
	}

	public static void _003C_003EXamlDynamicSetter_27(MenuItem P_0, CompiledBindingExtension P_1)
	{
		if (P_1 != null)
		{
			IBinding binding = P_1;
			AvaloniaObjectExtensions.Bind(P_0, MenuItem.CommandParameterProperty, binding);
		}
		else
		{
			P_0.CommandParameter = P_1;
		}
	}

	public static void _003C_003EXamlDynamicSetter_28(StyledElement P_0, object P_1)
	{
		if (P_1 is UnsetValueType)
		{
			P_0.SetValue(StyledElement.ThemeProperty, AvaloniaProperty.UnsetValue);
			return;
		}
		if (P_1 is IBinding)
		{
			IBinding binding = (IBinding)P_1;
			AvaloniaObjectExtensions.Bind(P_0, StyledElement.ThemeProperty, binding);
			return;
		}
		if (P_1 is ControlTheme)
		{
			P_0.Theme = (ControlTheme?)P_1;
			return;
		}
		if (P_1 == null)
		{
			P_0.Theme = (ControlTheme?)P_1;
			return;
		}
		throw new InvalidCastException();
	}

	public static void _003C_003EXamlDynamicSetter_29(SkiaImageControl P_0, CompiledBindingExtension P_1)
	{
		if (P_1 != null)
		{
			IBinding binding = P_1;
			AvaloniaObjectExtensions.Bind(P_0, SkiaImageControl.SourceProperty, binding);
		}
		else
		{
			P_0.Source = P_1;
		}
	}
}
