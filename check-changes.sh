#!/usr/bin/env bash

# Check every minute if there are any changes made to `master`, and if so, pull the changes and
# restart the `xi_net` screen that is running the Xi app.
#
# This script can be started in its own screen at startup.
#
# In `/etc/rc.local`, for example:
#   su - SOME_USER -c "screen -dmS xi_net_chk"
#   su - SOME_USER -c "screen -S xi_net_chk -X stuff 'cd /path/to/Xi.Net && ./check-changes.sh\n'"

function restart_xi {
  # Send a CTRL+C to the `xi_net` screen
  screen -S xi_net -X stuff "^C"
  
  # Wait a bit so that the process can stop
  sleep 5

  # Restart the Xi app again
  screen -S xi_net -X stuff './run-prod.sh\n'
}

while [ true ]
do
  RUNNING=$(ps aux | grep Xi.BlazorApp | grep :9900)

  if [ -z "$RUNNING" ]
  then
    echo "Xi has stopped, restarting!"
    
    restart_xi

    # Wait 60 seconds
    sleep 60
  else
    echo "Xi is still running"
  fi

  git fetch origin
  DIFF=$(git diff master origin/master)

  if [ -z "$DIFF" ]
  then
    echo "No changes..."
  else
    # Pull the changes
    git pull --no-edit origin master

    restart_xi
  fi

  # Wait 60 seconds
  sleep 60
done
