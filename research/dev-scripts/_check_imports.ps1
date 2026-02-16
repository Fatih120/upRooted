# Check if uiohook.dll is in Root.exe's PE import table
$path = "$env:LOCALAPPDATA\Root\current\Root.exe"
$stream = [System.IO.File]::OpenRead($path)
$reader = New-Object System.IO.BinaryReader($stream)

# Read DOS header
$stream.Position = 0x3C  # e_lfanew offset
$peOffset = $reader.ReadInt32()
Write-Host "PE header at: 0x$($peOffset.ToString('X'))"

# Read PE signature
$stream.Position = $peOffset
$peSignature = $reader.ReadUInt32()
Write-Host "PE signature: 0x$($peSignature.ToString('X'))"

# COFF header
$machine = $reader.ReadUInt16()
$numSections = $reader.ReadUInt16()
$stream.Position += 12  # skip timestamp, symbol table, num symbols
$optHeaderSize = $reader.ReadUInt16()
$characteristics = $reader.ReadUInt16()
Write-Host "Sections: $numSections, OptHeader size: $optHeaderSize"

# Optional header
$optHeaderStart = $stream.Position
$magic = $reader.ReadUInt16()
Write-Host "Optional header magic: 0x$($magic.ToString('X'))"

if ($magic -eq 0x20B) {
    # PE32+ (64-bit)
    $stream.Position = $optHeaderStart + 112  # Import table RVA in data directory
    $importRVA = $reader.ReadUInt32()
    $importSize = $reader.ReadUInt32()
    Write-Host "Import Directory RVA: 0x$($importRVA.ToString('X')), Size: $importSize"

    # Read section headers to find which section contains the import table
    $stream.Position = $optHeaderStart + $optHeaderSize
    $importFileOffset = 0
    for ($i = 0; $i -lt $numSections; $i++) {
        $nameBytes = $reader.ReadBytes(8)
        $name = [System.Text.Encoding]::ASCII.GetString($nameBytes).TrimEnd([char]0)
        $virtualSize = $reader.ReadUInt32()
        $virtualAddress = $reader.ReadUInt32()
        $rawDataSize = $reader.ReadUInt32()
        $rawDataPointer = $reader.ReadUInt32()
        $stream.Position += 16  # skip relocations, line numbers, etc.

        if ($importRVA -ge $virtualAddress -and $importRVA -lt ($virtualAddress + $virtualSize)) {
            $importFileOffset = $rawDataPointer + ($importRVA - $virtualAddress)
            Write-Host "Import table in section '$name' at file offset 0x$($importFileOffset.ToString('X'))"
        }
    }

    if ($importFileOffset -gt 0) {
        # Read import directory entries (20 bytes each)
        $stream.Position = $importFileOffset
        $dlls = @()
        while ($true) {
            $originalFirstThunk = $reader.ReadUInt32()
            $timeDateStamp = $reader.ReadUInt32()
            $forwarderChain = $reader.ReadUInt32()
            $nameRVA = $reader.ReadUInt32()
            $firstThunk = $reader.ReadUInt32()

            if ($nameRVA -eq 0) { break }

            # Find the section containing this name RVA
            $stream2Pos = $stream.Position
            $stream.Position = $optHeaderStart + $optHeaderSize
            for ($j = 0; $j -lt $numSections; $j++) {
                $nBytes = $reader.ReadBytes(8)
                $vSize = $reader.ReadUInt32()
                $vAddr = $reader.ReadUInt32()
                $rdSize = $reader.ReadUInt32()
                $rdPtr = $reader.ReadUInt32()
                $stream.Position += 16

                if ($nameRVA -ge $vAddr -and $nameRVA -lt ($vAddr + $vSize)) {
                    $nameFileOffset = $rdPtr + ($nameRVA - $vAddr)
                    $stream.Position = $nameFileOffset
                    $nameBytes2 = $reader.ReadBytes(256)
                    $nullIdx = [Array]::IndexOf($nameBytes2, [byte]0)
                    if ($nullIdx -gt 0) {
                        $dllName = [System.Text.Encoding]::ASCII.GetString($nameBytes2, 0, $nullIdx)
                        $dlls += $dllName
                    }
                    break
                }
            }
            $stream.Position = $stream2Pos
        }

        Write-Host "`nImported DLLs ($($dlls.Count)):"
        foreach ($dll in $dlls) {
            $highlight = ""
            if ($dll -like "*uiohook*") { $highlight = " <<<< FOUND!" }
            if ($dll -like "*hook*") { $highlight = " <<<< HOOK DLL" }
            Write-Host "  - $dll$highlight"
        }

        $hasUiohook = $dlls | Where-Object { $_ -like "*uiohook*" }
        if ($hasUiohook) {
            Write-Host "`n*** uiohook.dll IS in the import table - DllMain runs BEFORE CLR init ***"
        } else {
            Write-Host "`n*** uiohook.dll is NOT in the import table - loaded dynamically AFTER CLR init ***"
        }
    }
}

$reader.Close()
$stream.Close()
