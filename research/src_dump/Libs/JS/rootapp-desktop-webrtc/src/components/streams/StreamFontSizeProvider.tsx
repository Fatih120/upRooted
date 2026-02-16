import React, {createContext, useContext, useMemo} from 'react';

const StreamFontSizeContext = createContext<number>(16);
const StreamHeightContext = createContext<number>(0);

export const StreamFontSizeProvider = ({streamHeight, children}: {
  streamHeight: number,
  children: React.ReactNode
}) => {
  const fontSize = useMemo(() =>
      Math.max(10, Math.min(14, (streamHeight || 80) / 8)),
    [streamHeight]);

  return (
    <StreamHeightContext.Provider value={streamHeight}>
      <StreamFontSizeContext.Provider value={fontSize}>
        {children}
      </StreamFontSizeContext.Provider>
    </StreamHeightContext.Provider>
  );
};

export const useStreamFontSize = () => {
  const context = useContext(StreamFontSizeContext);
  if (context == undefined) {
    throw new Error('useStreamFontSize must be used within a StreamFontSizeProvider');
  }
  return context;
};

export const useStreamHeight = () => {
  const context = useContext(StreamHeightContext);
  if (context == undefined) {
    throw new Error('useStreamHeight must be used within a StreamFontSizeProvider');
  }
  return context;
};

export const useStreamIconSize = () => {
  const context = useStreamFontSize();
  const streamHeight = useStreamHeight();
  if(streamHeight < 60) {
    return context;
  }

  return context * 1.5;
}

export const useLargeStreamIconSize = () => {
  const context = useStreamFontSize();
  const streamHeight = useStreamHeight();
  if (streamHeight < 60) {
    return context * 1.5;
  }

  return context * 3;
}