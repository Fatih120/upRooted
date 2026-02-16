# How to Extract Your Root Bearer Token

## Method 1: Named Pipe Intercept (Best - captures token automatically)
Run `pipe_intercept.py` while starting the Root app. It monitors the NativeToWebRtcBridge
named pipe and captures the token when the .NET app passes it to the WebRTC iframe.

## Method 2: Memory Scan (If app is already running)
Run `memory_scan.py` while the Root app is running. It scans the Root.exe process memory
for tokens matching the 128-byte format (userId + deviceId + signature).

## Method 3: Network Intercept (Requires mitmproxy)
Set up mitmproxy and configure Root to use it. Capture the Authorization: Bearer header
from any gRPC-web request to api.rootapp.com.

## Method 4: DotNetBrowser DevTools (If available)
If DotNetBrowser has remote debugging enabled:
1. Connect to the debug port
2. In the WebRTC iframe console, run:
   ```js
   // The QNe service client stores the token
   // Look for window.__nativeToWebRtc state
   console.log(document.querySelector('iframe')?.contentWindow);
   ```

## Method 5: Paste Token Manually
If you can access any Root API request (via Wireshark, Fiddler, etc.):
1. Find any request to api.rootapp.com
2. Copy the Authorization header value (after "Bearer ")
3. Paste it into the TOKEN variable in root_exploit.py
