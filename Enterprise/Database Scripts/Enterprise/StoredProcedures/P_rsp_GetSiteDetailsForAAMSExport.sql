USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSiteDetailsForAAMSExport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSiteDetailsForAAMSExport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================  
-- rsp_GetSiteDetailsForAAMSExport  
-- -----------------------------------------------------------------  
--  
-- Get the site details to be exported for AAMS.  
--   
-- -----------------------------------------------------------------      
-- Revision History         
--         
-- 19/11/09 Renjish Created        
-- =================================================================   
  
CREATE PROCEDURE dbo.rsp_GetSiteDetailsForAAMSExport  
@Site_ID INT,
@Type INT
AS  
  
DECLARE @SenderCode VARCHAR(20)

SELECT @SenderCode = Setting_Value FROM Setting WHERE Setting_Name = 'SenderCode'

IF @Type = 1
--For Depot
	BEGIN
		SELECT ISNULL(D.Depot_Financial_Code, '') AS Site_Fiscal_Code, ISNULL(D.Depot_Name, '') AS Site_Name, ISNULL(D.Depot_Address,'') AS Site_Address_1, '' AS Site_Address_2, '' AS Site_Address_3, 
		''AS Site_Address_4, ISNULL(D.Depot_Postcode, '') AS Site_Postcode, ISNULL(D.Depot_Street_Number, '') AS Site_Street_Number, ISNULL(D.Depot_Province, '') AS Site_Province, ISNULL(D.Depot_Municipality, '') AS Site_Municipality, 
		ISNULL(D.Depot_Cadastral_Code, '') AS Site_Cadastral_Code, ISNULL(D.Depot_Area, '') AS Site_Area, ISNULL(D.Depot_Location_Type, '') AS Site_Location_Type, '' AS Site_Closed, ISNULL(BAD.BAD_AAMS_Code,'') AS BAD_AAMS_Code,
		ISNULL(@SenderCode, '') AS SenderCode, ISNULL(D.Depot_Toponym, '') AS Site_Toponym
		FROM dbo.Depot D 
		LEFT JOIN BMC_AAMS_Details BAD ON D.Depot_ID = BAD.BAD_Reference_ID
		WHERE BAD.BAD_AAMS_Entity_Type = 2 AND Depot_ID = @Site_ID AND BAD.BAD_Is_Warehouse = 1
	END
ELSE
	BEGIN
		SELECT ISNULL(S.Site_Fiscal_Code, '') AS Site_Fiscal_Code, ISNULL(S.Site_Name, '') AS Site_Name, ISNULL(S.Site_Address_1,'') AS Site_Address_1, ISNULL(S.Site_Address_2,'') AS Site_Address_2, ISNULL(S.Site_Address_3,'') AS Site_Address_3, 
		ISNULL(S.Site_Address_4, '') AS Site_Address_4, ISNULL(S.Site_Postcode, '') AS Site_Postcode, ISNULL(S.Site_Street_Number, '') AS Site_Street_Number, ISNULL(S.Site_Province, '') AS Site_Province, ISNULL(S.Site_Municipality, '') AS Site_Municipality, 
		ISNULL(S.Site_Cadastral_Code, '') AS Site_Cadastral_Code, ISNULL(S.Site_Area, '') AS Site_Area, ISNULL(S.Site_Location_Type, '') AS Site_Location_Type, ISNULL(S.Site_Closed, '') AS Site_Closed, ISNULL(BAD.BAD_AAMS_Code,'') AS BAD_AAMS_Code,
		ISNULL(@SenderCode, '') AS SenderCode, ISNULL(S.Site_Toponym, '') AS Site_Toponym
		FROM dbo.Site S 
		LEFT JOIN BMC_AAMS_Details BAD ON S.Site_ID = BAD.BAD_Reference_ID
		WHERE BAD.BAD_AAMS_Entity_Type = 2 AND Site_ID = @Site_ID AND BAD.BAD_Is_Warehouse <> 1
	END

GO

