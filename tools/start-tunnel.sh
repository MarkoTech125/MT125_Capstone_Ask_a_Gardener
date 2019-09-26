#!/bin/sh

./ngrok http -subdomain=capstone -host-header="localhost:5000" 5000
