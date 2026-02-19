// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.PrivacyBlockedActionView
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home;

public class PrivacyBlockedActionView : UserControl
{
	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public PrivacyBlockedActionView()
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
	private static void _0021XamlIlPopulate(IServiceProvider P_0, PrivacyBlockedActionView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<PrivacyBlockedActionView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<PrivacyBlockedActionView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FHome_002FPrivacyBlockedActionView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Home/PrivacyBlockedActionView.axaml")
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
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EPrivacyBlockedActionViewModel_002CRootApp_002EClient_002EAvalonia_002EMarkdownEngine_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootConfirmationControl.MarkdownEngineProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, markdownEngineProperty, compiledBindingExtension2);
		rootConfirmationControl4.TitleText = RootApp.Client.Avalonia.Resources.Strings.Resources.PrivacyBlockedActionTitle;
		StyledProperty<string> confirmationMessageProperty = RootConfirmationControl.ConfirmationMessageProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EPrivacyBlockedActionViewModel_002CRootApp_002EClient_002EAvalonia_002EErrorMessage_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootConfirmationControl.ConfirmationMessageProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, confirmationMessageProperty, compiledBindingExtension4);
		StyledProperty<string> svgPathProperty = RootConfirmationControl.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("InfoSVG");
		context.ProvideTargetProperty = RootConfirmationControl.SvgPathProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, svgPathProperty, binding);
		rootConfirmationControl4.SvgWidth = 24.0;
		rootConfirmationControl4.SvgHeight = 24.0;
		StyledProperty<ICommand?> closeViewModelCommandProperty = RootConfirmationControl.CloseViewModelCommandProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EPrivacyBlockedActionViewModel_002CRootApp_002EClient_002EAvalonia_002ECloseViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootConfirmationControl.CloseViewModelCommandProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, closeViewModelCommandProperty, compiledBindingExtension6);
		StyledProperty<ICommand?> confirmationCommandProperty = RootConfirmationControl.ConfirmationCommandProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EPrivacyBlockedActionViewModel_002CRootApp_002EClient_002EAvalonia_002ECloseViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootConfirmationControl.ConfirmationCommandProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, confirmationCommandProperty, compiledBindingExtension8);
		rootConfirmationControl4.ConfirmationButtonText = RootApp.Client.Avalonia.Resources.Strings.Resources.OK;
		StyledProperty<IBrush> confirmationButtonForegroundColorProperty = RootConfirmationControl.ConfirmationButtonForegroundColorProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = RootConfirmationControl.ConfirmationButtonForegroundColorProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, confirmationButtonForegroundColorProperty, binding2);
		StyledProperty<IBrush> confirmationButtonBackgroundColorProperty = RootConfirmationControl.ConfirmationButtonBackgroundColorProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = RootConfirmationControl.ConfirmationButtonBackgroundColorProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, confirmationButtonBackgroundColorProperty, binding3);
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
	private static void _0021XamlIlPopulateTrampoline(PrivacyBlockedActionView P_0)
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

