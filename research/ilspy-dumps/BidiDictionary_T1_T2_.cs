// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.BidiDictionary<T1,T2>
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Avalonia.Media.TextFormatting;

internal sealed class BidiDictionary<T1, T2> where T1 : notnull where T2 : notnull
{
	private Dictionary<T1, T2> _forward = new Dictionary<T1, T2>();

	private Dictionary<T2, T1> _reverse = new Dictionary<T2, T1>();

	public void ClearThenResetIfTooLarge()
	{
		FormattingBufferHelper.ClearThenResetIfTooLarge(ref _forward);
		FormattingBufferHelper.ClearThenResetIfTooLarge(ref _reverse);
	}

	public void Add(T1 P_0, T2 P_1)
	{
		_forward.Add(P_0, P_1);
		_reverse.Add(P_1, P_0);
	}

	public bool TryGetValue(T1 P_0, [MaybeNullWhen(false)] out T2 P_1)
	{
		return _forward.TryGetValue(P_0, out P_1);
	}
}

