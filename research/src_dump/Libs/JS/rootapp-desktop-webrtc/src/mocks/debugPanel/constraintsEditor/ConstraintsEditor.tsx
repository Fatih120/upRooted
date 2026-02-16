import './constraintsEditor.css';
import {Dialog, ErrorIcon, FormControl} from '@rootplatform/assets';
import {Button} from 'primereact/button';
import {ConstraintsFormSection} from './ConstraintsFormSection';
import {audioFormGroups, otherDisplayFormGroup, screenFormGroups, videoFormGroups} from './constraintTypes';
import {useConstraintsEditorModel} from './useConstraintsEditorModel';

const OverconstrainedError = () => (
  <div className={'overconstrained-error'}>
    <ErrorIcon/>
    <span>Overconstrained - no media matches the requested constraints. Please change your settings.</span>
  </div>
);

const ConstraintsEditor = () => {
  const {
    onSubmitUserMediaConstraints,
    onSubmitDisplayConstraints,
    userMediaDialogRef,
    displayDialogRef,
    displayMediaForm,
    userMediaForm,
    userMediaOverconstrained,
    displayMediaOverconstrained,
    resetUserMediaForm,
    resetDisplayMediaForm,
    userMediaFormKey,
    displayMediaFormKey,
    populateUserMediaForm,
    populateDisplayMediaForm
  } = useConstraintsEditorModel();

  const openUserMediaDialog = () => {
    populateUserMediaForm();
    userMediaDialogRef?.current?.open();
  };

  const openDisplayMediaDialog = () => {
    populateDisplayMediaForm();
    displayDialogRef?.current?.open();
  };

  return (<>
    <div className={'constraints-editor'}>
      <div className={'constraints-button-group'}>
        <Button className={'cancel-button'}
                onClick={openUserMediaDialog}>
          Edit User Media Constraints</Button>
        {userMediaOverconstrained && <OverconstrainedError/>}
      </div>
      <div className={'constraints-button-group'}>
        <Button className={'cancel-button'}
                onClick={openDisplayMediaDialog}>
          Edit Display Media Constraints</Button>
        {displayMediaOverconstrained && <OverconstrainedError/>}
      </div>
        <Dialog ref={userMediaDialogRef}
                submitButtons={['Reset', 'Update']}
                closeButton={'Cancel'}
                closeOnSubmit={[false, true]}
                title={'Edit User Media Constraints'}
                onSubmit={[resetUserMediaForm, userMediaForm.handleSubmit(onSubmitUserMediaConstraints)]}>
          <div className={'constraints-form'}
               key={`user-media-${userMediaFormKey}`}>
            <ConstraintsFormSection
              form={userMediaForm}
              groups={videoFormGroups}
              sectionHeader={'Camera Constraints (video)'}/>
            <ConstraintsFormSection
              form={userMediaForm}
              groups={audioFormGroups}
              sectionHeader={'Microphone Constraints (audio)'}/>
          </div>
        </Dialog>
      <Dialog ref={displayDialogRef}
              submitButtons={['Reset', 'Update']}
              closeButton={'Cancel'}
              closeOnSubmit={[false, true]}
              title={'Edit Display Media Constraints'}
              onSubmit={[resetDisplayMediaForm, displayMediaForm.handleSubmit(onSubmitDisplayConstraints)]}>
        <div className={'constraints-form'}
             key={`display-media-${displayMediaFormKey}`}>
          <ConstraintsFormSection
            form={displayMediaForm}
            groups={screenFormGroups}
            sectionHeader={'Screen Share Constraints'}/>
          <ConstraintsFormSection
            form={displayMediaForm}
            groups={audioFormGroups}
            sectionHeader={'Screen Audio Constraints'}/>
          <div className={'constraints-form-section'}>
            <div className={'section-header'}>{'Other Screen Constraints (experimental)'}</div>
            <div className={'grid-row-2'}>{
              otherDisplayFormGroup.map(({field, options}) => (
                <FormControl
                  key={field}
                  name={field}
                  inputType={'dropdown'}
                    options={options}
                  control={displayMediaForm.control}
                  label={field}/>))
            }
            </div>
          </div>
        </div>
      </Dialog>
    </div>
    </>
  );
};

export default ConstraintsEditor;
