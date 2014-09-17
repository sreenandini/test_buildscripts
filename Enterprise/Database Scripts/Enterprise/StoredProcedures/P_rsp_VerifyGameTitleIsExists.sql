USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_VerifyGameTitleIsExists]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_VerifyGameTitleIsExists]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_VerifyGameTitleIsExists]
      @Game_Title VARCHAR(100)
AS

/* ================================================================================= 
 * Purpose	:	To check whether game title exists or not
 * Author	:	Dinesh Rathinavel
 * Created  :	22/11/2012
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 22/11/2012  	Dinesh Rathinavel    Initial Version
 * ================================================================================= 
*/

BEGIN
      SET NOCOUNT ON 
      
      SELECT Game_Title FROM Game_Title WITH (NOLOCK) 
      WHERE 
		Game_Title = @Game_Title
		
END


GO

