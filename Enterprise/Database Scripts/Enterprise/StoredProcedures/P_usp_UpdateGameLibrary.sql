USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateGameLibrary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateGameLibrary]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

     
--------------------------------------------------------------------------     
--    
-- Description: Insert/fetch the game id for the given game name    
-- Inputs:      See inputs     
--    
-- Outputs:             
--                      
-- =======================================================================    
--     
-- Revision History    
--    
-- Sudarsan S  12/10/2009  Created    
-- Renjish   02/11/2009      Modified to insert a record in AAMS_Details when a game is created.    
-- Yoganandh  06/01/2011  Modified to update HQ Game ID    
-- Yoganandh  18/01/2011  Modified to update Site ID    
------------------------------------------------------------------------------------------------------    
    
CREATE PROCEDURE [dbo].[usp_UpdateGameLibrary]
	@doc XML,
	@Game_ID INT OUTPUT
AS
BEGIN  
 DECLARE @idoc                 INT            
 DECLARE @Game                 VARCHAR(100)            
 DECLARE @Game_Part_Number     VARCHAR(20)          
 DECLARE @IsGameNameFromVLT    INT          
 DECLARE @GameTitleId          INT          
 DECLARE @IsRegulatoryEnabled  VARCHAR(50)            
 DECLARE @RegulatoryType       VARCHAR(50)            
 DECLARE @CheckPartNumber      VARCHAR(20)        
 DECLARE @HQGameID             INT          
 DECLARE @SiteCode             VARCHAR(50)          
 DECLARE @SiteID               INT          
 DECLARE @GameTitleAssignedID  INT         
 DECLARE @ManufacturerID  INT           
   
   
 EXEC sp_xml_preparedocument @idoc OUTPUT,  
      @doc              
   
 SELECT @SiteCode = Site_Code,  
        @HQGameID = HQ_Game_ID,  
        @Game = Game_Name,  
        @Game_Part_Number = Game_Part_Number,  
        @IsGameNameFromVLT = IsGameNameFromVLT,
        @ManufacturerID = HQ_Manufacturer_ID
 FROM   OPENXML(@idoc, './Site', 2) WITH   
        (  
            Site_Code VARCHAR(50) './SiteCode',  
            HQ_Game_ID INT './Games/Game_Library/MG_Game_ID',  
            Game_Name VARCHAR(100) './Games/Game_Library/MG_Game_Name',  
            Game_Part_Number VARCHAR(20)   
            './Games/Game_Library/Game_Part_Number',  
            IsGameNameFromVLT INT './Games/Game_Library/IsGameNameFromVLT',
            HQ_Manufacturer_ID INT './Games/Game_Library/Manufacturer/HQ_Manufacturer_ID'        
        )              
   
 EXEC sp_xml_removedocument @idoc              
   
 SELECT @SiteID = Site_ID  
 FROM   SITE  
 WHERE  Site_Code = @SiteCode           
   
 SELECT @Game_ID = MG_Game_ID,  
        @GameTitleAssignedID = ISNULL(MG_Group_ID, 0)  
 FROM   dbo.Game_Library  
 WHERE  MG_Game_Name = @Game and MG_Game_Manufacturer_ID= @ManufacturerID   
   
 IF ISNULL(@Game_ID, 0) = 0  
 BEGIN  
     INSERT INTO dbo.Game_Library  
       (  
         MG_Game_Name,  
         Game_Part_Number,  
         IsGameNameFromVLT,  
         MG_HQ_Game_ID,  
         Site_ID,
         MG_Game_Manufacturer_ID    
       )  
     VALUES  
       (  
         @Game,  
         @Game_Part_Number,  
         @IsGameNameFromVLT,  
         @HQGameID,  
         @SiteID,
         @ManufacturerID    
       )              
     SELECT @Game_ID = SCOPE_IDENTITY()  
 END            
   
 IF (ISNULL(@IsGameNameFromVLT,0) = 1)  
 BEGIN  
     IF NOT EXISTS (  
            SELECT Game_Title  
            FROM   Game_title  
            WHERE  Game_title = @Game  
        )  
     BEGIN  
         INSERT INTO Game_Title  
           (  
             Game_Title,  
             Game_Category_ID  
           )  
         VALUES  
           (  
             @Game,  
             1  
           )       
           
            INSERT INTO MeterAnalysis.dbo.Game_Title  
           (  
             Game_Title,  
             Game_Category_ID  
           )  
         VALUES  
           (  
             @Game,  
             1  
           )             
         SELECT @GameTitleId = SCOPE_IDENTITY() 
		 UPDATE Game_Library  
		 SET    MG_Group_Id = ISNULL(@GameTitleId,1)
         WHERE  MG_Game_Name = @Game AND MG_Game_Manufacturer_ID= @ManufacturerID
         
         UPDATE MeterAnalysis.dbo.Game_Library  
		 SET    MG_Group_Id = ISNULL(@GameTitleId,1)
         WHERE  MG_Game_Name = @Game AND MG_Game_Manufacturer_ID= @ManufacturerID      
     END  
     ELSE  
     BEGIN  
         SELECT @GameTitleId = Game_Title_ID  
         FROM   Game_Title  
         WHERE  Game_title = @Game  
     END  
 END    

  
UPDATE Game_Library  
 SET    IsGameNameFromVLT = ISNULL(@IsGameNameFromVLT,0)  
 WHERE  MG_Game_Name = @Game and MG_Game_Manufacturer_ID= @ManufacturerID   
 
 UPDATE MeterAnalysis.dbo.Game_Library
 SET    IsGameNameFromVLT = ISNULL(@IsGameNameFromVLT,0)  
 WHERE  MG_Game_Name = @Game and MG_Game_Manufacturer_ID= @ManufacturerID   

-- MOdified Script to address the particular site where the Game Came from w.r.t mapping/game title  
IF (ISNULL(@GameTitleId, 0) > 0)
BEGIN
	EXEC usp_InsertExportHistory @GameTitleId,
	     'GAMETITLE',
	     @SiteCode
END  
   
IF (ISNULL(@GameTitleAssignedID, 0) > 0)  
BEGIN
 	EXEC usp_InsertExportHistory @GameTitleAssignedID,
 	     'GAMETITLE',
 	     @SiteCode
END   
   
 INSERT INTO Export_History  
   (  
     EH_Date,  
     EH_Reference1,  
     EH_Type,  
     EH_Site_Code  
   )  
 SELECT GETDATE(),  
        [STR],  
        'GAMELIBRARY_MAPPING',  
        @SiteCode         
 FROM   dbo.iter_charlist_to_tbl(@Game_ID, ',')  
 WHERE  [STR] > 0  
END

GO