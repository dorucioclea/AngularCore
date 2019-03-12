#!/bin/bash

set -e;

echo "Starting the service....";
dotnet AngularCore.dll;

exec "$@";