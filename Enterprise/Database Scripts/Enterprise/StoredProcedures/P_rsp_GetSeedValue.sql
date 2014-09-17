USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSeedValue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSeedValue]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
CREATE PROCEDURE [dbo].[rsp_GetSeedValue]  
(  
 @Site_Code AS INT,  
 @Table AS VARCHAR(100),  
 @SeedValue as INT OUTPUT  
)  
AS    
       
BEGIN  
 DECLARE @Site_Id int  
 SELECT @Site_Id = Site_Id FROM dbo.Site WHERE Site_Code = @Site_Code    
 DECLARE @sqlCommand nvarchar(1000)  
 IF @Table = 'MaintenanceHistory' OR @Table = 'MaintenanceSession'
 BEGIN  
  SET @sqlCommand = 'SELECT @SeedValue = ISNULL(MAX(ID),0)+1 FROM ' + @Table + ' WHERE Site_ID = ' + Cast(@Site_Id AS VARCHAR)  
 END  
 EXECUTE sp_executesql @sqlCommand, N'@SeedValue int output', @SeedValue = @SeedValue OUTPUT  
END    
  
/*  
declare @SeedValue as int  
exec [rsp_GetSeedValue] 1,'MaintenanceHistory',@SeedValue output   
select @SeedValue  
*/  
  

GO

