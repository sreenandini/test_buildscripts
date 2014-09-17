USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_EFTVersusBMCRevenueComparison]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_EFTVersusBMCRevenueComparison]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*-------------------------------------------------------------------------- 
---
--- Description: retrieve information required for SDS_rsp_Report_ElectronicTransferSlotActivityCumulative report
---               sp can be called a number of different ways, to either give line by line data, sub total or grand total figures
---
--- ** requires linking to correct fields in READ, and using new EFT audit table
---
--- Inputs:      see inputs
---
--- Outputs:     (0)   - no error .. 
---              OTHER - SQL error 
---
-------------------------------------------------------------------------- 
-- to USE
-- select * from [read]
-- EXEC rsp_Report_EFTVersusBMCRevenueComparison  @gamingdate='27 Oct 2011'
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
--- C.Taylor  ( Contractor )  13/04/10    Created
--- Vineetha M				  30/04/10    Modified	Updated read columns to the dummy fields
--- Jisha Lenu George		  20/12/10    Updated the criteria. Fix for #92412  
--- A.Vinod Kumar			  21/12/10    Unassigned zone names are marked as 'Not Set'  

--EXEC rsp_Report_EFTVersusBMCRevenueComparison @Company=0, @subcompany=0, @site=0, @zone=0, @gamingdate='05 Feb 2013'

--------------------------------------------------------------------------- */
CREATE PROCEDURE [dbo].[rsp_Report_EFTVersusBMCRevenueComparison]
	@Company INT = 0,
	@SubCompany INT = 0,
	@Region INT = 0,
	@Area INT = 0,
	@District INT = 0,
	@Site INT = 0,
	@Zone INT = 0,
	@GamingDate DATETIME ,
	@GroupByZone BIT ,
	@SiteIDList VARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON                
	DECLARE @CalcGamingDate DATETIME
	SET @CalcGamingDate = CAST(CONVERT(VARCHAR(12), @GamingDate) AS DATETIME)	
	
	IF @Company = 0
	    SET @Company = NULL
	
	IF @SubCompany = 0
	    SET @SubCompany = NULL
	    
	IF @Region = 0
	    SET @Region = NULL 
	
	IF @Area = 0
	    SET @Area = NULL
	
	IF @District = 0
	    SET @District = NULL
	
	IF @Site = 0
	    SET @Site = NULL
	
	IF @Zone = 0
	    SET @Zone = NULL
	
	
	
	-- get the raw information from read. 
	
	SELECT r.installation_id,
	       [Type] = 'Accounting',
	       [Ordering] = 1,
	       [WAT_In] = CAST(
	           SUM(CAST(Cashable_EFT_IN AS FLOAT) / 100) AS DECIMAL(10, 2)
	       ),
	       [WAT_Out] = CAST(
	           SUM(CAST(Cashable_EFT_OUT AS FLOAT) / 100) AS DECIMAL(10, 2)
	       ),
	       [Cashable_ePromo_In] = CAST(
	           SUM(CAST(Promo_Cashable_EFT_IN AS FLOAT) / 100) AS DECIMAL(10, 2)
	       ),
	       [Cashable_ePromo_Out] = CAST(
	           SUM(CAST(Promo_Cashable_EFT_OUT AS FLOAT) / 100) AS DECIMAL(10, 2)
	       ),
	       [NCashable_ePromo_In] = CAST(
	           SUM(CAST(NonCashable_EFT_IN AS FLOAT) / 100) AS DECIMAL(10, 2)
	       ),
	       [NCashable_ePromo_Out] = CAST(
	           SUM(CAST(NonCashable_EFT_OUT AS FLOAT) / 100) AS DECIMAL(10, 2)
	       ),
	       [SortOrder] = 1 
	       INTO 
	       #info
	FROM   [read] r WITH (NOLOCK)
	       LEFT OUTER JOIN vw_genericmachineinformation gmi WITH (NOLOCK)
	            ON  gmi.Installation_ID = r.Installation_ID
	       INNER JOIN [Site] S
	            ON  gmi.Site_ID = S.Site_ID
	       INNER JOIN Sub_Company SC WITH(NOLOCK)
	            ON  SC.sub_company_id = S.Sub_Company_ID
	       INNER JOIN Company C WITH(NOLOCK)
	            ON  SC.company_id = C.company_id
	WHERE  CAST(CONVERT(VARCHAR(20), r.ReadDate, 106) AS DATETIME) = @calcGamingDate 
	       --r.ReadDate = @calcGamingDate
	       --AND ISNULL(@Zone, gmi.Zone_ID) = gmi.Zone_ID
	       AND (@Zone IS NULL OR (@Zone IS NOT NULL AND Zone_ID =@Zone))
	       AND ISNULL(@Site, S.Site_id) = S.Site_ID
	       AND (
	               @SiteIDList IS NOT NULL
	               AND gmi.Site_ID IN (SELECT DATA
	                                   FROM   fnSplit(@SiteIDList, ','))
	           )
	       AND ISNULL(@Company, C.Company_Id) = C.Company_Id
	       AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	       AND ISNULL(@Region, S.Sub_Company_Region_ID) = S.Sub_Company_Region_ID
	       AND ISNULL(@Area, S.Sub_Company_Area_ID) = S.Sub_Company_Area_ID
	       AND ISNULL(@District, S.Sub_Company_District_ID) = S.Sub_Company_District_ID
	GROUP BY
	       r.installation_id 
	UNION ALL          
	SELECT gmi.installation_id,
	       [Type] = 'Accounting',
	       [Ordering] = 1,
	       [WAT_In] = CAST(0 AS DECIMAL(10, 2)),
	       [WAT_Out] = CAST(0 AS DECIMAL(10, 2)),
	       [Cashable_ePromo_In] = CAST(0 AS DECIMAL(10, 2)),
	       [Cashable_ePromo_Out] = CAST(0 AS DECIMAL(10, 2)),
	       [NCashable_ePromo_In] = CAST(0 AS DECIMAL(10, 2)),
	       [NCashable_ePromo_Out] = CAST(0 AS DECIMAL(10, 2)),
	       [SortOrder] = 1
	FROM   vw_genericmachineinformation gmi WITH (NOLOCK)
	       LEFT OUTER JOIN [read] r WITH (NOLOCK)
	            ON  gmi.Installation_ID = r.Installation_ID
	            AND CAST(CONVERT(VARCHAR(20), r.ReadDate, 106) AS DATETIME) = @calcGamingDate
	       INNER JOIN SITE S
	            ON  S.Site_ID = gmi.Site_ID
	       INNER JOIN Sub_Company SC WITH(NOLOCK)
	            ON  SC.sub_company_id = S.Sub_Company_ID
	       INNER JOIN Company C WITH(NOLOCK)
	            ON  SC.company_id = C.company_id
	WHERE  r.Read_ID IS NULL
	       AND @calcGamingDate BETWEEN CAST(
	               CONVERT(VARCHAR(12), gmi.Installation_Start_Date) AS DATETIME
	           ) 
	           AND CAST(
	               CONVERT(VARCHAR(12), ISNULL(gmi.Installation_End_Date, GETDATE())) 
	               AS DATETIME
	           )
	      -- AND ISNULL(@Zone, gmi.Zone_ID) = gmi.Zone_ID
	      AND (@Zone IS NULL OR (@Zone IS NOT NULL AND Zone_ID =@Zone))
	       AND ISNULL(@Site, S.Site_id) = S.Site_ID
	       AND (
	               @SiteIDList IS NOT NULL
	               AND gmi.Site_ID IN (SELECT DATA
	                                   FROM   fnSplit(@SiteIDList, ','))
	           )
	       AND ISNULL(@Company, C.Company_Id) = C.Company_Id
	       AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	       AND ISNULL(@Region, S.Sub_Company_Region_ID) = S.Sub_Company_Region_ID
	       AND ISNULL(@Area, S.Sub_Company_Area_ID) = S.Sub_Company_Area_ID
	       AND ISNULL(@District, S.Sub_Company_District_ID) = S.Sub_Company_District_ID
	
	UNION
	
	SELECT t.Installation_No,
	       [Type] = 'Transaction',
	       [Ordering] = 2,
	       [WAT_In] = CAST(SUM(WAT_Out) / 100 AS DECIMAL(10, 2)),
	       [WAT_Out] = CAST(0 AS DECIMAL(10, 2)),
	       [Cashable_ePromo_In] = CAST(SUM(Promo_Cashable_EFT_OUT) / 100 AS DECIMAL(10, 2)),
	       [Cashable_ePromo_Out] = CAST(0 AS DECIMAL(10, 2)),
	       [NCashable_ePromo_In] = CAST(SUM(NonCashable_EFT_OUT) / 100 AS DECIMAL(10, 2)),
	       [NCashable_ePromo_Out] = CAST(0 AS DECIMAL(10, 2)),
	       [SortOrder] = 1
	FROM   [AFT_Transactions] t WITH (NOLOCK) -- this will be treasury_entry   
	       
	       LEFT OUTER JOIN vw_genericmachineinformation gmi WITH (NOLOCK)
	            ON  gmi.Installation_ID = t.Installation_No
	       INNER JOIN SITE S
	            ON  S.Site_ID = gmi.Site_ID
	       INNER JOIN Sub_Company SC WITH(NOLOCK)
	            ON  SC.sub_company_id = S.Sub_Company_ID
	       INNER JOIN Company C WITH(NOLOCK)
	            ON  SC.company_id = C.company_id
	WHERE  CAST(CONVERT(VARCHAR(20), Transaction_Date, 106) AS DATETIME) = @calcGamingDate
	       AND UPPER(transaction_type) = 'WITHDRAWAL COMPLETE'
	      -- AND ISNULL(@Zone, gmi.Zone_ID) = gmi.Zone_ID
	       AND (@Zone IS NULL OR (@Zone IS NOT NULL AND Zone_ID =@Zone))
	       AND ISNULL(@Site, S.Site_id) = S.Site_ID
	       AND (
	               @SiteIDList IS NOT NULL
	               AND gmi.Site_ID IN (SELECT DATA
	                                   FROM   fnSplit(@SiteIDList, ','))
	           )
	       AND ISNULL(@Company, C.Company_Id) = C.Company_Id
	       AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	       AND ISNULL(@Region, S.Sub_Company_Region_ID) = S.Sub_Company_Region_ID
	       AND ISNULL(@Area, S.Sub_Company_Area_ID) = S.Sub_Company_Area_ID
	       AND ISNULL(@District, S.Sub_Company_District_ID) = S.Sub_Company_District_ID
	GROUP BY
	       t.Installation_No 
	
	
	--RETURN
	-- create a dummy transaction entry if no transactions found        
	
	INSERT INTO #info
	SELECT r.installation_id,
	       --'Transaction',                
	       [Type] = 'Transaction',
	       [Ordering] = 2,
	       [WAT_In] = CAST(0 AS DECIMAL(10, 2)),
	       [WAT_Out] = CAST(0 AS DECIMAL(10, 2)),
	       [Cashable_ePromo_In] = CAST(0 AS DECIMAL(10, 2)),
	       [Cashable_ePromo_Out] = CAST(0 AS DECIMAL(10, 2)),
	       [NCashable_ePromo_In] = CAST(0 AS DECIMAL(10, 2)),
	       [NCashable_ePromo_Out] = CAST(0 AS DECIMAL(10, 2)),
	       [SortOrder] = 1
	FROM   [read] r WITH (NOLOCK)
	WHERE  CONVERT(VARCHAR(20), r.ReadDate, 106) = CONVERT(VARCHAR(20), @gamingdate, 106) 
	       --r.ReadDate = @calcGamingDate
	       AND (
	               SELECT COUNT(*)
	               FROM   #info
	               WHERE  r.installation_id = #info.installation_id
	           ) = 1                
	
	SELECT * 
	       INTO #tmp
	FROM   #info          
	
	
	
	
	INSERT INTO #info
	SELECT installation_id = 0,
	       [Type] = 'Accounting',
	       [Ordering] = 4,
	       [WAT_In] = CAST(
	           SUM(CAST(Cashable_EFT_IN AS FLOAT) / 100) AS DECIMAL(10, 2)
	       ),
	       [WAT_Out] = CAST(
	           SUM(CAST(Cashable_EFT_OUT AS FLOAT) / 100) AS DECIMAL(10, 2)
	       ),
	       [Cashable_ePromo_In] = CAST(
	           SUM(CAST(Promo_Cashable_EFT_IN AS FLOAT) / 100) AS DECIMAL(10, 2)
	       ),
	       [Cashable_ePromo_Out] = CAST(
	           SUM(CAST(Promo_Cashable_EFT_OUT AS FLOAT) / 100) AS DECIMAL(10, 2)
	       ),
	       [NCashable_ePromo_In] = CAST(
	           SUM(CAST(NonCashable_EFT_IN AS FLOAT) / 100) AS DECIMAL(10, 2)
	       ),
	       [NCashable_ePromo_Out] = CAST(
	           SUM(CAST(NonCashable_EFT_OUT AS FLOAT) / 100) AS DECIMAL(10, 2)
	       ),
	       [SortOrder] = 2
	FROM   [read] r WITH (NOLOCK)
	       LEFT OUTER JOIN vw_genericmachineinformation gmi WITH (NOLOCK)
	            ON  gmi.Installation_ID = r.Installation_ID
	       INNER JOIN [Site] S
	            ON  S.Site_ID = gmi.Site_ID
	       INNER JOIN Sub_Company SC WITH(NOLOCK)
	            ON  SC.sub_company_id = S.Sub_Company_ID
	       INNER JOIN Company C WITH(NOLOCK)
	            ON  SC.company_id = C.company_id
	WHERE  CONVERT(VARCHAR(20), r.ReadDate, 106) = CONVERT(VARCHAR(20), @gamingdate, 106)
	       --r.ReadDate = @calcGamingDate
	      -- AND ISNULL(@Zone, gmi.Zone_ID) = gmi.Zone_ID
	       AND (@Zone IS NULL OR (@Zone IS NOT NULL AND Zone_ID =@Zone))
	       AND ISNULL(@Site, S.Site_id) = S.Site_ID
	       AND (
	               @SiteIDList IS NOT NULL
	               AND gmi.Site_ID IN (SELECT DATA
	                                   FROM   fnSplit(@SiteIDList, ','))
	           )
	       AND ISNULL(@Company, C.Company_Id) = C.Company_Id
	       AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	       AND ISNULL(@Region, S.Sub_Company_Region_ID) = S.Sub_Company_Region_ID
	       AND ISNULL(@Area, S.Sub_Company_Area_ID) = S.Sub_Company_Area_ID
	       AND ISNULL(@District, S.Sub_Company_District_ID) = S.Sub_Company_District_ID
	        
	UNION ALL
	SELECT installation_id = 0,
	       [Type] = 'Transaction',
	       [Ordering] = 5,
	       [WAT_In] = ISNULL(CAST(SUM(WAT_Out) / 100 AS DECIMAL(10, 2)), 0),
	       [WAT_Out] = 0,
	       [Cashable_ePromo_In] = ISNULL(
	           CAST(SUM(Promo_Cashable_EFT_OUT) / 100 AS DECIMAL(10, 2)),
	           0
	       ),
	       [Cashable_ePromo_Out] = 0,
	       [NCashable_ePromo_In] = ISNULL(CAST(SUM(NonCashable_EFT_OUT) / 100 AS DECIMAL(10, 2)), 0),
	       [NCashable_ePromo_Out] = 0,
	       [SortOrder] = 2
	FROM   [AFT_Transactions] t WITH (NOLOCK) -- this will be treasury_entry              
	       
	       LEFT OUTER JOIN vw_genericmachineinformation gmi WITH (NOLOCK)
	            ON  gmi.Installation_ID = t.Installation_No
	       INNER JOIN [Site] S
	            ON  S.Site_ID = gmi.Site_ID
	       INNER JOIN Sub_Company SC WITH(NOLOCK)
	            ON  SC.sub_company_id = S.Sub_Company_ID
	       INNER JOIN Company C WITH(NOLOCK)
	            ON  SC.company_id = C.company_id
	WHERE  CAST(CONVERT(VARCHAR(20), Transaction_Date, 106) AS DATETIME) = @calcGamingDate
	       AND UPPER(transaction_type) = 'WITHDRAWAL COMPLETE'
	       --AND ISNULL(@Zone, gmi.Zone_ID) = gmi.Zone_ID
	        AND (@Zone IS NULL OR (@Zone IS NOT NULL AND Zone_ID =@Zone))
	       AND ISNULL(@Site, S.Site_id) = S.Site_ID
	       AND (
	               @SiteIDList IS NOT NULL
	               AND gmi.Site_ID IN (SELECT DATA
	                                   FROM   fnSplit(@SiteIDList, ','))
	           )
	       AND ISNULL(@Company, C.Company_Id) = C.Company_Id
	       AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	       AND ISNULL(@Region, S.Sub_Company_Region_ID) = S.Sub_Company_Region_ID
	       AND ISNULL(@Area, S.Sub_Company_Area_ID) = S.Sub_Company_Area_ID
	       AND ISNULL(@District, S.Sub_Company_District_ID) = S.Sub_Company_District_ID
	
	
	INSERT INTO #tmp
	SELECT *
	FROM   #info
	WHERE  installation_id = 0     
	
	
	SELECT DISTINCT(t.Installation_No) AS Installation_No,
	       ISNULL(CAST(SUM(t.WAT_Out) / 100 AS DECIMAL(10, 2)), 0) AS WATOut,
	       ISNULL(
	           CAST(SUM(t.Promo_Cashable_EFT_OUT) / 100 AS DECIMAL(10, 2)),
	           0
	       ) AS PromoCashable,
	       ISNULL(
	           CAST(SUM(t.NonCashable_EFT_OUT) / 100 AS DECIMAL(10, 2)),
	           0
	       ) AS NonCashableOut 
	       INTO 
	       #temp12
	FROM   AFT_Transactions t
	       INNER JOIN vw_GenericMachineInformation vgmi
	            ON  vgmi.Installation_ID = t.installation_no
	       INNER JOIN SITE S
	            ON  S.Site_ID = vgmi.Site_ID
	       INNER JOIN Sub_Company SC WITH(NOLOCK)
	            ON  SC.sub_company_id = S.Sub_Company_ID
	       INNER JOIN Company C WITH(NOLOCK)
	            ON  SC.company_id = C.company_id
		WHERE
	            UPPER(transaction_type) = 'DEPOSIT COMPLETE'
	            AND CONVERT(VARCHAR(20), t.Transaction_Date, 106) = CONVERT(VARCHAR(20), @gamingdate, 106)
	            --AND ISNULL(@Zone, vgmi.Zone_ID) = vgmi.Zone_ID
	             AND (@Zone IS NULL OR (@Zone IS NOT NULL AND Zone_ID =@Zone))
	            AND ISNULL(@Site, vgmi.Site_id) = vgmi.Site_ID
	            AND (
	                    @SiteIDList IS NOT NULL
	                    AND vgmi.Site_ID IN (SELECT DATA
	                                         FROM   fnSplit(@SiteIDList, ','))
	                )
	            AND ISNULL(@Company, C.Company_Id) = C.Company_Id
	            AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	            AND ISNULL(@Region, S.Sub_Company_Region_ID) = S.Sub_Company_Region_ID
	            AND ISNULL(@Area, S.Sub_Company_Area_ID) = S.Sub_Company_Area_ID
	            AND ISNULL(@District, S.Sub_Company_District_ID) = S.Sub_Company_District_ID
	GROUP BY
	       Installation_No  
	
	
	SELECT t.Installation_No,
	       SUM(WATOut) AS WATOut1,
	       SUM(PromoCashable) AS Promocashable,
	       SUM(NonCashableOut) AS NonCashable 
	       INTO 
	       #temp1
	FROM   #temp12 t
	       INNER JOIN vw_GenericMachineInformation vgmi
	            ON  vgmi.Installation_ID = t.Installation_No
	GROUP BY
	       t.Installation_No  
	
	
	SELECT SUM(WATOut) AS WATOut1,
	       SUM(PromoCashable) AS Promocashable,
	       SUM(NonCashableOut) AS NonCashable 
	       INTO 
	       #temp2
	FROM   #temp12 t
	       INNER JOIN vw_GenericMachineInformation vgmi
	            ON  vgmi.Installation_ID = t.Installation_No  
	
	
	UPDATE #tmp
	SET    [WAT_Out] = ISNULL(#temp1.WATOut1, 0),
	       [Cashable_ePromo_Out] = ISNULL(#temp1.Promocashable, 0),
	       [NCashable_ePromo_Out] = ISNULL(#temp1.NonCashable, 0)
	FROM   #temp1
	       INNER JOIN #tmp
	            ON  #temp1.Installation_No = #tmp.Installation_id
	WHERE  [Type] = 'Transaction'
	       AND [Ordering] = 2   
	
	
	UPDATE #tmp
	SET    [WAT_Out] = ISNULL(t.WATOut1, 0),
	       [Cashable_ePromo_Out] = ISNULL(t.Promocashable, 0),
	       [NCashable_ePromo_Out] = ISNULL(t.NonCashable, 0)
	FROM   #temp2 t
	WHERE  [Type] = 'Transaction'
	       AND [Ordering] = 5   
	
	
	SELECT * INTO #info1
	FROM   #tmp  
	
	INSERT INTO #tmp
	--
	-- create a variance line        
	
	SELECT acc.installation_id,
	       [Type] = 'Variance',
	       [Ordering] = 3,
	       [WAT_In] = ISNULL((acc.[WAT_In] - trans.[WAT_In]), 0),
	       [WAT_Out] = ISNULL(
	           (
	               CAST(acc.[WAT_Out] AS DECIMAL(10, 2)) - CAST(trans.[WAT_Out] AS DECIMAL(10, 2))
	           ),
	           0
	       ),
	       [Cashable_ePromo_In] = ISNULL((acc.[Cashable_ePromo_In] - trans.[Cashable_ePromo_In]), 0),
	       [Cashable_ePromo_Out] = ISNULL((CAST(acc.[Cashable_ePromo_Out] AS DECIMAL(10, 2))), 0) 
	       - ISNULL((CAST(trans.[Cashable_ePromo_Out] AS DECIMAL(10, 2))), 0),
	       [NCashable_ePromo_In] = ISNULL(
	           (acc.[NCashable_ePromo_In] - trans.[NCashable_ePromo_In]),
	           0
	       ),
	       [NCashable_ePromo_Out] = ISNULL(
	           (
	               CAST(acc.[NCashable_ePromo_Out] AS DECIMAL(10, 2)) - CAST(trans.[NCashable_ePromo_Out]AS DECIMAL(10, 2))
	           ),
	           0
	       ),
	       [SortOrder] = 1
	FROM   #info1 acc
	       INNER JOIN #info1 trans
	            ON  acc.installation_id = trans.installation_id
	            AND acc.[Ordering] = 1
	            AND trans.[Ordering] = 2 
	
	UNION
	SELECT installation_id = 0,
	       [Type] = 'Variance',
	       [Ordering] = 6,
	       [WAT_In] = ISNULL((acc.[WAT_In] - trans.[WAT_In]), 0),
	       [WAT_Out] = ISNULL(
	           (
	               CAST(acc.[WAT_Out] AS DECIMAL(10, 2)) - CAST(trans.[WAT_Out] AS DECIMAL(10, 2))
	           ),
	           0
	       ),
	       [Cashable_ePromo_In] = ISNULL((acc.[Cashable_ePromo_In] - trans.[Cashable_ePromo_In]), 0),
	       [Cashable_ePromo_Out] = ISNULL(
	           (
	               CAST(acc.[Cashable_ePromo_Out] AS DECIMAL(10, 2)) - CAST(trans.[Cashable_ePromo_Out]AS DECIMAL(10, 2))
	           ),
	           0
	       ),
	       [NCashable_ePromo_In] = ISNULL(
	           (acc.[NCashable_ePromo_In] - trans.[NCashable_ePromo_In]),
	           0
	       ),
	       [NCashable_ePromo_Out] = ISNULL(
	           (
	               CAST(acc.[NCashable_ePromo_Out]AS DECIMAL(10, 2)) - CAST(trans.[NCashable_ePromo_Out] AS DECIMAL(10, 2))
	           ),
	           0
	       ),
	       [SortOrder] = 2
	FROM   #info1 acc
	       INNER JOIN #info1 trans
	            ON  acc.installation_id = trans.installation_id
	            AND acc.[Ordering] = 4
	            AND trans.[Ordering] = 5 
	
	
	-- return the consolidated values     
	SELECT gmi.Installation_ID,
	       gmi.Bar_Position_ID,
	       gmi.Bar_Position_Name,	-- stand                
	       gmi.Machine_Type_ID,
	       gmi.Machine_Type_Code,
	       gmi.Site_ID,
	       gmi.Site_Name,
	       case when [Ordering] in (4,5,6) then NULL
	       ELSE COALESCE(UPPER(gmi.Zone_name), 'NOT SET') END AS Zone_name,
	       gmi.zone_id,
	       gmi.Sub_Company_ID,
	       gmi.Sub_Company_Name,
	       gmi.Company_ID,
	       gmi.Company_Name,
	       gmi.Machine_Stock_No,	-- slot                
	       [Type],
	       [Ordering],
	       SortOrder,
	       WAT_In = ISNULL(SUM([WAT_In]), 0),
	       WAT_Out = ISNULL(SUM([WAT_Out]), 0),
	       Cashable_ePromo_In = ISNULL(SUM([Cashable_ePromo_In]), 0),
	       Cashable_ePromo_Out = ISNULL(SUM([Cashable_ePromo_Out]), 0),
	       NCashable_ePromo_In = ISNULL(SUM([NCashable_ePromo_In]), 0),
	       NCashable_ePromo_Out = ISNULL(SUM([NCashable_ePromo_Out]), 0)
	FROM   #tmp t
	       LEFT OUTER JOIN vw_genericmachineinformation gmi
	            ON  t.installation_id = ISNULL(gmi.installation_ID, 0)
	GROUP BY
	       gmi.Installation_Id,
	       SortOrder,
	       [Type],
	       [Ordering],
	       gmi.Machine_Stock_No,	-- slot   
	       gmi.Bar_Position_ID,
	       gmi.Bar_Position_Name,	-- stand          
	       gmi.Machine_Type_ID,
	       gmi.Machine_Type_Code,
	       gmi.Site_ID,
	       gmi.Site_Name,
	       gmi.Zone_name,
	       gmi.Zone_id,
	       gmi.Sub_Company_ID,
	       gmi.Sub_Company_Name,
	       gmi.Company_ID,
	       gmi.Company_Name
	ORDER BY
	       SortOrder,
	       Company_ID,
	       sub_company_id,
	       site_id,
	       Zone_name,
	       gmi.Machine_Stock_No,
	       gmi.Bar_Position_name,
	       Ordering 
	       ASC
	
	DROP TABLE #info 
	DROP TABLE #info1 
	DROP TABLE #temp1 
	DROP TABLE #temp2 
	DROP TABLE #temp12 
	DROP TABLE #tmp
END
GO

