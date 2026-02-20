// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.UI.Home.SystemTray.Profile.ProfileView
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.MarkupExtensions.CompiledBindings;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CompiledAvaloniaXaml;
using Microsoft.VisualStudio.Threading;
using RootApp.Client.Avalonia.Controls;
using RootApp.Client.Avalonia.Helpers.Caching;
using RootApp.Client.Avalonia.Resources.Converters.GlobalUsers;
using RootApp.Client.Avalonia.Resources.Converters.Installation;
using RootApp.Client.Avalonia.Resources.Strings;
using RootApp.Client.Avalonia.UI.Home.SystemTray.Profile;
using RootApp.WebApi.Shared.Enums;

public class ProfileView : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_160
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ProfileView> context = CreateContext(P_0);
			return new GlobalUserOnlineStatusConverter();
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<ProfileView> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ProfileView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ProfileView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FHome_002FSystemTray_002FProfile_002FProfileView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Home/SystemTray/Profile/ProfileView.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (ProfileView)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ProfileView> context = CreateContext(P_0);
			return new UserStatusStringToColorConverter();
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ProfileView> context = CreateContext(P_0);
			return new DownloadVisibilityConverter();
		}

		public static object Build_4(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ProfileView> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			StackPanel stackPanel = (StackPanel)obj;
			context.PushParent(stackPanel);
			stackPanel.Orientation = Orientation.Horizontal;
			Controls children = stackPanel.Children;
			Border border2;
			Border border = (border2 = new Border());
			((ISupportInitialize)border).BeginInit();
			children.Add(border);
			Border border4;
			Border border3 = (border4 = border2);
			context.PushParent(border4);
			border4.Width = 8.0;
			border4.Height = 8.0;
			border4.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
			StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
			MultiBinding multiBinding2;
			MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
			context.PushParent(multiBinding2);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("UserStatusStringToColorConverter");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
			object? obj2 = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			multiBinding2.Converter = (IMultiValueConverter)obj2;
			IList<IBinding> bindings = multiBinding2.Bindings;
			CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension();
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
			CompiledBindingExtension item = compiledBindingExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			bindings.Add(item);
			IList<IBinding> bindings2 = multiBinding2.Bindings;
			CompiledBindingExtension compiledBindingExtension2 = new CompiledBindingExtension();
			compiledBindingExtension2.Path = new CompiledBindingPathBuilder(1).Property(Application.ActualThemeVariantProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
			compiledBindingExtension2.Source = Application.Current;
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
			CompiledBindingExtension item2 = compiledBindingExtension2.ProvideValue(context);
			context.ProvideTargetProperty = null;
			bindings2.Add(item2);
			context.PopParent();
			AvaloniaObjectExtensions.Bind(border4, backgroundProperty, multiBinding);
			context.PopParent();
			((ISupportInitialize)border3).EndInit();
			Controls children2 = stackPanel.Children;
			TextBlock textBlock2;
			TextBlock textBlock = (textBlock2 = new TextBlock());
			((ISupportInitialize)textBlock).BeginInit();
			children2.Add(textBlock);
			TextBlock textBlock4;
			TextBlock textBlock3 = (textBlock4 = textBlock2);
			context.PushParent(textBlock4);
			StyledProperty<string?> textProperty = TextBlock.TextProperty;
			CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension();
			context.ProvideTargetProperty = TextBlock.TextProperty;
			CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(textBlock4, textProperty, compiledBindingExtension4);
			textBlock4.Margin = new Thickness(8.0, 0.0, 0.0, 0.0);
			textBlock4.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
			StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
			DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
			context.ProvideTargetProperty = TextBlock.ForegroundProperty;
			IBinding binding = dynamicResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(textBlock4, foregroundProperty, binding);
			textBlock4.FontSize = 14.0;
			StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
			context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
			object? obj3 = staticResourceExtension2.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock4, obj3);
			textBlock4.FontWeight = (FontWeight)450;
			context.PopParent();
			((ISupportInitialize)textBlock3).EndInit();
			context.PopParent();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static object Build_5(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<ProfileView> context = CreateContext(P_0);
			context.IntermediateRoot = new StackPanel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			StackPanel stackPanel = (StackPanel)obj;
			context.PushParent(stackPanel);
			stackPanel.Orientation = Orientation.Horizontal;
			Controls children = stackPanel.Children;
			Border border2;
			Border border = (border2 = new Border());
			((ISupportInitialize)border).BeginInit();
			children.Add(border);
			Border border4;
			Border border3 = (border4 = border2);
			context.PushParent(border4);
			border4.Width = 8.0;
			border4.Height = 8.0;
			border4.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
			StyledProperty<IBrush?> backgroundProperty = Border.BackgroundProperty;
			MultiBinding multiBinding2;
			MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
			context.PushParent(multiBinding2);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("UserStatusStringToColorConverter");
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
			object? obj2 = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			multiBinding2.Converter = (IMultiValueConverter)obj2;
			IList<IBinding> bindings = multiBinding2.Bindings;
			CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension();
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
			CompiledBindingExtension item = compiledBindingExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			bindings.Add(item);
			IList<IBinding> bindings2 = multiBinding2.Bindings;
			CompiledBindingExtension compiledBindingExtension2 = new CompiledBindingExtension();
			compiledBindingExtension2.Path = new CompiledBindingPathBuilder(1).Property(Application.ActualThemeVariantProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
			compiledBindingExtension2.Source = Application.Current;
			context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
			CompiledBindingExtension item2 = compiledBindingExtension2.ProvideValue(context);
			context.ProvideTargetProperty = null;
			bindings2.Add(item2);
			context.PopParent();
			AvaloniaObjectExtensions.Bind(border4, backgroundProperty, multiBinding);
			context.PopParent();
			((ISupportInitialize)border3).EndInit();
			Controls children2 = stackPanel.Children;
			TextBlock textBlock2;
			TextBlock textBlock = (textBlock2 = new TextBlock());
			((ISupportInitialize)textBlock).BeginInit();
			children2.Add(textBlock);
			TextBlock textBlock4;
			TextBlock textBlock3 = (textBlock4 = textBlock2);
			context.PushParent(textBlock4);
			StyledProperty<string?> textProperty = TextBlock.TextProperty;
			CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension();
			context.ProvideTargetProperty = TextBlock.TextProperty;
			CompiledBindingExtension compiledBindingExtension4 = compiledBindingExtension3.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(textBlock4, textProperty, compiledBindingExtension4);
			textBlock4.Margin = new Thickness(8.0, 0.0, 0.0, 0.0);
			textBlock4.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
			StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
			DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
			context.ProvideTargetProperty = TextBlock.ForegroundProperty;
			IBinding binding = dynamicResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(textBlock4, foregroundProperty, binding);
			textBlock4.FontSize = 14.0;
			StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
			context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
			object? obj3 = staticResourceExtension2.ProvideValue(context);
			context.ProvideTargetProperty = null;
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock4, obj3);
			textBlock4.FontWeight = (FontWeight)450;
			context.PopParent();
			((ISupportInitialize)textBlock3).EndInit();
			context.PopParent();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}
	}

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal Border OnlineBorder;

	[GeneratedCode("Avalonia.Generators.NameGenerator.InitializeComponentCodeGenerator", "11.3.12.0")]
	internal ComboBox StatusComboBox;

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	private ProfileViewModel _profileViewModel => (ProfileViewModel)base.DataContext;

	public ProfileView()
	{
		InitializeComponent();
	}

	private void onStatusComboBoxSelectionChanged(object? sender, SelectionChangedEventArgs e)
	{
		int selectedIndex = StatusComboBox.SelectedIndex;
		if (1 == 0)
		{
		}
		UserOnlineStatus userOnlineStatus = selectedIndex switch
		{
			0 => UserOnlineStatus.Active, 
			1 => UserOnlineStatus.Inactive, 
			2 => UserOnlineStatus.Disconnected, 
			_ => UserOnlineStatus.Disconnected, 
		};
		if (1 == 0)
		{
		}
		UserOnlineStatus statusAsync = userOnlineStatus;
		_profileViewModel.SetStatusAsync(statusAsync).Forget();
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
		OnlineBorder = nameScope?.Find<Border>("OnlineBorder");
		StatusComboBox = nameScope?.Find<ComboBox>("StatusComboBox");
	}

	[CompilerGenerated]
	private unsafe static void _0021XamlIlPopulate(IServiceProvider P_0, ProfileView P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<ProfileView> context = new CompiledAvaloniaXaml.XamlIlContext.Context<ProfileView>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FUI_002FHome_002FSystemTray_002FProfile_002FProfileView_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/UI/Home/SystemTray/Profile/ProfileView.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		if (P_1.Resources is ResourceDictionary resourceDictionary)
		{
			resourceDictionary.EnsureCapacity(resourceDictionary.Count + 3);
		}
		RenderOptions.SetBitmapInterpolationMode(P_1, BitmapInterpolationMode.MediumQuality);
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"GlobalUserOnlineStatusConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_160.Build_1), context));
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"UserStatusStringToColorConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_160.Build_2), context));
		((ResourceDictionary)P_1.Resources).AddDeferred((object)"DownloadVisibilityConverter", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_160.Build_3), context));
		Grid grid2;
		Grid grid = (grid2 = new Grid());
		((ISupportInitialize)grid).BeginInit();
		P_1.Content = grid;
		Grid grid4;
		Grid grid3 = (grid4 = grid2);
		context.PushParent(grid4);
		grid4.Margin = new Thickness(16.0, 16.0, 16.0, 16.0);
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(0.0, GridUnitType.Auto)
		});
		grid4.RowDefinitions.Add(new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		});
		Controls children = grid4.Children;
		StackPanel stackPanel2;
		StackPanel stackPanel = (stackPanel2 = new StackPanel());
		((ISupportInitialize)stackPanel).BeginInit();
		children.Add(stackPanel);
		StackPanel stackPanel4;
		StackPanel stackPanel3 = (stackPanel4 = stackPanel2);
		context.PushParent(stackPanel4);
		StackPanel stackPanel5 = stackPanel4;
		Controls children2 = stackPanel5.Children;
		Panel panel2;
		Panel panel = (panel2 = new Panel());
		((ISupportInitialize)panel).BeginInit();
		children2.Add(panel);
		Panel panel4;
		Panel panel3 = (panel4 = panel2);
		context.PushParent(panel4);
		panel4.HorizontalAlignment = HorizontalAlignment.Center;
		panel4.Margin = new Thickness(0.0, 0.0, 0.0, 12.0);
		Controls children3 = panel4.Children;
		Border border2;
		Border border = (border2 = new Border());
		((ISupportInitialize)border).BeginInit();
		children3.Add(border);
		Border border4;
		Border border3 = (border4 = border2);
		context.PushParent(border4);
		Border border5 = border4;
		GeometryGroup geometryGroup = new GeometryGroup();
		geometryGroup.Children.Add(new RectangleGeometry
		{
			Rect = Rect.Parse("0,0,64,64")
		});
		GeometryCollection children4 = geometryGroup.Children;
		EllipseGeometry ellipseGeometry = new EllipseGeometry();
		ellipseGeometry.Center = new Point(61.0, 61.0);
		ellipseGeometry.RadiusX = 8.0;
		ellipseGeometry.RadiusY = 8.0;
		children4.Add(ellipseGeometry);
		border5.Clip = geometryGroup;
		RootImageLoader rootImageLoader2;
		RootImageLoader rootImageLoader = (rootImageLoader2 = new RootImageLoader());
		((ISupportInitialize)rootImageLoader).BeginInit();
		border5.Child = rootImageLoader;
		RootImageLoader rootImageLoader4;
		RootImageLoader rootImageLoader3 = (rootImageLoader4 = rootImageLoader2);
		context.PushParent(rootImageLoader4);
		rootImageLoader4.Width = 64.0;
		rootImageLoader4.Height = 64.0;
		rootImageLoader4.CornerRadius = new CornerRadius(6.0, 6.0, 6.0, 6.0);
		StyledProperty<IBrush?> backgroundProperty = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding = dynamicResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, backgroundProperty, binding);
		StyledProperty<BitmapWrapper?> sourceProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002EProfileViewModel_002CRootApp_002EClient_002EAvalonia_002EProfilePictureAsyncBitmapWrapper_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).StreamTask<BitmapWrapper>().Build());
		context.ProvideTargetProperty = RootImageLoader.SourceProperty;
		CompiledBindingExtension compiledBindingExtension2 = compiledBindingExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootImageLoader4, sourceProperty, compiledBindingExtension2);
		rootImageLoader4.LoadingPlaceholderSize = 24.0;
		rootImageLoader4.Stretch = Stretch.UniformToFill;
		context.PopParent();
		((ISupportInitialize)rootImageLoader3).EndInit();
		context.PopParent();
		((ISupportInitialize)border3).EndInit();
		Controls children5 = panel4.Children;
		Border border7;
		Border border6 = (border7 = new Border());
		((ISupportInitialize)border6).BeginInit();
		children5.Add(border6);
		Border border8 = (border4 = border7);
		context.PushParent(border4);
		Border border9 = border4;
		border9.Name = "OnlineBorder";
		object obj = border9;
		context.AvaloniaNameScope.Register("OnlineBorder", obj);
		border9.Width = 10.0;
		border9.Height = 10.0;
		border9.CornerRadius = new CornerRadius(10.0, 10.0, 10.0, 10.0);
		border9.Margin = new Thickness(0.0, 0.0, -2.0, -2.0);
		border9.HorizontalAlignment = HorizontalAlignment.Right;
		border9.VerticalAlignment = VerticalAlignment.Bottom;
		StyledProperty<IBrush?> backgroundProperty2 = Border.BackgroundProperty;
		MultiBinding multiBinding2;
		MultiBinding multiBinding = (multiBinding2 = new MultiBinding());
		context.PushParent(multiBinding2);
		StaticResourceExtension staticResourceExtension = new StaticResourceExtension("GlobalUserOnlineStatusConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj2 = staticResourceExtension.ProvideValue(context);
		context.ProvideTargetProperty = null;
		multiBinding2.Converter = (IMultiValueConverter)obj2;
		IList<IBinding> bindings = multiBinding2.Bindings;
		CompiledBindingExtension obj3 = new CompiledBindingExtension
		{
			Path = new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002EProfileViewModel_002CRootApp_002EClient_002EAvalonia_002ESessionUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EOnlineStatus_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()
		};
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item = obj3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings.Add(item);
		IList<IBinding> bindings2 = multiBinding2.Bindings;
		CompiledBindingExtension compiledBindingExtension3 = new CompiledBindingExtension();
		compiledBindingExtension3.Path = new CompiledBindingPathBuilder(1).Property(Application.ActualThemeVariantProperty, PropertyInfoAccessorFactory.CreateAvaloniaPropertyAccessor).Build();
		compiledBindingExtension3.Source = Application.Current;
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EMultiBinding_002CAvalonia_002EMarkup_002EBindings_0021Property();
		CompiledBindingExtension item2 = compiledBindingExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		bindings2.Add(item2);
		context.PopParent();
		AvaloniaObjectExtensions.Bind(border9, backgroundProperty2, multiBinding);
		context.PopParent();
		((ISupportInitialize)border8).EndInit();
		context.PopParent();
		((ISupportInitialize)panel3).EndInit();
		Controls children6 = stackPanel5.Children;
		TextBlock textBlock2;
		TextBlock textBlock = (textBlock2 = new TextBlock());
		((ISupportInitialize)textBlock).BeginInit();
		children6.Add(textBlock);
		TextBlock textBlock4;
		TextBlock textBlock3 = (textBlock4 = textBlock2);
		context.PushParent(textBlock4);
		TextBlock textBlock5 = textBlock4;
		StyledProperty<string?> textProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension4 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002EProfileViewModel_002CRootApp_002EClient_002EAvalonia_002ESessionUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EUserName_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = TextBlock.TextProperty;
		CompiledBindingExtension compiledBindingExtension5 = compiledBindingExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, textProperty, compiledBindingExtension5);
		textBlock5.Margin = new Thickness(0.0, 0.0, 0.0, 16.0);
		textBlock5.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension2 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding2 = dynamicResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock5, foregroundProperty, binding2);
		textBlock5.FontSize = 20.0;
		StaticResourceExtension staticResourceExtension2 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj4 = staticResourceExtension2.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock5, obj4);
		textBlock5.HorizontalAlignment = HorizontalAlignment.Center;
		textBlock5.FontWeight = (FontWeight)450;
		context.PopParent();
		((ISupportInitialize)textBlock3).EndInit();
		Controls children7 = stackPanel5.Children;
		ComboBox comboBox2;
		ComboBox comboBox = (comboBox2 = new ComboBox());
		((ISupportInitialize)comboBox).BeginInit();
		children7.Add(comboBox);
		ComboBox comboBox4;
		ComboBox comboBox3 = (comboBox4 = comboBox2);
		context.PushParent(comboBox4);
		comboBox4.Name = "StatusComboBox";
		obj = comboBox4;
		context.AvaloniaNameScope.Register("StatusComboBox", obj);
		comboBox4.Margin = new Thickness(0.0, 0.0, 0.0, 12.0);
		comboBox4.Classes.Add("RootComboBox");
		comboBox4.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<IBrush?> backgroundProperty3 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension3 = new DynamicResourceExtension("Input");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding3 = dynamicResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(comboBox4, backgroundProperty3, binding3);
		StyledProperty<IBrush?> borderBrushProperty = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension4 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding4 = dynamicResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(comboBox4, borderBrushProperty, binding4);
		comboBox4.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		comboBox4.HorizontalContentAlignment = HorizontalAlignment.Left;
		DirectProperty<SelectingItemsControl, int> selectedIndexProperty = SelectingItemsControl.SelectedIndexProperty;
		CompiledBindingExtension compiledBindingExtension7;
		CompiledBindingExtension compiledBindingExtension6 = (compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002EProfileViewModel_002CRootApp_002EClient_002EAvalonia_002ESessionUser_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002ECoreDomain_002EModels_002EUser_002EGlobalUser_002CRootApp_002EClient_002ECoreDomain_002EOnlineStatus_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension7);
		CompiledBindingExtension compiledBindingExtension8 = compiledBindingExtension7;
		StaticResourceExtension staticResourceExtension3 = new StaticResourceExtension("OnlineStatusToIndexConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj5 = staticResourceExtension3.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension8.Converter = (IValueConverter)obj5;
		compiledBindingExtension8.Mode = BindingMode.OneWay;
		context.PopParent();
		context.ProvideTargetProperty = SelectingItemsControl.SelectedIndexProperty;
		CompiledBindingExtension compiledBindingExtension9 = compiledBindingExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(comboBox4, selectedIndexProperty, compiledBindingExtension9);
		comboBox4.AddHandler(SelectingItemsControl.SelectionChangedEvent, context.RootObject.onStatusComboBoxSelectionChanged);
		comboBox4.Items.Add("Online");
		comboBox4.Items.Add("Away");
		comboBox4.Items.Add("Offline");
		DataTemplate dataTemplate;
		DataTemplate selectionBoxItemTemplate = (dataTemplate = new DataTemplate());
		context.PushParent(dataTemplate);
		dataTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_160.Build_4), context);
		context.PopParent();
		comboBox4.SelectionBoxItemTemplate = selectionBoxItemTemplate;
		DataTemplate itemTemplate = (dataTemplate = new DataTemplate());
		context.PushParent(dataTemplate);
		dataTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_160.Build_5), context);
		context.PopParent();
		comboBox4.ItemTemplate = itemTemplate;
		context.PopParent();
		((ISupportInitialize)comboBox3).EndInit();
		Controls children8 = stackPanel5.Children;
		Border border11;
		Border border10 = (border11 = new Border());
		((ISupportInitialize)border10).BeginInit();
		children8.Add(border10);
		Border border12 = (border4 = border11);
		context.PushParent(border4);
		Border border13 = border4;
		border13.Margin = new Thickness(0.0, 0.0, 0.0, 12.0);
		StyledProperty<IBrush?> backgroundProperty4 = Border.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension5 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = Border.BackgroundProperty;
		IBinding binding5 = dynamicResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(border13, backgroundProperty4, binding5);
		border13.Height = 0.5;
		context.PopParent();
		((ISupportInitialize)border12).EndInit();
		Controls children9 = stackPanel5.Children;
		Button button2;
		Button button = (button2 = new Button());
		((ISupportInitialize)button).BeginInit();
		children9.Add(button);
		Button button4;
		Button button3 = (button4 = button2);
		context.PushParent(button4);
		Button button5 = button4;
		button5.Classes.Add("BorderButton");
		button5.Margin = new Thickness(0.0, 0.0, 0.0, 12.0);
		StyledProperty<IBrush?> backgroundProperty5 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension6 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding6 = dynamicResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, backgroundProperty5, binding6);
		StyledProperty<IBrush?> borderBrushProperty2 = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension7 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding7 = dynamicResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, borderBrushProperty2, binding7);
		button5.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		button5.HorizontalContentAlignment = HorizontalAlignment.Stretch;
		button5.Padding = new Thickness(12.0, 12.0, 12.0, 12.0);
		button5.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<ICommand?> commandProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension10 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002EProfileViewModel_002CRootApp_002EClient_002EAvalonia_002EShowProfileSettingsCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension11 = compiledBindingExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button5, commandProperty, compiledBindingExtension11);
		StackPanel stackPanel7;
		StackPanel stackPanel6 = (stackPanel7 = new StackPanel());
		((ISupportInitialize)stackPanel6).BeginInit();
		button5.Content = stackPanel6;
		StackPanel stackPanel8 = (stackPanel4 = stackPanel7);
		context.PushParent(stackPanel4);
		StackPanel stackPanel9 = stackPanel4;
		stackPanel9.Orientation = Orientation.Horizontal;
		Controls children10 = stackPanel9.Children;
		RootSvgImage rootSvgImage2;
		RootSvgImage rootSvgImage = (rootSvgImage2 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage).BeginInit();
		children10.Add(rootSvgImage);
		RootSvgImage rootSvgImage4;
		RootSvgImage rootSvgImage3 = (rootSvgImage4 = rootSvgImage2);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage5 = rootSvgImage4;
		rootSvgImage5.Width = 16.37;
		rootSvgImage5.Height = 16.67;
		StyledProperty<string?> svgPathProperty = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension8 = new DynamicResourceExtension("SettingsSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding8 = dynamicResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage5, svgPathProperty, binding8);
		context.PopParent();
		((ISupportInitialize)rootSvgImage3).EndInit();
		Controls children11 = stackPanel9.Children;
		TextBlock textBlock7;
		TextBlock textBlock6 = (textBlock7 = new TextBlock());
		((ISupportInitialize)textBlock6).BeginInit();
		children11.Add(textBlock6);
		TextBlock textBlock8 = (textBlock4 = textBlock7);
		context.PushParent(textBlock4);
		TextBlock textBlock9 = textBlock4;
		textBlock9.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Settings;
		textBlock9.Margin = new Thickness(12.0, 0.0, 0.0, 0.0);
		textBlock9.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty2 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension9 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding9 = dynamicResourceExtension9.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock9, foregroundProperty2, binding9);
		textBlock9.FontSize = 14.0;
		StaticResourceExtension staticResourceExtension4 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj6 = staticResourceExtension4.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock9, obj6);
		textBlock9.FontWeight = (FontWeight)450;
		context.PopParent();
		((ISupportInitialize)textBlock8).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel8).EndInit();
		context.PopParent();
		((ISupportInitialize)button3).EndInit();
		Controls children12 = stackPanel5.Children;
		Button button7;
		Button button6 = (button7 = new Button());
		((ISupportInitialize)button6).BeginInit();
		children12.Add(button6);
		Button button8 = (button4 = button7);
		context.PushParent(button4);
		Button button9 = button4;
		button9.Classes.Add("BorderButton");
		StyledProperty<IBrush?> backgroundProperty6 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension10 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding10 = dynamicResourceExtension10.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, backgroundProperty6, binding10);
		StyledProperty<IBrush?> borderBrushProperty3 = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension11 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding11 = dynamicResourceExtension11.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, borderBrushProperty3, binding11);
		button9.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		button9.HorizontalContentAlignment = HorizontalAlignment.Stretch;
		button9.Padding = new Thickness(12.0, 12.0, 12.0, 12.0);
		button9.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<ICommand?> commandProperty2 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension12 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002EProfileViewModel_002CRootApp_002EClient_002EAvalonia_002EShowSupportFormCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension13 = compiledBindingExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button9, commandProperty2, compiledBindingExtension13);
		StackPanel stackPanel11;
		StackPanel stackPanel10 = (stackPanel11 = new StackPanel());
		((ISupportInitialize)stackPanel10).BeginInit();
		button9.Content = stackPanel10;
		StackPanel stackPanel12 = (stackPanel4 = stackPanel11);
		context.PushParent(stackPanel4);
		StackPanel stackPanel13 = stackPanel4;
		stackPanel13.Orientation = Orientation.Horizontal;
		Controls children13 = stackPanel13.Children;
		RootSvgImage rootSvgImage7;
		RootSvgImage rootSvgImage6 = (rootSvgImage7 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage6).BeginInit();
		children13.Add(rootSvgImage6);
		RootSvgImage rootSvgImage8 = (rootSvgImage4 = rootSvgImage7);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage9 = rootSvgImage4;
		rootSvgImage9.Width = 18.0;
		rootSvgImage9.Height = 18.0;
		StyledProperty<string?> svgPathProperty2 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension12 = new DynamicResourceExtension("SupportSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding12 = dynamicResourceExtension12.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage9, svgPathProperty2, binding12);
		context.PopParent();
		((ISupportInitialize)rootSvgImage8).EndInit();
		Controls children14 = stackPanel13.Children;
		TextBlock textBlock11;
		TextBlock textBlock10 = (textBlock11 = new TextBlock());
		((ISupportInitialize)textBlock10).BeginInit();
		children14.Add(textBlock10);
		TextBlock textBlock12 = (textBlock4 = textBlock11);
		context.PushParent(textBlock4);
		TextBlock textBlock13 = textBlock4;
		textBlock13.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.Support;
		textBlock13.Margin = new Thickness(12.0, 0.0, 0.0, 0.0);
		textBlock13.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty3 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension13 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding13 = dynamicResourceExtension13.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock13, foregroundProperty3, binding13);
		textBlock13.FontSize = 14.0;
		StaticResourceExtension staticResourceExtension5 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj7 = staticResourceExtension5.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock13, obj7);
		textBlock13.FontWeight = (FontWeight)450;
		context.PopParent();
		((ISupportInitialize)textBlock12).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel12).EndInit();
		context.PopParent();
		((ISupportInitialize)button8).EndInit();
		Controls children15 = stackPanel5.Children;
		Button button11;
		Button button10 = (button11 = new Button());
		((ISupportInitialize)button10).BeginInit();
		children15.Add(button10);
		Button button12 = (button4 = button11);
		context.PushParent(button4);
		Button button13 = button4;
		button13.Classes.Add("BorderButton");
		StyledProperty<IBrush?> backgroundProperty7 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension14 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding14 = dynamicResourceExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button13, backgroundProperty7, binding14);
		StyledProperty<IBrush?> borderBrushProperty4 = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension15 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding15 = dynamicResourceExtension15.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button13, borderBrushProperty4, binding15);
		button13.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		button13.HorizontalContentAlignment = HorizontalAlignment.Stretch;
		button13.Padding = new Thickness(12.0, 12.0, 12.0, 12.0);
		button13.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<ICommand?> commandProperty3 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension14 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002EProfileViewModel_002CRootApp_002EClient_002EAvalonia_002ERestartClientCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension15 = compiledBindingExtension14.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button13, commandProperty3, compiledBindingExtension15);
		button13.Margin = new Thickness(0.0, 12.0, 0.0, 0.0);
		StyledProperty<bool> isVisibleProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension16 = (compiledBindingExtension7 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002EProfileViewModel_002CRootApp_002EClient_002EAvalonia_002EInstallationManager_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EHelpers_002EInstallation_002ERootInstallationManager_002CRootApp_002EClient_002EAvalonia_002EDownloadStatus_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build()));
		context.PushParent(compiledBindingExtension7);
		CompiledBindingExtension compiledBindingExtension17 = compiledBindingExtension7;
		StaticResourceExtension staticResourceExtension6 = new StaticResourceExtension("DownloadVisibilityConverter");
		context.ProvideTargetProperty = CompiledAvaloniaXaml.XamlIlHelpers.Avalonia_002EData_002EBindingBase_002CAvalonia_002EMarkup_002EConverter_0021Property();
		object? obj8 = staticResourceExtension6.ProvideValue(context);
		context.ProvideTargetProperty = null;
		compiledBindingExtension17.Converter = (IValueConverter)obj8;
		context.PopParent();
		context.ProvideTargetProperty = Visual.IsVisibleProperty;
		CompiledBindingExtension compiledBindingExtension18 = compiledBindingExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button13, isVisibleProperty, compiledBindingExtension18);
		StackPanel stackPanel15;
		StackPanel stackPanel14 = (stackPanel15 = new StackPanel());
		((ISupportInitialize)stackPanel14).BeginInit();
		button13.Content = stackPanel14;
		StackPanel stackPanel16 = (stackPanel4 = stackPanel15);
		context.PushParent(stackPanel4);
		StackPanel stackPanel17 = stackPanel4;
		stackPanel17.Orientation = Orientation.Horizontal;
		Controls children16 = stackPanel17.Children;
		RootSvgImage rootSvgImage11;
		RootSvgImage rootSvgImage10 = (rootSvgImage11 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage10).BeginInit();
		children16.Add(rootSvgImage10);
		RootSvgImage rootSvgImage12 = (rootSvgImage4 = rootSvgImage11);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage13 = rootSvgImage4;
		rootSvgImage13.Width = 18.0;
		rootSvgImage13.Height = 18.0;
		StyledProperty<string?> svgPathProperty3 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension16 = new DynamicResourceExtension("InstallUpdateSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding16 = dynamicResourceExtension16.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage13, svgPathProperty3, binding16);
		context.PopParent();
		((ISupportInitialize)rootSvgImage12).EndInit();
		Controls children17 = stackPanel17.Children;
		TextBlock textBlock15;
		TextBlock textBlock14 = (textBlock15 = new TextBlock());
		((ISupportInitialize)textBlock14).BeginInit();
		children17.Add(textBlock14);
		TextBlock textBlock16 = (textBlock4 = textBlock15);
		context.PushParent(textBlock4);
		TextBlock textBlock17 = textBlock4;
		textBlock17.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.UpdateClient;
		textBlock17.Margin = new Thickness(12.0, 0.0, 0.0, 0.0);
		textBlock17.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty4 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension17 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding17 = dynamicResourceExtension17.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock17, foregroundProperty4, binding17);
		textBlock17.FontSize = 14.0;
		StaticResourceExtension staticResourceExtension7 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj9 = staticResourceExtension7.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock17, obj9);
		textBlock17.FontWeight = (FontWeight)450;
		context.PopParent();
		((ISupportInitialize)textBlock16).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel16).EndInit();
		context.PopParent();
		((ISupportInitialize)button12).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel3).EndInit();
		Controls children18 = grid4.Children;
		Button button15;
		Button button14 = (button15 = new Button());
		((ISupportInitialize)button14).BeginInit();
		children18.Add(button14);
		Button button16 = (button4 = button15);
		context.PushParent(button4);
		Button button17 = button4;
		button17.Classes.Add("BorderButton");
		StyledProperty<IBrush?> backgroundProperty8 = TemplatedControl.BackgroundProperty;
		DynamicResourceExtension dynamicResourceExtension18 = new DynamicResourceExtension("BackgroundSecondary");
		context.ProvideTargetProperty = TemplatedControl.BackgroundProperty;
		IBinding binding18 = dynamicResourceExtension18.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button17, backgroundProperty8, binding18);
		StyledProperty<IBrush?> borderBrushProperty5 = TemplatedControl.BorderBrushProperty;
		DynamicResourceExtension dynamicResourceExtension19 = new DynamicResourceExtension("Border");
		context.ProvideTargetProperty = TemplatedControl.BorderBrushProperty;
		IBinding binding19 = dynamicResourceExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button17, borderBrushProperty5, binding19);
		button17.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
		button17.HorizontalContentAlignment = HorizontalAlignment.Stretch;
		button17.Padding = new Thickness(12.0, 12.0, 12.0, 12.0);
		button17.CornerRadius = new CornerRadius(8.0, 8.0, 8.0, 8.0);
		StyledProperty<ICommand?> commandProperty4 = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension19 = new CompiledBindingExtension(new CompiledBindingPathBuilder(1).Property(CompiledAvaloniaXaml.XamlIlHelpers.RootApp_002EClient_002EAvalonia_002EUI_002EHome_002ESystemTray_002EProfile_002EProfileViewModel_002CRootApp_002EClient_002EAvalonia_002EShowSignOutConfirmationViewModelCommand_0021Property(), PropertyInfoAccessorFactory.CreateInpcPropertyAccessor).Build());
		context.ProvideTargetProperty = Button.CommandProperty;
		CompiledBindingExtension compiledBindingExtension20 = compiledBindingExtension19.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(button17, commandProperty4, compiledBindingExtension20);
		Grid.SetRow(button17, 1);
		button17.VerticalAlignment = VerticalAlignment.Bottom;
		StackPanel stackPanel19;
		StackPanel stackPanel18 = (stackPanel19 = new StackPanel());
		((ISupportInitialize)stackPanel18).BeginInit();
		button17.Content = stackPanel18;
		StackPanel stackPanel20 = (stackPanel4 = stackPanel19);
		context.PushParent(stackPanel4);
		StackPanel stackPanel21 = stackPanel4;
		stackPanel21.Orientation = Orientation.Horizontal;
		Controls children19 = stackPanel21.Children;
		RootSvgImage rootSvgImage15;
		RootSvgImage rootSvgImage14 = (rootSvgImage15 = new RootSvgImage());
		((ISupportInitialize)rootSvgImage14).BeginInit();
		children19.Add(rootSvgImage14);
		RootSvgImage rootSvgImage16 = (rootSvgImage4 = rootSvgImage15);
		context.PushParent(rootSvgImage4);
		RootSvgImage rootSvgImage17 = rootSvgImage4;
		rootSvgImage17.Width = 13.33;
		rootSvgImage17.Height = 16.67;
		StyledProperty<string?> svgPathProperty4 = RootSvgImage.SvgPathProperty;
		DynamicResourceExtension dynamicResourceExtension20 = new DynamicResourceExtension("SignOutSVG");
		context.ProvideTargetProperty = RootSvgImage.SvgPathProperty;
		IBinding binding20 = dynamicResourceExtension20.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(rootSvgImage17, svgPathProperty4, binding20);
		context.PopParent();
		((ISupportInitialize)rootSvgImage16).EndInit();
		Controls children20 = stackPanel21.Children;
		TextBlock textBlock19;
		TextBlock textBlock18 = (textBlock19 = new TextBlock());
		((ISupportInitialize)textBlock18).BeginInit();
		children20.Add(textBlock18);
		TextBlock textBlock20 = (textBlock4 = textBlock19);
		context.PushParent(textBlock4);
		TextBlock textBlock21 = textBlock4;
		textBlock21.Text = RootApp.Client.Avalonia.Resources.Strings.Resources.SignOut;
		textBlock21.Margin = new Thickness(12.0, 0.0, 0.0, 0.0);
		textBlock21.Padding = new Thickness(0.0, 0.0, 0.0, 0.0);
		StyledProperty<IBrush?> foregroundProperty5 = TextBlock.ForegroundProperty;
		DynamicResourceExtension dynamicResourceExtension21 = new DynamicResourceExtension("TextPrimary");
		context.ProvideTargetProperty = TextBlock.ForegroundProperty;
		IBinding binding21 = dynamicResourceExtension21.ProvideValue(context);
		context.ProvideTargetProperty = null;
		AvaloniaObjectExtensions.Bind(textBlock21, foregroundProperty5, binding21);
		textBlock21.FontSize = 14.0;
		StaticResourceExtension staticResourceExtension8 = new StaticResourceExtension("RootFont");
		context.ProvideTargetProperty = TextBlock.FontFamilyProperty;
		object? obj10 = staticResourceExtension8.ProvideValue(context);
		context.ProvideTargetProperty = null;
		CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_1(textBlock21, obj10);
		textBlock21.FontWeight = (FontWeight)450;
		context.PopParent();
		((ISupportInitialize)textBlock20).EndInit();
		context.PopParent();
		((ISupportInitialize)stackPanel20).EndInit();
		context.PopParent();
		((ISupportInitialize)button16).EndInit();
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
	private static void _0021XamlIlPopulateTrampoline(ProfileView P_0)
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

