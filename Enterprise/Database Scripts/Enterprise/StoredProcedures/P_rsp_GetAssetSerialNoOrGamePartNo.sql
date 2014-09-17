USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetAssetSerialNoOrGamePartNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetAssetSerialNoOrGamePartNo]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  

CREATE PROCEDURE [dbo].[rsp_GetAssetSerialNoOrGamePartNo]
(
@AAMSCode as VARCHAR(64),
@EntityType as INT ,
@Result VARCHAR(64) OUTPUT
)

AS

BEGIN

IF @EntityType = 4

	SELECT @Result = GL.Game_Part_Number
	 FROM dbo.Game_Library GL
	INNER JOIN dbo.BMC_AAMS_Details BAD ON GL.MG_Game_ID = BAD.BAD_Reference_ID
	INNER JOIN dbo.AAMS_Entities AE ON  BAD.BAD_AAMS_Entity_Type = 4 
	where BAD.BAD_AAMS_Code=@AAMSCode
 

Else IF @EntityType = 3

    SELECT @Result = BAD_Asset_Serial_No  
    FROM dbo.BMC_AAMS_Details BAD
    where BAD.BAD_AAMS_Code=@AAMSCode and BAD.BAD_AAMS_Entity_Type = 3


END



GO

