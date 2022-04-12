# time dotnet publish --output bin/Release/linux-x64 --configuration Release --runtime linux-x64 --self-contained --verbosity minimal /p:PublishSingleFile=true /p:PublishTrimmed=true
# time dotnet publish --output bin/Release/win10-x64 --configuration Release --runtime win10-x64 --self-contained --verbosity minimal /p:PublishSingleFile=true /p:PublishTrimmed=true
# time dotnet publish --output bin/Release/linux-x64 --configuration Release --runtime linux-x64 --self-contained --verbosity minimal /p:PublishTrimmed=true
cd ~/Workspace/mailfilter && time ~/.local/opt/dotnet6/dotnet publish --output bin/Release/linux-x64 --configuration Release --runtime linux-x64 --self-contained -p:PublishTrimmed=true -p:PublishReadyToRun=true
# time dotnet publish --output bin/Release/win10-x64 --configuration Release --runtime win10-x64 --self-contained --verbosity minimal /p:PublishTrimmed=true
