import {
  AudioTrackConstraints,
  DisplayMediaStreamConstraints,
  ScreenTrackConstraints,
  selectDisplayMediaConstraints,
  selectUserMediaConstraints,
  UserMediaStreamConstraints,
  VideoTrackConstraints
} from '@rootplatform/apiclient-webrtc-store';
import {DialogRef} from '@rootplatform/assets';
import {useCallback, useRef, useState} from 'react';
import {useForm} from 'react-hook-form';
import {useAppSelector} from '../../../hooks';
import {selectDisplayMediaOverconstrained, selectScreenQualityMode, selectUserMediaOverconstrained} from '../../../redux/selectors';
import {DisplayMediaConstraintsForm, UserMediaConstraintsForm} from './constraintTypes';
import {RootState} from '../../../redux/store';
import {AUDIO_CONSTRAINTS, VIDEO_CONSTRAINTS} from '../../../constants';
import {ScreenQualityMode} from '../../../types';

type Constraint = AudioTrackConstraints | VideoTrackConstraints | ScreenTrackConstraints;
type ConstraintForm = UserMediaConstraintsForm | DisplayMediaConstraintsForm;
type ConstraintValue = { exact?: unknown; ideal?: unknown; min?: unknown; max?: unknown } | unknown;
const audioConstraintKeys = ['autoGainControl', 'channelCount', 'echoCancellation', 'latency', 'noiseSuppression', 'sampleRate', 'sampleSize', 'volume', 'audioGroupId'];
const videoConstraintKeys = ['width', 'height', 'frameRate', 'aspectRatio', 'resizeMode', 'facingMode', 'backgroundBlur', 'videoGroupId'];
const screenConstraintKeys = ['width', 'height', 'frameRate', 'aspectRatio', 'resizeMode', 'displaySurface', 'logicalSurface', 'cursor', 'screenPixelRatio', 'restrictOwnAudio', 'suppressLocalAudioPlayback'];

const constraintToFormValue = (value: unknown) => {
  if (value == null) return undefined;
  if (typeof value === 'boolean') return value;
  return String(value);
};

const constraintToFormFields = (constraint: ConstraintValue, fieldName: string): Record<string, unknown> => {
  if (constraint == null) return {};
  if (typeof constraint === 'object' && constraint !== null) {
    const range = constraint as { exact?: unknown; ideal?: unknown; min?: unknown; max?: unknown };
    return {
      ...(range.exact != null ? {[`${fieldName}Exact`]: constraintToFormValue(range.exact)} : {}),
      ...(range.ideal != null ? {[`${fieldName}Ideal`]: constraintToFormValue(range.ideal)} : {}),
      ...(range.min != null ? {[`${fieldName}Min`]: constraintToFormValue(range.min)} : {}),
      ...(range.max != null ? {[`${fieldName}Max`]: constraintToFormValue(range.max)} : {})
    };
  }
  return {[`${fieldName}Ideal`]: constraintToFormValue(constraint)};
};

const constraintsToFormData = (
  constraints: Constraint | undefined,
  keys: string[],
  prefix?: string
): Record<string, unknown> => {
  if (!constraints) return {};
  return keys.reduce((acc, key) => {
    const fieldName = prefix ? `${prefix}${key.charAt(0).toUpperCase()}${key.slice(1)}` : key;
    const constraintKey = key === 'audioGroupId' || key === 'videoGroupId' || key === 'screenGroupId' ? 'groupId' : key;
    const value = (constraints as Record<string, unknown>)[constraintKey];
    return {...acc, ...constraintToFormFields(value, fieldName)};
  }, {});
};

export const useConstraintsEditorModel = () => {
  const userMediaDialogRef = useRef<DialogRef>(null);
  const displayDialogRef = useRef<DialogRef>(null);
  const userMediaForm = useForm<UserMediaConstraintsForm>();
  const displayMediaForm = useForm<DisplayMediaConstraintsForm>();
  const userMediaOverconstrained = useAppSelector(selectUserMediaOverconstrained);
  const displayMediaOverconstrained = useAppSelector(selectDisplayMediaOverconstrained);
  const currentUserMediaConstraints = useAppSelector((state: RootState) => selectUserMediaConstraints(state.webRtc));
  const currentDisplayMediaConstraints = useAppSelector((state: RootState) => selectDisplayMediaConstraints(state.webRtc));
  const screenQualityMode = useAppSelector(selectScreenQualityMode);
  const [userMediaFormKey, setUserMediaFormKey] = useState(0);
  const [displayMediaFormKey, setDisplayMediaFormKey] = useState(0);

  const populateUserMediaForm = useCallback(() => {
    const mergedAudio = {...AUDIO_CONSTRAINTS, ...currentUserMediaConstraints?.audio} as AudioTrackConstraints;
    const mergedVideo = {...VIDEO_CONSTRAINTS.hd, ...currentUserMediaConstraints?.video} as VideoTrackConstraints;
    const audioFormData = constraintsToFormData(mergedAudio, audioConstraintKeys);
    const videoFormData = constraintsToFormData(mergedVideo, videoConstraintKeys);
    userMediaForm.reset({...audioFormData, ...videoFormData} as UserMediaConstraintsForm);
  }, [currentUserMediaConstraints, userMediaForm]);

  const populateDisplayMediaForm = useCallback(() => {
    const screenDefaults = screenQualityMode === ScreenQualityMode.PRESENTATION ? VIDEO_CONSTRAINTS.screenText : VIDEO_CONSTRAINTS.screen;
    const mergedScreen = {...screenDefaults, ...currentDisplayMediaConstraints?.video} as ScreenTrackConstraints;
    const mergedAudio = {...AUDIO_CONSTRAINTS, ...currentDisplayMediaConstraints?.audio} as AudioTrackConstraints;
    const screenFormData = constraintsToFormData(mergedScreen, screenConstraintKeys);
    const audioFormData = constraintsToFormData(mergedAudio, audioConstraintKeys);
    const displayOptions = {
      ...(currentDisplayMediaConstraints?.selfBrowserSurface ? {selfBrowserSurface: currentDisplayMediaConstraints.selfBrowserSurface} : {}),
      ...(currentDisplayMediaConstraints?.systemAudio ? {systemAudio: currentDisplayMediaConstraints.systemAudio} : {}),
      ...(currentDisplayMediaConstraints?.windowAudio ? {windowAudio: currentDisplayMediaConstraints.windowAudio} : {}),
      ...(currentDisplayMediaConstraints?.surfaceSwitching ? {surfaceSwitching: currentDisplayMediaConstraints.surfaceSwitching} : {}),
      ...(currentDisplayMediaConstraints?.monitorTypeSurfaces ? {monitorTypeSurfaces: currentDisplayMediaConstraints.monitorTypeSurfaces} : {})
    };
    displayMediaForm.reset({...screenFormData, ...audioFormData, ...displayOptions} as DisplayMediaConstraintsForm);
  }, [currentDisplayMediaConstraints, displayMediaForm, screenQualityMode]);
  const hasValue = (fieldValue: unknown) => fieldValue != null && fieldValue !== '';
  const toConstraintValue = (fieldValue: unknown) => (fieldValue === '' || fieldValue == null) ? undefined : fieldValue;

  const buildRange = <T extends ConstraintForm>(form: T, fieldName: string) => {
    let name = fieldName;
    if (fieldName.includes('GroupId')) {
      name = 'groupId' as keyof Constraint;
    }

    const ideal = toConstraintValue(form[`${fieldName}Ideal` as keyof T]);
    const exact = toConstraintValue(form[`${fieldName}Exact` as keyof T]);
    const min = toConstraintValue(form[`${fieldName}Min` as keyof T]);
    const max = toConstraintValue(form[`${fieldName}Max` as keyof T]);
    return [ideal, exact, min, max].some(val => hasValue(val)) && {
      [name]: {
        ...(hasValue(ideal) ? {ideal} : {}),
        ...(hasValue(exact) ? {exact} : {}),
        ...(hasValue(min) ? {min} : {}),
        ...(hasValue(max) ? {max} : {})
      }
    };
  };

  const buildConstraintGroup = (form: ConstraintForm, keys: string[]) => keys.reduce((acc, key) => ({...acc, ...buildRange(form, key)}), {});


  const buildDisplayOptions = (form: DisplayMediaConstraintsForm) => {
    const opts = {
      ...form.selfBrowserSurface ? {selfBrowserSurface: form.selfBrowserSurface} : {},
      ...form.systemAudio ? {systemAudio: form.systemAudio} : {},
      ...form.windowAudio ? {windowAudio: form.windowAudio} : {},
      ...form.surfaceSwitching ? {surfaceSwitching: form.surfaceSwitching} : {},
      ...form.monitorTypeSurfaces ? {monitorTypeSurfaces: form.monitorTypeSurfaces} : {}
    };
    return Object.keys(opts).length && opts;
  };

  const onSubmitUserMediaConstraints = (formData: UserMediaConstraintsForm) => {
    const audio = buildConstraintGroup(formData, audioConstraintKeys);
    const video = buildConstraintGroup(formData, videoConstraintKeys);
    const constraints = {
      ...audio ? {audio} : {},
      ...video ? {video} : {}
    };

    if (!Object.keys(constraints).length) return;

    window.__nativeToWebRtc.setUserMediaConstraints(constraints as UserMediaStreamConstraints);
  };

  const onSubmitDisplayConstraints = (formData: DisplayMediaConstraintsForm) => {
    const video = buildConstraintGroup(formData, screenConstraintKeys);
    const audio = buildConstraintGroup(formData, audioConstraintKeys);

    const constraints: DisplayMediaStreamConstraints = {
      ...(video ? {video} : {}),
      ...(audio ? {audio} : {}),
      ...buildDisplayOptions(formData)
    };

    if (!Object.keys(constraints).length) return;

    window.__nativeToWebRtc.setDisplayMediaConstraints(constraints);
  };

  const resetUserMediaForm = () => {
    userMediaForm.reset({} as UserMediaConstraintsForm, {keepDefaultValues: false});
    setUserMediaFormKey(prev => prev + 1);
    window.__nativeToWebRtc.setUserMediaConstraints({});
  };

  const resetDisplayMediaForm = () => {
    displayMediaForm.reset({} as DisplayMediaConstraintsForm, {keepDefaultValues: false});
    setDisplayMediaFormKey(prev => prev + 1);
    window.__nativeToWebRtc.setDisplayMediaConstraints({});
  };

  return {
    userMediaDialogRef,
    displayDialogRef,
    onSubmitUserMediaConstraints,
    onSubmitDisplayConstraints,
    userMediaForm,
    displayMediaForm,
    userMediaOverconstrained,
    displayMediaOverconstrained,
    resetUserMediaForm,
    resetDisplayMediaForm,
    userMediaFormKey,
    displayMediaFormKey,
    populateUserMediaForm,
    populateDisplayMediaForm
  };
};
