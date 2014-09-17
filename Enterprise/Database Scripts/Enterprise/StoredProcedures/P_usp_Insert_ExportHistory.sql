 USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Insert_ExportHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].usp_Insert_ExportHistory
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 
 
 CREATE PROCEDURE [dbo].usp_Insert_ExportHistory    
@Reference1 VARCHAR(50),    
@Type VARCHAR(50),  
@UserID INT,   
@Site_Code VARCHAR(200)    
AS                        
BEGIN        
    
 DECLARE @Site Table(      
 ID INT IDENTITY(1,1),      
 Site_Code VARCHAR(50))      
 DECLARE @Count INT      
 DECLARE @Current INT     
 DECLARE @SiteCode VARCHAR(50)      
    
 IF @Site_Code = 'ALL'      
 BEGIN      
      
   INSERT INTO @Site      
   SELECT Site_Code FROM SITE S  Inner join UserSite_lnk usl ON s.Site_ID = usl.SiteID AND usl.SecurityUserID =@UserID  
    WHERE ISNULL(Site_Code,'') <> ''      
      
   SELECT @Count = COUNT(Site_Code) FROM @Site      
   SET @Current = 1      
      
   WHILE (@Current <= @Count)      
    BEGIN      
   SELECT @SiteCode = Site_Code FROM @Site WHERE ID = @Current      
        
   INSERT INTO Export_History (EH_Date, EH_Reference1, EH_Type, EH_Site_Code)      
   VALUES(GETDATE(), @Reference1, @Type, @SiteCode)      
        
   SET @Current = @Current + 1      
    END        
 END      
 ELSE      
 BEGIN      
 INSERT INTO Export_History (EH_Date, EH_Reference1, EH_Type, EH_Site_Code)      
 VALUES(GETDATE(), @Reference1, @Type, @Site_Code)      
 END      
END 