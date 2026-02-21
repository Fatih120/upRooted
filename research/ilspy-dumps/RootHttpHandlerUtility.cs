using System;
using System.Net.Http;
using System.Net.Security;
using System.Security.Authentication;

namespace RootApp.Utility.HttpClientPolicy;

public static class RootHttpHandlerUtility
{
	private static readonly TimeSpan _pooledConnectionLifetime = TimeSpan.FromMinutes(5L);

	private static readonly TimeSpan _keepAlivePingDelay = TimeSpan.FromSeconds(60L);

	private static readonly TimeSpan _keepAlivePingTimeout = TimeSpan.FromSeconds(30L);

	public static SocketsHttpHandler Create()
	{
		return new SocketsHttpHandler
		{
			KeepAlivePingDelay = _keepAlivePingDelay,
			KeepAlivePingTimeout = _keepAlivePingTimeout,
			SslOptions = new SslClientAuthenticationOptions
			{
				EnabledSslProtocols = (SslProtocols.Tls12 | SslProtocols.Tls13)
			},
			PooledConnectionLifetime = _pooledConnectionLifetime
		};
	}
}
