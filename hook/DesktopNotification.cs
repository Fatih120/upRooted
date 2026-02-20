using System.Diagnostics;

namespace Uprooted;

/// <summary>
/// Fire-and-forget OS-level desktop notifications.
/// Windows: PowerShell WinRT toast. Linux: notify-send.
/// </summary>
internal static class DesktopNotification
{
    internal static void Show(string title, string body)
    {
        try
        {
            if (OperatingSystem.IsWindows())
            {
                // PowerShell one-liner: WinRT toast notification via AppId
                var ps = $"[Windows.UI.Notifications.ToastNotificationManager, Windows.UI.Notifications, ContentType = WindowsRuntime] | Out-Null; " +
                         $"$t = [Windows.UI.Notifications.ToastNotificationManager]::GetTemplateContent([Windows.UI.Notifications.ToastTemplateType]::ToastText02); " +
                         $"$t.GetElementsByTagName('text')[0].AppendChild($t.CreateTextNode('{EscapePowerShell(title)}')) | Out-Null; " +
                         $"$t.GetElementsByTagName('text')[1].AppendChild($t.CreateTextNode('{EscapePowerShell(body)}')) | Out-Null; " +
                         $"[Windows.UI.Notifications.ToastNotificationManager]::CreateToastNotifier('Root').Show([Windows.UI.Notifications.ToastNotification]::new($t))";

                Process.Start(new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = $"-NoProfile -NonInteractive -Command \"{ps}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                });
            }
            else if (OperatingSystem.IsLinux())
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "notify-send",
                    Arguments = $"\"{EscapeBash(title)}\" \"{EscapeBash(body)}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                });
            }
        }
        catch (Exception ex)
        {
            Logger.Log("DesktopNotification", $"Failed to show notification: {ex.Message}");
        }
    }

    private static string EscapePowerShell(string s) => s.Replace("'", "''");
    private static string EscapeBash(string s) => s.Replace("\"", "\\\"").Replace("$", "\\$");
}
