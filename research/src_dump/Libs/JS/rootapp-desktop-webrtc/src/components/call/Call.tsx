import './call.css';
import {PanelGroup, PanelResizeHandle} from 'react-resizable-panels';
import {DEFAULT_PANEL_HEIGHT_PERCENTAGE} from '../../constants';
import {DebugPanel} from '../../mocks/debugPanel/DebugPanel.tsx';
import {AudioStream} from '../streams/audioStream/AudioStream.tsx';
import {FocusedPanel} from './focusedPanel/FocusedPanel';
import {UnfocusedPanel} from './unfocusedPanel/UnfocusedPanel.tsx';
import {useCallModel} from './useCallModel.ts';

/**
 * Component to render the call, including audio, video, and screen share media streams.
 * It is composed of two resizable panels, one for the focused tile and one for the unfocused tiles.
 * If there is no focused tile, just the unfocused tiles panel is shown, and vice versa.
 */
export const Call = () => {
  const {
    isDebugMode,
    unfocusedTileIds,
    focusedTileId,
    audioStreamIds,
    theme,
    onDragging,
    panelGroupRef,
    unfocusedPanelRef
  } = useCallModel();

  return (
    <div
      className={'call'}
      data-theme={theme}>
      <PanelGroup
        id={'call'}
        className={'panel-group'}
        autoSaveId={'rootapp-webrtc-layout'}
        ref={panelGroupRef}
        direction={'vertical'}>
        {focusedTileId && <FocusedPanel
          defaultHeight={unfocusedTileIds?.length ? 100 - DEFAULT_PANEL_HEIGHT_PERCENTAGE : 100}/> }
        {/* Resize bar */}
        {focusedTileId && unfocusedTileIds?.length && <PanelResizeHandle
          id={'handle'}
          className={`resize-handle`}
          onDragging={onDragging}/>
        }
        {unfocusedTileIds && <UnfocusedPanel
          panelRef={unfocusedPanelRef}
          defaultHeight={focusedTileId ? DEFAULT_PANEL_HEIGHT_PERCENTAGE : 100}/>
        }
        {/* Debug panel with mock events */}
        {isDebugMode &&
            <DebugPanel/>}
      </PanelGroup>
      {/* All audio */}
      {
        audioStreamIds?.map((streamId: string) => (
          <AudioStream
            key={streamId}
            streamId={streamId}/>
        ))
      }
    </div>
  );
};

export default Call;
