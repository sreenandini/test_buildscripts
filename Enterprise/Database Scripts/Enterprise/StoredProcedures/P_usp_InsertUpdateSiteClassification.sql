USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertUpdateSiteClassification]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertUpdateSiteClassification]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[usp_InsertUpdateSiteClassification] 
(
	@SiteClassifID int = 0,
	@SiteClassifName varchar(50),
	@SiteClassifIdOUT int = NULL OUTPUT
)
AS
	SET @SiteClassifIdOUT = @SiteClassifID
BEGIN
	IF NOT EXISTS(SELECT 1 FROM Site_Classification WHERE Site_Classification_ID = @SiteClassifID)
	BEGIN
		INSERT INTO Site_Classification ([Site_Classification_Name]) VALUES (@SiteClassifName)
		SET @SiteClassifIdOUT = SCOPE_IDENTITY()
	END
	
	UPDATE Site_Classification
	SET
		[Site_Classification_Name] = @SiteClassifName
	WHERE 
		Site_Classification_ID = @SiteClassifIdOUT
END

GO

