USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetPayTable]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetPayTable]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
   
/*    
 * this stored procedure is to fetch the pay table details    
 *    
 * Change History:   
 *     
 * Vineetha  23-06-2010  created     
*/    
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
CREATE PROCEDURE  [dbo].rsp_GetPayTable    
(@GameID INT =0,    
 @ManufacturerID INT =0,    
@GameCategoryID INT = 0,
@Machine_Stock_No VARCHAR(50) = NULL
)      
 AS       
BEGIN      
  
 SELECT DISTINCT Machine_Stock_no,       
 CONVERT(VARCHAR, CONVERT (DECIMAL(10, 2), MGMD_Denom_Value/100.00), 1) Denom,
 Paytable_ID,       
 Payout,      
 PT_Description,      
 MaxBet,    
TheoreticalPayout      
 FROM MGMD_Installation MGI  
 INNER JOIN Installation Ins ON Ins.Installation_ID = MGI.MGMD_Installation_ID   AND (MGI.MGMD_Game_ID = @GameID OR @GameID = 0) 
 INNER JOIN Installation_Game_Info IGI ON MGI.MGMD_Installation_ID = IGI.Installation_No AND (IGI.IGI_Game_ID = @GameID OR @GameID = 0)  
 INNER JOIN Game_Library tGL ON (IGI.IGI_Game_ID = @GameID OR @GameID = 0) AND IGI.IGI_Game_ID = tGl.MG_Game_ID  
 INNER JOIN Game_Title tGT ON tGT.Game_Title_ID = tGL.MG_Group_ID  
 INNER JOIN Paytable tPT ON tPT.Paytable_ID = MGI.MGMD_Paytable_ID   
 INNER JOIN Machine Mac ON Mac.Machine_ID = Ins.Machine_ID  
 WHERE (tGT.Manufacturer_ID = @ManufacturerID OR @ManufacturerID = 0)    
  AND (tGT.Game_Category_ID = @GameCategoryID OR @GameCategoryID = 0)    
  AND (    
          ISNULL(tPT.Payout, 0) <> 0              
          AND ISNULL(tPT.TheoreticalPayout, 0) <> 0    
      )
  AND (@Machine_Stock_No IS NULL OR Mac.Machine_Stock_No = @Machine_Stock_No)
   ORDER BY tPT.Paytable_ID  
END
GO

