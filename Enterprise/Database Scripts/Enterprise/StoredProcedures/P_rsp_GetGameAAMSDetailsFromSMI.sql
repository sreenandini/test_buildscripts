USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetGameAAMSDetailsFromSMI]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetGameAAMSDetailsFromSMI]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.rsp_GetGameAAMSDetailsFromSMI
@SMINumber VARCHAR(64)
AS

SELECT GL.MG_Game_ID, BAD.BAD_AAMS_Code, BAD.BAD_AAMS_Status
 FROM dbo.Game_Library GL
INNER JOIN dbo.BMC_AAMS_Details BAD ON GL.MG_Game_ID = BAD.BAD_Reference_ID
INNER JOIN dbo.AAMS_Entities AE ON  BAD.BAD_AAMS_Entity_Type = 4
WHERE GL.MG_SMI_Number = @SMINumber



GO

