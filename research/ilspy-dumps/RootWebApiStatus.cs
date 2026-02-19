// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootWebApiStatus
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Skia.Lottie;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Resources.Enums;

public class RootWebApiStatus : UserControl
{
	public static readonly StyledProperty<WebApiStatus> WebApiStatusProperty = AvaloniaProperty.Register<RootWebApiStatus, WebApiStatus>("WebApiStatus", WebApiStatus.Default);

	public static readonly StyledProperty<string> SendingTextProperty = AvaloniaProperty.Register<RootWebApiStatus, string>("SendingText");

	public static readonly StyledProperty<string> SuccessTextProperty = AvaloniaProperty.Register<RootWebApiStatus, string>("SuccessText");

	public static readonly StyledProperty<string> FailedTextProperty = AvaloniaProperty.Register<RootWebApiStatus, string>("FailedText");

	public static readonly StyledProperty<CornerRadius> BorderCornerRadiusProperty = AvaloniaProperty.Register<RootWebApiStatus, CornerRadius>("BorderCornerRadius", new CornerRadius(0.0, 0.0, 12.0, 12.0));

	public static readonly StyledProperty<ICommand?> CloseCommandProperty = AvaloniaProperty.Register<Button, ICommand>("CloseCommand", null, false, BindingMode.OneWay, null, null, true);

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal UserControl RootWebApiStatusControl;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border BackgroundBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Lottie LoadingSpinner;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock StatusTextBlock;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public CornerRadius BorderCornerRadius
	{
		set
		{
			SetValue(BorderCornerRadiusProperty, value2);
		}
	}

	public string SendingText
	{
		get
		{
			return GetValue(SendingTextProperty);
		}
		set
		{
			SetValue(SendingTextProperty, value2);
		}
	}

	public string SuccessText
	{
		get
		{
			return GetValue(SuccessTextProperty);
		}
		set
		{
			SetValue(SuccessTextProperty, value2);
		}
	}

	public string FailedText
	{
		get
		{
			return GetValue(FailedTextProperty);
		}
		set
		{
			SetValue(FailedTextProperty, value2);
		}
	}

	public ICommand? CloseCommand => GetValue(CloseCommandProperty);

	protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs P_0)
	{
		base.OnPropertyChanged(P_0);
		if (P_0.Property == WebApiStatusProperty)
		{
			handleWebApiStatusChangeAsync(P_0.GetNewValue<WebApiStatus>());
		}
	}

	public RootWebApiStatus()
	{
		InitializeComponent();
	}

	private async Task handleWebApiStatusChangeAsync(WebApiStatus P_0)
	{
		switch (P_0)
		{
		case WebApiStatus.Sending:
			StatusTextBlock.Text = SendingText;
			StatusTextBlock.Foreground = Application.Current.FindResource(Application.Current.ActualThemeVariant, "TextPrimary") as IBrush;
			BackgroundBorder.Background = Application.Current.FindResource(Application.Current.ActualThemeVariant, "BackgroundTertiary") as IBrush;
			LoadingSpinner.Path = Application.Current.FindResource(Application.Current.ActualThemeVariant, "LoadingSpinnerPath").ToString();
			LoadingSpinner.IsVisible = true;
			base.IsVisible = true;
			break;
		case WebApiStatus.Success:
			StatusTextBlock.Text = SuccessText;
			StatusTextBlock.Foreground = Application.Current.FindResource(Application.Current.ActualThemeVariant, "TextWhite") as IBrush;
			BackgroundBorder.Background = Application.Current.FindResource(Application.Current.ActualThemeVariant, "BrandTertiary") as IBrush;
			LoadingSpinner.IsVisible = false;
			LoadingSpinner.Stop();
			base.IsVisible = true;
			await Task.Delay(300);
			base.IsVisible = false;
			CloseCommand?.Execute(null);
			break;
		case WebApiStatus.Failed:
			StatusTextBlock.Text = FailedText;
			StatusTextBlock.Foreground = Application.Current.FindResource(Application.Current.ActualThemeVariant, "TextWhite") as IBrush;
			BackgroundBorder.Background = Application.Current.FindResource(Application.Current.ActualThemeVariant, "Error") as IBrush;
			LoadingSpinner.IsVisible = false;
			LoadingSpinner.Stop();
			base.IsVisible = true;
			break;
		case WebApiStatus.Default:
			LoadingSpinner.IsVisible = false;
			LoadingSpinner.Stop();
			base.IsVisible = false;
			break;
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
		RootWebApiStatusControl = nameScope?.Find<UserControl>("RootWebApiStatusControl");
		BackgroundBorder = nameScope?.Find<Border>("BackgroundBorder");
		LoadingSpinner = nameScope?.Find<Lottie>("LoadingSpinner");
		StatusTextBlock = nameScope?.Find<TextBlock>("StatusTextBlock");
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulate(IServiceProvider P_0, RootWebApiStatus P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<RootWebApiStatus> context = new CompiledAvaloniaXaml.XamlIlContext.Context<RootWebApiStatus>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FControls_002FRootWebApiStatus_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Controls/RootWebApiStatus.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.Name = "RootWebApiStatusControl";
		object obj = P_1;
		context.AvaloniaNameScope.Register("RootWebApiStatusControl", obj);
		P_1.IsVisible = false;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		P_1.Content = border;
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		border4.Name = "BackgroundBorder";
		obj = border4;
		context.AvaloniaNameScope.Register("BackgroundBorder", obj);
		StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border4, backgroundProperty, binding);
		StyledProperty<CornerRadius> cornerRadiusProperty = Border.CornerRadiusProperty;
		CompiledBindingExtension obj2 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).ElementName(context.AvaloniaNameScope, "RootWebApiStatusControl").Property(BorderCornerRadiusProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = Border.CornerRadiusProperty;
		CompiledBindingExtension compiledBindingExtension = obj2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border4, cornerRadiusProperty, compiledBindingExtension);
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		border4.Child = stackPanel;
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		stackPanel4.Orientation = Orientation.Horizontal;
		stackPanel4.HorizontalAlignment = HorizontalAlignment.Center;
		stackPanel4.Height = 28.0;
		Controls children = stackPanel4.Children;
		Lottie lottie2;
		Lottie lottie = (lottie2 = new Lottie(context));
		((ISupportInitialize)lottie).BeginInit();
		children.Add(lottie);
		lottie2.Name = "LoadingSpinner";
		obj = lottie2;
		context.AvaloniaNameScope.Register("LoadingSpinner", obj);
		lottie2.Width = 17.0;
		lottie2.Height = 17.0;
		lottie2.Margin = new Thickness(0.0, 0.0, 12.0, 0.0);
		((ISupportInitialize)lottie2).EndInit();
		Controls children2 = stackPanel4.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children2.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		textBlock4.Name = "StatusTextBlock";
		obj = textBlock4;
		context.AvaloniaNameScope.Register("StatusTextBlock", obj);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj3 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock4, obj3);
		textBlock4.FontSize = 13.0;
		textBlock4.FontWeight = (FontWeight)450;
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock4, foregroundProperty, binding2);
		textBlock4.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		textBlock4.VerticalAlignment = VerticalAlignment.Center;
		textBlock4.HorizontalAlignment = HorizontalAlignment.Left;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
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
	private static void _0021XamlIlPopulateTrampoline(RootWebApiStatus P_0)
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

