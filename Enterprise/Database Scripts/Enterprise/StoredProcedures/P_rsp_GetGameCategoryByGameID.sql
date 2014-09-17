USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetGameCategoryByGameID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetGameCategoryByGameID]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.rsp_GetGameCategoryByGameID
      @Game_Category_ID int = 0
AS
/*****************************************************************************************************
DESCRIPTION : Gets  the Game Category  
CREATED DATE: 07 Jun 2012
CREATED BY: Lekha
MODULE            : Game Category      
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR                              DESCRIPTON                                                        MODIFIED DATE
------------------------------------------------------------------------------------------------------
                                                              
*****************************************************************************************************/

BEGIN
      SET NOCOUNT ON 
      SELECT Game_Category_Name FROM Game_Category WITH (NOLOCK) WHERE Game_Category_ID = @Game_Category_ID
END

GO

