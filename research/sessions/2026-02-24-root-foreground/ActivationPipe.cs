using System;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using RootApp.Client.Avalonia.UI.Main;

namespace RootApp.Client.Avalonia.Helpers.Activation;

public static class ActivationPipe
{
	public static string PipeName(string P_0)
	{
		return P_0 + "-ActivationPipe";
	}

	public static void SignalFirstInstance(string P_0, string? P_1 = null, int P_2 = 500)
	{
		try
		{
			using NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(".", P_0, PipeDirection.Out);
			namedPipeClientStream.Connect(P_2);
			string s = (string.IsNullOrWhiteSpace(P_1) ? "activate" : ("launch:" + P_1));
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			namedPipeClientStream.Write(bytes, 0, bytes.Length);
			namedPipeClientStream.Flush();
		}
		catch
		{
		}
	}

	public static void StartServerLoop(string P_0, Action<string?>? P_1, CancellationToken P_2)
	{
		Task.Run(async delegate
		{
			Window w = default(Window);
			while (!P_2.IsCancellationRequested)
			{
				try
				{
					using NamedPipeServerStream server = new NamedPipeServerStream(P_0, PipeDirection.In, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
					await server.WaitForConnectionAsync(P_2).ConfigureAwait(continueOnCapturedContext: false);
					byte[] buf = new byte[256];
					int bytesRead = await server.ReadAsync(buf, 0, buf.Length, P_2).ConfigureAwait(continueOnCapturedContext: false);
					string launchId = null;
					if (bytesRead > 0)
					{
						string message = Encoding.UTF8.GetString(buf, 0, bytesRead).Trim();
						if (message.StartsWith("launch:", StringComparison.OrdinalIgnoreCase))
						{
							string text = message;
							int length = "launch:".Length;
							launchId = text.Substring(length, text.Length - length);
							if (string.IsNullOrWhiteSpace(launchId))
							{
								launchId = null;
							}
						}
					}
					IApplicationLifetime applicationLifetime = Application.Current?.ApplicationLifetime;
					int num;
					if (applicationLifetime is IClassicDesktopStyleApplicationLifetime d)
					{
						w = d.MainWindow;
						num = ((w != null) ? 1 : 0);
					}
					else
					{
						num = 0;
					}
					if (num != 0)
					{
						await Dispatcher.UIThread.InvokeAsync(delegate
						{
							RestoreWindow(w);
							P_1?.Invoke(launchId);
						});
					}
				}
				catch (OperationCanceledException)
				{
				}
				catch
				{
					await Task.Delay(50, CancellationToken.None);
				}
			}
		}, P_2);
	}

	public static string SanitizeForOsIdentifier(string P_0)
	{
		string text = P_0.Trim();
		char[] array = new char[9] { '\\', '/', ':', '*', '?', '"', '<', '>', '|' };
		foreach (char c in array)
		{
			text = text.Replace(c.ToString(), string.Empty);
		}
		return string.Join("-", text.Split(new char[4] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
	}

	private static void RestoreWindow(Window P_0)
	{
		if (P_0 is MainWindow mainWindow)
		{
			mainWindow.RestoreFromTray();
		}
		else if (!P_0.IsVisible)
		{
			P_0.Show();
		}
		else if (P_0.WindowState == WindowState.Minimized)
		{
			P_0.WindowState = WindowState.Normal;
		}
		P_0.Topmost = true;
		P_0.Activate();
		P_0.Topmost = false;
	}
}
