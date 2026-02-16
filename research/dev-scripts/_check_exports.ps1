$bytes = [System.IO.File]::ReadAllBytes('C:\Users\bash\claude\Root.Dev\uprooted\tools\uprooted_profiler.dll')
$text = [System.Text.Encoding]::ASCII.GetString($bytes)
Write-Host "File size: $($bytes.Length) bytes"
Write-Host "DllGetClassObject at: $($text.IndexOf('DllGetClassObject'))"
Write-Host "DllCanUnloadNow at: $($text.IndexOf('DllCanUnloadNow'))"
Write-Host "UprootedDll at: $($text.IndexOf('UprootedDll'))"
Write-Host "PROFILER_LOG at: $($text.IndexOf('PROFILER_LOG'))"
Write-Host "ClassFactory at: $($text.IndexOf('ClassFactory'))"
