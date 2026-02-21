// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.NonPumpingLockHelper
using System;
using Avalonia;
using Avalonia.Threading;
using Avalonia.Utilities;

internal class NonPumpingLockHelper
{
	public interface IHelperImpl
	{
		int Wait(nint[] P_0, bool P_1, int P_2);
	}

	public static IDisposable? Use()
	{
		IHelperImpl service = AvaloniaLocator.Current.GetService<IHelperImpl>();
		if (service == null)
		{
			return null;
		}
		return NonPumpingSyncContext.Use(service);
	}
}

