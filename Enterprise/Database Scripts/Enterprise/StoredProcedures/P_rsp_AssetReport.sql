
USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_AssetReport]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_AssetReport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
/*
declare @p7 int
set @p7=NULL
exec sp_executesql N'EXEC @RETURN_VALUE = [dbo].[rsp_AssetReport] @StartDate = @p0, @EndDate = @p1, @SiteID = @p2, 
@Category = @p3',N'@p0 varchar(8000),@p1 varchar(8000),@p2 int,@p3 int,
@RETURN_VALUE int output',@p0='21 Jan 2014',@p1='25 Jan 2014',@p2=1,@p3=0,@RETURN_VALUE=@p7 output
select @p7
*/
--------------------------------------------------------------------------     
---    
--- Description: To get the data for Asset Report    
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
--- Anuradha J  25/06/09   Created    
--- Anuradha J  26/06/09   changed the condition to check for unique records and for datapak.     
--- Anuradha J  30/06/09   Removed Datapak Condition and included count distinct for read date    
--- Anuradha J  27/01/2010 Fixed conversion problem for numeric
--- Anil        11/10/2010 Modified to enter last record from installtion table.
---------------------------------------------------------------------------     
          
CREATE PROCEDURE rsp_AssetReport(
    @StartDate  VARCHAR(38),
    @EndDate    VARCHAR(38),
    @SiteID     INT,
    @Category   INT = 0
)
AS
	SET NOCOUNT ON
	
	DECLARE @dtStartDate DATETIME
	DECLARE @dtEndDate DATETIME
	
	SET @dtStartDate = CAST(@StartDate AS DATETIME)
	SET @dtEndDate=CAST(@EndDate AS DATETIME)
	
	IF @Category = 0
	    SET @Category = NULL 
	
	
	-- Get the machine.          
	
	    SELECT Machine_Stock_No,
			   MAX(MC.Machine_ID) AS MACHINE,
			   MAX(INS.Installation_ID) AS Installation_ID,
	           SUM(VW.RDCCashIn) AS RDCCashIn,
	           SUM(VW.RDCCashOut) RDCCashOut,
	           SUM(VW.RDCCash) AS RDCCash,
	           --Daily Win=Formula is [(Cashin-CashOut)/Count(Read_Date)]
	           --   cast((cast(Sum(VW.RDCCashIn) as decimal(38,2)) - cast(Sum(VW.RDCCashOut)as decimal(38,2)))/Count(distinct Vw.Read_date) as decimal(38,2))  as [Daily Win],          
	           CASE 
	                WHEN COUNT(DISTINCT VW.Readdate) > 0 THEN CAST(
	                         (SUM(VW.RDCCashIn) - SUM(VW.RDCCashOut)) / COUNT(DISTINCT VW.Readdate) 
	                         AS DECIMAL(38, 2)
	                     )
	                ELSE CAST(0.00 AS DECIMAL(18, 2))
	           END AS DailyWin,
	           --Act% Formula is [(Sum(CashOut)/Sum(Cashin))*100]          
	           CASE 
	                WHEN (ISNULL(SUM(VW.RDCCashIn), 0) > 0) THEN CAST(
	                         (SUM(VW.RDCCashOut) / SUM(VW.RDCCashIn) * 100) AS 
	                         DECIMAL(38, 2)
	                     )
	                ELSE 0.00
	           END AS ACT,
	           --Variance Formula is [(Theo %-((Sum(CashOut)/Sum(Cashin))*100))]          
	           CASE 
	                WHEN (ISNULL(SUM(VW.RDCCashIn), 0) > 0) THEN CAST(
	                         (
	                             INS.Installation_Percentage_Payout -(SUM(VW.RDCCashOut) / SUM(VW.RDCCashIn) * 100)
	                         ) AS DECIMAL(38, 2)
	                     )
	                ELSE 0.00
	           END AS VARIANCE
	           INTO #Temp1
	    FROM   

       Installation INS
	           LEFT JOIN VW_Read VW
	                ON  (VW.Installation_no = INS.Installation_ID)
	           INNER JOIN MACHINE MC WITH (NOLOCK)
	                ON  InS.Machine_ID = MC.Machine_ID
	           INNER JOIN Bar_Position BPN WITH (NOLOCK)
	                ON  InS.Bar_Position_ID = BPN.Bar_Position_ID
	           INNER JOIN SITE S1
	                ON  BPN.Site_ID = S1.Site_ID
	         WHERE  S1.Site_ID =@SiteID AND 
	         (VW.ReadDate ) >= (@StartDate) 
	         AND (VW.ReadDate) <= (@endDate)           
	           
       
     GROUP BY    
            MC.Machine_Stock_No,    
      INS.Installation_Percentage_Payout,
      BPN.Bar_Position_Name  


	           SELECT 
	                  BP.Bar_Position_Name AS Position,
	                  Z.Zone_Name,
	                  S.Site_Name,
  (
	                      CASE 
	                           WHEN MC.Machine_Name = 'MULTI GAME' THEN 
	                              ISNULL(MGMP.MUltiGameName,'MULTI GAME')
	                              ELSE MC.Machine_Name 
	                              END
	                 )
                             AS Model,
	                  (
	                      CASE 
	                           WHEN MC.Machine_Name = 'MULTI GAME' THEN 
                                    ISNULL(MGMP.MUltiGameName,'MULTI GAME')
	                           ELSE (SELECT TOP 1 GT.Game_Title FROM dbo.MGMD_Installation MGI WITH (NOLOCK)
	                  LEFT JOIN dbo.Game_Library GL
	                       ON  MGI.MGMD_Game_ID = GL.MG_Game_ID
	                  LEFT JOIN dbo.Game_Title GT
	                       ON  GT.Game_Title_ID = GL.MG_Group_ID
	                       WHERE MGI.MGMD_Installation_ID = M1.Installation_ID)
	                      END
	                  ) AS GameName,
	                  M.Machine_ID,
	                  M.Machine_Stock_No AS asset,
	                  MAN.Manufacturer_Name AS Manu,
	                  ISNULL(CAT.Machine_Type_Code, '') AS Category,
	                  --MT.Machine_Type_Code as [Type],    
	                  CAT.Machine_Type_Code AS [Type],
	                  M1.RDCCashIn AS Handle,
	                  M1.RDCCashOut,
	                  M1.RDCCash AS CasinoWin,
	                  M1.DailyWin AS DailyWin,
	                  I.Installation_Percentage_Payout AS TheoPerc,
	                  M1.ACT AS ActPerc,
	                  M1.VARIANCE AS PercVar
	           FROM   Installation I
	                 
	                           
	                  INNER JOIN MACHINE M WITH (NOLOCK)
	                       ON  I.Machine_ID = M.Machine_ID
	                       INNER JOIN #Temp1 M1
	                       ON  M.Machine_ID = M1.Machine AND M1.Installation_ID = I.Installation_ID
	                  INNER JOIN Machine_class MC WITH (NOLOCK)
	                       ON  M.Machine_Class_ID = MC.Machine_Class_ID 
	                           --INNER JOIN
	                           -- Machine_Type MT WITH (NOLOCK) ON MC.Machine_Type_ID = MT.Machine_Type_ID             
	                            LEFT JOIN MultiGameMapping MGMP 
	                       ON  MGMP.Machineid =  M.Machine_ID 
	                  INNER JOIN Machine_Type CAT WITH (NOLOCK)
	                       ON  M.Machine_Category_ID = CAT.Machine_Type_ID
	                  INNER JOIN Bar_Position BP WITH (NOLOCK)
	                       ON  I.Bar_Position_ID = BP.Bar_Position_ID
	                  INNER JOIN SITE S
	                       ON  BP.Site_ID = S.Site_ID
	                  LEFT JOIN [Zone] Z
	                       ON  BP.Zone_ID = Z.Zone_ID
	                  INNER JOIN Manufacturer MAN
	                       ON  MC.Manufacturer_ID = MAN.Manufacturer_ID
	                  
	           WHERE  S.Site_ID = @SiteID
	                  AND (
	                          (@Category IS NULL)
	                          OR (
	                                 @Category IS NOT NULL
	                                 AND --MT.machine_type_id = @Category          
	                                     CAT.machine_type_id = @Category
	                             )
	                      ) 
	           GROUP BY
					  M.Machine_Stock_No,
	                  I.Installation_Percentage_Payout,
	                  BP.Bar_Position_Name,
	                  M1.Installation_ID,
	                  BP.Bar_Position_ID,
	                  Z.Zone_Name,
	                  S.Site_Name,
                         ISNULL(MGMP.MUltiGameName,'MULTI GAME'),
	                  MC.Machine_Name,
	                  M.Machine_ID,
	                  M.Machine_Stock_No,
	                  MAN.Manufacturer_Name,
	                  CAT.Machine_Type_Code,
	                  CAT.Machine_Type_Code,
	                  M1.RDCCashIn,
	                  M1.RDCCashOut,
	                  M1.RDCCash,
	                  M1.DailyWin,
	                  M1.ACT,
	                  M1.VARIANCE,
	                  I.Datapak_ID,
	                  I.Installation_End_date--,
GO

