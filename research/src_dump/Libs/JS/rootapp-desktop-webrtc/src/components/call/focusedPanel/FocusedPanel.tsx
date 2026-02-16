import {TileId} from '@rootplatform/apiclient-webrtc-store';
import './focusedPanel.css';
import {ChevronUpIcon} from '@rootplatform/assets';
import {RefObject} from 'react';
import {ImperativePanelHandle, Panel} from 'react-resizable-panels';
import {MIN_PANEL_HEIGHT_PERCENTAGE} from '../../../constants';
import {Stream} from '../../streams/Stream.tsx';
import {StreamFontSizeProvider} from '../../streams/StreamFontSizeProvider.tsx';
import {useFocusedPanelModel} from './useFocusedPanelModel.ts';

/**
 * Component to render the call panel, including audio and video media streams.
 */
export const FocusedPanel = ({defaultHeight, panelRef, className}: {
  defaultHeight: number,
  panelRef?: RefObject<ImperativePanelHandle>
  className?: string,
}) => {
  const {
    height,
    styles,
    fullFocusButtonText,
    hiddenClass,
    tileId,
    onResize,
    onClickShowMembers,
    gridRef
  } = useFocusedPanelModel();

  return (
    <Panel
      id={'focused'}
      ref={panelRef}
      order={0}
      onResize={onResize}
      collapsible={true}
      defaultSize={defaultHeight}
      minSize={MIN_PANEL_HEIGHT_PERCENTAGE}
      className={['panel', className].join(' ')}> <StreamFontSizeProvider streamHeight={height || window?.innerHeight}>
      {
        fullFocusButtonText &&
          <button className={`full-focus-button ${hiddenClass}`}
                  onClick={onClickShowMembers}>
            {fullFocusButtonText}
              <ChevronUpIcon fontSize={20}/>
          </button>
      }{

      <div className={`stream-grid focused`}
           ref={gridRef}>
        {tileId && <Stream tileId={tileId as TileId}
                           key={tileId}
                           streamDimensionsStyle={styles}
        />}
      </div>
    }    </StreamFontSizeProvider></Panel>

  );
};
