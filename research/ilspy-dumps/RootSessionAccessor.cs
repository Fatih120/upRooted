// RootApp.Client.CoreDomain, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.CoreDomain.RootSessionAccessor
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using RootApp.Client.CoreDomain;

public class RootSessionAccessor : ObservableObject, IRootSessionAccessor, INotifyPropertyChanged
{
	[CompilerGenerated]
	private RootSession? _003CSession_003Ek__BackingField;

	[ObservableProperty]
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public RootSession? Session
	{
		get
		{
			return _003CSession_003Ek__BackingField;
		}
		set
		{
			if (!EqualityComparer<RootSession>.Default.Equals(_003CSession_003Ek__BackingField, rootSession))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Session);
				_003CSession_003Ek__BackingField = rootSession;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Session);
			}
		}
	}

	public void SetSession(RootSession P_0)
	{
		Session = P_0;
	}

	public void ClearSession()
	{
		Session = null;
	}
}

