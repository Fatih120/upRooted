import {ElementVolumeBoosterState, VolumeBoosterSettings} from '../types';

// Default settings
const DEFAULT_SETTINGS: Required<VolumeBoosterSettings> = {
  volumeCurveExponent: 2.5,
  compressorThreshold: 0,
  compressorKnee: 40,
  compressorRatio: 1.5,
  compressorAttack: 0.003,
  compressorRelease: 0.25
};

/**
 * Converts a linear volume (0-3) to an exponential gain value for perceptually correct volume.
 * Human hearing is logarithmic, so we need exponential scaling to make 200% sound "twice as loud".
 * Uses a power curve: gain = volume^exponent
 *
 * @param linearVolume - Linear volume value (0 = silent, 1 = normal, 2 = 200%, 3 = 300%)
 * @param exponent - Power exponent (2 = squared, 3 = cubed). Higher = more dramatic scaling.
 * @returns Exponentially scaled gain value
 */
function linearToExponentialGain(linearVolume: number, exponent: number): number {
  if (linearVolume <= 0) return 0;
  return Math.pow(linearVolume, exponent);
}

/**
 * Provides volume control past 100% for HTMLAudioElements using Web Audio API.
 * Unlike AudioNodeManager for local tracks, this does not create a new stream/track - it processes
 * the output of an existing <audio> element, preserving the original track ID.
 *
 * Uses a shared AudioContext across all audio elements for better performance with many streams.
 */
export class ElementVolumeBooster {
  private static _audioContext: AudioContext | null = null;
  private static _activeCount = 0;
  private static _settings: Required<VolumeBoosterSettings> = {...DEFAULT_SETTINGS};
  private static _boosters = new Map<HTMLAudioElement, ElementVolumeBoosterState>();

  private static getAudioContext(): AudioContext {
    if (!this._audioContext || this._audioContext.state === 'closed') {
      this._audioContext = new AudioContext();
      this._boosters.clear();
    }
    if (this._audioContext.state === 'suspended') {
      this._audioContext.resume();
    }
    return this._audioContext;
  }

  private static releaseAudioContext(): void {
    this._activeCount--;
    if (this._activeCount <= 0 && this._audioContext) {
      this._audioContext.close();
      this._audioContext = null;
      this._activeCount = 0;
      this._boosters.clear();
    }
  }

  /**
   * Updates the volume booster settings (exponent and compressor values).
   * Changes take effect immediately for all active boosters.
   */
  static updateSettings(settings: VolumeBoosterSettings): void {
    this._settings = {...this._settings, ...settings};

    // Update compressor settings on all active boosters
    this._boosters.forEach((booster) => {
      if (booster.connected) {
        booster.compressorNode.threshold.value = this._settings.compressorThreshold;
        booster.compressorNode.knee.value = this._settings.compressorKnee;
        booster.compressorNode.ratio.value = this._settings.compressorRatio;
        booster.compressorNode.attack.value = this._settings.compressorAttack;
        booster.compressorNode.release.value = this._settings.compressorRelease;
      }
    });

  }

  /**
   * Sets the volume for an attached element.
   * This method looks up the booster from the Map to ensure we always use the correct gainNode.
   */
  static setVolume(element: HTMLAudioElement, volume: number): void {
    const booster = this._boosters.get(element);
    if (!booster || !booster.connected) {
      return;
    }
    booster.gainNode.gain.value = linearToExponentialGain(volume, this._settings.volumeCurveExponent);
  }

  /**
   * Checks if the volume booster is currently attached and connected to an element.
   */
  static isAttached(element: HTMLAudioElement): boolean {
    const booster = this._boosters.get(element);
    return booster?.connected ?? false;
  }

  /**
   * Ensures the AudioContext is running. Call this after setSinkId or other operations
   * that might cause audio to stop.
   */
  static ensureAudioContextRunning(): void {
    if (this._audioContext && this._audioContext.state === 'suspended') {
      this._audioContext.resume();
    }
  }

  /**
   * Sets the output device for the AudioContext.
   * This allows audio processed through the volume booster to be routed to a specific output device.
   * Requires Chrome 110+ / Electron with Chromium 110+.
   *
   * @param deviceId - The device ID to route audio to (from navigator.mediaDevices.enumerateDevices())
   */
  static async setSinkId(deviceId: string): Promise<void> {
    if (!this._audioContext) {
      return;
    }

    // Check if setSinkId is supported on AudioContext
    if (typeof (this._audioContext as AudioContext & { setSinkId?: (id: string) => Promise<void> }).setSinkId !== 'function') {
      return;
    }

    await (this._audioContext as AudioContext & { setSinkId: (id: string) => Promise<void> }).setSinkId(deviceId);
  }

  /**
   * Gets the current sinkId of the AudioContext.
   */
  static getSinkId(): string | undefined {
    if (!this._audioContext) return undefined;
    return (this._audioContext as AudioContext & { sinkId?: string }).sinkId;
  }

  /**
   * Checks current audio levels flowing through the gain node.
   * Returns average and max frequency data. Useful for debugging.
   */
  static checkAudioLevels(element: HTMLAudioElement): { avg: number; max: number } | null {
    const booster = this._boosters.get(element);
    if (!booster || !booster.connected || !this._audioContext) {
      return null;
    }

    const analyser = this._audioContext.createAnalyser();
    analyser.fftSize = 256;
    booster.gainNode.connect(analyser);
    const dataArray = new Uint8Array(analyser.frequencyBinCount);
    analyser.getByteFrequencyData(dataArray);
    const avg = dataArray.reduce((a, b) => a + b, 0) / dataArray.length;
    const max = Math.max(...dataArray);
    analyser.disconnect();
    return { avg, max };
  }

  /**
   * Gets debug info about the current state.
   */
  static getDebugInfo(element?: HTMLAudioElement): {
    audioContextState: string | null;
    audioContextSinkId: string | undefined;
    activeCount: number;
    boosterCount: number;
    elementInfo?: {
      connected: boolean;
    };
  } {
    const info: ReturnType<typeof ElementVolumeBooster.getDebugInfo> = {
      audioContextState: this._audioContext?.state ?? null,
      audioContextSinkId: this.getSinkId(),
      activeCount: this._activeCount,
      boosterCount: this._boosters.size
    };

    if (element) {
      const booster = this._boosters.get(element);
      if (booster) {
        info.elementInfo = {
          connected: booster.connected
        };
      }
    }

    return info;
  }

  /**
   * Attaches volume boost control to an HTMLAudioElement.
   *
   * @param element - The audio element to control
   * @param defaultVolume - Initial volume (1.0 = 100%, 2.0 = 200%)
   * @returns A function to set the volume and a function to detach
   */
  static attach(element: HTMLAudioElement, defaultVolume = 1.0): {
    setVolume: (volume: number) => void;
    detach: () => void;
  } {
    const audioContext = this.getAudioContext();

    // Check if this element is already attached - reuse existing booster
    const existingBooster = this._boosters.get(element);
    if (existingBooster) {
      // If already connected, just update volume and return controls
      if (existingBooster.connected) {
        this.setVolume(element, defaultVolume);
        const setVolume = (volume: number) => this.setVolume(element, volume);
        const detach = () => this.detach(element);
        return {setVolume, detach};
      }

      // Was disconnected, reconnect to audioContext.destination
      this._activeCount++;
      existingBooster.sourceNode.connect(existingBooster.gainNode);
      existingBooster.gainNode.connect(existingBooster.compressorNode);
      existingBooster.compressorNode.connect(audioContext.destination);
      existingBooster.connected = true;

      // For MediaStream sources, mute the element
      if (element.srcObject instanceof MediaStream) {
        element.volume = 0;
      }

      this.setVolume(element, defaultVolume);

      const setVolume = (volume: number) => this.setVolume(element, volume);
      const detach = () => this.detach(element);
      return {setVolume, detach};
    }

    this._activeCount++;

    // Store original volume to restore on detach
    const originalVolume = element.volume;

    // Create source - use MediaStreamSource if srcObject is a MediaStream (WebRTC)
    let sourceNode: MediaElementAudioSourceNode | MediaStreamAudioSourceNode;

    if (element.srcObject instanceof MediaStream) {
      sourceNode = audioContext.createMediaStreamSource(element.srcObject);
    } else {
      sourceNode = audioContext.createMediaElementSource(element);
    }

    // Create gain node for volume control
    const gainNode = audioContext.createGain();
    gainNode.gain.value = linearToExponentialGain(defaultVolume, this._settings.volumeCurveExponent);

    // Create compressor to handle boosted audio without harsh clipping
    const compressorNode = audioContext.createDynamicsCompressor();
    compressorNode.threshold.value = this._settings.compressorThreshold;
    compressorNode.knee.value = this._settings.compressorKnee;
    compressorNode.ratio.value = this._settings.compressorRatio;
    compressorNode.attack.value = this._settings.compressorAttack;
    compressorNode.release.value = this._settings.compressorRelease;

    sourceNode.connect(gainNode);
    gainNode.connect(compressorNode);
    compressorNode.connect(audioContext.destination);

    // For MediaStream sources, mute the element since audio goes through AudioContext
    // This prevents double-playback
    if (element.srcObject instanceof MediaStream) {
      element.volume = 0;
    }

    this._boosters.set(element, {
      gainNode,
      sourceNode,
      compressorNode,
      connected: true,
      originalVolume
    });

    const setVolume = (volume: number) => this.setVolume(element, volume);

    const detach = () => this.detach(element);

    return {setVolume, detach};
  }

  /**
   * Detaches the volume control from an element and cleans up resources.
   * Note: We keep the booster in the map because MediaElementAudioSourceNode
   * cannot be recreated for the same element - we'll reconnect it if attach is called again.
   */
  static detach(element: HTMLAudioElement): void {
    const booster = this._boosters.get(element);
    if (!booster || !booster.connected) {
      return;
    }

    booster.compressorNode.disconnect();
    booster.gainNode.disconnect();
    booster.sourceNode.disconnect();
    booster.connected = false;

    // Restore original element volume
    element.volume = booster.originalVolume;

    this.releaseAudioContext();
  }
}
