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
       WHERE  [name] = 'rsp_GetCardLevelForEmployee'
              AND TYPE = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_GetCardLevelForEmployee
END

GO
--Exec rsp_GetCardLevelSettings 1
CREATE PROCEDURE rsp_GetCardLevelForEmployee
AS
	
	SELECT DISTINCT CardLevel 
	       	FROM   GameCappingCardLevelSettings 	
	order by CardLevel
GO







