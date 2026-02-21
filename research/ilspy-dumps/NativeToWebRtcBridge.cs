using System.Threading.Tasks;
using DotNetBrowser.Js;
using Microsoft.VisualStudio.Threading;
using RootApp.Browser.WebRtc.Models;

namespace RootApp.Browser.WebRtc.Bridges;

public class NativeToWebRtcBridge
{
	private readonly IJsObject _hook;

	private volatile bool _disposed;

	public NativeToWebRtcBridge(IJsObject P_0)
	{
		_hook = P_0;
	}

	public void MarkDisposed()
	{
		_disposed = true;
	}

	public void Initialize(IJsObject P_0)
	{
		_hook.InvokeAsync("initialize", P_0).Forget();
	}

	public void Disconnect()
	{
		if (!_disposed)
		{
			_hook.InvokeAsync("disconnect").Forget();
		}
	}

	public void SetIsVideoOn(bool P_0)
	{
		if (!_disposed)
		{
			_hook.InvokeAsync("setIsVideoOn", P_0).Forget();
		}
	}

	public void SetScreenQualityMode(ScreenQualityMode P_0)
	{
		if (!_disposed)
		{
			_hook.InvokeAsync("setScreenQualityMode", P_0).Forget();
		}
	}

	public void SetIsScreenShareOn(bool P_0, bool P_1 = false)
	{
		if (!_disposed)
		{
			_hook.InvokeAsync("setIsScreenShareOn", P_0, P_1).Forget();
		}
	}

	public void UpdateVideoDeviceId(string P_0)
	{
		if (!_disposed)
		{
			_hook.InvokeAsync("updateVideoDeviceId", P_0).Forget();
		}
	}

	public void UpdateAudioInputDeviceId(string P_0)
	{
		if (!_disposed)
		{
			_hook.InvokeAsync("updateAudioInputDeviceId", P_0).Forget();
		}
	}

	public void UpdateAudioOutputDeviceId(string P_0)
	{
		if (!_disposed)
		{
			_hook.InvokeAsync("updateAudioOutputDeviceId", P_0).Forget();
		}
	}

	public void SetPushToTalkMode(bool P_0)
	{
		if (!_disposed)
		{
			_hook.InvokeAsync("setPushToTalkMode", P_0).Forget();
		}
	}

	public void SetPushToTalk(bool P_0)
	{
		if (!_disposed)
		{
			_hook.InvokeAsync("setPushToTalk", P_0).Forget();
		}
	}

	public void SetMute(bool P_0)
	{
		if (!_disposed)
		{
			_hook.InvokeAsync("setMute", P_0).Forget();
		}
	}

	public void SetDeafen(bool P_0)
	{
		if (!_disposed)
		{
			_hook.InvokeAsync("setDeafen", P_0).Forget();
		}
	}

	public void SetTheme(string P_0)
	{
		if (!_disposed)
		{
			_hook.InvokeAsync("setTheme", P_0).Forget();
		}
	}

	public void SetDenoisePower(float P_0)
	{
		if (!_disposed)
		{
			_hook.InvokeAsync("setDenoisePower", P_0).Forget();
		}
	}

	public void SetInputVolume(float P_0)
	{
		if (!_disposed)
		{
			_hook.InvokeAsync("setInputVolume", P_0).Forget();
		}
	}

	public void SetOutputVolume(float P_0)
	{
		if (!_disposed)
		{
			_hook.InvokeAsync("setOutputVolume", P_0).Forget();
		}
	}

	public void SetTileVolume(string P_0, string P_1, float P_2)
	{
		if (!_disposed)
		{
			_hook.InvokeAsync("setTileVolume", P_0, P_1, P_2).Forget();
		}
	}

	public void ScreenPickerDismissed()
	{
		if (!_disposed)
		{
			_hook.InvokeAsync("screenPickerDismissed").Forget();
		}
	}

	public void ReceiveRawPacketContainer(byte[] P_0)
	{
		if (!_disposed)
		{
			_hook.InvokeAsync("receiveRawPacketContainer", P_0).Forget();
		}
	}

	public async Task NativeLoopbackAudioStartedAsync(int P_0, int P_1)
	{
		if (!_disposed)
		{
			await _hook.InvokeAsync("nativeLoopbackAudioStarted", P_0, P_1);
		}
	}

	public void SendNativeLoopbackAudioData(byte[] P_0, int P_1)
	{
		if (!_disposed)
		{
			_hook.Invoke("receiveNativeLoopbackAudioData", P_0, P_1);
		}
	}

	public void StopNativeLoopbackAudio()
	{
		if (!_disposed)
		{
			_hook.InvokeAsync("stopNativeLoopbackAudio").Forget();
		}
	}
}
