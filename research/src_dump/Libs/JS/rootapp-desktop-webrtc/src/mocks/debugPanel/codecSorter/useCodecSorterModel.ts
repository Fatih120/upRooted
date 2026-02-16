import {DragEndEvent, PointerSensor, useSensor, useSensors} from '@dnd-kit/core';
import {arrayMove} from '@dnd-kit/sortable';
import {Option} from './CodecSorter';

export const useCodecSorterModel = (options: Option[],
                                    value: string[],
                                    onChange: (next: string[]) => void) => {
  const sensors = useSensors(useSensor(PointerSensor));
  const availableOptions = options.filter(
    option => !value.includes(option.value)
  );

  const handleAdd = (event: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedValue = event.target.value;
    if (!selectedValue) return;
    if (!value.includes(selectedValue)) {
      onChange([...value, selectedValue]);
    }
    event.target.value = '';
  };

  const handleRemove = (removeValue: string) => {
    onChange(value.filter(item => item !== removeValue));
  };

  const handleDragEnd = (event: DragEndEvent) => {
    const {active, over} = event;
    if (!over || active.id === over.id) return;

    const oldIndex = value.indexOf(active.id as string);
    const newIndex = value.indexOf(over.id as string);
    if (oldIndex === -1 || newIndex === -1) return;
    onChange(arrayMove(value, oldIndex, newIndex));
  };
  return {handleAdd, availableOptions, sensors, handleDragEnd, value, handleRemove};
};
