USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertProfileSetting]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertProfileSetting]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE usp_InsertProfileSetting
@SettingsProfile_Description VARCHAR(200)
AS
IF NOT EXISTS(SELECT SettingsProfile_Description FROM SettingsProfile WHERE SettingsProfile_Description = @SettingsProfile_Description)
INSERT INTO SettingsProfile VALUES(RTRIM(LTRIM(@SettingsProfile_Description)))

GO

