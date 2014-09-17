SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_VaultTransactionsDetails]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_VaultTransactionsDetails]
GO

CREATE PROCEDURE rsp_Report_VaultTransactionsDetails
	@Company INT ,
	@SubCompany INT ,
	@Site INT,
	@TransactionType VARCHAR(200),
	@StartDate DATETIME ,
	@EndDate DATETIME ,
	@UserID INT ,
	@SiteIDList VARCHAR(MAX)
AS
BEGIN
	DECLARE @Criteria        VARCHAR(1000),
	        @companyname     VARCHAR(50),
	        @subcompanyname  VARCHAR(50),
	        @sitename        VARCHAR(50),
	        @region          VARCHAR(50),
	        @area            VARCHAR(50),
	        @district        VARCHAR(50)      
	
	
	IF (ISNULL(@company, 0) <> 0)
	    SELECT @companyname = company_name
	    FROM   company
	    WHERE  company_id = @company
	ELSE
	BEGIN
	    SET @company = NULL        
	    SET @companyname = '--Any--'
	END             
	
	
	IF (ISNULL(@subcompany, 0) <> 0)
	    SELECT @subcompanyname = sub_company_name
	    FROM   sub_company
	    WHERE  sub_company_id = @subcompany
	ELSE
	BEGIN
	    SET @subcompany = NULL        
	    SET @subcompanyname = '--Any--'
	END            
	
	
	IF (ISNULL(@site, 0) <> 0)
	    --SELECT @sitename = site_name
	    --FROM   SITE
	    --WHERE  site_id = @site      
	    
	    SELECT @sitename = site_name,
	           @region = SR.Sub_Company_Region_Name,
	           @area = SA.Sub_Company_Area_Name,
	           @district = SD.Sub_Company_District_Name
	    FROM   SITE S WITH (NOLOCK)
	           LEFT JOIN Sub_Company_Region SR WITH (NOLOCK)
	                ON  SR.Sub_Company_ID = S.Sub_Company_Region_ID
	           LEFT JOIN Sub_Company_Area SA WITH (NOLOCK)
	                ON  SA.Sub_Company_Area_ID = S.Sub_Company_Area_ID
	           LEFT JOIN Sub_Company_District SD WITH (NOLOCK)
	                ON  SD.Sub_Company_District_ID = S.Sub_Company_District_ID
	    WHERE  --site_id = @site      
	           @SiteIDList IS NOT NULL
	           AND S.site_id IN (SELECT DATA
	                             FROM   dbo.fnSplit (@SiteIDList, ','))
	ELSE
	BEGIN
	    SET @site = NULL        
	    SET @sitename = '--Any--'      
	    SET @region = '--Any--'      
	    SET @area = '--Any--'      
	    SET @district = '--Any--'
	END            
	
	
	
	SET @criteria = 'Company: ' + CAST(@companyname AS VARCHAR(50)) + ' | ' +
	    'Sub Company: ' + CAST(@subcompanyname AS VARCHAR(50)) + ' | ' +
	    'Site: ' + CAST(@sitename AS VARCHAR(50)) + ' | ' +
	    'Region : ' + CAST(@region AS VARCHAR(50)) + ' | ' +
	    'Area : ' + CAST(@area AS VARCHAR(50)) + ' | ' +
	    'District : ' + CAST(@district AS VARCHAR(50))      
	
	DECLARE @ProductVersion      VARCHAR(50),
	        @currencyformatting  VARCHAR(20)                        
	
	SELECT TOP 1 @ProductVersion = 'BMC Version : ' + VersionName
	FROM   VersionHistory
	ORDER BY
	       VersionDate DESC                        
	
	
	SELECT @currencyFormatting = CASE 
	                                  WHEN setting_value = 'es-AR' THEN 'it-IT'
	                                  ELSE setting_value
	                             END
	FROM   Setting
	WHERE  Setting_Name = 'BMC_Reports_Language'            
	
	
	DECLARE @temp        TABLE 
	        (
	            site_code VARCHAR(10),
	            TypeName VARCHAR(100),
	            VaultName VARCHAR(150),
	            Event_ID VARCHAR(20),
	            Amount VARCHAR(100),
	            Event_Detail VARCHAR(200),
	            Printed_Asset VARCHAR(100),
	            Print_Date DATETIME,
	            CreatedDate DATETIME,
	            Redeem_Date DATETIME,
	            Expired_Date DATETIME,
	            InventoryMethod VARCHAR(10)
	        )    
	
	DECLARE @iEventType  INT     
	
	SELECT @iEventType = CASE 
	                          WHEN @TransactionType = 'EVENT' THEN 0
	                          WHEN @TransactionType = 'VOUCHER' THEN 1
	                          WHEN @TransactionType = 'HANDPAY' THEN 2
	                          WHEN @TransactionType = 'JACKPOT' THEN 3
	                          WHEN @TransactionType = 'MYSTERY' THEN 4
	                          WHEN @TransactionType = 'PROGRESSIVE' THEN 5
	                          ELSE NULL
	                     END  
	
	
	
	IF (ISNULL(@iEventType, 1) <> 0)
	BEGIN
	    INSERT INTO @temp
	    SELECT s.site_code,
	           CASE 
	                WHEN te.TransactionEvent_Type = 1 THEN 'VOUCHER'
	                WHEN te.TransactionEvent_Type = 2 THEN 'HANDPAY'
	                WHEN te.TransactionEvent_Type = 3 THEN 'JACKPOT'
	                WHEN te.TransactionEvent_Type = 4 THEN 'MYSTERY'
	                WHEN te.TransactionEvent_Type = 5 THEN 'PROGRESSIVE'
	           END AS TypeName,
	           tv.Name AS VaultName,
	           '' Event_ID,
	           CAST(te.Amount AS VARCHAR(100)) AS Amount,
	           CASE 
	                WHEN te.TransactionEvent_Type = 2 THEN 'Handpay'
	                WHEN te.TransactionEvent_Type = 3 THEN 'Jackpot'
	                WHEN te.TransactionEvent_Type = 4 THEN 'Mystery'
	                WHEN te.TransactionEvent_Type = 5 THEN 'Progressive'
	                ELSE te.Event_Detail
	           END AS Event_Detail,
	           te.Printed_Asset,
	           te.Print_Date,
	           te.CreatedDate,
	           te.Redeem_Date,
	           CASE 
	                WHEN te.TransactionEvent_Type = 1 THEN te.Expired_Date
	                ELSE NULL
	           END AS Expired_Date,
	           CASE 
	                WHEN COALESCE(drp.IsVaultWebServiceEnabled, tv.IswebserviceEnabled, 0) 
	                     = 0 THEN 'BMC'
	                ELSE 'Vault'
	           END AS InventoryMethod
	    FROM   tVault_TransactionEvents te WITH (NOLOCK)
	           INNER JOIN [Site] s WITH (NOLOCK)
	                ON  s.Site_ID = te.Site_ID
	           INNER JOIN tVault_Devices tv WITH (NOLOCK)
	                ON  tv.Vault_ID = te.Vault_Id
	           LEFT OUTER JOIN tVault_drops drp WITH(NOLOCK)
	                ON  drp.Site_Drop_Ref = te.Site_Drop_ref
	                AND drp.Site_ID = s.Site_ID
	           INNER JOIN Sub_Company SC WITH (NOLOCK)
	                ON  SC.Sub_Company_ID = s.Sub_Company_ID
	           INNER JOIN Company C WITH (NOLOCK)
	                ON  C.Company_ID = sc.Company_ID
	    WHERE  (
	               @iEventType IS NULL
	               OR te.TransactionEvent_Type = ISNULL(@iEventType, te.TransactionEvent_Type)
	           )
	           AND te.CreatedDate BETWEEN @startdate AND @enddate
	           AND ISNULL(@Company, C.Company_Id) = C.Company_Id
	           AND ISNULL(@Subcompany, S.Sub_Company_Id) = S.Sub_Company_Id 
	               --AND ISNULL(@Site, S.Site_Id) = S.Site_Id
	           AND (
	                   @SiteIDList IS NOT NULL
	                   AND S.site_id IN (SELECT DATA
	                                     FROM   dbo.fnSplit (@SiteIDList, ','))
	               )
	END
	
	IF (ISNULL(@iEventType, 0) = 0)
	BEGIN
	    INSERT INTO @temp
	    SELECT s.site_code,
	           'EVENT' TypeName,
	           tv.Name AS VaultName,
	           EventID,
	           '-' Amount,
	           EventDescription Event_Detail,
	           tv.Name Printed_Asset,
	           EventDateTime Print_Date,
	           CreateDate,
	           NULL Redeem_Date,
	           NULL Expired_Date,
	           'Vault' AS InventoryMethod
	    FROM   VaultEvents ve WITH (NOLOCK)
	           INNER JOIN [Site] s WITH (NOLOCK)
	                ON  s.Site_ID = ve.SiteID
	           INNER JOIN tVault_Devices tv WITH (NOLOCK)
	                ON  tv.Vault_ID = ve.Vault_Id
	           INNER JOIN Sub_Company SC WITH (NOLOCK)
	                ON  SC.Sub_Company_ID = s.Sub_Company_ID
	           INNER JOIN Company C WITH (NOLOCK)
	                ON  C.Company_ID = sc.Company_ID
	    WHERE  ve.CreateDate BETWEEN @startdate AND @enddate
	           AND ISNULL(@Company, C.Company_Id) = C.Company_Id
	           AND ISNULL(@Subcompany, S.Sub_Company_Id) = S.Sub_Company_Id 
	               --AND ISNULL(@Site, S.Site_Id) = S.Site_Id
	           AND (
	                   @SiteIDList IS NOT NULL
	                   AND S.site_id IN (SELECT DATA
	                                     FROM   dbo.fnSplit (@SiteIDList, ','))
	               )
	END    
	
	SELECT *
	FROM   @temp
	ORDER BY
	       CreatedDate
END 

---Exec rsp_Report_VaultTransactionsDetails 0,0,0,'--All--','2013-10-27 00:00:09.200','2013-11-20 00:00:09.200',0      
