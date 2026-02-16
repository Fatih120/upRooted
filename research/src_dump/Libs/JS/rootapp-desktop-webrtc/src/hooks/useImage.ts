import ColorThief from 'colorthief';
import {useEffect, useRef, useState} from 'react';
import {isNative} from '../mocks';

/**
 * Hook to get an image along with its ideal background color
 * @param url
 */
export const useImage = (url?: string) => {
  const sizeParam = isNative() ? '?imageOptions=medium' : '/public';
  const imgRef = useRef<HTMLImageElement>(null);
  const [backgroundColor, setBackgroundColor] = useState<string>('transparent');
  const [isError, setIsError] = useState(false);

  useEffect(() => {
    if (imgRef.current && url) {
      imgRef.current.crossOrigin = 'Anonymous';
      imgRef.current.src = url + sizeParam;

      imgRef.current.onload = () => {
        if (imgRef.current?.src) {
          const color = new ColorThief().getColor(imgRef.current as HTMLImageElement);

          setBackgroundColor(color ? `rgb(${color[0]}, ${color[1]}, ${color[2]})` : 'transparent');
          setIsError(false);
        }
      };
      imgRef.current.onerror = () => {
        setIsError(true);
      };
    } else {
      setBackgroundColor('var(--color-background-secondary)');
    }
  }, [url]);

  return {imgRef, backgroundColor, isError};
};
