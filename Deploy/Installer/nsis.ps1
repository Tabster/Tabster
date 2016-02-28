& "C:\Program Files (x86)\NSIS\makensis.exe" /DAPPLICATION_VERSION="$env:APPVEYOR_BUILD_VERSION" /DSOLUTION_DIRECTORY="$env:APPVEYOR_BUILD_FOLDER" "$env:APPVEYOR_BUILD_FOLDER\Deploy\Installer\Tabster.nsi"

# move executable to project directory for clean AppVeyor artifact name
$installer = "$env:APPVEYOR_BUILD_FOLDER\Deploy\Installer\Tabster_$env:APPVEYOR_BUILD_VERSION_Setup.exe"
Move-Item "$installer" "$env:APPVEYOR_BUILD_FOLDER"