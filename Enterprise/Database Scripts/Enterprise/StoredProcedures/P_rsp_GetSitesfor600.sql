USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSitesfor600]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSitesfor600]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-------------------------------------------------------------------------- 
--
-- Description: get the list of sites Export Location specific data for AAMS
-- Inputs:      See inputs 
--
-- Outputs:         
--                  
-- =======================================================================
-- 
-- Revision History
--
-- Sudarsan S		03/12/2009		Created
------------------------------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[rsp_GetSitesfor600]
AS
BEGIN

	DECLARE @Start	INT
	DECLARE @Count	INT	
	DECLARE @SiteID	INT

	SET @Start = 1

	CREATE TABLE #TempSite
		(
			Sno	INT IDENTITY(1,1),
			Site_ID	INT
		)

	INSERT INTO #TempSite
	SELECT Site_ID FROM dbo.Site WHERE ISNULL(Site_Closed_Date, '') = ''

	SELECT @Count = COUNT(*) FROM #TempSite

	WHILE @Start <= @Count
	BEGIN
		SELECT @SiteID = Site_ID FROM #TempSite WHERE Sno = @Start
		EXEC dbo.rsp_ExportLocation600 'Parlour', @SiteID, 'YTD'
		EXEC dbo.rsp_ExportLocation600 'Parlour', @SiteID, 'MTD'
		EXEC dbo.rsp_ExportLocation600 'Parlour', @SiteID, 'PTD'

		SET @Start = @Start + 1

	END

END


GO

