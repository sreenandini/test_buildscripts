USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ecUpdateSubCompanyRegion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ecUpdateSubCompanyRegion]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_ecUpdateSubCompanyRegion
	@SubCompanyRegionID INT = 0,
	@SubCompanyRegionName VARCHAR(50),
	@SubCompanyRegionDescription VARCHAR(50),
	@Staff_ID INT = NULL,
	@SubCompanyID INT = 0,
	@CompanyID INT = 0,
	@ErrorCode INT = 0 OUTPUT,
	@ErrorMessage VARCHAR(100) = '' OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	IF EXISTS (
	       SELECT 1
	       FROM   Sub_Company_Region
	       WHERE  Sub_Company_Region_Name = @SubCompanyRegionName
	              AND Sub_Company_ID = @SubCompanyID
	              AND (
	                      @SubCompanyRegionID = 0
	                      OR Sub_Company_Region_ID <> @SubCompanyRegionID
	                  )
	   )
	BEGIN
	    SET @ErrorMessage = 'MSG_REGIONNAME_EXISTS'
	    SET @ErrorCode = 1
	    RETURN
	END
	
	IF @SubCompanyRegionID = 0
	BEGIN
	    INSERT INTO Sub_Company_Region
	      (
	        Sub_Company_ID,
	        Sub_Company_Region_Name,
	        Staff_ID,
	        Company_ID,
	        Sub_Company_Region_Description
	      )
	    VALUES
	      (
	        @SubCompanyID,
	        @SubCompanyRegionName,
	        @Staff_ID,
	        @CompanyID,
	        @SubCompanyRegionDescription
	      )
	      
	      INSERT INTO MeterAnalysis.dbo.Sub_Company_Region
	      (
	        Sub_Company_ID,
	        Sub_Company_Region_Name,
	        Staff_ID,
	        Company_ID,
	        Sub_Company_Region_Description
	      )
	     VALUES
	      (
	        @SubCompanyID,
	        @SubCompanyRegionName,
	        @Staff_ID,
	        @CompanyID,
	        @SubCompanyRegionDescription
	      )
	END
	ELSE
	BEGIN
	    UPDATE Sub_Company_Region
	    SET    Sub_Company_ID = @SubCompanyID,
	           Sub_Company_Region_Name = @SubCompanyRegionName,
	           Staff_ID = @Staff_ID,
	           Company_ID = @CompanyID,
	           Sub_Company_Region_Description = @SubCompanyRegionDescription
	    WHERE  Sub_Company_Region_ID = @SubCompanyRegionID
	    
	     UPDATE MeterAnalysis.dbo.Sub_Company_Region
	    SET    Sub_Company_ID = @SubCompanyID,
	           Sub_Company_Region_Name = @SubCompanyRegionName,
	           Staff_ID = @Staff_ID,
	           Company_ID = @CompanyID,
	           Sub_Company_Region_Description = @SubCompanyRegionDescription
	    WHERE  Sub_Company_Region_ID = @SubCompanyRegionID
	END
	IF @@ERROR <> 0
	BEGIN
	    SET @ErrorMessage = 'MSG_REGION_UPDATE_FAILED'
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

