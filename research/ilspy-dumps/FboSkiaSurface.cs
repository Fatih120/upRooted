// Avalonia.Skia, Version=11.3.10.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Skia.FboSkiaSurface
using System;
using Avalonia;
using Avalonia.OpenGL;
using Avalonia.Platform;
using Avalonia.Skia;
using SkiaSharp;

internal class FboSkiaSurface : ISkiaSurface, IDisposable
{
	private readonly GlSkiaGpu _gpu;

	private readonly GRContext _grContext;

	private readonly IGlContext _glContext;

	private readonly PixelSize _pixelSize;

	private int _fbo;

	private int _depthStencil;

	private int _texture;

	private SKSurface? _surface;

	private static readonly bool[] TrueFalse = new bool[2] { true, false };

	public SKSurface Surface => _surface ?? throw new ObjectDisposedException("FboSkiaSurface");

	public bool CanBlit { get; }

	public FboSkiaSurface(GlSkiaGpu P_0, GRContext P_1, IGlContext P_2, PixelSize P_3, GRSurfaceOrigin P_4)
	{
		_gpu = P_0;
		_grContext = P_1;
		_glContext = P_2;
		_pixelSize = P_3;
		int num = ((P_2.Version.Type == GlProfileType.OpenGLES) ? 6408 : 32856);
		GlInterface glInterface = P_2.GlInterface;
		glInterface.GetIntegerv(36006, out var num2);
		glInterface.GetIntegerv(36007, out var num3);
		glInterface.GetIntegerv(32873, out var num4);
		_fbo = glInterface.GenFramebuffer();
		glInterface.BindFramebuffer(36160, _fbo);
		_texture = glInterface.GenTexture();
		glInterface.BindTexture(3553, _texture);
		glInterface.TexImage2D(3553, 0, num, P_3.Width, P_3.Height, 0, 6408, 5121, IntPtr.Zero);
		glInterface.TexParameteri(3553, 10240, 9728);
		glInterface.TexParameteri(3553, 10241, 9728);
		glInterface.FramebufferTexture2D(36160, 36064, 3553, _texture, 0);
		bool flag = false;
		bool[] trueFalse = TrueFalse;
		foreach (bool num5 in trueFalse)
		{
			_depthStencil = glInterface.GenRenderbuffer();
			glInterface.BindRenderbuffer(36161, _depthStencil);
			if (num5)
			{
				glInterface.RenderbufferStorage(36161, 36168, P_3.Width, P_3.Height);
				glInterface.FramebufferRenderbuffer(36160, 36128, 36161, _depthStencil);
			}
			else
			{
				glInterface.RenderbufferStorage(36161, 35056, P_3.Width, P_3.Height);
				glInterface.FramebufferRenderbuffer(36160, 36096, 36161, _depthStencil);
				glInterface.FramebufferRenderbuffer(36160, 36128, 36161, _depthStencil);
			}
			if (glInterface.CheckFramebufferStatus(36160) == 36053)
			{
				flag = true;
				break;
			}
			glInterface.BindRenderbuffer(36161, num3);
			glInterface.DeleteRenderbuffer(_depthStencil);
		}
		glInterface.BindRenderbuffer(36161, num3);
		glInterface.BindTexture(3553, num4);
		glInterface.BindFramebuffer(36160, num2);
		if (!flag)
		{
			glInterface.DeleteFramebuffer(_fbo);
			glInterface.DeleteTexture(_texture);
			throw new OpenGlException("Unable to create FBO with stencil");
		}
		using GRBackendRenderTarget gRBackendRenderTarget = new GRBackendRenderTarget(P_3.Width, P_3.Height, 0, 8, new GRGlFramebufferInfo((uint)_fbo, SKColorType.Rgba8888.ToGlSizedFormat()));
		using SKSurfaceProperties sKSurfaceProperties = new SKSurfaceProperties(SKPixelGeometry.RgbHorizontal);
		_surface = SKSurface.Create(_grContext, gRBackendRenderTarget, P_4, SKColorType.Rgba8888, sKSurfaceProperties);
		CanBlit = glInterface.IsBlitFramebufferAvailable;
	}

	public void Dispose()
	{
		try
		{
			using (_glContext.EnsureCurrent())
			{
				_surface?.Dispose();
				_surface = null;
				GlInterface glInterface = _glContext.GlInterface;
				if (_fbo != 0)
				{
					glInterface.DeleteFramebuffer(_fbo);
					glInterface.DeleteTexture(_texture);
					glInterface.DeleteRenderbuffer(_depthStencil);
				}
			}
		}
		catch (PlatformGraphicsContextLostException)
		{
			if (_surface != null)
			{
				_gpu.AddPostDispose(_surface.Dispose);
			}
			_surface = null;
		}
		finally
		{
			_fbo = (_texture = (_depthStencil = 0));
		}
	}

	public void Blit(SKCanvas P_0)
	{
		P_0.Clear();
		P_0.Flush();
		GlInterface glInterface = _glContext.GlInterface;
		glInterface.GetIntegerv(36010, out var num);
		glInterface.BindFramebuffer(36008, _fbo);
		glInterface.BlitFramebuffer(0, 0, _pixelSize.Width, _pixelSize.Height, 0, 0, _pixelSize.Width, _pixelSize.Height, 16384, 9729);
		glInterface.BindFramebuffer(36008, num);
	}
}

