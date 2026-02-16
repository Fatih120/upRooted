import './pendingOverlay.css';
import {TileId} from '@rootplatform/apiclient-webrtc-store';
import {COLOR, LoadingIcon} from '@rootplatform/assets';
import {AnimatedRingIcon} from '../../../assets/icons/AnimatedRingIcon';
import {usePendingOverlayModel} from './usePendingOverlayModel';

const PendingOverlay = ({tileId}: { tileId: TileId }) => {
  const {isLoading, isRinging, size, username, bottomDistance, onClickUsername} = usePendingOverlayModel(tileId);

  return (
    <div className={'pending-overlay'}>
      {isLoading ?
        <LoadingIcon fontSize={size}
                     color={COLOR.textPrimary}
                     style={{animation: 'loadingSpinner 1s linear infinite'}}/> : null}
      {isRinging ? <div className={'ringing-bar'} style={{bottom: `${bottomDistance}px`}}>
        <AnimatedRingIcon fontSize={size}/> <div className={'ring-username'} color={COLOR.textPrimary} onClick={event => onClickUsername(event)}>{username}</div></div> : null}
    </div>
  );
};

export default PendingOverlay;
