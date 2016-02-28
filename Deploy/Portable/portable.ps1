$temp_directory = "$env:APPVEYOR_BUILD_FOLDER\Deploy\Portable\~TEMP"
New-Item "$temp_directory" -type directory

$zip_archive = "$env:APPVEYOR_BUILD_FOLDER\Deploy\Portable\Tabster $env:APPVEYOR_BUILD_VERSION Portable.exe"

# build using portable configuration
& msbuild.exe "$env:APPVEYOR_BUILD_FOLDER\Tabster.sln" /p:Configuration=Portable

$output_directory = "$env:APPVEYOR_BUILD_FOLDER\Tabster\bin\Portable"

# copy .exe, .dll, .config files
Copy-Item "$output_directory\*.exe" "$temp_directory"
Copy-Item "$output_directory\*.dll" "$temp_directory"
Copy-Item "$output_directory\*.config" "$temp_directory"

# copy subfolders recursively
Get-ChildItem -Path "$output_directory" -Recurse | ?{ $_.PSIsContainer } | Copy-Item -Destination "$temp_directory" -Force -Container

# zip contents in self-extracting archive
& "C:\ProgramData\chocolatey\lib\7zip.commandline\7za.exe" a -r -mmt -mx5 -sfx7z.sfx "$zip_archive" "$temp_directory\*.*"

# move executable to project directory for clean AppVeyor artifact name
Move-Item "$zip_archive" "$env:APPVEYOR_BUILD_FOLDER"