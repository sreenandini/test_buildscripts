/************************************************************
 * Reterives GameCapping Settings based on SiteCode
 * Time: 04/09/2013 16:09:42
 ************************************************************/
USE [Enterprise]
IF EXISTS (
       SELECT *
       FROM   sys.objects o
       WHERE  o.[name] = 'rsp_GetGameCappingParameters'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_GetGameCappingParameters
END

GO

CREATE PROCEDURE rsp_GetGameCappingParameters(@Site INT)
AS
	DECLARE @SiteCode VARCHAR(20)
	
	SELECT @SiteCode = site_code
	FROM   SITE
	WHERE  site_id = @Site
	
	SELECT G.GameCapID,
	       G.CapReleaseOnPlayerCardIn,
	       G.ReserveGameForPlayer,
	       G.ReserveGameForEmployee,
	       G.MintsToExpire,
	       G.SITE
	FROM   GameCapping G
	WHERE  [Site] = @SiteCode
GO





