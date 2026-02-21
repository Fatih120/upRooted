// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Rendering.RendererDiagnostics
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Avalonia.Rendering;

public class RendererDiagnostics : INotifyPropertyChanged
{
	private RendererDebugOverlays _debugOverlays;

	private LayoutPassTiming _lastLayoutPassTiming;

	private PropertyChangedEventArgs? _debugOverlaysChangedEventArgs;

	private PropertyChangedEventArgs? _lastLayoutPassTimingChangedEventArgs;

	[CompilerGenerated]
	private PropertyChangedEventHandler? m_PropertyChanged;

	public RendererDebugOverlays DebugOverlays
	{
		get
		{
			return _debugOverlays;
		}
		set
		{
			if (_debugOverlays != rendererDebugOverlays)
			{
				_debugOverlays = rendererDebugOverlays;
				OnPropertyChanged(_debugOverlaysChangedEventArgs ?? (_debugOverlaysChangedEventArgs = new PropertyChangedEventArgs("DebugOverlays")));
			}
		}
	}

	internal LayoutPassTiming LastLayoutPassTiming
	{
		get
		{
			return _lastLayoutPassTiming;
		}
		set
		{
			if (!_lastLayoutPassTiming.Equals(layoutPassTiming))
			{
				_lastLayoutPassTiming = layoutPassTiming;
				OnPropertyChanged(_lastLayoutPassTimingChangedEventArgs ?? (_lastLayoutPassTimingChangedEventArgs = new PropertyChangedEventArgs("LastLayoutPassTiming")));
			}
		}
	}

	public event PropertyChangedEventHandler? PropertyChanged
	{
		[CompilerGenerated]
		add
		{
			PropertyChangedEventHandler propertyChangedEventHandler = this.m_PropertyChanged;
			PropertyChangedEventHandler propertyChangedEventHandler2;
			do
			{
				propertyChangedEventHandler2 = propertyChangedEventHandler;
				PropertyChangedEventHandler propertyChangedEventHandler3 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, b);
				propertyChangedEventHandler = Interlocked.CompareExchange(ref this.m_PropertyChanged, propertyChangedEventHandler3, propertyChangedEventHandler2);
			}
			while ((object)propertyChangedEventHandler != propertyChangedEventHandler2);
		}
		[CompilerGenerated]
		remove
		{
			PropertyChangedEventHandler propertyChangedEventHandler = this.m_PropertyChanged;
			PropertyChangedEventHandler propertyChangedEventHandler2;
			do
			{
				propertyChangedEventHandler2 = propertyChangedEventHandler;
				PropertyChangedEventHandler propertyChangedEventHandler3 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value2);
				propertyChangedEventHandler = Interlocked.CompareExchange(ref this.m_PropertyChanged, propertyChangedEventHandler3, propertyChangedEventHandler2);
			}
			while ((object)propertyChangedEventHandler != propertyChangedEventHandler2);
		}
	}

	protected virtual void OnPropertyChanged(PropertyChangedEventArgs P_0)
	{
		this.PropertyChanged?.Invoke(this, P_0);
	}
}

