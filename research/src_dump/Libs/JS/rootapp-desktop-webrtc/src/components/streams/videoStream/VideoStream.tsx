import {TileId} from '@rootplatform/apiclient-webrtc-store';
import './videoStream.css';
import {CSSProperties} from 'react';
import {StreamOverlay} from '../streamOverlay/StreamOverlay.tsx';
import {StreamDimensionsStyle} from '../../../types';
import {useVideoStreamModel} from './useVideoStreamModel.ts';

/**
 * Component to display a webcam or screen share stream
 * @param tileId
 * @param dimensions
 * @constructor
 */
export const VideoStream = ({tileId, dimensions}: {
  tileId: TileId,
  dimensions: StreamDimensionsStyle
}) => {
  const {isScreenShare, isLocal, videoRef, speakingClassName} = useVideoStreamModel(tileId);

  return (
    <div style={dimensions as CSSProperties}
         className={speakingClassName}>
      <video className={[isLocal ? 'local' : 'remote', isScreenShare ? 'screen-stream' : 'video-stream'].join(' ')}
             autoPlay
             muted={true}
             ref={videoRef}
             playsInline>
      </video>
      <StreamOverlay tileId={tileId}/>
    </div>
  );
};
