import {TRACK_TYPE, TrackType} from '@rootplatform/apiclient-webrtc-store';
import {LocalTrackEvent, TrackEventType} from '../types';
import {ExtraWebRtcToNativeArgument} from './bridgeMiddleware';
import {setScreenDismissed} from './slice';
import {DesktopWebRtcAppDispatch} from './store';

/**
 * Tell the native client that local tracks have stopped, started, or failed
 * @param event
 * @param shouldIgnoreScreen
 * @param rawTrackTypes
 * @param dispatch
 * @param extra
 * @param includeScreenAudio
 */
export const alertLocalTrackEvent = (
  event: TrackEventType,
  shouldIgnoreScreen: boolean,
  rawTrackTypes: Array<TrackType>,
  dispatch: DesktopWebRtcAppDispatch,
  extra: ExtraWebRtcToNativeArgument,
  includeScreenAudio: boolean
) => {
  const bridge = extra.streamService?.webRtcToNative || window?.__webRtcToNative;
  let trackTypes = [...rawTrackTypes];
  if (!trackTypes.length) return;

  if (shouldIgnoreScreen && trackTypes.includes(TRACK_TYPE.screen)) {
    trackTypes = trackTypes.filter(trackType => trackType !== TRACK_TYPE.screen && trackType !== TRACK_TYPE.screenAudio);
  }

  trackTypes?.forEach(trackType => {
    const bridgeFunction = 'local' + trackType.charAt(0).toUpperCase() + trackType.slice(1) + event;
    if (bridge && bridgeFunction in bridge && typeof bridge[bridgeFunction as LocalTrackEvent] === 'function') {
      bridge[bridgeFunction as LocalTrackEvent]();
    }
  });

  if (includeScreenAudio && event !== 'Started' &&
    trackTypes.includes(TRACK_TYPE.screen) && !trackTypes.includes(TRACK_TYPE.screenAudio)
  ) {
    bridge?.[`localScreenAudio${event}`]?.();
  }

  if (shouldIgnoreScreen) {
    dispatch(setScreenDismissed({isDismissed: false}));
  }
};