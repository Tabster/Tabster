@ECHO OFF

IF %1.==. (
	SET /p APPLICATION_VERSION=Tabster Version: 
) ELSE (
    SET APPLICATION_VERSION=%*
)

SET BUILD_DIRECTORY=%CD%

pushd..

SET SOLUTION_DIRECTORY=%CD%

::build
ECHO.
ECHO Building Solution...
msbuild.exe "%SOLUTION_DIRECTORY%\Tabster.sln" /p:Configuration=Release

::run NSIS compiler
ECHO.
ECHO NSIS Compilation...
CD "%BUILD_DIRECTORY%"
makensis.exe /DAPPLICATION_VERSION="%APPLICATION_VERSION%" /DSOLUTION_DIRECTORY="%SOLUTION_DIRECTORY%" "%BUILD_DIRECTORY%\Tabster.nsi"
