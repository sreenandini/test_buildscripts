@ECHO OFF

ECHO			MSI INSTALLER KIT

SET varSourcePath="..\..\BMC_Installer_Projects"
SET varBuildPath="..\.."

IF NOT EXIST %varSourcePath% GOTO LAST
FOR /f "useback tokens=*" %%a in ('%varSourcePath%') do set varSourcePath=%%~a

SET varFrameWorkDIR="C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"

echo %varSourcePath%
ECHO CLEAN-UP STARTED...
ECHO ******************************************CLEANING ENTERPRISE SERVER KIT******************************************
RMDIR /s/q  "%varSourcePath%\Enterprise\X64 Kits\EntepriseServerInstallerKit_X64\bin"
RMDIR /s/q  "%varSourcePath%\Enterprise\X64 Kits\EntepriseServerInstallerKit_X64\obj"

ECHO ******************************************CLEANING ENTERPRISE CLIENT KIT******************************************
RMDIR /s/q  "%varSourcePath%\Enterprise\X64 Kits\EnterpriseClientInstallerKit_X64\bin"
RMDIR /s/q  "%varSourcePath%\Enterprise\X64 Kits\EnterpriseClientInstallerKit_X64\obj"

ECHO ******************************************CLEANING EXCHANGE SERVER KIT******************************************
RMDIR /s/q  "%varSourcePath%\Exchange\X64 Kits\ExchangeServerInstallerKit_X64\bin"
RMDIR /s/q  "%varSourcePath%\Exchange\X64 Kits\ExchangeServerInstallerKit_X64\obj"

ECHO ******************************************CLEANING EXCHANGE CLIENT KIT******************************************
RMDIR /s/q   "%varSourcePath%\Exchange\X64 Kits\ExchangeClientInstallerKit_X64\bin"
RMDIR /s/q   "%varSourcePath%\Exchange\X64 Kits\ExchangeClientInstallerKit_X64\obj"

ECHO ******************************************CLEANING ENTERPRISE SERVER KIT******************************************
RMDIR /s/q  "%varSourcePath%\Enterprise\X64 Kits\EntepriseServerInstallerKit_X64\bin"
RMDIR /s/q  "%varSourcePath%\Enterprise\X64 Kits\EntepriseServerInstallerKit_X64\obj"

ECHO ******************************************CLEANING ENTERPRISE CLIENT KIT******************************************
RMDIR /s/q "%varSourcePath%\Enterprise\X64 Kits\EnterpriseClientInstallerKit_X64\bin"
RMDIR /s/q "%varSourcePath%\Enterprise\X64 Kits\EnterpriseClientInstallerKit_X64\obj"

ECHO ******************************************CLEANING EXCHANGE SERVER KIT******************************************
RMDIR /s/q  "%varSourcePath%\Exchange\X64 Kits\ExchangeServerInstallerKit_X64\bin"
RMDIR /s/q  "%varSourcePath%\Exchange\X64 Kits\ExchangeServerInstallerKit_X64\obj"

ECHO ******************************************CLEANING EXCHANGE CLIENT KIT******************************************
RMDIR /s/q   "%varSourcePath%\Exchange\X64 Kits\ExchangeClientInstallerKit_X64\bin"
RMDIR /s/q   "%varSourcePath%\Exchange\X64 Kits\ExchangeClientInstallerKit_X64\obj"

ECHO CLEAN-UP COMPLETED...
PAUSE

ECHO BUILDING WIX PROJECTS STARTED
ECHO ******************************************BUILDING ENTERPRISE SERVER KIT******************************************
"%varFrameWorkDIR%" "%varSourcePath%\Enterprise\X64 Kits\EntepriseServerInstallerKit_x64\EntepriseServerInstallerKit.sln" /property:Configuration=Release;Platform=x64 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly 
ECHO COMPLETED 1 of 4

PAUSE
ECHO ******************************************BUILDING ENTERPRISE CLIENT KIT******************************************
"%varFrameWorkDIR%" "%varSourcePath%\Enterprise\X64 Kits\EnterpriseClientInstallerKit_X64\EnterpriseClientInstallerKit.sln" /property:Configuration=Release;Platform=x64 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly
ECHO COMPLETED 2 of 4

PAUSE
ECHO ******************************************BUILDING EXCHANGE SERVER KIT******************************************
"%varFrameWorkDIR%" "%varSourcePath%\Exchange\X64 Kits\ExchangeServerInstallerKit_x64\ExchangeServerInstallerKit.sln" /property:Configuration=Release;Platform=x64 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly
ECHO COMPLETED 3 of 4

PAUSE
ECHO ******************************************BUILDING EXCHANGE CLIENT KIT******************************************
"%varFrameWorkDIR%" "%varSourcePath%\Exchange\X64 Kits\ExchangeClientInstallerKit_X64\ExchangeClientInstallerKit.sln" /property:Configuration=Release;Platform=x64 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly
ECHO COMPLETED 4 of 4

ECHO COMPLETED BUILDING WIX PROJECTS STARTED
PAUSE

ECHO Deleting files from full kit folder

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

ECHO Deleting files from full kit folder Completed

PAUSE	

ECHO "COPYING ENTERPRISE SERVER"
"%varBuildPath%\DeleteFolder.exe" "-cp" "%varSourcePath%\Enterprise\X64 Kits\EntepriseServerInstallerKit_x64\bin\x64\Release" 	"%varBuildPath%\BMC_Installer_Setup\Enterprise Server\Enterprise Server\X64"

ECHO "COPYING ENTERPRISE CLIENT"
"%varBuildPath%\DeleteFolder.exe" "-cp" "%varSourcePath%\Enterprise\X64 Kits\EnterpriseClientInstallerKit_X64\bin\x64\Release" 	"%varBuildPath%\BMC_Installer_Setup\Enterprise Client\Enterprise Client\X64"

ECHO "COPYING EXCHANGE SERVER"
"%varBuildPath%\DeleteFolder.exe" "-cp" "%varSourcePath%\Exchange\X64 Kits\ExchangeServerInstallerKit_x64\bin\x64\Release" 		"%varBuildPath%\BMC_Installer_Setup\Exchange Server\Exchange Server\X64"

ECHO COPYING EXCHANGE CLIENT"
"%varBuildPath%\DeleteFolder.exe" "-cp" "%varSourcePath%\Exchange\X64 Kits\ExchangeClientInstallerKit_x64\bin\x64\Release" 		"%varBuildPath%\BMC_Installer_Setup\Exchange Client\Exchange Client\X64"

:LAST
ECHO Exiting
PAUSE
