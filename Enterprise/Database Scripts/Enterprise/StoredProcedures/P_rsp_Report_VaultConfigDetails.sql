/************************************************************
 * Code formatted by SoftTree SQL Assistant © v6.3.171
 * Time: 10/24/2013 8:40:55 PM
 ************************************************************/

USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA       = N'dbo'
              AND SPECIFIC_NAME     = N'rsp_Report_VaultConfigDetails'
   )
    DROP PROCEDURE dbo.rsp_Report_VaultConfigDetails
GO      
   
CREATE PROCEDURE rsp_Report_VaultConfigDetails
	@Company INT ,
	@SubCompany INT ,
	@Site INT,
	@UserID INT,
	@VaultStatus VARCHAR(20)
AS
/*===============================================================================
	VaultStatus:
	============
		--Active 
		--Inactive 
		--AssignedToSite
		--Terminated 
		--Sold 
===============================================================================*/
BEGIN
	
	DECLARE @Criteria           VARCHAR(1000),
	        @companyname        VARCHAR(50),
	        @subcompanyname     VARCHAR(50),
	        @sitename           VARCHAR(50),
	        @region             VARCHAR(50),
	        @area               VARCHAR(50),
	        @district           VARCHAR(50)
	
	
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
	
	IF @VaultStatus = '--All--' OR @VaultStatus='All'
	    SET @VaultStatus = NULL
	
	IF (ISNULL(@Site, 0) <> 0)
	    --SELECT @sitename = site_name
	    --FROM   SITE
	    --WHERE  site_id = @site    
	    
	    SELECT @sitename = site_name,
	           @region       = SR.Sub_Company_Region_Name,
	           @area         = SA.Sub_Company_Area_Name,
	           @district     = SD.Sub_Company_District_Name
	    FROM   SITE S WITH (NOLOCK)
	           LEFT JOIN Sub_Company_Region SR WITH (NOLOCK)
	                ON  SR.Sub_Company_ID = S.Sub_Company_Region_ID
	           LEFT JOIN Sub_Company_Area SA WITH (NOLOCK)
	                ON  SA.Sub_Company_Area_ID = S.Sub_Company_Area_ID
	           LEFT JOIN Sub_Company_District SD WITH (NOLOCK)
	                ON  SD.Sub_Company_District_ID = S.Sub_Company_District_ID
	    WHERE  site_id       = @Site
	ELSE
	BEGIN
	    SET @Site = NULL      
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
	
	DECLARE @ProductVersion         VARCHAR(50),
	        @currencyformatting     VARCHAR(20)                      
	
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
	
	
	
	SELECT * FROM 
	
	(SELECT                           Vault_ID,
	       NAME                          AS VaultName,
	       Serial_NO,
	       CASE WHEN VD.SoldDate IS NOT NULL THEN 'Sold'
				WHEN VD.End_Date IS NOT NULL THEN 'Terminated'
				WHEN TI.Assigned_To_Site IS NOT NULL AND TI.[Start_Date] IS NULL THEN 'Assigned To Site'
				WHEN TI.Assigned_To_Site IS NOT NULL AND TI.[Start_Date] IS NOT NULL  THEN 'Active'				 
	            WHEN (ISNULL(ACTIVE, 0)) <= 0 THEN 'Inactive'
	            ELSE 'Active'
	       END                           AS [Status],
	       s.Site_ID,
	       Created_Date,
	       VD.End_Date,
	       M.Manufacturer_Name           AS Manufacturer,
	       Type_Prefix                   AS [Type],
	       Capacity,
	       Alert_Level,
	       ISNULL(s.Site_Code, '')       AS Site_Code,
	       CASE 
	            WHEN (ISNULL(IsWebServiceEnabled, 0)) = 0 THEN 'BMC'
	            ELSE 'Vault'
	       END                           AS InventoryCalcuationMethod,
	       @ProductVersion               AS ProductVersion,
	       @Criteria                     AS Criteria,
	       ISNULL(Area.Sub_Company_Area_Name, ' - ') AS DropArea,
	       ISNULL(vd.NoofCassettes, 0)   AS Cassettes,
	       ISNULL(vd.NoofCoinHopper, 0)  AS CoinHoppers
	FROM   tVault_Devices vd
			LEFT OUTER JOIN TNGAInstallations TI 
				On  vd.NGADevice_ID = Ti.NGADevice_ID AND  TI.End_Date IS NULL 
			LEFT OUTER JOIN [Site] s
	            ON  TI.Site_ID = s.Site_ID
			INNER JOIN Manufacturer M WITH (NOLOCK)
	            ON  m.Manufacturer_ID = vd.Manufacturer_ID
			LEFT OUTER JOIN Sub_Company_Area Area WITH (NOLOCK)
	            ON  Area.Sub_Company_Area_ID = S.Sub_Company_Area_ID
			LEFT OUTER JOIN Sub_Company SC
	            ON  SC.Sub_Company_ID = s.Sub_Company_ID
			LEFT OUTER JOIN Company C
	            ON  C.Company_ID = sc.Company_ID
	WHERE  (@Company IS NULL OR @Company = C.Company_Id)
			AND (@Subcompany IS NULL OR @Subcompany = S.Sub_Company_Id)
			AND (@Site IS NULL OR @Site = S.Site_Id)	       
	 ) A
	 Where [Status] = ISNUll(@VaultStatus,[Status])
	 ORDER BY
	      Site_Code,
	      Vault_ID      
	       
END
GO

--Exec rsp_Report_VaultConfigDetails 0,0,0,0,'--All--'    