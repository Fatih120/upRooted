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
using Avalonia.Collections;
using Avalonia.Controls;
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
using Avalonia.Media.Imaging;
using Avalonia.Media.Immutable;
using Avalonia.Styling;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Resources.Converters.Channels;
using RootApp.Client.Avalonia.Resources.Strings;

namespace RootApp.Client.Avalonia.UI.Community.Channels;

public class ChannelView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_66
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ChannelView> context = CreateContext(P_0);
			return new HasCustomChannelIconToMarginConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<ChannelView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ChannelView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ChannelView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Community/Channels/ChannelView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Channels/ChannelView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (ChannelView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ChannelView> context = CreateContext(P_0);
			return new ChannelDescriptionToVisibilityConverter();
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ChannelView> context = CreateContext(P_0);
			return new ChannelTypeToSvgConverter();
		}

		public static object Build_4(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ChannelView> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((AvaloniaObject)obj).SetValue(Visual.ClipToBoundsProperty, value: false, BindingPriority.Template);
			((ISupportInitialize)obj).EndInit();
			return obj;
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border ActiveCallSideIndicator;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border BackgroundBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgImage LargeChannelTypeImage;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootSvgImage SmallChannelTypeImage;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock ChannelNameText;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	private ChannelViewModel _channelViewModel => (ChannelViewModel)base.DataContext;

	public ChannelView()
	{
		InitializeComponent();
	}

	private void onPointerEntered(object? sender, PointerEventArgs e)
	{
		if (_channelViewModel.PointerEnteredCommand.CanExecute(e))
		{
			_channelViewModel.PointerEnteredCommand.Execute(e);
		}
	}

	private void onPointerExited(object? sender, PointerEventArgs e)
	{
		if (_channelViewModel.PointerExitedCommand.CanExecute(e))
		{
			_channelViewModel.PointerExitedCommand.Execute(e);
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
		ActiveCallSideIndicator = nameScope?.Find<Border>("ActiveCallSideIndicator");
		BackgroundBorder = nameScope?.Find<Border>("BackgroundBorder");
		LargeChannelTypeImage = nameScope?.Find<RootSvgImage>("LargeChannelTypeImage");
		SmallChannelTypeImage = nameScope?.Find<RootSvgImage>("SmallChannelTypeImage");
		ChannelNameText = nameScope?.Find<TextBlock>("ChannelNameText");
	}

	[CompilerGenerated]
	private unsafe static void !XamlIlPopulate(IServiceProvider P_0, ChannelView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<ChannelView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ChannelView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Community/Channels/ChannelView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Channels/ChannelView.axaml")
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
		P_1.Background = new ImmutableSolidColorBrush(16777215u);
		P_1.Margin = new Thickness(0.0, 3.0, 0.0, 0.0);
		P_1.ClipToBounds = false;
		RenderOptions.SetBitmapInterpolationMode(P_1, BitmapInterpolationMode.MediumQuality);
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"HasCustomChannelIconToMarginConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_66.Build_1), context));
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"ChannelDescriptionToVisibilityConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_66.Build_2), context));
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"ChannelTypeToSvgConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_66.Build_3), context));
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		P_1.Content = panel;
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		Panel panel5 = panel4;
		panel5.ClipToBounds = false;
		global::Avalonia.Controls.Controls children = panel5.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Border border5 = border4;
		border5.CornerRadius = new CornerRadius(2.0, 2.0, 2.0, 2.0);
		border5.Width = 3.0;
		border5.Height = 30.0;
		border5.HorizontalAlignment = HorizontalAlignment.Left;
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, backgroundProperty, binding);
		border5.Margin = new Thickness(9.0, 0.0, 0.0, 0.0);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.HasActivity!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, isVisibleProperty, compiledBindingExtension2);
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
		Grid grid5 = grid4;
		grid5.Margin = new Thickness(16.0, 0.0, 16.0, 0.0);
		grid5.ClipToBounds = false;
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		global::Avalonia.Controls.Controls children3 = grid5.Children;
		Border border7;
		Border border6 = (border7 = new Border());
		((ISupportInitialize)border6).BeginInit();
		children3.Add(border6);
		Border border8 = (border4 = border7);
		context.PushParent(border4);
		Border border9 = border4;
		border9.Name = "ActiveCallSideIndicator";
		object obj = border9;
		context.AvaloniaNameScope.Register("ActiveCallSideIndicator", obj);
		border9.CornerRadius = new CornerRadius(0.0, 1.0, 1.0, 0.0);
		border9.Width = 0.5;
		border9.HorizontalAlignment = HorizontalAlignment.Left;
		border9.VerticalAlignment = VerticalAlignment.Stretch;
		border9.Margin = new Thickness(-16.0, 4.0, 0.0, -4.0);
		Grid.SetRowSpan(border9, 2);
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.MediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.HasActiveCall!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "False"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension3 = obj2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border9, isVisibleProperty2, compiledBindingExtension3);
		CompiledBindingExtension obj3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.MediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.HasActiveCall!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "False"
		};
		context.ProvideTargetProperty = "class:HasActiveCall";
		CompiledBindingExtension compiledBindingExtension4 = obj3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		IBinding binding2 = compiledBindingExtension4;
		border9.BindClass("HasActiveCall", binding2, null);
		border9.ClipToBounds = false;
		Styles styles = border9.Styles;
		Style style2;
		Style style = (style2 = new Style());
		context.PushParent(style2);
		Style style3 = style2;
		style3.Selector = ((Selector?)null).OfType(typeof(Border)).Name("ActiveCallSideIndicator");
		Setter setter2;
		Setter setter = (setter2 = new Setter());
		context.PushParent(setter2);
		Setter setter3 = setter2;
		setter3.Property = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("BrandSecondary");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Styling.Setter,Avalonia.Base.Value!Property();
		IBinding value = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter3.Value = value;
		context.PopParent();
		style3.Add(setter);
		context.PopParent();
		styles.Add(style);
		Styles styles2 = border9.Styles;
		Style style4 = new Style();
		style4.Selector = ((Selector?)null).OfType(typeof(Border)).Name("ActiveCallSideIndicator").Class("HasActiveCall");
		IList<IAnimation> animations = style4.Animations;
		Animation animation = new Animation();
		animation.IterationCount = IterationCount.Parse("Infinite");
		animation.Duration = TimeSpan.FromTicks(20000000L);
		animation.PlaybackDirection = PlaybackDirection.Alternate;
		KeyFrames children4 = animation.Children;
		KeyFrame keyFrame = new KeyFrame();
		keyFrame.Cue = Cue.Parse("0%", CultureInfo.InvariantCulture);
		AvaloniaList<IAnimationSetter> setters = keyFrame.Setters;
		Setter setter4 = new Setter();
		setter4.Property = Border.BoxShadowProperty;
		setter4.Value = BoxShadows.Parse("0 0 30 3 #35A8FF5D");
		setters.Add(setter4);
		AvaloniaList<IAnimationSetter> setters2 = keyFrame.Setters;
		Setter setter5 = new Setter();
		setter5.Property = Visual.OpacityProperty;
		setter5.Value = 0.85;
		setters2.Add(setter5);
		children4.Add(keyFrame);
		KeyFrames children5 = animation.Children;
		KeyFrame keyFrame2 = new KeyFrame();
		keyFrame2.Cue = Cue.Parse("100%", CultureInfo.InvariantCulture);
		AvaloniaList<IAnimationSetter> setters3 = keyFrame2.Setters;
		Setter setter6 = new Setter();
		setter6.Property = Border.BoxShadowProperty;
		setter6.Value = BoxShadows.Parse("0 0 40 5 #55A8FF5D");
		setters3.Add(setter6);
		AvaloniaList<IAnimationSetter> setters4 = keyFrame2.Setters;
		Setter setter7 = new Setter();
		setter7.Property = Visual.OpacityProperty;
		setter7.Value = 1.0;
		setters4.Add(setter7);
		children5.Add(keyFrame2);
		animations.Add(animation);
		styles2.Add(style4);
		context.PopParent();
		((ISupportInitialize)border8).EndInit();
		global::Avalonia.Controls.Controls children6 = grid5.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children6.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		button4.Classes.Add("TransparentButtonWithClickEffect");
		button4.Height = 43.0;
		Grid.SetRow(button4, 0);
		button4.Background = new ImmutableSolidColorBrush(16777215u);
		button4.Cursor = new Cursor(StandardCursorType.Hand);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.SelectChannelCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, commandProperty, compiledBindingExtension6);
		button4.AddHandler(InputElement.PointerEnteredEvent, context.RootObject.onPointerEntered);
		button4.AddHandler(InputElement.PointerExitedEvent, context.RootObject.onPointerExited);
		StyledProperty<bool> isEnabledProperty = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.CanSelectChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, isEnabledProperty, compiledBindingExtension8);
		RootMenuFlyout rootMenuFlyout;
		RootMenuFlyout contextFlyout = (rootMenuFlyout = new RootMenuFlyout());
		context.PushParent(rootMenuFlyout);
		ItemCollection items = rootMenuFlyout.Items;
		MenuItem menuItem2;
		MenuItem menuItem = (menuItem2 = new MenuItem());
		((ISupportInitialize)menuItem).BeginInit();
		items.Add(menuItem);
		MenuItem menuItem4;
		MenuItem menuItem3 = (menuItem4 = menuItem2);
		context.PushParent(menuItem4);
		MenuItem menuItem5 = menuItem4;
		menuItem5.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.MarkAsRead;
		StyledProperty<ICommand?> commandProperty2 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.MarkAsReadCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem5, commandProperty2, compiledBindingExtension10);
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.HasActivity!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem5, isVisibleProperty3, compiledBindingExtension12);
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
		menuItem9.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.EditChannel;
		StyledProperty<ICommand?> commandProperty3 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.ShowEditChannelViewModelCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem9, commandProperty3, compiledBindingExtension14);
		StyledProperty<bool> isEnabledProperty2 = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension15 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.LocalChannelPermission!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.LocalChannelPermission,RootApp.Client.CoreDomain.ChannelFullControl!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension16 = compiledBindingExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem9, isEnabledProperty2, compiledBindingExtension16);
		context.PopParent();
		((ISupportInitialize)menuItem8).EndInit();
		ItemCollection items3 = rootMenuFlyout.Items;
		Separator separator2;
		Separator separator = (separator2 = new Separator());
		((ISupportInitialize)separator).BeginInit();
		items3.Add(separator);
		((ISupportInitialize)separator2).EndInit();
		ItemCollection items4 = rootMenuFlyout.Items;
		MenuItem menuItem11;
		MenuItem menuItem10 = (menuItem11 = new MenuItem());
		((ISupportInitialize)menuItem10).BeginInit();
		items4.Add(menuItem10);
		MenuItem menuItem12 = (menuItem4 = menuItem11);
		context.PushParent(menuItem4);
		MenuItem menuItem13 = menuItem4;
		StyledProperty<ICommand?> commandProperty4 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension17 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.ShowAppSettingsViewModelCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension18 = compiledBindingExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem13, commandProperty4, compiledBindingExtension18);
		StyledProperty<bool> isEnabledProperty3 = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension19 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.CanShowAppSettings!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension20 = compiledBindingExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem13, isEnabledProperty3, compiledBindingExtension20);
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension21 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.IsAppChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension22 = compiledBindingExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem13, isVisibleProperty4, compiledBindingExtension22);
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		menuItem13.Header = stackPanel;
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		stackPanel5.Orientation = Orientation.Horizontal;
		stackPanel5.Spacing = 6.0;
		global::Avalonia.Controls.Controls children7 = stackPanel5.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children7.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Text = "App settings";
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj4 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock5, obj4);
		textBlock5.FontSize = 14.0;
		textBlock5.FontWeight = (FontWeight)450;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding3);
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		global::Avalonia.Controls.Controls children8 = stackPanel5.Children;
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
		ellipse5.Fill = new ImmutableSolidColorBrush(4283096704u);
		ellipse5.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension23 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.AppHasUpdateAvailable!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension24 = compiledBindingExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(ellipse5, isVisibleProperty5, compiledBindingExtension24);
		context.PopParent();
		((ISupportInitialize)ellipse3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)menuItem12).EndInit();
		ItemCollection items5 = rootMenuFlyout.Items;
		MenuItem menuItem15;
		MenuItem menuItem14 = (menuItem15 = new MenuItem());
		((ISupportInitialize)menuItem14).BeginInit();
		items5.Add(menuItem14);
		MenuItem menuItem16 = (menuItem4 = menuItem15);
		context.PushParent(menuItem4);
		MenuItem menuItem17 = menuItem4;
		menuItem17.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.Delete;
		menuItem17.Classes.Add("DeleteMenuItem");
		StyledProperty<ICommand?> commandProperty5 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension25 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.ShowDeleteChannelViewModelCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension26 = compiledBindingExtension25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem17, commandProperty5, compiledBindingExtension26);
		StyledProperty<bool> isEnabledProperty4 = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension27 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.CanDeleteChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension28 = compiledBindingExtension27.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem17, isEnabledProperty4, compiledBindingExtension28);
		StyledProperty<bool> isVisibleProperty6 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension29 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.IsAppChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension30 = compiledBindingExtension29.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem17, isVisibleProperty6, compiledBindingExtension30);
		context.PopParent();
		((ISupportInitialize)menuItem16).EndInit();
		ItemCollection items6 = rootMenuFlyout.Items;
		Separator separator4;
		Separator separator3 = (separator4 = new Separator());
		((ISupportInitialize)separator3).BeginInit();
		items6.Add(separator3);
		Separator separator6;
		Separator separator5 = (separator6 = separator4);
		context.PushParent(separator6);
		StyledProperty<bool> isVisibleProperty7 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension31 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.DeveloperModeEnabled!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension32 = compiledBindingExtension31.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(separator6, isVisibleProperty7, compiledBindingExtension32);
		context.PopParent();
		((ISupportInitialize)separator5).EndInit();
		ItemCollection items7 = rootMenuFlyout.Items;
		MenuItem menuItem19;
		MenuItem menuItem18 = (menuItem19 = new MenuItem());
		((ISupportInitialize)menuItem18).BeginInit();
		items7.Add(menuItem18);
		MenuItem menuItem20 = (menuItem4 = menuItem19);
		context.PushParent(menuItem4);
		MenuItem menuItem21 = menuItem4;
		menuItem21.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.CopyChannelId;
		StyledProperty<ICommand?> commandProperty6 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension33 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.CopyChannelIdCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension34 = compiledBindingExtension33.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem21, commandProperty6, compiledBindingExtension34);
		StyledProperty<bool> isVisibleProperty8 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension35 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.DeveloperModeEnabled!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension36 = compiledBindingExtension35.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem21, isVisibleProperty8, compiledBindingExtension36);
		context.PopParent();
		((ISupportInitialize)menuItem20).EndInit();
		context.PopParent();
		button4.ContextFlyout = contextFlyout;
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		button4.Content = grid6;
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		grid9.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(12.0, GridUnitType.Pixel)
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
			Width = new GridLength(12.0, GridUnitType.Pixel)
		});
		global::Avalonia.Controls.Controls children9 = grid9.Children;
		Border border11;
		Border border10 = (border11 = new Border());
		((ISupportInitialize)border10).BeginInit();
		children9.Add(border10);
		Border border12 = (border4 = border11);
		context.PushParent(border4);
		Border border13 = border4;
		border13.Name = "BackgroundBorder";
		obj = border13;
		context.AvaloniaNameScope.Register("BackgroundBorder", obj);
		border13.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border13, backgroundProperty2, binding4);
		Grid.SetColumnSpan(border13, 5);
		StyledProperty<bool> isVisibleProperty9 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension37 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.IsSelectedOrHighlighted!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension38 = compiledBindingExtension37.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border13, isVisibleProperty9, compiledBindingExtension38);
		context.PopParent();
		((ISupportInitialize)border12).EndInit();
		global::Avalonia.Controls.Controls children10 = grid9.Children;
		Panel panel7;
		Panel panel6 = (panel7 = new Panel());
		((ISupportInitialize)panel6).BeginInit();
		children10.Add(panel6);
		Panel panel8 = (panel4 = panel7);
		context.PushParent(panel4);
		Panel panel9 = panel4;
		Grid.SetColumn(panel9, 1);
		StyledProperty<Thickness> marginProperty = Layoutable.MarginProperty;
		CompiledBindingExtension compiledBindingExtension40;
		CompiledBindingExtension compiledBindingExtension39 = (compiledBindingExtension40 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.HasCustomChannelIcon!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension40);
		CompiledBindingExtension compiledBindingExtension41 = compiledBindingExtension40;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("HasCustomChannelIconToMarginConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj5 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension41.Converter = (IValueConverter)obj5;
		context.PopParent();
		context.ProvideTargetProperty = Layoutable.MarginProperty;
		CompiledBindingExtension compiledBindingExtension42 = compiledBindingExtension39.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(panel9, marginProperty, compiledBindingExtension42);
		global::Avalonia.Controls.Controls children11 = panel9.Children;
		Border border15;
		Border border14 = (border15 = new Border());
		((ISupportInitialize)border14).BeginInit();
		children11.Add(border14);
		Border border16 = (border4 = border15);
		context.PushParent(border4);
		Border border17 = border4;
		border17.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		border17.Width = 28.0;
		border17.Height = 28.0;
		border17.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<IBrush?> backgroundProperty3 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border17, backgroundProperty3, binding5);
		StyledProperty<bool> isVisibleProperty10 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension43 = (compiledBindingExtension40 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.HasCustomChannelIcon!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension40);
		CompiledBindingExtension compiledBindingExtension44 = compiledBindingExtension40;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("BoolInverterConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj6 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension44.Converter = (IValueConverter)obj6;
		context.PopParent();
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension45 = compiledBindingExtension43.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border17, isVisibleProperty10, compiledBindingExtension45);
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		border17.Child = rootSvgImage;
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage5 = rootSvgImage4;
		rootSvgImage5.Name = "LargeChannelTypeImage";
		obj = rootSvgImage5;
		context.AvaloniaNameScope.Register("LargeChannelTypeImage", obj);
		rootSvgImage5.Opacity = 0.6;
		rootSvgImage5.Width = 20.0;
		rootSvgImage5.Height = 20.0;
		StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
		MultiBinding multiBinding2;
		MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding3 = multiBinding2;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("ChannelTypeToSvgConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj7 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding3.Converter = (IMultiValueConverter)obj7;
		IList<IBinding> bindings = multiBinding3.Bindings;
		CompiledBindingExtension obj8 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.Type!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item = obj8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings.Add(item);
		IList<IBinding> bindings2 = multiBinding3.Bindings;
		CompiledBindingExtension compiledBindingExtension46 = new CompiledBindingExtension();
		compiledBindingExtension46.Path = new CompiledBindingPathBuilder(1).Property(Application.ActualThemeVariantProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
		compiledBindingExtension46.Source = Application.Current;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item2 = compiledBindingExtension46.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings2.Add(item2);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgImage5, svgPathProperty, multiBinding);
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		context.PopParent();
		((ISupportInitialize)border16).EndInit();
		global::Avalonia.Controls.Controls children12 = panel9.Children;
		Border border19;
		Border border18 = (border19 = new Border());
		((ISupportInitialize)border18).BeginInit();
		children12.Add(border18);
		Border border20 = (border4 = border19);
		context.PushParent(border4);
		Border border21 = border4;
		border21.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		border21.Width = 28.0;
		border21.Height = 28.0;
		border21.HorizontalAlignment = HorizontalAlignment.Left;
		StyledProperty<IBrush?> backgroundProperty4 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border21, backgroundProperty4, binding6);
		StyledProperty<bool> isVisibleProperty11 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension47 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.HasCustomChannelIcon!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension48 = compiledBindingExtension47.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border21, isVisibleProperty11, compiledBindingExtension48);
		GeometryGroup geometryGroup = new GeometryGroup();
		geometryGroup.Children.Add(new RectangleGeometry
		{
			Rect = Rect.Parse("0,0,28,28")
		});
		GeometryCollection children13 = geometryGroup.Children;
		EllipseGeometry ellipseGeometry = new EllipseGeometry();
		ellipseGeometry.Center = new Point(26.0, 26.0);
		ellipseGeometry.RadiusX = 12.0;
		ellipseGeometry.RadiusY = 12.0;
		children13.Add(ellipseGeometry);
		border21.Clip = geometryGroup;
		RootImageLoader rootImageLoader2;
		RootImageLoader rootImageLoader = (rootImageLoader2 = new RootImageLoader());
		((ISupportInitialize)rootImageLoader).BeginInit();
		border21.Child = rootImageLoader;
		RootImageLoader rootImageLoader4;
		RootImageLoader rootImageLoader3 = (rootImageLoader4 = rootImageLoader2);
		context.PushParent(rootImageLoader4);
		StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension49 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.ChannelPictureAsyncBitmapWrapper!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
		context.ProvideTargetProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension50 = compiledBindingExtension49.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, sourceProperty, compiledBindingExtension50);
		rootImageLoader4.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		rootImageLoader4.Width = 28.0;
		rootImageLoader4.Height = 28.0;
		rootImageLoader4.LoadingPlaceholderSize = 0.0;
		rootImageLoader4.Stretch = Stretch.UniformToFill;
		context.PopParent();
		((ISupportInitialize)rootImageLoader3).EndInit();
		context.PopParent();
		((ISupportInitialize)border20).EndInit();
		global::Avalonia.Controls.Controls children14 = panel9.Children;
		Panel panel11;
		Panel panel10 = (panel11 = new Panel());
		((ISupportInitialize)panel10).BeginInit();
		children14.Add(panel10);
		Panel panel12 = (panel4 = panel11);
		context.PushParent(panel4);
		Panel panel13 = panel4;
		panel13.Margin = new Thickness(0.0, 0.0, -7.0, 4.0);
		panel13.HorizontalAlignment = HorizontalAlignment.Right;
		panel13.VerticalAlignment = VerticalAlignment.Bottom;
		StyledProperty<bool> isVisibleProperty12 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension51 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.HasCustomChannelIcon!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension52 = compiledBindingExtension51.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(panel13, isVisibleProperty12, compiledBindingExtension52);
		global::Avalonia.Controls.Controls children15 = panel13.Children;
		Ellipse ellipse7;
		Ellipse ellipse6 = (ellipse7 = new Ellipse());
		((ISupportInitialize)ellipse6).BeginInit();
		children15.Add(ellipse6);
		Ellipse ellipse8 = (ellipse4 = ellipse7);
		context.PushParent(ellipse4);
		Ellipse ellipse9 = ellipse4;
		ellipse9.Width = 18.0;
		ellipse9.Height = 18.0;
		StyledProperty<IBrush?> fillProperty = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(ellipse9, fillProperty, binding7);
		context.PopParent();
		((ISupportInitialize)ellipse8).EndInit();
		global::Avalonia.Controls.Controls children16 = panel13.Children;
		RootSvgImage rootSvgImage7;
		RootSvgImage rootSvgImage6 = (rootSvgImage7 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage6).BeginInit();
		children16.Add(rootSvgImage6);
		RootSvgImage rootSvgImage8 = (rootSvgImage4 = rootSvgImage7);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage9 = rootSvgImage4;
		rootSvgImage9.Name = "SmallChannelTypeImage";
		obj = rootSvgImage9;
		context.AvaloniaNameScope.Register("SmallChannelTypeImage", obj);
		rootSvgImage9.Opacity = 0.6;
		rootSvgImage9.Width = 15.0;
		rootSvgImage9.Height = 15.0;
		StyledProperty<string?> svgPathProperty2 = RootSvgImage.SvgPathProperty;
		MultiBinding multiBinding4 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding5 = multiBinding2;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("ChannelTypeToSvgConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj9 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding5.Converter = (IMultiValueConverter)obj9;
		IList<IBinding> bindings3 = multiBinding5.Bindings;
		CompiledBindingExtension obj10 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.Type!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item3 = obj10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings3.Add(item3);
		IList<IBinding> bindings4 = multiBinding5.Bindings;
		CompiledBindingExtension compiledBindingExtension53 = new CompiledBindingExtension();
		compiledBindingExtension53.Path = new CompiledBindingPathBuilder(1).Property(Application.ActualThemeVariantProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
		compiledBindingExtension53.Source = Application.Current;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item4 = compiledBindingExtension53.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings4.Add(item4);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootSvgImage9, svgPathProperty2, multiBinding4);
		context.PopParent();
		((ISupportInitialize)rootSvgImage8).EndInit();
		context.PopParent();
		((ISupportInitialize)panel12).EndInit();
		context.PopParent();
		((ISupportInitialize)panel8).EndInit();
		global::Avalonia.Controls.Controls children17 = grid9.Children;
		Panel panel15;
		Panel panel14 = (panel15 = new Panel());
		((ISupportInitialize)panel14).BeginInit();
		children17.Add(panel14);
		Panel panel16 = (panel4 = panel15);
		context.PushParent(panel4);
		Panel panel17 = panel4;
		Grid.SetColumn(panel17, 2);
		panel17.Margin = new Thickness(10.0, 0.0, 0.0, 0.0);
		panel17.Height = 43.0;
		global::Avalonia.Controls.Controls children18 = panel17.Children;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		children18.Add(stackPanel6);
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		stackPanel9.VerticalAlignment = VerticalAlignment.Center;
		global::Avalonia.Controls.Controls children19 = stackPanel9.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children19.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Name = "ChannelNameText";
		obj = textBlock9;
		context.AvaloniaNameScope.Register("ChannelNameText", obj);
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension54 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.Name!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension55 = compiledBindingExtension54.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, textProperty, compiledBindingExtension55);
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj11 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock9, obj11);
		textBlock9.FontSize = 14.0;
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock9.TextTrimming = TextTrimming.CharacterEllipsis;
		CompiledBindingExtension compiledBindingExtension56 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.HasActivity!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = "class:IsTrue";
		CompiledBindingExtension compiledBindingExtension57 = compiledBindingExtension56.ProvideValue(context);
		context.ProvideTargetProperty = null;
		binding2 = compiledBindingExtension57;
		textBlock9.BindClass("IsTrue", binding2, null);
		CompiledBindingExtension compiledBindingExtension58 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.HasActivity!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = "class:IsFalse";
		CompiledBindingExtension compiledBindingExtension59 = compiledBindingExtension58.ProvideValue(context);
		context.ProvideTargetProperty = null;
		binding2 = compiledBindingExtension59;
		textBlock9.BindClass("IsFalse", binding2, null);
		CompiledBindingExtension obj12 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.MediaRoom!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaRoom,RootApp.Client.CoreDomain.HasActiveCall!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "False"
		};
		context.ProvideTargetProperty = "class:HasActiveCall";
		CompiledBindingExtension compiledBindingExtension60 = obj12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		binding2 = compiledBindingExtension60;
		textBlock9.BindClass("HasActiveCall", binding2, null);
		Styles styles3 = textBlock9.Styles;
		Style style5 = (style2 = new Style());
		context.PushParent(style2);
		Style style6 = style2;
		style6.Selector = ((Selector?)null).OfType(typeof(TextBlock)).Class("IsTrue");
		IList<SetterBase> setters5 = style6.Setters;
		Setter item5 = (setter2 = new Setter());
		context.PushParent(setter2);
		Setter setter8 = setter2;
		setter8.Property = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Styling.Setter,Avalonia.Base.Value!Property();
		IBinding value2 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter8.Value = value2;
		context.PopParent();
		setters5.Add(item5);
		IList<SetterBase> setters6 = style6.Setters;
		Setter setter9 = new Setter();
		setter9.Property = TextBlock.FontWeightProperty;
		setter9.Value = FontWeight.DemiBold;
		setters6.Add(setter9);
		context.PopParent();
		styles3.Add(style5);
		Styles styles4 = textBlock9.Styles;
		Style style7 = (style2 = new Style());
		context.PushParent(style2);
		Style style8 = style2;
		style8.Selector = ((Selector?)null).OfType(typeof(TextBlock)).Class("IsFalse");
		IList<SetterBase> setters7 = style8.Setters;
		Setter item6 = (setter2 = new Setter());
		context.PushParent(setter2);
		Setter setter10 = setter2;
		setter10.Property = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Styling.Setter,Avalonia.Base.Value!Property();
		IBinding value3 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter10.Value = value3;
		context.PopParent();
		setters7.Add(item6);
		IList<SetterBase> setters8 = style8.Setters;
		Setter setter11 = new Setter();
		setter11.Property = TextBlock.FontWeightProperty;
		setter11.Value = (FontWeight)450;
		setters8.Add(setter11);
		context.PopParent();
		styles4.Add(style7);
		Styles styles5 = textBlock9.Styles;
		Style style9 = (style2 = new Style());
		context.PushParent(style2);
		Style style10 = style2;
		style10.Selector = ((Selector?)null).OfType(typeof(TextBlock)).Class("HasActiveCall");
		IList<SetterBase> setters9 = style10.Setters;
		Setter item7 = (setter2 = new Setter());
		context.PushParent(setter2);
		Setter setter12 = setter2;
		setter12.Property = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("BrandSecondary");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Styling.Setter,Avalonia.Base.Value!Property();
		IBinding value4 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		setter12.Value = value4;
		context.PopParent();
		setters9.Add(item7);
		IList<SetterBase> setters10 = style10.Setters;
		Setter setter13 = new Setter();
		setter13.Property = TextBlock.FontWeightProperty;
		setter13.Value = FontWeight.DemiBold;
		setters10.Add(setter13);
		context.PopParent();
		styles5.Add(style9);
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		global::Avalonia.Controls.Controls children20 = stackPanel9.Children;
		Grid grid11;
		Grid grid10 = (grid11 = new Grid());
		((ISupportInitialize)grid10).BeginInit();
		children20.Add(grid10);
		Grid grid12 = (grid4 = grid11);
		context.PushParent(grid4);
		Grid grid13 = grid4;
		ColumnDefinitions columnDefinitions = new ColumnDefinitions();
		columnDefinitions.Capacity = 2;
		columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		grid13.ColumnDefinitions = columnDefinitions;
		StyledProperty<bool> isVisibleProperty13 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension61 = (compiledBindingExtension40 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.DisplayDescription!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension40);
		CompiledBindingExtension compiledBindingExtension62 = compiledBindingExtension40;
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("ChannelDescriptionToVisibilityConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj13 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension62.Converter = (IValueConverter)obj13;
		context.PopParent();
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension63 = compiledBindingExtension61.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(grid13, isVisibleProperty13, compiledBindingExtension63);
		global::Avalonia.Controls.Controls children21 = grid13.Children;
		Ellipse ellipse11;
		Ellipse ellipse10 = (ellipse11 = new Ellipse());
		((ISupportInitialize)ellipse10).BeginInit();
		children21.Add(ellipse10);
		Ellipse ellipse12 = (ellipse4 = ellipse11);
		context.PushParent(ellipse4);
		Ellipse ellipse13 = ellipse4;
		Grid.SetColumn(ellipse13, 0);
		ellipse13.Width = 6.0;
		ellipse13.Height = 6.0;
		ellipse13.Fill = new ImmutableSolidColorBrush(4283096704u);
		ellipse13.VerticalAlignment = VerticalAlignment.Center;
		ellipse13.Margin = new Thickness(0.0, 0.0, 4.0, 0.0);
		StyledProperty<bool> isVisibleProperty14 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension64 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.AppHasUpdateAvailable!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension65 = compiledBindingExtension64.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(ellipse13, isVisibleProperty14, compiledBindingExtension65);
		context.PopParent();
		((ISupportInitialize)ellipse12).EndInit();
		global::Avalonia.Controls.Controls children22 = grid13.Children;
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		children22.Add(textBlock10);
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		Grid.SetColumn(textBlock13, 1);
		StyledProperty<string?> textProperty2 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension66 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.DisplayDescription!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension67 = compiledBindingExtension66.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, textProperty2, compiledBindingExtension67);
		StaticResourceExtension staticResourceExtension8 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj14 = staticResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock13, obj14);
		textBlock13.FontSize = 11.0;
		textBlock13.TextTrimming = TextTrimming.CharacterEllipsis;
		textBlock13.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding8 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, foregroundProperty2, binding8);
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		context.PopParent();
		((ISupportInitialize)grid12).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		context.PopParent();
		((ISupportInitialize)panel16).EndInit();
		global::Avalonia.Controls.Controls children23 = grid9.Children;
		Border border23;
		Border border22 = (border23 = new Border());
		((ISupportInitialize)border22).BeginInit();
		children23.Add(border22);
		Border border24 = (border4 = border23);
		context.PushParent(border4);
		Border border25 = border4;
		Grid.SetColumn(border25, 3);
		StyledProperty<IBrush?> backgroundProperty5 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("Error");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding9 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border25, backgroundProperty5, binding9);
		border25.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		border25.VerticalAlignment = VerticalAlignment.Center;
		border25.Height = 17.0;
		border25.MinWidth = 17.0;
		StyledProperty<bool> isVisibleProperty15 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension68 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.Notifications!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Notifications.NotificationContainer,RootApp.Client.CoreDomain.ContainerUnviewedNotificationCount!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension69 = compiledBindingExtension68.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border25, isVisibleProperty15, compiledBindingExtension69);
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		border25.Child = textBlock14;
		TextBlock textBlock16 = (textBlock4 = textBlock15);
		context.PushParent(textBlock4);
		TextBlock textBlock17 = textBlock4;
		StyledProperty<string?> textProperty3 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension70 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.Channel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Channel,RootApp.Client.CoreDomain.Notifications!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Notifications.NotificationContainer,RootApp.Client.CoreDomain.ContainerUnviewedNotificationCount!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension71 = compiledBindingExtension70.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock17, textProperty3, compiledBindingExtension71);
		textBlock17.FontSize = 12.0;
		textBlock17.Margin = new Thickness(3.0, 0.0, 3.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding10 = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock17, foregroundProperty3, binding10);
		StaticResourceExtension staticResourceExtension9 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj15 = staticResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock17, obj15);
		textBlock17.FontWeight = FontWeight.Bold;
		textBlock17.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock17.HorizontalAlignment = HorizontalAlignment.Center;
		textBlock17.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock16).EndInit();
		context.PopParent();
		((ISupportInitialize)border24).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		global::Avalonia.Controls.Controls children24 = grid5.Children;
		ItemsControl itemsControl2;
		ItemsControl itemsControl = (itemsControl2 = new ItemsControl());
		((ISupportInitialize)itemsControl).BeginInit();
		children24.Add(itemsControl);
		ItemsControl itemsControl4;
		ItemsControl itemsControl3 = (itemsControl4 = itemsControl2);
		context.PushParent(itemsControl4);
		StyledProperty<IEnumerable?> itemsSourceProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension72 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelViewModel,RootApp.Client.Avalonia.MediaMembers!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension73 = compiledBindingExtension72.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl4, itemsSourceProperty, compiledBindingExtension73);
		Grid.SetRow(itemsControl4, 1);
		itemsControl4.ClipToBounds = false;
		itemsControl4.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_66.Build_4), context)
		};
		context.PopParent();
		((ISupportInitialize)itemsControl3).EndInit();
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
	private static void !XamlIlPopulateTrampoline(ChannelView P_0)
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
