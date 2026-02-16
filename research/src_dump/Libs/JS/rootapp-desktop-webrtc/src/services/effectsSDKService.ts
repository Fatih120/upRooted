import {DeviceGuid} from '@rootplatform/apiclient-internal';
import {
  errorPayload,
  failure,
  localTracksStartAborted,
  startLocalTracks,
  stopTracks,
  TRACK_TYPE,
  WebRtcAppDispatch, WebRtcErrorType
} from '@rootplatform/apiclient-webrtc-store';
import {atsvb, ErrorObject} from 'audio-effects-sdk';
import {EFFECTS_SDK_CONFIG, EFFECTS_SDK_CUSTOMER_ID} from '../constants';
import {isNative, MOCK_EFFECTS_SDK_CONFIG} from '../mocks';

export class EffectsSDKService {
  static audioEffectsSDK: atsvb;
  static shouldUseAudioEffectsSDK = true;
  static errors: Array<ErrorObject> = [];

  /**
   * Initializes the audio EffectsSDK.
   * @param dispatch
   * @param currentMemberDeviceId
   */
  static async init(dispatch: WebRtcAppDispatch, currentMemberDeviceId: DeviceGuid) {
    EffectsSDKService.audioEffectsSDK = new atsvb(EFFECTS_SDK_CUSTOMER_ID);

    EffectsSDKService.audioEffectsSDK.onError((error: ErrorObject) => EffectsSDKService.handleError(error, dispatch, currentMemberDeviceId));

    EffectsSDKService.audioEffectsSDK.config(isNative() ? EFFECTS_SDK_CONFIG : MOCK_EFFECTS_SDK_CONFIG);

    await EffectsSDKService.audioEffectsSDK.preload();
  }

  /**
   * Handles errors from the audio effects SDK.
   * @see https://github.com/EffectsSDK/audio-effects-sdk-web/blob/main/docs/Technical-Details.md
   * @param error
   * @param dispatch
   * @param currentMemberDeviceId
   * @private
   */
  static handleError(error: ErrorObject, dispatch: WebRtcAppDispatch, currentMemberDeviceId: DeviceGuid) {
    if (error.type !== 'error' || !EffectsSDKService.shouldUseAudioEffectsSDK) return;

    if (error.code === 1001) {
      setTimeout(() => {
        if (EffectsSDKService.audioEffectsSDK && typeof EffectsSDKService.audioEffectsSDK.run === 'function') {
          EffectsSDKService.audioEffectsSDK.run();
        }
      }, 2000);
    } else if (error.code !== 1002) {
      EffectsSDKService.errors.push(error);
      const errorCount = EffectsSDKService.errors.filter(e => e.code === error.code).length;
      if (errorCount > 2) {
        EffectsSDKService.shouldUseAudioEffectsSDK = false;
        dispatch(failure(errorPayload(WebRtcErrorType.AudioEffectFailed, error, 'Audio Effects SDK failed. Using browser noise suppression instead.')));
        EffectsSDKService.errors = [];
      }
      if (dispatch) {
        if (!currentMemberDeviceId) {
          return;
        }
        // Cover the case where a track is just starting...
        dispatch(localTracksStartAborted({trackTypes: [TRACK_TYPE.audio]}));

        // Cover the case where we are already in the middle of streaming...
        dispatch(stopTracks({trackTypes: [TRACK_TYPE.audio], deviceId: currentMemberDeviceId}));

        // ...and then try again (it will give up and use browser noise suppression if we received this error three times)
        dispatch(startLocalTracks({trackTypes: [TRACK_TYPE.audio]}));
      }
    }
  };
}