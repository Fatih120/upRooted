using System;

namespace RootApp.Core.Identifiers;

public interface IRootGuid
{
	(ulong High64, ulong Low64) GetValue();
}
public interface IRootGuid<T> : IRootGuid where T : struct, IRootGuid<T>
{
	static abstract T Create((ulong High64, ulong Low64) P_0);

	static abstract T Create(Guid P_0);
}
