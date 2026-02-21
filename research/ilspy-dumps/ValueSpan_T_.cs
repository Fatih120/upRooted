// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.ValueSpan<T>
public readonly record struct ValueSpan<T>
{
	public int Start { get; }

	public int Length { get; }

	public T Value { get; }

	public ValueSpan(int P_0, int P_1, T P_2)
	{
		Start = P_0;
		Length = P_1;
		Value = P_2;
	}
}

