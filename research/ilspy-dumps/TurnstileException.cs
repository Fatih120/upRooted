using System;

namespace RootApp.Browser.Turnstile;

public class TurnstileException : Exception
{
	public TurnstileException(string P_0)
		: base(P_0)
	{
	}
}
