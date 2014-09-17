USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ImportAFTTransaction]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ImportAFTTransaction]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


    
-- ===================================================================================================================================    
-- StoredProcedure [dbo].[usp_InsertAFTTransaction]    
-- -----------------------------------------------------------------------------------------------------------------------------------    
--    
--Insert the transaction details related to AFT    
-- -----------------------------------------------------------------------------------------------------------------------------------    
-- Revision History     
--     
-- 27 Apr 2010  Anuradha    created    
-- ===================================================================================================================================    
CREATE PROCEDURE usp_ImportAFTTransaction
(  
 @doc xml  
)  
as  
  
declare @InstallationID int  
declare @SiteCode varchar(20)  
  
DECLARE @docHandle int    
--   
--SET @doc='<?xml version="1.0" encoding="ISO-8859-1"?>' + @doc 


    EXEC sp_xml_PrepareDocument @docHandle OUTPUT, @doc   
SELECT @InstallationID = HQID,  
 @SiteCode = SiteCode 
 FROM OPENXML (@docHandle,  '/AFTTransactionDetails/Transactions',2)    
with    
(    
 HQID int 'HQID',    
 SiteCode varchar(50) 'SiteCode'  
  
) x     

                
          
    CREATE TABLE dbo.#TempAFT          
        (              
		
			Installation_No	int,
			Player_ID	int,
			Collection_No	int,
			WAT_Out float,
			Promo_Cashable_EFT_OUT float,
			NonCashable_EFT_OUT float,
			Transaction_Date	datetime,
			Transaction_Type	varchar(50),
			TransferID	int,
			AccountType	varchar,
			TransactionStatus	bit,	
			SiteCode varchar(20)
        )              
          
          
               
              
    --Create an internal representation of the XML document.                      
              
    insert into #TempAFT     
  (    
   Player_ID,    
   Collection_No,    
   WAT_Out,    
   Promo_Cashable_EFT_OUT,    
   NonCashable_EFT_OUT,    
   Transaction_Date,    
   Transaction_Type,    
   TransferID,    
   AccountType,    
   TransactionStatus,    
   SiteCode)                  
    SELECT A.* FROM OPENXML(@docHandle, './AFTTransactionDetails/Transactions/AFT_Transactions', 2) WITH                    
        (                
        Player_ID int './Player_ID',     
  Collection_No int './Collection_No',     
  WAT_Out float './WAT_Out',     
  Promo_Cashable_EFT_OUT float './Promo_Cashable_EFT_OUT',     
  NonCashable_EFT_OUT float './NonCashable_EFT_OUT',     
  Transaction_Date datetime './Transaction_Date',     
  Transaction_Type varchar(50) './Transaction_Type',     
  TransferID int './TransferID',     
  AccountType varchar './AccountType',     
  TransactionStatus bit './TransactionStatus',    
  SiteCode  varchar(20)   './SiteCode'    
      
     
  ) AS A        
    
Update   #TempAFT set Installation_No =    @InstallationID , SiteCode = @Sitecode            
              
    IF @@Error <> 0                  
    BEGIN                  
        GOTO ErrorHandler                  
    END                  
              
      
if not exists(select * from aft_transactions TE inner join  dbo.#TempAFT T      
 ON T.Installation_No = TE.Installation_No  and TE.Transaction_date = T.Transaction_date  )  
begin  
        INSERT INTO AFT_Transactions(                      
            Installation_No,    
   Player_ID,    
   Collection_No,    
   WAT_Out,    
   Promo_Cashable_EFT_OUT,    
   NonCashable_EFT_OUT,    
   Transaction_Date,    
   Transaction_Type,    
   TransferID,    
   AccountType,    
   TransactionStatus,    
   SiteCode)                                        
        SELECT                      
            T.Installation_No,    
   T.Player_ID,    
   T.Collection_No,    
   T.WAT_Out,    
   T.Promo_Cashable_EFT_OUT,    
   T.NonCashable_EFT_OUT,    
   T.Transaction_Date,    
   T.Transaction_Type,    
 T.TransferID,    
   T.AccountType,    
   T.TransactionStatus,    
   T.SiteCode    
        FROM                
            dbo.#TempAFT T      
  LEFT JOIN dbo.AFT_Transactions TE ON T.Installation_No = TE.Installation_No  and TE.Transaction_date = T.Transaction_date           
 
end  
  
else   
begin  
   Update AFT_Transactions set TransactionStatus = T.TransactionStatus  
    FROM                
 AFT_Transactions TE  
 Inner JOIN #TempAFT T  ON T.Installation_No = TE.Installation_No  and TE.Transaction_date = T.Transaction_date   
end   
                    
    EXEC sp_xml_RemoveDocument @docHandle                     
    RETURN 0                      
    ErrorHandler:                      
    RETURN -99                      
    
    

GO

