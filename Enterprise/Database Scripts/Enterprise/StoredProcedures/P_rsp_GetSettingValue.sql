USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSettingValue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSettingValue]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [rsp_GetSettingValue]
@SettingName VARCHAR(100)
AS
BEGIN
	SELECT Setting_Value FROM  setting WHERE setting_name = @SettingName
END


GO

