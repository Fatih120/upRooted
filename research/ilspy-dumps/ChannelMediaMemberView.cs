using System;
using System.CodeDom.Compiler;
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
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.Styling;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Controls.ContextMenus;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Resources.Converters.MediaRoomMember;
using RootApp.Client.Avalonia.Resources.Converters.Roles;

namespace RootApp.Client.Avalonia.UI.Community.Channels;

public class ChannelMediaMemberView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_64
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ChannelMediaMemberView> context = CreateContext(P_0);
			return new MediaRoomMemberSpeakingBorderOpacityConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<ChannelMediaMemberView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ChannelMediaMemberView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ChannelMediaMemberView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Community/Channels/ChannelMediaMemberView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Channels/ChannelMediaMemberView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (ChannelMediaMemberView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ChannelMediaMemberView> context = CreateContext(P_0);
			return new MediaRoomMemberSpeakingOpacityConverter();
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ChannelMediaMemberView> context = CreateContext(P_0);
			return new MediaRoomMemberConnectingOpacityConverter();
		}

		public static object Build_4(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ChannelMediaMemberView> context = CreateContext(P_0);
			return new CommunityRoleColorConverter();
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border SpeakingEffect;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal RootImageLoader ProfilePicture;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock NameTextBlock;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border LiveBadgeGlow;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Button LiveBadge;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	private ChannelMediaMemberViewModel? _channelMediaMemberViewModel => base.DataContext as ChannelMediaMemberViewModel;

	public ChannelMediaMemberView()
	{
		InitializeComponent();
	}

	private void onFlyoutOpening(object? sender, EventArgs e)
	{
		if (_channelMediaMemberViewModel != null && _channelMediaMemberViewModel.ProfileOpeningCommand.CanExecute(null))
		{
			_channelMediaMemberViewModel.ProfileOpeningCommand.Execute(null);
		}
	}

	private void onFlyoutClosing(object? sender, CancelEventArgs e)
	{
		if (_channelMediaMemberViewModel != null && _channelMediaMemberViewModel.ProfileClosingCommand.CanExecute(null))
		{
			_channelMediaMemberViewModel.ProfileClosingCommand.Execute(null);
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
		SpeakingEffect = nameScope?.Find<Border>("SpeakingEffect");
		ProfilePicture = nameScope?.Find<RootImageLoader>("ProfilePicture");
		NameTextBlock = nameScope?.Find<TextBlock>("NameTextBlock");
		LiveBadgeGlow = nameScope?.Find<Border>("LiveBadgeGlow");
		LiveBadge = nameScope?.Find<Button>("LiveBadge");
	}

	[CompilerGenerated]
	private unsafe static void !XamlIlPopulate(IServiceProvider P_0, ChannelMediaMemberView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<ChannelMediaMemberView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ChannelMediaMemberView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Community/Channels/ChannelMediaMemberView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Channels/ChannelMediaMemberView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.ClipToBounds = false;
		ResourceDictionary resourceDictionary = new ResourceDictionary();
		if (resourceDictionary is ResourceDictionary resourceDictionary2)
		{
			resourceDictionary2.EnsureCapacity(resourceDictionary2.Count + 4);
		}
		resourceDictionary.AddDeferred("MediaRoomMemberSpeakingBorderOpacityConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_64.Build_1), context));
		resourceDictionary.AddDeferred("MediaRoomMemberSpeakingOpacityConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_64.Build_2), context));
		resourceDictionary.AddDeferred("MediaRoomMemberConnectingOpacityConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_64.Build_3), context));
		resourceDictionary.AddDeferred("CommunityRoleColorConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_64.Build_4), context));
		P_1.Resources = resourceDictionary;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		P_1.Content = button;
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		Button button5 = button4;
		button5.Classes.Add("TransparentButtonWithClickEffect");
		button5.Margin = new Thickness(18.0, 5.0, 0.0, 2.0);
		button5.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button5.ClipToBounds = false;
		button5.HorizontalContentAlignment = HorizontalAlignment.Stretch;
		button5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		UserContextMenuView userContextMenuView;
		UserContextMenuView contextFlyout = (userContextMenuView = new UserContextMenuView());
		context.PushParent(userContextMenuView);
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelMediaMemberViewModel,RootApp.Client.Avalonia.UserContextMenu!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.System.Lazy`1,System.Runtime.Value!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = UserContextMenuView.DataContextProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_22(userContextMenuView, compiledBindingExtension2);
		context.PopParent();
		button5.ContextFlyout = contextFlyout;
		RootFlyout rootFlyout;
		RootFlyout flyout = (rootFlyout = new RootFlyout());
		context.PushParent(rootFlyout);
		rootFlyout.Placement = PlacementMode.RightEdgeAlignedTop;
		rootFlyout.VerticalOffset = -16.0;
		rootFlyout.HorizontalOffset = 8.0;
		rootFlyout.LimitSizeToWindow = false;
		StyledProperty<bool> isPopupOpenProperty = RootFlyout.IsPopupOpenProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelMediaMemberViewModel,RootApp.Client.Avalonia.IsPopupOpen!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootFlyout.IsPopupOpenProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootFlyout, isPopupOpenProperty, compiledBindingExtension4);
		rootFlyout.Opening += context.RootObject.onFlyoutOpening;
		rootFlyout.Closing += context.RootObject.onFlyoutClosing;
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		rootFlyout.Content = rootBorder;
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
		rootBorder4.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder4.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		rootBorder4.Margin = new Thickness(12.0, 12.0, 12.0, 12.0);
		StyledProperty<BoxShadows> boxShadowProperty = Border.BoxShadowProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("PopupBoxShadow");
		context.ProvideTargetProperty = Border.BoxShadowProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, boxShadowProperty, binding3);
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		rootBorder4.Child = contentControl;
		ContentControl contentControl4;
		ContentControl contentControl3 = (contentControl4 = contentControl2);
		context.PushParent(contentControl4);
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelMediaMemberViewModel,RootApp.Client.Avalonia.MemberProfile!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl4, compiledBindingExtension6);
		context.PopParent();
		((ISupportInitialize)contentControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		context.PopParent();
		button5.Flyout = flyout;
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		button5.Content = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		StyledProperty<double> opacityProperty = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension8;
		CompiledBindingExtension compiledBindingExtension7 = (compiledBindingExtension8 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelMediaMemberViewModel,RootApp.Client.Avalonia.Member!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsFullyConnected!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension8);
		CompiledBindingExtension compiledBindingExtension9 = compiledBindingExtension8;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("MediaRoomMemberConnectingOpacityConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension9.Converter = (IValueConverter)obj;
		context.PopParent();
		context.ProvideTargetProperty = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(grid4, opacityProperty, compiledBindingExtension10);
		grid4.ClipToBounds = false;
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(26.0, GridUnitType.Pixel)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(10.0, GridUnitType.Pixel)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(8.0, GridUnitType.Pixel)
		});
		global::Avalonia.Controls.Controls children = grid4.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Border border5 = border4;
		border5.Name = "SpeakingEffect";
		object obj2 = border5;
		context.AvaloniaNameScope.Register("SpeakingEffect", obj2);
		Grid.SetColumn(border5, 0);
		border5.Width = 26.0;
		border5.Height = 26.0;
		border5.CornerRadius = new CornerRadius(5.0, 5.0, 5.0, 5.0);
		StyledProperty<IBrush?> borderBrushProperty2 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("BrandSecondary");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border5, borderBrushProperty2, binding4);
		border5.BorderThickness = new Thickness(2.0, 2.0, 2.0, 2.0);
		CompiledBindingExtension compiledBindingExtension11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelMediaMemberViewModel,RootApp.Client.Avalonia.Member!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsSpeaking!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = "class:IsSpeaking";
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		IBinding binding5 = compiledBindingExtension12;
		border5.BindClass("IsSpeaking", binding5, null);
		Transitions transitions = new Transitions();
		DoubleTransition doubleTransition = new DoubleTransition();
		doubleTransition.Property = Visual.OpacityProperty;
		doubleTransition.Duration = TimeSpan.FromTicks(1500000L);
		doubleTransition.Easing = Easing.Parse("CubicEaseOut");
		transitions.Add(doubleTransition);
		BoxShadowsTransition boxShadowsTransition = new BoxShadowsTransition();
		boxShadowsTransition.Property = Border.BoxShadowProperty;
		boxShadowsTransition.Duration = TimeSpan.FromTicks(2000000L);
		transitions.Add(boxShadowsTransition);
		border5.Transitions = transitions;
		Styles styles = border5.Styles;
		Style style = new Style();
		style.Selector = ((Selector?)null).OfType(typeof(Border)).Name("SpeakingEffect");
		Setter setter = new Setter();
		setter.Property = Visual.OpacityProperty;
		setter.Value = 0.0;
		style.Add(setter);
		Setter setter2 = new Setter();
		setter2.Property = Border.BoxShadowProperty;
		setter2.Value = BoxShadows.Parse("0 0 0 0 Transparent");
		style.Add(setter2);
		styles.Add(style);
		Styles styles2 = border5.Styles;
		Style style2 = new Style();
		style2.Selector = ((Selector?)null).OfType(typeof(Border)).Name("SpeakingEffect").Class("IsSpeaking");
		IList<IAnimation> animations = style2.Animations;
		Animation animation = new Animation();
		animation.IterationCount = IterationCount.Parse("Infinite");
		animation.Duration = TimeSpan.FromTicks(15000000L);
		animation.PlaybackDirection = PlaybackDirection.Alternate;
		KeyFrames children2 = animation.Children;
		KeyFrame keyFrame = new KeyFrame();
		keyFrame.Cue = Cue.Parse("0%", CultureInfo.InvariantCulture);
		AvaloniaList<IAnimationSetter> setters = keyFrame.Setters;
		Setter setter3 = new Setter();
		setter3.Property = Border.BoxShadowProperty;
		setter3.Value = BoxShadows.Parse("0 0 4 1 #60A8FF5D");
		setters.Add(setter3);
		children2.Add(keyFrame);
		KeyFrames children3 = animation.Children;
		KeyFrame keyFrame2 = new KeyFrame();
		keyFrame2.Cue = Cue.Parse("100%", CultureInfo.InvariantCulture);
		AvaloniaList<IAnimationSetter> setters2 = keyFrame2.Setters;
		Setter setter4 = new Setter();
		setter4.Property = Border.BoxShadowProperty;
		setter4.Value = BoxShadows.Parse("0 0 8 2 #90A8FF5D");
		setters2.Add(setter4);
		children3.Add(keyFrame2);
		animations.Add(animation);
		Setter setter5 = new Setter();
		setter5.Property = Visual.OpacityProperty;
		setter5.Value = 1.0;
		style2.Add(setter5);
		styles2.Add(style2);
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		global::Avalonia.Controls.Controls children4 = grid4.Children;
		RootImageLoader rootImageLoader2;
		RootImageLoader rootImageLoader = (rootImageLoader2 = new RootImageLoader());
		((ISupportInitialize)rootImageLoader).BeginInit();
		children4.Add(rootImageLoader);
		RootImageLoader rootImageLoader4;
		RootImageLoader rootImageLoader3 = (rootImageLoader4 = rootImageLoader2);
		context.PushParent(rootImageLoader4);
		rootImageLoader4.Name = "ProfilePicture";
		obj2 = rootImageLoader4;
		context.AvaloniaNameScope.Register("ProfilePicture", obj2);
		Grid.SetColumn(rootImageLoader4, 0);
		rootImageLoader4.Width = 22.0;
		rootImageLoader4.Height = 22.0;
		StyledProperty<double> opacityProperty2 = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension13 = (compiledBindingExtension8 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelMediaMemberViewModel,RootApp.Client.Avalonia.Member!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsSpeaking!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension8);
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension8;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("MediaRoomMemberSpeakingOpacityConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj3 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension14.Converter = (IValueConverter)obj3;
		context.PopParent();
		context.ProvideTargetProperty = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension15 = compiledBindingExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, opacityProperty2, compiledBindingExtension15);
		rootImageLoader4.CornerRadius = new CornerRadius(4.0, 4.0, 4.0, 4.0);
		StyledProperty<IBrush?> backgroundProperty2 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding6 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, backgroundProperty2, binding6);
		StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension16 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelMediaMemberViewModel,RootApp.Client.Avalonia.ProfilePictureAsyncBitmapWrapper!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
		context.ProvideTargetProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension17 = compiledBindingExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, sourceProperty, compiledBindingExtension17);
		rootImageLoader4.LoadingPlaceholderSize = 12.0;
		rootImageLoader4.Stretch = Stretch.UniformToFill;
		Transitions transitions2 = new Transitions();
		DoubleTransition doubleTransition2 = new DoubleTransition();
		doubleTransition2.Property = Visual.OpacityProperty;
		doubleTransition2.Duration = TimeSpan.FromTicks(2000000L);
		doubleTransition2.Easing = Easing.Parse("CubicEaseOut");
		transitions2.Add(doubleTransition2);
		rootImageLoader4.Transitions = transitions2;
		context.PopParent();
		((ISupportInitialize)rootImageLoader3).EndInit();
		global::Avalonia.Controls.Controls children5 = grid4.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children5.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Name = "NameTextBlock";
		obj2 = textBlock5;
		context.AvaloniaNameScope.Register("NameTextBlock", obj2);
		Grid.SetColumn(textBlock5, 2);
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		textBlock5.FontSize = 14.0;
		StyledProperty<double> opacityProperty3 = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension18 = (compiledBindingExtension8 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelMediaMemberViewModel,RootApp.Client.Avalonia.Member!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsSpeaking!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension8);
		CompiledBindingExtension compiledBindingExtension19 = compiledBindingExtension8;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("MediaRoomMemberSpeakingOpacityConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj4 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension19.Converter = (IValueConverter)obj4;
		context.PopParent();
		context.ProvideTargetProperty = Visual.OpacityProperty;
		CompiledBindingExtension compiledBindingExtension20 = compiledBindingExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, opacityProperty3, compiledBindingExtension20);
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj5 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock5, obj5);
		textBlock5.FontWeight = FontWeight.Medium;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension21 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelMediaMemberViewModel,RootApp.Client.Avalonia.Member!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.GlobalUser!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.User.GlobalUser,RootApp.Client.CoreDomain.UserName!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension22 = compiledBindingExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, compiledBindingExtension22);
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock5.TextTrimming = TextTrimming.CharacterEllipsis;
		Transitions transitions3 = new Transitions();
		DoubleTransition doubleTransition3 = new DoubleTransition();
		doubleTransition3.Property = Visual.OpacityProperty;
		doubleTransition3.Duration = TimeSpan.FromTicks(2000000L);
		doubleTransition3.Easing = Easing.Parse("CubicEaseOut");
		transitions3.Add(doubleTransition3);
		textBlock5.Transitions = transitions3;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		MultiBinding multiBinding2;
		MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding3 = multiBinding2;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("CommunityRoleColorConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj6 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding3.Converter = (IMultiValueConverter)obj6;
		IList<IBinding> bindings = multiBinding3.Bindings;
		CompiledBindingExtension compiledBindingExtension23 = new CompiledBindingExtension();
		compiledBindingExtension23.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelMediaMemberViewModel,RootApp.Client.Avalonia.CommunityMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Member,RootApp.Client.CoreDomain.Roles!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Repositories.Community.IMemberRoleService,RootApp.Client.CoreDomain.PrimaryRole!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Role,RootApp.Client.CoreDomain.RoleColorHex!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension23.FallbackValue = null;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item = compiledBindingExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings.Add(item);
		IList<IBinding> bindings2 = multiBinding3.Bindings;
		CompiledBindingExtension compiledBindingExtension24 = new CompiledBindingExtension();
		compiledBindingExtension24.Path = new CompiledBindingPathBuilder(1).Property(Application.ActualThemeVariantProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
		compiledBindingExtension24.Source = Application.Current;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item2 = compiledBindingExtension24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings2.Add(item2);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, multiBinding);
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		global::Avalonia.Controls.Controls children6 = grid4.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children6.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		Grid.SetColumn(stackPanel4, 3);
		stackPanel4.VerticalAlignment = VerticalAlignment.Center;
		stackPanel4.Orientation = Orientation.Horizontal;
		stackPanel4.Spacing = 8.0;
		stackPanel4.Height = 20.0;
		global::Avalonia.Controls.Controls children7 = stackPanel4.Children;
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		children7.Add(panel);
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		Panel panel5 = panel4;
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		MultiBinding multiBinding4 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding5 = multiBinding2;
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj7 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding5.Converter = (IMultiValueConverter)obj7;
		IList<IBinding> bindings3 = multiBinding5.Bindings;
		CompiledBindingExtension obj8 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelMediaMemberViewModel,RootApp.Client.Avalonia.Member!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsMuted!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item3 = obj8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings3.Add(item3);
		IList<IBinding> bindings4 = multiBinding5.Bindings;
		CompiledBindingExtension obj9 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelMediaMemberViewModel,RootApp.Client.Avalonia.Member!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsAdminMuted!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item4 = obj9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings4.Add(item4);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(panel5, isVisibleProperty, multiBinding4);
		global::Avalonia.Controls.Controls children8 = panel5.Children;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		children8.Add(rootSvgImage);
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage5 = rootSvgImage4;
		rootSvgImage5.Width = 15.0;
		rootSvgImage5.Height = 15.0;
		StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("MicrophoneMutedSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding7 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage5, svgPathProperty, binding7);
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		context.PopParent();
		((ISupportInitialize)panel3).EndInit();
		global::Avalonia.Controls.Controls children9 = stackPanel4.Children;
		Panel panel7;
		Panel panel6 = (panel7 = new Panel());
		((ISupportInitialize)panel6).BeginInit();
		children9.Add(panel6);
		Panel panel8 = (panel4 = panel7);
		context.PushParent(panel4);
		Panel panel9 = panel4;
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		MultiBinding multiBinding6 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding7 = multiBinding2;
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj10 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding7.Converter = (IMultiValueConverter)obj10;
		IList<IBinding> bindings5 = multiBinding7.Bindings;
		CompiledBindingExtension obj11 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelMediaMemberViewModel,RootApp.Client.Avalonia.Member!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsMuted!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item5 = obj11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings5.Add(item5);
		IList<IBinding> bindings6 = multiBinding7.Bindings;
		CompiledBindingExtension obj12 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelMediaMemberViewModel,RootApp.Client.Avalonia.Member!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsAdminMuted!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item6 = obj12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings6.Add(item6);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(panel9, isVisibleProperty2, multiBinding6);
		global::Avalonia.Controls.Controls children10 = panel9.Children;
		RootSvgImage rootSvgImage7;
		RootSvgImage rootSvgImage6 = (rootSvgImage7 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage6).BeginInit();
		children10.Add(rootSvgImage6);
		RootSvgImage rootSvgImage8 = (rootSvgImage4 = rootSvgImage7);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage9 = rootSvgImage4;
		rootSvgImage9.Width = 15.0;
		rootSvgImage9.Height = 15.0;
		StyledProperty<string?> svgPathProperty2 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("MicrophoneBlockedSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding8 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage9, svgPathProperty2, binding8);
		context.PopParent();
		((ISupportInitialize)rootSvgImage8).EndInit();
		context.PopParent();
		((ISupportInitialize)panel8).EndInit();
		global::Avalonia.Controls.Controls children11 = stackPanel4.Children;
		Panel panel11;
		Panel panel10 = (panel11 = new Panel());
		((ISupportInitialize)panel10).BeginInit();
		children11.Add(panel10);
		Panel panel12 = (panel4 = panel11);
		context.PushParent(panel4);
		Panel panel13 = panel4;
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		MultiBinding multiBinding8 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding9 = multiBinding2;
		StaticResourceExtension staticResourceExtension8 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj13 = staticResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding9.Converter = (IMultiValueConverter)obj13;
		IList<IBinding> bindings7 = multiBinding9.Bindings;
		CompiledBindingExtension obj14 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelMediaMemberViewModel,RootApp.Client.Avalonia.Member!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsDeafened!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item7 = obj14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings7.Add(item7);
		IList<IBinding> bindings8 = multiBinding9.Bindings;
		CompiledBindingExtension obj15 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelMediaMemberViewModel,RootApp.Client.Avalonia.Member!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsAdminDeafened!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item8 = obj15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings8.Add(item8);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(panel13, isVisibleProperty3, multiBinding8);
		global::Avalonia.Controls.Controls children12 = panel13.Children;
		RootSvgImage rootSvgImage11;
		RootSvgImage rootSvgImage10 = (rootSvgImage11 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage10).BeginInit();
		children12.Add(rootSvgImage10);
		RootSvgImage rootSvgImage12 = (rootSvgImage4 = rootSvgImage11);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage13 = rootSvgImage4;
		rootSvgImage13.Width = 16.0;
		rootSvgImage13.Height = 16.0;
		StyledProperty<string?> svgPathProperty3 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("HeadphonesMutedSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding9 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage13, svgPathProperty3, binding9);
		context.PopParent();
		((ISupportInitialize)rootSvgImage12).EndInit();
		context.PopParent();
		((ISupportInitialize)panel12).EndInit();
		global::Avalonia.Controls.Controls children13 = stackPanel4.Children;
		Panel panel15;
		Panel panel14 = (panel15 = new Panel());
		((ISupportInitialize)panel14).BeginInit();
		children13.Add(panel14);
		Panel panel16 = (panel4 = panel15);
		context.PushParent(panel4);
		Panel panel17 = panel4;
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		MultiBinding multiBinding10 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding11 = multiBinding2;
		StaticResourceExtension staticResourceExtension9 = new StaticResourceExtension("AndConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj16 = staticResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding11.Converter = (IMultiValueConverter)obj16;
		IList<IBinding> bindings9 = multiBinding11.Bindings;
		CompiledBindingExtension obj17 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelMediaMemberViewModel,RootApp.Client.Avalonia.Member!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsDeafened!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item9 = obj17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings9.Add(item9);
		IList<IBinding> bindings10 = multiBinding11.Bindings;
		CompiledBindingExtension obj18 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelMediaMemberViewModel,RootApp.Client.Avalonia.Member!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsAdminDeafened!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item10 = obj18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings10.Add(item10);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(panel17, isVisibleProperty4, multiBinding10);
		global::Avalonia.Controls.Controls children14 = panel17.Children;
		RootSvgImage rootSvgImage15;
		RootSvgImage rootSvgImage14 = (rootSvgImage15 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage14).BeginInit();
		children14.Add(rootSvgImage14);
		RootSvgImage rootSvgImage16 = (rootSvgImage4 = rootSvgImage15);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage17 = rootSvgImage4;
		rootSvgImage17.Width = 16.0;
		rootSvgImage17.Height = 16.0;
		StyledProperty<string?> svgPathProperty4 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("HeadphonesBlockedSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding10 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage17, svgPathProperty4, binding10);
		context.PopParent();
		((ISupportInitialize)rootSvgImage16).EndInit();
		context.PopParent();
		((ISupportInitialize)panel16).EndInit();
		global::Avalonia.Controls.Controls children15 = stackPanel4.Children;
		Panel panel19;
		Panel panel18 = (panel19 = new Panel());
		((ISupportInitialize)panel18).BeginInit();
		children15.Add(panel18);
		Panel panel20 = (panel4 = panel19);
		context.PushParent(panel4);
		Panel panel21 = panel4;
		panel21.ClipToBounds = false;
		StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
		MultiBinding multiBinding12 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding13 = multiBinding2;
		StaticResourceExtension staticResourceExtension10 = new StaticResourceExtension("OrConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj19 = staticResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding13.Converter = (IMultiValueConverter)obj19;
		IList<IBinding> bindings11 = multiBinding13.Bindings;
		CompiledBindingExtension obj20 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelMediaMemberViewModel,RootApp.Client.Avalonia.Member!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsScreen!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item11 = obj20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings11.Add(item11);
		IList<IBinding> bindings12 = multiBinding13.Bindings;
		CompiledBindingExtension obj21 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelMediaMemberViewModel,RootApp.Client.Avalonia.Member!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Media.MediaMember,RootApp.Client.CoreDomain.IsVideo!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item12 = obj21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings12.Add(item12);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(panel21, isVisibleProperty5, multiBinding12);
		global::Avalonia.Controls.Controls children16 = panel21.Children;
		Border border7;
		Border border6 = (border7 = new Border());
		((ISupportInitialize)border6).BeginInit();
		children16.Add(border6);
		Border border8 = (border4 = border7);
		context.PushParent(border4);
		Border border9 = border4;
		border9.Name = "LiveBadgeGlow";
		obj2 = border9;
		context.AvaloniaNameScope.Register("LiveBadgeGlow", obj2);
		border9.CornerRadius = new CornerRadius(4.0, 4.0, 4.0, 4.0);
		border9.VerticalAlignment = VerticalAlignment.Center;
		Styles styles3 = border9.Styles;
		Style style3 = new Style();
		style3.Selector = ((Selector?)null).OfType(typeof(Border)).Name("LiveBadgeGlow");
		IList<IAnimation> animations2 = style3.Animations;
		Animation animation2 = new Animation();
		animation2.IterationCount = IterationCount.Parse("Infinite");
		animation2.Duration = TimeSpan.FromTicks(20000000L);
		animation2.PlaybackDirection = PlaybackDirection.Alternate;
		KeyFrames children17 = animation2.Children;
		KeyFrame keyFrame3 = new KeyFrame();
		keyFrame3.Cue = Cue.Parse("0%", CultureInfo.InvariantCulture);
		AvaloniaList<IAnimationSetter> setters3 = keyFrame3.Setters;
		Setter setter6 = new Setter();
		setter6.Property = Border.BoxShadowProperty;
		setter6.Value = BoxShadows.Parse("0 0 6 1 #60E03E3E");
		setters3.Add(setter6);
		children17.Add(keyFrame3);
		KeyFrames children18 = animation2.Children;
		KeyFrame keyFrame4 = new KeyFrame();
		keyFrame4.Cue = Cue.Parse("100%", CultureInfo.InvariantCulture);
		AvaloniaList<IAnimationSetter> setters4 = keyFrame4.Setters;
		Setter setter7 = new Setter();
		setter7.Property = Border.BoxShadowProperty;
		setter7.Value = BoxShadows.Parse("0 0 10 2 #90E03E3E");
		setters4.Add(setter7);
		children18.Add(keyFrame4);
		animations2.Add(animation2);
		styles3.Add(style3);
		Button button7;
		Button button6 = (button7 = new Button());
		((ISupportInitialize)button6).BeginInit();
		border9.Child = button6;
		Button button8 = (button4 = button7);
		context.PushParent(button4);
		Button button9 = button4;
		button9.Name = "LiveBadge";
		obj2 = button9;
		context.AvaloniaNameScope.Register("LiveBadge", obj2);
		button9.Classes.Add("BasicButton");
		button9.Background = new ImmutableSolidColorBrush(4292886078u);
		button9.CornerRadius = new CornerRadius(4.0, 4.0, 4.0, 4.0);
		button9.Padding = new Thickness(6.0, 2.0, 6.0, 2.0);
		button9.VerticalAlignment = VerticalAlignment.Center;
		button9.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension25 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelMediaMemberViewModel,RootApp.Client.Avalonia.FocusMediaCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension26 = compiledBindingExtension25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, commandProperty, compiledBindingExtension26);
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		button9.Content = textBlock6;
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.VerticalAlignment = VerticalAlignment.Center;
		textBlock9.FontSize = 10.0;
		textBlock9.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StaticResourceExtension staticResourceExtension11 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj22 = staticResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock9, obj22);
		textBlock9.FontWeight = FontWeight.Bold;
		textBlock9.Text = "LIVE";
		textBlock9.LetterSpacing = 0.5;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding11 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding11);
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		context.PopParent();
		((ISupportInitialize)button8).EndInit();
		context.PopParent();
		((ISupportInitialize)border8).EndInit();
		context.PopParent();
		((ISupportInitialize)panel20).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void !XamlIlPopulateTrampoline(ChannelMediaMemberView P_0)
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
