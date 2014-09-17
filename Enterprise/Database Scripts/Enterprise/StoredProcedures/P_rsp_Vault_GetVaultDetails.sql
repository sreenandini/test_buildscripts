USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_Vault_GetVaultDetails'
   )
    DROP PROCEDURE dbo.rsp_Vault_GetVaultDetails
GO 

CREATE PROCEDURE dbo.rsp_Vault_GetVaultDetails
	@Vault_id INT
AS
	/*****************************************************************************************************
DESCRIPTION	: PROC Description  
CREATED DATE: PROC CreateDate
MODULE		: PROC used in Modules	
CHANGE HISTORY :
Example :
rsp_Vault_GetVaultDetails 11
------------------------------------------------------------------------------------------------------
AUTHOR					DESCRIPTON										MODIFIED DATE
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/

BEGIN
	SELECT VD.Vault_ID,
	       VD.[NAME],
	       VD.Serial_NO,
	       VD.Site_ID,
	       VD.Alert_Level,
	       VD.Created_Date,
	       VD.End_Date,
	       VD.[Active],
	       VD.Manufacturer_ID,
	       VD.Type_Prefix,
	       ISNULL(VD.Capacity, 0.00) Capacity,
	       ISNULL(VD.NoofCoinHopper, 0) NoofCoinHopper,
	       ISNULL(VD.NoofCassettes, 0) NoofCassettes,
	       VD.IsWebServiceEnabled,
	       ISNULL(VD.PurchasePrice, 0.00) PurchasePrice,
	       ISNULL(VD.PurchaseInvoice, '') PurchaseInvoice,
	       ISNULL(VD.PurchaseDate, GETDATE()) PurchaseDate,
	       ISNULL(VD.depreciationDate, GETDATE()) depreciationDate,
	       ISNULL(VD.SoldPrice, 0.00) SoldPrice,
	       ISNULL(VD.SoldInvoice, '') SoldInvoice,
	       ISNULL(VD.SoldDate, GETDATE()) SoldDate,
	       ND.Description AS [Description],
	       CAST(
	           CASE 
	                WHEN NI.Installation_No IS NULL THEN 0
	                ELSE 1
	           END 
	           AS BIT
	       ) AS IsAssigned,
	       CAST(CASE WHEN VD.PurchaseDate IS NULL THEN 0 ELSE 1 END AS BIT) AS 
	       IsPurchased,
	       CAST(
	           CASE 
	                WHEN VD.IsWebServiceEnabled = 0 THEN 1
	                WHEN VD.IsWebServiceEnabled = 1
	           AND (ISNULL(VD.NoofCoinHopper, 0) + ISNULL(VD.NoofCassettes, 0)) 
	               >
	               0 THEN 1
	               ELSE 0 
	               END AS BIT
	       ) AS IsConfigured,
	       vd.StandardFillAmount StandaradFillAmount,
	       CAST(ISNULL(evh.PendingUpdate, 0)AS BIT) AS IsSiteUpdated, /*This column is to allow */
	       CAST(ISNUll(vd.FillRejection,0)AS BIT) FillRejection,
	      CAST(ISNUll(vd.AutoAdjustEnabled,0)AS BIT) AutoAdjustEnabled
	FROM   dbo.tVault_Devices VD WITH(NOLOCK)
	       INNER JOIN tNGADevices ND WITH(NOLOCK)
	            ON  vd.NGADevice_ID = ND.NGADevice_ID
	       LEFT OUTER JOIN tNGAInstallations NI WITH(NOLOCK)
	            ON  ND.NGADevice_ID = NI.NGADevice_ID
	            AND NI.End_Date IS NULL
	       LEFT OUTER JOIN ExchangeVersionHistory evh
	            ON  evh.Site_ID = NI.Site_ID
	WHERE  VD.Vault_ID = @Vault_id
	       AND vd.End_Date IS NULL
END
GO