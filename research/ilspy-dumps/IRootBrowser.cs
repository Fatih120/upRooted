using System;
using DotNetBrowser.Browser;
using RootApp.Core.Identifiers;

namespace RootApp.Browser;

public interface IRootBrowser : IDisposable
{
	RootGuid Id { get; }

	IBrowser DotNetBrowser { get; }
}
