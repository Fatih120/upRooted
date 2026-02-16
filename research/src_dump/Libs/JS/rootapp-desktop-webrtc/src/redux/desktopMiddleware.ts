import {createListenerMiddleware, ListenerMiddlewareInstance} from '@reduxjs/toolkit';
import {UserGuid} from '@rootplatform/apiclient-internal';
import {
  errorPayload,
  failure,
  localTracksStartAborted,
  log,
  selectBaseVolumeByTileId,
  selectCurrentAudioStreamId,
  selectCurrentScreenStreamId,
  selectLoadingLocalTrackTypes,
  selectPreferredMediaInputIdByType,
  selectTileById,
  selectUserIdByTileId,
  setPushToTalkMode,
  toggleFocusedTile,
  TRACK_TYPE,
  WebRtcAppDispatch,
  WebRtcErrorType,
  WebRtcRootState
} from '@rootplatform/apiclient-webrtc-store';
import {VIDEO_CONSTRAINTS} from '../constants';
import {EffectsSDKService, ElementVolumeBooster, StreamService} from '../services';
import {ScreenQualityMode} from '../types';
import {ExtraWebRtcToNativeArgument} from './bridgeMiddleware.ts';
import {
  clickFocus,
  clickUsername,
  customizeVolumeBooster,
  rightClick,
  setDenoisePower,
  setNoiseGateThreshold,
  setScreenContentHint,
  setScreenDismissed,
  setScreenQualityMode
} from './slice.ts';
import {dispatch, streamService} from './store.ts';

export const addPushToTalkModeMiddleware = (
  listenerMiddleware: ListenerMiddlewareInstance<
    WebRtcRootState,
    WebRtcAppDispatch,
    ExtraWebRtcToNativeArgument
  >
) => {
  listenerMiddleware.startListening({
    actionCreator: setPushToTalkMode,
    effect: async ({payload}) => {
      streamService.isPushToTalkMode = payload.isPushToTalkMode;
    }
  });
};

/**
 * Tell the native client to show a user's profile menu when clicking on the user's name in the stream overlay
 * @param listenerMiddleware
 */
export const addViewProfileMenuMiddleware = (
  listenerMiddleware: ListenerMiddlewareInstance<
    WebRtcRootState,
    WebRtcAppDispatch,
    ExtraWebRtcToNativeArgument>) => {
  listenerMiddleware.startListening({
    actionCreator: clickUsername,
    effect: async ({payload}, {extra, getState}) => {
      const state = getState();
      const userId = selectUserIdByTileId(state.webRtc, payload.tileId) as UserGuid;
      extra.streamService.webRtcToNative.viewProfileMenu(userId, payload.coordinates);
    }
  });
};

export const addViewContextMenuMiddleware = (
  listenerMiddleware: ListenerMiddlewareInstance<
    WebRtcRootState,
    WebRtcAppDispatch,
    ExtraWebRtcToNativeArgument
  >
) => {
  listenerMiddleware.startListening({
    actionCreator: rightClick,
    effect: async ({payload}, {extra, getState}) => {
      const state = getState();
      const volume = selectBaseVolumeByTileId(state.webRtc, payload.tileId);
      const userId = selectUserIdByTileId(state.webRtc, payload.tileId) as UserGuid;
      const tileType = selectTileById(state.webRtc.tiles, payload.tileId)?.tileType;

      extra.streamService.webRtcToNative.viewContextMenu(userId, payload.coordinates, tileType, volume);
    }
  });
};

export const addClickFocusMiddleware = (
  listenerMiddleware: ListenerMiddlewareInstance<
    WebRtcRootState,
    WebRtcAppDispatch,
    ExtraWebRtcToNativeArgument
  >
) => {
  listenerMiddleware.startListening({
    actionCreator: clickFocus,
    effect: async ({payload}, {dispatch}) => {
      dispatch(toggleFocusedTile({tileId: payload.tileId}));
    }
  });
};

export const addSetVolumeBoosterSettingsMiddleware = (
  listenerMiddleware: ListenerMiddlewareInstance<
    WebRtcRootState,
    WebRtcAppDispatch,
    ExtraWebRtcToNativeArgument
  >
) => {
  listenerMiddleware.startListening({
    actionCreator: customizeVolumeBooster,
    effect: async ({payload}) => {
      ElementVolumeBooster.updateSettings(payload);
    }
  });
};

export const addSetNoiseGateThresholdMiddleware = (
  listenerMiddleware: ListenerMiddlewareInstance<
    WebRtcRootState,
    WebRtcAppDispatch,
    ExtraWebRtcToNativeArgument
  >
) => {
  listenerMiddleware.startListening({
    actionCreator: setNoiseGateThreshold,
    effect: async ({payload}, {extra, getState}) => {
      const {threshold} = payload;
      const state = getState().webRtc;
      const currentAudioStreamId = selectCurrentAudioStreamId(state);
      extra.streamService?.streamThresholdSetterMap?.get(currentAudioStreamId)?.(threshold);
    }
  });
};

export const addSetDenoisePowerMiddleware = (
  listenerMiddleware: ListenerMiddlewareInstance<
    WebRtcRootState,
    WebRtcAppDispatch,
    ExtraWebRtcToNativeArgument
  >
) => {
  listenerMiddleware.startListening({
    actionCreator: setDenoisePower,
    effect: async ({payload}, {dispatch}) => {
      const {power} = payload;
      if (power < 0 || power > 1) {
        dispatch(failure(errorPayload(WebRtcErrorType.AudioEffectFailed, new Error('Denoise power must be between 0 and 1'), {power})));
        return;
      }
      try {
        EffectsSDKService.audioEffectsSDK.setDenoisePower(power);
      } catch (error) {
        dispatch(failure(errorPayload(WebRtcErrorType.AudioEffectFailed, error, {
          message: 'failed to set denoise power',
          power
        })));
      }
    }
  });
};

export const addSetScreenQualityModeMiddleware = (
  listenerMiddleware: ListenerMiddlewareInstance<
    WebRtcRootState,
    WebRtcAppDispatch,
    ExtraWebRtcToNativeArgument
  >
) => {
  listenerMiddleware.startListening({
    actionCreator: setScreenQualityMode,
    effect: async ({payload}, {extra, getState}) => {
      extra.streamService.screenQualityMode = payload.screenQualityMode;
      const streamId = selectCurrentScreenStreamId(getState().webRtc);
      const stream = extra.streamService.getStream(streamId);
      const {screenQualityMode} = payload;

      if (!stream || screenQualityMode == null) return;

      const mediaTrack = stream.getVideoTracks()?.[0];

      if (!mediaTrack) return;

      try {
        if (extra.streamService.utils.preferredScreenContentHint) {
          mediaTrack.contentHint = extra.streamService.utils.preferredScreenContentHint;
        } else {
          mediaTrack.contentHint = screenQualityMode === ScreenQualityMode.PRESENTATION ? 'detail' : 'motion';
        }
        log(`[setScreenQualityMode] Set screen contentHint to ${mediaTrack.contentHint}`);

        const defaultConstraints = screenQualityMode === ScreenQualityMode.PRESENTATION ? VIDEO_CONSTRAINTS.screenText : VIDEO_CONSTRAINTS.screen;
        const preferredScreenId = selectPreferredMediaInputIdByType(getState().webRtc, TRACK_TYPE.screen);
        const defaultMedia = {...preferredScreenId ? {deviceId: {exact: preferredScreenId}} : {}};
        const preferredConstraints = extra.streamService.screenConstraints || {};
        const constraints = {...defaultConstraints, ...preferredConstraints, ...defaultMedia};

        await mediaTrack.applyConstraints(constraints);

        log('[setScreenQualityMode] Applied screen constraints', {...constraints}, 'and track constraints are now', mediaTrack.getConstraints());
      } catch (error) {
        dispatch(failure(errorPayload(WebRtcErrorType.CodecSelectionFailed, error, {
          message: 'failed to set screen quality mode',
          screenQualityMode
        })));
      }
    }
  });
};

export const addSetScreenContentHintMiddleware = (
  listenerMiddleware: ListenerMiddlewareInstance<
    WebRtcRootState,
    WebRtcAppDispatch,
    ExtraWebRtcToNativeArgument
  >
) => {
  listenerMiddleware.startListening({
    actionCreator: setScreenContentHint,
    effect: async ({payload}, {extra, getState}) => {
      if (payload.contentHint == null) return;

      extra.streamService.utils.preferredScreenContentHint = payload.contentHint;

      const state = getState().webRtc;
      const streamId = selectCurrentScreenStreamId(state);
      const stream = extra.streamService.getStream(streamId);
      const mediaTrack = stream?.getVideoTracks()?.[0];

      if (!mediaTrack) return;

      mediaTrack.contentHint = payload.contentHint;
      log(`[setScreenContentHint] Set screen contentHint to ${mediaTrack.contentHint}`);

    }
  });
};

export const addSetScreenDismissedMiddleware = (
  listenerMiddleware: ListenerMiddlewareInstance<
    WebRtcRootState,
    WebRtcAppDispatch,
    ExtraWebRtcToNativeArgument
  >
) => {
  listenerMiddleware.startListening({
    actionCreator: setScreenDismissed,
    effect: async ({payload}, {getState, dispatch}) => {
      if (payload.isDismissed) {
        const loadingTrackTypes = selectLoadingLocalTrackTypes(getState().webRtc);
        if (loadingTrackTypes.includes(TRACK_TYPE.screen)) {
          dispatch(localTracksStartAborted({trackTypes: [TRACK_TYPE.screen, ...loadingTrackTypes.includes(TRACK_TYPE.screenAudio) ? [TRACK_TYPE.screenAudio] : []]}));
        }
      }
    }
  });
};

export const mediaMiddleware = (
  middleware: ListenerMiddlewareInstance<
    WebRtcRootState,
    WebRtcAppDispatch,
    ExtraWebRtcToNativeArgument
  >
) => {
  addPushToTalkModeMiddleware(middleware);
  addSetNoiseGateThresholdMiddleware(middleware);
  addSetDenoisePowerMiddleware(middleware);
  addSetScreenQualityModeMiddleware(middleware);
  addSetScreenContentHintMiddleware(middleware);
  addSetScreenDismissedMiddleware(middleware);
  addSetVolumeBoosterSettingsMiddleware(middleware);
};

export const tileMiddleware = (
  middleware: ListenerMiddlewareInstance<
    WebRtcRootState,
    WebRtcAppDispatch,
    ExtraWebRtcToNativeArgument
  >
) => {
  addViewProfileMenuMiddleware(middleware);
  addViewContextMenuMiddleware(middleware);
  addClickFocusMiddleware(middleware);
};

export const desktopMiddleware: (
  streamService: StreamService
) => ListenerMiddlewareInstance<
  WebRtcRootState,
  WebRtcAppDispatch,
  ExtraWebRtcToNativeArgument
> = (streamService: StreamService) => {
  const middleware = createListenerMiddleware<
    WebRtcRootState,
    WebRtcAppDispatch,
    ExtraWebRtcToNativeArgument
  >({
    extra: {
      streamService
    }
  });

  mediaMiddleware(middleware);
  tileMiddleware(middleware);

  return middleware;
};
