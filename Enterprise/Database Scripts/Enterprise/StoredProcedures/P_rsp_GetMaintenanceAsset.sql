USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetMaintenanceAsset]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetMaintenanceAsset]
GO

USE [Enterprise]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO

/****
Version History
----------------------------------------
Anil		28 Oct 2010		Created
----------------------------------------
***/




CREATE PROCEDURE [dbo].[rsp_GetMaintenanceAsset]
(@SiteID INT=2)
AS
BEGIN

SELECT Distinct tM.Machine_Stock_No AS [Asset No], tBP.Bar_Position_Name as [Type]	
FROM 
SITE tS 
INNER JOIN BAR_POSITION tBP ON tBP.Site_ID = tS.Site_ID
INNER JOIN INSTALLATION tI ON tI.Bar_Position_ID = tBP.Bar_Position_ID 
INNER JOIN MACHINE tM ON tM.Machine_ID = tI.Machine_ID
WHERE tS.Site_ID = @SiteID AND  tI.Installation_End_Date is null

END


GO

