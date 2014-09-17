USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ecUpdateSubCompanyDistrict]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ecUpdateSubCompanyDistrict]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_ecUpdateSubCompanyDistrict
	@SubCompanyDistrictID INT = 0,
	@SubCompanyDistrictName VARCHAR(50),
	@SubCompanyDistrictDescription VARCHAR(50) = '',
	@Staff_ID INT = NULL,
	@SubCompanyAreaID INT,
	@ErrorCode INT = 0 OUTPUT,
	@ErrorMessage VARCHAR(100) = '' OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	IF EXISTS (
	       SELECT 1
	       FROM   Sub_Company_District sca
	       WHERE  sca.Sub_Company_District_Name = @SubCompanyDistrictName
	              AND sca.Sub_Company_Area_ID = @SubCompanyAreaID
	              AND (
	                      @SubCompanyDistrictID = 0
	                      OR sca.Sub_Company_District_ID <> @SubCompanyDistrictID
	                  )
	   )
	BEGIN
	    SET @ErrorMessage = 'MSG_DISTRICTNAME_EXISTS'
	    SET @ErrorCode = 1
	    RETURN
	END
	
	IF @SubCompanyDistrictID = 0
	BEGIN
		INSERT INTO Sub_Company_District
		(
			Sub_Company_Area_ID,
			Sub_Company_District_Name,
			Staff_ID,
			Sub_Company_District_Description
		)
		VALUES
		(
			@SubCompanyAreaID,
			@SubCompanyDistrictName,
			@Staff_ID,
			@SubCompanyDistrictDescription
		)
		
		INSERT INTO MeterAnalysis.dbo.Sub_Company_District
		(
			Sub_Company_Area_ID,
			Sub_Company_District_Name,
			Staff_ID,
			Sub_Company_District_Description
		)
		VALUES
		(
			@SubCompanyAreaID,
			@SubCompanyDistrictName,
			@Staff_ID,
			@SubCompanyDistrictDescription
		)
	END
	ELSE
	BEGIN
		UPDATE Sub_Company_District
		SET
			-- Sub_Company_District_ID = ? -- this column value is auto-generated,
			Sub_Company_Area_ID = @SubCompanyAreaID,
			Sub_Company_District_Name = @SubCompanyDistrictName,
			Staff_ID = @Staff_ID,
			Sub_Company_District_Description = @SubCompanyDistrictDescription
		WHERE Sub_Company_District_ID = @SubCompanyDistrictID
		
		UPDATE MeterAnalysis.dbo.Sub_Company_District
		SET
			-- Sub_Company_District_ID = ? -- this column value is auto-generated,
			Sub_Company_Area_ID = @SubCompanyAreaID,
			Sub_Company_District_Name = @SubCompanyDistrictName,
			Staff_ID = @Staff_ID,
			Sub_Company_District_Description = @SubCompanyDistrictDescription
		WHERE Sub_Company_District_ID = @SubCompanyDistrictID
	END
	IF @@ERROR <> 0
	BEGIN
	    SET @ErrorMessage = 'MSG_DISTRICT_UPDATE_FAILED'
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

