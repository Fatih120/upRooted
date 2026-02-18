# MessageLogger diagnostic analyzer
# Reads uprooted-hook.log and summarizes MsgLogger/DIAG entries
# Usage: powershell -File analyze-msglogger.ps1 [-lines 5000]

param(
    [int]$lines = 5000
)

$logPath = "$env:LOCALAPPDATA\Root Communications\Root\profile\default\uprooted-hook.log"

if (-not (Test-Path $logPath)) {
    Write-Host "ERROR: Log file not found at $logPath" -ForegroundColor Red
    exit 1
}

Write-Host "=== MessageLogger Diagnostic Summary ===" -ForegroundColor Cyan
Write-Host "Log: $logPath" -ForegroundColor DarkGray
Write-Host "Scanning last $lines lines..." -ForegroundColor DarkGray
Write-Host ""

$allLines = Get-Content $logPath -ErrorAction Stop
$startIdx = [Math]::Max(0, $allLines.Count - $lines)
$recentLines = $allLines[$startIdx..($allLines.Count - 1)]

# Filter to MsgLogger + DIAG lines
$mlLines = $recentLines | Where-Object { $_ -match 'MsgLogger|DIAG' }

Write-Host "Total log lines scanned: $($recentLines.Count)" -ForegroundColor DarkGray
Write-Host "MsgLogger/DIAG lines: $($mlLines.Count)" -ForegroundColor DarkGray
Write-Host ""

# --- 1. Remove events (immediate values logged at event time, for comparison only) ---
$removeEvents = $mlLines | Where-Object { $_ -match '\[remove-event\]' }
$immTrue = ($removeEvents | Where-Object { $_ -match 'immediate=True' }).Count
$immFalse = ($removeEvents | Where-Object { $_ -match 'immediate=False' }).Count
$immNull = ($removeEvents | Where-Object { $_ -match 'immediate=null' }).Count
$hasBridge = ($removeEvents | Where-Object { $_ -match 'bridgeTarget=True' }).Count
$noBridge = ($removeEvents | Where-Object { $_ -match 'bridgeTarget=False' }).Count

Write-Host "--- Remove Events ---" -ForegroundColor Yellow
Write-Host "  Total [remove-event]: $($removeEvents.Count)"
Write-Host "  Immediate values (diagnostic only, NOT used for decisions):"
Write-Host "    immediate=True:  $immTrue" -ForegroundColor $(if ($immTrue -gt 0) { "Green" } else { "DarkGray" })
Write-Host "    immediate=False: $immFalse" -ForegroundColor $(if ($immFalse -gt 0) { "White" } else { "DarkGray" })
Write-Host "    immediate=null:  $immNull" -ForegroundColor $(if ($immNull -gt 0) { "Red" } else { "DarkGray" })
Write-Host "  Bridge target captured: yes=$hasBridge no=$noBridge" -ForegroundColor $(if ($noBridge -gt 0) { "Red" } else { "Green" })
Write-Host ""

# --- 2. MSG DEL (confirmed deletions) ---
$msgDels = $mlLines | Where-Object { $_ -match 'MSG DEL' }
Write-Host "--- Confirmed Deletions ---" -ForegroundColor Yellow
Write-Host "  MSG DEL entries: $($msgDels.Count)"
foreach ($d in $msgDels) {
    Write-Host "    $d" -ForegroundColor DarkCyan
}
Write-Host ""

# --- 3. Flush summaries ---
$flushLines = $mlLines | Where-Object { $_ -match '\[flush\]' }
Write-Host "--- Flush Summaries ---" -ForegroundColor Yellow
Write-Host "  [flush] entries: $($flushLines.Count)"
foreach ($f in ($flushLines | Select-Object -Last 5)) {
    Write-Host "    $f" -ForegroundColor DarkCyan
}
Write-Host ""

# --- 4. DIAG-FLUSH (deferred reads + retry tracking) ---
$diagFlush = $mlLines | Where-Object { $_ -match '\[DIAG-FLUSH\]' }
$deferredTrue = ($diagFlush | Where-Object { $_ -match 'deferred HasBeenDeleted=True' }).Count
$deferredFalse = ($diagFlush | Where-Object { $_ -match 'deferred HasBeenDeleted=False' }).Count
$deferredNull = ($diagFlush | Where-Object { $_ -match 'deferred HasBeenDeleted=null' }).Count
$deferredToNext = ($diagFlush | Where-Object { $_ -match 'deferring to next tick' }).Count
$discardedBuffer = ($diagFlush | Where-Object { $_ -match 'discarding \(buffer management\)' }).Count
$aboutToMark = ($diagFlush | Where-Object { $_ -match 'About to mark MSG DEL' }).Count

Write-Host "--- DIAG-FLUSH (deferred HasBeenDeleted reads) ---" -ForegroundColor Yellow
Write-Host "  Total [DIAG-FLUSH]: $($diagFlush.Count)"
Write-Host "  Deferred read results:"
Write-Host "    True (deletion):  $deferredTrue" -ForegroundColor $(if ($deferredTrue -gt 0) { "Green" } else { "DarkGray" })
Write-Host "    False:            $deferredFalse" -ForegroundColor $(if ($deferredFalse -gt 0) { "White" } else { "DarkGray" })
Write-Host "    null (disposed):  $deferredNull" -ForegroundColor $(if ($deferredNull -gt 0) { "Red" } else { "DarkGray" })
Write-Host "  Retry tracking:"
Write-Host "    Deferred to next tick: $deferredToNext" -ForegroundColor $(if ($deferredToNext -gt 0) { "Yellow" } else { "DarkGray" })
Write-Host "    Discarded (buffer mgmt): $discardedBuffer"
Write-Host "    About to mark MSG DEL: $aboutToMark" -ForegroundColor $(if ($aboutToMark -gt 0) { "Green" } else { "DarkGray" })
Write-Host ""

# Show last 10 DIAG-FLUSH lines for context
Write-Host "  Last 10 DIAG-FLUSH entries:" -ForegroundColor DarkGray
foreach ($df in ($diagFlush | Select-Object -Last 10)) {
    $color = "DarkCyan"
    if ($df -match 'HasBeenDeleted=True') { $color = "Green" }
    elseif ($df -match 'deferring to next tick') { $color = "Yellow" }
    elseif ($df -match 'About to mark MSG DEL') { $color = "Green" }
    Write-Host "    $df" -ForegroundColor $color
}
Write-Host ""

# --- 6. DIAG-INJ (injection runs) ---
$injStarts = $mlLines | Where-Object { $_ -match '\[DIAG-INJ\] === InjectDeletedMessageCards start ===' }
$injDone = $mlLines | Where-Object { $_ -match '\[DIAG-INJ\] === InjectDeletedMessageCards done:' }
$findPanelNull = ($mlLines | Where-Object { $_ -match '\[DIAG-INJ\] FindMessagePanel: null' }).Count
$visibleZero = ($mlLines | Where-Object { $_ -match '\[DIAG-INJ\] visibleMessages: 0' }).Count
$gridNull = ($mlLines | Where-Object { $_ -match '\[DIAG-INJ\].*grid=null' }).Count

Write-Host "--- DIAG-INJ (injection runs) ---" -ForegroundColor Yellow
Write-Host "  Injection starts: $($injStarts.Count)"
Write-Host "  Injection completions: $($injDone.Count)"
Write-Host "  FindMessagePanel null: $findPanelNull" -ForegroundColor $(if ($findPanelNull -gt 0) { "Red" } else { "Green" })
Write-Host "  visibleMessages=0: $visibleZero" -ForegroundColor $(if ($visibleZero -gt 0) { "Red" } else { "Green" })
Write-Host "  Grid not found (grid=null): $gridNull" -ForegroundColor $(if ($gridNull -gt 0) { "Red" } else { "Green" })
Write-Host ""

# Show injection results breakdown
Write-Host "  Last 5 injection completions:" -ForegroundColor DarkGray
foreach ($done in ($injDone | Select-Object -Last 5)) {
    $color = "DarkCyan"
    if ($done -match 'injected, 0 skipped') { $color = "Green" }
    elseif ($done -match '0 injected') { $color = "DarkYellow" }
    Write-Host "    $done" -ForegroundColor $color
}
Write-Host ""

# --- 7. ClearInjectedCards ---
$clearCalls = $mlLines | Where-Object { $_ -match '\[DIAG\] ClearInjectedCards:' }
Write-Host "--- ClearInjectedCards ---" -ForegroundColor Yellow
Write-Host "  Total calls: $($clearCalls.Count)"
$reasons = @{}
foreach ($c in $clearCalls) {
    if ($c -match 'ClearInjectedCards: ([^,]+)') {
        $reason = $Matches[1].Trim()
        if ($reasons.ContainsKey($reason)) { $reasons[$reason]++ } else { $reasons[$reason] = 1 }
    }
}
foreach ($r in $reasons.GetEnumerator()) {
    Write-Host "    $($r.Key): $($r.Value)" -ForegroundColor DarkCyan
}
Write-Host ""

# --- 8. HasBeenDeleted resolution ---
$resolutionLines = $mlLines | Where-Object { $_ -match 'HasBeenDeleted=' }
Write-Host "--- HasBeenDeleted Resolution ---" -ForegroundColor Yellow
Write-Host "  Lines mentioning HasBeenDeleted: $($resolutionLines.Count)"
foreach ($rl in ($resolutionLines | Select-Object -First 5)) {
    Write-Host "    $rl" -ForegroundColor DarkCyan
}
Write-Host ""

# --- 9. DIAG-TREE dumps ---
$treeDumps = $mlLines | Where-Object { $_ -match '\[DIAG-TREE\]' }
Write-Host "--- DIAG-TREE (visual tree dumps) ---" -ForegroundColor Yellow
Write-Host "  Tree dump lines: $($treeDumps.Count)"
foreach ($t in $treeDumps) {
    Write-Host "    $t" -ForegroundColor DarkCyan
}
Write-Host ""

# --- 10. Errors ---
$errors = $mlLines | Where-Object { $_ -match '(?i)error|exception|fail' }
Write-Host "--- Errors ---" -ForegroundColor Yellow
Write-Host "  Error lines: $($errors.Count)" -ForegroundColor $(if ($errors.Count -gt 0) { "Red" } else { "Green" })
foreach ($e in ($errors | Select-Object -Last 10)) {
    Write-Host "    $e" -ForegroundColor Red
}
Write-Host ""

# --- 11. Heartbeats (last 3) ---
$heartbeats = $mlLines | Where-Object { $_ -match '\[heartbeat\]' }
Write-Host "--- Heartbeats (last 3) ---" -ForegroundColor Yellow
foreach ($h in ($heartbeats | Select-Object -Last 3)) {
    Write-Host "    $h" -ForegroundColor DarkCyan
}
Write-Host ""

Write-Host "=== End Summary ===" -ForegroundColor Cyan
Write-Host "Paste this output for analysis." -ForegroundColor DarkGray
