from flask import Flask
import os
import socket

app = Flask(__name__)

@app.route("/")
def hello():
    return "<h3>Hello world!</h3>"

if __name__ == "__main__":
    app.run(host='0.0.0.0', port=80)
