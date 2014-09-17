/************************************************************
 * Frames GameCapping and GameCappingCardLevelSettings into
 * XML format and exports to exchange.
 * EXEC rsp_GetGameCappingParametersinXML 1002
 * Time: 12/09/2013 11:52:52
 ************************************************************/

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  [name] = 'rsp_GetGameCappingParametersinXML'
              AND [type] = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_GetGameCappingParametersinXML
END
GO

CREATE PROCEDURE rsp_GetGameCappingParametersinXML
	@Site VARCHAR(50)
AS
	DECLARE @GameXML           XML
	
	DECLARE @GamecardLevelXML  XML
	
	SET @GameXML = (
	        SELECT GameCapID,
	               CapReleaseOnPlayerCardIn,
	               ReserveGameForPlayer,
	               ReserveGameForEmployee,
	               MintsToExpire
	        FROM   GameCapping gc
	        WHERE  gc.[Site] = @Site FOR XML PATH('Game'), ROOT('GameCap')
	    )
	
	
	SET @GamecardLevelXML = (
	        SELECT SettingID,
	               CardLevel,
	               MaxNoofMachinestoCap,
	               MintstoCap	               
	        FROM   GameCappingCardLevelSettings gc
	        WHERE  gc.[Site] = @Site FOR XML PATH('CardLevel'), ROOT('GameCardLevel')
	    )
	
	SELECT '<GameCapParameters>' + CONVERT(VARCHAR(MAX),ISNULL(@GameXML,'')) + '' +
	       CONVERT(VARCHAR(MAX), isnull(@GamecardLevelXML,'')) + '</GameCapParameters>'
GO