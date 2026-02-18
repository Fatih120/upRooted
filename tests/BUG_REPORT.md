# Uprooted Test Bug Report

Generated after running 170 tests across: ColorUtils, GradientBrush, ClearUrlsEngine, UprootedSettings, MessageStore.

## Summary

**All 170 tests passed. Zero bugs discovered in the three new modules under test.**

| Module | Tests | Passed | Failed | Bugs |
|--------|-------|--------|--------|------|
| ColorUtils | 57 | 57 | 0 | 0 |
| GradientBrush | 15 | 15 | 0 | 0 |
| ClearUrlsEngine | 58 | 58 | 0 | 0 |
| UprootedSettings | 22 | 22 | 0 | 0 |
| MessageStore | 18 | 18 | 0 | 0 |
| **Total** | **170** | **170** | **0** | **0** |

---

## ClearUrlsEngine — Observations

All 33 declared tracking params are correctly stripped. Behavioral notes confirmed by tests:

- **Case-insensitive**: `UTM_SOURCE`, `FbClId` etc. are stripped (uses `StringComparer.OrdinalIgnoreCase`).
- **Prefix-safe**: `utm_source2` is NOT stripped (exact key match only).
- **Fragment-safe**: `#hash` is preserved and never interpreted as a query string.
- **Idempotent**: `CleanUrl(CleanUrl(url)) == CleanUrl(url)` always holds.
- **Empty query**: `?` with no params returns the URL unchanged.
- **Valueless params**: `?fbclid` (no `=`) is correctly identified as a tracking param by key.
- **Fragment-before-query**: `page#section?utm_source=x` — the `?` is inside the fragment; no stripping occurs. Correct behavior.

---

## UprootedSettings — Observations

INI parsing is robust. Behavioral notes confirmed by tests:

- **Bool parsing is case-sensitive**: Only `"true"` resolves to `true`. Values like `"True"`, `"yes"`, `"1"` yield `false`. This is intentional.
- **NsfwThreshold uses InvariantCulture**: European comma-decimal `"0,75"` fails to parse and falls back to default `0.6`. By design.
- **Negative MaxMessages rejected**: `MaxMessages > 0` guard works correctly; negative values fall back to default `10000`.
- **Cache invalidation**: `InvalidateCache()` correctly forces a re-read on next `Load()`.
- **Migration**: Existing installs without `Migrated.PluginDefaults=true` get all 6 legacy plugins enabled; explicitly-disabled plugins are not overridden.
- **Static `_settingsPath` caching**: Requires reflection-based reset between tests (not a bug, just a test infrastructure concern).

---

## MessageStore — Observations

Flat-file persistence is correct and resilient. Behavioral notes confirmed by tests:

- **URI encoding roundtrip**: Pipe `|`, newline `\n`, and Unicode `你好 🌍` all survive encode/decode correctly.
- **Malformed lines**: Lines with too few fields are silently skipped without crashing.
- **EDIT/DEL/CLR for unknown IDs**: Silently skipped. No stray entries, no crashes.
- **Truncate semantics**: Keeps the LAST N data lines (newest-first by append order). Header is preserved. File is unchanged when at or below the limit.
- **DateTime roundtrip**: The `O` (round-trip) format preserves millisecond precision and UTC kind.
- **Timer in tests**: The 5-second flush timer is harmless in tests — after `FlushBuffer` is called via reflection, the buffer is empty so subsequent timer ticks are no-ops.

---

## Potential Future Test Coverage

Not tested (out of scope for this run, no evidence of bugs):

- **ClearUrlsEngine** — actual keyboard event interception (requires Avalonia + AvaloniaEdit runtime)
- **UprootedSettings.Save()** — edge case: writing settings when the profile directory doesn't exist yet
- **MessageStore.FlushBuffer()** — concurrent writes from multiple threads hitting the write lock
- **Settings cache TTL** — 10-second TTL expiry without `InvalidateCache()` (would require `Thread.Sleep(11000)`)
