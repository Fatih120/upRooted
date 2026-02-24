using System.Diagnostics;
using System.IO.Pipes;

namespace Uprooted;

/// <summary>
/// Dev-only plugin: spawns a system console window (PowerShell/terminal) that streams
/// live log output via a named pipe (Windows) or FIFO (Linux). The console window is
/// independent of Root — it stays open even after Root closes, and can be reopened
/// at any time.
///
/// Architecture:
///   1. Logger.OnLine fires for every log line (set up in Logger.cs)
///   2. LogConsole writes each line to a NamedPipeServerStream (Windows) or FIFO (Linux)
///   3. A spawned PowerShell/bash process connects to the pipe/FIFO and prints lines
///
/// Linux note: .NET NamedPipeServerStream uses Unix domain sockets with a validation
/// handshake that raw clients (cat/socat) can't perform. A FIFO (mkfifo) is used
/// instead, which `cat` reads natively.
///
/// Toggle: Enable/Disable from plugin settings (dev channel only, live toggle).
/// </summary>
internal static class LogConsole
{
    private const string PipeName = "uprooted-log-console";
    private const int BackfillLines = 200;

    private static bool _enabled;

    private static NamedPipeServerStream? _pipeServer;
    private static StreamWriter? _pipeWriter;
    private static Process? _consoleProcess;
    private static string? _fifoPath;
    private static readonly object _pipeLock = new();

    /// <summary>
    /// Launch a new console window. If one is already running, close it first.
    /// Called directly from the "Live Console" button on the About page.
    /// </summary>
    internal static void Enable()
    {
        // If already streaming, tear down and relaunch
        if (_enabled)
        {
            _enabled = false;
            Logger.OnLine = null;
            CleanupPipe();
        }

        _enabled = true;
        ThreadPool.QueueUserWorkItem(_ =>
        {
            try
            {
                StartConsole();
            }
            catch (Exception ex)
            {
                Logger.Log("LogConsole", $"Enable error: {ex.Message}");
                _enabled = false;
            }
        });
    }

    /// <summary>
    /// Stop live streaming immediately and detach from Logger.OnLine.
    /// Safe to call even if the console was never started.
    /// </summary>
    internal static void Disable()
    {
        _enabled = false;
        Logger.OnLine = null;
        CleanupPipe();
        Logger.Log("LogConsole", "Live streaming disabled");
    }

    private static void StartConsole()
    {
        // Clean up any leftover pipe/FIFO from a previous session
        CleanupPipe();

        if (OperatingSystem.IsWindows())
        {
            // Windows: NamedPipeServerStream + PowerShell with NamedPipeClientStream
            _pipeServer = new NamedPipeServerStream(
                PipeName,
                PipeDirection.Out,
                1,
                PipeTransmissionMode.Byte,
                PipeOptions.Asynchronous);

            Logger.Log("LogConsole", "Named pipe created, spawning console...");
            SpawnWindowsConsole();

            try
            {
                _pipeServer.WaitForConnection();
            }
            catch (Exception ex)
            {
                Logger.Log("LogConsole", $"Pipe connection failed: {ex.Message}");
                CleanupPipe();
                _enabled = false;
                return;
            }

            _pipeWriter = new StreamWriter(_pipeServer) { AutoFlush = true };
        }
        else
        {
            // Linux: FIFO instead of NamedPipeServerStream.
            // .NET named pipes on Linux use Unix domain sockets with a validation
            // handshake that raw clients (cat/socat) can't perform.
            _fifoPath = Path.Combine(Path.GetTempPath(), "uprooted-log-console.fifo");

            try { File.Delete(_fifoPath); } catch { }

            using var mkfifo = Process.Start("mkfifo", _fifoPath);
            mkfifo?.WaitForExit();

            if (!File.Exists(_fifoPath))
            {
                Logger.Log("LogConsole", "Failed to create FIFO");
                _enabled = false;
                return;
            }

            Logger.Log("LogConsole", $"FIFO created at {_fifoPath}, spawning console...");
            SpawnLinuxConsole();

            // Opening a FIFO for writing blocks until a reader (cat) opens it
            try
            {
                var fs = new FileStream(_fifoPath, FileMode.Open, FileAccess.Write, FileShare.Read);
                _pipeWriter = new StreamWriter(fs) { AutoFlush = true };
            }
            catch (Exception ex)
            {
                Logger.Log("LogConsole", $"FIFO open failed: {ex.Message}");
                CleanupPipe();
                _enabled = false;
                return;
            }
        }

        Logger.Log("LogConsole", "Console connected");

        // Write backfill (last N lines from log file)
        WriteBackfill();

        // Subscribe to live log lines
        Logger.OnLine = OnLogLine;

        Logger.Log("LogConsole", "Live streaming active");
    }

    private static void SpawnWindowsConsole()
    {
        // PowerShell script that:
        // 1. Sets window title
        // 2. Connects to named pipe
        // 3. Reads and prints lines with color coding
        // 4. Stays open after pipe disconnects (Root closed)
        var ps = @"
# Window sizing: small square, tiny font via conhost registry hack
$host.UI.RawUI.WindowTitle = 'Uprooted Log Console'
try {
    # Resize buffer and window to a compact square
    $rawUI = $host.UI.RawUI
    $buf = $rawUI.BufferSize
    $buf.Width = 90
    $buf.Height = 9999
    $rawUI.BufferSize = $buf
    $win = $rawUI.WindowSize
    $win.Width = 90
    $win.Height = 30
    $rawUI.WindowSize = $win
    # Position window bottom-right of primary screen
    $rawUI.WindowPosition = New-Object System.Management.Automation.Host.Coordinates(
        [System.Windows.Forms.Screen]::PrimaryScreen.WorkingArea.Width - 680,
        [System.Windows.Forms.Screen]::PrimaryScreen.WorkingArea.Height - 500)
} catch {}
Add-Type -AssemblyName System.Windows.Forms 2>$null
# Set small font via Win32 console API
Add-Type @'
using System; using System.Runtime.InteropServices;
public class ConFont {
    [DllImport(""kernel32.dll"", SetLastError=true)] static extern IntPtr GetStdHandle(int h);
    [DllImport(""kernel32.dll"", SetLastError=true)] static extern bool SetCurrentConsoleFontEx(IntPtr h, bool max, ref CONSOLE_FONT_INFO_EX f);
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
    public struct CONSOLE_FONT_INFO_EX {
        public uint cbSize; public uint nFont; public short W; public short H;
        public int Family; public int Weight; [MarshalAs(UnmanagedType.ByValTStr, SizeConst=32)] public string Name;
    }
    public static void Set(short h, string name) {
        var f = new CONSOLE_FONT_INFO_EX(); f.cbSize = (uint)Marshal.SizeOf(f);
        f.H = h; f.W = 0; f.Name = name; f.Weight = 400;
        SetCurrentConsoleFontEx(GetStdHandle(-11), false, ref f);
    }
}
'@ 2>$null
try { [ConFont]::Set(12, 'Consolas') } catch {}
try {
    $pipe = New-Object System.IO.Pipes.NamedPipeClientStream('.', '" + PipeName + @"', 'In')
    $pipe.Connect(5000)
    $reader = New-Object System.IO.StreamReader($pipe)
    while ($null -ne ($line = $reader.ReadLine())) {
        if ($line -match 'error[=_]|FAILED') {
            Write-Host $line -ForegroundColor Red
        } elseif ($line -match '\|.*dur_ms=') {
            Write-Host $line -ForegroundColor Cyan
        } elseif ($line -match '\[Startup') {
            Write-Host $line -ForegroundColor Green
        } else {
            Write-Host $line -ForegroundColor Gray
        }
    }
    $reader.Close()
    $pipe.Close()
} catch {
    Write-Host ""[pipe disconnected: $_]"" -ForegroundColor Yellow
}
Write-Host ''
Write-Host '--- Root process ended. Log stream closed. ---' -ForegroundColor Yellow
Write-Host 'Press any key to close...' -ForegroundColor DarkGray
$null = $host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')
";

        var bytes = System.Text.Encoding.Unicode.GetBytes(ps);
        var encoded = Convert.ToBase64String(bytes);

        _consoleProcess = Process.Start(new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = $"-NoProfile -EncodedCommand {encoded}",
            UseShellExecute = true,  // opens in a new window
            CreateNoWindow = false,
        });

        Logger.Log("LogConsole", $"PowerShell console spawned (PID: {_consoleProcess?.Id})");
    }

    private static void SpawnLinuxConsole()
    {
        // cat reads natively from FIFOs: no socat/python3 needed
        var scriptPath = Path.Combine(Path.GetTempPath(), "uprooted-log-console.sh");

        File.WriteAllText(scriptPath,
$@"#!/bin/bash
echo -e '\033]0;Uprooted Log Console\007'
cat '{_fifoPath}'
echo ''
echo '--- Root process ended. Log stream closed. ---'
echo 'Press Enter to close...'
read
");

        // Try common terminal emulators
        string[] terminals = { "x-terminal-emulator", "gnome-terminal", "konsole", "xfce4-terminal", "xterm" };
        foreach (var term in terminals)
        {
            try
            {
                _consoleProcess = Process.Start(new ProcessStartInfo
                {
                    FileName = term,
                    Arguments = term == "gnome-terminal"
                        ? $"-- bash {scriptPath}"
                        : $"-e bash {scriptPath}",
                    UseShellExecute = false,
                });
                if (_consoleProcess != null)
                {
                    Logger.Log("LogConsole", $"Terminal spawned: {term} (PID: {_consoleProcess.Id})");
                    return;
                }
            }
            catch { }
        }
        Logger.Log("LogConsole", "No terminal emulator found");
    }

    private static void OnLogLine(string line)
    {
        if (!_enabled) return;

        lock (_pipeLock)
        {
            try
            {
                _pipeWriter?.WriteLine(line);
            }
            catch
            {
                // Pipe/FIFO broken (console closed) — disable silently
                _enabled = false;
                Logger.OnLine = null;
                ThreadPool.QueueUserWorkItem(_ => CleanupPipe());
            }
        }
    }

    private static void WriteBackfill()
    {
        try
        {
            var logPath = Logger.GetLogPath();
            if (!File.Exists(logPath)) return;

            // Read with sharing so Logger can still write
            string[] allLines;
            using (var fs = new FileStream(logPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs))
            {
                allLines = sr.ReadToEnd().Split('\n');
            }

            int start = Math.Max(0, allLines.Length - BackfillLines);
            lock (_pipeLock)
            {
                for (int i = start; i < allLines.Length; i++)
                {
                    if (!string.IsNullOrWhiteSpace(allLines[i]))
                        _pipeWriter?.WriteLine(allLines[i].TrimEnd('\r'));
                }
                _pipeWriter?.WriteLine("--- live stream starts here ---");
            }
        }
        catch (Exception ex)
        {
            Logger.Log("LogConsole", $"Backfill error: {ex.Message}");
        }
    }

    private static void CleanupPipe()
    {
        lock (_pipeLock)
        {
            try { _pipeWriter?.Dispose(); } catch { }
            try { _pipeServer?.Dispose(); } catch { }
            _pipeWriter = null;
            _pipeServer = null;
        }
        if (_fifoPath != null)
        {
            try { File.Delete(_fifoPath); } catch { }
            _fifoPath = null;
        }
        // Don't kill the console process — let it stay open showing the last output
    }
}
