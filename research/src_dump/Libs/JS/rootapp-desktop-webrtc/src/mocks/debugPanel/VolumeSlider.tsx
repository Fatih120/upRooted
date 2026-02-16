import {throttle} from 'lodash';
import {useEffect, useMemo, useState} from 'react';
import {MAX_VOLUME, MIN_VOLUME} from '@rootplatform/apiclient-webrtc-store';

const VolumeSlider = ({direction = 'horizontal', savedVolume, setVolume}: {
  direction: 'vertical' | 'horizontal',
  savedVolume: number,
  setVolume: (volume: number) => void
}) => {
  const debounceMs = 300;
  const label = '🔊';
  const showValue = true;
  const [sliderValue, setSliderValue] = useState<number>(savedVolume);
  const volume = savedVolume;

  const onChange = useMemo(() => throttle((newVolume: number) => {
    setVolume(newVolume);
  }, debounceMs), [debounceMs, volume]);

  useEffect(() => {
    if (volume === sliderValue) return;

    setSliderValue(volume);
  }, [volume]);

  return (
    <div className={'volume'}
         data-direction={direction}>
      <span className={'volume-label'}>{label}</span>
      <input
        className={'volume-range'}
        type={'range'}
        min={MIN_VOLUME}
        max={MAX_VOLUME}
        step={0.01}
        value={sliderValue}
        onChange={(e) => {
          setSliderValue(parseFloat(e.target.value));
          onChange(parseFloat(e.target.value));
        }}
      />
      {showValue && <span className={'volume-value'}>{Math.round(sliderValue * 100)}%</span>}
    </div>
  );
};

export default VolumeSlider;
