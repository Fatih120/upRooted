import {configureStore, ListenerMiddlewareInstance, ThunkAction, ThunkDispatch} from '@reduxjs/toolkit';
import {
  disconnectProtectedMiddleware,
  loggerMiddleware,
  WebRtcAppDispatch,
  webRtcReducer,
} from '@rootplatform/apiclient-webrtc-store';
import * as Sentry from '@sentry/react';
import {Action, combineReducers} from 'redux';
import {StreamService} from '../services';
import {bridgeMiddleware, ExtraWebRtcToNativeArgument} from './bridgeMiddleware';
import {desktopMiddleware} from './desktopMiddleware';
import {importedSharedReduxMiddleware} from './importedSharedReduxMiddleware';
import {sentryMiddleware} from './sentryMiddleware';
import {desktopWebRtcSlice} from './slice';

// The stream service must be created before the store is, so it can be passed to the middleware,
// but the store must be created before the service can dispatch any actions, so that is passed in below
export const streamService: StreamService = new StreamService();
export const rootReducer = combineReducers({
  webRtc: webRtcReducer,
  desktopWebRtc: desktopWebRtcSlice.reducer
});

export const webRtcDesktopClientStore = configureStore({
  reducer: rootReducer,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      thunk: {
        extraArgument: {
          client: {
            webRtcClient: window.__webRtcClient
          },
          streamService
        }
      }
    }).concat([loggerMiddleware, sentryMiddleware]).prepend([
      disconnectProtectedMiddleware,
      bridgeMiddleware(streamService).middleware,
      importedSharedReduxMiddleware(streamService).middleware,
      desktopMiddleware(streamService).middleware
    ]),
  enhancers: (getDefaultEnhancers) => getDefaultEnhancers().concat(Sentry.createReduxEnhancer())
});

export type AppDispatch = typeof webRtcDesktopClientStore.dispatch;
export type RootState = ReturnType<typeof rootReducer>;
export type AppThunk<ReturnType = void> = ThunkAction<
  ReturnType,
  RootState,
  unknown,
  Action<string>
>;

export type DesktopWebRtcAppDispatch = WebRtcAppDispatch & ThunkDispatch<RootState, unknown, Action>
export type BridgeMiddleware = ListenerMiddlewareInstance<
  RootState,
  DesktopWebRtcAppDispatch,
  ExtraWebRtcToNativeArgument
>;

export const dispatch: WebRtcAppDispatch = webRtcDesktopClientStore.dispatch as WebRtcAppDispatch;
// Now that it is available, we pass in the store's dispatch method to the stream service
streamService.dispatch = dispatch;
