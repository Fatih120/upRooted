import {selectTileById, TileId, TileState} from '@rootplatform/apiclient-webrtc-store';
import {useAppSelector} from '../../hooks';
import {StreamDimensionsStyle} from '../../types';
import {ProfileStream} from './profileStream/ProfileStream';
import {VideoStream} from './videoStream/VideoStream.tsx';
import './stream.css';

export const Stream = ({tileId, streamDimensionsStyle}: {
  tileId: TileId,
  streamDimensionsStyle: StreamDimensionsStyle,
}) => {
  const tileState = useAppSelector(state => selectTileById(state.webRtc.tiles, tileId))?.tileState;
  const isLoading = tileState === TileState.LOADING;
  const isProfile = tileState === TileState.PROFILE;
  const isRinging = tileState === TileState.RINGING;

  return <>
    {
      isProfile || isLoading || isRinging ?
        <ProfileStream
          key={tileId}
          tileId={tileId}
          dimensions={streamDimensionsStyle}>
        </ProfileStream> :
        <VideoStream
          key={tileId}
          tileId={tileId}
          dimensions={streamDimensionsStyle}>
        </VideoStream>
    }
  </>;
};