// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.ArraySlice<T>
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Avalonia.Utilities;

internal readonly struct ArraySlice<T> : IReadOnlyList<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>
{
	private readonly T[] _data;

	public static ArraySlice<T> Empty => new ArraySlice<T>(Array.Empty<T>());

	public bool IsEmpty => Length == 0;

	public int Start { get; }

	public int Length { get; }

	public Span<T> Span => new Span<T>(_data, Start, Length);

	public ref T this[int P_0]
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			int num = P_0 + Start;
			return ref _data[num];
		}
	}

	T IReadOnlyList<T>.this[int P_0] => this[P_0];

	int IReadOnlyCollection<T>.Count => Length;

	public ArraySlice(T[] P_0)
		: this(P_0, 0, P_0.Length)
	{
	}

	public ArraySlice(T[] P_0, int P_1, int P_2)
	{
		_data = P_0;
		Start = P_1;
		Length = P_2;
	}

	public static implicit operator ArraySlice<T>(T[] P_0)
	{
		return new ArraySlice<T>(P_0, 0, P_0.Length);
	}

	public void Fill(T P_0)
	{
		Span.Fill(P_0);
	}

	public ArraySlice<T> Slice(int P_0, int P_1)
	{
		return new ArraySlice<T>(_data, P_0, P_1);
	}

	public ImmutableReadOnlyListStructEnumerator<T> GetEnumerator()
	{
		return new ImmutableReadOnlyListStructEnumerator<T>(this);
	}

	IEnumerator<T> IEnumerable<T>.GetEnumerator()
	{
		return GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}

