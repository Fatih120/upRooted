using System.Collections.Generic;

namespace RootApp.Information.ClientDesktop;

public static class RootClientDesktopBuildInformation
{
	public static RootBuildInformation SharedInformation => RootBuildInformationShared.Value;

	public static RootBuildInformation ProtosInformation => RootBuildInformationProtos.Value;

	public static RootBuildInformation ClientDesktopInformation => RootBuildInformationClientDesktop.Value;

	public static IReadOnlyCollection<RootBuildInformation> All { get; } = new global::_003C_003Ez__ReadOnlyArray<RootBuildInformation>(new RootBuildInformation[3] { SharedInformation, ProtosInformation, ClientDesktopInformation });
}
