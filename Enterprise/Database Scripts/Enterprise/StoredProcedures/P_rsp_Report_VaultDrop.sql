USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_Report_VaultDrop'
   )
    DROP PROCEDURE dbo.rsp_Report_VaultDrop
GO

CREATE PROCEDURE dbo.rsp_Report_VaultDrop
	@Company INT,
	@SubCompany INT,
	@Site INT,
	@VaultStatus VARCHAR(20) = NULL,
	@IncludeZero BIT,
	@UserID INT,
	@StartDate DATETIME,
	@EndDate DATETIME,
	@SiteIDList VARCHAR(MAX)
AS
	/*****************************************************************************************************
DESCRIPTION : PROC Description  
CREATED DATE: PROC CreateDate
MODULE            : Enterprise Report     
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR                              DESCRIPTON                                                        MODIFIED DATE
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
	            Serial_NO VARCHAR(150),
	            Type_Prefix VARCHAR(10),
	            Capacity DECIMAL,
	            Alert_Level INT,
	            [Status] VARCHAR(50),
	            IsWebServiceEnabled BIT,
	            IsAutoAdjust VARCHAR(2) --For AutoAdjust
	        )
	
	
	INSERT INTO @temp
	SELECT *
	FROM   (
	           SELECT tv.Vault_ID,
	                  tv.Manufacturer_ID,
	                  tv.Name,
	                  tv.Serial_NO,
	                  tv.Type_Prefix,
	                  tv.Capacity,
	                  tv.Alert_Level,
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
	           tv.IswebserviceEnabled,
	           ' ' AS IsAutoAdjust --For AutoAdjust
	           FROM tVault_Devices tv
	           LEFT OUTER JOIN TNGAInstallations TI 
	           ON tv.NGADevice_ID = Ti.NGADevice_ID
	           AND Ti.End_Date IS NULL
	       ) A
	WHERE  A.[Status] = ISNULL(@VaultStatus, A.[Status]) 
	
	IF ISNULL(@IncludeZero, 1) = 1
	BEGIN
	    SELECT *,
	           (DropAmount) DropBalance
	    FROM   (
	               SELECT tv.Name VaultName,
	                      drp.Drop_ID AS Serial_NO,
	                      m.Manufacturer_Name,
	                      tv.Type_Prefix TYPE,
	                      tv.Capacity,
	                      tv.Alert_Level,
	                      s.Site_Code,
	                      drp.DropCompleteDate,
	                      drp.FillAmount,
	                      drp.BleedAmount,
	                      drp.AdjustmentAmount,
	                      CASE --WHEN tv.IsWebServiceEnabled = 0 THEN ISNULL(drp.Meter_Voucher, 0.00)  ****** CHANGED
	                           WHEN COALESCE(drp.IsVaultWebServiceEnabled, tv.IswebserviceEnabled, 0) 
	                                = 0 THEN ISNULL(drp.Meter_Voucher, 0.00)
	                           ELSE ISNULL(
	                                    (
	                                        SELECT SUM(te.AMOUNT)
	                                        FROM   tVault_TransactionEvents te
	                                        WHERE  te.Site_Drop_ref = drp.Site_Drop_Ref
	                                               AND te.Site_Id = drp.Site_ID
	                                               AND te.TransactionEvent_Type = 
	                                                   1 --VOUCHER
	                                    ),
	                                    0.00
	                                )
	                      END AS voucher_Out,
	                      CASE --WHEN tv.IsWebServiceEnabled = 0 THEN ISNULL(drp.Meter_Handpay, 0.00) ****** CHANGED
	                           WHEN COALESCE(drp.IsVaultWebServiceEnabled, tv.IswebserviceEnabled, 0) 
	                                = 0 THEN ISNULL(drp.Meter_Handpay, 0.00) 
	                                +
	                                ISNULL(drp.Meter_jackpot, 0.00)
	                           ELSE ISNULL(
	                                    (
	                                        SELECT SUM(te.AMOUNT)
	                                        FROM   tVault_TransactionEvents te
	                                        WHERE  te.Site_Drop_ref = drp.Site_Drop_Ref
	                                               AND te.Site_Id = drp.Site_ID
	                                               AND te.TransactionEvent_Type 
	                                                   <> 1 --HANDPAY/JACKPOT
	                                    ),
	                                    0.00
	                                )
	                      END AS HandPay_Out,
	                      tv.[Status] AS VaultStatus,
	                      CASE --WHEN tv.IsWebServiceEnabled = 0 THEN 'BMC' ****** CHANGED
	                           WHEN COALESCE(drp.IsVaultWebServiceEnabled, tv.IswebserviceEnabled, 0)
	                                = 0 THEN 'BMC'
	                           ELSE 'Vault'
	                      END AS InventoryMethod,
	                      ISNULL(sca.Sub_Company_Area_Name, '-') 
	                      Sub_Company_Area_Name,
	                      sf.Staff_First_Name + ', ' + sf.Staff_Last_Name [Name],
	                      drp.Site_Drop_Ref,
	                      CASE 
	                           WHEN COALESCE(drp.IsVaultWebServiceEnabled, tv.IswebserviceEnabled, 0) 
	                                = 0 THEN drp.meter_balance
	                           ELSE drp.vault_balance
	                      END AS DropAmount,
	                      '' AS AutoAdjust
	               FROM   tVault_drops drp WITH(NOLOCK)
	                      INNER JOIN Staff sf
	                           ON  sf.UserTableID = drp.DropCompleteUser
	                      INNER JOIN @temp tv
	                           ON  drp.Device_ID = tv.Vault_ID
	                      INNER JOIN Manufacturer m
	                           ON  m.Manufacturer_ID = tv.Manufacturer_ID
	                      INNER JOIN SITE s WITH(NOLOCK)
	                           ON  s.Site_ID = drp.Site_ID
	                               --AND drp.Site_ID = ISNULL(@Site, drp.Site_ID)
	                           AND (
	                                   @SiteIDList IS NOT NULL
	                                   AND drp.Site_Id IN (SELECT DATA
	                                                       FROM   dbo.fnSplit (@SiteIDList, ','))
	                               )
	                      INNER JOIN Sub_Company sc WITH(NOLOCK)
	                           ON  sc.Sub_Company_ID = s.Sub_Company_ID
	                           AND sc.Sub_Company_ID = ISNULL(@Subcompany, sc.Sub_Company_ID)
	                      INNER JOIN Company c WITH(NOLOCK)
	                           ON  c.Company_ID = sc.Company_ID
	                           AND c.Company_ID = ISNULL(@Company, sc.Company_ID)
	                      LEFT OUTER JOIN Sub_Company_Area SCA
	                           ON  s.Sub_Company_Area_ID = SCA.Sub_Company_Area_ID
	               WHERE  DropCompleteDate BETWEEN @Startdate AND @Enddate
	           ) X
	    ORDER BY
	           X.Serial_NO,
	           DropCompleteDate
	END
	ELSE
	BEGIN
	    SELECT *,
	           (DropAmount) DropBalance
	    FROM   (
	               SELECT tv.Name VaultName,
	                      drp.Drop_ID AS Serial_NO,
	                      m.Manufacturer_Name,
	                      tv.Type_Prefix TYPE,
	                      tv.Capacity,
	                      tv.Alert_Level,
	                      s.Site_Code,
	                      drp.DropCompleteDate,
	                      drp.FillAmount,
	                      drp.BleedAmount,
	                      drp.AdjustmentAmount,
	                      CASE --WHEN tv.IsWebServiceEnabled = 0 THEN ISNULL(drp.Meter_Voucher, 0.00) ****** CHANGED
	                           WHEN COALESCE(drp.IsVaultWebServiceEnabled, tv.IswebserviceEnabled, 0) 
	                                = 0 THEN ISNULL(drp.Meter_Voucher, 0.00)
	                           ELSE ISNULL(
	                                    (
	                                        SELECT SUM(te.AMOUNT)
	                                        FROM   tVault_TransactionEvents te
	                                        WHERE  te.Site_Drop_ref = drp.Site_Drop_Ref
	                                               AND te.Site_Id = drp.Site_ID
	                                               AND te.TransactionEvent_Type = 
	                                                   1 --VOUCHER
	                                    ),
	                                    0.00
	                                )
	                      END AS voucher_Out,
	                      CASE --WHEN tv.IsWebServiceEnabled = 0 THEN ISNULL(drp.Meter_Handpay, 0.00) ****** CHANGED
	                           WHEN COALESCE(drp.IsVaultWebServiceEnabled, tv.IswebserviceEnabled, 0) 
	                                = 0 THEN ISNULL(drp.Meter_Handpay, 0.00)
	                                +
	                                ISNULL(drp.Meter_jackpot, 0.00)
	                           ELSE ISNULL(
	                                    (
	                                        SELECT SUM(TE.AMOUNT)
	                                        FROM   tVault_TransactionEvents TE
	                                        WHERE  TE.Site_Drop_ref = drp.Site_Drop_Ref
	                                               AND te.Site_Id = drp.Site_ID
	                                               AND TransactionEvent_Type <>
	                                                   1 --HANDPAY/JACKPOT
	                                    ),
	                                    0.00
	                                )
	                      END AS HandPay_Out,
	                      tv.[Status] AS VaultStatus,
	                      CASE --WHEN tv.IsWebServiceEnabled = 0 THEN 'BMC' ****** CHANGED
	                           WHEN COALESCE(drp.IsVaultWebServiceEnabled, tv.IswebserviceEnabled, 0) 
	                                = 0 THEN 'BMC'
	                           ELSE 'Vault'
	                      END AS InventoryMethod,
	                      ISNULL(sca.Sub_Company_Area_Name, '-') 
	                      Sub_Company_Area_Name,
	                      sf.Staff_First_Name + ', ' + sf.Staff_Last_Name [Name],
	                      drp.Site_Drop_Ref,
	                      CASE 
	                           WHEN COALESCE(drp.IsVaultWebServiceEnabled, tv.IswebserviceEnabled, 0) 
	                                = 0 THEN drp.meter_balance
	                           ELSE drp.vault_balance
	                      END AS DropAmount,
	                      '' AS AutoAdjust
	               FROM   tVault_drops drp WITH(NOLOCK)
	                      INNER JOIN Staff sf
	                           ON  sf.UserTableID = drp.DropCompleteUser
	                      INNER JOIN @temp tv
	                           ON  drp.Device_ID = tv.Vault_ID
	                      INNER JOIN Manufacturer m
	                           ON  m.Manufacturer_ID = tv.Manufacturer_ID
	                      INNER JOIN SITE s WITH(NOLOCK)
	                           ON  s.Site_ID = drp.Site_ID
	                               --AND drp.Site_ID = ISNULL(@Site, drp.Site_ID)
	                           AND (
	                                   @SiteIDList IS NOT NULL
	                                   AND drp.Site_Id IN (SELECT DATA
	                                                       FROM   dbo.fnSplit (@SiteIDList, ','))
	                               )
	                      INNER JOIN Sub_Company sc WITH(NOLOCK)
	                           ON  sc.Sub_Company_ID = s.Sub_Company_ID
	                           AND sc.Sub_Company_ID = ISNULL(@Subcompany, sc.Sub_Company_ID)
	                      INNER JOIN Company c WITH(NOLOCK)
	                           ON  c.Company_ID = sc.Company_ID
	                           AND c.Company_ID = ISNULL(@Company, sc.Company_ID)
	                      LEFT OUTER JOIN Sub_Company_Area SCA
	                           ON  s.Sub_Company_Area_ID = SCA.Sub_Company_Area_ID
	               WHERE  DropCompleteDate BETWEEN @Startdate AND @Enddate
	           ) X
	    WHERE  (DropAmount) <> 0
	    ORDER BY
	           X.Serial_NO,
	           DropCompleteDate
	END
END
GO


--exec rsp_Report_VaultDrop 1,0,0,'--All--',1,0,'2013-11-12 17:48:02.020', '2013-11-19 17:28:02.020'

