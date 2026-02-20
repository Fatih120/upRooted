'use strict';

/**
 * SQLite persistence for Uprooted presence beacon.
 *
 * Schema: presence(uuid_hash TEXT PRIMARY KEY, updated_at INTEGER)
 * - uuid_hash: SHA-256 hex of the user's Root UUID (no plaintext UUIDs at rest)
 * - updated_at: Unix timestamp (seconds) of last registration
 *
 * Entries expire after 30 days (passive re-registration on next client startup).
 */

const crypto = require('crypto');
const Database = require('better-sqlite3');
const path = require('path');

const DB_PATH = process.env.PRESENCE_DB_PATH || path.join(__dirname, 'presence.db');

const db = new Database(DB_PATH);

// Enable WAL for concurrent reads
db.pragma('journal_mode = WAL');
db.pragma('synchronous = NORMAL');

db.prepare(`
  CREATE TABLE IF NOT EXISTS presence (
    uuid_hash   TEXT    PRIMARY KEY,
    updated_at  INTEGER NOT NULL
  )
`).run();

// Clean up expired entries on startup (30-day TTL)
const thirtyDaysAgo = () => Math.floor(Date.now() / 1000) - 30 * 86400;
db.prepare('DELETE FROM presence WHERE updated_at < ?').run(thirtyDaysAgo());

const stmtUpsert = db.prepare(
  'INSERT OR REPLACE INTO presence (uuid_hash, updated_at) VALUES (?, ?)'
);
const stmtCheck = db.prepare(
  'SELECT 1 FROM presence WHERE uuid_hash = ? AND updated_at > ? LIMIT 1'
);
const stmtCleanup = db.prepare('DELETE FROM presence WHERE updated_at < ?');

/**
 * Register (or refresh) a UUID. Stores SHA-256(uuid) → now().
 * @param {string} uuid — plaintext UUID from client
 */
function registerUuid(uuid) {
  const hash = hashUuid(uuid);
  const now = Math.floor(Date.now() / 1000);
  stmtUpsert.run(hash, now);
}

/**
 * Check if a UUID was registered within the last 30 days.
 * @param {string} uuid — plaintext UUID from query
 * @returns {boolean}
 */
function isActive(uuid) {
  const hash = hashUuid(uuid);
  const cutoff = thirtyDaysAgo();
  return !!stmtCheck.get(hash, cutoff);
}

/**
 * Periodic cleanup of expired entries (call hourly from server).
 */
function cleanup() {
  const result = stmtCleanup.run(thirtyDaysAgo());
  if (result.changes > 0)
    console.log(`[DB] Cleaned up ${result.changes} expired presence entries`);
}

function hashUuid(uuid) {
  return crypto.createHash('sha256').update(uuid.toLowerCase()).digest('hex');
}

module.exports = { registerUuid, isActive, cleanup };
