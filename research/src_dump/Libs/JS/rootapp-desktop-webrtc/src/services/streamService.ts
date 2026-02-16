import {GrpcWebFetchTransport} from '@protobuf-ts/grpcweb-transport';
import {WebRtcClient} from '@rootplatform/apiclient-internal';
import {
  BaseStreamService,
  errorPayload,
  failure,
  InitializeWebRtcPayload,
  log,
  selectDefaultPrimaryOutputVolume,
  WebRtcErrorType,
  WebRtcRootState
} from '@rootplatform/apiclient-webrtc-store';
import {AUDIO_CONSTRAINTS, VIDEO_CONSTRAINTS} from '../constants';
import {IWebRtcToNative, ScreenQualityMode} from '../types';
import {AudioNodeManager} from './audioNodeManager';
import {EffectsSDKService} from './effectsSDKService';

export class StreamService extends BaseStreamService {
  /** Bridge with the native desktop client */
  webRtcToNative: IWebRtcToNative = window.__webRtcToNative;

  /** Whether push to talk mode is enabled */
  _isPushToTalkMode = false;

  /** Remove after testing */
  statsPing: number | undefined;

  streamThresholdSetterMap: Map<string, (threshold: number) => void> = new Map();

  streamVolumeSetterMap: Map<string, (volume: number) => void> = new Map();

  getState!: () => WebRtcRootState;

  _screenQualityMode: ScreenQualityMode = ScreenQualityMode.GAMING;

  get isPushToTalkMode() {
    return this._isPushToTalkMode;
  }

  set isPushToTalkMode(value: boolean) {
    this._isPushToTalkMode = value;
  }

  get screenQualityMode() {
    return this._screenQualityMode;
  }

  set screenQualityMode(value: ScreenQualityMode) {
    log('Preferred screen quality mode updated to', value === ScreenQualityMode.PRESENTATION ? 'presentation' : value === ScreenQualityMode.GAMING ? 'gaming' : undefined);
    this._screenQualityMode = value;
  }

  constructor() {
    super();
  }

  /**
   * Initializes the WebRtcClient service and sets up the bridge with the native desktop client.
   * @param initParams - Initialization parameters for the WebRtcClient.
   * @returns The initialized WebRtcClient.
   */
  initWebRtcClient(initParams: InitializeWebRtcPayload) {
    if (!window.__webRtcClient) {
      const transport = new GrpcWebFetchTransport({
        baseUrl: initParams.webApiBaseUrl as string,
        format: 'binary'
      });

      window.__webRtcClient = new WebRtcClient(transport, false);
    }
    if (!window.__webRtcClient) {
      throw new Error('WebRtcClient is not instantiated');
    }
    if (!window.__webRtcToNative) {
      throw new Error('WebRtcToNative bridge is not instantiated');
    }
    this.webRtcToNative = window.__webRtcToNative;

    return window.__webRtcClient;
  }

  async getAudioInput(defaultMedia: MediaTrackConstraints): Promise<MediaStream> {
    return await this.getAudioInputStreams(defaultMedia);
  }

  async getVideoInput(defaultMedia: MediaTrackConstraints): Promise<MediaStream> {
    log('Calling getUserMedia with', {
      video: {...VIDEO_CONSTRAINTS.hd, ...defaultMedia, ...this.videoConstraints || {}}
    });
    return await navigator.mediaDevices.getUserMedia({
      video: {...VIDEO_CONSTRAINTS.hd, ...defaultMedia, ...this.videoConstraints || {}}
    });
  }

  async getScreenInput(defaultMedia: MediaTrackConstraints, withAudio?: boolean): Promise<MediaStream> {
    const defaultConstraints = this.screenQualityMode === ScreenQualityMode.PRESENTATION ? VIDEO_CONSTRAINTS.screenText : VIDEO_CONSTRAINTS.screen;

    // Check if native loopback audio is available before calling getDisplayMedia
    // If not available but audio requested, we'll need to request browser audio capture
    const nativeLoopbackAvailable = window.__nativeScreenAudioService?.isActive === true;

    // Only request browser audio if native loopback is not available
    const needBrowserAudio = withAudio && !nativeLoopbackAvailable;

    const allSettings = {
      video: {...defaultConstraints, ...defaultMedia, ...this.screenConstraints || {}},
      ...(needBrowserAudio ? {audio: this.screenAudioConstraints as MediaTrackConstraints || true} : {}),
      ...this.displayConstraints || {}
    };

    log('[getScreenInput] withAudio:', withAudio, 'nativeLoopbackAvailable:', nativeLoopbackAvailable, 'needBrowserAudio:', needBrowserAudio);

    const mediaStream = await navigator.mediaDevices.getDisplayMedia(allSettings);

    const track = mediaStream.getVideoTracks()[0];
    const existingAudioTracks = mediaStream.getAudioTracks();

    log('Called getDisplayMedia with', {...allSettings}, 'and screen track constraints are now', {...track.getConstraints()});
    log('[getScreenInput] MediaStream has', existingAudioTracks.length, 'audio tracks from getDisplayMedia');

    // Check for native loopback audio AFTER getDisplayMedia returns
    // The native loopback is initialized during the screen picker dialog, so it should be ready now
    if (withAudio) {
      const nativeLoopbackTrack = this.getNativeLoopbackAudioTrack();

      if (nativeLoopbackTrack) {
        log('[getScreenInput] Using native loopback audio track:', nativeLoopbackTrack.id);
        // Remove any browser-captured audio tracks first
        existingAudioTracks.forEach(t => {
          mediaStream.removeTrack(t);
          t.stop();
        });
        mediaStream.addTrack(nativeLoopbackTrack);
      } else if (existingAudioTracks.length > 0) {
        log('[getScreenInput] Using browser-captured system audio (native loopback not available)');
      } else {
        log('[getScreenInput] No audio available for screen share');
      }
    }

    track.contentHint = this.utils.preferredScreenContentHint ?? (this.screenQualityMode === ScreenQualityMode.PRESENTATION ? 'detail' : 'motion');

    log(`[getDisplayMedia] Set screen contentHint to ${track.contentHint}`);

    setTimeout(async () => {
      if (window && !this.utils.isKilled) {
        const mouse = new MouseEvent('mousemove', {clientX: 1, clientY: 1});
        window?.dispatchEvent(mouse);
      }
      if (track.readyState !== 'live') {
        return;
      }
      const updatedDefaultConstraints = this.screenQualityMode === ScreenQualityMode.PRESENTATION ? VIDEO_CONSTRAINTS.screenText : VIDEO_CONSTRAINTS.screen;

      await track.applyConstraints({...updatedDefaultConstraints, ...defaultMedia, ...this.screenConstraints || {}});
      log('[getDisplayMedia] Reapplied screen constraints on the track for good measure', {...updatedDefaultConstraints, ...defaultMedia, ...this.screenConstraints || {}}, 'and track constraints are now', track.getConstraints());
    }, 2000);


    return mediaStream;
  }

  /**
   * Gets the native loopback audio track if available.
   * This track captures system audio while excluding the app's own audio output,
   * preventing echo when screen sharing with audio.
   */
  private getNativeLoopbackAudioTrack(): MediaStreamTrack | null {
    return window.__nativeScreenAudioService?.mediaStreamTrack ?? null;
  }

  async startWebRtc(initParams: InitializeWebRtcPayload) {
    const res = await super.startWebRtc(initParams);
    this.statsPing = window.setInterval(() => {
      if (!this.peerConnectionAPI?.peerConnection) {
        return;
      }

      this.peerConnectionAPI.peerConnection.getStats().then((report) => {
        const localTracks: unknown[] = [];
        const remoteTracks: unknown[] = [];
        const peerConnectionVersionOfLocalTracks: unknown[] = [];
        const peerConnectionVersionOfRemoteTracks: unknown[] = [];
        const other: unknown[] = [];
        report.forEach((item) => {
          switch (item.type) {
            case 'inbound-rtp':
              remoteTracks.push(item);
              break;
            case 'outbound-rtp':
              localTracks.push(item);
              break;
            case 'remote-inbound-rtp':
              peerConnectionVersionOfLocalTracks.push(item);
              break;
            case 'remote-outbound-rtp':
              peerConnectionVersionOfRemoteTracks.push(item);
              break;
            default:
              other.push(item);
              break;
          }

        });
        log('RTCPeerConnection stats:', {
          other,
          localTracks,
          remoteTracks,
          peerConnectionVersionOfLocalTracks,
          peerConnectionVersionOfRemoteTracks
        });
      });
    }, 30000);
    return res;
  }

  /**
   * Stops the audio optimization effect and clears the original audio stream map when the call is ended
   */
  async stopWebRtc(): Promise<void> {
    await super.stopWebRtc();
    this.originalAudioStreamMap.forEach((audioStream) => {
      audioStream.getTracks().forEach((track) => {
        track.stop();
      });
      this.originalAudioStreamMap.clear();
    });

    // Clean up native screen audio service if still active
    // (normally cleaned up via track.stop() override, but this handles edge cases)
    window.__nativeScreenAudioService?.stop();

    if (this.statsPing) {
      window.clearInterval(this.statsPing);
    }
  }

  /**
   * Gets the audio input stream with the specified constraints, according to whether push to talk mode is on.
   * If push to talk mode is on: {noiseSuppression: true} is added to the constraints and the audio track is disabled.
   * If push to talk mode is off: The audio track uses a node-based noise cancellation effect instead of the noiseSuppression constraint.
   * @param defaultMedia
   */
  async getAudioInputStreams(defaultMedia: MediaTrackConstraints): Promise<MediaStream> {
    const constraints = {
      ...AUDIO_CONSTRAINTS,
      ...defaultMedia,
      ...this.audioConstraints || {},
      ...!EffectsSDKService.shouldUseAudioEffectsSDK ? {noiseSuppression: true} : {}
    };
    const allSettings = {audio: constraints as MediaTrackConstraints || true};

    log('Calling getUserMedia with', allSettings);

    const rawAudioStream = await navigator.mediaDevices.getUserMedia(allSettings);
    let audioStream: MediaStream = rawAudioStream;

    if (!EffectsSDKService.shouldUseAudioEffectsSDK) {
      rawAudioStream.getAudioTracks()[0].enabled = !this.isPushToTalkMode;
    } else if (EffectsSDKService.audioEffectsSDK) {
      try {
        EffectsSDKService.audioEffectsSDK.useStream(rawAudioStream);
        await new Promise<void>((resolve) => {
          EffectsSDKService.audioEffectsSDK.onReady = () => {
            audioStream = EffectsSDKService.audioEffectsSDK.getStream() as MediaStream || rawAudioStream;
            EffectsSDKService.audioEffectsSDK.run();
            log('Applied EffectsSDK to audio');
            resolve();
          };
        });
      } catch (error) {
        this.dispatch(failure(errorPayload(WebRtcErrorType.AudioEffectFailed, error, {streamId: audioStream.id})));
      }
    }

    const audioNodeManager = new AudioNodeManager();
    const defaultVolume = selectDefaultPrimaryOutputVolume(this.getState().webRtc);

    const result = await audioNodeManager.start(
      audioStream,
      ['noiseGate', 'volume'],
      this.dispatch,
      {
        noiseGate: {isPushToTalkMode: () => this.isPushToTalkMode},
        volume: {defaultVolume}
      }
    );

    if (result.stream) {
      this.originalAudioStreamMap.set(result.stream.id, audioStream);
      this.streamThresholdSetterMap.set(result.stream.id, result.setters.noiseGate.setThreshold);
      this.streamVolumeSetterMap.set(result.stream.id, result.setters.volume.setVolume);
      return result.stream;
    }

    return audioStream;
  }
}
