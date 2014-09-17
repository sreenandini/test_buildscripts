USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_EFTSlotActivity]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_EFTSlotActivity]
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
---
--- Inputs:      see inputs
---
--- Outputs:     (0)   - no error .. 
---              OTHER - SQL error 
---
-------------------------------------------------------------------------- 
-- to USE
--select * from site_aft_audithistory
-- exec rsp_Report_EFTSlotActivity @company=0,@subcompany=0,@site=0,@startdate='2013-10-28 13:27:06',@enddate='2013-10-29 17:43:45'
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
--- M.Durga Devi - 20th Aug 2012 - Modified - Date filter issue
--------------------------------------------------------------------------- */
CREATE PROCEDURE [dbo].rsp_Report_EFTSlotActivity
	@Company INT = 0,
	@SubCompany INT = 0,
	@Region INT = 0,
	@Area INT = 0,
	@District INT =0,
	@Site INT = 0,
	@StartDate DATETIME ,
	@EndDate DATETIME,
	@SiteIDList VARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON 	
	
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
	
	
	-- get the raw information, and append site, company etc information to it.        
	SELECT gmi.Bar_Position_ID,
	       gmi.Bar_Position_Name,	-- stand        
	       gmi.Installation_ID,
	       gmi.Site_ID,
	       gmi.Site_Name,
	       gmi.Sub_Company_ID,
	       gmi.Sub_Company_Name,
	       gmi.Company_ID,
	       gmi.Company_Name,
	       gmi.Machine_Stock_No,	-- slot 
	       [Trans_Date] = CONVERT(VARCHAR(20), t.aft_transactiondate, 105),
	       [Trans_Time] = CONVERT(VARCHAR(20), t.aft_transactiondate, 108),
	       [Trans_PlayerID] = t.aft_playerid,
	       [Trans_Type] = t.aft_transactiontype,
	       [Trans_ID] = t.aft_transactionID,
	       [Trans_ErrorCode] = t.aft_error_code,
	       -- withdrawals          
	       [WAT_In] = CAST(WATAmt / 100 AS DECIMAL(10, 2)),
	       [Cashable_ePromo_In] = CAST(CashableAmt / 100 AS DECIMAL(10, 2)),
	       [NCashable_ePromo_In] = CAST(NonCashableAmt / 100 AS DECIMAL(10, 2)),
	       -- deposits            
	       [WAT_Out] = CAST(0 AS DECIMAL(10, 2)),
	       [Cashable_ePromo_Out] = CAST(0 AS DECIMAL(10, 2)),
	       [NCashable_ePromo_Out] = CAST(0 AS DECIMAL(10, 2)),
	       [Type] = CASE 
	                     WHEN t.aft_transactiontype = 'WithDrawal Complete' THEN 
	                          2
	                     WHEN t.aft_transactiontype = 'WithDrawal Request' THEN 
	                          1
	                     WHEN t.aft_transactiontype = 'Deposit Complete' THEN 4
	                     WHEN t.aft_transactiontype = 'Deposit Request' THEN 3
	                     ELSE 0
	                END 
	       
	       INTO #tmp
	FROM   audit.dbo.site_aft_audithistory t
	       JOIN vw_genericmachineinformation gmi
	            ON  t.aft_installationno = gmi.installation_ID
	       INNER JOIN SITE S
	            ON  S.Site_ID = gmi.Site_ID
	       INNER JOIN Sub_Company SC WITH(NOLOCK)
	            ON  SC.sub_company_id = S.Sub_Company_ID
	       INNER JOIN Company C WITH(NOLOCK)
	            ON  SC.company_id = C.company_id
	WHERE  t.aft_transactiondate BETWEEN @startdate AND @enddate
	       AND ISNULL(@Site, S.Site_id) = S.Site_ID
	       AND (
	               @SiteIDList IS NOT NULL
	               AND t.Site_ID IN (SELECT DATA
	                                 FROM   fnSplit(@SiteIDList, ','))
	           )
	       AND ISNULL(@Company, C.Company_Id) = C.Company_Id
	       AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	       AND ISNULL(@Region, S.Sub_Company_Region_ID) = S.Sub_Company_Region_ID
	       AND ISNULL(@Area, S.Sub_Company_Area_ID) = S.Sub_Company_Area_ID
	       AND ISNULL(@District, S.Sub_Company_District_ID) = S.Sub_Company_District_ID
	       AND t.aft_error_code = 0
	ORDER BY
	       Company_ID,
	       sub_company_id,
	       site_id,
	       gmi.Bar_Position_name,
	       t.AFT_TransactionDate      
	
	
	UPDATE #tmp
	SET    #tmp.[WAT_Out] = CAST(t.WATAmt / 100 AS DECIMAL(10, 2)),
	       #tmp.[Cashable_ePromo_Out] = CAST(t.CashableAmt / 100 AS DECIMAL(10, 2)),
	       #tmp.[NCashable_ePromo_Out] = CAST(t.NonCashableAmt / 100 AS DECIMAL(10, 2)),
	       [WAT_In] = CAST(0 AS DECIMAL(10, 2)),
	       [Cashable_ePromo_In] = CAST(0 AS DECIMAL(10, 2)),
	       [NCashable_ePromo_In] = CAST(0 AS DECIMAL(10, 2))
	FROM   #tmp
	       JOIN audit.dbo.site_aft_audithistory t
	            ON  #tmp.[Trans_ID] = t.aft_transactionID
	            AND t.aft_installationno = #tmp.installation_ID
	WHERE  t.aft_transactiondate BETWEEN @startdate AND @enddate
	       AND #tmp.[Type] IN (3, 4)
	       AND t.aft_error_code = 0    
	
	SELECT *
	FROM   #tmp
	       END
	GO

