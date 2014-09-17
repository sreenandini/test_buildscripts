USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ImportAFTAuditHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ImportAFTAuditHistory]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------                           
--                          
-- Description: Get the data from XML and insert into AFTAuditHistory
--                          
-- Inputs:      NONE                    
--                          
-- Outputs:     
--                          

-- =======================================================================                          
--                           
-- Revision History                          
--                           
-- P SaravanaKumar     25/05/2009     Created    
---------------------------------------------------------------------------       

CREATE PROCEDURE [dbo].[usp_ImportAFTAuditHistory]   
@doc xml   
AS  
BEGIN  
	DECLARE @handle INT
	EXEC sp_xml_preparedocument @handle OUTPUT, @doc  

	CREATE  TABLE #Temp (
			AFT_Audit_ID BIGINT,
			AFT_TransactionDate DATETIME,
			AFT_TransactionType VARCHAR(30),
			AFT_Amount FLOAT,
			AFT_PlayerID INT,
			AFT_FirstName VARCHAR(50),
			AFT_LastName VARCHAR(50),
			AFT_ECash_Enabled BIT,
			AFT_Error_Code INT,
			AFT_Error_Message VARCHAR(100),
			Code VARCHAR(50))
	
	SELECT	AFT_Audit_ID,
			AFT_TransactionDate,
			AFT_TransactionType,
			AFT_Amount,
			AFT_PlayerID,
			AFT_FirstName,
			AFT_LastName,
			AFT_ECash_Enabled,
			AFT_Error_Code,
			AFT_Error_Message,
			Code
	INTO #AFTAD
	FROM OPENXML (@handle, './AFT_AuditHistorys/AFT_AuditHistory',2)  
    WITH #Temp

	INSERT INTO Site_AFT_AuditHistory
	SELECT	AFT_Audit_ID,
			AFT_TransactionDate,
			AFT_TransactionType,
			AFT_Amount,
			AFT_PlayerID,
			AFT_FirstName,
			AFT_LastName,
			AFT_ECash_Enabled,
			AFT_Error_Code,
			AFT_Error_Message,
			(SELECT Site_ID FROM [Site] WHERE Site_Code = Code) AS Site_ID
	FROM #AFTAD

	EXEC sp_xml_removedocument @handle
END
GO

