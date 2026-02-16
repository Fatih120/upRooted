import {createSlice, Draft, PayloadAction} from '@reduxjs/toolkit';
import {IUserResponse, UserGuid} from '@rootplatform/apiclient-internal';
import {TileId} from '@rootplatform/apiclient-webrtc-store';
import {noop} from 'lodash';
import {ScreenQualityMode, VolumeBoosterSettings} from '../types';
import {usersAdapter} from './selectors';

export interface DesktopWebRtcState {
  isLoadingProfile: boolean;
  users: ReturnType<typeof usersAdapter.getInitialState>;
  isFullFocus: boolean;
  screenQualityMode?: ScreenQualityMode;
  screenContentHint?: string;
  isScreenDismissed?: boolean;
}

const initialState: DesktopWebRtcState = {
  isLoadingProfile: false,
  users: usersAdapter.getInitialState({}),
  isFullFocus: false,
  screenQualityMode: ScreenQualityMode.GAMING,
  screenContentHint: 'motion',
  isScreenDismissed: false
};

export const desktopWebRtcSlice = createSlice({
  name: 'desktopWebRtc',
  initialState,
  reducers: {
    requestProfileUpdates(state: Draft<DesktopWebRtcState>, action: PayloadAction<{
      userIds: UserGuid[]
    }>) {
      noop(state, action);
    },
    applyProfileUpdates(state: Draft<DesktopWebRtcState>, action: PayloadAction<{
      users: IUserResponse[]
    }>) {
      usersAdapter.upsertMany(state.users, action.payload.users);
    },
    clickFocus(state: Draft<DesktopWebRtcState>, action: PayloadAction<{ tileId: TileId }>) {
      noop(state, action);
    },
    clickUsername(state: Draft<DesktopWebRtcState>, action: PayloadAction<{
      tileId: TileId, coordinates: {
        x: number, y: number
      }
    }>) {
      noop(state, action);
    },
    rightClick(state: Draft<DesktopWebRtcState>, action: PayloadAction<{
      tileId: TileId, coordinates: {
        x: number, y: number
      }
    }>) {
      noop(state, action);
    },
    setNoiseGateThreshold(state: Draft<DesktopWebRtcState>, action: PayloadAction<{ threshold: number }>) {
      noop(state, action);
    },
    setDenoisePower(state: Draft<DesktopWebRtcState>, action: PayloadAction<{ power: number }>) {
      noop(state, action);
    },
    setScreenQualityMode(state: Draft<DesktopWebRtcState>, action: PayloadAction<{
      screenQualityMode: ScreenQualityMode
    }>) {
      state.screenQualityMode = action.payload.screenQualityMode;
    },
    toggleFullFocus(state: Draft<DesktopWebRtcState>, action: PayloadAction<{ isFullFocus?: boolean }>) {
      state.isFullFocus = action.payload.isFullFocus ?? !state.isFullFocus;
    },
    setScreenContentHint(state: Draft<DesktopWebRtcState>, action: PayloadAction<{ contentHint: string }>) {
      const {contentHint} = action.payload;
      if (contentHint == null || contentHint === state.screenContentHint) return;

      state.screenContentHint = contentHint;
    },
    setScreenDismissed(state: Draft<DesktopWebRtcState>, action: PayloadAction<{isDismissed: boolean}>) {
      state.isScreenDismissed = action.payload?.isDismissed;
    },
    customizeVolumeBooster(state: Draft<DesktopWebRtcState>, action: PayloadAction<VolumeBoosterSettings>) {
      noop(state, action);
    }
  }
});

export const {
  requestProfileUpdates,
  applyProfileUpdates,
  clickFocus,
  clickUsername,
  rightClick,
  setNoiseGateThreshold,
  setDenoisePower,
  setScreenQualityMode,
  toggleFullFocus,
  setScreenContentHint,
  setScreenDismissed,
  customizeVolumeBooster
} = desktopWebRtcSlice.actions;
