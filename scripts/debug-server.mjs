// Simple HTTP debug server for Uprooted link-embeds (and other injected JS).
// Listens on localhost:9876, prints POSTed log lines to stdout.
import { createServer } from 'node:http';

const PORT = 9876;

const server = createServer((req, res) => {
  res.setHeader('Access-Control-Allow-Origin', '*');
  res.setHeader('Access-Control-Allow-Methods', 'POST, OPTIONS');
  res.setHeader('Access-Control-Allow-Headers', 'Content-Type');

  if (req.method === 'OPTIONS') {
    res.writeHead(204);
    res.end();
    return;
  }

  if (req.method === 'POST' && req.url === '/log') {
    let body = '';
    req.on('data', chunk => { body += chunk; });
    req.on('end', () => {
      console.log(body);
      res.writeHead(200);
      res.end('ok');
    });
    return;
  }

  res.writeHead(404);
  res.end();
});

server.listen(PORT, '127.0.0.1', () => {
  console.log(`[debug-server] Listening on http://127.0.0.1:${PORT}/log`);
  console.log('[debug-server] Waiting for log messages...\n');
});
