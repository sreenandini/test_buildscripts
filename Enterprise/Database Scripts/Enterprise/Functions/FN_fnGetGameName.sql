USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnGetGameName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnGetGameName]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fnGetGameName]
(@Machine_Name VARCHAR(50), @Installation_No INT) 
  RETURNS VARCHAR(50)
AS  
BEGIN  

	DECLARE @GameCount AS INT
	DECLARE @Return AS VARCHAR(50)
 
	SELECT 
		@GameCount = COUNT(*) 
	FROM 
		Installation I
		INNER JOIN dbo.Installation_game_info IGI ON IGI.Installation_No = I.Installation_ID
		INNER JOIN dbo.Game_Library GL ON IGI.Game_Name = GL.MG_Game_Name AND GL.Game_Part_Number = IGI.Game_Part_Number
		INNER JOIN dbo.Game_Title GT ON GT.Game_Title_ID = GL.MG_Group_ID 
		WHERE 
		I.Installation_ID = @Installation_No

	IF @GameCount > 1
		BEGIN
			SET @Return = 'Multi Game'
		END
	ELSE
		BEGIN
			SELECT 
				@Return = GT.Game_Title
			FROM 
				Installation I
				INNER JOIN dbo.Installation_game_info IGI ON IGI.Installation_No = I.Installation_ID
				INNER JOIN dbo.Game_Library GL ON IGI.Game_Name = GL.MG_Game_Name AND GL.Game_Part_Number = IGI.Game_Part_Number
				INNER JOIN dbo.Game_Title GT ON GT.Game_Title_ID = GL.MG_Group_ID
			WHERE 
				I.Installation_ID = @Installation_No
		END
	RETURN isnull(@Return,'Unassigned GameTitle')
END


GO

