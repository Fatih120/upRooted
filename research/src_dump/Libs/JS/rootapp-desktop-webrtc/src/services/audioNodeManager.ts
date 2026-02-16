import {
  errorPayload,
  failure,
  WebRtcAppDispatch,
  WebRtcErrorType
} from '@rootplatform/apiclient-webrtc-store';
import {
  AudioNodeOptionsMap,
  AudioNodesResult,
  AudioNodeType,
  AudioNodeHandler
} from '../types';
import {NoiseGateNodeHandler} from './noiseGateNodeHandler';
import {VolumeNodeHandler} from './volumeNodeHandler';

/**
 * Manages an audio processing pipeline with configurable nodes.
 * Creates a chain of audio nodes and provides setters for each node.
 *
 * @example
 * // Volume + noise gate
 * const audioNodeManager = new AudioNodeManager();
 * const result = await audioNodeManager.start(stream, ['volume', 'noiseGate'], dispatch, {
 *   volume: { defaultVolume: 1.0 },
 *   noiseGate: { isPushToTalkMode: () => false }
 * });
 * result.setters.volume.setVolume(0.8);
 * result.setters.noiseGate.setThreshold(-50);
 */
export class AudioNodeManager {
  private _audioContext?: AudioContext;
  private _audioSource?: MediaStreamAudioSourceNode;
  private _audioDestination?: MediaStreamAudioDestinationNode;
  private _handlers: Array<{handler: AudioNodeHandler<unknown>, node: AudioNode}> = [];
  private _dispatch?: WebRtcAppDispatch;

  /**
   * Starts the audio node chain with the specified nodes.
   *
   * @param audioStream - Audio stream to process
   * @param nodeTypes - Array of node types to apply in order
   * @param dispatch - Redux dispatch function for error handling
   * @param options - Options for each node type
   * @returns The processed stream, settings for each node, and a stop function
   */
  async start<T extends AudioNodeType[]>(
    audioStream: MediaStream,
    nodeTypes: T,
    dispatch: WebRtcAppDispatch,
    options?: Partial<{
      [K in T[number]]: AudioNodeOptionsMap[K];
    }>
  ): Promise<AudioNodesResult<T>> {
    this._dispatch = dispatch;
    this._audioContext = new AudioContext();

    const originalMediaTrack = audioStream.getAudioTracks()[0];
    this._audioSource = this._audioContext.createMediaStreamSource(audioStream);
    this._audioDestination = this._audioContext.createMediaStreamDestination();
    const outputMediaTrack = this._audioDestination.stream.getAudioTracks()[0];

    const setters: Record<string, unknown> = {};

    // Create handlers and nodes for each effect
    for (const effectType of nodeTypes) {
      const handler = this.createHandler(effectType, options?.[effectType as keyof typeof options]);
      const {node, setters: handlerSetters} = await handler.createNode(this._audioContext, dispatch, outputMediaTrack.id);

      this._handlers.push({handler, node});
      setters[effectType] = handlerSetters;
    }

    // Connect nodes in order
    let currentNode: AudioNode = this._audioSource;
    for (const {node} of this._handlers) {
      currentNode = currentNode.connect(node);
    }
    currentNode.connect(this._audioDestination);

    // Sync destination stream settings with original
    this.syncDestinationWithOriginalTrack(originalMediaTrack, outputMediaTrack);

    return {
      stream: this._audioDestination.stream,
      setters: setters as AudioNodesResult<T>['setters'],
      stop: () => this.stop()
    };
  }

  /**
   * Stops the audio effects chain and cleans up resources.
   */
  stop(): void {
    try {
      for (const {handler} of this._handlers) {
        handler.destroy();
      }
      this._handlers = [];

      this._audioDestination?.disconnect();
      this._audioSource?.disconnect();

      if (this._audioContext?.state !== 'closed') {
        this._audioContext?.close();
      }

      this._audioContext = undefined;
      this._audioSource = undefined;
      this._audioDestination = undefined;
    } catch (error) {
      this._dispatch?.(failure(errorPayload(WebRtcErrorType.AudioEffectFailed, error, 'Failed to stop audio effects chain')));
      throw error;
    }
  }

  /**
   * Creates a handler instance for the given node type.
   */
  private createHandler(nodeType: AudioNodeType, options?: unknown): AudioNodeHandler<unknown> {
    switch (nodeType) {
      case 'volume':
        return new VolumeNodeHandler(options as AudioNodeOptionsMap['volume'] | undefined);
      case 'noiseGate':
        if (!options || !(options as AudioNodeOptionsMap['noiseGate']).isPushToTalkMode) {
          throw new Error('noiseGate node requires isPushToTalkMode option');
        }
        return new NoiseGateNodeHandler(options as AudioNodeOptionsMap['noiseGate']);
      default:
        throw new Error(`Unknown node type: ${nodeType}`);
    }
  }

  /**
   * Syncs the destination stream settings with the original stream.
   */
  private syncDestinationWithOriginalTrack(originalTrack: MediaStreamTrack, outputTrack: MediaStreamTrack): void {
    if (!originalTrack || !outputTrack || !this._audioDestination) {
      const error = new Error('Media tracks not initialized');
      this._dispatch?.(failure(errorPayload(WebRtcErrorType.AudioEffectFailed, error, {
        originalTrack,
        outputTrack,
        audioDestination: this._audioDestination
      })));
      throw error;
    }

    outputTrack.enabled = originalTrack.enabled;
    outputTrack.getSettings = originalTrack.getSettings;
    outputTrack.stop = () => {
      this.stop();
      MediaStreamTrack.prototype.stop.call(originalTrack);
    };
  }
}

