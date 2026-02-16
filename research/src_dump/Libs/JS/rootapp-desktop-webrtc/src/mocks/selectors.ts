import {createSelector} from '@reduxjs/toolkit';
import {IUserResponse} from '@rootplatform/apiclient-internal';
import {selectCurrentUserId} from '@rootplatform/apiclient-webrtc-store';
import {RootState, selectAllUsers, selectUserById} from '../redux';

export const selectFirstUserNotCurrent = createSelector([
    (state: RootState) => selectAllUsers(state.desktopWebRtc),
    (state: RootState) => selectCurrentUserId(state.webRtc)
  ],
  (users, currentUserId) => currentUserId && users?.find(user => user.userId !== currentUserId) as IUserResponse
);
export const selectFirstUserIdNotCurrent = (state: RootState) =>
  selectFirstUserNotCurrent(state)?.userId;
export const selectFirstUsernameNotCurrent = createSelector([
    (state: RootState) => state,
    selectFirstUserIdNotCurrent
  ],
  (state, nextUserId) => nextUserId ? selectUserById(state.desktopWebRtc, nextUserId)?.username : null
);
