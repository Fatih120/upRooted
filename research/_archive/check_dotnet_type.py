"""Check if Root.exe is NativeAOT or standard .NET by examining PE imports."""
import struct

EXE = r"C:\Users\bash\AppData\Local\Root\current\Root.exe"

with open(EXE, "rb") as f:
    # Read DOS header
    f.seek(0x3C)
    pe_offset = struct.unpack("<I", f.read(4))[0]

    # Read PE signature
    f.seek(pe_offset)
    sig = f.read(4)
    assert sig == b"PE\x00\x00", f"Not a PE file: {sig}"

    # Read COFF header
    machine = struct.unpack("<H", f.read(2))[0]
    num_sections = struct.unpack("<H", f.read(2))[0]
    f.read(12)  # skip timestamp, symbol table, num symbols
    optional_header_size = struct.unpack("<H", f.read(2))[0]
    characteristics = struct.unpack("<H", f.read(2))[0]

    # Read optional header magic
    magic = struct.unpack("<H", f.read(2))[0]
    is_pe32plus = magic == 0x20B

    print(f"PE type: {'PE32+' if is_pe32plus else 'PE32'}")
    print(f"Sections: {num_sections}")

    # Skip to import directory
    if is_pe32plus:
        f.seek(pe_offset + 24 + 112)  # PE32+ import dir offset
    else:
        f.seek(pe_offset + 24 + 96)   # PE32 import dir offset

    import_rva = struct.unpack("<I", f.read(4))[0]
    import_size = struct.unpack("<I", f.read(4))[0]

    print(f"Import directory RVA: 0x{import_rva:08X}, size: {import_size}")

    # Read section headers to find which section contains the import directory
    section_header_offset = pe_offset + 24 + optional_header_size
    sections = []
    f.seek(section_header_offset)
    for _ in range(num_sections):
        name = f.read(8).rstrip(b'\x00').decode('ascii', errors='replace')
        virtual_size = struct.unpack("<I", f.read(4))[0]
        virtual_addr = struct.unpack("<I", f.read(4))[0]
        raw_size = struct.unpack("<I", f.read(4))[0]
        raw_offset = struct.unpack("<I", f.read(4))[0]
        f.read(16)  # skip relocations, line numbers, characteristics
        sections.append((name, virtual_addr, virtual_size, raw_offset, raw_size))

    print("\nSections:")
    for name, va, vs, ro, rs in sections:
        print(f"  {name:8s} VA=0x{va:08X} Size=0x{vs:08X} Raw=0x{ro:08X}")

    # Find the section containing the import directory
    for name, va, vs, ro, rs in sections:
        if va <= import_rva < va + vs:
            file_offset = ro + (import_rva - va)
            print(f"\nImport directory in section '{name}' at file offset 0x{file_offset:08X}")

            # Read import descriptors
            f.seek(file_offset)
            print("\nImported DLLs:")
            while True:
                desc = f.read(20)
                if len(desc) < 20:
                    break
                lookup_rva, timestamp, forwarder, name_rva, iat_rva = struct.unpack("<IIIII", desc)
                if name_rva == 0:
                    break

                # Read DLL name
                for sec_name, sec_va, sec_vs, sec_ro, sec_rs in sections:
                    if sec_va <= name_rva < sec_va + sec_vs:
                        dll_name_offset = sec_ro + (name_rva - sec_va)
                        pos = f.tell()
                        f.seek(dll_name_offset)
                        dll_name = b""
                        while True:
                            c = f.read(1)
                            if c == b'\x00' or not c:
                                break
                            dll_name += c
                        f.seek(pos)
                        print(f"  {dll_name.decode('ascii', errors='replace')}")
                        break
            break
