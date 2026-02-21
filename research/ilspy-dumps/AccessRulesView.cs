using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Resources.Converters.Channels;
using RootApp.Client.Avalonia.Resources.Strings;

namespace RootApp.Client.Avalonia.UI.Community.Channels.Permissions;

public class AccessRulesView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_69
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<AccessRulesView> context = CreateContext(P_0);
			return new ChannelPermissionMarginConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<AccessRulesView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<AccessRulesView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<AccessRulesView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Community/Channels/Permissions/AccessRulesView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Channels/Permissions/AccessRulesView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (AccessRulesView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<AccessRulesView> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal CheckBox SyncPermissionsCheckBox;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	private AccessRulesViewModel? _channelAccessRulesViewModel => base.DataContext as AccessRulesViewModel;

	public AccessRulesView()
	{
		InitializeComponent();
	}

	private void onRoleSelectorFlyoutOpening(object? sender, EventArgs e)
	{
		if (_channelAccessRulesViewModel != null && _channelAccessRulesViewModel.RoleSelectorFlyoutOpeningCommand.CanExecute(null))
		{
			_channelAccessRulesViewModel.RoleSelectorFlyoutOpeningCommand.Execute(null);
		}
	}

	private void onMemberFlyoutOpening(object? sender, EventArgs e)
	{
		if (_channelAccessRulesViewModel != null && _channelAccessRulesViewModel.MemberPickerFlyoutOpeningCommand.CanExecute(null))
		{
			_channelAccessRulesViewModel.MemberPickerFlyoutOpeningCommand.Execute(null);
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
		SyncPermissionsCheckBox = this.FindNameScope()?.Find<CheckBox>("SyncPermissionsCheckBox");
	}

	[CompilerGenerated]
	private unsafe static void !XamlIlPopulate(IServiceProvider P_0, AccessRulesView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<AccessRulesView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<AccessRulesView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Community/Channels/Permissions/AccessRulesView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Channels/Permissions/AccessRulesView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		ResourceDictionary resourceDictionary = new ResourceDictionary();
		resourceDictionary.AddDeferred("ChannelPermissionMarginConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_69.Build_1), context));
		P_1.Resources = resourceDictionary;
		RootScrollViewer rootScrollViewer2;
		RootScrollViewer rootScrollViewer = (rootScrollViewer2 = new RootScrollViewer());
		((ISupportInitialize)rootScrollViewer).BeginInit();
		P_1.Content = rootScrollViewer;
		RootScrollViewer rootScrollViewer4;
		RootScrollViewer rootScrollViewer3 = (rootScrollViewer4 = rootScrollViewer2);
		context.PushParent(rootScrollViewer4);
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		rootScrollViewer4.Content = stackPanel;
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		stackPanel5.Margin = new Thickness(24.0, 24.0, 24.0, 24.0);
		global::Avalonia.Controls.Controls children = stackPanel5.Children;
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		children.Add(rootBorder);
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		RootBorder rootBorder5 = rootBorder4;
		rootBorder5.Background = new ImmutableSolidColorBrush(439926264u);
		rootBorder5.BorderBrush = new ImmutableSolidColorBrush(4281908728u);
		rootBorder5.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder5.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		rootBorder5.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRulesViewModel,RootApp.Client.Avalonia.IsAppChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder5, isVisibleProperty, compiledBindingExtension2);
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		rootBorder5.Child = stackPanel6;
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		stackPanel9.Margin = new Thickness(24.0, 24.0, 24.0, 24.0);
		global::Avalonia.Controls.Controls children2 = stackPanel9.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children2.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.AppChannelPermissionsAllCaps;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock5, obj);
		textBlock5.FontWeight = FontWeight.Bold;
		textBlock5.FontSize = 14.0;
		textBlock5.Foreground = new ImmutableSolidColorBrush(4281908728u);
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		global::Avalonia.Controls.Controls children3 = stackPanel9.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children3.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		textBlock9.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.AppChannelPermissionsDescription;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock9, obj2);
		textBlock9.FontWeight = (FontWeight)450;
		textBlock9.TextWrapping = TextWrapping.Wrap;
		textBlock9.FontSize = 14.0;
		textBlock9.LineHeight = 20.0;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty, binding);
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		global::Avalonia.Controls.Controls children4 = stackPanel5.Children;
		RootBorder rootBorder7;
		RootBorder rootBorder6 = (rootBorder7 = new RootBorder());
		((ISupportInitialize)rootBorder6).BeginInit();
		children4.Add(rootBorder6);
		RootBorder rootBorder8 = (rootBorder4 = rootBorder7);
		context.PushParent(rootBorder4);
		RootBorder rootBorder9 = rootBorder4;
		rootBorder9.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, backgroundProperty, binding2);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder9, borderBrushProperty, binding3);
		rootBorder9.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder9.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		StackPanel stackPanel11;
		StackPanel stackPanel10 = (stackPanel11 = new StackPanel());
		((ISupportInitialize)stackPanel10).BeginInit();
		rootBorder9.Child = stackPanel10;
		StackPanel stackPanel12 = (stackPanel4 = stackPanel11);
		context.PushParent(stackPanel4);
		StackPanel stackPanel13 = stackPanel4;
		stackPanel13.Margin = new Thickness(24.0, 24.0, 24.0, 24.0);
		global::Avalonia.Controls.Controls children5 = stackPanel13.Children;
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		children5.Add(grid);
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		Grid grid5 = grid4;
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension obj3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRulesViewModel,RootApp.Client.Avalonia.SyncPermissions!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Converter = ObjectConverters.IsNotNull
		};
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension3 = obj3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(grid5, isVisibleProperty2, compiledBindingExtension3);
		StyledProperty<Thickness> marginProperty = Layoutable.MarginProperty;
		CompiledBindingExtension compiledBindingExtension5;
		CompiledBindingExtension compiledBindingExtension4 = (compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "SyncPermissionsCheckBox").Property(ToggleButton.IsCheckedProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension5);
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("ChannelPermissionMarginConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia.Data.BindingBase,Avalonia.Markup.Converter!Property();
		object? obj4 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension5.Converter = (IValueConverter)obj4;
		context.PopParent();
		context.ProvideTargetProperty = Layoutable.MarginProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(grid5, marginProperty, compiledBindingExtension6);
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		ColumnDefinitions columnDefinitions = grid5.ColumnDefinitions;
		ColumnDefinition columnDefinition = new ColumnDefinition();
		columnDefinition.MinWidth = 104.0;
		columnDefinition.Width = new GridLength(0.0, GridUnitType.Auto);
		columnDefinitions.Add(columnDefinition);
		grid5.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		global::Avalonia.Controls.Controls children6 = grid5.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children6.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		border4.Width = 40.0;
		border4.Height = 40.0;
		border4.Margin = new Thickness(0.0, 0.0, 20.0, 0.0);
		border4.CornerRadius = new CornerRadius(20.0, 20.0, 20.0, 20.0);
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border4, backgroundProperty2, binding4);
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		border4.Child = rootSvgImage;
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		rootSvgImage4.Width = 13.33;
		rootSvgImage4.Height = 17.15;
		StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("RefreshActionSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage4, svgPathProperty, binding5);
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		global::Avalonia.Controls.Controls children7 = grid5.Children;
		StackPanel stackPanel15;
		StackPanel stackPanel14 = (stackPanel15 = new StackPanel());
		((ISupportInitialize)stackPanel14).BeginInit();
		children7.Add(stackPanel14);
		StackPanel stackPanel16 = (stackPanel4 = stackPanel15);
		context.PushParent(stackPanel4);
		StackPanel stackPanel17 = stackPanel4;
		Grid.SetColumn(stackPanel17, 1);
		Grid.SetRow(stackPanel17, 0);
		global::Avalonia.Controls.Controls children8 = stackPanel17.Children;
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		children8.Add(textBlock10);
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		textBlock13.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock13.VerticalAlignment = VerticalAlignment.Center;
		textBlock13.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.SyncPermissionsAllCaps;
		textBlock13.FontSize = 14.0;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, foregroundProperty2, binding6);
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj5 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock13, obj5);
		textBlock13.FontWeight = FontWeight.Bold;
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		global::Avalonia.Controls.Controls children9 = stackPanel17.Children;
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		children9.Add(textBlock14);
		TextBlock textBlock16 = (textBlock4 = textBlock15);
		context.PushParent(textBlock4);
		TextBlock textBlock17 = textBlock4;
		textBlock17.Margin = new Thickness(0.0, 4.0, 0.0, 0.0);
		textBlock17.LineHeight = 20.0;
		textBlock17.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.SyncPermissionsDescription;
		textBlock17.TextWrapping = TextWrapping.Wrap;
		textBlock17.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock17, foregroundProperty3, binding7);
		textBlock17.FontSize = 14.0;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj6 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock17, obj6);
		textBlock17.FontWeight = (FontWeight)450;
		context.PopParent();
		((ISupportInitialize)textBlock16).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel16).EndInit();
		global::Avalonia.Controls.Controls children10 = grid5.Children;
		CheckBox checkBox2;
		CheckBox checkBox = (checkBox2 = new CheckBox());
		((ISupportInitialize)checkBox).BeginInit();
		children10.Add(checkBox);
		CheckBox checkBox4;
		CheckBox checkBox3 = (checkBox4 = checkBox2);
		context.PushParent(checkBox4);
		checkBox4.Name = "SyncPermissionsCheckBox";
		object obj7 = checkBox4;
		context.AvaloniaNameScope.Register("SyncPermissionsCheckBox", obj7);
		Grid.SetRow(checkBox4, 0);
		checkBox4.Classes.Add("ToggleSwitch");
		StyledProperty<bool?> isCheckedProperty = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension obj8 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRulesViewModel,RootApp.Client.Avalonia.SyncPermissions!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = ToggleButton.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension7 = obj8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(checkBox4, isCheckedProperty, compiledBindingExtension7);
		checkBox4.HorizontalAlignment = HorizontalAlignment.Right;
		Grid.SetColumn(checkBox4, 3);
		checkBox4.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)checkBox3).EndInit();
		global::Avalonia.Controls.Controls children11 = grid5.Children;
		Rectangle rectangle2;
		Rectangle rectangle = (rectangle2 = new Rectangle());
		((ISupportInitialize)rectangle).BeginInit();
		children11.Add(rectangle);
		Rectangle rectangle4;
		Rectangle rectangle3 = (rectangle4 = rectangle2);
		context.PushParent(rectangle4);
		Rectangle rectangle5 = rectangle4;
		Grid.SetRow(rectangle5, 1);
		Grid.SetColumnSpan(rectangle5, 4);
		rectangle5.Margin = new Thickness(0.0, 32.0, 0.0, 0.0);
		rectangle5.VerticalAlignment = VerticalAlignment.Bottom;
		rectangle5.Height = 0.5;
		StyledProperty<IBrush?> fillProperty = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle5, fillProperty, binding8);
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension8 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().ElementName(context.AvaloniaNameScope, "SyncPermissionsCheckBox").Property(ToggleButton.IsCheckedProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension9 = compiledBindingExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle5, isVisibleProperty3, compiledBindingExtension9);
		context.PopParent();
		((ISupportInitialize)rectangle3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		global::Avalonia.Controls.Controls children12 = stackPanel13.Children;
		StackPanel stackPanel19;
		StackPanel stackPanel18 = (stackPanel19 = new StackPanel());
		((ISupportInitialize)stackPanel18).BeginInit();
		children12.Add(stackPanel18);
		StackPanel stackPanel20 = (stackPanel4 = stackPanel19);
		context.PushParent(stackPanel4);
		StackPanel stackPanel21 = stackPanel4;
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension10 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().ElementName(context.AvaloniaNameScope, "SyncPermissionsCheckBox").Property(ToggleButton.IsCheckedProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension11 = compiledBindingExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(stackPanel21, isVisibleProperty4, compiledBindingExtension11);
		global::Avalonia.Controls.Controls children13 = stackPanel21.Children;
		TextBlock textBlock19;
		TextBlock textBlock18 = (textBlock19 = new TextBlock());
		((ISupportInitialize)textBlock18).BeginInit();
		children13.Add(textBlock18);
		TextBlock textBlock20 = (textBlock4 = textBlock19);
		context.PushParent(textBlock4);
		TextBlock textBlock21 = textBlock4;
		textBlock21.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock21.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ChannelVisibilityAllCaps;
		textBlock21.FontSize = 14.0;
		StyledProperty<IBrush?> foregroundProperty4 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock21, foregroundProperty4, binding9);
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj9 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock21, obj9);
		textBlock21.FontWeight = FontWeight.Bold;
		context.PopParent();
		((ISupportInitialize)textBlock20).EndInit();
		global::Avalonia.Controls.Controls children14 = stackPanel21.Children;
		TextBlock textBlock23;
		TextBlock textBlock22 = (textBlock23 = new TextBlock());
		((ISupportInitialize)textBlock22).BeginInit();
		children14.Add(textBlock22);
		TextBlock textBlock24 = (textBlock4 = textBlock23);
		context.PushParent(textBlock4);
		TextBlock textBlock25 = textBlock4;
		textBlock25.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		textBlock25.LineHeight = 20.0;
		textBlock25.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ChannelVisibilityDescription;
		textBlock25.TextWrapping = TextWrapping.Wrap;
		textBlock25.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty5 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock25, foregroundProperty5, binding10);
		textBlock25.FontSize = 14.0;
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj10 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock25, obj10);
		textBlock25.FontWeight = (FontWeight)450;
		context.PopParent();
		((ISupportInitialize)textBlock24).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel20).EndInit();
		global::Avalonia.Controls.Controls children15 = stackPanel13.Children;
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		children15.Add(grid6);
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		grid9.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension12 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().ElementName(context.AvaloniaNameScope, "SyncPermissionsCheckBox").Property(ToggleButton.IsCheckedProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension13 = compiledBindingExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(grid9, isVisibleProperty5, compiledBindingExtension13);
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
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		global::Avalonia.Controls.Controls children16 = grid9.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children16.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		Button button5 = button4;
		button5.Classes.Add("BorderButton");
		button5.Height = 36.0;
		button5.Margin = new Thickness(0.0, 0.0, 12.0, 0.0);
		button5.HorizontalAlignment = HorizontalAlignment.Stretch;
		StaticResourceExtension staticResourceExtension8 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj11 = staticResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_3(button5, obj11);
		button5.FontWeight = FontWeight.Medium;
		button5.Content = RootApp.Client.Avalonia.Resources.Strings.Resources.AddRole;
		StyledProperty<IBrush?> borderBrushProperty2 = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, borderBrushProperty2, binding11);
		StyledProperty<IBrush?> foregroundProperty6 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding12 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, foregroundProperty6, binding12);
		button5.Background = new ImmutableSolidColorBrush(16777215u);
		button5.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		button5.FontSize = 13.0;
		button5.CornerRadius = new CornerRadius(15.0, 15.0, 15.0, 15.0);
		RootFlyout rootFlyout;
		RootFlyout flyout = (rootFlyout = new RootFlyout());
		context.PushParent(rootFlyout);
		RootFlyout rootFlyout2 = rootFlyout;
		rootFlyout2.Placement = PlacementMode.Pointer;
		rootFlyout2.VerticalOffset = -12.0;
		rootFlyout2.HorizontalOffset = -8.0;
		rootFlyout2.Opening += context.RootObject.onRoleSelectorFlyoutOpening;
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		rootFlyout2.Content = contentControl;
		ContentControl contentControl4;
		ContentControl contentControl3 = (contentControl4 = contentControl2);
		context.PushParent(contentControl4);
		ContentControl contentControl5 = contentControl4;
		CompiledBindingExtension compiledBindingExtension14 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRulesViewModel,RootApp.Client.Avalonia.RoleSelectorViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension15 = compiledBindingExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl5, compiledBindingExtension15);
		context.PopParent();
		((ISupportInitialize)contentControl3).EndInit();
		context.PopParent();
		button5.Flyout = flyout;
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		global::Avalonia.Controls.Controls children17 = grid9.Children;
		Rectangle rectangle7;
		Rectangle rectangle6 = (rectangle7 = new Rectangle());
		((ISupportInitialize)rectangle6).BeginInit();
		children17.Add(rectangle6);
		Rectangle rectangle8 = (rectangle4 = rectangle7);
		context.PushParent(rectangle4);
		Rectangle rectangle9 = rectangle4;
		Grid.SetColumn(rectangle9, 1);
		rectangle9.Width = 0.5;
		StyledProperty<IBrush?> fillProperty2 = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding13 = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle9, fillProperty2, binding13);
		context.PopParent();
		((ISupportInitialize)rectangle8).EndInit();
		global::Avalonia.Controls.Controls children18 = grid9.Children;
		Button button7;
		Button button6 = (button7 = new Button());
		((ISupportInitialize)button6).BeginInit();
		children18.Add(button6);
		Button button8 = (button4 = button7);
		context.PushParent(button4);
		Button button9 = button4;
		Grid.SetColumn(button9, 2);
		button9.Classes.Add("BorderButton");
		button9.Height = 36.0;
		button9.Margin = new Thickness(12.0, 0.0, 0.0, 0.0);
		button9.HorizontalAlignment = HorizontalAlignment.Stretch;
		StaticResourceExtension staticResourceExtension9 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj12 = staticResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_3(button9, obj12);
		button9.FontWeight = FontWeight.Medium;
		button9.Content = RootApp.Client.Avalonia.Resources.Strings.Resources.AddUser;
		StyledProperty<IBrush?> borderBrushProperty3 = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding14 = dynamicResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, borderBrushProperty3, binding14);
		StyledProperty<IBrush?> foregroundProperty7 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension15 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding15 = dynamicResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, foregroundProperty7, binding15);
		button9.Background = new ImmutableSolidColorBrush(16777215u);
		button9.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		button9.FontSize = 13.0;
		button9.CornerRadius = new CornerRadius(15.0, 15.0, 15.0, 15.0);
		RootFlyout flyout2 = (rootFlyout = new RootFlyout());
		context.PushParent(rootFlyout);
		RootFlyout rootFlyout3 = rootFlyout;
		rootFlyout3.Placement = PlacementMode.Pointer;
		rootFlyout3.VerticalOffset = -12.0;
		rootFlyout3.HorizontalOffset = -8.0;
		rootFlyout3.Opening += context.RootObject.onMemberFlyoutOpening;
		ContentControl contentControl7;
		ContentControl contentControl6 = (contentControl7 = new ContentControl());
		((ISupportInitialize)contentControl6).BeginInit();
		rootFlyout3.Content = contentControl6;
		ContentControl contentControl8 = (contentControl4 = contentControl7);
		context.PushParent(contentControl4);
		ContentControl contentControl9 = contentControl4;
		CompiledBindingExtension compiledBindingExtension16 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRulesViewModel,RootApp.Client.Avalonia.MemberPickerViewModel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension17 = compiledBindingExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_4(contentControl9, compiledBindingExtension17);
		context.PopParent();
		((ISupportInitialize)contentControl8).EndInit();
		context.PopParent();
		button9.Flyout = flyout2;
		context.PopParent();
		((ISupportInitialize)button8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		global::Avalonia.Controls.Controls children19 = stackPanel13.Children;
		ItemsControl itemsControl2;
		ItemsControl itemsControl = (itemsControl2 = new ItemsControl());
		((ISupportInitialize)itemsControl).BeginInit();
		children19.Add(itemsControl);
		ItemsControl itemsControl4;
		ItemsControl itemsControl3 = (itemsControl4 = itemsControl2);
		context.PushParent(itemsControl4);
		StyledProperty<IEnumerable?> itemsSourceProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension18 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRulesViewModel,RootApp.Client.Avalonia.AccessRuleTags!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension19 = compiledBindingExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl4, itemsSourceProperty, compiledBindingExtension19);
		itemsControl4.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		StyledProperty<bool> isVisibleProperty6 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension20 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().ElementName(context.AvaloniaNameScope, "SyncPermissionsCheckBox").Property(ToggleButton.IsCheckedProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension21 = compiledBindingExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl4, isVisibleProperty6, compiledBindingExtension21);
		itemsControl4.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_69.Build_2), context)
		};
		context.PopParent();
		((ISupportInitialize)itemsControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel12).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootScrollViewer3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void !XamlIlPopulateTrampoline(AccessRulesView P_0)
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
