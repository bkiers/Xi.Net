#!/usr/bin/env bash

# Run the Xi app in production mode.
#
# This script can be started in its own screen at startup.
#
# In `/etc/rc.local`, for example:
#   su - SOME_USER -c "screen -dmS xi_net"
#   su - SOME_USER -c "screen -S xi_net -X stuff 'cd /path/to/xi && ./run-prod.sh\n'"

dotnet run --project ./src/Xi.BlazorApp -- Production
