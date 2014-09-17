USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetReportLiabilityTransferDetails]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetReportLiabilityTransferDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC rsp_GetReportLiabilityTransferDetails  
	@Company     INT = 0,    
	@SubCompany  INT = 0,
	@Region      INT = 0,
	@Area        INT = 0,
	@District    INT = 0,   
	@SiteId      INT = 0,       
	@StartDate	DATETIME,      
	@EndDate	DATETIME   
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
	
	IF @SiteID = 0
	    SET @SiteID = NULL
	
	
	SELECT TV.isiteID AS FromSIte,
	       TV.ipaysiteID AS ToSite,
	       TV.strBarCode AS ValidationNumber,
	       TV.dtPaid AS RenewedDate,
	       TV.iAmount AS Amount,
	       CASE TV.Ticket_Type
	            WHEN 0 THEN 'CASHABLE'
	            WHEN 2 THEN 'PROMO-CASHABLE'
	       END AS TicketType
	FROM   voucher TV(NOLOCK)
	       INNER JOIN [SITE] SI
	            ON  TV.iSiteid = SI.Site_Code
	       INNER JOIN Sub_Company SC
	            ON  SI.Sub_Company_ID = SC.Sub_Company_ID
	       INNER JOIN Company C
	            ON  SC.Company_ID = C.Company_ID
	WHERE  TV.strVoucherstatus = 'PD'
	       AND TV.dtPaid IS NOT NULL
	       AND TV.isiteID <> TV.iPaySiteID
	       AND ISNULL(@District, SI.Sub_Company_District_Id) = SI.Sub_Company_District_Id
	       AND ISNULL(@Area, SI.Sub_Company_Area_Id) = SI.Sub_Company_Area_Id
	       AND ISNULL(@Region, SI.Sub_Company_Region_Id) = SI.Sub_Company_Region_Id
	       AND ISNULL(@SubCompany, SI.Sub_Company_Id) = SI.Sub_Company_Id
	       AND ISNULL(@Company, C.Company_ID) = C.Company_ID
	       AND ISNULL(@SiteID, SI.Site_ID) = SI.Site_ID
	ORDER BY
	       TV.isiteID,
	       TV.iPaySiteID
END

GO

