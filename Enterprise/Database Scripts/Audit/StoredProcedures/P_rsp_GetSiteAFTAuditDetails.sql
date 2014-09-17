USE [Audit]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteAFTAuditDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteAFTAuditDetails]
GO

USE [Audit]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
CREATE PROCEDURE  rsp_GetSiteAFTAuditDetails      
      
   @FromDate	DATETIME,      
   @ToDate		DATETIME,        
   @Rows		INT,  
   @Sites		VARCHAR(100) = ''  
      
AS      
      
BEGIN         
   IF (@Rows <> 0 )     
      SET ROWCOUNT @rows  
       
 SELECT      
		AAH.AFT_Audit_ID AS [Audit ID],
		AAH.AFT_InstallationNo as [Installation No],
		AAH.AFT_TransactionDate AS [Transaction Date],
		AAH.AFT_TransactionType AS [Transaction Type],
		--AAH.AFT_Amount AS Amount,
AAH.cashableAmt AS CashableAmount,
AAH.NoncashableAmt AS NonCashableAmount,
AAH.WATAmt AS WATAmount,
		AAH.AFT_PlayerID AS [Player ID],
		AAH.AFT_FirstName AS [First Name],
		AAH.AFT_LastName AS [Last Name],
		AAH.AFT_ECash_Enabled AS [ECash Enabled],
		AAH.AFT_Error_Code AS [Error Code],
		AAH.AFT_Error_Message AS [Error Message],
		(SELECT Site_Name FROM Enterprise..Site WHERE Site_ID = AAH.Site_ID) AS [Site Name]
 FROM   
		Site_AFT_AuditHistory AAH
 WHERE        
        AAH.AFT_TransactionDate BETWEEN @FromDate AND @Todate        
	    AND AAH.Site_ID IN(
						SELECT (
								SELECT Site_ID 
								FROM Enterprise..Site 
								WHERE Site_ID = Data)
						FROM fnSplit ( @sites, ',' ) 
					 )
      ORDER BY AAH.AFT_TransactionDate DESC,AAH.AFT_Audit_ID DESC  
END

GO

