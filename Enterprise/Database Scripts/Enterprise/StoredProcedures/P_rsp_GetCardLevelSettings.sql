/************************************************************
 * Description: Gets the Card Level Settings Details
 * Time: 04/09/2013 11:25:21
 * exec rsp_GetCardLevelSettings 1
 ************************************************************/
USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  [name] = 'rsp_GetCardLevelSettings'
              AND TYPE = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_GetCardLevelSettings
END

GO
--Exec rsp_GetCardLevelSettings 1
CREATE PROCEDURE rsp_GetCardLevelSettings(@Site INT)
AS
	DECLARE @SiteCode VARCHAR(20)
	
	SELECT @SiteCode = SITE_Code
	FROM   SITE
	WHERE  Site_ID = @Site 
	
	SELECT SettingID , GC.CardLevel ,
	       GC.MaxNoofMachinestoCap,
	       GC. MintstoCap
	FROM   GameCappingCardLevelSettings GC
	WHERE  GC.SITE = @SiteCode
	order by GC.CardLevel
GO







