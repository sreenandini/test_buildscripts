USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnGetNames]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnGetNames]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- ===================================================================================================================================
-- [fnGetNames]
-- -----------------------------------------------------------------------------------------------------------------------------------
--
-- gets the Depot_Name/Operator_Name based on the ID passed
-- 

-- -----------------------------------------------------------------------------------------------------------------------------------
-- Revision History 
-- 
-- 16/05/2008	Sudarsan S	Created
-- ===================================================================================================================================

CREATE FUNCTION [dbo].[fnGetNames](
	@ID	INT,
	@Type	VARCHAR(10)
	
)
RETURNS VARCHAR(100)
AS

BEGIN

DECLARE @Name	VARCHAR(100)

SET @Name = CASE WHEN @Type = 'DEPOT' THEN (SELECT ISNULL(Depot_Name,'') FROM dbo.Depot WHERE Depot_ID = @ID)
				ELSE (SELECT ISNULL(Operator_Name, '') FROM dbo.Operator WHERE Operator_ID = @ID) END

RETURN ISNULL(@Name, '')

END

GO

