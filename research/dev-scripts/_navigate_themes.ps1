Add-Type -AssemblyName System.Windows.Forms
Add-Type -Name WinNav -Namespace NavAPI -MemberDefinition @'
[DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr hWnd);
[DllImport("user32.dll")] public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
[DllImport("user32.dll")] public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
[DllImport("user32.dll")] public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int processId);
[DllImport("user32.dll")] public static extern bool IsWindowVisible(IntPtr hWnd);
[DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
[DllImport("user32.dll")] public static extern bool SetCursorPos(int X, int Y);
[DllImport("user32.dll")] public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);
public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
[StructLayout(LayoutKind.Sequential)]
public struct RECT { public int Left; public int Top; public int Right; public int Bottom; }
public const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
public const uint MOUSEEVENTF_LEFTUP = 0x0004;
public const uint MOUSEEVENTF_WHEEL = 0x0800;
'@

function Find-RootWindow {
    $rootProc = Get-Process Root -ErrorAction SilentlyContinue | Select-Object -First 1
    if (-not $rootProc) { return $null }
    $script:targetPid = $rootProc.Id
    $script:foundHwnd = [IntPtr]::Zero
    $script:bestArea = 0
    $cb = [NavAPI.WinNav+EnumWindowsProc]{
        param([IntPtr]$h, [IntPtr]$l)
        $wp = 0
        [NavAPI.WinNav]::GetWindowThreadProcessId($h, [ref]$wp) | Out-Null
        if ($wp -eq $script:targetPid -and [NavAPI.WinNav]::IsWindowVisible($h)) {
            $r = New-Object NavAPI.WinNav+RECT
            [NavAPI.WinNav]::GetWindowRect($h, [ref]$r) | Out-Null
            $a = ($r.Right - $r.Left) * ($r.Bottom - $r.Top)
            if ($a -gt $script:bestArea) { $script:bestArea = $a; $script:foundHwnd = $h }
        }
        return $true
    }
    [NavAPI.WinNav]::EnumWindows($cb, [IntPtr]::Zero) | Out-Null
    return $script:foundHwnd
}

function Click-At($x, $y) {
    [NavAPI.WinNav]::SetCursorPos($x, $y) | Out-Null
    Start-Sleep -Milliseconds 100
    [NavAPI.WinNav]::mouse_event([NavAPI.WinNav]::MOUSEEVENTF_LEFTDOWN, 0, 0, 0, [UIntPtr]::Zero)
    Start-Sleep -Milliseconds 50
    [NavAPI.WinNav]::mouse_event([NavAPI.WinNav]::MOUSEEVENTF_LEFTUP, 0, 0, 0, [UIntPtr]::Zero)
}

function Scroll-At($x, $y, $delta) {
    [NavAPI.WinNav]::SetCursorPos($x, $y) | Out-Null
    Start-Sleep -Milliseconds 100
    # delta: positive = scroll up, negative = scroll down. 120 = one notch
    [NavAPI.WinNav]::mouse_event([NavAPI.WinNav]::MOUSEEVENTF_WHEEL, 0, 0, [uint32]$delta, [UIntPtr]::Zero)
}

$hwnd = Find-RootWindow
if ($hwnd -eq [IntPtr]::Zero) { Write-Host "No Root window"; exit 1 }

# Focus
[NavAPI.WinNav]::ShowWindow($hwnd, 9) | Out-Null
Start-Sleep -Milliseconds 200
[NavAPI.WinNav]::SetForegroundWindow($hwnd) | Out-Null
Start-Sleep -Milliseconds 500

$rect = New-Object NavAPI.WinNav+RECT
[NavAPI.WinNav]::GetWindowRect($hwnd, [ref]$rect) | Out-Null
$w = $rect.Right - $rect.Left
$h = $rect.Bottom - $rect.Top
Write-Host "Root window: ${w}x${h} at ($($rect.Left),$($rect.Top))"

# Step 1: Click gear icon (bottom-left, 55px from left edge, 30px from bottom)
$gearX = $rect.Left + 55
$gearY = $rect.Bottom - 30
Write-Host "Step 1: Clicking gear at ($gearX, $gearY)"
Click-At $gearX $gearY
Start-Sleep -Seconds 2

# Step 2: Scroll the sidebar down to reveal our nav items
# Sidebar is at about x=250 (center of sidebar), y in the middle
$sidebarX = $rect.Left + 250
$sidebarY = $rect.Top + 300
Write-Host "Step 2: Scrolling sidebar at ($sidebarX, $sidebarY)"
# Scroll down by multiple notches (-120 per notch, negative = down)
Scroll-At $sidebarX $sidebarY (-360)
Start-Sleep -Milliseconds 500
Scroll-At $sidebarX $sidebarY (-360)
Start-Sleep -Seconds 1

# Step 3: Click "Themes" nav item (should be near bottom of sidebar after scroll)
# From recon data: nav items are at x=12-250 (left margin 12), each 40px tall
# After scrolling, "Themes" should be at approximately y = bottom_of_visible_sidebar - 40
# Let's try clicking at the vertical center of where Themes would be
# The nav items are: UPROOTED (header), Uprooted, Plugins, Themes
# Each item ~40px. Themes is 3rd item = at about 160px below UPROOTED header.
# With sidebar scrolled, it might be at different positions. Try a wider range.
$themesX = $rect.Left + 200  # center of sidebar area
$themesY = $rect.Bottom - 60  # near bottom after scroll
Write-Host "Step 3: Clicking Themes at ($themesX, $themesY)"
Click-At $themesX $themesY
Start-Sleep -Seconds 1

Write-Host "Navigation complete"
