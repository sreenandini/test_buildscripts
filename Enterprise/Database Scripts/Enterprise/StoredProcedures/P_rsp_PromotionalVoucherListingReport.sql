USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_PromotionalVoucherListingReport]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_PromotionalVoucherListingReport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE rsp_PromotionalVoucherListingReport 
	@Company		INT = 0,
	@SubCompany		INT = 0,
	@Region			INT = 0,
	@Area			INT = 0,
	@District		INT = 0,
	@Site			INT = 0,
	@StartDate		DATETIME,
	@EndDate		DATETIME,
	@VoucherStatus	VARCHAR(25),
	@SiteIDList		VARCHAR(MAX)
AS
BEGIN
	
	--For Promotional Tickets (BMC Promo + TIS Promo)  
	
	--IF NOT EXISTS( SELECT 1 FROM Zone WHERE Zone_ID = @Zone)
	--BEGIN
	--SET  @Zone=0
	--END 

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
	       --  TD.ideviceid as SLOT,       
	       
	       TV.dtPrinted AS IssueDate,
	       TV.strBarCode AS ValidationNumber,
	       dbo.StatusCodeDescription(TV.strVoucherStatus, TV.dtExpire) AS STATUS,
	       dbo.compute_decimal(TV.iAmount) AS Amount,
	       CASE Ticket_Type
	            WHEN 0 THEN 'CASHABLE'
	            WHEN 1 THEN 'NON CASHABLE'
	       END AS VoucherType,
	       CASE PT.PromotionalID
	            WHEN 1 THEN 'TIS'
	            ELSE 'BMC'
	       END AS Source,
	       EVD.EffectiveDate
	       
	       --  ZONE.Zone_Name
	       --[dbo].FnGetZoneNameForSerial(TD.strSerial,TD.Site_Code) AS Zone_Name
	FROM   Voucher TV
	       INNER JOIN PromotionalTickets PT
	            ON  PT.VoucherID = TV.iVoucherID
	       INNER JOIN [SITE] SI
	            ON  TV.iSiteid = SI.Site_Code
	       INNER JOIN SiteWorkstations SW
	            ON  SI.Site_ID = SW.site_ID
	       INNER JOIN Sub_Company SC
	            ON  Si.Sub_Company_ID = SC.Sub_Company_ID
	       INNER JOIN Company C
	            ON  C.Company_ID = SC.Company_ID
	       INNER JOIN Device TD
	            ON  TV.iDeviceID = TD.ideviceid --AND (TD.iSiteid=TD.Site_Code or ISNULL(TD.iSiteid,1000)=1000) 
	                
	       LEFT OUTER JOIN ExtVoucherDetails EVD
	            ON  EVD.VoucherID = TV.iVoucherID
	            AND EVD.SiteCode = TV.iSiteID
	WHERE  (
	           (ISNULL(@District, SI.Sub_Company_District_Id) = SI.Sub_Company_District_Id)
	           AND (ISNULL(@Area, SI.Sub_Company_Area_Id) = SI.Sub_Company_Area_Id)
	           AND (ISNULL(@Region, SI.Sub_Company_Region_Id) = SI.Sub_Company_Region_Id)
	           AND (ISNULL(@Site, SI.Site_ID) = SI.site_id)
	           AND (ISNULL(@SubCompany, SI.Sub_Company_ID) = SI.Sub_Company_ID)
	           AND (ISNULL(@Company, C.Company_ID) = C.Company_ID) 
	               --
	       ) 
	       --AND (@Zone= 0 OR ([dbo].FnGetZoneIDForSerial(TD.strSerial,TD.Site_Code)=@Zone))
	       AND dtPrinted BETWEEN @StartDate AND @EndDate
	       AND (
	               (
	                   (@VoucherStatus = 'ALL' OR @VoucherStatus = '--All--')
	                   -- AND ISNULL(TV.strVoucherStatus, '') <> 'LT'
	               )
	               OR (
	                      @VoucherStatus = 'LIABILITYTRANSFER'
	                      AND TV.strVoucherStatus = 'LT'
	                  )
	               OR (@VoucherStatus = 'PAID' AND TV.strVoucherStatus = 'PD')
	               OR (@VoucherStatus = 'VOID' AND TV.strVoucherStatus = 'VD')
	               OR (
	                      @VoucherStatus = 'PARTIALLYPAID'
	                      AND TV.strVoucherStatus = 'PP'
	                  )
	               OR (
	                      @VoucherStatus = 'EXPIRED'
	                      AND TV.strVoucherStatus IS NULL
	                      AND TV.dtExpire < GETDATE()
	                  )
	               OR (
	                      @VoucherStatus = 'CANCELLED'
	                      AND TV.strVoucherStatus = 'NA'
	                  )
	               OR (
	                      @VoucherStatus = 'ACTIVE'
	                      AND TV.strVoucherStatus IS NULL
	                      AND TV.dtExpire > GETDATE()
	                  )
	           )
	       AND (
	               @SiteIDList IS NOT NULL
	               AND SI.Site_ID IN (SELECT DATA
	                                  FROM   fnSplit(@SiteIDList, ','))
	           )
	GROUP BY
	       --zone.zone_id,
	       SI.Site_name,
	       TD.Site_Code,
	       TV.dtPrinted,
	       TV.strBarCode,
	       TV.strVoucherStatus,
	       TV.dtExpire,
	       TV.iAmount,
	       TV.Ticket_Type,
	       PT.PromotionalID,
	       EVD.EffectiveDate
	ORDER BY
	       STATUS,
	       IssueDate
END
GO

