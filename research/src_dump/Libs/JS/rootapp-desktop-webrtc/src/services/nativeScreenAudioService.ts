import {log} from '@rootplatform/apiclient-webrtc-store';

/**
 * Service for receiving native loopback audio from C# and exposing it as a MediaStreamTrack.
 * Uses WASAPI process loopback capture to get system audio while excluding the app's own output,
 * preventing echo when screen sharing with audio.
 */
export class NativeScreenAudioService {
  private _isActive = false;
  private _sampleRate = 0;
  private _channels = 0;
  private _audioContext: AudioContext | null = null;
  private _workletNode: AudioWorkletNode | null = null;
  private _mediaStreamTrack: MediaStreamTrack | null = null;

  get isActive(): boolean {
    return this._isActive;
  }

  get mediaStreamTrack(): MediaStreamTrack | null {
    return this._mediaStreamTrack;
  }

  /**
   * Starts the native loopback audio pipeline.
   * Called by C# when WASAPI loopback capture starts.
   */
  async start(sampleRate: number, channels: number): Promise<MediaStreamTrack | null> {
    log('[NativeScreenAudio] Starting:', sampleRate, 'Hz,', channels, 'channels');

    this._isActive = true;
    this._sampleRate = sampleRate;
    this._channels = channels;

    try {
      await this.initializeAudioPipeline();
      return this._mediaStreamTrack;
    } catch (error) {
      log('[NativeScreenAudio] Failed to initialize:', error);
      this.stop();
      return null;
    }
  }

  /**
   * Receives audio data from C#.
   * @param data - Float32 audio samples
   */
  receiveAudioData(data: Float32Array): void {
    if (!this._isActive || !this._workletNode) {
      return;
    }

    this._workletNode.port.postMessage({
      type: 'audioData',
      data: data
    });
  }

  /**
   * Stops the native loopback audio pipeline and cleans up resources.
   * Called by C# when loopback capture stops.
   */
  stop(): void {
    if (!this._isActive) return;

    log('[NativeScreenAudio] Stopping');
    this._isActive = false;

    if (this._workletNode) {
      try {
        this._workletNode.disconnect();
      } catch {
        // Ignore disconnect errors during cleanup
      }
      this._workletNode = null;
    }

    // Don't stop the track here - let WebRTC disconnect handle it.
    // Stopping while attached to a peer connection can cause hangs.
    this._mediaStreamTrack = null;

    if (this._audioContext && this._audioContext.state !== 'closed') {
      try {
        this._audioContext.close();
      } catch {
        // Ignore close errors during cleanup
      }
    }
    this._audioContext = null;
  }

  /**
   * Initializes the Web Audio API pipeline with an AudioWorklet for receiving native audio.
   */
  private async initializeAudioPipeline(): Promise<void> {
    this._audioContext = new AudioContext({sampleRate: this._sampleRate});

    const destination = this._audioContext.createMediaStreamDestination();

    // Inline AudioWorklet processor as a Blob URL
    const processorCode = this.createProcessorCode();
    const blob = new Blob([processorCode], {type: 'application/javascript'});
    const workletUrl = URL.createObjectURL(blob);

    try {
      await this._audioContext.audioWorklet.addModule(workletUrl);
    } finally {
      URL.revokeObjectURL(workletUrl);
    }

    this._workletNode = new AudioWorkletNode(this._audioContext, 'native-screen-audio-processor', {
      numberOfInputs: 0,
      numberOfOutputs: 1,
      outputChannelCount: [this._channels]
    });

    // Connect worklet to destination to produce the MediaStreamTrack
    this._workletNode.connect(destination);

    // Keep the audio graph alive with a silent connection to destination
    const silentGain = this._audioContext.createGain();
    silentGain.gain.value = 0;
    this._workletNode.connect(silentGain);
    silentGain.connect(this._audioContext.destination);

    this._mediaStreamTrack = destination.stream.getAudioTracks()[0];

    // Override the track's stop() method to clean up resources when the track is stopped
    // This follows the same pattern as AudioNodeManager.syncDestinationWithOriginalTrack
    if (this._mediaStreamTrack) {
      const originalStop = this._mediaStreamTrack.stop.bind(this._mediaStreamTrack);
      this._mediaStreamTrack.stop = () => {
        this.stop();
        originalStop();
      };
    }

    log('[NativeScreenAudio] Pipeline initialized, track:', this._mediaStreamTrack?.id);
  }

  /**
   * Creates the AudioWorklet processor code with a ring buffer for handling audio data.
   */
  private createProcessorCode(): string {
    const ringBufferSize = Math.ceil(this._sampleRate * 0.2) * this._channels;

    return `
      const RING_BUFFER_SIZE = ${ringBufferSize};

      class NativeScreenAudioProcessor extends AudioWorkletProcessor {
        constructor() {
          super();
          this.ringBuffer = new Float32Array(RING_BUFFER_SIZE);
          this.writeIndex = 0;
          this.readIndex = 0;
          this.samplesAvailable = 0;
          this.channels = ${this._channels};

          this.port.onmessage = (event) => {
            if (event.data.type === 'audioData') {
              this.handleAudioData(event.data.data);
            }
          };
        }

        handleAudioData(data) {
          const samplesToWrite = data.length;
          for (let i = 0; i < samplesToWrite; i++) {
            this.ringBuffer[this.writeIndex] = data[i];
            this.writeIndex = (this.writeIndex + 1) % RING_BUFFER_SIZE;
          }
          this.samplesAvailable += samplesToWrite;
          if (this.samplesAvailable > RING_BUFFER_SIZE) {
            const overflow = this.samplesAvailable - RING_BUFFER_SIZE;
            this.readIndex = (this.readIndex + overflow) % RING_BUFFER_SIZE;
            this.samplesAvailable = RING_BUFFER_SIZE;
          }
        }

        process(inputs, outputs, parameters) {
          const output = outputs[0];
          if (!output || output.length === 0) return true;

          const framesNeeded = output[0].length;
          const samplesNeeded = framesNeeded * this.channels;

          if (this.samplesAvailable >= samplesNeeded) {
            for (let i = 0; i < framesNeeded; i++) {
              if (output[0]) output[0][i] = this.ringBuffer[this.readIndex];
              this.readIndex = (this.readIndex + 1) % RING_BUFFER_SIZE;
              if (this.channels > 1) {
                if (output[1]) output[1][i] = this.ringBuffer[this.readIndex];
                this.readIndex = (this.readIndex + 1) % RING_BUFFER_SIZE;
              }
            }
            this.samplesAvailable -= samplesNeeded;
          } else {
            for (const channel of output) {
              if (channel) channel.fill(0);
            }
          }
          return true;
        }
      }

      registerProcessor('native-screen-audio-processor', NativeScreenAudioProcessor);
    `;
  }
}
