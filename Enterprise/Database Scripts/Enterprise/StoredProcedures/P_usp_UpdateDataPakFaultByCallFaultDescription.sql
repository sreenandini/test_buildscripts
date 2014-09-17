  
  --EXEC  [usp_UpdateDataPakFaultByCallFaultDescription] 'Comms Failure'
  
USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_UpdateDataPakFaultByCallFaultDescription]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_UpdateDataPakFaultByCallFaultDescription]
GO



SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
  
CREATE PROCEDURE [dbo].[usp_UpdateDataPakFaultByCallFaultDescription]
@FaultTypeDesc Varchar(60)  
AS   
BEGIN  
 SET NOCOUNT ON
 DECLARE @CallFaultID INT 
 DECLARE @CallGroupId INT 
 SELECT @CallFaultID = Call_Fault_ID,@CallGroupId = Call_Group_ID 
 FROM CALL_FAULT  WHERE   
 Call_Fault_Description = @FaultTypeDesc   
  
 IF (ISNULL(@CallFaultID,0) > 0)  
 BEGIN  
  UPDATE DATAPAK_FAULT  
   SET CALL_FAULT_ID = @CallFaultID, TYPE =  @CallGroupId  
  FROM DATAPAK_FAULT  
  WHERE UPPER(DATAPAK_FAULT_TEXT) = UPPER(@FaultTypeDesc)  
    
    
 END  
   
 IF(@@ROWCOUNT > 0)  
  PRINT 'UPDATED DATA'  
 ELSE  
  PRINT 'UPDATE FAILED'  
END  