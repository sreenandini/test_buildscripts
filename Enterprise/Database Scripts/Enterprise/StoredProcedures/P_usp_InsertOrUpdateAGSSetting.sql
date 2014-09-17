USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertOrUpdateAGSSetting]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertOrUpdateAGSSetting]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

    
CREATE PROCEDURE [dbo].[usp_InsertOrUpdateAGSSetting]    
 @Setting_Value VARCHAR(8000)    
AS    
BEGIN    
 SET NOCOUNT ON      
     
 IF NOT EXISTS(    
        SELECT Setting_Name    
        FROM   Setting WITH(NOLOCK)    
        WHERE  Setting_Name = 'AGSValue'    
    )    
 BEGIN    
     INSERT INTO dbo.Setting    
       (    
         Setting_Name,    
         Setting_Value    
       )    
     VALUES    
       (    
         'AGSValue',    
         @Setting_Value    
       )    
 END    
 ELSE    
 BEGIN    
     UPDATE Setting    
     SET    Setting_Value = @Setting_Value    
     WHERE  Setting_Name = 'AGSValue'    
 END    
     
     
   
     
     
     --INSERT INTO Export_History    
     --  (    
     --    EH_Date,    
     --    EH_Reference1,    
     --    EH_Type,    
     --    EH_Site_Code    
     --  )    
     --SELECT GETDATE(),    
     --       Site_ID,    
     --       'SITESETUP',    
     --       Site_Code    
     --FROM   dbo.[Site]    
     --WHERE  ISNULL(Site_Code, '') <> ''    
-- Insert the setting value in Site Settings also  
DECLARE @SP_ID INT  
DECLARE @SM_ID INT  
SELECT @SP_ID = SettingsProfile_ID FROM SettingsProfile WHERE SettingsProfile_Description = 'Default Profile'  
  
  
IF NOT EXISTS(SELECT * FROM SettingsMaster WHERE SettingsMaster_Name = 'AGSValue')  
BEGIN  
   
 INSERT INTO SettingsMaster VALUES('AGSValue', 'DB', 'AGS Combination.' ,'N')   
 SELECT @SM_ID = SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AGSValue'  
 IF @SP_ID IS NOT NULL AND @SM_ID IS NOT NULL  
    INSERT INTO SettingsProfileItems VALUES(@SP_ID, @SM_ID, @Setting_Value)   
END  
ELSE  
 BEGIN  
  SELECT @SM_ID = SettingsMaster_ID FROM SettingsMaster WHERE SettingsMaster_Name = 'AGSValue'  
     IF @SP_ID IS NOT NULL AND @SM_ID IS NOT NULL  
     UPDATE SettingsProfileItems SET SettingsProfileItems_SettingsMaster_Values=@Setting_Value WHERE SettingsProfileItems_SettingsMaster_ID=@SM_ID  
   
 END  
   
   
INSERT INTO Export_History    
       (    
         EH_Date,    
         EH_Reference1,    
         EH_Type,    
         EH_Site_Code    
       )    
     SELECT   
    GETDATE(),    
            Site_ID,    
            'SITESETTINGS',    
            Site_Code    
     FROM   dbo.[Site]    
     WHERE  ISNULL(Site_Code, '') <> ''    
     
END     
  
  

GO

