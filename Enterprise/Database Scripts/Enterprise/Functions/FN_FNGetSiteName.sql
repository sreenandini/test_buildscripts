USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FNGetSiteName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[FNGetSiteName]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[FNGetSiteName]      
(@SiteCode int)      
RETURNS varchar(50)      
AS      
BEGIN      
	declare @Return varchar(50)      
	SELECT @Return=Site_Name FROM SITE WHERE Site_code=@SiteCode
RETURN @return
END 

GO

