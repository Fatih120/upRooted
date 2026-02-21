// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.ImmediateRenderer
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Media;
using Avalonia.Rendering;
using Avalonia.VisualTree;

internal class ImmediateRenderer
{
	public static void Render(DrawingContext P_0, Visual P_1)
	{
		Render(P_0, P_1, new Rect(P_1.Bounds.Size));
	}

	public static void Render(DrawingContext P_0, Visual P_1, Rect P_2)
	{
		using (P_0.PushTransform(Matrix.CreateTranslation(0.0 - P_2.Position.X, 0.0 - P_2.Position.Y)))
		{
			using (P_0.PushClip(P_2))
			{
				Render(P_0, P_1, new Rect(P_1.Bounds.Size), Matrix.Identity, new Rect(P_2.Size));
			}
		}
	}

	private static void Render(DrawingContext P_0, Visual P_1, Rect P_2, Matrix P_3, Rect P_4)
	{
		if (!P_1.IsVisible)
		{
			return;
		}
		double opacity = P_1.Opacity;
		if (!(opacity > 0.0))
		{
			return;
		}
		Rect rect = new Rect(P_2.Size);
		Matrix? matrix = P_1.RenderTransform?.Value;
		Matrix matrix3;
		if (matrix.HasValue)
		{
			Matrix valueOrDefault = matrix.GetValueOrDefault();
			Matrix matrix2 = Matrix.CreateTranslation(P_1.RenderTransformOrigin.ToPixels(P_1.Bounds.Size));
			matrix3 = -matrix2 * valueOrDefault * matrix2 * Matrix.CreateTranslation(P_2.Position);
		}
		else
		{
			matrix3 = Matrix.CreateTranslation(P_2.Position);
		}
		using ((P_1.RenderOptions != default(RenderOptions)) ? new DrawingContext.PushedState?(P_0.PushRenderOptions(P_1.RenderOptions)) : ((DrawingContext.PushedState?)null))
		{
			using (P_0.PushTransform(matrix3))
			{
				using (P_1.HasMirrorTransform ? new DrawingContext.PushedState?(P_0.PushTransform(new Matrix(-1.0, 0.0, 0.0, 1.0, P_1.Bounds.Width, 0.0))) : ((DrawingContext.PushedState?)null))
				{
					using (P_0.PushOpacity(opacity))
					{
						DrawingContext.PushedState? pushedState = ((P_1 == null || !P_1.ClipToBounds) ? ((DrawingContext.PushedState?)null) : ((!(P_1 is IVisualWithRoundRectClip visualWithRoundRectClip)) ? new DrawingContext.PushedState?(P_0.PushClip(rect)) : new DrawingContext.PushedState?(P_0.PushClip(new RoundedRect(in rect, visualWithRoundRectClip.ClipToBoundsRadius)))));
						using (pushedState)
						{
							Geometry clip = P_1.Clip;
							using ((clip != null) ? new DrawingContext.PushedState?(P_0.PushGeometryClip(clip)) : ((DrawingContext.PushedState?)null))
							{
								IBrush opacityMask = P_1.OpacityMask;
								using ((opacityMask != null) ? new DrawingContext.PushedState?(P_0.PushOpacityMask(opacityMask, rect)) : ((DrawingContext.PushedState?)null))
								{
									Matrix matrix4 = matrix3 * P_3;
									if (rect.TransformToAABB(matrix4).Intersects(P_4))
									{
										P_1.Render(P_0);
									}
									IEnumerable<Visual> enumerable;
									if (!P_1.HasNonUniformZIndexChildren)
									{
										IEnumerable<Visual> visualChildren = P_1.VisualChildren;
										enumerable = visualChildren;
									}
									else
									{
										IEnumerable<Visual> visualChildren = P_1.VisualChildren.OrderBy((Visual x) => x, ZIndexComparer.Instance);
										enumerable = visualChildren;
									}
									if (P_1.ClipToBounds)
									{
										matrix4 = Matrix.Identity;
										P_4 = rect;
									}
									foreach (Visual item in enumerable)
									{
										Render(P_0, item, item.Bounds, matrix4, P_4);
									}
								}
							}
						}
					}
				}
			}
		}
	}
}

