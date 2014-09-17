USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAAMSDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAAMSDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[rsp_GetAAMSDetails](@Site_Id INT)    
AS    

	SELECT	BAD.BAD_ID,BAD.BAD_Reference_ID,BAD.BAD_Asset_Serial_No,BAD.BAD_AAMS_Entity_Type,
			BAD.BAD_AAMS_Code,BAD.BAD_AAMS_Status,BAD.BAD_Verification_Status,BAD.BAD_Entity_Current_Status,
			BAD.BAD_Entity_Floor_Controller_Status,BAD.BAD_Entity_Command,BAD.BAD_Is_Warehouse,BAD.BAD_Updated_Date,
			BAD.BAD_Comments,BAD.BAD_Game_Name
	FROM bmc_aams_details BAD 
	INNER JOIN Machine M ON BAD.BAD_Asset_Serial_No  = M.Machine_Manufacturers_Serial_No 
	INNER JOIN Installation I ON M.Machine_ID = I.Machine_ID
	INNER JOIN Bar_position BP ON I.Bar_Position_ID = BP.Bar_Position_ID
	WHERE BAD.BAD_AAMS_Entity_Type = 3
	AND BP.Site_ID = (SELECT Site_ID from Site where Site_Code=@Site_Id)

	UNION
	
	SELECT	BAD_ID,BAD_Reference_ID,BAD_Asset_Serial_No,BAD_AAMS_Entity_Type,
			BAD_AAMS_Code,BAD_AAMS_Status,BAD_Verification_Status,BAD_Entity_Current_Status,
			BAD_Entity_Floor_Controller_Status,BAD_Entity_Command,BAD_Is_Warehouse,BAD_Updated_Date,
			BAD_Comments,BAD_Game_Name
	FROM bmc_aams_details 
	WHERE BAD_AAMS_Entity_Type = 4
	
	FOR XML PATH('AAMSDetail'), ROOT('AAMS')  ,TYPE, ELEMENTS XSINIL 
GO

