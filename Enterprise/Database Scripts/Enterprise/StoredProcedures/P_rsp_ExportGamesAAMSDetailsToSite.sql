USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportGamesAAMSDetailsToSite]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportGamesAAMSDetailsToSite]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =================================================================    
-- rsp_ExportGamesAAMSDetailsToSite    
-- -----------------------------------------------------------------    
--    
-- Insert AAMS records in Export_History.      
--     
-- -----------------------------------------------------------------        
-- Revision History           
--           
-- 02/02/10 Renjish Created          
-- =================================================================     
    
CREATE PROCEDURE dbo.rsp_ExportGamesAAMSDetailsToSite    
@Site_Code VARCHAR(50)  
AS    
BEGIN
INSERT INTO Export_History (EH_Date, EH_Reference1, EH_Type, EH_Site_Code)
SELECT GETDATE(), BAD_ID, 'AAMSCONFIG', @Site_Code FROM BMC_AAMS_Details WHERE BAD_AAMS_Entity_Type = 4 AND BAD_Game_Name IS NOT NULL   

INSERT INTO Export_History  
       	(  
         	EH_Date,  
         	EH_Reference1,  
         	EH_Type,  
         	EH_Site_Code  
       	)  
       
     	SELECT GETDATE(),  
            	MG_Game_ID,  
            	'GAMELIBRARY_MAPPING',  
            	@Site_Code  
     	FROM   Game_Library WITH(NOLOCK)

END

GO

