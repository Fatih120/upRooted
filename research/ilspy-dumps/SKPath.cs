// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKPath
using System;
using System.Reflection;
using SkiaSharp;

[DefaultMember("Item")]
public class SKPath : SKObject, ISKSkipObjectRegistration
{
	public class Iterator : SKObject, ISKSkipObjectRegistration
	{
		private readonly SKPath path;

		internal Iterator(SKPath P_0, bool P_1)
			: base(SkiaApi.sk_path_create_iter(P_0.Handle, P_1 ? 1 : 0), true)
		{
			path = P_0;
		}

		protected override void Dispose(bool P_0)
		{
			base.Dispose(P_0);
		}

		protected override void DisposeNative()
		{
			SkiaApi.sk_path_iter_destroy(Handle);
		}

		public unsafe SKPathVerb Next(Span<SKPoint> P_0)
		{
			if (P_0 == null)
			{
				throw new ArgumentNullException("points");
			}
			if (P_0.Length != 4)
			{
				throw new ArgumentException("Must be an array of four elements.", "points");
			}
			fixed (SKPoint* ptr = P_0)
			{
				return SkiaApi.sk_path_iter_next(Handle, ptr);
			}
		}

		public float ConicWeight()
		{
			return SkiaApi.sk_path_iter_conic_weight(Handle);
		}
	}

	public class RawIterator : SKObject, ISKSkipObjectRegistration
	{
		private readonly SKPath path;

		internal RawIterator(SKPath P_0)
			: base(SkiaApi.sk_path_create_rawiter(P_0.Handle), true)
		{
			path = P_0;
		}

		protected override void Dispose(bool P_0)
		{
			base.Dispose(P_0);
		}

		protected override void DisposeNative()
		{
			SkiaApi.sk_path_rawiter_destroy(Handle);
		}

		public SKPathVerb Next(SKPoint[] P_0)
		{
			return Next(new Span<SKPoint>(P_0));
		}

		public unsafe SKPathVerb Next(Span<SKPoint> P_0)
		{
			if (P_0 == null)
			{
				throw new ArgumentNullException("points");
			}
			if (P_0.Length != 4)
			{
				throw new ArgumentException("Must be an array of four elements.", "points");
			}
			fixed (SKPoint* ptr = P_0)
			{
				return SkiaApi.sk_path_rawiter_next(Handle, ptr);
			}
		}
	}

	public SKPathFillType FillType
	{
		get
		{
			return SkiaApi.sk_path_get_filltype(Handle);
		}
		set
		{
			SkiaApi.sk_path_set_filltype(Handle, sKPathFillType);
		}
	}

	public SKRect TightBounds
	{
		get
		{
			if (GetTightBounds(out var result))
			{
				return result;
			}
			return SKRect.Empty;
		}
	}

	internal SKPath(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	public SKPath()
		: this(SkiaApi.sk_path_new(), true)
	{
		if (Handle == IntPtr.Zero)
		{
			throw new InvalidOperationException("Unable to create a new SKPath instance.");
		}
	}

	public SKPath(SKPath P_0)
		: this(SkiaApi.sk_path_clone(P_0.Handle), true)
	{
		if (Handle == IntPtr.Zero)
		{
			throw new InvalidOperationException("Unable to copy the SKPath instance.");
		}
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeNative()
	{
		SkiaApi.sk_path_delete(Handle);
	}

	public bool Contains(float P_0, float P_1)
	{
		return SkiaApi.sk_path_contains(Handle, P_0, P_1);
	}

	public void MoveTo(SKPoint P_0)
	{
		SkiaApi.sk_path_move_to(Handle, P_0.X, P_0.Y);
	}

	public void MoveTo(float P_0, float P_1)
	{
		SkiaApi.sk_path_move_to(Handle, P_0, P_1);
	}

	public void LineTo(SKPoint P_0)
	{
		SkiaApi.sk_path_line_to(Handle, P_0.X, P_0.Y);
	}

	public void LineTo(float P_0, float P_1)
	{
		SkiaApi.sk_path_line_to(Handle, P_0, P_1);
	}

	public void QuadTo(SKPoint P_0, SKPoint P_1)
	{
		SkiaApi.sk_path_quad_to(Handle, P_0.X, P_0.Y, P_1.X, P_1.Y);
	}

	public void QuadTo(float P_0, float P_1, float P_2, float P_3)
	{
		SkiaApi.sk_path_quad_to(Handle, P_0, P_1, P_2, P_3);
	}

	public void ConicTo(SKPoint P_0, SKPoint P_1, float P_2)
	{
		SkiaApi.sk_path_conic_to(Handle, P_0.X, P_0.Y, P_1.X, P_1.Y, P_2);
	}

	public void CubicTo(SKPoint P_0, SKPoint P_1, SKPoint P_2)
	{
		SkiaApi.sk_path_cubic_to(Handle, P_0.X, P_0.Y, P_1.X, P_1.Y, P_2.X, P_2.Y);
	}

	public void CubicTo(float P_0, float P_1, float P_2, float P_3, float P_4, float P_5)
	{
		SkiaApi.sk_path_cubic_to(Handle, P_0, P_1, P_2, P_3, P_4, P_5);
	}

	public void ArcTo(float P_0, float P_1, float P_2, SKPathArcSize P_3, SKPathDirection P_4, float P_5, float P_6)
	{
		SkiaApi.sk_path_arc_to(Handle, P_0, P_1, P_2, P_3, P_4, P_5, P_6);
	}

	public void Close()
	{
		SkiaApi.sk_path_close(Handle);
	}

	public unsafe void AddRect(SKRect P_0, SKPathDirection P_1 = SKPathDirection.Clockwise)
	{
		SkiaApi.sk_path_add_rect(Handle, &P_0, P_1);
	}

	public unsafe void AddOval(SKRect P_0, SKPathDirection P_1 = SKPathDirection.Clockwise)
	{
		SkiaApi.sk_path_add_oval(Handle, &P_0, P_1);
	}

	public unsafe void Transform(SKMatrix P_0)
	{
		SkiaApi.sk_path_transform(Handle, &P_0);
	}

	public void AddPath(SKPath P_0, SKPathAddMode P_1 = SKPathAddMode.Append)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("other");
		}
		SkiaApi.sk_path_add_path(Handle, P_0.Handle, P_1);
	}

	public unsafe void AddRoundRect(SKRect P_0, float P_1, float P_2, SKPathDirection P_3 = SKPathDirection.Clockwise)
	{
		SkiaApi.sk_path_add_rounded_rect(Handle, &P_0, P_1, P_2, P_3);
	}

	public void AddCircle(float P_0, float P_1, float P_2, SKPathDirection P_3 = SKPathDirection.Clockwise)
	{
		SkiaApi.sk_path_add_circle(Handle, P_0, P_1, P_2, P_3);
	}

	public unsafe void AddPoly(SKPoint[] P_0, bool P_1 = true)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("points");
		}
		fixed (SKPoint* ptr = P_0)
		{
			SkiaApi.sk_path_add_poly(Handle, ptr, P_0.Length, P_1);
		}
	}

	public Iterator CreateIterator(bool P_0)
	{
		return new Iterator(this, P_0);
	}

	public RawIterator CreateRawIterator()
	{
		return new RawIterator(this);
	}

	public bool Op(SKPath P_0, SKPathOp P_1, SKPath P_2)
	{
		if (P_0 == null)
		{
			throw new ArgumentNullException("other");
		}
		if (P_2 == null)
		{
			throw new ArgumentNullException("result");
		}
		return SkiaApi.sk_pathop_op(Handle, P_0.Handle, P_1, P_2.Handle);
	}

	public SKPath Op(SKPath P_0, SKPathOp P_1)
	{
		SKPath sKPath = new SKPath();
		if (Op(P_0, P_1, sKPath))
		{
			return sKPath;
		}
		sKPath.Dispose();
		return null;
	}

	public unsafe bool GetTightBounds(out SKRect P_0)
	{
		fixed (SKRect* ptr = &P_0)
		{
			return SkiaApi.sk_pathop_tight_bounds(Handle, ptr);
		}
	}

	internal static SKPath GetObject(IntPtr P_0, bool P_1 = true)
	{
		if (!(P_0 == IntPtr.Zero))
		{
			return new SKPath(P_0, P_1);
		}
		return null;
	}
}

