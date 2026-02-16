using System.Collections;
using System.Reflection;
using Avalonia.Media;

namespace Uprooted.Tests;

/// <summary>
/// Tests that verify LinearGradientBrush can be created both directly
/// and via the same reflection approach used in AvaloniaReflection.cs.
/// This diagnoses gradient rendering issues without needing Root.
/// </summary>
public class GradientBrushTests
{
    // === Direct API tests (baseline) ===

    [Fact]
    public void DirectApi_HorizontalGradient_HasCorrectStops()
    {
        var brush = new LinearGradientBrush
        {
            StartPoint = new Avalonia.RelativePoint(0, 0, Avalonia.RelativeUnit.Relative),
            EndPoint = new Avalonia.RelativePoint(1, 0, Avalonia.RelativeUnit.Relative),
        };
        brush.GradientStops.Add(new GradientStop(Colors.White, 0.0));
        brush.GradientStops.Add(new GradientStop(Colors.Transparent, 1.0));

        Assert.Equal(2, brush.GradientStops.Count);
        Assert.Equal(0.0, brush.StartPoint.Point.X);
        Assert.Equal(0.0, brush.StartPoint.Point.Y);
        Assert.Equal(1.0, brush.EndPoint.Point.X);
        Assert.Equal(0.0, brush.EndPoint.Point.Y);
        Assert.Equal(Avalonia.RelativeUnit.Relative, brush.StartPoint.Unit);
    }

    [Fact]
    public void DirectApi_RainbowGradient_Has7Stops()
    {
        var brush = new LinearGradientBrush
        {
            StartPoint = new Avalonia.RelativePoint(0, 0, Avalonia.RelativeUnit.Relative),
            EndPoint = new Avalonia.RelativePoint(1, 0, Avalonia.RelativeUnit.Relative),
        };
        brush.GradientStops.Add(new GradientStop(Colors.Red, 0.0));
        brush.GradientStops.Add(new GradientStop(Colors.Yellow, 1.0 / 6));
        brush.GradientStops.Add(new GradientStop(Colors.Lime, 2.0 / 6));
        brush.GradientStops.Add(new GradientStop(Colors.Cyan, 3.0 / 6));
        brush.GradientStops.Add(new GradientStop(Colors.Blue, 4.0 / 6));
        brush.GradientStops.Add(new GradientStop(Colors.Magenta, 5.0 / 6));
        brush.GradientStops.Add(new GradientStop(Colors.Red, 1.0));

        Assert.Equal(7, brush.GradientStops.Count);
    }

    // === Reflection-based tests (mimicking AvaloniaReflection.cs) ===

    [Fact]
    public void Reflection_CanCreateLinearGradientBrush()
    {
        var brushType = typeof(LinearGradientBrush);
        var brush = Activator.CreateInstance(brushType);
        Assert.NotNull(brush);
    }

    [Fact]
    public void Reflection_CanCreateRelativePointWithEnumParseName()
    {
        var relPointType = typeof(Avalonia.RelativePoint);
        var relUnitType = typeof(Avalonia.RelativeUnit);

        // Use Enum.Parse by NAME (not by int value — Relative=0, Absolute=1 in Avalonia!)
        var relativeUnit = Enum.Parse(relUnitType, "Relative");
        Assert.Equal(Avalonia.RelativeUnit.Relative, relativeUnit);

        // Create via 3-param constructor
        var point = Activator.CreateInstance(relPointType, 0.5, 0.5, relativeUnit);
        Assert.NotNull(point);

        var rp = (Avalonia.RelativePoint)point!;
        Assert.Equal(0.5, rp.Point.X);
        Assert.Equal(0.5, rp.Point.Y);
        Assert.Equal(Avalonia.RelativeUnit.Relative, rp.Unit);
    }

    [Fact]
    public void Reflection_ParseMethodCreatesRelativePoints()
    {
        var relPointType = typeof(Avalonia.RelativePoint);
        var parseMethod = relPointType.GetMethod("Parse",
            BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(string) }, null);
        Assert.NotNull(parseMethod);

        var point = (Avalonia.RelativePoint)parseMethod!.Invoke(null, new object[] { "100%,0%" })!;
        Assert.Equal(1.0, point.Point.X);
        Assert.Equal(0.0, point.Point.Y);
        Assert.Equal(Avalonia.RelativeUnit.Relative, point.Unit);
    }

    [Fact]
    public void Reflection_CanSetStartEndPoints_ViaParse()
    {
        var brush = Activator.CreateInstance(typeof(LinearGradientBrush))!;
        var relPointType = typeof(Avalonia.RelativePoint);

        var parseMethod = relPointType.GetMethod("Parse",
            BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(string) }, null)!;

        var startPoint = parseMethod.Invoke(null, new object[] { "0%,0%" });
        var endPoint = parseMethod.Invoke(null, new object[] { "100%,0%" });

        brush.GetType().GetProperty("StartPoint")?.SetValue(brush, startPoint);
        brush.GetType().GetProperty("EndPoint")?.SetValue(brush, endPoint);

        var lgb = (LinearGradientBrush)brush;
        Assert.Equal(0.0, lgb.StartPoint.Point.X);
        Assert.Equal(1.0, lgb.EndPoint.Point.X);
        Assert.Equal(Avalonia.RelativeUnit.Relative, lgb.StartPoint.Unit);
    }

    [Fact]
    public void Reflection_CanSetStartEndPoints_ViaEnumParse()
    {
        var brush = Activator.CreateInstance(typeof(LinearGradientBrush))!;
        var relUnitType = typeof(Avalonia.RelativeUnit);
        var relPointType = typeof(Avalonia.RelativePoint);

        // Use Enum.Parse by NAME — Relative=0 in Avalonia (not 1!)
        var relativeUnit = Enum.Parse(relUnitType, "Relative");
        var startPoint = Activator.CreateInstance(relPointType, 0.0, 0.0, relativeUnit);
        var endPoint = Activator.CreateInstance(relPointType, 1.0, 0.0, relativeUnit);

        brush.GetType().GetProperty("StartPoint")?.SetValue(brush, startPoint);
        brush.GetType().GetProperty("EndPoint")?.SetValue(brush, endPoint);

        var lgb = (LinearGradientBrush)brush;
        Assert.Equal(0.0, lgb.StartPoint.Point.X);
        Assert.Equal(1.0, lgb.EndPoint.Point.X);
        Assert.Equal(Avalonia.RelativeUnit.Relative, lgb.StartPoint.Unit);
    }

    [Fact]
    public void Reflection_GradientStops_IsIList()
    {
        var brush = Activator.CreateInstance(typeof(LinearGradientBrush))!;
        var gradientStops = brush.GetType().GetProperty("GradientStops")?.GetValue(brush);

        Assert.NotNull(gradientStops);
        Assert.True(gradientStops is IList, $"GradientStops should be IList, actual type: {gradientStops?.GetType().FullName}");
    }

    [Fact]
    public void Reflection_CanAddStopsViaIList()
    {
        var brush = Activator.CreateInstance(typeof(LinearGradientBrush))!;
        var gradientStops = brush.GetType().GetProperty("GradientStops")?.GetValue(brush);
        Assert.NotNull(gradientStops);

        var stopType = typeof(GradientStop);
        var colorParse = typeof(Avalonia.Media.Color).GetMethod("Parse",
            BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(string) }, null);
        Assert.NotNull(colorParse);

        var stops = new[]
        {
            ("#FFFFFFFF", 0.0),
            ("#00FFFFFF", 1.0)
        };

        var stopList = gradientStops as IList;
        Assert.NotNull(stopList);

        foreach (var (hex, offset) in stops)
        {
            var color = colorParse!.Invoke(null, new object[] { hex });
            Assert.NotNull(color);

            var stop = Activator.CreateInstance(stopType);
            Assert.NotNull(stop);

            stop!.GetType().GetProperty("Color")?.SetValue(stop, color);
            stop.GetType().GetProperty("Offset")?.SetValue(stop, offset);
            stopList!.Add(stop);
        }

        var lgb = (LinearGradientBrush)brush;
        Assert.Equal(2, lgb.GradientStops.Count);
        Assert.Equal(0.0, lgb.GradientStops[0].Offset);
        Assert.Equal(1.0, lgb.GradientStops[1].Offset);
    }

    [Fact]
    public void Reflection_FullGradientCreation_MatchesDirectApi()
    {
        // This test exactly mirrors what CreateLinearGradientBrush does in AvaloniaReflection.cs
        var brushType = typeof(LinearGradientBrush);
        var stopType = typeof(GradientStop);
        var relPointType = typeof(Avalonia.RelativePoint);
        var colorParse = typeof(Avalonia.Media.Color).GetMethod("Parse",
            BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(string) }, null);

        // Create brush
        var brush = Activator.CreateInstance(brushType)!;

        // Set RelativePoints via Parse (the fixed approach)
        var relPointParse = relPointType.GetMethod("Parse",
            BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(string) }, null)!;
        var startPoint = relPointParse.Invoke(null, new object[] { "0%,0%" });
        var endPoint = relPointParse.Invoke(null, new object[] { "100%,0%" });
        brush.GetType().GetProperty("StartPoint")?.SetValue(brush, startPoint);
        brush.GetType().GetProperty("EndPoint")?.SetValue(brush, endPoint);

        // Get/create stops collection
        var gradientStops = brush.GetType().GetProperty("GradientStops")?.GetValue(brush);
        if (gradientStops == null)
        {
            var stopsType = typeof(Avalonia.Media.GradientStops);
            gradientStops = Activator.CreateInstance(stopsType);
            brush.GetType().GetProperty("GradientStops")?.SetValue(brush, gradientStops);
        }

        // Add stops exactly as the hook code does
        var stops = new[]
        {
            ("#FFFF0000", 0.0),
            ("#FFFFFF00", 1.0 / 6),
            ("#FF00FF00", 2.0 / 6),
            ("#FF00FFFF", 3.0 / 6),
            ("#FF0000FF", 4.0 / 6),
            ("#FFFF00FF", 5.0 / 6),
            ("#FFFF0000", 1.0)
        };

        if (gradientStops is IList stopList)
        {
            foreach (var (hex, offset) in stops)
            {
                var color = colorParse!.Invoke(null, new object[] { hex });
                if (color == null) continue;
                var stop = Activator.CreateInstance(stopType);
                if (stop == null) continue;
                stop.GetType().GetProperty("Color")?.SetValue(stop, color);
                stop.GetType().GetProperty("Offset")?.SetValue(stop, offset);
                stopList.Add(stop);
            }
        }

        var lgb = (LinearGradientBrush)brush;

        // Verify everything
        Assert.Equal(7, lgb.GradientStops.Count);
        Assert.Equal(Avalonia.RelativeUnit.Relative, lgb.StartPoint.Unit);
        Assert.Equal(Avalonia.RelativeUnit.Relative, lgb.EndPoint.Unit);
        Assert.Equal(0.0, lgb.StartPoint.Point.X);
        Assert.Equal(0.0, lgb.StartPoint.Point.Y);
        Assert.Equal(1.0, lgb.EndPoint.Point.X);
        Assert.Equal(0.0, lgb.EndPoint.Point.Y);

        // Verify first and last stop
        Assert.Equal(0.0, lgb.GradientStops[0].Offset);
        Assert.Equal(1.0, lgb.GradientStops[6].Offset);
        Assert.Equal(Colors.Red, lgb.GradientStops[0].Color);
    }

    // === Test Color.Parse with AARRGGBB format ===

    [Theory]
    [InlineData("#FFFFFFFF", 255, 255, 255, 255)]  // Opaque white
    [InlineData("#00FFFFFF", 0, 255, 255, 255)]     // Transparent white
    [InlineData("#FF000000", 255, 0, 0, 0)]         // Opaque black
    [InlineData("#00000000", 0, 0, 0, 0)]           // Transparent black
    [InlineData("#FFFF0000", 255, 255, 0, 0)]       // Opaque red
    public void ColorParse_AARRGGBB_ParsesCorrectly(string hex, byte a, byte r, byte g, byte b)
    {
        var color = Avalonia.Media.Color.Parse(hex);
        Assert.Equal(a, color.A);
        Assert.Equal(r, color.R);
        Assert.Equal(g, color.G);
        Assert.Equal(b, color.B);
    }

    // === Test that Type.Find works for all gradient types ===

    [Theory]
    [InlineData("Avalonia.Media.LinearGradientBrush")]
    [InlineData("Avalonia.Media.GradientStop")]
    [InlineData("Avalonia.Media.GradientStops")]
    [InlineData("Avalonia.RelativePoint")]
    [InlineData("Avalonia.RelativeUnit")]
    public void AvaloniaTypes_CanBeFound(string typeName)
    {
        Type? found = null;
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            found = asm.GetType(typeName);
            if (found != null) break;
        }
        Assert.NotNull(found);
    }
}
