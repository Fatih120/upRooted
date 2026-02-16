import {DeviceGuid, IPacket, IUserResponse, UserGuid, WebRtcClient} from '@rootplatform/apiclient-internal';
import {
  Codec,
  DisplayMediaStreamConstraints,
  InitializeWebRtcPayload,
  Theme,
  TileType,
  TrackType,
  UserMediaStreamConstraints,
  WebRtcError,
  WebRtcPermission
} from '@rootplatform/apiclient-webrtc-store';
import {VolumeBoosterSettings} from './audioEffects';
import {NativeScreenAudioService} from '../services/nativeScreenAudioService';

/**
 * Global Window object to contain the two sides of the bridge and the WebRtcClient api client.
 */
declare global {
  interface Window {
    __mediaManager: IMediaManager,
    __webRtcToNative: IWebRtcToNative,
    __nativeToWebRtc: INativeToWebRtc,
    __rootApiBaseUrl: string, // this is the url we need to instantiate WebRtcClient
    __webRtcClient: WebRtcClient,
    __nativeScreenAudioService: NativeScreenAudioService | null,
  }
}

/**
 * A temporary interface for desktop to independently manage media devices
 */
export interface IMediaManager {
  /**
   * Gets the list of devices of the specified kind(s) as a JSON string.
   * If no kind is specified, all devices are returned
   * @param kind
   */
  getDevices: (kind?: MediaDeviceKind | MediaDeviceKind[]) => Promise<string>;
}

export enum ScreenQualityMode {
  UNSPECIFIED = 0,
  GAMING = 1,
  PRESENTATION = 2,
}

export type InitializeDesktopWebRtcPayload = InitializeWebRtcPayload & {
  initialScreenQualityMode?: ScreenQualityMode,
};

/**
 * These are triggered by the webRtc (react) client side to inform the native (c#) desktop client side of events and to request profile data
 * This interface is implemented only on the native side, and the webrtc client side calls these functions from the instance on the window object
 */
export interface IWebRtcToNative {
  initialized(): void, // Once the session has been initialized (connected to cloudflare and ready for packets) AND all initial remote and local tracks have been started

  remoteLiveMediaTrackStarted(): void, // video and screen AFTER connected, in the middle of a call (not during initialize, not during disconnect)

  remoteAudioTrackStarted(userIds: UserGuid[]): void, // audio only AFTER connected, in the middle of a call when someone has just joined (not during initialize, not during disconnect)

  remoteLiveMediaTrackStopped(): void, // video or screen stopped, in the middle of a call (not during initialize, not during disconnect)

  disconnected(): void, // Once the webrtc client is disconnected

  localMuteWasSet(isMuted: boolean): void,

  localDeafenWasSet(isDeafened: boolean): void,

  localAudioFailed(): void,

  localAudioStarted(): void,

  localVideoFailed(): void,

  localVideoStarted(): void,

  localScreenFailed(): void,

  localScreenStarted(): void,

  localScreenAudioFailed(): void,

  localAudioStopped(): void,

  localVideoStopped(): void,

  localScreenStopped(): void,

  localScreenAudioStopped(): void,

  getUserProfile(userId: UserGuid): Promise<IUserResponse>,

  getUserProfiles(userIds: UserGuid[]): Promise<IUserResponse[]>,

  setSpeaking(isSpeaking: boolean, deviceId: DeviceGuid, userId: UserGuid): void,

  setHandRaised(isHandRaised: boolean, deviceId: DeviceGuid, userId: UserGuid): void,

  failed(error: WebRtcError): void,

  setAdminMute(deviceId: DeviceGuid, isMuted: boolean): void, // us muting someone else

  setAdminDeafen(deviceId: DeviceGuid, isDeafened: boolean): void, // us deafening someone else

  kickPeer(userId: UserGuid): void,

  viewProfileMenu(userId: UserGuid, coordinates: { x: number, y: number }): void,

  viewContextMenu(
    userId: UserGuid,
    coordinates: { x: number, y: number },
    tileType?: TileType, // primary or screen
    volume?: number // volume will be between 0 and 2 inclusive
  ): void,

  log(message: string): void,
}

export type TrackEventType = 'Stopped' | 'Started' | 'Failed';
export type LocalTrackEvent = `local${Capitalize<TrackType>}${TrackEventType}` & keyof IWebRtcToNative;

/**
 * These are triggered by the native (c#) desktop client side to inform the webRtc (react) client side of changes
 */
export interface INativeToWebRtc {
  initialize: (state: InitializeDesktopWebRtcPayload) => void,
  disconnect: () => void,
  setIsVideoOn: (isVideo: boolean) => void,
  setIsScreenShareOn: (isScreenShare: boolean, withAudio?: boolean) => void,
  setIsAudioOn: (isAudio: boolean) => void,
  updateVideoDeviceId: (videoSourceId: string) => void,
  updateAudioInputDeviceId: (micSourceId: string) => void,
  updateAudioOutputDeviceId: (soundSourceId: string) => void,
  updateScreenShareDeviceId: (screenSourceId: string) => void,
  updateScreenAudioDeviceId: (screenAudioSourceId: string) => void,
  updateProfile: (user: IUserResponse) => void,
  updateMyPermission: (myUserPermission: WebRtcPermission) => void,
  setPushToTalkMode: (isPushToTalkMode: boolean) => void,
  setPushToTalk: (isPushingToTalk: boolean) => void,
  setMute: (isMuted: boolean) => void,
  setDeafen: (isDeafened: boolean) => void,
  setHandRaised: (isHandRaised: boolean) => void,
  setTheme: (theme: Theme) => void,
  setNoiseGateThreshold: (threshold: number) => void,
  setDenoisePower: (power: number) => void,
  setScreenQualityMode: (qualityMode: ScreenQualityMode) => void,
  toggleFullFocus: (isFullFocus: boolean) => void,
  setPreferredCodecs: (preferredCodecs: Array<Codec>) => void,
  setUserMediaConstraints: (constraints: UserMediaStreamConstraints) => void,
  setDisplayMediaConstraints: (constraints: DisplayMediaStreamConstraints) => void,
  setScreenContentHint: (contentHint: string) => void,
  setAdminMute: (userId: UserGuid, isAdminMuted: boolean) => void,
  setAdminDeafen: (userId: UserGuid, isAdminDeafened: boolean) => void,
  screenPickerDismissed: () => void,
  // volume must be between 0 and 2 inclusive for these next three
  setTileVolume: (userId: UserGuid, tileType: TileType, volume: number) => void,
  setOutputVolume: (volume: number) => void,
  setInputVolume: (volume: number) => void,
  customizeVolumeBooster: (settings: VolumeBoosterSettings) => void,
  kick: (userId: UserGuid) => void,
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  receiveRawPacket: (data: any) => void,
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  receiveRawPacketContainer: (data: any) => void,
  receivePacket: (packet: IPacket) => void,
  // Native screen audio (loopback capture from C#)
  nativeLoopbackAudioStarted: (sampleRate: number, channels: number) => Promise<void>,
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  receiveNativeLoopbackAudioData: (bridgeData: any, byteCount: number) => void,
  getNativeLoopbackAudioTrack: () => MediaStreamTrack | null,
  stopNativeLoopbackAudio: () => void,
}
