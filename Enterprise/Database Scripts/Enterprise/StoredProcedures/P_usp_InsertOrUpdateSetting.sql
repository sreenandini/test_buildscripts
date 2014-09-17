USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertOrUpdateSetting]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertOrUpdateSetting]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- usp_InsertUpdateSetting
-- -----------------------------------------------------------------
-- 
-- To insert or update setting values
-- 
-- -----------------------------------------------------------------
-- Revision History
-- 
-- 14/07/2012 Dinesh Rathinavel Created
-- 
-- =================================================================


CREATE PROCEDURE [dbo].[usp_InsertOrUpdateSetting]
	@Setting_Name VARCHAR(100),
	@Setting_Value VARCHAR(8000) OUTPUT
AS
BEGIN
	
	SET NOCOUNT ON

	IF NOT EXISTS(SELECT [Setting_Name] FROM [dbo].[Setting] WHERE [Setting_Name] = @Setting_Name)
	BEGIN
		INSERT INTO [dbo].[Setting]
		(
			[Setting_Name],
			[Setting_Value]
		)
		VALUES
		(
			@Setting_Name,
			@Setting_Value
		)
	END
	ELSE
	BEGIN
		UPDATE [dbo].[Setting]
			SET [Setting_Value] = @Setting_Value
		WHERE [Setting_Name] = @Setting_Name
	END

END

GO

