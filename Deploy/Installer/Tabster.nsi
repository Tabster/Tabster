!include "MUI2.nsh"
!include "FileAssociation.nsh"
!include "NsisDotNetChecker\nsis\DotNetChecker.nsh"

!addplugindir "NsisDotNetChecker\bin"

;--------------------------------
;Constants

  !define PRIMARY_EXE_NAME "Tabster"
  
  !define PRODUCT_NAME "Tabster"
  !define PRODUCT_VERSION "${APPLICATION_VERSION}"
  !define PRODUCT_PUBLISHER "Nate Shoffner"
  !define PRODUCT_WEB_SITE "http://nateshoffner.com"
  !define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\${PRIMARY_EXE_NAME}.exe"
  !define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
  !define PRODUCT_UNINST_ROOT_KEY "HKLM"

;--------------------------------
;General

  ;Name and file
  Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
  OutFile "${PRODUCT_NAME} ${PRODUCT_VERSION} Setup.exe"

  ;Default installation folder
  InstallDir "$PROGRAMFILES\${PRODUCT_NAME}"
  
  ;Get installation folder from registry if available
  InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
  
  ShowInstDetails show
  ShowUnInstDetails show
  
  SetCompressor lzma

  ;Request application privileges for Windows Vista
  RequestExecutionLevel admin
  
  BrandingText "${PRODUCT_PUBLISHER}"

;--------------------------------
;Interface Settings
  
  !define MUI_ABORTWARNING
  !define MUI_ICON "Icon.ico"
  !define MUI_UNICON "Icon.ico"

;--------------------------------
;Pages

  !insertmacro MUI_PAGE_LICENSE  "License.txt"
  !insertmacro MUI_PAGE_DIRECTORY
  !insertmacro MUI_PAGE_INSTFILES
  
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES
  
  !insertmacro MUI_PAGE_FINISH
  
;--------------------------------
;Languages
 
  !insertmacro MUI_LANGUAGE "English"

;--------------------------------
;Installer Sections

Section "Dummy Section" SecDummy

  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer
  
  !insertmacro CheckNetFramework 35 ;

  !define X86_DIRECTORY "$INSTDIR\x86"
  !define X64_DIRECTORY "$INSTDIR\x64"
  CreateDirectory "${X86_DIRECTORY}"
  CreateDirectory "${X64_DIRECTORY}"
  
  File "${SOLUTION_DIRECTORY}\LICENSE"
  File "${SOLUTION_DIRECTORY}\Tabster\bin\Release\Tabster.exe"
  File "${SOLUTION_DIRECTORY}\Tabster\bin\Release\Tabster.exe.config"
  File "${SOLUTION_DIRECTORY}\Tabster\bin\Release\Tabster.Core.dll"
  File "${SOLUTION_DIRECTORY}\Tabster\bin\Release\Tabster.Data.dll"
  File "${SOLUTION_DIRECTORY}\Tabster\bin\Release\Tabster.Printing.dll"
  File "${SOLUTION_DIRECTORY}\Tabster\bin\Release\Tabster.WinForms.dll"
  File "${SOLUTION_DIRECTORY}\Tabster\bin\Release\ObjectListView.dll"
  File "${SOLUTION_DIRECTORY}\Tabster\bin\Release\System.Data.SQLite.dll"
  File "${SOLUTION_DIRECTORY}\Tabster\bin\Release\log4net.dll"
  File "${SOLUTION_DIRECTORY}\Tabster\bin\Release\Newtonsoft.Json.dll"
  File "${SOLUTION_DIRECTORY}\Tabster\bin\Release\ICSharpCode.SharpZipLib.dll"
  File "${SOLUTION_DIRECTORY}\Tabster\bin\Release\RecentFilesMenuItem.dll"

  SetOutPath "${X86_DIRECTORY}"
  File "${SOLUTION_DIRECTORY}\Tabster\bin\Release\x86\SQLite.Interop.dll"
  SetOutPath "${X64_DIRECTORY}"
  File "${SOLUTION_DIRECTORY}\Tabster\bin\Release\x64\SQLite.Interop.dll"
  
  CreateShortCut "$DESKTOP\Tabster.lnk" "$INSTDIR\${PRIMARY_EXE_NAME}.exe"
  CreateDirectory "$SMPROGRAMS\${PRODUCT_NAME}"
  CreateShortCut "$SMPROGRAMS\${PRODUCT_NAME}\${PRIMARY_EXE_NAME}.lnk" "$INSTDIR\${PRIMARY_EXE_NAME}.exe"
  CreateShortCut "$SMPROGRAMS\${PRODUCT_NAME}\${PRIMARY_EXE_NAME} (Safe Mode).lnk" "$INSTDIR\${PRIMARY_EXE_NAME}.exe" "-safemode"
  
  !define RESOURCES_DIRECTORY "$INSTDIR\Resources"
  
  CreateDirectory "${RESOURCES_DIRECTORY}"
  
  ; resources
  SetOutPath "${RESOURCES_DIRECTORY}\SourceCodePro"
  File "${SOLUTION_DIRECTORY}\Tabster\bin\Release\Resources\SourceCodePro\SourceCodePro-Regular.ttf"
  File "${SOLUTION_DIRECTORY}\Tabster\bin\Release\Resources\SourceCodePro\SIL OPEN FONT LICENSE.txt"

  !define APPDATA_DIRECTORY "$APPDATA\${PRODUCT_NAME}"
  !define PLUGINS_DIRECTORY "${APPDATA_DIRECTORY}\Plugins"

  CreateDirectory "${APPDATA_DIRECTORY}"
  CreateDirectory "${PLUGINS_DIRECTORY}"
  
  SetOutPath "${PLUGINS_DIRECTORY}"
  File /r "${SOLUTION_DIRECTORY}\Deploy\Plugins\*.*"

  ; file association
  ${registerExtension} "$INSTDIR\${PRIMARY_EXE_NAME}.exe" ".tabster" "Tabster File"
  ${registerExtension} "$INSTDIR\${PRIMARY_EXE_NAME}.exe" ".tablist" "Tabster Playlist"

SectionEnd

;--------------------------------
;Uninstaller Section

Section "Uninstall"

  Delete "$INSTDIR\LICENSE"

  Delete "$INSTDIR\Tabster.exe"
  Delete "$INSTDIR\Tabster.exe.config"
  Delete "$INSTDIR\Tabster.Core.dll"
  Delete "$INSTDIR\Tabster.Data.dll"
  Delete "$INSTDIR\Tabster.Printing.dll"
  Delete "$INSTDIR\Tabster.WinForms.dll"
  
  Delete "$INSTDIR\ObjectListView.dll"
  Delete "$INSTDIR\System.Data.SQLite.dll"
  Delete "$INSTDIR\x86\SQLite.Interop.dll"
  Delete "$INSTDIR\x64\SQLite.Interop.dll"
  Delete "$INSTDIR\log4net.dll"
  Delete "$INSTDIR\Newtonsoft.Json.dll"
  Delete "$INSTDIR\ICSharpCode.SharpZipLib.dll"
  Delete "$INSTDIR\RecentFilesMenuItem.dll"
  
  RMDir "$INSTDIR\x86"
  RMDir "$INSTDIR\x64"
  
  Delete "$INSTDIR\Resources\SourceCodePro\SourceCodePro-Regular.ttf"
  Delete "$INSTDIR\Resources\SourceCodePro\SIL OPEN FONT LICENSE.txt"
  RMDir /r "$INSTDIR\Resources"
  
  Delete "$INSTDIR\Uninstall.exe"
  Delete "$DESKTOP\Tabster.lnk"
  Delete "$SMPROGRAMS\Tabster\Tabster.lnk"
  Delete "$SMPROGRAMS\Tabster\Tabster (Safe Mode).lnk"
  RMDir "$SMPROGRAMS\Tabster"
  RMDir "$INSTDIR"
 
  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  
  ; file association
  ${unregisterExtension} ".tabster" "Tabster File"
  
  SetAutoClose true

SectionEnd

Function .onInstSuccess
  WriteUninstaller "$INSTDIR\Uninstall.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\${PRIMARY_EXE_NAME}.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\Uninstall.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\${PRIMARY_EXE_NAME}.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
FunctionEnd

Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) was successfully removed from your computer."
FunctionEnd

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to completely remove $(^Name) and all of its components?" IDYES +2
  Abort
FunctionEnd