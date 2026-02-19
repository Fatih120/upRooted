// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.MemberGroupView
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
using RootApp.Client.Avalonia.UI.Community.Members;

public class MemberGroupView : UserControl
{
	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public MemberGroupView()
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
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulate(IServiceProvider P_0, MemberGroupView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<MemberGroupView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MemberGroupView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FCommunity_002FMembers_002FMemberGroupView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Members/MemberGroupView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberGroupViewModel_002CRootApp_002EClient_002EAvalonia_002EShouldRender_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(P_1, isVisibleProperty, compiledBindingExtension2);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		P_1.Content = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		grid4.Margin = new Thickness(8.0, 9.0, 12.0, 9.0);
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(8.0, GridUnitType.Pixel)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		Controls children = grid4.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberGroupViewModel_002CRootApp_002EClient_002EAvalonia_002EMemberGroup_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002EMemberGroup_002CRootApp_002EClient_002ECoreDomain_002EGroupName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, compiledBindingExtension4);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj);
		textBlock5.FontWeight = (FontWeight)450;
		textBlock5.FontSize = 14.0;
		textBlock5.HorizontalAlignment = HorizontalAlignment.Left;
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding);
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		Controls children2 = grid4.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children2.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Grid.SetColumn(border4, 2);
		border4.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("HighlightStrong");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border4, backgroundProperty, binding2);
		border4.HorizontalAlignment = HorizontalAlignment.Right;
		border4.VerticalAlignment = VerticalAlignment.Center;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		border4.Child = textBlock6;
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		StyledProperty<string?> textProperty2 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMemberGroupViewModel_002CRootApp_002EClient_002EAvalonia_002EMemberGroup_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002EMemberGroup_002CRootApp_002EClient_002ECoreDomain_002EVisibleMemberCount_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, textProperty2, compiledBindingExtension6);
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj2);
		textBlock9.FontWeight = FontWeight.Bold;
		textBlock9.FontSize = 12.0;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding3);
		textBlock9.HorizontalAlignment = HorizontalAlignment.Center;
		textBlock9.VerticalAlignment = VerticalAlignment.Center;
		textBlock9.Padding = new Thickness(8.0, 0.0, 8.0, 0.0);
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
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
	private static void _0021XamlIlPopulateTrampoline(MemberGroupView P_0)
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

