# Avalonia Skia Custom Draw Findings (2026-02-21)

## Scope

Investigated whether the current decompiled Root/Avalonia dumps contain the required APIs and patterns for implementing a custom Avalonia 11 control that renders a position-aware sweep-gradient border using SkiaSharp + custom draw operations.

This report captures only findings from the current research pass.

## Source Batches Processed

### First Skia batch (3 files)
- `research/ilspy-dumps/AvaloniaSkiaAvalonia.cs`
- `research/ilspy-dumps/AvaloniaSkiaAvaloniaSkia.cs`
- `research/ilspy-dumps/AvaloniaSkiaAvaloniaSkiaHelpers.cs`

Split results:
- 2 files from `AvaloniaSkiaAvalonia.cs`
- 44 files from `AvaloniaSkiaAvaloniaSkia.cs`
- 5 files from `AvaloniaSkiaAvaloniaSkiaHelpers.cs`

Total: **51 files** generated.

### Default Avalonia batch (3 files)
- `research/ilspy-dumps/AvaloniaRendering.cs`
- `research/ilspy-dumps/AvaloniaRenderingSceneGraph.cs`
- `research/ilspy-dumps/AvaloniaUtilities.cs`

Split results:
- 20 files from `AvaloniaRendering.cs`
- 2 files from `AvaloniaRenderingSceneGraph.cs`
- 52 files from `AvaloniaUtilities.cs`

Total: **74 files** generated.

## Confirmed APIs and Patterns

### 1. `ISkiaSharpApiLease` exists and is usable

Evidence:
- `research/ilspy-dumps/ISkiaSharpApiLease.cs`
- `research/ilspy-dumps/ISkiaSharpApiLeaseFeature.cs`
- `research/ilspy-dumps/ISkiaSharpPlatformGraphicsApiLease.cs`

Signatures:
- `ISkiaSharpApiLeaseFeature.Lease()`
- `ISkiaSharpApiLease.SkCanvas`
- `ISkiaSharpApiLease.GrContext`
- `ISkiaSharpApiLease.TryLeasePlatformGraphicsApi()`

### 2. Lease feature is exposed by Skia drawing context impl

Evidence:
- `research/ilspy-dumps/DrawingContextImpl.cs`

Key points:
- `GetFeature(Type)` returns `SkiaLeaseFeature` when requested type is `ISkiaSharpApiLeaseFeature`.
- `Lease()` returns an `ApiLease` object.
- Lease tracks/guards re-entrancy with `_leased` checks.
- On lease dispose, canvas matrix is restored (`SetMatrix(_revertTransform)`).

### 3. `ICustomDrawOperation` exists in scene graph

Evidence:
- `research/ilspy-dumps/ICustomDrawOperation.cs`

Interface shape:
- `Rect Bounds { get; }`
- `bool HitTest(Point)`
- `void Render(ImmediateDrawingContext)`
- Extends `IEquatable<ICustomDrawOperation>` and `IDisposable`.

Related:
- `research/ilspy-dumps/ICustomHitTest.cs`
- `research/ilspy-dumps/IHitTester.cs`

### 4. Sweep gradient support is present in Skia renderer path

Evidence:
- `research/ilspy-dumps/DrawingContextImpl.cs`

Observed call shape:
- `SKShader.CreateSweepGradient(center, colors, positions, matrix)`

Important: current renderer usage found in dumps relies on matrix-based sweep gradient call, not startAngle/endAngle overload usage.

## Version Signals

- Avalonia.Skia dump assembly header shows: `Avalonia.Skia, Version=11.3.10.0`.
- Base Avalonia dump headers show: `Avalonia.Base, Version=11.3.12.0`.
- Existing session/runtime notes in hook docs indicate Avalonia 11.3.x in target runtime.

## What Was Not Found in Current Dumps

### 1. `DrawingContext.Custom(...)` call sites

`ICustomDrawOperation` is present, but explicit call-site decompilation for `DrawingContext.Custom(...)` was not found in the currently split files.

Likely cause: `DrawingContext` / `ImmediateDrawingContext` concrete files are in additional Avalonia assemblies not yet dumped into this set.

### 2. Direct `ISkiaSharpApiLease` usage from app code

No Root app-level code in current dumps directly demonstrating custom-op render obtaining a lease was found yet.

## Practical Implementation Guidance (from findings)

Based on the decompiled APIs currently available:

1. Implement custom operation via `ICustomDrawOperation` (`Render(ImmediateDrawingContext)`).
2. From the drawing context impl, request `ISkiaSharpApiLeaseFeature` via `GetFeature` and call `Lease()`.
3. Use `lease.SkCanvas` for raw Skia drawing.
4. Dispose lease reliably (matrix state restoration is expected on dispose).
5. For broad compatibility with observed renderer path, prefer matrix-based sweep gradients over assuming start/end-angle overload usage.

## Open Items

To complete exact end-to-end call mapping, dump/split the classes containing:
- `Avalonia.Media.DrawingContext`
- `Avalonia.Media.ImmediateDrawingContext`

Then trace:
- `DrawingContext.Custom(...)`
- handoff from `ICustomDrawOperation.Render(ImmediateDrawingContext)` to platform impl and feature access.

## Files Added By This Investigation (examples)

From Skia split:
- `research/ilspy-dumps/ISkiaSharpApiLease.cs`
- `research/ilspy-dumps/ISkiaSharpApiLeaseFeature.cs`
- `research/ilspy-dumps/ISkiaSharpPlatformGraphicsApiLease.cs`
- `research/ilspy-dumps/DrawingContextImpl.cs`

From Avalonia rendering split:
- `research/ilspy-dumps/ICustomDrawOperation.cs`
- `research/ilspy-dumps/ICustomHitTest.cs`
- `research/ilspy-dumps/ImmediateRenderer.cs`
- `research/ilspy-dumps/IHitTester.cs`

