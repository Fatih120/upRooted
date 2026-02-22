# Avalonia Skia Custom Draw Findings (2026-02-21)

## Scope

Investigated whether the current decompiled Root/Avalonia dumps contain the required APIs and patterns for implementing a custom Avalonia 11 control that renders a position-aware sweep-gradient border using SkiaSharp + custom draw operations.

This report captures only findings from the current research pass.

## Source Batches Processed

### First Skia batch (3 files)
- `AvaloniaSkiaAvalonia.cs` (temporary aggregate)
- `AvaloniaSkiaAvaloniaSkia.cs` (temporary aggregate)
- `AvaloniaSkiaAvaloniaSkiaHelpers.cs` (temporary aggregate)

Split results:
- 2 files from `AvaloniaSkiaAvalonia.cs`
- 44 files from `AvaloniaSkiaAvaloniaSkia.cs`
- 5 files from `AvaloniaSkiaAvaloniaSkiaHelpers.cs`

Total: **51 files** generated.

### Default Avalonia batch (3 files)
- `AvaloniaRendering.cs` (temporary aggregate)
- `AvaloniaRenderingSceneGraph.cs` (temporary aggregate)
- `AvaloniaUtilities.cs` (temporary aggregate)

Split results:
- 20 files from `AvaloniaRendering.cs`
- 2 files from `AvaloniaRenderingSceneGraph.cs`
- 52 files from `AvaloniaUtilities.cs`

Total: **74 files** generated.

### Media/Platform/VisualTree batch (4 files, temporary)
- Aggregate files for `Avalonia.Media`, `Avalonia.Platform`, `Avalonia.Visuals.Platform`, and `Avalonia.VisualTree` were decompiled during this pass to validate the custom draw call chain.
- These unsplit aggregate files were intentionally removed after analysis to avoid repository bloat.
- `Avalonia.Media` was later re-dumped and split again, so the key custom draw files are now retained as standalone files.

Key outcome:
- The previously missing `DrawingContext.Custom(...)` and `ImmediateDrawingContext` call chain was validated from this batch before cleanup.

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

Observed renderer call shape:
- `SKShader.CreateSweepGradient(center, colors, positions, matrix)`

SkiaSharp API signatures in dumped runtime:
- `CreateSweepGradient(SKPoint, SKColor[], float[], SKMatrix)`
- `CreateSweepGradient(SKPoint, SKColor[], float[], SKShaderTileMode, float startAngle, float endAngle, SKMatrix)`

Evidence:
- `research/ilspy-dumps/SKShader.cs`

Important: current renderer usage found in dumps relies on matrix-based sweep gradient call, not startAngle/endAngle overload usage.

## Version Signals

- Avalonia.Skia dump assembly header shows: `Avalonia.Skia, Version=11.3.10.0`.
- Base Avalonia dump headers show: `Avalonia.Base, Version=11.3.12.0`.
- Existing session/runtime notes in hook docs indicate Avalonia 11.3.x in target runtime.

### 5. Full call chain for custom draw operation is now confirmed

Evidence:
- `research/ilspy-dumps/DrawingContext.cs`
- `research/ilspy-dumps/PlatformDrawingContext.cs`
- `research/ilspy-dumps/ImmediateDrawingContext.cs`
- `research/ilspy-dumps/DrawingContextImpl.cs`

Confirmed flow:
1. Control calls `DrawingContext.Custom(ICustomDrawOperation)`.
2. `PlatformDrawingContext.Custom(...)` creates `ImmediateDrawingContext(_impl, false)` and calls `op.Render(immediateCtx)`.
3. `ImmediateDrawingContext.TryGetFeature(Type)` forwards to `PlatformImpl.GetFeature(Type)`.
4. Skia backend (`DrawingContextImpl`) returns `ISkiaSharpApiLeaseFeature`.
5. Caller leases, draws via `lease.SkCanvas`, disposes lease.

This is the complete bridge from scene-graph custom op to Skia canvas access.

## What Was Not Found in Earlier Dumps (resolved)

- `DrawingContext.Custom(...)` and `ImmediateDrawingContext` were missing from earlier batches and were later validated via temporary `Avalonia.Media` aggregate decompilation; these are now retained in split files (`DrawingContext.cs`, `PlatformDrawingContext.cs`, `ImmediateDrawingContext.cs`).
- Direct Root app call sites that use `ISkiaSharpApiLease` remain out of scope for this extraction set; this is expected because lease usage is generally in custom drawing code, not necessarily in Root app controls.

## Practical Implementation Guidance (from findings)

Based on the decompiled APIs currently available:

1. Implement custom operation via `ICustomDrawOperation` (`Render(ImmediateDrawingContext)`).
2. From the drawing context impl, request `ISkiaSharpApiLeaseFeature` via `GetFeature` and call `Lease()`.
3. Use `lease.SkCanvas` for raw Skia drawing.
4. Dispose lease reliably (matrix state restoration is expected on dispose).
5. Runtime includes start/end-angle sweep overload, but matrix-based usage is what Avalonia renderer itself currently uses in this dump set.

## Open Items

- Optional only: find a concrete control in dumps that calls `DrawingContext.Custom(...)` to mirror style and lifetime/equality patterns (`ICustomDrawOperation.Equals`, `Bounds`, `Dispose`).
- Optional only: extract more scene graph node implementations if deeper hit-test/invalidation behavior needs verification.

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

From Avalonia.Media split:
- `research/ilspy-dumps/DrawingContext.cs`
- `research/ilspy-dumps/PlatformDrawingContext.cs`
- `research/ilspy-dumps/ImmediateDrawingContext.cs`

From SkiaSharp split:
- `research/ilspy-dumps/SKShader.cs`
