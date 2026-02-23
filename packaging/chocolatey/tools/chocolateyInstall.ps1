$ErrorActionPreference = 'Stop'

$version = $env:chocolateyPackageVersion
$url = "https://github.com/The-Uprooted-Project/uprooted/releases/download/v$version/Uprooted-$version-windows-amd64.exe"

$packageArgs = @{
    packageName   = $env:ChocolateyPackageName
    url           = $url
    checksum      = '%%CHECKSUM%%'
    checksumType  = 'sha256'
    fileFullPath  = Join-Path $env:ChocolateyPackageFolder 'tools\uprooted.exe'
}

Get-ChocolateyWebFile @packageArgs
Install-BinFile -Name 'uprooted' -Path (Join-Path $env:ChocolateyPackageFolder 'tools\uprooted.exe')
