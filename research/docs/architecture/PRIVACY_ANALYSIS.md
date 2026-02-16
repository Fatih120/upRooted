# Root Communications - Privacy Policy vs Actual Behavior

## Source Documents

- Privacy Policy: https://www.rootapp.com/privacy-policy
- Terms of Use: https://www.rootapp.com/terms-of-use
- Community Guidelines: https://www.rootapp.com/community-guidelines
- No security page exists (404)
- No bug bounty / responsible disclosure program exists (404)
- No /trust page exists (404)
- No /legal page exists (404)

## Privacy Policy Claims vs Reality

### 1. Undisclosed Third-Party Data Processor: Sentry

POLICY SAYS: Third parties disclosed include "service providers" and Google Analytics is named specifically.

REALITY: Two separate Sentry projects receive user data, neither is disclosed:
- DSN 1: https://c1dfb07d783ad5325c245c1fd3725390@o447951.ingest.sentry.io/4509632503087104 (sub-apps, SDK v1.33.7)
- DSN 2: https://75fd2cd278ac35657edd7ee8df86eb37@o4509469920133120.ingest.us.sentry.io/4509832005681152 (WebRTC bundle)

Both are exploitable (HTTP 200 on fake event submission). Sentry is a third-party data processor receiving PII and is not mentioned anywhere in the privacy policy.


### 2. Undisclosed Session Replay Recording

POLICY SAYS: They collect "interaction with the Site and/or App" and "browsing activity."

REALITY: replaysOnErrorSampleRate is set to .25, meaning 25% of errors trigger a full session replay recording. This is a video-like capture of DOM state, user clicks, input values, and page content sent to Sentry's US servers. This is far beyond "browsing activity."

No user is informed that their session may be screen-recorded and sent to a third party.


### 3. Automatic PII Transmission Without Consent

POLICY SAYS: "All data transmitted between users and Root's backend services is encrypted using industry-standard TLS."

REALITY: sendDefaultPii is set to true in the Sentry config. Per Sentry docs this automatically attaches:
- User IP addresses
- Cookies
- User-Agent strings
- Request headers (potentially containing auth tokens)

This PII is sent to Sentry automatically on every error event. No opt-in, no opt-out, no disclosure.


### 4. TURN/ICE Server Credentials Leaked to Third Party

POLICY SAYS: Nothing about server credentials being shared with third parties.

REALITY: When WebRTC peer connections fail, TURN server credentials (username + password) are included in the error payload sent to Sentry:
```javascript
this.utils.throwError(Nn(Ot.PeerConnectionFailed, t, {
    credential: e.credentials,
    username: e.username
}))
```


### 5. No Data Retention Periods

POLICY SAYS: No retention periods are specified anywhere.

GDPR REQUIRES: Clear data retention periods for all categories of personal data. How long does Sentry retain error reports? Session replays? PII? None of this is addressed.


### 6. Persistent Installation Fingerprint

POLICY SAYS: They collect "online and/or unique identifiers."

REALITY: .betaId (e3126fe30f9c4b6f91040b3e80497a8b) is a persistent UUID sent to installer.rootapp.com during every update check. This enables long-term installation tracking. The policy mentions "unique identifiers" generically but does not disclose this specific tracking mechanism.


### 7. "We do not recognize automated do-not-track browser signals"

Combined with sendDefaultPii: true and session replay, users who signal they don't want tracking still get:
- PII automatically transmitted to Sentry
- 25% chance of session replay on errors
- No opt-out mechanism whatsoever


### 8. Children's Privacy Gap

POLICY SAYS: "The Service is not intended for users under the age of 16 and Root does not knowingly collect personal information" from minors.

REALITY: The Sentry config with sendDefaultPii and session replay has no age-gating. If a minor uses the app and hits an error, their session is recorded and PII is sent to Sentry identically to any adult user.


### 9. Data Processing Location

POLICY SAYS: "your personal information will be collected, processed, and stored in the United States"

REALITY: Sentry's US region servers receive the data (ingest.us.sentry.io). For EU users, this means PII and session recordings are transferred to the US via a third party not disclosed in the privacy policy. No data processing agreement with Sentry is mentioned. No Standard Contractual Clauses or adequacy decision referenced.


### 10. Effects SDK (effectssdk.ai) Not Disclosed

POLICY SAYS: Nothing about audio/video processing third parties.

REALITY: The WebRTC bundle loads WASM binaries from effectssdk.ai at runtime:
- https://effectssdk.ai/sdk/audio/dev/3.2.2/ort-wasm.wasm
- https://effectssdk.ai/sdk/audio/dev/3.2.2/ort-wasm-simd.wasm
- https://effectssdk.ai/sdk/session/ (session API)

This third party has access to audio/video processing context. Not mentioned in the privacy policy.


## Terms of Use Notable Clauses

- Binding arbitration in Los Angeles, CA (JAMS)
- No class actions permitted
- "AS IS" and "AS AVAILABLE" basis
- Liability capped at $100 or 30 days of purchases
- 1-year statute of limitations on claims
- California governing law
- They can terminate your account at any time without notice
- They can modify or discontinue the Service at any time
- Assignment prohibited without Root's consent

## GDPR/CCPA Specific Violations

1. No lawful basis specified for Sentry data processing (GDPR Art. 6)
2. No disclosure of Sentry as a data processor (GDPR Art. 13/14)
3. No data retention periods (GDPR Art. 13(2)(a))
4. No mention of data subject rights regarding Sentry-held data (GDPR Art. 15-22)
5. Session replay recording without consent may violate GDPR Art. 7 (consent must be freely given, specific, informed)
6. sendDefaultPii transmitting IP addresses to US-based third party without adequate safeguards (GDPR Art. 44-49)
7. No Data Protection Impact Assessment apparent for session replay (GDPR Art. 35)
8. CCPA: No "Do Not Sell My Personal Information" link or equivalent for Sentry data sharing
