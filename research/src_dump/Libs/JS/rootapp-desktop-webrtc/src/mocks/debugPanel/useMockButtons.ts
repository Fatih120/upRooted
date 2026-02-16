import {AuthToken, IUserResponse, UserGuid} from '@rootplatform/apiclient-internal';
import {InitializeWebRtcPayload, log, Theme} from '@rootplatform/apiclient-webrtc-store';
import {ScreenQualityMode} from '../../types';

export const useMockButtons = (
  nextUser: IUserResponse | undefined,
  initParams: InitializeWebRtcPayload,
  nextTheme: Theme,
  getNextDevice: (kind: MediaDeviceKind) => MediaDeviceInfo | undefined,
  allMediaDevices: React.MutableRefObject<{
    audioinput: MediaDeviceInfo[];
    videoinput: MediaDeviceInfo[];
    audiooutput: MediaDeviceInfo[];
  }>,
  nextIds: { audioinput: number; videoinput: number; audiooutput: number }
) => {
  const adminButtons = nextUser
    ? {
      'Kick': () => window.__nativeToWebRtc.kick(nextUser.userId),
      'Admin Mute': () => window.__nativeToWebRtc.setAdminMute(nextUser.userId, true),
      'Admin Unmute': () => window.__nativeToWebRtc.setAdminMute(nextUser.userId, false),
      'Admin Deafen': () => window.__nativeToWebRtc.setAdminDeafen(nextUser.userId, true),
      'Admin Undeafen': () => window.__nativeToWebRtc.setAdminDeafen(nextUser.userId, false)
    }
    : {};

  const getUpcomingDeviceLabel = (kind: MediaDeviceKind) => {
    const devices = allMediaDevices.current[kind];
    const currentIndex = nextIds[kind];
    const nextIndex = (currentIndex + 1) % devices.length;
    return devices[nextIndex]?.label ? `: ${devices[nextIndex].label}` : '';
  };

  const buttons = {
    'Start Call': () => {
      const token = new AuthToken(import.meta.env.VITE_AUTH_TOKEN);
      initParams.currentUserId = token.userId as UserGuid;
      window.__nativeToWebRtc.initialize(initParams);
    },
    'End Call': () => window.__nativeToWebRtc.disconnect(),
    'Turn Screen On': () => window.__nativeToWebRtc.setIsScreenShareOn(true),
    'Turn Screen On With Audio': () => window.__nativeToWebRtc.setIsScreenShareOn(true, true),
    'Turn Screen Off': () => window.__nativeToWebRtc.setIsScreenShareOn(false),
    'Mute Audio': () => window.__nativeToWebRtc.setMute(true),
    'Unmute Audio': () => window.__nativeToWebRtc.setMute(false),
    'Deafen Audio': () => window.__nativeToWebRtc.setDeafen(true),
    'Undeafen Audio': () => window.__nativeToWebRtc.setDeafen(false),
    'Turn Video On': () => window.__nativeToWebRtc.setIsVideoOn(true),
    'Turn Video Off': () => window.__nativeToWebRtc.setIsVideoOn(false),
    'Raise Hand': () => window.__nativeToWebRtc.setHandRaised(true),
    'Lower Hand': () => window.__nativeToWebRtc.setHandRaised(false),
    'List Devices': () =>
      navigator.mediaDevices.enumerateDevices().then(devices => log('Found Devices: ', devices)),
    'Change Theme': () => window.__nativeToWebRtc.setTheme(nextTheme),
    'Turn Push To Talk Mode On': () => window.__nativeToWebRtc.setPushToTalkMode(true),
    'Turn Push To Talk Mode Off': () => window.__nativeToWebRtc.setPushToTalkMode(false),
    'Push To Talk': () => window.__nativeToWebRtc.setPushToTalk(true),
    'Stop Push To Talk': () => window.__nativeToWebRtc.setPushToTalk(false),
    'Use Screen Quality Gaming Mode': () => window.__nativeToWebRtc.setScreenQualityMode(ScreenQualityMode.GAMING),
    'Use Screen Quality Presentation Mode': () => window.__nativeToWebRtc.setScreenQualityMode(ScreenQualityMode.PRESENTATION),
    [`Next Video Input${getUpcomingDeviceLabel('videoinput')}`]: () => {
      const device = getNextDevice('videoinput');
      if (device) window.__nativeToWebRtc.updateVideoDeviceId(device.deviceId);
    },
    [`Next Audio Input${getUpcomingDeviceLabel('audioinput')}`]: () => {
      const device = getNextDevice('audioinput');
      if (device) window.__nativeToWebRtc.updateAudioInputDeviceId(device.deviceId);
    },
    [`Next Audio Output${getUpcomingDeviceLabel('audiooutput')}`]: () => {
      const device = getNextDevice('audiooutput');
      if (device) window.__nativeToWebRtc.updateAudioOutputDeviceId(device.deviceId);
    },
    ...adminButtons
  };

  return {buttons, adminButtons};
};

