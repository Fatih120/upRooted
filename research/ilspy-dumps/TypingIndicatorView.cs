using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Resources.Themes;
using RootApp.Client.CoreDomain.Models.Community;

namespace RootApp.Client.Avalonia.UI.Messages;

public class TypingIndicatorView : UserControl
{
	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal TextBlock UsernameTextBlock;

	[CompilerGenerated]
	private static Action<object> !XamlIlPopulateOverride;

	private TypingIndicatorViewModel? _typingIndicatorViewModel => base.DataContext as TypingIndicatorViewModel;

	public TypingIndicatorView()
	{
		InitializeComponent();
	}

	protected override void OnDataContextChanged(EventArgs P_0)
	{
		base.OnDataContextChanged(P_0);
		updateMemberColor();
	}

	private void updateMemberColor()
	{
		TypingIndicatorViewModel typingIndicatorViewModel = _typingIndicatorViewModel;
		if (typingIndicatorViewModel == null || typingIndicatorViewModel.MessageContainerMember == null || !(_typingIndicatorViewModel.MessageContainerMember is Member member))
		{
			return;
		}
		Role primaryRole = member.Roles.PrimaryRole;
		if (primaryRole != null && !string.IsNullOrEmpty(primaryRole.RoleColorHex))
		{
			if (ThemeService.IsDefaultColor(primaryRole.RoleColorHex))
			{
				UsernameTextBlock.Foreground = new SolidColorBrush(Color.Parse(ThemeService.GetInvertedDefaultColorHex(primaryRole.RoleColorHex)));
			}
			else
			{
				UsernameTextBlock.Foreground = new SolidColorBrush(Color.Parse(primaryRole.RoleColorHex));
			}
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
		UsernameTextBlock = this.FindNameScope()?.Find<TextBlock>("UsernameTextBlock");
	}

	[CompilerGenerated]
	private static void !XamlIlPopulate(IServiceProvider P_0, TypingIndicatorView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<TypingIndicatorView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<TypingIndicatorView>(P_0, new object[1] { !AvaloniaResources.NamespaceInfo:/UI/Messages/TypingIndicatorView.axaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Messages/TypingIndicatorView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		P_1.Margin = new Thickness(0.0, 0.0, 8.0, 0.0);
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		P_1.Content = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(1.0, GridUnitType.Star)
		});
		grid4.ColumnDefinitions.Add(new ColumnDefinition
		{
			Width = new GridLength(0.0, GridUnitType.Auto)
		});
		global::Avalonia.Controls.Controls children = grid4.Children;
		RootImageLoader rootImageLoader2;
		RootImageLoader rootImageLoader = (rootImageLoader2 = new RootImageLoader());
		((ISupportInitialize)rootImageLoader).BeginInit();
		children.Add(rootImageLoader);
		RootImageLoader rootImageLoader4;
		RootImageLoader rootImageLoader3 = (rootImageLoader4 = rootImageLoader2);
		context.PushParent(rootImageLoader4);
		Grid.SetColumn(rootImageLoader4, 0);
		rootImageLoader4.Width = 20.0;
		rootImageLoader4.Height = 20.0;
		rootImageLoader4.Margin = new Thickness(0.0, 0.0, 8.0, 0.0);
		rootImageLoader4.CornerRadius = new CornerRadius(4.0, 4.0, 4.0, 4.0);
		StyledProperty<IBrush?> backgroundProperty = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("HighlightNormal");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, backgroundProperty, binding);
		StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.TypingIndicatorViewModel,RootApp.Client.Avalonia.ProfilePictureAsyncBitmapWrapper!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
		context.ProvideTargetProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, sourceProperty, compiledBindingExtension2);
		rootImageLoader4.LoadingPlaceholderSize = 10.0;
		context.PopParent();
		((ISupportInitialize)rootImageLoader3).EndInit();
		global::Avalonia.Controls.Controls children2 = grid4.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children2.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		textBlock5.Name = "UsernameTextBlock";
		object obj = textBlock5;
		context.AvaloniaNameScope.Register("UsernameTextBlock", obj);
		Grid.SetColumn(textBlock5, 1);
		textBlock5.VerticalAlignment = VerticalAlignment.Center;
		textBlock5.FontSize = 13.0;
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj2 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock5, obj2);
		textBlock5.FontWeight = FontWeight.Medium;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.Avalonia.UI.Messages.TypingIndicatorViewModel,RootApp.Client.Avalonia.GlobalUser!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp.Client.CoreDomain.Models.User.GlobalUser,RootApp.Client.CoreDomain.UserName!Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, compiledBindingExtension4);
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding2);
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		global::Avalonia.Controls.Controls children3 = grid4.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children3.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		Grid.SetColumn(textBlock9, 2);
		textBlock9.VerticalAlignment = VerticalAlignment.Center;
		textBlock9.FontSize = 13.0;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj3 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters.<>XamlDynamicSetter_1(textBlock9, obj3);
		textBlock9.FontWeight = (FontWeight)450;
		textBlock9.Text = " is typing";
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("TextSecondary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding3);
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
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
	private static void !XamlIlPopulateTrampoline(TypingIndicatorView P_0)
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
