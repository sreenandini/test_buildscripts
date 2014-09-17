USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ExportEmpCardDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ExportEmpCardDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create Procedure [dbo].[usp_ExportEmpCardDetails] (@UserID INT, @SiteID INT)    
AS    
BEGIN    
	DECLARE @IsEmployeeCardTrackingEnabled BIT

	SELECT @IsEmployeeCardTrackingEnabled = CASE WHEN Setting_Value = 'TRUE' THEN  1  ELSE  0 END
	FROM Setting WHERE Setting_Name = 'IsEmployeeCardTrackingEnabled'

	IF @IsEmployeeCardTrackingEnabled = 1	
	BEGIN
		DECLARE @Site_Code VARCHAR(50)
		SELECT @Site_Code = Site_Code FROM [Site] WHERE Site_ID = @SiteID
		EXEC usp_InsertExportHistory @UserID,'Userdetails',@Site_Code
	END	
END

GO
