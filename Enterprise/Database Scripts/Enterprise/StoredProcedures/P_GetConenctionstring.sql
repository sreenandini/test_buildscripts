USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetConenctionstring]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetConenctionstring]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[GetConenctionstring]    
@Site_ID AS VARCHAR(200)    
AS    
BEGIN    
 SELECT Connectionstring     
 FROM Site WHERE Site_ID = @Site_ID    
END  
GO

