USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSetting]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetSetting]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GetSetting]     
   ( @SettingName    VARCHAR(100), @DefaultValue  VARCHAR(8000))
RETURNS VARCHAR(8000)   
AS    
BEGIN 
	DECLARE @SettingValue varchar(8000)
	SELECT @SettingName = ISNULL(SETTING_VALUE,@DefaultValue) FROM SETTING WHERE SETTING_NAME = @SettingName
	RETURN @SettingName
END 


GO

