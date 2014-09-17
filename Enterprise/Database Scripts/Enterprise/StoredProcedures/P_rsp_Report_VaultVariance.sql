USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_Report_VaultVariance'
   )
    DROP PROCEDURE dbo.rsp_Report_VaultVariance
GO  

CREATE PROCEDURE dbo.rsp_Report_VaultVariance
	@Company INT,
	@SubCompany INT,
	@Site INT,
	@VaultStatus VARCHAR(20),
	@IncludeZero BIT,
	@StartDate DATETIME,
	@EndDate DATETIME,
	@UserID INT,
	@SiteIDList VARCHAR(MAX)
AS
	/*****************************************************************************************************  
DESCRIPTION : PROC Description    
CREATED DATE: PROC CreateDate  
MODULE  : Enterprise Report   
CHANGE HISTORY :  
------------------------------------------------------------------------------------------------------  
AUTHOR     DESCRIPTON          MODIFIED DATE  
------------------------------------------------------------------------------------------------------  

*****************************************************************************************************/  

BEGIN
	IF ISNULL(@Company, 0) = 0
	    SET @Company = NULL  
	
	IF ISNULL(@Subcompany, 0) = 0
	    SET @Subcompany = NULL  
	
	IF ISNULL(@Site, 0) = 0
	    SET @Site = NULL 
	
	IF @VaultStatus = '--All--' OR @VaultStatus='All'
	    SET @VaultStatus = NULL
	
	DECLARE @temp TABLE 
	        (
	            Vault_ID INT,
	            Manufacturer_ID INT,
	            NAME VARCHAR(150),
	            [Status] VARCHAR(50),
	            IsWebServiceEnabled BIT
	        )
	
	INSERT INTO @temp
	SELECT *
	FROM   (
	           SELECT tv.Vault_ID,
	                  tv.Manufacturer_ID,
	                  tv.Name,
	                  CASE 
	                       WHEN tv.SoldDate IS NOT NULL THEN 'Sold'
	                       WHEN tv.End_Date IS NOT NULL THEN 'Terminated'
	                       WHEN ti.Assigned_To_Site IS NOT NULL
	           AND START_DATE IS NULL THEN 'Assigned To Site' 
	               WHEN ti.Assigned_To_Site IS NOT NULL
	           AND START_DATE IS NOT NULL THEN 'Active' 
	               WHEN(ISNULL(ACTIVE, 0)) <= 0 THEN 'Inactive' 
	               ELSE 'Active' 
	               END AS [Status],
	           tv.IswebserviceEnabled
	           FROM tVault_Devices tv
	           LEFT OUTER JOIN TNGAInstallations TI 
	           ON tv.NGADevice_ID = Ti.NGADevice_ID
	           AND Ti.End_Date IS NULL
	       ) A
	WHERE  A.[Status] = ISNULL(@VaultStatus, A.[Status]) 
	
	IF ISNULL(@IncludeZero, 1) = 1
	BEGIN
	    SELECT tv.Name VaultName,
	           drp.Site_ID,
	           drp.Drop_ID,
	           drp.DropCompleteDate,
	           drp.Meter_Balance,
	           drp.Vault_Balance,
	           drp.Declared_Balance,
	           drp.Declared_Balance -drp.Meter_Balance BMCVariance,
	           drp.Declared_Balance -drp.Vault_Balance VaultVariance,
	           drp.ModifiedDate,
	           sf.Staff_First_Name + ', ' + sf.Staff_Last_Name [Name],
	           tv.[Status] AS VaultStatus,
	           CASE 
	                WHEN COALESCE(DRP.IsVaultWebServiceEnabled, tv.IswebserviceEnabled, 0)
	                     = 0 THEN 'BMC'
	                ELSE 'Vault'
	           END AS InventoryMethod
	    FROM   tVault_drops drp WITH(NOLOCK)
	           INNER JOIN Staff sf
	                ON  sf.UserTableID = drp.ModifiedUser
	           INNER JOIN @temp tv
	                ON  drp.Device_ID = tv.Vault_ID
	           INNER JOIN Manufacturer m
	                ON  m.Manufacturer_ID = tv.Manufacturer_ID
	           INNER JOIN SITE s WITH(NOLOCK)
	                ON  s.Site_ID = drp.Site_ID
	           INNER JOIN Sub_Company sc WITH(NOLOCK)
	                ON  sc.Sub_Company_ID = s.Sub_Company_ID
	           INNER JOIN Company c WITH(NOLOCK)
	                ON  c.Company_ID = sc.Company_ID
	    WHERE  drp.IsDeclared = 1
	           --AND drp.Site_ID = ISNULL(@Site, drp.Site_ID)
	           AND (
	                   @SiteIDList IS NOT NULL
	                   AND drp.site_id IN (SELECT DATA
	                                       FROM   dbo.fnSplit (@SiteIDList, ','))
	               )
	           AND sc.Sub_Company_ID = ISNULL(@Subcompany, sc.Sub_Company_ID)
	           AND c.Company_ID = ISNULL(@Company, c.Company_ID)
	           AND drp.ModifiedDate BETWEEN @Startdate AND @Enddate
	    ORDER BY
	           drp.Drop_ID
	END
	ELSE
	BEGIN
	    SELECT *
	    FROM   (
	               SELECT tv.Name VaultName,
	                      drp.Site_ID,
	                      drp.Drop_ID,
	                      drp.DropCompleteDate,
	                      drp.Meter_Balance,
	                      drp.Vault_Balance,
	                      drp.Declared_Balance,
	                      drp.Declared_Balance -drp.Meter_Balance BMCVariance,
	                      drp.Declared_Balance -drp.Vault_Balance VaultVariance,
	                      drp.ModifiedDate,
	                      sf.Staff_First_Name + ', ' + sf.Staff_Last_Name [Name],
	                      tv.[Status] AS VaultStatus,
	                      CASE 
	                           WHEN COALESCE(DRP.IsVaultWebServiceEnabled, tv.IswebserviceEnabled, 0) 
	                                = 0 THEN 'BMC'
	                           ELSE 'Vault'
	                      END AS InventoryMethod
	               FROM   tVault_drops drp WITH(NOLOCK)
	                      INNER JOIN Staff sf
	                           ON  sf.UserTableID = drp.ModifiedUser
	                      INNER JOIN @temp tv
	                           ON  drp.Device_ID = tv.Vault_ID
	                      INNER JOIN Manufacturer m
	                           ON  m.Manufacturer_ID = tv.Manufacturer_ID
	                      INNER JOIN SITE s WITH(NOLOCK)
	                           ON  s.Site_ID = drp.Site_ID
	                      INNER JOIN Sub_Company sc WITH(NOLOCK)
	                           ON  sc.Sub_Company_ID = s.Sub_Company_ID
	                      INNER JOIN Company c WITH(NOLOCK)
	                           ON  c.Company_ID = sc.Company_ID
	               WHERE  drp.IsDeclared = 1
	                      --AND drp.Site_ID = ISNULL(@Site, drp.Site_ID)
	                      AND (
	                              @SiteIDList IS NOT NULL
	                              AND drp.site_id IN (SELECT DATA
	                                                  FROM   dbo.fnSplit (@SiteIDList, ','))
	                          )
	                      AND sc.Sub_Company_ID = ISNULL(@Subcompany, sc.Sub_Company_ID)
	                      AND c.Company_ID = ISNULL(@Company, c.Company_ID)
	                      AND drp.ModifiedDate BETWEEN @Startdate AND @Enddate
	           ) X
	    WHERE  (
	               (X.InventoryMethod = 'BMC' AND BMCVariance <> 0)
	               OR (X.InventoryMethod = 'Vault' AND VaultVariance <> 0)
	           )
	    ORDER BY
	           X.Drop_ID
	END
END
GO

--exec rsp_Report_VaultVariance @Company=0,@Subcompany=0,@Site=0,@VaultStatus='--All--',@IncludeZero=0,@Startdate='2013-11-17 00:00:01',@Enddate='2013-11-22 23:29:57',@UserID=1


