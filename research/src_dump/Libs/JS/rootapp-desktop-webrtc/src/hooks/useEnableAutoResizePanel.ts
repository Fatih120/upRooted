import {RefObject, useCallback, useRef} from 'react';
import {ImperativePanelGroupHandle} from 'react-resizable-panels';
import {pxToPercent} from '../services';

/**
 * Hook to stop react-resizable-panel from auto-resizing the unfocused panel while the window is resized.
 */
export const useEnableAutoResizePanel = () => {
  const panelGroupRef = useRef<ImperativePanelGroupHandle>(null);

  const setAutosizeEnabled = useCallback((shouldEnableResize: boolean) => {
    const unfocusedPanel = document.getElementById('unfocused');
    const focusedPanel = document.getElementById('focused');

    if (!unfocusedPanel || !focusedPanel) {
      return;
    }

    if (!shouldEnableResize) {
      disableResizing(focusedPanel, unfocusedPanel);
    } else {
      activateResizing(focusedPanel, unfocusedPanel, panelGroupRef);
    }
  }, []);
  return {panelGroupRef, setAutosizeEnabled};
};

/**
 * Disables auto-resizing of the unfocused panel while the window is resized.
 * @param focusedPanel
 * @param unfocusedPanel
 */
const disableResizing = (focusedPanel: HTMLElement, unfocusedPanel: HTMLElement) => {
  const unfocusedHeight = unfocusedPanel?.clientHeight;
  const focusedHeight = focusedPanel?.clientHeight;
  focusedPanel.style.flex = `1 1 ${focusedHeight}px`;
  unfocusedPanel.style.flex = `0 0 ${unfocusedHeight}px`;
};

/**
 * Activates auto-resizing of the panels when the resize handle is dragged.
 * @param focusedPanel
 * @param unfocusedPanel
 * @param panelGroupRef
 */
const activateResizing = (focusedPanel: HTMLElement, unfocusedPanel: HTMLElement, panelGroupRef: RefObject<ImperativePanelGroupHandle | null>) => {
  const unfocusedHeight = unfocusedPanel?.clientHeight;
  const focusedHeight = focusedPanel?.clientHeight;
  const totalHeight = unfocusedHeight + focusedHeight;
  const focusedFlex = pxToPercent(focusedHeight, totalHeight);
  const unfocusedFlex = pxToPercent(unfocusedHeight, totalHeight);

  focusedPanel.style.flex = `${focusedFlex} 1 0px`;
  unfocusedPanel.style.flex = `${unfocusedFlex} 1 0px`;

  // This informs react-resizable-panels of the most up-to-date numbers.
  panelGroupRef.current?.setLayout([focusedFlex, unfocusedFlex]);
};
