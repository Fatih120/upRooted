using System;
using System.Net.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http.Logging;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using RootApp.Core;

namespace RootApp.Utility.HttpClientPolicy;

public sealed class RootClientHttpHandlers : IRootHttpHandlers, IDisposable
{
	private readonly ILogger<RootClientHttpHandlers> _logger;

	private readonly RootHttpHandlersOptions _options;

	public HttpMessageHandler DefaultHandler { get; }

	public HttpMessageHandler GrpcHandler { get; }

	public HttpMessageHandler RootHttpHandler { get; }

	public RootClientHttpHandlers(ILogger<RootClientHttpHandlers> P_0, RootAppVersion P_1, IOptions<RootHttpHandlersOptions> P_2, IHostEnvironment P_3)
	{
		_logger = P_0;
		_options = P_2.Value;
		DefaultHandler = CreateDefaultHandler();
		GrpcHandler = CreateGrpcHandler(CreateBaseRootHandler());
		RootHttpHandler = CreateRootApiHandler(CreateBaseRootHandler());
	}

	public void Dispose()
	{
		DefaultHandler.Dispose();
		GrpcHandler.Dispose();
		RootHttpHandler.Dispose();
	}

	private HttpMessageHandler CreateDefaultHandler()
	{
		SocketsHttpHandler socketsHttpHandler = RootHttpHandlerUtility.Create();
		ResiliencePipeline<HttpResponseMessage> resiliencePipeline = CreateDefaultResiliencePipeline();
		return CreateHandler(socketsHttpHandler, resiliencePipeline);
	}

	private static HttpMessageHandler CreateGrpcHandler(HttpMessageHandler P_0)
	{
		return P_0;
	}

	private HttpMessageHandler CreateRootApiHandler(HttpMessageHandler P_0)
	{
		ResiliencePipeline<HttpResponseMessage> resiliencePipeline = CreateRootHttpResiliencePipeline();
		return CreateHandler(P_0, resiliencePipeline);
	}

	private ResiliencePipeline<HttpResponseMessage> CreateDefaultResiliencePipeline()
	{
		HttpStandardResilienceOptions httpStandardResilienceOptions = _options.Default ?? new HttpStandardResilienceOptions();
		return new ResiliencePipelineBuilder<HttpResponseMessage>().AddRateLimiter(httpStandardResilienceOptions.RateLimiter).AddTimeout(httpStandardResilienceOptions.TotalRequestTimeout).AddRetry(httpStandardResilienceOptions.Retry)
			.AddTimeout(httpStandardResilienceOptions.AttemptTimeout)
			.Build();
	}

	private ResiliencePipeline<HttpResponseMessage> CreateRootHttpResiliencePipeline()
	{
		HttpStandardResilienceOptions httpStandardResilienceOptions = _options.RootHttp ?? new HttpStandardResilienceOptions();
		return new ResiliencePipelineBuilder<HttpResponseMessage>().AddRateLimiter(httpStandardResilienceOptions.RateLimiter).AddTimeout(httpStandardResilienceOptions.TotalRequestTimeout).AddRetry(httpStandardResilienceOptions.Retry)
			.AddCircuitBreaker(httpStandardResilienceOptions.CircuitBreaker)
			.AddTimeout(httpStandardResilienceOptions.AttemptTimeout)
			.Build();
	}

	private static SocketsHttpHandler CreateBaseRootHandler()
	{
		return RootHttpHandlerUtility.Create();
	}

	private HttpMessageHandler CreateHandler(HttpMessageHandler P_0, ResiliencePipeline<HttpResponseMessage> P_1)
	{
		ResilienceHandler resilienceHandler = new ResilienceHandler(P_1);
		if (_logger.IsEnabled(LogLevel.Information))
		{
			LoggingHttpMessageHandler innerHandler = new LoggingHttpMessageHandler(_logger)
			{
				InnerHandler = P_0
			};
			resilienceHandler.InnerHandler = innerHandler;
			return new LoggingScopeHttpMessageHandler(_logger)
			{
				InnerHandler = resilienceHandler
			};
		}
		resilienceHandler.InnerHandler = P_0;
		return resilienceHandler;
	}
}
