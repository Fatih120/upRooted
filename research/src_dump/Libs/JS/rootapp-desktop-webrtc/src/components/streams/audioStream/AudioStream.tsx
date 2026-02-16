import {StreamDimensionsStyle} from '../../../types';
import {useAudioStreamModel} from './useAudioStreamModel.ts';

/**
 * Component to render an audio stream (nothing visible)
 * @param streamId
 * @param tileId
 * @constructor
 */
export const AudioStream = ({streamId}: {
  streamId: string,
  dimensions?: StreamDimensionsStyle
}) => {
  const {
    isCurrentAudio,
    audioRef
  } = useAudioStreamModel(streamId);
  return (
    <>
      {
        isCurrentAudio ? null :
          <audio id={'audio-stream'}
                 autoPlay
                 ref={audioRef}>
          </audio>
      }
    </>
  );
};
