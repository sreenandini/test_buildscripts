USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetInstallationGameInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetInstallationGameInfo]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[rsp_GetInstallationGameInfo](@Site_Id INT)    
AS    

SELECT	IGI_ID,Installation_No,Game_Position,Max_Bet,
		Prog_Group,Prog_Level,Game_Name,Paytables,
		IsAvailable,Game_Verification,Game_Current_Status,Game_AAMS_Status,
		Game_Floor_Controller_Status,Game_Entity_Command,Game_Comments,Installation_cRC,
		Game_Part_Number, Game_Enable_AAMS_Status
FROM Installation_Game_Info   
INNER JOIN Site ON Site.Site_ID = Installation_Game_Info.Site_ID
WHERE Site.Site_Code = @Site_Id  
FOR XML PATH('InstallationGameInfo'), ROOT('Game')  ,TYPE, ELEMENTS XSINIL 


GO

