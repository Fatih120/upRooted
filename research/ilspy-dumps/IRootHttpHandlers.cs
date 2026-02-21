using System;
using System.Net.Http;

namespace RootApp.Utility.HttpClientPolicy;

public interface IRootHttpHandlers : IDisposable
{
	HttpMessageHandler DefaultHandler { get; }

	HttpMessageHandler GrpcHandler { get; }

	HttpMessageHandler RootHttpHandler { get; }
}
