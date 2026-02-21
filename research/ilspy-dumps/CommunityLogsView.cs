using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
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
using RootApp.Client.Avalonia.Resources.Strings;

namespace RootApp.Client.Avalonia.UI.Community.Settings;

public class CommunityLogsView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_116
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<CommunityLogsView> context = CreateContext(P_0);
			context.IntermediateRoot = new VirtualizingStackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<CommunityLogsView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<CommunityLogsView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<CommunityLogsView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Community/Settings/CommunityLogsView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Settings/CommunityLogsView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (CommunityLogsView)service;
				}
			}
			return context;
		}
	}

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	private CommunityLogsViewModel? _actionLogsViewModel => base.DataContext as CommunityLogsViewModel;

	public CommunityLogsView()
	{
		InitializeComponent();
	}

	private void onScrollViewerScrollChanged(object? sender, ScrollChangedEventArgs e)
	{
		if (sender is ScrollViewer { Extent: { Height: var height }, Viewport: var viewport } scrollViewer)
		{
			double num = height - viewport.Height;
			double y = scrollViewer.Offset.Y;
			if (num - y <= 200.0 && _actionLogsViewModel != null && _actionLogsViewModel.FetchCommunityLogsCommand.CanExecute(null))
			{
				_actionLogsViewModel.FetchCommunityLogsCommand.Execute(null);
			}
		}
	}

	private void onAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
	{
		if (_actionLogsViewModel != null && _actionLogsViewModel.FetchCommunityLogsCommand.CanExecute(null))
		{
			_actionLogsViewModel.FetchCommunityLogsCommand.Execute(null);
		}
	}

	private void onDetachedFromVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
	{
		if (_actionLogsViewModel != null && _actionLogsViewModel.ResetStateCommand.CanExecute(null))
		{
			_actionLogsViewModel.ResetStateCommand.Execute(null);
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
	}

	[CompilerGenerated]
	private unsafe static void !XamlIlPopulate(IServiceProvider P_0, CommunityLogsView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<CommunityLogsView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<CommunityLogsView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Community/Settings/CommunityLogsView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Settings/CommunityLogsView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.DetachedFromVisualTree += context.RootObject.onDetachedFromVisualTree;
		P_1.AttachedToVisualTree += context.RootObject.onAttachedToVisualTree;
		RootScrollViewer rootScrollViewer2;
		RootScrollViewer rootScrollViewer = (rootScrollViewer2 = new RootScrollViewer());
		((ISupportInitialize)rootScrollViewer).BeginInit();
		P_1.Content = rootScrollViewer;
		RootScrollViewer rootScrollViewer4;
		RootScrollViewer rootScrollViewer3 = (rootScrollViewer4 = rootScrollViewer2);
		context.PushParent(rootScrollViewer4);
		rootScrollViewer4.AddHandler(ScrollViewer.ScrollChangedEvent, context.RootObject.onScrollViewerScrollChanged);
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		rootScrollViewer4.Content = panel;
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		panel4.Margin = new Thickness(24.0, 0.0, 24.0, 0.0);
		global::Avalonia.Controls.Controls children = panel4.Children;
		ItemsControl itemsControl2;
		ItemsControl itemsControl = (itemsControl2 = new ItemsControl());
		((ISupportInitialize)itemsControl).BeginInit();
		children.Add(itemsControl);
		ItemsControl itemsControl4;
		ItemsControl itemsControl3 = (itemsControl4 = itemsControl2);
		context.PushParent(itemsControl4);
		StyledProperty<IEnumerable?> itemsSourceProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Settings.CommunityLogsViewModel,RootApp.Client.Avalonia.CommunityLogs!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl4, itemsSourceProperty, compiledBindingExtension2);
		itemsControl4.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_116.Build_1), context)
		};
		context.PopParent();
		((ISupportInitialize)itemsControl3).EndInit();
		global::Avalonia.Controls.Controls children2 = panel4.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children2.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		stackPanel4.VerticalAlignment = VerticalAlignment.Center;
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Settings.CommunityLogsViewModel,RootApp.Client.Avalonia.CommunityLogs!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.System.Collections.ObjectModel.Collection`1,System.Runtime.Count_117!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(stackPanel4, isVisibleProperty, compiledBindingExtension4);
		stackPanel4.MaxWidth = 340.0;
		global::Avalonia.Controls.Controls children3 = stackPanel4.Children;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		children3.Add(rootSvgImage);
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		rootSvgImage4.Width = 78.0;
		rootSvgImage4.Height = 78.0;
		StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("EmptyStateSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage4, svgPathProperty, binding);
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		global::Avalonia.Controls.Controls children4 = stackPanel4.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children4.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ActionLogsEmpty;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock5, obj);
		textBlock5.FontWeight = FontWeight.Bold;
		textBlock5.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		textBlock5.FontSize = 24.0;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding2);
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock5.HorizontalAlignment = HorizontalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		global::Avalonia.Controls.Controls children5 = stackPanel4.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children5.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.TextWrapping = TextWrapping.Wrap;
		textBlock9.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.ActionLogsEmptyDescription;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock9, obj2);
		textBlock9.FontWeight = FontWeight.Medium;
		textBlock9.Margin = new Thickness(0.0, 10.0, 0.0, 0.0);
		textBlock9.FontSize = 14.0;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding3);
		textBlock9.Padding = new Thickness(10.0, 0.0, 10.0, 0.0);
		textBlock9.HorizontalAlignment = HorizontalAlignment.Center;
		textBlock9.TextAlignment = TextAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		context.PopParent();
		((ISupportInitialize)panel3).EndInit();
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
	private static void !XamlIlPopulateTrampoline(CommunityLogsView P_0)
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
