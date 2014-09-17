@Echo OFF

ECHO REMOVE ALL PROJECT COMPONANTS

SET varSourcePath="..\..\..\Source"
SET varBuildPath="..\.."


IF NOT EXIST %varSourcePath% GOTO LAST

ECHO CLEAR PROJECT DLLS

FOR /f "useback tokens=*" %%a in ('%varSourcePath%') do set varSourcePath=%%~a


SET varFrameWorkDIR64="c:\Windows\System32\regsvr32.exe"
SET varFrameWorkDIR32="c:\Windows\SysWOW64\regsvr32.exe"

ECHO UNREGISTERING POSPrinter.ocx
"%varFrameWorkDIR64%" /s /u "%varSourcePath%\Site Controller\CashDeskOperator\DLLs\POSPrinter.ocx"
"%varFrameWorkDIR32%" /s /u "%varSourcePath%\Site Controller\CashDeskOperator\DLLs\POSPrinter.ocx"

ECHO Y|rd /s/q DELETING COM PROJECTS

ECHO Unregistering 'ComExchange.dll'
"%varFrameWorkDIR64%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\ComExchange.dll"
"%varFrameWorkDIR32%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\ComExchange.dll"

ECHO Unregistering 'BMCExComms.dll'
"%varFrameWorkDIR64%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\BMCExComms.dll"
"%varFrameWorkDIR32%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\BMCExComms.dll"

ECHO Unregistering 'NoteCumTktScan.dll'
"%varFrameWorkDIR64%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\NoteCumTktScan.dll"
"%varFrameWorkDIR32%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\NoteCumTktScan.dll"

ECHO Unregistering 'Gen2Printer.dll'
"%varFrameWorkDIR64%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\Gen2Printer.dll"
"%varFrameWorkDIR32%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\Gen2Printer.dll"

ECHO Unregistering 'JetScan.dll'
"%varFrameWorkDIR64%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\JetScan.dll"
"%varFrameWorkDIR32%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\JetScan.dll"

ECHO Unregistering 'Printercomm.dll'
"%varFrameWorkDIR64%" /s /u"%varSourcePath%\Site Controller\Comms\Com Dlls\Printercomm.dll"
"%varFrameWorkDIR32%" /s /u"%varSourcePath%\Site Controller\Comms\Com Dlls\Printercomm.dll"

ECHO Unregistering 'SDGTicketGen.dll'
"%varFrameWorkDIR64%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\SDGTicketGen.dll"
"%varFrameWorkDIR32%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\SDGTicketGen.dll"

ECHO Y|rd /s/q "%varSourcePath%\Site Controller\Comms\Com Dlls"

PAUSE

ECHO DELETING ENCRYPTED DATABASE FILES
ECHO Y|rd /s/q "%varSourcePath%\Enterprise\Database Scripts\ConsolidatedSqlScripts\"
ECHO Y|rd /s/q "%varSourcePath%\Enterprise\Database Scripts\Database\"

ECHO Y|rd /s/q "%varSourcePath%\Site Controller\Database Scripts\ConsolidatedSqlScripts\"
ECHO Y|rd /s/q "%varSourcePath%\Site Controller\Database Scripts\Database\"


ECHO ***************************************************************************************************************************
ECHO DELETING.....

"%varFrameWorkDIR64%" /s /u "%varSourcePath%\Site Controller\CashDeskOperator\DLLs\POSPrinter.ocx"
"%varFrameWorkDIR32%" /s /u "%varSourcePath%\Site Controller\CashDeskOperator\DLLs\POSPrinter.ocx"

ECHO Y|rd /s/q DELETING COM PROJECTS

ECHO Unregistering 'ComExchange.dll'
"%varFrameWorkDIR64%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\ComExchange.dll"
"%varFrameWorkDIR32%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\ComExchange.dll"

ECHO Unregistering 'BMCExComms.dll'
"%varFrameWorkDIR64%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\BMCExComms.dll"
"%varFrameWorkDIR32%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\BMCExComms.dll"

ECHO Unregistering 'NoteCumTktScan.dll'
"%varFrameWorkDIR64%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\NoteCumTktScan.dll"
"%varFrameWorkDIR32%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\NoteCumTktScan.dll"

ECHO Unregistering 'Gen2Printer.dll'
"%varFrameWorkDIR64%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\Gen2Printer.dll"
"%varFrameWorkDIR32%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\Gen2Printer.dll"

ECHO Unregistering 'JetScan.dll'
"%varFrameWorkDIR64%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\JetScan.dll"
"%varFrameWorkDIR32%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\JetScan.dll"

ECHO Unregistering 'Printercomm.dll'
"%varFrameWorkDIR64%" /s /u"%varSourcePath%\Site Controller\Comms\Com Dlls\Printercomm.dll"
"%varFrameWorkDIR32%" /s /u"%varSourcePath%\Site Controller\Comms\Com Dlls\Printercomm.dll"

ECHO Unregistering 'SDGTicketGen.dll'
"%varFrameWorkDIR64%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\SDGTicketGen.dll"
"%varFrameWorkDIR32%" /s /u "%varSourcePath%\Site Controller\Comms\Com Dlls\SDGTicketGen.dll"

ECHO Y|rd /s/q "%varSourcePath%\Site Controller\Comms\Com Dlls"

ECHO DELETING ENCRYPTED DATABASE FILES
ECHO Y|rd /s/q "%varSourcePath%\Enterprise\Database Scripts\ConsolidatedSqlScripts\"
ECHO Y|rd /s/q "%varSourcePath%\Enterprise\Database Scripts\Database\"

ECHO Y|rd /s/q "%varSourcePath%\Site Controller\Database Scripts\ConsolidatedSqlScripts\"
ECHO Y|rd /s/q "%varSourcePath%\Site Controller\Database Scripts\Database\"

ECHO DELETING ALL THE BINARIES

pause

ECHO DELETING ALL TEMPORARY FOLDERS
"%varSourcePath%\DeleteFolder.exe" "-dl" "bin"
"%varSourcePath%\DeleteFolder.exe" "-dl" "obj"
"%varSourcePath%\DeleteFolder.exe" "-dl" "x64"
"%varSourcePath%\DeleteFolder.exe" "-dl" "x86"
"%varSourcePath%\DeleteFolder.exe" "-dl" "X64"
"%varSourcePath%\DeleteFolder.exe" "-dl" "X86"
"%varSourcePath%\DeleteFolder.exe" "-dl" "Release"

"%varSourcePath%\DeleteFolder.exe" "-dl" "bin"
"%varSourcePath%\DeleteFolder.exe" "-dl" "obj"
"%varSourcePath%\DeleteFolder.exe" "-dl" "x64"
"%varSourcePath%\DeleteFolder.exe" "-dl" "x86"
"%varSourcePath%\DeleteFolder.exe" "-dl" "X64"
"%varSourcePath%\DeleteFolder.exe" "-dl" "X86"
"%varSourcePath%\DeleteFolder.exe" "-dl" "Release"

ECHO ALL PROJECTS BINARIES AND DLL ARE REMOVED

PAUSE

ECHO DELETING INSTALLER KITS....

SET varSourcePath="..\..\BMC_Installer"

"%varSourcePath%\DeleteFolder.exe" "-dl" "bin"
"%varSourcePath%\DeleteFolder.exe" "-dl" "obj"
"%varSourcePath%\DeleteFolder.exe" "-dl" "x64"
"%varSourcePath%\DeleteFolder.exe" "-dl" "x86"
"%varSourcePath%\DeleteFolder.exe" "-dl" "X64"
"%varSourcePath%\DeleteFolder.exe" "-dl" "X86"
"%varSourcePath%\DeleteFolder.exe" "-dl" "Release"

"%varSourcePath%\DeleteFolder.exe" "-dl" "bin"
"%varSourcePath%\DeleteFolder.exe" "-dl" "obj"
"%varSourcePath%\DeleteFolder.exe" "-dl" "x64"
"%varSourcePath%\DeleteFolder.exe" "-dl" "x86"
"%varSourcePath%\DeleteFolder.exe" "-dl" "X64"
"%varSourcePath%\DeleteFolder.exe" "-dl" "X86"
"%varSourcePath%\DeleteFolder.exe" "-dl" "Release"

ECHO Deleting "Errors.log" file
DEL /s /q /f "Errors.log"

ECHO TEMPORARY FILES DELETION OPERATION COMPELETD....

ECHO DELETING FILES FROM FULL KIT FOLDER

ECHO Enterprise server
DEL /s /q /f "%varBuildPath%\BMC_Installer_Setup\Enterprise Server\Enterprise Server\X64"
RMDIR /s /q "%varBuildPath%\BMC_Installer_Setup\Enterprise Server\Enterprise Server\X64\Bally Technologies"

ECHO Enterprise Client
DEL /s /q /f "%varBuildPath%\BMC_Installer_Setup\Enterprise Client\Enterprise Client\X64"
RMDIR /s /q "%varBuildPath%\BMC_Installer_Setup\Enterprise Client\Enterprise Client\X64\Bally Technologies"

ECHO Exchange server
DEL /s /q /f "%varBuildPath%\BMC_Installer_Setup\Exchange Server\Exchange Server\X64"
RMDIR /s /q "%varBuildPath%\BMC_Installer_Setup\Exchange Server\Exchange Server\X64\Bally Technologies"

ECHO Exchange Client
DEL /s /q /f "%varBuildPath%\BMC_Installer_Setup\Exchange Client\Exchange Client\X64"
RMDIR /s /q "%varBuildPath%\BMC_Installer_Setup\Exchange Client\Exchange Client\X64\Bally Technologies"

ECHO TEMPORARY FILES DELETION OPERATION COMPELETD....

PAUSE
EXIT

:LAST
ECHO EXITING DELETE OPERATION
PAUSE
