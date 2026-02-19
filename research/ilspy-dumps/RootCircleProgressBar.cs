// RootApp.Client.Avalonia, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Avalonia.Controls.RootCircleProgressBar
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Markup.Xaml.XamlIl.Runtime;
using Avalonia.Media;
using Avalonia.Styling;
using CompiledAvaloniaXaml;
using RootApp.Client.Avalonia.Controls;

public class RootCircleProgressBar : UserControl
{
	[CompilerGenerated]
	private class XamlClosure_16
	{
		public static object Build_1(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<RootCircleProgressBar> context = CreateContext(P_0);
			context.IntermediateRoot = new Panel();
			object obj = context.IntermediateRoot;
			((ISupportInitialize)obj).BeginInit();
			Panel panel = (Panel)obj;
			context.PushParent(panel);
			Controls children = panel.Children;
			Arc arc2;
			Arc arc = (arc2 = new Arc());
			((ISupportInitialize)arc).BeginInit();
			children.Add(arc);
			Arc arc4;
			Arc arc3 = (arc4 = arc2);
			context.PushParent(arc4);
			AvaloniaObjectExtensions.Bind(arc4, Layoutable.WidthProperty, new TemplateBinding(WidthProperty).ProvideValue());
			AvaloniaObjectExtensions.Bind(arc4, Layoutable.HeightProperty, new TemplateBinding(HeightProperty).ProvideValue());
			arc4.SetValue(Arc.StartAngleProperty, 0.0, BindingPriority.Template);
			arc4.SetValue(Shape.StretchProperty, Stretch.None, BindingPriority.Template);
			StyledProperty<IBrush?> strokeProperty = Shape.StrokeProperty;
			DynamicResourceExtension dynamicResourceExtension = new DynamicResourceExtension("TextPrimary");
			context.ProvideTargetProperty = Shape.StrokeProperty;
			IBinding binding = dynamicResourceExtension.ProvideValue(context);
			context.ProvideTargetProperty = null;
			AvaloniaObjectExtensions.Bind(arc4, strokeProperty, binding);
			arc4.SetValue(Visual.OpacityProperty, 0.1, BindingPriority.Template);
			AvaloniaObjectExtensions.Bind(arc4, Shape.StrokeThicknessProperty, new TemplateBinding(StrokeWidthProperty).ProvideValue());
			arc4.SetValue(Arc.SweepAngleProperty, 360.0, BindingPriority.Template);
			context.PopParent();
			((ISupportInitialize)arc3).EndInit();
			Controls children2 = panel.Children;
			Arc arc6;
			Arc arc5 = (arc6 = new Arc());
			((ISupportInitialize)arc5).BeginInit();
			children2.Add(arc5);
			arc6.Name = "PART_ArcFill";
			object obj2 = arc6;
			context.AvaloniaNameScope.Register("PART_ArcFill", obj2);
			AvaloniaObjectExtensions.Bind(arc6, Layoutable.WidthProperty, new TemplateBinding(WidthProperty).ProvideValue());
			AvaloniaObjectExtensions.Bind(arc6, Layoutable.HeightProperty, new TemplateBinding(HeightProperty).ProvideValue());
			arc6.SetValue(Shape.StretchProperty, Stretch.None, BindingPriority.Template);
			AvaloniaObjectExtensions.Bind(arc6, Shape.StrokeProperty, new TemplateBinding(StrokeBrushProperty).ProvideValue());
			arc6.SetValue(Shape.StrokeJoinProperty, PenLineJoin.Round, BindingPriority.Template);
			arc6.SetValue(Shape.StrokeLineCapProperty, PenLineCap.Round, BindingPriority.Template);
			AvaloniaObjectExtensions.Bind(arc6, Shape.StrokeThicknessProperty, new TemplateBinding(StrokeWidthProperty).ProvideValue());
			AvaloniaObjectExtensions.Bind(arc6, Arc.SweepAngleProperty, new TemplateBinding(ValueProperty).ProvideValue());
			StyledProperty<Transitions?> transitionsProperty = Animatable.TransitionsProperty;
			Transitions transitions = new Transitions();
			BrushTransition brushTransition = new BrushTransition();
			brushTransition.Property = Shape.StrokeProperty;
			brushTransition.Duration = TimeSpan.FromTicks(5000000L);
			transitions.Add(brushTransition);
			DoubleTransition doubleTransition = new DoubleTransition();
			doubleTransition.Easing = Easing.Parse("CircularEaseOut");
			doubleTransition.Property = Arc.SweepAngleProperty;
			doubleTransition.Duration = TimeSpan.FromTicks(8000000L);
			transitions.Add(doubleTransition);
			DoubleTransition doubleTransition2 = new DoubleTransition();
			doubleTransition2.Easing = Easing.Parse("CircularEaseOut");
			doubleTransition2.Property = Arc.StartAngleProperty;
			doubleTransition2.Duration = TimeSpan.FromTicks(8000000L);
			transitions.Add(doubleTransition2);
			arc6.SetValue(transitionsProperty, transitions, BindingPriority.Template);
			((ISupportInitialize)arc6).EndInit();
			Controls children3 = panel.Children;
			ContentControl contentControl2;
			ContentControl contentControl = (contentControl2 = new ContentControl());
			((ISupportInitialize)contentControl).BeginInit();
			children3.Add(contentControl);
			AvaloniaObjectExtensions.Bind(contentControl2, Layoutable.MarginProperty, new TemplateBinding(StrokeWidthProperty).ProvideValue());
			contentControl2.SetValue(ContentControl.HorizontalContentAlignmentProperty, HorizontalAlignment.Center, BindingPriority.Template);
			contentControl2.SetValue(ContentControl.VerticalContentAlignmentProperty, VerticalAlignment.Center, BindingPriority.Template);
			CompiledAvaloniaXaml.XamlDynamicSetters._003C_003EXamlDynamicSetter_5(contentControl2, BindingPriority.Template, new TemplateBinding(ContentControl.ContentProperty).ProvideValue());
			((ISupportInitialize)contentControl2).EndInit();
			context.PopParent();
			((ISupportInitialize)obj).EndInit();
			return obj;
		}

		public static CompiledAvaloniaXaml.XamlIlContext.Context<RootCircleProgressBar> CreateContext(IServiceProvider P_0)
		{
			CompiledAvaloniaXaml.XamlIlContext.Context<RootCircleProgressBar> context = new CompiledAvaloniaXaml.XamlIlContext.Context<RootCircleProgressBar>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FControls_002FRootCircleProgressBar_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Controls/RootCircleProgressBar.axaml");
			if (P_0 != null)
			{
				object service = P_0.GetService(typeof(IRootObjectProvider));
				if (service != null)
				{
					service = ((IRootObjectProvider)service).RootObject;
					context.RootObject = (RootCircleProgressBar)service;
				}
			}
			return context;
		}
	}

	private double _value = 50.0;

	public static readonly StyledProperty<double> ValueProperty = AvaloniaProperty.Register<RootCircleProgressBar, double>("Value", 50.0, false, BindingMode.OneWay, null, (AvaloniaObject avaloniaObject, double d) => d * 3.6);

	public new static readonly StyledProperty<int> HeightProperty = AvaloniaProperty.Register<RootCircleProgressBar, int>("Height", 150);

	public new static readonly StyledProperty<int> WidthProperty = AvaloniaProperty.Register<RootCircleProgressBar, int>("Width", 150);

	public static readonly StyledProperty<int> StrokeWidthProperty = AvaloniaProperty.Register<RootCircleProgressBar, int>("StrokeWidth", 10);

	public static readonly StyledProperty<IBrush> StrokeBrushProperty = AvaloniaProperty.Register<RootCircleProgressBar, IBrush>("StrokeBrush");

	public static readonly StyledProperty<bool> IsIndeterminateProperty = AvaloniaProperty.Register<RootCircleProgressBar, bool>("IsIndeterminate", false);

	[CompilerGenerated]
	private static Action<object> _0021XamlIlPopulateOverride;

	public new int Height
	{
		set
		{
			SetValue(HeightProperty, value2);
		}
	}

	public new int Width
	{
		set
		{
			SetValue(WidthProperty, value2);
		}
	}

	public int StrokeWidth
	{
		set
		{
			SetValue(StrokeWidthProperty, value2);
		}
	}

	public RootCircleProgressBar()
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
	private unsafe static void _0021XamlIlPopulate(IServiceProvider P_0, RootCircleProgressBar P_1)
	{
		CompiledAvaloniaXaml.XamlIlContext.Context<RootCircleProgressBar> context = new CompiledAvaloniaXaml.XamlIlContext.Context<RootCircleProgressBar>(P_0, new object[1] { _0021AvaloniaResources.NamespaceInfo_003A_002FControls_002FRootCircleProgressBar_002Eaxaml.Singleton }, "avares://RootApp.Client.Avalonia/Controls/RootCircleProgressBar.axaml")
		{
			RootObject = P_1,
			IntermediateRoot = P_1
		};
		((ISupportInitialize)P_1).BeginInit();
		context.PushParent(P_1);
		Styles styles = P_1.Styles;
		Style style2;
		Style style = (style2 = new Style());
		context.PushParent(style2);
		style2.Selector = ((Selector?)null).OfType(typeof(RootCircleProgressBar));
		Setter setter2;
		Setter setter = (setter2 = new Setter());
		context.PushParent(setter2);
		setter2.Property = TemplatedControl.TemplateProperty;
		ControlTemplate controlTemplate;
		ControlTemplate value = (controlTemplate = new ControlTemplate());
		context.PushParent(controlTemplate);
		controlTemplate.Content = XamlIlRuntimeHelpers.DeferredTransformationFactoryV3<Control>((nint)(delegate*<IServiceProvider, object>)(&XamlClosure_16.Build_1), context);
		context.PopParent();
		setter2.Value = value;
		context.PopParent();
		style2.Add(setter);
		Style style3 = new Style();
		style3.Selector = ((Selector?)null).Nesting().PropertyEquals(IsIndeterminateProperty, true).Template()
			.OfType(typeof(Arc))
			.Name("PART_ArcFill");
		IList<IAnimation> animations = style3.Animations;
		Animation animation = new Animation();
		animation.IterationCount = IterationCount.Parse("Infinite");
		animation.Duration = TimeSpan.FromTicks(12000000L);
		KeyFrames children = animation.Children;
		KeyFrame keyFrame = new KeyFrame();
		keyFrame.Cue = Cue.Parse("0%", CultureInfo.InvariantCulture);
		AvaloniaList<IAnimationSetter> setters = keyFrame.Setters;
		Setter setter3 = new Setter();
		setter3.Property = Arc.StartAngleProperty;
		setter3.Value = 270.0;
		setters.Add(setter3);
		children.Add(keyFrame);
		KeyFrames children2 = animation.Children;
		KeyFrame keyFrame2 = new KeyFrame();
		keyFrame2.Cue = Cue.Parse("100%", CultureInfo.InvariantCulture);
		AvaloniaList<IAnimationSetter> setters2 = keyFrame2.Setters;
		Setter setter4 = new Setter();
		setter4.Property = Arc.StartAngleProperty;
		setter4.Value = 630.0;
		setters2.Add(setter4);
		children2.Add(keyFrame2);
		animations.Add(animation);
		Setter setter5 = new Setter();
		setter5.Property = Arc.SweepAngleProperty;
		setter5.Value = 90.0;
		style3.Add(setter5);
		style2.Add(style3);
		Style style4 = new Style();
		style4.Selector = ((Selector?)null).Nesting().PropertyEquals(IsIndeterminateProperty, false).Template()
			.OfType(typeof(Arc))
			.Name("PART_ArcFill");
		Setter setter6 = new Setter();
		setter6.Property = Arc.StartAngleProperty;
		setter6.Value = 270.0;
		style4.Add(setter6);
		style2.Add(style4);
		context.PopParent();
		styles.Add(style);
		context.PopParent();
		((ISupportInitialize)P_1).EndInit();
		if (P_1 is StyledElement styledElement)
		{
			NameScope.SetNameScope(styledElement, context.AvaloniaNameScope);
		}
		context.AvaloniaNameScope.Complete();
	}

	[CompilerGenerated]
	private static void _0021XamlIlPopulateTrampoline(RootCircleProgressBar P_0)
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

