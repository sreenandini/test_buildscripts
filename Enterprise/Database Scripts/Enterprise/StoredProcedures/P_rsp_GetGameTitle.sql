USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetGameTitle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetGameTitle]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
* Revision History  
*   
* <Name of the owner>         DateCreated       Type(Created/Modified)  Description  
  Kalaiyarasan.P              18-Sep-2012         Created               This SP is used to get Game Title details
  --Exec  rsp_GetGameTitle                                                                   
*/  
  
CREATE PROCEDURE rsp_GetGameTitle
@IsMultiGame BIT
AS
BEGIN
	SET NOCOUNT ON
	SELECT Game_Title_ID
		  ,Game_Category_ID
		  ,Game_Title
		  ,Manufacturer_ID
     FROM Game_Title WITH(NOLOCK) WHERE ISNULL(IsMultiGame,0) =  @IsMultiGame 	  
     ORDER BY Game_Title
END

GO

