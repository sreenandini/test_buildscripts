USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_getRedeemedTicketByDevice]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_getRedeemedTicketByDevice]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_getRedeemedTicketByDevice
	@Company	INT = 0,
	@SubCompany INT = 0,
	@Region		INT = 0,
	@Area		INT = 0,
	@District	INT = 0,
	@Site		INT = 0,
	@Zone		INT = 0,
	@StartDate	DATETIME,
	@EndDate	DATETIME,
	@DeviceType VARCHAR(50),
	@GroupByZone BIT,
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
	
	IF @Zone = 0
	    SET @Zone = NULL
	
	DECLARE @Site_Code VARCHAR(50)      
	IF (@Site IS NOT NULL)
	BEGIN
	    SELECT @Site_Code = site_code
	    FROM   SITE
	    WHERE  Site_id = @Site
	END      
	
	SELECT DISTINCT 
	       SI.Site_Name AS SiteName,
	       PD.strSerial AS PaidAsset,
	       --isnull(EB.Bar_Position_Name,'CASHDESK') as PaidStand,     
	       [dbo].FnGetPositionForSerial(
	           PD.strSerial,
	           LTRIM(RTRIM(CONVERT(VARCHAR(50), PD.iSiteID)))
	       ) AS PaidStand,
	       TV.dtPaid AS RedemptionDate,
	       TV.strBarCode AS TicketNumber,
	       dbo.compute_decimal(TV.iAmount) AS TicketValue,
	       dbo.Fn_VoucherType(TV.Ticket_Type, TV.iVoucherID) AS VoucherType,
	       '---' AS PlayerID,
	       ID.strSerial AS IssuedAsset,
	       [dbo].FnGetPositionForSerial(ID.strSerial, TV.iSiteID) AS IssuedStand,
	       TV.dtPrinted AS IssuedDate,
	       [dbo].FnGetZoneNameForSerial(PD.strSerial, PD.Site_Code) AS Zone_Name
	FROM   voucher TV
	       LEFT OUTER JOIN Device PD
	            ON  TV.ipaydeviceid = PD.ideviceid
	            AND TV.iPaySiteID = PD.Site_Code
	            AND PD.Site_Code = PD.iSiteID
				AND (@Site IS NULL OR PD.Site_Code = @Site_Code)
	            AND TV.strvoucherStatus = 'PD'
	       LEFT OUTER JOIN Device ID
	            ON  TV.ideviceid = ID.ideviceid
	            AND TV.ipaySiteID = ID.Site_Code
	            AND ID.Site_Code = ID.iSiteID
	       INNER JOIN SITE SI
	            ON  SI.SITE_CODE = TV.iPaySiteID
	       INNER JOIN Sub_Company SC
	            ON  SI.Sub_Company_ID = SC.Sub_Company_ID
	       INNER JOIN Company C
	            ON  C.Company_ID = SC.Company_ID
	WHERE  (
	           ISNULL(@Zone,0) = 0
	           OR (
	                  [dbo].FnGetZoneIDForSerial(PD.strSerial, PD.Site_Code) = @Zone
	              )
	       )
	       AND ISNULL(@District, SI.Sub_Company_District_Id) = SI.Sub_Company_District_Id
	       AND ISNULL(@Area, SI.Sub_Company_Area_Id) = SI.Sub_Company_Area_Id
	       AND ISNULL(@Region, SI.Sub_Company_Region_Id) = SI.Sub_Company_Region_Id
	       AND (ISNULL(@Site, SI.Site_ID) = SI.site_id)
	       AND (ISNULL(@SubCompany, SI.Sub_Company_ID) = SI.Sub_Company_ID)
	       AND (ISNULL(@Company, C.Company_ID) = C.Company_ID)
	       AND TV.strvoucherStatus = 'PD'
	       AND TV.dtPaid BETWEEN @StartDate AND @EndDate
	       AND (
	               ((@DeviceType = 'ALL' OR @DeviceType='--All--')AND TV.ipaydeviceid IS NOT NULL)
	               OR (
	                      @DeviceType = 'SLOT'
	                      AND TV.ipaydeviceid IS NOT NULL
	                      AND PD.strSerial IN (SELECT EM1.Machine_Stock_No
	                                           FROM   MACHINE EM1)
	                  )
	               OR (
	                      @DeviceType = 'CASHDESK'
	                      AND TV.ipaydeviceid IS NOT NULL
	                      AND PD.strSerial IN (SELECT TIW1.Site_Workstation
	                                           FROM   siteworkstations TIW1)
	                  )
	           )
	       AND (
	               @SiteIDList IS NOT NULL
	               AND SI.Site_ID IN (SELECT DATA
	                                  FROM   fnSplit(@SiteIDList, ','))
	           )
	ORDER BY
	       TV.dtPaid
END
GO

