from http.server import BaseHTTPRequestHandler, HTTPServer
from types import SimpleNamespace
import time

Δ = SimpleNamespace()
Δ.name = "localhost"
Δ.server = None

class Σ(BaseHTTPRequestHandler):
    #region#*DEFAULT_CLASS_PARAMETERS
    port = 6969
    #endregion
    def do_GET(self):
        self.send_response(200)
        self.send_header("Content-type","text/html")
        self.end_headers()

if __name__ == "__main__":
    Δ.server = HTTPServer((Δ.name, Σ.port), Σ)
    print(f"Server started http://{Δ.name}:{Σ.port}")

    try:
        Δ.server.serve_forever()
    except KeyboardInterrupt:
        pass

    Δ.server.server_close()
    print("Server stopped.")
