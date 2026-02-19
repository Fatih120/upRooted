// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.NewTabView
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Immutable;
using Avalonia.VisualTree;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home;
using Tabalonia.Controls;

public class NewTabView : UserControl
{
	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	private NewTabViewModel? _newTabViewModel => base.DataContext as NewTabViewModel;

	public NewTabView()
	{
		InitializeComponent();
	}

	protected override void OnLoaded(RoutedEventArgs P_0)
	{
		base.OnLoaded(P_0);
		DragTabItem dragTabItem = this.FindAncestorOfType<DragTabItem>();
		if (dragTabItem != null)
		{
			dragTabItem.PointerPressed += onDragItemPointerPressed;
		}
	}

	protected override void OnUnloaded(RoutedEventArgs P_0)
	{
		base.OnUnloaded(P_0);
		DragTabItem dragTabItem = this.FindAncestorOfType<DragTabItem>();
		if (dragTabItem != null)
		{
			dragTabItem.PointerPressed -= onDragItemPointerPressed;
		}
	}

	private void onDragItemPointerPressed(object? sender, PointerPressedEventArgs e)
	{
		if (_newTabViewModel != null && e.GetCurrentPoint(this).Properties.IsMiddleButtonPressed && _newTabViewModel.CloseTabCommand.CanExecute(null))
		{
			_newTabViewModel.CloseTabCommand.Execute(null);
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
	private static void _0021XamlIlPopulate(IServiceProvider P_0, NewTabView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<NewTabView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<NewTabView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FHome_002FNewTabView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Home/NewTabView.axaml")
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
		grid4.Margin = new Thickness(6.0, 0.0, 6.0, 0.0);
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(6.0, GridUnitType.Pixel)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		Controls children = grid4.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children.Add(border);
		border2.Width = 28.0;
		border2.Height = 28.0;
		border2.Background = new ImmutableSolidColorBrush(4286611584u);
		border2.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		((ISupportInitialize)border2).EndInit();
		Controls children2 = grid4.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children2.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		Grid.SetColumn(textBlock4, 2);
		textBlock4.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.NewTab;
		textBlock4.VerticalAlignment = VerticalAlignment.Center;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock4, obj);
		textBlock4.FontWeight = FontWeight.Medium;
		textBlock4.FontSize = 13.0;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock4, foregroundProperty, binding);
		textBlock4.TextTrimming = TextTrimming.CharacterEllipsis;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
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
	private static void _0021XamlIlPopulateTrampoline(NewTabView P_0)
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

