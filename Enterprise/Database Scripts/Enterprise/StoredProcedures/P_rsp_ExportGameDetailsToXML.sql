USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_ExportGameDetailsToXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_ExportGameDetailsToXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* Purpose: To export games themes details to exchange as an XML 
 *	Change History:
 * exec [rsp_ExportGameDetailsToXML] 8
 *	Vineetha Mathew		15-10-2009		created
*/

CREATE PROCEDURE [dbo].[rsp_ExportGameDetailsToXML]
	@GameID INT = 0
AS
BEGIN
	  SELECT  
		GM.MG_Game_ID AS MG_Game_ID  
		,GM.MG_Game_Name AS MG_Game_Name      
		,GM.MG_Game_StartDate AS ReleaseDate        
		,GM.MG_Game_Version AS Version      
		,GM.MG_Game_SerialNo AS SerialNo          
		,M.Manufacturer_Name AS Manufacturer
		,GM.Game_Part_Number As Game_Part_Number          
		,GM.MG_Game_Manufacturer_ID AS Game_Manufacturer_ID
	FROM Game_Library GM
		INNER JOIN Game_Title GT ON GT.Game_Title_ID = GM.MG_Group_ID  
		LEFT JOIN Manufacturer M ON M.Manufacturer_ID = GT.Manufacturer_ID               
	WHERE ((@GameID = 0) OR (@GameID <> 0 AND GM.MG_Game_ID = @GameID ))    
	FOR XML PATH('GAMES'), ELEMENTS XSINIL, ROOT('GAMETHEMES')
END

GO

