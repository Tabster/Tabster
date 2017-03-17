& git clone "https://github.com/ReVolly/NsisDotNetChecker" "$env:APPVEYOR_BUILD_FOLDER\Deploy\Installer\" 2>&1 | % { $_.ToString() }

& "C:\Program Files (x86)\NSIS\makensis.exe" /DAPPLICATION_VERSION="$env:APPVEYOR_BUILD_VERSION" /DSOLUTION_DIRECTORY="$env:APPVEYOR_BUILD_FOLDER" "$env:APPVEYOR_BUILD_FOLDER\Deploy\Installer\Tabster.nsi"

$installer = "$env:APPVEYOR_BUILD_FOLDER\Deploy\Installer\Tabster $env:APPVEYOR_BUILD_VERSION Setup.exe"

# sign the installer
& "$env:sign_tool" sign /f "$env:APPVEYOR_BUILD_FOLDER\Deploy\tabster.pfx" /p $env:cert_pass /t "http://timestamp.comodoca.com/authenticode" "$installer"

# move executable to project directory for clean AppVeyor artifact name
Move-Item "$installer" "$env:APPVEYOR_BUILD_FOLDER"