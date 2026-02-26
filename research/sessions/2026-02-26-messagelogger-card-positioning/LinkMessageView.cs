using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Resources.Converters.Messages;

namespace RootApp.Client.Avalonia.UI.Messages;

public class LinkMessageView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_176
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<LinkMessageView> context = CreateContext(P_0);
			return new WebsitePreviewMaxWidthConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<LinkMessageView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<LinkMessageView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<LinkMessageView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Messages/LinkMessageView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Messages/LinkMessageView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (LinkMessageView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<LinkMessageView> context = CreateContext(P_0);
			return new WebsitePreviewHorizontalAlignmentConverter();
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<LinkMessageView> context = CreateContext(P_0);
			return new WebsitePreviewMarginConverter();
		}

		public static object Build_4(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<LinkMessageView> context = CreateContext(P_0);
			return new WebsitePreviewBorderThicknessConverter();
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal UserControl MainView;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootBorder MainBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock SiteName;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootLinkButton Title;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock Description;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal ContentControl ImageContentControl;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	private LinkMessageViewModel? _linkMessageViewModel => base.DataContext as LinkMessageViewModel;

	public LinkMessageView()
	{
		InitializeComponent();
	}

	protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnAttachedToVisualTree(P_0);
		LinkMessageViewModel linkMessageViewModel = _linkMessageViewModel;
		if (linkMessageViewModel != null && linkMessageViewModel.FullScreenContent)
		{
			MainBorder.Background = Brushes.Transparent;
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
		MainView = nameScope?.Find<UserControl>("MainView");
		MainBorder = nameScope?.Find<RootBorder>("MainBorder");
		SiteName = nameScope?.Find<TextBlock>("SiteName");
		Title = nameScope?.Find<RootLinkButton>("Title");
		Description = nameScope?.Find<TextBlock>("Description");
		ImageContentControl = nameScope?.Find<ContentControl>("ImageContentControl");
	}

	[CompilerGenerated]
	private unsafe static void !XamlIlPopulate(IServiceProvider P_0, LinkMessageView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<LinkMessageView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<LinkMessageView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Messages/LinkMessageView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Messages/LinkMessageView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.LinkMessageViewModel,RootApp.Client.Avalonia.HasFinishedRendered!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(P_1, isVisibleProperty, compiledBindingExtension2);
		P_1.Name = "MainView";
		object obj = P_1;
		context.AvaloniaNameScope.Register("MainView", obj);
		ResourceDictionary resourceDictionary = new ResourceDictionary();
		if (resourceDictionary is ResourceDictionary resourceDictionary2)
		{
			resourceDictionary2.EnsureCapacity(resourceDictionary2.Count + 4);
		}
		resourceDictionary.AddDeferred("WebsitePreviewMaxWidthConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_176.Build_1), context));
		resourceDictionary.AddDeferred("WebsitePreviewHorizontalAlignmentConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_176.Build_2), context));
		resourceDictionary.AddDeferred("WebsitePreviewMarginConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_176.Build_3), context));
		resourceDictionary.AddDeferred("WebsitePreviewBorderThicknessConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_176.Build_4), context));
		P_1.Resources = resourceDictionary;
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		P_1.Content = rootBorder;
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, backgroundProperty, binding);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, borderBrushProperty, binding2);
		rootBorder4.CornerRadius = new CornerRadius(10.0, 10.0, 10.0, 10.0);
		StyledProperty<HorizontalAlignment> horizontalAlignmentProperty = Layoutable.HorizontalAlignmentProperty;
		CompiledBindingExtension compiledBindingExtension4;
		CompiledBindingExtension compiledBindingExtension3 = (compiledBindingExtension4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.LinkMessageViewModel,RootApp.Client.Avalonia.FullScreenContent!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension4);
		CompiledBindingExtension compiledBindingExtension5 = compiledBindingExtension4;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("WebsitePreviewHorizontalAlignmentConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj2 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension5.Converter = (IValueConverter)obj2;
		context.PopParent();
		context.ProvideTargetProperty = Layoutable.HorizontalAlignmentProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, horizontalAlignmentProperty, compiledBindingExtension6);
		StyledProperty<double> maxWidthProperty = Layoutable.MaxWidthProperty;
		CompiledBindingExtension compiledBindingExtension7 = (compiledBindingExtension4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.LinkMessageViewModel,RootApp.Client.Avalonia.FullScreenContent!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension4);
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension4;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("WebsitePreviewMaxWidthConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj3 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension8.Converter = (IValueConverter)obj3;
		context.PopParent();
		context.ProvideTargetProperty = Layoutable.MaxWidthProperty;
		CompiledBindingExtension compiledBindingExtension9 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, maxWidthProperty, compiledBindingExtension9);
		StyledProperty<Thickness> marginProperty = Layoutable.MarginProperty;
		CompiledBindingExtension compiledBindingExtension10 = (compiledBindingExtension4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.LinkMessageViewModel,RootApp.Client.Avalonia.FullScreenContent!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension4);
		CompiledBindingExtension compiledBindingExtension11 = compiledBindingExtension4;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("WebsitePreviewMarginConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj4 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension11.Converter = (IValueConverter)obj4;
		context.PopParent();
		context.ProvideTargetProperty = Layoutable.MarginProperty;
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, marginProperty, compiledBindingExtension12);
		StyledProperty<Thickness> dynamicBorderThicknessProperty = RootBorder.DynamicBorderThicknessProperty;
		CompiledBindingExtension compiledBindingExtension13 = (compiledBindingExtension4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.LinkMessageViewModel,RootApp.Client.Avalonia.FullScreenContent!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension4);
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension4;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("WebsitePreviewBorderThicknessConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj5 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension14.Converter = (IValueConverter)obj5;
		context.PopParent();
		context.ProvideTargetProperty = RootBorder.DynamicBorderThicknessProperty;
		CompiledBindingExtension compiledBindingExtension15 = compiledBindingExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, dynamicBorderThicknessProperty, compiledBindingExtension15);
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension16 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.LinkMessageViewModel,RootApp.Client.Avalonia.ShouldRenderAnything!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension17 = compiledBindingExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, isVisibleProperty2, compiledBindingExtension17);
		rootBorder4.Name = "MainBorder";
		obj = rootBorder4;
		context.AvaloniaNameScope.Register("MainBorder", obj);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		rootBorder4.Child = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		StyledProperty<Thickness> marginProperty2 = Layoutable.MarginProperty;
		CompiledBindingExtension compiledBindingExtension18 = (compiledBindingExtension4 = new CompiledBindingExtension());
		context.PushParent(compiledBindingExtension4);
		CompiledBindingExtension compiledBindingExtension19 = compiledBindingExtension4;
		compiledBindingExtension19.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.LinkMessageViewModel,RootApp.Client.Avalonia.FullScreenContent!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build();
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("MarginIfFalseConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj6 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension19.Converter = (IValueConverter)obj6;
		compiledBindingExtension19.ConverterParameter = "12,12,12,12";
		context.PopParent();
		context.ProvideTargetProperty = Layoutable.MarginProperty;
		CompiledBindingExtension compiledBindingExtension20 = compiledBindingExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(grid4, marginProperty2, compiledBindingExtension20);
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		global::Avalonia.Controls.Controls children = grid4.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension21 = (compiledBindingExtension4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.LinkMessageViewModel,RootApp.Client.Avalonia.FullScreenContent!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension4);
		CompiledBindingExtension compiledBindingExtension22 = compiledBindingExtension4;
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("BoolInverterConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj7 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension22.Converter = (IValueConverter)obj7;
		context.PopParent();
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension23 = compiledBindingExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(stackPanel4, isVisibleProperty3, compiledBindingExtension23);
		global::Avalonia.Controls.Controls children2 = stackPanel4.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children2.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Name = "SiteName";
		obj = textBlock5;
		context.AvaloniaNameScope.Register("SiteName", obj);
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension24 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.LinkMessageViewModel,RootApp.Client.Avalonia.SiteName!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension25 = compiledBindingExtension24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, compiledBindingExtension25);
		textBlock5.Margin = new Thickness(0.0, 0.0, 0.0, 8.0);
		textBlock5.TextWrapping = TextWrapping.Wrap;
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj8 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.LinkMessageViewModel,RootApp.Client.Avalonia.SiteName!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Converter = ObjectConverters.IsNotNull
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension26 = obj8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, isVisibleProperty4, compiledBindingExtension26);
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding3);
		textBlock5.FontSize = 13.0;
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj9 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock5, obj9);
		textBlock5.FontWeight = (FontWeight)450;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		global::Avalonia.Controls.Controls children3 = stackPanel4.Children;
		RootLinkButton rootLinkButton2;
		RootLinkButton rootLinkButton = (rootLinkButton2 = new RootLinkButton());
		((ISupportInitialize)rootLinkButton).BeginInit();
		children3.Add(rootLinkButton);
		RootLinkButton rootLinkButton4;
		RootLinkButton rootLinkButton3 = (rootLinkButton4 = rootLinkButton2);
		context.PushParent(rootLinkButton4);
		rootLinkButton4.Name = "Title";
		obj = rootLinkButton4;
		context.AvaloniaNameScope.Register("Title", obj);
		CompiledBindingExtension compiledBindingExtension27 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.LinkMessageViewModel,RootApp.Client.Avalonia.Title!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension28 = compiledBindingExtension27.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(rootLinkButton4, compiledBindingExtension28);
		StyledProperty<IBrush?> foregroundProperty2 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("Link");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootLinkButton4, foregroundProperty2, binding4);
		rootLinkButton4.FontSize = 15.0;
		StaticResourceExtension staticResourceExtension8 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj10 = staticResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_3(rootLinkButton4, obj10);
		rootLinkButton4.FontWeight = FontWeight.Medium;
		StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.LinkMessageViewModel,RootApp.Client.Avalonia.Title!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Converter = ObjectConverters.IsNotNull
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension29 = obj11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootLinkButton4, isVisibleProperty5, compiledBindingExtension29);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension30 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.LinkMessageViewModel,RootApp.Client.Avalonia.OpenLinkCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension31 = compiledBindingExtension30.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootLinkButton4, commandProperty, compiledBindingExtension31);
		context.PopParent();
		((ISupportInitialize)rootLinkButton3).EndInit();
		global::Avalonia.Controls.Controls children4 = stackPanel4.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children4.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Name = "Description";
		obj = textBlock9;
		context.AvaloniaNameScope.Register("Description", obj);
		textBlock9.TextWrapping = TextWrapping.Wrap;
		StyledProperty<string?> textProperty2 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension32 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.LinkMessageViewModel,RootApp.Client.Avalonia.Description!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension33 = compiledBindingExtension32.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, textProperty2, compiledBindingExtension33);
		StyledProperty<bool> isVisibleProperty6 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj12 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.LinkMessageViewModel,RootApp.Client.Avalonia.Description!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Converter = ObjectConverters.IsNotNull
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension34 = obj12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, isVisibleProperty6, compiledBindingExtension34);
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty3, binding5);
		textBlock9.FontSize = 13.0;
		StaticResourceExtension staticResourceExtension9 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj13 = staticResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock9, obj13);
		textBlock9.FontWeight = (FontWeight)450;
		textBlock9.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		global::Avalonia.Controls.Controls children5 = grid4.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children5.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Grid.SetRow(border4, 1);
		StyledProperty<Thickness> marginProperty3 = Layoutable.MarginProperty;
		CompiledBindingExtension compiledBindingExtension35 = (compiledBindingExtension4 = new CompiledBindingExtension());
		context.PushParent(compiledBindingExtension4);
		CompiledBindingExtension compiledBindingExtension36 = compiledBindingExtension4;
		compiledBindingExtension36.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.LinkMessageViewModel,RootApp.Client.Avalonia.FullScreenContent!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build();
		StaticResourceExtension staticResourceExtension10 = new StaticResourceExtension("MarginIfFalseConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj14 = staticResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension36.Converter = (IValueConverter)obj14;
		compiledBindingExtension36.ConverterParameter = "0,8,0,0";
		context.PopParent();
		context.ProvideTargetProperty = Layoutable.MarginProperty;
		CompiledBindingExtension compiledBindingExtension37 = compiledBindingExtension35.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border4, marginProperty3, compiledBindingExtension37);
		StyledProperty<bool> isVisibleProperty7 = Visual.IsVisibleProperty;
		MultiBinding multiBinding2;
		MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		multiBinding2.Converter = BoolConverters.Or;
		IList<IBinding> bindings = multiBinding2.Bindings;
		CompiledBindingExtension obj15 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "SiteName").Property(Visual.IsVisibleProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item = obj15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings.Add(item);
		IList<IBinding> bindings2 = multiBinding2.Bindings;
		CompiledBindingExtension obj16 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "Title").Property(Visual.IsVisibleProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item2 = obj16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings2.Add(item2);
		IList<IBinding> bindings3 = multiBinding2.Bindings;
		CompiledBindingExtension obj17 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "Description").Property(Visual.IsVisibleProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item3 = obj17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings3.Add(item3);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(border4, isVisibleProperty7, multiBinding);
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		global::Avalonia.Controls.Controls children6 = grid4.Children;
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		children6.Add(contentControl);
		ContentControl contentControl4;
		ContentControl contentControl3 = (contentControl4 = contentControl2);
		context.PushParent(contentControl4);
		Grid.SetRow(contentControl4, 2);
		contentControl4.Name = "ImageContentControl";
		obj = contentControl4;
		context.AvaloniaNameScope.Register("ImageContentControl", obj);
		contentControl4.VerticalAlignment = VerticalAlignment.Top;
		CompiledBindingExtension compiledBindingExtension38 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.LinkMessageViewModel,RootApp.Client.Avalonia.Content!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension39 = compiledBindingExtension38.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl4, compiledBindingExtension39);
		StyledProperty<bool> isVisibleProperty8 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj18 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.LinkMessageViewModel,RootApp.Client.Avalonia.Content!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Converter = ObjectConverters.IsNotNull
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension40 = obj18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(contentControl4, isVisibleProperty8, compiledBindingExtension40);
		context.PopParent();
		((ISupportInitialize)contentControl3).EndInit();
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
	private static void !XamlIlPopulateTrampoline(LinkMessageView P_0)
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
