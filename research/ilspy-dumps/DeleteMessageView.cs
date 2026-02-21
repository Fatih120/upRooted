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
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Markdown;
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.Avalonia.Resources.Strings;

namespace RootApp.Client.Avalonia.UI.Messages;

public class DeleteMessageView : UserControl
{
	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	public DeleteMessageView()
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
	private static void !XamlIlPopulate(IServiceProvider P_0, DeleteMessageView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<DeleteMessageView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<DeleteMessageView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Messages/DeleteMessageView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Messages/DeleteMessageView.axaml")
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
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.DeleteMessageViewModel,RootApp.Client.Avalonia.MarkdownEngine!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootConfirmationControl.MarkdownEngineProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, markdownEngineProperty, compiledBindingExtension2);
		rootConfirmationControl4.TitleText = RootApp.Client.Avalonia.Resources.Strings.Resources.DeleteMessage;
		StyledProperty<string> confirmationMessageProperty = RootConfirmationControl.ConfirmationMessageProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.DeleteMessageViewModel,RootApp.Client.Avalonia.ConfirmationMessage!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootConfirmationControl.ConfirmationMessageProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, confirmationMessageProperty, compiledBindingExtension4);
		rootConfirmationControl4.ConfirmationButtonText = RootApp.Client.Avalonia.Resources.Strings.Resources.Delete;
		rootConfirmationControl4.CancelButtonText = RootApp.Client.Avalonia.Resources.Strings.Resources.Cancel;
		StyledProperty<string> svgPathProperty = RootConfirmationControl.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("RemoveSVG");
		context.ProvideTargetProperty = RootConfirmationControl.SvgPathProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, svgPathProperty, binding);
		rootConfirmationControl4.SvgWidth = 19.0;
		rootConfirmationControl4.SvgHeight = 24.0;
		rootConfirmationControl4.WebApiSendingText = RootApp.Client.Avalonia.Resources.Strings.Resources.DeletingMessage;
		rootConfirmationControl4.WebApiSuccessText = RootApp.Client.Avalonia.Resources.Strings.Resources.SuccessfullyDeletedMessage;
		rootConfirmationControl4.WebApiFailedText = RootApp.Client.Avalonia.Resources.Strings.Resources.FailedToDeleteMessage;
		StyledProperty<WebApiStatus> webApiStatusProperty = RootConfirmationControl.WebApiStatusProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.DeleteMessageViewModel,RootApp.Client.Avalonia.WebApiStatus!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootConfirmationControl.WebApiStatusProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, webApiStatusProperty, compiledBindingExtension6);
		StyledProperty<ICommand?> confirmationCommandProperty = RootConfirmationControl.ConfirmationCommandProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.DeleteMessageViewModel,RootApp.Client.Avalonia.DeleteMessageCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootConfirmationControl.ConfirmationCommandProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, confirmationCommandProperty, compiledBindingExtension8);
		StyledProperty<ICommand?> closeViewModelCommandProperty = RootConfirmationControl.CloseViewModelCommandProperty;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.DeleteMessageViewModel,RootApp.Client.Avalonia.CloseViewModelCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootConfirmationControl.CloseViewModelCommandProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootConfirmationControl4, closeViewModelCommandProperty, compiledBindingExtension10);
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
	private static void !XamlIlPopulateTrampoline(DeleteMessageView P_0)
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
