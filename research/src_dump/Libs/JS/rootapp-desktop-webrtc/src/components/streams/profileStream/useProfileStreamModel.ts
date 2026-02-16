import {selectTileById, TileId, TileState} from '@rootplatform/apiclient-webrtc-store';
import {useAppSelector, useImage, useSpeakingClassName} from '../../../hooks';
import {selectUserImgByTileId, selectUsernameByTileId} from '../../../redux';
import {useLargeStreamIconSize} from '../StreamFontSizeProvider';

export const useProfileStreamModel = (tileId: TileId) => {
  const tile = useAppSelector((state) => selectTileById(state.webRtc.tiles, tileId));
  const isPending = tile?.tileState === TileState.LOADING || tile?.tileState === TileState.RINGING;
  const username = useAppSelector((state) =>
    selectUsernameByTileId(state, tileId)
  );
  const profileImgUrl = useAppSelector((state) =>
    selectUserImgByTileId(state, tileId)
  );

  const {imgRef, backgroundColor, isError} = useImage(profileImgUrl);
  const speakingClassName = useSpeakingClassName(tileId);
  const altImageSize = useLargeStreamIconSize();

  return {
    username,
    imgRef,
    imgFail: isError,
    backgroundColor,
    speakingClassName,
    altImageSize,
    isPending
  };
};
