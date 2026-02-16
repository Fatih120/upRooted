import * as Sentry from '@sentry/react';

const environment = navigator?.userAgent?.toLowerCase()
  ?.replace('default', 'production')
  ?.match(/production|staging|experimental|development/)?.[0] ?? 'debug';
Sentry.init({
  dsn: 'https://75fd2cd278ac35657edd7ee8df86eb37@o4509469920133120.ingest.us.sentry.io/4509832005681152',
  environment,
  // Setting this option to true will send default PII data to Sentry.
  // For example, automatic IP address collection on events
  sendDefaultPii: true,
  integrations: [
    Sentry.browserTracingIntegration()
  ],
  // Tracing
  tracesSampleRate: 0.025, //  Capture 2.5% of the transactions
  // Session Replay
  replaysOnErrorSampleRate: 0.25, // Capture 25% of error sessions
  // Enable logs to be sent to Sentry
  enableLogs: environment !== 'debug'

});