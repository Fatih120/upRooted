// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Community.Members.KickMemberView
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Community.Members;

public class KickMemberView : UserControl
{
	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public KickMemberView()
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
	private static void _0021XamlIlPopulate(IServiceProvider P_0, KickMemberView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<KickMemberView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<KickMemberView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FCommunity_002FMembers_002FKickMemberView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Community/Members/KickMemberView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		RootConfirmationControl rootConfirmationControl2;
		RootConfirmationControl rootConfirmationControl = (rootConfirmationControl2 = new RootConfirmationControl());
		((ISupportInitialize)rootConfirmationControl).BeginInit();
		P_1.Content = rootConfirmationControl;
		RootConfirmationControl rootConfirmationControl4;
		RootConfirmationControl rootConfirmationControl3 = (rootConfirmationControl4 = rootConfirmationControl2);
		context.PushParent(rootConfirmationControl4);
		StyledProperty<IMarkdownEngine> markdownEngineProperty = RootConfirmationControl.MarkdownEngineProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EKickMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EMarkdownEngine_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootConfirmationControl.MarkdownEngineProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, markdownEngineProperty, compiledBindingExtension2);
		rootConfirmationControl4.TitleText = RootApp.Client.Avalonia.Resources.Strings.Resources.KickMember;
		StyledProperty<string> confirmationMessageProperty = RootConfirmationControl.ConfirmationMessageProperty;
		CompiledBindingExtension compiledBindingExtension4;
		CompiledBindingExtension compiledBindingExtension3 = (compiledBindingExtension4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EKickMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EMember_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002ECommunity_002EMember_002CRootApp_002EClient_002ECoreDomain_002EGlobalUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EUserName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor)
			.Build()));
		context.PushParent(compiledBindingExtension4);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("StringFormatConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension4.Converter = (IValueConverter)obj;
		compiledBindingExtension4.ConverterParameter = RootApp.Client.Avalonia.Resources.Strings.Resources.KickConfirmationMessage;
		context.PopParent();
		context.ProvideTargetProperty = RootConfirmationControl.ConfirmationMessageProperty;
		CompiledBindingExtension compiledBindingExtension5 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, confirmationMessageProperty, compiledBindingExtension5);
		rootConfirmationControl4.ConfirmationButtonText = RootApp.Client.Avalonia.Resources.Strings.Resources.Kick;
		rootConfirmationControl4.CancelButtonText = RootApp.Client.Avalonia.Resources.Strings.Resources.Cancel;
		StyledProperty<string> svgPathProperty = RootConfirmationControl.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("UserKickedSVG");
		context.ProvideTargetProperty = RootConfirmationControl.SvgPathProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, svgPathProperty, binding);
		rootConfirmationControl4.SvgWidth = 24.0;
		rootConfirmationControl4.SvgHeight = 24.0;
		rootConfirmationControl4.WebApiSendingText = RootApp.Client.Avalonia.Resources.Strings.Resources.KickingMember;
		rootConfirmationControl4.WebApiSuccessText = RootApp.Client.Avalonia.Resources.Strings.Resources.SuccessfullyKickedMember;
		rootConfirmationControl4.WebApiFailedText = RootApp.Client.Avalonia.Resources.Strings.Resources.FailedToKickMember;
		StyledProperty<WebApiStatus> webApiStatusProperty = RootConfirmationControl.WebApiStatusProperty;
		CompiledBindingExtension compiledBindingExtension6 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EKickMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EWebApiStatus_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootConfirmationControl.WebApiStatusProperty;
		CompiledBindingExtension compiledBindingExtension7 = compiledBindingExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, webApiStatusProperty, compiledBindingExtension7);
		StyledProperty<ICommand?> confirmationCommandProperty = RootConfirmationControl.ConfirmationCommandProperty;
		CompiledBindingExtension compiledBindingExtension8 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EKickMemberViewModel_002CRootApp_002EClient_002EAvalonia_002EKickMemberCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootConfirmationControl.ConfirmationCommandProperty;
		CompiledBindingExtension compiledBindingExtension9 = compiledBindingExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, confirmationCommandProperty, compiledBindingExtension9);
		StyledProperty<ICommand?> closeViewModelCommandProperty = RootConfirmationControl.CloseViewModelCommandProperty;
		CompiledBindingExtension compiledBindingExtension10 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002ECommunity_002EMembers_002EKickMemberViewModel_002CRootApp_002EClient_002EAvalonia_002ECloseViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootConfirmationControl.CloseViewModelCommandProperty;
		CompiledBindingExtension compiledBindingExtension11 = compiledBindingExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, closeViewModelCommandProperty, compiledBindingExtension11);
		context.PopParent();
		((ISupportInitialize)rootConfirmationControl3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(KickMemberView P_0)
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

