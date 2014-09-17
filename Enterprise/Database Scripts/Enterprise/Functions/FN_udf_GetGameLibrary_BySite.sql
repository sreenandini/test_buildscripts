USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_GetGameLibrary_BySite]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[udf_GetGameLibrary_BySite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------                           
--                          
-- Description: Gets the GameLibrary based upon the site id
--                          
-- Inputs:      NONE                    
--                          
-- Outputs:     Filtered GameLibrary data
--                          

-- =======================================================================================================                          
--                           
-- Revision History                          
--                           
-- A.Vinod Kumar		07/01/2011		Created	
-- Anuradha J		    23/03/2011	    Modified - Included condition to check for gamelibary site id.	
----------------------------------------------------------------------------------------------------------   
CREATE FUNCTION udf_GetGameLibrary_BySite 
(	
	@Site_Id INT
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT GM.[MG_Game_ID]
      ,GM.[MG_Game_Name]
      ,GM.[MG_Alias_Game_Name]
      ,GM.[MG_Game_Version]
      ,GM.[MG_Game_SerialNo]
      ,GM.[MG_Game_Manufacturer_ID]
      ,GM.[MG_Game_StartDate]
      ,GM.[MG_Game_EndDate]
      ,GM.[MG_SMI_Number]
      ,GM.[Game_Part_Number]
      ,GM.[MG_Game_CategoryID]
      ,GM.[MG_Group_ID]
      ,GM.[IsGameNameFromVLT]
      ,GM.[MG_HQ_Game_ID]
  FROM Game_Library GM 
		INNER JOIN Installation_Game_Info IGI ON IGI.Game_Part_Number = GM.Game_Part_Number AND IGI.Game_Name = GM.MG_Game_Name
		INNER JOIN Installation I ON I.Installation_ID = IGI.Installation_No		
		INNER JOIN Bar_Position B on B.Bar_Position_Id = I.Bar_Position_Id  	
		INNER JOIN Site ST ON ST.Site_ID = B.Site_ID  and GM.Site_Id = ST.Site_ID  	
  WHERE ST.Site_Code = @Site_Id							
  GROUP BY GM.[MG_Game_ID]
      ,GM.[MG_Game_Name]
      ,GM.[MG_Alias_Game_Name]
      ,GM.[MG_Game_Version]
      ,GM.[MG_Game_SerialNo]
      ,GM.[MG_Game_Manufacturer_ID]
      ,GM.[MG_Game_StartDate]
      ,GM.[MG_Game_EndDate]
      ,GM.[MG_SMI_Number]
      ,GM.[Game_Part_Number]
      ,GM.[MG_Game_CategoryID]
      ,GM.[MG_Group_ID]
      ,GM.[IsGameNameFromVLT]
	  ,GM.[MG_HQ_Game_ID]  
)

GO

