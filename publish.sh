# time dotnet publish --output bin/Release/linux-x64 --configuration Release --runtime linux-x64 --self-contained --verbosity minimal /p:PublishSingleFile=true /p:PublishTrimmed=true
# time dotnet publish --output bin/Release/win10-x64 --configuration Release --runtime win10-x64 --self-contained --verbosity minimal /p:PublishSingleFile=true /p:PublishTrimmed=true
# time dotnet publish --output bin/Release/linux-x64 --configuration Release --runtime linux-x64 --self-contained --verbosity minimal /p:PublishTrimmed=true
cd ~/Workspace/mailfilter && time ~/.local/opt/dotnet/dotnet publish --output bin/Release/linux-x64 --configuration Release --framework "net5.0" --runtime linux-x64 --self-contained --verbosity minimal -p:PublishTrimmed=true -p:PublishReadyToRun=true
# time dotnet publish --output bin/Release/win10-x64 --configuration Release --runtime win10-x64 --self-contained --verbosity minimal /p:PublishTrimmed=true
