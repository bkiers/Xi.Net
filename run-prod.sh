#!/usr/bin/env bash

# Run the Xi app in production mode.
#
# This script can be started in its own screen at startup.
#
# In `/etc/rc.local`, for example:
#   su - SOME_USER -c "screen -dmS xi_net"
#   su - SOME_USER -c "screen -S xi_net -X stuff 'cd /path/to/Xi.Net && ./run-prod.sh\n'"

# Write the current date-time into build.txt
date +"%Y-%m-%d %T" > ./src/Xi.BlazorApp/build.txt

# Clear any previous binaries
rm -rf ./src/Xi.BlazorApp/bin/Release

# Create a production release
dotnet publish -c Release -p:UseAppHost=false

# Run the production build
cd src/Xi.BlazorApp/bin/Release/net6.0/publish
dotnet Xi.BlazorApp.dll --urls "https://localhost:9900"
