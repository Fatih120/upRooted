import './debugPanel.css';
import {TileType} from '@rootplatform/apiclient-webrtc-store';
import {ChevronDownIcon, ChevronUpIcon, SIZE} from '@rootplatform/assets';
import {Button} from 'primereact/button';
import CodecSorter from './codecSorter/CodecSorter';
import ConstraintsEditor from './constraintsEditor/ConstraintsEditor';
import ContentHintPicker from './contentHintPicker/ContentHintPicker';
import {useDebugPanelModel} from './useDebugPanelModel';
import VolumeSlider from './VolumeSlider';

export const DebugPanel = () => {
  const {
    expanded,
    setExpanded,
    nextUserName,
    buttons,
    adminButtons,
    codecOptions,
    selectedCodecs,
    onChangeCodecs,
    submitCodecs,
    pause,
    globalVolume,
    userInputVolume,
    currentUserId
  } = useDebugPanelModel();

  return (
    <div className={'debug-panel'}>
      <div className={'call-actions-header'}>
        <Button className={'actions-expand-button submit-button'}
                onClick={() => setExpanded(!expanded)}>
        <span> {expanded ? <span><ChevronDownIcon fontSize={SIZE.icon16}/> Collapse</span> :
          <span><ChevronUpIcon fontSize={SIZE.icon16}/> Expand</span>} Debug Panel</span>
        </Button>
        <div className={'controls-container'}>
                <Button className={'pause-button'}
                tooltip={'Pause debugger'}
                tooltipOptions={{position: 'left'}}
                onClick={pause()}>
          <div style={{border: '1px solid red', marginRight: SIZE.p16}}>
            <div style={{opacity: '.5', backgroundColor: 'red', height: SIZE.icon16, width: SIZE.icon16}}></div>
          </div>
      </Button>
        </div>
      </div>
      <div className={`call-actions ${expanded ? 'expanded' : 'not-expanded'}`}>
        <span className={'volume-container'}>Output <VolumeSlider direction={'horizontal'}
                                                                  savedVolume={globalVolume}
                                                                  setVolume={volume => window.__nativeToWebRtc?.setOutputVolume?.(volume)}/></span>
        <span className={'volume-container'}>Input <VolumeSlider direction={'horizontal'}
                                                                 savedVolume={userInputVolume}
                                                                 setVolume={volume => window.__nativeToWebRtc?.setTileVolume?.(currentUserId, TileType.PRIMARY, volume)}/></span>

        {Object.entries(buttons).map(([label, onClick]) => (
          <Button className={'cancel-button'}
                  style={{fontSize: SIZE.font12}}
                  key={label}
                  onClick={onClick}>
            {label} {Object.keys(adminButtons).some(k => label.startsWith(k)) ? nextUserName : ''}
          </Button>
        ))}
        <div className={'debug-form-card'}>
          <label>Choose & Sort Preferred Codecs</label>
          <CodecSorter options={codecOptions}
                       value={selectedCodecs}
                       onChange={onChangeCodecs}/>
          <Button className={'submit-button'}
                  style={{width: 'fit-content'}}
                  onClick={submitCodecs}>Update</Button>
        </div>
        <div className={'debug-form-card'}>
          <ConstraintsEditor/>
        </div>
        <div className={'debug-form-card'}>
          <ContentHintPicker/>
        </div>
      </div>
    </div>
  );
};
