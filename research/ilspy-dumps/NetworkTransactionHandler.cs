using System;
using System.Collections.Generic;
using System.Linq;
using DotNetBrowser.Handlers;
using DotNetBrowser.Net;
using DotNetBrowser.Net.Handlers;
using Microsoft.Extensions.Options;
using RootApp.Core;
using RootApp.WebApi.Client;

namespace RootApp.Browser.Handlers;

public sealed class NetworkTransactionHandler : IHandler<StartTransactionParameters, StartTransactionResponse>
{
	private readonly RootWebApiConfig _options = P_1.Value;

	private readonly IRootDefaultApiConnection _rootConnection;

	public NetworkTransactionHandler(IRootDefaultApiConnection P_0, IOptions<RootWebApiConfig> P_1)
	{
		_rootConnection = P_0;
		base._002Ector();
	}

	public StartTransactionResponse Handle(StartTransactionParameters P_0)
	{
		Uri uri = new Uri(P_0.UrlRequest.Url);
		Uri webApiUrl = _options.WebApiUrl;
		if (!string.Equals(uri.Authority, webApiUrl.Authority, StringComparison.OrdinalIgnoreCase))
		{
			return StartTransactionResponse.Continue();
		}
		List<HttpHeader> list = new List<HttpHeader>();
		list.AddRange(P_0.Headers.Cast<HttpHeader>());
		list.Add(new HttpHeader("Authorization", "Bearer " + _rootConnection.Connection.ClientToken.Token));
		return StartTransactionResponse.OverrideHeaders(new _003C_003Ez__ReadOnlyList<HttpHeader>(list));
	}
}
