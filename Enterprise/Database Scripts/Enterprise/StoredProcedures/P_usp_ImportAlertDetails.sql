/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 6/9/2014 2:32:42 PM
 ************************************************************/

USE Enterprise
GO

IF EXISTS (
       SELECT 1
       FROM   sys.objects o
       WHERE  o.[name] = 'usp_ImportAlertDetails'
              AND o.[type] = 'p'
   )
BEGIN
    DROP PROCEDURE usp_ImportAlertDetails
END

GO
/*
* Revision History
* 
* Anuradha			Created				29 May 2014
* 
* the stored procedure updates teh alert details 
*/

CREATE PROCEDURE usp_ImportAlertDetails
	@SiteCode VARCHAR(10),
	@doc VARCHAR(MAX),
	@IsSuccess BIT OUTPUT
AS
BEGIN
	SET NOCOUNT ON  
	
	
	DECLARE @idoc INT  
	EXEC sp_xml_preparedocument @idoc OUTPUT,
	     @doc  
	
	SELECT * INTO #tempAlert
	FROM   OPENXML(@idoc, '/AlertDetails', 2) 
	       WITH 
	       (
	           ID INT './ID',
	           AlertType VARCHAR(30) './AlertType',
	           AlertStatus INT './AlertStatus',
	           AlertMessage VARCHAR(MAX) './AlertMessage',
	           AlertReceived DATETIME './AlertReceivedOn',
	           AlertExportedDate DATETIME './AlertExportedDate'
	       )
	
	SET @IsSuccess = 1  
	
	IF NOT EXISTS (  
        SELECT *
        FROM   #tempAlert tA  
               JOIN AlertProcessHistory aph  
                    ON  tA.AlertReceived = aph.APE_Received_Date  
                    AND ta.AlertMessage = CAST(aph.APH_Message AS VARCHAR(MAX))  
	   )
	BEGIN
	    INSERT INTO AlertProcessHistory
	      (
	        -- APH_ID -- this column value is auto-generated,  
	        APH_AEH_ID,
	        APH_Site_Code,
	        APH_Type,
	        APH_Message,
	        APH_Status,
	        APE_Received_Date,
	        APH_Processed_Date,
	        APH_Result
	      )
	    SELECT tA.ID,
	           @SiteCode,
	           tA.AlertType,
	           tA.AlertMessage,
	           NULL,
	           tA.AlertReceived,
	           NULL,
	           NULL
	    FROM   #tempAlert tA
	END  
	
	EXEC sp_xml_removedocument @idoc 
	DROP TABLE #tempAlert
END
GO
