@ECHO OFF

SET /p APPLICATION_VERSION=Tabster Version: 

SET BUILD_DIRECTORY=%CD%
SET "TEMP_DIRECTORY=%BUILD_DIRECTORY%\~TEMP"
SET "ZIP_ARCHIVE=%BUILD_DIRECTORY%\Tabster %APPLICATION_VERSION%.zip"

IF EXIST "%TEMP_DIRECTORY%" rmdir /S /Q "%TEMP_DIRECTORY%"
mkdir "%TEMP_DIRECTORY%"
mkdir "%TEMP_DIRECTORY%\Plugins\FileTypes"
mkdir "%TEMP_DIRECTORY%\Plugins\Searching"

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
copy "%VS_BUILD_DIRECTORY%\Updater.exe" "%TEMP_DIRECTORY%\Updater.exe"
copy "%VS_BUILD_DIRECTORY%\Tabster.Controls.dll" "%TEMP_DIRECTORY%\Tabster.Controls.dll"
copy "%VS_BUILD_DIRECTORY%\Tabster.Core.dll" "%TEMP_DIRECTORY%\Tabster.Core.dll"
copy "%VS_BUILD_DIRECTORY%\Tabster.Data.dll" "%TEMP_DIRECTORY%\Tabster.Data.dll"
copy "%VS_BUILD_DIRECTORY%\Tabster.Utils.dll" "%TEMP_DIRECTORY%\Tabster.Utils.dll"

copy "%VS_BUILD_DIRECTORY%\ObjectListView.dll" "%TEMP_DIRECTORY%\ObjectListView.dll"

::native filetype plugins
copy "%VS_BUILD_DIRECTORY%\Plugins\FileTypes\TextFile.dll" "%TEMP_DIRECTORY%\Plugins\FileTypes\TextFile.dll"
copy "%VS_BUILD_DIRECTORY%\Plugins\FileTypes\HtmlFile.dll" "%TEMP_DIRECTORY%\Plugins\FileTypes\HtmlFile.dll"
::HtmlFile dependency
copy "%VS_BUILD_DIRECTORY%\Plugins\FileTypes\HtmlAgilityPack.dll" "%TEMP_DIRECTORY%\Plugins\FileTypes\HtmlAgilityPack.dll"
copy "%VS_BUILD_DIRECTORY%\Plugins\FileTypes\RtfFile.dll" "%TEMP_DIRECTORY%\Plugins\FileTypes\RtfFile.dll"
copy "%VS_BUILD_DIRECTORY%\Plugins\FileTypes\WordDoc.dll" "%TEMP_DIRECTORY%\Plugins\FileTypes\WordDoc.dll"
::WordDoc dependency
copy "%VS_BUILD_DIRECTORY%\Plugins\FileTypes\DocX.dll" "%TEMP_DIRECTORY%\Plugins\FileTypes\DocX.dll"
copy "%VS_BUILD_DIRECTORY%\Plugins\FileTypes\PngFile.dll" "%TEMP_DIRECTORY%\Plugins\FileTypes\PngFile.dll"

::native search plugins

copy "%VS_BUILD_DIRECTORY%\Plugins\Searching\UltimateGuitar.dll" "%TEMP_DIRECTORY%\Plugins\Searching\UltimateGuitar.dll"
copy "%VS_BUILD_DIRECTORY%\Plugins\Searching\GuitartabsDotCC.dll" "%TEMP_DIRECTORY%\Plugins\Searching\GuitartabsDotCC.dll"
copy "%VS_BUILD_DIRECTORY%\Plugins\Searching\Songsterr.dll" "%TEMP_DIRECTORY%\Plugins\Searching\Songsterr.dll"
::common dependency
copy "%VS_BUILD_DIRECTORY%\Plugins\Searching\HtmlAgilityPack.dll" "%TEMP_DIRECTORY%\Plugins\Searching\HtmlAgilityPack.dll"

::compression
ECHO.
ECHO Compressing...
 
7z a -r "%ZIP_ARCHIVE%" "%TEMP_DIRECTORY%\*.*"

::cleanup
ECHO.
ECHO Cleaning Up...
rmdir /S /Q "%TEMP_DIRECTORY%"