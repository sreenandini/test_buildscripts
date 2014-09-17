/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 7/16/2014 4:02:29 PM
 ************************************************************/

USE Enterprise
GO

IF EXISTS (
       SELECT 1
       FROM   sys.objects o
       WHERE  o.[name] = 'rsp_GetAllMailServerInfoinXML'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_GetAllMailServerInfoinXML
END

GO


/*
* Revision History	
* *********************************************************************************************************
* 
* Anuradha				Created				26 june 2014
*/

CREATE PROCEDURE rsp_GetAllMailServerInfoinXML
AS
BEGIN
	SELECT MSI_ServerName AS ServerName,
	       MSI_Port AS Port,
	       MSI_EnableSSL AS EnableSSL,
	       MSI_UID AS UID,
	       MSI_PWD AS PWD
	FROM   MailServerInfo msi
	       FOR XML PATH('ServerInfo'),
	       ROOT('MailServerInfo')
END
	