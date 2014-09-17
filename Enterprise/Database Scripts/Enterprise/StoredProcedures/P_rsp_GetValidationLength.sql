USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetValidationLength]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetValidationLength]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_GetValidationLength]  
 @Installation_No INT,  
 @Validation_Length INT OUTPUT  
AS    
    
BEGIN    
  
 SELECT @Validation_Length = ISNULL(MC.Validation_Length, 0)   
  FROM dbo.Machine_Class MC   
 INNER JOIN dbo.Machine M ON MC.Machine_Class_Id = M.Machine_Class_Id
 INNER JOIN dbo.Installation I ON I.Machine_Id = M.Machine_Id
   WHERE I.Installation_Id = @Installation_No
  
END    

GO

