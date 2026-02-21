using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Platform.Storage;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Resources.Strings;

namespace RootApp.Client.Avalonia.UI.Community.Content;

public class TextChannelFileUploadView : UserControl
{
	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border BackgroundBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Grid FileDropGrid;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock UploadToTextBlock;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	private TextChannelFileUploadViewModel? _textChannelFileUploadViewModel => base.DataContext as TextChannelFileUploadViewModel;

	public TextChannelFileUploadView()
	{
		InitializeComponent();
	}

	protected override void OnLoaded(RoutedEventArgs P_0)
	{
		base.OnLoaded(P_0);
		AddHandler(DragDrop.DragEnterEvent, dragEnter);
		AddHandler(DragDrop.DropEvent, drop);
		AddHandler(DragDrop.DragLeaveEvent, dragLeave);
	}

	protected override void OnUnloaded(RoutedEventArgs P_0)
	{
		base.OnUnloaded(P_0);
		RemoveHandler(DragDrop.DragEnterEvent, dragEnter);
		RemoveHandler(DragDrop.DropEvent, drop);
		RemoveHandler(DragDrop.DragLeaveEvent, dragLeave);
	}

	private void dragEnter(object? sender, DragEventArgs e)
	{
		RemoveHandler(DragDrop.DragEnterEvent, dragEnter);
		IStorageItem[] array = e.DataTransfer.TryGetFiles();
		if (array != null && array.Length != 0 && _textChannelFileUploadViewModel != null && _textChannelFileUploadViewModel.ProcessFilesCommand.CanExecute(array))
		{
			_textChannelFileUploadViewModel.ProcessFilesCommand.Execute(array);
		}
	}

	private void drop(object? sender, DragEventArgs e)
	{
		if (_textChannelFileUploadViewModel != null && _textChannelFileUploadViewModel.UploadFilesCommand.CanExecute(null))
		{
			_textChannelFileUploadViewModel.UploadFilesCommand.Execute(null);
		}
		closeView();
	}

	private void dragLeave(object? sender, DragEventArgs e)
	{
		closeView();
	}

	private void closeView()
	{
		if (_textChannelFileUploadViewModel != null && _textChannelFileUploadViewModel.CloseViewCommand.CanExecute(null))
		{
			_textChannelFileUploadViewModel.CloseViewCommand.Execute(null);
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	[ExcludeFromCodeCoverage]
	public void InitializeComponent(bool P_0 = true)
	{
		if (P_0)
		{
			!XamlIlPopulateTrampoline(this);
		}
		INameScope nameScope = this.FindNameScope();
		BackgroundBorder = nameScope?.Find<Border>("BackgroundBorder");
		FileDropGrid = nameScope?.Find<Grid>("FileDropGrid");
		UploadToTextBlock = nameScope?.Find<TextBlock>("UploadToTextBlock");
	}

	[CompilerGenerated]
	private static void !XamlIlPopulate(IServiceProvider P_0, TextChannelFileUploadView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<TextChannelFileUploadView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<TextChannelFileUploadView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Community/Content/TextChannelFileUploadView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Content/TextChannelFileUploadView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		DragDrop.SetAllowDrop(P_1, true);
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		P_1.Content = panel;
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		Panel panel5 = panel4;
		global::Avalonia.Controls.Controls children = panel5.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Border border5 = border4;
		border5.Name = "BackgroundBorder";
		object obj = border5;
		context.AvaloniaNameScope.Register("BackgroundBorder", obj);
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("DropShadow");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, backgroundProperty, binding);
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		global::Avalonia.Controls.Controls children2 = panel5.Children;
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		children2.Add(grid);
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		grid4.Name = "FileDropGrid";
		obj = grid4;
		context.AvaloniaNameScope.Register("FileDropGrid", obj);
		grid4.IsHitTestVisible = false;
		global::Avalonia.Controls.Controls children3 = grid4.Children;
		Border border7;
		Border border6 = (border7 = new Border());
		((ISupportInitialize)border6).BeginInit();
		children3.Add(border6);
		Border border8 = (border4 = border7);
		context.PushParent(border4);
		Border border9 = border4;
		border9.Width = 560.0;
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border9, backgroundProperty2, binding2);
		border9.VerticalAlignment = VerticalAlignment.Center;
		border9.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		Panel panel7;
		Panel panel6 = (panel7 = new Panel());
		((ISupportInitialize)panel6).BeginInit();
		border9.Child = panel6;
		Panel panel8 = (panel4 = panel7);
		context.PushParent(panel4);
		Panel panel9 = panel4;
		global::Avalonia.Controls.Controls children4 = panel9.Children;
		Rectangle rectangle2;
		Rectangle rectangle = (rectangle2 = new Rectangle());
		((ISupportInitialize)rectangle).BeginInit();
		children4.Add(rectangle);
		Rectangle rectangle4;
		Rectangle rectangle3 = (rectangle4 = rectangle2);
		context.PushParent(rectangle4);
		rectangle4.Margin = new Thickness(20.0, 20.0, 20.0, 20.0);
		rectangle4.StrokeThickness = 2.0;
		AvaloniaList<double> avaloniaList = new AvaloniaList<double>();
		avaloniaList.Capacity = 2;
		avaloniaList.Add(5.0);
		avaloniaList.Add(5.0);
		rectangle4.StrokeDashArray = avaloniaList;
		rectangle4.StrokeLineCap = PenLineCap.Round;
		StyledProperty<IBrush?> strokeProperty = Shape.StrokeProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = Shape.StrokeProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle4, strokeProperty, binding3);
		context.PopParent();
		((ISupportInitialize)rectangle3).EndInit();
		global::Avalonia.Controls.Controls children5 = panel9.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children5.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		stackPanel4.Margin = new Thickness(48.0, 48.0, 48.0, 48.0);
		stackPanel4.VerticalAlignment = VerticalAlignment.Center;
		stackPanel4.MaxWidth = 340.0;
		global::Avalonia.Controls.Controls children6 = stackPanel4.Children;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		children6.Add(rootSvgImage);
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		rootSvgImage4.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
		rootSvgImage4.Width = 78.0;
		rootSvgImage4.Height = 78.0;
		StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("EmptyStateSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage4, svgPathProperty, binding4);
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		global::Avalonia.Controls.Controls children7 = stackPanel4.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children7.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Name = "UploadToTextBlock";
		obj = textBlock5;
		context.AvaloniaNameScope.Register("UploadToTextBlock", obj);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock5, obj2);
		textBlock5.FontWeight = FontWeight.Bold;
		textBlock5.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		textBlock5.FontSize = 24.0;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding5);
		textBlock5.TextAlignment = TextAlignment.Center;
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock5.HorizontalAlignment = HorizontalAlignment.Center;
		textBlock5.TextWrapping = TextWrapping.Wrap;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		MultiBinding multiBinding2;
		MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("PrependStringConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj3 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding2.Converter = (IMultiValueConverter)obj3;
		IList<IBinding> bindings = multiBinding2.Bindings;
		CompiledBindingExtension obj4 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Content.TextChannelFileUploadViewModel,RootApp.Client.Avalonia.Name!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item = obj4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings.Add(item);
		IList<IBinding> bindings2 = multiBinding2.Bindings;
		CompiledBindingExtension obj5 = new CompiledBindingExtension
		{
			Source = RootApp.Client.Avalonia.Resources.Strings.Resources.UploadTo
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item2 = obj5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings2.Add(item2);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, multiBinding);
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		global::Avalonia.Controls.Controls children8 = stackPanel4.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children8.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.TextWrapping = TextWrapping.Wrap;
		textBlock9.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.AddMoreCommentsBeforeSending;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj6 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock9, obj6);
		textBlock9.FontWeight = FontWeight.Medium;
		textBlock9.Margin = new Thickness(0.0, 10.0, 0.0, 0.0);
		textBlock9.FontSize = 14.0;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding6);
		textBlock9.Padding = new Thickness(10.0, 0.0, 10.0, 0.0);
		textBlock9.HorizontalAlignment = HorizontalAlignment.Center;
		textBlock9.TextAlignment = TextAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)panel8).EndInit();
		context.PopParent();
		((ISupportInitialize)border8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		context.PopParent();
		((ISupportInitialize)panel3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void !XamlIlPopulateTrampoline(TextChannelFileUploadView P_0)
	{
		if (!XamlIlPopulateOverride != null)
		{
			!XamlIlPopulateOverride(P_0);
		}
		else
		{
			!XamlIlPopulate(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(null), P_0);
		}
	}
}
