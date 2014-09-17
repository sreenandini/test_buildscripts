USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetBarPositionSiteDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetBarPositionSiteDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-------------------------------------------------------------------------- 
--
-- Description: Gets the required Bar Position and Site Details.
--
-- Inputs:      See inputs
--
-- Outputs:     Bar_Position_ID,Bar_Position_Name,Site_Code,Site_Name & Site_ID 
--
-- =======================================================================
-- 
-- Revision History
-- 
-- Renjish N   24/06/08   Created 
-- 
--------------------------------------------------------------------------- 
CREATE PROCEDURE [dbo].[rsp_GetBarPositionSiteDetails]
   
    @SubCompanyID  INT

AS 

    SELECT Bar_Position.Bar_Position_ID, 
	Bar_Position.Bar_Position_Name, 
	Site.Site_Code, 
	Site.Site_Name, 
	Site.Site_ID 
	FROM Bar_Position INNER JOIN Site 
	ON Bar_Position.Site_ID = Site.Site_ID 
	WHERE Sub_Company_ID = @SubCompanyID 
	ORDER BY Site_Name, Site.Site_ID, Bar_Position_Name, Bar_Position_ID




GO

