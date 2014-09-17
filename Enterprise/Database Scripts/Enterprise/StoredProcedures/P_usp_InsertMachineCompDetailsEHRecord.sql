USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertMachineCompDetailsEHRecord]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertMachineCompDetailsEHRecord]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =================================================================    
-- usp_InsertMachineCompDetailsEHRecord    
-- -----------------------------------------------------------------    
--    
-- Insert Comp Details records in Export_History.      
--     
-- -----------------------------------------------------------------        
-- Revision History           
--           
-- 29/05/10 Renjish Created          
-- =================================================================     
    
CREATE PROCEDURE dbo.usp_InsertMachineCompDetailsEHRecord    
@ID INT,    
@Site_Code VARCHAR(50)  
AS    
  
SET NOCOUNT ON  
  
DECLARE @Count INT  
DECLARE @Current INT  
  
DECLARE @Site Table(  
ID INT IDENTITY(1,1),  
Site_Code VARCHAR(50))   
  
IF @ID <> 0  
BEGIN  
 IF @Site_Code = 'ALL'  
 BEGIN  
  DECLARE @SiteCode VARCHAR(50)  
  
  INSERT INTO @Site  
  SELECT Site_Code FROM dbo.Site WHERE ISNULL(Site_Code,'') <> ''  
  
  SELECT @Count = COUNT(Site_Code) FROM @Site  
  SET @Current = 1  
  
  WHILE (@Current <= @Count)  
  BEGIN  
    SELECT @SiteCode = Site_Code FROM @Site WHERE ID = @Current  
  
    INSERT INTO dbo.Export_History (EH_Date, EH_Reference1, EH_Type, EH_Site_Code)  
    VALUES(GETDATE(), @ID, 'MACHINECOMPDETAILS', @SiteCode)  
  
    SET @Current = @Current + 1  
  END    
 END  
 ELSE  
 BEGIN  
  INSERT INTO dbo.Export_History (EH_Date, EH_Reference1, EH_Type, EH_Site_Code)  
  VALUES(GETDATE(), @ID, 'MACHINECOMPDETAILS', @Site_Code)  
 END  
END  
  
SET NOCOUNT OFF  


GO

