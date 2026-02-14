// Chromium wrapper: forwards all args to chromium_real.exe
// and adds --remote-debugging-port=9222 for CDP access.
using System;
using System.Diagnostics;
using System.IO;

class ChromiumWrapper
{
    static int Main(string[] args)
    {
        string dir = AppDomain.CurrentDomain.BaseDirectory;
        string real = Path.Combine(dir, "chromium_real.exe");

        if (!File.Exists(real))
        {
            Console.Error.WriteLine("[Uprooted] chromium_real.exe not found at: " + real);
            return 1;
        }

        // Rebuild the original command line (preserving quoting)
        // Environment.CommandLine includes the exe path, so strip it
        string cmdLine = Environment.CommandLine;
        // Remove the exe path from the beginning
        if (cmdLine.StartsWith("\""))
        {
            int endQuote = cmdLine.IndexOf('"', 1);
            if (endQuote >= 0)
                cmdLine = cmdLine.Substring(endQuote + 1).TrimStart();
        }
        else
        {
            int space = cmdLine.IndexOf(' ');
            if (space >= 0)
                cmdLine = cmdLine.Substring(space + 1).TrimStart();
            else
                cmdLine = "";
        }

        // Add remote debugging port
        string extraArgs = "--remote-debugging-port=9222";
        string fullArgs = cmdLine + " " + extraArgs;

        var psi = new ProcessStartInfo
        {
            FileName = real,
            Arguments = fullArgs,
            UseShellExecute = false,
            WorkingDirectory = dir
        };

        try
        {
            var proc = Process.Start(psi);
            if (proc != null)
            {
                proc.WaitForExit();
                return proc.ExitCode;
            }
            return 1;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("[Uprooted] Failed to start chromium_real.exe: " + ex.Message);
            return 1;
        }
    }
}
