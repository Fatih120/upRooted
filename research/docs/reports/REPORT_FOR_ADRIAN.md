Everything below was tested live against production. I used my own account (watchthelight) and kept things non-destructive beyond the channel rename you already saw.

## How I Got In

Your WebRTC bundle ships a gRPC client for all 27 backend services. 163 methods. It's supposed to be for voice/video but it's got stubs for everything - messages, communities, users, files, bans, roles. The protobuf field numbers and types are right there in the JS. A 13.5MB source map sitting next to it in production gave me the full original TypeScript. That's the whole API on a platter.

---

## The Big Ones

### 1. Channel Edit Has No Authorization Check (FIXED?)
You know this one. `ChannelGrpcService/Edit` lets any authenticated user rename any channel in any community. No role check, no membership check, nothing. The frustrating part is that `ChannelGrpcService/Delete` on the same channel correctly returns UNAUTH - so the auth framework exists and works. Someone just forgot to hook it up for Edit. I'd bet money this same oversight exists on other Edit endpoints.

### 2. Cross-Community Enumeration:
During testing of this my account was not a member of the Root community. I could still:
- `ChannelGroupGrpcService/List` - all 6 channel groups, with names, permissions, role UUIDs
- `ChannelGrpcService/List` - all 10 channels, names, descriptions, icons, timestamps
- Get on both services - full details on any individual group or channel

That's how I found the Chat channel to rename. Anyone with a token can map every community's structure without joining.

### 3. Full Member List Dumped in One Call:
`CommunityMemberGrpcService/ListAll` gave me 16,971 member UUIDs for the Root community. One request. ~8MB response. No pagination, no membership check. Your entire user directory, handed to anyone who asks.

### 4. DM Any User Without Consent:
`DirectMessageGrpcService/Create` opens a DM with any user by UUID. No friend request, no shared community, no consent flow. I tested it against the community owner's account and it went through. Pair this with that 16,971-user member dump and someone could spam every user on the platform.

### 5. SSRF via Profile Picture:
`UserGrpcService/SetProfilePicture` takes a URL and the server fetches it. You're blocking direct IPs like 169.254.169.254 and 127.0.0.1, which is good. But internal DNS names sail right through and return OK. Classic DNS rebinding gap.

### 6. Token Refresh Kills the Old Token Instantly:
`UserGrpcService/RefreshToken` issues a new token and immediately invalidates the previous one. So if someone gets your token, they call RefreshToken, get a fresh one, and your entire session dies - every API call starts returning 401. You'd have to restart the app. They now have exclusive access and there's no way to kick them without them doing it first. I did this to myself by accident during testing and bricked my own session.


## Smaller crap!:

### 7. Zero Rate Limiting lmao:
20 concurrent calls to `UserGrpcService/GetSelf` - all 20 returned OK in 0.2 seconds. Same for `NotificationGrpcService/List` - 20 calls, 0.1 seconds, no throttling. Brute-force and enumeration are wide open.

### 8. User Lookup by Username
`GetExtendedUsersByUsername` takes any username and hands back their UUID and profile picture. Tested "admin", "root", "test", all returned data. This is how you bootstrap the UUID-based attacks above.

### 9. Notification Wipe
`NotificationGrpcService/DeleteAll` works with no restrictions. An attacker with a stolen token can nuke every notification to cover their tracks.

### 10. Guessable Invite Codes
`CommunityInviteLinkGetInfo` with code "root" returns community info. Nobody's going to be shocked that someone tried "root" as a code :skull:

### 11. TURN Credentials Without a Voice Session uhhh
`WebRtcGrpcService/GetIceInfo` hands out Cloudflare TURN server URLs, username, and credential to any authenticated user. You don't need to be in a call. Probably short-lived creds but it's still free relay infrastructure for anyone who asks haha

### 12. SignUp From Authenticated Session
`UserGrpcService/SignUp` returns OK when you're already logged in. That's a bot farming endpoint if i've ever seen one

## Things I Couldn't Verify But Look Sketchy to me:

- `AccessRuleGrpcService` (Create/Edit/Delete) returned INTERNAL errors, probably my protobuf format was off. These let users create permission overrides on channels. If they work without auth, that's straight privilege escalation.
- File and Directory services returned BAD_ARG, likely need a different ID format. If they work without auth, that's cross-community file access.
- `CommunityAppGrpcService/SetDevSettings` takes `community_permission` and `channel_permission` fields directly. If the server just applies those, that's another privesc path nobody would notice.

