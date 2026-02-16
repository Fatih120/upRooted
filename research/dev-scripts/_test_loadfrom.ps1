# Quick test: can .NET 10 load UprootedHook.dll via Assembly.LoadFrom?
$code = @'
using System;
using System.Reflection;

class Test {
    static void Main() {
        string path = @"C:\Users\bash\claude\Root.Dev\uprooted\hook\bin\Release\net10.0\UprootedHook.dll";
        Console.WriteLine($"Loading: {path}");
        Console.WriteLine($"Exists: {System.IO.File.Exists(path)}");
        try {
            var asm = Assembly.LoadFrom(path);
            Console.WriteLine($"Assembly: {asm.FullName}");
            Console.WriteLine($"Types: {asm.GetTypes().Length}");
            foreach (var t in asm.GetTypes()) {
                Console.WriteLine($"  Type: {t.FullName}");
            }
            var entry = asm.CreateInstance("UprootedHook.Entry");
            Console.WriteLine($"CreateInstance: {entry}");
            Console.WriteLine($"Result type: {entry?.GetType().FullName ?? "null"}");
        } catch (Exception ex) {
            Console.WriteLine($"ERROR: {ex}");
        }
    }
}
'@

# Write to temp file and run with dotnet-script or csc
$tempCs = "$env:TEMP\test_loadfrom.cs"
$tempExe = "$env:TEMP\test_loadfrom.exe"
$code | Set-Content $tempCs

# Use dotnet run with inline project
$tempDir = "$env:TEMP\test_loadfrom_proj"
if (Test-Path $tempDir) { Remove-Item $tempDir -Recurse -Force }
New-Item -ItemType Directory $tempDir | Out-Null

@'
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
  </PropertyGroup>
</Project>
'@ | Set-Content "$tempDir\test.csproj"

$code | Set-Content "$tempDir\Program.cs"

Push-Location $tempDir
try {
    dotnet run 2>&1
} finally {
    Pop-Location
}
