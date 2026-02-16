import {
  convertPacket,
  convertPacketContainer,
  IChannelWebRtcUserAttachPacket,
  IChannelWebRtcUserDeviceSetDataChannelPacket,
  IDirectMessageMemberAddedPacket,
  IDirectMessageMemberDeletedPacket,
  IDirectMessageRingDeclinedPacket,
  IDirectMessageWebRtcUserDeviceSetDataChannelPacket,
  IPacket,
  IUserResponse,
  IWebRtcUserDetachPacket,
  IWebRtcUserDeviceSetStatusPacket,
  IWebRtcUserDeviceSetTransportPacket,
  PacketContainer,
  PacketContainerBase,
  PacketType,
  PacketTypes,
  UserGuid
} from '@rootplatform/apiclient-internal';
import {
  Codec,
  DataChannelName,
  directMessageRing,
  directMessageRingDeclined,
  disconnect,
  DisplayMediaStreamConstraints,
  errorPayload,
  failure,
  getTileId,
  initialize,
  InitializeWebRtcPayload,
  kickPeer,
  LOG_LEVEL,
  MemberDevice,
  peerAttach,
  peerDetach,
  peerDeviceDataChannelUpdate,
  peerDeviceStatusUpdate,
  peerDeviceTransportUpdate,
  setAdminDeafen,
  setAdminMute,
  setDeafen,
  setDisplayMediaStreamConstraints,
  setGlobalVolume,
  setHandRaised,
  setMute,
  setPreferredCodecs,
  setPushingToTalk,
  setPushToTalkMode,
  setTheme,
  setTileVolume,
  setUserMediaStreamConstraints,
  startLocalTracks,
  stopTracks,
  Theme,
  TileType,
  TRACK_TYPE,
  TrackType,
  updateAudioOutput,
  updateMediaInput,
  updatePermissions,
  UserMediaStreamConstraints,
  WebRtcErrorType,
  WebRtcPermission
} from '@rootplatform/apiclient-webrtc-store';
import {inRange, omit} from 'lodash';
import {
  applyProfileUpdates,
  dispatch,
  requestProfileUpdates,
  setDenoisePower,
  setNoiseGateThreshold,
  setScreenContentHint,
  setScreenDismissed,
  setScreenQualityMode,
  customizeVolumeBooster,
  toggleFullFocus,
  webRtcDesktopClientStore
} from '../redux';
import {INativeToWebRtc, InitializeDesktopWebRtcPayload, ScreenQualityMode, VolumeBoosterSettings} from '../types';
import {NativeScreenAudioService} from './nativeScreenAudioService';

type PacketKeys = keyof PacketTypes;
type PacketMap = {
  [K in PacketKeys]?: (p: NonNullable<PacketTypes[K]>) => Promise<void> | void;
};

export class NativeToWebRtc implements INativeToWebRtc {
  private currentUserId: UserGuid | null = null;

  private packetMethodMap: PacketMap = {
    [PacketType.ChannelWebRtcUserAttach]: this.getPeerUsers,
    [PacketType.DirectMessageWebRtcUserAttach]: this.getPeerUsers,
    [PacketType.ChannelWebRtcUserDeviceSetTransport]: this.updatePeerDeviceTransport,
    [PacketType.DirectMessageWebRtcUserDeviceSetTransport]: this.updatePeerDeviceTransport,
    [PacketType.ChannelWebRtcUserDeviceSetStatus]: this.updateDeviceStatus,
    [PacketType.DirectMessageWebRtcUserDeviceSetStatus]: this.updateDeviceStatus,
    [PacketType.ChannelWebRtcUserDetach]: this.detachPeer,
    [PacketType.DirectMessageWebRtcUserDetach]: this.detachPeer,
    [PacketType.DirectMessageMemberAdded]: this.directMessageRing,
    [PacketType.DirectMessageMemberDeleted]: this.directMessageRingDecline,
    [PacketType.DirectMessageRingDeclined]: this.directMessageRingDecline,
    [PacketType.ChannelWebRtcUserDeviceSetDataChannel]: this.setDataChannel,
    [PacketType.DirectMessageWebRtcUserDeviceSetDataChannel]: this.setDataChannel
  };

  // region WebRtc Call Connection

  initialize(initCallerParams: InitializeDesktopWebRtcPayload): void {
    const initParams = JSON.parse(JSON.stringify(initCallerParams));
    const initialScreenQualityMode = initParams.initialScreenQualityMode;
    delete initParams.initialScreenQualityMode;
    if (initialScreenQualityMode != null) {
      dispatch(setScreenQualityMode({screenQualityMode: initialScreenQualityMode}));
    }
    dispatch(initialize({
      initParams: {
        ...initParams,
        initialTrackTypes: [TRACK_TYPE.audio]
      } as InitializeWebRtcPayload
    }));
    this.currentUserId = initParams.currentUserId;
  }

  disconnect() {
    if (dispatch) {
      dispatch(disconnect());
    } else {
      window.__webRtcToNative.disconnected();
    }
  }

  // endregion

  // region Current User Transport (Media stream starting/stopping)

  setIsVideoOn(isVideo: boolean) {
    this.startStopTrack(isVideo, TRACK_TYPE.video);
  }

  setIsScreenShareOn(isScreen: boolean, withAudio: boolean = false) {
    this.startStopTrack(isScreen, TRACK_TYPE.screen, withAudio);
  }

  setIsAudioOn(isAudio: boolean) {
    this.startStopTrack(isAudio, TRACK_TYPE.audio);
  }

  updateVideoDeviceId(videoSourceId: string) {
    dispatch(updateMediaInput({trackType: TRACK_TYPE.video, mediaId: videoSourceId}));
  }

  updateAudioInputDeviceId(audioSourceId: string) {
    dispatch(updateMediaInput({trackType: TRACK_TYPE.audio, mediaId: audioSourceId}));
  }

  updateAudioOutputDeviceId(soundSourceId: string) {
    dispatch(updateAudioOutput({mediaId: soundSourceId}));
  }

  updateScreenShareDeviceId(screenSourceId: string) {
    dispatch(updateMediaInput({trackType: TRACK_TYPE.screen, mediaId: screenSourceId}));
  }

  updateScreenAudioDeviceId(screenAudioSourceId: string) {
    dispatch(updateMediaInput({trackType: TRACK_TYPE.screenAudio, mediaId: screenAudioSourceId}));
  }

  // endregion

  // region Current User Updates

  updateProfile(user: IUserResponse) {
    dispatch(applyProfileUpdates({users: [user]}));
  }

  updateMyPermission(myUserPermission: WebRtcPermission) {
    dispatch(updatePermissions({permissions: myUserPermission}));
  }

  // endregion

  // region Current User Status Updates

  setPushToTalkMode(isPushToTalkMode: boolean) {
    dispatch(setPushToTalkMode({isPushToTalkMode}));
  }

  setPushToTalk(isPushingToTalk: boolean) {
    dispatch(setPushingToTalk({isPushingToTalk}));
  }

  setMute(isMuted: boolean) {
    dispatch(setMute({deviceId: getCurrDeviceId(), isMuted}));
  }

  setDeafen(isDeafened: boolean) {
    dispatch(setDeafen({deviceId: getCurrDeviceId(), isDeafened}));
  }

  setHandRaised(isHandRaised: boolean) {
    dispatch(setHandRaised({deviceId: getCurrDeviceId(), isHandRaised}));
  }

  setTheme(theme: Theme) {
    dispatch(setTheme({theme}));
  }

  setNoiseGateThreshold(threshold: number) {
    dispatch(setNoiseGateThreshold({threshold}));
  }

  setDenoisePower(power: number) {
    dispatch(setDenoisePower({power}));
  }

  setScreenQualityMode(screenQualityMode: ScreenQualityMode) {
    dispatch(setScreenQualityMode({screenQualityMode}));
  }

  toggleFullFocus(isFullFocus: boolean) {
    dispatch(toggleFullFocus({isFullFocus}));
  }

  setPreferredCodecs(preferredCodecs: Array<Codec>) {
    dispatch(setPreferredCodecs({preferredCodecs}));
  }

  setUserMediaConstraints(constraints: UserMediaStreamConstraints) {
    dispatch(setUserMediaStreamConstraints({constraints}));
  }

  setDisplayMediaConstraints(constraints: DisplayMediaStreamConstraints) {
    dispatch(setDisplayMediaStreamConstraints({constraints}));
  }

  setScreenContentHint(contentHint: string) {
    dispatch(setScreenContentHint({contentHint}));
  }

  setAdminMute(userId: UserGuid, isAdminMuted: boolean) {
    dispatch(setAdminMute({userId, isAdminMuted, originatedFromCurrentUser: true}));
  }

  setAdminDeafen(userId: UserGuid, isAdminDeafened: boolean) {
    dispatch(setAdminDeafen({userId, isAdminDeafened, originatedFromCurrentUser: true}));
  }

  screenPickerDismissed() {
    dispatch(setScreenDismissed({isDismissed: true}));
  }

  setInputVolume(volume: number) {
    if (this.currentUserId) {
      dispatch(setTileVolume({tileId: getTileId(this.currentUserId, TileType.PRIMARY), volume}));
    } else {
      dispatch(failure(errorPayload(WebRtcErrorType.SetVolumeFailed, new Error('Cannot set input volume because current userId is unknown'))));
    }
  }

  // endregion

  // region Actions on Other Users

  setTileVolume(userId: UserGuid, tileType: TileType, volume: number) {
    dispatch(setTileVolume({tileId: getTileId(userId, tileType), volume}));
  }

  setOutputVolume(volume: number) {
    dispatch(setGlobalVolume({volume}));
  }

  customizeVolumeBooster(settings: VolumeBoosterSettings) {
    dispatch(customizeVolumeBooster(settings));
  }

  kick(userId: UserGuid) {
    dispatch(kickPeer({userId}));
  }

  // endregion

  // region Packet Handlers

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  receiveRawPacket(bridgeData: any): void {
    const rawPacket = new Uint8Array(bridgeData.ToJsArrayBuffer());
    const convertedPacket = convertPacket(rawPacket);
    if (!convertedPacket) {
      dispatch(failure(errorPayload(WebRtcErrorType.BadPacket, null, convertedPacket)));
      return;
    }
    this.receivePacket(convertedPacket.packet);
  }

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  receiveRawPacketContainer(bridgeData: any): void {
    const rawPacket = new Uint8Array(bridgeData.ToJsArrayBuffer());
    const packetContainerBase = PacketContainerBase.fromBinary(rawPacket);
    const packetContainer = PacketContainer.toClient(packetContainerBase, false);
    const convertedPacket = convertPacketContainer(packetContainer);
    if (!convertedPacket) return;
    this.receivePacket(convertedPacket);
  }

  receivePacket(basePacket: IPacket | null): void {
    if (!basePacket?.packetType) return;

    const isRingPacket = inRange(basePacket.packetType, PacketType.DirectMessageBoundaryMinValue, PacketType.DirectMessageBoundaryMaxValue);

    if (!inRange(basePacket.packetType, PacketType.DirectMessageWebRtcBoundaryMinValue, PacketType.DirectMessageWebRtcBoundaryMaxValue) &&
      !inRange(basePacket.packetType, PacketType.ChannelWebRtcBoundaryMinValue, PacketType.ChannelWebRtcBoundaryMaxValue) &&
      !isRingPacket) return;

    if (isRingPacket || ('deviceId' in basePacket) && ('userId' in basePacket) && ('containerId' in basePacket)) {
      this.logPacket(basePacket, basePacket.packetType);
      this.forwardPacketToMethodHandler(
        this.packetMethodMap[basePacket.packetType as keyof PacketTypes],
        basePacket
      );
    } else {
      dispatch(failure(errorPayload(WebRtcErrorType.BadPacket, new Error('Packet must have deviceId, userId, and containerId'), {packet: basePacket})));
    }
  }

  /**
   * Forwards the packet to the appropriate method handler
   * The method handlers pay attention only to packets about other user actions, except for the status update which
   * is also listened to for the current user
   * @param packetHandler
   * @param packet
   * @private
   */
  private forwardPacketToMethodHandler<
    // eslint-disable-next-line @typescript-eslint/no-unsafe-function-type
    HandlerType extends Function | undefined,
    PacketKeyType extends PacketKeys,
    Params extends Parameters<NonNullable<PacketMap[PacketKeyType]>>
  >(packetHandler: HandlerType, ...packet: Params): void {
    packetHandler?.(...packet);
  };

  private getPeerUsers(packet: IChannelWebRtcUserAttachPacket) {
    if (packet.deviceId !== getCurrDeviceId()) {
      dispatch(requestProfileUpdates({userIds: [packet.userId]}));
      dispatch(peerAttach({device: packet as MemberDevice}));
    }
  }

  private updateDeviceStatus(packet: IWebRtcUserDeviceSetStatusPacket) {
    // This is the only packet that we also listen to when it is about the current user
    dispatch(peerDeviceStatusUpdate(omit(packet, 'packetType')));
  }

  private updatePeerDeviceTransport(packet: IWebRtcUserDeviceSetTransportPacket) {
    if (packet.deviceId !== getCurrDeviceId()) {
      dispatch(peerDeviceTransportUpdate(omit(packet, 'packetType')));
    }
  }

  private detachPeer(packet: IWebRtcUserDetachPacket) {
    if (packet.deviceId !== getCurrDeviceId()) {
      dispatch(stopTracks({deviceId: packet.deviceId, trackTypes: Object.values(TRACK_TYPE)}));
      dispatch(peerDetach({deviceId: packet.deviceId}));
    }
  }

  private directMessageRing(packet: IDirectMessageMemberAddedPacket) {
    packet.memberUserIds.forEach(userId => {
      dispatch(directMessageRing({userId}));
    });
  }

  private directMessageRingDecline(packet: IDirectMessageRingDeclinedPacket | IDirectMessageMemberDeletedPacket) {
    dispatch(directMessageRingDeclined({userId: packet.userId}));
  }

  private setDataChannel(packet: IDirectMessageWebRtcUserDeviceSetDataChannelPacket | IChannelWebRtcUserDeviceSetDataChannelPacket) {
    if (packet.deviceId === getCurrDeviceId()) return;
    dispatch(peerDeviceDataChannelUpdate({channelName: packet.dataChannelName as DataChannelName}));
  }

  // endregion

  // region Helpers

  private startStopTrack(isMedia: boolean, mediaType: TrackType, withScreenAudio?: boolean) {
    const trackTypes = [mediaType];
    if (withScreenAudio && mediaType === TRACK_TYPE.screen) {
      trackTypes.push(TRACK_TYPE.screenAudio);
    }
    if (isMedia) {
      dispatch(startLocalTracks({trackTypes}));
    } else {
      dispatch(stopTracks({deviceId: getCurrDeviceId(), trackTypes}));
    }
  }

  private logPacket(packet: IPacket, packetType: PacketType) {
    if (LOG_LEVEL.includes('packet')) {
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      console.groupCollapsed(`%c[PACKET:${packetType}]`, 'color: #A1869E', ` ${Object.keys(PacketType).find((key: string) => (PacketType as any)[key] as number === packet.packetType)}`);
      console.log(packet);
      console.groupEnd();
    }
  }

  // endregion

  // region Native Screen Audio (loopback capture from C#)

  /**
   * Called by C# when native WASAPI loopback capture starts.
   */
  async nativeLoopbackAudioStarted(sampleRate: number, channels: number) {
    if (!window.__nativeScreenAudioService) {
      window.__nativeScreenAudioService = new NativeScreenAudioService();
    }
    await window.__nativeScreenAudioService.start(sampleRate, channels);
  }

  /**
   * Called by C# with captured audio data.
   */
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  receiveNativeLoopbackAudioData(bridgeData: any, byteCount: number) {
    const service = window.__nativeScreenAudioService;
    if (!service?.isActive) {
      return;
    }

    const arrayBuffer = bridgeData.ToJsArrayBuffer();
    const float32Data = new Float32Array(arrayBuffer, 0, byteCount / 4);
    service.receiveAudioData(float32Data);
  }

  /**
   * Gets the MediaStreamTrack for native loopback audio, if available.
   */
  getNativeLoopbackAudioTrack(): MediaStreamTrack | null {
    return window.__nativeScreenAudioService?.mediaStreamTrack ?? null;
  }

  /**
   * Called by C# when native loopback capture is stopped without a screen share starting.
   * This happens when the user dismisses the screen picker or selects "no audio".
   */
  stopNativeLoopbackAudio() {
    window.__nativeScreenAudioService?.stop();
  }

  // endregion
}

window.__nativeScreenAudioService = null;
window.__nativeToWebRtc = new NativeToWebRtc();

const getCurrDeviceId = () => webRtcDesktopClientStore.getState().webRtc.currentMemberDeviceId;

