USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_Report_MachinePerformance]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_Report_MachinePerformance]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*--------------------------------------------------------------------------   
---  
--- Description: retrieve information required for SDS_SlotPerformance report  
---               sp can be called a number of different ways, to either give line by line data, sub total or grand total figures  
---  
--- Inputs:      see inputs  
---  
--- Outputs:     (0)   - no error ..   
---              OTHER - SQL error   
---  
--------------------------------------------------------------------------   
-- to USE  
-- EXEC rsp_Report_MachinePerformance @runtype = 1, @gamingdate = '27 Sep 2010', @UsePhysicalWin=0, @site =0,@company=0, @subcompany=0  
-- EXEC rsp_Report_MachinePerformance @runtype = 2, @gamingdate = '01 jan 2010', @UsePhysicalWin=1, @site =0,@zone='',@company=6, @subcompany=0  
-- EXEC rsp_Report_MachinePerformance @runtype = 2, @denom=1  sub total, by denom 1.00  
-- EXEC rsp_Report_MachinePerformance @runtype = 2, @site = 55 sub total, by site (55)  
-- EXEC rsp_Report_MachinePerformance @runtype = 2, @machinetypeid=2, @denom=.25,@gamingdate = '01 jan 2010',@UsePhysicalWin=1, @site =55,@zone='' -- sub total, by machine type  
-- EXEC rsp_Report_MachinePerformance @runtype = 3, @gamingdate = '01 jan 2010', @UsePhysicalWin=1, @site =0,@zone='',@company=0, @subcompany=0  
-- EXEC rsp_Report_MachinePerformance @runtype = 2, @gamingdate = '01 jan 2010', @UsePhysicalWin=1, @site =0,@zone='ER'  
-----------------------------------------------------------------------------  
-- SDS comparable naming conventions  
--  Area = zone  
--  denom = pop  
--  site  
--  stand = bar_pos_name  
--- slot = asset number  
--- =======================================================================  
---   
--- Revision History  
---   
--- Jisha Lenu George   05/10/2010  Created 
--- Jisha Lenu George   12/11/2010  Updated the ReadDays check
--- Jisha Lenu George	20/12/10    Updated the criteria. Fix for #92412  
--------------------------------------------------------------------------- */  

CREATE PROCEDURE rsp_Report_MachinePerformance
  @company        INT         ,                  
  @subcompany     INT         ,                    
  @site           INT         ,                     
  --@zone           VARCHAR(100) ,                     
  @gamingdate     DATETIME    ,                         
  @denom          FLOAT       = 0  OUT,   --                   
  @MachineTypeID  INT         = 0  OUT,   --                   
  @runtype        INT         = 1  OUT,   -- (1), normal, (2) sub total, (3), grand total                   
  @UsePhysicalWin BIT         = 1  OUT    -- report switches from declared - 1, to metered                                 
AS
BEGIN              
    SET DATEFORMAT DMY                   

    DECLARE @mtdStart DATETIME,                  
            @qtdStart DATETIME,                  
            @ytdStart DATETIME                  

    DECLARE @currencyFormatting VARCHAR(20)                  
	SELECT @currencyFormatting = setting_value  	
	FROM Setting WHERE Setting_Name = 'BMC_Reports_Language'               

    DECLARE @Criteria VARCHAR(200)  
	DECLARE @zone VARCHAR(100)                      
	SET @zone = ''
     
declare @companyname varchar(50)      
if (ISNULL(@company,0) <> 0)      
 select @companyname = company_name from company where company_id = @company      
else      
 set  @companyname = '--Any--'      
      
declare @subcompanyname varchar(50)      
if (ISNULL(@subcompany,0) <> 0)      
 select @subcompanyname = sub_company_name from sub_company where sub_company_id = @subcompany      
else      
 set  @subcompanyname = '--Any--'      
      
declare @sitename varchar(50)      
if (ISNULL(@site,0) <> 0)      
 select @sitename = site_name from site where site_id = @site      
else      
 set  @sitename = '--Any--'      
      
declare @zonename varchar(50)      
if (LEN(@zone) < 1)      
 set  @zonename = '--Any--'      
else      
 set  @zonename = @zone      
      
SET @criteria = 'Company: ' + cast(@companyname as varchar(50))+ ' | ' +          
      'Sub Company: ' + cast(@subcompanyname as varchar(50))+ ' | ' +          
      'Site: ' + cast(@sitename as varchar(50))+ ' | ' +          
      'Zone: ' + @zonename  +
	  'Gaming Date: ' + CAST(@gamingdate AS VARCHAR(50)) + ' | ' +          
                                                 
					CASE WHEN @currencyFormatting = 'eu' THEN ' '     
							 ELSE 'Use Physical Win: ' + CASE WHEN @UsePhysicalWin = 1 THEN + 'YES' ELSE 'NO' END + ' | '     
						END +         
                    'Grouping on (Site, Denom)' 

    DECLARE @productVersion  VARCHAR(50)                  
    SELECT TOP 1 @productVersion = 'BMC Version : ' + VersionName FROM VersionHistory ORDER BY VersionDate DESC                  

    DECLARE @productHeader  VARCHAR(50)                  
    SELECT @productHeader = setting_value FROM Setting WHERE Setting_Name = 'BMC_Reports_Header'                  



    -- Quarter Jan-Mar, Apr-Jun, Jul-Sep,Oct-Dec                  
    DECLARE @qStartMonth INT                  
    SET @qStartMonth = CASE WHEN MONTH(@gamingdate) BETWEEN 1 AND 3 THEN 1                  
                            WHEN MONTH(@gamingdate) BETWEEN 4 AND 6 THEN 4                  
                            WHEN MONTH(@gamingdate) BETWEEN 7 AND 9 THEN 7                  
                       ELSE 10                  
                       END                 

    SET @mtdStart = '01/' + CAST ( DATEPART( MONTH, CAST(@gamingdate AS DATETIME) ) AS VARCHAR(3)) + '/' + CAST(YEAR(CAST(@gamingdate AS DATETIME)) AS VARCHAR(4))
    SET @qtdStart = '01/' + CAST (@qstartmonth AS VARCHAR(4)) + '/' + CAST(YEAR(CAST(@gamingdate AS DATETIME)) AS VARCHAR(4))                  
    SET @ytdStart = '01 Jan ' + CAST (YEAR(CAST(@gamingdate AS DATETIME)) AS VARCHAR(4))                  

    -- Get monthly values                  
    SELECT  [Order] = 1,                  
            [Type] = 'M',
            ReadDays =  SUM(ISNULL(VWR.Read_Days, 1)) ,            
            CoinIn = SUM(VWR.RDCCashIn),                    
            TotalDrop = CASE WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCashIn) -- Check this later
						ELSE SUM(VWR.TotalCashIn)
                        END,                  
            Expenses = CASE WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCashOut) -- Check this later
					   ELSE SUM(VWR.TotalCashOut)             
                       END,                  
            NetWin = CASE WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCash) -- Check this later
                     ELSE SUM(VWR.RDCCash)                 
                     END,                

            VWGMI.Bar_Position_ID,                  
            VWGMI.Bar_Position_Name,      -- stand                  
            VWGMI.Machine_Type_ID,                  
            VWGMI.Machine_Type_Code,                  
            VWGMI.Site_ID,                  
            VWGMI.Site_Name,
            VWGMI.Sub_Company_ID,                  
            VWGMI.Sub_Company_Name,                  
            VWGMI.Company_ID,                  
            VWGMI.Company_Name,
            VWGMI.Machine_Stock_No,       -- slot                  
            VWGMI.Zone_Name,              -- area
            VWGMI.holdper,
                
            ActWinPer = CASE WHEN @UsePhysicalWin = 1 THEN dbo.fnSDSActualWinPercentage ( SUM(VWR.RDCCash), SUM(VWR.RDCCashIn))  -- Check this later
                        ELSE dbo.fnSDSActualWinPercentage ( SUM(VWR.RDCCash),SUM(VWR.RDCCashIn))   
                        END,  
            PerVar = CASE WHEN @UsePhysicalWin = 1 THEN ((dbo.fnSDSActualWinPercentage ( SUM(VWR.RDCCash), SUM(VWR.RDCCashIn))) - VWGMI.HoldPer)  -- Check this later
                     ELSE ((dbo.fnSDSActualWinPercentage ( SUM(VWR.RDCCash), SUM(VWR.RDCCashIn))) - VWGMI.HoldPer   )
                     END,    

            WeightedAvgPer = 0,
            installation_price_per_play = CAST(CAST( VWGMI.installation_price_per_play AS FLOAT ) / 100 AS FLOAT ),
            ProductVersion = @productVersion,                  
            ProductHeader = @productHeader                  

    INTO #tmp
    FROM vw_GenericMachineInformation VWGMI                  
    INNER JOIN VW_Read VWR ON  VWGMI.Installation_ID = VWR.Installation_No   
    AND (CAST(VWGMI.Installation_Start_Date AS DATETIME) BETWEEN CAST(@mtdStart AS DATETIME) AND CAST(@gamingdate AS DATETIME))  

    WHERE              
        (( @denom <> 0 AND installation_price_per_play = @denom ) OR @denom = 0 ) AND            
        (( @zone <> '' AND zone_name = @zone ) OR @zone = '' ) AND 
        (( @site <> '' AND VWGMI.site_id = @site ) OR @site = 0 ) AND 
        (( @Company <> '' AND Company_id = @Company ) OR @Company = 0 ) AND 
        (( @Subcompany <> '' AND Sub_Company_id = @Subcompany ) OR @Subcompany = 0)                      
    GROUP BY
            VWGMI.HoldPer,
            VWGMI.Bar_Position_ID,                  
            VWGMI.Bar_Position_Name,   -- stand                  
            VWGMI.Machine_Type_ID,                  
            VWGMI.Machine_Type_Code ,                 
            VWGMI.Site_ID,                  
            VWGMI.Site_Name,

            VWGMI.Sub_Company_ID,                  
            VWGMI.Sub_Company_Name,                  
            VWGMI.Company_ID,                  
            VWGMI.Company_Name,

            VWGMI.Machine_Stock_No,    -- slot                  
            VWGMI.Zone_Name,           -- area
            installation_price_per_play

    UNION ALL

    -- Get quarterly values                
    SELECT  [Order] = 2,                
            [Type] = 'Q',                
            ReadDays =  SUM(ISNULL(VWR.Read_Days, 1)) ,            
            CoinIn = SUM(VWR.RDCCashIn),                    
            TotalDrop = CASE WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCashIn) -- Check this later
						ELSE SUM(VWR.TotalCashIn)
                        END,                  
            Expenses = CASE WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCashOut) -- Check this later
					   ELSE SUM(VWR.TotalCashOut)             
                       END,                  
            NetWin = CASE WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCash) -- Check this later
                     ELSE SUM(VWR.RDCCash)                 
                     END,                  

            VWGMI.Bar_Position_ID,                  
            VWGMI.Bar_Position_Name,      -- stand                  
            VWGMI.Machine_Type_ID,                  
            VWGMI.Machine_Type_Code ,                 
            VWGMI.Site_ID,                  
            VWGMI.Site_Name,
            VWGMI.Sub_Company_ID,                  
            VWGMI.Sub_Company_Name,                  
            VWGMI.Company_ID,                  
            VWGMI.Company_Name,
            VWGMI.Machine_Stock_No,       -- slot                  
            VWGMI.Zone_Name,              -- area
            VWGMI.holdper, 
               
            ActWinPer = CASE WHEN @UsePhysicalWin = 1 THEN dbo.fnSDSActualWinPercentage ( SUM(VWR.RDCCash), SUM(VWR.RDCCashIn))  -- Check this later
                        ELSE dbo.fnSDSActualWinPercentage ( SUM(VWR.RDCCash),SUM(VWR.RDCCashIn))   
                        END,  
            PerVar = CASE WHEN @UsePhysicalWin = 1 THEN (dbo.fnSDSActualWinPercentage ( SUM(VWR.RDCCash), SUM(VWR.RDCCashIn))) - VWGMI.HoldPer  -- Check this later
                     ELSE (dbo.fnSDSActualWinPercentage ( SUM(VWR.RDCCash), SUM(VWR.RDCCashIn))) - VWGMI.HoldPer   
                     END,    

            WeightedAvgPer = 0,
            installation_price_per_play = CAST(CAST( VWGMI.installation_price_per_play AS FLOAT ) / 100 AS FLOAT ),
            ProductVersion = @productVersion,                  
            ProductHeader = @productHeader                 

    FROM vw_GenericMachineInformation VWGMI                  
    INNER JOIN VW_Read VWR ON  VWGMI.Installation_ID = VWR.Installation_No     
    AND (CAST(VWGMI.Installation_Start_Date AS DATETIME) BETWEEN CAST(@qtdStart AS DATETIME) AND CAST(@gamingdate AS DATETIME))              

    WHERE           
        (( @denom <> 0 AND installation_price_per_play = @denom ) OR @denom = 0 ) AND          
        (( @zone <> '' AND zone_name = @zone ) OR @zone = '' ) AND 
        (( @site <> '' AND VWGMI.site_id = @site ) OR @site = 0 ) AND 
        (( @Company <> '' AND Company_id = @Company ) OR @Company = 0 ) AND 
        (( @Subcompany <> '' AND Sub_Company_id = @Subcompany ) OR @Subcompany = 0 )            

    GROUP BY 
            VWGMI.Bar_Position_ID,                
            VWGMI.Bar_Position_Name,     -- stand                
            VWGMI.Machine_Type_ID,                
            VWGMI.Machine_Type_Code ,               
            VWGMI.Site_ID,                
            VWGMI.Site_Name, 

            VWGMI.Sub_Company_ID,                
            VWGMI.Sub_Company_Name,                
            VWGMI.Company_ID,                
            VWGMI.Company_Name,  

            VWGMI.Machine_Stock_No,      -- slot                
            VWGMI.Zone_Name,             -- area                
            VWGMI.holdper,                
            VWGMI.installation_price_per_play           

    UNION ALL                

    -- Get yearly values               
    SELECT  [Order] = 3,                
            [Type] = 'Y',                
            ReadDays = SUM(ISNULL(VWR.Read_Days, 1)) ,            
            CoinIn = SUM(VWR.RDCCashIn),                    
            TotalDrop = CASE WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCashIn) -- Check this later
						ELSE SUM(VWR.TotalCashIn)
                        END,                  
            Expenses = CASE WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCashOut) -- Check this later
					   ELSE SUM(VWR.TotalCashOut)             
                       END,                  
            NetWin = CASE WHEN @UsePhysicalWin = 1 THEN SUM(VWR.RDCCash) -- Check this later
                     ELSE SUM(VWR.RDCCash)                 
                     END,                 

            VWGMI.Bar_Position_ID,                  
            VWGMI.Bar_Position_Name,      -- stand                  
            VWGMI.Machine_Type_ID,                  
            VWGMI.Machine_Type_Code,                  
            VWGMI.Site_ID,                  
            VWGMI.Site_Name,
            VWGMI.Sub_Company_ID,                  
            VWGMI.Sub_Company_Name,                  
            VWGMI.Company_ID,                  
            VWGMI.Company_Name,
            VWGMI.Machine_Stock_No,       -- slot                  
            VWGMI.Zone_Name,              -- area
            VWGMI.holdper, 
               
            ActWinPer = CASE WHEN @UsePhysicalWin = 1 THEN dbo.fnSDSActualWinPercentage ( SUM(VWR.RDCCash), SUM(VWR.RDCCashIn))  -- Check this later
                        ELSE dbo.fnSDSActualWinPercentage ( SUM(VWR.RDCCash),SUM(VWR.RDCCashIn))  
                        END,  
            PerVar = CASE WHEN @UsePhysicalWin = 1 THEN (dbo.fnSDSActualWinPercentage ( SUM(VWR.RDCCash), SUM(VWR.RDCCashIn)) - VWGMI.HoldPer ) -- Check this later
                     ELSE (dbo.fnSDSActualWinPercentage ( SUM(VWR.RDCCash), SUM(VWR.RDCCashIn)) - VWGMI.HoldPer   )
                     END,    

            WeightedAvgPer = 0,
            installation_price_per_play = CAST(CAST( VWGMI.installation_price_per_play AS FLOAT ) / 100 AS FLOAT ),
            ProductVersion = @productVersion,                  
            ProductHeader = @productHeader                 

    FROM vw_GenericMachineInformation VWGMI                  
    INNER JOIN VW_Read VWR ON  VWGMI.Installation_ID = VWR.Installation_No                   
    AND (CAST(VWGMI.Installation_Start_Date AS DATETIME) BETWEEN CAST(@ytdStart AS DATETIME) AND CAST(@gamingdate AS DATETIME))

    WHERE           
        (( @denom <> 0 AND installation_price_per_play = @denom ) OR @denom = 0 ) AND          
        (( @zone <> '' AND zone_name = @zone ) OR @zone = '' ) AND 
        (( @site <> '' AND VWGMI.site_id = @site ) OR @site = 0 ) AND 
        (( @Company <> '' AND Company_id = @Company ) OR @Company = 0 ) AND 
        (( @Subcompany <> '' AND Sub_Company_id = @Subcompany ) OR @Subcompany = 0 )            

    GROUP BY
            VWGMI.Bar_Position_ID,                
            VWGMI.Bar_Position_Name ,   -- stand                
            VWGMI.Machine_Type_ID,                
            VWGMI.Machine_Type_Code,                
            VWGMI.Site_ID,                
            VWGMI.Site_Name,                

            VWGMI.Sub_Company_ID,                
            VWGMI.Sub_Company_Name,                
            VWGMI.Company_ID,                
            VWGMI.Company_Name,                

            VWGMI.Machine_Stock_No,     -- slot                
            VWGMI.Zone_Name,            -- area                
            VWGMI.holdper,                
            VWGMI.installation_price_per_play  

    -- Return the values                
    IF ( @runtype = 1 )                
    BEGIN                
        IF EXISTS ( SELECT 1 FROM #tmp )             

        SELECT 
				[Order],                
                [Type],         
                ReadDays,                
                CoinIn,                
                TotalDrop,                
                Expenses,                
                NetWin,                
                Bar_Position_ID,                
                Bar_Position_Name,                          
                Machine_Type_ID,                
                Machine_Type_Code,              
                Site_ID,                
                Site_Name,
                Sub_Company_ID,                
                Sub_Company_Name,                
                Company_ID,                
                Company_Name,
                Machine_Stock_No,                
                Zone_Name,                
                HoldPer,                
                ActWinPer,                
                PerVar,                
                WeightedAvgPer = (CAST ( NetWin / CASE WHEN ReadDays = 0 then 1 else ReadDays END  AS FLOAT ))/            
                                    CASE WHEN (CAST ( CoinIn / CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END  AS FLOAT )) = 0 THEN 1 
                                    ELSE (CAST ( CoinIn / CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END AS FLOAT )) END ,            
                installation_price_per_play,                 
                ProductVersion,                 
                ProductHeader,                
                AvgCoinInPerDayPerMachine = CAST (CoinIn / CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END AS FLOAT ),                
                AvgNetWinPerDayPerMachine = CAST (NetWin / CASE WHEN ReadDays = 0 THEN 1 ELSE ReadDays END AS FLOAT ),                
                TheoWin = ( CoinIn * HoldPer ) / 100,                
                Criteria = @criteria,                
                currencyformatting = @currencyFormatting                

        FROM #tmp            
        ORDER BY Bar_Position_ID, [Order]         
    ELSE                
        -- blank line                
        SELECT  [Order] = NULL,                
                [Type] = NULL,                
                ReadDays = NULL,                
                CoinIn = NULL,                
                TotalDrop = NULL,                
                Expenses = NULL,                
                NetWin = NULL,                
                Bar_Position_ID = NULL,                
                Bar_Position_Name = NULL,                          
                Machine_Type_ID = NULL,                
                Machine_Type_Code = NULL,                
                Site_ID = NULL,                
                Site_Name = NULL,
                Sub_Company_ID = NULL,                
                Sub_Company_Name = NULL,                
                Company_ID = NULL,                
                Company_Name = NULL,
                Machine_Stock_No = NULL,                
                Zone_Name = NULL,                
                HoldPer = NULL,                
                ActWinPer = NULL,                
                PerVar = NULL,                
                WeightedAvgPer = NULL,
                installation_price_per_play = NULL,
                ProductVersion = @productVersion,                 
                ProductHeader = @productHeader,
                AvgCoinInPerDayPerMachine  = NULL,                
                AvgNetWinPerDayPerMachine  = NULL,
                TheoWin = NULL,
                Criteria = @criteria,
                currencyformatting = @currencyFormatting                
    END
    ELSE IF ( @runtype = 2 )  -- Sub Grouping                
    BEGIN                
    SELECT  [Order],                
            [Type],            
            ReadDays            = AVG(ReadDays),                
            CoinIn              = SUM(CoinIn),                
            TotalDrop           = SUM(TotalDrop),                
            Expenses            = SUM(Expenses),          
            NetWin              = SUM(NetWin),                
            Bar_Position_ID     = NULL,                
            Bar_Position_Name   = NULL,                          
            Machine_Type_ID     = CASE WHEN @machinetypeid != '' THEN Machine_Type_ID ELSE NULL END,                
            Machine_Type_Code   = CASE WHEN @machinetypeid != '' THEN Machine_Type_Code ELSE NULL END,                
            Site_ID             = CASE WHEN @site != '' THEN Site_ID ELSE NULL END,                
            Site_Name           = CASE WHEN @site != '' THEN Site_Name ELSE NULL END,
            Sub_Company_ID      = CASE WHEN @subcompany != '' THEN Sub_Company_ID ELSE NULL END,                
            Sub_Company_Name    = CASE WHEN @subcompany != '' THEN Sub_Company_Name ELSE NULL END,                
            Company_ID          = CASE WHEN @company != '' THEN Company_ID ELSE NULL END,                
            Company_Name        = CASE WHEN @company != '' THEN Company_Name ELSE NULL END,
            Machine_Stock_No    = NULL,  -- slot                
            Zone_Name           = CASE WHEN @zone != '' THEN Zone_Name ELSE NULL END,                          
            HoldPer             = AVG(HoldPer),                
            ActWinPer           = AVG(ActWinPer),                
            PerVar              = AVG(PerVar),                
            WeightedAvgPer      = (CAST (SUM(NetWin) / AVG(CASE WHEN ReadDays = 0 then 1 else ReadDays END) AS FLOAT ) / COUNT(DISTINCT Bar_Position_ID))            
                                    / CASE WHEN ((CAST (SUM(CoinIn) / AVG(CASE WHEN ReadDays = 0 then 1 else ReadDays END) AS FLOAT ) / COUNT(DISTINCT Bar_Position_ID))) = 0 THEN 1 
                                      ELSE (CAST ( SUM(CoinIn) / AVG(CASE WHEN ReadDays = 0 then 1 else ReadDays END) AS FLOAT ) / COUNT(DISTINCT Bar_Position_ID)) END,            

            installation_price_per_play = CASE WHEN @denom != 0 THEN installation_price_per_play ELSE NULL END,                 
            ProductVersion,                 
            ProductHeader,                 
            AvgCoinInPerDayPerMachine = CAST (SUM(CoinIn) / AVG(CASE WHEN ReadDays = 0 then 1 else ReadDays END) AS FLOAT ) / COUNT(DISTINCT Bar_Position_ID),                
            AvgNetWinPerDayPerMachine = CAST (SUM(NetWin) / AVG(CASE WHEN ReadDays = 0 then 1 else ReadDays END) AS FLOAT ) / COUNT(DISTINCT Bar_Position_ID),                
            TheoWin = (SUM(CoinIn) * AVG(HoldPer) ) / 100,                
            Criteria = @criteria,                
            currencyformatting = @currencyFormatting                

    FROM #tmp              
    WHERE           
        (( @denom <> 0 AND #tmp.installation_price_per_play = @denom ) OR @denom = 0 ) AND          
        (( @zone <> '' AND zone_name = @zone ) OR @zone = '' ) AND 
        (( @site <> '' AND #tmp.site_id = @site ) OR @site = 0 ) AND 
        (( @Company <> '' AND Company_id = @Company ) OR @Company = 0 ) AND 
        (( @Subcompany <> '' AND Sub_Company_id = @Subcompany ) OR @Subcompany = 0 )              

    GROUP BY    CASE WHEN @zone != '' THEN Zone_Name ELSE NULL END,                
                CASE WHEN @denom != 0 THEN installation_price_per_play ELSE NULL END,                 
                CASE WHEN @site != '' THEN Site_ID ELSE NULL END,                
                CASE WHEN @site != '' THEN Site_Name ELSE NULL END,                
                CASE WHEN @subcompany != '' THEN Sub_Company_ID ELSE NULL END,                
                CASE WHEN @subcompany != '' THEN Sub_Company_Name ELSE NULL END,                
                CASE WHEN @company != '' THEN Company_ID ELSE NULL END,                
                CASE WHEN @company != '' THEN Company_Name ELSE NULL END,                
                CASE WHEN @machinetypeid != '' THEN Machine_Type_ID ELSE NULL END,                
                CASE WHEN @machinetypeid != '' THEN Machine_Type_Code ELSE NULL END,                
                [Type],                
                [Order],                
                ProductVersion, 
                ProductHeader , 
                #tmp.site_id              

    ORDER BY [Order]            
    END 
    ELSE IF ( @runtype = 3 )                 
    BEGIN                

        SELECT  [Order],                
                [Type],                
                ReadDays          = AVG(ReadDays),                
                CoinIn            = SUM(CoinIn),                
                TotalDrop         = SUM(TotalDrop),                
                Expenses          = SUM(Expenses),                
                NetWin            = SUM(NetWin),                
                Bar_Position_ID   = NULL,                
                Bar_Position_Name = NULL,                          
                Machine_Type_ID   = NULL,                
                Machine_Type_Code = NULL,                
                Site_ID           = NULL,                
                Site_Name         = NULL,                
                Sub_Company_ID    = NULL,                
                Sub_Company_Name  = NULL,                
                Company_ID        = NULL,                
                Company_Name      = NULL,
                Machine_Stock_No  = NULL,                      
                Zone_Name         = NULL,        
                HoldPer           = AVG(HoldPer),                
                ActWinPer         = AVG(ActWinPer),                
                PerVar            = AVG(PerVar),                
                WeightedAvgPer    = (CAST ( SUM(NetWin) / AVG(CASE WHEN ReadDays = 0 then 1 else ReadDays END) AS FLOAT ) / COUNT(DISTINCT Bar_Position_ID))            
                                        / CASE WHEN ((CAST ( SUM(CoinIn) / AVG(CASE WHEN ReadDays = 0 then 1 else ReadDays END) AS FLOAT ) / COUNT(DISTINCT Bar_Position_ID))) = 0 THEN 1 
                                          ELSE (CAST ( SUM(CoinIn) / AVG(CASE WHEN ReadDays = 0 then 1 else ReadDays END) AS FLOAT ) / COUNT(DISTINCT Bar_Position_ID)) END,            

                installation_price_per_play = NULL,                
                ProductVersion,                 
                ProductHeader,                
                AvgCoinInPerDayPerMachine = CAST ( SUM(CoinIn) / AVG(CASE WHEN ReadDays = 0 then 1 else ReadDays END) AS FLOAT ) / COUNT(DISTINCT Bar_Position_ID),                
                AvgNetWinPerDayPerMachine = CAST ( SUM(NetWin) / AVG(CASE WHEN ReadDays = 0 then 1 else ReadDays END) AS FLOAT ) / COUNT(DISTINCT Bar_Position_ID),                
                TheoWin = ( SUM(CoinIn) * AVG(HoldPer) ) / 100,                
                Criteria = @criteria,                
                currencyformatting = @currencyFormatting                

        FROM #tmp
        GROUP BY [Order],                
                 [Type],                
                 ProductVersion,                 
                 ProductHeader
        ORDER BY [Order]
    END

END     

GO

