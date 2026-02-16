import {TileId} from '@rootplatform/apiclient-webrtc-store';
import {AvatarPlaceholderIcon} from '@rootplatform/assets';
import {CSSProperties} from 'react';
import {StreamDimensionsStyle} from '../../../types';
import PendingOverlay from '../pendingOverlay/PendingOverlay';
import {StreamOverlay} from '../streamOverlay/StreamOverlay';
import {useProfileStreamModel} from './useProfileStreamModel';
import './profileStream.css';

export const ProfileStream = ({
                                tileId,
                                dimensions
                              }: {
  tileId: TileId;
  dimensions?: StreamDimensionsStyle;
}) => {
  const {username, imgRef, imgFail, backgroundColor, speakingClassName, altImageSize, isPending} =
    useProfileStreamModel(tileId);

  return (
    <div style={dimensions as CSSProperties}
         className={speakingClassName}>
      <div className={'profile'}
           style={{backgroundColor}}>
        {imgFail ? (
          <div className={'alt-image'}>
            <AvatarPlaceholderIcon fontSize={altImageSize * 2}/>
          </div>
        ) : null}
        <img ref={imgRef}
             style={{width: imgFail ? '0' : 'initial'}}
             alt={username}/>
      </div>
      {isPending && <PendingOverlay tileId={tileId}/>}
      <StreamOverlay tileId={tileId}/>
    </div>
  );
};
