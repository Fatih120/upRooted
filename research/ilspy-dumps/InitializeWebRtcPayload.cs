using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using DotNetBrowser.Frames;
using DotNetBrowser.Js;

namespace RootApp.Browser.WebRtc.Models;

public class InitializeWebRtcPayload
{
	[CompilerGenerated]
	private string _003Ctheme_003Ek__BackingField;

	[CompilerGenerated]
	private string _003CcallPlatform_003Ek__BackingField;

	[CompilerGenerated]
	private string _003CcurrentDeviceId_003Ek__BackingField;

	[CompilerGenerated]
	private WebRtcPermission? _003Cpermissions_003Ek__BackingField;

	[CompilerGenerated]
	private string _003CcurrentUserId_003Ek__BackingField;

	[CompilerGenerated]
	private string _003CcontainerId_003Ek__BackingField;

	[CompilerGenerated]
	private string? _003CcommunityId_003Ek__BackingField;

	[CompilerGenerated]
	private string _003CwebApiBaseUrl_003Ek__BackingField;

	[CompilerGenerated]
	private string _003CvideoDeviceId_003Ek__BackingField;

	[CompilerGenerated]
	private string _003CaudioInputDeviceId_003Ek__BackingField;

	[CompilerGenerated]
	private string _003CaudioOutputDeviceId_003Ek__BackingField;

	[CompilerGenerated]
	private string? _003CscreenAudioDeviceId_003Ek__BackingField;

	[CompilerGenerated]
	private char? _003CpreferredRid_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CisPushToTalkMode_003Ek__BackingField;

	[CompilerGenerated]
	private bool _003CdebugMode_003Ek__BackingField;

	[CompilerGenerated]
	private string _003Clogging_003Ek__BackingField;

	[CompilerGenerated]
	private string[]? _003CinitialTrackTypes_003Ek__BackingField;

	[CompilerGenerated]
	private double? _003CinputVolume_003Ek__BackingField;

	[CompilerGenerated]
	private double? _003CoutputVolume_003Ek__BackingField;

	[CompilerGenerated]
	private UserTileVolume[]? _003CtileVolumes_003Ek__BackingField;

	[CompilerGenerated]
	private double? _003CdefaultInputVolume_003Ek__BackingField;

	[CompilerGenerated]
	private double? _003CdefaultGlobalOutputVolume_003Ek__BackingField;

	[CompilerGenerated]
	private double? _003CdefaultPrimaryOutputVolume_003Ek__BackingField;

	[CompilerGenerated]
	private double? _003CdefaultScreenOutputVolume_003Ek__BackingField;

	[CompilerGenerated]
	private string[] _003CringingUserIds_003Ek__BackingField;

	[CompilerGenerated]
	private int _003CringTimeoutMs_003Ek__BackingField;

	[CompilerGenerated]
	private string[] _003CactiveUserIds_003Ek__BackingField;

	[CompilerGenerated]
	private WebRtcCodec[]? _003CpreferredCodecs_003Ek__BackingField;

	[CompilerGenerated]
	private string? _003CpreferredScreenContentHint_003Ek__BackingField;

	[CompilerGenerated]
	private UserMediaStreamConstraints? _003CuserMediaConstraints_003Ek__BackingField;

	[CompilerGenerated]
	private DisplayMediaStreamConstraints? _003CdisplayMediaConstraints_003Ek__BackingField;

	[CompilerGenerated]
	private Dictionary<string, bool>? _003CexperimentalFlags_003Ek__BackingField;

	public required string theme
	{
		[CompilerGenerated]
		get
		{
			return _003Ctheme_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003Ctheme_003Ek__BackingField = text;
		}
	}

	public required string callPlatform
	{
		[CompilerGenerated]
		get
		{
			return _003CcallPlatform_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CcallPlatform_003Ek__BackingField = text;
		}
	}

	public required string currentDeviceId
	{
		[CompilerGenerated]
		get
		{
			return _003CcurrentDeviceId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CcurrentDeviceId_003Ek__BackingField = text;
		}
	}

	public WebRtcPermission? permissions
	{
		[CompilerGenerated]
		get
		{
			return _003Cpermissions_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003Cpermissions_003Ek__BackingField = webRtcPermission;
		}
	}

	public required string currentUserId
	{
		[CompilerGenerated]
		get
		{
			return _003CcurrentUserId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CcurrentUserId_003Ek__BackingField = text;
		}
	}

	public required string containerId
	{
		[CompilerGenerated]
		get
		{
			return _003CcontainerId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CcontainerId_003Ek__BackingField = text;
		}
	}

	public string? communityId
	{
		[CompilerGenerated]
		get
		{
			return _003CcommunityId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CcommunityId_003Ek__BackingField = text;
		}
	}

	public required string webApiBaseUrl
	{
		[CompilerGenerated]
		get
		{
			return _003CwebApiBaseUrl_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CwebApiBaseUrl_003Ek__BackingField = text;
		}
	}

	public required string videoDeviceId
	{
		[CompilerGenerated]
		get
		{
			return _003CvideoDeviceId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CvideoDeviceId_003Ek__BackingField = text;
		}
	}

	public required string audioInputDeviceId
	{
		[CompilerGenerated]
		get
		{
			return _003CaudioInputDeviceId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CaudioInputDeviceId_003Ek__BackingField = text;
		}
	}

	public required string audioOutputDeviceId
	{
		[CompilerGenerated]
		get
		{
			return _003CaudioOutputDeviceId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CaudioOutputDeviceId_003Ek__BackingField = text;
		}
	}

	public string? screenAudioDeviceId
	{
		[CompilerGenerated]
		get
		{
			return _003CscreenAudioDeviceId_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CscreenAudioDeviceId_003Ek__BackingField = text;
		}
	}

	public char? preferredRid
	{
		[CompilerGenerated]
		get
		{
			return _003CpreferredRid_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CpreferredRid_003Ek__BackingField = c;
		}
	}

	public required bool isPushToTalkMode
	{
		[CompilerGenerated]
		get
		{
			return _003CisPushToTalkMode_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CisPushToTalkMode_003Ek__BackingField = flag;
		}
	}

	public required bool debugMode
	{
		[CompilerGenerated]
		get
		{
			return _003CdebugMode_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CdebugMode_003Ek__BackingField = flag;
		}
	}

	public required string logging
	{
		[CompilerGenerated]
		get
		{
			return _003Clogging_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003Clogging_003Ek__BackingField = text;
		}
	}

	public string[]? initialTrackTypes
	{
		[CompilerGenerated]
		get
		{
			return _003CinitialTrackTypes_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CinitialTrackTypes_003Ek__BackingField = array;
		}
	}

	public double? inputVolume
	{
		[CompilerGenerated]
		get
		{
			return _003CinputVolume_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CinputVolume_003Ek__BackingField = num;
		}
	}

	public double? outputVolume
	{
		[CompilerGenerated]
		get
		{
			return _003CoutputVolume_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CoutputVolume_003Ek__BackingField = num;
		}
	}

	public UserTileVolume[]? tileVolumes
	{
		[CompilerGenerated]
		get
		{
			return _003CtileVolumes_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CtileVolumes_003Ek__BackingField = array;
		}
	}

	public double? defaultInputVolume
	{
		[CompilerGenerated]
		get
		{
			return _003CdefaultInputVolume_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CdefaultInputVolume_003Ek__BackingField = num;
		}
	}

	public double? defaultGlobalOutputVolume
	{
		[CompilerGenerated]
		get
		{
			return _003CdefaultGlobalOutputVolume_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CdefaultGlobalOutputVolume_003Ek__BackingField = num;
		}
	}

	public double? defaultPrimaryOutputVolume
	{
		[CompilerGenerated]
		get
		{
			return _003CdefaultPrimaryOutputVolume_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CdefaultPrimaryOutputVolume_003Ek__BackingField = num;
		}
	}

	public double? defaultScreenOutputVolume
	{
		[CompilerGenerated]
		get
		{
			return _003CdefaultScreenOutputVolume_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CdefaultScreenOutputVolume_003Ek__BackingField = num;
		}
	}

	public required string[] ringingUserIds
	{
		[CompilerGenerated]
		get
		{
			return _003CringingUserIds_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CringingUserIds_003Ek__BackingField = array;
		}
	}

	public required int ringTimeoutMs
	{
		[CompilerGenerated]
		get
		{
			return _003CringTimeoutMs_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CringTimeoutMs_003Ek__BackingField = num;
		}
	}

	public required string[] activeUserIds
	{
		[CompilerGenerated]
		get
		{
			return _003CactiveUserIds_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CactiveUserIds_003Ek__BackingField = array;
		}
	}

	public WebRtcCodec[]? preferredCodecs
	{
		[CompilerGenerated]
		get
		{
			return _003CpreferredCodecs_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CpreferredCodecs_003Ek__BackingField = array;
		}
	}

	public string? preferredScreenContentHint
	{
		[CompilerGenerated]
		get
		{
			return _003CpreferredScreenContentHint_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CpreferredScreenContentHint_003Ek__BackingField = text;
		}
	}

	public UserMediaStreamConstraints? userMediaConstraints
	{
		[CompilerGenerated]
		get
		{
			return _003CuserMediaConstraints_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CuserMediaConstraints_003Ek__BackingField = userMediaStreamConstraints;
		}
	}

	public DisplayMediaStreamConstraints? displayMediaConstraints
	{
		[CompilerGenerated]
		get
		{
			return _003CdisplayMediaConstraints_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CdisplayMediaConstraints_003Ek__BackingField = displayMediaStreamConstraints;
		}
	}

	public Dictionary<string, bool>? experimentalFlags
	{
		[CompilerGenerated]
		get
		{
			return _003CexperimentalFlags_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CexperimentalFlags_003Ek__BackingField = dictionary;
		}
	}

	public IJsObject ToJsObject(IFrame P_0)
	{
		string jsonString = JsonSerializer.Serialize(this, InitializeWebRtcPayloadJsonContext.Default.InitializeWebRtcPayload);
		return P_0.ParseJsonString<IJsObject>(jsonString);
	}
}
