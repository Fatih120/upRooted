#!/usr/bin/env python3
"""
Pack update files into an encrypted .uprpkg package for Uprooted auto-updater.

Usage:
    python scripts/pack-update.py --input-dir ./artifacts --output auto-update.uprpkg
    python scripts/pack-update.py --input-dir ./artifacts --output auto-update.uprpkg --verify
"""

import argparse
import os
import struct
import sys

# 64-byte master key — must match AutoUpdater.cs PackageMasterKey exactly
MASTER_KEY = bytes([
    0x76, 0xC3, 0x47, 0x44, 0xE3, 0xD1, 0x44, 0xA2, 0x4C, 0xCA, 0xA3, 0x95, 0x5C, 0x02, 0x62, 0xF4,
    0x26, 0x1D, 0x46, 0x30, 0x4F, 0xB3, 0x5D, 0x5A, 0x53, 0x5D, 0x71, 0x7B, 0x67, 0x71, 0x13, 0x71,
    0xD0, 0x92, 0x47, 0x07, 0x32, 0x06, 0x84, 0xA8, 0x9C, 0xF4, 0x06, 0x56, 0x9D, 0x1D, 0xC8, 0x0B,
    0xED, 0x62, 0x59, 0x79, 0xC8, 0x72, 0x20, 0xD5, 0x8C, 0x1B, 0xA0, 0xE1, 0xA4, 0xE5, 0x84, 0x0B,
])

# Expected files (same list as AutoUpdater.cs UpdateFiles)
EXPECTED_FILES = [
    "UprootedHook.dll",
    "UprootedHook.deps.json",
    "UprootedHook.net9.dll",
    "UprootedHook.net9.deps.json",
    "uprooted-preload.js",
    "uprooted.css",
    "nsfw-filter.js",
    "link-embeds.js",
]

MAGIC = b"UPRK"
FORMAT_VERSION = 0x01


def encrypt_data(plaintext: bytes, nonce: bytes) -> bytes:
    """Encrypt (or decrypt — symmetric) data using multi-layer XOR key derivation."""
    result = bytearray(len(plaintext))
    for pos in range(len(plaintext)):
        key = MASTER_KEY[pos % 64] ^ nonce[pos % 32] ^ ((pos >> 8) & 0xFF)
        result[pos] = plaintext[pos] ^ key
    return bytes(result)


def pack(input_dir: str, output_path: str) -> dict[str, bytes]:
    """Pack files from input_dir into an encrypted .uprpkg at output_path.
    Returns dict of filename -> original bytes for verification."""
    # Read all expected files
    file_data: dict[str, bytes] = {}
    for filename in EXPECTED_FILES:
        path = os.path.join(input_dir, filename)
        if not os.path.isfile(path):
            print(f"ERROR: Missing file: {path}", file=sys.stderr)
            sys.exit(1)
        with open(path, "rb") as fh:
            data = fh.read()
        if len(data) == 0:
            print(f"ERROR: Empty file: {path}", file=sys.stderr)
            sys.exit(1)
        file_data[filename] = data

    # Generate random nonce
    nonce = os.urandom(32)

    # Build package
    buf = bytearray()

    # Header
    buf += MAGIC
    buf += struct.pack("B", FORMAT_VERSION)
    buf += struct.pack("<H", len(file_data))
    buf += nonce

    # File entries
    for filename, data in file_data.items():
        name_bytes = filename.encode("utf-8")
        buf += struct.pack("<H", len(name_bytes))
        buf += name_bytes
        buf += struct.pack("<I", len(data))
        buf += encrypt_data(data, nonce)

    # Write output
    os.makedirs(os.path.dirname(output_path) or ".", exist_ok=True)
    with open(output_path, "wb") as f:
        f.write(buf)

    print(f"Packed {len(file_data)} files into {output_path}")
    print(f"  Package size: {len(buf):,} bytes")
    for filename, data in file_data.items():
        print(f"  {filename}: {len(data):,} bytes")

    return file_data


def unpack(package_path: str) -> dict[str, bytes]:
    """Unpack and decrypt a .uprpkg file. Returns filename -> decrypted bytes."""
    with open(package_path, "rb") as fh:
        data = fh.read()

    if len(data) < 39:
        print("ERROR: Package too small", file=sys.stderr)
        sys.exit(1)

    if data[:4] != MAGIC:
        print("ERROR: Invalid package (bad magic)", file=sys.stderr)
        sys.exit(1)

    if data[4] != FORMAT_VERSION:
        print(f"ERROR: Unsupported version: {data[4]}", file=sys.stderr)
        sys.exit(1)

    file_count = struct.unpack_from("<H", data, 5)[0]
    nonce = data[7:39]
    offset = 39

    result: dict[str, bytes] = {}
    for i in range(file_count):
        name_len = struct.unpack_from("<H", data, offset)[0]
        offset += 2
        filename = data[offset:offset + name_len].decode("utf-8")
        offset += name_len
        data_len = struct.unpack_from("<I", data, offset)[0]
        offset += 4
        encrypted = data[offset:offset + data_len]
        offset += data_len
        result[filename] = encrypt_data(encrypted, nonce)

    return result


def main():
    parser = argparse.ArgumentParser(description="Pack Uprooted update files into encrypted .uprpkg")
    parser.add_argument("--input-dir", required=True, help="Directory containing the 8 update files")
    parser.add_argument("--output", required=True, help="Output .uprpkg path")
    parser.add_argument("--verify", action="store_true", help="Pack then unpack and verify byte-for-byte match")
    args = parser.parse_args()

    originals = pack(args.input_dir, args.output)

    if args.verify:
        print("\nVerifying round-trip...")
        unpacked = unpack(args.output)

        if set(unpacked.keys()) != set(originals.keys()):
            print(f"ERROR: File set mismatch. Expected {sorted(originals.keys())}, got {sorted(unpacked.keys())}", file=sys.stderr)
            sys.exit(1)

        for filename in originals:
            if unpacked[filename] != originals[filename]:
                print(f"ERROR: Content mismatch for {filename}", file=sys.stderr)
                sys.exit(1)
            print(f"  {filename}: OK ({len(originals[filename]):,} bytes)")

        print("Verification passed — all files match byte-for-byte")


if __name__ == "__main__":
    main()
