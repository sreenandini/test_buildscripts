USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetCashDeskTransactions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetCashDeskTransactions]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.rsp_GetCashDeskTransactions
(@doc varchar(max)) 
AS
-- =================================================================
-- rsp_GetCashDeskTransactions
-- -----------------------------------------------------------------
--
-- returns cash desk transaction details.
-- 
-- -----------------------------------------------------------------    
-- Revision History       
--       
-- 09/09/09 Renjish Created      
-- 28/09/09 Vineetha Modified – Rearranged the order of HQ_Id to the bottom of the columns list
-- 18/01/2011 A.Vinod Kumar Modified – Actual values fetched from Treasury_Entry
-- =================================================================  
BEGIN 

	-- idoc is the document handle of the internal representation of an XML document which is created by calling sp_xml_preparedocument. 
	DECLARE @idoc int  
	-- internal variables 
	DECLARE @InstallationNo int 
	DECLARE @error  int  
	DECLARE @iRowCount  int 


	--Table Variable to hold the data temporarily.
	CREATE TABLE dbo.#tempInstallations(
		HQ_Installation_No INT,
		Installation_No INT)
	
	--add the encoding version as we need to process special characters like pound symbol 
	SET @doc='<?xml version="1.0" encoding="ISO-8859-1"?>' + @doc  

	--Create an internal representation of the XML document.
	EXEC sp_xml_preparedocument @idoc OUTPUT, @doc  



	--Copy the xml data to the table variable.
	INSERT INTO dbo.#tempInstallations
	SELECT * FROM 
	OPENXML (@idoc, '/Installations/Installation',2) 
	WITH dbo.#tempInstallations        
	--WITH (HQ_INSTALLATION_NO int './Installation/HQ_Installation_No')--,
	--	  INSTALLATION_NO int './Installation/Installation_No')    

	--Removes the internal representation of the XML document.
	EXEC sp_xml_removedocument @idoc 

	SELECT 
			TE.[Treasury_ID] AS Treasury_No
		  ,TE.[Collection_ID] AS Collection_No
		  ,TE.[Installation_ID] AS Installation_No
		  ,TE.[Treasury_User]
		  ,CONVERT(VARCHAR, TE.Treasury_Date + ' ' + TE.Treasury_Time) AS Treasury_Date
		  ,TE.[Treasury_Amount]
		  ,TE.[Treasury_Reason]
		  ,TE.[Treasury_Allocated]
		  ,TE.[Treasury_Type]
		  ,TE.[Treasury_Temp]
		  ,TE.[Treasury_Docket_No]
		  ,TE.[Treasury_Breakdown_2000p]
		  ,TE.[Treasury_Breakdown_1000p]
		  ,TE.[Treasury_Breakdown_500p]
		  ,TE.[Treasury_Breakdown_200p]
		  ,TE.[Treasury_Breakdown_100p]
		  ,TE.[Treasury_Breakdown_50p]
		  ,TE.[Treasury_Breakdown_20p]
		  ,TE.[Treasury_Breakdown_10p]
		  ,TE.[Treasury_Breakdown_5p]
		  ,TE.[Treasury_Breakdown_2p]
		  ,TE.[Treasury_Float_Issued_By]
		  ,TE.[Treasury_Float_Recovered_Total]
		  ,TE.[Treasury_Float_Recovered_2000p]
		  ,TE.[Treasury_Float_Recovered_1000p]
		  ,TE.[Treasury_Float_Recovered_500p]
		  ,TE.[Treasury_Float_Recovered_200p]
		  ,TE.[Treasury_Float_Recovered_100p]
		  ,TE.[Treasury_Float_Recovered_50p]
		  ,TE.[Treasury_Float_Recovered_20p]
		  ,TE.[Treasury_Float_Recovered_10p]
		  ,TE.[Treasury_Float_Recovered_5p]
		  ,TE.[Treasury_Float_Recovered_2p]
		  ,null AS Treasury_Issuer_User_No
		  ,TE.[Treasury_Membership_No]
		  ,CAST (TE.[Treasury_Actual_Date] AS VARCHAR(30)) [Treasury_Actual_Date]
		  ,TE.[AuthorizedUser_No]
		  ,CAST (TE.[Authorized_Date] AS VARCHAR(30)) [Authorized_Date]
		  ,TE.[Treasury_Reason_Code]
		  ,TE.[IsManualAttendantPay]
		  ,TE.[HQ_Treasury_No] AS HQ_ID
	FROM dbo.Treasury_Entry TE WITH(NOLOCK) 
	INNER JOIN #tempInstallations TI ON TE.Installation_ID = TI.HQ_Installation_No
	WHERE ISNULL(Collection_id,0) = 0

END



GO

