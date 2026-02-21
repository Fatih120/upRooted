using System.Runtime.CompilerServices;
using System.Text.Json;
using DotNetBrowser.Frames;
using DotNetBrowser.Js;

namespace RootApp.Browser.RootApps.Models;

public class FileUploadResponse
{
	[CompilerGenerated]
	private string[] _003Ctokens_003Ek__BackingField;

	public required string[] tokens
	{
		[CompilerGenerated]
		get
		{
			return _003Ctokens_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003Ctokens_003Ek__BackingField = array;
		}
	}

	public IJsObject ToJsObject(IFrame P_0)
	{
		string jsonString = JsonSerializer.Serialize(this, FileUploadResponseJsonContext.Default.FileUploadResponse);
		return P_0.ParseJsonString<IJsObject>(jsonString);
	}
}
