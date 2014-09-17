/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 03/12/2014 6:10:01 PM
 ************************************************************/

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_EBS_GetZoneDetails]    Script Date: 03/03/2014 18:29:55 ******/
IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_EBS_GetZoneDetails]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_EBS_GetZoneDetails]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_EBS_GetZoneDetails]    Script Date: 03/03/2014 18:29:55 ******/
/*
SELECT * FROM ZONE
[rsp_EBS_GetZoneDetails] '1212'
SELECT * FROM SITE
*/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- exec [dbo].[rsp_EBS_GetZoneDetails] '1005', 5
CREATE PROCEDURE [dbo].[rsp_EBS_GetZoneDetails]
(@SiteCode VARCHAR(50), @ZoneID INT = NULL)
AS
BEGIN
	DECLARE @Zone XML
	
	-- Check the data in deletion history table
	SELECT @Zone = EBS_RefValue
	FROM   dbo.EBS_ObjectDeletion_History WITH(NOLOCK)
	WHERE  EBS_RefType = 'ZONE'
	       AND EBS_RefID = @ZoneID
	
	IF (@Zone IS NULL)
	BEGIN
	    SELECT Zone_ID AS ZoneID,
	           ISNULL(Zone_Name, '') AS ZoneName,
	           ISNULL(Zone_Name, '') AS ZoneValue,
	           CAST(1 AS BIT) AS IsActive
	    FROM   [Zone] Z WITH(NOLOCK)
	           INNER JOIN [Site] S WITH(NOLOCK)
	                ON  Z.Site_ID = S.Site_ID
	    WHERE  Zone_ID = COALESCE(@ZoneID, Zone_ID)
	           AND S.Site_Code = COALESCE(@SiteCode, S.Site_Code)
	    ORDER BY
	           ZoneName
	END
	ELSE
	BEGIN
	    SELECT Z.C.query('ZoneID/text()').value('.', 'INT') AS ZoneID,
	           Z.C.query('ZoneName/text()') AS ZoneName,
	           Z.C.query('ZoneID/text()') AS ZoneValue,
	           Z.C.query('IsActive/text()').value('.', 'BIT') AS IsActive
	    FROM   @Zone.nodes('/Zones/Zone') Z(C)
	END
END
GO


