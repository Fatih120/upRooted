using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Platform.Storage;
using Avalonia.Styling;
using Avalonia.Threading;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Helpers.Overlay;
using RootApp.Client.Avalonia.Resources.Converters.GlobalUsers;
using RootApp.Client.Avalonia.Resources.Converters.Messages;
using RootApp.Client.Avalonia.Resources.Strings;

namespace RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages;

public class DirectMessageContentView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_136
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<DirectMessageContentView> context = CreateContext(P_0);
			return new MessageDateConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<DirectMessageContentView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<DirectMessageContentView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<DirectMessageContentView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Home/SystemTray/DirectMessages/DirectMessageContentView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Home/SystemTray/DirectMessages/DirectMessageContentView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (DirectMessageContentView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<DirectMessageContentView> context = CreateContext(P_0);
			return new GlobalUserOnlineStatusConverter();
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<DirectMessageContentView> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((AvaloniaObject)obj).SetValue(Layoutable.MarginProperty, new Thickness(0.0, 0.0, 0.0, 12.0), BindingPriority.Template);
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static object Build_4(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<DirectMessageContentView> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((AvaloniaObject)obj).SetValue(Layoutable.MarginProperty, new Thickness(0.0, 12.0, 0.0, 12.0), BindingPriority.Template);
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static object Build_5(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<DirectMessageContentView> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((AvaloniaObject)obj).SetValue(StackPanel.OrientationProperty, Orientation.Horizontal, BindingPriority.Template);
			((ISupportInitialize)obj).EndInit();
			return obj;
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Grid TitleGrid;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootMessageScrollViewer ScrollViewer;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border MessageBlockerBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootBorder AutoCompleteBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock NewMessagesTextBlock;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock NewMessagesDateTextBlock;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal ContentControl CallContent;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal ContentControl DetailsContent;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	private DirectMessageContentViewModel? _directMessageContentViewModel => base.DataContext as DirectMessageContentViewModel;

	public DirectMessageContentView()
	{
		InitializeComponent();
	}

	protected override void OnLoaded(RoutedEventArgs P_0)
	{
		base.OnLoaded(P_0);
		if (_directMessageContentViewModel != null)
		{
			AddHandler(DragDrop.DragEnterEvent, DragEnter);
			_directMessageContentViewModel.PropertyChanged += onDirectMessageContentViewModelPropertyChanged;
			if (_directMessageContentViewModel.ViewLoadedCommand.CanExecute(null))
			{
				_directMessageContentViewModel.ViewLoadedCommand.Execute(null);
			}
			if (_directMessageContentViewModel.InitialMessagesBeganRendering)
			{
				MessageBlockerBorder.IsVisible = false;
			}
		}
	}

	protected override void OnUnloaded(RoutedEventArgs P_0)
	{
		base.OnUnloaded(P_0);
		if (_directMessageContentViewModel != null)
		{
			RemoveHandler(DragDrop.DragEnterEvent, DragEnter);
			_directMessageContentViewModel.PropertyChanged -= onDirectMessageContentViewModelPropertyChanged;
			if (_directMessageContentViewModel.ViewUnloadedCommand.CanExecute(null))
			{
				_directMessageContentViewModel.ViewUnloadedCommand.Execute(null);
			}
			CallContent.Content = null;
		}
	}

	protected override void OnSizeChanged(SizeChangedEventArgs P_0)
	{
		base.OnSizeChanged(P_0);
		if (base.Bounds.Width == 0.0)
		{
			_directMessageContentViewModel?.SetFocusTrackingState(false);
		}
		else
		{
			_directMessageContentViewModel?.SetFocusTrackingState(true);
		}
	}

	private void onDirectMessageContentViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (_directMessageContentViewModel != null && e.PropertyName == "InitialMessagesBeganRendering" && _directMessageContentViewModel.InitialMessagesBeganRendering)
			{
				MessageBlockerBorder.IsVisible = false;
			}
		});
	}

	private void DragEnter(object? sender, DragEventArgs e)
	{
		if (_directMessageContentViewModel == null)
		{
			e.Handled = true;
			return;
		}
		if (_directMessageContentViewModel.IsFileUploaderOpen)
		{
			e.Handled = true;
			return;
		}
		nint valueOrDefault = (TopLevel.GetTopLevel(this)?.TryGetPlatformHandle()?.Handle).GetValueOrDefault();
		if (!OverlayInterop.IsWindowAtCursorPosition(valueOrDefault))
		{
			e.Handled = true;
			return;
		}
		IStorageItem[] array = e.DataTransfer.TryGetFiles();
		if (array != null && array.Length != 0)
		{
			_directMessageContentViewModel.ShowFileUploadView();
		}
		e.Handled = true;
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
		TitleGrid = nameScope?.Find<Grid>("TitleGrid");
		ScrollViewer = nameScope?.Find<RootMessageScrollViewer>("ScrollViewer");
		MessageBlockerBorder = nameScope?.Find<Border>("MessageBlockerBorder");
		AutoCompleteBorder = nameScope?.Find<RootBorder>("AutoCompleteBorder");
		NewMessagesTextBlock = nameScope?.Find<TextBlock>("NewMessagesTextBlock");
		NewMessagesDateTextBlock = nameScope?.Find<TextBlock>("NewMessagesDateTextBlock");
		CallContent = nameScope?.Find<ContentControl>("CallContent");
		DetailsContent = nameScope?.Find<ContentControl>("DetailsContent");
	}

	[CompilerGenerated]
	private unsafe static void !XamlIlPopulate(IServiceProvider P_0, DirectMessageContentView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<DirectMessageContentView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<DirectMessageContentView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Home/SystemTray/DirectMessages/DirectMessageContentView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Home/SystemTray/DirectMessages/DirectMessageContentView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		DragDrop.SetAllowDrop(P_1, true);
		StyledProperty<IBrush?> backgroundProperty = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(P_1, backgroundProperty, binding);
		ResourceDictionary resourceDictionary = new ResourceDictionary();
		if (resourceDictionary is ResourceDictionary resourceDictionary2)
		{
			resourceDictionary2.EnsureCapacity(resourceDictionary2.Count + 2);
		}
		resourceDictionary.AddDeferred("MessageDateConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_136.Build_1), context));
		resourceDictionary.AddDeferred("GlobalUserOnlineStatusConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_136.Build_2), context));
		P_1.Resources = resourceDictionary;
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		P_1.Content = panel;
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		Panel panel5 = panel4;
		global::Avalonia.Controls.Controls children = panel5.Children;
		Rectangle rectangle2;
		Rectangle rectangle = (rectangle2 = new Rectangle());
		((ISupportInitialize)rectangle).BeginInit();
		children.Add(rectangle);
		Rectangle rectangle4;
		Rectangle rectangle3 = (rectangle4 = rectangle2);
		context.PushParent(rectangle4);
		Rectangle rectangle5 = rectangle4;
		rectangle5.Height = 0.5;
		StyledProperty<IBrush?> fillProperty = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle5, fillProperty, binding2);
		rectangle5.VerticalAlignment = VerticalAlignment.Top;
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.ShowCloseButton!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle5, isVisibleProperty, compiledBindingExtension2);
		context.PopParent();
		((ISupportInitialize)rectangle3).EndInit();
		global::Avalonia.Controls.Controls children2 = panel5.Children;
		Panel panel7;
		Panel panel6 = (panel7 = new Panel());
		((ISupportInitialize)panel6).BeginInit();
		children2.Add(panel6);
		Panel panel8 = (panel4 = panel7);
		context.PushParent(panel4);
		Panel panel9 = panel4;
		StyledProperty<Thickness> marginProperty = Layoutable.MarginProperty;
		CompiledBindingExtension compiledBindingExtension4;
		CompiledBindingExtension compiledBindingExtension3 = (compiledBindingExtension4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.ShowCloseButton!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension4);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("MarginIfTrueConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension4.Converter = (IValueConverter)obj;
		compiledBindingExtension4.ConverterParameter = "0,.5,0,0";
		context.PopParent();
		context.ProvideTargetProperty = Layoutable.MarginProperty;
		CompiledBindingExtension compiledBindingExtension5 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(panel9, marginProperty, compiledBindingExtension5);
		global::Avalonia.Controls.Controls children3 = panel9.Children;
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		children3.Add(grid);
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		Grid grid5 = grid4;
		grid5.Margin = new Thickness(16.0, 0.0, 16.0, 0.0);
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension6 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().ElementName(context.AvaloniaNameScope, "DetailsContent").Property(Visual.IsVisibleProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension7 = compiledBindingExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(grid5, isVisibleProperty2, compiledBindingExtension7);
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(60.0, GridUnitType.Pixel)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(6.0, GridUnitType.Pixel)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(20.0, GridUnitType.Pixel)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(6.0, GridUnitType.Pixel)
		});
		global::Avalonia.Controls.Controls children4 = grid5.Children;
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		children4.Add(grid6);
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		Grid.SetRow(grid9, 0);
		grid9.Name = "TitleGrid";
		object obj2 = grid9;
		context.AvaloniaNameScope.Register("TitleGrid", obj2);
		grid9.Background = new ImmutableSolidColorBrush(16777215u);
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		global::Avalonia.Controls.Controls children5 = grid9.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children5.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		Button button5 = button4;
		Grid.SetColumn(button5, 0);
		button5.Classes.Add("BasicButton");
		button5.Width = 26.0;
		button5.Height = 26.0;
		button5.VerticalAlignment = VerticalAlignment.Center;
		button5.HorizontalAlignment = HorizontalAlignment.Left;
		button5.BorderThickness = new Thickness(1.0, 1.0, 1.0, 1.0);
		StyledProperty<IBrush?> borderBrushProperty = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, borderBrushProperty, binding3);
		button5.Background = new ImmutableSolidColorBrush(16777215u);
		button5.CornerRadius = new CornerRadius(13.0, 13.0, 13.0, 13.0);
		button5.Margin = new Thickness(0.0, 0.0, 8.0, 0.0);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension8 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.CloseConversationCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension9 = compiledBindingExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, commandProperty, compiledBindingExtension9);
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension10 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.ShowCloseButton!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension11 = compiledBindingExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, isVisibleProperty3, compiledBindingExtension11);
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		button5.Content = rootSvgImage;
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage5 = rootSvgImage4;
		StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("DownArrowSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage5, svgPathProperty, binding4);
		rootSvgImage5.Width = 10.0;
		rootSvgImage5.Height = 8.0;
		rootSvgImage5.VerticalAlignment = VerticalAlignment.Center;
		rootSvgImage5.RenderTransform = new RotateTransform
		{
			Angle = 90.0
		};
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		global::Avalonia.Controls.Controls children6 = grid9.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children6.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Border border5 = border4;
		Grid.SetColumn(border5, 0);
		border5.Margin = new Thickness(8.0, 0.0, 0.0, 20.0);
		border5.Height = 20.0;
		border5.MinWidth = 20.0;
		border5.IsHitTestVisible = false;
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("Error");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, backgroundProperty2, binding5);
		border5.BorderThickness = new Thickness(3.0, 3.0, 3.0, 3.0);
		StyledProperty<IBrush?> borderBrushProperty2 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, borderBrushProperty2, binding6);
		border5.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		border5.HorizontalAlignment = HorizontalAlignment.Center;
		border5.Padding = new Thickness(1.0, 0.0, 1.0, 0.0);
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		MultiBinding multiBinding2;
		MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding3 = multiBinding2;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj3 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding3.Converter = (IMultiValueConverter)obj3;
		IList<IBinding> bindings = multiBinding3.Bindings;
		CompiledBindingExtension obj4 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.OtherDmActivityCount!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item = obj4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings.Add(item);
		IList<IBinding> bindings2 = multiBinding3.Bindings;
		CompiledBindingExtension obj5 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.ShowCloseButton!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item2 = obj5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings2.Add(item2);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(border5, isVisibleProperty4, multiBinding);
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		border5.Child = textBlock;
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.FontSize = 11.0;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding7);
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj6 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock5, obj6);
		textBlock5.FontWeight = FontWeight.Bold;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension obj7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.OtherDmActivityCount!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			FallbackValue = "0"
		};
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension12 = obj7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, compiledBindingExtension12);
		textBlock5.HorizontalAlignment = HorizontalAlignment.Center;
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		textBlock5.TextTrimming = TextTrimming.CharacterEllipsis;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		global::Avalonia.Controls.Controls children7 = grid9.Children;
		Panel panel11;
		Panel panel10 = (panel11 = new Panel());
		((ISupportInitialize)panel10).BeginInit();
		children7.Add(panel10);
		Panel panel12 = (panel4 = panel11);
		context.PushParent(panel4);
		Panel panel13 = panel4;
		Grid.SetColumn(panel13, 1);
		StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj8 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DirectMessageViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageViewModel,RootApp.Client.Avalonia.SingleMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Converter = ObjectConverters.IsNotNull
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension13 = obj8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(panel13, isVisibleProperty5, compiledBindingExtension13);
		panel13.Margin = new Thickness(0.0, 0.0, 8.0, 0.0);
		panel13.HorizontalAlignment = HorizontalAlignment.Right;
		global::Avalonia.Controls.Controls children8 = panel13.Children;
		Ellipse ellipse2;
		Ellipse ellipse = (ellipse2 = new Ellipse());
		((ISupportInitialize)ellipse).BeginInit();
		children8.Add(ellipse);
		Ellipse ellipse4;
		Ellipse ellipse3 = (ellipse4 = ellipse2);
		context.PushParent(ellipse4);
		Ellipse ellipse5 = ellipse4;
		ellipse5.Width = 8.0;
		ellipse5.Height = 8.0;
		StyledProperty<IBrush?> fillProperty2 = Shape.FillProperty;
		MultiBinding multiBinding4 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding5 = multiBinding2;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("GlobalUserOnlineStatusConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj9 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding5.Converter = (IMultiValueConverter)obj9;
		IList<IBinding> bindings3 = multiBinding5.Bindings;
		CompiledBindingExtension obj10 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DirectMessageViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageViewModel,RootApp.Client.Avalonia.SingleMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.IMessageContainerMember,RootApp.Client.CoreDomain.GlobalUser!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.User.GlobalUser,RootApp.Client.CoreDomain.OnlineStatus!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item3 = obj10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings3.Add(item3);
		IList<IBinding> bindings4 = multiBinding5.Bindings;
		CompiledBindingExtension compiledBindingExtension14 = new CompiledBindingExtension();
		compiledBindingExtension14.Path = new CompiledBindingPathBuilder(1).Property(Application.ActualThemeVariantProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
		compiledBindingExtension14.Source = Application.Current;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item4 = compiledBindingExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings4.Add(item4);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(ellipse5, fillProperty2, multiBinding4);
		context.PopParent();
		((ISupportInitialize)ellipse3).EndInit();
		context.PopParent();
		((ISupportInitialize)panel12).EndInit();
		global::Avalonia.Controls.Controls children9 = grid9.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children9.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		Grid.SetColumn(textBlock9, 2);
		StyledProperty<string?> textProperty2 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension15 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DirectMessageViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageViewModel,RootApp.Client.Avalonia.DirectMessageName!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension16 = compiledBindingExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, textProperty2, compiledBindingExtension16);
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock9.Margin = new Thickness(0.0, 0.0, 8.0, 0.0);
		textBlock9.TextTrimming = TextTrimming.CharacterEllipsis;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding8);
		textBlock9.FontSize = 16.0;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj11 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock9, obj11);
		textBlock9.FontWeight = (FontWeight)450;
		textBlock9.VerticalAlignment = VerticalAlignment.Center;
		textBlock9.HorizontalAlignment = HorizontalAlignment.Left;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		global::Avalonia.Controls.Controls children10 = grid9.Children;
		Panel panel15;
		Panel panel14 = (panel15 = new Panel());
		((ISupportInitialize)panel14).BeginInit();
		children10.Add(panel14);
		Panel panel16 = (panel4 = panel15);
		context.PushParent(panel4);
		Panel panel17 = panel4;
		Grid.SetColumn(panel17, 3);
		global::Avalonia.Controls.Controls children11 = panel17.Children;
		RootSvgButton rootSvgButton2;
		RootSvgButton rootSvgButton = (rootSvgButton2 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton).BeginInit();
		children11.Add(rootSvgButton);
		RootSvgButton rootSvgButton4;
		RootSvgButton rootSvgButton3 = (rootSvgButton4 = rootSvgButton2);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton5 = rootSvgButton4;
		rootSvgButton5.Classes.Add("SvgDimmedButton");
		rootSvgButton5.Width = 32.0;
		rootSvgButton5.Height = 32.0;
		rootSvgButton5.SvgWidth = 18.0;
		rootSvgButton5.SvgHeight = 18.0;
		rootSvgButton5.Margin = new Thickness(0.0, 0.0, 4.0, 0.0);
		StyledProperty<string> svgPathProperty2 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("CallSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton5, svgPathProperty2, binding9);
		StyledProperty<ICommand?> commandProperty2 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension17 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.StartCallCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension18 = compiledBindingExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton5, commandProperty2, compiledBindingExtension18);
		ToolTip.SetPlacement(rootSvgButton5, PlacementMode.Bottom);
		ToolTip.SetVerticalOffset(rootSvgButton5, 1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton5, 0.0);
		ToolTip.SetShowDelay(rootSvgButton5, 0);
		StyledProperty<bool> isVisibleProperty6 = Visual.IsVisibleProperty;
		MultiBinding multiBinding6 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding7 = multiBinding2;
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj12 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding7.Converter = (IMultiValueConverter)obj12;
		IList<IBinding> bindings5 = multiBinding7.Bindings;
		CompiledBindingExtension obj13 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DirectMessageViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageViewModel,RootApp.Client.Avalonia.DirectMessage!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.DirectMessages.DirectMessage,RootApp.Client.CoreDomain.MediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.HasActiveCall!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item5 = obj13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings5.Add(item5);
		IList<IBinding> bindings6 = multiBinding7.Bindings;
		CompiledBindingExtension obj14 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DirectMessageViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageViewModel,RootApp.Client.Avalonia.DirectMessage!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.DirectMessages.DirectMessage,RootApp.Client.CoreDomain.IsDraft!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item6 = obj14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings6.Add(item6);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgButton5, isVisibleProperty6, multiBinding6);
		RootToolTip rootToolTip2;
		RootToolTip rootToolTip = (rootToolTip2 = new RootToolTip());
		((ISupportInitialize)rootToolTip).BeginInit();
		ToolTip.SetTip(rootSvgButton5, rootToolTip);
		RootToolTip rootToolTip4;
		RootToolTip rootToolTip3 = (rootToolTip4 = rootToolTip2);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip5 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip5, PlacementMode.Bottom);
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		rootToolTip5.Content = textBlock10;
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		textBlock13.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Call;
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj15 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock13, obj15);
		textBlock13.FontWeight = (FontWeight)450;
		textBlock13.FontSize = 14.0;
		textBlock13.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock13.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton3).EndInit();
		global::Avalonia.Controls.Controls children12 = panel17.Children;
		RootSvgButton rootSvgButton7;
		RootSvgButton rootSvgButton6 = (rootSvgButton7 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton6).BeginInit();
		children12.Add(rootSvgButton6);
		RootSvgButton rootSvgButton8 = (rootSvgButton4 = rootSvgButton7);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton9 = rootSvgButton4;
		rootSvgButton9.Classes.Add("SvgDimmedButton");
		rootSvgButton9.Width = 32.0;
		rootSvgButton9.Height = 32.0;
		rootSvgButton9.SvgWidth = 19.0;
		rootSvgButton9.SvgHeight = 19.0;
		rootSvgButton9.Margin = new Thickness(0.0, 0.0, 4.0, 0.0);
		StyledProperty<string> svgPathProperty3 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("DirectMessagesSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, svgPathProperty3, binding10);
		StyledProperty<ICommand?> commandProperty3 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension19 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.FocusMessagesCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension20 = compiledBindingExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, commandProperty3, compiledBindingExtension20);
		ToolTip.SetPlacement(rootSvgButton9, PlacementMode.Bottom);
		ToolTip.SetVerticalOffset(rootSvgButton9, 1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton9, 0.0);
		ToolTip.SetShowDelay(rootSvgButton9, 0);
		StyledProperty<bool> isVisibleProperty7 = Visual.IsVisibleProperty;
		MultiBinding multiBinding8 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding9 = multiBinding2;
		StaticResourceExtension staticResourceExtension8 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj16 = staticResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding9.Converter = (IMultiValueConverter)obj16;
		IList<IBinding> bindings7 = multiBinding9.Bindings;
		CompiledBindingExtension obj17 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DirectMessageViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageViewModel,RootApp.Client.Avalonia.DirectMessage!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.DirectMessages.DirectMessage,RootApp.Client.CoreDomain.MediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.HasActiveCall!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item7 = obj17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings7.Add(item7);
		IList<IBinding> bindings8 = multiBinding9.Bindings;
		CompiledBindingExtension compiledBindingExtension21 = new CompiledBindingExtension();
		compiledBindingExtension21.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DirectMessageViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageViewModel,RootApp.Client.Avalonia.DirectMessage!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.DirectMessages.DirectMessage,RootApp.Client.CoreDomain.MediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension21.Converter = ObjectConverters.IsNotNull;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item8 = compiledBindingExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings8.Add(item8);
		IList<IBinding> bindings9 = multiBinding9.Bindings;
		CompiledBindingExtension obj18 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.FocusedOnCall!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item9 = obj18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings9.Add(item9);
		IList<IBinding> bindings10 = multiBinding9.Bindings;
		CompiledBindingExtension obj19 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DirectMessageViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageViewModel,RootApp.Client.Avalonia.DirectMessage!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.DirectMessages.DirectMessage,RootApp.Client.CoreDomain.MediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.IsPoppedOut!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item10 = obj19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings10.Add(item10);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgButton9, isVisibleProperty7, multiBinding8);
		RootToolTip rootToolTip7;
		RootToolTip rootToolTip6 = (rootToolTip7 = new RootToolTip());
		((ISupportInitialize)rootToolTip6).BeginInit();
		ToolTip.SetTip(rootSvgButton9, rootToolTip6);
		RootToolTip rootToolTip8 = (rootToolTip4 = rootToolTip7);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip9 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip9, PlacementMode.Bottom);
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		rootToolTip9.Content = textBlock14;
		TextBlock textBlock16 = (textBlock4 = textBlock15);
		context.PushParent(textBlock4);
		TextBlock textBlock17 = textBlock4;
		textBlock17.Text = "View messages";
		StaticResourceExtension staticResourceExtension9 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj20 = staticResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock17, obj20);
		textBlock17.FontWeight = (FontWeight)450;
		textBlock17.FontSize = 14.0;
		textBlock17.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock17.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock16).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip8).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton8).EndInit();
		global::Avalonia.Controls.Controls children13 = panel17.Children;
		Button button7;
		Button button6 = (button7 = new Button());
		((ISupportInitialize)button6).BeginInit();
		children13.Add(button6);
		Button button8 = (button4 = button7);
		context.PushParent(button4);
		Button button9 = button4;
		button9.Classes.Add("BorderButton");
		button9.Height = 32.0;
		button9.Width = 112.0;
		StyledProperty<IBrush?> borderBrushProperty3 = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, borderBrushProperty3, binding11);
		button9.Background = new ImmutableSolidColorBrush(16777215u);
		button9.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		button9.CornerRadius = new CornerRadius(46.0, 46.0, 46.0, 46.0);
		button9.HorizontalContentAlignment = HorizontalAlignment.Stretch;
		button9.Margin = new Thickness(0.0, 0.0, 4.0, 0.0);
		button9.Padding = new Thickness(5.0, 4.0, 5.0, 4.0);
		StyledProperty<ICommand?> commandProperty4 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension22 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.FocusCallCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension23 = compiledBindingExtension22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, commandProperty4, compiledBindingExtension23);
		StyledProperty<bool> isVisibleProperty8 = Visual.IsVisibleProperty;
		MultiBinding multiBinding10 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding11 = multiBinding2;
		StaticResourceExtension staticResourceExtension10 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj21 = staticResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding11.Converter = (IMultiValueConverter)obj21;
		IList<IBinding> bindings11 = multiBinding11.Bindings;
		CompiledBindingExtension obj22 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DirectMessageViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageViewModel,RootApp.Client.Avalonia.DirectMessage!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.DirectMessages.DirectMessage,RootApp.Client.CoreDomain.MediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.HasActiveCall!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item11 = obj22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings11.Add(item11);
		IList<IBinding> bindings12 = multiBinding11.Bindings;
		CompiledBindingExtension compiledBindingExtension24 = new CompiledBindingExtension();
		compiledBindingExtension24.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DirectMessageViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageViewModel,RootApp.Client.Avalonia.DirectMessage!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.DirectMessages.DirectMessage,RootApp.Client.CoreDomain.MediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension24.Converter = ObjectConverters.IsNotNull;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item12 = compiledBindingExtension24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings12.Add(item12);
		IList<IBinding> bindings13 = multiBinding11.Bindings;
		CompiledBindingExtension obj23 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.FocusedOnCall!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item13 = obj23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings13.Add(item13);
		IList<IBinding> bindings14 = multiBinding11.Bindings;
		CompiledBindingExtension obj24 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DirectMessageViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageViewModel,RootApp.Client.Avalonia.DirectMessage!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.DirectMessages.DirectMessage,RootApp.Client.CoreDomain.MediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.IsPoppedOut!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item14 = obj24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings14.Add(item14);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(button9, isVisibleProperty8, multiBinding10);
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		button9.Content = stackPanel;
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		stackPanel5.Orientation = Orientation.Horizontal;
		stackPanel5.Spacing = 8.0;
		global::Avalonia.Controls.Controls children14 = stackPanel5.Children;
		Panel panel19;
		Panel panel18 = (panel19 = new Panel());
		((ISupportInitialize)panel18).BeginInit();
		children14.Add(panel18);
		Panel panel20 = (panel4 = panel19);
		context.PushParent(panel4);
		Panel panel21 = panel4;
		global::Avalonia.Controls.Controls children15 = panel21.Children;
		ThemeVariantScope themeVariantScope2;
		ThemeVariantScope themeVariantScope = (themeVariantScope2 = new ThemeVariantScope());
		((ISupportInitialize)themeVariantScope).BeginInit();
		children15.Add(themeVariantScope);
		ThemeVariantScope themeVariantScope4;
		ThemeVariantScope themeVariantScope3 = (themeVariantScope4 = themeVariantScope2);
		context.PushParent(themeVariantScope4);
		ThemeVariantScope themeVariantScope5 = themeVariantScope4;
		themeVariantScope5.RequestedThemeVariant = ThemeVariant.Light;
		Ellipse ellipse7;
		Ellipse ellipse6 = (ellipse7 = new Ellipse());
		((ISupportInitialize)ellipse6).BeginInit();
		themeVariantScope5.Child = ellipse6;
		Ellipse ellipse8 = (ellipse4 = ellipse7);
		context.PushParent(ellipse4);
		Ellipse ellipse9 = ellipse4;
		StyledProperty<IBrush?> fillProperty3 = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("BrandTertiary");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding12 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(ellipse9, fillProperty3, binding12);
		ellipse9.Width = 24.0;
		ellipse9.Height = 24.0;
		context.PopParent();
		((ISupportInitialize)ellipse8).EndInit();
		context.PopParent();
		((ISupportInitialize)themeVariantScope3).EndInit();
		global::Avalonia.Controls.Controls children16 = panel21.Children;
		RootSvgImage rootSvgImage7;
		RootSvgImage rootSvgImage6 = (rootSvgImage7 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage6).BeginInit();
		children16.Add(rootSvgImage6);
		RootSvgImage rootSvgImage8 = (rootSvgImage4 = rootSvgImage7);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage9 = rootSvgImage4;
		rootSvgImage9.Width = 11.0;
		rootSvgImage9.Height = 11.0;
		rootSvgImage9.VerticalAlignment = VerticalAlignment.Center;
		rootSvgImage9.HorizontalAlignment = HorizontalAlignment.Center;
		StyledProperty<string?> svgPathProperty4 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("DMCallSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding13 = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage9, svgPathProperty4, binding13);
		context.PopParent();
		((ISupportInitialize)rootSvgImage8).EndInit();
		context.PopParent();
		((ISupportInitialize)panel20).EndInit();
		global::Avalonia.Controls.Controls children17 = stackPanel5.Children;
		TextBlock textBlock19;
		TextBlock textBlock18 = (textBlock19 = new TextBlock());
		((ISupportInitialize)textBlock18).BeginInit();
		children17.Add(textBlock18);
		TextBlock textBlock20 = (textBlock4 = textBlock19);
		context.PushParent(textBlock4);
		TextBlock textBlock21 = textBlock4;
		StaticResourceExtension staticResourceExtension11 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj25 = staticResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock21, obj25);
		textBlock21.FontWeight = (FontWeight)450;
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding14 = dynamicResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock21, foregroundProperty3, binding14);
		textBlock21.FontSize = 14.0;
		textBlock21.VerticalAlignment = VerticalAlignment.Center;
		textBlock21.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ViewCall;
		context.PopParent();
		((ISupportInitialize)textBlock20).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)button8).EndInit();
		global::Avalonia.Controls.Controls children18 = panel17.Children;
		Button button11;
		Button button10 = (button11 = new Button());
		((ISupportInitialize)button10).BeginInit();
		children18.Add(button10);
		Button button12 = (button4 = button11);
		context.PushParent(button4);
		Button button13 = button4;
		button13.Classes.Add("BorderButton");
		button13.Height = 32.0;
		button13.Width = 112.0;
		StyledProperty<IBrush?> borderBrushProperty4 = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension15 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding15 = dynamicResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button13, borderBrushProperty4, binding15);
		button13.Background = new ImmutableSolidColorBrush(16777215u);
		button13.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		button13.CornerRadius = new CornerRadius(46.0, 46.0, 46.0, 46.0);
		button13.HorizontalContentAlignment = HorizontalAlignment.Stretch;
		button13.Margin = new Thickness(0.0, 0.0, 4.0, 0.0);
		button13.Padding = new Thickness(5.0, 4.0, 5.0, 4.0);
		StyledProperty<ICommand?> commandProperty5 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension25 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.StartCallCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension26 = compiledBindingExtension25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button13, commandProperty5, compiledBindingExtension26);
		StyledProperty<bool> isVisibleProperty9 = Visual.IsVisibleProperty;
		MultiBinding multiBinding12 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding13 = multiBinding2;
		StaticResourceExtension staticResourceExtension12 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj26 = staticResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding13.Converter = (IMultiValueConverter)obj26;
		IList<IBinding> bindings15 = multiBinding13.Bindings;
		CompiledBindingExtension obj27 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DirectMessageViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageViewModel,RootApp.Client.Avalonia.DirectMessage!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.DirectMessages.DirectMessage,RootApp.Client.CoreDomain.MediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.HasActiveCall!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item15 = obj27.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings15.Add(item15);
		IList<IBinding> bindings16 = multiBinding13.Bindings;
		CompiledBindingExtension compiledBindingExtension27 = new CompiledBindingExtension();
		compiledBindingExtension27.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DirectMessageViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageViewModel,RootApp.Client.Avalonia.DirectMessage!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.DirectMessages.DirectMessage,RootApp.Client.CoreDomain.MediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension27.Converter = ObjectConverters.IsNull;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item16 = compiledBindingExtension27.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings16.Add(item16);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(button13, isVisibleProperty9, multiBinding12);
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		button13.Content = stackPanel6;
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		stackPanel9.Orientation = Orientation.Horizontal;
		stackPanel9.Spacing = 8.0;
		global::Avalonia.Controls.Controls children19 = stackPanel9.Children;
		Panel panel23;
		Panel panel22 = (panel23 = new Panel());
		((ISupportInitialize)panel22).BeginInit();
		children19.Add(panel22);
		Panel panel24 = (panel4 = panel23);
		context.PushParent(panel4);
		Panel panel25 = panel4;
		global::Avalonia.Controls.Controls children20 = panel25.Children;
		ThemeVariantScope themeVariantScope7;
		ThemeVariantScope themeVariantScope6 = (themeVariantScope7 = new ThemeVariantScope());
		((ISupportInitialize)themeVariantScope6).BeginInit();
		children20.Add(themeVariantScope6);
		ThemeVariantScope themeVariantScope8 = (themeVariantScope4 = themeVariantScope7);
		context.PushParent(themeVariantScope4);
		ThemeVariantScope themeVariantScope9 = themeVariantScope4;
		themeVariantScope9.RequestedThemeVariant = ThemeVariant.Light;
		Ellipse ellipse11;
		Ellipse ellipse10 = (ellipse11 = new Ellipse());
		((ISupportInitialize)ellipse10).BeginInit();
		themeVariantScope9.Child = ellipse10;
		Ellipse ellipse12 = (ellipse4 = ellipse11);
		context.PushParent(ellipse4);
		Ellipse ellipse13 = ellipse4;
		StyledProperty<IBrush?> fillProperty4 = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension16 = new DynamicResourceExtension("BrandTertiary");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding16 = dynamicResourceExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(ellipse13, fillProperty4, binding16);
		ellipse13.Width = 24.0;
		ellipse13.Height = 24.0;
		context.PopParent();
		((ISupportInitialize)ellipse12).EndInit();
		context.PopParent();
		((ISupportInitialize)themeVariantScope8).EndInit();
		global::Avalonia.Controls.Controls children21 = panel25.Children;
		RootSvgImage rootSvgImage11;
		RootSvgImage rootSvgImage10 = (rootSvgImage11 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage10).BeginInit();
		children21.Add(rootSvgImage10);
		RootSvgImage rootSvgImage12 = (rootSvgImage4 = rootSvgImage11);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage13 = rootSvgImage4;
		rootSvgImage13.Width = 11.0;
		rootSvgImage13.Height = 11.0;
		rootSvgImage13.VerticalAlignment = VerticalAlignment.Center;
		rootSvgImage13.HorizontalAlignment = HorizontalAlignment.Center;
		StyledProperty<string?> svgPathProperty5 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension17 = new DynamicResourceExtension("DMCallSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding17 = dynamicResourceExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage13, svgPathProperty5, binding17);
		context.PopParent();
		((ISupportInitialize)rootSvgImage12).EndInit();
		context.PopParent();
		((ISupportInitialize)panel24).EndInit();
		global::Avalonia.Controls.Controls children22 = stackPanel9.Children;
		TextBlock textBlock23;
		TextBlock textBlock22 = (textBlock23 = new TextBlock());
		((ISupportInitialize)textBlock22).BeginInit();
		children22.Add(textBlock22);
		TextBlock textBlock24 = (textBlock4 = textBlock23);
		context.PushParent(textBlock4);
		TextBlock textBlock25 = textBlock4;
		StaticResourceExtension staticResourceExtension13 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj28 = staticResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock25, obj28);
		textBlock25.FontWeight = (FontWeight)450;
		StyledProperty<IBrush?> foregroundProperty4 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension18 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding18 = dynamicResourceExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock25, foregroundProperty4, binding18);
		textBlock25.FontSize = 14.0;
		textBlock25.VerticalAlignment = VerticalAlignment.Center;
		textBlock25.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.JoinCall;
		context.PopParent();
		((ISupportInitialize)textBlock24).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		context.PopParent();
		((ISupportInitialize)button12).EndInit();
		context.PopParent();
		((ISupportInitialize)panel16).EndInit();
		global::Avalonia.Controls.Controls children23 = grid9.Children;
		RootSvgButton rootSvgButton11;
		RootSvgButton rootSvgButton10 = (rootSvgButton11 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton10).BeginInit();
		children23.Add(rootSvgButton10);
		RootSvgButton rootSvgButton12 = (rootSvgButton4 = rootSvgButton11);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton13 = rootSvgButton4;
		Grid.SetColumn(rootSvgButton13, 4);
		rootSvgButton13.Classes.Add("SvgDimmedButton");
		rootSvgButton13.Width = 32.0;
		rootSvgButton13.Height = 32.0;
		rootSvgButton13.SvgWidth = 21.0;
		rootSvgButton13.SvgHeight = 21.0;
		rootSvgButton13.Margin = new Thickness(0.0, 0.0, 4.0, 0.0);
		StyledProperty<string> svgPathProperty6 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension19 = new DynamicResourceExtension("PopoutSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding19 = dynamicResourceExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton13, svgPathProperty6, binding19);
		StyledProperty<ICommand?> commandProperty6 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension28 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.PopoutCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension29 = compiledBindingExtension28.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton13, commandProperty6, compiledBindingExtension29);
		ToolTip.SetPlacement(rootSvgButton13, PlacementMode.Bottom);
		ToolTip.SetVerticalOffset(rootSvgButton13, 1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton13, 0.0);
		ToolTip.SetShowDelay(rootSvgButton13, 0);
		StyledProperty<bool> isVisibleProperty10 = Visual.IsVisibleProperty;
		MultiBinding multiBinding14 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding15 = multiBinding2;
		StaticResourceExtension staticResourceExtension14 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj29 = staticResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding15.Converter = (IMultiValueConverter)obj29;
		IList<IBinding> bindings17 = multiBinding15.Bindings;
		CompiledBindingExtension obj30 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DirectMessageViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageViewModel,RootApp.Client.Avalonia.DirectMessage!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.DirectMessages.DirectMessage,RootApp.Client.CoreDomain.MediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.HasActiveCall!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item17 = obj30.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings17.Add(item17);
		IList<IBinding> bindings18 = multiBinding15.Bindings;
		CompiledBindingExtension compiledBindingExtension30 = new CompiledBindingExtension();
		compiledBindingExtension30.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DirectMessageViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageViewModel,RootApp.Client.Avalonia.DirectMessage!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.DirectMessages.DirectMessage,RootApp.Client.CoreDomain.MediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension30.Converter = ObjectConverters.IsNotNull;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item18 = compiledBindingExtension30.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings18.Add(item18);
		IList<IBinding> bindings19 = multiBinding15.Bindings;
		CompiledBindingExtension obj31 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DirectMessageViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageViewModel,RootApp.Client.Avalonia.DirectMessage!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.DirectMessages.DirectMessage,RootApp.Client.CoreDomain.MediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.IsPoppedOut!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item19 = obj31.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings19.Add(item19);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgButton13, isVisibleProperty10, multiBinding14);
		RootToolTip rootToolTip11;
		RootToolTip rootToolTip10 = (rootToolTip11 = new RootToolTip());
		((ISupportInitialize)rootToolTip10).BeginInit();
		ToolTip.SetTip(rootSvgButton13, rootToolTip10);
		RootToolTip rootToolTip12 = (rootToolTip4 = rootToolTip11);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip13 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip13, PlacementMode.Bottom);
		TextBlock textBlock27;
		TextBlock textBlock26 = (textBlock27 = new TextBlock());
		((ISupportInitialize)textBlock26).BeginInit();
		rootToolTip13.Content = textBlock26;
		TextBlock textBlock28 = (textBlock4 = textBlock27);
		context.PushParent(textBlock4);
		TextBlock textBlock29 = textBlock4;
		textBlock29.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.OpenInNewWindow;
		StaticResourceExtension staticResourceExtension15 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj32 = staticResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock29, obj32);
		textBlock29.FontWeight = (FontWeight)450;
		textBlock29.FontSize = 14.0;
		textBlock29.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock29.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock28).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip12).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton12).EndInit();
		global::Avalonia.Controls.Controls children24 = grid9.Children;
		RootSvgButton rootSvgButton15;
		RootSvgButton rootSvgButton14 = (rootSvgButton15 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton14).BeginInit();
		children24.Add(rootSvgButton14);
		RootSvgButton rootSvgButton16 = (rootSvgButton4 = rootSvgButton15);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton17 = rootSvgButton4;
		Grid.SetColumn(rootSvgButton17, 6);
		rootSvgButton17.Classes.Add("SvgDimmedButton");
		rootSvgButton17.Width = 32.0;
		rootSvgButton17.Height = 32.0;
		rootSvgButton17.SvgWidth = 4.0;
		rootSvgButton17.SvgHeight = 16.0;
		StyledProperty<string> svgPathProperty7 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension20 = new DynamicResourceExtension("EllipsisVerticalSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding20 = dynamicResourceExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton17, svgPathProperty7, binding20);
		ToolTip.SetPlacement(rootSvgButton17, PlacementMode.Bottom);
		ToolTip.SetVerticalOffset(rootSvgButton17, 1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton17, 0.0);
		ToolTip.SetShowDelay(rootSvgButton17, 0);
		RootToolTip rootToolTip15;
		RootToolTip rootToolTip14 = (rootToolTip15 = new RootToolTip());
		((ISupportInitialize)rootToolTip14).BeginInit();
		ToolTip.SetTip(rootSvgButton17, rootToolTip14);
		RootToolTip rootToolTip16 = (rootToolTip4 = rootToolTip15);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip17 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip17, PlacementMode.Bottom);
		TextBlock textBlock31;
		TextBlock textBlock30 = (textBlock31 = new TextBlock());
		((ISupportInitialize)textBlock30).BeginInit();
		rootToolTip17.Content = textBlock30;
		TextBlock textBlock32 = (textBlock4 = textBlock31);
		context.PushParent(textBlock4);
		TextBlock textBlock33 = textBlock4;
		textBlock33.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Options;
		StaticResourceExtension staticResourceExtension16 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj33 = staticResourceExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock33, obj33);
		textBlock33.FontWeight = (FontWeight)450;
		textBlock33.FontSize = 14.0;
		textBlock33.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock33.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock32).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip16).EndInit();
		RootMenuFlyout rootMenuFlyout;
		RootMenuFlyout flyout = (rootMenuFlyout = new RootMenuFlyout());
		context.PushParent(rootMenuFlyout);
		rootMenuFlyout.Placement = PlacementMode.BottomEdgeAlignedRight;
		rootMenuFlyout.HorizontalOffset = 12.0;
		ItemCollection items = rootMenuFlyout.Items;
		MenuItem menuItem2;
		MenuItem menuItem = (menuItem2 = new MenuItem());
		((ISupportInitialize)menuItem).BeginInit();
		items.Add(menuItem);
		MenuItem menuItem4;
		MenuItem menuItem3 = (menuItem4 = menuItem2);
		context.PushParent(menuItem4);
		MenuItem menuItem5 = menuItem4;
		menuItem5.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.AddFriendToDM;
		StyledProperty<ICommand?> commandProperty7 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension31 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.ShowAddMembersCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension32 = compiledBindingExtension31.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem5, commandProperty7, compiledBindingExtension32);
		context.PopParent();
		((ISupportInitialize)menuItem3).EndInit();
		ItemCollection items2 = rootMenuFlyout.Items;
		MenuItem menuItem7;
		MenuItem menuItem6 = (menuItem7 = new MenuItem());
		((ISupportInitialize)menuItem6).BeginInit();
		items2.Add(menuItem6);
		MenuItem menuItem8 = (menuItem4 = menuItem7);
		context.PushParent(menuItem4);
		MenuItem menuItem9 = menuItem4;
		menuItem9.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.PinToTab;
		StyledProperty<ICommand?> commandProperty8 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension33 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.PinToTabCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension34 = compiledBindingExtension33.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem9, commandProperty8, compiledBindingExtension34);
		StyledProperty<bool> isVisibleProperty11 = Visual.IsVisibleProperty;
		MultiBinding multiBinding16 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding17 = multiBinding2;
		StaticResourceExtension staticResourceExtension17 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj34 = staticResourceExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding17.Converter = (IMultiValueConverter)obj34;
		IList<IBinding> bindings20 = multiBinding17.Bindings;
		CompiledBindingExtension obj35 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.ShowPinToTab!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item20 = obj35.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings20.Add(item20);
		IList<IBinding> bindings21 = multiBinding17.Bindings;
		CompiledBindingExtension obj36 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DirectMessageViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageViewModel,RootApp.Client.Avalonia.IsPinned!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item21 = obj36.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings21.Add(item21);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(menuItem9, isVisibleProperty11, multiBinding16);
		context.PopParent();
		((ISupportInitialize)menuItem8).EndInit();
		ItemCollection items3 = rootMenuFlyout.Items;
		MenuItem menuItem11;
		MenuItem menuItem10 = (menuItem11 = new MenuItem());
		((ISupportInitialize)menuItem10).BeginInit();
		items3.Add(menuItem10);
		MenuItem menuItem12 = (menuItem4 = menuItem11);
		context.PushParent(menuItem4);
		MenuItem menuItem13 = menuItem4;
		menuItem13.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.UnpinFromTab;
		StyledProperty<ICommand?> commandProperty9 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension35 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.PinToTabCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension36 = compiledBindingExtension35.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem13, commandProperty9, compiledBindingExtension36);
		StyledProperty<bool> isVisibleProperty12 = Visual.IsVisibleProperty;
		MultiBinding multiBinding18 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding19 = multiBinding2;
		StaticResourceExtension staticResourceExtension18 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj37 = staticResourceExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding19.Converter = (IMultiValueConverter)obj37;
		IList<IBinding> bindings22 = multiBinding19.Bindings;
		CompiledBindingExtension obj38 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.ShowPinToTab!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item22 = obj38.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings22.Add(item22);
		IList<IBinding> bindings23 = multiBinding19.Bindings;
		CompiledBindingExtension obj39 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DirectMessageViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageViewModel,RootApp.Client.Avalonia.IsPinned!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item23 = obj39.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings23.Add(item23);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(menuItem13, isVisibleProperty12, multiBinding18);
		context.PopParent();
		((ISupportInitialize)menuItem12).EndInit();
		ItemCollection items4 = rootMenuFlyout.Items;
		MenuItem menuItem15;
		MenuItem menuItem14 = (menuItem15 = new MenuItem());
		((ISupportInitialize)menuItem14).BeginInit();
		items4.Add(menuItem14);
		MenuItem menuItem16 = (menuItem4 = menuItem15);
		context.PushParent(menuItem4);
		MenuItem menuItem17 = menuItem4;
		menuItem17.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.ViewDetails;
		StyledProperty<ICommand?> commandProperty10 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension37 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.ShowDetailsCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension38 = compiledBindingExtension37.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem17, commandProperty10, compiledBindingExtension38);
		context.PopParent();
		((ISupportInitialize)menuItem16).EndInit();
		context.PopParent();
		rootSvgButton17.Flyout = flyout;
		context.PopParent();
		((ISupportInitialize)rootSvgButton16).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		global::Avalonia.Controls.Controls children25 = grid5.Children;
		Rectangle rectangle7;
		Rectangle rectangle6 = (rectangle7 = new Rectangle());
		((ISupportInitialize)rectangle6).BeginInit();
		children25.Add(rectangle6);
		Rectangle rectangle8 = (rectangle4 = rectangle7);
		context.PushParent(rectangle4);
		Rectangle rectangle9 = rectangle4;
		Grid.SetRow(rectangle9, 0);
		rectangle9.VerticalAlignment = VerticalAlignment.Bottom;
		rectangle9.Height = 0.5;
		StyledProperty<IBrush?> fillProperty5 = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension21 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding21 = dynamicResourceExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle9, fillProperty5, binding21);
		rectangle9.Margin = new Thickness(-16.0, 0.0, -16.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rectangle8).EndInit();
		global::Avalonia.Controls.Controls children26 = grid5.Children;
		RootMessageScrollViewer rootMessageScrollViewer2;
		RootMessageScrollViewer rootMessageScrollViewer = (rootMessageScrollViewer2 = new RootMessageScrollViewer());
		((ISupportInitialize)rootMessageScrollViewer).BeginInit();
		children26.Add(rootMessageScrollViewer);
		RootMessageScrollViewer rootMessageScrollViewer4;
		RootMessageScrollViewer rootMessageScrollViewer3 = (rootMessageScrollViewer4 = rootMessageScrollViewer2);
		context.PushParent(rootMessageScrollViewer4);
		Grid.SetRow(rootMessageScrollViewer4, 1);
		rootMessageScrollViewer4.Name = "ScrollViewer";
		obj2 = rootMessageScrollViewer4;
		context.AvaloniaNameScope.Register("ScrollViewer", obj2);
		StyledProperty<ICommand> downloadNewerMessagesCommandProperty = RootMessageScrollViewer.DownloadNewerMessagesCommandProperty;
		CompiledBindingExtension compiledBindingExtension39 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DownloadNewerMessagesCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootMessageScrollViewer.DownloadNewerMessagesCommandProperty;
		CompiledBindingExtension compiledBindingExtension40 = compiledBindingExtension39.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMessageScrollViewer4, downloadNewerMessagesCommandProperty, compiledBindingExtension40);
		StyledProperty<ICommand> downloadOlderMessagesCommandProperty = RootMessageScrollViewer.DownloadOlderMessagesCommandProperty;
		CompiledBindingExtension compiledBindingExtension41 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DownloadOlderMessagesCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootMessageScrollViewer.DownloadOlderMessagesCommandProperty;
		CompiledBindingExtension compiledBindingExtension42 = compiledBindingExtension41.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMessageScrollViewer4, downloadOlderMessagesCommandProperty, compiledBindingExtension42);
		StyledProperty<ICommand> setNewMessagesBannerStatusCommandProperty = RootMessageScrollViewer.SetNewMessagesBannerStatusCommandProperty;
		CompiledBindingExtension compiledBindingExtension43 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.SetNewMessagesBannerStatusCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootMessageScrollViewer.SetNewMessagesBannerStatusCommandProperty;
		CompiledBindingExtension compiledBindingExtension44 = compiledBindingExtension43.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMessageScrollViewer4, setNewMessagesBannerStatusCommandProperty, compiledBindingExtension44);
		StyledProperty<ICommand> messagesBeganRenderingCommandProperty = RootMessageScrollViewer.MessagesBeganRenderingCommandProperty;
		CompiledBindingExtension compiledBindingExtension45 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.MessagesBeganRenderingCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootMessageScrollViewer.MessagesBeganRenderingCommandProperty;
		CompiledBindingExtension compiledBindingExtension46 = compiledBindingExtension45.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMessageScrollViewer4, messagesBeganRenderingCommandProperty, compiledBindingExtension46);
		StyledProperty<ICommand> setAutoScrollStatusCommandProperty = RootMessageScrollViewer.SetAutoScrollStatusCommandProperty;
		CompiledBindingExtension compiledBindingExtension47 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.SetAutoScrollStatusCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootMessageScrollViewer.SetAutoScrollStatusCommandProperty;
		CompiledBindingExtension compiledBindingExtension48 = compiledBindingExtension47.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMessageScrollViewer4, setAutoScrollStatusCommandProperty, compiledBindingExtension48);
		StyledProperty<ICommand> setShowJumpToPresentCommandProperty = RootMessageScrollViewer.SetShowJumpToPresentCommandProperty;
		CompiledBindingExtension compiledBindingExtension49 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.SetShowJumpToPresentCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootMessageScrollViewer.SetShowJumpToPresentCommandProperty;
		CompiledBindingExtension compiledBindingExtension50 = compiledBindingExtension49.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMessageScrollViewer4, setShowJumpToPresentCommandProperty, compiledBindingExtension50);
		rootMessageScrollViewer4.Margin = new Thickness(-16.0, 0.0, -16.0, 0.0);
		RootMessageItemsControl rootMessageItemsControl2;
		RootMessageItemsControl rootMessageItemsControl = (rootMessageItemsControl2 = new RootMessageItemsControl());
		((ISupportInitialize)rootMessageItemsControl).BeginInit();
		rootMessageScrollViewer4.Content = rootMessageItemsControl;
		RootMessageItemsControl rootMessageItemsControl4;
		RootMessageItemsControl rootMessageItemsControl3 = (rootMessageItemsControl4 = rootMessageItemsControl2);
		context.PushParent(rootMessageItemsControl4);
		StyledProperty<IEnumerable?> itemsSourceProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension51 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.Messages!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension52 = compiledBindingExtension51.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMessageItemsControl4, itemsSourceProperty, compiledBindingExtension52);
		rootMessageItemsControl4.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_136.Build_3), context)
		};
		context.PopParent();
		((ISupportInitialize)rootMessageItemsControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootMessageScrollViewer3).EndInit();
		global::Avalonia.Controls.Controls children27 = grid5.Children;
		Border border7;
		Border border6 = (border7 = new Border());
		((ISupportInitialize)border6).BeginInit();
		children27.Add(border6);
		Border border8 = (border4 = border7);
		context.PushParent(border4);
		Border border9 = border4;
		border9.Name = "MessageBlockerBorder";
		obj2 = border9;
		context.AvaloniaNameScope.Register("MessageBlockerBorder", obj2);
		Grid.SetRow(border9, 1);
		StyledProperty<IBrush?> backgroundProperty3 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension22 = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding22 = dynamicResourceExtension22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border9, backgroundProperty3, binding22);
		context.PopParent();
		((ISupportInitialize)border8).EndInit();
		global::Avalonia.Controls.Controls children28 = grid5.Children;
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		children28.Add(rootBorder);
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		RootBorder rootBorder5 = rootBorder4;
		rootBorder5.Name = "AutoCompleteBorder";
		obj2 = rootBorder5;
		context.AvaloniaNameScope.Register("AutoCompleteBorder", obj2);
		Grid.SetRow(rootBorder5, 1);
		StyledProperty<IBrush?> backgroundProperty4 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension23 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding23 = dynamicResourceExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, backgroundProperty4, binding23);
		StyledProperty<IBrush?> borderBrushProperty5 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension24 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding24 = dynamicResourceExtension24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, borderBrushProperty5, binding24);
		rootBorder5.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder5.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<BoxShadows> boxShadowProperty = Border.BoxShadowProperty;
		DynamicResourceExtension dynamicResourceExtension25 = new DynamicResourceExtension("PopupBoxShadow");
		context.ProvideTargetProperty = Border.BoxShadowProperty;
		IBinding binding25 = dynamicResourceExtension25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, boxShadowProperty, binding25);
		rootBorder5.Margin = new Thickness(0.0, 0.0, 0.0, 8.0);
		rootBorder5.VerticalAlignment = VerticalAlignment.Bottom;
		StyledProperty<bool> isVisibleProperty13 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension53 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.RootMessageTextboxViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.Controls.Messaging.RootMessageTextboxViewModel,RootApp.Client.Avalonia.AutoCompleteItems!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.System.Collections.ObjectModel.Collection`1,System.Runtime.Count_74!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension54 = compiledBindingExtension53.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, isVisibleProperty13, compiledBindingExtension54);
		RootScrollViewer rootScrollViewer2;
		RootScrollViewer rootScrollViewer = (rootScrollViewer2 = new RootScrollViewer());
		((ISupportInitialize)rootScrollViewer).BeginInit();
		rootBorder5.Child = rootScrollViewer;
		RootScrollViewer rootScrollViewer4;
		RootScrollViewer rootScrollViewer3 = (rootScrollViewer4 = rootScrollViewer2);
		context.PushParent(rootScrollViewer4);
		ItemsControl itemsControl2;
		ItemsControl itemsControl = (itemsControl2 = new ItemsControl());
		((ISupportInitialize)itemsControl).BeginInit();
		rootScrollViewer4.Content = itemsControl;
		ItemsControl itemsControl4;
		ItemsControl itemsControl3 = (itemsControl4 = itemsControl2);
		context.PushParent(itemsControl4);
		ItemsControl itemsControl5 = itemsControl4;
		StyledProperty<IEnumerable?> itemsSourceProperty2 = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension55 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.RootMessageTextboxViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.Controls.Messaging.RootMessageTextboxViewModel,RootApp.Client.Avalonia.AutoCompleteItems!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension56 = compiledBindingExtension55.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl5, itemsSourceProperty2, compiledBindingExtension56);
		itemsControl5.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_136.Build_4), context)
		};
		context.PopParent();
		((ISupportInitialize)itemsControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootScrollViewer3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		global::Avalonia.Controls.Controls children29 = grid5.Children;
		Border border11;
		Border border10 = (border11 = new Border());
		((ISupportInitialize)border10).BeginInit();
		children29.Add(border10);
		Border border12 = (border4 = border11);
		context.PushParent(border4);
		Border border13 = border4;
		Grid.SetRow(border13, 1);
		border13.Height = 56.0;
		border13.VerticalAlignment = VerticalAlignment.Top;
		StyledProperty<IBrush?> backgroundProperty5 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension26 = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding26 = dynamicResourceExtension26.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border13, backgroundProperty5, binding26);
		StyledProperty<bool> isVisibleProperty14 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension57 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.ShowNewMessagesBanner!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension58 = compiledBindingExtension57.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border13, isVisibleProperty14, compiledBindingExtension58);
		border13.Margin = new Thickness(-16.0, 0.0, -16.0, 0.0);
		Grid grid11;
		Grid grid10 = (grid11 = new Grid());
		((ISupportInitialize)grid10).BeginInit();
		border13.Child = grid10;
		Grid grid12 = (grid4 = grid11);
		context.PushParent(grid4);
		Grid grid13 = grid4;
		grid13.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid13.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		global::Avalonia.Controls.Controls children30 = grid13.Children;
		StackPanel stackPanel11;
		StackPanel stackPanel10 = (stackPanel11 = new StackPanel());
		((ISupportInitialize)stackPanel10).BeginInit();
		children30.Add(stackPanel10);
		StackPanel stackPanel12 = (stackPanel4 = stackPanel11);
		context.PushParent(stackPanel4);
		StackPanel stackPanel13 = stackPanel4;
		stackPanel13.Margin = new Thickness(20.0, 0.0, 0.0, 0.0);
		stackPanel13.VerticalAlignment = VerticalAlignment.Center;
		global::Avalonia.Controls.Controls children31 = stackPanel13.Children;
		TextBlock textBlock35;
		TextBlock textBlock34 = (textBlock35 = new TextBlock());
		((ISupportInitialize)textBlock34).BeginInit();
		children31.Add(textBlock34);
		TextBlock textBlock36 = (textBlock4 = textBlock35);
		context.PushParent(textBlock4);
		TextBlock textBlock37 = textBlock4;
		textBlock37.Name = "NewMessagesTextBlock";
		obj2 = textBlock37;
		context.AvaloniaNameScope.Register("NewMessagesTextBlock", obj2);
		textBlock37.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty5 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension27 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding27 = dynamicResourceExtension27.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock37, foregroundProperty5, binding27);
		textBlock37.FontSize = 14.0;
		StaticResourceExtension staticResourceExtension19 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj40 = staticResourceExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock37, obj40);
		textBlock37.FontWeight = FontWeight.Bold;
		StyledProperty<string?> textProperty3 = TextBlock.TextProperty;
		CompiledBindingExtension obj41 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DirectMessageViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageViewModel,RootApp.Client.Avalonia.DirectMessage!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.DirectMessages.DirectMessage,RootApp.Client.CoreDomain.Messages!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IMessageService,RootApp.Client.CoreDomain.NewMessagesCount!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			StringFormat = RootApp.Client.Avalonia.Resources.Strings.Resources.NewMessages
		};
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension59 = obj41.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock37, textProperty3, compiledBindingExtension59);
		context.PopParent();
		((ISupportInitialize)textBlock36).EndInit();
		global::Avalonia.Controls.Controls children32 = stackPanel13.Children;
		TextBlock textBlock39;
		TextBlock textBlock38 = (textBlock39 = new TextBlock());
		((ISupportInitialize)textBlock38).BeginInit();
		children32.Add(textBlock38);
		TextBlock textBlock40 = (textBlock4 = textBlock39);
		context.PushParent(textBlock4);
		TextBlock textBlock41 = textBlock4;
		textBlock41.Name = "NewMessagesDateTextBlock";
		obj2 = textBlock41;
		context.AvaloniaNameScope.Register("NewMessagesDateTextBlock", obj2);
		textBlock41.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock41.Margin = new Thickness(0.0, 5.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty6 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension28 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding28 = dynamicResourceExtension28.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock41, foregroundProperty6, binding28);
		textBlock41.Opacity = 0.64;
		textBlock41.FontSize = 13.0;
		StaticResourceExtension staticResourceExtension20 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj42 = staticResourceExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock41, obj42);
		textBlock41.FontWeight = (FontWeight)450;
		StyledProperty<string?> textProperty4 = TextBlock.TextProperty;
		CompiledBindingExtension obj43 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DirectMessageViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageViewModel,RootApp.Client.Avalonia.DirectMessage!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.DirectMessages.DirectMessage,RootApp.Client.CoreDomain.LastViewedAtLongString!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			StringFormat = RootApp.Client.Avalonia.Resources.Strings.Resources.SinceDate
		};
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension60 = obj43.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock41, textProperty4, compiledBindingExtension60);
		context.PopParent();
		((ISupportInitialize)textBlock40).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel12).EndInit();
		global::Avalonia.Controls.Controls children33 = grid13.Children;
		ThemeVariantScope themeVariantScope11;
		ThemeVariantScope themeVariantScope10 = (themeVariantScope11 = new ThemeVariantScope());
		((ISupportInitialize)themeVariantScope10).BeginInit();
		children33.Add(themeVariantScope10);
		ThemeVariantScope themeVariantScope12 = (themeVariantScope4 = themeVariantScope11);
		context.PushParent(themeVariantScope4);
		ThemeVariantScope themeVariantScope13 = themeVariantScope4;
		Grid.SetColumn(themeVariantScope13, 1);
		themeVariantScope13.RequestedThemeVariant = ThemeVariant.Dark;
		Button button15;
		Button button14 = (button15 = new Button());
		((ISupportInitialize)button14).BeginInit();
		themeVariantScope13.Child = button14;
		Button button16 = (button4 = button15);
		context.PushParent(button4);
		Button button17 = button4;
		button17.Classes.Add("BasicButton");
		button17.Margin = new Thickness(0.0, 0.0, 20.0, 0.0);
		button17.Height = 36.0;
		button17.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		button17.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> backgroundProperty6 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension29 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding29 = dynamicResourceExtension29.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button17, backgroundProperty6, binding29);
		StyledProperty<ICommand?> commandProperty11 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension61 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.MarkAsReadCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension62 = compiledBindingExtension61.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button17, commandProperty11, compiledBindingExtension62);
		StackPanel stackPanel15;
		StackPanel stackPanel14 = (stackPanel15 = new StackPanel());
		((ISupportInitialize)stackPanel14).BeginInit();
		button17.Content = stackPanel14;
		StackPanel stackPanel16 = (stackPanel4 = stackPanel15);
		context.PushParent(stackPanel4);
		StackPanel stackPanel17 = stackPanel4;
		stackPanel17.Orientation = Orientation.Horizontal;
		stackPanel17.Margin = new Thickness(12.0, 0.0, 12.0, 0.0);
		global::Avalonia.Controls.Controls children34 = stackPanel17.Children;
		TextBlock textBlock43;
		TextBlock textBlock42 = (textBlock43 = new TextBlock());
		((ISupportInitialize)textBlock42).BeginInit();
		children34.Add(textBlock42);
		TextBlock textBlock44 = (textBlock4 = textBlock43);
		context.PushParent(textBlock4);
		TextBlock textBlock45 = textBlock4;
		textBlock45.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock45.Margin = new Thickness(0.0, 0.0, 12.0, 0.0);
		textBlock45.VerticalAlignment = VerticalAlignment.Center;
		textBlock45.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.MarkAsRead;
		StyledProperty<IBrush?> foregroundProperty7 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension30 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding30 = dynamicResourceExtension30.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock45, foregroundProperty7, binding30);
		textBlock45.FontSize = 14.0;
		StaticResourceExtension staticResourceExtension21 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj44 = staticResourceExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock45, obj44);
		textBlock45.FontWeight = (FontWeight)450;
		context.PopParent();
		((ISupportInitialize)textBlock44).EndInit();
		global::Avalonia.Controls.Controls children35 = stackPanel17.Children;
		RootSvgImage rootSvgImage15;
		RootSvgImage rootSvgImage14 = (rootSvgImage15 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage14).BeginInit();
		children35.Add(rootSvgImage14);
		RootSvgImage rootSvgImage16 = (rootSvgImage4 = rootSvgImage15);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage17 = rootSvgImage4;
		rootSvgImage17.Width = 14.0;
		rootSvgImage17.Height = 11.0;
		rootSvgImage17.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<string?> svgPathProperty8 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension31 = new DynamicResourceExtension("MarkAsReadCheckmarkSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding31 = dynamicResourceExtension31.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage17, svgPathProperty8, binding31);
		context.PopParent();
		((ISupportInitialize)rootSvgImage16).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel16).EndInit();
		context.PopParent();
		((ISupportInitialize)button16).EndInit();
		context.PopParent();
		((ISupportInitialize)themeVariantScope12).EndInit();
		context.PopParent();
		((ISupportInitialize)grid12).EndInit();
		context.PopParent();
		((ISupportInitialize)border12).EndInit();
		global::Avalonia.Controls.Controls children36 = grid5.Children;
		RootBorder rootBorder7;
		RootBorder rootBorder6 = (rootBorder7 = new RootBorder());
		((ISupportInitialize)rootBorder6).BeginInit();
		children36.Add(rootBorder6);
		RootBorder rootBorder8 = (rootBorder4 = rootBorder7);
		context.PushParent(rootBorder4);
		RootBorder rootBorder9 = rootBorder4;
		Grid.SetRow(rootBorder9, 1);
		rootBorder9.HorizontalAlignment = HorizontalAlignment.Center;
		rootBorder9.VerticalAlignment = VerticalAlignment.Bottom;
		rootBorder9.Margin = new Thickness(0.0, 0.0, 0.0, 16.0);
		StyledProperty<IBrush?> backgroundProperty7 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension32 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding32 = dynamicResourceExtension32.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, backgroundProperty7, binding32);
		StyledProperty<IBrush?> borderBrushProperty6 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension33 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding33 = dynamicResourceExtension33.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, borderBrushProperty6, binding33);
		rootBorder9.DynamicBorderThickness = new Thickness(1.0, 1.0, 1.0, 1.0);
		rootBorder9.CornerRadius = new CornerRadius(20.0, 20.0, 20.0, 20.0);
		rootBorder9.Padding = new Thickness(6.0, 6.0, 6.0, 6.0);
		rootBorder9.BoxShadow = BoxShadows.Parse("0 4 16 0 #60000000");
		StyledProperty<bool> isVisibleProperty15 = Visual.IsVisibleProperty;
		MultiBinding multiBinding20 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding21 = multiBinding2;
		multiBinding21.Converter = BoolConverters.Or;
		IList<IBinding> bindings24 = multiBinding21.Bindings;
		CompiledBindingExtension obj45 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.ShowJumpToPresent!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item24 = obj45.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings24.Add(item24);
		IList<IBinding> bindings25 = multiBinding21.Bindings;
		CompiledBindingExtension obj46 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.DirectMessageViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageViewModel,RootApp.Client.Avalonia.DirectMessage!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.DirectMessages.DirectMessage,RootApp.Client.CoreDomain.Messages!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IMessageService,RootApp.Client.CoreDomain.InFocusMode!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item25 = obj46.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings25.Add(item25);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootBorder9, isVisibleProperty15, multiBinding20);
		StackPanel stackPanel19;
		StackPanel stackPanel18 = (stackPanel19 = new StackPanel());
		((ISupportInitialize)stackPanel18).BeginInit();
		rootBorder9.Child = stackPanel18;
		StackPanel stackPanel20 = (stackPanel4 = stackPanel19);
		context.PushParent(stackPanel4);
		StackPanel stackPanel21 = stackPanel4;
		stackPanel21.Orientation = Orientation.Horizontal;
		global::Avalonia.Controls.Controls children37 = stackPanel21.Children;
		TextBlock textBlock47;
		TextBlock textBlock46 = (textBlock47 = new TextBlock());
		((ISupportInitialize)textBlock46).BeginInit();
		children37.Add(textBlock46);
		TextBlock textBlock48 = (textBlock4 = textBlock47);
		context.PushParent(textBlock4);
		TextBlock textBlock49 = textBlock4;
		textBlock49.Padding = new Thickness(16.0, 8.0, 16.0, 8.0);
		textBlock49.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<IBrush?> foregroundProperty8 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension34 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding34 = dynamicResourceExtension34.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock49, foregroundProperty8, binding34);
		textBlock49.FontSize = 13.0;
		StaticResourceExtension staticResourceExtension22 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj47 = staticResourceExtension22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock49, obj47);
		textBlock49.FontWeight = (FontWeight)450;
		textBlock49.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ViewingOlderMessages;
		context.PopParent();
		((ISupportInitialize)textBlock48).EndInit();
		global::Avalonia.Controls.Controls children38 = stackPanel21.Children;
		Button button19;
		Button button18 = (button19 = new Button());
		((ISupportInitialize)button18).BeginInit();
		children38.Add(button18);
		Button button20 = (button4 = button19);
		context.PushParent(button4);
		Button button21 = button4;
		button21.Classes.Add("TransparentButtonWithClickEffect");
		StyledProperty<IBrush?> backgroundProperty8 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension35 = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding35 = dynamicResourceExtension35.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button21, backgroundProperty8, binding35);
		button21.CornerRadius = new CornerRadius(14.0, 14.0, 14.0, 14.0);
		button21.Padding = new Thickness(16.0, 8.0, 16.0, 8.0);
		button21.Cursor = new Cursor(StandardCursorType.Hand);
		StyledProperty<ICommand?> commandProperty12 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension63 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.MarkAsReadCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension64 = compiledBindingExtension63.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button21, commandProperty12, compiledBindingExtension64);
		TextBlock textBlock51;
		TextBlock textBlock50 = (textBlock51 = new TextBlock());
		((ISupportInitialize)textBlock50).BeginInit();
		button21.Content = textBlock50;
		TextBlock textBlock52 = (textBlock4 = textBlock51);
		context.PushParent(textBlock4);
		TextBlock textBlock53 = textBlock4;
		StyledProperty<IBrush?> foregroundProperty9 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension36 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding36 = dynamicResourceExtension36.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock53, foregroundProperty9, binding36);
		textBlock53.FontSize = 13.0;
		StaticResourceExtension staticResourceExtension23 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj48 = staticResourceExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock53, obj48);
		textBlock53.FontWeight = FontWeight.Bold;
		textBlock53.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.JumpToPresent;
		context.PopParent();
		((ISupportInitialize)textBlock52).EndInit();
		context.PopParent();
		((ISupportInitialize)button20).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel20).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder8).EndInit();
		global::Avalonia.Controls.Controls children39 = grid5.Children;
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		children39.Add(contentControl);
		ContentControl contentControl4;
		ContentControl contentControl3 = (contentControl4 = contentControl2);
		context.PushParent(contentControl4);
		ContentControl contentControl5 = contentControl4;
		Grid.SetRow(contentControl5, 2);
		CompiledBindingExtension compiledBindingExtension65 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.RootMessageTextboxViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension66 = compiledBindingExtension65.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl5, compiledBindingExtension66);
		context.PopParent();
		((ISupportInitialize)contentControl3).EndInit();
		global::Avalonia.Controls.Controls children40 = grid5.Children;
		ItemsControl itemsControl7;
		ItemsControl itemsControl6 = (itemsControl7 = new ItemsControl());
		((ISupportInitialize)itemsControl6).BeginInit();
		children40.Add(itemsControl6);
		ItemsControl itemsControl8 = (itemsControl4 = itemsControl7);
		context.PushParent(itemsControl4);
		ItemsControl itemsControl9 = itemsControl4;
		Grid.SetRow(itemsControl9, 4);
		StyledProperty<IEnumerable?> itemsSourceProperty3 = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension67 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.TypingUsers!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension68 = compiledBindingExtension67.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl9, itemsSourceProperty3, compiledBindingExtension68);
		itemsControl9.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_136.Build_5), context)
		};
		context.PopParent();
		((ISupportInitialize)itemsControl8).EndInit();
		global::Avalonia.Controls.Controls children41 = grid5.Children;
		ContentControl contentControl7;
		ContentControl contentControl6 = (contentControl7 = new ContentControl());
		((ISupportInitialize)contentControl6).BeginInit();
		children41.Add(contentControl6);
		ContentControl contentControl8 = (contentControl4 = contentControl7);
		context.PushParent(contentControl4);
		ContentControl contentControl9 = contentControl4;
		contentControl9.Name = "CallContent";
		obj2 = contentControl9;
		context.AvaloniaNameScope.Register("CallContent", obj2);
		Grid.SetRow(contentControl9, 1);
		Grid.SetRowSpan(contentControl9, 5);
		contentControl9.Margin = new Thickness(-16.0, 0.0, -16.0, 0.0);
		CompiledBindingExtension compiledBindingExtension69 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.CallContentViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension70 = compiledBindingExtension69.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl9, compiledBindingExtension70);
		StyledProperty<bool> isVisibleProperty16 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension71 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.FocusedOnCall!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension72 = compiledBindingExtension71.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(contentControl9, isVisibleProperty16, compiledBindingExtension72);
		context.PopParent();
		((ISupportInitialize)contentControl8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		global::Avalonia.Controls.Controls children42 = panel9.Children;
		ContentControl contentControl11;
		ContentControl contentControl10 = (contentControl11 = new ContentControl());
		((ISupportInitialize)contentControl10).BeginInit();
		children42.Add(contentControl10);
		ContentControl contentControl12 = (contentControl4 = contentControl11);
		context.PushParent(contentControl4);
		ContentControl contentControl13 = contentControl4;
		contentControl13.Name = "DetailsContent";
		obj2 = contentControl13;
		context.AvaloniaNameScope.Register("DetailsContent", obj2);
		CompiledBindingExtension compiledBindingExtension73 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.SecondaryContentViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension74 = compiledBindingExtension73.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl13, compiledBindingExtension74);
		StyledProperty<bool> isVisibleProperty17 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension75 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.SystemTray.DirectMessages.DirectMessageContentViewModel,RootApp.Client.Avalonia.SecondaryContentViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension76 = compiledBindingExtension75.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(contentControl13, isVisibleProperty17, compiledBindingExtension76);
		context.PopParent();
		((ISupportInitialize)contentControl12).EndInit();
		context.PopParent();
		((ISupportInitialize)panel8).EndInit();
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
	private static void !XamlIlPopulateTrampoline(DirectMessageContentView P_0)
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
