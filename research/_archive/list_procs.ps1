Get-CimInstance Win32_Process -Filter "Name='chromium.exe'" | ForEach-Object {
    $type = "browser"
    if ($_.CommandLine -match '--type=(\S+)') { $type = $Matches[1] }
    Write-Output ("PID=" + $_.ProcessId + " TYPE=" + $type)
}
