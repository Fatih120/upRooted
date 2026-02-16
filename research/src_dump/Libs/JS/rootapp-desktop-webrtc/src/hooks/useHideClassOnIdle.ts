import {useEffect, useState} from 'react';

export function useHideClassOnIdle(delayMs = 1500) {
  const [hidden, setHidden] = useState(false);

  useEffect(() => {
    let timeout: number;

    const setIdle = () => {
      setHidden(true);
      document?.body?.classList?.add('idle');
    };

    const reset = () => {
      setHidden(false);
      document?.body?.classList?.remove('idle');
      clearTimeout(timeout);
      timeout = window.setTimeout(setIdle, delayMs);
    };

    window.addEventListener('mousemove', reset);
    window.addEventListener('mouseout', setIdle);
    window.addEventListener('blur', setIdle);

    reset();

    return () => {
      window.removeEventListener('mousemove', reset);
      window.removeEventListener('mouseout', setIdle);
      window.removeEventListener('blur', setIdle);
      clearTimeout(timeout);
    };
  }, [delayMs]);

  return hidden ? 'hidden' : 'visible';
}