using System;
using System.Reflection;
using Microsoft.Extensions.Hosting;
using RootApp.Core;
using RootApp.Information;
using RootApp.Utility;

namespace RootApp.Hosting.Extensions;

public static class RootAppVersionFactory
{
	public static RootAppVersion CreateRootAppVersion(IHostEnvironment P_0, string P_1, RootBuildInformation P_2)
	{
		AssemblyName name = (Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()).GetName();
		string text = (string.IsNullOrWhiteSpace(P_0.ApplicationName) ? (name.Name ?? "Unknown") : P_0.ApplicationName);
		if (!Version.TryParse(P_2.GitHubRefName, out Version version))
		{
			version = name.Version;
		}
		string text2 = ((P_2.GitHubBuild && (object)version != null) ? version.ToString() : P_2.Describe);
		if ((object)version == null)
		{
			version = new Version();
		}
		string text3 = text + "/" + text2;
		string text4 = text;
		if (text4.StartsWith("RootApp.", StringComparison.Ordinal))
		{
			string text5 = text4;
			int length = "RootApp.".Length;
			text4 = text5.Substring(length, text5.Length - length);
		}
		if (string.IsNullOrWhiteSpace(text4))
		{
			throw new InvalidOperationException("Invalid application name " + text);
		}
		return new RootAppVersion(text3, text, version, RootCapitalization.ToFirstUpper(P_1), P_0.EnvironmentName, P_2.Branch, P_2.CommitHash, text4);
	}
}
