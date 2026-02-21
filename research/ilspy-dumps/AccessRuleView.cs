using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Resources.Strings;

namespace RootApp.Client.Avalonia.UI.Community.Channels.Permissions;

public class AccessRuleView : UserControl
{
	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock ChannelsHeader;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal PermissionView ManageChannelPermission;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock MessagesHeader;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal PermissionView SendMessagesPermission;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal PermissionView AttachFilesPermission;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal PermissionView MentionMembersPermission;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal PermissionView ViewHistoryPermission;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal PermissionView AddReactionsPermission;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal PermissionView DeleteMessagesPermission;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal PermissionView ManagePinnedMessagesPermission;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock FilesHeader;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal PermissionView ViewFilesPermission;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal PermissionView UploadFilesPermission;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal PermissionView ManageFilesPermission;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock CallsHeader;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal PermissionView StreamAudioPermission;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal PermissionView StreamVideoPermission;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal PermissionView MuteMicrophonesPermission;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal PermissionView MuteSpeakersPermission;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal PermissionView KickVoiceMemberPermission;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	public AccessRuleView()
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
		INameScope nameScope = this.FindNameScope();
		ChannelsHeader = nameScope?.Find<TextBlock>("ChannelsHeader");
		ManageChannelPermission = nameScope?.Find<PermissionView>("ManageChannelPermission");
		MessagesHeader = nameScope?.Find<TextBlock>("MessagesHeader");
		SendMessagesPermission = nameScope?.Find<PermissionView>("SendMessagesPermission");
		AttachFilesPermission = nameScope?.Find<PermissionView>("AttachFilesPermission");
		MentionMembersPermission = nameScope?.Find<PermissionView>("MentionMembersPermission");
		ViewHistoryPermission = nameScope?.Find<PermissionView>("ViewHistoryPermission");
		AddReactionsPermission = nameScope?.Find<PermissionView>("AddReactionsPermission");
		DeleteMessagesPermission = nameScope?.Find<PermissionView>("DeleteMessagesPermission");
		ManagePinnedMessagesPermission = nameScope?.Find<PermissionView>("ManagePinnedMessagesPermission");
		FilesHeader = nameScope?.Find<TextBlock>("FilesHeader");
		ViewFilesPermission = nameScope?.Find<PermissionView>("ViewFilesPermission");
		UploadFilesPermission = nameScope?.Find<PermissionView>("UploadFilesPermission");
		ManageFilesPermission = nameScope?.Find<PermissionView>("ManageFilesPermission");
		CallsHeader = nameScope?.Find<TextBlock>("CallsHeader");
		StreamAudioPermission = nameScope?.Find<PermissionView>("StreamAudioPermission");
		StreamVideoPermission = nameScope?.Find<PermissionView>("StreamVideoPermission");
		MuteMicrophonesPermission = nameScope?.Find<PermissionView>("MuteMicrophonesPermission");
		MuteSpeakersPermission = nameScope?.Find<PermissionView>("MuteSpeakersPermission");
		KickVoiceMemberPermission = nameScope?.Find<PermissionView>("KickVoiceMemberPermission");
	}

	[CompilerGenerated]
	private static void !XamlIlPopulate(IServiceProvider P_0, AccessRuleView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<AccessRuleView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<AccessRuleView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Community/Channels/Permissions/AccessRuleView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Channels/Permissions/AccessRuleView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		P_1.Content = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		global::Avalonia.Controls.Controls children = grid4.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		stackPanel5.Orientation = Orientation.Horizontal;
		stackPanel5.Margin = new Thickness(24.0, 24.0, 24.0, 24.0);
		global::Avalonia.Controls.Controls children2 = stackPanel5.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children2.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		button4.Classes.Add("BasicButton");
		button4.Width = 30.0;
		button4.Height = 30.0;
		button4.VerticalAlignment = VerticalAlignment.Center;
		button4.HorizontalAlignment = HorizontalAlignment.Left;
		button4.BorderThickness = new Thickness(1.0, 1.0, 1.0, 1.0);
		StyledProperty<IBrush?> borderBrushProperty = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, borderBrushProperty, binding);
		button4.Background = new ImmutableSolidColorBrush(16777215u);
		button4.CornerRadius = new CornerRadius(15.0, 15.0, 15.0, 15.0);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.NavigateBackCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, commandProperty, compiledBindingExtension2);
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		button4.Content = rootSvgImage;
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("DownArrowSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage4, svgPathProperty, binding2);
		rootSvgImage4.Width = 12.0;
		rootSvgImage4.Height = 10.0;
		rootSvgImage4.VerticalAlignment = VerticalAlignment.Center;
		rootSvgImage4.RenderTransform = new RotateTransform
		{
			Angle = 90.0
		};
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		global::Avalonia.Controls.Controls children3 = stackPanel5.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children3.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock5.Margin = new Thickness(12.0, 0.0, 0.0, 0.0);
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.Name!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, compiledBindingExtension4);
		textBlock5.FontSize = 20.0;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding3);
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock5, obj);
		textBlock5.FontWeight = FontWeight.Medium;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		global::Avalonia.Controls.Controls children4 = grid4.Children;
		RootScrollViewer rootScrollViewer2;
		RootScrollViewer rootScrollViewer = (rootScrollViewer2 = new RootScrollViewer());
		((ISupportInitialize)rootScrollViewer).BeginInit();
		children4.Add(rootScrollViewer);
		RootScrollViewer rootScrollViewer4;
		RootScrollViewer rootScrollViewer3 = (rootScrollViewer4 = rootScrollViewer2);
		context.PushParent(rootScrollViewer4);
		Grid.SetRow(rootScrollViewer4, 1);
		RootScrollViewer.SetEnableDropShadowOnScroll(rootScrollViewer4, true);
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		rootScrollViewer4.Content = stackPanel6;
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		stackPanel9.Margin = new Thickness(24.0, 0.0, 24.0, 24.0);
		global::Avalonia.Controls.Controls children5 = stackPanel9.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children5.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Name = "ChannelsHeader";
		object obj2 = textBlock9;
		context.AvaloniaNameScope.Register("ChannelsHeader", obj2);
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock9.Margin = new Thickness(0.0, 0.0, 0.0, 10.0);
		textBlock9.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Channels;
		textBlock9.FontSize = 20.0;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding4);
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj3 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock9, obj3);
		textBlock9.FontWeight = FontWeight.Medium;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		global::Avalonia.Controls.Controls children6 = stackPanel9.Children;
		PermissionView permissionView2;
		PermissionView permissionView = (permissionView2 = new PermissionView());
		((ISupportInitialize)permissionView).BeginInit();
		children6.Add(permissionView);
		PermissionView permissionView4;
		PermissionView permissionView3 = (permissionView4 = permissionView2);
		context.PushParent(permissionView4);
		PermissionView permissionView5 = permissionView4;
		permissionView5.Name = "ManageChannelPermission";
		obj2 = permissionView5;
		context.AvaloniaNameScope.Register("ManageChannelPermission", obj2);
		permissionView5.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		permissionView5.Title = RootApp.Client.Avalonia.Resources.Strings.Resources.FullControlChannel;
		permissionView5.Description = RootApp.Client.Avalonia.Resources.Strings.Resources.FullControlChannelDescription;
		permissionView5.IsAdministrative = true;
		StyledProperty<bool?> isCheckedProperty = PermissionView.IsCheckedProperty;
		CompiledBindingExtension obj4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.FullControlChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = PermissionView.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension5 = obj4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView5, isCheckedProperty, compiledBindingExtension5);
		context.PopParent();
		((ISupportInitialize)permissionView3).EndInit();
		global::Avalonia.Controls.Controls children7 = stackPanel9.Children;
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		children7.Add(textBlock10);
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		textBlock13.Name = "MessagesHeader";
		obj2 = textBlock13;
		context.AvaloniaNameScope.Register("MessagesHeader", obj2);
		textBlock13.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock13.Margin = new Thickness(0.0, 20.0, 0.0, 10.0);
		textBlock13.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Messages;
		textBlock13.FontSize = 20.0;
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, foregroundProperty3, binding5);
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj5 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock13, obj5);
		textBlock13.FontWeight = FontWeight.Medium;
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension6 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.IsAppChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension7 = compiledBindingExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, isVisibleProperty, compiledBindingExtension7);
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		global::Avalonia.Controls.Controls children8 = stackPanel9.Children;
		PermissionView permissionView7;
		PermissionView permissionView6 = (permissionView7 = new PermissionView());
		((ISupportInitialize)permissionView6).BeginInit();
		children8.Add(permissionView6);
		PermissionView permissionView8 = (permissionView4 = permissionView7);
		context.PushParent(permissionView4);
		PermissionView permissionView9 = permissionView4;
		permissionView9.Name = "SendMessagesPermission";
		obj2 = permissionView9;
		context.AvaloniaNameScope.Register("SendMessagesPermission", obj2);
		permissionView9.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		permissionView9.Title = RootApp.Client.Avalonia.Resources.Strings.Resources.SendMessages;
		permissionView9.Description = RootApp.Client.Avalonia.Resources.Strings.Resources.SendMessagesDescription;
		permissionView9.IsAdministrative = false;
		StyledProperty<bool?> isCheckedProperty2 = PermissionView.IsCheckedProperty;
		CompiledBindingExtension obj6 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.SendMessage!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = PermissionView.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension8 = obj6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView9, isCheckedProperty2, compiledBindingExtension8);
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.IsAppChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView9, isVisibleProperty2, compiledBindingExtension10);
		context.PopParent();
		((ISupportInitialize)permissionView8).EndInit();
		global::Avalonia.Controls.Controls children9 = stackPanel9.Children;
		PermissionView permissionView11;
		PermissionView permissionView10 = (permissionView11 = new PermissionView());
		((ISupportInitialize)permissionView10).BeginInit();
		children9.Add(permissionView10);
		PermissionView permissionView12 = (permissionView4 = permissionView11);
		context.PushParent(permissionView4);
		PermissionView permissionView13 = permissionView4;
		permissionView13.Name = "AttachFilesPermission";
		obj2 = permissionView13;
		context.AvaloniaNameScope.Register("AttachFilesPermission", obj2);
		permissionView13.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		permissionView13.Title = RootApp.Client.Avalonia.Resources.Strings.Resources.AttachFiles;
		permissionView13.Description = RootApp.Client.Avalonia.Resources.Strings.Resources.AttachFilesDescription;
		permissionView13.IsAdministrative = false;
		StyledProperty<bool?> isCheckedProperty3 = PermissionView.IsCheckedProperty;
		CompiledBindingExtension obj7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.AttachFiles!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = PermissionView.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension11 = obj7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView13, isCheckedProperty3, compiledBindingExtension11);
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension12 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.IsAppChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension13 = compiledBindingExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView13, isVisibleProperty3, compiledBindingExtension13);
		context.PopParent();
		((ISupportInitialize)permissionView12).EndInit();
		global::Avalonia.Controls.Controls children10 = stackPanel9.Children;
		PermissionView permissionView15;
		PermissionView permissionView14 = (permissionView15 = new PermissionView());
		((ISupportInitialize)permissionView14).BeginInit();
		children10.Add(permissionView14);
		PermissionView permissionView16 = (permissionView4 = permissionView15);
		context.PushParent(permissionView4);
		PermissionView permissionView17 = permissionView4;
		permissionView17.Name = "MentionMembersPermission";
		obj2 = permissionView17;
		context.AvaloniaNameScope.Register("MentionMembersPermission", obj2);
		permissionView17.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		permissionView17.Title = RootApp.Client.Avalonia.Resources.Strings.Resources.UseGlobalMentions;
		permissionView17.Description = RootApp.Client.Avalonia.Resources.Strings.Resources.UseGlobalMentionsDescription;
		permissionView17.IsAdministrative = false;
		StyledProperty<bool?> isCheckedProperty4 = PermissionView.IsCheckedProperty;
		CompiledBindingExtension obj8 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.UseGlobalMentions!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = PermissionView.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension14 = obj8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView17, isCheckedProperty4, compiledBindingExtension14);
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension15 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.IsAppChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension16 = compiledBindingExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView17, isVisibleProperty4, compiledBindingExtension16);
		context.PopParent();
		((ISupportInitialize)permissionView16).EndInit();
		global::Avalonia.Controls.Controls children11 = stackPanel9.Children;
		PermissionView permissionView19;
		PermissionView permissionView18 = (permissionView19 = new PermissionView());
		((ISupportInitialize)permissionView18).BeginInit();
		children11.Add(permissionView18);
		PermissionView permissionView20 = (permissionView4 = permissionView19);
		context.PushParent(permissionView4);
		PermissionView permissionView21 = permissionView4;
		permissionView21.Name = "ViewHistoryPermission";
		obj2 = permissionView21;
		context.AvaloniaNameScope.Register("ViewHistoryPermission", obj2);
		permissionView21.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		permissionView21.Title = RootApp.Client.Avalonia.Resources.Strings.Resources.ViewHistory;
		permissionView21.Description = RootApp.Client.Avalonia.Resources.Strings.Resources.ViewHistoryDescription;
		permissionView21.IsAdministrative = false;
		StyledProperty<bool?> isCheckedProperty5 = PermissionView.IsCheckedProperty;
		CompiledBindingExtension obj9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.ViewHistory!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = PermissionView.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension17 = obj9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView21, isCheckedProperty5, compiledBindingExtension17);
		StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension18 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.IsAppChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension19 = compiledBindingExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView21, isVisibleProperty5, compiledBindingExtension19);
		context.PopParent();
		((ISupportInitialize)permissionView20).EndInit();
		global::Avalonia.Controls.Controls children12 = stackPanel9.Children;
		PermissionView permissionView23;
		PermissionView permissionView22 = (permissionView23 = new PermissionView());
		((ISupportInitialize)permissionView22).BeginInit();
		children12.Add(permissionView22);
		PermissionView permissionView24 = (permissionView4 = permissionView23);
		context.PushParent(permissionView4);
		PermissionView permissionView25 = permissionView4;
		permissionView25.Name = "AddReactionsPermission";
		obj2 = permissionView25;
		context.AvaloniaNameScope.Register("AddReactionsPermission", obj2);
		permissionView25.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		permissionView25.Title = RootApp.Client.Avalonia.Resources.Strings.Resources.AddReactions;
		permissionView25.Description = RootApp.Client.Avalonia.Resources.Strings.Resources.AddReactionsDescription;
		permissionView25.IsAdministrative = false;
		StyledProperty<bool?> isCheckedProperty6 = PermissionView.IsCheckedProperty;
		CompiledBindingExtension obj10 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.AddReactions!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = PermissionView.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension20 = obj10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView25, isCheckedProperty6, compiledBindingExtension20);
		StyledProperty<bool> isVisibleProperty6 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension21 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.IsAppChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension22 = compiledBindingExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView25, isVisibleProperty6, compiledBindingExtension22);
		context.PopParent();
		((ISupportInitialize)permissionView24).EndInit();
		global::Avalonia.Controls.Controls children13 = stackPanel9.Children;
		PermissionView permissionView27;
		PermissionView permissionView26 = (permissionView27 = new PermissionView());
		((ISupportInitialize)permissionView26).BeginInit();
		children13.Add(permissionView26);
		PermissionView permissionView28 = (permissionView4 = permissionView27);
		context.PushParent(permissionView4);
		PermissionView permissionView29 = permissionView4;
		permissionView29.Name = "DeleteMessagesPermission";
		obj2 = permissionView29;
		context.AvaloniaNameScope.Register("DeleteMessagesPermission", obj2);
		permissionView29.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		permissionView29.Title = RootApp.Client.Avalonia.Resources.Strings.Resources.DeleteMessages;
		permissionView29.Description = RootApp.Client.Avalonia.Resources.Strings.Resources.DeleteMessagesDescription;
		permissionView29.IsAdministrative = true;
		StyledProperty<bool?> isCheckedProperty7 = PermissionView.IsCheckedProperty;
		CompiledBindingExtension obj11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.DeleteMessages!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = PermissionView.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension23 = obj11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView29, isCheckedProperty7, compiledBindingExtension23);
		StyledProperty<bool> isVisibleProperty7 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension24 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.IsAppChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension25 = compiledBindingExtension24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView29, isVisibleProperty7, compiledBindingExtension25);
		context.PopParent();
		((ISupportInitialize)permissionView28).EndInit();
		global::Avalonia.Controls.Controls children14 = stackPanel9.Children;
		PermissionView permissionView31;
		PermissionView permissionView30 = (permissionView31 = new PermissionView());
		((ISupportInitialize)permissionView30).BeginInit();
		children14.Add(permissionView30);
		PermissionView permissionView32 = (permissionView4 = permissionView31);
		context.PushParent(permissionView4);
		PermissionView permissionView33 = permissionView4;
		permissionView33.Name = "ManagePinnedMessagesPermission";
		obj2 = permissionView33;
		context.AvaloniaNameScope.Register("ManagePinnedMessagesPermission", obj2);
		permissionView33.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		permissionView33.Title = RootApp.Client.Avalonia.Resources.Strings.Resources.ManagePinnedMessages;
		permissionView33.Description = RootApp.Client.Avalonia.Resources.Strings.Resources.ManagePinnedMessagesDescription;
		permissionView33.IsAdministrative = true;
		StyledProperty<bool?> isCheckedProperty8 = PermissionView.IsCheckedProperty;
		CompiledBindingExtension obj12 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.ManagePinnedMessages!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = PermissionView.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension26 = obj12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView33, isCheckedProperty8, compiledBindingExtension26);
		StyledProperty<bool> isVisibleProperty8 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension27 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.IsAppChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension28 = compiledBindingExtension27.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView33, isVisibleProperty8, compiledBindingExtension28);
		context.PopParent();
		((ISupportInitialize)permissionView32).EndInit();
		global::Avalonia.Controls.Controls children15 = stackPanel9.Children;
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		children15.Add(textBlock14);
		TextBlock textBlock16 = (textBlock4 = textBlock15);
		context.PushParent(textBlock4);
		TextBlock textBlock17 = textBlock4;
		textBlock17.Name = "FilesHeader";
		obj2 = textBlock17;
		context.AvaloniaNameScope.Register("FilesHeader", obj2);
		textBlock17.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock17.Margin = new Thickness(0.0, 20.0, 0.0, 10.0);
		textBlock17.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Files;
		textBlock17.FontSize = 20.0;
		StyledProperty<IBrush?> foregroundProperty4 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock17, foregroundProperty4, binding6);
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj13 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock17, obj13);
		textBlock17.FontWeight = FontWeight.Medium;
		StyledProperty<bool> isVisibleProperty9 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension29 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.IsAppChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension30 = compiledBindingExtension29.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock17, isVisibleProperty9, compiledBindingExtension30);
		context.PopParent();
		((ISupportInitialize)textBlock16).EndInit();
		global::Avalonia.Controls.Controls children16 = stackPanel9.Children;
		PermissionView permissionView35;
		PermissionView permissionView34 = (permissionView35 = new PermissionView());
		((ISupportInitialize)permissionView34).BeginInit();
		children16.Add(permissionView34);
		PermissionView permissionView36 = (permissionView4 = permissionView35);
		context.PushParent(permissionView4);
		PermissionView permissionView37 = permissionView4;
		permissionView37.Name = "ViewFilesPermission";
		obj2 = permissionView37;
		context.AvaloniaNameScope.Register("ViewFilesPermission", obj2);
		permissionView37.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		permissionView37.Title = RootApp.Client.Avalonia.Resources.Strings.Resources.ViewFiles;
		permissionView37.Description = RootApp.Client.Avalonia.Resources.Strings.Resources.ViewFilesDescription;
		permissionView37.IsAdministrative = false;
		StyledProperty<bool?> isCheckedProperty9 = PermissionView.IsCheckedProperty;
		CompiledBindingExtension obj14 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.ViewFiles!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = PermissionView.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension31 = obj14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView37, isCheckedProperty9, compiledBindingExtension31);
		StyledProperty<bool> isVisibleProperty10 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension32 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.IsAppChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension33 = compiledBindingExtension32.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView37, isVisibleProperty10, compiledBindingExtension33);
		context.PopParent();
		((ISupportInitialize)permissionView36).EndInit();
		global::Avalonia.Controls.Controls children17 = stackPanel9.Children;
		PermissionView permissionView39;
		PermissionView permissionView38 = (permissionView39 = new PermissionView());
		((ISupportInitialize)permissionView38).BeginInit();
		children17.Add(permissionView38);
		PermissionView permissionView40 = (permissionView4 = permissionView39);
		context.PushParent(permissionView4);
		PermissionView permissionView41 = permissionView4;
		permissionView41.Name = "UploadFilesPermission";
		obj2 = permissionView41;
		context.AvaloniaNameScope.Register("UploadFilesPermission", obj2);
		permissionView41.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		permissionView41.Title = RootApp.Client.Avalonia.Resources.Strings.Resources.UploadFiles;
		permissionView41.Description = RootApp.Client.Avalonia.Resources.Strings.Resources.UploadFilesDescription;
		permissionView41.IsAdministrative = false;
		StyledProperty<bool?> isCheckedProperty10 = PermissionView.IsCheckedProperty;
		CompiledBindingExtension obj15 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.UploadFiles!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = PermissionView.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension34 = obj15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView41, isCheckedProperty10, compiledBindingExtension34);
		StyledProperty<bool> isVisibleProperty11 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension35 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.IsAppChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension36 = compiledBindingExtension35.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView41, isVisibleProperty11, compiledBindingExtension36);
		context.PopParent();
		((ISupportInitialize)permissionView40).EndInit();
		global::Avalonia.Controls.Controls children18 = stackPanel9.Children;
		PermissionView permissionView43;
		PermissionView permissionView42 = (permissionView43 = new PermissionView());
		((ISupportInitialize)permissionView42).BeginInit();
		children18.Add(permissionView42);
		PermissionView permissionView44 = (permissionView4 = permissionView43);
		context.PushParent(permissionView4);
		PermissionView permissionView45 = permissionView4;
		permissionView45.Name = "ManageFilesPermission";
		obj2 = permissionView45;
		context.AvaloniaNameScope.Register("ManageFilesPermission", obj2);
		permissionView45.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		permissionView45.Title = RootApp.Client.Avalonia.Resources.Strings.Resources.ManageFiles;
		permissionView45.Description = RootApp.Client.Avalonia.Resources.Strings.Resources.ManageFilesDescription;
		permissionView45.IsAdministrative = true;
		StyledProperty<bool?> isCheckedProperty11 = PermissionView.IsCheckedProperty;
		CompiledBindingExtension obj16 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.ManageFiles!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = PermissionView.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension37 = obj16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView45, isCheckedProperty11, compiledBindingExtension37);
		StyledProperty<bool> isVisibleProperty12 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension38 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.IsAppChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension39 = compiledBindingExtension38.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView45, isVisibleProperty12, compiledBindingExtension39);
		context.PopParent();
		((ISupportInitialize)permissionView44).EndInit();
		global::Avalonia.Controls.Controls children19 = stackPanel9.Children;
		TextBlock textBlock19;
		TextBlock textBlock18 = (textBlock19 = new TextBlock());
		((ISupportInitialize)textBlock18).BeginInit();
		children19.Add(textBlock18);
		TextBlock textBlock20 = (textBlock4 = textBlock19);
		context.PushParent(textBlock4);
		TextBlock textBlock21 = textBlock4;
		textBlock21.Name = "CallsHeader";
		obj2 = textBlock21;
		context.AvaloniaNameScope.Register("CallsHeader", obj2);
		textBlock21.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock21.Margin = new Thickness(0.0, 20.0, 0.0, 10.0);
		textBlock21.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.VoiceAndVideo;
		textBlock21.FontSize = 20.0;
		StyledProperty<IBrush?> foregroundProperty5 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock21, foregroundProperty5, binding7);
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj17 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock21, obj17);
		textBlock21.FontWeight = FontWeight.Medium;
		StyledProperty<bool> isVisibleProperty13 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension40 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.IsAppChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension41 = compiledBindingExtension40.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock21, isVisibleProperty13, compiledBindingExtension41);
		context.PopParent();
		((ISupportInitialize)textBlock20).EndInit();
		global::Avalonia.Controls.Controls children20 = stackPanel9.Children;
		PermissionView permissionView47;
		PermissionView permissionView46 = (permissionView47 = new PermissionView());
		((ISupportInitialize)permissionView46).BeginInit();
		children20.Add(permissionView46);
		PermissionView permissionView48 = (permissionView4 = permissionView47);
		context.PushParent(permissionView4);
		PermissionView permissionView49 = permissionView4;
		permissionView49.Name = "StreamAudioPermission";
		obj2 = permissionView49;
		context.AvaloniaNameScope.Register("StreamAudioPermission", obj2);
		permissionView49.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		permissionView49.Title = RootApp.Client.Avalonia.Resources.Strings.Resources.StreamAudio;
		permissionView49.Description = RootApp.Client.Avalonia.Resources.Strings.Resources.StreamAudioDescription;
		permissionView49.IsAdministrative = false;
		StyledProperty<bool?> isCheckedProperty12 = PermissionView.IsCheckedProperty;
		CompiledBindingExtension obj18 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.StreamAudio!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = PermissionView.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension42 = obj18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView49, isCheckedProperty12, compiledBindingExtension42);
		StyledProperty<bool> isVisibleProperty14 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension43 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.IsAppChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension44 = compiledBindingExtension43.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView49, isVisibleProperty14, compiledBindingExtension44);
		context.PopParent();
		((ISupportInitialize)permissionView48).EndInit();
		global::Avalonia.Controls.Controls children21 = stackPanel9.Children;
		PermissionView permissionView51;
		PermissionView permissionView50 = (permissionView51 = new PermissionView());
		((ISupportInitialize)permissionView50).BeginInit();
		children21.Add(permissionView50);
		PermissionView permissionView52 = (permissionView4 = permissionView51);
		context.PushParent(permissionView4);
		PermissionView permissionView53 = permissionView4;
		permissionView53.Name = "StreamVideoPermission";
		obj2 = permissionView53;
		context.AvaloniaNameScope.Register("StreamVideoPermission", obj2);
		permissionView53.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		permissionView53.Title = RootApp.Client.Avalonia.Resources.Strings.Resources.StreamVideo;
		permissionView53.Description = RootApp.Client.Avalonia.Resources.Strings.Resources.StreamVideoDescription;
		permissionView53.IsAdministrative = false;
		StyledProperty<bool?> isCheckedProperty13 = PermissionView.IsCheckedProperty;
		CompiledBindingExtension obj19 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.StreamVideo!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = PermissionView.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension45 = obj19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView53, isCheckedProperty13, compiledBindingExtension45);
		StyledProperty<bool> isVisibleProperty15 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension46 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.IsAppChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension47 = compiledBindingExtension46.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView53, isVisibleProperty15, compiledBindingExtension47);
		context.PopParent();
		((ISupportInitialize)permissionView52).EndInit();
		global::Avalonia.Controls.Controls children22 = stackPanel9.Children;
		PermissionView permissionView55;
		PermissionView permissionView54 = (permissionView55 = new PermissionView());
		((ISupportInitialize)permissionView54).BeginInit();
		children22.Add(permissionView54);
		PermissionView permissionView56 = (permissionView4 = permissionView55);
		context.PushParent(permissionView4);
		PermissionView permissionView57 = permissionView4;
		permissionView57.Name = "MuteMicrophonesPermission";
		obj2 = permissionView57;
		context.AvaloniaNameScope.Register("MuteMicrophonesPermission", obj2);
		permissionView57.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		permissionView57.Title = RootApp.Client.Avalonia.Resources.Strings.Resources.MuteMicrophones;
		permissionView57.Description = RootApp.Client.Avalonia.Resources.Strings.Resources.MuteMicrophonesDescription;
		permissionView57.IsAdministrative = true;
		StyledProperty<bool?> isCheckedProperty14 = PermissionView.IsCheckedProperty;
		CompiledBindingExtension obj20 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.MuteMicrophones!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = PermissionView.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension48 = obj20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView57, isCheckedProperty14, compiledBindingExtension48);
		StyledProperty<bool> isVisibleProperty16 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension49 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.IsAppChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension50 = compiledBindingExtension49.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView57, isVisibleProperty16, compiledBindingExtension50);
		context.PopParent();
		((ISupportInitialize)permissionView56).EndInit();
		global::Avalonia.Controls.Controls children23 = stackPanel9.Children;
		PermissionView permissionView59;
		PermissionView permissionView58 = (permissionView59 = new PermissionView());
		((ISupportInitialize)permissionView58).BeginInit();
		children23.Add(permissionView58);
		PermissionView permissionView60 = (permissionView4 = permissionView59);
		context.PushParent(permissionView4);
		PermissionView permissionView61 = permissionView4;
		permissionView61.Name = "MuteSpeakersPermission";
		obj2 = permissionView61;
		context.AvaloniaNameScope.Register("MuteSpeakersPermission", obj2);
		permissionView61.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		permissionView61.Title = RootApp.Client.Avalonia.Resources.Strings.Resources.MuteSpeakers;
		permissionView61.Description = RootApp.Client.Avalonia.Resources.Strings.Resources.MuteSpeakersDescription;
		permissionView61.IsAdministrative = true;
		StyledProperty<bool?> isCheckedProperty15 = PermissionView.IsCheckedProperty;
		CompiledBindingExtension obj21 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.MuteSpeakers!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = PermissionView.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension51 = obj21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView61, isCheckedProperty15, compiledBindingExtension51);
		StyledProperty<bool> isVisibleProperty17 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension52 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.IsAppChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension53 = compiledBindingExtension52.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView61, isVisibleProperty17, compiledBindingExtension53);
		context.PopParent();
		((ISupportInitialize)permissionView60).EndInit();
		global::Avalonia.Controls.Controls children24 = stackPanel9.Children;
		PermissionView permissionView63;
		PermissionView permissionView62 = (permissionView63 = new PermissionView());
		((ISupportInitialize)permissionView62).BeginInit();
		children24.Add(permissionView62);
		PermissionView permissionView64 = (permissionView4 = permissionView63);
		context.PushParent(permissionView4);
		PermissionView permissionView65 = permissionView4;
		permissionView65.Name = "KickVoiceMemberPermission";
		obj2 = permissionView65;
		context.AvaloniaNameScope.Register("KickVoiceMemberPermission", obj2);
		permissionView65.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		permissionView65.Title = RootApp.Client.Avalonia.Resources.Strings.Resources.KickVoiceMember;
		permissionView65.Description = RootApp.Client.Avalonia.Resources.Strings.Resources.KickVoiceMemberDescription;
		permissionView65.IsAdministrative = true;
		StyledProperty<bool?> isCheckedProperty16 = PermissionView.IsCheckedProperty;
		CompiledBindingExtension obj22 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.KickVoiceMember!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = PermissionView.IsCheckedProperty;
		CompiledBindingExtension compiledBindingExtension54 = obj22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView65, isCheckedProperty16, compiledBindingExtension54);
		StyledProperty<bool> isVisibleProperty18 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension55 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.Permissions.AccessRuleViewModel,RootApp.Client.Avalonia.IsAppChannel!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension56 = compiledBindingExtension55.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(permissionView65, isVisibleProperty18, compiledBindingExtension56);
		context.PopParent();
		((ISupportInitialize)permissionView64).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		context.PopParent();
		((ISupportInitialize)rootScrollViewer3).EndInit();
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
	private static void !XamlIlPopulateTrampoline(AccessRuleView P_0)
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
