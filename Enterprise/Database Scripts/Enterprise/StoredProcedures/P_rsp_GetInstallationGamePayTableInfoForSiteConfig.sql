USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetInstallationGamePayTableInfoForSiteConfig]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetInstallationGamePayTableInfoForSiteConfig]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------                           
--                          
-- Description: Get the data from InstallationGamePayTableInfo and convert that into XML
--                          
-- Inputs:      NONE                    
--                          
-- Outputs:     Game InstallationGamePayTableInfo XML Data
--                          

-- =======================================================================                          
--                           
-- Revision History                          
--                           
-- A.Vinod Kumar	07/01/11	Created
---------------------------------------------------------------------------                    

CREATE PROCEDURE [dbo].[rsp_GetInstallationGamePayTableInfoForSiteConfig]
(	
	@Site_Id INT
)
AS
BEGIN
DECLARE @XMLData XML
	SET @XMLData = (
	SELECT IGPI.[IGPI_ID]
			,IGPI.[IGPI_Installation_ID]
			,IGPI.[IGPI_Game_ID]
			,IGPI.[IGPI_Paytable_ID]
	FROM dbo.udf_GetInstallationGamePayTableInfo_BySite(@Site_id) AS IGPI
	ORDER BY IGPI.[IGPI_ID]
	FOR XML PATH('Game_Info'), ELEMENTS XSINIL, ROOT('Game'))
SELECT CONVERT(VARCHAR(MAX),@XMLData)   
END


GO

