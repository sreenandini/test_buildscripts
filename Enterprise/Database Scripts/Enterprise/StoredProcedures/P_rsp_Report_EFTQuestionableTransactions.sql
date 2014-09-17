USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_EFTQuestionableTransactions]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_EFTQuestionableTransactions]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--EXEC  rsp_Report_EFTQuestionableTransactions @company=1,@subcompany=0,@site=0,@startdate='2014-04-18 00:00:01',@enddate='2014-05-19 17:42:22',@SiteIDList='1,4'
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
--
-- EXEC rsp_Report_EFTQuestionableTransactions @startdate='01 Jul 2011', @enddate='01 Nov 2013'
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
--- C.Taylor  ( Contractor )  15/04/10    Created
--------------------------------------------------------------------------- */
CREATE PROCEDURE [dbo].rsp_Report_EFTQuestionableTransactions
	@Company INT = 0,
	@SubCompany INT = 0,
	@Region INT = 0,
	@Area INT = 0,
	@District INT = 0,
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
	       gmi.Installation_Id,
	       gmi.Site_ID,
	       gmi.Site_Name,
	       gmi.Sub_Company_ID,
	       gmi.Sub_Company_Name,
	       gmi.Company_ID,
	       gmi.Company_Name,
	       gmi.Machine_Stock_No,	-- slot 
	       [Trans_Date] = t.AFT_TransactionDate,
	       [Trans_PlayerID] = t.aft_playerid,
	       [Trans_Type] = t.aft_transactiontype,
	       [Trans_ID] = t.aft_TransactionID,
	       [Trans_ErrorCode] = t.aft_error_code,
	       [ErrorDesription] = t.aft_error_message,
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
	                     WHEN t.aft_transactiontype = 'Deposit Complete' THEN 3
	                     WHEN t.aft_transactiontype = 'Deposit Request' THEN 4
	                END
	       
	       INTO #tmp
	FROM   audit.dbo.site_aft_audithistory t
	       JOIN vw_genericmachineinformation gmi
	            ON  t.aft_installationno = gmi.installation_ID
	       INNER JOIN SITE S
	            ON  gmi.Site_id = S.site_id
	       INNER JOIN Sub_Company SC WITH(NOLOCK)
	            ON  SC.Sub_Company_ID = S.Sub_Company_ID
	       INNER JOIN Company C WITH(NOLOCK)
	            ON  SC.Company_ID = C.Company_ID
	WHERE  t.aft_transactiondate >= @startdate
	       AND t.aft_transactiondate <= @enddate
	       AND ISNULL(@Site, S.Site_ID) = S.Site_ID
	       AND (
	               @SiteIDList IS NOT NULL
	               AND t.Site_ID IN (SELECT DATA
	                                 FROM   fnSplit(@SiteIDList, ','))
	           )
	       AND ISNULL(@Company, C.Company_ID) = C.Company_ID
	       AND ISNULL(@SubCompany, S.Sub_Company_ID) = S.Sub_Company_ID
	       AND ISNULL(@Region, S.Sub_Company_Region_ID) = S.Sub_Company_Region_ID
	       AND ISNULL(@Area, S.Sub_Company_Area_ID) = S.Sub_Company_Area_ID
	       AND ISNULL(@District, S.Sub_Company_District_ID) = S.Sub_Company_District_ID
	       AND t.aft_error_code <> 0
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
	            ON  #tmp.[Trans_ID] = t.aft_TransactionID
	            AND t.aft_installationno = #tmp.installation_ID
	WHERE  t.aft_transactiondate >= @startdate
	       AND t.aft_transactiondate <= @enddate
	       AND #tmp.[Type] IN (3, 4)
	       AND t.aft_error_code <> 0  
	
	
	
	SELECT *
	FROM   #tmp
	ORDER BY
	       Trans_Date DESC
END
GO

