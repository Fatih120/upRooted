// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Main.ConnectionBlockingView
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.UI.Main;

public class ConnectionBlockingView : UserControl
{
	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border BackgroundBorder;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public ConnectionBlockingView()
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
		BackgroundBorder = this.FindNameScope()?.Find<Border>("BackgroundBorder");
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulate(IServiceProvider P_0, ConnectionBlockingView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<ConnectionBlockingView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ConnectionBlockingView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FMain_002FConnectionBlockingView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Main/ConnectionBlockingView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		P_1.Content = border;
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		border4.Name = "BackgroundBorder";
		object obj = border4;
		context.AvaloniaNameScope.Register("BackgroundBorder", obj);
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("DropShadow");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border4, backgroundProperty, binding);
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(ConnectionBlockingView P_0)
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

