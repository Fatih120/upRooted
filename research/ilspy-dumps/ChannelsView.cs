using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Controls.ReorderableList;
using RootApp.Client.Avalonia.Resources.Strings;

namespace RootApp.Client.Avalonia.UI.Community.Channels;

public class ChannelsView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_65
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ChannelsView> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<ChannelsView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ChannelsView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ChannelsView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Community/Channels/ChannelsView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Channels/ChannelsView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (ChannelsView)service;
				}
			}
			return context;
		}
	}

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	public ChannelsView()
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
	private unsafe static void !XamlIlPopulate(IServiceProvider P_0, ChannelsView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<ChannelsView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ChannelsView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Community/Channels/ChannelsView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Channels/ChannelsView.axaml")
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
		menuItem4.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.CreateChannelGroup;
		StyledProperty<ICommand?> commandProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelsViewModel,RootApp.Client.Avalonia.ShowCreateChannelGroupViewModelCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem4, commandProperty, compiledBindingExtension2);
		StyledProperty<bool> isEnabledProperty = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelsViewModel,RootApp.Client.Avalonia.Community!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.Community,RootApp.Client.CoreDomain.LocalCommunityPermission!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.Community.LocalCommunityPermission,RootApp.Client.CoreDomain.CommunityCreateChannelGroup!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = InputElement.IsEnabledProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem4, isEnabledProperty, compiledBindingExtension4);
		context.PopParent();
		((ISupportInitialize)menuItem3).EndInit();
		context.PopParent();
		rootScrollViewer4.ContextFlyout = contextFlyout;
		RootReorderableList rootReorderableList2;
		RootReorderableList rootReorderableList = (rootReorderableList2 = new RootReorderableList());
		((ISupportInitialize)rootReorderableList).BeginInit();
		rootScrollViewer4.Content = rootReorderableList;
		RootReorderableList rootReorderableList4;
		RootReorderableList rootReorderableList3 = (rootReorderableList4 = rootReorderableList2);
		context.PushParent(rootReorderableList4);
		StyledProperty<IList?> realItemsProperty = RootReorderableList.RealItemsProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelsViewModel,RootApp.Client.Avalonia.Channels!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootReorderableList.RealItemsProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootReorderableList4, realItemsProperty, compiledBindingExtension6);
		StyledProperty<ICommand?> dragInitiatedCommandProperty = RootReorderableList.DragInitiatedCommandProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelsViewModel,RootApp.Client.Avalonia.DragInitiatedCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootReorderableList.DragInitiatedCommandProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootReorderableList4, dragInitiatedCommandProperty, compiledBindingExtension8);
		StyledProperty<ICommand?> dragCancelledCommandProperty = RootReorderableList.DragCancelledCommandProperty;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelsViewModel,RootApp.Client.Avalonia.DragCancelledCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootReorderableList.DragCancelledCommandProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootReorderableList4, dragCancelledCommandProperty, compiledBindingExtension10);
		StyledProperty<ICommand?> reorderRequestedCommandProperty = RootReorderableList.ReorderRequestedCommandProperty;
		CompiledBindingExtension compiledBindingExtension11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelsViewModel,RootApp.Client.Avalonia.ReorderRequestedCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootReorderableList.ReorderRequestedCommandProperty;
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootReorderableList4, reorderRequestedCommandProperty, compiledBindingExtension12);
		StyledProperty<Func<ReorderRequestedData, bool>> canReorderProperty = RootReorderableList.CanReorderProperty;
		CompiledBindingExtension compiledBindingExtension13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Community.Channels.ChannelsViewModel,RootApp.Client.Avalonia.CanReorderChannelItem!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootReorderableList.CanReorderProperty;
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootReorderableList4, canReorderProperty, compiledBindingExtension14);
		rootReorderableList4.Margin = new Thickness(0.0, 0.0, 0.0, 10.0);
		rootReorderableList4.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_65.Build_1), context)
		};
		context.PopParent();
		((ISupportInitialize)rootReorderableList3).EndInit();
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
	private static void !XamlIlPopulateTrampoline(ChannelsView P_0)
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
