// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Media.FontSourceIdentifier
using System;
using System.Runtime.CompilerServices;

internal readonly record struct FontSourceIdentifier
{
	public string Name
	{
		[CompilerGenerated]
		get
		{
			return _003CName_003Ek__BackingField;
		}
		[CompilerGenerated]
		init
		{
			_003CName_003Ek__BackingField = text;
		}
	}

	public Uri? Source
	{
		[CompilerGenerated]
		get
		{
			return _003CSource_003Ek__BackingField;
		}
		[CompilerGenerated]
		init
		{
			_003CSource_003Ek__BackingField = uri;
		}
	}

	public FontSourceIdentifier(string P_0, Uri? P_1)
	{
		Name = P_0;
		Source = P_1;
	}
}

