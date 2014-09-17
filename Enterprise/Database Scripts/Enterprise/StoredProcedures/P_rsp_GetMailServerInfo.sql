/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 7/8/2014 6:25:56 PM
 ************************************************************/


USE Enterprise
GO

/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 6/23/2014 11:48:48 AM
 ************************************************************/

IF EXISTS (
       SELECT 1
       FROM   sys.objects o
       WHERE  o.[name] = 'rsp_GetMailServerInfo'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_GetMailServerInfo
END
GO

/*  
* Revision History  
*   
* Anuradha   Created    29 May 2014  
*   
* save the mail server information
*/  
--exec rsp_GetMailServerInfo 


CREATE PROCEDURE rsp_GetMailServerInfo
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT MSI_ID AS ID,
	       MSI_ServerName AS ServerName,
	       MSI_Port AS Port,
	       MSI_EnableSSL AS EnableSSL,
	       MSI_UID AS UID,
	       MSI_PWD AS PWD
	FROM   MailServerInfo
END

GO

