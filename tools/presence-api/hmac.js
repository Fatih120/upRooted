'use strict';

/**
 * HMAC validation for Uprooted presence registration.
 *
 * Proof token: HMAC-SHA256(uuid:dayNumber, SHARED_SECRET)
 * - dayNumber = days since Unix epoch (Math.floor(Date.now() / 86400000))
 * - Secret shared between this server and hook/UprootedPresenceBeacon.cs
 * - Server accepts tokens for today or yesterday (clock skew tolerance)
 *
 * Secret is loaded from PRESENCE_HMAC_SECRET env var. The hook stores the same
 * secret XOR-obfuscated in _encryptedKey/_keyXor byte arrays.
 *
 * Default (development) secret: 4b9e2a71c38f561da4e7305c89f26b14d843972e61b50a7ce328549fc16a378d
 * Set PRESENCE_HMAC_SECRET in production to override.
 */

const crypto = require('crypto');

// Default key matches UprootedPresenceBeacon._encryptedKey XOR _keyXor
const SECRET = process.env.PRESENCE_HMAC_SECRET
  || '4b9e2a71c38f561da4e7305c89f26b14d843972e61b50a7ce328549fc16a378d';

/**
 * Validate a registration proof token.
 * @param {string} uuid  — client UUID (lowercase)
 * @param {number} ts    — dayNumber from client
 * @param {string} token — HMAC-SHA256 hex from client
 * @returns {boolean}
 */
function validateToken(uuid, ts, token) {
  const today = Math.floor(Date.now() / 86400000);

  // Accept today or yesterday (clock skew tolerance)
  if (ts !== today && ts !== today - 1) {
    return false;
  }

  const message = `${uuid.toLowerCase()}:${ts}`;
  const expected = crypto
    .createHmac('sha256', Buffer.from(SECRET, 'hex'))
    .update(message, 'utf8')
    .digest('hex');

  // Constant-time comparison to prevent timing attacks
  try {
    return crypto.timingSafeEqual(
      Buffer.from(token.toLowerCase(), 'hex'),
      Buffer.from(expected, 'hex')
    );
  } catch {
    return false; // token wrong length or malformed
  }
}

module.exports = { validateToken };
