using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Threading;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Resources.Enums;
using RootApp.Client.Avalonia.Resources.Strings;

namespace RootApp.Client.Avalonia.UI.Login;

public class LoginView : UserControl
{
	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Button LoginButton;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	private LoginViewModel _loginViewModel => (LoginViewModel)base.DataContext;

	public LoginView()
	{
		InitializeComponent();
	}

	protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs P_0)
	{
		base.OnAttachedToLogicalTree(P_0);
		_loginViewModel.PropertyChanged += onLoginViewModelPropertyChanged;
	}

	protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs P_0)
	{
		base.OnDetachedFromVisualTree(P_0);
		_loginViewModel.PropertyChanged -= onLoginViewModelPropertyChanged;
	}

	private void onLoginViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		Dispatcher.UIThread.Post(delegate
		{
			if (e.PropertyName == "IsTopMostViewModel")
			{
				LoginButton.IsDefault = _loginViewModel.IsTopMostViewModel;
			}
		});
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	[ExcludeFromCodeCoverage]
	public void InitializeComponent(bool P_0 = true)
	{
		if (P_0)
		{
			!XamlIlPopulateTrampoline(this);
		}
		LoginButton = this.FindNameScope()?.Find<Button>("LoginButton");
	}

	[CompilerGenerated]
	private static void !XamlIlPopulate(IServiceProvider P_0, LoginView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<LoginView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<LoginView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Login/LoginView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Login/LoginView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		StyledProperty<IBrush?> backgroundProperty = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(P_1, backgroundProperty, binding);
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		P_1.Content = panel;
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		global::Avalonia.Controls.Controls children = panel4.Children;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		children.Add(rootSvgImage);
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage5 = rootSvgImage4;
		StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("Pixels1SVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage5, svgPathProperty, binding2);
		rootSvgImage5.Margin = new Thickness(-600.0, -735.0, 0.0, 0.0);
		rootSvgImage5.Width = 48.0;
		rootSvgImage5.Height = 41.0;
		rootSvgImage5.HorizontalAlignment = HorizontalAlignment.Center;
		rootSvgImage5.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		global::Avalonia.Controls.Controls children2 = panel4.Children;
		RootSvgImage rootSvgImage7;
		RootSvgImage rootSvgImage6 = (rootSvgImage7 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage6).BeginInit();
		children2.Add(rootSvgImage6);
		RootSvgImage rootSvgImage8 = (rootSvgImage4 = rootSvgImage7);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage9 = rootSvgImage4;
		StyledProperty<string?> svgPathProperty2 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("Pixels2SVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage9, svgPathProperty2, binding3);
		rootSvgImage9.Margin = new Thickness(685.0, -670.0, 0.0, 0.0);
		rootSvgImage9.Width = 53.0;
		rootSvgImage9.Height = 59.0;
		rootSvgImage9.HorizontalAlignment = HorizontalAlignment.Center;
		rootSvgImage9.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)rootSvgImage8).EndInit();
		global::Avalonia.Controls.Controls children3 = panel4.Children;
		RootSvgImage rootSvgImage11;
		RootSvgImage rootSvgImage10 = (rootSvgImage11 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage10).BeginInit();
		children3.Add(rootSvgImage10);
		RootSvgImage rootSvgImage12 = (rootSvgImage4 = rootSvgImage11);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage13 = rootSvgImage4;
		StyledProperty<string?> svgPathProperty3 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("Pixels3SVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage13, svgPathProperty3, binding4);
		rootSvgImage13.Margin = new Thickness(790.0, 0.0, 0.0, 0.0);
		rootSvgImage13.Width = 70.0;
		rootSvgImage13.Height = 42.0;
		rootSvgImage13.HorizontalAlignment = HorizontalAlignment.Center;
		rootSvgImage13.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)rootSvgImage12).EndInit();
		global::Avalonia.Controls.Controls children4 = panel4.Children;
		RootSvgImage rootSvgImage15;
		RootSvgImage rootSvgImage14 = (rootSvgImage15 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage14).BeginInit();
		children4.Add(rootSvgImage14);
		RootSvgImage rootSvgImage16 = (rootSvgImage4 = rootSvgImage15);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage17 = rootSvgImage4;
		StyledProperty<string?> svgPathProperty4 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("Pixels4SVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage17, svgPathProperty4, binding5);
		rootSvgImage17.Margin = new Thickness(-800.0, 350.0, 0.0, 0.0);
		rootSvgImage17.Width = 52.0;
		rootSvgImage17.Height = 58.0;
		rootSvgImage17.HorizontalAlignment = HorizontalAlignment.Center;
		rootSvgImage17.VerticalAlignment = VerticalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)rootSvgImage16).EndInit();
		global::Avalonia.Controls.Controls children5 = panel4.Children;
		RootBorder rootBorder2;
		RootBorder rootBorder = (rootBorder2 = new RootBorder());
		((ISupportInitialize)rootBorder).BeginInit();
		children5.Add(rootBorder);
		RootBorder rootBorder4;
		RootBorder rootBorder3 = (rootBorder4 = rootBorder2);
		context.PushParent(rootBorder4);
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, backgroundProperty2, binding6);
		StyledProperty<IBrush?> borderBrushProperty = Border.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BorderBrushProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootBorder4, borderBrushProperty, binding7);
		rootBorder4.DynamicBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootBorder4.Width = 700.0;
		rootBorder4.Height = 548.0;
		rootBorder4.CornerRadius = new CornerRadius(24.0, 24.0, 24.0, 24.0);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		rootBorder4.Child = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		RowDefinitions rowDefinitions = new RowDefinitions();
		rowDefinitions.Capacity = 2;
		rowDefinitions.Add(new RowDefinition(new GridLength(1.0, GridUnitType.Star)));
		rowDefinitions.Add(new RowDefinition(new GridLength(0.0, GridUnitType.Auto)));
		grid4.RowDefinitions = rowDefinitions;
		global::Avalonia.Controls.Controls children6 = grid4.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children6.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		Grid.SetRow(stackPanel5, 0);
		stackPanel5.VerticalAlignment = VerticalAlignment.Top;
		stackPanel5.HorizontalAlignment = HorizontalAlignment.Center;
		stackPanel5.Width = 480.0;
		global::Avalonia.Controls.Controls children7 = stackPanel5.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children7.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Welcome;
		textBlock5.Margin = new Thickness(0.0, 44.0, 0.0, 44.0);
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock5, obj);
		textBlock5.FontWeight = (FontWeight)450;
		textBlock5.FontSize = 42.0;
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding8);
		textBlock5.HorizontalAlignment = HorizontalAlignment.Center;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		global::Avalonia.Controls.Controls children8 = stackPanel5.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children8.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Username;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock9, obj2);
		textBlock9.FontWeight = (FontWeight)450;
		textBlock9.FontSize = 14.0;
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding9);
		textBlock9.Margin = new Thickness(0.0, 0.0, 0.0, 8.0);
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		global::Avalonia.Controls.Controls children9 = stackPanel5.Children;
		RootTextbox rootTextbox2;
		RootTextbox rootTextbox = (rootTextbox2 = new RootTextbox());
		((ISupportInitialize)rootTextbox).BeginInit();
		children9.Add(rootTextbox);
		RootTextbox rootTextbox4;
		RootTextbox rootTextbox3 = (rootTextbox4 = rootTextbox2);
		context.PushParent(rootTextbox4);
		RootTextbox rootTextbox5 = rootTextbox4;
		StyledProperty<string> textProperty = RootTextbox.TextProperty;
		CompiledBindingExtension obj3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Login.LoginViewModel,RootApp.Client.Avalonia.Username!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = RootTextbox.TextProperty;
		CompiledBindingExtension compiledBindingExtension = obj3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox5, textProperty, compiledBindingExtension);
		rootTextbox5.ValidationPropertyName = "Username";
		rootTextbox5.Margin = new Thickness(0.0, 0.0, 0.0, 24.0);
		rootTextbox5.BorderHeight = 52.0;
		StyledProperty<IBrush> borderBackgroundBrushProperty = RootTextbox.BorderBackgroundBrushProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("Input");
		context.ProvideTargetProperty = RootTextbox.BorderBackgroundBrushProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox5, borderBackgroundBrushProperty, binding10);
		rootTextbox5.BorderCornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		StyledProperty<IBrush> borderBorderBrushProperty = RootTextbox.BorderBorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = RootTextbox.BorderBorderBrushProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox5, borderBorderBrushProperty, binding11);
		rootTextbox5.BorderBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootTextbox5.TextboxFontSize = 15.0;
		rootTextbox5.TextboxMargin = new Thickness(16.0, 0.0, 36.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rootTextbox3).EndInit();
		global::Avalonia.Controls.Controls children10 = stackPanel5.Children;
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		children10.Add(textBlock10);
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		textBlock13.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Password;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj4 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock13, obj4);
		textBlock13.FontSize = 14.0;
		textBlock13.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding12 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, foregroundProperty3, binding12);
		textBlock13.FontWeight = (FontWeight)450;
		textBlock13.Margin = new Thickness(0.0, 0.0, 0.0, 8.0);
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		global::Avalonia.Controls.Controls children11 = stackPanel5.Children;
		RootTextbox rootTextbox7;
		RootTextbox rootTextbox6 = (rootTextbox7 = new RootTextbox());
		((ISupportInitialize)rootTextbox6).BeginInit();
		children11.Add(rootTextbox6);
		RootTextbox rootTextbox8 = (rootTextbox4 = rootTextbox7);
		context.PushParent(rootTextbox4);
		RootTextbox rootTextbox9 = rootTextbox4;
		StyledProperty<string> textProperty2 = RootTextbox.TextProperty;
		CompiledBindingExtension obj5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Login.LoginViewModel,RootApp.Client.Avalonia.Password!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build())
		{
			Mode = BindingMode.TwoWay
		};
		context.ProvideTargetProperty = RootTextbox.TextProperty;
		CompiledBindingExtension compiledBindingExtension2 = obj5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox9, textProperty2, compiledBindingExtension2);
		rootTextbox9.ValidationPropertyName = "Password";
		rootTextbox9.IsPasswordTextbox = true;
		rootTextbox9.Margin = new Thickness(0.0, 0.0, 0.0, 24.0);
		rootTextbox9.BorderHeight = 52.0;
		StyledProperty<IBrush> borderBackgroundBrushProperty2 = RootTextbox.BorderBackgroundBrushProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("Input");
		context.ProvideTargetProperty = RootTextbox.BorderBackgroundBrushProperty;
		IBinding binding13 = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox9, borderBackgroundBrushProperty2, binding13);
		rootTextbox9.BorderCornerRadius = new CornerRadius(12.0, 12.0, 12.0, 12.0);
		StyledProperty<IBrush> borderBorderBrushProperty2 = RootTextbox.BorderBorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = RootTextbox.BorderBorderBrushProperty;
		IBinding binding14 = dynamicResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootTextbox9, borderBorderBrushProperty2, binding14);
		rootTextbox9.BorderBorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		rootTextbox9.TextboxFontSize = 15.0;
		rootTextbox9.TextboxMargin = new Thickness(16.0, 0.0, 36.0, 0.0);
		context.PopParent();
		((ISupportInitialize)rootTextbox8).EndInit();
		global::Avalonia.Controls.Controls children12 = stackPanel5.Children;
		RootLinkButton rootLinkButton2;
		RootLinkButton rootLinkButton = (rootLinkButton2 = new RootLinkButton());
		((ISupportInitialize)rootLinkButton).BeginInit();
		children12.Add(rootLinkButton);
		RootLinkButton rootLinkButton4;
		RootLinkButton rootLinkButton3 = (rootLinkButton4 = rootLinkButton2);
		context.PushParent(rootLinkButton4);
		RootLinkButton rootLinkButton5 = rootLinkButton4;
		rootLinkButton5.Content = RootApp.Client.Avalonia.Resources.Strings.Resources.ForgotUsernameOrPassword;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj6 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_3(rootLinkButton5, obj6);
		rootLinkButton5.FontWeight = (FontWeight)450;
		rootLinkButton5.HorizontalAlignment = HorizontalAlignment.Right;
		rootLinkButton5.FontSize = 14.0;
		rootLinkButton5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty4 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension15 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding15 = dynamicResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootLinkButton5, foregroundProperty4, binding15);
		rootLinkButton5.Margin = new Thickness(0.0, 0.0, 0.0, 24.0);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Login.LoginViewModel,RootApp.Client.Avalonia.ShowForgotUsernameOrPasswordPickerViewModelCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootLinkButton5, commandProperty, compiledBindingExtension4);
		context.PopParent();
		((ISupportInitialize)rootLinkButton3).EndInit();
		global::Avalonia.Controls.Controls children13 = stackPanel5.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children13.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		button4.Name = "LoginButton";
		object obj7 = button4;
		context.AvaloniaNameScope.Register("LoginButton", obj7);
		button4.Classes.Add("BorderButton");
		button4.Height = 52.0;
		button4.IsDefault = true;
		StyledProperty<IBrush?> backgroundProperty3 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension16 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding16 = dynamicResourceExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, backgroundProperty3, binding16);
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj8 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_3(button4, obj8);
		button4.FontWeight = FontWeight.Medium;
		button4.Content = RootApp.Client.Avalonia.Resources.Strings.Resources.Login;
		StyledProperty<IBrush?> borderBrushProperty2 = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension17 = new DynamicResourceExtension("TextTertiary");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding17 = dynamicResourceExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, borderBrushProperty2, binding17);
		StyledProperty<IBrush?> foregroundProperty5 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension18 = new DynamicResourceExtension("BackgroundTertiary");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding18 = dynamicResourceExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, foregroundProperty5, binding18);
		button4.BorderThickness = new Thickness(0.0, 0.0, 0.0, 0.0);
		button4.FontSize = 16.0;
		button4.Margin = new Thickness(0.0, 0.0, 0.0, 24.0);
		button4.CornerRadius = new CornerRadius(20.0, 20.0, 20.0, 20.0);
		StyledProperty<ICommand?> commandProperty2 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension5 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Login.LoginViewModel,RootApp.Client.Avalonia.LoginCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension6 = compiledBindingExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button4, commandProperty2, compiledBindingExtension6);
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		global::Avalonia.Controls.Controls children14 = stackPanel5.Children;
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		children14.Add(stackPanel6);
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		stackPanel9.Orientation = Orientation.Horizontal;
		stackPanel9.HorizontalAlignment = HorizontalAlignment.Center;
		global::Avalonia.Controls.Controls children15 = stackPanel9.Children;
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		children15.Add(textBlock14);
		TextBlock textBlock16 = (textBlock4 = textBlock15);
		context.PushParent(textBlock4);
		TextBlock textBlock17 = textBlock4;
		textBlock17.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.DontHaveAnAccountWithQuestionMark;
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj9 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock17, obj9);
		textBlock17.FontWeight = (FontWeight)450;
		textBlock17.HorizontalAlignment = HorizontalAlignment.Right;
		textBlock17.FontSize = 14.0;
		textBlock17.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty6 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension19 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding19 = dynamicResourceExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock17, foregroundProperty6, binding19);
		textBlock17.Margin = new Thickness(0.0, 0.0, 4.0, 0.0);
		context.PopParent();
		((ISupportInitialize)textBlock16).EndInit();
		global::Avalonia.Controls.Controls children16 = stackPanel9.Children;
		RootLinkButton rootLinkButton7;
		RootLinkButton rootLinkButton6 = (rootLinkButton7 = new RootLinkButton());
		((ISupportInitialize)rootLinkButton6).BeginInit();
		children16.Add(rootLinkButton6);
		RootLinkButton rootLinkButton8 = (rootLinkButton4 = rootLinkButton7);
		context.PushParent(rootLinkButton4);
		RootLinkButton rootLinkButton9 = rootLinkButton4;
		rootLinkButton9.Content = RootApp.Client.Avalonia.Resources.Strings.Resources.Register;
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TemplatedControl.FontFamilyProperty;
		object? obj10 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_3(rootLinkButton9, obj10);
		rootLinkButton9.FontWeight = FontWeight.Medium;
		rootLinkButton9.FontStyle = FontStyle.Italic;
		rootLinkButton9.HorizontalAlignment = HorizontalAlignment.Right;
		rootLinkButton9.FontSize = 14.0;
		rootLinkButton9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty7 = TemplatedControl.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension20 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TemplatedControl.ForegroundProperty;
		IBinding binding20 = dynamicResourceExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootLinkButton9, foregroundProperty7, binding20);
		StyledProperty<ICommand?> commandProperty3 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Login.LoginViewModel,RootApp.Client.Avalonia.ShowRegisterViewModelCommand!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootLinkButton9, commandProperty3, compiledBindingExtension8);
		context.PopParent();
		((ISupportInitialize)rootLinkButton8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		global::Avalonia.Controls.Controls children17 = grid4.Children;
		RootWebApiStatus rootWebApiStatus2;
		RootWebApiStatus rootWebApiStatus = (rootWebApiStatus2 = new RootWebApiStatus());
		((ISupportInitialize)rootWebApiStatus).BeginInit();
		children17.Add(rootWebApiStatus);
		RootWebApiStatus rootWebApiStatus4;
		RootWebApiStatus rootWebApiStatus3 = (rootWebApiStatus4 = rootWebApiStatus2);
		context.PushParent(rootWebApiStatus4);
		Grid.SetRow(rootWebApiStatus4, 1);
		StyledProperty<WebApiStatus> webApiStatusProperty = RootWebApiStatus.WebApiStatusProperty;
		CompiledBindingExtension compiledBindingExtension9 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Login.LoginViewModel,RootApp.Client.Avalonia.WebApiStatus!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = RootWebApiStatus.WebApiStatusProperty;
		CompiledBindingExtension compiledBindingExtension10 = compiledBindingExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootWebApiStatus4, webApiStatusProperty, compiledBindingExtension10);
		rootWebApiStatus4.SendingText = RootApp.Client.Avalonia.Resources.Strings.Resources.LoggingIn;
		rootWebApiStatus4.SuccessText = RootApp.Client.Avalonia.Resources.Strings.Resources.SuccessfullLogin;
		rootWebApiStatus4.FailedText = RootApp.Client.Avalonia.Resources.Strings.Resources.FailedLogin;
		rootWebApiStatus4.BorderCornerRadius = new CornerRadius(0.0, 0.0, 24.0, 24.0);
		context.PopParent();
		((ISupportInitialize)rootWebApiStatus3).EndInit();
		context.PopParent();
		((ISupportInitialize)grid3).EndInit();
		context.PopParent();
		((ISupportInitialize)rootBorder3).EndInit();
		context.PopParent();
		((ISupportInitialize)panel3).EndInit();
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void !XamlIlPopulateTrampoline(LoginView P_0)
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
