import {createListenerMiddleware, PayloadAction} from '@reduxjs/toolkit';
import {DeviceGuid, UserGuid} from '@rootplatform/apiclient-internal';
import {
  ClientErrorAction,
  closeRemoteTransports,
  createRemoteTransports,
  directMessageRing,
  disconnected,
  errorPayload,
  failure,
  initialize,
  initialized,
  kickPeer,
  localStreamsStartFailed,
  localTransportsClosed,
  localTransportsCreated,
  MemberDevice,
  peerAttachedWithoutTracks,
  remoteStreamStarted,
  remoteTransportsCreated,
  selectAllAudioStreamIdsButForDeviceId,
  selectAuditoryStreamIdByTileId,
  selectConnectionStatus,
  selectCurrentAudioStreamId,
  selectCurrentDevice,
  selectCurrentDeviceId,
  selectCurrentUserId,
  selectDeviceById,
  selectDeviceByUserId,
  selectIsPushToTalkMode,
  selectTrackByStreamId,
  selectUserIdByDeviceId,
  selectUserIdByTileId,
  selectUserIdByTrackId,
  setAdminDeafen,
  setAdminMute,
  setDeafen,
  setHandRaised,
  setMute,
  setPushToTalkMode,
  setSpeaking,
  setTileVolume,
  startLocalTracks,
  streamEnabled,
  TRACK_TYPE,
  TrackType,
  Utils,
  WebRtcErrorType
} from '@rootplatform/apiclient-webrtc-store';
import {uniq} from 'lodash';
import {EffectsSDKService, StreamService} from '../services';
import {selectIsScreenPickerDismissed} from './selectors';
import {applyProfileUpdates, requestProfileUpdates, setScreenDismissed, setScreenQualityMode} from './slice';
import {alertLocalTrackEvent} from './stateUtils';
import {BridgeMiddleware, DesktopWebRtcAppDispatch, dispatch, RootState} from './store';

export type ExtraWebRtcToNativeArgument = {
  streamService: StreamService;
};

export const addInitializeMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: initialize,
    effect: async ({payload}, {dispatch, extra, getState}) => {
      extra.streamService.getState = getState;

      dispatch(requestProfileUpdates({userIds: [...payload.initParams.activeUserIds || [] as Array<UserGuid>, payload.initParams.currentUserId]}));
      try {
        await EffectsSDKService.init(dispatch, payload.initParams.currentDeviceId);
      } catch (error) {
        dispatch(failure(errorPayload(WebRtcErrorType.AudioEffectFailed, error, {currentDeviceId: payload.initParams.currentDeviceId})));
      }

      if (payload.initParams.isPushToTalkMode) {
        dispatch(setPushToTalkMode({isPushToTalkMode: true}));
      }
    }
  });
};
/**
 * Tell the native client that the webRTC client has been initialized
 * @param listenerMiddleware
 */
export const addInitializedMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: initialized,
    effect: async (
      {payload}: PayloadAction<{ sessionMembers: MemberDevice[] }>,
      {extra, getOriginalState, getState}
    ) => {
      const initialUserIds = new Set([...getOriginalState().webRtc.callEnvironment.initialUserIds || []]);
      const currentUserId = selectCurrentUserId(getState().webRtc);
      const newUserIds = payload.sessionMembers.map((member) => member.userId).filter(userId => !initialUserIds.has(userId) && userId !== currentUserId);
      if (newUserIds.length) {
        dispatch(requestProfileUpdates({userIds: newUserIds}));
      }
      extra.streamService.webRtcToNative.initialized();
    }
  });
};

export const addDirectMessageRingMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: directMessageRing,
    effect: async ({payload}) => {
      dispatch(requestProfileUpdates({userIds: [payload.userId]}));
    }
  });
};

/**
 * Tell the native client that the webRTC client has been disconnected
 * @param listenerMiddleware
 */
export const addDisconnectedMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: disconnected,
    effect: async (_, {extra}) => {
      const bridge = extra.streamService?.webRtcToNative || window.__webRtcToNative;
      bridge?.disconnected();
    }
  });
};

export const addStartLocalTracksMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: startLocalTracks,
    effect: async ({payload}, {getState, dispatch}) => {
      if (payload.trackTypes.includes(TRACK_TYPE.screen) && getState().desktopWebRtc.isScreenDismissed) {
        dispatch(setScreenDismissed({isDismissed: false}));
      }
    }
  });
};

export const addPeerAttachedWithoutTracksMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: peerAttachedWithoutTracks,
    effect: async ({payload}, {extra, getState}) => {
      const isInitialized = selectConnectionStatus(getState().webRtc) === 'Open';
      const {userId} = payload;
      const userDevice = selectDeviceByUserId(getState().webRtc, userId);
      if (isInitialized && userId && !userDevice?.screenTrackId && !userDevice?.audioTrackId && !userDevice?.videoTrackId && !userDevice?.screenAudioTrackId) {
        const bridge = extra.streamService?.webRtcToNative || window.__webRtcToNative;
        bridge?.remoteAudioTrackStarted([userId]);
      }
    }
  });
};

export const addLocalTransportsCreatedMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: localTransportsCreated,
    effect: async ({payload}, {extra, dispatch}) => {
      const trackTypes = payload.tracks?.map(Utils.getTrackType).filter(Boolean) || [];

      if (trackTypes.includes(TRACK_TYPE.screen)) {
        dispatch(setScreenQualityMode({screenQualityMode: extra.streamService.screenQualityMode}));
      }

      alertLocalTrackEvent('Started', false, trackTypes as Array<TrackType>, dispatch, extra, false);
    }
  });
};

export const addLocalTransportsClosedMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: localTransportsClosed,
    effect: async ({payload}, {extra, dispatch}) => {
      const trackTypes = payload.tracks?.map(Utils.getTrackType).filter(Boolean) || [];
      alertLocalTrackEvent('Stopped', false, trackTypes as Array<TrackType>, dispatch, extra, false);
    }
  });
};

export const addLocalStreamsStartFailedMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: localStreamsStartFailed,
    effect: async ({payload}, {extra, getState, dispatch}) => {
      const isScreenAudioFailed = !!payload.error.rootError?.message?.toLowerCase().includes('audio');
      const shouldIgnoreScreenFailed = selectIsScreenPickerDismissed(getState());

      alertLocalTrackEvent('Failed', shouldIgnoreScreenFailed, payload.trackTypes, dispatch, extra, isScreenAudioFailed);
    }
  });
};

export const addCreateRemoteTransportsMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: createRemoteTransports,
    effect: async ({payload}, {extra, getState}) => {
      const isInitialized = selectConnectionStatus(getState().webRtc) === 'Open';
      if (isInitialized) {
        if (payload.devices.some(device => {
          const hasScreen = selectDeviceById(getState().webRtc, device.deviceId)?.screenTrackId;
          return !hasScreen && device.screenTrackId;
        })) {
          extra.streamService.webRtcToNative.remoteLiveMediaTrackStarted();
        }
      }
    }
  });
};

export const addRemoteTransportsCreatedMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: remoteTransportsCreated,
    effect: async ({payload}, {extra, getState}) => {
      const isInitialized = selectConnectionStatus(getState().webRtc) === 'Open';
      if (isInitialized) {
        if (payload.tracks.some(track => track?.streamId && track?.isVideo)) {
          extra.streamService.webRtcToNative.remoteLiveMediaTrackStarted();
        }
        if (payload.tracks.some(track => track?.streamId && track?.isAudio)) {
          const userIds = uniq([...payload.tracks.map(track => selectUserIdByTrackId(getState().webRtc, track?.trackId)) || []]);
          if (userIds.length > 0) {
            extra.streamService.webRtcToNative.remoteAudioTrackStarted(userIds);
          }
        }
      }
    }
  });
};
export const addRemoteStreamStartedMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: remoteStreamStarted,
    effect: async ({payload}, {extra, getState}) => {
      const isInitialized = selectConnectionStatus(getState().webRtc) === 'Open';
      if (isInitialized) {
        const trackWithStreamId = selectTrackByStreamId(getState().webRtc, payload.streamIdentifier.streamId);
        if (trackWithStreamId && !trackWithStreamId?.isAudio) {
          extra.streamService.webRtcToNative.remoteLiveMediaTrackStarted();
        }
        if (trackWithStreamId && trackWithStreamId?.isAudio) {
          const userId = selectUserIdByTrackId(getState().webRtc, trackWithStreamId?.trackId);
          if (userId) {
            extra.streamService.webRtcToNative.remoteAudioTrackStarted([userId]);
          }
        }
      }
    }
  });
};

export const addCloseRemoteTransportsMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: closeRemoteTransports,
    effect: async ({payload}, {extra, getState}) => {
      const isInitialized = selectConnectionStatus(getState().webRtc) === 'Open';
      if (isInitialized) {
        const bridge = extra.streamService?.webRtcToNative || window.__webRtcToNative;
        if (payload.tracks.some(track => track?.isVideo || track?.isScreen)) {
          bridge?.remoteLiveMediaTrackStopped();
        }
      }
    }
  });
};

/**
 * Request profile updates from the native client
 * @param listenerMiddleware
 */
export const addRequestProfileUpdatesMiddleware = (
  listenerMiddleware: BridgeMiddleware) => {
  listenerMiddleware.startListening({
    actionCreator: requestProfileUpdates,
    effect: async ({payload}, {extra, dispatch}) => {
      if (payload.userIds?.length) {
        const bridge = extra.streamService?.webRtcToNative || window.__webRtcToNative;
        const profiles = await bridge?.getUserProfiles(payload.userIds);
        dispatch(applyProfileUpdates({users: profiles}));
      }
    }
  });
};

/**
 * Tell the native client that the current user is speaking
 * @param listenerMiddleware
 */
export const addSetSpeakingMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: setSpeaking,
    effect: async ({payload}, {extra, getState}) => {
      const deviceId = Utils.getDeviceIdsFromPayload(getState().webRtc, payload)?.[0] as DeviceGuid;
      if (deviceId) {
        const userId = selectUserIdByDeviceId(getState().webRtc, deviceId) as UserGuid;
        if (userId) {
          extra.streamService.webRtcToNative.setSpeaking(!!payload.isSpeaking, deviceId, userId);
        }
      }
    }
  });
};

/**
 * Tell the native client that the current user has raised their hand
 * @param listenerMiddleware
 */
export const addSetHandRaisedMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: setHandRaised,
    effect: async ({payload}, {extra, getState}) => {
      const deviceId = Utils.getDeviceIdsFromPayload(getState().webRtc, payload)?.[0] as DeviceGuid;
      if (deviceId) {
        const userId = selectUserIdByDeviceId(getState().webRtc, deviceId) as UserGuid;
        if (userId) {
          extra.streamService.webRtcToNative.setHandRaised(!!payload.isHandRaised, deviceId, userId);
        }
      }
    }
  });
};

/**
 * Inform the native client that an API call has failed
 * @param listenerMiddleware
 */
export const addFailureMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: failure,
    effect: async ({payload}, {extra, dispatch}) => {
      const bridge = extra.streamService?.webRtcToNative || window.__webRtcToNative;
      const payloadError = 'error' in payload ? payload.error : payload;
      if (!payloadError || payloadError.clientErrorAction === ClientErrorAction.None) {
        return;
      }
      if ([WebRtcErrorType.BrowserMediaPermissionDenied, WebRtcErrorType.ReplaceTracksFailed, WebRtcErrorType.GetLocalMediaFailed, WebRtcErrorType.BitrateLimitingFailed].includes(payloadError.errorType)) {
        const trackType = payloadError.unitFailures?.find(unit => unit && 'trackType' in unit)?.trackType;
        alertLocalTrackEvent('Failed', false, trackType ? [trackType] : [], dispatch, extra, false);
      }

      bridge?.failed(payloadError);
    }
  });
};

/**
 * Tell the native client that the current user has, as an admin, muted another user
 * @param listenerMiddleware
 */
export const addAdminMuteMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: setAdminMute,
    effect: async ({payload}, {extra, getState}) => {
      const deviceId = Utils.getDeviceIdsFromPayload(getState().webRtc, payload)[0];

      if (payload.originatedFromCurrentUser && deviceId) {
        extra.streamService.webRtcToNative.setAdminMute(
          deviceId,
          !!payload.isAdminMuted
        );
      }
    }
  });
};

/**
 * Tell the native client that the current user has, as an admin, deafened another user
 * @param listenerMiddleware
 */
export const addAdminDeafenMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: setAdminDeafen,
    effect: async ({payload}, {extra, getState}) => {
      const deviceId = Utils.getDeviceIdsFromPayload(getState().webRtc, payload)[0];

      if (payload.originatedFromCurrentUser && deviceId) {
        extra.streamService.webRtcToNative.setAdminDeafen(
          deviceId,
          !!payload.isAdminDeafened
        );
      }
    }
  });
};

export const addStreamEnabledMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: streamEnabled,
    effect: async ({payload}, {extra, getState}) => {
      const {isEnabled, reason} = payload;
      const bridge = extra.streamService?.webRtcToNative || window.__webRtcToNative;
      const deviceId = Utils.getDeviceIdsFromPayload(getState().webRtc, payload)[0];
      const currentDeviceId = selectCurrentDeviceId(getState().webRtc);

      if (deviceId && currentDeviceId) {
        if (deviceId === currentDeviceId) {
          const device = selectCurrentDevice(getState().webRtc);
          if (!device?.isPushToTalkMode && (reason === 'mute' || reason === 'adminMute')) {
            bridge?.localMuteWasSet?.(!isEnabled);
          }
        } else {
          const currentDevice = selectCurrentDevice(getState().webRtc);
          const isDeafened = currentDevice?.isDeafened || currentDevice?.isAdminDeafened;
          const otherStreams = selectAllAudioStreamIdsButForDeviceId(getState().webRtc, currentDeviceId);

          const hasSetAllOtherStreams = otherStreams.reduce(
            (acc: boolean, streamId: string) => acc && extra.streamService.getIsStreamEnabled(streamId) !== isDeafened, true
          );

          if (hasSetAllOtherStreams && reason === 'adminDeafen' || reason === 'deafen') {
            bridge?.localDeafenWasSet?.(!!isDeafened);
          }
        }
      }
    }
  });
};


/**
 * Tell the native client that the current user has, as an admin, kicked another user
 * @param listenerMiddleware
 */
export const addKickMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: kickPeer,
    effect: async ({payload}, {extra}) => {
      extra.streamService.webRtcToNative.kickPeer(payload.userId);
    }
  });
};

export const addSetDeafenMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: setDeafen,
    effect: async ({payload}, {extra, getState}) => {
      const currentDeviceId = selectCurrentDeviceId(getState().webRtc);
      const deviceId = Utils.getDeviceIdsFromPayload(getState().webRtc, payload)[0];
      const hasOtherStreams = !!selectAllAudioStreamIdsButForDeviceId(getState().webRtc, currentDeviceId).length;
      const bridge = extra.streamService?.webRtcToNative || window.__webRtcToNative;
      if (currentDeviceId && currentDeviceId === deviceId && !hasOtherStreams) {
        bridge?.localDeafenWasSet?.(!!payload.isDeafened);
      }
    }
  });
};

export const addSetMuteMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: setMute,
    effect: async ({payload}, {extra, getState}) => {
      const currentDeviceId = selectCurrentDeviceId(getState().webRtc);
      const deviceId = Utils.getDeviceIdsFromPayload(getState().webRtc, payload)[0];
      const hasAudio = selectCurrentAudioStreamId(getState().webRtc);
      const isPushToTalkMode = selectIsPushToTalkMode(getState().webRtc);
      const bridge = extra.streamService?.webRtcToNative || window.__webRtcToNative;
      if (currentDeviceId && currentDeviceId === deviceId && (!hasAudio || isPushToTalkMode)) {
        bridge?.localMuteWasSet?.(!!payload.isMuted);
      }
    }
  });
};

export const addSetTileVolumeMiddleware = (
  listenerMiddleware: BridgeMiddleware
) => {
  listenerMiddleware.startListening({
    actionCreator: setTileVolume,
    effect: async ({payload}, {extra, getState}) => {
      const {tileId, volume} = payload;
      if (volume == null) return;
      const userId = selectUserIdByTileId(getState().webRtc, tileId);
      if (userId && userId === selectCurrentUserId(getState().webRtc)) {
        const streamId = selectAuditoryStreamIdByTileId(getState().webRtc, tileId);
        if (streamId) {
          const setInputAudioVolume = extra.streamService.streamVolumeSetterMap?.get(streamId);
          setInputAudioVolume?.(volume);
        }
      }
    }
  });
};


export const webRtcToNativeMiddleware = (middleware: BridgeMiddleware) => {
  addInitializeMiddleware(middleware);
  addInitializedMiddleware(middleware);
  addDirectMessageRingMiddleware(middleware);
  addDisconnectedMiddleware(middleware);
  addStartLocalTracksMiddleware(middleware);
  addPeerAttachedWithoutTracksMiddleware(middleware);
  addCreateRemoteTransportsMiddleware(middleware);
  addRemoteTransportsCreatedMiddleware(middleware);
  addRemoteStreamStartedMiddleware(middleware);
  addCloseRemoteTransportsMiddleware(middleware);
  addRequestProfileUpdatesMiddleware(middleware);
  addSetSpeakingMiddleware(middleware);
  addSetHandRaisedMiddleware(middleware);
  addFailureMiddleware(middleware);
  addAdminMuteMiddleware(middleware);
  addAdminDeafenMiddleware(middleware);
  addStreamEnabledMiddleware(middleware);
  addKickMiddleware(middleware);
  addLocalTransportsCreatedMiddleware(middleware);
  addLocalStreamsStartFailedMiddleware(middleware);
  addLocalTransportsClosedMiddleware(middleware);
  addSetDeafenMiddleware(middleware);
  addSetMuteMiddleware(middleware);

};

export const nativeToWebRtcMiddleware = (middleware: BridgeMiddleware) => {
  addSetTileVolumeMiddleware(middleware);
};

export const bridgeMiddleware: (streamService: StreamService) => BridgeMiddleware = (streamService: StreamService) => {
  const middleware = createListenerMiddleware<
    RootState,
    DesktopWebRtcAppDispatch,
    ExtraWebRtcToNativeArgument
  >({
    extra: {
      streamService
    }
  });

  webRtcToNativeMiddleware(middleware);
  nativeToWebRtcMiddleware(middleware);

  return middleware;
};
