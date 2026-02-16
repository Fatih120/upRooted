import {selectScaledVolumeByStreamId} from '@rootplatform/apiclient-webrtc-store';
import {RefObject, useEffect, useRef, useState} from 'react';
import {useAppSelector} from './useAppSelector';
import {ElementVolumeBooster} from '../services';

/**
 * Hook that attaches volume boost control (>100% support) to an audio element.
 * Uses ElementVolumeBooster with a shared AudioContext for performance.
 *
 * @param audioRef - Ref to the audio element
 * @param streamId - Stream ID to get volume from state
 * @param enabled - Whether to enable volume boosting (false for current user's audio)
 */
export const useVolumeBooster = (
  audioRef: RefObject<HTMLAudioElement | null>,
  streamId: string,
  enabled: boolean
) => {
  const setVolumeRef = useRef<((v: number) => void) | null>(null);
  const [element, setElement] = useState<HTMLAudioElement | null>(null);
  const volume = useAppSelector(state => selectScaledVolumeByStreamId(state.webRtc, streamId));

  // Track when the audio element becomes available and has a srcObject
  useEffect(() => {
    const el = audioRef.current;
    if (!el) return;

    const handleReady = () => {
      if (el.srcObject) {
        setElement(el);
      }
    };

    // Check if already ready
    handleReady();

    // Listen for when srcObject becomes available
    el.addEventListener('loadedmetadata', handleReady);
    return () => el.removeEventListener('loadedmetadata', handleReady);
  }, [audioRef.current?.srcObject]);

  // Attach/detach volume booster
  // Wait until volume is defined before attaching to ensure correct initial volume.
  // Including volume in deps ensures we attach with the correct value once it's available.
  // Note: ElementVolumeBooster.attach() is optimized to just update volume if already attached.
  useEffect(() => {
    if (!element || !enabled || volume == null) {
      return;
    }

    const {setVolume, detach} = ElementVolumeBooster.attach(element, volume);
    setVolumeRef.current = setVolume;

    return () => {
      detach();
      setVolumeRef.current = null;
    };
  }, [element, enabled, volume]);

  // Update volume when it changes (redundant when attach effect runs, but handles
  // the case where volume changes without triggering reattach)
  useEffect(() => {
    if (volume != null && setVolumeRef.current) {
      setVolumeRef.current(volume);
    }
  }, [volume]);
};

