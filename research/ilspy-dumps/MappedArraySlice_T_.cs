// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.MappedArraySlice<T>
using System.Runtime.CompilerServices;
using Avalonia.Utilities;

internal readonly struct MappedArraySlice<T>(in ArraySlice<T> P_0, in ArraySlice<int> P_1) where T : struct
{
	private readonly ArraySlice<T> _data = P_0;

	private readonly ArraySlice<int> _map = P_1;

	public int Length => _map.Length;

	public ref T this[int P_0]
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			return ref _data[_map[P_0]];
		}
	}
}

