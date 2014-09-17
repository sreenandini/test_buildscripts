USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertStdOpeningHrsDesc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertStdOpeningHrsDesc]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  PROCEDURE [dbo].[usp_InsertStdOpeningHrsDesc] 
(
	@SOHr_Desc VARCHAR(50),	
	@SOHr_ID INT = 0 OUTPUT
)
AS

BEGIN

	IF NOT EXISTS (SELECT 1 FROM Standard_Opening_Hours WHERE Standard_Opening_Hours_Description = @SOHr_Desc)
	BEGIN	

		INSERT INTO 
		Standard_Opening_Hours 
		(	Standard_Opening_Hours_Description )
		VALUES
		(	@SOHr_Desc	)	
		
		SET @SOHr_ID = SCOPE_IDENTITY()	

	END	
END


GO

