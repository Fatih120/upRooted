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
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Resources.Converters.Messages;
using RootApp.Client.Avalonia.Resources.Converters.Roles;

namespace RootApp.Client.Avalonia.UI.Messages;

public class MessageReplyView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_177
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MessageReplyView> context = CreateContext(P_0);
			return new MessageReplyDeletedConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<MessageReplyView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MessageReplyView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MessageReplyView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Messages/MessageReplyView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Messages/MessageReplyView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (MessageReplyView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MessageReplyView> context = CreateContext(P_0);
			return new MessageReplyDeletedFontStyleConverter();
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MessageReplyView> context = CreateContext(P_0);
			return new CommunityRoleColorConverter();
		}
	}

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	public MessageReplyView()
	{
		InitializeComponent();
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	[ExcludeFromCodeCoverage]
	public void InitializeComponent(bool P_0 = true)
	{
		if (P_0)
		{
			!XamlIlPopulateTrampoline(this);
		}
	}

	[CompilerGenerated]
	private unsafe static void !XamlIlPopulate(IServiceProvider P_0, MessageReplyView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<MessageReplyView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MessageReplyView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Messages/MessageReplyView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Messages/MessageReplyView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension obj = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.MessageReplyViewModel,RootApp.Client.Avalonia.MessageContainerMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.IMessageContainerMember,RootApp.Client.CoreDomain.GlobalUser!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.User.GlobalUser,RootApp.Client.CoreDomain.IsBlocked!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			FallbackValue = "True"
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension = obj.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(P_1, isVisibleProperty, compiledBindingExtension);
		ResourceDictionary resourceDictionary = new ResourceDictionary();
		if (resourceDictionary is ResourceDictionary resourceDictionary2)
		{
			resourceDictionary2.EnsureCapacity(resourceDictionary2.Count + 3);
		}
		resourceDictionary.AddDeferred("MessageReplyDeletedConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_177.Build_1), context));
		resourceDictionary.AddDeferred("MessageReplyDeletedFontStyleConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_177.Build_2), context));
		resourceDictionary.AddDeferred("CommunityRoleColorConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_177.Build_3), context));
		P_1.Resources = resourceDictionary;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		P_1.Content = button;
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		button4.Classes.Add("TransparentButtonWithOpacity");
		button4.Background = new ImmutableSolidColorBrush(16777215u);
		button4.Cursor = new Cursor(StandardCursorType.Hand);
		button4.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.MessageReplyViewModel,RootApp.Client.Avalonia.JumpToMessageCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension3 = compiledBindingExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, commandProperty, compiledBindingExtension3);
		button4.HorizontalContentAlignment = HorizontalAlignment.Stretch;
		button4.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		button4.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		button4.Content = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(34.0, GridUnitType.Pixel)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(16.0, GridUnitType.Pixel)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		global::Avalonia.Controls.Controls children = grid4.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Grid.SetColumn(border4, 0);
		Grid.SetColumnSpan(border4, 2);
		border4.Width = 28.0;
		border4.Height = 9.0;
		border4.Background = new ImmutableSolidColorBrush(16777215u);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border4, borderBrushProperty, binding);
		border4.BorderThickness = new Thickness(2.0, 2.0, 0.0, 0.0);
		border4.CornerRadius = new CornerRadius(5.0, 0.0, 0.0, 0.0);
		border4.VerticalAlignment = VerticalAlignment.Center;
		border4.Margin = new Thickness(12.0, 0.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		global::Avalonia.Controls.Controls children2 = grid4.Children;
		RootImageLoader rootImageLoader2;
		RootImageLoader rootImageLoader = (rootImageLoader2 = new RootImageLoader());
		((ISupportInitialize)rootImageLoader).BeginInit();
		children2.Add(rootImageLoader);
		RootImageLoader rootImageLoader4;
		RootImageLoader rootImageLoader3 = (rootImageLoader4 = rootImageLoader2);
		context.PushParent(rootImageLoader4);
		Grid.SetColumn(rootImageLoader4, 2);
		rootImageLoader4.Width = 14.0;
		rootImageLoader4.Height = 14.0;
		rootImageLoader4.CornerRadius = new CornerRadius(4.0, 4.0, 4.0, 4.0);
		StyledProperty<IBrush?> backgroundProperty = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, backgroundProperty, binding2);
		StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.MessageReplyViewModel,RootApp.Client.Avalonia.ProfilePictureAsyncBitmapWrapper!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
		context.ProvideTargetProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension5 = compiledBindingExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, sourceProperty, compiledBindingExtension5);
		rootImageLoader4.LoadingPlaceholderSize = 16.0;
		rootImageLoader4.Stretch = Stretch.UniformToFill;
		rootImageLoader4.Margin = new Thickness(0.0, 0.0, 4.0, 6.0);
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		MultiBinding multiBinding2;
		MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding3 = multiBinding2;
		multiBinding3.Converter = BoolConverters.And;
		IList<IBinding> bindings = multiBinding3.Bindings;
		CompiledBindingExtension obj2 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.MessageReplyViewModel,RootApp.Client.Avalonia.MessageReply!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.MessageReply,RootApp.Client.CoreDomain.IsDeleted!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item = obj2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings.Add(item);
		IList<IBinding> bindings2 = multiBinding3.Bindings;
		CompiledBindingExtension compiledBindingExtension6 = new CompiledBindingExtension();
		compiledBindingExtension6.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.MessageReplyViewModel,RootApp.Client.Avalonia.MessageReply!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.MessageReply,RootApp.Client.CoreDomain.MessageContent!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build();
		compiledBindingExtension6.Converter = ObjectConverters.IsNotNull;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item2 = compiledBindingExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings2.Add(item2);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(rootImageLoader4, isVisibleProperty2, multiBinding);
		context.PopParent();
		((ISupportInitialize)rootImageLoader3).EndInit();
		global::Avalonia.Controls.Controls children3 = grid4.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children3.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		Grid.SetColumn(textBlock5, 3);
		textBlock5.FontSize = 13.0;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension obj3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.MessageReplyViewModel,RootApp.Client.Avalonia.MessageContainerMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.IMessageContainerMember,RootApp.Client.CoreDomain.GlobalUser!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.User.GlobalUser,RootApp.Client.CoreDomain.UserName!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build())
		{
			TargetNullValue = "[Username]"
		};
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension7 = obj3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, compiledBindingExtension7);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj4 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock5, obj4);
		textBlock5.FontWeight = FontWeight.Medium;
		textBlock5.Margin = new Thickness(0.0, 0.0, 4.0, 6.0);
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		MultiBinding multiBinding4 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding5 = multiBinding2;
		multiBinding5.Converter = BoolConverters.And;
		IList<IBinding> bindings3 = multiBinding5.Bindings;
		CompiledBindingExtension obj5 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.MessageReplyViewModel,RootApp.Client.Avalonia.MessageReply!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.MessageReply,RootApp.Client.CoreDomain.IsDeleted!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item3 = obj5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings3.Add(item3);
		IList<IBinding> bindings4 = multiBinding5.Bindings;
		CompiledBindingExtension compiledBindingExtension8 = new CompiledBindingExtension();
		compiledBindingExtension8.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.MessageReplyViewModel,RootApp.Client.Avalonia.MessageReply!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.MessageReply,RootApp.Client.CoreDomain.MessageContent!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build();
		compiledBindingExtension8.Converter = ObjectConverters.IsNotNull;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item4 = compiledBindingExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings4.Add(item4);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(textBlock5, isVisibleProperty3, multiBinding4);
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		MultiBinding multiBinding6 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding7 = multiBinding2;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("CommunityRoleColorConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Converter!Property();
		object? obj6 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding7.Converter = (IMultiValueConverter)obj6;
		IList<IBinding> bindings5 = multiBinding7.Bindings;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension();
		compiledBindingExtension9.Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.MessageReplyViewModel,RootApp.Client.Avalonia.CommunityMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Member,RootApp.Client.CoreDomain.Roles!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor, true).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Repositories.Community.IMemberRoleService,RootApp.Client.CoreDomain.PrimaryRole!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Role,RootApp.Client.CoreDomain.RoleColorHex!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension9.FallbackValue = null;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item5 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings5.Add(item5);
		IList<IBinding> bindings6 = multiBinding7.Bindings;
		CompiledBindingExtension compiledBindingExtension10 = new CompiledBindingExtension();
		compiledBindingExtension10.Path = new CompiledBindingPathBuilder(1).Property(Application.ActualThemeVariantProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
		compiledBindingExtension10.Source = Application.Current;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.MultiBinding,Avalonia.Markup.Bindings!Property();
		CompiledBindingExtension item6 = compiledBindingExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings6.Add(item6);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, multiBinding6);
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		global::Avalonia.Controls.Controls children4 = grid4.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children4.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		Grid.SetColumn(textBlock9, 4);
		textBlock9.FontSize = 13.0;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding3);
		StyledProperty<string?> textProperty2 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension12;
		CompiledBindingExtension compiledBindingExtension11 = (compiledBindingExtension12 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.MessageReplyViewModel,RootApp.Client.Avalonia.MessageReply!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.MessageReply,RootApp.Client.CoreDomain.MessageContent!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension12);
		CompiledBindingExtension compiledBindingExtension13 = compiledBindingExtension12;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("MessageReplyDeletedConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj7 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension13.Converter = (IValueConverter)obj7;
		context.PopParent();
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, textProperty2, compiledBindingExtension14);
		StyledProperty<FontStyle> fontStyleProperty = TextBlock.FontStyleProperty;
		CompiledBindingExtension compiledBindingExtension15 = (compiledBindingExtension12 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.MessageReplyViewModel,RootApp.Client.Avalonia.MessageReply!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Messages.MessageReply,RootApp.Client.CoreDomain.MessageContent!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension12);
		CompiledBindingExtension compiledBindingExtension16 = compiledBindingExtension12;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("MessageReplyDeletedFontStyleConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj8 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension16.Converter = (IValueConverter)obj8;
		context.PopParent();
		context.ProvideTargetProperty = TextBlock.FontStyleProperty;
		CompiledBindingExtension compiledBindingExtension17 = compiledBindingExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, fontStyleProperty, compiledBindingExtension17);
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj9 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock9, obj9);
		textBlock9.FontWeight = FontWeight.Normal;
		textBlock9.TextTrimming = TextTrimming.CharacterEllipsis;
		textBlock9.TextWrapping = TextWrapping.NoWrap;
		textBlock9.Margin = new Thickness(0.0, 0.0, 0.0, 6.0);
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
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
	private static void !XamlIlPopulateTrampoline(MessageReplyView P_0)
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
