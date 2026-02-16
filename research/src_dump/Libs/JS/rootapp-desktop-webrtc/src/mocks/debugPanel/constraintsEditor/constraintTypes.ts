import {DropdownOption} from '@rootplatform/assets';

export enum InclusionEnum {
  INCLUDE = 'include',
  EXCLUDE = 'exclude'
}
export enum WindowAudioEnum {
  EXCLUDE = 'exclude',
  WINDOW = 'window',
  SYSTEM = 'system'
}
export enum DisplaySurfaceEnum {
  MONITOR = 'monitor',
  WINDOW = 'window',
  APPLICATION = 'application',
  BROWSER = 'browser'
}
export enum CursorEnum {
  ALWAYS = 'always',
  MOTION = 'motion',
  NEVER = 'never'
}
export enum ResizeModeEnum {
  NONE = 'none',
  CROP_AND_SCALE = 'crop-and-scale'
}
export const BooleanValue = {
  DEFAULT: undefined as undefined,
  TRUE: true as const,
  FALSE: false as const
};
export enum FacingModeEnum {
  USER = 'user',
  ENVIRONMENT = 'environment',
  LEFT = 'left',
  RIGHT = 'right'
}

export const inclusionOptions = [
  {label: InclusionEnum.INCLUDE, value: InclusionEnum.INCLUDE},
  {label: InclusionEnum.EXCLUDE, value: InclusionEnum.EXCLUDE}
] as Array<DropdownOption>;
export const windowAudioOptions = [
  {label: WindowAudioEnum.EXCLUDE, value: WindowAudioEnum.EXCLUDE},
  {label: WindowAudioEnum.WINDOW, value: WindowAudioEnum.WINDOW},
  {label: WindowAudioEnum.SYSTEM, value: WindowAudioEnum.SYSTEM}
] as Array<DropdownOption>;
export const displaySurfaceOptions = [
  {label: DisplaySurfaceEnum.MONITOR, value: DisplaySurfaceEnum.MONITOR},
  {label: DisplaySurfaceEnum.WINDOW, value: DisplaySurfaceEnum.WINDOW},
  {label: DisplaySurfaceEnum.APPLICATION, value: DisplaySurfaceEnum.APPLICATION},
  {label: DisplaySurfaceEnum.BROWSER, value: DisplaySurfaceEnum.BROWSER}
] as Array<DropdownOption>;
export const cursorOptions = [
  {label: CursorEnum.ALWAYS, value: CursorEnum.ALWAYS},
  {label: CursorEnum.MOTION, value: CursorEnum.MOTION},
  {label: CursorEnum.NEVER, value: CursorEnum.NEVER}
] as Array<DropdownOption>;
export const resizeModeOptions = [
  {label: ResizeModeEnum.NONE, value: ResizeModeEnum.NONE},
  {label: ResizeModeEnum.CROP_AND_SCALE, value: ResizeModeEnum.CROP_AND_SCALE}
] as Array<DropdownOption>;
export const facingModeOptions = [
  {label: FacingModeEnum.USER, value: FacingModeEnum.USER},
  {label: FacingModeEnum.ENVIRONMENT, value: FacingModeEnum.ENVIRONMENT},
  {label: FacingModeEnum.LEFT, value: FacingModeEnum.LEFT},
  {label: FacingModeEnum.RIGHT, value: FacingModeEnum.RIGHT}
] as Array<DropdownOption>;
export const booleanOptions = [
  {label: '(default)', value: BooleanValue.DEFAULT},
  {label: 'true', value: BooleanValue.TRUE},
  {label: 'false', value: BooleanValue.FALSE}
] as Array<DropdownOption>;

type AudioForm = {
  autoGainControlExact?: boolean;
  autoGainControlIdeal?: boolean;

  channelCountExact?: number;
  channelCountIdeal?: number;
  channelCountMin?: number;
  channelCountMax?: number;

  echoCancellationExact?: boolean;
  echoCancellationIdeal?: boolean;

  latencyExact?: number;
  latencyIdeal?: number;
  latencyMin?: number;
  latencyMax?: number;

  noiseSuppressionExact?: boolean;
  noiseSuppressionIdeal?: boolean;

  sampleRateExact?: number;
  sampleRateIdeal?: number;
  sampleRateMin?: number;
  sampleRateMax?: number;

  sampleSizeExact?: number;
  sampleSizeIdeal?: number;
  sampleSizeMin?: number;
  sampleSizeMax?: number;

  volumeExact?: number;
  volumeIdeal?: number;
  volumeMin?: number;
  volumeMax?: number;

  audioGroupIdExact?: string;
  audioGroupIdIdeal?: string;
}

export type UserMediaConstraintsForm = {
  // video
  facingModeIdeal?: FacingModeEnum;

  backgroundBlurIdeal?: boolean;

  widthIdeal?: number;
  widthMin?: number;
  widthMax?: number;

  heightIdeal?: number;
  heightMin?: number;
  heightMax?: number;

  frameRateIdeal?: number;
  frameRateMin?: number;
  frameRateMax?: number;

  aspectRatioIdeal?: number;
  aspectRatioMin?: number;
  aspectRatioMax?: number;

  resizeModeIdeal?: ResizeModeEnum;

  videoGroupIdIdeal?: string;
} & AudioForm; // audio

export type DisplayMediaConstraintsForm = {
  // screen
  displaySurfaceExact?: DisplaySurfaceEnum;
  displaySurfaceIdeal?: DisplaySurfaceEnum;

  logicalSurfaceExact?: boolean;
  logicalSurfaceIdeal?: boolean;

  cursorExact?: CursorEnum;
  cursorIdeal?: CursorEnum;

  screenPixelRatioExact?: number;
  screenPixelRatioIdeal?: number;
  screenPixelRatioMin?: number;
  screenPixelRatioMax?: number;

  restrictOwnAudioExact?: boolean;
  restrictOwnAudioIdeal?: boolean;

  suppressLocalAudioPlaybackExact?: boolean;
  suppressLocalAudioPlaybackIdeal?: boolean;

  widthExact?: number;
  widthIdeal?: number;
  widthMin?: number;
  widthMax?: number;

  heightExact?: number;
  heightIdeal?: number;
  heightMin?: number;
  heightMax?: number;

  frameRateExact?: number;
  frameRateIdeal?: number;
  frameRateMin?: number;
  frameRateMax?: number;

  aspectRatioExact?: number;
  aspectRatioIdeal?: number;
  aspectRatioMin?: number;
  aspectRatioMax?: number;

  resizeModeExact?: ResizeModeEnum;
  resizeModeIdeal?: ResizeModeEnum;

  // other display
  selfBrowserSurface?: InclusionEnum;
  systemAudio?: InclusionEnum;
  windowAudio?: WindowAudioEnum;
  surfaceSwitching?: InclusionEnum;
  monitorTypeSurfaces?: InclusionEnum;
} & AudioForm; // screen audio

export const audioFormGroups = [
  {header: 'autoGainControl:', field: 'autoGainControl', inputType: 'dropdown', options: booleanOptions},
  {header: 'channelCount:', field: 'channelCount', inputType: 'number'},
  {header: 'echoCancellation:', field: 'echoCancellation', inputType: 'dropdown', options: booleanOptions},
  {header: 'latency:', field: 'latency', inputType: 'number'},
  {header: 'noiseSuppression:', field: 'noiseSuppression', inputType: 'dropdown', options: booleanOptions},
  {header: 'sampleRate:', field: 'sampleRate', inputType: 'number'},
  {header: 'sampleSize:', field: 'sampleSize', inputType: 'number'},
  {header: 'volume:', field: 'volume', inputType: 'number'},
  {header: 'groupId:', field: 'audioGroupId', inputType: 'text'}
];

export const videoFormGroups = [
  {header: 'facingMode:', field: 'facingMode', inputType: 'dropdown', options: facingModeOptions},
  {header: 'backgroundBlur:', field: 'backgroundBlur', inputType: 'dropdown', options: booleanOptions},
  {header: 'width:', field: 'width', inputType: 'number'},
  {header: 'height:', field: 'height', inputType: 'number'},
  {header: 'frameRate:', field: 'frameRate', inputType: 'number'},
  {header: 'aspectRatio:', field: 'aspectRatio', inputType: 'number'},
  {header: 'resizeMode:', field: 'resizeMode', inputType: 'dropdown', options: resizeModeOptions},
  {header: 'groupId:', field: 'videoGroupId', inputType: 'text'}
];

export const screenFormGroups = [
  {header: 'displaySurface:', field: 'displaySurface', inputType: 'dropdown', options: displaySurfaceOptions},
  {header: 'logicalSurface:', field: 'logicalSurface', inputType: 'dropdown', options: booleanOptions},
  {header: 'cursor:', field: 'cursor', inputType: 'dropdown', options: cursorOptions},
  {header: 'screenPixelRatio:', field: 'screenPixelRatio', inputType: 'number'},
  {header: 'restrictOwnAudio:', field: 'restrictOwnAudio', inputType: 'dropdown', options: booleanOptions},
  {header: 'suppressLocalAudioPlayback:', field: 'suppressLocalAudioPlayback', inputType: 'dropdown', options: booleanOptions},
  {header: 'width:', field: 'width', inputType: 'number'},
  {header: 'height:', field: 'height', inputType: 'number'},
  {header: 'frameRate:', field: 'frameRate', inputType: 'number'},
  {header: 'aspectRatio:', field: 'aspectRatio', inputType: 'number'},
  {header: 'resizeMode:', field: 'resizeMode', inputType: 'dropdown', options: resizeModeOptions},
  {header: 'groupId:', field: 'screenGroupId', inputType: 'text'}
];

export const otherDisplayFormGroup: Array<{
  field: keyof DisplayMediaConstraintsForm,
  options: Array<DropdownOption>
}> = [{field: 'selfBrowserSurface', options: inclusionOptions},
  {field: 'systemAudio', options: inclusionOptions},
  {field: 'windowAudio', options: windowAudioOptions},
  {field: 'surfaceSwitching', options: inclusionOptions},
  {field: 'monitorTypeSurfaces', options: inclusionOptions}];