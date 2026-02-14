"""
Post-mortem log analyzer for uprooted-hook.log
Parses the log and generates a structured report with:
- Startup timeline
- Visual tree structure (from Diag dumps)
- ListBox items and their styles
- Injection events and state changes
- Errors and warnings
- Back button / freeze analysis
- Style data extraction

Usage: python analyze-log.py [logfile]
"""

import sys
import os
import re
from collections import defaultdict
from datetime import datetime

DEFAULT_LOG = os.path.join(
    os.environ.get("LOCALAPPDATA", ""),
    "Root Communications", "Root", "profile", "default", "uprooted-hook.log"
)

def parse_timestamp(line):
    m = re.match(r'\[(\d{2}:\d{2}:\d{2}\.\d{3})\]', line)
    if m:
        return m.group(1)
    return None

def parse_category(line):
    m = re.match(r'\[\d{2}:\d{2}:\d{2}\.\d{3}\]\s*\[(\w+)\]', line)
    if m:
        return m.group(1)
    return None

def parse_message(line):
    m = re.match(r'\[\d{2}:\d{2}:\d{2}\.\d{3}\]\s*\[\w+\]\s*(.*)', line)
    if m:
        return m.group(1)
    return line

def main():
    logfile = sys.argv[1] if len(sys.argv) > 1 else DEFAULT_LOG

    if not os.path.exists(logfile):
        print(f"Log file not found: {logfile}")
        sys.exit(1)

    with open(logfile, 'r', encoding='utf-8', errors='replace') as f:
        lines = f.readlines()

    print(f"=== Uprooted Hook Log Analysis ===")
    print(f"File: {logfile}")
    print(f"Total lines: {len(lines)}")
    print()

    # Categorize lines
    categories = defaultdict(list)
    errors = []
    warnings = []
    timestamps = []
    tree_lines = []
    listbox_items = []
    style_data = []
    injection_events = []
    back_button_events = []

    for i, line in enumerate(lines):
        line = line.rstrip()
        ts = parse_timestamp(line)
        cat = parse_category(line)
        msg = parse_message(line)

        if ts:
            timestamps.append((ts, i))
        if cat:
            categories[cat].append((i, ts, msg))

        # Errors and warnings
        if re.search(r'(?i)error|fail|crash|fatal|exception', line):
            errors.append((i+1, line.rstrip()))
        elif re.search(r'(?i)warn|MISSING|abort', line):
            warnings.append((i+1, line.rstrip()))

        # Tree structure lines (indented with type names)
        if cat in ('Diag', 'Style', 'Tree'):
            tree_lines.append((i, msg))

        # ListBox items
        m = re.search(r'\[(\d+)\]\s*"([^"]*)".*?(\d+)x(\d+).*?y=(\S+)', msg or '')
        if m and cat == 'Diag':
            listbox_items.append({
                'index': int(m.group(1)),
                'text': m.group(2),
                'width': m.group(3),
                'height': m.group(4),
                'y': m.group(5)
            })

        # Style data
        if 'FontSize=' in line or 'FontWeight=' in line or 'Foreground=' in line or 'BG=' in line:
            style_data.append((i+1, line.rstrip()))

        # Injection events
        if cat == 'Injector' and any(kw in msg for kw in [
            'detected', 'inject', 'cleanup', 'complete', 'removed', 'clicked',
            'selection changed', 'closed', 'Back button', 'PointerPressed',
            'MaxHeight', 'ScrollViewer', 'content page'
        ]):
            injection_events.append((ts, msg))

        # Back button
        if 'back button' in line.lower() or 'Back button' in line:
            back_button_events.append((i+1, line.rstrip()))

    # === REPORT ===

    # 1. Timeline
    print("=" * 60)
    print("STARTUP TIMELINE")
    print("=" * 60)
    for cat_name in ['Startup', 'Reflection']:
        if cat_name in categories:
            for idx, ts, msg in categories[cat_name]:
                if any(kw in msg for kw in ['Phase', 'Resolved', 'OK', 'FAIL', 'Ready', 'Hook v']):
                    print(f"  [{ts}] {msg}")
    print()

    # 2. Injection Events
    print("=" * 60)
    print("INJECTION EVENTS")
    print("=" * 60)
    for ts, msg in injection_events:
        print(f"  [{ts}] {msg}")
    if not injection_events:
        print("  (none - settings page was not opened)")
    print()

    # 3. ListBox Items
    print("=" * 60)
    print("LISTBOX ITEMS")
    print("=" * 60)
    for item in listbox_items:
        marker = " <-- ADVANCED" if item['text'] == 'Advanced' else ""
        print(f"  [{item['index']:2d}] \"{item['text']}\" ({item['width']}x{item['height']} at y={item['y']}){marker}")
    if not listbox_items:
        print("  (no ListBox item data captured)")
    print()

    # 4. Visual Tree Structure
    print("=" * 60)
    print("VISUAL TREE DUMPS")
    print("=" * 60)
    in_section = False
    section_name = ""
    for i, msg in tree_lines:
        if '===' in msg:
            in_section = True
            section_name = msg.strip('= ')
            print(f"\n  --- {section_name} ---")
        elif in_section and msg.strip():
            # Indent tree lines
            print(f"    {msg}")
    if not tree_lines:
        print("  (no tree dumps captured)")
    print()

    # 5. Style Data
    print("=" * 60)
    print("STYLE DATA (for matching)")
    print("=" * 60)
    seen = set()
    for lineno, line in style_data:
        # Deduplicate
        key = re.sub(r'\[\d{2}:\d{2}:\d{2}\.\d{3}\]\s*', '', line)
        if key not in seen:
            seen.add(key)
            print(f"  L{lineno}: {key}")
    if not style_data:
        print("  (no style data captured)")
    print()

    # 6. Back Button Analysis
    print("=" * 60)
    print("BACK BUTTON ANALYSIS")
    print("=" * 60)
    for lineno, line in back_button_events:
        print(f"  L{lineno}: {line}")
    if not back_button_events:
        print("  (no back button events)")
    print()

    # 7. Errors & Warnings
    print("=" * 60)
    print(f"ERRORS ({len(errors)})")
    print("=" * 60)
    for lineno, line in errors:
        print(f"  L{lineno}: {line}")
    if not errors:
        print("  (none)")
    print()

    print("=" * 60)
    print(f"WARNINGS ({len(warnings)})")
    print("=" * 60)
    for lineno, line in warnings:
        print(f"  L{lineno}: {line}")
    if not warnings:
        print("  (none)")
    print()

    # 8. Category Summary
    print("=" * 60)
    print("LOG CATEGORY SUMMARY")
    print("=" * 60)
    for cat_name, entries in sorted(categories.items(), key=lambda x: -len(x[1])):
        print(f"  [{cat_name}]: {len(entries)} lines")
    print()

    # 9. Key Measurements
    print("=" * 60)
    print("KEY MEASUREMENTS")
    print("=" * 60)
    for cat_name in categories:
        for idx, ts, msg in categories[cat_name]:
            # Extract numeric measurements
            for pattern, label in [
                (r'MaxHeight.*?(\d+\.?\d*)', 'ListBox MaxHeight'),
                (r'Clip height.*?(\d+\.?\d*)', 'Clip Height'),
                (r'Advanced.*?index\s*(\d+)', 'Advanced Index'),
                (r'(\d+) controls added', 'Controls Injected'),
                (r'Bounds:\s*(\S+)', 'Content Bounds'),
                (r'size=(\S+)', 'Sidebar Size'),
            ]:
                m = re.search(pattern, msg)
                if m:
                    print(f"  {label}: {m.group(1)}")
    print()

    # 10. Duration analysis
    if len(timestamps) >= 2:
        first_ts = timestamps[0][0]
        last_ts = timestamps[-1][0]
        print("=" * 60)
        print("TIMING")
        print("=" * 60)
        print(f"  First log entry: {first_ts}")
        print(f"  Last log entry:  {last_ts}")

        # Find key milestones
        for cat_name in categories:
            for idx, ts, msg in categories[cat_name]:
                if 'Hook Ready' in msg:
                    print(f"  Hook ready at:   {ts}")
                if 'Injection complete' in msg:
                    print(f"  Injected at:     {ts}")
                if 'cleaning up' in msg.lower():
                    print(f"  Cleanup at:      {ts}")
        print()

if __name__ == '__main__':
    main()
