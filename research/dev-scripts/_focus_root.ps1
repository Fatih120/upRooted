Add-Type -Name Win -Namespace WinAPI -MemberDefinition @'
[DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr hWnd);
[DllImport("user32.dll")] public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
'@

$root = Get-Process -Name Root -ErrorAction SilentlyContinue | Select-Object -First 1
if ($root -and $root.MainWindowHandle -ne [IntPtr]::Zero) {
    [WinAPI.Win]::ShowWindow($root.MainWindowHandle, 9) | Out-Null  # SW_RESTORE
    Start-Sleep -Milliseconds 500
    [WinAPI.Win]::SetForegroundWindow($root.MainWindowHandle) | Out-Null
    Start-Sleep -Seconds 2
    Write-Host "Root window focused"
} else {
    Write-Host "Root not running or no main window"
}
