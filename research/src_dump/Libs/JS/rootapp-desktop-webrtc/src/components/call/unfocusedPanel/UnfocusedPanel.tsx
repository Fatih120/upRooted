import {TileId} from '@rootplatform/apiclient-webrtc-store';
import './unfocusedPanel.css';
import {ChevronDownIcon} from '@rootplatform/assets';
import {RefObject} from 'react';
import {ImperativePanelHandle, Panel} from 'react-resizable-panels';
import {MIN_PANEL_HEIGHT_PERCENTAGE} from '../../../constants';
import {Stream} from '../../streams/Stream.tsx';
import {StreamFontSizeProvider} from '../../streams/StreamFontSizeProvider.tsx';
import {useUnfocusedPanelModel} from './useUnfocusedPanelModel.ts';

/**
 * Component to render the call panel, including audio and video media streams.
 */
export const UnfocusedPanel = ({defaultHeight, panelRef, className}: {
  defaultHeight: number,
  panelRef: RefObject<ImperativePanelHandle>
  className?: string,
}) => {
  const {
    gridRef,
    height,
    getPosition,
    styles,
    fullFocusButtonText,
    onResize,
    hiddenClass,
    tileIds,
    onClickHideMembers
  } = useUnfocusedPanelModel();

  return (
    <Panel
      id={'unfocused'}
      ref={panelRef}
      onResize={onResize}
      collapsible={true}
      collapsedSize={0}
      order={1}
      defaultSize={defaultHeight}
      minSize={MIN_PANEL_HEIGHT_PERCENTAGE}
      className={['panel', className].join(' ')}>
      <StreamFontSizeProvider streamHeight={height || 0}>
        {
          fullFocusButtonText &&
            <button className={`full-focus-button ${hiddenClass}`}
                    onClick={onClickHideMembers}>
              {fullFocusButtonText}
                <ChevronDownIcon fontSize={20}/>
            </button>
        }
        <div
          className={`stream-grid unfocused`}
          ref={gridRef}>
          {
            tileIds?.map((tileId: TileId, index) => (
              <Stream tileId={tileId}
                      key={tileId}
                      streamDimensionsStyle={{
                        ...getPosition(index),
                        ...styles
                      }}
              />
            ))
          }
        </div>
      </StreamFontSizeProvider>
    </Panel>
  );
};
