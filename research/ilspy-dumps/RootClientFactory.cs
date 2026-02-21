using System;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Options;
using RootApp.Core;

namespace RootApp.Utility.HttpClientPolicy;

public sealed class RootClientFactory
{
	private readonly IRootHttpHandlers _httpHandlers;

	private readonly RootWebApiConfig _options;

	private readonly RootAppVersion _rootAppVersion;

	public RootClientFactory(RootAppVersion P_0, IOptions<RootWebApiConfig> P_1, IRootHttpHandlers P_2)
	{
		_httpHandlers = P_2;
		_options = P_1.Value;
		_rootAppVersion = P_0;
		base._002Ector();
	}

	public HttpClient CreateDefaultHttpClient()
	{
		HttpMessageHandler defaultHandler = _httpHandlers.DefaultHandler;
		return CreateHttpClient(defaultHandler, null);
	}

	public HttpClient CreateWebApiHttpClient<T>()
	{
		return CreateHttpClient(_httpHandlers.RootHttpHandler, _options.WebApiUrl);
	}

	private HttpClient CreateHttpClient(HttpMessageHandler P_0, Uri? P_1)
	{
		HttpClient httpClient = new HttpClient(P_0, false)
		{
			DefaultRequestVersion = HttpVersion.Version30,
			DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower
		};
		httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(_rootAppVersion.UserAgent);
		if ((object)P_1 != null)
		{
			httpClient.BaseAddress = P_1;
		}
		return httpClient;
	}
}
