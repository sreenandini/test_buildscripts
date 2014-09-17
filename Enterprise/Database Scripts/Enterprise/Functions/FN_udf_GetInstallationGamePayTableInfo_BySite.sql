USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_GetInstallationGamePayTableInfo_BySite]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[udf_GetInstallationGamePayTableInfo_BySite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------                           
--                          
-- Description: Gets the InstallationGamePayTableInfo based upon the site id
--                          
-- Inputs:      NONE                    
--                          
-- Outputs:     Filtered InstallationGamePayTableInfo data
--                          

-- =======================================================================                          
--                           
-- Revision History                          
--                           
-- A.Vinod Kumar		07/01/2011		Created		
---------------------------------------------------------------------------   
CREATE FUNCTION udf_GetInstallationGamePayTableInfo_BySite 
(	
	@Site_Id INT
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT IGPI.[IGPI_ID]
			,IGPI.[IGPI_Installation_ID]
			,IGPI.[IGPI_Game_ID]
			,IGPI.[IGPI_Paytable_ID]
	FROM [dbo].[Installation_Game_Paytable_Info] AS IGPI
		INNER JOIN Game_Library GM ON GM.MG_HQ_Game_ID = IGPI.IGPI_Game_ID
		INNER JOIN PayTable PT ON PT.Game_ID = GM.MG_Game_ID AND PT.HQ_PayTable_ID = IGPI.[IGPI_Paytable_ID]
		INNER JOIN Installation_Game_Info IGI ON IGI.Game_Part_Number = GM.Game_Part_Number AND IGI.Game_Name = GM.MG_Game_Name
		INNER JOIN Installation I ON I.Installation_ID = IGI.Installation_No AND I.Installation_ID = IGPI.[IGPI_Installation_ID]
		INNER JOIN Bar_Position B on B.Bar_Position_Id = I.Bar_Position_Id  	
		INNER JOIN Site ST ON ST.Site_ID = B.Site_ID
  WHERE ST.Site_Code = @Site_Id							
  GROUP BY IGPI.[IGPI_ID]
			,IGPI.[IGPI_Installation_ID]
			,IGPI.[IGPI_Game_ID]
			,IGPI.[IGPI_Paytable_ID] 
)

GO

