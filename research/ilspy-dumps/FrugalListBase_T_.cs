// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.FrugalListBase<T>
using Avalonia.Utilities;

internal abstract class FrugalListBase<T>
{
	protected int _count;

	public int Count => _count;

	public abstract int Capacity { get; }

	public abstract FrugalListStoreState Add(T P_0);

	public abstract void Insert(int P_0, T P_1);

	public abstract void SetAt(int P_0, T P_1);

	public abstract void RemoveAt(int P_0);

	public abstract T EntryAt(int P_0);

	public abstract void Promote(FrugalListBase<T> P_0);
}

