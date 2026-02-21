// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.GRGlInterface
using System;
using System.Runtime.InteropServices;
using SkiaSharp;

public class GRGlInterface : SKObject, ISKReferenceCounted, ISKSkipObjectRegistration
{
	private static class AngleLoader
	{
		private static readonly IntPtr libEGL;

		private static readonly IntPtr libGLESv2;

		public static bool IsValid
		{
			get
			{
				if (libEGL != IntPtr.Zero)
				{
					return libGLESv2 != IntPtr.Zero;
				}
				return false;
			}
		}

		[DllImport("Kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		private static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string P_0);

		[DllImport("Kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr P_0, [MarshalAs(UnmanagedType.LPStr)] string P_1);

		[DllImport("libEGL.dll")]
		private static extern IntPtr eglGetProcAddress([MarshalAs(UnmanagedType.LPStr)] string P_0);

		static AngleLoader()
		{
			if (1 == 0)
			{
			}
			libEGL = LoadLibrary("libEGL.dll");
			libGLESv2 = LoadLibrary("libGLESv2.dll");
		}

		public static IntPtr GetProc(string name)
		{
			if (1 == 0)
			{
			}
			if (!IsValid)
			{
				return IntPtr.Zero;
			}
			IntPtr intPtr = GetProcAddress(libGLESv2, name);
			if (intPtr == IntPtr.Zero)
			{
				intPtr = GetProcAddress(libEGL, name);
			}
			if (intPtr == IntPtr.Zero)
			{
				intPtr = eglGetProcAddress(name);
			}
			return intPtr;
		}
	}

	internal GRGlInterface(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	public static GRGlInterface Create()
	{
		return CreateGl() ?? CreateAngle();
	}

	private static GRGlInterface CreateGl()
	{
		return GetObject(SkiaApi.gr_glinterface_create_native_interface());
	}

	public static GRGlInterface CreateAngle()
	{
		_ = 1;
		return CreateAngle(AngleLoader.GetProc);
	}

	public static GRGlInterface CreateAngle(GRGlGetProcedureAddressDelegate P_0)
	{
		return CreateGles(P_0);
	}

	public unsafe static GRGlInterface CreateOpenGl(GRGlGetProcedureAddressDelegate P_0)
	{
		GCHandle gCHandle;
		IntPtr intPtr;
		GRGlGetProcProxyDelegate gRGlGetProcProxyDelegate = DelegateProxies.Create(P_0, DelegateProxies.GRGlGetProcDelegateProxy, out gCHandle, out intPtr);
		try
		{
			return GetObject(SkiaApi.gr_glinterface_assemble_gl_interface((void*)intPtr, gRGlGetProcProxyDelegate));
		}
		finally
		{
			gCHandle.Free();
		}
	}

	public unsafe static GRGlInterface CreateGles(GRGlGetProcedureAddressDelegate P_0)
	{
		GCHandle gCHandle;
		IntPtr intPtr;
		GRGlGetProcProxyDelegate gRGlGetProcProxyDelegate = DelegateProxies.Create(P_0, DelegateProxies.GRGlGetProcDelegateProxy, out gCHandle, out intPtr);
		try
		{
			return GetObject(SkiaApi.gr_glinterface_assemble_gles_interface((void*)intPtr, gRGlGetProcProxyDelegate));
		}
		finally
		{
			gCHandle.Free();
		}
	}

	internal static GRGlInterface GetObject(IntPtr P_0)
	{
		if (!(P_0 == IntPtr.Zero))
		{
			return new GRGlInterface(P_0, true);
		}
		return null;
	}
}

