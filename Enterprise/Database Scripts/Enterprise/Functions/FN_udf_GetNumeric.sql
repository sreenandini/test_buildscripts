USE [Enterprise]
GO

/****** Object:  UserDefinedFunction [dbo].[udf_GetNumeric]    Script Date: 03/04/2014 11:56:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[udf_GetNumeric]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[udf_GetNumeric]
GO

USE [Enterprise]
GO

/****** Object:  UserDefinedFunction [dbo].[udf_GetNumeric]    Script Date: 03/04/2014 11:56:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[udf_GetNumeric]  
(@strAlphaNumeric VARCHAR(256))  
RETURNS VARCHAR(256)  
AS  
BEGIN  
DECLARE @intAlpha INT  
SET @intAlpha = PATINDEX('%[^0-9]%', @strAlphaNumeric)  
BEGIN  
WHILE @intAlpha > 0  
BEGIN  
SET @strAlphaNumeric = STUFF(@strAlphaNumeric, @intAlpha, 1, '' )  
SET @intAlpha = PATINDEX('%[^0-9]%', @strAlphaNumeric )  
END  
END  
RETURN ISNULL(@strAlphaNumeric,0)  
END  

GO


