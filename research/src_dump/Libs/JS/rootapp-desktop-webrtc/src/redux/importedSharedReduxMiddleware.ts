import {
  BaseStreamService,
  ExtraWebRtcArgument,
  WebRtcAppDispatch,
  webRtcMiddleware,
  WebRtcRootState,
} from '@rootplatform/apiclient-webrtc-store';
import { createListenerMiddleware, ListenerMiddlewareInstance } from '@reduxjs/toolkit';

export const importedSharedReduxMiddleware = <T extends BaseStreamService>(
    streamService: T
): ListenerMiddlewareInstance<WebRtcRootState, WebRtcAppDispatch, ExtraWebRtcArgument> => {
    const middleware: ListenerMiddlewareInstance<WebRtcRootState, WebRtcAppDispatch, ExtraWebRtcArgument> = createListenerMiddleware<
    WebRtcRootState,
    WebRtcAppDispatch,
    ExtraWebRtcArgument
    >({
    extra: {
            streamService: streamService
        }
  });
  webRtcMiddleware(middleware);

  return middleware;
};
