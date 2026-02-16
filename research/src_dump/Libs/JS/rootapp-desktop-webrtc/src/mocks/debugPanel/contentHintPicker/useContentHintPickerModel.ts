import {useForm} from 'react-hook-form';

export const useContentHintPickerModel = () => {
  const {control, handleSubmit} = useForm<{ contentHint: string }>();
  const onSubmit = (data: { contentHint: string }) => {
    window.__nativeToWebRtc.setScreenContentHint(data.contentHint);
  };
  const options = [{label: 'None', value: ''}, {label: 'Motion', value: 'motion'}, {
    label: 'Detail',
    value: 'detail'
  }, {label: 'Text', value: 'text'}];

  return {control, handleSubmit, onSubmit, options};
};
