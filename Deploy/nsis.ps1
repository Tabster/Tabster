$script = $env:APPVEYOR_BUILD_FOLDER + "\Build\Tabster.nsi"
& "C:\Program Files (x86)\NSIS\makensis.exe" /DAPPLICATION_VERSION="$env:APPVEYOR_BUILD_VERSION" /DSOLUTION_DIRECTORY="$env:APPVEYOR_BUILD_FOLDER" "$script"
Move-Item .\*.exe ..\