using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
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
using Avalonia.Platform;
using Avalonia.Styling;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Helpers.Overlay;
using RootApp.Client.Avalonia.Helpers.Screens;
using RootApp.Client.Avalonia.Resources.Converters.MediaRoomMember;

namespace RootApp.Client.Avalonia.UI.Overlay;

public class OverlayWindow : Window
{
	private enum SnapZone
	{
		None,
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight
	}

	[CompilerGenerated]
	private class XamlClosure_188
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<OverlayWindow> context = CreateContext(P_0);
			return new MediaRoomMemberSpeakingBorderOpacityConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<OverlayWindow> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<OverlayWindow> context = new CompiledAvaloniaXaml.XamlIlContext.Context<OverlayWindow>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Overlay/OverlayWindow.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Overlay/OverlayWindow.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (OverlayWindow)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<OverlayWindow> context = CreateContext(P_0);
			return new MediaRoomMemberSpeakingOpacityConverter();
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<OverlayWindow> context = CreateContext(P_0);
			return new OverlayVoiceUserVisibilityConverter();
		}

		public static object Build_4(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<OverlayWindow> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((AvaloniaObject)obj).SetValue(StackPanel.SpacingProperty, 2.0, BindingPriority.Template);
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static object Build_5(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<OverlayWindow> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			StackPanel stackPanel = (StackPanel)obj;
			context.PushParent(stackPanel);
			StackPanel stackPanel2 = stackPanel;
			stackPanel2.Orientation = Orientation.Horizontal;
			stackPanel2.Margin = new Thickness(4.0, 3.0, 4.0, 3.0);
			stackPanel2.Spacing = 6.0;
			StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
			MultiBinding multiBinding2;
			MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
			context.PushParent(multiBinding2);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("VoiceUserVisibilityConverter");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
			object? obj2 = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			multiBinding2.Converter = (IMultiValueConverter)obj2;
			IList<IBinding> bindings = multiBinding2.Bindings;
			CompiledBindingExtension obj3 = new CompiledBindingExtension
			{
				Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayVoiceUser,RootApp.Client.Avalonia.IsSpeaking!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
			};
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
			CompiledBindingExtension item = obj3.ProvideValue(context);
			context.ProvideTargetProperty = null;
			bindings.Add(item);
			IList<IBinding> bindings2 = multiBinding2.Bindings;
			CompiledBindingExtension obj4 = new CompiledBindingExtension
			{
				Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "MainGrid").Property(StyledElement.DataContextProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.OnlyShowSpeaking!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
					.Build()
			};
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
			CompiledBindingExtension item2 = obj4.ProvideValue(context);
			context.ProvideTargetProperty = null;
			bindings2.Add(item2);
			context.PopParent();
			AvaloniaObjectExtensions.Bind(stackPanel2, isVisibleProperty, multiBinding);
			global::Avalonia.Controls.Controls children = stackPanel2.Children;
			Grid grid2;
			Grid grid = (grid2 = new Grid());
			((ISupportInitialize)grid).BeginInit();
			children.Add(grid);
			Grid grid4;
			Grid grid3 = (grid4 = grid2);
			context.PushParent(grid4);
			grid4.Width = 26.0;
			grid4.Height = 26.0;
			StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
			CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "MainGrid").Property(StyledElement.DataContextProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.ShowAvatars!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build());
			context.ProvideTargetProperty = Visual.IsVisibleProperty;
			CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(grid4, isVisibleProperty2, compiledBindingExtension2);
			global::Avalonia.Controls.Controls children2 = grid4.Children;
			Border border2;
			Border border = (border2 = new Border());
			((ISupportInitialize)border).BeginInit();
			children2.Add(border);
			Border border4;
			Border border3 = (border4 = border2);
			context.PushParent(border4);
			border4.CornerRadius = new CornerRadius(5.0, 5.0, 5.0, 5.0);
			StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
			DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BrandSecondary");
			context.ProvideTargetProperty = Border.BorderBrushProperty;
			IBinding binding = dynamicResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(border4, borderBrushProperty, binding);
			border4.BorderThickness = new Thickness(2.0, 2.0, 2.0, 2.0);
			StyledProperty<double> opacityProperty = Visual.OpacityProperty;
			CompiledBindingExtension compiledBindingExtension4;
			CompiledBindingExtension compiledBindingExtension3 = (compiledBindingExtension4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayVoiceUser,RootApp.Client.Avalonia.IsSpeaking!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
			context.PushParent(compiledBindingExtension4);
			CompiledBindingExtension compiledBindingExtension5 = compiledBindingExtension4;
			StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("SpeakingBorderOpacityConverter");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
			object? obj5 = staticResourceExtension2.ProvideValue(context);
			context.ProvideTargetProperty = null;
			compiledBindingExtension5.Converter = (IValueConverter)obj5;
			context.PopParent();
			context.ProvideTargetProperty = Visual.OpacityProperty;
			CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension3.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(border4, opacityProperty, compiledBindingExtension6);
			context.PopParent();
			((ISupportInitialize)border3).EndInit();
			global::Avalonia.Controls.Controls children3 = grid4.Children;
			RootImageLoader rootImageLoader2;
			RootImageLoader rootImageLoader = (rootImageLoader2 = new RootImageLoader());
			((ISupportInitialize)rootImageLoader).BeginInit();
			children3.Add(rootImageLoader);
			RootImageLoader rootImageLoader4;
			RootImageLoader rootImageLoader3 = (rootImageLoader4 = rootImageLoader2);
			context.PushParent(rootImageLoader4);
			rootImageLoader4.Width = 22.0;
			rootImageLoader4.Height = 22.0;
			rootImageLoader4.CornerRadius = new CornerRadius(4.0, 4.0, 4.0, 4.0);
			StyledProperty<IBrush?> backgroundProperty = TemplatedControl.BackgroundProperty;
			DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("HighlightNormal");
			context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
			IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(rootImageLoader4, backgroundProperty, binding2);
			StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
			CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayVoiceUser,RootApp.Client.Avalonia.AvatarBitmap!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
			context.ProvideTargetProperty = RootImageLoader.SourceProperty;
			CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(rootImageLoader4, sourceProperty, compiledBindingExtension8);
			rootImageLoader4.LoadingPlaceholderSize = 10.0;
			rootImageLoader4.Stretch = Stretch.UniformToFill;
			StyledProperty<double> opacityProperty2 = Visual.OpacityProperty;
			CompiledBindingExtension compiledBindingExtension9 = (compiledBindingExtension4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayVoiceUser,RootApp.Client.Avalonia.IsSpeaking!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
			context.PushParent(compiledBindingExtension4);
			CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension4;
			StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("SpeakingOpacityConverter");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
			object? obj6 = staticResourceExtension3.ProvideValue(context);
			context.ProvideTargetProperty = null;
			compiledBindingExtension10.Converter = (IValueConverter)obj6;
			context.PopParent();
			context.ProvideTargetProperty = Visual.OpacityProperty;
			CompiledBindingExtension compiledBindingExtension11 = compiledBindingExtension9.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(rootImageLoader4, opacityProperty2, compiledBindingExtension11);
			context.PopParent();
			((ISupportInitialize)rootImageLoader3).EndInit();
			context.PopParent();
			((ISupportInitialize)grid3).EndInit();
			global::Avalonia.Controls.Controls children4 = stackPanel2.Children;
			TextBlock textBlock2;
			TextBlock textBlock = (textBlock2 = new TextBlock());
			((ISupportInitialize)textBlock).BeginInit();
			children4.Add(textBlock);
			TextBlock textBlock4;
			TextBlock textBlock3 = (textBlock4 = textBlock2);
			context.PushParent(textBlock4);
			textBlock4.VerticalAlignment = VerticalAlignment.Center;
			textBlock4.FontSize = 14.0;
			StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("RootFont");
			context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
			object? obj7 = staticResourceExtension4.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock4, obj7);
			textBlock4.FontWeight = FontWeight.Medium;
			StyledProperty<string?> textProperty = TextBlock.TextProperty;
			CompiledBindingExtension compiledBindingExtension12 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayVoiceUser,RootApp.Client.Avalonia.DisplayName!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
			context.ProvideTargetProperty = TextBlock.TextProperty;
			CompiledBindingExtension compiledBindingExtension13 = compiledBindingExtension12.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(textBlock4, textProperty, compiledBindingExtension13);
			StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
			DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TextPrimary");
			context.ProvideTargetProperty = TextBlock.ForegroundProperty;
			IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(textBlock4, foregroundProperty, binding3);
			textBlock4.TextTrimming = TextTrimming.CharacterEllipsis;
			textBlock4.MaxWidth = 180.0;
			StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
			CompiledBindingExtension compiledBindingExtension14 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "MainGrid").Property(StyledElement.DataContextProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.ShowNames!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build());
			context.ProvideTargetProperty = Visual.IsVisibleProperty;
			CompiledBindingExtension compiledBindingExtension15 = compiledBindingExtension14.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(textBlock4, isVisibleProperty3, compiledBindingExtension15);
			StyledProperty<double> opacityProperty3 = Visual.OpacityProperty;
			CompiledBindingExtension compiledBindingExtension16 = (compiledBindingExtension4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayVoiceUser,RootApp.Client.Avalonia.IsSpeaking!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
			context.PushParent(compiledBindingExtension4);
			CompiledBindingExtension compiledBindingExtension17 = compiledBindingExtension4;
			StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("SpeakingOpacityConverter");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
			object? obj8 = staticResourceExtension5.ProvideValue(context);
			context.ProvideTargetProperty = null;
			compiledBindingExtension17.Converter = (IValueConverter)obj8;
			context.PopParent();
			context.ProvideTargetProperty = Visual.OpacityProperty;
			CompiledBindingExtension compiledBindingExtension18 = compiledBindingExtension16.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(textBlock4, opacityProperty3, compiledBindingExtension18);
			context.PopParent();
			((ISupportInitialize)textBlock3).EndInit();
			global::Avalonia.Controls.Controls children5 = stackPanel2.Children;
			StackPanel stackPanel4;
			StackPanel stackPanel3 = (stackPanel4 = new StackPanel());
			((ISupportInitialize)stackPanel3).BeginInit();
			children5.Add(stackPanel3);
			StackPanel stackPanel5 = (stackPanel = stackPanel4);
			context.PushParent(stackPanel);
			StackPanel stackPanel6 = stackPanel;
			stackPanel6.Orientation = Orientation.Horizontal;
			stackPanel6.Spacing = 6.0;
			stackPanel6.VerticalAlignment = VerticalAlignment.Center;
			stackPanel6.Height = 20.0;
			global::Avalonia.Controls.Controls children6 = stackPanel6.Children;
			RootSvgImage rootSvgImage2;
			RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
			((ISupportInitialize)rootSvgImage).BeginInit();
			children6.Add(rootSvgImage);
			RootSvgImage rootSvgImage4;
			RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
			context.PushParent(rootSvgImage4);
			RootSvgImage rootSvgImage5 = rootSvgImage4;
			rootSvgImage5.Width = 15.0;
			rootSvgImage5.Height = 15.0;
			StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
			DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("MicrophoneMutedSVG");
			context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
			IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(rootSvgImage5, svgPathProperty, binding4);
			StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
			CompiledBindingExtension compiledBindingExtension19 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayVoiceUser,RootApp.Client.Avalonia.IsMuted!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
			context.ProvideTargetProperty = Visual.IsVisibleProperty;
			CompiledBindingExtension compiledBindingExtension20 = compiledBindingExtension19.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(rootSvgImage5, isVisibleProperty4, compiledBindingExtension20);
			context.PopParent();
			((ISupportInitialize)rootSvgImage3).EndInit();
			global::Avalonia.Controls.Controls children7 = stackPanel6.Children;
			RootSvgImage rootSvgImage7;
			RootSvgImage rootSvgImage6 = (rootSvgImage7 = new RootSvgImage());
			((ISupportInitialize)rootSvgImage6).BeginInit();
			children7.Add(rootSvgImage6);
			RootSvgImage rootSvgImage8 = (rootSvgImage4 = rootSvgImage7);
			context.PushParent(rootSvgImage4);
			RootSvgImage rootSvgImage9 = rootSvgImage4;
			rootSvgImage9.Width = 16.0;
			rootSvgImage9.Height = 16.0;
			StyledProperty<string?> svgPathProperty2 = RootSvgImage.SvgPathProperty;
			DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("HeadphonesMutedSVG");
			context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
			IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(rootSvgImage9, svgPathProperty2, binding5);
			StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
			CompiledBindingExtension compiledBindingExtension21 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayVoiceUser,RootApp.Client.Avalonia.IsDeafened!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
			context.ProvideTargetProperty = Visual.IsVisibleProperty;
			CompiledBindingExtension compiledBindingExtension22 = compiledBindingExtension21.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(rootSvgImage9, isVisibleProperty5, compiledBindingExtension22);
			context.PopParent();
			((ISupportInitialize)rootSvgImage8).EndInit();
			context.PopParent();
			((ISupportInitialize)stackPanel5).EndInit();
			context.PopParent();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static object Build_6(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<OverlayWindow> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((AvaloniaObject)obj).SetValue(StackPanel.SpacingProperty, 4.0, BindingPriority.Template);
			((AvaloniaObject)obj).SetValue(Visual.ClipToBoundsProperty, value: false, BindingPriority.Template);
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static object Build_7(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<OverlayWindow> context = CreateContext(P_0);
			context.IntermediateRoot = new OverlayMessageView();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}
	}

	private nint _hwnd;

	private (int X, int Y, int Width, int Height)? _lastBounds;

	private bool _isTopmost;

	private OverlayPosition _voicePosition = OverlayPosition.TopLeft;

	private OverlayPosition _chatPosition = OverlayPosition.TopRight;

	private Border? _voiceWidgetBorder;

	private Border? _chatWidgetBorder;

	private Border? _voiceResizeGrip;

	private Border? _chatResizeGrip;

	private bool _isDraggingVoiceWidget;

	private bool _isDraggingChatWidget;

	private Point _dragStartPoint;

	private Point _widgetStartPosition;

	private TranslateTransform? _voiceWidgetTransform;

	private TranslateTransform? _chatWidgetTransform;

	private bool _isResizing;

	private Point _resizeStartPoint;

	private double _resizeStartScale;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Grid MainGrid;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border WelcomeNotification;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal OverlayActionBar ActionBarControl;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal LayoutTransformControl VoiceWidgetContainer;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border VoiceWidgetBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal StackPanel VoicePanel;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border VoiceResizeGrip;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal LayoutTransformControl ChatWidgetContainer;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border ChatWidgetBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal StackPanel ChatPanel;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border ChatResizeGrip;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	public OverlayPosition VoicePosition
	{
		set
		{
			if (_voicePosition != overlayPosition)
			{
				_voicePosition = overlayPosition;
				UpdateContentAlignment();
			}
		}
	}

	public OverlayPosition ChatPosition
	{
		set
		{
			if (_chatPosition != overlayPosition)
			{
				_chatPosition = overlayPosition;
				UpdateContentAlignment();
			}
		}
	}

	public bool IsInteractiveMode => (base.DataContext as OverlayViewModel)?.IsInteractiveMode ?? false;

	public OverlayWindow()
	{
		InitializeComponent();
		UpdateContentAlignment();
		SetupDragHandlers();
	}

	public OverlayWindow(OverlayViewModel P_0)
		: this()
	{
		base.DataContext = P_0;
		if (P_0.UseCustomVoicePosition)
		{
			ApplyCustomVoicePosition(P_0.VoicePanelX, P_0.VoicePanelY);
		}
		if (P_0.UseCustomChatPosition)
		{
			ApplyCustomChatPosition(P_0.ChatPanelX, P_0.ChatPanelY);
		}
		P_0.ResetPositionsRequested += OnResetPositionsRequested;
	}

	private void SetupDragHandlers()
	{
		_voiceWidgetBorder = this.FindControl<Border>("VoiceWidgetBorder");
		_chatWidgetBorder = this.FindControl<Border>("ChatWidgetBorder");
		if (_voiceWidgetBorder != null)
		{
			_voiceWidgetBorder.PointerPressed += OnVoiceWidgetPointerPressed;
			_voiceWidgetBorder.PointerMoved += OnVoiceWidgetPointerMoved;
			_voiceWidgetBorder.PointerReleased += OnVoiceWidgetPointerReleased;
			_voiceWidgetBorder.PointerCaptureLost += OnVoiceWidgetPointerCaptureLost;
		}
		if (_chatWidgetBorder != null)
		{
			_chatWidgetBorder.PointerPressed += OnChatWidgetPointerPressed;
			_chatWidgetBorder.PointerMoved += OnChatWidgetPointerMoved;
			_chatWidgetBorder.PointerReleased += OnChatWidgetPointerReleased;
			_chatWidgetBorder.PointerCaptureLost += OnChatWidgetPointerCaptureLost;
		}
		_voiceResizeGrip = this.FindControl<Border>("VoiceResizeGrip");
		_chatResizeGrip = this.FindControl<Border>("ChatResizeGrip");
		if (_voiceResizeGrip != null)
		{
			_voiceResizeGrip.PointerPressed += OnResizeGripPointerPressed;
			_voiceResizeGrip.PointerMoved += OnResizeGripPointerMoved;
			_voiceResizeGrip.PointerReleased += OnResizeGripPointerReleased;
			_voiceResizeGrip.PointerCaptureLost += OnResizeGripPointerCaptureLost;
		}
		if (_chatResizeGrip != null)
		{
			_chatResizeGrip.PointerPressed += OnResizeGripPointerPressed;
			_chatResizeGrip.PointerMoved += OnResizeGripPointerMoved;
			_chatResizeGrip.PointerReleased += OnResizeGripPointerReleased;
			_chatResizeGrip.PointerCaptureLost += OnResizeGripPointerCaptureLost;
		}
	}

	private void OnResetPositionsRequested()
	{
		ResetVoiceWidgetPosition();
		ResetChatWidgetPosition();
	}

	private void OnVoiceWidgetPointerPressed(object? sender, PointerPressedEventArgs e)
	{
		if (!IsInteractiveMode || !(sender is Border border) || e.Source is Button)
		{
			return;
		}
		LayoutTransformControl layoutTransformControl = this.FindControl<LayoutTransformControl>("VoiceWidgetContainer");
		if (layoutTransformControl != null)
		{
			_isDraggingVoiceWidget = true;
			_dragStartPoint = e.GetPosition(this);
			TranslateTransform translateTransform = layoutTransformControl.RenderTransform as TranslateTransform;
			if (translateTransform == null)
			{
				translateTransform = (TranslateTransform)(layoutTransformControl.RenderTransform = new TranslateTransform());
			}
			_voiceWidgetTransform = translateTransform;
			_widgetStartPosition = new Point(translateTransform.X, translateTransform.Y);
			e.Pointer.Capture(border);
			e.Handled = true;
			if (base.DataContext is OverlayViewModel overlayViewModel)
			{
				overlayViewModel.IsVoicePanelDragging = true;
			}
			ApplyDragStartEffects(border);
		}
	}

	private void OnVoiceWidgetPointerMoved(object? sender, PointerEventArgs e)
	{
		if (_isDraggingVoiceWidget && _voiceWidgetTransform != null)
		{
			LayoutTransformControl layoutTransformControl = this.FindControl<LayoutTransformControl>("VoiceWidgetContainer");
			if (layoutTransformControl != null)
			{
				Point position = e.GetPosition(this);
				Point point = position - _dragStartPoint;
				double num = _widgetStartPosition.X + point.X;
				double num2 = _widgetStartPosition.Y + point.Y;
				_voiceWidgetTransform.X = num;
				_voiceWidgetTransform.Y = num2;
				SnapZone item = CheckSnapZones(layoutTransformControl, num, num2).Zone;
				UpdateSnapIndicator(item, layoutTransformControl.Bounds.Width, layoutTransformControl.Bounds.Height);
				e.Handled = true;
			}
		}
	}

	private void OnVoiceWidgetPointerReleased(object? sender, PointerReleasedEventArgs e)
	{
		if (_isDraggingVoiceWidget && sender is Border border)
		{
			FinishVoiceWidgetDrag(border);
			e.Pointer.Capture(null);
			e.Handled = true;
		}
	}

	private void OnVoiceWidgetPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
	{
		if (_isDraggingVoiceWidget && sender is Border border)
		{
			FinishVoiceWidgetDrag(border);
		}
	}

	private void FinishVoiceWidgetDrag(Border P_0)
	{
		_isDraggingVoiceWidget = false;
		if (_voiceWidgetTransform == null)
		{
			return;
		}
		LayoutTransformControl layoutTransformControl = this.FindControl<LayoutTransformControl>("VoiceWidgetContainer");
		if (layoutTransformControl != null)
		{
			double x = _voiceWidgetTransform.X;
			double y = _voiceWidgetTransform.Y;
			var (num, num2, _) = CheckSnapZones(layoutTransformControl, x, y);
			AnimateToPosition(_voiceWidgetTransform, num, num2);
			HideSnapIndicator();
			ApplyDragEndEffects(P_0);
			if (base.DataContext is OverlayViewModel overlayViewModel)
			{
				overlayViewModel.IsVoicePanelDragging = false;
				overlayViewModel.UseCustomVoicePosition = true;
				overlayViewModel.VoicePanelX = num;
				overlayViewModel.VoicePanelY = num2;
				overlayViewModel.NotifyVoicePanelPositionChanged();
			}
		}
	}

	private void ResetVoiceWidgetPosition()
	{
		LayoutTransformControl layoutTransformControl = this.FindControl<LayoutTransformControl>("VoiceWidgetContainer");
		if (layoutTransformControl != null)
		{
			if (layoutTransformControl.RenderTransform is TranslateTransform translateTransform)
			{
				AnimateToPosition(translateTransform, 0.0, 0.0);
			}
			else
			{
				TranslateTransform renderTransform = new TranslateTransform();
				layoutTransformControl.RenderTransform = renderTransform;
			}
			if (base.DataContext is OverlayViewModel overlayViewModel)
			{
				overlayViewModel.UseCustomVoicePosition = false;
				overlayViewModel.VoicePanelX = 0.0;
				overlayViewModel.VoicePanelY = 0.0;
				overlayViewModel.NotifyVoicePanelPositionChanged();
			}
		}
	}

	private void ApplyCustomVoicePosition(double P_0, double P_1)
	{
		LayoutTransformControl layoutTransformControl = this.FindControl<LayoutTransformControl>("VoiceWidgetContainer");
		if (layoutTransformControl != null)
		{
			TranslateTransform translateTransform = layoutTransformControl.RenderTransform as TranslateTransform;
			if (translateTransform == null)
			{
				translateTransform = (TranslateTransform)(layoutTransformControl.RenderTransform = new TranslateTransform());
			}
			translateTransform.X = P_0;
			translateTransform.Y = P_1;
			_voiceWidgetTransform = translateTransform;
		}
	}

	private void OnChatWidgetPointerPressed(object? sender, PointerPressedEventArgs e)
	{
		if (!IsInteractiveMode || !(sender is Border border) || e.Source is Button)
		{
			return;
		}
		LayoutTransformControl layoutTransformControl = this.FindControl<LayoutTransformControl>("ChatWidgetContainer");
		if (layoutTransformControl != null)
		{
			_isDraggingChatWidget = true;
			_dragStartPoint = e.GetPosition(this);
			TranslateTransform translateTransform = layoutTransformControl.RenderTransform as TranslateTransform;
			if (translateTransform == null)
			{
				translateTransform = (TranslateTransform)(layoutTransformControl.RenderTransform = new TranslateTransform());
			}
			_chatWidgetTransform = translateTransform;
			_widgetStartPosition = new Point(translateTransform.X, translateTransform.Y);
			e.Pointer.Capture(border);
			e.Handled = true;
			if (base.DataContext is OverlayViewModel overlayViewModel)
			{
				overlayViewModel.IsChatPanelDragging = true;
			}
			ApplyDragStartEffects(border);
		}
	}

	private void OnChatWidgetPointerMoved(object? sender, PointerEventArgs e)
	{
		if (_isDraggingChatWidget && _chatWidgetTransform != null)
		{
			LayoutTransformControl layoutTransformControl = this.FindControl<LayoutTransformControl>("ChatWidgetContainer");
			if (layoutTransformControl != null)
			{
				Point position = e.GetPosition(this);
				Point point = position - _dragStartPoint;
				double num = _widgetStartPosition.X + point.X;
				double num2 = _widgetStartPosition.Y + point.Y;
				_chatWidgetTransform.X = num;
				_chatWidgetTransform.Y = num2;
				SnapZone item = CheckSnapZones(layoutTransformControl, num, num2).Zone;
				UpdateSnapIndicator(item, layoutTransformControl.Bounds.Width, layoutTransformControl.Bounds.Height);
				e.Handled = true;
			}
		}
	}

	private void OnChatWidgetPointerReleased(object? sender, PointerReleasedEventArgs e)
	{
		if (_isDraggingChatWidget && sender is Border border)
		{
			FinishChatWidgetDrag(border);
			e.Pointer.Capture(null);
			e.Handled = true;
		}
	}

	private void OnChatWidgetPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
	{
		if (_isDraggingChatWidget && sender is Border border)
		{
			FinishChatWidgetDrag(border);
		}
	}

	private void FinishChatWidgetDrag(Border P_0)
	{
		_isDraggingChatWidget = false;
		if (_chatWidgetTransform == null)
		{
			return;
		}
		LayoutTransformControl layoutTransformControl = this.FindControl<LayoutTransformControl>("ChatWidgetContainer");
		if (layoutTransformControl != null)
		{
			double x = _chatWidgetTransform.X;
			double y = _chatWidgetTransform.Y;
			var (num, num2, _) = CheckSnapZones(layoutTransformControl, x, y);
			AnimateToPosition(_chatWidgetTransform, num, num2);
			HideSnapIndicator();
			ApplyDragEndEffects(P_0);
			if (base.DataContext is OverlayViewModel overlayViewModel)
			{
				overlayViewModel.IsChatPanelDragging = false;
				overlayViewModel.UseCustomChatPosition = true;
				overlayViewModel.ChatPanelX = num;
				overlayViewModel.ChatPanelY = num2;
				overlayViewModel.NotifyChatPanelPositionChanged();
			}
		}
	}

	private void ResetChatWidgetPosition()
	{
		LayoutTransformControl layoutTransformControl = this.FindControl<LayoutTransformControl>("ChatWidgetContainer");
		if (layoutTransformControl != null)
		{
			if (layoutTransformControl.RenderTransform is TranslateTransform translateTransform)
			{
				AnimateToPosition(translateTransform, 0.0, 0.0);
			}
			else
			{
				TranslateTransform renderTransform = new TranslateTransform();
				layoutTransformControl.RenderTransform = renderTransform;
			}
			if (base.DataContext is OverlayViewModel overlayViewModel)
			{
				overlayViewModel.UseCustomChatPosition = false;
				overlayViewModel.ChatPanelX = 0.0;
				overlayViewModel.ChatPanelY = 0.0;
				overlayViewModel.NotifyChatPanelPositionChanged();
			}
		}
	}

	private void ApplyCustomChatPosition(double P_0, double P_1)
	{
		LayoutTransformControl layoutTransformControl = this.FindControl<LayoutTransformControl>("ChatWidgetContainer");
		if (layoutTransformControl != null)
		{
			TranslateTransform translateTransform = layoutTransformControl.RenderTransform as TranslateTransform;
			if (translateTransform == null)
			{
				translateTransform = (TranslateTransform)(layoutTransformControl.RenderTransform = new TranslateTransform());
			}
			translateTransform.X = P_0;
			translateTransform.Y = P_1;
			_chatWidgetTransform = translateTransform;
		}
	}

	private static void ApplyDragStartEffects(Border P_0)
	{
	}

	private static void ApplyDragEndEffects(Border P_0)
	{
	}

	private static void AnimateToPosition(TranslateTransform P_0, double P_1, double P_2)
	{
		P_0.X = P_1;
		P_0.Y = P_2;
	}

	private void OnResizeGripPointerPressed(object? sender, PointerPressedEventArgs e)
	{
		if (IsInteractiveMode && sender is Border border)
		{
			_isResizing = true;
			_resizeStartPoint = e.GetPosition(this);
			if (base.DataContext is OverlayViewModel overlayViewModel)
			{
				_resizeStartScale = overlayViewModel.Scale;
			}
			e.Pointer.Capture(border);
			e.Handled = true;
		}
	}

	private void OnResizeGripPointerMoved(object? sender, PointerEventArgs e)
	{
		if (_isResizing)
		{
			Point position = e.GetPosition(this);
			Point point = position - _resizeStartPoint;
			double num = (point.X + point.Y) / 200.0;
			double num2 = Math.Clamp(_resizeStartScale + num, 0.5, 1.5);
			num2 = Math.Round(num2 * 20.0) / 20.0;
			if (base.DataContext is OverlayViewModel overlayViewModel)
			{
				overlayViewModel.Scale = num2;
			}
			e.Handled = true;
		}
	}

	private void OnResizeGripPointerReleased(object? sender, PointerReleasedEventArgs e)
	{
		if (_isResizing)
		{
			_isResizing = false;
			e.Pointer.Capture(null);
			e.Handled = true;
		}
	}

	private void OnResizeGripPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
	{
		if (_isResizing)
		{
			_isResizing = false;
		}
	}

	private (double X, double Y, SnapZone Zone) CheckSnapZones(Control P_0, double P_1, double P_2)
	{
		Rect bounds = P_0.Bounds;
		double width = base.Width;
		double height = base.Height;
		HorizontalAlignment horizontalAlignment = P_0.HorizontalAlignment;
		VerticalAlignment verticalAlignment = P_0.VerticalAlignment;
		Thickness margin = P_0.Margin;
		double num = horizontalAlignment switch
		{
			HorizontalAlignment.Left => margin.Left, 
			HorizontalAlignment.Right => width - bounds.Width - margin.Right, 
			_ => (width - bounds.Width) / 2.0, 
		};
		double num2 = verticalAlignment switch
		{
			VerticalAlignment.Top => margin.Top, 
			VerticalAlignment.Bottom => height - bounds.Height - margin.Bottom, 
			_ => (height - bounds.Height) / 2.0, 
		};
		double num3 = num + P_1;
		double num4 = num2 + P_2;
		double num5 = 16.0;
		double num6 = 16.0;
		double num7 = width - bounds.Width - 16.0;
		double num8 = 16.0;
		double num9 = 16.0;
		double num10 = height - bounds.Height - 16.0;
		double num11 = width - bounds.Width - 16.0;
		double num12 = height - bounds.Height - 16.0;
		SnapZone snapZone = SnapZone.None;
		double num13 = P_1;
		double num14 = P_2;
		if (Math.Abs(num3 - num5) < 60.0 && Math.Abs(num4 - num6) < 60.0)
		{
			snapZone = SnapZone.TopLeft;
			num13 = num5 - num;
			num14 = num6 - num2;
		}
		else if (Math.Abs(num3 - num7) < 60.0 && Math.Abs(num4 - num8) < 60.0)
		{
			snapZone = SnapZone.TopRight;
			num13 = num7 - num;
			num14 = num8 - num2;
		}
		else if (Math.Abs(num3 - num9) < 60.0 && Math.Abs(num4 - num10) < 60.0)
		{
			snapZone = SnapZone.BottomLeft;
			num13 = num9 - num;
			num14 = num10 - num2;
		}
		else if (Math.Abs(num3 - num11) < 60.0 && Math.Abs(num4 - num12) < 60.0)
		{
			snapZone = SnapZone.BottomRight;
			num13 = num11 - num;
			num14 = num12 - num2;
		}
		return (X: num13, Y: num14, Zone: snapZone);
	}

	private void UpdateSnapIndicator(SnapZone P_0, double P_1, double P_2)
	{
		if (!(base.DataContext is OverlayViewModel overlayViewModel))
		{
			return;
		}
		if (P_0 == SnapZone.None)
		{
			overlayViewModel.ShowSnapIndicator = false;
			return;
		}
		overlayViewModel.ShowSnapIndicator = true;
		switch (P_0)
		{
		case SnapZone.TopLeft:
			overlayViewModel.SnapIndicatorX = 16.0;
			overlayViewModel.SnapIndicatorY = 16.0;
			overlayViewModel.SnapIndicatorWidth = P_1;
			overlayViewModel.SnapIndicatorHeight = P_2;
			break;
		case SnapZone.TopRight:
			overlayViewModel.SnapIndicatorX = base.Width - P_1 - 16.0;
			overlayViewModel.SnapIndicatorY = 16.0;
			overlayViewModel.SnapIndicatorWidth = P_1;
			overlayViewModel.SnapIndicatorHeight = P_2;
			break;
		case SnapZone.BottomLeft:
			overlayViewModel.SnapIndicatorX = 16.0;
			overlayViewModel.SnapIndicatorY = base.Height - P_2 - 16.0;
			overlayViewModel.SnapIndicatorWidth = P_1;
			overlayViewModel.SnapIndicatorHeight = P_2;
			break;
		case SnapZone.BottomRight:
			overlayViewModel.SnapIndicatorX = base.Width - P_1 - 16.0;
			overlayViewModel.SnapIndicatorY = base.Height - P_2 - 16.0;
			overlayViewModel.SnapIndicatorWidth = P_1;
			overlayViewModel.SnapIndicatorHeight = P_2;
			break;
		}
	}

	private void HideSnapIndicator()
	{
		if (base.DataContext is OverlayViewModel overlayViewModel)
		{
			overlayViewModel.ShowSnapIndicator = false;
		}
	}

	public void SetInteractiveMode(bool P_0)
	{
		if (_hwnd != 0)
		{
			OverlayInterop.SetInteractive(_hwnd, P_0);
			_isTopmost = false;
		}
	}

	private void UpdateContentAlignment()
	{
		LayoutTransformControl layoutTransformControl = this.FindControl<LayoutTransformControl>("VoiceWidgetContainer");
		if (layoutTransformControl != null)
		{
			ApplyPositionToWidget(layoutTransformControl, _voicePosition);
		}
		LayoutTransformControl layoutTransformControl2 = this.FindControl<LayoutTransformControl>("ChatWidgetContainer");
		if (layoutTransformControl2 != null)
		{
			ApplyPositionToWidget(layoutTransformControl2, _chatPosition);
		}
	}

	private static void ApplyPositionToWidget(Control P_0, OverlayPosition P_1)
	{
		switch (P_1)
		{
		case OverlayPosition.TopLeft:
			P_0.HorizontalAlignment = HorizontalAlignment.Left;
			P_0.VerticalAlignment = VerticalAlignment.Top;
			break;
		case OverlayPosition.TopRight:
			P_0.HorizontalAlignment = HorizontalAlignment.Right;
			P_0.VerticalAlignment = VerticalAlignment.Top;
			break;
		case OverlayPosition.BottomLeft:
			P_0.HorizontalAlignment = HorizontalAlignment.Left;
			P_0.VerticalAlignment = VerticalAlignment.Bottom;
			break;
		case OverlayPosition.BottomRight:
			P_0.HorizontalAlignment = HorizontalAlignment.Right;
			P_0.VerticalAlignment = VerticalAlignment.Bottom;
			break;
		}
	}

	protected override void OnOpened(EventArgs P_0)
	{
		base.OnOpened(P_0);
		bool flag = true;
		ConfigureWindowsOverlay();
	}

	protected override void OnClosed(EventArgs P_0)
	{
		base.OnClosed(P_0);
		if (base.DataContext is OverlayViewModel overlayViewModel)
		{
			overlayViewModel.ResetPositionsRequested -= OnResetPositionsRequested;
		}
		if (_voiceWidgetBorder != null)
		{
			_voiceWidgetBorder.PointerPressed -= OnVoiceWidgetPointerPressed;
			_voiceWidgetBorder.PointerMoved -= OnVoiceWidgetPointerMoved;
			_voiceWidgetBorder.PointerReleased -= OnVoiceWidgetPointerReleased;
			_voiceWidgetBorder.PointerCaptureLost -= OnVoiceWidgetPointerCaptureLost;
			_voiceWidgetBorder = null;
		}
		if (_chatWidgetBorder != null)
		{
			_chatWidgetBorder.PointerPressed -= OnChatWidgetPointerPressed;
			_chatWidgetBorder.PointerMoved -= OnChatWidgetPointerMoved;
			_chatWidgetBorder.PointerReleased -= OnChatWidgetPointerReleased;
			_chatWidgetBorder.PointerCaptureLost -= OnChatWidgetPointerCaptureLost;
			_chatWidgetBorder = null;
		}
		if (_voiceResizeGrip != null)
		{
			_voiceResizeGrip.PointerPressed -= OnResizeGripPointerPressed;
			_voiceResizeGrip.PointerMoved -= OnResizeGripPointerMoved;
			_voiceResizeGrip.PointerReleased -= OnResizeGripPointerReleased;
			_voiceResizeGrip.PointerCaptureLost -= OnResizeGripPointerCaptureLost;
			_voiceResizeGrip = null;
		}
		if (_chatResizeGrip != null)
		{
			_chatResizeGrip.PointerPressed -= OnResizeGripPointerPressed;
			_chatResizeGrip.PointerMoved -= OnResizeGripPointerMoved;
			_chatResizeGrip.PointerReleased -= OnResizeGripPointerReleased;
			_chatResizeGrip.PointerCaptureLost -= OnResizeGripPointerCaptureLost;
			_chatResizeGrip = null;
		}
	}

	private void ConfigureWindowsOverlay()
	{
		IPlatformHandle platformHandle = TryGetPlatformHandle();
		if (platformHandle != null)
		{
			_hwnd = platformHandle.Handle;
			OverlayInterop.ConfigureOverlayWindow(_hwnd);
			UpdatePositionToFullscreenGame();
		}
	}

	public void UpdatePositionToFullscreenGame()
	{
		if (_hwnd == 0)
		{
			return;
		}
		(int, int, int, int)? fullscreenGameMonitorBounds = FullScreenDetector.GetFullscreenGameMonitorBounds();
		if (!fullscreenGameMonitorBounds.HasValue)
		{
			OverlayInterop.MakeTopmost(_hwnd);
			return;
		}
		(int, int, int, int)? lastBounds = _lastBounds;
		(int, int, int, int)? tuple = fullscreenGameMonitorBounds;
		bool hasValue = lastBounds.HasValue;
		if (hasValue == tuple.HasValue)
		{
			if (hasValue)
			{
				(int, int, int, int) valueOrDefault = lastBounds.GetValueOrDefault();
				(int, int, int, int) valueOrDefault2 = tuple.GetValueOrDefault();
				if (valueOrDefault.Item1 != valueOrDefault2.Item1 || valueOrDefault.Item2 != valueOrDefault2.Item2 || valueOrDefault.Item3 != valueOrDefault2.Item3 || valueOrDefault.Item4 != valueOrDefault2.Item4)
				{
					goto IL_00c2;
				}
			}
			if (!_isTopmost)
			{
				OverlayInterop.MakeTopmost(_hwnd);
				_isTopmost = true;
			}
			return;
		}
		goto IL_00c2;
		IL_00c2:
		_lastBounds = fullscreenGameMonitorBounds;
		_isTopmost = true;
		var (num, num2, num3, num4) = fullscreenGameMonitorBounds.Value;
		OverlayInterop.MakeTopmostAt(_hwnd, num, num2, num3, num4);
		base.Position = new PixelPoint(num, num2);
		double num5 = ((base.VisualRoot is TopLevel topLevel) ? topLevel.RenderScaling : 1.0);
		base.Width = (double)num3 / num5;
		base.Height = (double)num4 / num5;
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	[ExcludeFromCodeCoverage]
	public void InitializeComponent(bool P_0 = true, bool P_1 = true)
	{
		if (P_0)
		{
			!XamlIlPopulateTrampoline(this);
		}
		INameScope nameScope = this.FindNameScope();
		MainGrid = nameScope?.Find<Grid>("MainGrid");
		WelcomeNotification = nameScope?.Find<Border>("WelcomeNotification");
		ActionBarControl = nameScope?.Find<OverlayActionBar>("ActionBarControl");
		VoiceWidgetContainer = nameScope?.Find<LayoutTransformControl>("VoiceWidgetContainer");
		VoiceWidgetBorder = nameScope?.Find<Border>("VoiceWidgetBorder");
		VoicePanel = nameScope?.Find<StackPanel>("VoicePanel");
		VoiceResizeGrip = nameScope?.Find<Border>("VoiceResizeGrip");
		ChatWidgetContainer = nameScope?.Find<LayoutTransformControl>("ChatWidgetContainer");
		ChatWidgetBorder = nameScope?.Find<Border>("ChatWidgetBorder");
		ChatPanel = nameScope?.Find<StackPanel>("ChatPanel");
		ChatResizeGrip = nameScope?.Find<Border>("ChatResizeGrip");
	}

	[CompilerGenerated]
	private unsafe static void !XamlIlPopulate(IServiceProvider P_0, OverlayWindow P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<OverlayWindow> context = new CompiledAvaloniaXaml.XamlIlContext.Context<OverlayWindow>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Overlay/OverlayWindow.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Overlay/OverlayWindow.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		if (P_1.Resources is ResourceDictionary resourceDictionary)
		{
			resourceDictionary.EnsureCapacity(resourceDictionary.Count + 3);
		}
		P_1.Title = "Root Voice Overlay";
		P_1.SystemDecorations = SystemDecorations.None;
		P_1.Background = new ImmutableSolidColorBrush(16777215u);
		P_1.TransparencyLevelHint = new WindowTransparencyLevel[1] { WindowTransparencyLevel.Transparent };
		P_1.Topmost = true;
		P_1.ShowInTaskbar = false;
		P_1.Focusable = false;
		P_1.CanResize = false;
		P_1.WindowStartupLocation = WindowStartupLocation.Manual;
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"SpeakingBorderOpacityConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_188.Build_1), context));
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"SpeakingOpacityConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_188.Build_2), context));
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"VoiceUserVisibilityConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_188.Build_3), context));
		Styles styles = P_1.Styles;
		Style style = new Style();
		style.Selector = ((Selector?)null).OfType(typeof(Border)).Class("WidgetContainer");
		Setter setter = new Setter();
		setter.Property = Border.BorderBrushProperty;
		setter.Value = new ImmutableSolidColorBrush(16777215u);
		style.Add(setter);
		Setter setter2 = new Setter();
		setter2.Property = Border.BorderThicknessProperty;
		setter2.Value = new Thickness(2.0, 2.0, 2.0, 2.0);
		style.Add(setter2);
		Setter setter3 = new Setter();
		setter3.Property = Border.CornerRadiusProperty;
		setter3.Value = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		style.Add(setter3);
		Setter setter4 = new Setter();
		setter4.Property = InputElement.CursorProperty;
		setter4.Value = new Cursor(StandardCursorType.Arrow);
		style.Add(setter4);
		styles.Add(style);
		Styles styles2 = P_1.Styles;
		Style style2 = new Style();
		style2.Selector = ((Selector?)null).OfType(typeof(Border)).Class("WidgetContainer").Class("Interactive");
		Setter setter5 = new Setter();
		setter5.Property = Border.BorderBrushProperty;
		setter5.Value = new ImmutableSolidColorBrush(1627389951u);
		style2.Add(setter5);
		Setter setter6 = new Setter();
		setter6.Property = Border.BackgroundProperty;
		setter6.Value = new ImmutableSolidColorBrush(150994943u);
		style2.Add(setter6);
		Setter setter7 = new Setter();
		setter7.Property = InputElement.CursorProperty;
		setter7.Value = new Cursor(StandardCursorType.Hand);
		style2.Add(setter7);
		styles2.Add(style2);
		Styles styles3 = P_1.Styles;
		Style style3 = new Style();
		style3.Selector = ((Selector?)null).OfType(typeof(Border)).Class("WidgetContainer").Class("Interactive")
			.Class(":pointerover");
		Setter setter8 = new Setter();
		setter8.Property = Border.BorderBrushProperty;
		setter8.Value = new ImmutableSolidColorBrush(uint.MaxValue);
		style3.Add(setter8);
		Setter setter9 = new Setter();
		setter9.Property = Border.BackgroundProperty;
		setter9.Value = new ImmutableSolidColorBrush(369098751u);
		style3.Add(setter9);
		styles3.Add(style3);
		Styles styles4 = P_1.Styles;
		Style style4 = new Style();
		style4.Selector = ((Selector?)null).OfType(typeof(Border)).Class("WidgetContainer").Class("Dragging");
		Setter setter10 = new Setter();
		setter10.Property = Border.BorderBrushProperty;
		setter10.Value = new ImmutableSolidColorBrush(4283983346u);
		style4.Add(setter10);
		Setter setter11 = new Setter();
		setter11.Property = Border.BackgroundProperty;
		setter11.Value = new ImmutableSolidColorBrush(542664178u);
		style4.Add(setter11);
		Setter setter12 = new Setter();
		setter12.Property = InputElement.CursorProperty;
		setter12.Value = new Cursor(StandardCursorType.Hand);
		style4.Add(setter12);
		styles4.Add(style4);
		Styles styles5 = P_1.Styles;
		Style style5 = new Style();
		style5.Selector = ((Selector?)null).OfType(typeof(Border)).Class("WidgetHeader");
		Setter setter13 = new Setter();
		setter13.Property = Border.BackgroundProperty;
		setter13.Value = new ImmutableSolidColorBrush(3425316145u);
		style5.Add(setter13);
		Setter setter14 = new Setter();
		setter14.Property = Border.CornerRadiusProperty;
		setter14.Value = new CornerRadius(4.0, 4.0, 4.0, 4.0);
		style5.Add(setter14);
		Setter setter15 = new Setter();
		setter15.Property = Decorator.PaddingProperty;
		setter15.Value = new Thickness(8.0, 4.0, 8.0, 4.0);
		style5.Add(setter15);
		Setter setter16 = new Setter();
		setter16.Property = Layoutable.MarginProperty;
		setter16.Value = new Thickness(0.0, 0.0, 0.0, 8.0);
		style5.Add(setter16);
		styles5.Add(style5);
		Styles styles6 = P_1.Styles;
		Style style6 = new Style();
		style6.Selector = ((Selector?)null).OfType(typeof(Button)).Class("WidgetSettingsButton");
		Setter setter17 = new Setter();
		setter17.Property = TemplatedControl.BackgroundProperty;
		setter17.Value = new ImmutableSolidColorBrush(16777215u);
		style6.Add(setter17);
		Setter setter18 = new Setter();
		setter18.Property = TemplatedControl.BorderThicknessProperty;
		setter18.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
		style6.Add(setter18);
		Setter setter19 = new Setter();
		setter19.Property = TemplatedControl.PaddingProperty;
		setter19.Value = new Thickness(4.0, 4.0, 4.0, 4.0);
		style6.Add(setter19);
		Setter setter20 = new Setter();
		setter20.Property = InputElement.CursorProperty;
		setter20.Value = new Cursor(StandardCursorType.Hand);
		style6.Add(setter20);
		Setter setter21 = new Setter();
		setter21.Property = Visual.OpacityProperty;
		setter21.Value = 0.7;
		style6.Add(setter21);
		styles6.Add(style6);
		Styles styles7 = P_1.Styles;
		Style style7 = new Style();
		style7.Selector = ((Selector?)null).OfType(typeof(Button)).Class("WidgetSettingsButton").Class(":pointerover");
		Setter setter22 = new Setter();
		setter22.Property = Visual.OpacityProperty;
		setter22.Value = 1.0;
		style7.Add(setter22);
		Setter setter23 = new Setter();
		setter23.Property = TemplatedControl.BackgroundProperty;
		setter23.Value = new ImmutableSolidColorBrush(553648127u);
		style7.Add(setter23);
		styles7.Add(style7);
		Styles styles8 = P_1.Styles;
		Style style8 = new Style();
		style8.Selector = ((Selector?)null).OfType(typeof(ToggleButton)).Class("WidgetPinButton");
		Setter setter24 = new Setter();
		setter24.Property = TemplatedControl.BackgroundProperty;
		setter24.Value = new ImmutableSolidColorBrush(16777215u);
		style8.Add(setter24);
		Setter setter25 = new Setter();
		setter25.Property = TemplatedControl.BorderThicknessProperty;
		setter25.Value = new Thickness(0.0, 0.0, 0.0, 0.0);
		style8.Add(setter25);
		Setter setter26 = new Setter();
		setter26.Property = TemplatedControl.PaddingProperty;
		setter26.Value = new Thickness(4.0, 4.0, 4.0, 4.0);
		style8.Add(setter26);
		Setter setter27 = new Setter();
		setter27.Property = InputElement.CursorProperty;
		setter27.Value = new Cursor(StandardCursorType.Hand);
		style8.Add(setter27);
		Setter setter28 = new Setter();
		setter28.Property = Visual.OpacityProperty;
		setter28.Value = 0.7;
		style8.Add(setter28);
		styles8.Add(style8);
		Styles styles9 = P_1.Styles;
		Style style9 = new Style();
		style9.Selector = ((Selector?)null).OfType(typeof(ToggleButton)).Class("WidgetPinButton").Class(":pointerover");
		Setter setter29 = new Setter();
		setter29.Property = Visual.OpacityProperty;
		setter29.Value = 1.0;
		style9.Add(setter29);
		Setter setter30 = new Setter();
		setter30.Property = TemplatedControl.BackgroundProperty;
		setter30.Value = new ImmutableSolidColorBrush(553648127u);
		style9.Add(setter30);
		styles9.Add(style9);
		Styles styles10 = P_1.Styles;
		Style style10 = new Style();
		style10.Selector = ((Selector?)null).OfType(typeof(ToggleButton)).Class("WidgetPinButton").Class(":checked");
		Setter setter31 = new Setter();
		setter31.Property = Visual.OpacityProperty;
		setter31.Value = 1.0;
		style10.Add(setter31);
		styles10.Add(style10);
		Styles styles11 = P_1.Styles;
		Style style11 = new Style();
		style11.Selector = ((Selector?)null).OfType(typeof(Border)).Class("ResizeGrip");
		Setter setter32 = new Setter();
		setter32.Property = Layoutable.WidthProperty;
		setter32.Value = 16.0;
		style11.Add(setter32);
		Setter setter33 = new Setter();
		setter33.Property = Layoutable.HeightProperty;
		setter33.Value = 16.0;
		style11.Add(setter33);
		Setter setter34 = new Setter();
		setter34.Property = Border.BackgroundProperty;
		setter34.Value = new ImmutableSolidColorBrush(16777215u);
		style11.Add(setter34);
		Setter setter35 = new Setter();
		setter35.Property = InputElement.CursorProperty;
		setter35.Value = new Cursor(StandardCursorType.BottomRightCorner);
		style11.Add(setter35);
		Setter setter36 = new Setter();
		setter36.Property = Visual.OpacityProperty;
		setter36.Value = 0.5;
		style11.Add(setter36);
		styles11.Add(style11);
		Styles styles12 = P_1.Styles;
		Style style12 = new Style();
		style12.Selector = ((Selector?)null).OfType(typeof(Border)).Class("ResizeGrip").Class(":pointerover");
		Setter setter37 = new Setter();
		setter37.Property = Visual.OpacityProperty;
		setter37.Value = 1.0;
		style12.Add(setter37);
		styles12.Add(style12);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		P_1.Content = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		Grid grid5 = grid4;
		grid5.Name = "MainGrid";
		object obj = grid5;
		context.AvaloniaNameScope.Register("MainGrid", obj);
		global::Avalonia.Controls.Controls children = grid5.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Border border5 = border4;
		border5.Background = new ImmutableSolidColorBrush(4278190080u);
		border5.Opacity = 0.5;
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsInteractiveMode!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, isVisibleProperty, compiledBindingExtension2);
		border5.IsHitTestVisible = true;
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		global::Avalonia.Controls.Controls children2 = grid5.Children;
		Border border7;
		Border border6 = (border7 = new Border());
		((ISupportInitialize)border6).BeginInit();
		children2.Add(border6);
		Border border8 = (border4 = border7);
		context.PushParent(border4);
		Border border9 = border4;
		border9.Name = "WelcomeNotification";
		obj = border9;
		context.AvaloniaNameScope.Register("WelcomeNotification", obj);
		border9.Background = new ImmutableSolidColorBrush(3862653688u);
		border9.CornerRadius = new CornerRadius(0.0, 0.0, 8.0, 8.0);
		border9.Padding = new Thickness(16.0, 12.0, 16.0, 12.0);
		border9.HorizontalAlignment = HorizontalAlignment.Center;
		border9.VerticalAlignment = VerticalAlignment.Top;
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.ShowWelcomeNotification!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border9, isVisibleProperty2, compiledBindingExtension4);
		border9.IsHitTestVisible = true;
		border9.Cursor = new Cursor(StandardCursorType.Hand);
		border9.RenderTransform = new TranslateTransform
		{
			Y = -50.0
		};
		Styles styles13 = border9.Styles;
		Style style13 = new Style();
		style13.Selector = ((Selector?)null).OfType(typeof(Border)).Name("WelcomeNotification").PropertyEquals(Visual.IsVisibleProperty, true);
		IList<IAnimation> animations = style13.Animations;
		Animation animation = new Animation();
		animation.Duration = TimeSpan.FromTicks(3000000L);
		animation.Easing = Easing.Parse("CubicEaseOut");
		animation.FillMode = FillMode.Forward;
		KeyFrames children3 = animation.Children;
		KeyFrame keyFrame = new KeyFrame();
		keyFrame.Cue = Cue.Parse("0%", CultureInfo.InvariantCulture);
		AvaloniaList<IAnimationSetter> setters = keyFrame.Setters;
		Setter setter38 = new Setter();
		setter38.Property = Visual.OpacityProperty;
		setter38.Value = 0.0;
		setters.Add(setter38);
		AvaloniaList<IAnimationSetter> setters2 = keyFrame.Setters;
		Setter setter39 = new Setter();
		setter39.Property = TranslateTransform.YProperty;
		setter39.Value = -50.0;
		setters2.Add(setter39);
		children3.Add(keyFrame);
		KeyFrames children4 = animation.Children;
		KeyFrame keyFrame2 = new KeyFrame();
		keyFrame2.Cue = Cue.Parse("100%", CultureInfo.InvariantCulture);
		AvaloniaList<IAnimationSetter> setters3 = keyFrame2.Setters;
		Setter setter40 = new Setter();
		setter40.Property = Visual.OpacityProperty;
		setter40.Value = 1.0;
		setters3.Add(setter40);
		AvaloniaList<IAnimationSetter> setters4 = keyFrame2.Setters;
		Setter setter41 = new Setter();
		setter41.Property = TranslateTransform.YProperty;
		setter41.Value = 0.0;
		setters4.Add(setter41);
		children4.Add(keyFrame2);
		animations.Add(animation);
		styles13.Add(style13);
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		border9.Child = grid6;
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		ColumnDefinitions columnDefinitions = new ColumnDefinitions();
		columnDefinitions.Capacity = 3;
		columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid9.ColumnDefinitions = columnDefinitions;
		global::Avalonia.Controls.Controls children5 = grid9.Children;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		children5.Add(rootSvgImage);
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage5 = rootSvgImage4;
		Grid.SetColumn(rootSvgImage5, 0);
		rootSvgImage5.Width = 24.0;
		rootSvgImage5.Height = 24.0;
		StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("SmallRootLogoSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage5, svgPathProperty, binding);
		rootSvgImage5.Margin = new Thickness(0.0, 0.0, 12.0, 0.0);
		rootSvgImage5.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		global::Avalonia.Controls.Controls children6 = grid9.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children6.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		Grid.SetColumn(stackPanel5, 1);
		stackPanel5.VerticalAlignment = VerticalAlignment.Center;
		global::Avalonia.Controls.Controls children7 = stackPanel5.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children7.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Text = "Root Overlay Active";
		textBlock5.Foreground = new ImmutableSolidColorBrush(uint.MaxValue);
		textBlock5.FontSize = 14.0;
		textBlock5.FontWeight = FontWeight.DemiBold;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock5, obj2);
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		global::Avalonia.Controls.Controls children8 = stackPanel5.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children8.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Foreground = new ImmutableSolidColorBrush(3439329279u);
		textBlock9.FontSize = 12.0;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj3 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock9, obj3);
		textBlock9.Margin = new Thickness(0.0, 2.0, 0.0, 0.0);
		InlineCollection? inlines = textBlock9.Inlines;
		Run run = new Run();
		((ISupportInitialize)run).BeginInit();
		run.Text = "Press ";
		((ISupportInitialize)run).EndInit();
		inlines.Add(run);
		textBlock9.Inlines.Add(" ");
		InlineCollection? inlines2 = textBlock9.Inlines;
		Run run2 = new Run();
		((ISupportInitialize)run2).BeginInit();
		Run run3 = run2;
		context.PushParent(run3);
		Run run4 = run3;
		StyledProperty<string?> textProperty = Run.TextProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.WelcomeKeybindingText!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Run.TextProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(run4, textProperty, compiledBindingExtension6);
		run4.FontWeight = FontWeight.Bold;
		context.PopParent();
		((ISupportInitialize)run2).EndInit();
		inlines2.Add(run2);
		textBlock9.Inlines.Add(" ");
		InlineCollection? inlines3 = textBlock9.Inlines;
		Run run5 = new Run();
		((ISupportInitialize)run5).BeginInit();
		run5.Text = " to open overlay settings";
		((ISupportInitialize)run5).EndInit();
		inlines3.Add(run5);
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		global::Avalonia.Controls.Controls children9 = grid9.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children9.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		Button button5 = button4;
		Grid.SetColumn(button5, 2);
		button5.Background = new ImmutableSolidColorBrush(16777215u);
		button5.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button5.Padding = new Thickness(4.0, 4.0, 4.0, 4.0);
		button5.Margin = new Thickness(12.0, 0.0, 0.0, 0.0);
		button5.Cursor = new Cursor(StandardCursorType.Hand);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.DismissWelcomeNotificationCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, commandProperty, compiledBindingExtension8);
		button5.VerticalAlignment = VerticalAlignment.Center;
		RootSvgImage rootSvgImage7;
		RootSvgImage rootSvgImage6 = (rootSvgImage7 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage6).BeginInit();
		button5.Content = rootSvgImage6;
		RootSvgImage rootSvgImage8 = (rootSvgImage4 = rootSvgImage7);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage9 = rootSvgImage4;
		rootSvgImage9.Width = 16.0;
		rootSvgImage9.Height = 16.0;
		StyledProperty<string?> svgPathProperty2 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("CloseSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage9, svgPathProperty2, binding2);
		context.PopParent();
		((ISupportInitialize)rootSvgImage8).EndInit();
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		context.PopParent();
		((ISupportInitialize)border8).EndInit();
		global::Avalonia.Controls.Controls children10 = grid5.Children;
		OverlayActionBar overlayActionBar2;
		OverlayActionBar overlayActionBar = (overlayActionBar2 = new OverlayActionBar());
		((ISupportInitialize)overlayActionBar).BeginInit();
		children10.Add(overlayActionBar);
		OverlayActionBar overlayActionBar4;
		OverlayActionBar overlayActionBar3 = (overlayActionBar4 = overlayActionBar2);
		context.PushParent(overlayActionBar4);
		overlayActionBar4.Name = "ActionBarControl";
		obj = overlayActionBar4;
		context.AvaloniaNameScope.Register("ActionBarControl", obj);
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.ActionBar!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = StyledElement.DataContextProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_6(overlayActionBar4, compiledBindingExtension10);
		overlayActionBar4.HorizontalAlignment = HorizontalAlignment.Center;
		overlayActionBar4.VerticalAlignment = VerticalAlignment.Bottom;
		overlayActionBar4.Margin = new Thickness(0.0, 0.0, 0.0, 24.0);
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		MultiBinding multiBinding2;
		MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding3 = multiBinding2;
		multiBinding3.Converter = BoolConverters.And;
		IList<IBinding> bindings = multiBinding3.Bindings;
		CompiledBindingExtension obj4 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "MainGrid").Property(StyledElement.DataContextProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsInteractiveMode!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item = obj4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings.Add(item);
		IList<IBinding> bindings2 = multiBinding3.Bindings;
		CompiledBindingExtension obj5 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "MainGrid").Property(StyledElement.DataContextProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsInVoiceChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item2 = obj5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings2.Add(item2);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(overlayActionBar4, isVisibleProperty3, multiBinding);
		context.PopParent();
		((ISupportInitialize)overlayActionBar3).EndInit();
		global::Avalonia.Controls.Controls children11 = grid5.Children;
		Border border11;
		Border border10 = (border11 = new Border());
		((ISupportInitialize)border10).BeginInit();
		children11.Add(border10);
		Border border12 = (border4 = border11);
		context.PushParent(border4);
		Border border13 = border4;
		border13.Background = new ImmutableSolidColorBrush(4283983346u);
		border13.CornerRadius = new CornerRadius(4.0, 4.0, 4.0, 4.0);
		border13.Padding = new Thickness(8.0, 4.0, 8.0, 4.0);
		border13.HorizontalAlignment = HorizontalAlignment.Center;
		border13.VerticalAlignment = VerticalAlignment.Top;
		border13.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsInteractiveMode!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border13, isVisibleProperty4, compiledBindingExtension12);
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		border13.Child = textBlock10;
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		textBlock13.Foreground = new ImmutableSolidColorBrush(uint.MaxValue);
		textBlock13.FontSize = 12.0;
		textBlock13.FontWeight = FontWeight.Medium;
		InlineCollection? inlines4 = textBlock13.Inlines;
		Run run6 = new Run();
		((ISupportInitialize)run6).BeginInit();
		run6.Text = "Interactive Mode - Drag to reposition, ";
		((ISupportInitialize)run6).EndInit();
		inlines4.Add(run6);
		textBlock13.Inlines.Add(" ");
		InlineCollection? inlines5 = textBlock13.Inlines;
		Run run7 = new Run();
		((ISupportInitialize)run7).BeginInit();
		run3 = run7;
		context.PushParent(run3);
		Run run8 = run3;
		StyledProperty<string?> textProperty2 = Run.TextProperty;
		CompiledBindingExtension compiledBindingExtension13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.WelcomeKeybindingText!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Run.TextProperty;
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(run8, textProperty2, compiledBindingExtension14);
		run8.FontWeight = FontWeight.Bold;
		context.PopParent();
		((ISupportInitialize)run7).EndInit();
		inlines5.Add(run7);
		textBlock13.Inlines.Add(" ");
		InlineCollection? inlines6 = textBlock13.Inlines;
		Run run9 = new Run();
		((ISupportInitialize)run9).BeginInit();
		run9.Text = " to exit";
		((ISupportInitialize)run9).EndInit();
		inlines6.Add(run9);
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		context.PopParent();
		((ISupportInitialize)border12).EndInit();
		global::Avalonia.Controls.Controls children12 = grid5.Children;
		Canvas canvas2;
		Canvas canvas = (canvas2 = new Canvas());
		((ISupportInitialize)canvas).BeginInit();
		children12.Add(canvas);
		Canvas canvas4;
		Canvas canvas3 = (canvas4 = canvas2);
		context.PushParent(canvas4);
		StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension15 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.ShowSnapIndicator!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension16 = compiledBindingExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(canvas4, isVisibleProperty5, compiledBindingExtension16);
		canvas4.IsHitTestVisible = false;
		global::Avalonia.Controls.Controls children13 = canvas4.Children;
		Border border15;
		Border border14 = (border15 = new Border());
		((ISupportInitialize)border14).BeginInit();
		children13.Add(border14);
		Border border16 = (border4 = border15);
		context.PushParent(border4);
		Border border17 = border4;
		AttachedProperty<double> leftProperty = Canvas.LeftProperty;
		CompiledBindingExtension compiledBindingExtension17 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.SnapIndicatorX!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Canvas.LeftProperty;
		CompiledBindingExtension compiledBindingExtension18 = compiledBindingExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border17, leftProperty, compiledBindingExtension18);
		AttachedProperty<double> topProperty = Canvas.TopProperty;
		CompiledBindingExtension compiledBindingExtension19 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.SnapIndicatorY!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Canvas.TopProperty;
		CompiledBindingExtension compiledBindingExtension20 = compiledBindingExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border17, topProperty, compiledBindingExtension20);
		StyledProperty<double> widthProperty = Layoutable.WidthProperty;
		CompiledBindingExtension compiledBindingExtension21 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.SnapIndicatorWidth!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Layoutable.WidthProperty;
		CompiledBindingExtension compiledBindingExtension22 = compiledBindingExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border17, widthProperty, compiledBindingExtension22);
		StyledProperty<double> heightProperty = Layoutable.HeightProperty;
		CompiledBindingExtension compiledBindingExtension23 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.SnapIndicatorHeight!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Layoutable.HeightProperty;
		CompiledBindingExtension compiledBindingExtension24 = compiledBindingExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border17, heightProperty, compiledBindingExtension24);
		border17.Background = new ImmutableSolidColorBrush(542664178u);
		border17.BorderBrush = new ImmutableSolidColorBrush(4283983346u);
		border17.BorderThickness = new Thickness(2.0, 2.0, 2.0, 2.0);
		border17.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		border17.Opacity = 0.8;
		Styles styles14 = border17.Styles;
		Style style14 = new Style();
		style14.Selector = ((Selector?)null).OfType(typeof(Border));
		IList<IAnimation> animations2 = style14.Animations;
		Animation animation2 = new Animation();
		animation2.Duration = TimeSpan.FromTicks(3000000L);
		animation2.IterationCount = IterationCount.Parse("Infinite");
		animation2.PlaybackDirection = PlaybackDirection.Alternate;
		KeyFrames children14 = animation2.Children;
		KeyFrame keyFrame3 = new KeyFrame();
		keyFrame3.Cue = Cue.Parse("0%", CultureInfo.InvariantCulture);
		AvaloniaList<IAnimationSetter> setters5 = keyFrame3.Setters;
		Setter setter42 = new Setter();
		setter42.Property = Visual.OpacityProperty;
		setter42.Value = 0.5;
		setters5.Add(setter42);
		children14.Add(keyFrame3);
		KeyFrames children15 = animation2.Children;
		KeyFrame keyFrame4 = new KeyFrame();
		keyFrame4.Cue = Cue.Parse("100%", CultureInfo.InvariantCulture);
		AvaloniaList<IAnimationSetter> setters6 = keyFrame4.Setters;
		Setter setter43 = new Setter();
		setter43.Property = Visual.OpacityProperty;
		setter43.Value = 0.9;
		setters6.Add(setter43);
		children15.Add(keyFrame4);
		animations2.Add(animation2);
		styles14.Add(style14);
		context.PopParent();
		((ISupportInitialize)border16).EndInit();
		context.PopParent();
		((ISupportInitialize)canvas3).EndInit();
		global::Avalonia.Controls.Controls children16 = grid5.Children;
		LayoutTransformControl layoutTransformControl2;
		LayoutTransformControl layoutTransformControl = (layoutTransformControl2 = new LayoutTransformControl());
		((ISupportInitialize)layoutTransformControl).BeginInit();
		children16.Add(layoutTransformControl);
		LayoutTransformControl layoutTransformControl4;
		LayoutTransformControl layoutTransformControl3 = (layoutTransformControl4 = layoutTransformControl2);
		context.PushParent(layoutTransformControl4);
		LayoutTransformControl layoutTransformControl5 = layoutTransformControl4;
		layoutTransformControl5.Name = "VoiceWidgetContainer";
		obj = layoutTransformControl5;
		context.AvaloniaNameScope.Register("VoiceWidgetContainer", obj);
		layoutTransformControl5.HorizontalAlignment = HorizontalAlignment.Left;
		layoutTransformControl5.VerticalAlignment = VerticalAlignment.Top;
		layoutTransformControl5.Margin = new Thickness(16.0, 16.0, 16.0, 16.0);
		ScaleTransform scaleTransform;
		ScaleTransform layoutTransform = (scaleTransform = new ScaleTransform());
		context.PushParent(scaleTransform);
		ScaleTransform scaleTransform2 = scaleTransform;
		StyledProperty<double> scaleXProperty = ScaleTransform.ScaleXProperty;
		CompiledBindingExtension compiledBindingExtension25 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.Scale!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ScaleTransform.ScaleXProperty;
		CompiledBindingExtension compiledBindingExtension26 = compiledBindingExtension25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(scaleTransform2, scaleXProperty, compiledBindingExtension26);
		StyledProperty<double> scaleYProperty = ScaleTransform.ScaleYProperty;
		CompiledBindingExtension compiledBindingExtension27 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.Scale!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ScaleTransform.ScaleYProperty;
		CompiledBindingExtension compiledBindingExtension28 = compiledBindingExtension27.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(scaleTransform2, scaleYProperty, compiledBindingExtension28);
		context.PopParent();
		layoutTransformControl5.LayoutTransform = layoutTransform;
		StyledProperty<bool> isVisibleProperty6 = Visual.IsVisibleProperty;
		MultiBinding multiBinding4 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding5 = multiBinding2;
		multiBinding5.Converter = BoolConverters.Or;
		IList<IBinding> bindings3 = multiBinding5.Bindings;
		CompiledBindingExtension obj6 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsInteractiveMode!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item3 = obj6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings3.Add(item3);
		IList<IBinding> bindings4 = multiBinding5.Bindings;
		CompiledBindingExtension obj7 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsVoiceOverlayVisible!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item4 = obj7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings4.Add(item4);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(layoutTransformControl5, isVisibleProperty6, multiBinding4);
		Border border19;
		Border border18 = (border19 = new Border());
		((ISupportInitialize)border18).BeginInit();
		layoutTransformControl5.Child = border18;
		Border border20 = (border4 = border19);
		context.PushParent(border4);
		Border border21 = border4;
		border21.Name = "VoiceWidgetBorder";
		obj = border21;
		context.AvaloniaNameScope.Register("VoiceWidgetBorder", obj);
		border21.Classes.Add("WidgetContainer");
		CompiledBindingExtension compiledBindingExtension29 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsInteractiveMode!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = "class:Interactive";
		CompiledBindingExtension compiledBindingExtension30 = compiledBindingExtension29.ProvideValue(context);
		context.ProvideTargetProperty = null;
		IBinding binding3 = compiledBindingExtension30;
		border21.BindClass("Interactive", binding3, null);
		CompiledBindingExtension compiledBindingExtension31 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsVoicePanelDragging!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = "class:Dragging";
		CompiledBindingExtension compiledBindingExtension32 = compiledBindingExtension31.ProvideValue(context);
		context.ProvideTargetProperty = null;
		binding3 = compiledBindingExtension32;
		border21.BindClass("Dragging", binding3, null);
		border21.Padding = new Thickness(8.0, 8.0, 8.0, 8.0);
		StyledProperty<double> opacityProperty = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension33 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.Opacity!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension34 = compiledBindingExtension33.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border21, opacityProperty, compiledBindingExtension34);
		Grid grid11;
		Grid grid10 = (grid11 = new Grid());
		((ISupportInitialize)grid10).BeginInit();
		border21.Child = grid10;
		Grid grid12 = (grid4 = grid11);
		context.PushParent(grid4);
		Grid grid13 = grid4;
		global::Avalonia.Controls.Controls children17 = grid13.Children;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		children17.Add(stackPanel6);
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		stackPanel9.Name = "VoicePanel";
		obj = stackPanel9;
		context.AvaloniaNameScope.Register("VoicePanel", obj);
		stackPanel9.MaxWidth = 400.0;
		global::Avalonia.Controls.Controls children18 = stackPanel9.Children;
		Border border23;
		Border border22 = (border23 = new Border());
		((ISupportInitialize)border22).BeginInit();
		children18.Add(border22);
		Border border24 = (border4 = border23);
		context.PushParent(border4);
		Border border25 = border4;
		border25.Classes.Add("WidgetHeader");
		StyledProperty<bool> isVisibleProperty7 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension35 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsInteractiveMode!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension36 = compiledBindingExtension35.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border25, isVisibleProperty7, compiledBindingExtension36);
		Grid grid15;
		Grid grid14 = (grid15 = new Grid());
		((ISupportInitialize)grid14).BeginInit();
		border25.Child = grid14;
		Grid grid16 = (grid4 = grid15);
		context.PushParent(grid4);
		Grid grid17 = grid4;
		ColumnDefinitions columnDefinitions2 = new ColumnDefinitions();
		columnDefinitions2.Capacity = 3;
		columnDefinitions2.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		columnDefinitions2.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		columnDefinitions2.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid17.ColumnDefinitions = columnDefinitions2;
		global::Avalonia.Controls.Controls children19 = grid17.Children;
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		children19.Add(textBlock14);
		Grid.SetColumn(textBlock15, 0);
		textBlock15.Text = "Voice";
		textBlock15.Foreground = new ImmutableSolidColorBrush(uint.MaxValue);
		textBlock15.FontSize = 12.0;
		textBlock15.FontWeight = FontWeight.DemiBold;
		textBlock15.VerticalAlignment = VerticalAlignment.Center;
		((ISupportInitialize)textBlock15).EndInit();
		global::Avalonia.Controls.Controls children20 = grid17.Children;
		Button button7;
		Button button6 = (button7 = new Button());
		((ISupportInitialize)button6).BeginInit();
		children20.Add(button6);
		Button button8 = (button4 = button7);
		context.PushParent(button4);
		Button button9 = button4;
		Grid.SetColumn(button9, 2);
		button9.Classes.Add("WidgetSettingsButton");
		StyledProperty<ICommand?> commandProperty2 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension37 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.ToggleVoiceOverlayEnabledCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension38 = compiledBindingExtension37.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, commandProperty2, compiledBindingExtension38);
		ToolTip.SetPlacement(button9, PlacementMode.Bottom);
		ToolTip.SetVerticalOffset(button9, 4.0);
		ToolTip.SetShowDelay(button9, 300);
		RootToolTip rootToolTip2;
		RootToolTip rootToolTip = (rootToolTip2 = new RootToolTip());
		((ISupportInitialize)rootToolTip).BeginInit();
		ToolTip.SetTip(button9, rootToolTip);
		RootToolTip rootToolTip4;
		RootToolTip rootToolTip3 = (rootToolTip4 = rootToolTip2);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip5 = rootToolTip4;
		TextBlock textBlock17;
		TextBlock textBlock16 = (textBlock17 = new TextBlock());
		((ISupportInitialize)textBlock16).BeginInit();
		rootToolTip5.Content = textBlock16;
		TextBlock textBlock18 = (textBlock4 = textBlock17);
		context.PushParent(textBlock4);
		TextBlock textBlock19 = textBlock4;
		textBlock19.Text = "Toggle voice overlay visibility";
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj8 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock19, obj8);
		textBlock19.FontWeight = (FontWeight)450;
		textBlock19.FontSize = 14.0;
		textBlock19.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)textBlock18).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip3).EndInit();
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		button9.Content = panel;
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		Panel panel5 = panel4;
		global::Avalonia.Controls.Controls children21 = panel5.Children;
		RootSvgImage rootSvgImage11;
		RootSvgImage rootSvgImage10 = (rootSvgImage11 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage10).BeginInit();
		children21.Add(rootSvgImage10);
		RootSvgImage rootSvgImage12 = (rootSvgImage4 = rootSvgImage11);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage13 = rootSvgImage4;
		rootSvgImage13.Width = 14.0;
		rootSvgImage13.Height = 14.0;
		StyledProperty<string?> svgPathProperty3 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("PinFilledSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding4 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage13, svgPathProperty3, binding4);
		StyledProperty<bool> isVisibleProperty8 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension39 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsVoiceOverlayEnabled!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension40 = compiledBindingExtension39.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage13, isVisibleProperty8, compiledBindingExtension40);
		context.PopParent();
		((ISupportInitialize)rootSvgImage12).EndInit();
		global::Avalonia.Controls.Controls children22 = panel5.Children;
		RootSvgImage rootSvgImage15;
		RootSvgImage rootSvgImage14 = (rootSvgImage15 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage14).BeginInit();
		children22.Add(rootSvgImage14);
		RootSvgImage rootSvgImage16 = (rootSvgImage4 = rootSvgImage15);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage17 = rootSvgImage4;
		rootSvgImage17.Width = 14.0;
		rootSvgImage17.Height = 14.0;
		StyledProperty<string?> svgPathProperty4 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("PinSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding5 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage17, svgPathProperty4, binding5);
		StyledProperty<bool> isVisibleProperty9 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension41 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsVoiceOverlayEnabled!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension42 = compiledBindingExtension41.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage17, isVisibleProperty9, compiledBindingExtension42);
		context.PopParent();
		((ISupportInitialize)rootSvgImage16).EndInit();
		context.PopParent();
		((ISupportInitialize)panel3).EndInit();
		context.PopParent();
		((ISupportInitialize)button8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid16).EndInit();
		context.PopParent();
		((ISupportInitialize)border24).EndInit();
		global::Avalonia.Controls.Controls children23 = stackPanel9.Children;
		Grid grid19;
		Grid grid18 = (grid19 = new Grid());
		((ISupportInitialize)grid18).BeginInit();
		children23.Add(grid18);
		Grid grid20 = (grid4 = grid19);
		context.PushParent(grid4);
		Grid grid21 = grid4;
		StyledProperty<bool> isVisibleProperty10 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension43 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsInVoiceChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension44 = compiledBindingExtension43.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(grid21, isVisibleProperty10, compiledBindingExtension44);
		global::Avalonia.Controls.Controls children24 = grid21.Children;
		Grid grid23;
		Grid grid22 = (grid23 = new Grid());
		((ISupportInitialize)grid22).BeginInit();
		children24.Add(grid22);
		Grid grid24 = (grid4 = grid23);
		context.PushParent(grid4);
		Grid grid25 = grid4;
		StyledProperty<bool> isVisibleProperty11 = Visual.IsVisibleProperty;
		MultiBinding multiBinding6 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding7 = multiBinding2;
		multiBinding7.Converter = BoolConverters.And;
		IList<IBinding> bindings5 = multiBinding7.Bindings;
		CompiledBindingExtension compiledBindingExtension45 = new CompiledBindingExtension();
		compiledBindingExtension45.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.ChannelName!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build();
		compiledBindingExtension45.Converter = StringConverters.IsNotNullOrEmpty;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item5 = compiledBindingExtension45.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings5.Add(item5);
		IList<IBinding> bindings6 = multiBinding7.Bindings;
		CompiledBindingExtension compiledBindingExtension46 = new CompiledBindingExtension();
		compiledBindingExtension46.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.OnlyShowSpeaking!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build();
		compiledBindingExtension46.Converter = BoolConverters.Not;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item6 = compiledBindingExtension46.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings6.Add(item6);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(grid25, isVisibleProperty11, multiBinding6);
		global::Avalonia.Controls.Controls children25 = grid25.Children;
		Border border27;
		Border border26 = (border27 = new Border());
		((ISupportInitialize)border26).BeginInit();
		children25.Add(border26);
		Border border28 = (border4 = border27);
		context.PushParent(border4);
		Border border29 = border4;
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding6 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border29, backgroundProperty, binding6);
		border29.Opacity = 0.25;
		border29.CornerRadius = new CornerRadius(6.0, 6.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)border28).EndInit();
		global::Avalonia.Controls.Controls children26 = grid25.Children;
		Border border31;
		Border border30 = (border31 = new Border());
		((ISupportInitialize)border30).BeginInit();
		children26.Add(border30);
		Border border32 = (border4 = border31);
		context.PushParent(border4);
		Border border33 = border4;
		border33.Padding = new Thickness(10.0, 6.0, 10.0, 6.0);
		TextBlock textBlock21;
		TextBlock textBlock20 = (textBlock21 = new TextBlock());
		((ISupportInitialize)textBlock20).BeginInit();
		border33.Child = textBlock20;
		TextBlock textBlock22 = (textBlock4 = textBlock21);
		context.PushParent(textBlock4);
		TextBlock textBlock23 = textBlock4;
		StyledProperty<string?> textProperty3 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension47 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.ChannelName!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension48 = compiledBindingExtension47.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock23, textProperty3, compiledBindingExtension48);
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding7 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock23, foregroundProperty, binding7);
		textBlock23.FontSize = 12.0;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj9 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock23, obj9);
		textBlock23.FontWeight = FontWeight.DemiBold;
		textBlock23.TextTrimming = TextTrimming.CharacterEllipsis;
		context.PopParent();
		((ISupportInitialize)textBlock22).EndInit();
		context.PopParent();
		((ISupportInitialize)border32).EndInit();
		context.PopParent();
		((ISupportInitialize)grid24).EndInit();
		context.PopParent();
		((ISupportInitialize)grid20).EndInit();
		global::Avalonia.Controls.Controls children27 = stackPanel9.Children;
		Grid grid27;
		Grid grid26 = (grid27 = new Grid());
		((ISupportInitialize)grid26).BeginInit();
		children27.Add(grid26);
		Grid grid28 = (grid4 = grid27);
		context.PushParent(grid4);
		Grid grid29 = grid4;
		StyledProperty<bool> isVisibleProperty12 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension49 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsInVoiceChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension50 = compiledBindingExtension49.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(grid29, isVisibleProperty12, compiledBindingExtension50);
		global::Avalonia.Controls.Controls children28 = grid29.Children;
		Border border35;
		Border border34 = (border35 = new Border());
		((ISupportInitialize)border34).BeginInit();
		children28.Add(border34);
		Border border36 = (border4 = border35);
		context.PushParent(border4);
		Border border37 = border4;
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding8 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border37, backgroundProperty2, binding8);
		border37.Opacity = 0.25;
		border37.CornerRadius = new CornerRadius(0.0, 0.0, 6.0, 6.0);
		StyledProperty<bool> isVisibleProperty13 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj10 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.OnlyShowSpeaking!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Converter = BoolConverters.Not
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension51 = obj10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border37, isVisibleProperty13, compiledBindingExtension51);
		context.PopParent();
		((ISupportInitialize)border36).EndInit();
		global::Avalonia.Controls.Controls children29 = grid29.Children;
		Border border39;
		Border border38 = (border39 = new Border());
		((ISupportInitialize)border38).BeginInit();
		children29.Add(border38);
		Border border40 = (border4 = border39);
		context.PushParent(border4);
		Border border41 = border4;
		border41.Padding = new Thickness(4.0, 2.0, 4.0, 2.0);
		ItemsControl itemsControl2;
		ItemsControl itemsControl = (itemsControl2 = new ItemsControl());
		((ISupportInitialize)itemsControl).BeginInit();
		border41.Child = itemsControl;
		ItemsControl itemsControl4;
		ItemsControl itemsControl3 = (itemsControl4 = itemsControl2);
		context.PushParent(itemsControl4);
		ItemsControl itemsControl5 = itemsControl4;
		StyledProperty<IEnumerable?> itemsSourceProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension52 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.VoiceUsers!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension53 = compiledBindingExtension52.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl5, itemsSourceProperty, compiledBindingExtension53);
		itemsControl5.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_188.Build_4), context)
		};
		DataTemplate dataTemplate;
		DataTemplate itemTemplate = (dataTemplate = new DataTemplate());
		context.PushParent(dataTemplate);
		dataTemplate.DataType = typeof(OverlayVoiceUser);
		dataTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_188.Build_5), context);
		context.PopParent();
		itemsControl5.ItemTemplate = itemTemplate;
		context.PopParent();
		((ISupportInitialize)itemsControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)border40).EndInit();
		context.PopParent();
		((ISupportInitialize)grid28).EndInit();
		global::Avalonia.Controls.Controls children30 = stackPanel9.Children;
		Border border43;
		Border border42 = (border43 = new Border());
		((ISupportInitialize)border42).BeginInit();
		children30.Add(border42);
		Border border44 = (border4 = border43);
		context.PushParent(border4);
		Border border45 = border4;
		border45.Background = new ImmutableSolidColorBrush(1073741824u);
		border45.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		border45.Padding = new Thickness(16.0, 24.0, 16.0, 24.0);
		StyledProperty<bool> isVisibleProperty14 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension54 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsInVoiceChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension55 = compiledBindingExtension54.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border45, isVisibleProperty14, compiledBindingExtension55);
		StackPanel stackPanel11;
		StackPanel stackPanel10 = (stackPanel11 = new StackPanel());
		((ISupportInitialize)stackPanel10).BeginInit();
		border45.Child = stackPanel10;
		StackPanel stackPanel12 = (stackPanel4 = stackPanel11);
		context.PushParent(stackPanel4);
		StackPanel stackPanel13 = stackPanel4;
		stackPanel13.HorizontalAlignment = HorizontalAlignment.Center;
		stackPanel13.Spacing = 4.0;
		global::Avalonia.Controls.Controls children31 = stackPanel13.Children;
		RootSvgImage rootSvgImage19;
		RootSvgImage rootSvgImage18 = (rootSvgImage19 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage18).BeginInit();
		children31.Add(rootSvgImage18);
		RootSvgImage rootSvgImage20 = (rootSvgImage4 = rootSvgImage19);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage21 = rootSvgImage4;
		rootSvgImage21.Width = 24.0;
		rootSvgImage21.Height = 24.0;
		StyledProperty<string?> svgPathProperty5 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("HeadphonesSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding9 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage21, svgPathProperty5, binding9);
		rootSvgImage21.HorizontalAlignment = HorizontalAlignment.Center;
		rootSvgImage21.Opacity = 0.5;
		context.PopParent();
		((ISupportInitialize)rootSvgImage20).EndInit();
		global::Avalonia.Controls.Controls children32 = stackPanel13.Children;
		TextBlock textBlock25;
		TextBlock textBlock24 = (textBlock25 = new TextBlock());
		((ISupportInitialize)textBlock24).BeginInit();
		children32.Add(textBlock24);
		textBlock25.Text = "Not in a voice channel";
		textBlock25.Foreground = new ImmutableSolidColorBrush(2164260863u);
		textBlock25.FontSize = 11.0;
		textBlock25.HorizontalAlignment = HorizontalAlignment.Center;
		((ISupportInitialize)textBlock25).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel12).EndInit();
		context.PopParent();
		((ISupportInitialize)border44).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		global::Avalonia.Controls.Controls children33 = grid13.Children;
		Border border47;
		Border border46 = (border47 = new Border());
		((ISupportInitialize)border46).BeginInit();
		children33.Add(border46);
		Border border48 = (border4 = border47);
		context.PushParent(border4);
		Border border49 = border4;
		border49.Name = "VoiceResizeGrip";
		obj = border49;
		context.AvaloniaNameScope.Register("VoiceResizeGrip", obj);
		border49.Classes.Add("ResizeGrip");
		border49.HorizontalAlignment = HorizontalAlignment.Right;
		border49.VerticalAlignment = VerticalAlignment.Bottom;
		border49.Margin = new Thickness(0.0, 0.0, -4.0, -4.0);
		StyledProperty<bool> isVisibleProperty15 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension56 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsInteractiveMode!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension57 = compiledBindingExtension56.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border49, isVisibleProperty15, compiledBindingExtension57);
		ToolTip.SetPlacement(border49, PlacementMode.Left);
		ToolTip.SetHorizontalOffset(border49, -4.0);
		ToolTip.SetShowDelay(border49, 300);
		RootToolTip rootToolTip7;
		RootToolTip rootToolTip6 = (rootToolTip7 = new RootToolTip());
		((ISupportInitialize)rootToolTip6).BeginInit();
		ToolTip.SetTip(border49, rootToolTip6);
		RootToolTip rootToolTip8 = (rootToolTip4 = rootToolTip7);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip9 = rootToolTip4;
		TextBlock textBlock27;
		TextBlock textBlock26 = (textBlock27 = new TextBlock());
		((ISupportInitialize)textBlock26).BeginInit();
		rootToolTip9.Content = textBlock26;
		TextBlock textBlock28 = (textBlock4 = textBlock27);
		context.PushParent(textBlock4);
		TextBlock textBlock29 = textBlock4;
		textBlock29.Text = "Drag to resize";
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj11 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock29, obj11);
		textBlock29.FontWeight = (FontWeight)450;
		textBlock29.FontSize = 14.0;
		textBlock29.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)textBlock28).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip8).EndInit();
		Path path2;
		Path path = (path2 = new Path());
		((ISupportInitialize)path).BeginInit();
		border49.Child = path;
		path2.Data = Geometry.Parse("M 0,12 L 12,0 M 4,12 L 12,4 M 8,12 L 12,8");
		path2.Stroke = new ImmutableSolidColorBrush(2164260863u);
		path2.StrokeThickness = 1.5;
		((ISupportInitialize)path2).EndInit();
		context.PopParent();
		((ISupportInitialize)border48).EndInit();
		context.PopParent();
		((ISupportInitialize)grid12).EndInit();
		context.PopParent();
		((ISupportInitialize)border20).EndInit();
		context.PopParent();
		((ISupportInitialize)layoutTransformControl3).EndInit();
		global::Avalonia.Controls.Controls children34 = grid5.Children;
		LayoutTransformControl layoutTransformControl7;
		LayoutTransformControl layoutTransformControl6 = (layoutTransformControl7 = new LayoutTransformControl());
		((ISupportInitialize)layoutTransformControl6).BeginInit();
		children34.Add(layoutTransformControl6);
		LayoutTransformControl layoutTransformControl8 = (layoutTransformControl4 = layoutTransformControl7);
		context.PushParent(layoutTransformControl4);
		LayoutTransformControl layoutTransformControl9 = layoutTransformControl4;
		layoutTransformControl9.Name = "ChatWidgetContainer";
		obj = layoutTransformControl9;
		context.AvaloniaNameScope.Register("ChatWidgetContainer", obj);
		layoutTransformControl9.HorizontalAlignment = HorizontalAlignment.Right;
		layoutTransformControl9.VerticalAlignment = VerticalAlignment.Top;
		layoutTransformControl9.Margin = new Thickness(16.0, 16.0, 16.0, 16.0);
		ScaleTransform layoutTransform2 = (scaleTransform = new ScaleTransform());
		context.PushParent(scaleTransform);
		ScaleTransform scaleTransform3 = scaleTransform;
		StyledProperty<double> scaleXProperty2 = ScaleTransform.ScaleXProperty;
		CompiledBindingExtension compiledBindingExtension58 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.Scale!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ScaleTransform.ScaleXProperty;
		CompiledBindingExtension compiledBindingExtension59 = compiledBindingExtension58.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(scaleTransform3, scaleXProperty2, compiledBindingExtension59);
		StyledProperty<double> scaleYProperty2 = ScaleTransform.ScaleYProperty;
		CompiledBindingExtension compiledBindingExtension60 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.Scale!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ScaleTransform.ScaleYProperty;
		CompiledBindingExtension compiledBindingExtension61 = compiledBindingExtension60.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(scaleTransform3, scaleYProperty2, compiledBindingExtension61);
		context.PopParent();
		layoutTransformControl9.LayoutTransform = layoutTransform2;
		StyledProperty<bool> isVisibleProperty16 = Visual.IsVisibleProperty;
		MultiBinding multiBinding8 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding9 = multiBinding2;
		multiBinding9.Converter = BoolConverters.Or;
		IList<IBinding> bindings7 = multiBinding9.Bindings;
		CompiledBindingExtension obj12 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsInteractiveMode!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item7 = obj12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings7.Add(item7);
		IList<IBinding> bindings8 = multiBinding9.Bindings;
		CompiledBindingExtension obj13 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsChatOverlayVisible!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item8 = obj13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings8.Add(item8);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(layoutTransformControl9, isVisibleProperty16, multiBinding8);
		Border border51;
		Border border50 = (border51 = new Border());
		((ISupportInitialize)border50).BeginInit();
		layoutTransformControl9.Child = border50;
		Border border52 = (border4 = border51);
		context.PushParent(border4);
		Border border53 = border4;
		border53.Name = "ChatWidgetBorder";
		obj = border53;
		context.AvaloniaNameScope.Register("ChatWidgetBorder", obj);
		border53.Classes.Add("WidgetContainer");
		CompiledBindingExtension compiledBindingExtension62 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsInteractiveMode!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = "class:Interactive";
		CompiledBindingExtension compiledBindingExtension63 = compiledBindingExtension62.ProvideValue(context);
		context.ProvideTargetProperty = null;
		binding3 = compiledBindingExtension63;
		border53.BindClass("Interactive", binding3, null);
		CompiledBindingExtension compiledBindingExtension64 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsChatPanelDragging!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = "class:Dragging";
		CompiledBindingExtension compiledBindingExtension65 = compiledBindingExtension64.ProvideValue(context);
		context.ProvideTargetProperty = null;
		binding3 = compiledBindingExtension65;
		border53.BindClass("Dragging", binding3, null);
		border53.Padding = new Thickness(8.0, 8.0, 8.0, 8.0);
		StyledProperty<double> opacityProperty2 = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension66 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.Opacity!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension67 = compiledBindingExtension66.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border53, opacityProperty2, compiledBindingExtension67);
		Grid grid31;
		Grid grid30 = (grid31 = new Grid());
		((ISupportInitialize)grid30).BeginInit();
		border53.Child = grid30;
		Grid grid32 = (grid4 = grid31);
		context.PushParent(grid4);
		Grid grid33 = grid4;
		global::Avalonia.Controls.Controls children35 = grid33.Children;
		StackPanel stackPanel15;
		StackPanel stackPanel14 = (stackPanel15 = new StackPanel());
		((ISupportInitialize)stackPanel14).BeginInit();
		children35.Add(stackPanel14);
		StackPanel stackPanel16 = (stackPanel4 = stackPanel15);
		context.PushParent(stackPanel4);
		StackPanel stackPanel17 = stackPanel4;
		stackPanel17.Name = "ChatPanel";
		obj = stackPanel17;
		context.AvaloniaNameScope.Register("ChatPanel", obj);
		stackPanel17.Width = 270.0;
		stackPanel17.ClipToBounds = false;
		global::Avalonia.Controls.Controls children36 = stackPanel17.Children;
		Border border55;
		Border border54 = (border55 = new Border());
		((ISupportInitialize)border54).BeginInit();
		children36.Add(border54);
		Border border56 = (border4 = border55);
		context.PushParent(border4);
		Border border57 = border4;
		border57.Classes.Add("WidgetHeader");
		StyledProperty<bool> isVisibleProperty17 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension68 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsInteractiveMode!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension69 = compiledBindingExtension68.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border57, isVisibleProperty17, compiledBindingExtension69);
		Grid grid35;
		Grid grid34 = (grid35 = new Grid());
		((ISupportInitialize)grid34).BeginInit();
		border57.Child = grid34;
		Grid grid36 = (grid4 = grid35);
		context.PushParent(grid4);
		Grid grid37 = grid4;
		ColumnDefinitions columnDefinitions3 = new ColumnDefinitions();
		columnDefinitions3.Capacity = 3;
		columnDefinitions3.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		columnDefinitions3.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		columnDefinitions3.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid37.ColumnDefinitions = columnDefinitions3;
		global::Avalonia.Controls.Controls children37 = grid37.Children;
		TextBlock textBlock31;
		TextBlock textBlock30 = (textBlock31 = new TextBlock());
		((ISupportInitialize)textBlock30).BeginInit();
		children37.Add(textBlock30);
		Grid.SetColumn(textBlock31, 0);
		textBlock31.Text = "Chat";
		textBlock31.Foreground = new ImmutableSolidColorBrush(uint.MaxValue);
		textBlock31.FontSize = 12.0;
		textBlock31.FontWeight = FontWeight.DemiBold;
		textBlock31.VerticalAlignment = VerticalAlignment.Center;
		((ISupportInitialize)textBlock31).EndInit();
		global::Avalonia.Controls.Controls children38 = grid37.Children;
		Button button11;
		Button button10 = (button11 = new Button());
		((ISupportInitialize)button10).BeginInit();
		children38.Add(button10);
		Button button12 = (button4 = button11);
		context.PushParent(button4);
		Button button13 = button4;
		Grid.SetColumn(button13, 2);
		button13.Classes.Add("WidgetSettingsButton");
		StyledProperty<ICommand?> commandProperty3 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension70 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.ToggleChatOverlayEnabledCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension71 = compiledBindingExtension70.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button13, commandProperty3, compiledBindingExtension71);
		ToolTip.SetPlacement(button13, PlacementMode.Bottom);
		ToolTip.SetVerticalOffset(button13, 4.0);
		ToolTip.SetShowDelay(button13, 300);
		RootToolTip rootToolTip11;
		RootToolTip rootToolTip10 = (rootToolTip11 = new RootToolTip());
		((ISupportInitialize)rootToolTip10).BeginInit();
		ToolTip.SetTip(button13, rootToolTip10);
		RootToolTip rootToolTip12 = (rootToolTip4 = rootToolTip11);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip13 = rootToolTip4;
		TextBlock textBlock33;
		TextBlock textBlock32 = (textBlock33 = new TextBlock());
		((ISupportInitialize)textBlock32).BeginInit();
		rootToolTip13.Content = textBlock32;
		TextBlock textBlock34 = (textBlock4 = textBlock33);
		context.PushParent(textBlock4);
		TextBlock textBlock35 = textBlock4;
		textBlock35.Text = "Toggle chat overlay visibility";
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj14 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock35, obj14);
		textBlock35.FontWeight = (FontWeight)450;
		textBlock35.FontSize = 14.0;
		textBlock35.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)textBlock34).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip12).EndInit();
		Panel panel7;
		Panel panel6 = (panel7 = new Panel());
		((ISupportInitialize)panel6).BeginInit();
		button13.Content = panel6;
		Panel panel8 = (panel4 = panel7);
		context.PushParent(panel4);
		Panel panel9 = panel4;
		global::Avalonia.Controls.Controls children39 = panel9.Children;
		RootSvgImage rootSvgImage23;
		RootSvgImage rootSvgImage22 = (rootSvgImage23 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage22).BeginInit();
		children39.Add(rootSvgImage22);
		RootSvgImage rootSvgImage24 = (rootSvgImage4 = rootSvgImage23);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage25 = rootSvgImage4;
		rootSvgImage25.Width = 14.0;
		rootSvgImage25.Height = 14.0;
		StyledProperty<string?> svgPathProperty6 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("PinFilledSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding10 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage25, svgPathProperty6, binding10);
		StyledProperty<bool> isVisibleProperty18 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension72 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsChatOverlayEnabled!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension73 = compiledBindingExtension72.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage25, isVisibleProperty18, compiledBindingExtension73);
		context.PopParent();
		((ISupportInitialize)rootSvgImage24).EndInit();
		global::Avalonia.Controls.Controls children40 = panel9.Children;
		RootSvgImage rootSvgImage27;
		RootSvgImage rootSvgImage26 = (rootSvgImage27 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage26).BeginInit();
		children40.Add(rootSvgImage26);
		RootSvgImage rootSvgImage28 = (rootSvgImage4 = rootSvgImage27);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage29 = rootSvgImage4;
		rootSvgImage29.Width = 14.0;
		rootSvgImage29.Height = 14.0;
		StyledProperty<string?> svgPathProperty7 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("PinSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding11 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage29, svgPathProperty7, binding11);
		StyledProperty<bool> isVisibleProperty19 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension74 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsChatOverlayEnabled!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension75 = compiledBindingExtension74.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage29, isVisibleProperty19, compiledBindingExtension75);
		context.PopParent();
		((ISupportInitialize)rootSvgImage28).EndInit();
		context.PopParent();
		((ISupportInitialize)panel8).EndInit();
		context.PopParent();
		((ISupportInitialize)button12).EndInit();
		context.PopParent();
		((ISupportInitialize)grid36).EndInit();
		context.PopParent();
		((ISupportInitialize)border56).EndInit();
		global::Avalonia.Controls.Controls children41 = stackPanel17.Children;
		ItemsControl itemsControl7;
		ItemsControl itemsControl6 = (itemsControl7 = new ItemsControl());
		((ISupportInitialize)itemsControl6).BeginInit();
		children41.Add(itemsControl6);
		ItemsControl itemsControl8 = (itemsControl4 = itemsControl7);
		context.PushParent(itemsControl4);
		ItemsControl itemsControl9 = itemsControl4;
		StyledProperty<IEnumerable?> itemsSourceProperty2 = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension76 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.ChatMessages!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension77 = compiledBindingExtension76.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl9, itemsSourceProperty2, compiledBindingExtension77);
		itemsControl9.ClipToBounds = false;
		StyledProperty<bool> isVisibleProperty20 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension78 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsChatOverlayEnabled!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension79 = compiledBindingExtension78.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl9, isVisibleProperty20, compiledBindingExtension79);
		itemsControl9.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_188.Build_6), context)
		};
		DataTemplate dataTemplate2 = new DataTemplate();
		dataTemplate2.DataType = typeof(OverlayMessageViewModel);
		dataTemplate2.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_188.Build_7), context);
		itemsControl9.ItemTemplate = dataTemplate2;
		context.PopParent();
		((ISupportInitialize)itemsControl8).EndInit();
		global::Avalonia.Controls.Controls children42 = stackPanel17.Children;
		Border border59;
		Border border58 = (border59 = new Border());
		((ISupportInitialize)border58).BeginInit();
		children42.Add(border58);
		Border border60 = (border4 = border59);
		context.PushParent(border4);
		Border border61 = border4;
		border61.Background = new ImmutableSolidColorBrush(1073741824u);
		border61.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		border61.Padding = new Thickness(16.0, 24.0, 16.0, 24.0);
		StyledProperty<bool> isVisibleProperty21 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension80 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsChatOverlayEnabled!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension81 = compiledBindingExtension80.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border61, isVisibleProperty21, compiledBindingExtension81);
		StackPanel stackPanel19;
		StackPanel stackPanel18 = (stackPanel19 = new StackPanel());
		((ISupportInitialize)stackPanel18).BeginInit();
		border61.Child = stackPanel18;
		StackPanel stackPanel20 = (stackPanel4 = stackPanel19);
		context.PushParent(stackPanel4);
		StackPanel stackPanel21 = stackPanel4;
		stackPanel21.HorizontalAlignment = HorizontalAlignment.Center;
		stackPanel21.Spacing = 4.0;
		global::Avalonia.Controls.Controls children43 = stackPanel21.Children;
		RootSvgImage rootSvgImage31;
		RootSvgImage rootSvgImage30 = (rootSvgImage31 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage30).BeginInit();
		children43.Add(rootSvgImage30);
		RootSvgImage rootSvgImage32 = (rootSvgImage4 = rootSvgImage31);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage33 = rootSvgImage4;
		rootSvgImage33.Width = 24.0;
		rootSvgImage33.Height = 24.0;
		StyledProperty<string?> svgPathProperty8 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("EyeSlashSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding12 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage33, svgPathProperty8, binding12);
		rootSvgImage33.HorizontalAlignment = HorizontalAlignment.Center;
		rootSvgImage33.Opacity = 0.5;
		context.PopParent();
		((ISupportInitialize)rootSvgImage32).EndInit();
		global::Avalonia.Controls.Controls children44 = stackPanel21.Children;
		TextBlock textBlock37;
		TextBlock textBlock36 = (textBlock37 = new TextBlock());
		((ISupportInitialize)textBlock36).BeginInit();
		children44.Add(textBlock36);
		textBlock37.Text = "Chat Hidden";
		textBlock37.Foreground = new ImmutableSolidColorBrush(2164260863u);
		textBlock37.FontSize = 11.0;
		textBlock37.HorizontalAlignment = HorizontalAlignment.Center;
		((ISupportInitialize)textBlock37).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel20).EndInit();
		context.PopParent();
		((ISupportInitialize)border60).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel16).EndInit();
		global::Avalonia.Controls.Controls children45 = grid33.Children;
		Border border63;
		Border border62 = (border63 = new Border());
		((ISupportInitialize)border62).BeginInit();
		children45.Add(border62);
		Border border64 = (border4 = border63);
		context.PushParent(border4);
		Border border65 = border4;
		border65.Name = "ChatResizeGrip";
		obj = border65;
		context.AvaloniaNameScope.Register("ChatResizeGrip", obj);
		border65.Classes.Add("ResizeGrip");
		border65.HorizontalAlignment = HorizontalAlignment.Right;
		border65.VerticalAlignment = VerticalAlignment.Bottom;
		border65.Margin = new Thickness(0.0, 0.0, -4.0, -4.0);
		StyledProperty<bool> isVisibleProperty22 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension82 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Overlay.OverlayViewModel,RootApp.Client.Avalonia.IsInteractiveMode!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension83 = compiledBindingExtension82.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border65, isVisibleProperty22, compiledBindingExtension83);
		ToolTip.SetPlacement(border65, PlacementMode.Left);
		ToolTip.SetHorizontalOffset(border65, -4.0);
		ToolTip.SetShowDelay(border65, 300);
		RootToolTip rootToolTip15;
		RootToolTip rootToolTip14 = (rootToolTip15 = new RootToolTip());
		((ISupportInitialize)rootToolTip14).BeginInit();
		ToolTip.SetTip(border65, rootToolTip14);
		RootToolTip rootToolTip16 = (rootToolTip4 = rootToolTip15);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip17 = rootToolTip4;
		TextBlock textBlock39;
		TextBlock textBlock38 = (textBlock39 = new TextBlock());
		((ISupportInitialize)textBlock38).BeginInit();
		rootToolTip17.Content = textBlock38;
		TextBlock textBlock40 = (textBlock4 = textBlock39);
		context.PushParent(textBlock4);
		TextBlock textBlock41 = textBlock4;
		textBlock41.Text = "Drag to resize";
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj15 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock41, obj15);
		textBlock41.FontWeight = (FontWeight)450;
		textBlock41.FontSize = 14.0;
		textBlock41.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)textBlock40).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip16).EndInit();
		Path path4;
		Path path3 = (path4 = new Path());
		((ISupportInitialize)path3).BeginInit();
		border65.Child = path3;
		path4.Data = Geometry.Parse("M 0,12 L 12,0 M 4,12 L 12,4 M 8,12 L 12,8");
		path4.Stroke = new ImmutableSolidColorBrush(2164260863u);
		path4.StrokeThickness = 1.5;
		((ISupportInitialize)path4).EndInit();
		context.PopParent();
		((ISupportInitialize)border64).EndInit();
		context.PopParent();
		((ISupportInitialize)grid32).EndInit();
		context.PopParent();
		((ISupportInitialize)border52).EndInit();
		context.PopParent();
		((ISupportInitialize)layoutTransformControl8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void !XamlIlPopulateTrampoline(OverlayWindow P_0)
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
