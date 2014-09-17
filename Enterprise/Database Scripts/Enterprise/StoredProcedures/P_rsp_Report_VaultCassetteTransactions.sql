SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_VaultCassetteTransactions]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_VaultCassetteTransactions]
GO
--EXEC rsp_Report_VaultCassetteTransactions 0,0,0,'2013-12-01 19:55:01','2013-12-24 16:12:26.767',0
CREATE PROCEDURE rsp_Report_VaultCassetteTransactions
      @Company INT,
      @SubCompany INT,
      @Site INT,
      @StartDate DateTime,
      @EndDate DateTime,
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
             vt.Transactions_ID,
             CASE 
				WHEN tvtr.Reason_ID = 9 THEN tt.Type_Description + ' *'
				ELSE tt.Type_Description
             END AS Type_Description,             
             tct.Cassette_ID,
             CASE 
				WHEN  tctype.CassetteType_Name ='Rejection' THEN tc.Cassette_Name + ' (R)'
				ELSE  tc.Cassette_Name 
			 END AS Cassette_Name,             
             tct.FillAmount,
             vt.CreatedDate,
             tct.AmountOnFill  AS TotalAmountOnTransaction,
             td2.Drop_ID
      FROM   dbo.tvault_Transactions vt
                  INNER JOIN tVault_Cassettetransactions  tct
                  ON vt.Transactions_ID =tct.Transaction_ID
                  INNER JOIN tVault_Cassettes tc
                  ON tc.Cassette_ID=tct.Cassette_ID AND tc.Vault_ID=vt.Device_ID
             INNER JOIN SITE s
                  ON  vt.site_id = s.site_id
             INNER JOIN dbo.tVault_Devices td
                  ON  td.Vault_ID = vt.Device_ID
             INNER JOIN dbo.tVault_Transaction_Type tt
                  ON  tt.Type_ID = vt.Type
             INNER JOIN tVault_Transaction_Reason tvtr
                  ON tvtr.Reason_ID = vt.Reason_ID
             INNER JOIN tVault_Drops td2
                  ON  vt.Site_Drop_ref = td2.Site_Drop_Ref  
                  AND td2.site_id= s.site_id
             INNER JOIN Sub_Company SC WITH(NOLOCK)
                  ON  SC.Sub_Company_ID = s.Sub_Company_ID
             INNER JOIN Company C WITH(NOLOCK)
                  ON  C.Company_ID = sc.Company_ID
             INNER JOIN tVault_CassetteTypes tctype WITH (NOLOCK)
				  ON tc.[Type]=tctype.CassetteType_ID     
      WHERE  ISNULL(@Company, C.Company_Id) = C.Company_Id
             AND ISNULL(@Subcompany, S.Sub_Company_Id) = S.Sub_Company_Id
             --AND ISNULL(@Site, S.Site_Id) = S.Site_Id
             AND (
	               @SiteIDList IS NOT NULL AND S.Site_Id IN (SELECT DATA    
                                              FROM   dbo.fnSplit (@SiteIDList,','))
	           )
             AND vt.CreatedDate BETWEEN @StartDate AND @EndDate
      ORDER BY
             site_name ASC,
             vt.CreatedDate ASC,
             tc.[Type] ASC,
             tct.Denom             
END

GO

