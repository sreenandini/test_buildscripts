@ECHO OFF

ECHO			COM FILES
SET varSourcePath="..\..\.."

IF NOT EXIST %varSourcePath% GOTO LAST

FOR /f "useback tokens=*" %%a in ('%varSourcePath%') do set varSourcePath=%%~a

SET varFrameWorkDIR="C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"

ECHO *********************************Start Unregistering Dlls *********************************

ECHO Unregistering 'BMCExComms.dll'
"c:\Windows\System32\regsvr32.exe" /s /u  "%varSourcePath%\Source\Site Controller\Comms\CommonComExchange\ComExchange\BMCExComms\x64\Release\BMCExComms.dll"
"c:\Windows\SysWOW64\regsvr32.exe" /s /u  "%varSourcePath%\Source\Site Controller\Comms\CommonComExchange\ComExchange\BMCExComms\Release\BMCExComms.dll"

ECHO Unregistering 'ComExchange.dll'
"c:\Windows\System32\regsvr32.exe" /s /u  "%varSourcePath%\Source\Site Controller\Comms\CommonComExchange\ComExchange\x64\Release\ComExchange.dll"
"c:\Windows\SysWOW64\regsvr32.exe" /s /u  "%varSourcePath%\Source\Site Controller\Comms\CommonComExchange\ComExchange\Release\ComExchange.dll"

ECHO Unregistering 'NoteCumTktScan.dll'
"c:\Windows\System32\regsvr32.exe" /s /u "%varSourcePath%\Source\Site Controller\Comms\SerialCommDll\SerialCommDll\x64\Release\NoteCumTktScan.dll"
"c:\Windows\SysWOW64\regsvr32.exe" /s /u "%varSourcePath%\Source\Site Controller\Comms\SerialCommDll\SerialCommDll\Release\NoteCumTktScan.dll"

ECHO Unregistering 'Gen2Printer.dll'
"c:\Windows\System32\regsvr32.exe" /s /u "%varSourcePath%\Source\Site Controller\Comms\SerialCommDll\SerialCommDll\x64\Release\Gen2Printer.dll"
"c:\Windows\SysWOW64\regsvr32.exe" /s /u "%varSourcePath%\Source\Site Controller\Comms\SerialCommDll\SerialCommDll\Release\Gen2Printer.dll"

ECHO Unregistering 'JetScan.dll'
"c:\Windows\System32\regsvr32.exe" /s /u "%varSourcePath%\Source\Site Controller\Comms\SerialCommDll\SerialCommDll\x64\Release\JetScan.dll"
"c:\Windows\SysWOW64\regsvr32.exe" /s /u "%varSourcePath%\Source\Site Controller\Comms\SerialCommDll\SerialCommDll\Release\JetScan.dll"

ECHO Unregistering 'Printercomm.dll'
"c:\Windows\System32\regsvr32.exe" /s /u "%varSourcePath%\Source\Site Controller\Comms\SerialCommDll\SerialCommDll\x64\Release\Printercomm.dll"
"c:\Windows\SysWOW64\regsvr32.exe" /s /u "%varSourcePath%\Source\Site Controller\Comms\SerialCommDll\SerialCommDll\Release\Printercomm.dll"

ECHO Unregistering 'SDGTicketGen.dll'
"c:\Windows\System32\regsvr32.exe" /s /u "%varSourcePath%\Source\Site Controller\Comms\SerialCommDll\SerialCommDll\x64\Release\SDGTicketGen.dll"
"c:\Windows\SysWOW64\regsvr32.exe" /s /u "%varSourcePath%\Source\Site Controller\Comms\SerialCommDll\SerialCommDll\Release\SDGTicketGen.dll"

ECHO Unregistering 'BMCExComms.dll'
"c:\Windows\System32\regsvr32.exe" /s /u  "%varSourcePath%\Source\Site Controller\Comms\Com Dlls\BMCExComms.dll"
"c:\Windows\SysWOW64\regsvr32.exe" /s /u  "%varSourcePath%\Source\Site Controller\Comms\Com Dlls\BMCExComms.dll"

ECHO Unregistering 'ComExchange.dll'
"c:\Windows\System32\regsvr32.exe" /s /u  "%varSourcePath%\Source\Site Controller\Comms\Com Dlls\ComExchange.dll"
"c:\Windows\SysWOW64\regsvr32.exe" /s /u  "%varSourcePath%\Source\Site Controller\Comms\Com Dlls\ComExchange.dll"

ECHO Unregistering 'NoteCumTktScan.dll'
"c:\Windows\System32\regsvr32.exe" /s /u "%varSourcePath%\Source\Site Controller\Comms\Com Dlls\NoteCumTktScan.dll"
"c:\Windows\SysWOW64\regsvr32.exe" /s /u "%varSourcePath%\Source\Site Controller\Comms\Com Dlls\NoteCumTktScan.dll"

ECHO Unregistering 'Gen2Printer.dll'
"c:\Windows\System32\regsvr32.exe" /s /u "%varSourcePath%\Source\Site Controller\Comms\Com Dlls\Gen2Printer.dll"
"c:\Windows\SysWOW64\regsvr32.exe" /s /u "%varSourcePath%\Source\Site Controller\Comms\Com Dlls\Gen2Printer.dll"

ECHO Unregistering 'JetScan.dll'
"c:\Windows\System32\regsvr32.exe" /s /u "%varSourcePath%\Source\Site Controller\Comms\Com Dlls\JetScan.dll"
"c:\Windows\SysWOW64\regsvr32.exe" /s /u "%varSourcePath%\Source\Site Controller\Comms\Com Dlls\JetScan.dll"

ECHO Unregistering 'Printercomm.dll'
"c:\Windows\System32\regsvr32.exe" /s /u "%varSourcePath%\Source\Site Controller\Comms\Com Dlls\Printercomm.dll"
"c:\Windows\SysWOW64\regsvr32.exe" /s /u "%varSourcePath%\Source\Site Controller\Comms\Com Dlls\Printercomm.dll"

ECHO Unregistering 'SDGTicketGen.dll'
"c:\Windows\System32\regsvr32.exe" /s /u "%varSourcePath%\Source\Site Controller\Comms\Com Dlls\SDGTicketGen.dll"
"c:\Windows\SysWOW64\regsvr32.exe" /s /u "%varSourcePath%\Source\Site Controller\Comms\Com Dlls\SDGTicketGen.dll"

ECHO *********************************Completing Unregistering Process*********************************

ECHO **********************************BUILD PROCESS STARTED**********************************

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\Comms\TicketingCOM\TicketingCOM\TicketingCOM.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

SET varFrameWorkDIR="C:\Windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe"

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\Comms\TicketingCOM\TicketingCOM\bin\x86\Release\TicketingCOM.dll" /tlb: "%varSourcePath%\Source\Site Controller\Comms\TicketingCOM\TicketingCOM\bin\x86\Release\TicketingCOM.tlb"
ECHO Ticketing Com Completed
pause

SET varFrameWorkDIR="C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe"

ECHO BUILDING BMCEXCOMMS
%varFrameWorkDIR% "%varSourcePath%\Source\Site Controller\Comms\CommonComExchange\ComExchange\BMCExComms\BMCExComms.vcxproj" /property:Configuration=Release;Platform=Win32 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 
pause

ECHO BUILDING COMEXCHANGE
%varFrameWorkDIR% "%varSourcePath%\Source\Site Controller\Comms\CommonComExchange\ComExchange\ComExchange\ComExchange.vcxproj" /property:Configuration=Release;Platform=Win32 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 
pause

ECHO BUILDING GEN2PRINTER
"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\Comms\SerialCommDll\SerialCommDll\Gen2Printer\Gen2Printer.vcxproj" /property:Configuration=Release;Platform=Win32 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

ECHO BUILDING JETSCAN
"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\Comms\SerialCommDll\SerialCommDll\JetScan\JetScan.vcxproj" /property:Configuration=Release;Platform=Win32 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

ECHO BUILDING NOTECUMTKTSCAN
"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\Comms\SerialCommDll\SerialCommDll\NoteCumTktScan\NoteCumTktScan.vcxproj" /property:Configuration=Release;Platform=Win32 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

ECHO BUILDING PRINTERCOMM
"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\Comms\SerialCommDll\SerialCommDll\Printercomm\Printercomm.vcxproj" /property:Configuration=Release;Platform=Win32 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 

ECHO BUILDING SDGTICKETGEN
"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\Comms\SerialCommDll\SerialCommDll\SDGTicketGen\SDGTicketGen.vcxproj" /property:Configuration=Release;Platform=Win32 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly; 


ECHO BUILDING ComConfigEditor

"%varFrameWorkDIR%" "%varSourcePath%\Source\Site Controller\Comms\ComConfigEditor\ComConfigEditor.csproj" /property:Configuration=Release;Platform=x86 /t:rebuild /fl /flp:logfile=Errors.log;Append;errorsonly

ECHO *********************************COMPLETING BUILD PROCESS*********************************
PAUSE

ECHO Creating Directory COM DLL for Comms"
IF NOT EXIST "%varSourcePath%\Source\Site Controller\Comms\Com Dlls" (
	MKDIR "%varSourcePath%\Source\Site Controller\Comms\Com Dlls"
) ELSE (
	ECHO COM DLL Directory Already Exist!!!
)


ECHO *********************************STARTED COPYING COM DLL'S TO A COMMON FOLDER*********************************

XCOPY /S /Q /Y /I /H "%varSourcePath%\Source\Site Controller\Comms\CommonComExchange\ComExchange\BMCExComms\Release\BMCExComms.dll" "%varSourcePath%\Source\Site Controller\Comms\Com Dlls"

XCOPY /S /Q /Y /I /H "%varSourcePath%\Source\Site Controller\Comms\CommonComExchange\ComExchange\ComExchange\Release\ComExchange.dll" "%varSourcePath%\Source\Site Controller\Comms\Com Dlls"

XCOPY /S /Q /Y /I /H "%varSourcePath%\Source\Site Controller\Comms\SerialCommDll\SerialCommDll\NoteCumTktScan\Release\NoteCumTktScan.dll" "%varSourcePath%\Source\Site Controller\Comms\Com Dlls"

XCOPY /S /Q /Y /I /H "%varSourcePath%\Source\Site Controller\Comms\SerialCommDll\SerialCommDll\Gen2Printer\Release\Gen2Printer.dll" "%varSourcePath%\Source\Site Controller\Comms\Com Dlls"

XCOPY /S /Q /Y /I /H "%varSourcePath%\Source\Site Controller\Comms\SerialCommDll\SerialCommDll\JetScan\Release\JetScan.dll" "%varSourcePath%\Source\Site Controller\Comms\Com Dlls"

XCOPY /S /Q /Y /I /H "%varSourcePath%\Source\Site Controller\Comms\SerialCommDll\SerialCommDll\Printercomm\Release\Printercomm.dll" "%varSourcePath%\Source\Site Controller\Comms\Com Dlls"

XCOPY /S /Q /Y /I /H "%varSourcePath%\Source\Site Controller\Comms\SerialCommDll\SerialCommDll\SDGTicketGen\Release\SDGTicketGen.dll" "%varSourcePath%\Source\Site Controller\Comms\Com Dlls"


ECHO *********************************COPYING COM DLL'S TO A COMMON FOLDER COMPLETED*********************************
pause

ECHO *********************************Start Registering Dlls For X86*********************************

ECHO Registering 'BMCExComms.dll'

"c:\Windows\SysWOW64\regsvr32.exe" /S -i "%varSourcePath%\Source\Site Controller\Comms\Com Dlls\BMCExComms.dll"

ECHO Registering 'ComExchange.dll'

"c:\Windows\SysWOW64\regsvr32.exe" /S -i "%varSourcePath%\Source\Site Controller\Comms\Com Dlls\ComExchange.dll"

ECHO Registering 'NoteCumTktScan.dll'

"c:\Windows\SysWOW64\regsvr32.exe" /S -i "%varSourcePath%\Source\Site Controller\Comms\Com Dlls\NoteCumTktScan.dll"

ECHO Registering 'Gen2Printer.dll'

"c:\Windows\SysWOW64\regsvr32.exe" /S -i "%varSourcePath%\Source\Site Controller\Comms\Com Dlls\Gen2Printer.dll"

ECHO Registering 'JetScan.dll'

"c:\Windows\SysWOW64\regsvr32.exe" /S -i "%varSourcePath%\Source\Site Controller\Comms\Com Dlls\JetScan.dll"

ECHO Registering 'Printercomm.dll'

"c:\Windows\SysWOW64\regsvr32.exe" /S -i "%varSourcePath%\Source\Site Controller\Comms\Com Dlls\Printercomm.dll"

ECHO Registering 'SDGTicketGen.dll'

"c:\Windows\SysWOW64\regsvr32.exe" /S -i "%varSourcePath%\Source\Site Controller\Comms\Com Dlls\SDGTicketGen.dll"

ECHO *********************************Completing Registering Dlls For X86*********************************

PAUSE
EXIT

:LAST
ECHO EXITING COM BUILD SCRIPT
PAUSE
