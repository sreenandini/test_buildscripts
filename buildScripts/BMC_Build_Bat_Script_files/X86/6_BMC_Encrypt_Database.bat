ECHO ENCRYPTING ENTERPRISE AND EXCHANGE DATABASE 

ECHO Creating Directory ExchangeDatabase in "C:\ExchangeDatabase"
IF NOT EXIST "C:\ExchangeDatabase" (
	MKDIR "C:\ExchangeDatabase"
) ELSE (
	ECHO Directory Already Exist!!!
)

SET varSourcePath="..\..\..\"

IF NOT EXIST %varSourcePath% GOTO LAST

ECHO ENCRYPTING ENTERPRISE DATABASE STARTED
"%varSourcePath%\Source\Enterprise\Database Scripts\Consolidate.exe"

ECHO ENCRYPTING EXCHANGE DATABASE STARTED
"%varSourcePath%\Source\Site Controller\Database Scripts\Consolidate.exe"


:LAST
ECHO ENCRYPTING ENTERPRISE AND EXCHANGE DATABASE COMPLETED
PAUSE
