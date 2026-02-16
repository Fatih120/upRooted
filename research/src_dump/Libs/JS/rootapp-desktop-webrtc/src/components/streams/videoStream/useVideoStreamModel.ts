import {
  selectIsLocalTileByTileId,
  selectIsScreenTile,
  selectVisualStreamIdByTileId,
  TileId
} from '@rootplatform/apiclient-webrtc-store';
import {useEffect, useRef} from 'react';
import {useAppSelector, useSpeakingClassName} from '../../../hooks';
import {streamService} from '../../../redux';

export const useVideoStreamModel = (tileId: TileId) => {
  const videoRef = useRef<HTMLVideoElement>(null);
  const isLocal = useAppSelector(state => selectIsLocalTileByTileId(state.webRtc, tileId));
  const streamId = useAppSelector(state => selectVisualStreamIdByTileId(state.webRtc, tileId));
  const isScreenShare = useAppSelector(state => selectIsScreenTile(state.webRtc, tileId));
  const speakingClassName = useSpeakingClassName(tileId, isScreenShare);
  const stream = streamService.getStream(streamId);
  const srcTrack = stream?.getTracks()?.[0];

  useEffect(() => {
    const currRef = videoRef.current;
    if (!currRef || !stream) return;

    currRef.srcObject = stream;

    return () => {
      currRef.srcObject = null;
    };
  }, [stream, srcTrack?.contentHint]);

  return {isScreenShare, speakingClassName, isLocal, videoRef};
};
