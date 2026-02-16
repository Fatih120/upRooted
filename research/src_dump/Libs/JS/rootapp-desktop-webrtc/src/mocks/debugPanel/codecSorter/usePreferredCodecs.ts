import {uniqBy} from 'lodash';
import {useState} from 'react';

export const usePreferredCodecs = () => {
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const options = uniqBy([...(globalThis as any)?.RTCRtpSender?.getCapabilities('audio')?.codecs || [], ...(globalThis as any)?.RTCRtpSender?.getCapabilities('video')?.codecs || []].map((codec) => {
    const codecString = `${codec.mimeType}${codec.sdpFmtpLine ? `;${codec.sdpFmtpLine}` : ''}`;
    return {label: codecString, value: codecString};
  }), 'value').sort();
  const [codecs, setCodecs] = useState<string[]>([]);
  const onChange = (next: string[]) => setCodecs(next);
  const submitCodecs = () => {
    const selectedCodecs = codecs.map(codec => {
      const props = codec?.split(';');
      const name: string = props[0]?.split('/')?.[1] || props[0] || '';
      const profileLevelIdIndex = props.findIndex(prop => prop.startsWith('profile-level-id'));
      const profileLevelId = profileLevelIdIndex > -1 ? props[profileLevelIdIndex].split('=')[1] : undefined;
      const packetizationModeIndex = props.findIndex(prop => prop.startsWith('packetization-mode'));
      const packetizationMode = packetizationModeIndex > -1 ? Number(props[packetizationModeIndex].split('=')[1]) : undefined;

      return {name, ...profileLevelId ? {profileLevelId} : {}, ...packetizationMode != null ? {packetizationMode} : {}};
    });
    window.__nativeToWebRtc.setPreferredCodecs(selectedCodecs);
  };

  return {
    selectedCodecs: codecs,
    codecOptions: options,
    onChangeCodecs: onChange,
    submitCodecs
  };
};