USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetPaytableForGame]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetPaytableForGame]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
CREATE PROCEDURE [dbo].[rsp_GetPaytableForGame]                  
(  
@GroupID INT ,
@Installation_No INT
)      
AS           
    
BEGIN      
  
  SELECT DISTINCT  
	PT.PT_Description AS PaytableID,  
	PT.Payout AS Payout,  
	PT.TheoreticalPayout AS TheoreticalPayout,  
	PT.MaxBet AS MaxBet  
 FROM 
	Installation I   
	INNER JOIN installation_game_info IGI ON IGI.Installation_No = I.Installation_ID 
	INNER JOIN Game_Library tGL ON tGL.MG_Game_Name = IGI.Game_Name -- AND tGL.Game_Part_Number = IGI.Game_Part_Number  
	INNER JOIN Paytable PT ON PT.Game_ID=tGL.MG_Game_ID  
 WHERE tGL.MG_Group_ID = @GroupID  
	   AND I.Installation_ID = @Installation_No
   
END     

GO

