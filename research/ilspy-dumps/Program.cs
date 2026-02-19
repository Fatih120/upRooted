// Root, Version=0.9.92.0, Culture=neutral, PublicKeyToken=null
// Program
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using Avalonia;
using Avalonia.Labs.Notifications;
using Microsoft.Extensions.Hosting;
using ReactiveUI.Avalonia;
using RootApp.Client.Avalonia;
using RootApp.Client.Avalonia.Desktop.Windows;
using Velopack;

[CompilerGenerated]
internal class Program
{
	private static void _003CMain_003E_0024(string[] args)
	{
		Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
		Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
		Directory.SetCurrentDirectory(AppContext.BaseDirectory);
		ApplyUserProfileEnvironment(args);
		VelopackApp.Build().Run();
		RootLauncher.Run(args, (IHost host) => new AppCompositionRoot(host), delegate(AppCompositionRoot compositionRoot)
		{
			BuildAvaloniaApp(compositionRoot).StartWithClassicDesktopLifetime(args);
		});
		static void ApplyUserProfileEnvironment(string[] array)
		{
			string text = null;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Equals("--Application:UserProfile", StringComparison.OrdinalIgnoreCase) && i + 1 < array.Length)
				{
					text = array[i + 1];
					break;
				}
			}
			string text2 = text switch
			{
				"Experimental" => "Experimental", 
				"Development" => "DevCloud", 
				"Staging" => "Staging", 
				_ => null, 
			};
			if (text2 != null)
			{
				Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", text2);
			}
		}
		static AppBuilder BuildAvaloniaApp(AppCompositionRoot compositionRoot)
		{
			return AppBuilder.Configure(((IServiceProvider)compositionRoot).GetRequiredService<App>).UsePlatformDetect().UseReactiveUI()
				.With(new Win32PlatformOptions
				{
					OverlayPopups = false
				})
				.WithAppNotifications(new AppNotificationOptions
				{
					AppIcon = Path.Combine(AppContext.BaseDirectory, "Resources", "Assets", "rooticon.ico")
				});
		}
	}
}
