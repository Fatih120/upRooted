from http.server import HTTPServer, BaseHTTPRequestHandler
import datetime, sys

class Handler(BaseHTTPRequestHandler):
    def do_GET(self):
        ts = datetime.datetime.now().strftime("%H:%M:%S.%f")[:-3]
        line = f"[{ts}] {self.path}"
        print(f">>> GOT REQUEST: {line}", flush=True)
        with open(r"C:\Users\bash\JS_VERIFY.txt", "a") as f:
            f.write(line + "\n")
        self.send_response(200)
        self.send_header("Access-Control-Allow-Origin", "*")
        self.end_headers()
        self.wfile.write(b"ok")
    def log_message(self, format, *args):
        pass  # suppress default logging

print("Verification server listening on :18999", flush=True)
HTTPServer(("127.0.0.1", 18999), Handler).serve_forever()
