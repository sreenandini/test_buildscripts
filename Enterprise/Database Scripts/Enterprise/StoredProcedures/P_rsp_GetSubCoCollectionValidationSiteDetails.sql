USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSubCoCollectionValidationSiteDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSubCoCollectionValidationSiteDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-------------------------------------------------------------------------- 
--
-- Description: Gets the site names for the Sub Company id provided.
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
CREATE PROCEDURE [dbo].[rsp_GetSubCoCollectionValidationSiteDetails]
   
    @SubCompanyID  INT

AS 

SELECT Site_Name 
FROM 
Site 
Inner Join Sub_Company ON Sub_Company.Sub_Company_ID = Site.Sub_Company_ID 
WHERE Sub_Company.Sub_Company_ID = @SubCompanyID
ORDER BY Site_Name




GO

