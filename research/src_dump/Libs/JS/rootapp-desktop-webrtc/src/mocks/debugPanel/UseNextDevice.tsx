import {useEffect, useRef, useState} from 'react';

// Mock Helper to cycle through input/output devices
export const useNextDevice = () => {
  const allMediaDevices = useRef({
    audioinput: [] as Array<MediaDeviceInfo>,
    videoinput: [] as Array<MediaDeviceInfo>,
    audiooutput: [] as Array<MediaDeviceInfo>,
  });

  const selectedMediaIndices = useRef({
    audioinput: 0,
    videoinput: 0,
    audiooutput: 0
  });

  const [nextIds, setNextIds] = useState({
    audioinput: 0,
    videoinput: 0,
    audiooutput: 0
  });

  const selectNextDevice = (kind: MediaDeviceKind): MediaDeviceInfo | undefined => {
    const devices = allMediaDevices.current[kind];
    if (!devices.length) return undefined;

    const nextIndex = (selectedMediaIndices.current[kind] + 1) % devices.length;
    selectedMediaIndices.current[kind] = nextIndex;
    setNextIds(prev => ({ ...prev, [kind]: nextIndex }));

    return devices[nextIndex];
  };

  useEffect(() => {
    navigator.mediaDevices.enumerateDevices().then(devices => {
      const grouped = {
        audioinput: [] as MediaDeviceInfo[],
        videoinput: [] as MediaDeviceInfo[],
        audiooutput: [] as MediaDeviceInfo[]
      };
      devices.forEach(device => {
        grouped[device.kind].push(device);
    });
      allMediaDevices.current = grouped;

    setNextIds({
        audioinput: grouped.audioinput.length > 1 ? 1 : 0,
        videoinput: grouped.videoinput.length > 1 ? 1 : 0,
        audiooutput: grouped.audiooutput.length > 1 ? 1 : 0
      });
    });
  }, []);

  return { allMediaDevices, getNextDevice: selectNextDevice, nextIds };
};
