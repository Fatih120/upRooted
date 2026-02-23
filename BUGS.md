# Bugs

> Known bugs to fix. Add new entries at the top. Check items off when fixed.

---

- [x] **WhoReacted causes startup crash loop** — Unprotected `RunOnUIThread` call on a ThreadPool thread in Phase 2→3 transition: if it throws, it's an unhandled exception that kills the process. Combined with 0ms initial timer delay (scanning before chat loads). Fix: wrapped in try/catch, added 3s initial delay.
  - File: `hook/WhoReactedEngine.cs`

- [x] **UserBio text not centered on profile popup** — Bio text ("I'm Entropy. What more is there to say?") is not horizontally centered in the profile card. Should always be perfectly centered regardless of text length.
  - File: `hook/UserBioEngine.cs`

