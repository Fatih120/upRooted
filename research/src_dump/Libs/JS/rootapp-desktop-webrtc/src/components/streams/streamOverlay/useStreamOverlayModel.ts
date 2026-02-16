import {UserGuid} from '@rootplatform/apiclient-internal';
import {PrimaryTile, selectIsFocusable, selectTileById, TileId, TileType} from '@rootplatform/apiclient-webrtc-store';
import {useCallback} from 'react';
import {useAppDispatch, useAppSelector} from '../../../hooks';
import {useHideClassOnIdle} from '../../../hooks/useHideClassOnIdle';
import {clickFocus, clickUsername, rightClick, selectUserById} from '../../../redux';
import {useStreamFontSize, useStreamIconSize} from '../StreamFontSizeProvider.tsx';

export const useStreamOverlayModel = (tileId: TileId) => {
  const dispatch = useAppDispatch();
  const tile = useAppSelector(state => selectTileById(state.webRtc.tiles, tileId));
  const isLoading = tile?.tileState === 'loading';
  const isRinging = tile?.tileState === 'ringing';
  const isPrimaryTile = tile?.tileType === TileType.PRIMARY;
  const {
    isHandRaised,
    isMuted,
    isDeafened,
    isAdminMuted,
    isAdminDeafened,
    userId
  } = isPrimaryTile ? tile as PrimaryTile : {userId: tile?.userId as UserGuid};
  const isFocusable = useAppSelector(state => selectIsFocusable(state.webRtc));
  const username = useAppSelector(state => selectUserById(state.desktopWebRtc, userId))?.username;
  const iconSize = useStreamIconSize();
  const fontSize = useStreamFontSize();
  const hiddenClass = useHideClassOnIdle();
  const onClick = useCallback((event: React.MouseEvent<HTMLDivElement, MouseEvent>) => {
    if (isFocusable) {
      event.preventDefault();
      dispatch(clickFocus({tileId}));
    }
  }, [dispatch, tileId, isFocusable]);

  const onRightClick = useCallback((event: React.MouseEvent<HTMLDivElement, MouseEvent>) => {
    event.preventDefault();
    dispatch(rightClick({tileId, coordinates: {x: event.clientX, y: event.clientY}}));
  }, [dispatch, tileId]);

  const onClickUsername = useCallback((event: React.MouseEvent<HTMLDivElement, MouseEvent>) => {
    event.stopPropagation();
    event.preventDefault();
    dispatch(clickUsername({tileId, coordinates: {x: event.clientX, y: event.clientY}}));
  }, [dispatch, tileId]);

  const onClickIcon = useCallback((event: React.MouseEvent<HTMLDivElement, MouseEvent>) => {
    event.stopPropagation();
  }, []);

  return {
    isMuted,
    isDeafened,
    isHandRaised,
    onClick,
    onRightClick,
    onClickUsername,
    onClickIcon,
    username,
    fontSize,
    iconSize,
    isPrimaryTile,
    isLoading,
    isRinging,
    isAdminMuted,
    isAdminDeafened,
    isFocusable,
    hiddenClass
  };
};
