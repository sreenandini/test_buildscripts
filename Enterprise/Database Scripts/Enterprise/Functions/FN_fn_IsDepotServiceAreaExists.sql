USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_IsDepotServiceAreaExists]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].[fn_IsDepotServiceAreaExists]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION fn_IsDepotServiceAreaExists
(
	@ServiceArea    VARCHAR(2000),
	@ServiceAreaID  INT,
	@DepotID INT
	)
RETURNS BIT
AS
BEGIN
	DECLARE @status AS BIT
	SET @status = 0

	IF EXISTS (
			SELECT 1
			FROM Service_Areas WITH (NOLOCK)
			WHERE RTRIM(LTRIM(Service_Area_Name)) = RTRIM(LTRIM(@ServiceArea))
				AND Service_Area_ID <> @ServiceAreaID
				AND Depot_ID = @DepotID
			)
	BEGIN
		SET @status = 1
	END
	ELSE
	BEGIN
		SET @status = 0
	END
	RETURN @status
END
GO

