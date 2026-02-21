// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.IWeakEventSubscriber<TEventArgs>
using Avalonia.Utilities;

public interface IWeakEventSubscriber<in TEventArgs>
{
	void OnEvent(object? P_0, WeakEvent P_1, TEventArgs P_2);
}

