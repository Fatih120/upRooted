import {
  selectAudioStreamIds,
  selectFocusedTileId,
  selectIsDebugMode,
  selectTheme,
  selectUnfocusedTileIds
} from '@rootplatform/apiclient-webrtc-store';
import {useEffect, useRef} from 'react';
import {ImperativePanelHandle} from 'react-resizable-panels';
import {ENABLE_AUTOSIZE_TIMEOUT, MIN_PANEL_HEIGHT_PERCENTAGE} from '../../constants';
import {useAppSelector, useEnableAutoResizePanel} from '../../hooks';
import {selectIsFullFocus} from '../../redux';

export const useCallModel = () => {
  const unfocusedPanelRef = useRef<ImperativePanelHandle>(null);
  const isDebugMode = useAppSelector(state => selectIsDebugMode(state.webRtc));
  const unfocusedTileIds = useAppSelector(state => selectUnfocusedTileIds(state.webRtc));
  const focusedTileId = useAppSelector(state => selectFocusedTileId(state.webRtc));
  const audioStreamIds = useAppSelector(state => selectAudioStreamIds(state.webRtc));
  const theme = useAppSelector((state) => selectTheme(state.webRtc));
  const isFullFocus = useAppSelector(state => selectIsFullFocus(state));
  const {panelGroupRef, setAutosizeEnabled} = useEnableAutoResizePanel();
  const isResizing = useRef<boolean>(false);

  const onDragging = (isDragging: boolean) => {
    setAutosizeEnabled(isDragging);
    isResizing.current = isDragging;
    const unfocusedPanelHeight = Number(unfocusedPanelRef.current?.getSize() || 0);
    if (!isDragging && unfocusedPanelHeight && unfocusedPanelHeight < MIN_PANEL_HEIGHT_PERCENTAGE) {
      unfocusedPanelRef.current?.resize(MIN_PANEL_HEIGHT_PERCENTAGE);
    }
  };

  useEffect(() => {
    let timeout: number;
    if (unfocusedPanelRef.current?.getSize() == null || !focusedTileId) return;

    const unfocusedPanelHeight = Number(unfocusedPanelRef.current?.getSize() || 0);

    const setPanelExpanded = (shouldExpand: boolean) => {
      setAutosizeEnabled(true);
      unfocusedPanelRef?.current?.[shouldExpand ? 'expand' : 'collapse']?.();
      clearTimeout(timeout);
      timeout = window.setTimeout(() => setAutosizeEnabled(false), ENABLE_AUTOSIZE_TIMEOUT);
    };

    if (isFullFocus && unfocusedPanelHeight > 0) {
      setPanelExpanded(false);
    } else if (!isFullFocus && unfocusedPanelHeight === 0) {
      setPanelExpanded(true);
    }

    return () => {
      clearTimeout(timeout);
    };
  }, [unfocusedPanelRef.current, isFullFocus, setAutosizeEnabled, isResizing.current]);

  return {
    onDragging,
    isDebugMode,
    unfocusedTileIds,
    focusedTileId,
    audioStreamIds,
    theme,
    panelGroupRef,
    unfocusedPanelRef
  };
};
