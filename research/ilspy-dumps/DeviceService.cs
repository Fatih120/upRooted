using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using DotNetBrowser.Handlers;
using DotNetBrowser.Js;
using DotNetBrowser.Js.Collections;
using DotNetBrowser.Media;
using DotNetBrowser.Media.Handlers;
using RootApp.Browser.Models;
using RootApp.Browser.WebRtc.Models;
using RootApp.Client.Domain.Helpers.Store;
using RootApp.Client.Domain.Helpers.Store.State.Media;

namespace RootApp.Browser.WebRtc.Services;

public class DeviceService : IDisposable
{
	private readonly IJsObject _window;

	private readonly ILocalDataStore _localDataStore;

	private readonly MediaDeviceState _mediaDeviceState;

	[CompilerGenerated]
	private Action<MediaDeviceInfo>? m_SelectedDeviceChanged;

	[CompilerGenerated]
	private Action<float>? m_NoiseSuppressionStrengthChanged;

	[CompilerGenerated]
	private Action<bool, bool>? m_MediaConstraintsChanged;

	public event Action<MediaDeviceInfo>? SelectedDeviceChanged
	{
		[CompilerGenerated]
		add
		{
			Action<MediaDeviceInfo> action = this.m_SelectedDeviceChanged;
			Action<MediaDeviceInfo> action2;
			do
			{
				action2 = action;
				Action<MediaDeviceInfo> action3 = (Action<MediaDeviceInfo>)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_SelectedDeviceChanged, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action<MediaDeviceInfo> action = this.m_SelectedDeviceChanged;
			Action<MediaDeviceInfo> action2;
			do
			{
				action2 = action;
				Action<MediaDeviceInfo> action3 = (Action<MediaDeviceInfo>)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_SelectedDeviceChanged, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public event Action<float>? NoiseSuppressionStrengthChanged
	{
		[CompilerGenerated]
		add
		{
			Action<float> action = this.m_NoiseSuppressionStrengthChanged;
			Action<float> action2;
			do
			{
				action2 = action;
				Action<float> action3 = (Action<float>)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_NoiseSuppressionStrengthChanged, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action<float> action = this.m_NoiseSuppressionStrengthChanged;
			Action<float> action2;
			do
			{
				action2 = action;
				Action<float> action3 = (Action<float>)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_NoiseSuppressionStrengthChanged, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public event Action<bool, bool>? MediaConstraintsChanged
	{
		[CompilerGenerated]
		add
		{
			Action<bool, bool> action = this.m_MediaConstraintsChanged;
			Action<bool, bool> action2;
			do
			{
				action2 = action;
				Action<bool, bool> action3 = (Action<bool, bool>)Delegate.Combine(action2, b);
				action = Interlocked.CompareExchange(ref this.m_MediaConstraintsChanged, action3, action2);
			}
			while ((object)action != action2);
		}
		[CompilerGenerated]
		remove
		{
			Action<bool, bool> action = this.m_MediaConstraintsChanged;
			Action<bool, bool> action2;
			do
			{
				action2 = action;
				Action<bool, bool> action3 = (Action<bool, bool>)Delegate.Remove(action2, value2);
				action = Interlocked.CompareExchange(ref this.m_MediaConstraintsChanged, action3, action2);
			}
			while ((object)action != action2);
		}
	}

	public DeviceService(IJsObject P_0, bool P_1, ILocalDataStore P_2)
	{
		_window = P_0;
		_localDataStore = P_2;
		if (P_1)
		{
			setupMediaDeviceHandler();
		}
		if (!_localDataStore.TryGetGlobal(DataStoreKeys.MediaDevices, out _mediaDeviceState, MediaDeviceStateSerializerContext.Default.MediaDeviceState))
		{
			_mediaDeviceState = new MediaDeviceState();
		}
	}

	public async Task<IReadOnlyList<IJsObject>> GetRawAudioOutputDevicesAsync()
	{
		IReadOnlyList<IJsObject> devices = ((IJsArray)(await (await _window.Frame.ExecuteJavaScript<IJsPromise>("navigator.mediaDevices.enumerateDevices().then(devices => devices.filter(device => device.kind === 'audiooutput'));", userGesture: true)).AsPromise().ResolveAsync()).Data).ToReadOnlyList<IJsObject>();
		if (devices.Any(delegate(IJsObject device)
		{
			string text = device.Properties["kind"]?.ToString() ?? "";
			string value = device.Properties["label"]?.ToString() ?? "";
			return text == "audiooutput" && string.IsNullOrWhiteSpace(value);
		}))
		{
			await (await _window.Frame.ExecuteJavaScript<IJsPromise>("navigator.mediaDevices.getUserMedia({ audio: true })\r\n                .then(stream => {\r\n                    stream.getTracks().forEach(track => track.stop());\r\n                })\r\n                .catch(e => console.warn('Audio permission not granted', e));", userGesture: true)).AsPromise().ResolveAsync();
			devices = ((IJsArray)(await (await _window.Frame.ExecuteJavaScript<IJsPromise>("navigator.mediaDevices.enumerateDevices().then(devices => devices.filter(device => device.kind === 'audiooutput'));", userGesture: true)).AsPromise().ResolveAsync()).Data).ToReadOnlyList<IJsObject>();
		}
		return devices;
	}

	public IEnumerable<MediaDeviceInfo> GetAudioInputDevices()
	{
		IEnumerable<MediaDevice> audioCaptureDevices = _window.Frame.Browser.Engine.MediaDevices.AudioCaptureDevices;
		List<MediaDeviceInfo> list = new List<MediaDeviceInfo>();
		foreach (MediaDevice item in audioCaptureDevices)
		{
			MediaDeviceInfo mediaDeviceInfo = createMediaDevice(item);
			if (mediaDeviceInfo.Kind == "audioinput")
			{
				list.Add(mediaDeviceInfo);
			}
		}
		if (!string.IsNullOrEmpty(_mediaDeviceState.AudioInputDeviceName))
		{
			MediaDeviceInfo mediaDeviceInfo2 = list.FirstOrDefault((MediaDeviceInfo d) => d.Label == _mediaDeviceState.AudioInputDeviceName);
			if (mediaDeviceInfo2 != null)
			{
				list.Remove(mediaDeviceInfo2);
				list.Insert(0, mediaDeviceInfo2);
			}
		}
		return list;
	}

	public IEnumerable<MediaDeviceInfo> GetAudioOutputDevices(IReadOnlyList<IJsObject> P_0)
	{
		List<MediaDeviceInfo> list = new List<MediaDeviceInfo>();
		foreach (IJsObject item in P_0)
		{
			MediaDeviceInfo mediaDeviceInfo = createMediaDevice(item);
			if (mediaDeviceInfo.Kind == "audiooutput")
			{
				list.Add(mediaDeviceInfo);
			}
		}
		if (!string.IsNullOrEmpty(_mediaDeviceState.AudioOutputDeviceName))
		{
			MediaDeviceInfo mediaDeviceInfo2 = list.FirstOrDefault((MediaDeviceInfo d) => d.Label == _mediaDeviceState.AudioOutputDeviceName);
			if (mediaDeviceInfo2 != null)
			{
				list.Remove(mediaDeviceInfo2);
				list.Insert(0, mediaDeviceInfo2);
			}
		}
		return list;
	}

	public IEnumerable<MediaDeviceInfo> GetVideoInputDevices()
	{
		IEnumerable<MediaDevice> videoCaptureDevices = _window.Frame.Browser.Engine.MediaDevices.VideoCaptureDevices;
		List<MediaDeviceInfo> list = new List<MediaDeviceInfo>();
		foreach (MediaDevice item in videoCaptureDevices)
		{
			MediaDeviceInfo mediaDeviceInfo = createMediaDevice(item);
			if (mediaDeviceInfo.Kind == "videoinput")
			{
				list.Add(mediaDeviceInfo);
			}
		}
		if (!string.IsNullOrEmpty(_mediaDeviceState.VideoInputDeviceName))
		{
			MediaDeviceInfo mediaDeviceInfo2 = list.FirstOrDefault((MediaDeviceInfo d) => d.Label == _mediaDeviceState.VideoInputDeviceName);
			if (mediaDeviceInfo2 != null)
			{
				list.Remove(mediaDeviceInfo2);
				list.Insert(0, mediaDeviceInfo2);
			}
		}
		return list;
	}

	public void SelectAudioInputDevice(MediaDeviceInfo P_0)
	{
		_mediaDeviceState.AudioInputDeviceName = P_0.Label;
		_mediaDeviceState.AudioInputDeviceId = P_0.DeviceId;
		saveMediaDeviceState();
		this.SelectedDeviceChanged?.Invoke(P_0);
	}

	public void SelectAudioOutputDevice(MediaDeviceInfo P_0)
	{
		_mediaDeviceState.AudioOutputDeviceName = P_0.Label;
		_mediaDeviceState.AudioOutputDeviceId = P_0.DeviceId;
		saveMediaDeviceState();
		this.SelectedDeviceChanged?.Invoke(P_0);
	}

	public void SelectVideoInputDevice(MediaDeviceInfo P_0)
	{
		_mediaDeviceState.VideoInputDeviceName = P_0.Label;
		_mediaDeviceState.VideoInputDeviceId = P_0.DeviceId;
		saveMediaDeviceState();
		this.SelectedDeviceChanged?.Invoke(P_0);
	}

	public double GetNoiseSuppressionStrength()
	{
		return _mediaDeviceState.NoiseSuppressionStrength;
	}

	public void SetNoiseSuppressionStrength(double P_0)
	{
		if (!(Math.Abs(_mediaDeviceState.NoiseSuppressionStrength - P_0) < 0.0001))
		{
			_mediaDeviceState.NoiseSuppressionStrength = P_0;
			saveMediaDeviceState();
			this.NoiseSuppressionStrengthChanged?.Invoke((float)P_0);
		}
	}

	public bool GetEchoCancellationEnabled()
	{
		return _mediaDeviceState.EchoCancellationEnabled;
	}

	public void SetEchoCancellationEnabled(bool P_0)
	{
		_mediaDeviceState.EchoCancellationEnabled = P_0;
		saveMediaDeviceState();
		this.MediaConstraintsChanged?.Invoke(P_0, GetAutomaticGainControlEnabled());
	}

	public bool GetAutomaticGainControlEnabled()
	{
		return _mediaDeviceState.AutomaticGainControlEnabled;
	}

	public void SetAutomaticGainControlEnabled(bool P_0)
	{
		_mediaDeviceState.AutomaticGainControlEnabled = P_0;
		saveMediaDeviceState();
		this.MediaConstraintsChanged?.Invoke(GetEchoCancellationEnabled(), P_0);
	}

	public async Task<MediaDeviceState> GetSelectedMediaDeviceStateAsync()
	{
		List<MediaDeviceInfo> audioOutputDevices = new List<MediaDeviceInfo>();
		audioOutputDevices.AddRange(GetAudioOutputDevices(await GetRawAudioOutputDevicesAsync()));
		if (string.IsNullOrEmpty(_mediaDeviceState.AudioOutputDeviceName) || (!string.IsNullOrEmpty(_mediaDeviceState.AudioOutputDeviceName) && audioOutputDevices.FirstOrDefault((MediaDeviceInfo d) => d.Label == _mediaDeviceState.AudioOutputDeviceName) == null))
		{
			if (audioOutputDevices.Any())
			{
				MediaDeviceInfo firstDevice = audioOutputDevices.First();
				_mediaDeviceState.AudioOutputDeviceName = firstDevice.Label;
				_mediaDeviceState.AudioOutputDeviceId = firstDevice.DeviceId;
			}
		}
		else
		{
			MediaDeviceInfo device = audioOutputDevices.FirstOrDefault((MediaDeviceInfo d) => d.Label == _mediaDeviceState.AudioOutputDeviceName);
			if (device != null)
			{
				_mediaDeviceState.AudioOutputDeviceId = device.DeviceId;
			}
		}
		saveMediaDeviceState();
		return _mediaDeviceState;
	}

	private MediaDeviceInfo createMediaDevice(IJsObject P_0)
	{
		return new MediaDeviceInfo
		{
			DeviceId = P_0.Properties["deviceId"].ToString(),
			GroupId = P_0.Properties["groupId"].ToString(),
			Kind = P_0.Properties["kind"].ToString(),
			Label = P_0.Properties["label"].ToString()
		};
	}

	private MediaDeviceInfo createMediaDevice(MediaDevice P_0)
	{
		return new MediaDeviceInfo
		{
			DeviceId = P_0.Id,
			GroupId = "",
			Kind = ((P_0.Type == MediaDeviceType.AudioDevice) ? "audioinput" : "videoinput"),
			Label = P_0.Name
		};
	}

	private void saveMediaDeviceState()
	{
		_localDataStore.SetGlobal(DataStoreKeys.MediaDevices, _mediaDeviceState, MediaDeviceStateSerializerContext.Default.MediaDeviceState);
	}

	private void setupMediaDeviceHandler()
	{
		if (_window.Frame.Browser.Engine.MediaDevices.SelectMediaDeviceHandler != null)
		{
			return;
		}
		_window.Frame.Browser.Engine.MediaDevices.SelectMediaDeviceHandler = new Handler<SelectMediaDeviceParameters, SelectMediaDeviceResponse>(delegate(SelectMediaDeviceParameters parameters)
		{
			if (parameters.Type == MediaDeviceType.AudioDevice)
			{
				MediaDevice mediaDevice = parameters.Devices.FirstOrDefault((MediaDevice d) => d.Name == _mediaDeviceState.AudioInputDeviceName);
				if (mediaDevice == null)
				{
					mediaDevice = parameters.Devices.FirstOrDefault((MediaDevice d) => d.Type == MediaDeviceType.AudioDevice);
					if (mediaDevice != null)
					{
						_mediaDeviceState.AudioInputDeviceName = mediaDevice.Name;
						_mediaDeviceState.AudioInputDeviceId = mediaDevice.Id;
						saveMediaDeviceState();
					}
				}
				return SelectMediaDeviceResponse.Select(mediaDevice);
			}
			if (parameters.Type == MediaDeviceType.VideoDevice)
			{
				MediaDevice mediaDevice2 = parameters.Devices.FirstOrDefault((MediaDevice d) => d.Name == _mediaDeviceState.VideoInputDeviceName);
				if (mediaDevice2 == null)
				{
					mediaDevice2 = parameters.Devices.FirstOrDefault((MediaDevice d) => d.Type == MediaDeviceType.VideoDevice);
					if (mediaDevice2 != null)
					{
						_mediaDeviceState.VideoInputDeviceName = mediaDevice2.Name;
						_mediaDeviceState.VideoInputDeviceId = mediaDevice2.Id;
						saveMediaDeviceState();
					}
				}
				return SelectMediaDeviceResponse.Select(mediaDevice2);
			}
			return SelectMediaDeviceResponse.Select(parameters.Devices.FirstOrDefault());
		});
	}

	public void Dispose()
	{
		_window.Frame.Browser.Engine.MediaDevices.SelectMediaDeviceHandler = null;
	}
}
