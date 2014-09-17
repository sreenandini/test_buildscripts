USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetTicketLiabilityReport]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetTicketLiabilityReport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE rsp_GetTicketLiabilityReport
	@Company		INT = 0,
	@SubCompany		INT = 0,
	@Region			INT = 0,
	@Area			INT = 0,
	@District		INT = 0,
	@Site			INT = 0,
	@Zone			INT = 0,
	@IssueDate		DATETIME,
	@VoucherStatus	VARCHAR(25),
	@DeviceType		VARCHAR(50),
	@GroupByZone	BIT,
	@SiteIDList		VARCHAR(MAX)
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
	
	IF @Zone = 0
	    SET @Zone = NULL
	
	DECLARE @Site_Code VARCHAR(50) 
	     
	IF (@Site IS NOT NULL)
	BEGIN
	    SELECT @Site_Code = Site_Code
	    FROM   [SITE]
	    WHERE  Site_id = @Site
	END      
	
	SELECT DISTINCT 
	       SI.Site_Name AS SiteName,
	       TV.dtPrinted AS IssueDate,
	       TV.strBarCode AS VoucherNumber,
	       NULL AS GameSequenceNumber,
	       dbo.Fn_VoucherType(TV.Ticket_Type, TV.iVoucherID) AS VoucherType,
	       dbo.Fn_VoucherSource(TV.iVoucherID) AS Source,
	       dbo.StatusCodeDescription(TV.strVoucherStatus, TV.dtExpire) AS 
	       CurrentStatus,
	       dbo.FN_IssueStatusDescription(
	           TV.strVoucherStatus,
	           @IssueDate,
	           TV.dtPrinted,
	           TV.dtPaid,
	           TV.dtExpire
	       ) AS IssueStatus,
	       dbo.compute_decimal(TV.iAmount) AS Amount,
	       [dbo].FnGetZoneNameForSerial(TD.strSerial, TD.Site_Code) AS Zone_Name
	FROM   Voucher TV
	       INNER JOIN [SITE] SI
	            ON  TV.iSiteid = SI.Site_Code
	       INNER JOIN Sub_Company SC
	            ON  Si.Sub_Company_ID = SC.Sub_Company_ID
	       INNER JOIN Company C
	            ON  C.Company_ID = SC.Company_ID
	       INNER JOIN Device TD
	            ON  TV.ideviceid = TD.ideviceid
	            AND TV.iSiteId = TD.Site_Code
	       LEFT OUTER JOIN siteworkstations TIW
	            ON  TIW.Site_Workstation = TD.strSerial
	       LEFT OUTER JOIN MACHINE EM
	            ON  EM.Machine_Stock_No = TD.strSerial
	WHERE  (
	           @Zone IS NULL
	           OR (
	                  [dbo].FnGetZoneIDForSerial(TD.strSerial, TD.Site_Code) = @Zone
	              )
	       )
	       AND (
	               ISNULL(@Site, 0) = 0
	               OR TV.iSiteId = @Site_Code
	           )
	       AND ISNULL(@District, SI.Sub_Company_District_Id) = SI.Sub_Company_District_Id
	       AND ISNULL(@Area, SI.Sub_Company_Area_Id) = SI.Sub_Company_Area_Id
	       AND ISNULL(@Region, SI.Sub_Company_Region_Id) = SI.Sub_Company_Region_Id
	       AND ISNULL(@SubCompany, SI.Sub_Company_Id) = SI.Sub_Company_Id
	       AND ISNULL(@Company, C.Company_ID) = C.Company_ID
	           -- OR TV.iSiteId=(select site_code from site where Site_ID=@SITE))
	       AND dtPrinted <= @IssueDate
	       AND (dtPaid IS NULL OR dtPaid > @IssueDate)
	       AND dbo.StatusCodeDescription(TV.strVoucherStatus, TV.dtExpire) <> 'LIABILITYTRANSFER'
	       AND (
	               (@VoucherStatus = 'ALL' OR @VoucherStatus = '--All--')
	               OR (@VoucherStatus = 'VOID' AND TV.strVoucherStatus = 'VD')
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
	       CurrentStatus,
	       IssueDate
END
GO