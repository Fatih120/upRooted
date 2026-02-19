// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.EnterVerificationCodeView
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
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home;

public class EnterVerificationCodeView : UserControl
{
	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public EnterVerificationCodeView()
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
	private static void _0021XamlIlPopulate(IServiceProvider P_0, EnterVerificationCodeView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<EnterVerificationCodeView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<EnterVerificationCodeView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FHome_002FEnterVerificationCodeView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Home/EnterVerificationCodeView.axaml")
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
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EEnterVerificationCodeViewModel_002CRootApp_002EClient_002EAvalonia_002EMarkdownEngine_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootConfirmationControl.MarkdownEngineProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, markdownEngineProperty, compiledBindingExtension2);
		rootConfirmationControl4.TitleText = RootApp.Client.Avalonia.Resources.Strings.Resources.EmailValidation;
		StyledProperty<string> confirmationMessageProperty = RootConfirmationControl.ConfirmationMessageProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EEnterVerificationCodeViewModel_002CRootApp_002EClient_002EAvalonia_002EEmailValidationDescription_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootConfirmationControl.ConfirmationMessageProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, confirmationMessageProperty, compiledBindingExtension4);
		rootConfirmationControl4.ConfirmationButtonText = RootApp.Client.Avalonia.Resources.Strings.Resources.Verify;
		StyledProperty<IBrush> confirmationButtonBackgroundColorProperty = RootConfirmationControl.ConfirmationButtonBackgroundColorProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = RootConfirmationControl.ConfirmationButtonBackgroundColorProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, confirmationButtonBackgroundColorProperty, binding);
		StyledProperty<IBrush> confirmationButtonForegroundColorProperty = RootConfirmationControl.ConfirmationButtonForegroundColorProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = RootConfirmationControl.ConfirmationButtonForegroundColorProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, confirmationButtonForegroundColorProperty, binding2);
		rootConfirmationControl4.CancelButtonText = RootApp.Client.Avalonia.Resources.Strings.Resources.Cancel;
		StyledProperty<string> svgPathProperty = RootConfirmationControl.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("LockSVG");
		context.ProvideTargetProperty = RootConfirmationControl.SvgPathProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, svgPathProperty, binding3);
		rootConfirmationControl4.SvgWidth = 24.0;
		rootConfirmationControl4.SvgHeight = 24.0;
		rootConfirmationControl4.TypedConfirmationTitle = RootApp.Client.Avalonia.Resources.Strings.Resources.EnterYourUniqueVerificationCode;
		rootConfirmationControl4.TypedConfirmationPlaceholderText = RootApp.Client.Avalonia.Resources.Strings.Resources.VerificationCodePlaceholder;
		StyledProperty<string> confirmationTextBoxTextProperty = RootConfirmationControl.ConfirmationTextBoxTextProperty;
		CompiledBindingExtension obj = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EEnterVerificationCodeViewModel_002CRootApp_002EClient_002EAvalonia_002EConfirmationTextBoxText_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = RootConfirmationControl.ConfirmationTextBoxTextProperty;
		CompiledBindingExtension compiledBindingExtension5 = obj.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, confirmationTextBoxTextProperty, compiledBindingExtension5);
		rootConfirmationControl4.WebApiSendingText = RootApp.Client.Avalonia.Resources.Strings.Resources.VerifyingAccount;
		rootConfirmationControl4.WebApiSuccessText = RootApp.Client.Avalonia.Resources.Strings.Resources.SuccessfullyVerifiedAccount;
		rootConfirmationControl4.WebApiFailedText = RootApp.Client.Avalonia.Resources.Strings.Resources.FailedToVerifyAccount;
		StyledProperty<WebApiStatus> webApiStatusProperty = RootConfirmationControl.WebApiStatusProperty;
		CompiledBindingExtension compiledBindingExtension6 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EEnterVerificationCodeViewModel_002CRootApp_002EClient_002EAvalonia_002EWebApiStatus_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootConfirmationControl.WebApiStatusProperty;
		CompiledBindingExtension compiledBindingExtension7 = compiledBindingExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, webApiStatusProperty, compiledBindingExtension7);
		StyledProperty<ICommand?> confirmationCommandProperty = RootConfirmationControl.ConfirmationCommandProperty;
		CompiledBindingExtension compiledBindingExtension8 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EEnterVerificationCodeViewModel_002CRootApp_002EClient_002EAvalonia_002ESubmitVerificationCodeCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootConfirmationControl.ConfirmationCommandProperty;
		CompiledBindingExtension compiledBindingExtension9 = compiledBindingExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, confirmationCommandProperty, compiledBindingExtension9);
		StyledProperty<ICommand?> closeViewModelCommandProperty = RootConfirmationControl.CloseViewModelCommandProperty;
		CompiledBindingExtension compiledBindingExtension10 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002EEnterVerificationCodeViewModel_002CRootApp_002EClient_002EAvalonia_002ECloseViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
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
	private static void _0021XamlIlPopulateTrampoline(EnterVerificationCodeView P_0)
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

