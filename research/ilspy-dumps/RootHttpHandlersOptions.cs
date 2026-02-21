using System.Runtime.CompilerServices;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Options;

namespace RootApp.Utility.HttpClientPolicy;

public class RootHttpHandlersOptions
{
	[CompilerGenerated]
	private HttpStandardResilienceOptions? _003CDefault_003Ek__BackingField;

	[CompilerGenerated]
	private HttpStandardResilienceOptions? _003CRootGrpc_003Ek__BackingField;

	[CompilerGenerated]
	private HttpStandardResilienceOptions? _003CRootHttp_003Ek__BackingField;

	[ValidateObjectMembers]
	public HttpStandardResilienceOptions? Default
	{
		[CompilerGenerated]
		get
		{
			return _003CDefault_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CDefault_003Ek__BackingField = httpStandardResilienceOptions;
		}
	}

	[ValidateObjectMembers]
	public HttpStandardResilienceOptions? RootGrpc
	{
		[CompilerGenerated]
		get
		{
			return _003CRootGrpc_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CRootGrpc_003Ek__BackingField = httpStandardResilienceOptions;
		}
	}

	[ValidateObjectMembers]
	public HttpStandardResilienceOptions? RootHttp
	{
		[CompilerGenerated]
		get
		{
			return _003CRootHttp_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			_003CRootHttp_003Ek__BackingField = httpStandardResilienceOptions;
		}
	}
}
