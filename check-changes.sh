#!/usr/bin/env bash

# Check every minute if there are any changes made to `master`, and if so, pull the changes and
# restart the `xi_net` screen that is running the Xi app.
#
# This script can be started in its own screen at startup.
#
# In `/etc/rc.local`, for example:
#   su - SOME_USER -c "screen -dmS xi_net_chk"
#   su - SOME_USER -c "screen -S xi_net_chk -X stuff 'cd /path/to/Xi.Net && ./check-changes.sh\n'"

while [ true ]
do
  if pgrep -x "Xi.BlazorApp" >/dev/null
  then
    echo "Xi is still running"
  else
    echo "Xi has stopped, restarting!"
    
    # Send a CTRL+C to the `xi_net` screen
    screen -S xi_net -X stuff "^C"
    # Wait a bit so that the process can stop
    sleep 5
    # Restart the Xi app again
    screen -S xi_net -X stuff './run-prod.sh\n'

    # Wait 2 minutes so that everything is up and running again
    sleep 120
  fi

  git fetch origin
  DIFF=$(git diff master origin/master)

  if [ -z "$DIFF" ]
  then
    echo "No changes..."
  else
    # Pull the changes
    git pull --no-edit origin master

    # Send a CTRL+C to the `xi_net` screen
    screen -S xi_net -X stuff "^C"
    # Wait a bit so that the process can stop
    sleep 5
    # Restart the Xi app again
    screen -S xi_net -X stuff './run-prod.sh\n'
  fi

  # Wait 60 seconds
  sleep 60
done
