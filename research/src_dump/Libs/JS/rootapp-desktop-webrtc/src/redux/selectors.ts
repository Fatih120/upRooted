import {createEntityAdapter, EntityAdapter} from '@reduxjs/toolkit';
import {IUserResponse, UserGuid} from '@rootplatform/apiclient-internal';
import {OVERCONSTRAINED, selectUserIdByTileId, TileId, TRACK_TYPE} from '@rootplatform/apiclient-webrtc-store';
import {DesktopWebRtcState} from './slice';
import {RootState} from './store';

/**
 * Users are the unique list of people that have joined the call.
 * In the future, this can support each user joining from multiple memberDevices.
 */
export const usersAdapter: EntityAdapter<IUserResponse, UserGuid> = createEntityAdapter({
  selectId: (user: IUserResponse) => user.userId
});

export const {
  selectIds: selectUserIds,
  selectById: selectUserById,
  selectEntities: selectUserEntities,
  selectAll: selectAllUsers,
  selectTotal: selectTotalUsers
} = usersAdapter.getSelectors((state: DesktopWebRtcState) => state.users);

export const selectUserByTileId = (state: RootState, tileId: TileId) =>
  selectUserById(state.desktopWebRtc, selectUserIdByTileId(state.webRtc, tileId) as UserGuid);
export const selectUserImgByTileId = (state: RootState, tileId: TileId) =>
  selectUserByTileId(state, tileId)?.profilePictureAssetUri;
export const selectUsernameByTileId = (state: RootState, tileId: TileId) =>
  selectUserByTileId(state, tileId)?.username;
export const selectIsFullFocus = (state: RootState) => state?.desktopWebRtc?.isFullFocus
export const selectScreenQualityMode = (state: RootState) => state?.desktopWebRtc?.screenQualityMode
export const selectIsScreenPickerDismissed = (state: RootState) => !!state?.desktopWebRtc?.isScreenDismissed

export const selectUserMediaOverconstrained = (state: RootState) => {
  const errors = (state?.webRtc as any)?.errors || [];
  return errors.some((error: any) => {
    const trackType = error.meta?.trackType;
    return error.rootError?.name === OVERCONSTRAINED && (trackType === TRACK_TYPE.audio || trackType === TRACK_TYPE.video);
  });
};

export const selectDisplayMediaOverconstrained = (state: RootState) => {
  const errors = (state?.webRtc as any)?.errors || [];
  return errors.some((error: any) => {
    const trackType = error.meta?.trackType;
    return error.rootError?.name === OVERCONSTRAINED && (trackType === TRACK_TYPE.screen || trackType === TRACK_TYPE.screenAudio);
  });
};