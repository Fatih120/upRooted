import {selectFocusedTileId, selectUnfocusedTileIds} from '@rootplatform/apiclient-webrtc-store';
import {useGoodGrid, useGridDimensions} from 'good-grid/react';
import {useEffect, useRef, useState} from 'react';
import {useAppDispatch, useAppSelector} from '../../../hooks';
import {useHideClassOnIdle} from '../../../hooks/useHideClassOnIdle';
import {Lang} from '../../../i18n';
import {selectIsFullFocus, toggleFullFocus} from '../../../redux';
import {remToPx} from '../../../services';
import {Position} from '../../../types';

export const useUnfocusedPanelModel = () => {
  const tileIds = useAppSelector(state => selectUnfocusedTileIds(state.webRtc));
  const gridRef = useRef<HTMLDivElement>(null);
  const focusedTileId = useAppSelector(state => selectFocusedTileId(state.webRtc));
  const hasFocused = !!focusedTileId;
  const isFullFocus = useAppSelector(state => selectIsFullFocus(state));
  const [fullFocusButtonText, setFullFocusButtonText] = useState<string | undefined>(undefined);
  const panelHeight = document.getElementById('unfocused')?.scrollHeight;
  const hiddenClass = useHideClassOnIdle();
  const dimensions = useGridDimensions(gridRef);
  const gap = remToPx(0.75);
  const dispatch = useAppDispatch();
  const hideMembersLabel = Lang.hideMembers();

  const {width, height, getPosition} = useGoodGrid({
    dimensions,
    count: tileIds.length,
    aspectRatio: '16:9',
    gap
  });

  const onResize = (size: number, previousSize?: number) => {
    if (size == null || previousSize == null) return;
    if (!isFullFocus && size === 0 && Number(previousSize) > 0) {
      dispatch(toggleFullFocus({isFullFocus: true}));
    }
  };

  const onClickHideMembers = () => {
    dispatch(toggleFullFocus({isFullFocus: true}));
  }

  useEffect(() => {
    if (tileIds?.length && !isFullFocus && hasFocused) {
      if(fullFocusButtonText !== hideMembersLabel) {
        setFullFocusButtonText(hideMembersLabel);
      }
    } else if(fullFocusButtonText !== undefined){
      setFullFocusButtonText(undefined);
    }
  }, [isFullFocus, gridRef, hasFocused, panelHeight]);

  const styles = {
    width,
    height,
    objectFit: 'contain',
    position: 'absolute' as Position,
    transition: '0.2s all'
  };
  return {
    gridRef,
    height,
    getPosition,
    styles,
    fullFocusButtonText,
    onResize,
    onClickHideMembers,
    hiddenClass,
    tileIds
  };
};