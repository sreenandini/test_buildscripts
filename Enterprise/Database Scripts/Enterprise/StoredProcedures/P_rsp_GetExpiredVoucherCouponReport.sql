USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetExpiredVoucherCouponReport]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetExpiredVoucherCouponReport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--  
-- Description: Fetches the expired voucher coupon records
--  
-- Inputs:  StartDate,EndDate,DeviceType 
-- Outputs:       
--  
-- =======================================================================  
--   
-- Revision History  

-- Kirubakar S 12/05/2010 Created
  
--------------------------------------------------------------------------- 

  
CREATE PROCEDURE rsp_GetExpiredVoucherCouponReport
	@Company	INT = 0,
	@SubCompany INT = 0,
	@Region		INT = 0,
	@Area		INT = 0,
	@District	INT = 0,
	@Site		INT = 0,
	@StartDate	DATETIME,
	@EndDate	DATETIME,
	@DeviceType VARCHAR(50),
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
	
	SELECT DISTINCT 
	       SI.Site_Name AS SiteName,
	       TV.dtPrinted AS IssueDate,
	       TV.dtExpire AS ExpiryDate,
	       TV.dtPaid AS RedeemDate,
	       TV.strBarCode AS VoucherNumber,
	       NULL AS GameSequenceNumber,
	       dbo.Fn_VoucherType(TV.Ticket_Type, TV.iVoucherID) AS VoucherType,
	       dbo.Fn_VoucherSource(TV.iVoucherID) AS Source,
	       --  dbo.Fn_VoucherType(TV.Ticket_Type) AS VoucherType,
	       --  'BMC' AS Source,  
	       dbo.StatusCodeDescription(TV.strVoucherStatus, TV.dtExpire) AS CurrentStatus,
	       dbo.compute_decimal(TV.iAmount) AS Amount
	FROM   Voucher TV
	       INNER JOIN [SITE] SI
	            ON  TV.iSiteid = SI.Site_Code
	       INNER JOIN Device TD
	            ON  TV.ideviceid = TD.ideviceid
	            AND TV.iSiteId = TD.Site_Code
	       INNER JOIN Sub_Company SC
	            ON  Si.Sub_Company_ID = SC.Sub_Company_ID
	       INNER JOIN Company C
	            ON  C.Company_ID = sc.Company_ID
	       LEFT OUTER JOIN siteworkstations TIW
	            ON  TIW.Site_Workstation = TD.strSerial
	       LEFT OUTER JOIN MACHINE EM
	            ON  EM.Machine_Stock_No = TD.strSerial
	WHERE  (
	           (TV.strVoucherStatus IS NULL AND TV.dtExpire <= GETDATE())
	           OR (TV.strVoucherStatus = 'PD' AND TV.dtPaid >= TV.dtExpire)
	       )
	       AND (ISNULL(TV.strVoucherStatus,'') NOT IN ('NA', 'LT'))
	       AND (
	               ISNULL(@District, SI.Sub_Company_District_Id) = SI.Sub_Company_District_Id
	           )
	       AND (
	               ISNULL(@Area, SI.Sub_Company_Area_Id) = SI.Sub_Company_Area_Id
	           )
	       AND (
	               ISNULL(@Region, SI.Sub_Company_Region_Id) = SI.Sub_Company_Region_Id
	           )
	       AND (ISNULL(@Site, SI.Site_ID) = SI.Site_ID)
	       AND (ISNULL(@SubCompany, SI.Sub_Company_ID) = SI.Sub_Company_ID)
	       AND (ISNULL(@Company, C.Company_ID) = C.Company_ID)
	       AND TV.dtExpire BETWEEN @StartDate AND @EndDate
	       AND (
	               ((@DeviceType = 'ALL' OR @DeviceType='--All--')AND TV.ideviceid IS NOT NULL)
	               OR (
	                      @DeviceType = 'SLOT'
	                      AND TV.ideviceid IS NOT NULL
	                      AND TD.strSerial IN (SELECT EM1.Machine_Stock_No
	                                           FROM   MACHINE EM1)
	                  )
	               OR (
	                      @DeviceType = 'CASHDESK'
	                      AND TV.ideviceid IS NOT NULL
	                      AND TD.strSerial IN (SELECT TIW1.Site_Workstation
	                                           FROM   siteworkstations TIW1)
	                  )
	           )
	       AND (
	               @SiteIDList IS NOT NULL
	               AND SI.Site_ID IN (SELECT DATA
	                                  FROM   fnSplit(@SiteIDList, ','))
	           )
	ORDER BY
	       dtExpire
END
GO

