
USE [Enterprise]
GO

/*

exec rsp_REPORT_AccountAvgDailyWin 
@company=4,
@subcompany=0,
@region=0,
@area=0,
@district=0,
@site=0,
@category=0,
@startdate=N'2012-12-09 15:18:58',
@enddate=N'2012-12-15 15:18:58'


* /
*/
/****** Object:  StoredProcedure [dbo].[rsp_REPORT_AccountAvgDailyWin]    Script Date: 07/23/2013 15:36:09 ******/
IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_REPORT_AccountAvgDailyWin]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_REPORT_AccountAvgDailyWin]
GO

USE [Enterprise]
GO

/****** Object:  StoredProcedure [dbo].[rsp_REPORT_AccountAvgDailyWin]    Script Date: 07/23/2013 15:36:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[rsp_REPORT_AccountAvgDailyWin]
	@Company INT = 0,
	@SubCompany INT = 0,
	@Region INT = 0,
	@Area INT = 0,
	@District INT = 0,
	@Site INT = 0,
	@Category INT = 0,
	@StartDate DATETIME,
	@EndDate DATETIME,
	@SiteIDList VARCHAR(MAX)
AS
	DECLARE @calcStartDate  DATETIME,
	        @calcEndDate    DATETIME,
	        @NoOfGamingDays INT,
			@StartHour		INT
	DECLARE @IsAFTIncludedInCalculation BIT  		
	
	SET DATEFORMAT dmy 
	--SET @calcStartDate=Cast(@startdate as DateTime)
	--SET @calcEndDate=Cast(@enddate as DateTime)    
	SELECT @IsAFTIncludedInCalculation = CASE   
                                           WHEN Setting_value = 'True' THEN 1  
                                           ELSE 0  
                                      END  
	FROM   Setting  
	WHERE  Setting_Name = 'IsAFTIncludedInCalculation'   
	
	SET @calcStartDate = CAST(CONVERT(VARCHAR(12), @startdate, 106) AS DATETIME)  
	SET @calcEndDate = CAST(CONVERT(VARCHAR(12), @enddate, 106) AS DATETIME)
	
	SELECT @StartHour=Setting_Value FROM Setting WHERE Setting_Name='GAMING_DAY_START_HOUR'  
	
	SET @NoOfGamingDays=DATEDIFF(d,DATEADD(hh, ISNULL(@StartHour,6), DATEADD(d, 0, DATEDIFF(d, 0, @calcStartDate))),DATEADD(hh, ISNULL(@StartHour,6), DATEADD(d, 0, DATEDIFF(d, 0, @calcEndDate))))
	
	IF(ISNULL(@NoOfGamingDays,0)=0)
	BEGIN
		SET @NoOfGamingDays=1
	END
	
	IF @company = 0
	    SET @company = NULL
	
	IF @subcompany = 0
	    SET @subcompany = NULL
	
	IF @region = 0
	    SET @region = NULL
	
	IF @area = 0
	    SET @area = NULL
	
	IF @district = 0
	    SET @district = NULL
	
	IF @site = 0
	    SET @site = NULL
	
	IF @category = 0
	    SET @category = NULL  
	
	
	DECLARE @NOTSET VARCHAR(20)    
	
	SET @NOTSET = 'UN-DEFINED'  
	
	SELECT company.company_id,
	       Installation.Bar_Position_Id,
	       SITE.sub_company_id,
	       SITE.sub_company_region_id,
	       SITE.sub_company_area_id,
	       SITE.sub_company_district_id,
	       SITE.site_id,
	       SITE.site_name,
	       machine_type_id = CASE 
	                              WHEN cat.machine_type_id IS NOT NULL THEN cat.machine_type_id
	                              ELSE 0
	                         END,
	       machine_type_code = CASE 
	                                WHEN cat.machine_type_code IS NOT NULL THEN 
	                                     cat.machine_type_code
	                                ELSE @NOTSET
	                           END,
	       machine_type_description = CASE 
	                                       WHEN cat.machine_type_description IS 
	                                            NOT NULL THEN cat.machine_type_description
	                                       ELSE @NOTSET
	                                  END,
	       MACHINE.machine_stock_no,
	       CASE Machine_Class.Machine_Name
	            WHEN 'Auto Detected'  THEN 
	            ISNULL(MGMP.Multigamename,'Multi Game')
	            WHEN 'Multi Game'  THEN
	            ISNULL(MGMP.Multigamename,'Multi Game')
                    ELSE 
                     Machine_Class.Machine_Name
	       END AS machinename,
	       (Bar_Position.Bar_Position_Name) AS PosName,
	       (Collection_Calcs.Collection_VTP) / 10 AS Handle,
			Collection_Declared_Notes +(CAST(ISNULL(COLLECTION.DeclaredTicketValue, 0) AS FLOAT))      
			 -(
			      (
			          CAST(ISNULL(COLLECTION.DeclaredTicketPrintedValue, 0) AS FLOAT)
			      ) 
			      + COALESCE(
			          (
			              SELECT CAST(SUM(Treasury_Amount)AS FLOAT)
			              FROM   dbo.Treasury_Entry (NOLOCK)
			              WHERE  Treasury_Entry.Collection_id = dbo.Collection.Collection_id
			                     AND Treasury_Type IN ('Shortpay', 'Offline Voucher-Shortpay')
			          ),
			          0
			      ) 
			      - COALESCE(
			          (
			              SELECT CAST(SUM(Treasury_Amount)AS FLOAT)
			              FROM   dbo.Treasury_Entry (NOLOCK)
			              WHERE  Treasury_Entry.Collection_id = dbo.Collection.Collection_id
			                     AND Treasury_Type = 'VOID'
			          ),
			          0
			      )
			  )  
			 -(dbo.GetAttendantPay(COLLECTION.Collection_id))
			 +(
			      (Collection_Calcs.Collection_Net_Coin) 
			      - CAST(
			          COALESCE(
			              (
			                  SELECT SUM(Treasury_Amount)
			                  FROM   Treasury_Entry(NOLOCK)
			                  WHERE  Treasury_Entry.Collection_ID = dbo.Collection_Calcs.Collection_ID
			                         AND Treasury_Type = 'Refund'
			              ),
			              0
			          ) AS REAL
			      )
			  )           
			  +(
			       CASE 
			            WHEN @IsAFTIncludedInCalculation = 1 THEN (dbo.GetEftIn(COLLECTION.collection_id) / 100.00) 
			                 -(dbo.GetEftOut(COLLECTION.collection_id) / 100.00)
			            ELSE 0
			       END
			   ) 
			AS [Cash_Take],
	       CASE 
	            WHEN (Collection_Details.collection_days = 0) THEN 0
	            ELSE Collection_Details.collection_days
	       END AS Collection_Days,
	       installation.machine_id,
	       Installation.Installation_Percentage_Payout,
	       Installation.Installation_Price_Per_Play,
	       @NoOfGamingDays AS NoOfGamingDays
	FROM   dbo.Collection (NOLOCK)
	       INNER JOIN dbo.Collection_Calcs (NOLOCK)
	            ON  dbo.Collection_Calcs.Collection_ID = dbo.Collection.Collection_ID
	       INNER JOIN dbo.Collection_Details (NOLOCK)
	            ON  dbo.Collection.Collection_ID = dbo.Collection_Details.Collection_ID
	       INNER JOIN dbo.Batch (NOLOCK)
	            ON  dbo.Collection.Batch_ID = dbo.Batch.Batch_ID
	       INNER JOIN dbo.Installation (NOLOCK)
	            ON  dbo.Collection.Installation_ID = dbo.Installation.Installation_ID
	       INNER JOIN dbo.Machine (NOLOCK)
	            ON  dbo.Installation.Machine_ID = dbo.Machine.Machine_ID
	       LEFT JOIN machine_type cat
	            ON  cat.machine_type_id = MACHINE.machine_category_id
	       INNER JOIN dbo.Machine_Class (NOLOCK)
	            ON  dbo.Machine.Machine_Class_ID = dbo.Machine_Class.Machine_Class_ID
               LEFT JOIN MultiGameMapping MGMP
	          ON MGMP.Machineid=MACHINE.Machine_ID
	       INNER JOIN dbo.Bar_Position (NOLOCK)
	            ON  dbo.Installation.Bar_Position_ID = dbo.Bar_Position.Bar_Position_ID
	       INNER JOIN dbo.Site (NOLOCK)
	            ON  dbo.Bar_Position.Site_ID = dbo.Site.Site_ID
	       JOIN sub_company
	            ON  SITE.sub_company_id = sub_company.sub_company_id
	       JOIN company
	            ON  sub_company.company_id = company.company_id
	WHERE  CAST(collection_date AS DATETIME) BETWEEN @calcStartDate AND @calcEndDate
	       AND (
	               (@company IS NULL)
	               OR (@company IS NOT NULL AND company.company_id = @company)
	           )
	       AND (
	               (@subcompany IS NULL)
	               OR (
	                      @subcompany IS NOT NULL
	                      AND SITE.sub_company_id = @subcompany
	                  )
	           )
	       AND (
	               (@region IS NULL)
	               OR (
	                      @region IS NOT NULL
	                      AND SITE.sub_company_region_id = @region
	                  )
	           )
	       AND (
	               (@area IS NULL)
	               OR (@area IS NOT NULL AND SITE.sub_company_area_id = @area)
	           )
	       AND (
	               (@district IS NULL)
	               OR (
	                      @district IS NOT NULL
	                      AND SITE.sub_company_district_id = @district
	                  )
	           )
	       AND (
	               @SiteIDList IS NOT NULL
	               AND SITE.site_id IN (SELECT DATA
	                                    FROM   dbo.fnSplit (@SiteIDList, ','))
	           )
	       AND (
	               (@category IS NULL)
	               OR (@category IS NOT NULL AND cat.machine_type_id = @category)
	           )
	ORDER BY
	       company.company_id,
	       SITE.sub_company_id,
	       SITE.sub_company_region_id,
	       SITE.sub_company_area_id,
	       SITE.sub_company_district_id,
	       SITE.site_id,
	       SITE.site_name,
	       cat.machine_type_id,
	       Bar_Position.Bar_Position_Name,
	       installation.installation_start_date DESC
GO


