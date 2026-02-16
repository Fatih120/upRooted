import {selectIsSpeakingByTileId, TileId} from '@rootplatform/apiclient-webrtc-store';
import {useEffect, useRef} from 'react';
import {useAppSelector} from './useAppSelector';

/**
 * Returns a class name that indicates if the track is capturing speech or has in the past.
 * @param tileId
 * @param isScreenShare
 */
export const useSpeakingClassName = (tileId: TileId, isScreenShare = false) => {
  if (isScreenShare) {
    return 'stream-container';
  }

  const isSpeaking = useAppSelector(state => selectIsSpeakingByTileId(state.webRtc, tileId));
  const hasSpoken = useRef(false);
  const speakingClassName =
    `stream-container${
      isSpeaking ? ' speaking' : ''
    }${
      hasSpoken.current ? ' has-spoken' : ''
    }`;

  useEffect(() => {
    if (isSpeaking) {
      hasSpoken.current = true;
    }
  }, [isSpeaking]);

  return speakingClassName;
};