// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SKPictureRecorder
using System;
using SkiaSharp;

public class SKPictureRecorder : SKObject, ISKSkipObjectRegistration
{
	internal SKPictureRecorder(IntPtr P_0, bool P_1)
		: base(P_0, P_1)
	{
	}

	public SKPictureRecorder()
		: this(SkiaApi.sk_picture_recorder_new(), true)
	{
		if (Handle == IntPtr.Zero)
		{
			throw new InvalidOperationException("Unable to create a new SKPictureRecorder instance.");
		}
	}

	protected override void Dispose(bool P_0)
	{
		base.Dispose(P_0);
	}

	protected override void DisposeNative()
	{
		SkiaApi.sk_picture_recorder_delete(Handle);
	}

	public unsafe SKCanvas BeginRecording(SKRect P_0)
	{
		return SKObject.OwnedBy(SKCanvas.GetObject(SkiaApi.sk_picture_recorder_begin_recording(Handle, &P_0), false), this);
	}

	public SKPicture EndRecording()
	{
		return SKPicture.GetObject(SkiaApi.sk_picture_recorder_end_recording(Handle));
	}
}

