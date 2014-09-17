@ECHO OFF

ECHO			COMMON DLL
SET varSourcePath="..\..\..\"

IF NOT EXIST %varSourcePath% GOTO LAST

FOR /f "useback tokens=*" %%a in ('%varSourcePath%') do set varSourcePath=%%~a

SET varFrameWorkDIR="C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"

ECHO Register POSPrinter.ocx for Common Projects
"c:\Windows\SysWOW64\regsvr32.exe" /s "%varSourcePath%\Source\Site Controller\CashDeskOperator\DLLs\POSPrinter.ocx"

ECHO BUILDING COMMON PROJECTS

"%varFrameWorkDIR%" "%varSourcePath%\Source\Common\BMC.ConfigHelper\BMC.RegParser\BMC.RegParser.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly
ECHO ***********COMPLETED 0 of 9***********

ECHO Invoking BMC.RegParser
"%varSourcePath%\Source\Common\BMC.ConfigHelper\BMC.RegParser\bin\x86\Release\BMC.RegParser.exe"

"%varFrameWorkDIR%" "%varSourcePath%\Source\Common\BMC.CoreLib\BMC.CoreLib.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly
ECHO ***********COMPLETED 5 of 9***********

"%varFrameWorkDIR%" "%varSourcePath%\Source\Common\BMC.ConfigHelper\BMC.ConfigurationEditor\BMC.ConfigurationEditor.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly
ECHO ***********COMPLETED 5 of 9***********

"%varFrameWorkDIR%" "%varSourcePath%\Source\Common\BMC.ConfigHelper\BMC.RegisterInstallation\BMC.RegisterInstallation.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly
ECHO ***********COMPLETED 5 of New Changes***********

"%varFrameWorkDIR%" "%varSourcePath%\Source\Common\BMC.Common\BMC.Common.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 
ECHO ***********COMPLETED 1 of 9***********

"%varFrameWorkDIR%" "%varSourcePath%\Source\Common\BMC.DataAccess\BMC.DataAccess.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 
ECHO ***********COMPLETED 2 of 9***********

"%varFrameWorkDIR%" "%varSourcePath%\Source\Common\BMC.Security\BMC.Security.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 
ECHO ***********COMPLETED 3 of 9***********

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\BMC.Monitoring\BMC.Monitoring.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 
ECHO ***********COMPLETED 4 of 9***********

"%varFrameWorkDIR%" "%varSourcePath%\Source\Common\BMC.CoreLib\BMC.CoreLib.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 
ECHO ***********COMPLETED 5 of 9***********

"%varFrameWorkDIR%" "%varSourcePath%\Source\Common\BMCEventsTransmitter\BMCEventsTransmitter.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 
ECHO ***********COMPLETED 6 of 9***********
pause

ECHO "BUILDING AUDIT VIEWER COMPONENT..."
"%varFrameWorkDIR%"  "%varSourcePath%\Source\Site Controller\AuditViewer\Audit.Transport\Audit.Transport.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 
ECHO ***********COMPLETED 7 of 9***********

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\AuditViewer\Audit.DBBuilder\Audit.DBBuilder.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 
ECHO ***********COMPLETED 8 of 9***********

"%varFrameWorkDIR%"  "%varSourcePath%\Source\Site Controller\AuditViewer\AuditBusiness\Audit.BusinessClasses.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 
ECHO ***********COMPLETED 9 of 9***********

"%varFrameWorkDIR%" "%varSourcePath%\Source\Common\BMC.Utilities\BMC.Utilities\BMC.Utilities\BMC.Utilities.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

PAUSE

EXIT

:LAST
ECHO EXITING COMMON BUILD SCRIPT
PAUSE