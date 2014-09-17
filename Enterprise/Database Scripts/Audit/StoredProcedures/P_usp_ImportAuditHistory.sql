USE [Audit]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ImportAuditHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ImportAuditHistory]
GO

USE [Audit]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_ImportAuditHistory]   
@doc xml   
AS  
BEGIN  
	DECLARE @handle INT
	EXEC sp_xml_preparedocument @handle OUTPUT, @doc  
	
	CREATE TABLE #Temp(
			Audit_ID BIGINT,
			Audit_Date DATETIME,
			Audit_User_ID INT,
			Audit_User_Name VARCHAR(50),
			Audit_Module_ID INT,
			Audit_Module_Name VARCHAR(50),
			Audit_Screen_Name VARCHAR(50),
			Audit_Desc VARCHAR(500),
			Audit_Slot VARCHAR(50),
			Audit_Field  VARCHAR(100),
			Audit_Old_Vl VARCHAR(500),
			Audit_New_Vl VARCHAR(500),
			Audit_Operation_Type VARCHAR(25),
			Code VARCHAR(50))

	SELECT	Audit_ID,
			Audit_Date,
			Audit_User_ID,
			Audit_User_Name,
			Audit_Module_ID,
			Audit_Module_Name,
			Audit_Screen_Name,
			Audit_Desc,
			Audit_Slot,
			Audit_Field,
			Audit_Old_Vl,
			Audit_New_Vl,
			Audit_Operation_Type,
			Code
	INTO #AD
	FROM OPENXML (@handle, 'Audit_Historys/Audit_History',2)  
    WITH #Temp	

	INSERT INTO Site_Audit_History
	SELECT	Audit_ID,
			Audit_Date,
			Audit_User_ID,
			Audit_User_Name,
			Audit_Module_ID,
			Audit_Module_Name,
			Audit_Screen_Name,
			Audit_Desc,
			Audit_Slot,
			Audit_Field,
			Audit_Old_Vl,
			Audit_New_Vl,
			Audit_Operation_Type,
			(SELECT Site_ID FROM Enterprise..Site WHERE Site_Code = Code) AS Site_ID
	FROM #AD

	EXEC sp_xml_removedocument @handle
END


GO

