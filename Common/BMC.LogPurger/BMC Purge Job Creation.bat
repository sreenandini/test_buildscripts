rem schtasks /create /tn "BMC Purge Job" /tr "\"C:\Program Files\Bally Technologies\Exchange Server\Purger\PurgeLogFiles.exe\"" /sc daily /st 00:01:00

rem pause

@Echo off

cd /d %~dp0
cd LogPurger
Set CURRENTDIR=%CD%
schtasks /create /tn "BMC Purge Job" /tr "\"%CURRENTDIR%\BMC.LogPurger.exe\"" /sc daily /st 00:01:00
pause



