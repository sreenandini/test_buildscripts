USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA       = N'dbo'
              AND SPECIFIC_NAME     = N'rsp_Report_VaultCassettesInventoryStatus'
   )
    DROP PROCEDURE dbo.rsp_Report_VaultCassettesInventoryStatus
GO  
CREATE PROCEDURE dbo.rsp_Report_VaultCassettesInventoryStatus
	@Company INT,
	@SubCompany INT,
	@Site INT,
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
	 	 
		 
		SELECT s.Site_ID,  
				s.Site_Name,  
				s.Site_Code,  
				vd.Name,  
				tvc.Vault_ID,
				CASE 
					WHEN  tctype.CassetteType_Name ='Rejection' THEN tvc.Cassette_Name + ' (R)'
					ELSE  tvc.Cassette_Name 
				END AS Cassette_Name,  				
				tc.Cassette_ID,  
				tc.Denom,  
				tc.CurrentBalance,  
				tc.Transaction_ID,  
				CASE   
					WHEN (ISNULL(vd.IsWebServiceEnabled, 0)) = 0 THEN 'BMC'  
					ELSE 'Vault'  
				END AS InventoryCalcuationMethod,
			
				'Active' AS [Status] 
			 
		FROM   tNGAInstallations tNGA WITH(NOLOCK)  
				INNER JOIN SITE s WITH(NOLOCK)  
					ON  tNGA.Site_ID = s.Site_ID  
				INNER JOIN tVault_Devices vd(NOLOCK)  
					ON  vd.NGADevice_ID = tNGA.NGADevice_ID  
				INNER JOIN tVault_Cassettes tvc  
					ON  tvc.Vault_ID = vd.Vault_ID  
				INNER JOIN tVault_CassetteTransactions tc WITH(NOLOCK)  
					ON  tc.Cassette_ID = tvc.Cassette_ID  
				INNER JOIN tVault_CassetteTypes tctype WITH (NOLOCK)
				  ON tvc.[Type]=tctype.CassetteType_ID   
				INNER JOIN (  
					 SELECT MAX(transaction_id) transaction_id,  
							tvct.Cassette_ID  
					 FROM   tVault_CassetteTransactions tvct  
					 GROUP BY  
							tvct.Cassette_ID  
				 ) A  
					ON  tc.Transaction_ID = A.transaction_id  
					AND tc.Cassette_ID = A.Cassette_ID  
				LEFT JOIN Sub_Company_Area Area WITH(NOLOCK)  
					ON  Area.Sub_Company_Area_ID = S.Sub_Company_Area_ID  
				LEFT JOIN Sub_Company_District District WITH(NOLOCK)  
					ON  District.Sub_Company_District_ID = S.Sub_Company_District_ID  
				LEFT JOIN Sub_Company_Region Region WITH(NOLOCK)  
					ON  Region.Sub_Company_Region_ID = S.Sub_Company_Region_ID  
				INNER JOIN Sub_Company SC WITH(NOLOCK)  
					ON  SC.Sub_Company_ID = s.Sub_Company_ID  
				INNER JOIN Company C WITH(NOLOCK)  
					ON  C.Company_ID = sc.Company_ID  
		WHERE  tNGA.End_Date IS NULL  
				AND tNGA.start_date IS NOT NULL
				AND tNGA.Assigned_To_Site IS NOT NULL
				AND ISNULL(@Company, C.Company_Id) = C.Company_Id  
				AND ISNULL(@Subcompany, S.Sub_Company_Id) = S.Sub_Company_Id  
				--AND ISNULL(@Site, S.Site_Id) = S.Site_Id       
				AND (
	                   @SiteIDList IS NOT NULL
	                   AND S.Site_Id IN (SELECT DATA
	                                       FROM   dbo.fnSplit (@SiteIDList, ','))
	               )
		ORDER BY  
			Site_Code,  
			Vault_ID,
			[TYPE],
			tc.Denom
END
GO

--exec rsp_Report_VaultCassettesInventoryStatus 0,0,0,1