USE Enterprise
GO

IF EXISTS (SELECT 1 FROM sys.objects o WHERE o.[name]='rsp_GetEmailSubscriberDetails' AND o.[type]='P')
BEGIN
	DROP PROCEDURE rsp_GetEmailSubscriberDetails
END 

GO
/*
* Revision History
* ************************************************************************************
* 
* Anuradha				Created					30 June 2014
* 
* this returns the list of configured email subscribers to send alerts.
*/


CREATE PROCEDURE rsp_GetEmailSubscriberDetails
@AlertType VARCHAR(50) = NULL
As	

BEGIN
	SET NOCOUNT ON
	
		if (@alerttype ='') set @AlertType = null
	SELECT
		as1.AS_ID,
		at.AlertType_ID AS ID,
		at.AlertType_Name AS TypeName,
		as1.AS_Subject AS [SUBJECT],
		as1.AS_From_Mail AS FromMail,
		as1.AS_To_Mail AS ToMail,
		as1.AS_CC_Mail AS CCMail,
		as1.AS_BCC_Mail AS BCCMail
	FROM
		EmailSubscriberDetails as1
		INNER JOIN EmailSubscriberAlertlnk al ON al.ESD_ID= as1.AS_ID
	INNER JOIN AlertType at ON al.AT_ID = at.AlertType_ID
		WHERE (@AlertType IS NULL OR (@AlertType IS NOT NULL AND  at.AlertType_Name = @AlertType))
END

GO
