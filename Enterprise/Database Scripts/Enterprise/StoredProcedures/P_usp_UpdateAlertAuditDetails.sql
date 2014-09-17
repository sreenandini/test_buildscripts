/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 6/24/2014 6:08:38 PM
 ************************************************************/

USE Enterprise
GO

IF EXISTS (
       SELECT 1
       FROM   sys.objects o
       WHERE  o.[name] = 'usp_UpdateAlertAuditDetails'
              AND o.[type] = 'p'
   )
BEGIN
    DROP PROCEDURE usp_UpdateAlertAuditDetails
END

GO
/*
* Revision History
* 
* Anuradha			Created				29 May 2014
* 
* store the alert information in audit table.
*/


CREATE PROCEDURE usp_UpdateAlertAuditDetails
	@SiteCode VARCHAR(20),
	@doc VARCHAR(MAX),
	@APHID INT,
	@IsSuccess INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON  
	
	DECLARE @idoc INT  
	
	EXEC sp_xml_preparedocument @idoc OUTPUT,
	     @doc  
	
	DECLARE @tAudit TABLE 
	        (
	            AlertType VARCHAR(200),
	            AlertMessage VARCHAR(200),
	            [User] VARCHAR(20)
	        )  
	
	
	INSERT INTO @tAudit
	  (
	    AlertType,
	    Alertmessage,
	    [User]
	  )
	SELECT AlertType,
	       AlertMessage,
	       [User]
	FROM   OPENXML(@idoc, './Alert', 2) 
	       WITH 
	       (
	           AlertType VARCHAR(200) './AlertType',
	           AlertMessage VARCHAR(200) './AlertMessage',
	           [User] VARCHAR(20) './User'
	       )
	
	EXEC sp_xml_removedocument @idoc  
	
	    INSERT INTO EmailAlertDetails
	      (
	        -- EMD_ID -- this column value is auto-generated,  
	        EMD_Type_ID,
	        EMD_APH_ID,
	        EMD_Content,
	        EMD_SiteCode
	      )
	    SELECT at.AlertType_ID,
	           @APHID,
	           t.AlertMessage,
	           @SiteCode
	    FROM   @tAudit t
	           INNER JOIN AlertType at
	                ON  at.AlertType_Name = t.AlertType
	
	
	SELECT @IsSuccess = 0
END
GO