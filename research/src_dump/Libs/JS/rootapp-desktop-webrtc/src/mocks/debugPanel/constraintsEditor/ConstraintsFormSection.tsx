import {DropdownOption, FormControl} from '@rootplatform/assets';
import {UseFormReturn} from 'react-hook-form';

export type FormSectionGroup = { header: string, field: string, inputType: string, options?: Array<DropdownOption> };

export const ConstraintsFormSection = ({form, groups, sectionHeader}: {
  form: UseFormReturn,
  groups: Array<FormSectionGroup>,
  sectionHeader: string
}) => {


  return (
    <div className={'constraints-form-section'}>
      <div className={'section-header'}>{sectionHeader}</div>
      {groups.map(({header, field, inputType, options}) => {
        let fields;
        if (inputType === 'number') {
          const fieldTypes = ['Exact', 'Ideal', 'Min', 'Max'];
          fields = sectionHeader.includes('Screen') ? fieldTypes.slice(1) : fieldTypes;
        } else {
          fields = sectionHeader.includes('Screen') ? ['Ideal'] : ['Exact', 'Ideal'];
        }

        return (
          <div key={header}
               className={inputType === 'number' || inputType === 'checkbox' ? 'grid-row-4' : 'grid-row-2'}>
            <div className={'header'}>{header}</div>
            {fields.map((fieldType) => (
              <FormControl
                key={`${field}${fieldType}`}
                name={`${field}${fieldType}`}
                inputType={inputType as typeof FormControl.prototype.props.inputType}
                {...(options ? {options} : {})}
                {...(inputType === 'text' ? {
                  register: form.register,
                  formState: form.formState
                } : {control: form.control})}
                label={fieldType}
              />
            ))}
          </div>
        );
      })}
    </div>
  );
};
