# $env:APPVEYOR_BUILD_FOLDER
# $env:APPVEYOR_BUILD_VERSION
# remtoe -force
$APPVEYOR_BUILD_FOLDER = "D:\TabsterSplit\Tabster"
$APPVEYOR_BUILD_VERSION = "2.0.0.96"

$temp_directory = "$APPVEYOR_BUILD_FOLDER\Build\~TEMP"
New-Item "$temp_directory" -type directory -force

$zip_archive = "$APPVEYOR_BUILD_FOLDER\Build\Tabster $APPVEYOR_BUILD_VERSION Portable.exe"

& msbuild.exe "$APPVEYOR_BUILD_FOLDER\Tabster.sln" /p:Configuration=Portable

$output_directory = "$APPVEYOR_BUILD_FOLDER\Tabster\bin\Portable"

Copy-Item "$output_directory\*.exe" "$temp_directory"
Copy-Item "$output_directory\*.dll" "$temp_directory"
Copy-Item "$output_directory\*.config" "$temp_directory"
Copy-Item "$output_directory\*.config" "$temp_directory"

# copy subfolders recursively
Get-ChildItem -Path "$output_directory" -Recurse | ?{ $_.PSIsContainer } | copy-item -Destination "$temp_directory" -Force -Container