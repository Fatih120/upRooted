import {setSpeaking, WebRtcAppDispatch} from '@rootplatform/apiclient-webrtc-store';
import {AudioNodeHandler, NoiseGateNodeOptions, NoiseGateNodeSetters} from '../types';
import {noiseGateJS} from './index';

/**
 * Handles noise gate processing using an AudioWorkletNode for voice activity detection.
 * Only used as a fallback if the EffectsSDK noise gate is not available.
 */
export class NoiseGateNodeHandler implements AudioNodeHandler<NoiseGateNodeSetters> {
  private _noiseGateNode?: AudioWorkletNode;
  private _audioContext?: AudioContext;
  private _dispatch?: WebRtcAppDispatch;
  private _options: NoiseGateNodeOptions;
  private _outputTrackId?: string;

  constructor(options: NoiseGateNodeOptions) {
    this._options = options;
  }

  async createNode(audioContext: AudioContext, dispatch: WebRtcAppDispatch, outputTrackId: string): Promise<{
    node: AudioNode;
    setters: NoiseGateNodeSetters;
  }> {
    this._audioContext = audioContext;
    this._dispatch = dispatch;
    this._outputTrackId = outputTrackId;

    const noiseGateWorklet = await this.loadNoiseGateWorklet();
    await audioContext.audioWorklet.addModule(noiseGateWorklet);

    this._noiseGateNode = new AudioWorkletNode(audioContext, 'noise-gate-worklet');
    this._noiseGateNode.port.onmessage = this.onVoiceActivity.bind(this);

    const setThreshold = (threshold: number) => {
      if (this._noiseGateNode && this._audioContext) {
        this._noiseGateNode.parameters.get('threshold')!.setValueAtTime(threshold, this._audioContext.currentTime);
      }
    };

    const setOutputTrackId = (trackId: string) => {
      this._outputTrackId = trackId;
    };

    return {
      node: this._noiseGateNode,
      setters: {setThreshold, setOutputTrackId}
    };
  }

  destroy(): void {
    this._noiseGateNode?.port?.close();
    this._noiseGateNode?.disconnect();
    this._noiseGateNode = undefined;
    this._audioContext = undefined;
    this._dispatch = undefined;
  }

  /**
   * Loads the noise gate worklet as a data URL.
   */
  private async loadNoiseGateWorklet(): Promise<string> {
    const noiseGateReader = new FileReader();
    noiseGateReader.readAsDataURL(new Blob([noiseGateJS], {type: 'application/javascript'}));

    return new Promise((resolve) => {
      noiseGateReader.onloadend = () => {
        resolve(noiseGateReader.result as string);
      };
    });
  }

  /**
   * Handles voice activity detection events from the noise gate node.
   */
  private onVoiceActivity(event: MessageEvent): void {
    if (!this._outputTrackId || !this._dispatch) {
      return;
    }
    if (!this._options.isPushToTalkMode()) {
      this._dispatch(setSpeaking({trackId: this._outputTrackId, isSpeaking: event.data.talking}));
    }
  }
}
