USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnGetDepotName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnGetDepotName]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[fnGetDepotName](
	@Depot_ID	INT
)
RETURNS VARCHAR(100)
AS

BEGIN

DECLARE @Name	VARCHAR(100)

SELECT @Name = ISNULL(Depot_Name,'') FROM dbo.Depot WHERE Depot_ID = @Depot_ID

RETURN ISNULL(@Name, '')

END


GO

