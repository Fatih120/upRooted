// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.HashCode
using System;
using System.Runtime.CompilerServices;

internal struct HashCode
{
	private static readonly uint s_seed = GenerateGlobalSeed();

	private uint _v1;

	private uint _v2;

	private uint _v3;

	private uint _v4;

	private uint _queue1;

	private uint _queue2;

	private uint _queue3;

	private uint _length;

	private static uint GenerateGlobalSeed()
	{
		Random random = new Random();
		return (uint)random.Next();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void Initialize(out uint P_0, out uint P_1, out uint P_2, out uint P_3)
	{
		P_0 = (uint)((int)s_seed + -1640531535 + -2048144777);
		P_1 = s_seed + 2246822519u;
		P_2 = s_seed;
		P_3 = s_seed - 2654435761u;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static uint Round(uint P_0, uint P_1)
	{
		return RotateLeft(P_0 + (uint)((int)P_1 * -2048144777), 13) * 2654435761u;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static uint QueueRound(uint P_0, uint P_1)
	{
		return RotateLeft(P_0 + (uint)((int)P_1 * -1028477379), 17) * 668265263;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static uint MixState(uint P_0, uint P_1, uint P_2, uint P_3)
	{
		return RotateLeft(P_0, 1) + RotateLeft(P_1, 7) + RotateLeft(P_2, 12) + RotateLeft(P_3, 18);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static uint RotateLeft(uint P_0, int P_1)
	{
		return (P_0 << P_1) | (P_0 >> 32 - P_1);
	}

	private static uint MixEmptyState()
	{
		return s_seed + 374761393;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static uint MixFinal(uint P_0)
	{
		P_0 ^= P_0 >> 15;
		P_0 *= 2246822519u;
		P_0 ^= P_0 >> 13;
		P_0 *= 3266489917u;
		P_0 ^= P_0 >> 16;
		return P_0;
	}

	public unsafe void Add(void* P_0)
	{
		Add((P_0 != null) ? ((IntPtr)P_0).GetHashCode() : 0);
	}

	public void Add<T>(T P_0)
	{
		Add(P_0?.GetHashCode() ?? 0);
	}

	private void Add(int P_0)
	{
		uint num = _length++;
		switch (num % 4)
		{
		case 0u:
			_queue1 = (uint)P_0;
			return;
		case 1u:
			_queue2 = (uint)P_0;
			return;
		case 2u:
			_queue3 = (uint)P_0;
			return;
		}
		if (num == 3)
		{
			Initialize(out _v1, out _v2, out _v3, out _v4);
		}
		_v1 = Round(_v1, _queue1);
		_v2 = Round(_v2, _queue2);
		_v3 = Round(_v3, _queue3);
		_v4 = Round(_v4, (uint)P_0);
	}

	public int ToHashCode()
	{
		uint length = _length;
		uint num = length % 4;
		uint num2 = ((length < 4) ? MixEmptyState() : MixState(_v1, _v2, _v3, _v4));
		num2 += length * 4;
		if (num != 0)
		{
			num2 = QueueRound(num2, _queue1);
			if (num > 1)
			{
				num2 = QueueRound(num2, _queue2);
				if (num > 2)
				{
					num2 = QueueRound(num2, _queue3);
				}
			}
		}
		return (int)MixFinal(num2);
	}
}

