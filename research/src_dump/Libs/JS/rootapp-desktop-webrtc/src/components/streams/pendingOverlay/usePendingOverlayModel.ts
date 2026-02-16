import {selectTileById, TileId, TileState} from '@rootplatform/apiclient-webrtc-store';
import {useCallback} from 'react';
import {useAppSelector} from '../../../hooks';
import {clickUsername, dispatch, selectUsernameByTileId} from '../../../redux';
import {useLargeStreamIconSize, useStreamFontSize} from '../StreamFontSizeProvider';

export const usePendingOverlayModel = (tileId: TileId) => {
  const tile = useAppSelector(state => selectTileById(state.webRtc.tiles, tileId));
  const size = useLargeStreamIconSize();
  const bottomDistance = useStreamFontSize();
  const username = useAppSelector(state => selectUsernameByTileId(state, tileId));
  const onClickUsername = useCallback((event: React.MouseEvent<HTMLDivElement, MouseEvent>) => {
    event.stopPropagation();
    event.preventDefault();
    dispatch(clickUsername({tileId, coordinates: {x: event.clientX, y: event.clientY}}));
  }, [dispatch, tileId]);

  return {
    isLoading: tile?.tileState === TileState.LOADING,
    isRinging: tile?.tileState === TileState.RINGING,
    size,
    username,
    bottomDistance,
    onClickUsername
  };
};
