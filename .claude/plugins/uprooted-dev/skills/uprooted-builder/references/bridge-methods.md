# Bridge Methods Quick Reference

All bridge method signatures for building patches. Source: `src/types/bridge.ts`.

## Type Aliases

```typescript
type UserGuid = string & { readonly __brand: "UserGuid" };     // Opaque user ID
type DeviceGuid = string & { readonly __brand: "DeviceGuid" }; // Opaque device ID
type Theme = "dark" | "light" | "pure-dark";
type TileType = "camera" | "screen" | "audio";
type ScreenQualityMode = "motion" | "detail" | "auto";
type Codec = string;
```

## Key Types

```typescript
interface Coordinates { x: number; y: number; }
interface IUserResponse { userId: UserGuid; displayName: string; avatarUrl?: string; [key: string]: unknown; }
type WebRtcPermission = Record<string, boolean>;
interface VolumeBoosterSettings { enabled: boolean; gain: number; }
interface WebRtcError { code: string; message: string; }
interface IPacket { type: string; data: unknown; }
interface UserMediaStreamConstraints { audio?: MediaTrackConstraints | boolean; video?: MediaTrackConstraints | boolean; }
interface DisplayMediaStreamConstraints { audio?: MediaTrackConstraints | boolean; video?: MediaTrackConstraints | boolean; }

interface InitializeDesktopWebRtcPayload {
  token: string;           // Bearer auth token
  channelId: string;       // Voice channel ID
  communityId: string;     // Community/server ID
  userId: UserGuid;        // Current user
  deviceId: DeviceGuid;    // Current device
  theme: Theme;            // Active theme at join
  [key: string]: unknown;  // Future fields
}
```

## BridgeEvent Interface

```typescript
interface BridgeEvent {
  method: string;           // Method name being called
  args: unknown[];          // Call arguments (mutable — change to modify args)
  cancelled: boolean;       // Set to true to cancel the call
  returnValue?: unknown;    // Return value when cancelled/replaced
}
```

## INativeToWebRtc — Native to JS (42 methods)

Intercepted with `bridge: "nativeToWebRtc"`. C# host controls the WebRTC session.

### Connection

| Method | Signature |
|--------|-----------|
| `initialize` | `(state: InitializeDesktopWebRtcPayload): void` |
| `disconnect` | `(): void` |

### Media Control

| Method | Signature |
|--------|-----------|
| `setIsVideoOn` | `(isVideo: boolean): void` |
| `setIsScreenShareOn` | `(isScreenShare: boolean, withAudio?: boolean): void` |
| `setIsAudioOn` | `(isAudio: boolean): void` |

### Device Selection

| Method | Signature |
|--------|-----------|
| `updateVideoDeviceId` | `(videoSourceId: string): void` |
| `updateAudioInputDeviceId` | `(micSourceId: string): void` |
| `updateAudioOutputDeviceId` | `(soundSourceId: string): void` |
| `updateScreenShareDeviceId` | `(screenSourceId: string): void` |
| `updateScreenAudioDeviceId` | `(screenAudioSourceId: string): void` |

### User State

| Method | Signature |
|--------|-----------|
| `updateProfile` | `(user: IUserResponse): void` |
| `updateMyPermission` | `(myUserPermission: WebRtcPermission): void` |
| `setPushToTalkMode` | `(isPushToTalkMode: boolean): void` |
| `setPushToTalk` | `(isPushingToTalk: boolean): void` |
| `setMute` | `(isMuted: boolean): void` |
| `setDeafen` | `(isDeafened: boolean): void` |
| `setHandRaised` | `(isHandRaised: boolean): void` |
| `setTheme` | `(theme: Theme): void` |

### Audio & Quality

| Method | Signature |
|--------|-----------|
| `setNoiseGateThreshold` | `(threshold: number): void` |
| `setDenoisePower` | `(power: number): void` |
| `setScreenQualityMode` | `(qualityMode: ScreenQualityMode): void` |
| `setPreferredCodecs` | `(preferredCodecs: Codec[]): void` |
| `setUserMediaConstraints` | `(constraints: UserMediaStreamConstraints): void` |
| `setDisplayMediaConstraints` | `(constraints: DisplayMediaStreamConstraints): void` |
| `setScreenContentHint` | `(contentHint: string): void` |

### UI Control

| Method | Signature |
|--------|-----------|
| `toggleFullFocus` | `(isFullFocus: boolean): void` |
| `screenPickerDismissed` | `(): void` |

### Moderation

| Method | Signature |
|--------|-----------|
| `setAdminMute` | `(userId: UserGuid, isAdminMuted: boolean): void` |
| `setAdminDeafen` | `(userId: UserGuid, isAdminDeafened: boolean): void` |
| `kick` | `(userId: UserGuid): void` |

### Volume

| Method | Signature |
|--------|-----------|
| `setTileVolume` | `(userId: UserGuid, tileType: TileType, volume: number): void` |
| `setOutputVolume` | `(volume: number): void` |
| `setInputVolume` | `(volume: number): void` |
| `customizeVolumeBooster` | `(settings: VolumeBoosterSettings): void` |

### Packets & Native Audio

| Method | Signature |
|--------|-----------|
| `receiveRawPacket` | `(data: unknown): void` |
| `receiveRawPacketContainer` | `(data: unknown): void` |
| `receivePacket` | `(packet: IPacket): void` |
| `nativeLoopbackAudioStarted` | `(sampleRate: number, channels: number): Promise<void>` |
| `receiveNativeLoopbackAudioData` | `(bridgeData: unknown, byteCount: number): void` |
| `getNativeLoopbackAudioTrack` | `(): MediaStreamTrack \| null` |
| `stopNativeLoopbackAudio` | `(): void` |

## IWebRtcToNative — JS to Native (29 methods)

Intercepted with `bridge: "webRtcToNative"`. JS notifies the C# host of state changes.

### Session Lifecycle

| Method | Signature |
|--------|-----------|
| `initialized` | `(): void` |
| `disconnected` | `(): void` |
| `failed` | `(error: WebRtcError): void` |

### Local Track Events

| Method | Signature |
|--------|-----------|
| `localAudioStarted` | `(): void` |
| `localAudioStopped` | `(): void` |
| `localAudioFailed` | `(): void` |
| `localVideoStarted` | `(): void` |
| `localVideoStopped` | `(): void` |
| `localVideoFailed` | `(): void` |
| `localScreenStarted` | `(): void` |
| `localScreenStopped` | `(): void` |
| `localScreenFailed` | `(): void` |
| `localScreenAudioFailed` | `(): void` |
| `localScreenAudioStopped` | `(): void` |

### Remote Track Events

| Method | Signature |
|--------|-----------|
| `remoteLiveMediaTrackStarted` | `(): void` |
| `remoteLiveMediaTrackStopped` | `(): void` |
| `remoteAudioTrackStarted` | `(userIds: UserGuid[]): void` |

### User State Notifications

| Method | Signature |
|--------|-----------|
| `localMuteWasSet` | `(isMuted: boolean): void` |
| `localDeafenWasSet` | `(isDeafened: boolean): void` |
| `setSpeaking` | `(isSpeaking: boolean, deviceId: DeviceGuid, userId: UserGuid): void` |
| `setHandRaised` | `(isHandRaised: boolean, deviceId: DeviceGuid, userId: UserGuid): void` |

### Moderation Outbound

| Method | Signature |
|--------|-----------|
| `setAdminMute` | `(deviceId: DeviceGuid, isMuted: boolean): void` |
| `setAdminDeafen` | `(deviceId: DeviceGuid, isDeafened: boolean): void` |
| `kickPeer` | `(userId: UserGuid): void` |

### Profile & UI

| Method | Signature |
|--------|-----------|
| `getUserProfile` | `(userId: UserGuid): Promise<IUserResponse>` |
| `getUserProfiles` | `(userIds: UserGuid[]): Promise<IUserResponse[]>` |
| `viewProfileMenu` | `(userId: UserGuid, coordinates: Coordinates): void` |
| `viewContextMenu` | `(userId: UserGuid, coordinates: Coordinates, tileType?: TileType, volume?: number): void` |

### Logging

| Method | Signature |
|--------|-----------|
| `log` | `(message: string): void` |

## Common Patch Patterns

### Observe (log without modifying)

```typescript
patches: [{
  bridge: "nativeToWebRtc",
  method: "initialize",
  before(args) {
    const state = args[0] as InitializeDesktopWebRtcPayload;
    console.log(`[Uprooted:my-plugin] Joined channel ${state.channelId}`);
    // Don't return false — let it through
  },
}]
```

### Cancel (block a call)

```typescript
patches: [{
  bridge: "nativeToWebRtc",
  method: "kick",
  before(args) {
    console.log("[Uprooted:my-plugin] Blocked kick for", args[0]);
    return false;  // Cancel the original call
  },
}]
```

### Replace (substitute implementation)

```typescript
patches: [{
  bridge: "webRtcToNative",
  method: "log",
  replace(message: string) {
    // Prefix all native logs
    (window as any).__webRtcToNative_original?.log?.(`[Modified] ${message}`);
  },
}]
```

### Multi-Method Monitor

```typescript
const methods = ["localAudioStarted", "localAudioStopped", "localVideoStarted", "localVideoStopped"] as const;

patches: methods.map(method => ({
  bridge: "webRtcToNative" as const,
  method,
  before(args: unknown[]) {
    console.log(`[Uprooted:my-plugin] ${method} called`);
  },
}))
```

### Modify Arguments

```typescript
patches: [{
  bridge: "nativeToWebRtc",
  method: "setNoiseGateThreshold",
  before(args) {
    // Force minimum threshold
    if ((args[0] as number) < 0.1) {
      args[0] = 0.1;
    }
    // Don't return false — call proceeds with modified args
  },
}]
```

## Bridge Availability

- `window.__nativeToWebRtc` — only available during voice sessions (proxied by Uprooted)
- `window.__webRtcToNative` — only available during voice sessions (proxied by Uprooted)
- Proxy installation happens at preload via `Object.defineProperty` traps for deferred assignment
- Patches are active only while the plugin is started
