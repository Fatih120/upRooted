// Avalonia.Themes.Simple, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Themes.Simple.SimpleTheme
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Themes.Simple;
using CompiledAvaloniaXaml;

public class SimpleTheme : Styles
{
	[CompilerGenerated]
	private class XamlClosure_3
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeBackgroundColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = new CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FSimpleTheme_002Examl.Singleton }, "avares://Avalonia.Themes.Simple/SimpleTheme.xaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (SimpleTheme)service;
				}
			}
			return context;
		}

		public static object Build_2(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeBorderLowColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_3(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeBorderMidColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_4(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeBorderHighColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_5(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeControlLowColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_6(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeControlMidColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_7(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeControlMidHighColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_8(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeControlHighColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_9(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeControlVeryHighColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_10(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeControlHighlightLowColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_11(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeControlHighlightMidColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_12(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeControlHighlightHighColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_13(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeForegroundColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_14(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("HighlightColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_15(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("HighlightColor2");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_16(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("HyperlinkVisitedColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_17(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4278190080u)
			};
		}

		public static object Build_18(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(16777215u)
			};
		}

		public static object Build_19(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4278190080u)
			};
		}

		public static object Build_20(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4293256677u)
			};
		}

		public static object Build_21(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4291480266u)
			};
		}

		public static object Build_22(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeBackgroundColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_23(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeBorderLowColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_24(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeBorderMidColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_25(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeBorderHighColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_26(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeControlLowColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_27(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeControlMidColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_28(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeControlMidHighColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_29(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeControlHighColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_30(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeControlVeryHighColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_31(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeControlHighlightLowColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_32(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeControlHighlightMidColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_33(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeControlHighlightHighColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_34(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeForegroundColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_35(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("HighlightColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_36(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("HighlightColor2");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_37(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("HyperlinkVisitedColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_38(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(uint.MaxValue)
			};
		}

		public static object Build_39(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(16777215u)
			};
		}

		public static object Build_40(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(uint.MaxValue)
			};
		}

		public static object Build_41(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4293256677u)
			};
		}

		public static object Build_42(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(4291480266u)
			};
		}

		public static object Build_43(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			return new FontFamily(((IUriContext)context).BaseUri, "fonts:Inter#Inter, $Default");
		}

		public static object Build_44(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("HighlightForegroundColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_45(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeForegroundLowColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_46(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeAccentColor2");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_47(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeAccentColor3");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_48(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeAccentColor4");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_49(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeAccentColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_50(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ErrorColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_51(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ErrorLowColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_52(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush = new SolidColorBrush();
			solidColorBrush.Opacity = 0.75;
			solidColorBrush.Color = Color.FromUInt32(4282664004u);
			return solidColorBrush;
		}

		public static object Build_53(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush = new SolidColorBrush();
			solidColorBrush.Opacity = 0.75;
			solidColorBrush.Color = Color.FromUInt32(4278221516u);
			return solidColorBrush;
		}

		public static object Build_54(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush = new SolidColorBrush();
			solidColorBrush.Opacity = 0.75;
			solidColorBrush.Color = Color.FromUInt32(4280262213u);
			return solidColorBrush;
		}

		public static object Build_55(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush = new SolidColorBrush();
			solidColorBrush.Opacity = 0.75;
			solidColorBrush.Color = Color.FromUInt32(4294816552u);
			return solidColorBrush;
		}

		public static object Build_56(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush = new SolidColorBrush();
			solidColorBrush.Opacity = 0.75;
			solidColorBrush.Color = Color.FromUInt32(4290584620u);
			return solidColorBrush;
		}

		public static object Build_57(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			return new SolidColorBrush
			{
				Color = Color.FromUInt32(16777215u)
			};
		}

		public static object Build_58(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			solidColorBrush.Opacity = 0.4;
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeAccentColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}

		public static object Build_59(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = CreateContext(P_0);
			SolidColorBrush solidColorBrush;
			SolidColorBrush result = (solidColorBrush = new SolidColorBrush());
			context.PushParent(solidColorBrush);
			solidColorBrush.Opacity = 0.4;
			StaticResourceExtension staticResourceExtension = new StaticResourceExtension("ThemeAccentColor");
			context.ProvideTargetProperty = SolidColorBrush.ColorProperty;
			object? obj = staticResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			XamlDynamicSetters._003C_003EXamlDynamicSetter_18(solidColorBrush, obj);
			context.PopParent();
			return result;
		}
	}

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public SimpleTheme(IServiceProvider? P_0 = null)
	{
		_0021XamlIlPopulateTrampoline(P_0, this);
	}

	[CompilerGenerated]
	private unsafe static void _0021XamlIlPopulate(IServiceProvider P_0, SimpleTheme P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme> context = new CompiledAvaloniaXaml.XamlIlContext.Context<SimpleTheme>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FSimpleTheme_002Examl.Singleton }, "avares://Avalonia.Themes.Simple/SimpleTheme.xaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		SimpleTheme simpleTheme2;
		SimpleTheme simpleTheme = (simpleTheme2 = P_1);
		context.PushParent(simpleTheme2);
		ResourceDictionary resourceDictionary;
		ResourceDictionary resources = (resourceDictionary = new ResourceDictionary());
		context.PushParent(resourceDictionary);
		ResourceDictionary resourceDictionary2 = resourceDictionary;
		if (resourceDictionary2 is ResourceDictionary resourceDictionary3)
		{
			resourceDictionary3.EnsureCapacity(resourceDictionary3.Count + 55);
		}
		IDictionary<ThemeVariant, IThemeVariantProvider> themeDictionaries = resourceDictionary2.ThemeDictionaries;
		ThemeVariant key = ThemeVariant.Default;
		ResourceDictionary value = (resourceDictionary = new ResourceDictionary());
		context.PushParent(resourceDictionary);
		ResourceDictionary resourceDictionary4 = resourceDictionary;
		if (resourceDictionary4 is ResourceDictionary resourceDictionary5)
		{
			resourceDictionary5.EnsureCapacity(resourceDictionary5.Count + 37);
		}
		((IThemeVariantProvider)resourceDictionary4).Key = ThemeVariant.Default;
		((IDictionary<object, object>)resourceDictionary4).Add((object)"ThemeBackgroundColor", (object)Color.FromUInt32(uint.MaxValue));
		((IDictionary<object, object>)resourceDictionary4).Add((object)"ThemeBorderLowColor", (object)Color.FromUInt32(4289374890u));
		((IDictionary<object, object>)resourceDictionary4).Add((object)"ThemeBorderMidColor", (object)Color.FromUInt32(4287137928u));
		((IDictionary<object, object>)resourceDictionary4).Add((object)"ThemeBorderHighColor", (object)Color.FromUInt32(4281545523u));
		((IDictionary<object, object>)resourceDictionary4).Add((object)"ThemeControlLowColor", (object)Color.FromUInt32(4287007129u));
		((IDictionary<object, object>)resourceDictionary4).Add((object)"ThemeControlMidColor", (object)Color.FromUInt32(4294309365u));
		((IDictionary<object, object>)resourceDictionary4).Add((object)"ThemeControlMidHighColor", (object)Color.FromUInt32(4290954185u));
		((IDictionary<object, object>)resourceDictionary4).Add((object)"ThemeControlHighColor", (object)Color.FromUInt32(4285032552u));
		((IDictionary<object, object>)resourceDictionary4).Add((object)"ThemeControlVeryHighColor", (object)Color.FromUInt32(4284177243u));
		((IDictionary<object, object>)resourceDictionary4).Add((object)"ThemeControlHighlightLowColor", (object)Color.FromUInt32(4293980400u));
		((IDictionary<object, object>)resourceDictionary4).Add((object)"ThemeControlHighlightMidColor", (object)Color.FromUInt32(4291875024u));
		((IDictionary<object, object>)resourceDictionary4).Add((object)"ThemeControlHighlightHighColor", (object)Color.FromUInt32(4286611584u));
		((IDictionary<object, object>)resourceDictionary4).Add((object)"ThemeForegroundColor", (object)Color.FromUInt32(4278190080u));
		((IDictionary<object, object>)resourceDictionary4).Add((object)"HighlightColor", (object)Color.FromUInt32(4278742942u));
		((IDictionary<object, object>)resourceDictionary4).Add((object)"HighlightColor2", (object)Color.FromUInt32(4278804613u));
		((IDictionary<object, object>)resourceDictionary4).Add((object)"HyperlinkVisitedColor", (object)Color.FromUInt32(4285013416u));
		resourceDictionary4.AddDeferred("ThemeBackgroundBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_1), context));
		resourceDictionary4.AddDeferred("ThemeBorderLowBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_2), context));
		resourceDictionary4.AddDeferred("ThemeBorderMidBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_3), context));
		resourceDictionary4.AddDeferred("ThemeBorderHighBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_4), context));
		resourceDictionary4.AddDeferred("ThemeControlLowBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_5), context));
		resourceDictionary4.AddDeferred("ThemeControlMidBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_6), context));
		resourceDictionary4.AddDeferred("ThemeControlMidHighBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_7), context));
		resourceDictionary4.AddDeferred("ThemeControlHighBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_8), context));
		resourceDictionary4.AddDeferred("ThemeControlVeryHighBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_9), context));
		resourceDictionary4.AddDeferred("ThemeControlHighlightLowBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_10), context));
		resourceDictionary4.AddDeferred("ThemeControlHighlightMidBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_11), context));
		resourceDictionary4.AddDeferred("ThemeControlHighlightHighBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_12), context));
		resourceDictionary4.AddDeferred("ThemeForegroundBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_13), context));
		resourceDictionary4.AddDeferred("HighlightBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_14), context));
		resourceDictionary4.AddDeferred("HighlightBrush2", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_15), context));
		resourceDictionary4.AddDeferred("HyperlinkVisitedBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_16), context));
		resourceDictionary4.AddDeferred("RefreshVisualizerForeground", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_17), context));
		resourceDictionary4.AddDeferred("RefreshVisualizerBackground", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_18), context));
		resourceDictionary4.AddDeferred("CaptionButtonForeground", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_19), context));
		resourceDictionary4.AddDeferred("CaptionButtonBackground", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_20), context));
		resourceDictionary4.AddDeferred("CaptionButtonBorderBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_21), context));
		context.PopParent();
		themeDictionaries.Add(key, value);
		IDictionary<ThemeVariant, IThemeVariantProvider> themeDictionaries2 = resourceDictionary2.ThemeDictionaries;
		ThemeVariant dark = ThemeVariant.Dark;
		ResourceDictionary value2 = (resourceDictionary = new ResourceDictionary());
		context.PushParent(resourceDictionary);
		ResourceDictionary resourceDictionary6 = resourceDictionary;
		if (resourceDictionary6 is ResourceDictionary resourceDictionary7)
		{
			resourceDictionary7.EnsureCapacity(resourceDictionary7.Count + 37);
		}
		((IThemeVariantProvider)resourceDictionary6).Key = ThemeVariant.Dark;
		((IDictionary<object, object>)resourceDictionary6).Add((object)"ThemeBackgroundColor", (object)Color.FromUInt32(4280821800u));
		((IDictionary<object, object>)resourceDictionary6).Add((object)"ThemeBorderLowColor", (object)Color.FromUInt32(4283453520u));
		((IDictionary<object, object>)resourceDictionary6).Add((object)"ThemeBorderMidColor", (object)Color.FromUInt32(4286611584u));
		((IDictionary<object, object>)resourceDictionary6).Add((object)"ThemeBorderHighColor", (object)Color.FromUInt32(4288716960u));
		((IDictionary<object, object>)resourceDictionary6).Add((object)"ThemeControlLowColor", (object)Color.FromUInt32(4280821800u));
		((IDictionary<object, object>)resourceDictionary6).Add((object)"ThemeControlMidColor", (object)Color.FromUInt32(4283453520u));
		((IDictionary<object, object>)resourceDictionary6).Add((object)"ThemeControlMidHighColor", (object)Color.FromUInt32(4285032552u));
		((IDictionary<object, object>)resourceDictionary6).Add((object)"ThemeControlHighColor", (object)Color.FromUInt32(4286611584u));
		((IDictionary<object, object>)resourceDictionary6).Add((object)"ThemeControlVeryHighColor", (object)Color.FromUInt32(4293913583u));
		((IDictionary<object, object>)resourceDictionary6).Add((object)"ThemeControlHighlightLowColor", (object)Color.FromUInt32(4289243304u));
		((IDictionary<object, object>)resourceDictionary6).Add((object)"ThemeControlHighlightMidColor", (object)Color.FromUInt32(4286743170u));
		((IDictionary<object, object>)resourceDictionary6).Add((object)"ThemeControlHighlightHighColor", (object)Color.FromUInt32(4283453520u));
		((IDictionary<object, object>)resourceDictionary6).Add((object)"ThemeForegroundColor", (object)Color.FromUInt32(4292796126u));
		((IDictionary<object, object>)resourceDictionary6).Add((object)"HighlightColor", (object)Color.FromUInt32(4279344858u));
		((IDictionary<object, object>)resourceDictionary6).Add((object)"HighlightColor2", (object)Color.FromUInt32(4278804613u));
		((IDictionary<object, object>)resourceDictionary6).Add((object)"HyperlinkVisitedColor", (object)Color.FromUInt32(4291136249u));
		resourceDictionary6.AddDeferred("ThemeBackgroundBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_22), context));
		resourceDictionary6.AddDeferred("ThemeBorderLowBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_23), context));
		resourceDictionary6.AddDeferred("ThemeBorderMidBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_24), context));
		resourceDictionary6.AddDeferred("ThemeBorderHighBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_25), context));
		resourceDictionary6.AddDeferred("ThemeControlLowBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_26), context));
		resourceDictionary6.AddDeferred("ThemeControlMidBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_27), context));
		resourceDictionary6.AddDeferred("ThemeControlMidHighBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_28), context));
		resourceDictionary6.AddDeferred("ThemeControlHighBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_29), context));
		resourceDictionary6.AddDeferred("ThemeControlVeryHighBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_30), context));
		resourceDictionary6.AddDeferred("ThemeControlHighlightLowBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_31), context));
		resourceDictionary6.AddDeferred("ThemeControlHighlightMidBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_32), context));
		resourceDictionary6.AddDeferred("ThemeControlHighlightHighBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_33), context));
		resourceDictionary6.AddDeferred("ThemeForegroundBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_34), context));
		resourceDictionary6.AddDeferred("HighlightBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_35), context));
		resourceDictionary6.AddDeferred("HighlightBrush2", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_36), context));
		resourceDictionary6.AddDeferred("HyperlinkVisitedBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_37), context));
		resourceDictionary6.AddDeferred("RefreshVisualizerForeground", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_38), context));
		resourceDictionary6.AddDeferred("RefreshVisualizerBackground", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_39), context));
		resourceDictionary6.AddDeferred("CaptionButtonForeground", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_40), context));
		resourceDictionary6.AddDeferred("CaptionButtonBackground", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_41), context));
		resourceDictionary6.AddDeferred("CaptionButtonBorderBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_42), context));
		context.PopParent();
		themeDictionaries2.Add(dark, value2);
		resourceDictionary2.AddDeferred("ContentControlThemeFontFamily", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_43), context));
		((IDictionary<object, object>)resourceDictionary2).Add((object)"ThemeAccentColor", (object)Color.FromUInt32(3423706842u));
		((IDictionary<object, object>)resourceDictionary2).Add((object)"ThemeAccentColor2", (object)Color.FromUInt32(2568068826u));
		((IDictionary<object, object>)resourceDictionary2).Add((object)"ThemeAccentColor3", (object)Color.FromUInt32(1712430810u));
		((IDictionary<object, object>)resourceDictionary2).Add((object)"ThemeAccentColor4", (object)Color.FromUInt32(856792794u));
		((IDictionary<object, object>)resourceDictionary2).Add((object)"ThemeForegroundLowColor", (object)Color.FromUInt32(4286611584u));
		((IDictionary<object, object>)resourceDictionary2).Add((object)"HighlightForegroundColor", (object)Color.FromUInt32(uint.MaxValue));
		((IDictionary<object, object>)resourceDictionary2).Add((object)"ErrorColor", (object)Color.FromUInt32(4294901760u));
		((IDictionary<object, object>)resourceDictionary2).Add((object)"ErrorLowColor", (object)Color.FromUInt32(285147136u));
		resourceDictionary2.AddDeferred("HighlightForegroundBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_44), context));
		resourceDictionary2.AddDeferred("ThemeForegroundLowBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_45), context));
		resourceDictionary2.AddDeferred("ThemeAccentBrush2", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_46), context));
		resourceDictionary2.AddDeferred("ThemeAccentBrush3", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_47), context));
		resourceDictionary2.AddDeferred("ThemeAccentBrush4", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_48), context));
		resourceDictionary2.AddDeferred("ThemeAccentBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_49), context));
		resourceDictionary2.AddDeferred("ErrorBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_50), context));
		resourceDictionary2.AddDeferred("ErrorLowBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_51), context));
		resourceDictionary2.AddDeferred("NotificationCardBackgroundBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_52), context));
		resourceDictionary2.AddDeferred("NotificationCardInformationBackgroundBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_53), context));
		resourceDictionary2.AddDeferred("NotificationCardSuccessBackgroundBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_54), context));
		resourceDictionary2.AddDeferred("NotificationCardWarningBackgroundBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_55), context));
		resourceDictionary2.AddDeferred("NotificationCardErrorBackgroundBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_56), context));
		resourceDictionary2.AddDeferred("ThemeControlTransparentBrush", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_57), context));
		resourceDictionary2.AddDeferred("DatePickerFlyoutPresenterHighlightFill", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_58), context));
		resourceDictionary2.AddDeferred("TimePickerFlyoutPresenterHighlightFill", XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<object>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_3.Build_59), context));
		((IDictionary<object, object>)resourceDictionary2).Add((object)"ThemeBorderThickness", (object)new Thickness(1.0, 1.0, 1.0, 1.0));
		((IDictionary<object, object>)resourceDictionary2).Add((object)"ThemeDisabledOpacity", (object)0.5);
		((IDictionary<object, object>)resourceDictionary2).Add((object)"FontSizeSmall", (object)10.0);
		((IDictionary<object, object>)resourceDictionary2).Add((object)"FontSizeNormal", (object)12.0);
		((IDictionary<object, object>)resourceDictionary2).Add((object)"FontSizeLarge", (object)16.0);
		((IDictionary<object, object>)resourceDictionary2).Add((object)"ScrollBarThickness", (object)18.0);
		((IDictionary<object, object>)resourceDictionary2).Add((object)"ScrollBarThumbThickness", (object)8.0);
		((IDictionary<object, object>)resourceDictionary2).Add((object)"IconElementThemeHeight", (object)20.0);
		((IDictionary<object, object>)resourceDictionary2).Add((object)"IconElementThemeWidth", (object)20.0);
		((IDictionary<object, object>)resourceDictionary2).Add((object)"TextControlPlaceholderOpacity", (object)0.5);
		((IDictionary<object, object>)resourceDictionary2).Add((object)"StringDatePickerDayText", (object)"day");
		((IDictionary<object, object>)resourceDictionary2).Add((object)"StringDatePickerMonthText", (object)"month");
		((IDictionary<object, object>)resourceDictionary2).Add((object)"StringDatePickerYearText", (object)"year");
		((IDictionary<object, object>)resourceDictionary2).Add((object)"StringTimePickerHourText", (object)"hour");
		((IDictionary<object, object>)resourceDictionary2).Add((object)"StringTimePickerMinuteText", (object)"minute");
		((IDictionary<object, object>)resourceDictionary2).Add((object)"StringTimePickerSecondText", (object)"second");
		((IDictionary<object, object>)resourceDictionary2).Add((object)"StringTextFlyoutCutText", (object)"Cut");
		((IDictionary<object, object>)resourceDictionary2).Add((object)"StringTextFlyoutCopyText", (object)"Copy");
		((IDictionary<object, object>)resourceDictionary2).Add((object)"StringTextFlyoutPasteText", (object)"Paste");
		((IDictionary<object, object>)resourceDictionary2).Add((object)"StringManagedFileChooserFileNameWatermark", (object)"File name");
		((IDictionary<object, object>)resourceDictionary2).Add((object)"StringManagedFileChooserShowHiddenFilesText", (object)"Show hidden files");
		((IDictionary<object, object>)resourceDictionary2).Add((object)"StringManagedFileChooserOkText", (object)"OK");
		((IDictionary<object, object>)resourceDictionary2).Add((object)"StringManagedFileChooserCancelText", (object)"Cancel");
		((IDictionary<object, object>)resourceDictionary2).Add((object)"StringManagedFileChooserNameColumn", (object)"Name");
		((IDictionary<object, object>)resourceDictionary2).Add((object)"StringManagedFileChooserDateModifiedColumn", (object)"Date Modified");
		((IDictionary<object, object>)resourceDictionary2).Add((object)"StringManagedFileChooserTypeColumn", (object)"Type");
		((IDictionary<object, object>)resourceDictionary2).Add((object)"StringManagedFileChooserSizeColumn", (object)"Size");
		((IDictionary<object, object>)resourceDictionary2).Add((object)"StringManagedFileChooserOverwritePromptFileAlreadyExistsText", (object)"{0} already exists. Do you want to replace it?");
		((IDictionary<object, object>)resourceDictionary2).Add((object)"StringManagedFileChooserOverwritePromptConfirmText", (object)"Yes");
		((IDictionary<object, object>)resourceDictionary2).Add((object)"StringManagedFileChooserOverwritePromptCancelText", (object)"No");
		context.PopParent();
		simpleTheme2.Resources = resources;
		simpleTheme2.Add(_0021AvaloniaResources.Build_003A_002FControls_002FSimpleControls_002Examl(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(context)));
		context.PopParent();
		if (simpleTheme is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(IServiceProvider P_0, SimpleTheme P_1)
	{
		if (_0021XamlIlPopulateOverride != null)
		{
			_0021XamlIlPopulateOverride(P_1);
		}
		else
		{
			_0021XamlIlPopulate(XamlIlRuntimeHelpers.CreateRootServiceProviderV3(P_0), P_1);
		}
	}
}
