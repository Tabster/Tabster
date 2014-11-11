@ECHO OFF

SET /p APPLICATION_VERSION=Tabster Version: 

SET BUILD_DIRECTORY=%CD%

SET ZIP_ARCHIVE="%BUILD_DIRECTORY%\Tabster %APPLICATION_VERSION%.zip"

pushd..

SET SOLUTION_DIRECTORY=%CD%

SET VS_BUILD_DIRECTORY=%SOLUTION_DIRECTORY%\Tabster\bin\Portable\

::build
ECHO.
ECHO Building Solution...
C:\windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe "%SOLUTION_DIRECTORY%\Tabster.sln" /p:Configuration=Portable

::compress
ECHO.
ECHO Compressing...
7z a -r %ZIP_ARCHIVE% "%VS_BUILD_DIRECTORY%\*.exe"
7z a -r %ZIP_ARCHIVE% "%VS_BUILD_DIRECTORY%\*.dll"