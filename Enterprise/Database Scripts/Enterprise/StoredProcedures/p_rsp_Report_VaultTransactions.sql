USE ENTERPRISE 
GO
 
IF OBJECT_ID('rsp_Report_VaultTransactions') IS NOT NULL
    DROP PROC dbo.rsp_Report_VaultTransactions
GO
 
CREATE PROCEDURE dbo.rsp_Report_VaultTransactions
	@Company INT,
	@SubCompany INT,
	@Site INT,
	@StartDate DATETIME,
	@EndDate DATETIME,
	@UserID INT,
	@SiteIDList VARCHAR(MAX)
AS
BEGIN
	IF ISNULL(@Company, 0) = 0
	    SET @Company = NULL    
	
	IF ISNULL(@Subcompany, 0) = 0
	    SET @Subcompany = NULL    
	
	IF ISNULL(@Site, 0) = 0
	    SET @Site = NULL  
	
	SELECT s.Site_Name,
	       s.Site_Code,
	       td.Name VaultName,
	       td.Serial_NO,
	       CAST(vt.Transactions_ID AS INT) AS TransactionID,
	       CASE 
	            WHEN tvtr.Reason_ID = 9 THEN tt.Type_Description + ' *'
	            ELSE tt.Type_Description
	       END AS Type_Description,
	       CASE 
	            WHEN tt.Type_Description = 'DROP' AND ISNULL(td2.IsVaultWebServiceEnabled, td.IsWebServiceEnabled) 
	                 = 1 THEN (-1) * td2.vault_balance
	            ELSE vt.TransactionAmount
	       END TransactionAmount,
	       CAST(td2.Drop_ID AS INT) AS DropID,
	       CASE 
	            WHEN td2.IsVaultWebServiceEnabled = 1 THEN 'VAULT'
	            ELSE 'BMC'
	       END AS IsWebserviceEnabled,
	       vt.CreatedDate,
	       CASE 
	            WHEN ISNULL(td2.IsVaultWebServiceEnabled, td.IsWebServiceEnabled) 
	                 = 1 THEN ISNULL(vt.VaultTotalAmountOnTransaction, 0)
	            ELSE vt.TotalAmountOnTransaction
	       END AS 'VaultBalance'
	FROM   dbo.tvault_Transactions vt
	       INNER JOIN SITE s
	            ON  vt.site_id = s.site_id
	       INNER JOIN dbo.tVault_Devices td
	            ON  td.Vault_ID = vt.Device_ID
	       INNER JOIN dbo.tVault_Transaction_Type tt
	            ON  tt.Type_ID = vt.Type
	       INNER JOIN tVault_Transaction_Reason tvtr
	            ON  tvtr.Reason_ID = vt.Reason_ID
	       INNER JOIN tVault_Drops td2
	            ON  vt.Site_Drop_ref = td2.Site_Drop_Ref
	            AND td2.Site_ID = s.site_id
	       INNER JOIN Sub_Company SC WITH(NOLOCK)
	            ON  SC.Sub_Company_ID = s.Sub_Company_ID
	       INNER JOIN Company C WITH(NOLOCK)
	            ON  C.Company_ID = sc.Company_ID
	WHERE  ISNULL(@Company, C.Company_Id) = C.Company_Id
	       AND ISNULL(@Subcompany, S.Sub_Company_Id) = S.Sub_Company_Id
	           --AND ISNULL(@Site, S.Site_Id) = S.Site_Id
	       AND (
	               @SiteIDList IS NOT NULL
	               AND S.site_id IN (SELECT DATA
	                                 FROM   dbo.fnSplit (@SiteIDList, ','))
	           )
	       AND vt.CreatedDate BETWEEN @Startdate AND @Enddate
	ORDER BY
	       site_name ASC,
	       vt.CreatedDate ASC
END
GO


