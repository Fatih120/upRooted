import {
  selectIsCurrentAudioStreamId,
  selectIsCurrentScreenAudioStreamId,
  selectIsStreamEnabled,
  selectPreferredAudioOutputId
} from '@rootplatform/apiclient-webrtc-store';
import {useEffect, useRef} from 'react';
import {useAppSelector, useVolumeBooster} from '../../../hooks';
import {streamService} from '../../../redux';
import {ElementVolumeBooster} from '../../../services';

export const useAudioStreamModel = (streamId: string) => {
  const audioRef = useRef<HTMLAudioElement>(null);
  const isCurrentAudio = useAppSelector(state => selectIsCurrentAudioStreamId(state.webRtc, streamId));
  const isCurrentScreenAudio = useAppSelector(state => selectIsCurrentScreenAudioStreamId(state.webRtc, streamId));
  const audioOutputId = useAppSelector(state => selectPreferredAudioOutputId(state.webRtc));
  const isEnabled = useAppSelector(state => selectIsStreamEnabled(state.webRtc, streamId));
  const stream = streamService.getStream(streamId);

  // Volume boost for remote streams (not current user's audio)
  useVolumeBooster(audioRef, streamId, !isCurrentAudio && !isCurrentScreenAudio && !!stream);

  // Set audio output device when it changes
  useEffect(() => {
    if (!isCurrentAudio && !isCurrentScreenAudio && audioOutputId) {
      const element = audioRef?.current;
      const isBoosterAttached = element ? ElementVolumeBooster.isAttached(element) : false;

      // If volume booster is attached, set its AudioContext sinkId
      // Audio flows through AudioContext, not the element, so we need to change the context's output
      if (isBoosterAttached) {
        ElementVolumeBooster.setSinkId(audioOutputId).catch(console.error);
      }

      // Also set element sinkId for when booster is not attached
      if (element && element.sinkId !== audioOutputId) {
        element.setSinkId(audioOutputId).catch(console.error);
      }
    }
  }, [audioOutputId, isCurrentAudio, isCurrentScreenAudio]);

  // Handle srcObject and enabled/muted state
  useEffect(() => {
    const currRef = audioRef.current;
    if (!currRef || !stream) return;

    const getAudioTrack = (ms: MediaStream | null) =>
      ms?.getAudioTracks?.()?.[0] ?? ms?.getTracks?.()?.find(t => t.kind === 'audio') ?? null;

    const previousMuted = currRef.muted;

    // Always set srcObject - the volume booster reads from the element's stream
    // and routes audio to AudioContext.destination (with element muted to prevent double-play)
    currRef.srcObject = stream;
    currRef.muted = !isEnabled;

    const applyEnabledState = () => {
      const track = getAudioTrack(currRef.srcObject as MediaStream) ?? getAudioTrack(stream);
      if (!track) return;
      track.enabled = isEnabled;
      currRef.muted = !isEnabled;
    };

    applyEnabledState();

    // Needed to fix a known issue in most browsers where starting a track with enabled=false doesn't take effect
    // until a state transition occurs
    currRef.addEventListener('playing', applyEnabledState);
    currRef.addEventListener('loadedmetadata', applyEnabledState);
    const frameListener = window.requestAnimationFrame(applyEnabledState);

    return () => {
      window.cancelAnimationFrame(frameListener);
      currRef.removeEventListener('playing', applyEnabledState);
      currRef.removeEventListener('loadedmetadata', applyEnabledState);
      currRef.muted = previousMuted;
      currRef.srcObject = null;
    };
  }, [stream, isEnabled]);

  return {
    isCurrentAudio,
    audioRef
  };
};
