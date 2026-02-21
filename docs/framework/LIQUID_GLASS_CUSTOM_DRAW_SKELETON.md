# LiquidGlass Custom Draw Skeleton

This is a typed Avalonia 11 + SkiaSharp skeleton for the LiquidGlass border effect.  
In this repo, `hook/` does not reference Avalonia assemblies directly, so keep this as implementation guidance unless you add a dedicated typed project.

## Control Skeleton

```csharp
public sealed class LiquidGlassBorder : Avalonia.Controls.Border
{
    public static readonly StyledProperty<double> StrokeThicknessProperty =
        AvaloniaProperty.Register<LiquidGlassBorder, double>(nameof(StrokeThickness), 1.5d);

    public static readonly StyledProperty<double> SweepStartAngleProperty =
        AvaloniaProperty.Register<LiquidGlassBorder, double>(nameof(SweepStartAngle), 0d);

    public static readonly StyledProperty<double> SweepEndAngleProperty =
        AvaloniaProperty.Register<LiquidGlassBorder, double>(nameof(SweepEndAngle), 360d);

    public double StrokeThickness
    {
        get => GetValue(StrokeThicknessProperty);
        set => SetValue(StrokeThicknessProperty, value);
    }

    public double SweepStartAngle
    {
        get => GetValue(SweepStartAngleProperty);
        set => SetValue(SweepStartAngleProperty, value);
    }

    public double SweepEndAngle
    {
        get => GetValue(SweepEndAngleProperty);
        set => SetValue(SweepEndAngleProperty, value);
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);
        var bounds = Bounds;
        if (bounds.Width <= 0 || bounds.Height <= 0 || StrokeThickness <= 0)
            return;

        var topLevel = TopLevel.GetTopLevel(this);
        var root = topLevel as Visual ?? VisualRoot;
        var screenPos = root is null ? null : TranslatePoint(new Point(0, 0), root);
        if (screenPos is null)
            return;

        context.Custom(new LiquidGlassBorderDrawOperation(
            bounds: bounds,
            thickness: StrokeThickness,
            screenPosition: screenPos.Value,
            startAngle: SweepStartAngle,
            endAngle: SweepEndAngle));
    }
}
```

## Draw Operation Skeleton

```csharp
internal sealed class LiquidGlassBorderDrawOperation : ICustomDrawOperation
{
    public Rect Bounds { get; }

    private readonly double _thickness;
    private readonly Point _screenPosition;
    private readonly float _startAngle;
    private readonly float _endAngle;
    private bool _disposed;

    public LiquidGlassBorderDrawOperation(
        Rect bounds,
        double thickness,
        Point screenPosition,
        double startAngle,
        double endAngle)
    {
        Bounds = bounds;
        _thickness = thickness;
        _screenPosition = screenPosition;
        _startAngle = (float)startAngle;
        _endAngle = (float)endAngle;
    }

    public void Render(ImmediateDrawingContext context)
    {
        var leaseFeature = context.TryGetFeature(typeof(ISkiaSharpApiLeaseFeature)) as ISkiaSharpApiLeaseFeature;
        if (leaseFeature is null)
            return;

        using var lease = leaseFeature.Lease();
        var canvas = lease.SkCanvas;

        var center = new SKPoint((float)(Bounds.Width * 0.5), (float)(Bounds.Height * 0.5));
        var radius = (float)Math.Max(1.0, Math.Min(Bounds.Width, Bounds.Height) * 0.5 - _thickness * 0.5);

        using var path = new SKPath();
        path.AddRoundRect(
            new SKRect((float)(_thickness * 0.5), (float)(_thickness * 0.5),
                       (float)(Bounds.Width - _thickness * 0.5), (float)(Bounds.Height - _thickness * 0.5)),
            16f, 16f);

        // Position-aware phase offset from screen-space position.
        var phase = (float)((_screenPosition.X + _screenPosition.Y) % 360.0);
        var start = _startAngle + phase;
        var end = _endAngle + phase;

        using var paint = new SKPaint
        {
            IsAntialias = true,
            Style = SKPaintStyle.Stroke,
            StrokeWidth = (float)_thickness,
            Shader = SKShader.CreateSweepGradient(
                center,
                new[]
                {
                    new SKColor(255, 255, 255, 210),
                    new SKColor(255, 255, 255, 40),
                    new SKColor(255, 255, 255, 210)
                },
                new[] { 0f, 0.5f, 1f },
                SKShaderTileMode.Clamp,
                start,
                end,
                SKMatrix.Identity)
        };

        canvas.DrawPath(path, paint);
    }

    public bool HitTest(Point p) => Bounds.Contains(p);

    public bool Equals(ICustomDrawOperation? other)
    {
        if (other is not LiquidGlassBorderDrawOperation o) return false;
        return Bounds == o.Bounds
            && _thickness.Equals(o._thickness)
            && _screenPosition == o._screenPosition
            && _startAngle.Equals(o._startAngle)
            && _endAngle.Equals(o._endAngle);
    }

    public void Dispose() => _disposed = true;
}
```

## Notes For Uprooted Hook Integration

- Current hook layer is reflection-only and does not compile against Avalonia/Skia packages.
- If you want this as production hook code, either:
  - add a typed helper assembly with Avalonia + Avalonia.Skia refs and load it, or
  - generate runtime types with Reflection.Emit that implement `ICustomDrawOperation`.
- Keep `context.Custom(...)` call in control `Render`, and lease usage strictly inside draw-op `Render`.
