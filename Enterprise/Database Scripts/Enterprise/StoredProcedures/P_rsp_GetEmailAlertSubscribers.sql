/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 6/23/2014 4:11:45 PM
 ************************************************************/

USE Enterprise
GO

IF EXISTS (
       SELECT 1
       FROM   sys.objects o
       WHERE  o.[name] = 'rsp_GetEmailAlertSubscribers'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_GetEmailAlertSubscribers
END
GO

/*
* *******************************************************************************************************
* Revision History
* 
* Anuradha			Created			23rd June 2014
* 
* get the list of subscribers for sending email Alert.
*/

CREATE PROCEDURE rsp_GetEmailAlertSubscribers
@AlertType VARCHAR(50) = NULL
AS
BEGIN
	SET NOCOUNT ON
	
	if (@alerttype ='') set @AlertType = null
	
	SELECT as1.AS_ID,
	      at.AlertType_Name,
	       as1.AS_To_Mail,
	       as1.AS_CC_Mail,
	       as1.AS_BCC_Mail
	FROM   EmailSubscriberDetails as1
	INNER JOIN EmailSubscriberAlertlnk al ON al.ESD_ID= as1.AS_ID
	INNER JOIN AlertType at ON al.AT_ID = at.AlertType_ID
	WHERE (@AlertType IS NULL OR (@AlertType IS NOT NULL AND  at.AlertType_Name = @AlertType))
END

GO

