USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetGameTitleDetailsByTitleId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetGameTitleDetailsByTitleId]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE dbo.rsp_GetGameTitleDetailsByTitleId
      @Game_Title INT
AS

/* ================================================================================= 
 * Purpose	:	To get the Game title details specific to Game Title Id
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
      
      DECLARE @IsMultiGame BIT
      DECLARE @IsEnrolled INT
      
      SELECT @IsMultiGame = ISNULL(GT.IsMultiGame,0) FROM Game_Title GT WITH (NOLOCK)        
      WHERE Game_Title_ID = @Game_Title
      
      IF @IsMultiGame = 1
      BEGIN
			IF EXISTS(SELECT 1 FROM Game_Title gt 
			INNER JOIN MultiGameMapping mgm ON gt.Game_Title = mgm.MultiGameName
			INNER JOIN [Machine] m ON M.Machine_ID = mgm.MachineID 
			WHERE m.Machine_Status_Flag = 1 AND GT.Game_Title_ID = @Game_Title)
				SET @IsEnrolled = 1
			
      END
      ELSE
      BEGIN
      		IF EXISTS(SELECT 1 FROM Game_Title gt 
			INNER JOIN Game_Library gl ON gl.MG_Group_ID = gt.Game_Title_ID
			INNER JOIN Installation_Game_Info igi ON igi.IGI_Game_ID = GL.MG_Game_ID
			INNER JOIN Installation i ON i.Installation_ID = igi.Installation_No			
			WHERE i.Installation_End_Date IS NULL AND GT.Game_Title_ID = @Game_Title)
				SET @IsEnrolled = 1
      END
		 
	  SELECT *,@IsEnrolled AS EnrolledStatus FROM Game_Title GT WITH (NOLOCK)        
			WHERE Game_Title_ID = @Game_Title 
		
END


GO

