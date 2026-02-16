import {GrpcWebFetchTransport} from '@protobuf-ts/grpcweb-transport';
import {
  AssetClient,
  AuthToken,
  CommunityClient,
  CommunityGuid,
  DeviceGuid,
  IUserResponse,
  MessageContainerGuid,
  RootApiConnection,
  RootPacketManager,
  UserClient,
  UserGuid,
  WebRtcClient
} from '@rootplatform/apiclient-internal';
import {
  ClientErrorAction,
  errorPayload,
  InitializeWebRtcPayload, TileType,
  WebRtcError,
  WebRtcErrorType
} from '@rootplatform/apiclient-webrtc-store';
import {noop} from 'lodash';
import {NativeToWebRtc} from '../services';
import {IWebRtcToNative} from '../types';

export function isNative(): boolean {
  return navigator.userAgent.toLowerCase().indexOf('rootplatform') >= 0;
}

// NOTE, only .env variables starting with VITE_ are imported to here.

class MockNative implements IWebRtcToNative {
  communityClient: CommunityClient;
  userClient: UserClient;
  communityId = import.meta.env.VITE_COMMUNITY_ID as CommunityGuid;
  channelId = import.meta.env.VITE_CONTAINER_ID as MessageContainerGuid;
  deviceId = <DeviceGuid>import.meta.env.VITE_DEVICE_ID;
  userId = <UserGuid>import.meta.env.VITE_USER_ID;
  token = new AuthToken(import.meta.env.VITE_AUTH_TOKEN);
  baseUrl = import.meta.env.VITE_BASE_URL;
  assetClient: AssetClient;

  constructor(communityClient: CommunityClient, userClient: UserClient, assetClient: AssetClient) {
    this.communityClient = communityClient;
    this.userClient = userClient;
    this.assetClient = assetClient;
  }

  async start() {
    // Get new hubserver endpoint
    // Attach
    // Wire up package manager.

    const token = new AuthToken(import.meta.env.VITE_AUTH_TOKEN);
    this.userId = token.userId as UserGuid;

    const initState: InitializeWebRtcPayload = {
      theme: 'dark',
      callPlatform: 'desktop',
      currentDeviceId: this.deviceId,
      currentUserId: this.userId,
      communityId: this.communityId,
      containerId: this.channelId,
      permissions: {
        channelVoiceKick: true,
        channelVoiceTalk: true,
        channelVoiceDeafenOther: true,
        channelVoiceMuteOther: true,
        channelVideoStreamMedia: true
      },
      debugMode: true,
      initialTrackTypes: ['audio'],
      logging: 'info-state-packet'
    } as InitializeWebRtcPayload;
    // Get the packet flowing.
    await this.communityClient.attach({communityId: this.communityId});
    window.__nativeToWebRtc.initialize(initState);
  }

  initialized() {
    console.warn('[Mock bridge] Initialized');
  }

  remoteLiveMediaTrackStarted() {
    console.warn('[Mock bridge] Remote live media track started');
  }

  remoteAudioTrackStarted() {
    console.warn('[Mock bridge] Remote audio track started');
  }

  remoteLiveMediaTrackStopped() {
    console.warn('[Mock bridge] Remote live media track stopped');
  }

  disconnected() {
    console.warn('[Mock bridge] Disconnected');
  }

  localMuteWasSet(isMuted: boolean) {
    noop(isMuted);
  }

  localDeafenWasSet(isDeafened: boolean) {
    noop(isDeafened);
  }

  localAudioFailed() {
  }

  localAudioStarted() {
  }

  localAudioStopped() {
  }

  localVideoFailed() {
  }

  localVideoStarted() {
  }

  localVideoStopped() {
  }

  localScreenFailed() {
  }

  localScreenStarted() {
  }

  localScreenStopped() {
  }

  localScreenAudioFailed() {
  }

  localScreenAudioStopped() {
  }

  async getUserProfile(userId: UserGuid): Promise<IUserResponse> {
    const result = await this.getUserProfiles([userId]);
    return result[0];
  }

  async getUserProfiles(userIds: UserGuid[]): Promise<IUserResponse[]> {
    let result;
    try {
      result = await this.userClient.getExtendedUsersById({userIds: userIds});
    } catch (error) {
      console.warn('[Mock bridge] Failed to get users', errorPayload(WebRtcErrorType.UnknownApiError, error, {userIds}));
      return [];
    }
    try {
      const allUris = result.users.map(user => user.profilePictureAssetUri);

      const allAssets = await this.assetClient.get({uris: allUris as string[]});
      return result.users.map(user => {
        const uri = user.profilePictureAssetUri;
        if (uri) {
          const fullUrl = allAssets?.legacyAssets[uri]?.assetLinks?.[0]?.url;
          const assetUri = fullUrl?.slice(0, fullUrl?.lastIndexOf('/'));
          return {...user, profilePictureAssetUri: assetUri};

        }
        return user;
      });
    } catch (error) {
      console.warn('[Mock bridge] Failed to get profile picture assets', errorPayload(WebRtcErrorType.UnknownApiError, error, result));
      return result.users;
    }
  }

  setSpeaking() {
    return;
  }

  setHandRaised(isHandRaised: boolean) {
    console.warn(`[Mock bridge] Set Hand Raised ${isHandRaised}`);
  }

  failed(error: WebRtcError) {
    if (error.clientErrorAction === ClientErrorAction.ImmediateReset || error.clientErrorAction === ClientErrorAction.ResetWithDelay) {
      const resetCount = parseInt(localStorage.getItem('reset-count') || '0', 10);
      console.log('resetCount', resetCount);
      const lastError = localStorage.getItem('last-error');
      const currentError = String(error.errorType);
      if (currentError === lastError && resetCount > 1) {
        alert('Something terrible happened and reset did not fix it. Error: ' + JSON.stringify(error));
        setTimeout(() => {
          localStorage.setItem('reset-count', '1');
          localStorage.setItem('last-error', '');
        }, 20000);

      } else if (currentError === lastError) {
        localStorage.setItem('reset-count', String(resetCount + 1));
        window.location.reload();
      } else {
        localStorage.setItem('last-error', String(currentError));
        localStorage.setItem('reset-count', '1');
        window.location.reload();
      }
    } else if (error.clientErrorAction === ClientErrorAction.RequestUserDeviceFix) {
      console.warn('[Mock bridge] webrtc error - request user to check their device', error);
    }
  }

  setAdminMute(deviceId: DeviceGuid, isMuted: boolean) {
    console.warn(`[Mock bridge] Admin Mute${deviceId} => ${isMuted}`);
  }

  setAdminDeafen(deviceId: DeviceGuid, isDeafened: boolean) {
    console.warn(`[Mock bridge] Admin Deafen ${deviceId} => ${isDeafened}`);
  }

  kickPeer(userId: UserGuid) {
    console.warn(`[Mock bridge] Kicked ${userId}`);
  }

  viewProfileMenu(userId: UserGuid, coordinates: { x: number; y: number }) {
    console.warn(`[Mock bridge] View Profile Menu for user ${userId} at ${coordinates.x}, ${coordinates.y}`);
  }

  viewContextMenu(userId: UserGuid, coordinates: { x: number; y: number }, tileType: TileType, volume: number) {
    console.warn(`[Mock bridge] View Context Menu for user ${userId}'s ${tileType} tile at ${coordinates.x}, ${coordinates.y} with volume ${volume}`);
  }

  log(message: string) {
    noop(message);
  }
}

if (!isNative()) {
  console.warn('[Mock bridge] Not Native, starting bridge');
  // Disconnect on leave.
  addEventListener('beforeunload', () => {
    console.warn('[Mock bridge] beforeunload');
    RootPacketManager.disconnect();
  });
  window.__rootApiBaseUrl = import.meta.env.VITE_BASE_URL;
  // This will always overlay
  window.__nativeToWebRtc = new NativeToWebRtc();

  const transport = new GrpcWebFetchTransport({
    baseUrl: window.__rootApiBaseUrl,
    meta: {'Authorization': 'Bearer ' + import.meta.env.VITE_AUTH_TOKEN},
    format: 'binary'
  });

  window.__webRtcClient = new WebRtcClient(transport, false);
  const communityClient = new CommunityClient(transport, false);

  const userClient = new UserClient(transport, false);

  const assetClient = new AssetClient(transport, false);
  const mockNative = new MockNative(communityClient, userClient, assetClient);
  window.__webRtcToNative = mockNative;
  RootPacketManager.asObservable().subscribe((packet) => {
    window.__nativeToWebRtc.receivePacket(packet);
  });

  const apiConnection = new RootApiConnection(window.__rootApiBaseUrl, mockNative.token, '', false);
  try {
    RootPacketManager.connect(apiConnection);
    const communityId = import.meta.env.VITE_COMMUNITY_ID as CommunityGuid;
    await communityClient.attach({communityId});
  } catch (error) {
    console.warn(errorPayload(WebRtcErrorType.UnknownApiError, error, '[Mock Bridge] Failed to attach to the community client'));
    throw error;
  }
  mockNative.start().then(async () => {
    console.warn('[Mock Bridge] Started');
  });
}
