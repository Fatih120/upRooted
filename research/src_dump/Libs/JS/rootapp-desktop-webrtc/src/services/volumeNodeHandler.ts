import {DEFAULT_INPUT_VOLUME, log} from '@rootplatform/apiclient-webrtc-store';
import {AudioNodeHandler, VolumeNodeOptions, VolumeNodeSetters} from '../types';

/**
 * Handles volume control using a GainNode in the Web Audio API.
 */
export class VolumeNodeHandler implements AudioNodeHandler<VolumeNodeSetters> {
  private _gainNode?: GainNode;
  private _audioContext?: AudioContext;
  private _options: VolumeNodeOptions;

  constructor(options: VolumeNodeOptions = {}) {
    this._options = options;
  }

  async createNode(audioContext: AudioContext): Promise<{
    node: AudioNode;
    setters: VolumeNodeSetters;
  }> {
    this._audioContext = audioContext;
    this._gainNode = audioContext.createGain();
    this._gainNode.gain.value = this._options.defaultVolume ?? DEFAULT_INPUT_VOLUME;

    const setVolume = (volume: number) => {
      if (this._gainNode && this._audioContext) {
        this._gainNode.gain.setValueAtTime(volume, this._audioContext.currentTime);
      }
    };

    return {
      node: this._gainNode,
      setters: {setVolume}
    };
  }

  destroy(): void {
    this._gainNode?.disconnect();
    this._gainNode = undefined;
    this._audioContext = undefined;
  }
}

