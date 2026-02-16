Good morning or whatever your timezone may be.

I was bug hunting this morning and discovered some potential concerns:

My main concern is the Sentry DSN.

It looks like users are able to abuse your Sentry DSN. The sentry DSN WITH auth key is exposed at
https://c1dfb07d783ad5325c245c1fd3725390@o447951.ingest.sentry.io/4509632503087104
sentry.javascript.browser v1.33.7 is outdated, latest is 8.x.

There is a SECOND sentry DSN exposed for webrtc specific traffic at
https://75fd2cd278ac35657edd7ee8df86eb37@o4509469920133120.ingest.us.sentry.io/4509832005681152

Proof that its exploitable, I sent a fake error and it was accepted:

curl -X POST "https://o447951.ingest.sentry.io/api/4509632503087104/envelope/?sentry_key=c1dfb07d783ad5325c245c1fd3725390&sentry_version=7&sentry_client=sentry.javascript.browser%2F1.33.7" -H "Content-Type: text/plain;charset=UTF-8" -d '{"event_id":"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa","sent_at":"2026-02-12T00:00:00.000Z","sdk":{"name":"sentry.javascript.browser","version":"1.33.7"}} {"type":"event","content_type":"application/json"} {"message":"Pentest: fake error from external script","level":"error","platform":"javascript","timestamp":1739318400}'

Result: HTTP 200, accepted. Anyone can flood your Sentry with garbage or use it for social engineering via fake error reports.

You'll also want to know that sendDefaultPii is set to true in the WebRTC sentry config and replaysOnErrorSampleRate is .25, so 25% of errors trigger a full session replay recording sent to Sentry with PII attached. Thats a GDPR/CCPA concern since user PII is being automatically transmitted to a third party without explicit consent. I'd recommend rotating both DSN keys and setting sendDefaultPii to false.

I was unable to submit this information via the standard channel, it did not accept text this long. Below are the rest of my findings:


1. Hardcoded Bearer Token in Production WebRTC Bundle

A full Bearer auth token is hardcoded 4 times in the production WebRTC bundle (rootapp-desktop-webrtc.js, line 13929) inside a "Mock Bridge" class:

Authorization: "Bearer AChObn-FjgGkgtSzdaVi0wAsrDJSU4oSpE_mAVP-Hid2s1K_agqiTq35xjQmFtHY6sieXvQHTbQhbvTUGcbZwdar9JegpWlwWiFDMzXg6viHxLmglupX70uVimExkhJENIuwxIaKNxyXtpbEaKZdoVfKutg-0YEU6nIIRZ8UidY"

Along with hardcoded IDs:

communityId = "ACiNMcK2gwKa7z5MDT_ScQ"
channelId   = "ACiNMcK3iQSUPvZBKshscQ"
deviceId    = "ACysMlJTihKkT-YBU_4eJw"
userId      = "AChObn-FjgGkgtSzdaVi0w"

Anyone can extract this from the local JS file and auth API calls as that user. The mock bridge gate (!SA()- a user-agent check for "rootplatform") is trivially bypassable. I'd recommend stripping the entire mock bridge class from production builds.


2. TURN/ICE Credentials Leaked via Sentry

When a WebRTC peer connection fails, the TURN server credential and username get included in the error context:

this.utils.throwError(Nn(Ot.PeerConnectionFailed, t, { credential: e.credentials, username: e.username, bundlePolicy: "max-bundle" }))

Combined with sendDefaultPii being true these credentials end up on Sentry's servers. Should sanitize error payloads so credentials never get attached.


3. innerHTML with User Nicknames (Stored XSS)

The DevBar component ships in production and directly injects user data into the DOM via innerHTML, no sanitization:

t$11.innerHTML = '<div>' + e$9.nickname + '</div><div class="user-id">' + e$9.id + '</div>'

If someone sets their nickname to <img src=x onerror=alert(1)> it just executes. Theres about 23 innerHTML sinks in the Raid Planner sub-app alone. Using textContent instead of innerHTML for user data would fix this.


I have a more detailed report with exact file paths, line numbers, and code extracts for everything if you want to see it. Happy to work with your team on any of this.

Thanks for your time, keep up the good work on Root, its a cool app.

Best,
bash
