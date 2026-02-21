// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.IRef<T>
using System;
using Avalonia.Utilities;

internal interface IRef<out T> : IDisposable where T : class
{
	T Item { get; }

	IRef<T> Clone();
}

