USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetPaytableDetailsForGame]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetPaytableDetailsForGame]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-----------------------------------------------------------------------------------------------------        
--                
-- Description: Get Paytable details for Game
-- Inputs:                  
--                
-- Outputs:     Result Set - Paytable Details
--                                  
-- =======================================================================                
--                 
-- Revision History                
--                
-- Yoganandh P	10/01/2011	Created
-- Yoganandh P	18/01/2011	Modified Paytable and GameLibrary Join Conditions and used Site ID in Where Condition
------------------------------------------------------------------------------------------------------        
CREATE PROCEDURE rsp_GetPaytableDetailsForGame(@MachineId AS INT)
AS
BEGIN
	DECLARE @SiteID INT
	DECLARE @Installation_ID INT
	
	SELECT @SiteID = tB.Site_ID,
		   @Installation_ID = tI.Installation_ID
	FROM   Installation tI
	       INNER JOIN Bar_Position tB
	            ON  tI.Bar_Position_ID = tB.Bar_Position_ID
	WHERE  Machine_ID = @MachineId
	       AND tI.Installation_End_Date IS NULL
	
	SELECT DISTINCT CASE 
	                     WHEN tGT.Game_Title IS NULL THEN '[TBD]'
	                     WHEN tGT.Game_Title = '' THEN '[TBD]'
	                     ELSE tGT.Game_Title
	                END AS AliasGameName,
	       CASE 
	            WHEN Manufacturer_Name IS NULL THEN 'Not Available'
	            WHEN Manufacturer_Name = '' THEN 'Not Available'
	            ELSE Manufacturer_Name
	       END AS Manufacturer_Name,
	       tI.Installation_ID AS Installation_No,
	       tI.Machine_ID,
	       tGL.MG_Game_Name AS Game_Name,
	       tP.Paytable_ID AS PaytableID,
	       tP.PT_Description AS PaytableDescription,
	       tP.Payout AS Payout,
	       tP.MaxBet AS MaxBet,
	       tP.TheoreticalPayout AS TheoreticalPayout
	FROM   Installation_Game_Info IGI
	       JOIN Installation tI
	            ON  tI.Installation_ID = IGI.Installation_No
	       JOIN Game_Library tGL
	            ON  tGL.MG_Game_ID = IGI.IGI_Game_ID
	       JOIN Game_Title tGT
	            ON  tGT.Game_Title_ID = tGL.MG_Group_ID
	       JOIN Paytable tP
	            ON  tP.Game_ID = tGL.MG_Game_ID
	       LEFT JOIN Manufacturer M
	            ON  M.Manufacturer_ID = tGT.Manufacturer_ID
	WHERE  tI.Machine_ID = @MachineId
		   AND ti.Installation_ID = @Installation_ID
	       AND ISNULL(tP.PT_Description, '') <> ''
	ORDER BY
	       AliasGameName,
	       Manufacturer_Name
END


GO

