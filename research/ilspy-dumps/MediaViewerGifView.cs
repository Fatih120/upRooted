using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using CompiledAvaloniaXaml;
using RootApp.SkiaImageLoader;

namespace RootApp.Client.Avalonia.UI.MediaViewer;

public class MediaViewerGifView : UserControl
{
	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border GifPlaceholderContainer;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal SkiaImageControl SkiaImage;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public MediaViewerGifView()
	{
		InitializeComponent();
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	[ExcludeFromCodeCoverage]
	public void InitializeComponent(bool P_0 = true)
	{
		if (P_0)
		{
			_0021XamlIlPopulateTrampoline(this);
		}
		INameScope nameScope = this.FindNameScope();
		GifPlaceholderContainer = nameScope?.Find<Border>("GifPlaceholderContainer");
		SkiaImage = nameScope?.Find<SkiaImageControl>("SkiaImage");
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulate(IServiceProvider P_0, MediaViewerGifView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<MediaViewerGifView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MediaViewerGifView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FMediaViewer_002FMediaViewerGifView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/MediaViewer/MediaViewerGifView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		P_1.Content = border;
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		border4.Name = "GifPlaceholderContainer";
		object obj = border4;
		context.AvaloniaNameScope.Register("GifPlaceholderContainer", obj);
		border4.CornerRadius = new CornerRadius(10.0, 10.0, 10.0, 10.0);
		border4.ClipToBounds = true;
		border4.VerticalAlignment = VerticalAlignment.Center;
		border4.HorizontalAlignment = HorizontalAlignment.Center;
		StyledProperty<double> maxHeightProperty = Layoutable.MaxHeightProperty;
		CompiledBindingExtension obj2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002EMediaViewerGifViewModel_002CRootApp_002EClient_002EAvalonia_002EPlaceholderHeight_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.OneWay
		};
		context.ProvideTargetProperty = Layoutable.MaxHeightProperty;
		CompiledBindingExtension compiledBindingExtension = obj2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border4, maxHeightProperty, compiledBindingExtension);
		StyledProperty<double> maxWidthProperty = Layoutable.MaxWidthProperty;
		CompiledBindingExtension obj3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002EMediaViewerGifViewModel_002CRootApp_002EClient_002EAvalonia_002EPlaceholderWidth_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.OneWay
		};
		context.ProvideTargetProperty = Layoutable.MaxWidthProperty;
		CompiledBindingExtension compiledBindingExtension2 = obj3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border4, maxWidthProperty, compiledBindingExtension2);
		SkiaImageControl skiaImageControl2;
		SkiaImageControl skiaImageControl = (skiaImageControl2 = new SkiaImageControl());
		((ISupportInitialize)skiaImageControl).BeginInit();
		border4.Child = skiaImageControl;
		SkiaImageControl skiaImageControl4;
		SkiaImageControl skiaImageControl3 = (skiaImageControl4 = skiaImageControl2);
		context.PushParent(skiaImageControl4);
		skiaImageControl4.Name = "SkiaImage";
		obj = skiaImageControl4;
		context.AvaloniaNameScope.Register("SkiaImage", obj);
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension();
		compiledBindingExtension3.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EMediaViewer_002EMediaViewerGifViewModel_002CRootApp_002EClient_002EAvalonia_002EImageData_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build();
		compiledBindingExtension3.Mode = BindingMode.OneWay;
		context.ProvideTargetProperty = SkiaImageControl.SourceProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_29(skiaImageControl4, compiledBindingExtension4);
		skiaImageControl4.Stretch = Stretch.Uniform;
		context.PopParent();
		((ISupportInitialize)skiaImageControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(MediaViewerGifView P_0)
	{
		if (_0021XamlIlPopulateOverride != null)
		{
			_0021XamlIlPopulateOverride(P_0);
		}
		else
		{
			_0021XamlIlPopulate(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(null), P_0);
		}
	}
}
