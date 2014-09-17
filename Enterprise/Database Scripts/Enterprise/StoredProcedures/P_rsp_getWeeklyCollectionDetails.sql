USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_getWeeklyCollectionDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_getWeeklyCollectionDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------                             
--                            
-- Description: Gets the weekly collection details for the given site code       
--              for the given week
--              
-- Inputs:     @Site_Code
--				@Week_ID
--                            
--                            
-- RETURN:      NONE                      
--                            
-- =======================================================================                            
--                             
-- Revision History                            
--                             
-- Madhu		19/09/2008     Created      
---------------------------------------------------------------------------                      
Create procedure [dbo].[rsp_getWeeklyCollectionDetails] 
(
	@Site_Code varchar(10),
	@Week_ID int
)
as

SELECT VW_CollectionData.*, [Zone].Zone_Name, VW_CollectionDetails.Collection_Days 
FROM ((VW_CollectionData  INNER JOIN Bar_Position ON VW_CollectionData.Bar_Position_ID = Bar_Position.Bar_Position_ID) 
LEFT JOIN [Zone] ON Bar_Position.Zone_ID = [Zone].Zone_ID) JOIN Site on VW_CollectionData.Site_Id = site.Site_ID 
JOIN Sub_Company ON Site.Sub_Company_ID = Sub_Company.Sub_Company_ID JOIN Company ON Company.Company_ID = Sub_Company.Company_ID 
LEFT JOIN Sub_Company_Area ON Site.Sub_Company_Area_ID = Sub_Company_Area.Sub_Company_Area_ID 
LEFT JOIN Sub_Company_District ON Site.Sub_Company_District_ID = Sub_Company_District.Sub_Company_District_ID 
LEFT JOIN Sub_Company_Region ON Site.Sub_Company_Region_ID = Sub_Company_Region.Sub_Company_Region_ID 
INNER JOIN VW_CollectionDetails ON VW_CollectionDetails.Collection_ID = VW_CollectionData.Collection_ID  
where site.site_code =@Site_Code and VW_CollectionData.Week_ID = @Week_ID ORDER BY VW_CollectionData.PosName

GO

