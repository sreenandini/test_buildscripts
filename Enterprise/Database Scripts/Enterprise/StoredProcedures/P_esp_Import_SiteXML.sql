USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[esp_Import_SiteXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[esp_Import_SiteXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE esp_Import_SiteXML       
  @Site_Code varchar(20),      
  @Type      varchar(50),      
  @SiteXML  text,      
  @EH_ID  int      
        
AS      
BEGIN   

SET  @Site_Code  = LTRIM(RTRIM(@Site_Code))     
    
IF LTRIM(RTRIM(@Type)) In ('NEWINSTALLATION', 'GMUCHANGEINSTALLATION', 'CONVERTINSTALLATION', 'REMOVEINSTALLATION', 'TREASURY')
BEGIN    
    INSERT INTO Import_History (    
                    IH_EH_ID    
                    ,IH_Site_Code    
                    ,IH_Type    
                    ,IH_Details    
                    ,IH_Status    
                    ,IH_Received_Date    
                    ,IH_Processed_Date)    
    VALUES    
        (@EH_ID,    
         @Site_Code,    
         @Type,    
         @SiteXML,    
         100,    
         GETDATE(),    
         GETDATE())    
    
    
END    
ELSE    
BEGIN    
    INSERT INTO Import_History (IH_Site_Code,IH_Type,IH_Details,IH_Received_Date, IH_EH_ID)      
                       VALUES  (@site_code,@type,@sitexml,GETDATE(), @EH_ID)      
END    
      
RETURN @@ERROR      
END 

GO

