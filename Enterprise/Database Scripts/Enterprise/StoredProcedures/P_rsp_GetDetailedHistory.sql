
USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetDetailedHistory]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetDetailedHistory]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


  
--------------------------------------------------------------------------     
---    
--- Description: returns details of all machine history entries for use in site viewer  
---    
--- Inputs:      see inputs    
---    
--- Outputs:     (0)   - no error ..     
---              OTHER - SQL error     
---     
--- =======================================================================    
---     
--- Revision History    
---     
--- C.Taylor   23 Jun 2009  added a new field, TermsInUse flag  
---                             defaulted to detailed return  
--- Madhu  24 July 2009 Remodelled the SP to use terms processor  
--- Naveen  29 July 2009 Changed To Left Outer Join From Inner Join  
---------------------------------------------------------------------------    
CREATE PROCEDURE rsp_GetDetailedHistory(@Bar_Position_ID INT, @IsDetailed BIT, @SiteID INT = NULL)
AS
	-- set by defaul      
	SET @IsDetailed = 1       
	
	DECLARE @TermsInUse INT      
	
	SELECT @TermsInUse = COUNT(1)
	FROM   Collection_Terms WITH(NOLOCK)
	
	CREATE TABLE #tmpCollectionData
	(
		[Installation_ID]						  [int] NOT NULL,
		[Collection_ID]                           [int] NOT NULL,
		[Collection_Days]                         [int] NOT NULL,
		[Cash_Take]                               [float] NULL,
		[Collection_Gross]                        [real] NULL,
		[Collection_Company_Share]                [real] NULL,
		[Collection_Supplier_Share]               [real] NULL,
		[Collection_Location_Share]               [real] NULL,
		[Collection_Other_Share]                  [real] NULL
		CONSTRAINT [PK_tmpCollectionData] PRIMARY KEY CLUSTERED([Collection_ID] ASC)
	)
	INSERT INTO #tmpCollectionData
	EXEC P_rsp_GetCollectionData_ForAssetHistory @SiteID,
	   @Bar_Position_ID
	
	IF @TermsInUse = 0
	BEGIN
	    SELECT TOP 500 I.Installation_ID,
	           MAX(I.Installation_Jackpot_Value) AS Installation_Jackpot_Value,
	           COUNT(CC.Collection_ID) AS Collection_ID,
	           SUM(CC.Collection_Gross) AS Collection_Gross,
	           SUM(CC.Collection_Days) AS Collection_Days,
	           SUM(CC.Collection_Company_Share) AS Collection_Company_Share,
	           SUM(CC.Collection_Supplier_Share) AS Collection_Supplier_Share,
	           SUM(CC.Collection_Location_Share) AS Collection_Location_Share,
	           SUM(CC.Collection_Other_Share) AS Collection_Other_Share,
	           Collection_CashTake = SUM(CC.cash_take),
	           AvgDailyWin = CASE 
	                              WHEN SUM(CC.Collection_Days) = 0 THEN 0
	                              ELSE SUM(CC.cash_take) / SUM(CC.Collection_Days)
	                         END,
	           MAX(MC.Machine_Name) AS Machine_Name,
(
	           CASE 
	           WHEN  MAX(MC.Machine_Name)= 'MULTI GAME' THEN 
	           ISNULL(MGMP.MUltiGameName,'MULTI GAME')
	           ELSE  MAX(MC.Machine_Name)
	           END
	            )
	           AS GameName,
	           MAX(MC.Machine_BACTA_Code) AS Machine_BACTA_Code,
	           MAX(MT.Machine_Type_Code) AS Machine_Type_Code,
	           MAX(MC.Machine_Class_Model_Code) AS Machine_Class_Model_Code,
	           MAX(M.Machine_Stock_No) AS Machine_Stock_No,
	           MAX(M.Machine_Manufacturers_Serial_No) AS 
	           Machine_Manufacturers_Serial_No,
	           MAX(M.Machine_Alternative_Serial_Numbers) AS 
	           Machine_Alternative_Serial_Numbers,
	           MAX(I.Installation_Price_Per_Play) AS Installation_Price_Per_Play,
	           MAX(I.Installation_Start_Date) AS Installation_Start_Date,
	           MAX(I.Installation_End_Date) AS Installation_End_Date,
	           MAX(D.Depot_Name) AS Depot_Name,
	           MAX(O.Operator_Name) AS Operator_Name,
	           MAX(MC.Machine_Class_ID) AS Machine_Class_ID
	    FROM   dbo.Installation I WITH (NOLOCK)
	           INNER JOIN Bar_Position B WITH (NOLOCK)
	                ON  B.Bar_Position_ID = I.Bar_Position_ID
	           INNER JOIN [SITE] S
	                ON  S.Site_ID = B.Site_ID	           
	           LEFT JOIN #tmpCollectionData cc
	                ON  CC.Installation_ID = I.Installation_ID
	           INNER JOIN dbo.Machine M WITH (NOLOCK)
	                ON  I.Machine_ID = M.Machine_ID
	           INNER JOIN dbo.Machine_Class MC WITH (NOLOCK)
	                ON  M.Machine_Class_ID = MC.Machine_Class_ID
                  LEFT JOIN MultiGameMapping MGMP 
	                ON  MGMP.Machineid =  M.Machine_ID  
	           INNER JOIN dbo.Machine_Type MT WITH (NOLOCK)
	                ON  MC.Machine_Type_ID = MT.Machine_Type_ID
	           LEFT JOIN dbo.Depot D WITH (NOLOCK)
	                ON  M.Depot_ID = D.Depot_ID
	           LEFT JOIN dbo.Operator O WITH (NOLOCK)
	                ON  D.Supplier_ID = O.Operator_ID
	    WHERE -- I.Installation_End_Date IS NULL
				I.Bar_Position_ID= @Bar_Position_ID
	    GROUP BY
	           CONVERT(DATETIME, I.Installation_Start_Date, 101),
	           I.Installation_ID,MGMP.MUltiGameName
	    ORDER BY
	           CONVERT(DATETIME, I.Installation_Start_Date, 101) DESC,
	           I.Installation_ID DESC
	END
	ELSE
	BEGIN
	    SELECT I.Installation_ID,
	           MAX(I.Installation_Jackpot_Value) AS Installation_Jackpot_Value,
	           COUNT(CC.Collection_ID) AS Collection_ID,
	           SUM(CC.Collection_Gross) AS Collection_Gross,
	           SUM(CC.Collection_Days) AS Collection_Days,
	           SUM(CC.Collection_Company_Share) AS Collection_Company_Share,
	           SUM(CC.Collection_Supplier_Share) AS Collection_Supplier_Share,
	           SUM(CC.Collection_Location_Share) AS Collection_Location_Share,
	           SUM(CC.Collection_Other_Share) AS Collection_Other_Share,
	           Collection_CashTake = (
	               SUM(CC.Collection_Company_Share) + SUM(CC.Collection_Supplier_Share) 
	               + SUM(CC.Collection_Location_Share) + SUM(CC.Collection_Other_Share)
	           ),
	           AvgDailyWin = CASE 
	                              WHEN SUM(CC.Collection_Days) = 0 THEN 0
	                              ELSE (
	                                       SUM(CC.Collection_Company_Share) +
	                                       SUM(CC.Collection_Supplier_Share) +
	                                       SUM(CC.Collection_Location_Share) +
	                                       SUM(CC.Collection_Other_Share)
	                                   ) / SUM(CC.Collection_Days)
	                         END,
	           MAX(MC.Machine_Name) AS Machine_Name,
 (
	           CASE 
	           WHEN  MAX(MC.Machine_Name)= 'MULTI GAME' THEN 
	           ISNULL(MGMP.MUltiGameName,'MULTI GAME')
	           ELSE  MAX(MC.Machine_Name)
	           END
	            )
	           AS GameName,
	           MAX(MC.Machine_BACTA_Code) AS Machine_BACTA_Code,
	           MAX(MT.Machine_Type_Code) AS Machine_Type_Code,
	           MAX(MC.Machine_Class_Model_Code) AS Machine_Class_Model_Code,
	           MAX(M.Machine_Stock_No) AS Machine_Stock_No,
	           MAX(M.Machine_Manufacturers_Serial_No) AS 
	           Machine_Manufacturers_Serial_No,
	           MAX(M.Machine_Alternative_Serial_Numbers) AS 
	           Machine_Alternative_Serial_Numbers,
	           MAX(I.Installation_Price_Per_Play) AS Installation_Price_Per_Play,
	           MAX(I.Installation_Start_Date) AS Installation_Start_Date,
	           MAX(I.Installation_End_Date) AS Installation_End_Date,
	           MAX(D.Depot_Name) AS Depot_Name,
	           MAX(O.Operator_Name) AS Operator_Name,
	           MAX(MC.Machine_Class_ID) AS Machine_Class_ID
	    FROM   dbo.Installation I WITH (NOLOCK)
	           INNER JOIN Bar_Position B WITH (NOLOCK)
	                ON  B.Bar_Position_ID = I.Bar_Position_ID
	           INNER JOIN [SITE] S
	                ON  S.Site_ID = B.Site_ID
	           LEFT JOIN #tmpCollectionData cc WITH (NOLOCK)
	                ON  cc.Installation_ID = i.Installation_ID	           
	           INNER JOIN dbo.Machine M WITH (NOLOCK)
	                ON  I.Machine_ID = M.Machine_ID
	           INNER JOIN dbo.Machine_Class MC WITH (NOLOCK)
	                ON  M.Machine_Class_ID = MC.Machine_Class_ID
                      LEFT JOIN MultiGameMapping MGMP 
	                ON  MGMP.Machineid =  M.Machine_ID   
	           INNER JOIN dbo.Machine_Type MT WITH (NOLOCK)
	                ON  MC.Machine_Type_ID = MT.Machine_Type_ID
	           LEFT JOIN dbo.Depot D WITH (NOLOCK)
	                ON  M.Depot_ID = D.Depot_ID
	           LEFT JOIN dbo.Operator O WITH (NOLOCK)
	                ON  D.Supplier_ID = O.Operator_ID
	    WHERE -- I.Installation_End_Date IS NULL
				I.Bar_Position_ID= @Bar_Position_ID
	    GROUP BY
	           CONVERT(DATETIME, I.Installation_Start_Date, 101),
	           I.Installation_ID,MGMP.MUltiGameName
	    ORDER BY
	           CONVERT(DATETIME, I.Installation_Start_Date, 101) DESC,
	           I.Installation_ID DESC
	END
GO
