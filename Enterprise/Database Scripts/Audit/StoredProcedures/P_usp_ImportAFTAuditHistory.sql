USE [Audit]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ImportAFTAuditHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ImportAFTAuditHistory]
GO

USE [Audit]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_ImportAFTAuditHistory]        
@doc xml       
AS      
BEGIN      
 DECLARE @handle INT        
 EXEC sp_xml_preparedocument @handle OUTPUT, @doc          
        
 CREATE  TABLE #Temp (        
   AFT_Audit_ID BIGINT,        
   AFT_InstallationNo int,        
   AFT_TransactionDate DATETIME,        
   AFT_TransactionType VARCHAR(30),        
   --AFT_Amount FLOAT,        
   CashableAmt Float,        
   NoncashableAmt Float,        
   WATAmt Float,        
   AFT_PlayerID INT,        
   AFT_FirstName VARCHAR(50),        
   AFT_LastName VARCHAR(50),        
   AFT_ECash_Enabled BIT,        
   AFT_Error_Code INT,        
   AFT_Error_Message VARCHAR(100),        
   Code VARCHAR(50),
   TransferID INT  
 )          
           
 SELECT AFT_Audit_ID,          
   AFT_InstallationNo,         
   AFT_TransactionDate,          
   AFT_TransactionType,          
   --AFT_Amount,          
   cast (CashableAmt as decimal(10,2)) as CashableAmt,           
   cast (NoncashableAmt as decimal(10,2)) as NoncashableAmt,              
   cast (WATAmt as decimal(10,2)) WATAmt,            
   AFT_PlayerID,          
   AFT_FirstName,          
   AFT_LastName,          
   AFT_ECash_Enabled,          
   AFT_Error_Code,          
   AFT_Error_Message,          
   Code,  
   TransferID 
 INTO #AFTAD          
 FROM OPENXML (@handle, './AFT_AuditHistorys/AFT_AuditHistory',2)            
    WITH #Temp         
      
IF NOT EXISTS(SELECT * FROM Site_AFT_AuditHistory AFT  INNER JOIN #AFTAD AD ON  AFT.AFT_TransactionDate=AD.AFT_TransactionDate AND AFT.AFT_Audit_ID= AD.AFT_Audit_ID     
 AND AFT.AFT_InstallationNo = AD.AFT_InstallationNo AND AFT.AFT_TransactionType =  AD.AFT_TransactionType AND AFT.SITE_ID = (SELECT Site_ID FROM Enterprise.dbo.Site WHERE Site_Code = AD.Code) )     
Begin    
         
 INSERT INTO Site_AFT_AuditHistory          
 SELECT AFT_Audit_ID,          
   AFT_InstallationNo,          
   AFT_TransactionDate,          
   AFT_TransactionType,          
   --AFT_Amount,          
   cast (CashableAmt as decimal(10,2)) as CashableAmt,          
   cast (NoncashableAmt as decimal(10,2)) as NoncashableAmt,                
   cast (WATAmt as decimal(10,2)) WATAmt,               
   AFT_PlayerID,          
   AFT_FirstName,          
   AFT_LastName,          
   AFT_ECash_Enabled,          
   AFT_Error_Code,          
   AFT_Error_Message,          
   (SELECT Site_ID FROM Enterprise.dbo.Site WHERE Site_Code = Code) AS Site_ID    ,  
   TransferID       
 FROM #AFTAD          
          
End    
    
ELSE    
Begin    
    
 UPDATE Site_AFT_AuditHistory SET    
  AFT_TransactionDate = AD.AFT_TransactionDate,            
     AFT_TransactionType = AD.AFT_TransactionType,            
   --AFT_Amount,            
     CashableAmt = AD.CashableAmt ,            
     NoncashableAmt = AD.NoncashableAmt,                  
     WATAmt =AD.WATAmt,                 
             
   AFT_Error_Code = AD.AFT_Error_Code,            
   AFT_Error_Message = AD.AFT_Error_Message,  
   AFT_TransactionID = AD.TransferID   
  FROM Site_AFT_AuditHistory AS AFT    
   INNER JOIN #AFTAD AD ON    
 AFT.AFT_TransactionDate=AD.AFT_TransactionDate  AND AFT.AFT_Audit_ID= AD.AFT_Audit_ID     
 AND AFT.AFT_InstallationNo = AD.AFT_InstallationNo AND AFT.AFT_TransactionType =  AD.AFT_TransactionType AND AFT.SITE_ID = (SELECT Site_ID FROM Enterprise.dbo.Site WHERE Site_Code = AD.Code)     
End    
    
 EXEC sp_xml_removedocument @handle          
END          