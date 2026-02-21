using Microsoft.Extensions.DependencyInjection;

namespace RootApp.Information.ClientDesktop.Hosting;

public static class RootBuildInformationRegistration
{
	public static IServiceCollection AddClientRootInformation(this IServiceCollection services)
	{
		services.AddSingleton(RootClientDesktopBuildInformation.ClientDesktopInformation).AddSingleton(RootClientDesktopBuildInformation.All);
		return services;
	}
}
