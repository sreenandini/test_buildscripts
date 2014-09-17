USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportCRCToXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportCRCToXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* Purpose: To export CRC details to exchange as an XML 
 *	Change History:
 * exec [rsp_ExportCRCToXML] 8
 *	Vineetha Mathew		15-10-2009		created
*/

CREATE PROCEDURE [dbo].[rsp_ExportCRCToXML]
	@GameID INT=0
AS

BEGIN

SET DATEFORMAT dmy
DECLARE @CRC XML

		SET @CRC=(SELECT DISTINCT
			GL.MG_Game_Name as GAMENAME
			,GC.CRC as CRC
		  FROM dbo.Game_Library GL
	INNER JOIN dbo.Game_CRCDetails GC ON GL.MG_Game_ID=GC.Game_id			
		 WHERE (( @GameID = 0)OR ( @GameID <> 0 AND GC.Game_ID = @GameID ))  
			FOR XML PATH('CRC'), ROOT('CRCDETAILS'))				

	SELECT @CRC
END

GO

