Get-CimInstance Win32_Process -Filter "Name='chromium.exe'" | Where-Object {
    $_.CommandLine -match '--type=renderer'
} | ForEach-Object {
    Write-Output ("=== PID " + $_.ProcessId + " ===")
    Write-Output $_.CommandLine
    Write-Output ""
}
