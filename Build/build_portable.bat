@ECHO OFF

IF %1.==. (
	SET /p APPLICATION_VERSION=Tabster Version: 
) ELSE (
    SET APPLICATION_VERSION=%*
)

SET BUILD_DIRECTORY=%CD%
SET "TEMP_DIRECTORY=%BUILD_DIRECTORY%\~TEMP"
SET "ZIP_ARCHIVE=%BUILD_DIRECTORY%\Tabster %APPLICATION_VERSION% Portable.exe"

IF EXIST "%TEMP_DIRECTORY%" rmdir /S /Q "%TEMP_DIRECTORY%"
mkdir "%TEMP_DIRECTORY%"
mkdir "%TEMP_DIRECTORY%\Resources\Fonts\"

IF EXIST "%ZIP_ARCHIVE%" del /F "%ZIP_ARCHIVE%"

pushd..

SET SOLUTION_DIRECTORY=%CD%

SET VS_BUILD_DIRECTORY=%SOLUTION_DIRECTORY%\Tabster\bin\Portable

::build
ECHO.
ECHO Building Solution...
C:\windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe "%SOLUTION_DIRECTORY%\Tabster.sln" /p:Configuration=Portable

::copy files
ECHO.
ECHO Copying Files...

::core files
copy "%VS_BUILD_DIRECTORY%\Tabster.exe" "%TEMP_DIRECTORY%\Tabster.exe"
copy "%VS_BUILD_DIRECTORY%\Tabster.exe" "%TEMP_DIRECTORY%\Tabster.exe.config"
copy "%VS_BUILD_DIRECTORY%\Tabster.WinForms.dll" "%TEMP_DIRECTORY%\Tabster.WinForms.dll"
copy "%VS_BUILD_DIRECTORY%\Tabster.Core.dll" "%TEMP_DIRECTORY%\Tabster.Core.dll"
copy "%VS_BUILD_DIRECTORY%\Tabster.Data.dll" "%TEMP_DIRECTORY%\Tabster.Data.dll"
copy "%VS_BUILD_DIRECTORY%\Tabster.Printing.dll" "%TEMP_DIRECTORY%\Tabster.Printing.dll"

copy "%VS_BUILD_DIRECTORY%\Newtonsoft.Json.dll" "%TEMP_DIRECTORY%\Newtonsoft.Json.dll"
copy "%VS_BUILD_DIRECTORY%\ObjectListView.dll" "%TEMP_DIRECTORY%\ObjectListView.dll"
copy "%VS_BUILD_DIRECTORY%\System.Data.SQLite.dll" "%TEMP_DIRECTORY%\System.Data.SQLite.dll"
copy "%VS_BUILD_DIRECTORY%\x86\SQLite.Interop.dll" "%TEMP_DIRECTORY%\SQLite.Interop.dll"
copy "%VS_BUILD_DIRECTORY%\log4net.dll" "%TEMP_DIRECTORY%\log4net.dll"

::resources
copy "%VS_BUILD_DIRECTORY%\Resources\Fonts\SourceCodePro-Regular.ttf" "%TEMP_DIRECTORY%\Resources\Fonts\SourceCodePro-Regular.ttf"

::compression
ECHO.
ECHO Compressing...
 
7z a -r "%ZIP_ARCHIVE%" -mmt -mx5 -sfx7z.sfx "%TEMP_DIRECTORY%\*.*"

::cleanup
ECHO.
ECHO Cleaning Up...
rmdir /S /Q "%TEMP_DIRECTORY%"