using System;
using System.Net.Http;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace RootApp.AppHub.Client.Implementations;

public class AppConnectionFactory(ILoggerFactory)
{
	private readonly ILoggerFactory _loggerFactory = P_0;

	public AppConnection Create(HttpClient P_0, string P_1, Action P_2, Action P_3, CancellationToken P_4)
	{
		return new AppConnection(P_0, P_1, P_2, P_3, P_0, P_4);
	}
}
