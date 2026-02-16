import {Middleware, PayloadAction, UnknownAction} from '@reduxjs/toolkit';
import {UserGuid} from '@rootplatform/apiclient-internal';
import {
  formatState,
  initialize,
  InitializeWebRtcPayload,
  selectCurrentUserId,
  selectEnvironmentIds
} from '@rootplatform/apiclient-webrtc-store';
import * as Sentry from '@sentry/react';
import {isNative} from '../mocks';
import {selectUserById} from './selectors';
import {RootState, streamService} from './store';

const {logger} = Sentry;

let allLogs = '';

type SentryTags = {
  environment: string;
  currentUserId?: UserGuid;
  username?: string;
  actionType: string;
  channelId: string;
  communityId: string | 'DM';
  date: string;
  time: string;
  ms: string;
  stats?: RTCStatsReport;
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  error?: any;
}

export const sentryMiddleware: Middleware = (store: { getState: () => RootState }) =>
  (next: (action: unknown) => unknown) =>
    async (action: unknown) => {
      const result = next(action);
      let typedAction = action as UnknownAction;
      if (!typedAction?.type?.toLowerCase().includes('webrtc') || typedAction?.type?.toLowerCase().includes('speaking')) return result;

      const data = `Payload = ${JSON.stringify(typedAction?.payload, null, 2)}\n----------------------------------\nNew State = ${JSON.stringify({
        webRtc: formatState(store.getState()),
        desktop: store.getState().desktopWebRtc
      }, null, 2)}`;

      const tags = buildFilterTags(store, typedAction);

      logInternalLogs(data, tags);

      await logAllFailures(data, tags, typedAction);

      return result;
    };

/**
 * Build strings for date, time and seconds:milliseconds
 * @param date
 */
const getNowStrings = (date: Date = new Date()) => {
  const dateString = new Intl.DateTimeFormat('en-US', {
    month: 'short',
    day: 'numeric'
  }).format(date);

  const timeFmt = new Intl.DateTimeFormat('en-US', {
    hour: 'numeric',
    minute: '2-digit',
    hour12: true
  }).formatToParts(date);
  const hour = timeFmt.find(p => p.type === 'hour')?.value ?? '';
  const minute = timeFmt.find(p => p.type === 'minute')?.value ?? '';
  const dayPeriod = (timeFmt.find(p => p.type === 'dayPeriod')?.value ?? '').toLowerCase();
  const timeString = `${hour}:${minute}${dayPeriod}`;

  const secsString = `${String(date.getSeconds()).padStart(2, '0')}:${String(date.getMilliseconds()).padStart(3, '0')}`;

  return {dateString, timeString, secsString};
};

/**
 * Sends warning logs for failures from all environments except localhost to Sentry.
 * @param data
 * @param tags
 * @param typedAction
 */
const logAllFailures = async (data: string, tags: SentryTags, typedAction: UnknownAction) => {
  const {currentUserId, username, environment} = tags;
  const user = `[${[currentUserId, username].filter(Boolean).join('/')}]`;
  allLogs += `${user} [${typedAction?.type}]\n${data}` + '\n\n==================================\n\n';

  if (typedAction.type.includes('fail') && environment !== 'debug') {
    let stats;
    if (streamService?.peerConnectionAPI?.peerConnection) {
      stats = await streamService.peerConnectionAPI.peerConnection.getStats();
    }
    logger.warn(allLogs, {
      ...tags,
      error: typedAction.payload,
      ...stats ? {stats} : {}
    });
  }
};

/**
 * Sends info logs to Sentry if the environment is not production.
 * @param data
 * @param tags
 */
const logInternalLogs = (data: string, tags: SentryTags) => {
  const {environment} = tags;
  if ((isNative() || environment !== 'debug') && environment !== 'production') {
    logger.info(data, {
      ...tags
    });
  }
};

/**
 * Builds the data and tags for the log.
 * @param store
 * @param typedAction
 */
const buildFilterTags = (store: { getState: () => RootState }, typedAction: UnknownAction): SentryTags => {
  let currentUserId = selectCurrentUserId(store?.getState()?.webRtc);
  let environmentIds = selectEnvironmentIds(store?.getState()?.webRtc);
  const environment = navigator?.userAgent?.toLowerCase()
    ?.replace('default', 'production')
    ?.match(/production|staging|experimental|development/)?.[0] ?? 'debug';
  if (typedAction.type === initialize.type && !currentUserId) {
    const initAction = typedAction as PayloadAction<{ initParams: InitializeWebRtcPayload }>;
    currentUserId = initAction.payload.initParams.currentUserId;
  }

  const username = selectUserById(store.getState().desktopWebRtc, currentUserId as UserGuid)?.username || '';

  const {dateString: date, timeString: time, secsString: ms} = getNowStrings();

  return {
    environment,
    currentUserId,
    username,
    actionType: typedAction.type.replace('webRtc/', ''),
    channelId: environmentIds.containerId,
    communityId: environmentIds.communityId || 'DM',
    date,
    time,
    ms
  };
};

