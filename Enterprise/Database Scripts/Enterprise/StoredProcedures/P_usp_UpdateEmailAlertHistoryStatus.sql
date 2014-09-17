USE Enterprise
GO

IF EXISTS (SELECT 1 FROM sys.objects o WHERE o.[name]='usp_UpdateEmailAlertHistoryStatus' AND o.[type]='P')
BEGIN
	DROP PROCEDURE usp_UpdateEmailAlertHistoryStatus
END  

GO

/*  
* Revision History  
* ******************************************************************************************  
* Update the Alert Audit history with status  
*   
* Anuradha   Created    12 June 2014  
*   
*/  
CREATE PROCEDURE usp_UpdateEmailAlertHistoryStatus  
(@ID INT, @Result VARCHAR(8000), @Status INT)        
AS    
BEGIN          
   
   IF @id IS NULL  SET @id =  0
    UPDATE           
        EmailAlertDetails          
    SET          
         EMD_Sent_Mail_Status = @Status,          
         EMD_Sent_Result = @Result   ,  
         EMD_SentDate =getdate()       
    WHERE          
        (@id = 0 OR (@id <> 0 AND  EMD_ID = @ID))       
       
            
END   

