#!/bin/env bash
rm -rf bin
rm -rf obj

# time dotnet publish --output bin/Release/linux-x64 --configuration Release --runtime linux-x64 --self-contained --verbosity minimal /p:PublishSingleFile=true /p:PublishTrimmed=true -p:PublishReadyToRun=true
# time dotnet publish --output bin/Release/win10-x64 --configuration Release --runtime win10-x64 --self-contained --verbosity minimal /p:PublishSingleFile=true /p:PublishTrimmed=true -p:PublishReadyToRun=true

time dotnet publish --output bin/Release/linux-x64 --configuration Release --runtime linux-x64 -p:PublishReadyToRun=true
# enabling '-p:PublishTrimmed=true' will lead to fail in NewtonSoft's JSON decoder
