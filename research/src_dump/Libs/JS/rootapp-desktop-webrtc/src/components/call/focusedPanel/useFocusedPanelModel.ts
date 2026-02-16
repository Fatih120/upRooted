import {
  selectFocusedTileId,
  selectIsScreenTile,
  selectTileById,
  selectUnfocusedTileIds,
  TileId
} from '@rootplatform/apiclient-webrtc-store';
import {COLOR} from '@rootplatform/assets';
import {useGoodGrid, useGridDimensions} from 'good-grid/react';
import {useCallback, useEffect, useRef, useState} from 'react';
import {useAppDispatch, useAppSelector} from '../../../hooks';
import {useHideClassOnIdle} from '../../../hooks/useHideClassOnIdle';
import {Lang} from '../../../i18n';
import {selectIsFullFocus, toggleFullFocus} from '../../../redux';
import {remToPx} from '../../../services';
import {Position, StreamDimensionsStyle} from '../../../types';

export const useFocusedPanelModel = () => {
  const tileId = useAppSelector(state => selectFocusedTileId(state.webRtc));
  const gridRef = useRef<HTMLDivElement>(null);
  const isScreenFocused = useAppSelector(state => selectIsScreenTile(state.webRtc, tileId as TileId));
  const unfocusedTiles = useAppSelector(state => selectUnfocusedTileIds(state.webRtc));
  const [fullFocusButtonText, setFullFocusButtonText] = useState<string | undefined>(undefined);
  const isFullFocus = useAppSelector(state => selectIsFullFocus(state));
  const isLoadingFocused = useAppSelector(state => selectTileById(state.webRtc?.tiles, tileId as TileId)?.tileState === 'loading');
  const hiddenClass = useHideClassOnIdle();
  const dispatch = useAppDispatch();
  const showMembersLabel = Lang.showMembers();
  const dimensions = useGridDimensions(gridRef);
  const gap = remToPx(isScreenFocused && !isLoadingFocused ? 0.03125: 0.25);
  const aspectRatio = useRef<string>('16:9');
  const updateAspectRatio = useCallback((screenWidth: number, screenHeight: number) => {
    if (screenWidth && screenHeight && aspectRatio.current !== `${screenWidth}:${screenHeight}`) {
      aspectRatio.current = `${screenWidth}:${screenHeight}`;
    }
  }, [aspectRatio.current]);

  useEffect(() => {
    if (!isScreenFocused || isLoadingFocused) {
      aspectRatio.current = '16:9';
      return;
    }
    const screenElement = document.getElementById('focused')?.querySelector('video') as HTMLVideoElement | null;
    if (!screenElement) return;

    let rafId = 0;


    const onLoaded = () => updateAspectRatio(screenElement.videoWidth, screenElement.videoHeight);
    const onVideoResize = () => updateAspectRatio(screenElement.videoWidth, screenElement.videoHeight);
    const onFrame = () => {
      updateAspectRatio(screenElement.videoWidth, screenElement.videoHeight);
      if (screenElement.requestVideoFrameCallback) {
        screenElement.requestVideoFrameCallback(onFrame);
      }
    };

    screenElement.addEventListener('loadedmetadata', onLoaded);
    screenElement.addEventListener('resize', onVideoResize);
    if (screenElement.requestVideoFrameCallback) {
      screenElement.requestVideoFrameCallback(onFrame);
    } else {
      rafId = requestAnimationFrame(() => updateAspectRatio(screenElement.videoWidth, screenElement.videoHeight));
    }

    return () => {
      screenElement.removeEventListener('loadedmetadata', onLoaded);
      screenElement.removeEventListener('resize', onVideoResize);
      if (rafId) {
        cancelAnimationFrame(rafId);
      }
    };
  }, [isScreenFocused, isLoadingFocused, tileId, aspectRatio]);

  const {width, height, getPosition} = useGoodGrid({
    dimensions,
    count: 1,
    gap,
    aspectRatio: aspectRatio.current
  });

  const onResize = (size: number, previousSize?: number) => {
    if (size == null || previousSize == null) return;
    if (isFullFocus && Number(previousSize) === 100 && size < 100) {
      dispatch(toggleFullFocus({isFullFocus: false}));
    }
  };

  const onClickShowMembers = () => {
    dispatch(toggleFullFocus({isFullFocus: false}));
  };

  useEffect(() => {
    if (isFullFocus && unfocusedTiles?.length) {
      if (fullFocusButtonText !== showMembersLabel) {
        setFullFocusButtonText(showMembersLabel);
      }
    } else if (fullFocusButtonText !== undefined) {
      setFullFocusButtonText(undefined);
    }

  }, [isFullFocus, unfocusedTiles, height]);

  const styles = {
    backgroundColor: COLOR.backgroundPrimary,
    width,
    height,
    ...(isScreenFocused && !isLoadingFocused ? {
      objectFit: 'cover'
    } : {
      position: 'absolute' as Position,
      ...getPosition(0),
      objectFit: 'contain',
      aspectRatio: 16 / 9
    })
  } as StreamDimensionsStyle;

  return {
    tileId,
    height,
    styles,
    fullFocusButtonText,
    hiddenClass,
    onResize,
    onClickShowMembers,
    gridRef
  };
};