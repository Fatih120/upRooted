import {CommunityGuid, DeviceGuid, MessageContainerGuid, UserGuid} from '@rootplatform/apiclient-internal';
import {
  InitializeWebRtcPayload,
  selectCurrentUserId,
  selectGlobalVolume,
  selectInputVolume,
  selectTheme
} from '@rootplatform/apiclient-webrtc-store';
import {useState} from 'react';
import {useAppSelector} from '../../hooks';
import {RootState} from '../../redux';
import {selectFirstUsernameNotCurrent, selectFirstUserNotCurrent} from '../selectors';
import {usePreferredCodecs} from './codecSorter/usePreferredCodecs';
import {useMockButtons} from './useMockButtons';
import {useNextDevice} from './UseNextDevice';

export const useDebugPanelModel = () => {
  const [expanded, setExpanded] = useState(false);
  const initParams = {
    theme: 'dark',
    callPlatform: 'desktop',
    currentDeviceId: import.meta.env.VITE_DEVICE_ID as DeviceGuid,
    communityId: import.meta.env.VITE_COMMUNITY_ID as CommunityGuid,
    containerId: import.meta.env.VITE_CONTAINER_ID as MessageContainerGuid,
    permissions: {
      channelVoiceKick: true,
      channelVoiceTalk: true,
      channelVoiceDeafenOther: true,
      channelVoiceMuteOther: true,
      channelVideoStreamMedia: true
    },
    logging: 'info-state-packet',
    webApiBaseUrl: import.meta.env.VITE_BASE_URL,
    isPushToTalkMode: false,
    debugMode: true
  } as InitializeWebRtcPayload;

  const nextUser = useAppSelector(selectFirstUserNotCurrent);
  const nextUserName = useAppSelector(selectFirstUsernameNotCurrent);
  const theme = useAppSelector((state: RootState) => selectTheme(state.webRtc));
  const nextTheme = theme === 'light' ? 'dark' : theme === 'dark' ? 'pure-dark' : 'light';
  const {allMediaDevices, getNextDevice, nextIds} = useNextDevice();
  const {
    buttons,
    adminButtons
  } = useMockButtons(nextUser, initParams, nextTheme, getNextDevice, allMediaDevices, nextIds);
  const {codecOptions, selectedCodecs, onChangeCodecs, submitCodecs} = usePreferredCodecs();
  const pause = () => {
    return () => {
      // eslint-disable-next-line no-debugger
      debugger
    };
  };
  const globalVolume = useAppSelector((state: RootState) => selectGlobalVolume(state.webRtc));
  const currentUserId = useAppSelector((state: RootState) => selectCurrentUserId(state.webRtc)) as UserGuid;
  const userInputVolume = useAppSelector((state: RootState) => selectInputVolume(state.webRtc));

  return {
    expanded,
    setExpanded,
    nextUserName,
    buttons,
    adminButtons,
    codecOptions,
    selectedCodecs,
    onChangeCodecs,
    submitCodecs,
    pause,
    globalVolume,
    userInputVolume,
    currentUserId
  };
};