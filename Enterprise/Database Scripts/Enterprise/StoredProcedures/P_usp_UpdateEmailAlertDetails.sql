/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 8/20/2014 5:28:10 PM
 ************************************************************/

USE Enterprise
GO

IF EXISTS (
       SELECT 1
       FROM   sys.objects o
       WHERE  o.[name] = 'usp_UpdateEmailAlertDetails'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE usp_UpdateEmailAlertDetails
END

GO


/*
* Revision History	
* *********************************************************************************************************
* 
* Anuradha				Created				26 june 2014
*/

CREATE PROCEDURE usp_UpdateEmailAlertDetails
AS
BEGIN
	SET NOCOUNT ON
	
	IF EXISTS (
	       SELECT 1
	       FROM   EmailAlertDetails ead
	       WHERE  ead.EMD_Sent_Mail_Status IS NULL
	              OR  ead.EMD_Sent_Mail_Status = -1
	   )
	BEGIN
	    UPDATE EmailAlertDetails
	    SET    -- EMD_ID = ? -- this column value is auto-generated,
	           EMD_Sent_Mail_Status = 500
	    WHERE  EMD_Sent_Mail_Status IS NULL
	           OR  EMD_Sent_Mail_Status = -1
	END
END

GO
