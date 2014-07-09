!include "FileAssociation.nsh"

!define PRODUCT_NAME "Tabster"
!define PRODUCT_VERSION "${APPLICATION_VERSION}"
!define PRODUCT_PUBLISHER "Nate Shoffner"
!define PRODUCT_WEB_SITE "http://nateshoffner.com"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\Tabster.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

SetCompressor lzma

; MUI 1.67 compatible ------
!include "MUI.nsh"

; MUI Settings

!define MUI_ABORTWARNING
!define MUI_ICON "Icon.ico"
!define MUI_UNICON "Icon.ico"
!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_LICENSE "License.txt"
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!define MUI_FINISHPAGE_RUN "$INSTDIR\Tabster.exe"
!insertmacro MUI_PAGE_FINISH
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_LANGUAGE "English"

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "Tabster ${PRODUCT_VERSION} Setup.exe"
InstallDir "$PROGRAMFILES\Tabster"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show

Section "MainSection" SEC01
  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer

  File "${SOLUTION_DIRECTORY}\Tabster\bin\Release\Tabster.exe"
  File "${SOLUTION_DIRECTORY}\Tabster\bin\Release\Tabster.Core.dll"
  
  CreateShortCut "$DESKTOP\Tabster.lnk" "$INSTDIR\Tabster.exe"
  CreateDirectory "$SMPROGRAMS\Tabster"
  CreateShortCut "$SMPROGRAMS\Tabster\Tabster.lnk" "$INSTDIR\Tabster.exe"
  
  CreateDirectory "$INSTDIR\Plugins"
  SetOutPath "$INSTDIR\Plugins"
  
  ; native plugins
  File "${SOLUTION_DIRECTORY}\Plugins\UltimateGuitar\bin\Release\UltimateGuitar.dll"
  File "${SOLUTION_DIRECTORY}\Plugins\GuitartabsDotCC\bin\Release\GuitartabsDotCC.dll"
  
  ; file association
  ${registerExtension} "$INSTDIR\Tabster.exe" ".tabster" "Tabster File"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\Uninstall.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\Tabster.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\Uninstall.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\Tabster.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd

Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) was successfully removed from your computer."
FunctionEnd

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to completely remove $(^Name) and all of its components?" IDYES +2
  Abort
FunctionEnd

Section Uninstall
  Delete "$INSTDIR\Tabster.exe"
  Delete "$INSTDIR\Tabster.Core.dll"
  Delete "$INSTDIR\Uninstall.exe"
  Delete "$DESKTOP\Tabster.lnk"
  Delete "$SMPROGRAMS\Tabster\Tabster.lnk"
  RMDir "$SMPROGRAMS\Tabster"
 
  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  
  ; file association
  ${unregisterExtension} ".tabster" "Tabster File"
  
  SetAutoClose true
SectionEnd