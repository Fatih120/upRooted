export const VIDEO_CONSTRAINTS: Record<string, MediaTrackConstraints> = {
  qvga: {width: {ideal: 320}, height: {ideal: 240}, frameRate: {ideal: 30}},
  vga: {width: {ideal: 640}, height: {ideal: 480}, frameRate: {ideal: 30}},
  hd: {width: {ideal: 960}, height: {ideal: 720}, frameRate: {ideal: 30}},
  fhd: {width: {ideal: 1920}, height: {ideal: 1080}, frameRate: {ideal: 30}},
  screen: {width: {ideal: 1280}, height: {ideal: 720}, frameRate: {ideal: 30}},
  screenText: {frameRate: {max: 5}},
  qhd: {width: {ideal: 2560}, height: {ideal: 1440}, frameRate: {ideal: 15}},
  uhd: {width: {ideal: 3840}, height: {ideal: 2160}, frameRate: {ideal: 15}}
} as const;

export const AUDIO_CONSTRAINTS = {
  echoCancellation: true,
  autoGainControl: true
} as const;

