Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing
Add-Type -Name Win -Namespace WinAPI2 -MemberDefinition @'
[DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr hWnd);
[DllImport("user32.dll")] public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
[DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
[DllImport("user32.dll")] public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, uint nFlags);
[DllImport("user32.dll")] public static extern bool IsWindowVisible(IntPtr hWnd);
[DllImport("user32.dll")] public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
[DllImport("user32.dll")] public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int processId);
[DllImport("user32.dll", CharSet = CharSet.Auto)] public static extern int GetClassName(IntPtr hWnd, System.Text.StringBuilder lpClassName, int nMaxCount);
public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
[StructLayout(LayoutKind.Sequential)]
public struct RECT { public int Left; public int Top; public int Right; public int Bottom; }
'@

$rootProc = Get-Process -Name Root -ErrorAction SilentlyContinue | Select-Object -First 1
if (-not $rootProc) {
    Write-Host "Root not found"
    exit 1
}

# Find Root's main window via EnumWindows (handles empty MainWindowTitle)
$targetPid = $rootProc.Id
$foundHwnd = [IntPtr]::Zero
$bestArea = 0

$callback = [WinAPI2.Win+EnumWindowsProc]{
    param([IntPtr]$hWnd, [IntPtr]$lParam)
    $wpid = 0
    [WinAPI2.Win]::GetWindowThreadProcessId($hWnd, [ref]$wpid) | Out-Null
    if ($wpid -eq $script:targetPid -and [WinAPI2.Win]::IsWindowVisible($hWnd)) {
        $r = New-Object WinAPI2.Win+RECT
        [WinAPI2.Win]::GetWindowRect($hWnd, [ref]$r) | Out-Null
        $area = ($r.Right - $r.Left) * ($r.Bottom - $r.Top)
        if ($area -gt $script:bestArea) {
            $script:bestArea = $area
            $script:foundHwnd = $hWnd
        }
    }
    return $true
}
[WinAPI2.Win]::EnumWindows($callback, [IntPtr]::Zero) | Out-Null

if ($foundHwnd -eq [IntPtr]::Zero) {
    # Fallback: try MainWindowHandle
    if ($rootProc.MainWindowHandle -ne [IntPtr]::Zero) {
        $foundHwnd = $rootProc.MainWindowHandle
    } else {
        Write-Host "No visible Root window found"
        exit 1
    }
}

$hwnd = $foundHwnd
Write-Host "Root PID=$targetPid Handle=$hwnd"

# Focus it
[WinAPI2.Win]::ShowWindow($hwnd, 9) | Out-Null
Start-Sleep -Milliseconds 300
[WinAPI2.Win]::SetForegroundWindow($hwnd) | Out-Null
Start-Sleep -Seconds 1

# Get window dimensions
$rect = New-Object WinAPI2.Win+RECT
[WinAPI2.Win]::GetWindowRect($hwnd, [ref]$rect) | Out-Null
$w = $rect.Right - $rect.Left
$h = $rect.Bottom - $rect.Top
Write-Host "Window size: ${w}x${h} at ($($rect.Left),$($rect.Top))"

if ($w -le 0 -or $h -le 0) {
    Write-Host "Invalid size"
    exit 1
}

# Use PrintWindow to capture content regardless of z-order
$bitmap = New-Object System.Drawing.Bitmap($w, $h)
$graphics = [System.Drawing.Graphics]::FromImage($bitmap)
$hdc = $graphics.GetHdc()
# PW_RENDERFULLCONTENT = 2
[WinAPI2.Win]::PrintWindow($hwnd, $hdc, 2) | Out-Null
$graphics.ReleaseHdc($hdc)
$graphics.Dispose()

$path = 'C:\Users\bash\claude\Root.Dev\screenshot_themes.png'
$bitmap.Save($path, [System.Drawing.Imaging.ImageFormat]::Png)
$bitmap.Dispose()
Write-Host "Screenshot saved: ${w}x${h}"
