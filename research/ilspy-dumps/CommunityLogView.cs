using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Markdown;

namespace RootApp.Client.Avalonia.UI.Community.Settings;

public class CommunityLogView : UserControl
{
	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal UserControl ActionLogUserControl;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgImage ActionLogIconSvgImage;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	private CommunityLogViewModel? _actionLogViewModel => base.DataContext as CommunityLogViewModel;

	public CommunityLogView()
	{
		InitializeComponent();
	}

	protected override void OnLoaded(RoutedEventArgs P_0)
	{
		base.OnLoaded(P_0);
		if (_actionLogViewModel != null)
		{
			ActionLogIconSvgImage[!RootSvgImage.SvgPathProperty] = new DynamicResourceExtension(_actionLogViewModel.ActionLogIconPath);
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
		ActionLogUserControl = nameScope?.Find<UserControl>("ActionLogUserControl");
		ActionLogIconSvgImage = nameScope?.Find<RootSvgImage>("ActionLogIconSvgImage");
	}

	[CompilerGenerated]
	private static void !XamlIlPopulate(IServiceProvider P_0, CommunityLogView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<CommunityLogView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<CommunityLogView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Community/Settings/CommunityLogView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Settings/CommunityLogView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.Name = "ActionLogUserControl";
		object obj = P_1;
		context.AvaloniaNameScope.Register("ActionLogUserControl", obj);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		P_1.Content = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		Grid grid5 = grid4;
		grid5.Margin = new Thickness(0.0, 4.0, 0.0, 4.0);
		global::Avalonia.Controls.Controls children = grid5.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		button4.Classes.Add("BorderButton");
		button4.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		button4.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		button4.HorizontalContentAlignment = HorizontalAlignment.Stretch;
		StyledProperty<IBrush?> backgroundProperty = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, backgroundProperty, binding);
		StyledProperty<IBrush?> borderBrushProperty = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, borderBrushProperty, binding2);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Command("ShowActionLogDetailsCommand", CompiledAvaloniaXaml.XamlIlTrampolines.RootApp.Client.Avalonia:RootApp.Client.Avalonia.UI.Community.Settings.CommunityLogViewModel+ShowActionLogDetailsCommand_0!CommandExecuteTrampoline, null, null).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, commandProperty, compiledBindingExtension2);
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		button4.Content = grid6;
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		grid9.Margin = new Thickness(16.0, 16.0, 16.0, 16.0);
		grid9.HorizontalAlignment = HorizontalAlignment.Stretch;
		ColumnDefinitions columnDefinitions = new ColumnDefinitions();
		columnDefinitions.Capacity = 4;
		columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(24.0, GridUnitType.Pixel)));
		grid9.ColumnDefinitions = columnDefinitions;
		global::Avalonia.Controls.Controls children2 = grid9.Children;
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		children2.Add(panel);
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		global::Avalonia.Controls.Controls children3 = panel4.Children;
		Ellipse ellipse2;
		Ellipse ellipse = (ellipse2 = new Ellipse());
		((ISupportInitialize)ellipse).BeginInit();
		children3.Add(ellipse);
		Ellipse ellipse4;
		Ellipse ellipse3 = (ellipse4 = ellipse2);
		context.PushParent(ellipse4);
		ellipse4.Width = 40.0;
		ellipse4.Height = 40.0;
		StyledProperty<IBrush?> fillProperty = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(ellipse4, fillProperty, binding3);
		ellipse4.Opacity = 0.1;
		context.PopParent();
		((ISupportInitialize)ellipse3).EndInit();
		global::Avalonia.Controls.Controls children4 = panel4.Children;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		children4.Add(rootSvgImage);
		rootSvgImage2.Name = "ActionLogIconSvgImage";
		obj = rootSvgImage2;
		context.AvaloniaNameScope.Register("ActionLogIconSvgImage", obj);
		rootSvgImage2.Height = 18.0;
		rootSvgImage2.Width = 18.0;
		((ISupportInitialize)rootSvgImage2).EndInit();
		context.PopParent();
		((ISupportInitialize)panel3).EndInit();
		global::Avalonia.Controls.Controls children5 = grid9.Children;
		RootImageLoader rootImageLoader2;
		RootImageLoader rootImageLoader = (rootImageLoader2 = new RootImageLoader());
		((ISupportInitialize)rootImageLoader).BeginInit();
		children5.Add(rootImageLoader);
		RootImageLoader rootImageLoader4;
		RootImageLoader rootImageLoader3 = (rootImageLoader4 = rootImageLoader2);
		context.PushParent(rootImageLoader4);
		Grid.SetColumn(rootImageLoader4, 1);
		rootImageLoader4.Width = 38.0;
		rootImageLoader4.Height = 38.0;
		rootImageLoader4.Margin = new Thickness(15.0, 0.0, 15.0, 0.0);
		rootImageLoader4.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<IBrush?> backgroundProperty2 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, backgroundProperty2, binding4);
		StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Settings.CommunityLogViewModel,RootApp.Client.Avalonia.ProfilePictureAsyncBitmapWrapper!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
		context.ProvideTargetProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, sourceProperty, compiledBindingExtension4);
		rootImageLoader4.LoadingPlaceholderSize = 18.0;
		rootImageLoader4.Stretch = Stretch.UniformToFill;
		context.PopParent();
		((ISupportInitialize)rootImageLoader3).EndInit();
		global::Avalonia.Controls.Controls children6 = grid9.Children;
		Grid grid11;
		Grid grid10 = (grid11 = new Grid());
		((ISupportInitialize)grid10).BeginInit();
		children6.Add(grid10);
		Grid grid12 = (grid4 = grid11);
		context.PushParent(grid4);
		Grid grid13 = grid4;
		Grid.SetColumn(grid13, 2);
		RowDefinitions rowDefinitions = new RowDefinitions();
		rowDefinitions.Capacity = 2;
		rowDefinitions.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
		rowDefinitions.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
		grid13.RowDefinitions = rowDefinitions;
		global::Avalonia.Controls.Controls children7 = grid13.Children;
		RootMarkdownTextBlock rootMarkdownTextBlock2;
		RootMarkdownTextBlock rootMarkdownTextBlock = (rootMarkdownTextBlock2 = new RootMarkdownTextBlock());
		((ISupportInitialize)rootMarkdownTextBlock).BeginInit();
		children7.Add(rootMarkdownTextBlock);
		RootMarkdownTextBlock rootMarkdownTextBlock4;
		RootMarkdownTextBlock rootMarkdownTextBlock3 = (rootMarkdownTextBlock4 = rootMarkdownTextBlock2);
		context.PushParent(rootMarkdownTextBlock4);
		StyledProperty<IMarkdownEngine?> engineProperty = RootMarkdownTextBlock.EngineProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Settings.CommunityLogViewModel,RootApp.Client.Avalonia.MarkdownEngine!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootMarkdownTextBlock.EngineProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMarkdownTextBlock4, engineProperty, compiledBindingExtension6);
		DirectProperty<RootMarkdownTextBlock, string?> markdownProperty = RootMarkdownTextBlock.MarkdownProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Settings.CommunityLogViewModel,RootApp.Client.Avalonia.FormattedCommunityLog!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.Helpers.CommunityLogs.FormattedCommunityLog,RootApp.Client.Avalonia.Title!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootMarkdownTextBlock.MarkdownProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMarkdownTextBlock4, markdownProperty, compiledBindingExtension8);
		rootMarkdownTextBlock4.VerticalAlignment = VerticalAlignment.Top;
		context.PopParent();
		((ISupportInitialize)rootMarkdownTextBlock3).EndInit();
		global::Avalonia.Controls.Controls children8 = grid13.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children8.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		Grid.SetRow(textBlock4, 1);
		textBlock4.FontSize = 12.0;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Settings.CommunityLogViewModel,RootApp.Client.Avalonia.ActionDateString!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock4, textProperty, compiledBindingExtension10);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock4, obj2);
		textBlock4.FontWeight = (FontWeight)450;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock4, foregroundProperty, binding5);
		textBlock4.VerticalAlignment = VerticalAlignment.Bottom;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid12).EndInit();
		global::Avalonia.Controls.Controls children9 = grid9.Children;
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage3).BeginInit();
		children9.Add(rootSvgImage3);
		RootSvgImage rootSvgImage6;
		RootSvgImage rootSvgImage5 = (rootSvgImage6 = rootSvgImage4);
		context.PushParent(rootSvgImage6);
		Grid.SetColumn(rootSvgImage6, 3);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Settings.CommunityLogViewModel,RootApp.Client.Avalonia.HasDetails!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage6, isVisibleProperty, compiledBindingExtension12);
		StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("DownArrowSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage6, svgPathProperty, binding6);
		rootSvgImage6.Width = 12.31;
		rootSvgImage6.Height = 8.49;
		rootSvgImage6.Opacity = 0.5;
		rootSvgImage6.VerticalAlignment = VerticalAlignment.Center;
		rootSvgImage6.RenderTransform = new RotateTransform
		{
			Angle = 270.0
		};
		context.PopParent();
		((ISupportInitialize)rootSvgImage5).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
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
	private static void !XamlIlPopulateTrampoline(CommunityLogView P_0)
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
