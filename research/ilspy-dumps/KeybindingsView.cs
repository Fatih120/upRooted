// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings.KeybindingsView
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Controls.Keybinds;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.Settings;

public class KeybindingsView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_165
	{
		public unsafe static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<KeybindingsView> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			StackPanel stackPanel = (StackPanel)obj;
			context.PushParent(stackPanel);
			stackPanel.Margin = new Thickness(0.0, 0.0, 0.0, 28.0);
			Controls children = stackPanel.Children;
			TextBlock textBlock2;
			TextBlock textBlock = (textBlock2 = new TextBlock());
			((ISupportInitialize)textBlock).BeginInit();
			children.Add(textBlock);
			TextBlock textBlock4;
			TextBlock textBlock3 = (textBlock4 = textBlock2);
			context.PushParent(textBlock4);
			StyledProperty<string?> textProperty = TextBlock.TextProperty;
			CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002ESettings_002EKeybindingCategoryViewModel_002CRootApp_002EClient_002EAvalonia_002ECategoryName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
			context.ProvideTargetProperty = TextBlock.TextProperty;
			CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(textBlock4, textProperty, compiledBindingExtension2);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
			context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
			object? obj2 = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock4, obj2);
			textBlock4.FontWeight = FontWeight.Medium;
			textBlock4.FontSize = 20.0;
			textBlock4.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
			textBlock4.Margin = new Thickness(0.0, 0.0, 0.0, 28.0);
			textBlock4.TextWrapping = TextWrapping.Wrap;
			StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
			DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
			context.ProvideTargetProperty = TextBlock.ForegroundProperty;
			IBinding binding = dynamicResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(textBlock4, foregroundProperty, binding);
			context.PopParent();
			((ISupportInitialize)textBlock3).EndInit();
			Controls children2 = stackPanel.Children;
			RootBorder rootBorder2;
			RootBorder rootBorder = (rootBorder2 = new RootBorder());
			((ISupportInitialize)rootBorder).BeginInit();
			children2.Add(rootBorder);
			RootBorder rootBorder4;
			RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
			context.PushParent(rootBorder4);
			StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
			DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("BackgroundSecondary");
			context.ProvideTargetProperty = Border.BackgroundProperty;
			IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(rootBorder4, backgroundProperty, binding2);
			StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
			DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("Border");
			context.ProvideTargetProperty = Border.BorderBrushProperty;
			IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(rootBorder4, borderBrushProperty, binding3);
			rootBorder4.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
			rootBorder4.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
			rootBorder4.Padding = new Thickness(24.0, 24.0, 24.0, 24.0);
			ItemsControl itemsControl2;
			ItemsControl itemsControl = (itemsControl2 = new ItemsControl());
			((ISupportInitialize)itemsControl).BeginInit();
			rootBorder4.Child = itemsControl;
			ItemsControl itemsControl4;
			ItemsControl itemsControl3 = (itemsControl4 = itemsControl2);
			context.PushParent(itemsControl4);
			StyledProperty<IEnumerable?> itemsSourceProperty = ItemsControl.ItemsSourceProperty;
			CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002ESettings_002EKeybindingCategoryViewModel_002CRootApp_002EClient_002EAvalonia_002EItems_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
			context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
			CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(itemsControl4, itemsSourceProperty, compiledBindingExtension4);
			itemsControl4.ItemsPanel = new ItemsPanelTemplate
			{
				Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&Build_2), context)
			};
			DataTemplate dataTemplate;
			DataTemplate itemTemplate = (dataTemplate = new DataTemplate());
			context.PushParent(dataTemplate);
			dataTemplate.DataType = typeof(KeybindingItemViewModel);
			dataTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&Build_3), context);
			context.PopParent();
			itemsControl4.ItemTemplate = itemTemplate;
			context.PopParent();
			((ISupportInitialize)itemsControl3).EndInit();
			context.PopParent();
			((ISupportInitialize)rootBorder3).EndInit();
			context.PopParent();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<KeybindingsView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<KeybindingsView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<KeybindingsView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FHome_002FSystemTray_002FProfile_002FSettings_002FKeybindingsView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Home/SystemTray/Profile/Settings/KeybindingsView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (KeybindingsView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<KeybindingsView> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((AvaloniaObject)obj).SetValue(StackPanel.SpacingProperty, 16.0, BindingPriority.Template);
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public unsafe static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<KeybindingsView> context = CreateContext(P_0);
			context.IntermediateRoot = new Grid();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			Grid grid = (Grid)obj;
			context.PushParent(grid);
			grid.ColumnDefinitions.Add(new ColumnDefinition
			{
				Width = new GridLength(1.0, GridUnitType.Star)
			});
			grid.ColumnDefinitions.Add(new ColumnDefinition
			{
				Width = new GridLength(0.0, GridUnitType.Auto)
			});
			Controls children = grid.Children;
			StackPanel stackPanel2;
			StackPanel stackPanel = (stackPanel2 = new StackPanel());
			((ISupportInitialize)stackPanel).BeginInit();
			children.Add(stackPanel);
			StackPanel stackPanel4;
			StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
			context.PushParent(stackPanel4);
			Grid.SetColumn(stackPanel4, 0);
			stackPanel4.VerticalAlignment = VerticalAlignment.Center;
			stackPanel4.Margin = new Thickness(0.0, 0.0, 16.0, 0.0);
			Controls children2 = stackPanel4.Children;
			TextBlock textBlock2;
			TextBlock textBlock = (textBlock2 = new TextBlock());
			((ISupportInitialize)textBlock).BeginInit();
			children2.Add(textBlock);
			TextBlock textBlock4;
			TextBlock textBlock3 = (textBlock4 = textBlock2);
			context.PushParent(textBlock4);
			TextBlock textBlock5 = textBlock4;
			StyledProperty<string?> textProperty = TextBlock.TextProperty;
			CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002ESettings_002EKeybindingItemViewModel_002CRootApp_002EClient_002EAvalonia_002EDisplayName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
			context.ProvideTargetProperty = TextBlock.TextProperty;
			CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(textBlock5, textProperty, compiledBindingExtension2);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
			context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
			object? obj2 = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj2);
			textBlock5.FontWeight = FontWeight.Bold;
			textBlock5.FontSize = 14.0;
			textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
			textBlock5.TextWrapping = TextWrapping.Wrap;
			StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
			DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
			context.ProvideTargetProperty = TextBlock.ForegroundProperty;
			IBinding binding = dynamicResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding);
			context.PopParent();
			((ISupportInitialize)textBlock3).EndInit();
			Controls children3 = stackPanel4.Children;
			TextBlock textBlock7;
			TextBlock textBlock6 = (textBlock7 = new TextBlock());
			((ISupportInitialize)textBlock6).BeginInit();
			children3.Add(textBlock6);
			TextBlock textBlock8 = (textBlock4 = textBlock7);
			context.PushParent(textBlock4);
			TextBlock textBlock9 = textBlock4;
			StyledProperty<string?> textProperty2 = TextBlock.TextProperty;
			CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002ESettings_002EKeybindingItemViewModel_002CRootApp_002EClient_002EAvalonia_002EDescription_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
			context.ProvideTargetProperty = TextBlock.TextProperty;
			CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(textBlock9, textProperty2, compiledBindingExtension4);
			StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
			context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
			object? obj3 = staticResourceExtension2.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj3);
			textBlock9.FontWeight = (FontWeight)450;
			textBlock9.FontSize = 14.0;
			textBlock9.LineHeight = 20.0;
			textBlock9.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
			textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
			StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
			DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("TextSecondary");
			context.ProvideTargetProperty = TextBlock.ForegroundProperty;
			IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding2);
			textBlock9.TextWrapping = TextWrapping.Wrap;
			context.PopParent();
			((ISupportInitialize)textBlock8).EndInit();
			context.PopParent();
			((ISupportInitialize)stackPanel3).EndInit();
			Controls children4 = grid.Children;
			ContentControl contentControl2;
			ContentControl contentControl = (contentControl2 = new ContentControl());
			((ISupportInitialize)contentControl).BeginInit();
			children4.Add(contentControl);
			ContentControl contentControl4;
			ContentControl contentControl3 = (contentControl4 = contentControl2);
			context.PushParent(contentControl4);
			Grid.SetColumn(contentControl4, 1);
			CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002ESettings_002EKeybindingItemViewModel_002CRootApp_002EClient_002EAvalonia_002EKeybindViewModel_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
			context.ProvideTargetProperty = ContentControl.ContentProperty;
			CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_4(contentControl4, compiledBindingExtension6);
			contentControl4.VerticalAlignment = VerticalAlignment.Center;
			contentControl4.Width = 280.0;
			DataTemplate dataTemplate = new DataTemplate();
			dataTemplate.DataType = typeof(KeybindEditorViewModel);
			dataTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&Build_4), context);
			contentControl4.ContentTemplate = dataTemplate;
			context.PopParent();
			((ISupportInitialize)contentControl3).EndInit();
			context.PopParent();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static object Build_4(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<KeybindingsView> context = CreateContext(P_0);
			context.IntermediateRoot = new KeybindEditorView();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}
	}

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public KeybindingsView()
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
	private unsafe static void _0021XamlIlPopulate(IServiceProvider P_0, KeybindingsView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<KeybindingsView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<KeybindingsView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FHome_002FSystemTray_002FProfile_002FSettings_002FKeybindingsView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Home/SystemTray/Profile/Settings/KeybindingsView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
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
		Controls children = stackPanel5.Children;
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		children.Add(rootBorder);
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
		rootBorder4.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		rootBorder4.Padding = new Thickness(24.0, 24.0, 24.0, 24.0);
		rootBorder4.Margin = new Thickness(0.0, 0.0, 0.0, 28.0);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002ESettings_002EKeybindingsViewModel_002CRootApp_002EClient_002EAvalonia_002EAnyModified_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, isVisibleProperty, compiledBindingExtension2);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		rootBorder4.Child = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		ColumnDefinitions columnDefinitions = grid4.ColumnDefinitions;
		ColumnDefinition columnDefinition = new ColumnDefinition();
		columnDefinition.MinWidth = 104.0;
		columnDefinition.Width = new GridLength(0.0, GridUnitType.Auto);
		columnDefinitions.Add(columnDefinition);
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
		Controls children3 = stackPanel9.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children3.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock5.FontSize = 14.0;
		textBlock5.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ResetAllKeybindings;
		textBlock5.TextWrapping = TextWrapping.Wrap;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding3);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj);
		textBlock5.FontWeight = FontWeight.Bold;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		Controls children4 = stackPanel9.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children4.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Margin = new Thickness(0.0, 8.0, 0.0, 0.0);
		textBlock9.LineHeight = 20.0;
		textBlock9.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ResetAllKeybindingsDescription;
		textBlock9.TextWrapping = TextWrapping.Wrap;
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding4);
		textBlock9.FontSize = 14.0;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj2);
		textBlock9.FontWeight = (FontWeight)450;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		Controls children5 = grid4.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children5.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		Grid.SetColumn(button4, 2);
		button4.Classes.Add("BorderButton");
		button4.Padding = new Thickness(20.0, 8.0, 20.0, 8.0);
		button4.Content = RootApp.Client.Avalonia.Resources.Strings.Resources.ResetAll;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj3 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_3(button4, obj3);
		button4.FontSize = 14.0;
		button4.FontWeight = FontWeight.Medium;
		button4.CornerRadius = new CornerRadius(20.0, 20.0, 20.0, 20.0);
		button4.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button4.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<IBrush?> foregroundProperty3 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, foregroundProperty3, binding5);
		StyledProperty<IBrush?> backgroundProperty2 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, backgroundProperty2, binding6);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002ESettings_002EKeybindingsViewModel_002CRootApp_002EClient_002EAvalonia_002EResetAllToDefaultsCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, commandProperty, compiledBindingExtension4);
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		Controls children6 = stackPanel5.Children;
		ItemsControl itemsControl2;
		ItemsControl itemsControl = (itemsControl2 = new ItemsControl());
		((ISupportInitialize)itemsControl).BeginInit();
		children6.Add(itemsControl);
		ItemsControl itemsControl4;
		ItemsControl itemsControl3 = (itemsControl4 = itemsControl2);
		context.PushParent(itemsControl4);
		StyledProperty<IEnumerable?> itemsSourceProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002ESettings_002EKeybindingsViewModel_002CRootApp_002EClient_002EAvalonia_002ECategories_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl4, itemsSourceProperty, compiledBindingExtension6);
		DataTemplate dataTemplate;
		DataTemplate itemTemplate = (dataTemplate = new DataTemplate());
		context.PushParent(dataTemplate);
		dataTemplate.DataType = typeof(KeybindingCategoryViewModel);
		dataTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_165.Build_1), context);
		context.PopParent();
		itemsControl4.ItemTemplate = itemTemplate;
		context.PopParent();
		((ISupportInitialize)itemsControl3).EndInit();
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
	private static void _0021XamlIlPopulateTrampoline(KeybindingsView P_0)
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

