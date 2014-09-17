/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 7/1/2014 5:03:37 PM
 ************************************************************/

USE Enterprise
GO

IF EXISTS (
       SELECT 1
       FROM   sys.objects o
       WHERE  o.[name] = 'rsp_GetAllEmailSubscribersinXML'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_GetAllEmailSubscribersinXML
END

GO


/*
* Revision History	
* *********************************************************************************************************
* 
* Anuradha				Created				26 june 2014
*/

CREATE PROCEDURE rsp_GetAllEmailSubscribersinXML
AS
BEGIN
SELECT
		as1.AS_ID as AID,
		at.AlertType_ID AS ID,
		at.AlertType_Name AS TypeName,
		as1.AS_Subject AS [SUBJECT],
		as1.AS_From_Mail AS FromMail,
		as1.AS_To_Mail AS ToMail,
		as1.AS_CC_Mail AS CCMail,
		as1.AS_BCC_Mail AS BCCMail
	FROM
		EmailSubscriberDetails as1
		INNER JOIN EmailSubscriberAlertlnk esa ON as1.AS_ID = esa.ESD_ID
		INNER JOIN AlertType at ON esa.AT_ID= at.AlertType_ID	
		
		FOR XML PATH('ID'), ROOT('MailRoot')
END
	