using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Styling;
using Avalonia.Threading;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Resources.Converters.VoiceBar;
using RootApp.Client.Avalonia.Resources.Strings;

namespace RootApp.Client.Avalonia.UI.Home.VoiceBar;

public class VoiceBarView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_170
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<VoiceBarView> context = CreateContext(P_0);
			return new VoiceBarConnectionSvgConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<VoiceBarView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<VoiceBarView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<VoiceBarView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Home/VoiceBar/VoiceBarView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Home/VoiceBar/VoiceBarView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (VoiceBarView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<VoiceBarView> context = CreateContext(P_0);
			return new VoiceBarConnectionForegroundConverter();
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<VoiceBarView> context = CreateContext(P_0);
			return new VoiceBarConnectionTextConverter();
		}

		public static object Build_4(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<VoiceBarView> context = CreateContext(P_0);
			return new VoiceBarWebcamSvgConverter();
		}

		public static object Build_5(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<VoiceBarView> context = CreateContext(P_0);
			return new VoiceBarScreenshareSvgConverter();
		}

		public static object Build_6(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<VoiceBarView> context = CreateContext(P_0);
			return new VoiceBarSelectionBrushConverter();
		}

		public static object Build_7(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<VoiceBarView> context = CreateContext(P_0);
			context.IntermediateRoot = new Border();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			AvaloniaObjectExtensions.Bind((AvaloniaObject)obj, Border.BorderBrushProperty, new TemplateBinding(TemplatedControl.BorderBrushProperty).ProvideValue());
			AvaloniaObjectExtensions.Bind((AvaloniaObject)obj, Border.CornerRadiusProperty, new TemplateBinding(TemplatedControl.CornerRadiusProperty).ProvideValue());
			AvaloniaObjectExtensions.Bind((AvaloniaObject)obj, Border.BorderThicknessProperty, new TemplateBinding(TemplatedControl.BorderThicknessProperty).ProvideValue());
			AvaloniaObjectExtensions.Bind((AvaloniaObject)obj, Border.BackgroundProperty, new TemplateBinding(TemplatedControl.BackgroundProperty).ProvideValue());
			RootSvgImage rootSvgImage2;
			RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
			((ISupportInitialize)rootSvgImage).BeginInit();
			((Decorator)obj).Child = rootSvgImage;
			AvaloniaObjectExtensions.Bind(rootSvgImage2, Layoutable.WidthProperty, new TemplateBinding(RootSvgButton.SvgWidthProperty).ProvideValue());
			AvaloniaObjectExtensions.Bind(rootSvgImage2, Layoutable.HeightProperty, new TemplateBinding(RootSvgButton.SvgHeightProperty).ProvideValue());
			AvaloniaObjectExtensions.Bind(rootSvgImage2, RootSvgImage.SvgPathProperty, new TemplateBinding(RootSvgButton.SvgPathProperty).ProvideValue());
			AvaloniaObjectExtensions.Bind(rootSvgImage2, Visual.OpacityProperty, new TemplateBinding(RootSvgButton.SvgOpacityProperty).ProvideValue());
			rootSvgImage2.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			rootSvgImage2.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			((ISupportInitialize)rootSvgImage2).EndInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}
	}

	private int _currentLevel = 0;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal StackPanel RightControls;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal StackPanel MediaButtonsPanel;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton ScreenshareBlockedButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton ScreenshareButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton WebcamBlockedButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton WebcamButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Rectangle Separator1;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Grid MuteGrid;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton MicrophoneImage;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton MicrophoneMutedImage;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton MicrophoneBlockedImage;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Grid DeafenGrid;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton HeadphonesImage;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton HeadphonesMutedImage;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton HeadphonesBlockedImage;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton SettingsButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Rectangle Separator2;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Button DisconnectButton;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock DisconnectText;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock NameTextBlock;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Button ConnectionBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal StackPanel ConnectionButtonContent;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgImage ConnectionImage;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock ConnectionTextBlock;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Rectangle ConnectionSeparator1;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgImage VoiceImage;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock ChannelNameTextBlock;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal StackPanel MediaStreamingContent;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgButton PopoutButton;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	private VoiceBarViewModel _voiceBarViewModel => (VoiceBarViewModel)base.DataContext;

	public VoiceBarView()
	{
		InitializeComponent();
	}

	protected override void OnSizeChanged(SizeChangedEventArgs P_0)
	{
		base.OnSizeChanged(P_0);
		Dispatcher.UIThread.Post(CheckForCollision, DispatcherPriority.Render);
	}

	private void CheckForCollision()
	{
		if (!base.IsLoaded || base.Bounds.Width <= 0.0)
		{
			return;
		}
		double availableWidth = base.Bounds.Width - 32.0;
		double width = RightControls.Bounds.Width;
		double leftContentWidth = GetLeftContentWidth();
		double num = leftContentWidth + width + 16.0;
		if (num > availableWidth)
		{
			if (_currentLevel < 6)
			{
				_currentLevel++;
				ApplyResponsiveLevel(_currentLevel);
				Dispatcher.UIThread.Post(CheckForCollision, DispatcherPriority.Render);
			}
		}
		else
		{
			if (_currentLevel <= 0)
			{
				return;
			}
			int previousLevel = _currentLevel;
			_currentLevel--;
			ApplyResponsiveLevel(_currentLevel);
			Dispatcher.UIThread.Post(delegate
			{
				double width2 = RightControls.Bounds.Width;
				double leftContentWidth2 = GetLeftContentWidth();
				double num2 = leftContentWidth2 + width2 + 16.0;
				if (num2 > availableWidth)
				{
					_currentLevel = previousLevel;
					ApplyResponsiveLevel(_currentLevel);
				}
			}, DispatcherPriority.Render);
		}
	}

	private double GetLeftContentWidth()
	{
		double num = 32.0;
		if (NameTextBlock.IsVisible && NameTextBlock.Bounds.Width > 0.0)
		{
			num += 12.0 + NameTextBlock.Bounds.Width;
		}
		if (ConnectionBorder.IsVisible && ConnectionBorder.Bounds.Width > 0.0)
		{
			num += 20.0 + ConnectionBorder.Bounds.Width;
		}
		if (PopoutButton.IsVisible && PopoutButton.Bounds.Width > 0.0)
		{
			num += 12.0 + PopoutButton.Bounds.Width;
		}
		return num;
	}

	private void ApplyResponsiveLevel(int P_0)
	{
		bool flag = P_0 < 1;
		DisconnectText.IsVisible = flag;
		DisconnectButton.Width = (flag ? 124 : 48);
		bool flag2 = P_0 < 2;
		ChannelNameTextBlock.IsVisible = flag2;
		VoiceImage.IsVisible = flag2;
		ConnectionSeparator1.IsVisible = flag2;
		ConnectionTextBlock.Margin = (flag2 ? new Thickness(0.0, 0.0, 12.0, 0.0) : new Thickness(0.0));
		bool isVisible = P_0 < 3;
		NameTextBlock.IsVisible = isVisible;
		SettingsButton.IsVisible = isVisible;
		PopoutButton.IsVisible = isVisible;
		Separator2.IsVisible = isVisible;
		bool isVisible2 = P_0 < 4;
		MediaButtonsPanel.IsVisible = isVisible2;
		bool flag3 = P_0 < 5;
		ConnectionTextBlock.IsVisible = flag3;
		ConnectionImage.Margin = (flag3 ? new Thickness(0.0, 0.0, 10.0, 0.0) : new Thickness(0.0));
		bool isVisible3 = P_0 < 6;
		ConnectionBorder.IsVisible = isVisible3;
	}

	private void onScreensharePickerOpening(object? sender, EventArgs e)
	{
		if (_voiceBarViewModel.ScreenshareOpeningCommand.CanExecute(null))
		{
			_voiceBarViewModel.ScreenshareOpeningCommand.Execute(null);
		}
	}

	private void onScreensharePickerClosed(object? sender, EventArgs e)
	{
		if (_voiceBarViewModel.ScreenshareClosingCommand.CanExecute(false))
		{
			_voiceBarViewModel.ScreenshareClosingCommand.Execute(false);
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
		RightControls = nameScope?.Find<StackPanel>("RightControls");
		MediaButtonsPanel = nameScope?.Find<StackPanel>("MediaButtonsPanel");
		ScreenshareBlockedButton = nameScope?.Find<RootSvgButton>("ScreenshareBlockedButton");
		ScreenshareButton = nameScope?.Find<RootSvgButton>("ScreenshareButton");
		WebcamBlockedButton = nameScope?.Find<RootSvgButton>("WebcamBlockedButton");
		WebcamButton = nameScope?.Find<RootSvgButton>("WebcamButton");
		Separator1 = nameScope?.Find<Rectangle>("Separator1");
		MuteGrid = nameScope?.Find<Grid>("MuteGrid");
		MicrophoneImage = nameScope?.Find<RootSvgButton>("MicrophoneImage");
		MicrophoneMutedImage = nameScope?.Find<RootSvgButton>("MicrophoneMutedImage");
		MicrophoneBlockedImage = nameScope?.Find<RootSvgButton>("MicrophoneBlockedImage");
		DeafenGrid = nameScope?.Find<Grid>("DeafenGrid");
		HeadphonesImage = nameScope?.Find<RootSvgButton>("HeadphonesImage");
		HeadphonesMutedImage = nameScope?.Find<RootSvgButton>("HeadphonesMutedImage");
		HeadphonesBlockedImage = nameScope?.Find<RootSvgButton>("HeadphonesBlockedImage");
		SettingsButton = nameScope?.Find<RootSvgButton>("SettingsButton");
		Separator2 = nameScope?.Find<Rectangle>("Separator2");
		DisconnectButton = nameScope?.Find<Button>("DisconnectButton");
		DisconnectText = nameScope?.Find<TextBlock>("DisconnectText");
		NameTextBlock = nameScope?.Find<TextBlock>("NameTextBlock");
		ConnectionBorder = nameScope?.Find<Button>("ConnectionBorder");
		ConnectionButtonContent = nameScope?.Find<StackPanel>("ConnectionButtonContent");
		ConnectionImage = nameScope?.Find<RootSvgImage>("ConnectionImage");
		ConnectionTextBlock = nameScope?.Find<TextBlock>("ConnectionTextBlock");
		ConnectionSeparator1 = nameScope?.Find<Rectangle>("ConnectionSeparator1");
		VoiceImage = nameScope?.Find<RootSvgImage>("VoiceImage");
		ChannelNameTextBlock = nameScope?.Find<TextBlock>("ChannelNameTextBlock");
		MediaStreamingContent = nameScope?.Find<StackPanel>("MediaStreamingContent");
		PopoutButton = nameScope?.Find<RootSvgButton>("PopoutButton");
	}

	[CompilerGenerated]
	private unsafe static void !XamlIlPopulate(IServiceProvider P_0, VoiceBarView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<VoiceBarView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<VoiceBarView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Home/VoiceBar/VoiceBarView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Home/VoiceBar/VoiceBarView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.Height = 56.0;
		ResourceDictionary resourceDictionary = new ResourceDictionary();
		if (resourceDictionary is ResourceDictionary resourceDictionary2)
		{
			resourceDictionary2.EnsureCapacity(resourceDictionary2.Count + 6);
		}
		resourceDictionary.AddDeferred("VoiceBarConnectionSvgConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_170.Build_1), context));
		resourceDictionary.AddDeferred("VoiceBarConnectionForegroundConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_170.Build_2), context));
		resourceDictionary.AddDeferred("VoiceBarConnectionTextConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_170.Build_3), context));
		resourceDictionary.AddDeferred("VoiceBarWebcamSvgConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_170.Build_4), context));
		resourceDictionary.AddDeferred("VoiceBarScreenshareSvgConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_170.Build_5), context));
		resourceDictionary.AddDeferred("VoiceBarSelectionBrushConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_170.Build_6), context));
		P_1.Resources = resourceDictionary;
		Styles styles = P_1.Styles;
		Style style = new Style();
		style.Selector = ((Selector?)null).OfType(typeof(RootSvgButton)).Class("MediaSvgButton");
		Setter setter = new Setter();
		setter.Property = TemplatedControl.BorderThicknessProperty;
		setter.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
		style.Add(setter);
		Setter setter2 = new Setter();
		setter2.Property = Visual.OpacityProperty;
		setter2.Value = 1.0;
		style.Add(setter2);
		Setter setter3 = new Setter();
		setter3.Property = RootSvgButton.SvgOpacityProperty;
		setter3.Value = 1.0;
		style.Add(setter3);
		Setter setter4 = new Setter();
		setter4.Property = InputElement.FocusableProperty;
		setter4.Value = false;
		style.Add(setter4);
		Setter setter5 = new Setter();
		setter5.Property = InputElement.CursorProperty;
		setter5.Value = new Cursor(StandardCursorType.Hand);
		style.Add(setter5);
		Setter setter6 = new Setter();
		setter6.Property = TemplatedControl.TemplateProperty;
		setter6.Value = new ControlTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_170.Build_7), context)
		};
		style.Add(setter6);
		styles.Add(style);
		Styles styles2 = P_1.Styles;
		Style style2 = new Style();
		style2.Selector = ((Selector?)null).OfType(typeof(RootSvgButton)).Class("MediaSvgButton").Class(":pointerover");
		Setter setter7 = new Setter();
		setter7.Property = Visual.OpacityProperty;
		setter7.Value = 0.4;
		style2.Add(setter7);
		styles2.Add(style2);
		Styles styles3 = P_1.Styles;
		Style style4;
		Style style3 = (style4 = new Style());
		context.PushParent(style4);
		style4.Selector = ((Selector?)null).OfType(typeof(RootSvgButton)).Class("MediaSvgButton").Class(":pressed");
		Setter setter9;
		Setter setter8 = (setter9 = new Setter());
		context.PushParent(setter9);
		setter9.Property = Visual.RenderTransformProperty;
		setter9.Value = (ITransform)new TransformConverter().ConvertFrom(context, CultureInfo.InvariantCulture, "scale(0.98)");
		context.PopParent();
		style4.Add(setter8);
		context.PopParent();
		styles3.Add(style3);
		Styles styles4 = P_1.Styles;
		Style style5 = new Style();
		style5.Selector = ((Selector?)null).OfType(typeof(RootSvgButton)).Class("MediaSvgButton").Class(":disabled");
		Setter setter10 = new Setter();
		setter10.Property = Visual.OpacityProperty;
		setter10.Value = 0.4;
		style5.Add(setter10);
		styles4.Add(style5);
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		P_1.Content = rootBorder;
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		RootBorder rootBorder5 = rootBorder4;
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, borderBrushProperty, binding);
		rootBorder5.DynamicBorderThickness = new Thickness(0.0, 0.5, 0.0, 0.0);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		rootBorder5.Child = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		Grid grid5 = grid4;
		global::Avalonia.Controls.Controls children = grid5.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border4, backgroundProperty, binding2);
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		global::Avalonia.Controls.Controls children2 = grid5.Children;
		DockPanel dockPanel2;
		DockPanel dockPanel = (dockPanel2 = new DockPanel());
		((ISupportInitialize)dockPanel).BeginInit();
		children2.Add(dockPanel);
		DockPanel dockPanel4;
		DockPanel dockPanel3 = (dockPanel4 = dockPanel2);
		context.PushParent(dockPanel4);
		dockPanel4.Margin = new Thickness(16.0, 0.0, 16.0, 0.0);
		global::Avalonia.Controls.Controls children3 = dockPanel4.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children3.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		stackPanel5.Name = "RightControls";
		object obj = stackPanel5;
		context.AvaloniaNameScope.Register("RightControls", obj);
		DockPanel.SetDock(stackPanel5, Dock.Right);
		stackPanel5.Orientation = Orientation.Horizontal;
		stackPanel5.VerticalAlignment = VerticalAlignment.Center;
		global::Avalonia.Controls.Controls children4 = stackPanel5.Children;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		children4.Add(stackPanel6);
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		stackPanel9.Name = "MediaButtonsPanel";
		obj = stackPanel9;
		context.AvaloniaNameScope.Register("MediaButtonsPanel", obj);
		stackPanel9.Orientation = Orientation.Horizontal;
		global::Avalonia.Controls.Controls children5 = stackPanel9.Children;
		RootSvgButton rootSvgButton2;
		RootSvgButton rootSvgButton = (rootSvgButton2 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton).BeginInit();
		children5.Add(rootSvgButton);
		RootSvgButton rootSvgButton4;
		RootSvgButton rootSvgButton3 = (rootSvgButton4 = rootSvgButton2);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton5 = rootSvgButton4;
		rootSvgButton5.Name = "ScreenshareBlockedButton";
		obj = rootSvgButton5;
		context.AvaloniaNameScope.Register("ScreenshareBlockedButton", obj);
		rootSvgButton5.Height = 35.0;
		rootSvgButton5.Width = 45.0;
		rootSvgButton5.Classes.Add("MediaSvgButton");
		StyledProperty<string> svgPathProperty = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("ShareScreenBlockedSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton5, svgPathProperty, binding3);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension obj2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.MessageContainer!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.IMessageContainer,RootApp.Client.CoreDomain.LocalChannelPermission!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.LocalChannelPermission,RootApp.Client.CoreDomain.ChannelVideoStreamMedia!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "False"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension = obj2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton5, isVisibleProperty, compiledBindingExtension);
		rootSvgButton5.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		rootSvgButton5.SvgWidth = 24.0;
		rootSvgButton5.SvgHeight = 24.0;
		ToolTip.SetPlacement(rootSvgButton5, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgButton5, -1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton5, 0.0);
		ToolTip.SetShowDelay(rootSvgButton5, 0);
		RootToolTip rootToolTip2;
		RootToolTip rootToolTip = (rootToolTip2 = new RootToolTip());
		((ISupportInitialize)rootToolTip).BeginInit();
		ToolTip.SetTip(rootSvgButton5, rootToolTip);
		RootToolTip rootToolTip4;
		RootToolTip rootToolTip3 = (rootToolTip4 = rootToolTip2);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip5 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip5, PlacementMode.Top);
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		rootToolTip5.Content = textBlock;
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.InsufficientPermissions;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj3 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock5, obj3);
		textBlock5.FontWeight = (FontWeight)450;
		textBlock5.FontSize = 14.0;
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton3).EndInit();
		global::Avalonia.Controls.Controls children6 = stackPanel9.Children;
		RootSvgButton rootSvgButton7;
		RootSvgButton rootSvgButton6 = (rootSvgButton7 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton6).BeginInit();
		children6.Add(rootSvgButton6);
		RootSvgButton rootSvgButton8 = (rootSvgButton4 = rootSvgButton7);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton9 = rootSvgButton4;
		rootSvgButton9.Name = "ScreenshareButton";
		obj = rootSvgButton9;
		context.AvaloniaNameScope.Register("ScreenshareButton", obj);
		rootSvgButton9.Height = 35.0;
		rootSvgButton9.Width = 45.0;
		rootSvgButton9.Classes.Add("MediaSvgButton");
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.MessageContainer!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.IMessageContainer,RootApp.Client.CoreDomain.LocalChannelPermission!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.LocalChannelPermission,RootApp.Client.CoreDomain.ChannelVideoStreamMedia!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "False"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension2 = obj4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, isVisibleProperty2, compiledBindingExtension2);
		rootSvgButton9.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		rootSvgButton9.SvgWidth = 24.0;
		rootSvgButton9.SvgHeight = 24.0;
		StyledProperty<bool> isEnabledProperty = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.ScreenShareIsEnabled!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, isEnabledProperty, compiledBindingExtension4);
		ToolTip.SetPlacement(rootSvgButton9, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgButton9, -1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton9, 0.0);
		ToolTip.SetShowDelay(rootSvgButton9, 0);
		RootToolTip rootToolTip7;
		RootToolTip rootToolTip6 = (rootToolTip7 = new RootToolTip());
		((ISupportInitialize)rootToolTip6).BeginInit();
		ToolTip.SetTip(rootSvgButton9, rootToolTip6);
		RootToolTip rootToolTip8 = (rootToolTip4 = rootToolTip7);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip9 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip9, PlacementMode.Top);
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		rootToolTip9.Content = panel;
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		Panel panel5 = panel4;
		global::Avalonia.Controls.Controls children7 = panel5.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children7.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ShareYourScreen;
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsScreen!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "True"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension5 = obj5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, isVisibleProperty3, compiledBindingExtension5);
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj6 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock9, obj6);
		textBlock9.FontWeight = (FontWeight)450;
		textBlock9.FontSize = 14.0;
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock9.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		global::Avalonia.Controls.Controls children8 = panel5.Children;
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		children8.Add(textBlock10);
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		textBlock13.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.StopSharingYourScreen;
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsScreen!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "False"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension6 = obj7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, isVisibleProperty4, compiledBindingExtension6);
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj8 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock13, obj8);
		textBlock13.FontWeight = (FontWeight)450;
		textBlock13.FontSize = 14.0;
		textBlock13.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock13.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		context.PopParent();
		((ISupportInitialize)panel3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip8).EndInit();
		StyledProperty<string> svgPathProperty2 = RootSvgButton.SvgPathProperty;
		MultiBinding multiBinding2;
		MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding3 = multiBinding2;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("VoiceBarScreenshareSvgConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj9 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding3.Converter = (IMultiValueConverter)obj9;
		IList<IBinding> bindings = multiBinding3.Bindings;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension();
		compiledBindingExtension7.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsScreen!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension7.FallbackValue = "";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings.Add(item);
		IList<IBinding> bindings2 = multiBinding3.Bindings;
		CompiledBindingExtension compiledBindingExtension8 = new CompiledBindingExtension();
		compiledBindingExtension8.Path = new CompiledBindingPathBuilder(1).Property(Application.ActualThemeVariantProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
		compiledBindingExtension8.Source = Application.Current;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item2 = compiledBindingExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings2.Add(item2);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgButton9, svgPathProperty2, multiBinding);
		StyledProperty<IBrush?> backgroundProperty2 = TemplatedControl.BackgroundProperty;
		MultiBinding multiBinding4 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding5 = multiBinding2;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("VoiceBarSelectionBrushConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj10 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding5.Converter = (IMultiValueConverter)obj10;
		IList<IBinding> bindings3 = multiBinding5.Bindings;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension();
		compiledBindingExtension9.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsScreen!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension9.FallbackValue = "";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item3 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings3.Add(item3);
		IList<IBinding> bindings4 = multiBinding5.Bindings;
		CompiledBindingExtension compiledBindingExtension10 = new CompiledBindingExtension();
		compiledBindingExtension10.Path = new CompiledBindingPathBuilder(1).Property(Application.ActualThemeVariantProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
		compiledBindingExtension10.Source = Application.Current;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item4 = compiledBindingExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings4.Add(item4);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgButton9, backgroundProperty2, multiBinding4);
		RootFlyout rootFlyout;
		RootFlyout flyout = (rootFlyout = new RootFlyout());
		context.PushParent(rootFlyout);
		rootFlyout.Placement = PlacementMode.TopEdgeAlignedRight;
		rootFlyout.HorizontalOffset = 16.0;
		rootFlyout.LimitSizeToWindow = false;
		StyledProperty<bool> isPopupOpenProperty = RootFlyout.IsPopupOpenProperty;
		CompiledBindingExtension compiledBindingExtension11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.ScreensharePickerIsOpen!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootFlyout.IsPopupOpenProperty;
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootFlyout, isPopupOpenProperty, compiledBindingExtension12);
		rootFlyout.Opening += context.RootObject.onScreensharePickerOpening;
		rootFlyout.Closed += context.RootObject.onScreensharePickerClosed;
		RootBorder rootBorder7;
		RootBorder rootBorder6 = (rootBorder7 = new RootBorder());
		((ISupportInitialize)rootBorder6).BeginInit();
		rootFlyout.Content = rootBorder6;
		RootBorder rootBorder8 = (rootBorder4 = rootBorder7);
		context.PushParent(rootBorder4);
		RootBorder rootBorder9 = rootBorder4;
		StyledProperty<IBrush?> backgroundProperty3 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, backgroundProperty3, binding4);
		StyledProperty<IBrush?> borderBrushProperty2 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, borderBrushProperty2, binding5);
		rootBorder9.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder9.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		rootBorder9.Margin = new Thickness(12.0, 12.0, 12.0, 12.0);
		StyledProperty<BoxShadows> boxShadowProperty = Border.BoxShadowProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("PopupBoxShadow");
		context.ProvideTargetProperty = Border.BoxShadowProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, boxShadowProperty, binding6);
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		rootBorder9.Child = contentControl;
		ContentControl contentControl4;
		ContentControl contentControl3 = (contentControl4 = contentControl2);
		context.PushParent(contentControl4);
		CompiledBindingExtension compiledBindingExtension13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.ScreensharePicker!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl4, compiledBindingExtension14);
		context.PopParent();
		((ISupportInitialize)contentControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder8).EndInit();
		context.PopParent();
		rootSvgButton9.Flyout = flyout;
		context.PopParent();
		((ISupportInitialize)rootSvgButton8).EndInit();
		global::Avalonia.Controls.Controls children9 = stackPanel9.Children;
		RootSvgButton rootSvgButton11;
		RootSvgButton rootSvgButton10 = (rootSvgButton11 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton10).BeginInit();
		children9.Add(rootSvgButton10);
		RootSvgButton rootSvgButton12 = (rootSvgButton4 = rootSvgButton11);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton13 = rootSvgButton4;
		rootSvgButton13.Name = "WebcamBlockedButton";
		obj = rootSvgButton13;
		context.AvaloniaNameScope.Register("WebcamBlockedButton", obj);
		rootSvgButton13.Height = 35.0;
		rootSvgButton13.Width = 45.0;
		StyledProperty<string> svgPathProperty3 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("VideoShareBlockedSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton13, svgPathProperty3, binding7);
		StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.MessageContainer!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.IMessageContainer,RootApp.Client.CoreDomain.LocalChannelPermission!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.LocalChannelPermission,RootApp.Client.CoreDomain.ChannelVideoStreamMedia!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "False"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension15 = obj11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton13, isVisibleProperty5, compiledBindingExtension15);
		rootSvgButton13.Margin = new Thickness(8.0, 0.0, 16.0, 0.0);
		rootSvgButton13.Classes.Add("MediaSvgButton");
		rootSvgButton13.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		rootSvgButton13.SvgWidth = 24.0;
		rootSvgButton13.SvgHeight = 24.0;
		ToolTip.SetPlacement(rootSvgButton13, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgButton13, -1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton13, 0.0);
		ToolTip.SetShowDelay(rootSvgButton13, 0);
		RootToolTip rootToolTip11;
		RootToolTip rootToolTip10 = (rootToolTip11 = new RootToolTip());
		((ISupportInitialize)rootToolTip10).BeginInit();
		ToolTip.SetTip(rootSvgButton13, rootToolTip10);
		RootToolTip rootToolTip12 = (rootToolTip4 = rootToolTip11);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip13 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip13, PlacementMode.Top);
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		rootToolTip13.Content = textBlock14;
		TextBlock textBlock16 = (textBlock4 = textBlock15);
		context.PushParent(textBlock4);
		TextBlock textBlock17 = textBlock4;
		textBlock17.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.InsufficientPermissions;
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj12 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock17, obj12);
		textBlock17.FontWeight = (FontWeight)450;
		textBlock17.FontSize = 14.0;
		textBlock17.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock17.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock16).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip12).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton12).EndInit();
		global::Avalonia.Controls.Controls children10 = stackPanel9.Children;
		RootSvgButton rootSvgButton15;
		RootSvgButton rootSvgButton14 = (rootSvgButton15 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton14).BeginInit();
		children10.Add(rootSvgButton14);
		RootSvgButton rootSvgButton16 = (rootSvgButton4 = rootSvgButton15);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton17 = rootSvgButton4;
		rootSvgButton17.Name = "WebcamButton";
		obj = rootSvgButton17;
		context.AvaloniaNameScope.Register("WebcamButton", obj);
		StyledProperty<bool> isVisibleProperty6 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.MessageContainer!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.IMessageContainer,RootApp.Client.CoreDomain.LocalChannelPermission!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.LocalChannelPermission,RootApp.Client.CoreDomain.ChannelVideoStreamMedia!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "False"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension16 = obj13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton17, isVisibleProperty6, compiledBindingExtension16);
		rootSvgButton17.Height = 35.0;
		rootSvgButton17.Width = 45.0;
		rootSvgButton17.Margin = new Thickness(8.0, 0.0, 16.0, 0.0);
		rootSvgButton17.Classes.Add("MediaSvgButton");
		rootSvgButton17.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		rootSvgButton17.SvgWidth = 24.0;
		rootSvgButton17.SvgHeight = 24.0;
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension17 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.ToggleWebcamCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension18 = compiledBindingExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton17, commandProperty, compiledBindingExtension18);
		StyledProperty<bool> isEnabledProperty2 = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension19 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.VideoIsEnabled!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension20 = compiledBindingExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton17, isEnabledProperty2, compiledBindingExtension20);
		ToolTip.SetPlacement(rootSvgButton17, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgButton17, -1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton17, 0.0);
		ToolTip.SetShowDelay(rootSvgButton17, 0);
		RootToolTip rootToolTip15;
		RootToolTip rootToolTip14 = (rootToolTip15 = new RootToolTip());
		((ISupportInitialize)rootToolTip14).BeginInit();
		ToolTip.SetTip(rootSvgButton17, rootToolTip14);
		RootToolTip rootToolTip16 = (rootToolTip4 = rootToolTip15);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip17 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip17, PlacementMode.Top);
		Panel panel7;
		Panel panel6 = (panel7 = new Panel());
		((ISupportInitialize)panel6).BeginInit();
		rootToolTip17.Content = panel6;
		Panel panel8 = (panel4 = panel7);
		context.PushParent(panel4);
		Panel panel9 = panel4;
		global::Avalonia.Controls.Controls children11 = panel9.Children;
		TextBlock textBlock19;
		TextBlock textBlock18 = (textBlock19 = new TextBlock());
		((ISupportInitialize)textBlock18).BeginInit();
		children11.Add(textBlock18);
		TextBlock textBlock20 = (textBlock4 = textBlock19);
		context.PushParent(textBlock4);
		TextBlock textBlock21 = textBlock4;
		textBlock21.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.TurnOnCamera;
		StyledProperty<bool> isVisibleProperty7 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj14 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsVideo!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "True"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension21 = obj14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock21, isVisibleProperty7, compiledBindingExtension21);
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj15 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock21, obj15);
		textBlock21.FontWeight = (FontWeight)450;
		textBlock21.FontSize = 14.0;
		textBlock21.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock21.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock20).EndInit();
		global::Avalonia.Controls.Controls children12 = panel9.Children;
		TextBlock textBlock23;
		TextBlock textBlock22 = (textBlock23 = new TextBlock());
		((ISupportInitialize)textBlock22).BeginInit();
		children12.Add(textBlock22);
		TextBlock textBlock24 = (textBlock4 = textBlock23);
		context.PushParent(textBlock4);
		TextBlock textBlock25 = textBlock4;
		textBlock25.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.TurnOffCamera;
		StyledProperty<bool> isVisibleProperty8 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj16 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsVideo!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "False"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension22 = obj16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock25, isVisibleProperty8, compiledBindingExtension22);
		StaticResourceExtension staticResourceExtension8 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj17 = staticResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock25, obj17);
		textBlock25.FontWeight = (FontWeight)450;
		textBlock25.FontSize = 14.0;
		textBlock25.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock25.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock24).EndInit();
		context.PopParent();
		((ISupportInitialize)panel8).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip16).EndInit();
		StyledProperty<string> svgPathProperty4 = RootSvgButton.SvgPathProperty;
		MultiBinding multiBinding6 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding7 = multiBinding2;
		StaticResourceExtension staticResourceExtension9 = new StaticResourceExtension("VoiceBarWebcamSvgConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj18 = staticResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding7.Converter = (IMultiValueConverter)obj18;
		IList<IBinding> bindings5 = multiBinding7.Bindings;
		CompiledBindingExtension compiledBindingExtension23 = new CompiledBindingExtension();
		compiledBindingExtension23.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsVideo!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension23.FallbackValue = "";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item5 = compiledBindingExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings5.Add(item5);
		IList<IBinding> bindings6 = multiBinding7.Bindings;
		CompiledBindingExtension compiledBindingExtension24 = new CompiledBindingExtension();
		compiledBindingExtension24.Path = new CompiledBindingPathBuilder(1).Property(Application.ActualThemeVariantProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
		compiledBindingExtension24.Source = Application.Current;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item6 = compiledBindingExtension24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings6.Add(item6);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgButton17, svgPathProperty4, multiBinding6);
		StyledProperty<IBrush?> backgroundProperty4 = TemplatedControl.BackgroundProperty;
		MultiBinding multiBinding8 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding9 = multiBinding2;
		StaticResourceExtension staticResourceExtension10 = new StaticResourceExtension("VoiceBarSelectionBrushConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj19 = staticResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding9.Converter = (IMultiValueConverter)obj19;
		IList<IBinding> bindings7 = multiBinding9.Bindings;
		CompiledBindingExtension compiledBindingExtension25 = new CompiledBindingExtension();
		compiledBindingExtension25.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsVideo!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension25.FallbackValue = "";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item7 = compiledBindingExtension25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings7.Add(item7);
		IList<IBinding> bindings8 = multiBinding9.Bindings;
		CompiledBindingExtension compiledBindingExtension26 = new CompiledBindingExtension();
		compiledBindingExtension26.Path = new CompiledBindingPathBuilder(1).Property(Application.ActualThemeVariantProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
		compiledBindingExtension26.Source = Application.Current;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item8 = compiledBindingExtension26.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings8.Add(item8);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgButton17, backgroundProperty4, multiBinding8);
		context.PopParent();
		((ISupportInitialize)rootSvgButton16).EndInit();
		global::Avalonia.Controls.Controls children13 = stackPanel9.Children;
		Rectangle rectangle2;
		Rectangle rectangle = (rectangle2 = new Rectangle());
		((ISupportInitialize)rectangle).BeginInit();
		children13.Add(rectangle);
		Rectangle rectangle4;
		Rectangle rectangle3 = (rectangle4 = rectangle2);
		context.PushParent(rectangle4);
		Rectangle rectangle5 = rectangle4;
		rectangle5.Name = "Separator1";
		obj = rectangle5;
		context.AvaloniaNameScope.Register("Separator1", obj);
		rectangle5.Width = 0.5;
		rectangle5.Height = 25.0;
		StyledProperty<IBrush?> fillProperty = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle5, fillProperty, binding8);
		rectangle5.Margin = new Thickness(0.0, 0.0, 16.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rectangle3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		global::Avalonia.Controls.Controls children14 = stackPanel5.Children;
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		children14.Add(grid6);
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		grid9.Name = "MuteGrid";
		obj = grid9;
		context.AvaloniaNameScope.Register("MuteGrid", obj);
		grid9.Margin = new Thickness(0.0, 0.0, 16.0, 0.0);
		global::Avalonia.Controls.Controls children15 = grid9.Children;
		RootSvgButton rootSvgButton19;
		RootSvgButton rootSvgButton18 = (rootSvgButton19 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton18).BeginInit();
		children15.Add(rootSvgButton18);
		RootSvgButton rootSvgButton20 = (rootSvgButton4 = rootSvgButton19);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton21 = rootSvgButton4;
		rootSvgButton21.Name = "MicrophoneImage";
		obj = rootSvgButton21;
		context.AvaloniaNameScope.Register("MicrophoneImage", obj);
		rootSvgButton21.Classes.Add("Custom");
		StyledProperty<ICommand?> commandProperty2 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension27 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.ToggleMuteCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension28 = compiledBindingExtension27.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton21, commandProperty2, compiledBindingExtension28);
		StyledProperty<string> svgPathProperty5 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("MicrophoneSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton21, svgPathProperty5, binding9);
		rootSvgButton21.Width = 24.0;
		rootSvgButton21.Height = 24.0;
		rootSvgButton21.SvgWidth = 24.0;
		rootSvgButton21.SvgHeight = 24.0;
		ToolTip.SetPlacement(rootSvgButton21, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgButton21, -1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton21, 0.0);
		ToolTip.SetShowDelay(rootSvgButton21, 0);
		StyledProperty<bool> isVisibleProperty9 = Visual.IsVisibleProperty;
		MultiBinding multiBinding10 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding11 = multiBinding2;
		multiBinding11.Converter = BoolConverters.And;
		IList<IBinding> bindings9 = multiBinding11.Bindings;
		CompiledBindingExtension compiledBindingExtension29 = new CompiledBindingExtension();
		compiledBindingExtension29.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsMuted!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension29.FallbackValue = "False";
		compiledBindingExtension29.Converter = BoolConverters.Not;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item9 = compiledBindingExtension29.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings9.Add(item9);
		IList<IBinding> bindings10 = multiBinding11.Bindings;
		CompiledBindingExtension compiledBindingExtension30 = new CompiledBindingExtension();
		compiledBindingExtension30.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsAdminMuted!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension30.FallbackValue = "False";
		compiledBindingExtension30.Converter = BoolConverters.Not;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item10 = compiledBindingExtension30.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings10.Add(item10);
		IList<IBinding> bindings11 = multiBinding11.Bindings;
		CompiledBindingExtension compiledBindingExtension31 = new CompiledBindingExtension();
		compiledBindingExtension31.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.MessageContainer!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.IMessageContainer,RootApp.Client.CoreDomain.LocalChannelPermission!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.LocalChannelPermission,RootApp.Client.CoreDomain.ChannelVoiceTalk!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension31.FallbackValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item11 = compiledBindingExtension31.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings11.Add(item11);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgButton21, isVisibleProperty9, multiBinding10);
		RootToolTip rootToolTip19;
		RootToolTip rootToolTip18 = (rootToolTip19 = new RootToolTip());
		((ISupportInitialize)rootToolTip18).BeginInit();
		ToolTip.SetTip(rootSvgButton21, rootToolTip18);
		RootToolTip rootToolTip20 = (rootToolTip4 = rootToolTip19);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip21 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip21, PlacementMode.Top);
		TextBlock textBlock27;
		TextBlock textBlock26 = (textBlock27 = new TextBlock());
		((ISupportInitialize)textBlock26).BeginInit();
		rootToolTip21.Content = textBlock26;
		TextBlock textBlock28 = (textBlock4 = textBlock27);
		context.PushParent(textBlock4);
		TextBlock textBlock29 = textBlock4;
		textBlock29.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Mute;
		StaticResourceExtension staticResourceExtension11 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj20 = staticResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock29, obj20);
		textBlock29.FontWeight = (FontWeight)450;
		textBlock29.FontSize = 14.0;
		textBlock29.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock29.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock28).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip20).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton20).EndInit();
		global::Avalonia.Controls.Controls children16 = grid9.Children;
		RootSvgButton rootSvgButton23;
		RootSvgButton rootSvgButton22 = (rootSvgButton23 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton22).BeginInit();
		children16.Add(rootSvgButton22);
		RootSvgButton rootSvgButton24 = (rootSvgButton4 = rootSvgButton23);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton25 = rootSvgButton4;
		rootSvgButton25.Name = "MicrophoneMutedImage";
		obj = rootSvgButton25;
		context.AvaloniaNameScope.Register("MicrophoneMutedImage", obj);
		rootSvgButton25.Classes.Add("Custom");
		StyledProperty<ICommand?> commandProperty3 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension32 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.ToggleMuteCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension33 = compiledBindingExtension32.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton25, commandProperty3, compiledBindingExtension33);
		StyledProperty<string> svgPathProperty6 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("MicrophoneMutedSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton25, svgPathProperty6, binding10);
		rootSvgButton25.Width = 24.0;
		rootSvgButton25.Height = 24.0;
		rootSvgButton25.SvgWidth = 24.0;
		rootSvgButton25.SvgHeight = 24.0;
		ToolTip.SetPlacement(rootSvgButton25, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgButton25, -1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton25, 0.0);
		ToolTip.SetShowDelay(rootSvgButton25, 0);
		StyledProperty<bool> isVisibleProperty10 = Visual.IsVisibleProperty;
		MultiBinding multiBinding12 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding13 = multiBinding2;
		StaticResourceExtension staticResourceExtension12 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj21 = staticResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding13.Converter = (IMultiValueConverter)obj21;
		IList<IBinding> bindings12 = multiBinding13.Bindings;
		CompiledBindingExtension compiledBindingExtension34 = new CompiledBindingExtension();
		compiledBindingExtension34.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsMuted!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension34.FallbackValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item12 = compiledBindingExtension34.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings12.Add(item12);
		IList<IBinding> bindings13 = multiBinding13.Bindings;
		CompiledBindingExtension compiledBindingExtension35 = new CompiledBindingExtension();
		compiledBindingExtension35.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsAdminMuted!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension35.FallbackValue = "False";
		compiledBindingExtension35.Converter = BoolConverters.Not;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item13 = compiledBindingExtension35.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings13.Add(item13);
		IList<IBinding> bindings14 = multiBinding13.Bindings;
		CompiledBindingExtension compiledBindingExtension36 = new CompiledBindingExtension();
		compiledBindingExtension36.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.MessageContainer!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.IMessageContainer,RootApp.Client.CoreDomain.LocalChannelPermission!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.LocalChannelPermission,RootApp.Client.CoreDomain.ChannelVoiceTalk!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension36.FallbackValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item14 = compiledBindingExtension36.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings14.Add(item14);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgButton25, isVisibleProperty10, multiBinding12);
		RootToolTip rootToolTip23;
		RootToolTip rootToolTip22 = (rootToolTip23 = new RootToolTip());
		((ISupportInitialize)rootToolTip22).BeginInit();
		ToolTip.SetTip(rootSvgButton25, rootToolTip22);
		RootToolTip rootToolTip24 = (rootToolTip4 = rootToolTip23);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip25 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip25, PlacementMode.Top);
		TextBlock textBlock31;
		TextBlock textBlock30 = (textBlock31 = new TextBlock());
		((ISupportInitialize)textBlock30).BeginInit();
		rootToolTip25.Content = textBlock30;
		TextBlock textBlock32 = (textBlock4 = textBlock31);
		context.PushParent(textBlock4);
		TextBlock textBlock33 = textBlock4;
		textBlock33.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Unmute;
		StaticResourceExtension staticResourceExtension13 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj22 = staticResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock33, obj22);
		textBlock33.FontWeight = (FontWeight)450;
		textBlock33.FontSize = 14.0;
		textBlock33.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock33.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock32).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip24).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton24).EndInit();
		global::Avalonia.Controls.Controls children17 = grid9.Children;
		RootSvgButton rootSvgButton27;
		RootSvgButton rootSvgButton26 = (rootSvgButton27 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton26).BeginInit();
		children17.Add(rootSvgButton26);
		RootSvgButton rootSvgButton28 = (rootSvgButton4 = rootSvgButton27);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton29 = rootSvgButton4;
		rootSvgButton29.Name = "MicrophoneBlockedImage";
		obj = rootSvgButton29;
		context.AvaloniaNameScope.Register("MicrophoneBlockedImage", obj);
		rootSvgButton29.Classes.Add("Custom");
		StyledProperty<string> svgPathProperty7 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("MicrophoneBlockedSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton29, svgPathProperty7, binding11);
		rootSvgButton29.Width = 24.0;
		rootSvgButton29.Height = 24.0;
		rootSvgButton29.SvgWidth = 24.0;
		rootSvgButton29.SvgHeight = 24.0;
		ToolTip.SetPlacement(rootSvgButton29, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgButton29, -1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton29, 0.0);
		ToolTip.SetShowDelay(rootSvgButton29, 0);
		StyledProperty<bool> isVisibleProperty11 = Visual.IsVisibleProperty;
		MultiBinding multiBinding14 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding15 = multiBinding2;
		StaticResourceExtension staticResourceExtension14 = new StaticResourceExtension("OrConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj23 = staticResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding15.Converter = (IMultiValueConverter)obj23;
		IList<IBinding> bindings15 = multiBinding15.Bindings;
		CompiledBindingExtension compiledBindingExtension37 = new CompiledBindingExtension();
		compiledBindingExtension37.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsAdminMuted!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension37.FallbackValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item15 = compiledBindingExtension37.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings15.Add(item15);
		IList<IBinding> bindings16 = multiBinding15.Bindings;
		CompiledBindingExtension compiledBindingExtension38 = new CompiledBindingExtension();
		compiledBindingExtension38.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.MessageContainer!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.IMessageContainer,RootApp.Client.CoreDomain.LocalChannelPermission!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.LocalChannelPermission,RootApp.Client.CoreDomain.ChannelVoiceTalk!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension38.FallbackValue = "False";
		compiledBindingExtension38.Converter = BoolConverters.Not;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item16 = compiledBindingExtension38.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings16.Add(item16);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgButton29, isVisibleProperty11, multiBinding14);
		RootToolTip rootToolTip27;
		RootToolTip rootToolTip26 = (rootToolTip27 = new RootToolTip());
		((ISupportInitialize)rootToolTip26).BeginInit();
		ToolTip.SetTip(rootSvgButton29, rootToolTip26);
		RootToolTip rootToolTip28 = (rootToolTip4 = rootToolTip27);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip29 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip29, PlacementMode.Top);
		Panel panel11;
		Panel panel10 = (panel11 = new Panel());
		((ISupportInitialize)panel10).BeginInit();
		rootToolTip29.Content = panel10;
		Panel panel12 = (panel4 = panel11);
		context.PushParent(panel4);
		Panel panel13 = panel4;
		global::Avalonia.Controls.Controls children18 = panel13.Children;
		TextBlock textBlock35;
		TextBlock textBlock34 = (textBlock35 = new TextBlock());
		((ISupportInitialize)textBlock34).BeginInit();
		children18.Add(textBlock34);
		TextBlock textBlock36 = (textBlock4 = textBlock35);
		context.PushParent(textBlock4);
		TextBlock textBlock37 = textBlock4;
		textBlock37.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.AdminMuted;
		StaticResourceExtension staticResourceExtension15 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj24 = staticResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock37, obj24);
		textBlock37.FontWeight = (FontWeight)450;
		textBlock37.FontSize = 14.0;
		StyledProperty<bool> isVisibleProperty12 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj25 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsAdminMuted!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "False"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension39 = obj25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock37, isVisibleProperty12, compiledBindingExtension39);
		textBlock37.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock37.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock36).EndInit();
		global::Avalonia.Controls.Controls children19 = panel13.Children;
		TextBlock textBlock39;
		TextBlock textBlock38 = (textBlock39 = new TextBlock());
		((ISupportInitialize)textBlock38).BeginInit();
		children19.Add(textBlock38);
		TextBlock textBlock40 = (textBlock4 = textBlock39);
		context.PushParent(textBlock4);
		TextBlock textBlock41 = textBlock4;
		textBlock41.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.InsufficientPermissions;
		StaticResourceExtension staticResourceExtension16 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj26 = staticResourceExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock41, obj26);
		textBlock41.FontWeight = (FontWeight)450;
		textBlock41.FontSize = 14.0;
		StyledProperty<bool> isVisibleProperty13 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj27 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.MessageContainer!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.IMessageContainer,RootApp.Client.CoreDomain.LocalChannelPermission!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.LocalChannelPermission,RootApp.Client.CoreDomain.ChannelVoiceTalk!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "False"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension40 = obj27.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock41, isVisibleProperty13, compiledBindingExtension40);
		textBlock41.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock41.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock40).EndInit();
		context.PopParent();
		((ISupportInitialize)panel12).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip28).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton28).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		global::Avalonia.Controls.Controls children20 = stackPanel5.Children;
		Grid grid11;
		Grid grid10 = (grid11 = new Grid());
		((ISupportInitialize)grid10).BeginInit();
		children20.Add(grid10);
		Grid grid12 = (grid4 = grid11);
		context.PushParent(grid4);
		Grid grid13 = grid4;
		grid13.Name = "DeafenGrid";
		obj = grid13;
		context.AvaloniaNameScope.Register("DeafenGrid", obj);
		grid13.Margin = new Thickness(0.0, 0.0, 16.0, 0.0);
		global::Avalonia.Controls.Controls children21 = grid13.Children;
		RootSvgButton rootSvgButton31;
		RootSvgButton rootSvgButton30 = (rootSvgButton31 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton30).BeginInit();
		children21.Add(rootSvgButton30);
		RootSvgButton rootSvgButton32 = (rootSvgButton4 = rootSvgButton31);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton33 = rootSvgButton4;
		rootSvgButton33.Name = "HeadphonesImage";
		obj = rootSvgButton33;
		context.AvaloniaNameScope.Register("HeadphonesImage", obj);
		rootSvgButton33.Classes.Add("Custom");
		StyledProperty<ICommand?> commandProperty4 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension41 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.ToggleDeafenCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension42 = compiledBindingExtension41.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton33, commandProperty4, compiledBindingExtension42);
		StyledProperty<string> svgPathProperty8 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("HeadphonesSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding12 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton33, svgPathProperty8, binding12);
		rootSvgButton33.Width = 24.0;
		rootSvgButton33.Height = 24.0;
		rootSvgButton33.SvgWidth = 20.0;
		rootSvgButton33.SvgHeight = 18.0;
		ToolTip.SetPlacement(rootSvgButton33, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgButton33, -1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton33, 0.0);
		ToolTip.SetShowDelay(rootSvgButton33, 0);
		StyledProperty<bool> isVisibleProperty14 = Visual.IsVisibleProperty;
		MultiBinding multiBinding16 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding17 = multiBinding2;
		multiBinding17.Converter = BoolConverters.And;
		IList<IBinding> bindings17 = multiBinding17.Bindings;
		CompiledBindingExtension compiledBindingExtension43 = new CompiledBindingExtension();
		compiledBindingExtension43.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsDeafened!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension43.FallbackValue = "False";
		compiledBindingExtension43.Converter = BoolConverters.Not;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item17 = compiledBindingExtension43.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings17.Add(item17);
		IList<IBinding> bindings18 = multiBinding17.Bindings;
		CompiledBindingExtension compiledBindingExtension44 = new CompiledBindingExtension();
		compiledBindingExtension44.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsAdminDeafened!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension44.FallbackValue = "False";
		compiledBindingExtension44.Converter = BoolConverters.Not;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item18 = compiledBindingExtension44.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings18.Add(item18);
		IList<IBinding> bindings19 = multiBinding17.Bindings;
		CompiledBindingExtension compiledBindingExtension45 = new CompiledBindingExtension();
		compiledBindingExtension45.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.MessageContainer!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.IMessageContainer,RootApp.Client.CoreDomain.LocalChannelPermission!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.LocalChannelPermission,RootApp.Client.CoreDomain.ChannelVoiceTalk!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension45.FallbackValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item19 = compiledBindingExtension45.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings19.Add(item19);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgButton33, isVisibleProperty14, multiBinding16);
		RootToolTip rootToolTip31;
		RootToolTip rootToolTip30 = (rootToolTip31 = new RootToolTip());
		((ISupportInitialize)rootToolTip30).BeginInit();
		ToolTip.SetTip(rootSvgButton33, rootToolTip30);
		RootToolTip rootToolTip32 = (rootToolTip4 = rootToolTip31);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip33 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip33, PlacementMode.Top);
		TextBlock textBlock43;
		TextBlock textBlock42 = (textBlock43 = new TextBlock());
		((ISupportInitialize)textBlock42).BeginInit();
		rootToolTip33.Content = textBlock42;
		TextBlock textBlock44 = (textBlock4 = textBlock43);
		context.PushParent(textBlock4);
		TextBlock textBlock45 = textBlock4;
		textBlock45.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Deafen;
		StaticResourceExtension staticResourceExtension17 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj28 = staticResourceExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock45, obj28);
		textBlock45.FontWeight = (FontWeight)450;
		textBlock45.FontSize = 14.0;
		textBlock45.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock45.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock44).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip32).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton32).EndInit();
		global::Avalonia.Controls.Controls children22 = grid13.Children;
		RootSvgButton rootSvgButton35;
		RootSvgButton rootSvgButton34 = (rootSvgButton35 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton34).BeginInit();
		children22.Add(rootSvgButton34);
		RootSvgButton rootSvgButton36 = (rootSvgButton4 = rootSvgButton35);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton37 = rootSvgButton4;
		rootSvgButton37.Name = "HeadphonesMutedImage";
		obj = rootSvgButton37;
		context.AvaloniaNameScope.Register("HeadphonesMutedImage", obj);
		rootSvgButton37.Classes.Add("Custom");
		StyledProperty<ICommand?> commandProperty5 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension46 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.ToggleDeafenCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension47 = compiledBindingExtension46.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton37, commandProperty5, compiledBindingExtension47);
		StyledProperty<string> svgPathProperty9 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("HeadphonesMutedSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding13 = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton37, svgPathProperty9, binding13);
		rootSvgButton37.Width = 24.0;
		rootSvgButton37.Height = 24.0;
		rootSvgButton37.SvgWidth = 24.0;
		rootSvgButton37.SvgHeight = 24.0;
		ToolTip.SetPlacement(rootSvgButton37, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgButton37, -1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton37, 0.0);
		ToolTip.SetShowDelay(rootSvgButton37, 0);
		StyledProperty<bool> isVisibleProperty15 = Visual.IsVisibleProperty;
		MultiBinding multiBinding18 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding19 = multiBinding2;
		StaticResourceExtension staticResourceExtension18 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj29 = staticResourceExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding19.Converter = (IMultiValueConverter)obj29;
		IList<IBinding> bindings20 = multiBinding19.Bindings;
		CompiledBindingExtension compiledBindingExtension48 = new CompiledBindingExtension();
		compiledBindingExtension48.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsDeafened!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension48.FallbackValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item20 = compiledBindingExtension48.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings20.Add(item20);
		IList<IBinding> bindings21 = multiBinding19.Bindings;
		CompiledBindingExtension compiledBindingExtension49 = new CompiledBindingExtension();
		compiledBindingExtension49.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsAdminDeafened!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension49.FallbackValue = "False";
		compiledBindingExtension49.Converter = BoolConverters.Not;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item21 = compiledBindingExtension49.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings21.Add(item21);
		IList<IBinding> bindings22 = multiBinding19.Bindings;
		CompiledBindingExtension compiledBindingExtension50 = new CompiledBindingExtension();
		compiledBindingExtension50.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.MessageContainer!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.IMessageContainer,RootApp.Client.CoreDomain.LocalChannelPermission!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.LocalChannelPermission,RootApp.Client.CoreDomain.ChannelVoiceTalk!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension50.FallbackValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item22 = compiledBindingExtension50.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings22.Add(item22);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgButton37, isVisibleProperty15, multiBinding18);
		RootToolTip rootToolTip35;
		RootToolTip rootToolTip34 = (rootToolTip35 = new RootToolTip());
		((ISupportInitialize)rootToolTip34).BeginInit();
		ToolTip.SetTip(rootSvgButton37, rootToolTip34);
		RootToolTip rootToolTip36 = (rootToolTip4 = rootToolTip35);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip37 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip37, PlacementMode.Top);
		TextBlock textBlock47;
		TextBlock textBlock46 = (textBlock47 = new TextBlock());
		((ISupportInitialize)textBlock46).BeginInit();
		rootToolTip37.Content = textBlock46;
		TextBlock textBlock48 = (textBlock4 = textBlock47);
		context.PushParent(textBlock4);
		TextBlock textBlock49 = textBlock4;
		textBlock49.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Undeafen;
		StaticResourceExtension staticResourceExtension19 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj30 = staticResourceExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock49, obj30);
		textBlock49.FontWeight = (FontWeight)450;
		textBlock49.FontSize = 14.0;
		textBlock49.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock49.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock48).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip36).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton36).EndInit();
		global::Avalonia.Controls.Controls children23 = grid13.Children;
		RootSvgButton rootSvgButton39;
		RootSvgButton rootSvgButton38 = (rootSvgButton39 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton38).BeginInit();
		children23.Add(rootSvgButton38);
		RootSvgButton rootSvgButton40 = (rootSvgButton4 = rootSvgButton39);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton41 = rootSvgButton4;
		rootSvgButton41.Name = "HeadphonesBlockedImage";
		obj = rootSvgButton41;
		context.AvaloniaNameScope.Register("HeadphonesBlockedImage", obj);
		rootSvgButton41.Classes.Add("Custom");
		StyledProperty<string> svgPathProperty10 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("HeadphonesBlockedSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding14 = dynamicResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton41, svgPathProperty10, binding14);
		rootSvgButton41.Width = 24.0;
		rootSvgButton41.Height = 24.0;
		rootSvgButton41.SvgWidth = 24.0;
		rootSvgButton41.SvgHeight = 24.0;
		ToolTip.SetPlacement(rootSvgButton41, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgButton41, -1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton41, 0.0);
		ToolTip.SetShowDelay(rootSvgButton41, 0);
		StyledProperty<bool> isVisibleProperty16 = Visual.IsVisibleProperty;
		MultiBinding multiBinding20 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding21 = multiBinding2;
		StaticResourceExtension staticResourceExtension20 = new StaticResourceExtension("OrConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj31 = staticResourceExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding21.Converter = (IMultiValueConverter)obj31;
		IList<IBinding> bindings23 = multiBinding21.Bindings;
		CompiledBindingExtension compiledBindingExtension51 = new CompiledBindingExtension();
		compiledBindingExtension51.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsAdminDeafened!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension51.FallbackValue = "False";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item23 = compiledBindingExtension51.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings23.Add(item23);
		IList<IBinding> bindings24 = multiBinding21.Bindings;
		CompiledBindingExtension compiledBindingExtension52 = new CompiledBindingExtension();
		compiledBindingExtension52.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.MessageContainer!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.IMessageContainer,RootApp.Client.CoreDomain.LocalChannelPermission!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.LocalChannelPermission,RootApp.Client.CoreDomain.ChannelVoiceTalk!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension52.FallbackValue = "False";
		compiledBindingExtension52.Converter = BoolConverters.Not;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item24 = compiledBindingExtension52.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings24.Add(item24);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgButton41, isVisibleProperty16, multiBinding20);
		RootToolTip rootToolTip39;
		RootToolTip rootToolTip38 = (rootToolTip39 = new RootToolTip());
		((ISupportInitialize)rootToolTip38).BeginInit();
		ToolTip.SetTip(rootSvgButton41, rootToolTip38);
		RootToolTip rootToolTip40 = (rootToolTip4 = rootToolTip39);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip41 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip41, PlacementMode.Top);
		Panel panel15;
		Panel panel14 = (panel15 = new Panel());
		((ISupportInitialize)panel14).BeginInit();
		rootToolTip41.Content = panel14;
		Panel panel16 = (panel4 = panel15);
		context.PushParent(panel4);
		Panel panel17 = panel4;
		global::Avalonia.Controls.Controls children24 = panel17.Children;
		TextBlock textBlock51;
		TextBlock textBlock50 = (textBlock51 = new TextBlock());
		((ISupportInitialize)textBlock50).BeginInit();
		children24.Add(textBlock50);
		TextBlock textBlock52 = (textBlock4 = textBlock51);
		context.PushParent(textBlock4);
		TextBlock textBlock53 = textBlock4;
		textBlock53.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.AdminDeafened;
		StaticResourceExtension staticResourceExtension21 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj32 = staticResourceExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock53, obj32);
		textBlock53.FontWeight = (FontWeight)450;
		textBlock53.FontSize = 14.0;
		StyledProperty<bool> isVisibleProperty17 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj33 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsAdminDeafened!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "False"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension53 = obj33.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock53, isVisibleProperty17, compiledBindingExtension53);
		textBlock53.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock53.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock52).EndInit();
		global::Avalonia.Controls.Controls children25 = panel17.Children;
		TextBlock textBlock55;
		TextBlock textBlock54 = (textBlock55 = new TextBlock());
		((ISupportInitialize)textBlock54).BeginInit();
		children25.Add(textBlock54);
		TextBlock textBlock56 = (textBlock4 = textBlock55);
		context.PushParent(textBlock4);
		TextBlock textBlock57 = textBlock4;
		textBlock57.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.InsufficientPermissions;
		StaticResourceExtension staticResourceExtension22 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj34 = staticResourceExtension22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock57, obj34);
		textBlock57.FontWeight = (FontWeight)450;
		textBlock57.FontSize = 14.0;
		StyledProperty<bool> isVisibleProperty18 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj35 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.MessageContainer!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.IMessageContainer,RootApp.Client.CoreDomain.LocalChannelPermission!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.LocalChannelPermission,RootApp.Client.CoreDomain.ChannelVoiceTalk!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "False"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension54 = obj35.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock57, isVisibleProperty18, compiledBindingExtension54);
		textBlock57.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock57.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock56).EndInit();
		context.PopParent();
		((ISupportInitialize)panel16).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip40).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton40).EndInit();
		context.PopParent();
		((ISupportInitialize)grid12).EndInit();
		global::Avalonia.Controls.Controls children26 = stackPanel5.Children;
		RootSvgButton rootSvgButton43;
		RootSvgButton rootSvgButton42 = (rootSvgButton43 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton42).BeginInit();
		children26.Add(rootSvgButton42);
		RootSvgButton rootSvgButton44 = (rootSvgButton4 = rootSvgButton43);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton45 = rootSvgButton4;
		rootSvgButton45.Name = "SettingsButton";
		obj = rootSvgButton45;
		context.AvaloniaNameScope.Register("SettingsButton", obj);
		rootSvgButton45.Margin = new Thickness(0.0, 0.0, 16.0, 0.0);
		rootSvgButton45.Width = 24.0;
		rootSvgButton45.Classes.Add("Custom");
		rootSvgButton45.Height = 24.0;
		StyledProperty<ICommand?> commandProperty6 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension55 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.OpenSettingsCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension56 = compiledBindingExtension55.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton45, commandProperty6, compiledBindingExtension56);
		StyledProperty<string> svgPathProperty11 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension15 = new DynamicResourceExtension("SettingsSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding15 = dynamicResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton45, svgPathProperty11, binding15);
		rootSvgButton45.SvgWidth = 24.0;
		rootSvgButton45.SvgHeight = 24.0;
		ToolTip.SetPlacement(rootSvgButton45, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgButton45, -1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton45, 0.0);
		ToolTip.SetShowDelay(rootSvgButton45, 0);
		RootToolTip rootToolTip43;
		RootToolTip rootToolTip42 = (rootToolTip43 = new RootToolTip());
		((ISupportInitialize)rootToolTip42).BeginInit();
		ToolTip.SetTip(rootSvgButton45, rootToolTip42);
		RootToolTip rootToolTip44 = (rootToolTip4 = rootToolTip43);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip45 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip45, PlacementMode.Top);
		TextBlock textBlock59;
		TextBlock textBlock58 = (textBlock59 = new TextBlock());
		((ISupportInitialize)textBlock58).BeginInit();
		rootToolTip45.Content = textBlock58;
		TextBlock textBlock60 = (textBlock4 = textBlock59);
		context.PushParent(textBlock4);
		TextBlock textBlock61 = textBlock4;
		textBlock61.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.AudioSettings;
		StaticResourceExtension staticResourceExtension23 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj36 = staticResourceExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock61, obj36);
		textBlock61.FontWeight = (FontWeight)450;
		textBlock61.FontSize = 14.0;
		textBlock61.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock61.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock60).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip44).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton44).EndInit();
		global::Avalonia.Controls.Controls children27 = stackPanel5.Children;
		Rectangle rectangle7;
		Rectangle rectangle6 = (rectangle7 = new Rectangle());
		((ISupportInitialize)rectangle6).BeginInit();
		children27.Add(rectangle6);
		Rectangle rectangle8 = (rectangle4 = rectangle7);
		context.PushParent(rectangle4);
		Rectangle rectangle9 = rectangle4;
		rectangle9.Name = "Separator2";
		obj = rectangle9;
		context.AvaloniaNameScope.Register("Separator2", obj);
		rectangle9.Width = 0.5;
		rectangle9.Height = 25.0;
		StyledProperty<IBrush?> fillProperty2 = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension16 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding16 = dynamicResourceExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle9, fillProperty2, binding16);
		rectangle9.Margin = new Thickness(0.0, 0.0, 16.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rectangle8).EndInit();
		global::Avalonia.Controls.Controls children28 = stackPanel5.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children28.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		Button button5 = button4;
		button5.Name = "DisconnectButton";
		obj = button5;
		context.AvaloniaNameScope.Register("DisconnectButton", obj);
		button5.Classes.Add("BorderButton");
		button5.Height = 36.0;
		button5.Width = 124.0;
		StyledProperty<IBrush?> backgroundProperty5 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension17 = new DynamicResourceExtension("Error");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding17 = dynamicResourceExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, backgroundProperty5, binding17);
		button5.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		button5.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<ICommand?> commandProperty7 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension57 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.DisconnectCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension58 = compiledBindingExtension57.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, commandProperty7, compiledBindingExtension58);
		ToolTip.SetPlacement(button5, PlacementMode.Top);
		ToolTip.SetVerticalOffset(button5, -1.0);
		ToolTip.SetShowDelay(button5, 0);
		RootToolTip rootToolTip47;
		RootToolTip rootToolTip46 = (rootToolTip47 = new RootToolTip());
		((ISupportInitialize)rootToolTip46).BeginInit();
		ToolTip.SetTip(button5, rootToolTip46);
		RootToolTip rootToolTip48 = (rootToolTip4 = rootToolTip47);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip49 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip49, PlacementMode.Top);
		TextBlock textBlock63;
		TextBlock textBlock62 = (textBlock63 = new TextBlock());
		((ISupportInitialize)textBlock62).BeginInit();
		rootToolTip49.Content = textBlock62;
		TextBlock textBlock64 = (textBlock4 = textBlock63);
		context.PushParent(textBlock4);
		TextBlock textBlock65 = textBlock4;
		textBlock65.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Disconnect;
		StaticResourceExtension staticResourceExtension24 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj37 = staticResourceExtension24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock65, obj37);
		textBlock65.FontWeight = (FontWeight)450;
		textBlock65.FontSize = 14.0;
		textBlock65.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock65.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock64).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip48).EndInit();
		StackPanel stackPanel11;
		StackPanel stackPanel10 = (stackPanel11 = new StackPanel());
		((ISupportInitialize)stackPanel10).BeginInit();
		button5.Content = stackPanel10;
		StackPanel stackPanel12 = (stackPanel4 = stackPanel11);
		context.PushParent(stackPanel4);
		StackPanel stackPanel13 = stackPanel4;
		stackPanel13.Orientation = Orientation.Horizontal;
		stackPanel13.Margin = new Thickness(10.0, 8.0, 10.0, 8.0);
		stackPanel13.HorizontalAlignment = HorizontalAlignment.Center;
		global::Avalonia.Controls.Controls children29 = stackPanel13.Children;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		children29.Add(rootSvgImage);
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage5 = rootSvgImage4;
		StyledProperty<string?> svgPathProperty12 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension18 = new DynamicResourceExtension("DeclineCallSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding18 = dynamicResourceExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage5, svgPathProperty12, binding18);
		rootSvgImage5.Width = 20.0;
		rootSvgImage5.Height = 20.0;
		rootSvgImage5.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		global::Avalonia.Controls.Controls children30 = stackPanel13.Children;
		TextBlock textBlock67;
		TextBlock textBlock66 = (textBlock67 = new TextBlock());
		((ISupportInitialize)textBlock66).BeginInit();
		children30.Add(textBlock66);
		TextBlock textBlock68 = (textBlock4 = textBlock67);
		context.PushParent(textBlock4);
		TextBlock textBlock69 = textBlock4;
		textBlock69.Name = "DisconnectText";
		obj = textBlock69;
		context.AvaloniaNameScope.Register("DisconnectText", obj);
		textBlock69.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Disconnect;
		textBlock69.FontSize = 14.0;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension19 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding19 = dynamicResourceExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock69, foregroundProperty, binding19);
		StaticResourceExtension staticResourceExtension25 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj38 = staticResourceExtension25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock69, obj38);
		textBlock69.FontWeight = FontWeight.Medium;
		textBlock69.Margin = new Thickness(10.0, 0.0, 0.0, 0.0);
		textBlock69.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock68).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel12).EndInit();
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		global::Avalonia.Controls.Controls children31 = dockPanel4.Children;
		StackPanel stackPanel15;
		StackPanel stackPanel14 = (stackPanel15 = new StackPanel());
		((ISupportInitialize)stackPanel14).BeginInit();
		children31.Add(stackPanel14);
		StackPanel stackPanel16 = (stackPanel4 = stackPanel15);
		context.PushParent(stackPanel4);
		StackPanel stackPanel17 = stackPanel4;
		stackPanel17.Orientation = Orientation.Horizontal;
		stackPanel17.VerticalAlignment = VerticalAlignment.Center;
		stackPanel17.HorizontalAlignment = HorizontalAlignment.Left;
		global::Avalonia.Controls.Controls children32 = stackPanel17.Children;
		RootImageLoader rootImageLoader2;
		RootImageLoader rootImageLoader = (rootImageLoader2 = new RootImageLoader());
		((ISupportInitialize)rootImageLoader).BeginInit();
		children32.Add(rootImageLoader);
		RootImageLoader rootImageLoader4;
		RootImageLoader rootImageLoader3 = (rootImageLoader4 = rootImageLoader2);
		context.PushParent(rootImageLoader4);
		rootImageLoader4.Width = 32.0;
		rootImageLoader4.Height = 32.0;
		rootImageLoader4.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<IBrush?> backgroundProperty6 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension20 = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding20 = dynamicResourceExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, backgroundProperty6, binding20);
		StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension59 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.ProfilePictureAsyncBitmapWrapper!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
		context.ProvideTargetProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension60 = compiledBindingExtension59.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, sourceProperty, compiledBindingExtension60);
		rootImageLoader4.LoadingPlaceholderSize = 15.0;
		rootImageLoader4.Stretch = Stretch.UniformToFill;
		context.PopParent();
		((ISupportInitialize)rootImageLoader3).EndInit();
		global::Avalonia.Controls.Controls children33 = stackPanel17.Children;
		TextBlock textBlock71;
		TextBlock textBlock70 = (textBlock71 = new TextBlock());
		((ISupportInitialize)textBlock70).BeginInit();
		children33.Add(textBlock70);
		TextBlock textBlock72 = (textBlock4 = textBlock71);
		context.PushParent(textBlock4);
		TextBlock textBlock73 = textBlock4;
		textBlock73.Name = "NameTextBlock";
		obj = textBlock73;
		context.AvaloniaNameScope.Register("NameTextBlock", obj);
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension61 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.UserInfoService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IUserInfoService,RootApp.Client.CoreDomain.SessionUser!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.User.GlobalUser,RootApp.Client.CoreDomain.UserName!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension62 = compiledBindingExtension61.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock73, textProperty, compiledBindingExtension62);
		textBlock73.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock73.FontSize = 14.0;
		StaticResourceExtension staticResourceExtension26 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj39 = staticResourceExtension26.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock73, obj39);
		textBlock73.FontWeight = FontWeight.Medium;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension21 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding21 = dynamicResourceExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock73, foregroundProperty2, binding21);
		textBlock73.VerticalAlignment = VerticalAlignment.Center;
		textBlock73.Margin = new Thickness(12.0, 0.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)textBlock72).EndInit();
		global::Avalonia.Controls.Controls children34 = stackPanel17.Children;
		Button button7;
		Button button6 = (button7 = new Button());
		((ISupportInitialize)button6).BeginInit();
		children34.Add(button6);
		Button button8 = (button4 = button7);
		context.PushParent(button4);
		Button button9 = button4;
		button9.Name = "ConnectionBorder";
		obj = button9;
		context.AvaloniaNameScope.Register("ConnectionBorder", obj);
		button9.Classes.Add("ListBorderButton");
		button9.Margin = new Thickness(20.0, 0.0, 0.0, 0.0);
		button9.Height = 35.0;
		button9.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		StyledProperty<IBrush?> backgroundProperty7 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension22 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding22 = dynamicResourceExtension22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, backgroundProperty7, binding22);
		StyledProperty<IBrush?> borderBrushProperty3 = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension23 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding23 = dynamicResourceExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, borderBrushProperty3, binding23);
		button9.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<ICommand?> commandProperty8 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension63 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.FocusChannelCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension64 = compiledBindingExtension63.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, commandProperty8, compiledBindingExtension64);
		StackPanel stackPanel19;
		StackPanel stackPanel18 = (stackPanel19 = new StackPanel());
		((ISupportInitialize)stackPanel18).BeginInit();
		button9.Content = stackPanel18;
		StackPanel stackPanel20 = (stackPanel4 = stackPanel19);
		context.PushParent(stackPanel4);
		StackPanel stackPanel21 = stackPanel4;
		stackPanel21.Name = "ConnectionButtonContent";
		obj = stackPanel21;
		context.AvaloniaNameScope.Register("ConnectionButtonContent", obj);
		stackPanel21.Orientation = Orientation.Horizontal;
		stackPanel21.Margin = new Thickness(14.0, 0.0, 14.0, 0.0);
		global::Avalonia.Controls.Controls children35 = stackPanel21.Children;
		RootSvgImage rootSvgImage7;
		RootSvgImage rootSvgImage6 = (rootSvgImage7 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage6).BeginInit();
		children35.Add(rootSvgImage6);
		RootSvgImage rootSvgImage8 = (rootSvgImage4 = rootSvgImage7);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage9 = rootSvgImage4;
		rootSvgImage9.Name = "ConnectionImage";
		obj = rootSvgImage9;
		context.AvaloniaNameScope.Register("ConnectionImage", obj);
		rootSvgImage9.Width = 13.0;
		rootSvgImage9.Height = 12.0;
		rootSvgImage9.Margin = new Thickness(0.0, 0.0, 10.0, 0.0);
		StyledProperty<string?> svgPathProperty13 = RootSvgImage.SvgPathProperty;
		MultiBinding multiBinding22 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding23 = multiBinding2;
		StaticResourceExtension staticResourceExtension27 = new StaticResourceExtension("VoiceBarConnectionSvgConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj40 = staticResourceExtension27.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding23.Converter = (IMultiValueConverter)obj40;
		IList<IBinding> bindings25 = multiBinding23.Bindings;
		CompiledBindingExtension compiledBindingExtension65 = new CompiledBindingExtension();
		compiledBindingExtension65.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.VoiceCallConnectionStatus!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension65.FallbackValue = "";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item25 = compiledBindingExtension65.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings25.Add(item25);
		IList<IBinding> bindings26 = multiBinding23.Bindings;
		CompiledBindingExtension compiledBindingExtension66 = new CompiledBindingExtension();
		compiledBindingExtension66.Path = new CompiledBindingPathBuilder(1).Property(Application.ActualThemeVariantProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
		compiledBindingExtension66.Source = Application.Current;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item26 = compiledBindingExtension66.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings26.Add(item26);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgImage9, svgPathProperty13, multiBinding22);
		context.PopParent();
		((ISupportInitialize)rootSvgImage8).EndInit();
		global::Avalonia.Controls.Controls children36 = stackPanel21.Children;
		TextBlock textBlock75;
		TextBlock textBlock74 = (textBlock75 = new TextBlock());
		((ISupportInitialize)textBlock74).BeginInit();
		children36.Add(textBlock74);
		TextBlock textBlock76 = (textBlock4 = textBlock75);
		context.PushParent(textBlock4);
		TextBlock textBlock77 = textBlock4;
		textBlock77.Name = "ConnectionTextBlock";
		obj = textBlock77;
		context.AvaloniaNameScope.Register("ConnectionTextBlock", obj);
		StaticResourceExtension staticResourceExtension28 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj41 = staticResourceExtension28.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock77, obj41);
		textBlock77.FontWeight = FontWeight.Bold;
		textBlock77.FontSize = 12.0;
		textBlock77.VerticalAlignment = VerticalAlignment.Center;
		textBlock77.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock77.Margin = new Thickness(0.0, 0.0, 12.0, 0.0);
		StyledProperty<string?> textProperty2 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension68;
		CompiledBindingExtension compiledBindingExtension67 = (compiledBindingExtension68 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.VoiceCallConnectionStatus!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build()));
		context.PushParent(compiledBindingExtension68);
		StaticResourceExtension staticResourceExtension29 = new StaticResourceExtension("VoiceBarConnectionTextConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj42 = staticResourceExtension29.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension68.Converter = (IValueConverter)obj42;
		compiledBindingExtension68.FallbackValue = "";
		context.PopParent();
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension69 = compiledBindingExtension67.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock77, textProperty2, compiledBindingExtension69);
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		MultiBinding multiBinding24 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding25 = multiBinding2;
		StaticResourceExtension staticResourceExtension30 = new StaticResourceExtension("VoiceBarConnectionForegroundConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj43 = staticResourceExtension30.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding25.Converter = (IMultiValueConverter)obj43;
		IList<IBinding> bindings27 = multiBinding25.Bindings;
		CompiledBindingExtension compiledBindingExtension70 = new CompiledBindingExtension();
		compiledBindingExtension70.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.VoiceCallConnectionStatus!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension70.FallbackValue = "";
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item27 = compiledBindingExtension70.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings27.Add(item27);
		IList<IBinding> bindings28 = multiBinding25.Bindings;
		CompiledBindingExtension compiledBindingExtension71 = new CompiledBindingExtension();
		compiledBindingExtension71.Path = new CompiledBindingPathBuilder(1).Property(Application.ActualThemeVariantProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
		compiledBindingExtension71.Source = Application.Current;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item28 = compiledBindingExtension71.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings28.Add(item28);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(textBlock77, foregroundProperty3, multiBinding24);
		context.PopParent();
		((ISupportInitialize)textBlock76).EndInit();
		global::Avalonia.Controls.Controls children37 = stackPanel21.Children;
		Rectangle rectangle11;
		Rectangle rectangle10 = (rectangle11 = new Rectangle());
		((ISupportInitialize)rectangle10).BeginInit();
		children37.Add(rectangle10);
		Rectangle rectangle12 = (rectangle4 = rectangle11);
		context.PushParent(rectangle4);
		Rectangle rectangle13 = rectangle4;
		rectangle13.Name = "ConnectionSeparator1";
		obj = rectangle13;
		context.AvaloniaNameScope.Register("ConnectionSeparator1", obj);
		rectangle13.Width = 0.5;
		rectangle13.Height = 18.0;
		StyledProperty<IBrush?> fillProperty3 = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension24 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding24 = dynamicResourceExtension24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle13, fillProperty3, binding24);
		rectangle13.Margin = new Thickness(0.0, 0.0, 12.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rectangle12).EndInit();
		global::Avalonia.Controls.Controls children38 = stackPanel21.Children;
		RootSvgImage rootSvgImage11;
		RootSvgImage rootSvgImage10 = (rootSvgImage11 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage10).BeginInit();
		children38.Add(rootSvgImage10);
		RootSvgImage rootSvgImage12 = (rootSvgImage4 = rootSvgImage11);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage13 = rootSvgImage4;
		rootSvgImage13.Name = "VoiceImage";
		obj = rootSvgImage13;
		context.AvaloniaNameScope.Register("VoiceImage", obj);
		rootSvgImage13.Opacity = 0.6;
		rootSvgImage13.Width = 22.0;
		rootSvgImage13.Height = 22.0;
		StyledProperty<string?> svgPathProperty14 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension25 = new DynamicResourceExtension("VoiceSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding25 = dynamicResourceExtension25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage13, svgPathProperty14, binding25);
		rootSvgImage13.Margin = new Thickness(0.0, 0.0, 10.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rootSvgImage12).EndInit();
		global::Avalonia.Controls.Controls children39 = stackPanel21.Children;
		TextBlock textBlock79;
		TextBlock textBlock78 = (textBlock79 = new TextBlock());
		((ISupportInitialize)textBlock78).BeginInit();
		children39.Add(textBlock78);
		TextBlock textBlock80 = (textBlock4 = textBlock79);
		context.PushParent(textBlock4);
		TextBlock textBlock81 = textBlock4;
		textBlock81.Name = "ChannelNameTextBlock";
		obj = textBlock81;
		context.AvaloniaNameScope.Register("ChannelNameTextBlock", obj);
		StaticResourceExtension staticResourceExtension31 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj44 = staticResourceExtension31.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock81, obj44);
		textBlock81.FontWeight = FontWeight.Bold;
		textBlock81.FontSize = 12.0;
		StyledProperty<IBrush?> foregroundProperty4 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension26 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding26 = dynamicResourceExtension26.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock81, foregroundProperty4, binding26);
		StyledProperty<string?> textProperty3 = TextBlock.TextProperty;
		CompiledBindingExtension obj45 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.MessageContainer!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.IMessageContainer,RootApp.Client.CoreDomain.Name!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = ""
		};
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension72 = obj45.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock81, textProperty3, compiledBindingExtension72);
		textBlock81.VerticalAlignment = VerticalAlignment.Center;
		textBlock81.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock81.TextTrimming = TextTrimming.CharacterEllipsis;
		textBlock81.MaxWidth = 200.0;
		context.PopParent();
		((ISupportInitialize)textBlock80).EndInit();
		global::Avalonia.Controls.Controls children40 = stackPanel21.Children;
		StackPanel stackPanel23;
		StackPanel stackPanel22 = (stackPanel23 = new StackPanel());
		((ISupportInitialize)stackPanel22).BeginInit();
		children40.Add(stackPanel22);
		StackPanel stackPanel24 = (stackPanel4 = stackPanel23);
		context.PushParent(stackPanel4);
		StackPanel stackPanel25 = stackPanel4;
		stackPanel25.Name = "MediaStreamingContent";
		obj = stackPanel25;
		context.AvaloniaNameScope.Register("MediaStreamingContent", obj);
		stackPanel25.Orientation = Orientation.Horizontal;
		stackPanel25.Margin = new Thickness(12.0, 0.0, 0.0, 0.0);
		StyledProperty<bool> isVisibleProperty19 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj46 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.HasMediaStreaming!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "False"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension73 = obj46.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(stackPanel25, isVisibleProperty19, compiledBindingExtension73);
		global::Avalonia.Controls.Controls children41 = stackPanel25.Children;
		Rectangle rectangle15;
		Rectangle rectangle14 = (rectangle15 = new Rectangle());
		((ISupportInitialize)rectangle14).BeginInit();
		children41.Add(rectangle14);
		Rectangle rectangle16 = (rectangle4 = rectangle15);
		context.PushParent(rectangle4);
		Rectangle rectangle17 = rectangle4;
		rectangle17.Width = 0.5;
		rectangle17.Height = 18.0;
		StyledProperty<IBrush?> fillProperty4 = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension27 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding27 = dynamicResourceExtension27.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle17, fillProperty4, binding27);
		context.PopParent();
		((ISupportInitialize)rectangle16).EndInit();
		global::Avalonia.Controls.Controls children42 = stackPanel25.Children;
		Ellipse ellipse2;
		Ellipse ellipse = (ellipse2 = new Ellipse());
		((ISupportInitialize)ellipse).BeginInit();
		children42.Add(ellipse);
		Ellipse ellipse4;
		Ellipse ellipse3 = (ellipse4 = ellipse2);
		context.PushParent(ellipse4);
		StyledProperty<IBrush?> fillProperty5 = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension28 = new DynamicResourceExtension("Error");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding28 = dynamicResourceExtension28.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(ellipse4, fillProperty5, binding28);
		ellipse4.Margin = new Thickness(12.0, 0.0, 0.0, 0.0);
		ellipse4.Width = 12.0;
		ellipse4.Height = 12.0;
		context.PopParent();
		((ISupportInitialize)ellipse3).EndInit();
		global::Avalonia.Controls.Controls children43 = stackPanel25.Children;
		TextBlock textBlock83;
		TextBlock textBlock82 = (textBlock83 = new TextBlock());
		((ISupportInitialize)textBlock82).BeginInit();
		children43.Add(textBlock82);
		TextBlock textBlock84 = (textBlock4 = textBlock83);
		context.PushParent(textBlock4);
		TextBlock textBlock85 = textBlock4;
		StaticResourceExtension staticResourceExtension32 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj47 = staticResourceExtension32.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock85, obj47);
		textBlock85.FontWeight = FontWeight.Bold;
		textBlock85.FontSize = 12.0;
		textBlock85.Margin = new Thickness(8.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty5 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension29 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding29 = dynamicResourceExtension29.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock85, foregroundProperty5, binding29);
		textBlock85.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.MediaStreaming;
		textBlock85.VerticalAlignment = VerticalAlignment.Center;
		textBlock85.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)textBlock84).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel24).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel20).EndInit();
		context.PopParent();
		((ISupportInitialize)button8).EndInit();
		global::Avalonia.Controls.Controls children44 = stackPanel17.Children;
		RootSvgButton rootSvgButton47;
		RootSvgButton rootSvgButton46 = (rootSvgButton47 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton46).BeginInit();
		children44.Add(rootSvgButton46);
		RootSvgButton rootSvgButton48 = (rootSvgButton4 = rootSvgButton47);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton49 = rootSvgButton4;
		rootSvgButton49.Name = "PopoutButton";
		obj = rootSvgButton49;
		context.AvaloniaNameScope.Register("PopoutButton", obj);
		rootSvgButton49.Classes.Add("SvgDimmedButton");
		rootSvgButton49.Background = new ImmutableSolidColorBrush(16777215u);
		rootSvgButton49.Width = 32.0;
		rootSvgButton49.Height = 32.0;
		rootSvgButton49.SvgWidth = 21.0;
		rootSvgButton49.SvgHeight = 21.0;
		rootSvgButton49.Margin = new Thickness(12.0, 0.0, 0.0, 0.0);
		StyledProperty<string> svgPathProperty15 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension30 = new DynamicResourceExtension("PopoutSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding30 = dynamicResourceExtension30.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton49, svgPathProperty15, binding30);
		StyledProperty<ICommand?> commandProperty9 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension74 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.PopoutCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension75 = compiledBindingExtension74.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton49, commandProperty9, compiledBindingExtension75);
		ToolTip.SetPlacement(rootSvgButton49, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgButton49, 1.0);
		ToolTip.SetHorizontalOffset(rootSvgButton49, 0.0);
		ToolTip.SetShowDelay(rootSvgButton49, 0);
		StyledProperty<bool> isVisibleProperty20 = Visual.IsVisibleProperty;
		MultiBinding multiBinding26 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding27 = multiBinding2;
		StaticResourceExtension staticResourceExtension33 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj48 = staticResourceExtension33.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding27.Converter = (IMultiValueConverter)obj48;
		IList<IBinding> bindings29 = multiBinding27.Bindings;
		CompiledBindingExtension obj49 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.HasActiveCall!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item29 = obj49.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings29.Add(item29);
		IList<IBinding> bindings30 = multiBinding27.Bindings;
		CompiledBindingExtension compiledBindingExtension76 = new CompiledBindingExtension();
		compiledBindingExtension76.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.SelfMediaMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension76.Converter = ObjectConverters.IsNotNull;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item30 = compiledBindingExtension76.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings30.Add(item30);
		IList<IBinding> bindings31 = multiBinding27.Bindings;
		CompiledBindingExtension obj50 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Home.VoiceBar.VoiceBarViewModel,RootApp.Client.Avalonia.RootSessionAccessor!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.IRootSessionAccessor,RootApp.Client.CoreDomain.Session!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.RootSession,RootApp.Client.CoreDomain.ActiveMediaRoomService!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Services.IActiveMediaRoomService,RootApp.Client.CoreDomain.ActiveMediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.IsPoppedOut!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item31 = obj50.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings31.Add(item31);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgButton49, isVisibleProperty20, multiBinding26);
		RootToolTip rootToolTip51;
		RootToolTip rootToolTip50 = (rootToolTip51 = new RootToolTip());
		((ISupportInitialize)rootToolTip50).BeginInit();
		ToolTip.SetTip(rootSvgButton49, rootToolTip50);
		RootToolTip rootToolTip52 = (rootToolTip4 = rootToolTip51);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip53 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip53, PlacementMode.Top);
		TextBlock textBlock87;
		TextBlock textBlock86 = (textBlock87 = new TextBlock());
		((ISupportInitialize)textBlock86).BeginInit();
		rootToolTip53.Content = textBlock86;
		TextBlock textBlock88 = (textBlock4 = textBlock87);
		context.PushParent(textBlock4);
		TextBlock textBlock89 = textBlock4;
		textBlock89.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.OpenInNewWindow;
		StaticResourceExtension staticResourceExtension34 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj51 = staticResourceExtension34.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock89, obj51);
		textBlock89.FontWeight = (FontWeight)450;
		textBlock89.FontSize = 14.0;
		textBlock89.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock89.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock88).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip52).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgButton48).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel16).EndInit();
		context.PopParent();
		((ISupportInitialize)dockPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void !XamlIlPopulateTrampoline(VoiceBarView P_0)
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
