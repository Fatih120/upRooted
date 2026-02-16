import {closestCenter, DndContext} from '@dnd-kit/core';
import './codecSorter.css';
import {SortableContext, useSortable, verticalListSortingStrategy} from '@dnd-kit/sortable';
import {CSS} from '@dnd-kit/utilities';
import {useCodecSorterModel} from './useCodecSorterModel';

export type SortableChipProps = {
  id: string;
  label: string;
  onRemove: () => void;
};

export type Option = { value: string; label: string };

export type SortableMultiSelectProps = {
  options: Option[];
  value: string[];
  onChange: (next: string[]) => void;
};

const SortableChip = ({id, label, onRemove}: SortableChipProps) => {
  const {attributes, listeners, setNodeRef, transform, transition} =
    useSortable({id});

  const style: React.CSSProperties = {
    transform: CSS.Transform.toString(transform),
    transition
  };

  return (
    <li ref={setNodeRef}
        style={style}
        className={'sortable-chip'}
    >
      <span  {...attributes} {...listeners}>
        <span aria-hidden>☰ </span>
        <span>{label}</span>
      </span>
      <button
        type="button"
        onClick={e => {
          e.preventDefault();
          e.stopPropagation();
          onRemove();
        }}
        style={{
          border: 'none',
          background: 'transparent',
          cursor: 'pointer',
          padding: 0,
          lineHeight: 1,
          width: 'fit-content'
        }}
      >
        ×
      </button>
    </li>
  );
};

const CodecSorter = ({options, onChange, value}: SortableMultiSelectProps) => {
  const {
    handleAdd,
    availableOptions,
    sensors,
    handleDragEnd,
    handleRemove
  } = useCodecSorterModel(options, value, onChange);

  return (
    <div className={'sortable-multi-select'}>
      <select onChange={handleAdd}
              value={undefined}>
        <option value={undefined}
                disabled>
          Select codecs…
        </option>
        {availableOptions.map((option: { value: string, label: string }) => (
          <option key={option.value}
                  value={option.value}>
            {option.label}
          </option>
        ))}
      </select>
      <DndContext
        sensors={sensors}
        collisionDetection={closestCenter}
        onDragEnd={handleDragEnd}
      >
        <SortableContext
          items={value}
          strategy={verticalListSortingStrategy}
        >
          <ul
            style={{
              listStyle: 'none',
              padding: 0,
              margin: 0,
              display: 'flex',
              flexWrap: 'wrap',
              gap: 4
            }}
          >
            {value.map((selectedValue: string) => (
              <SortableChip
                key={selectedValue}
                id={selectedValue}
                label={
                  options.find((option: { value: string }) => option.value === selectedValue)
                    ?.label ?? selectedValue
                }
                onRemove={() => handleRemove(selectedValue)}
              />
            ))}
          </ul>
        </SortableContext>
      </DndContext>
    </div>
  );
};

export default CodecSorter;
