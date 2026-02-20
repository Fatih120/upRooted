'use strict';

/**
 * Uprooted Presence Beacon API
 *
 * Endpoints:
 *   POST /presence/register  — register own UUID (HMAC-authenticated)
 *   GET  /presence/:uuid     — check if a UUID is registered (public, rate-limited)
 *
 * Deployment: systemd or PM2, reverse-proxied via nginx on pawtropolis
 *   PRESENCE_HMAC_SECRET  — 32-byte hex HMAC secret (default: dev key)
 *   PRESENCE_DB_PATH      — SQLite DB path (default: ./presence.db)
 *   PORT                  — listen port (default: 3742)
 *
 * Security:
 *   - Registration: HMAC-SHA256(uuid:dayNumber, SECRET) proof required
 *   - UUIDs stored as SHA-256 hashes (no plaintext at rest)
 *   - Registration: 1 per UUID per 3 min (rate limit)
 *   - Query: 60 req/min per IP (rate limit)
 *   - Entries expire after 30 days (passive re-registration on next startup)
 */

const express = require('express');
const rateLimit = require('express-rate-limit');
const { validateToken } = require('./hmac');
const { registerUuid, isActive, cleanup } = require('./db');

const app = express();
const PORT = parseInt(process.env.PORT || '3742', 10);

// Validate UUIDs (Root uses standard 8-4-4-4-12 format)
const UUID_RE = /^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$/i;

// Per-UUID registration throttle (stored in memory — single-process server)
// Prevents re-registering more than once per ~3 min even if rate limiter is bypassed
const registrationTimestamps = new Map(); // uuid → last registered ms
const REGISTER_COOLDOWN_MS = 3 * 60 * 1000; // 3 min — short so failed registrations retry quickly

app.use(express.json({ limit: '4kb' }));

// Trust nginx proxy for rate limiting by real IP
app.set('trust proxy', 1);

// ===== Rate limiters =====

const registerLimiter = rateLimit({
  windowMs: 60 * 60 * 1000, // 1 hour
  max: 5,                    // max 5 registration attempts per IP per hour
  standardHeaders: true,
  legacyHeaders: false,
  message: { error: 'Too many registration attempts, try again later' },
});

const queryLimiter = rateLimit({
  windowMs: 60 * 1000, // 1 minute
  max: 60,             // 60 queries per IP per minute
  standardHeaders: true,
  legacyHeaders: false,
  message: { error: 'Rate limit exceeded' },
});

// ===== POST /presence/register =====

app.post('/presence/register', registerLimiter, (req, res) => {
  const { uuid, token, ts, v } = req.body ?? {};

  // Validate inputs
  if (typeof uuid !== 'string' || !UUID_RE.test(uuid)) {
    return res.status(400).json({ error: 'Invalid uuid' });
  }
  if (typeof token !== 'string' || !/^[0-9a-f]{64}$/i.test(token)) {
    return res.status(400).json({ error: 'Invalid token' });
  }
  if (typeof ts !== 'number' || !Number.isInteger(ts) || ts < 0) {
    return res.status(400).json({ error: 'Invalid ts' });
  }
  if (typeof v !== 'string' || v.length > 20) {
    return res.status(400).json({ error: 'Invalid v' });
  }

  // Per-UUID cooldown (in addition to IP rate limit)
  const normalUuid = uuid.toLowerCase();
  const lastReg = registrationTimestamps.get(normalUuid) ?? 0;
  if (Date.now() - lastReg < REGISTER_COOLDOWN_MS) {
    return res.status(429).json({ error: 'Already registered recently' });
  }

  // HMAC validation
  if (!validateToken(normalUuid, ts, token)) {
    return res.status(403).json({ error: 'Invalid proof token' });
  }

  // Store (SHA-256 of UUID)
  try {
    registerUuid(normalUuid);
    registrationTimestamps.set(normalUuid, Date.now());
    console.log(`[Register] ${normalUuid.slice(0, 8)}... v${v}`);
    return res.status(200).json({ ok: true });
  } catch (err) {
    console.error('[Register] DB error:', err);
    return res.status(500).json({ error: 'Internal error' });
  }
});

// ===== GET /presence/:uuid =====

app.get('/presence/:uuid', queryLimiter, (req, res) => {
  const { uuid } = req.params;

  if (!UUID_RE.test(uuid)) {
    return res.status(400).json({ error: 'Invalid uuid' });
  }

  try {
    const active = isActive(uuid);
    return res.json({ active });
  } catch (err) {
    console.error('[Query] DB error:', err);
    return res.status(500).json({ error: 'Internal error' });
  }
});

// ===== Health check =====

app.get('/health', (_req, res) => res.json({ ok: true }));

// ===== Startup =====

app.listen(PORT, '127.0.0.1', () => {
  console.log(`[Presence API] Listening on 127.0.0.1:${PORT}`);
  console.log(`[Presence API] HMAC secret: ${process.env.PRESENCE_HMAC_SECRET ? 'from env' : 'default (dev)'}`);
});

// Hourly cleanup: expired DB entries + stale in-memory throttle timestamps
const TIMESTAMP_MAX_AGE_MS = 30 * 24 * 60 * 60 * 1000; // 30 days — matches DB expiry
setInterval(() => {
  cleanup();
  const cutoff = Date.now() - TIMESTAMP_MAX_AGE_MS;
  for (const [uuid, ts] of registrationTimestamps) {
    if (ts < cutoff) registrationTimestamps.delete(uuid);
  }
}, 60 * 60 * 1000);
