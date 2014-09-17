USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ecUpdateSubCompanyArea]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ecUpdateSubCompanyArea]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_ecUpdateSubCompanyArea
	@SubCompanyAreaID INT = 0,
	@SubCompanyAreaName VARCHAR(50),
	@SubCompanyAreaDescription VARCHAR(50) = '',
	@Staff_ID INT = NULL,
	@SubCompanyRegionID INT,
	@ErrorCode INT = 0 OUTPUT,
	@ErrorMessage VARCHAR(100) = '' OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	IF EXISTS (
	       SELECT 1
	       FROM   Sub_Company_Area sca
	       WHERE  sca.Sub_Company_Area_Name = @SubCompanyAreaName
	              AND sca.Sub_Company_Region_ID = @SubCompanyRegionID
	              AND (
	                      @SubCompanyAreaID = 0
	                      OR sca.Sub_Company_Area_ID <> @SubCompanyAreaID
	                  )
	   )
	BEGIN
	    SET @ErrorMessage = 'MSG_AREANAME_EXISTS'
	    SET @ErrorCode = 1
	    RETURN
	END
	
	IF @SubCompanyAreaID = 0
	BEGIN
		INSERT INTO Sub_Company_Area
		(
			Sub_Company_Region_ID,
			Sub_Company_Area_Name,
			Staff_ID,
			Sub_Company_Area_Description
		)
		VALUES
		(
			@SubCompanyRegionID,
			@SubCompanyAreaName,
			@Staff_ID,
			@SubCompanyAreaDescription
		)
		
		
		INSERT INTO MeterAnalysis.dbo.Sub_Company_Area
		(
			Sub_Company_Region_ID,
			Sub_Company_Area_Name,
			Staff_ID,
			Sub_Company_Area_Description
		)
		VALUES
		(
			@SubCompanyRegionID,
			@SubCompanyAreaName,
			@Staff_ID,
			@SubCompanyAreaDescription
		)
	END
	ELSE
	BEGIN
		UPDATE Sub_Company_Area
		SET
			-- Sub_Company_Area_ID = ? -- this column value is auto-generated,
			Sub_Company_Region_ID = @SubCompanyRegionID,
			Sub_Company_Area_Name = @SubCompanyAreaName,
			Staff_ID = @Staff_ID,
			Sub_Company_Area_Description = @SubCompanyAreaDescription
		WHERE Sub_Company_Area_ID = @SubCompanyAreaID
		
		UPDATE MeterAnalysis.dbo.Sub_Company_Area
		SET
			-- Sub_Company_Area_ID = ? -- this column value is auto-generated,
			Sub_Company_Region_ID = @SubCompanyRegionID,
			Sub_Company_Area_Name = @SubCompanyAreaName,
			Staff_ID = @Staff_ID,
			Sub_Company_Area_Description = @SubCompanyAreaDescription
		WHERE Sub_Company_Area_ID = @SubCompanyAreaID
	END
	IF @@ERROR <> 0
	BEGIN
	    SET @ErrorMessage = 'MSG_AREA_UPDATE_FAILED'
	    SET @ErrorCode = 2
	    RETURN
	END
	ELSE
	BEGIN
	    SET @ErrorMessage = ''
	    SET @ErrorCode = 0
	    RETURN
	END
END

GO

