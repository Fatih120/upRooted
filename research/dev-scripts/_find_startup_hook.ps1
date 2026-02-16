# Search Root.exe for StartupHookProvider related strings and IL patterns
$path = "$env:LOCALAPPDATA\Root\current\Root.exe"
$bytes = [System.IO.File]::ReadAllBytes($path)
$text = [System.Text.Encoding]::UTF8.GetString($bytes)

Write-Host "Binary size: $($bytes.Length) bytes"

# Search for StartupHookProvider (metadata string)
$idx = 0
$results = @()
while (($idx = $text.IndexOf("StartupHookProvider", $idx)) -ge 0) {
    $results += $idx
    $idx++
}
Write-Host "`n'StartupHookProvider' found at $($results.Count) locations:"
foreach ($r in $results) {
    $context = $text.Substring([Math]::Max(0, $r - 30), [Math]::Min(80, $text.Length - [Math]::Max(0, $r - 30)))
    $context = $context -replace '[^\x20-\x7E]', '.'
    Write-Host "  offset 0x$($r.ToString('X8')): ...$context..."
}

# Search for ProcessStartupHooks
$idx = 0
$results2 = @()
while (($idx = $text.IndexOf("ProcessStartupHooks", $idx)) -ge 0) {
    $results2 += $idx
    $idx++
}
Write-Host "`n'ProcessStartupHooks' found at $($results2.Count) locations:"
foreach ($r in $results2) {
    $context = $text.Substring([Math]::Max(0, $r - 30), [Math]::Min(80, $text.Length - [Math]::Max(0, $r - 30)))
    $context = $context -replace '[^\x20-\x7E]', '.'
    Write-Host "  offset 0x$($r.ToString('X8')): ...$context..."
}

# Search for get_IsSupported near StartupHookProvider
$idx = 0
$results3 = @()
while (($idx = $text.IndexOf("get_IsSupported", $idx)) -ge 0) {
    $results3 += $idx
    $idx++
}
Write-Host "`n'get_IsSupported' found at $($results3.Count) locations:"
foreach ($r in $results3) {
    $context = $text.Substring([Math]::Max(0, $r - 40), [Math]::Min(100, $text.Length - [Math]::Max(0, $r - 40)))
    $context = $context -replace '[^\x20-\x7E]', '.'
    Write-Host "  offset 0x$($r.ToString('X8')): ...$context..."
}

# Search for CallProcessStartupHooks (the native caller)
$idx = 0
$results4 = @()
while (($idx = $text.IndexOf("CallProcessStartupHooks", $idx)) -ge 0) {
    $results4 += $idx
    $idx++
}
Write-Host "`n'CallProcessStartupHooks' found at $($results4.Count) locations:"
foreach ($r in $results4) {
    $context = $text.Substring([Math]::Max(0, $r - 30), [Math]::Min(80, $text.Length - [Math]::Max(0, $r - 30)))
    $context = $context -replace '[^\x20-\x7E]', '.'
    Write-Host "  offset 0x$($r.ToString('X8')): ...$context..."
}

# Search for DOTNET_STARTUP_HOOKS env var name in binary
$idx = 0
$results5 = @()
while (($idx = $text.IndexOf("STARTUP_HOOKS", $idx)) -ge 0) {
    $results5 += $idx
    $idx++
}
Write-Host "`n'STARTUP_HOOKS' found at $($results5.Count) locations:"
foreach ($r in $results5) {
    $context = $text.Substring([Math]::Max(0, $r - 30), [Math]::Min(80, $text.Length - [Math]::Max(0, $r - 30)))
    $context = $context -replace '[^\x20-\x7E]', '.'
    Write-Host "  offset 0x$($r.ToString('X8')): ...$context..."
}
