// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.MembersView
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Media.Immutable;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Resources.Converters.CommunityMembers;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Community.Members;

public class MembersView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_81
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MembersView> context = CreateContext(P_0);
			return new BoolToAngleConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<MembersView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MembersView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MembersView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FCommunity_002FMembers_002FMembersView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Members/MembersView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (MembersView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<MembersView> context = CreateContext(P_0);
			context.IntermediateRoot = new VirtualizingStackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal UserControl MainUserControl;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal MenuItem InviteMembersMenuItem;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal MenuItem CommunitySettingsMenuItem;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Panel InMenuPanel;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Panel OutMenuPanel;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	private MembersViewModel _membersViewModel => (MembersViewModel)base.DataContext;

	public MembersView()
	{
		InitializeComponent();
	}

	protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnAttachedToVisualTree(P_0);
		if (_membersViewModel.AttachedToVisualTreeCommand.CanExecute(null))
		{
			_membersViewModel.AttachedToVisualTreeCommand.Execute(null);
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
		INameScope nameScope = this.FindNameScope();
		MainUserControl = nameScope?.Find<UserControl>("MainUserControl");
		InviteMembersMenuItem = nameScope?.Find<MenuItem>("InviteMembersMenuItem");
		CommunitySettingsMenuItem = nameScope?.Find<MenuItem>("CommunitySettingsMenuItem");
		InMenuPanel = nameScope?.Find<Panel>("InMenuPanel");
		OutMenuPanel = nameScope?.Find<Panel>("OutMenuPanel");
	}

	[CompilerGenerated]
	private unsafe static void _0021XamlIlPopulate(IServiceProvider P_0, MembersView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<MembersView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<MembersView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FCommunity_002FMembers_002FMembersView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Members/MembersView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		if (P_1.Resources is ResourceDictionary resourceDictionary)
		{
			resourceDictionary.EnsureCapacity(resourceDictionary.Count + 2);
		}
		StyledProperty<double> widthProperty = Layoutable.WidthProperty;
		CompiledBindingExtension compiledBindingExtension2;
		CompiledBindingExtension compiledBindingExtension = (compiledBindingExtension2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EMenuIn_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension2);
		CompiledBindingExtension compiledBindingExtension3 = compiledBindingExtension2;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("CommunityMembersBoolToWidthConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension3.Converter = (IValueConverter)obj;
		context.PopParent();
		context.ProvideTargetProperty = Layoutable.WidthProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(P_1, widthProperty, compiledBindingExtension4);
		P_1.Name = "MainUserControl";
		object obj2 = P_1;
		context.AvaloniaNameScope.Register("MainUserControl", obj2);
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"BoolToAngleConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_81.Build_1), context));
		IResourceDictionary resources = P_1.Resources;
		RootMenuFlyout rootMenuFlyout;
		RootMenuFlyout value = (rootMenuFlyout = new RootMenuFlyout());
		context.PushParent(rootMenuFlyout);
		rootMenuFlyout.Placement = PlacementMode.Pointer;
		ItemCollection items = rootMenuFlyout.Items;
		MenuItem menuItem2;
		MenuItem menuItem = (menuItem2 = new MenuItem());
		((ISupportInitialize)menuItem).BeginInit();
		items.Add(menuItem);
		MenuItem menuItem4;
		MenuItem menuItem3 = (menuItem4 = menuItem2);
		context.PushParent(menuItem4);
		MenuItem menuItem5 = menuItem4;
		menuItem5.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.MarkAllChannelsAsRead;
		StyledProperty<ICommand?> commandProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EMarkAllChannelsAsReadCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem5, commandProperty, compiledBindingExtension6);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EHasAnyActivity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem5, isVisibleProperty, compiledBindingExtension8);
		context.PopParent();
		((ISupportInitialize)menuItem3).EndInit();
		ItemCollection items2 = rootMenuFlyout.Items;
		MenuItem menuItem7;
		MenuItem menuItem6 = (menuItem7 = new MenuItem());
		((ISupportInitialize)menuItem6).BeginInit();
		items2.Add(menuItem6);
		MenuItem menuItem8 = (menuItem4 = menuItem7);
		context.PushParent(menuItem4);
		MenuItem menuItem9 = menuItem4;
		menuItem9.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.Search;
		StyledProperty<ICommand?> commandProperty2 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EShowSearchPaneCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem9, commandProperty2, compiledBindingExtension10);
		context.PopParent();
		((ISupportInitialize)menuItem8).EndInit();
		ItemCollection items3 = rootMenuFlyout.Items;
		Separator separator2;
		Separator separator = (separator2 = new Separator());
		((ISupportInitialize)separator).BeginInit();
		items3.Add(separator);
		Separator separator4;
		Separator separator3 = (separator4 = separator2);
		context.PushParent(separator4);
		Separator separator5 = separator4;
		StyledProperty<bool> isVisibleProperty2 = Visual.IsVisibleProperty;
		MultiBinding multiBinding2;
		MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding3 = multiBinding2;
		multiBinding3.Converter = BoolConverters.Or;
		IList<IBinding> bindings = multiBinding3.Bindings;
		CompiledBindingExtension obj3 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "InviteMembersMenuItem").Property(Visual.IsVisibleProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item = obj3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings.Add(item);
		IList<IBinding> bindings2 = multiBinding3.Bindings;
		CompiledBindingExtension obj4 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "CommunitySettingsMenuItem").Property(Visual.IsVisibleProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item2 = obj4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings2.Add(item2);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(separator5, isVisibleProperty2, multiBinding);
		context.PopParent();
		((ISupportInitialize)separator3).EndInit();
		ItemCollection items4 = rootMenuFlyout.Items;
		MenuItem menuItem11;
		MenuItem menuItem10 = (menuItem11 = new MenuItem());
		((ISupportInitialize)menuItem10).BeginInit();
		items4.Add(menuItem10);
		MenuItem menuItem12 = (menuItem4 = menuItem11);
		context.PushParent(menuItem4);
		MenuItem menuItem13 = menuItem4;
		menuItem13.Name = "InviteMembersMenuItem";
		obj2 = menuItem13;
		context.AvaloniaNameScope.Register("InviteMembersMenuItem", obj2);
		menuItem13.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.InviteMembers;
		StyledProperty<ICommand?> commandProperty3 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension11 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EShowInviteMembersViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension12 = compiledBindingExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem13, commandProperty3, compiledBindingExtension12);
		StyledProperty<bool> isVisibleProperty3 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension13 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002ELocalCommunityPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalCommunityPermission_002CRootApp_002EClient_002ECoreDomain_002ECommunityCreateInvite_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension14 = compiledBindingExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem13, isVisibleProperty3, compiledBindingExtension14);
		context.PopParent();
		((ISupportInitialize)menuItem12).EndInit();
		ItemCollection items5 = rootMenuFlyout.Items;
		MenuItem menuItem15;
		MenuItem menuItem14 = (menuItem15 = new MenuItem());
		((ISupportInitialize)menuItem14).BeginInit();
		items5.Add(menuItem14);
		MenuItem menuItem16 = (menuItem4 = menuItem15);
		context.PushParent(menuItem4);
		MenuItem menuItem17 = menuItem4;
		menuItem17.Name = "CommunitySettingsMenuItem";
		obj2 = menuItem17;
		context.AvaloniaNameScope.Register("CommunitySettingsMenuItem", obj2);
		menuItem17.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.CommunitySettings;
		StyledProperty<ICommand?> commandProperty4 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension15 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EShowCommunitySettingsViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension16 = compiledBindingExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem17, commandProperty4, compiledBindingExtension16);
		StyledProperty<bool> isVisibleProperty4 = Visual.IsVisibleProperty;
		MultiBinding multiBinding4 = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		MultiBinding multiBinding5 = multiBinding2;
		multiBinding5.Converter = BoolConverters.Or;
		IList<IBinding> bindings3 = multiBinding5.Bindings;
		CompiledBindingExtension obj5 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002ELocalCommunityPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalCommunityPermission_002CRootApp_002EClient_002ECoreDomain_002ECommunityManageApps_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item3 = obj5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings3.Add(item3);
		IList<IBinding> bindings4 = multiBinding5.Bindings;
		CompiledBindingExtension obj6 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002ELocalCommunityPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalCommunityPermission_002CRootApp_002EClient_002ECoreDomain_002ECommunityManageAuditLog_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item4 = obj6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings4.Add(item4);
		IList<IBinding> bindings5 = multiBinding5.Bindings;
		CompiledBindingExtension obj7 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002ELocalCommunityPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalCommunityPermission_002CRootApp_002EClient_002ECoreDomain_002ECommunityManageBans_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item5 = obj7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings5.Add(item5);
		IList<IBinding> bindings6 = multiBinding5.Bindings;
		CompiledBindingExtension obj8 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002ELocalCommunityPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalCommunityPermission_002CRootApp_002EClient_002ECoreDomain_002ECommunityManageCommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item6 = obj8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings6.Add(item6);
		IList<IBinding> bindings7 = multiBinding5.Bindings;
		CompiledBindingExtension obj9 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002ELocalCommunityPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalCommunityPermission_002CRootApp_002EClient_002ECoreDomain_002ECommunityManageRoles_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item7 = obj9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings7.Add(item7);
		IList<IBinding> bindings8 = multiBinding5.Bindings;
		CompiledBindingExtension obj10 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002ELocalCommunityPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalCommunityPermission_002CRootApp_002EClient_002ECoreDomain_002ECommunityManageEmojis_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item8 = obj10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings8.Add(item8);
		IList<IBinding> bindings9 = multiBinding5.Bindings;
		CompiledBindingExtension obj11 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002ELocalCommunityPermission_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ELocalCommunityPermission_002CRootApp_002EClient_002ECoreDomain_002ECommunityManageInvites_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
				.Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item9 = obj11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings9.Add(item9);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(menuItem17, isVisibleProperty4, multiBinding4);
		context.PopParent();
		((ISupportInitialize)menuItem16).EndInit();
		ItemCollection items6 = rootMenuFlyout.Items;
		Separator separator7;
		Separator separator6 = (separator7 = new Separator());
		((ISupportInitialize)separator6).BeginInit();
		items6.Add(separator6);
		((ISupportInitialize)separator7).EndInit();
		ItemCollection items7 = rootMenuFlyout.Items;
		MenuItem menuItem19;
		MenuItem menuItem18 = (menuItem19 = new MenuItem());
		((ISupportInitialize)menuItem18).BeginInit();
		items7.Add(menuItem18);
		MenuItem menuItem20 = (menuItem4 = menuItem19);
		context.PushParent(menuItem4);
		MenuItem menuItem21 = menuItem4;
		menuItem21.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.DeleteCommunity;
		menuItem21.Classes.Add("DeleteMenuItem");
		StyledProperty<ICommand?> commandProperty5 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension17 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EShowDeleteCommunityViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension18 = compiledBindingExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem21, commandProperty5, compiledBindingExtension18);
		StyledProperty<bool> isVisibleProperty5 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension19 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002EIsOwner_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension20 = compiledBindingExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem21, isVisibleProperty5, compiledBindingExtension20);
		context.PopParent();
		((ISupportInitialize)menuItem20).EndInit();
		ItemCollection items8 = rootMenuFlyout.Items;
		MenuItem menuItem23;
		MenuItem menuItem22 = (menuItem23 = new MenuItem());
		((ISupportInitialize)menuItem22).BeginInit();
		items8.Add(menuItem22);
		MenuItem menuItem24 = (menuItem4 = menuItem23);
		context.PushParent(menuItem4);
		MenuItem menuItem25 = menuItem4;
		menuItem25.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.LeaveCommunity;
		menuItem25.Classes.Add("DeleteMenuItem");
		StyledProperty<ICommand?> commandProperty6 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension21 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Command("ShowLeaveCommunityViewModel", CompiledAvaloniaXaml.XamlIlTrampolines.RootApp_002EClient_002EAvalonia_003ARootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002BShowLeaveCommunityViewModel_0_0021CommandExecuteTrampoline, null, null).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension22 = compiledBindingExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem25, commandProperty6, compiledBindingExtension22);
		StyledProperty<bool> isVisibleProperty6 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension23 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002EIsOwner_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension24 = compiledBindingExtension23.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem25, isVisibleProperty6, compiledBindingExtension24);
		context.PopParent();
		((ISupportInitialize)menuItem24).EndInit();
		ItemCollection items9 = rootMenuFlyout.Items;
		Separator separator9;
		Separator separator8 = (separator9 = new Separator());
		((ISupportInitialize)separator8).BeginInit();
		items9.Add(separator8);
		Separator separator10 = (separator4 = separator9);
		context.PushParent(separator4);
		Separator separator11 = separator4;
		StyledProperty<bool> isVisibleProperty7 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension25 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EDeveloperModeEnabled_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension26 = compiledBindingExtension25.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(separator11, isVisibleProperty7, compiledBindingExtension26);
		context.PopParent();
		((ISupportInitialize)separator10).EndInit();
		ItemCollection items10 = rootMenuFlyout.Items;
		MenuItem menuItem27;
		MenuItem menuItem26 = (menuItem27 = new MenuItem());
		((ISupportInitialize)menuItem26).BeginInit();
		items10.Add(menuItem26);
		MenuItem menuItem28 = (menuItem4 = menuItem27);
		context.PushParent(menuItem4);
		MenuItem menuItem29 = menuItem4;
		menuItem29.Header = RootApp.Client.Avalonia.Resources.Strings.Resources.CopyCommunityId;
		StyledProperty<ICommand?> commandProperty7 = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension27 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECopyCommunityIdCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = MenuItem.CommandProperty;
		CompiledBindingExtension compiledBindingExtension28 = compiledBindingExtension27.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem29, commandProperty7, compiledBindingExtension28);
		StyledProperty<bool> isVisibleProperty8 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension29 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EDeveloperModeEnabled_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension30 = compiledBindingExtension29.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(menuItem29, isVisibleProperty8, compiledBindingExtension30);
		context.PopParent();
		((ISupportInitialize)menuItem28).EndInit();
		context.PopParent();
		resources.Add("SharedRootMenuFlyout", value);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		P_1.Content = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		Grid grid5 = grid4;
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Pixel)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Pixel)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid5.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		Controls children = grid5.Children;
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		children.Add(panel);
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		Panel panel5 = panel4;
		panel5.Name = "InMenuPanel";
		obj2 = panel5;
		context.AvaloniaNameScope.Register("InMenuPanel", obj2);
		Grid.SetRow(panel5, 0);
		StyledProperty<bool> isVisibleProperty9 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension31 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EMenuIn_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension32 = compiledBindingExtension31.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(panel5, isVisibleProperty9, compiledBindingExtension32);
		Controls children2 = panel5.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children2.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		Button button5 = button4;
		button5.Classes.Add("BasicButton");
		button5.Background = new ImmutableSolidColorBrush(16777215u);
		button5.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button5.VerticalAlignment = VerticalAlignment.Top;
		button5.Margin = new Thickness(0.0, 50.0, 0.0, 0.0);
		RenderOptions.SetBitmapInterpolationMode(button5, BitmapInterpolationMode.MediumQuality);
		StaticResourceExtension obj12 = new StaticResourceExtension
		{
			ResourceKey = "SharedRootMenuFlyout"
		};
		context.ProvideTargetProperty = Button.FlyoutProperty;
		object? obj13 = obj12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_24(button5, obj13);
		RootImageLoader rootImageLoader2;
		RootImageLoader rootImageLoader = (rootImageLoader2 = new RootImageLoader());
		((ISupportInitialize)rootImageLoader).BeginInit();
		button5.Content = rootImageLoader;
		RootImageLoader rootImageLoader4;
		RootImageLoader rootImageLoader3 = (rootImageLoader4 = rootImageLoader2);
		context.PushParent(rootImageLoader4);
		RootImageLoader rootImageLoader5 = rootImageLoader4;
		StyledProperty<IBrush?> backgroundProperty = TemplatedControl.BackgroundProperty;
		CompiledBindingExtension compiledBindingExtension33 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002EPictureHex_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		CompiledBindingExtension compiledBindingExtension34 = compiledBindingExtension33.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader5, backgroundProperty, compiledBindingExtension34);
		StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension35 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunityPictureAsyncBitmapWrapper_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
		context.ProvideTargetProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension36 = compiledBindingExtension35.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader5, sourceProperty, compiledBindingExtension36);
		rootImageLoader5.LoadingPlaceholderSize = 0.0;
		rootImageLoader5.Stretch = Stretch.UniformToFill;
		rootImageLoader5.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		rootImageLoader5.Height = 40.0;
		rootImageLoader5.Width = 40.0;
		StaticResourceExtension obj14 = new StaticResourceExtension
		{
			ResourceKey = "SharedRootMenuFlyout"
		};
		context.ProvideTargetProperty = Control.ContextFlyoutProperty;
		object? obj15 = obj14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_25(rootImageLoader5, obj15);
		context.PopParent();
		((ISupportInitialize)rootImageLoader3).EndInit();
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		Controls children3 = panel5.Children;
		Grid grid7;
		Grid grid6 = (grid7 = new Grid());
		((ISupportInitialize)grid6).BeginInit();
		children3.Add(grid6);
		Grid grid8 = (grid4 = grid7);
		context.PushParent(grid4);
		Grid grid9 = grid4;
		grid9.VerticalAlignment = VerticalAlignment.Bottom;
		grid9.Margin = new Thickness(16.0, 0.0, 0.0, 8.0);
		StyledProperty<bool> isVisibleProperty10 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension37 = (compiledBindingExtension2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EMenuIn_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension2);
		CompiledBindingExtension compiledBindingExtension38 = compiledBindingExtension2;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("BoolInverterConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj16 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension38.Converter = (IValueConverter)obj16;
		context.PopParent();
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension39 = compiledBindingExtension37.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(grid9, isVisibleProperty10, compiledBindingExtension39);
		Controls children4 = grid9.Children;
		RootSvgButton rootSvgButton2;
		RootSvgButton rootSvgButton = (rootSvgButton2 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton).BeginInit();
		children4.Add(rootSvgButton);
		RootSvgButton rootSvgButton4;
		RootSvgButton rootSvgButton3 = (rootSvgButton4 = rootSvgButton2);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton5 = rootSvgButton4;
		rootSvgButton5.Classes.Add("CustomSvgDimmedButton");
		rootSvgButton5.Margin = new Thickness(10.0, 0.0, 11.0, 0.0);
		rootSvgButton5.VerticalAlignment = VerticalAlignment.Center;
		rootSvgButton5.Width = 18.0;
		rootSvgButton5.Height = 18.0;
		StyledProperty<string> svgPathProperty = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("EllipsisVerticalSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton5, svgPathProperty, binding);
		rootSvgButton5.SvgWidth = 4.0;
		rootSvgButton5.SvgHeight = 18.0;
		ToolTip.SetPlacement(rootSvgButton5, PlacementMode.Right);
		ToolTip.SetVerticalOffset(rootSvgButton5, 0.0);
		ToolTip.SetHorizontalOffset(rootSvgButton5, 4.0);
		ToolTip.SetShowDelay(rootSvgButton5, 0);
		RootToolTip rootToolTip2;
		RootToolTip rootToolTip = (rootToolTip2 = new RootToolTip());
		((ISupportInitialize)rootToolTip).BeginInit();
		ToolTip.SetTip(rootSvgButton5, rootToolTip);
		RootToolTip rootToolTip4;
		RootToolTip rootToolTip3 = (rootToolTip4 = rootToolTip2);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip5 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip5, PlacementMode.Right);
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		rootToolTip5.Content = textBlock;
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.CommunityOptions;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj17 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj17);
		textBlock5.FontWeight = (FontWeight)450;
		textBlock5.FontSize = 14.0;
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip3).EndInit();
		StaticResourceExtension obj18 = new StaticResourceExtension
		{
			ResourceKey = "SharedRootMenuFlyout"
		};
		context.ProvideTargetProperty = Button.FlyoutProperty;
		object? obj19 = obj18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_24(rootSvgButton5, obj19);
		context.PopParent();
		((ISupportInitialize)rootSvgButton3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid8).EndInit();
		Controls children5 = panel5.Children;
		RootSvgButton rootSvgButton7;
		RootSvgButton rootSvgButton6 = (rootSvgButton7 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton6).BeginInit();
		children5.Add(rootSvgButton6);
		RootSvgButton rootSvgButton8 = (rootSvgButton4 = rootSvgButton7);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton9 = rootSvgButton4;
		StyledProperty<string> svgPathProperty2 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("DownArrowSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, svgPathProperty2, binding2);
		rootSvgButton9.Classes.Add("Custom");
		rootSvgButton9.SvgWidth = 10.0;
		rootSvgButton9.SvgHeight = 7.0;
		rootSvgButton9.Margin = new Thickness(0.0, 10.0, 0.0, 0.0);
		rootSvgButton9.HorizontalAlignment = HorizontalAlignment.Center;
		rootSvgButton9.VerticalAlignment = VerticalAlignment.Top;
		StyledProperty<IBrush?> backgroundProperty2 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, backgroundProperty2, binding3);
		rootSvgButton9.CornerRadius = new CornerRadius(14.0, 14.0, 14.0, 14.0);
		rootSvgButton9.Width = 28.0;
		rootSvgButton9.Height = 28.0;
		rootSvgButton9.BorderThickness = new Thickness(1.0, 1.0, 1.0, 1.0);
		StyledProperty<IBrush?> borderBrushProperty = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, borderBrushProperty, binding4);
		StyledProperty<ICommand?> commandProperty8 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension40 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EToggleMenuCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension41 = compiledBindingExtension40.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton9, commandProperty8, compiledBindingExtension41);
		RotateTransform rotateTransform;
		RotateTransform renderTransform = (rotateTransform = new RotateTransform());
		context.PushParent(rotateTransform);
		RotateTransform rotateTransform2 = rotateTransform;
		StyledProperty<double> angleProperty = RotateTransform.AngleProperty;
		CompiledBindingExtension compiledBindingExtension42 = (compiledBindingExtension2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EMenuIn_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension2);
		CompiledBindingExtension compiledBindingExtension43 = compiledBindingExtension2;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("BoolToAngleConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj20 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension43.Converter = (IValueConverter)obj20;
		context.PopParent();
		context.ProvideTargetProperty = RotateTransform.AngleProperty;
		CompiledBindingExtension compiledBindingExtension44 = compiledBindingExtension42.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rotateTransform2, angleProperty, compiledBindingExtension44);
		context.PopParent();
		rootSvgButton9.RenderTransform = renderTransform;
		context.PopParent();
		((ISupportInitialize)rootSvgButton8).EndInit();
		Controls children6 = panel5.Children;
		RootSvgButton rootSvgButton11;
		RootSvgButton rootSvgButton10 = (rootSvgButton11 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton10).BeginInit();
		children6.Add(rootSvgButton10);
		RootSvgButton rootSvgButton12 = (rootSvgButton4 = rootSvgButton11);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton13 = rootSvgButton4;
		rootSvgButton13.Classes.Add("CustomSvgDimmedButton");
		StyledProperty<string> svgPathProperty3 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("EllipsisVerticalSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton13, svgPathProperty3, binding5);
		rootSvgButton13.SvgWidth = 4.0;
		rootSvgButton13.SvgHeight = 18.0;
		rootSvgButton13.Width = 32.0;
		rootSvgButton13.Height = 24.0;
		rootSvgButton13.Margin = new Thickness(0.0, 102.0, 0.0, 12.0);
		StyledProperty<bool> isVisibleProperty11 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension45 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EMenuIn_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension46 = compiledBindingExtension45.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton13, isVisibleProperty11, compiledBindingExtension46);
		ToolTip.SetPlacement(rootSvgButton13, PlacementMode.Right);
		ToolTip.SetVerticalOffset(rootSvgButton13, 0.0);
		ToolTip.SetHorizontalOffset(rootSvgButton13, 4.0);
		ToolTip.SetShowDelay(rootSvgButton13, 0);
		RootToolTip rootToolTip7;
		RootToolTip rootToolTip6 = (rootToolTip7 = new RootToolTip());
		((ISupportInitialize)rootToolTip6).BeginInit();
		ToolTip.SetTip(rootSvgButton13, rootToolTip6);
		RootToolTip rootToolTip8 = (rootToolTip4 = rootToolTip7);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip9 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip9, PlacementMode.Right);
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		rootToolTip9.Content = textBlock6;
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.CommunityOptions;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj21 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj21);
		textBlock9.FontWeight = (FontWeight)450;
		textBlock9.FontSize = 14.0;
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock9.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip8).EndInit();
		StaticResourceExtension obj22 = new StaticResourceExtension
		{
			ResourceKey = "SharedRootMenuFlyout"
		};
		context.ProvideTargetProperty = Button.FlyoutProperty;
		object? obj23 = obj22.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_24(rootSvgButton13, obj23);
		context.PopParent();
		((ISupportInitialize)rootSvgButton12).EndInit();
		context.PopParent();
		((ISupportInitialize)panel3).EndInit();
		Controls children7 = grid5.Children;
		Panel panel7;
		Panel panel6 = (panel7 = new Panel());
		((ISupportInitialize)panel6).BeginInit();
		children7.Add(panel6);
		Panel panel8 = (panel4 = panel7);
		context.PushParent(panel4);
		Panel panel9 = panel4;
		panel9.Name = "OutMenuPanel";
		obj2 = panel9;
		context.AvaloniaNameScope.Register("OutMenuPanel", obj2);
		Grid.SetRow(panel9, 0);
		StyledProperty<bool> isVisibleProperty12 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension47 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Not().Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EMenuIn_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension48 = compiledBindingExtension47.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(panel9, isVisibleProperty12, compiledBindingExtension48);
		StaticResourceExtension obj24 = new StaticResourceExtension
		{
			ResourceKey = "SharedRootMenuFlyout"
		};
		context.ProvideTargetProperty = Control.ContextFlyoutProperty;
		object? obj25 = obj24.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_25(panel9, obj25);
		Controls children8 = panel9.Children;
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		children8.Add(rootBorder);
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		StyledProperty<IBrush?> backgroundProperty3 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("BackgroundPrimary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, backgroundProperty3, binding6);
		StyledProperty<IBrush?> borderBrushProperty2 = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, borderBrushProperty2, binding7);
		rootBorder4.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder4.CornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		rootBorder4.Padding = new Thickness(8.0, 8.0, 0.0, 8.0);
		rootBorder4.Margin = new Thickness(10.0, 10.0, 10.0, 0.0);
		RenderOptions.SetBitmapInterpolationMode(rootBorder4, BitmapInterpolationMode.MediumQuality);
		Grid grid11;
		Grid grid10 = (grid11 = new Grid());
		((ISupportInitialize)grid10).BeginInit();
		rootBorder4.Child = grid10;
		Grid grid12 = (grid4 = grid11);
		context.PushParent(grid4);
		Grid grid13 = grid4;
		ColumnDefinitions columnDefinitions = new ColumnDefinitions();
		columnDefinitions.Capacity = 4;
		columnDefinitions.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(12.0, GridUnitType.Pixel)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		columnDefinitions.Add(new ColumnDefinition(new GridLength(32.0, GridUnitType.Pixel)));
		grid13.ColumnDefinitions = columnDefinitions;
		Controls children9 = grid13.Children;
		Button button7;
		Button button6 = (button7 = new Button());
		((ISupportInitialize)button6).BeginInit();
		children9.Add(button6);
		Button button8 = (button4 = button7);
		context.PushParent(button4);
		Button button9 = button4;
		button9.Classes.Add("BasicButton");
		button9.Background = new ImmutableSolidColorBrush(16777215u);
		button9.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button9.VerticalAlignment = VerticalAlignment.Top;
		StaticResourceExtension obj26 = new StaticResourceExtension
		{
			ResourceKey = "SharedRootMenuFlyout"
		};
		context.ProvideTargetProperty = Button.FlyoutProperty;
		object? obj27 = obj26.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_24(button9, obj27);
		RootImageLoader rootImageLoader7;
		RootImageLoader rootImageLoader6 = (rootImageLoader7 = new RootImageLoader());
		((ISupportInitialize)rootImageLoader6).BeginInit();
		button9.Content = rootImageLoader6;
		RootImageLoader rootImageLoader8 = (rootImageLoader4 = rootImageLoader7);
		context.PushParent(rootImageLoader4);
		RootImageLoader rootImageLoader9 = rootImageLoader4;
		StyledProperty<IBrush?> backgroundProperty4 = TemplatedControl.BackgroundProperty;
		CompiledBindingExtension compiledBindingExtension49 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002EPictureHex_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		CompiledBindingExtension compiledBindingExtension50 = compiledBindingExtension49.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader9, backgroundProperty4, compiledBindingExtension50);
		StyledProperty<BitmapWrapper?> sourceProperty2 = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension51 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunityPictureAsyncBitmapWrapper_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
		context.ProvideTargetProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension52 = compiledBindingExtension51.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader9, sourceProperty2, compiledBindingExtension52);
		rootImageLoader9.LoadingPlaceholderSize = 0.0;
		rootImageLoader9.Stretch = Stretch.UniformToFill;
		rootImageLoader9.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		rootImageLoader9.Width = 54.0;
		rootImageLoader9.Height = 54.0;
		context.PopParent();
		((ISupportInitialize)rootImageLoader8).EndInit();
		context.PopParent();
		((ISupportInitialize)button8).EndInit();
		Controls children10 = grid13.Children;
		Grid grid15;
		Grid grid14 = (grid15 = new Grid());
		((ISupportInitialize)grid14).BeginInit();
		children10.Add(grid14);
		Grid grid16 = (grid4 = grid15);
		context.PushParent(grid4);
		Grid grid17 = grid4;
		Grid.SetColumn(grid17, 2);
		Grid.SetRow(grid17, 0);
		grid17.VerticalAlignment = VerticalAlignment.Center;
		RowDefinitions rowDefinitions = new RowDefinitions();
		rowDefinitions.Capacity = 2;
		rowDefinitions.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		rowDefinitions.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid17.RowDefinitions = rowDefinitions;
		Controls children11 = grid17.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children11.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		Grid.SetRow(stackPanel5, 0);
		stackPanel5.Orientation = Orientation.Horizontal;
		Controls children12 = stackPanel5.Children;
		RootTrimTooltipTextBlock rootTrimTooltipTextBlock2;
		RootTrimTooltipTextBlock rootTrimTooltipTextBlock = (rootTrimTooltipTextBlock2 = new RootTrimTooltipTextBlock());
		((ISupportInitialize)rootTrimTooltipTextBlock).BeginInit();
		children12.Add(rootTrimTooltipTextBlock);
		RootTrimTooltipTextBlock rootTrimTooltipTextBlock4;
		RootTrimTooltipTextBlock rootTrimTooltipTextBlock3 = (rootTrimTooltipTextBlock4 = rootTrimTooltipTextBlock2);
		context.PushParent(rootTrimTooltipTextBlock4);
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension53 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002EName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension54 = compiledBindingExtension53.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTrimTooltipTextBlock4, textProperty, compiledBindingExtension54);
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj28 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(rootTrimTooltipTextBlock4, obj28);
		rootTrimTooltipTextBlock4.FontWeight = FontWeight.Medium;
		rootTrimTooltipTextBlock4.FontSize = 14.0;
		rootTrimTooltipTextBlock4.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTrimTooltipTextBlock4, foregroundProperty, binding8);
		rootTrimTooltipTextBlock4.TextTrimming = TextTrimming.CharacterEllipsis;
		rootTrimTooltipTextBlock4.TextWrapping = TextWrapping.NoWrap;
		rootTrimTooltipTextBlock4.MaxWidth = 150.0;
		ToolTip.SetPlacement(rootTrimTooltipTextBlock4, PlacementMode.Bottom);
		ToolTip.SetVerticalOffset(rootTrimTooltipTextBlock4, 1.0);
		ToolTip.SetShowDelay(rootTrimTooltipTextBlock4, 0);
		context.PopParent();
		((ISupportInitialize)rootTrimTooltipTextBlock3).EndInit();
		Controls children13 = stackPanel5.Children;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		children13.Add(rootSvgImage);
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage5 = rootSvgImage4;
		StyledProperty<bool> isVisibleProperty13 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension55 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002EIsVerified_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension56 = compiledBindingExtension55.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage5, isVisibleProperty13, compiledBindingExtension56);
		StyledProperty<string?> svgPathProperty4 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("VerifiedCommunitySVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage5, svgPathProperty4, binding9);
		rootSvgImage5.Width = 16.0;
		rootSvgImage5.Height = 16.0;
		rootSvgImage5.Margin = new Thickness(4.0, 0.0, 0.0, 0.0);
		rootSvgImage5.VerticalAlignment = VerticalAlignment.Center;
		rootSvgImage5.Cursor = new Cursor(StandardCursorType.Hand);
		ToolTip.SetPlacement(rootSvgImage5, PlacementMode.Top);
		ToolTip.SetVerticalOffset(rootSvgImage5, -2.0);
		ToolTip.SetShowDelay(rootSvgImage5, 300);
		RootToolTip rootToolTip11;
		RootToolTip rootToolTip10 = (rootToolTip11 = new RootToolTip());
		((ISupportInitialize)rootToolTip10).BeginInit();
		ToolTip.SetTip(rootSvgImage5, rootToolTip10);
		RootToolTip rootToolTip12 = (rootToolTip4 = rootToolTip11);
		context.PushParent(rootToolTip4);
		RootToolTip rootToolTip13 = rootToolTip4;
		ToolTip.SetPlacement(rootToolTip13, PlacementMode.Top);
		Grid grid19;
		Grid grid18 = (grid19 = new Grid());
		((ISupportInitialize)grid18).BeginInit();
		rootToolTip13.Content = grid18;
		Grid grid20 = (grid4 = grid19);
		context.PushParent(grid4);
		Grid grid21 = grid4;
		grid21.MaxWidth = 300.0;
		ColumnDefinitions columnDefinitions2 = new ColumnDefinitions();
		columnDefinitions2.Capacity = 3;
		columnDefinitions2.Add(new ColumnDefinition(new GridLength(0.0, GridUnitType.Auto)));
		columnDefinitions2.Add(new ColumnDefinition(new GridLength(12.0, GridUnitType.Pixel)));
		columnDefinitions2.Add(new ColumnDefinition(new GridLength(1.0, GridUnitType.Star)));
		grid21.ColumnDefinitions = columnDefinitions2;
		Controls children14 = grid21.Children;
		RootSvgImage rootSvgImage7;
		RootSvgImage rootSvgImage6 = (rootSvgImage7 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage6).BeginInit();
		children14.Add(rootSvgImage6);
		RootSvgImage rootSvgImage8 = (rootSvgImage4 = rootSvgImage7);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage9 = rootSvgImage4;
		rootSvgImage9.Width = 36.0;
		rootSvgImage9.Height = 36.0;
		StyledProperty<string?> svgPathProperty5 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("VerifiedCommunitySVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage9, svgPathProperty5, binding10);
		context.PopParent();
		((ISupportInitialize)rootSvgImage8).EndInit();
		Controls children15 = grid21.Children;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		children15.Add(stackPanel6);
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		Grid.SetColumn(stackPanel9, 2);
		stackPanel9.Spacing = 2.0;
		stackPanel9.VerticalAlignment = VerticalAlignment.Center;
		Controls children16 = stackPanel9.Children;
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		children16.Add(textBlock10);
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		textBlock13.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.VerifiedCommunity;
		textBlock13.HorizontalAlignment = HorizontalAlignment.Center;
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj29 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock13, obj29);
		textBlock13.FontWeight = FontWeight.DemiBold;
		textBlock13.FontSize = 14.0;
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, foregroundProperty2, binding11);
		textBlock13.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		context.PopParent();
		((ISupportInitialize)grid20).EndInit();
		context.PopParent();
		((ISupportInitialize)rootToolTip12).EndInit();
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		Controls children17 = grid17.Children;
		StackPanel stackPanel11;
		StackPanel stackPanel10 = (stackPanel11 = new StackPanel());
		((ISupportInitialize)stackPanel10).BeginInit();
		children17.Add(stackPanel10);
		StackPanel stackPanel12 = (stackPanel4 = stackPanel11);
		context.PushParent(stackPanel4);
		StackPanel stackPanel13 = stackPanel4;
		Grid.SetRow(stackPanel13, 1);
		stackPanel13.Orientation = Orientation.Horizontal;
		stackPanel13.Margin = new Thickness(0.0, 4.0, 0.0, 0.0);
		stackPanel13.Spacing = 6.0;
		stackPanel13.VerticalAlignment = VerticalAlignment.Center;
		Controls children18 = stackPanel13.Children;
		RootSvgImage rootSvgImage11;
		RootSvgImage rootSvgImage10 = (rootSvgImage11 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage10).BeginInit();
		children18.Add(rootSvgImage10);
		RootSvgImage rootSvgImage12 = (rootSvgImage4 = rootSvgImage11);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage13 = rootSvgImage4;
		rootSvgImage13.Width = 10.0;
		rootSvgImage13.Height = 10.0;
		rootSvgImage13.Opacity = 0.64;
		StyledProperty<string?> svgPathProperty6 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("UserSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding12 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage13, svgPathProperty6, binding12);
		context.PopParent();
		((ISupportInitialize)rootSvgImage12).EndInit();
		Controls children19 = stackPanel13.Children;
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		children19.Add(textBlock14);
		TextBlock textBlock16 = (textBlock4 = textBlock15);
		context.PushParent(textBlock4);
		TextBlock textBlock17 = textBlock4;
		StaticResourceExtension staticResourceExtension8 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj30 = staticResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock17, obj30);
		textBlock17.FontWeight = (FontWeight)450;
		textBlock17.FontSize = 12.0;
		textBlock17.Margin = new Thickness(0.0, 1.0, 0.0, 0.0);
		StyledProperty<string?> textProperty2 = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension57 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunity_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002ECommunity_002CRootApp_002EClient_002ECoreDomain_002EMembers_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EServices_002EIMemberService_002CRootApp_002EClient_002ECoreDomain_002EMemberCount_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build());
		compiledBindingExtension57.FallbackValue = "0";
		compiledBindingExtension57.StringFormat = RootApp.Client.Avalonia.Resources.Strings.Resources.MembersCount;
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension58 = compiledBindingExtension57.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock17, textProperty2, compiledBindingExtension58);
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding13 = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock17, foregroundProperty3, binding13);
		context.PopParent();
		((ISupportInitialize)textBlock16).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel12).EndInit();
		context.PopParent();
		((ISupportInitialize)grid16).EndInit();
		Controls children20 = grid13.Children;
		RootSvgButton rootSvgButton15;
		RootSvgButton rootSvgButton14 = (rootSvgButton15 = new RootSvgButton());
		((ISupportInitialize)rootSvgButton14).BeginInit();
		children20.Add(rootSvgButton14);
		RootSvgButton rootSvgButton16 = (rootSvgButton4 = rootSvgButton15);
		context.PushParent(rootSvgButton4);
		RootSvgButton rootSvgButton17 = rootSvgButton4;
		Grid.SetColumn(rootSvgButton17, 3);
		Grid.SetRow(rootSvgButton17, 0);
		StyledProperty<string> svgPathProperty7 = RootSvgButton.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("DownArrowSVG");
		context.ProvideTargetProperty = RootSvgButton.SvgPathProperty;
		IBinding binding14 = dynamicResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton17, svgPathProperty7, binding14);
		rootSvgButton17.Classes.Add("SvgDimmedButton");
		rootSvgButton17.SvgWidth = 10.0;
		rootSvgButton17.SvgHeight = 7.0;
		rootSvgButton17.Background = new ImmutableSolidColorBrush(16777215u);
		rootSvgButton17.CornerRadius = new CornerRadius(0.0, 0.0, 0.0, 0.0);
		rootSvgButton17.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		rootSvgButton17.Width = 70.0;
		rootSvgButton17.Height = 32.0;
		StyledProperty<bool> isVisibleProperty14 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension59 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "OutMenuPanel").Property(InputElement.IsPointerOverProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension60 = compiledBindingExtension59.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton17, isVisibleProperty14, compiledBindingExtension60);
		StyledProperty<ICommand?> commandProperty9 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension61 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EToggleMenuCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension62 = compiledBindingExtension61.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgButton17, commandProperty9, compiledBindingExtension62);
		RotateTransform renderTransform2 = (rotateTransform = new RotateTransform());
		context.PushParent(rotateTransform);
		RotateTransform rotateTransform3 = rotateTransform;
		StyledProperty<double> angleProperty2 = RotateTransform.AngleProperty;
		CompiledBindingExtension compiledBindingExtension63 = (compiledBindingExtension2 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EMenuIn_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension2);
		CompiledBindingExtension compiledBindingExtension64 = compiledBindingExtension2;
		StaticResourceExtension staticResourceExtension9 = new StaticResourceExtension("BoolToAngleConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj31 = staticResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension64.Converter = (IValueConverter)obj31;
		context.PopParent();
		context.ProvideTargetProperty = RotateTransform.AngleProperty;
		CompiledBindingExtension compiledBindingExtension65 = compiledBindingExtension63.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rotateTransform3, angleProperty2, compiledBindingExtension65);
		context.PopParent();
		rootSvgButton17.RenderTransform = renderTransform2;
		context.PopParent();
		((ISupportInitialize)rootSvgButton16).EndInit();
		context.PopParent();
		((ISupportInitialize)grid12).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		context.PopParent();
		((ISupportInitialize)panel8).EndInit();
		Controls children21 = grid5.Children;
		Rectangle rectangle2;
		Rectangle rectangle = (rectangle2 = new Rectangle());
		((ISupportInitialize)rectangle).BeginInit();
		children21.Add(rectangle);
		Rectangle rectangle4;
		Rectangle rectangle3 = (rectangle4 = rectangle2);
		context.PushParent(rectangle4);
		Grid.SetRow(rectangle4, 2);
		rectangle4.Height = 0.5;
		StyledProperty<IBrush?> fillProperty = Shape.FillProperty;
		DynamicResourceExtension dynamicResourceExtension15 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Shape.FillProperty;
		IBinding binding15 = dynamicResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle4, fillProperty, binding15);
		StyledProperty<bool> isVisibleProperty15 = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension66 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EMenuIn_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension67 = compiledBindingExtension66.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rectangle4, isVisibleProperty15, compiledBindingExtension67);
		rectangle4.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rectangle3).EndInit();
		Controls children22 = grid5.Children;
		RootMemberVisibilitySwitch rootMemberVisibilitySwitch2;
		RootMemberVisibilitySwitch rootMemberVisibilitySwitch = (rootMemberVisibilitySwitch2 = new RootMemberVisibilitySwitch());
		((ISupportInitialize)rootMemberVisibilitySwitch).BeginInit();
		children22.Add(rootMemberVisibilitySwitch);
		RootMemberVisibilitySwitch rootMemberVisibilitySwitch4;
		RootMemberVisibilitySwitch rootMemberVisibilitySwitch3 = (rootMemberVisibilitySwitch4 = rootMemberVisibilitySwitch2);
		context.PushParent(rootMemberVisibilitySwitch4);
		Grid.SetRow(rootMemberVisibilitySwitch4, 3);
		rootMemberVisibilitySwitch4.Margin = new Thickness(10.0, 12.0, 10.0, 12.0);
		StyledProperty<MemberVisibilityOption> selectedOptionProperty = RootMemberVisibilitySwitch.SelectedOptionProperty;
		CompiledBindingExtension compiledBindingExtension68 = new CompiledBindingExtension();
		compiledBindingExtension68.Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "MainUserControl").Property(StyledElement.DataContextProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).TypeCast<MembersViewModel>()
			.Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002ECommunityMemberFilter_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build();
		compiledBindingExtension68.Mode = BindingMode.TwoWay;
		context.ProvideTargetProperty = RootMemberVisibilitySwitch.SelectedOptionProperty;
		CompiledBindingExtension compiledBindingExtension69 = compiledBindingExtension68.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootMemberVisibilitySwitch4, selectedOptionProperty, compiledBindingExtension69);
		context.PopParent();
		((ISupportInitialize)rootMemberVisibilitySwitch3).EndInit();
		Controls children23 = grid5.Children;
		RootScrollViewer rootScrollViewer2;
		RootScrollViewer rootScrollViewer = (rootScrollViewer2 = new RootScrollViewer());
		((ISupportInitialize)rootScrollViewer).BeginInit();
		children23.Add(rootScrollViewer);
		RootScrollViewer rootScrollViewer4;
		RootScrollViewer rootScrollViewer3 = (rootScrollViewer4 = rootScrollViewer2);
		context.PushParent(rootScrollViewer4);
		Grid.SetRow(rootScrollViewer4, 4);
		RootScrollViewer.SetEnableDropShadowOnScroll(rootScrollViewer4, true);
		ItemsControl itemsControl2;
		ItemsControl itemsControl = (itemsControl2 = new ItemsControl());
		((ISupportInitialize)itemsControl).BeginInit();
		rootScrollViewer4.Content = itemsControl;
		ItemsControl itemsControl4;
		ItemsControl itemsControl3 = (itemsControl4 = itemsControl2);
		context.PushParent(itemsControl4);
		StyledProperty<IEnumerable?> itemsSourceProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension70 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EMembersViewModel_002CRootApp_002EClient_002EAvalonia_002EMembers_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = ItemsControl.ItemsSourceProperty;
		CompiledBindingExtension compiledBindingExtension71 = compiledBindingExtension70.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(itemsControl4, itemsSourceProperty, compiledBindingExtension71);
		itemsControl4.Margin = new Thickness(4.0, 0.0, 4.0, 10.0);
		itemsControl4.ItemsPanel = new ItemsPanelTemplate
		{
			Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_81.Build_2), context)
		};
		context.PopParent();
		((ISupportInitialize)itemsControl3).EndInit();
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
	private static void _0021XamlIlPopulateTrampoline(MembersView P_0)
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

