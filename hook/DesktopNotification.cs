using System.Diagnostics;

namespace Uprooted;

/// <summary>
/// Fire-and-forget OS-level desktop notifications.
/// Windows: PowerShell WinRT toast via -EncodedCommand (safe from injection).
/// Linux: notify-send with direct arguments (UseShellExecute=false, no shell).
/// </summary>
internal static class DesktopNotification
{
    internal static void Show(string title, string body)
    {
        try
        {
            if (OperatingSystem.IsWindows())
                ShowWindows(title, body);
            else if (OperatingSystem.IsLinux())
                ShowLinux(title, body);
        }
        catch (Exception ex)
        {
            Logger.Log("DesktopNotification", $"Failed to show notification: {ex.Message}");
        }
    }

    private static void ShowWindows(string title, string body)
    {
        // Use -EncodedCommand with Base64-encoded UTF-16LE to avoid all escaping issues.
        // PowerShell variables $t use safe CreateTextNode() — user strings are never
        // interpolated into the PowerShell source, only passed as .NET method arguments.
        var ps = "[Windows.UI.Notifications.ToastNotificationManager, Windows.UI.Notifications, ContentType = WindowsRuntime] | Out-Null; " +
                 "$t = [Windows.UI.Notifications.ToastNotificationManager]::GetTemplateContent([Windows.UI.Notifications.ToastTemplateType]::ToastText02); " +
                 $"$t.GetElementsByTagName('text')[0].AppendChild($t.CreateTextNode('{EscapeSingleQuote(title)}')) | Out-Null; " +
                 $"$t.GetElementsByTagName('text')[1].AppendChild($t.CreateTextNode('{EscapeSingleQuote(body)}')) | Out-Null; " +
                 "[Windows.UI.Notifications.ToastNotificationManager]::CreateToastNotifier('Root').Show([Windows.UI.Notifications.ToastNotification]::new($t))";

        var bytes = System.Text.Encoding.Unicode.GetBytes(ps);
        var encoded = Convert.ToBase64String(bytes);

        using var p = Process.Start(new ProcessStartInfo
        {
            FileName = "powershell",
            Arguments = $"-NoProfile -NonInteractive -EncodedCommand {encoded}",
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        });
    }

    private static void ShowLinux(string title, string body)
    {
        // UseShellExecute=false: args go directly to notify-send (no shell interpolation).
        // Use ArgumentList-style quoting via ProcessStartInfo to avoid escaping entirely.
        using var p = Process.Start(new ProcessStartInfo
        {
            FileName = "notify-send",
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        }.Also(psi =>
        {
            psi.ArgumentList.Add("--");
            psi.ArgumentList.Add(title);
            psi.ArgumentList.Add(body);
        }));
    }

    /// <summary>
    /// Escape single quotes for PowerShell single-quoted string context.
    /// Inside -EncodedCommand, the PS source uses single-quoted strings for
    /// CreateTextNode arguments, so only ' needs doubling.
    /// </summary>
    private static string EscapeSingleQuote(string s) => s.Replace("'", "''");
}

internal static class ProcessStartInfoExtensions
{
    internal static ProcessStartInfo Also(this ProcessStartInfo psi, Action<ProcessStartInfo> configure)
    {
        configure(psi);
        return psi;
    }
}
