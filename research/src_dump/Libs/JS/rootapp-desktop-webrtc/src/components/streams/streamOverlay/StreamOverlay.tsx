import {TileId} from '@rootplatform/apiclient-webrtc-store';
import './streamOverlay.css';
import {
  COLOR,
  HeadphonesDenied2Icon,
  HeadphonesOffIcon,
  MicrophoneDenied2Icon,
  MicrophoneOffIcon
} from '@rootplatform/assets';
import {HandIcon} from '../../../assets/icons/HandIcon.tsx';
import {useStreamOverlayModel} from './useStreamOverlayModel.ts';

export const StreamOverlay = ({tileId}: { tileId: TileId }) => {
  const {
    isMuted,
    isDeafened,
    isAdminMuted,
    isAdminDeafened,
    isHandRaised,
    onClick,
    onRightClick,
    onClickUsername,
    onClickIcon,
    username,
    fontSize,
    isPrimaryTile,
    isFocusable,
    isRinging,
    hiddenClass,
  } = useStreamOverlayModel(tileId);

  return (
    <div className={'stream-overlay-container'}>
      <div className={`stream-overlay${isFocusable ? '' : ' only-stream'}`}
           onClick={event => onClick(event)}
           onContextMenu={event => onRightClick(event)}>
        {!isRinging && <div className={`username ${hiddenClass}`}
                            data-theme={'dark'}
                            style={{fontSize: `${fontSize}px`}}
                            onClick={event => onClickUsername(event)}>{username}
        </div>}
      </div>
      {isPrimaryTile ?
        <div className={'sound-icons'}
             data-theme={'dark'}>
          {isMuted || isAdminMuted ?
            <div className={'muted-icon'}
                 style={{fontSize: `${fontSize}px`}}
                 onClick={event => onClickIcon(event)}>
              {isAdminMuted ?
                <MicrophoneDenied2Icon color={COLOR.textPrimary}/> :
                <MicrophoneOffIcon color={COLOR.textPrimary}/>}
            </div> : null}
          {isDeafened || isAdminDeafened ?
            <div className={'deafened-icon'}
                 style={{fontSize: `${fontSize}px`}}
                 onClick={event => onClickIcon(event)}>
              {isAdminDeafened ?
                <HeadphonesDenied2Icon color={COLOR.textPrimary}/> :
                <HeadphonesOffIcon color={COLOR.textPrimary}/>}
            </div> : null}
        </div> : null}
      {isHandRaised ?
        <div className={'hand-raised'}
             data-theme={'dark'}
             style={{fontSize: `${fontSize}px`}}
             onClick={event => onClickIcon(event)}>
          <HandIcon/>
        </div> : null}
    </div>
  );
};