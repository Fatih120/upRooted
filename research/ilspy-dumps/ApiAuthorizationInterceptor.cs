using System;
using System.Threading.Tasks;
using Grpc.Core;

namespace RootApp.WebApi.Client.Shared.Implementations;

public sealed class ApiAuthorizationInterceptor : IApiAuthorization
{
	public CallCredentials Credentials { get; }

	public string Authorization { get; set; }

	public ApiAuthorizationInterceptor(string authorization)
	{
		Authorization = authorization ?? throw new ArgumentNullException("authorization");
		Credentials = CallCredentials.FromInterceptor(InterceptAsync);
	}

	private Task InterceptAsync(AuthInterceptorContext context, Metadata metadata)
	{
		metadata.Add("Authorization", Authorization);
		return Task.CompletedTask;
	}
}
