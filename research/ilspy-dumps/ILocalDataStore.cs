// RootApp.Client.Domain, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// RootApp.Client.Domain.Helpers.Store.ILocalDataStore
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization.Metadata;

public interface ILocalDataStore
{
	void Set<T>(ReadOnlySpan<string> P_0, T P_1, JsonTypeInfo<T> P_2);

	bool TryGet<T>(ReadOnlySpan<string> P_0, [NotNullWhen(true)] out T? P_1, JsonTypeInfo<T> P_2);
}

