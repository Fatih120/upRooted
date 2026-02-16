import './contentHintPicker.css';
import {FormControl} from '@rootplatform/assets';
import {Button} from 'primereact/button';
import {useContentHintPickerModel} from './useContentHintPickerModel';

const ContentHintPicker = () => {
  const {control, handleSubmit, options, onSubmit} = useContentHintPickerModel();

  return (
    <form onSubmit={handleSubmit(onSubmit)}
          className={'content-hint-picker'}>
      <FormControl inputType={'dropdown'}
                   placeholder={'Select contentHint...'}
                   control={control}
                   name={'contentHint'}
                   label={'Content Hint'}
                   options={options}/>
      <Button className={'submit-button'}
              type={'submit'}>Update</Button>
    </form>
  );
};

export default ContentHintPicker;
