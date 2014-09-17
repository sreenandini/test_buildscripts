USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_Report_VaultBalanceReport]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_Report_VaultBalanceReport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--select getdate()
---Exec rsp_Report_VaultBalanceReport 2,0,0
CREATE PROCEDURE rsp_Report_VaultBalanceReport
	@Company INT = 0,
	@SubCompany INT = 0,
	@Region INT = 0,
	@Area INT = 0,
	@District INT = 0,
	@Site INT = 0,
	@SiteIDList VARCHAR(MAX)
AS
BEGIN
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
	
	
	SELECT gmi.Site_Name,
	       s.Site_Code,	     
	       tf.VaultID AS VaultID,
	       tf.FillDate,
	       ISNULL(tf.FillAmount, 0) AS FillAmount,
	       tf.Fills,
	       tf.TotalFundsIn,
	       tf.VoucherOut,
	       (tf.AttendantPay + tf.Jackpot) AS AttendantPayJackpot
	       INTO #tmpMasterGroup
	FROM   vw_genericmachineinformation gmi WITH(NOLOCK)
	       INNER JOIN TotalVaultBalance tf WITH(NOLOCK)
	            ON  tf.Installation_ID = gmi.Installation_ID	            
	       INNER JOIN Installation I WITH(NOLOCK)
	            ON  I.Installation_ID = tf.Installation_ID
	       INNER JOIN [Site] S WITH(NOLOCK)
	            ON  S.Site_ID = tf.Site_ID	      
	       INNER JOIN tVault_Devices vd
	            ON  vd.Site_ID = tf.Site_ID
	       INNER JOIN Sub_Company SC WITH(NOLOCK)
	            ON  SC.Sub_Company_ID = S.Sub_Company_ID
	       INNER JOIN Company C WITH(NOLOCK)
	            ON  SC.Company_ID = C.Company_ID
	WHERE  I.Installation_End_Date IS NULL
	       AND tf.Site_Id = ISNULL(@site, tf.Site_Id)
	       AND ISNULL(@Company, C.Company_Id) = C.Company_Id
	       AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	       AND ISNULL(@Region, S.Sub_Company_Region_ID) = S.Sub_Company_Region_ID
	       AND ISNULL(@Area, S.Sub_Company_Area_ID) = S.Sub_Company_Area_ID
	       AND ISNULL(@District, S.Sub_Company_District_ID) = S.Sub_Company_District_ID
	       AND vd.Active = 1
	       AND (
	               @SiteIDList IS NOT NULL
	               AND S.Site_ID IN (SELECT DATA
	                                 FROM   fnSplit(@SiteIDList, ','))
	           )
	GROUP BY
	       gmi.Site_Name,
	       gmi.Bar_Position_Name,
	       s.Site_Code,
	       gmi.Sub_Company_Name,
	       gmi.Company_Name,
	       gmi.Machine_Stock_No,
	       gmi.Zone_Name,
	       gmi.Installation_End_Date,
	       VaultID,
	       tf.FillDate,
	       FillAmount,
	       Fills,
	       TotalFundsIn,
	       VoucherOut,
	       tf.AttendantPay,
	       tf.Jackpot,
	       TotalFundsAtSite
	
	
	SELECT Site_Name,
	       Site_Code,	      
	       VaultID,
	       MAX(FillDate) AS FillDate,
	       AVG(FillAmount) AS FillAmount,
	       AVG(Fills) AS Fills,
	       SUM(TotalFundsIn) AS TotalFundsIn,
	       SUM(TotalFundsIn) + (AVG(Fills) -(SUM(VoucherOut) + SUM(AttendantPayJackpot))) AS 
	       TotalFundsAtSite,
	       SUM(VoucherOut) AS VoucherOut,
	       SUM(AttendantPayJackpot) AS AttendantPayJackpot,
	       (AVG(Fills) -(SUM(VoucherOut) + SUM(AttendantPayJackpot))) AS 
	       CurrentInventory
	FROM   #tmpMasterGroup
	GROUP BY
	       Site_Name,
	       Site_Code,
	       VaultID
	      
	
	
	DROP TABLE #tmpMasterGroup
END
GO

