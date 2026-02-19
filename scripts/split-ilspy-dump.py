#!/usr/bin/env python3
"""Split a multi-class ILSpy export into individual .cs files.

ILSpy exports multiple classes into one file. Each class begins with:
    // Assembly, Version=x.x.x.x, Culture=neutral, PublicKeyToken=...
    // Full.Namespace.ClassName

This script splits on that boundary and writes one file per class into
the same directory as the input (or a specified output directory).

Usage:
    python3 scripts/split-ilspy-dump.py research/ilspy-dumps/_batch.cs
    python3 scripts/split-ilspy-dump.py export.cs -o research/ilspy-dumps/
    python3 scripts/split-ilspy-dump.py export.cs --dry-run
    python3 scripts/split-ilspy-dump.py export.cs --prefix Style_

Filename derivation (from line 2 comment):
    // RootApp.Client.Avalonia.UI.Messages.MessageView  →  MessageView.cs
    // CompiledAvaloniaXaml.!AvaloniaResources           →  -AvaloniaResources.cs
    // Avalonia.Controls.CheckBox                        →  CheckBox.cs

Duplicate handling: if a file already exists, appends _2, _3, etc.
"""

import argparse
import re
import sys
from pathlib import Path

ASSEMBLY_PATTERN = re.compile(
    r"^// .+, Version=\d+\.\d+\.\d+\.\d+, Culture=\w+, PublicKeyToken="
)


def derive_filename(class_comment: str, prefix: str) -> str:
    """Derive a .cs filename from the ILSpy class comment (line 2).

    Examples:
        '// RootApp.Client.Avalonia.UI.Messages.MessageView' → 'MessageView.cs'
        '// CompiledAvaloniaXaml.!AvaloniaResources'          → '-AvaloniaResources.cs'
        '// CompiledAvaloniaXaml.!AvaloniaResources.XamlClosure_53' → 'XamlClosure_53.cs'
    """
    # Strip leading '// '
    name = class_comment.lstrip("/").strip()

    # Take the last segment after the final dot
    if "." in name:
        name = name.rsplit(".", 1)[-1]

    # ILSpy uses ! prefix for generated resources — replace with -
    name = name.replace("!", "-")

    # Sanitize: keep alphanumeric, underscore, hyphen
    name = re.sub(r"[^a-zA-Z0-9_\-]", "_", name)

    if not name:
        name = "Unknown"

    return f"{prefix}{name}.cs"


def split_dump(input_path: Path, output_dir: Path, prefix: str, dry_run: bool) -> list:
    """Split a multi-class ILSpy dump into individual files.

    Returns list of (filename, line_count, class_comment) tuples.
    """
    lines = input_path.read_text(encoding="utf-8").splitlines(keepends=True)

    chunks = []  # list of (assembly_comment, class_comment, content_lines)
    current_assembly = None
    current_class = None
    current_lines = []

    i = 0
    while i < len(lines):
        line = lines[i]
        stripped = line.rstrip("\n\r")

        # Detect boundary: assembly comment followed by class comment
        if ASSEMBLY_PATTERN.match(stripped) and i + 1 < len(lines):
            next_line = lines[i + 1].rstrip("\n\r")
            if next_line.startswith("// ") and not ASSEMBLY_PATTERN.match(next_line):
                # Save previous chunk
                if current_assembly is not None:
                    chunks.append((current_assembly, current_class, current_lines))

                current_assembly = stripped
                current_class = next_line
                current_lines = [line, lines[i + 1]]
                i += 2
                continue

        current_lines.append(line)
        i += 1

    # Save final chunk
    if current_assembly is not None:
        chunks.append((current_assembly, current_class, current_lines))

    if not chunks:
        print(f"No ILSpy class boundaries found in {input_path}")
        return []

    results = []
    used_names = {}

    for assembly_comment, class_comment, content_lines in chunks:
        base_name = derive_filename(class_comment, prefix)

        # Handle duplicates
        if base_name in used_names:
            used_names[base_name] += 1
            stem = base_name.rsplit(".cs", 1)[0]
            base_name = f"{stem}_{used_names[base_name]}.cs"
        else:
            used_names[base_name] = 1

        out_path = output_dir / base_name
        line_count = len(content_lines)

        # Check for existing file
        exists = out_path.exists()
        if exists and not dry_run:
            # Find a non-colliding name
            counter = 2
            while out_path.exists():
                stem = base_name.rsplit(".cs", 1)[0]
                out_path = output_dir / f"{stem}_{counter}.cs"
                counter += 1

        if dry_run:
            status = "EXISTS" if exists else "NEW"
            print(f"  [{status}] {out_path.name} ({line_count} lines) ← {class_comment}")
        else:
            out_path.write_text("".join(content_lines), encoding="utf-8")

        results.append((out_path.name, line_count, class_comment))

    return results


def main():
    parser = argparse.ArgumentParser(
        description="Split a multi-class ILSpy export into individual .cs files."
    )
    parser.add_argument("input", type=Path, help="Input .cs file with multiple classes")
    parser.add_argument(
        "-o", "--output-dir", type=Path, default=None,
        help="Output directory (default: same as input file)"
    )
    parser.add_argument(
        "--dry-run", action="store_true",
        help="Show what would be created without writing files"
    )
    parser.add_argument(
        "--prefix", default="",
        help="Prefix for output filenames (e.g. 'Style_' or 'VM_')"
    )
    parser.add_argument(
        "--delete-input", action="store_true",
        help="Delete the input batch file after successful split"
    )

    args = parser.parse_args()

    if not args.input.exists():
        print(f"Error: {args.input} not found", file=sys.stderr)
        sys.exit(1)

    output_dir = args.output_dir or args.input.parent
    output_dir.mkdir(parents=True, exist_ok=True)

    if args.dry_run:
        print(f"Dry run — scanning {args.input}:")

    results = split_dump(args.input, output_dir, args.prefix, args.dry_run)

    if not results:
        sys.exit(1)

    total_lines = sum(r[1] for r in results)
    action = "Would create" if args.dry_run else "Created"
    print(f"\n{action} {len(results)} files ({total_lines} lines total) in {output_dir}/")

    if args.delete_input and not args.dry_run and results:
        args.input.unlink()
        print(f"Deleted input: {args.input}")


if __name__ == "__main__":
    main()
