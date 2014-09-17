USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_CheckTemplateNameExists]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_CheckTemplateNameExists]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_CheckTemplateNameExists  
 @TemplateName VARCHAR(50)   
AS  
BEGIN      
  
 IF Exists (SELECT 1
  FROM   AssetCreationTemplate      
  WHERE  TemplateName = @TemplateName )  
  RETURN 1  
   ELSE        
  RETURN 0  
  END   
GO

