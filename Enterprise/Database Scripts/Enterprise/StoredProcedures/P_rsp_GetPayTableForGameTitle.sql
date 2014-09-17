USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetPayTableForGameTitle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetPayTableForGameTitle]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
   
/*    
 * this stored procedure is to fetch the pay table details for the game title    
 *    
 * Change History:   
 *     
 * Vineetha  23-06-2010  created     
*/    
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE  [dbo].rsp_GetPayTableForGameTitle         
(@GameID INT =0,      
@ManufacturerID INT =0,      
@GameCategoryID INT = 0                
)              
 AS               
BEGIN              
                
 SELECT  DISTINCT
 Machine_Stock_No,
 CONVERT(VARCHAR, CONVERT (DECIMAL(10, 2), MGMD_Denom_Value/100.00), 1) 
 Denom,
 Paytable_ID,             
 Payout,            
 PT_Description,            
 MaxBet,      
TheoreticalPayout,      
tGL.MG_Game_Name as Game_Title,      
tGL.MG_Game_ID      
FROM MGMD_Installation MGI        
 INNER JOIN Installation Ins ON Ins.Installation_ID = MGI.MGMD_Installation_ID
 INNER JOIN Installation_Game_Info IGI ON MGI.MGMD_Installation_ID = IGI.Installation_No
 INNER JOIN Game_Library tGL ON MGI.MGMD_Game_ID = tGl.MG_Game_ID
 INNER JOIN Game_Title tGT ON tGT.Game_Title_ID = tGL.MG_Group_ID    AND (tGT.Game_Title_ID = @GameID OR @GameID = 0) 
 INNER JOIN Paytable tPT ON tPT.Paytable_ID = MGI.MGMD_Paytable_ID
 INNER JOIN Machine Mac ON Mac.Machine_ID = Ins.Machine_ID
WHERE (tGT.Manufacturer_ID = @ManufacturerID OR @ManufacturerID = 0)      
                                AND (tGT.Game_Category_ID = @GameCategoryID OR @GameCategoryID = 0)      
   ORDER BY tPT.Paytable_ID              
              
END

GO