// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.MemberInviteView
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
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Community.Members;

public class MemberInviteView : UserControl
{
	[CompilerGenerated]
	private static Action<object?>? _0021XamlIlPopulateOverride;

	private MemberInviteViewModel? _memberInviteViewModel => base.DataContext as MemberInviteViewModel;

	public MemberInviteView()
	{
		InitializeComponent();
	}

	private void onFlyoutOpening(object? sender, EventArgs e)
	{
		if (_memberInviteViewModel != null && _memberInviteViewModel.RoleSelectorFlyoutOpeningCommand.CanExecute(null))
		{
			_memberInviteViewModel.RoleSelectorFlyoutOpeningCommand.Execute(null);
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	[ExcludeFromCodeCoverage]
	public void InitializeComponent(bool P_0 = true)
	{
		if (P_0)
		{
			_0021XamlIlPopulateTrampoline(this);
		}
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulate(IServiceProvider? P_0, MemberInviteView? P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<MemberInviteView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MemberInviteView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FCommunity_002FMembers_002FMemberInviteView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Members/MemberInviteView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		P_1.Content = stackPanel;
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		stackPanel5.Orientation = Orientation.Vertical;
		stackPanel5.Margin = new Thickness(0.0, 0.0, 0.0, 10.0);
		Controls children = stackPanel5.Children;
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		children.Add(grid);
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		Controls children2 = grid4.Children;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		children2.Add(stackPanel6);
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		stackPanel9.Orientation = Orientation.Horizontal;
		Controls children3 = stackPanel9.Children;
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		children3.Add(panel);
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		Panel panel5 = panel4;
		panel5.VerticalAlignment = VerticalAlignment.Center;
		panel5.HorizontalAlignment = HorizontalAlignment.Left;
		Controls children4 = panel5.Children;
		RootImageLoader rootImageLoader2;
		RootImageLoader rootImageLoader = (rootImageLoader2 = new RootImageLoader());
		((ISupportInitialize)rootImageLoader).BeginInit();
		children4.Add(rootImageLoader);
		RootImageLoader rootImageLoader4;
		RootImageLoader rootImageLoader3 = (rootImageLoader4 = rootImageLoader2);
		context.PushParent(rootImageLoader4);
		rootImageLoader4.Width = 35.0;
		rootImageLoader4.Height = 35.0;
		rootImageLoader4.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		StyledProperty<IBrush?> backgroundProperty = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, backgroundProperty, binding);
		StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberInviteViewModel_002CRootApp_002EClient_002EAvalonia_002EProfilePictureAsyncBitmapWrapper_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
		context.ProvideTargetProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, sourceProperty, compiledBindingExtension2);
		rootImageLoader4.LoadingPlaceholderSize = 16.0;
		rootImageLoader4.VerticalAlignment = VerticalAlignment.Center;
		rootImageLoader4.Stretch = Stretch.UniformToFill;
		context.PopParent();
		((ISupportInitialize)rootImageLoader3).EndInit();
		context.PopParent();
		((ISupportInitialize)panel3).EndInit();
		Controls children5 = stackPanel9.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children5.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Margin = new Thickness(10.0, 0.0, 10.0, 0.0);
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberInviteViewModel_002CRootApp_002EClient_002EAvalonia_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EUserName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, compiledBindingExtension4);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj);
		textBlock5.FontSize = 14.0;
		textBlock5.FontWeight = (FontWeight)450;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding2);
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock5.TextTrimming = TextTrimming.CharacterEllipsis;
		textBlock5.HorizontalAlignment = HorizontalAlignment.Left;
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		Controls children6 = grid4.Children;
		StackPanel stackPanel11;
		StackPanel stackPanel10 = (stackPanel11 = new StackPanel());
		((ISupportInitialize)stackPanel10).BeginInit();
		children6.Add(stackPanel10);
		StackPanel stackPanel12 = (stackPanel4 = stackPanel11);
		context.PushParent(stackPanel4);
		StackPanel stackPanel13 = stackPanel4;
		stackPanel13.Orientation = Orientation.Horizontal;
		stackPanel13.HorizontalAlignment = HorizontalAlignment.Right;
		stackPanel13.VerticalAlignment = VerticalAlignment.Center;
		stackPanel13.Height = 25.0;
		Grid.SetColumn(stackPanel13, 1);
		Controls children7 = stackPanel13.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children7.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		Button button5 = button4;
		button5.Classes.Add("BorderButton");
		button5.MinWidth = 75.0;
		button5.Margin = new Thickness(0.0, 0.0, 5.0, 0.0);
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj2 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(button5, obj2);
		button5.FontSize = 14.0;
		button5.FontWeight = (FontWeight)450;
		button5.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		button5.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button5.HorizontalAlignment = HorizontalAlignment.Right;
		StyledProperty<IBrush?> foregroundProperty2 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, foregroundProperty2, binding3);
		StyledProperty<IBrush?> backgroundProperty2 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, backgroundProperty2, binding4);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		MultiBinding multiBinding2;
		MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding3 = multiBinding2;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("MultiBoolInverterConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj3 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding3.Converter = (IMultiValueConverter)obj3;
		IList<IBinding> bindings = multiBinding3.Bindings;
		CompiledBindingExtension obj4 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberInviteViewModel_002CRootApp_002EClient_002EAvalonia_002EHasBeenInvited_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item = obj4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings.Add(item);
		IList<IBinding> bindings2 = multiBinding3.Bindings;
		CompiledBindingExtension obj5 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberInviteViewModel_002CRootApp_002EClient_002EAvalonia_002EIsBannedMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item2 = obj5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings2.Add(item2);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(button5, isVisibleProperty, multiBinding);
		StackPanel stackPanel15;
		StackPanel stackPanel14 = (stackPanel15 = new StackPanel());
		((ISupportInitialize)stackPanel14).BeginInit();
		button5.Content = stackPanel14;
		StackPanel stackPanel16 = (stackPanel4 = stackPanel15);
		context.PushParent(stackPanel4);
		StackPanel stackPanel17 = stackPanel4;
		stackPanel17.Orientation = Orientation.Horizontal;
		Controls children8 = stackPanel17.Children;
		StackPanel stackPanel19;
		StackPanel stackPanel18 = (stackPanel19 = new StackPanel());
		((ISupportInitialize)stackPanel18).BeginInit();
		children8.Add(stackPanel18);
		StackPanel stackPanel20 = (stackPanel4 = stackPanel19);
		context.PushParent(stackPanel4);
		StackPanel stackPanel21 = stackPanel4;
		stackPanel21.Orientation = Orientation.Horizontal;
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension6;
		CompiledBindingExtension compiledBindingExtension5 = (compiledBindingExtension6 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberInviteViewModel_002CRootApp_002EClient_002EAvalonia_002ENumRoles_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension6);
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("GreaterThanZeroToTrueConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj6 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension6.Converter = (IValueConverter)obj6;
		context.PopParent();
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension7 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(stackPanel21, isVisibleProperty2, compiledBindingExtension7);
		Controls children9 = stackPanel21.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children9.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		StyledProperty<string?> textProperty2 = TextBlock.TextProperty;
		CompiledBindingExtension obj7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberInviteViewModel_002CRootApp_002EClient_002EAvalonia_002ENumRoles_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			StringFormat = "+{0}"
		};
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension8 = obj7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, textProperty2, compiledBindingExtension8);
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj8 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj8);
		textBlock9.FontSize = 14.0;
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty3, binding5);
		textBlock9.Margin = new Thickness(8.0, 0.0, 0.0, 0.0);
		textBlock9.FontWeight = (FontWeight)450;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		Controls children10 = stackPanel21.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children10.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		border4.Width = 1.0;
		border4.VerticalAlignment = VerticalAlignment.Stretch;
		StyledProperty<IBrush?> backgroundProperty3 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border4, backgroundProperty3, binding6);
		border4.Opacity = 0.1;
		border4.Margin = new Thickness(6.0, 0.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel20).EndInit();
		Controls children11 = stackPanel17.Children;
		Panel panel7;
		Panel panel6 = (panel7 = new Panel());
		((ISupportInitialize)panel6).BeginInit();
		children11.Add(panel6);
		Panel panel8 = (panel4 = panel7);
		context.PushParent(panel4);
		Panel panel9 = panel4;
		panel9.Width = 75.0;
		Controls children12 = panel9.Children;
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		children12.Add(textBlock10);
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		textBlock13.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.AddRole;
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj9 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock13, obj9);
		textBlock13.HorizontalAlignment = HorizontalAlignment.Center;
		textBlock13.FontSize = 14.0;
		textBlock13.Margin = new Thickness(6.0, 0.0, 6.0, 0.0);
		textBlock13.FontWeight = (FontWeight)450;
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		context.PopParent();
		((ISupportInitialize)panel8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel16).EndInit();
		RootFlyout rootFlyout;
		RootFlyout flyout = (rootFlyout = new RootFlyout());
		context.PushParent(rootFlyout);
		rootFlyout.Placement = PlacementMode.Pointer;
		rootFlyout.VerticalOffset = -12.0;
		rootFlyout.HorizontalOffset = -8.0;
		rootFlyout.Opening += context.RootObject.onFlyoutOpening;
		ContentControl contentControl2;
		ContentControl contentControl = (contentControl2 = new ContentControl());
		((ISupportInitialize)contentControl).BeginInit();
		rootFlyout.Content = contentControl;
		ContentControl contentControl4;
		ContentControl contentControl3 = (contentControl4 = contentControl2);
		context.PushParent(contentControl4);
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberInviteViewModel_002CRootApp_002EClient_002EAvalonia_002ERoleSelectorViewModel_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ContentControl.ContentProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_4(contentControl4, compiledBindingExtension10);
		context.PopParent();
		((ISupportInitialize)contentControl3).EndInit();
		context.PopParent();
		button5.Flyout = flyout;
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		Controls children13 = stackPanel13.Children;
		Panel panel11;
		Panel panel10 = (panel11 = new Panel());
		((ISupportInitialize)panel10).BeginInit();
		children13.Add(panel10);
		Panel panel12 = (panel4 = panel11);
		context.PushParent(panel4);
		Panel panel13 = panel4;
		Controls children14 = panel13.Children;
		Button button7;
		Button button6 = (button7 = new Button());
		((ISupportInitialize)button6).BeginInit();
		children14.Add(button6);
		Button button8 = (button4 = button7);
		context.PushParent(button4);
		Button button9 = button4;
		button9.Classes.Add("BorderButton");
		button9.MinWidth = 65.0;
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj10 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(button9, obj10);
		button9.Width = 22.0;
		button9.FontSize = 14.0;
		button9.FontWeight = (FontWeight)450;
		button9.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		button9.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button9.HorizontalAlignment = HorizontalAlignment.Right;
		button9.Content = RootApp.Client.Avalonia.Resources.Strings.Resources.Invite;
		StyledProperty<IBrush?> backgroundProperty4 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("BrandPrimary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, backgroundProperty4, binding7);
		StyledProperty<IBrush?> foregroundProperty4 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("TextWhite");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, foregroundProperty4, binding8);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberInviteViewModel_002CRootApp_002EClient_002EAvalonia_002ESendCommunityMemberInviteCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, commandProperty, compiledBindingExtension12);
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		MultiBinding multiBinding4 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding5 = multiBinding2;
		StaticResourceExtension staticResourceExtension8 = new StaticResourceExtension("MultiBoolInverterConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj11 = staticResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding5.Converter = (IMultiValueConverter)obj11;
		IList<IBinding> bindings3 = multiBinding5.Bindings;
		CompiledBindingExtension obj12 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberInviteViewModel_002CRootApp_002EClient_002EAvalonia_002EHasBeenInvited_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item3 = obj12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings3.Add(item3);
		IList<IBinding> bindings4 = multiBinding5.Bindings;
		CompiledBindingExtension obj13 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberInviteViewModel_002CRootApp_002EClient_002EAvalonia_002EIsBannedMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item4 = obj13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings4.Add(item4);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(button9, isVisibleProperty3, multiBinding4);
		context.PopParent();
		((ISupportInitialize)button8).EndInit();
		Controls children15 = panel13.Children;
		Button button11;
		Button button10 = (button11 = new Button());
		((ISupportInitialize)button10).BeginInit();
		children15.Add(button10);
		Button button12 = (button4 = button11);
		context.PushParent(button4);
		Button button13 = button4;
		button13.Classes.Add("BorderButton");
		StaticResourceExtension staticResourceExtension9 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj14 = staticResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(button13, obj14);
		button13.FontSize = 14.0;
		button13.FontWeight = (FontWeight)450;
		button13.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		button13.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button13.HorizontalContentAlignment = HorizontalAlignment.Right;
		button13.HorizontalAlignment = HorizontalAlignment.Right;
		button13.IsEnabled = false;
		button13.Content = RootApp.Client.Avalonia.Resources.Strings.Resources.Sent;
		StyledProperty<IBrush?> foregroundProperty5 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button13, foregroundProperty5, binding9);
		button13.Background = new ImmutableSolidColorBrush(16777215u);
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberInviteViewModel_002CRootApp_002EClient_002EAvalonia_002EHasBeenInvited_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button13, isVisibleProperty4, compiledBindingExtension14);
		context.PopParent();
		((ISupportInitialize)button12).EndInit();
		Controls children16 = panel13.Children;
		Button button15;
		Button button14 = (button15 = new Button());
		((ISupportInitialize)button14).BeginInit();
		children16.Add(button14);
		Button button16 = (button4 = button15);
		context.PushParent(button4);
		Button button17 = button4;
		button17.Classes.Add("BorderButton");
		StaticResourceExtension staticResourceExtension10 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj15 = staticResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(button17, obj15);
		button17.FontSize = 14.0;
		button17.FontWeight = (FontWeight)450;
		button17.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		button17.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button17.HorizontalContentAlignment = HorizontalAlignment.Right;
		button17.HorizontalAlignment = HorizontalAlignment.Right;
		button17.IsEnabled = false;
		button17.Content = RootApp.Client.Avalonia.Resources.Strings.Resources.CanNotInviteBannedUser;
		StyledProperty<IBrush?> foregroundProperty6 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button17, foregroundProperty6, binding10);
		button17.Background = new ImmutableSolidColorBrush(16777215u);
		StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension15 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberInviteViewModel_002CRootApp_002EClient_002EAvalonia_002EIsBannedMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension16 = compiledBindingExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button17, isVisibleProperty5, compiledBindingExtension16);
		context.PopParent();
		((ISupportInitialize)button16).EndInit();
		context.PopParent();
		((ISupportInitialize)panel12).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel12).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(MemberInviteView? P_0)
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

