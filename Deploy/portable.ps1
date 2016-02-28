$temp_directory = "$env:APPVEYOR_BUILD_FOLDER\Build\~TEMP"
New-Item "$temp_directory" -type directory

$zip_archive = "$env:APPVEYOR_BUILD_FOLDER\Build\Tabster $env:APPVEYOR_BUILD_VERSION Portable.exe"

& msbuild.exe "$env:APPVEYOR_BUILD_FOLDER\Tabster.sln" /p:Configuration=Portable

$output_directory = "$env:APPVEYOR_BUILD_FOLDER\Tabster\bin\Portable"

Copy-Item "$output_directory\*.exe" "$temp_directory"
Copy-Item "$output_directory\*.dll" "$temp_directory"
Copy-Item "$output_directory\*.config" "$temp_directory"
Copy-Item "$output_directory\*.config" "$temp_directory"

# copy subfolders recursively
Get-ChildItem -Path "$output_directory" -Recurse | ?{ $_.PSIsContainer } | copy-item -Destination "$temp_directory" -Force -Container