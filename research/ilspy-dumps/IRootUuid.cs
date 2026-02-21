namespace RootApp.Core;

public interface IRootUuid
{
	ulong High64 { get; }

	ulong Low64 { get; }
}
public interface IRootUuid<T> : IRootUuid where T : IRootUuid<T>
{
}
