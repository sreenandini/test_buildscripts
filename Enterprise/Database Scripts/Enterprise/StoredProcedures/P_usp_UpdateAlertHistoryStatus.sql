USE Enterprise
GO

IF EXISTS (SELECT 1 FROM sys.objects o WHERE o.[name]='usp_UpdateAlertHistorystatus' AND o.[type]='P')
BEGIN
	DROP PROCEDURE usp_UpdateAlertHistoryStatus
END
GO

/*
* Revision History
* ******************************************************************************************
* Update the Alert Process history with status
* 
* Anuradha			Created				12 June 2014
* 
*/
CREATE PROCEDURE usp_UpdateAlertHistoryStatus
(@ID INT, @Result VARCHAR(8000), @Status INT)      
AS  
BEGIN        
   BEGIN TRAN   
    UPDATE       
        AlertProcessHistory      
    SET      
         APH_Status = @Status,      
         APH_Result = @Result,      
         APH_Processed_Date = Getdate()      
    WHERE      
         APH_ID = @ID   
  
 IF @@ERROR <> 0  
  ROLLBACK TRAN  
 ELSE  
  COMMIT TRAN     
        
END  

GO
