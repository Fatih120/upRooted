import * as Sentry from '@sentry/react';
import {Provider} from 'react-redux';
import './assets/styles/app.css';
import Call from './components/call/Call.tsx';
import {webRtcDesktopClientStore} from './redux';

export const App = () => {
  return (
    <Sentry.ErrorBoundary>
      <Provider store={webRtcDesktopClientStore}>
        <Call/>
      </Provider>
    </Sentry.ErrorBoundary>
  );
};
