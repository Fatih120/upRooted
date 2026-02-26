# Security Audit: February 2026

> **Scope:** Full codebase audit covering 37 C# hook files, 2 native C profilers, 7 Rust installer modules, 2 shell/PowerShell installers, and 1 Python packaging script.
>
> **Methodology:** Eight parallel analysis passes covering: auto-updater, file I/O, native interop, IPC/gRPC, logging/data leaks, cryptography, counter-mitigation surface, and installer deployment.

---

## Executive Summary

**68 findings** across 8 audit domains. The most severe issues cluster around three themes:

1. **Supply chain integrity:** The auto-update pipeline uses XOR obfuscation (not encryption), has no code signing, and embeds a recoverable GitHub PAT in the shipped DLL.
2. **SSRF in LinkEmbedEngine:** Chat messages containing internal/localhost URLs cause the victim's client to fetch those URLs from their local network.
3. **Detection surface:** Root could detect Uprooted via 13+ trivial vectors (env vars, visual tree tags, assembly names, file artifacts, named threads).

### Severity Distribution

| Severity | Count | Domains |
|----------|-------|---------|
| Critical | 5 | Auto-updater (3), Presence beacon (1), Installer (1) |
| High | 8 | Auto-updater (2), SSRF (3), Native interop (2), Installer (4) |
| Medium | 17 | File I/O (4), IPC (4), Logging (3), Installer (7), Profiler (3) |
| Low | 20 | Across all domains |
| Info | 7 | Positive findings / good practices |

---

## Part 1: Critical Findings

### C1. .uprpkg "Encryption" Is XOR Obfuscation

**Files:** `hook/AutoUpdater.cs:59-65,632-638` | `scripts/pack-update.py:16-21,39-45`

The update package format uses a custom XOR cipher:
```python
key = MASTER_KEY[pos % 64] ^ nonce[pos % 32] ^ ((pos >> 8) & 0xFF)
result[pos] = plaintext[pos] ^ key
```

Problems:
- **The keystream repeats every 256 bytes** (`LCM(64, 32, 256)`).
- **Known-plaintext attack is trivial:** `.deps.json` files have predictable content; XOR ciphertext with known plaintext recovers the keystream.
- **No MAC/signature:** An attacker can bit-flip the ciphertext and the plaintext changes correspondingly. No integrity check prevents this.
- **The 64-byte master key is hardcoded** in both `AutoUpdater.cs` and `pack-update.py`, and ships in the distributed DLL.

**Impact:** MITM or CDN compromise leads to arbitrary DLL injection into Root.exe at next restart.

### C2. No Cryptographic Signature on Update Packages

**File:** `hook/AutoUpdater.cs:395-424`

The only "verification" is magic bytes (`UPRK`), format version, file count, and filename presence checks. The SHA-256 hash at line 396 is used solely for hotfix detection (comparing against the previously-seen hash), not for verification against a trusted source. There is no RSA/ECDSA/Ed25519 signature, no code signing, and no hash pinning.

### C3. Hardcoded GitHub PAT in Distributed Binary

**File:** `hook/AutoUpdater.cs:38-56`

A GitHub Personal Access Token is XOR-"encrypted" with its own key stored in the same binary:
```csharp
private static readonly byte[] EncryptedPat = { 0x0B, 0x47, 0xD9, ... };
private static readonly byte[] PatXorKey   = { 0x6C, 0x2E, 0xAD, ... };
```

Anyone who decompiles the DLL (or XORs the two arrays) recovers the PAT. It grants read access to `The-Uprooted-Project/uprooted-private` releases.

### C4. HMAC Shared Secret with Plaintext Comment

**File:** `hook/UprootedPresenceBeacon.cs:36-49`

The HMAC secret is XOR-obfuscated, but line 39 contains the plaintext key in a comment:
```csharp
// Plain key = 4b9e2a71c38f561da4e7305c89f26b14d843972e61b50a7ce328549fc16a378d
```

Additionally present in `tools/presence-api/test.js` (4 occurrences). Any user can forge presence registrations for arbitrary UUIDs.

### C5. Overly Broad Process Kill in Bash Installer

**File:** `install-uprooted-linux.sh:1089-1093,1144-1149`

```bash
pkill -f "Root" 2>/dev/null || true
```

The `-f` flag matches full command lines. This kills **any** process whose command line contains "Root" (e.g., `/root/scripts/backup.sh`, `node /MyRootProject/server.js`). The Rust installer correctly uses `pkill -x` (exact match).

---

## Part 2: High-Severity Findings

### H1. SSRF in LinkEmbedEngine: No Private IP Validation

**File:** `hook/LinkEmbedEngine.cs:394,404-445,1135,1168,1289`

URLs from chat messages pass directly to HTTP fetch methods with **no validation against private/internal address ranges**. An attacker posts `http://169.254.169.254/latest/meta-data/` or `http://127.0.0.1:8080/admin` in chat, and every Uprooted client in that channel fetches it from their local network.

**Affected paths:**
- Primary URL fetch (`FetchAndInject`)
- oEmbed discovery (chained SSRF via attacker-controlled `<link>` tags)
- `og:image` URLs (fetched without validation)

No blocklist exists anywhere for localhost, RFC 1918 (10/8, 172.16/12, 192.168/16), link-local (169.254/16), or IPv6 loopback (::1).

### H2. JavaScript Injection via Incomplete String Escaping

**File:** `hook/LinkEmbedEngine.cs:879-882`

`EscapeJsString` does not escape Unicode line separators:
```csharp
private static string EscapeJsString(string s)
{
    return s.Replace("\\", "\\\\").Replace("'", "\\'")
            .Replace("\n", "\\n").Replace("\r", "\\r");
}
```

Missing: U+2028 (line separator), U+2029 (paragraph separator), and null bytes. A URL containing these characters can escape the JS string literal context.

### H3. No TLS Certificate Pinning on Update Downloads

**File:** `hook/AutoUpdater.cs:744-749`

`HttpClient` uses default settings with no `ServerCertificateCustomValidationCallback`. Combined with the lack of package signatures (C2), a rogue CA certificate (enterprise proxy, malware) enables full MITM.

### H4. No Integrity Check on Loaded Hook DLL

**Files:** `tools/uprooted_profiler.c:48` | `tools/uprooted_profiler_linux.c:230`

The profiler loads `UprootedHook.dll` from a user-writable directory without any hash/signature verification. A local attacker who replaces the DLL achieves code execution within Root.exe.

### H5. No Downgrade/Rollback Protection

**File:** `hook/AutoUpdater.cs:316-341`

No monotonic version counter prevents an attacker from advertising an older version with known vulnerabilities as the "latest."

### H6. No Download Integrity Verification (Bash Installer)

**File:** `install-uprooted-linux.sh:721-815`

Downloads a tarball from GitHub, checks only gzip magic bytes, then extracts directly. No SHA-256 checksum or GPG signature verification.

### H7-H8. Symlink Attacks on Deployed Files (Rust Installer)

**Files:** `installer/src-tauri/src/hook.rs:84-116` | `installer/src-tauri/src/patcher.rs:105-120`

Neither `deploy_files()` nor the HTML patch temp file check for symlinks before writing. A local attacker who creates a symlink at a known deploy path can redirect writes to arbitrary files.

---

## Part 3: Medium-Severity Findings

### File I/O and Settings

| ID | Finding | File | Lines |
|----|---------|------|-------|
| M1 | Raw JSON injection into HTML `<script>` tags: `uprooted-settings.json` is embedded without proper validation | `HtmlPatchVerifier.cs` | 289-303 |
| M2 | TOCTOU race in HTML patch writes: symlink between Exists/Read/Write | `HtmlPatchVerifier.cs` | 169-177 |
| M3 | Non-atomic HTML patch writes (corruption on crash) | `HtmlPatchVerifier.cs` | 263 |
| M4 | API keys (DeepL, NSFW) stored plaintext on disk with default umask | `UprootedSettings.cs` | 249, 272 |

### IPC, Bridge, and gRPC

| ID | Finding | File | Lines |
|----|---------|------|-------|
| M5 | DNS rebinding: URL validation (if added) can be bypassed via TTL=0 DNS | `LinkEmbedEngine.cs` | Architecture |
| M6 | HMAC presence secret trivially recoverable from binary | `UprootedPresenceBeacon.cs` | 38-49 |
| M7 | Full UUID logged in error paths | `UprootedPresenceBeacon.cs` | 129 |
| M8 | No UUID format validation before use in URL path (potential path traversal) | `UprootedPresenceBeacon.cs` | 313-321 |

### Logging and Data Exposure

| ID | Finding | File | Lines |
|----|---------|------|-------|
| M9 | Other users' deleted messages stored plaintext on disk + content in logs | `MessageStore.cs`, `MessageLogger.cs` | 72-103, 517 |
| M10 | Named pipe/FIFO without access controls (any local process reads logs) | `LogConsole.cs` | 85-90, 114 |
| M11 | Shell script written to predictable `/tmp/` path (symlink attack) | `LogConsole.cs` | 246-256 |

### Native Interop

| ID | Finding | File | Lines |
|----|---------|------|-------|
| M12 | Log file symlink race on Linux (no O_NOFOLLOW) | `uprooted_profiler_linux.c` | 371 |
| M13 | Overly permissive process name guard: accepts any binary starting with "Root" | `uprooted_profiler_linux.c` | 1312-1339 |
| M14 | Aggressive JS method resolution could match wrong method on future DotNetBrowser | `DotNetBrowserReflection.cs` | 166-410 |

### Installer

| ID | Finding | File | Lines |
|----|---------|------|-------|
| M15 | Environment variable poisoning via empty HOME/LOCALAPPDATA | `hook.rs`, `detection.rs` | 53-63, 19-40 |
| M16 | TOCTOU race on backup file creation | `patcher.rs` | 91-100 |
| M17 | Non-atomic ~/.profile writes | `hook.rs` | 341-367 |
| M18 | Desktop Exec= path injection (unquoted) | `install-uprooted-linux.sh`, `hook.rs` | 937-945, 445 |
| M19 | Over-aggressive block removal in ~/.profile cleanup | `hook.rs`, `install-uprooted-linux.sh` | 396-428, 408-427 |

### Cryptography

| ID | Finding | File | Lines |
|----|---------|------|-------|
| M20 | Google Translate API key hardcoded (semi-public, Google's own key) | `TranslateEngine.cs` | 47 |

---

## Part 4: Low-Severity Findings (Summary)

| ID | Finding | File |
|----|---------|------|
| L1 | Settings fallback to CWD on exception | `UprootedSettings.cs:86-89` |
| L2 | INI injection via unvalidated plugin names | `UprootedSettings.cs:176-179` |
| L3 | EscapeJsonString does not escape `</script>` | `HtmlPatchVerifier.cs:339-357` |
| L4 | Weak backup file creation logic | `HtmlPatchVerifier.cs:249-257` |
| L5 | Non-atomic message store truncate | `MessageStore.cs:238` |
| L6 | No file permissions on created files (0644 on Linux) | All file-creating modules |
| L7 | Dev password hash uses unsalted SHA-256 | `AutoUpdater.cs:36` |
| L8 | Path traversal in .uprpkg extraction (mitigated by filename allowlist) | `AutoUpdater.cs:610,431` |
| L9 | Unprotected settings file controls update behavior | `UprootedSettings.cs` |
| L10 | Incomplete log value escaping (log injection) | `Logger.cs:172-182` |
| L11 | Audit log writes raw protobuf fields | `AuditLogEngine.cs:317-329` |
| L12 | Notification API could be injection vector if misused | `DesktopNotification.cs:32-36` |
| L13 | Unsafe `wcscpy` + C:\ fallback in profiler | `uprooted_profiler.c:53-54` |
| L14 | `sprintf` without bounds in hex dump | Both profiler .c files |
| L15 | Non-thread-safe log file handle init | Both profiler .c files |
| L16 | Chat message content logged in discovery scan | `BrowserDiscovery.cs:343` |
| L17 | ReDoS potential in OG tag regex | `LinkEmbedEngine.cs:94-96` |
| L18 | SilentTypingEngine redirects to localhost:0 | `SilentTypingEngine.cs:84` |
| L19 | Single quotes in paths break generated wrapper scripts | `install-uprooted-linux.sh:898` |
| L20 | Diagnose mode mutates state (deploys files, sets env vars) | `cli.rs:286-324` |

---

## Part 5: Counter-Mitigation Analysis (Detection Surface)

This section enumerates how Root could detect and block Uprooted, ranked by detection reliability and implementation effort.

### Tier 1: Trivial Detection (1-3 LOC, zero false positives)

| Vector | Detection Method | Mitigation Difficulty |
|--------|-----------------|----------------------|
| **Environment variables** (`CORECLR_PROFILER={D1A6F5A0-...}`) | `Environment.GetEnvironmentVariable()` | Very Hard: required for CLR loading |
| **Loaded assemblies** (`UprootedHook`) | `AppDomain.CurrentDomain.GetAssemblies()` | Medium: entry points exempt from obfuscation |
| **Thread name** `"Uprooted-Injector"` | Thread enumeration | Trivial: rename to null/generic |
| **Named pipe** `"uprooted-log-console"` | Pipe enumeration / File.Exists | Trivial: randomize name |
| **Profiler GUID** `{D1A6F5A0-1234-4567-89AB-CDEF01234567}` | Env var check | Trivial: generate random GUID |

### Tier 2: Easy Detection (tree walk, file check, network monitor)

| Vector | Detection Method | Mitigation Difficulty |
|--------|-----------------|----------------------|
| **Visual tree tags** (~80+ `"uprooted-*"` tags) | `Control.Tag?.ToString()?.StartsWith("uprooted")` | **Hard**: hundreds of usages, architectural |
| **File artifacts** (`uprooted-hook.log`, `uprooted-settings.ini`, directory `uprooted/`) | `File.Exists()` | Medium: rename files |
| **HTML patch markers** (`<!-- uprooted:start -->`) | HTML hash check or string search | Medium: use opaque markers |
| **Network traffic** to `api.uprooted.sh` | DiagnosticListener / DNS | Medium: proxy through generic hostname |
| **User-Agent** `"Uprooted-AutoUpdater/1.0"` | DiagnosticListener header check | Trivial: rename |
| **Process modules** (`uprooted_profiler.dll`) | `Process.Modules` | Trivial: rename DLLs |

### Tier 3: Moderate Detection

| Vector | Detection Method | Mitigation Difficulty |
|--------|-----------------|----------------------|
| **DiagnosticListener subscriptions** | Enumerate subscribers to HttpHandlerDiagnosticListener | Hard: architectural to SilentTyping |
| **Visual tree text** (`"UPROOTED"`, `"Uprooted Dev"`, version strings) | Text content scan | Hard: user-visible strings |
| **CSS/JS element IDs** (`uprooted-link-embeds-css`) | `document.getElementById()` | Medium |
| **Timing side channel** (multi-phase polling adds startup delay) | Startup timing measurement | Hard: inherent to injection |

### Key Mitigations to Prioritize

1. **Clear env vars post-injection** in the profiler's native code (call `SetEnvironmentVariable(name, NULL)` / `unsetenv()` after `Prof_Initialize`).
2. **Replace visual tree tags** with a `ConditionalWeakTable<Control, string>` lookup or short opaque tokens that change per build.
3. **Rename thread** from `"Uprooted-Injector"` to `null` or a generic .NET-like name.
4. **Generate a random profiler GUID** instead of the obviously hand-crafted `D1A6F5A0-1234-4567-89AB-CDEF01234567`.
5. **Rename file artifacts** and directories to non-identifiable names.
6. **Use opaque HTML markers** instead of `<!-- uprooted:start -->`.

---

## Part 6: Positive Security Observations

Several good practices are already in place:

1. **Atomic settings writes:** `UprootedSettings.Save()` uses write-to-temp-then-rename.
2. **Embed rendering via native Avalonia controls:** No XSS risk since OG metadata renders as `TextBlock.Text`, not HTML.
3. **One-shot injection guards:** Both profilers use `InterlockedCompareExchange` / `__sync_val_compare_and_swap`.
4. **Event mask clearing:** Profilers clear all event subscriptions after injection, minimizing the callback surface.
5. **Try/catch in injected IL:** Hook loading failures don't crash Root.exe.
6. **`set -euo pipefail`** in bash installer for strict error handling.
7. **User-scoped registry** (HKCU, not HKLM) for Windows environment variables.
8. **No weak crypto algorithms:** No MD5/SHA1/DES/RC4 in Uprooted-authored code.
9. **CI/CD secrets management:** All secrets via `${{ secrets.* }}`, `.env` files properly gitignored.
10. **Timing-safe HMAC comparison** server-side (`crypto.timingSafeEqual()`).
11. **UUID hashing before DB storage** in presence API for privacy.

---

## Part 7: Prioritized Recommendations

### Immediate (security impact)

1. **Add Ed25519 package signing** to the auto-updater. Embed only the public key in the client. This is the single highest-impact fix: it closes C1, C2, H3, and H5 simultaneously.
2. **Add SSRF protection** to LinkEmbedEngine: validate resolved IPs against private ranges at connection time using `SocketsHttpHandler.ConnectCallback`.
3. **Fix `EscapeJsString`**: add U+2028, U+2029, and null byte escaping.
4. **Sanitize JSON injection** in HtmlPatchVerifier: either parse-and-reserialize the settings JSON or reject `</script>` sequences.
5. **Rotate the GitHub PAT** immediately (it is extractable from any distributed build). Move to a server-side proxy for dev-channel downloads.

### Short-term (hardening)

6. **Remove the plaintext HMAC key comment** from `UprootedPresenceBeacon.cs:39`.
7. **Add symlink checks** before all file writes in both the Rust installer and the HTML patcher.
8. **Set restrictive file permissions** (0600) on log files, settings, and message store on Linux.
9. **Sanitize .uprpkg filenames**: reject paths containing `/`, `\`, or `..`.
10. **Fix `pkill -f "Root"`** in the bash installer to `pkill -x "Root"`.
11. **Validate UUID format** before using in URL paths in the presence beacon.
12. **Escape `</` as `<\/`** in `EscapeJsonString` for inline `<script>` safety.

### Counter-mitigation hardening

13. **Clear profiler env vars** in native code post-initialization.
14. **Replace `"uprooted-*"` visual tree tags** with opaque identifiers or ConditionalWeakTable.
15. **Remove/randomize** the `"Uprooted-Injector"` thread name.
16. **Generate a real random GUID** for the profiler CLSID.
17. **Rename identifiable files and directories** (log, settings, install dir).
18. **Use generic User-Agent** strings on HTTP requests.
