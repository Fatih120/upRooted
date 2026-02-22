using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Media.Immutable;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Resources.Converters.Messages;

namespace RootApp.Client.Avalonia.UI.Messages;

public class ReactionView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_181
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ReactionView> context = CreateContext(P_0);
			return new MessageReactionBorderThicknessConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<ReactionView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ReactionView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ReactionView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Messages/ReactionView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Messages/ReactionView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (ReactionView)service;
				}
			}
			return context;
		}
	}

	private RootImageLoader? _tooltipEmojiLoader;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border TooltipEmojiContainer;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Button ReactionBorder;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	private ReactionViewModel _reactionViewModel => (ReactionViewModel)base.DataContext;

	public ReactionView()
	{
		InitializeComponent();
	}

	private async void onToolTipGridAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
	{
		if (_reactionViewModel.LoadReactionDataCommand.CanExecute(null))
		{
			_reactionViewModel.LoadReactionDataCommand.Execute(null);
		}
		_tooltipEmojiLoader = new RootImageLoader
		{
			LoadingPlaceholderSize = 0.0,
			Stretch = Stretch.Uniform
		};
		RenderOptions.SetBitmapInterpolationMode(_tooltipEmojiLoader, BitmapInterpolationMode.MediumQuality);
		TooltipEmojiContainer.Child = _tooltipEmojiLoader;
		BitmapWrapper bitmapWrapper = await _reactionViewModel.EmojiAsyncBitmapWrapper;
		if (_tooltipEmojiLoader != null)
		{
			_tooltipEmojiLoader.Source = bitmapWrapper;
		}
	}

	private void onToolTipGridDetachedFromVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
	{
		TooltipEmojiContainer.Child = null;
		_tooltipEmojiLoader = null;
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
		TooltipEmojiContainer = nameScope?.Find<Border>("TooltipEmojiContainer");
		ReactionBorder = nameScope?.Find<Button>("ReactionBorder");
	}

	[CompilerGenerated]
	private unsafe static void !XamlIlPopulate(IServiceProvider P_0, ReactionView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<ReactionView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ReactionView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Messages/ReactionView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Messages/ReactionView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.Margin = new Thickness(0.0, 4.0, 4.0, 0.0);
		P_1.Height = 30.0;
		RenderOptions.SetBitmapInterpolationMode(P_1, BitmapInterpolationMode.MediumQuality);
		ResourceDictionary resourceDictionary = new ResourceDictionary();
		resourceDictionary.AddDeferred("MessageReactionBorderThicknessConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_181.Build_1), context));
		P_1.Resources = resourceDictionary;
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		P_1.Content = panel;
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		ToolTip.SetPlacement(panel4, PlacementMode.Top);
		ToolTip.SetVerticalOffset(panel4, -4.0);
		ToolTip.SetHorizontalOffset(panel4, 0.0);
		ToolTip.SetShowDelay(panel4, 500);
		RootToolTip rootToolTip2;
		RootToolTip rootToolTip = (rootToolTip2 = new RootToolTip());
		((ISupportInitialize)rootToolTip).BeginInit();
		ToolTip.SetTip(panel4, rootToolTip);
		RootToolTip rootToolTip4;
		RootToolTip rootToolTip3 = (rootToolTip4 = rootToolTip2);
		context.PushParent(rootToolTip4);
		ToolTip.SetPlacement(rootToolTip4, PlacementMode.Top);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		rootToolTip4.Content = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		Grid grid5 = grid4;
		grid5.Margin = new Thickness(6.0, 6.0, 6.0, 6.0);
		grid5.MaxWidth = 300.0;
		grid5.AttachedToVisualTree += context.RootObject.onToolTipGridAttachedToVisualTree;
		grid5.DetachedFromVisualTree += context.RootObject.onToolTipGridDetachedFromVisualTree;
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(12.0, GridUnitType.Pixel)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		global::Avalonia.Controls.Controls children = grid5.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children.Add(border);
		border2.Name = "TooltipEmojiContainer";
		object obj = border2;
		context.AvaloniaNameScope.Register("TooltipEmojiContainer", obj);
		border2.Width = 32.0;
		border2.Height = 32.0;
		((ISupportInitialize)border2).EndInit();
		global::Avalonia.Controls.Controls children2 = grid5.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children2.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		Grid.SetColumn(textBlock5, 2);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock5, obj2);
		textBlock5.FontWeight = (FontWeight)450;
		textBlock5.FontSize = 14.0;
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding);
		textBlock5.TextWrapping = TextWrapping.Wrap;
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		textBlock5.LineSpacing = 2.0;
		textBlock5.TextAlignment = TextAlignment.Center;
		InlineCollection inlineCollection;
		InlineCollection inlines = (inlineCollection = new InlineCollection());
		context.PushParent(inlineCollection);
		Run run = new Run();
		((ISupportInitialize)run).BeginInit();
		Run run2 = run;
		context.PushParent(run2);
		Run run3 = run2;
		StyledProperty<string?> textProperty = Run.TextProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.ReactionViewModel,RootApp.Client.Avalonia.CombinedUsername!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Run.TextProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(run3, textProperty, compiledBindingExtension2);
		context.PopParent();
		((ISupportInitialize)run).EndInit();
		inlineCollection.Add(run);
		Run run4 = new Run();
		((ISupportInitialize)run4).BeginInit();
		run2 = run4;
		context.PushParent(run2);
		Run run5 = run2;
		StyledProperty<string?> textProperty2 = Run.TextProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.ReactionViewModel,RootApp.Client.Avalonia.ExtraCount!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Run.TextProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(run5, textProperty2, compiledBindingExtension4);
		AttachedProperty<IBrush?> foregroundProperty2 = TextElement.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TextElement.ForegroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(run5, foregroundProperty2, binding2);
		context.PopParent();
		((ISupportInitialize)run4).EndInit();
		inlineCollection.Add(run4);
		LineBreak lineBreak = new LineBreak();
		((ISupportInitialize)lineBreak).BeginInit();
		((ISupportInitialize)lineBreak).EndInit();
		inlineCollection.Add(lineBreak);
		Run run6 = new Run();
		((ISupportInitialize)run6).BeginInit();
		run2 = run6;
		context.PushParent(run2);
		Run run7 = run2;
		run7.Text = "reacted with ";
		AttachedProperty<IBrush?> foregroundProperty3 = TextElement.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TextElement.ForegroundProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(run7, foregroundProperty3, binding3);
		context.PopParent();
		((ISupportInitialize)run6).EndInit();
		inlineCollection.Add(run6);
		Run run8 = new Run();
		((ISupportInitialize)run8).BeginInit();
		run2 = run8;
		context.PushParent(run2);
		Run run9 = run2;
		StyledProperty<string?> textProperty3 = Run.TextProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.ReactionViewModel,RootApp.Client.Avalonia.Reaction!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.Reaction,RootApp.Client.CoreDomain.ShortCode!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Run.TextProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(run9, textProperty3, compiledBindingExtension6);
		AttachedProperty<IBrush?> foregroundProperty4 = TextElement.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextElement.ForegroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(run9, foregroundProperty4, binding4);
		context.PopParent();
		((ISupportInitialize)run8).EndInit();
		inlineCollection.Add(run8);
		context.PopParent();
		textBlock5.Inlines = inlines;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip3).EndInit();
		global::Avalonia.Controls.Controls children3 = panel4.Children;
		Border border4;
		Border border3 = (border4 = new Border());
		((ISupportInitialize)border3).BeginInit();
		children3.Add(border3);
		Border border6;
		Border border5 = (border6 = border4);
		context.PushParent(border6);
		Border border7 = border6;
		border7.Opacity = 0.1;
		border7.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border7, backgroundProperty, binding5);
		border7.BorderBrush = new ImmutableSolidColorBrush(16777215u);
		border7.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.ReactionViewModel,RootApp.Client.Avalonia.Reaction!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.Reaction,RootApp.Client.CoreDomain.ContainsSelf!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border7, isVisibleProperty, compiledBindingExtension8);
		context.PopParent();
		((ISupportInitialize)border5).EndInit();
		global::Avalonia.Controls.Controls children4 = panel4.Children;
		Border border9;
		Border border8 = (border9 = new Border());
		((ISupportInitialize)border8).BeginInit();
		children4.Add(border8);
		Border border10 = (border6 = border9);
		context.PushParent(border6);
		Border border11 = border6;
		border11.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		border11.Opacity = 0.64;
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border11, backgroundProperty2, binding6);
		border11.BorderBrush = new ImmutableSolidColorBrush(16777215u);
		border11.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.ReactionViewModel,RootApp.Client.Avalonia.Reaction!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.Reaction,RootApp.Client.CoreDomain.ContainsSelf!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border11, isVisibleProperty2, compiledBindingExtension10);
		context.PopParent();
		((ISupportInitialize)border10).EndInit();
		global::Avalonia.Controls.Controls children5 = panel4.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children5.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		button4.Classes.Add("BasicButtonNeverOpaque");
		button4.Name = "ReactionBorder";
		obj = button4;
		context.AvaloniaNameScope.Register("ReactionBorder", obj);
		button4.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<IBrush?> borderBrushProperty = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, borderBrushProperty, binding7);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.ReactionViewModel,RootApp.Client.Avalonia.ReactionSelectedCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, commandProperty, compiledBindingExtension12);
		button4.Background = new ImmutableSolidColorBrush(16777215u);
		StyledProperty<bool> isEnabledProperty = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.ReactionViewModel,RootApp.Client.Avalonia.Message!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.Message,RootApp.Client.CoreDomain.MessageContainer!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.IMessageContainer,RootApp.Client.CoreDomain.LocalChannelPermission!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.LocalChannelPermission,RootApp.Client.CoreDomain.ChannelCreateMessageReaction!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, isEnabledProperty, compiledBindingExtension14);
		StyledProperty<Thickness> borderThicknessProperty = TemplatedControl.BorderThicknessProperty;
		CompiledBindingExtension compiledBindingExtension16;
		CompiledBindingExtension compiledBindingExtension15 = (compiledBindingExtension16 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.ReactionViewModel,RootApp.Client.Avalonia.Reaction!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.Reaction,RootApp.Client.CoreDomain.ContainsSelf!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension16);
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("MessageReactionBorderThicknessConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj3 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension16.Converter = (IValueConverter)obj3;
		context.PopParent();
		context.ProvideTargetProperty = TemplatedControl.BorderThicknessProperty;
		CompiledBindingExtension compiledBindingExtension17 = compiledBindingExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, borderThicknessProperty, compiledBindingExtension17);
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		button4.Content = grid6;
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		grid9.Margin = new Thickness(8.0, 2.0, 8.0, 2.0);
		ColumnDefinitions columnDefinitions = new ColumnDefinitions();
		columnDefinitions.Capacity = 3;
		columnDefinitions.Add(new ColumnDefinition(new GridLength(16.0, GridUnitType.Pixel)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(10.0, GridUnitType.Pixel)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid9.ColumnDefinitions = columnDefinitions;
		global::Avalonia.Controls.Controls children6 = grid9.Children;
		RootImageLoader rootImageLoader2;
		RootImageLoader rootImageLoader = (rootImageLoader2 = new RootImageLoader());
		((ISupportInitialize)rootImageLoader).BeginInit();
		children6.Add(rootImageLoader);
		RootImageLoader rootImageLoader4;
		RootImageLoader rootImageLoader3 = (rootImageLoader4 = rootImageLoader2);
		context.PushParent(rootImageLoader4);
		rootImageLoader4.Width = 20.0;
		rootImageLoader4.Height = 20.0;
		StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension18 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.ReactionViewModel,RootApp.Client.Avalonia.EmojiAsyncBitmapWrapper!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
		context.ProvideTargetProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension19 = compiledBindingExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, sourceProperty, compiledBindingExtension19);
		rootImageLoader4.LoadingPlaceholderSize = 0.0;
		context.PopParent();
		((ISupportInitialize)rootImageLoader3).EndInit();
		global::Avalonia.Controls.Controls children7 = grid9.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children7.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		Grid.SetColumn(textBlock9, 2);
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<string?> textProperty4 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension20 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.ReactionViewModel,RootApp.Client.Avalonia.Reaction!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.Reaction,RootApp.Client.CoreDomain.Count!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension21 = compiledBindingExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, textProperty4, compiledBindingExtension21);
		textBlock9.VerticalAlignment = VerticalAlignment.Center;
		textBlock9.HorizontalAlignment = HorizontalAlignment.Center;
		textBlock9.FontSize = 16.0;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj4 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock9, obj4);
		textBlock9.FontWeight = (FontWeight)450;
		StyledProperty<IBrush?> foregroundProperty5 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty5, binding8);
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
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
	private static void !XamlIlPopulateTrampoline(ReactionView P_0)
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
