# Real-time log watcher for uprooted-hook.log with color-coded output
# Usage: powershell -File watch-log.ps1
#   -f             : follow mode (tail -f equivalent, default)
#   -last <N>      : show last N lines first (default 50)
#   -filter <text> : only show lines matching text
#   -no-diag       : hide [Diag] and [Style] lines (less noise)
#   -errors        : only show errors, warnings, crashes

param(
    [switch]$f = $true,
    [int]$last = 50,
    [string]$filter = "",
    [switch]$noDiag,
    [switch]$errors
)

$logPath = "$env:LOCALAPPDATA\Root Communications\Root\profile\default\uprooted-hook.log"

Write-Host "=== Uprooted Hook Log Watcher ===" -ForegroundColor Cyan
Write-Host "Log: $logPath" -ForegroundColor DarkGray
Write-Host "Press Ctrl+C to stop" -ForegroundColor DarkGray
Write-Host ""

if (-not (Test-Path $logPath)) {
    Write-Host "Log file not found. Launch Root with test-hook.ps1 first." -ForegroundColor Red
    Write-Host "Waiting for log file to appear..." -ForegroundColor Yellow
    while (-not (Test-Path $logPath)) {
        Start-Sleep -Milliseconds 500
    }
    Write-Host "Log file appeared!" -ForegroundColor Green
}

function Format-LogLine {
    param([string]$line)

    if ($filter -and $line -notmatch [regex]::Escape($filter)) { return }
    if ($errors -and $line -notmatch '(?i)error|fail|crash|warn|MISSING|abort|fatal') { return }
    if ($noDiag -and $line -match '\[(Diag|Style|Tree)\]') { return }

    # Color coding by category
    $color = "White"
    if ($line -match '\[Entry\]') { $color = "Green" }
    elseif ($line -match '\[Startup\]') { $color = "Cyan" }
    elseif ($line -match '\[Reflection\]') { $color = "DarkCyan" }
    elseif ($line -match '\[Injector\]') { $color = "Green" }
    elseif ($line -match '\[TreeWalker\]') { $color = "Yellow" }
    elseif ($line -match '\[Diag\]') { $color = "Magenta" }
    elseif ($line -match '\[Style\]') { $color = "DarkMagenta" }
    elseif ($line -match '\[Recon\]') { $color = "Blue" }

    # Highlight errors/warnings (fallback = non-fatal, stays yellow)
    if ($line -match '(?i)error|fail|crash|fatal') {
        if ($line -match '(?i)fallback') { $color = "DarkYellow" }
        else { $color = "Red" }
    }
    elseif ($line -match '(?i)warn|MISSING') { $color = "DarkYellow" }

    # Highlight key events
    if ($line -match 'Hook Ready|Injection complete|Back button pressed') {
        $color = "White"
        Write-Host ""
        Write-Host ("=" * 60) -ForegroundColor $color
    }

    Write-Host $line -ForegroundColor $color

    if ($line -match 'Hook Ready|Injection complete|Back button pressed') {
        Write-Host ("=" * 60) -ForegroundColor $color
        Write-Host ""
    }
}

# Show last N lines
$existingLines = Get-Content $logPath -ErrorAction SilentlyContinue
if ($existingLines) {
    $startIdx = [Math]::Max(0, $existingLines.Count - $last)
    if ($startIdx -gt 0) {
        Write-Host "... ($startIdx earlier lines omitted) ..." -ForegroundColor DarkGray
    }
    for ($i = $startIdx; $i -lt $existingLines.Count; $i++) {
        Format-LogLine $existingLines[$i]
    }
    Write-Host ""
    Write-Host "--- Live tail starting ---" -ForegroundColor DarkGray
}

# Follow mode: poll for new lines
$lastLineCount = $existingLines.Count
while ($true) {
    Start-Sleep -Milliseconds 200
    try {
        $currentLines = Get-Content $logPath -ErrorAction SilentlyContinue
        if ($currentLines -and $currentLines.Count -gt $lastLineCount) {
            for ($i = $lastLineCount; $i -lt $currentLines.Count; $i++) {
                Format-LogLine $currentLines[$i]
            }
            $lastLineCount = $currentLines.Count
        }
    } catch { }
}
