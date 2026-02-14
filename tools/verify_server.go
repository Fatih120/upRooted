package main

import (
	"fmt"
	"log"
	"net/http"
	"os"
	"time"
)

func main() {
	logFile, _ := os.OpenFile(`C:\Users\bash\JS_VERIFY.txt`, os.O_CREATE|os.O_APPEND|os.O_WRONLY, 0644)

	http.HandleFunc("/", func(w http.ResponseWriter, r *http.Request) {
		ts := time.Now().Format("15:04:05.000")
		line := fmt.Sprintf("[%s] %s %s", ts, r.Method, r.URL.String())
		fmt.Println(">>> GOT REQUEST:", line)
		if logFile != nil {
			fmt.Fprintln(logFile, line)
			logFile.Sync()
		}
		w.Header().Set("Access-Control-Allow-Origin", "*")
		w.WriteHeader(200)
		fmt.Fprint(w, "ok")
	})

	fmt.Println("Verification server listening on :18999")
	log.Fatal(http.ListenAndServe(":18999", nil))
}
