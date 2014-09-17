USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[RSP_GETREPORTLIABILITYTRANSFERSDETAILS]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[RSP_GETREPORTLIABILITYTRANSFERSDETAILS]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/****** Object:  StoredProcedure [dbo].[RSP_GETREPORTLIABILITYTRANSFERSDETAILS]      
 Script Date: 04/07/2012 04:02:36   
 Created By: Kirubakar S******/  
CREATE PROCEDURE [dbo].[RSP_GETREPORTLIABILITYTRANSFERSDETAILS]
	@Company     INT = 0,
	@SubCompany  INT = 0,
	@Region      INT = 0,
	@Area        INT = 0,
	@District    INT = 0,
	@Site        INT = 0,
	@StartDate   DATETIME,
	@EndDate     DATETIME ,
	@SiteIDList  VARCHAR(MAX)
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
	
	SELECT TV.isiteID AS FromSIte,
	       TV.ipaysiteID AS ToSite,
	       TV.strBarCode AS ValidationNumber,
	       TV.dtPaid AS RenewedDate,
	       ISNULL(
	           CAST(CAST(iAmount AS DECIMAL(10, 2)) / 100 AS DECIMAL(10, 2)),
	           0
	       ) AS Amount,
	       CASE TV.Ticket_Type
	            WHEN 0 THEN 'CASHABLE'
	            WHEN 2 THEN 'PROMO-CASHABLE'
	       END AS TicketType,
	       C.Company_ID AS CompanyID,
	       C.Company_Name AS CompanyName,
	       SC.Sub_Company_ID AS SubCompanyID,
	       SC.Sub_Company_Name AS SubCompanyName
	FROM   Voucher TV
	       INNER JOIN SITE SI
	            ON  TV.iSiteid = SI.Site_Code
	       INNER JOIN Sub_Company SC
	            ON  SI.Sub_Company_ID = SC.Sub_Company_ID
	       INNER JOIN Company C
	            ON  SC.Company_ID = C.Company_ID
	WHERE  strvoucherstatus = 'LT'
	       AND TV.Ticket_Type IN (0, 2)
	       AND (ISNULL(@SubCompany, SI.Sub_Company_ID) = SI.Sub_Company_ID)
	       AND (ISNULL(@Company, C.Company_ID) = C.Company_ID)
	       AND (ISNULL(@Site, SI.Site_ID) = SI.Site_ID)
	       AND ISNULL(@District, SI.Sub_Company_District_Id) = SI.Sub_Company_District_Id
	       AND ISNULL(@Area, SI.Sub_Company_Area_Id) = SI.Sub_Company_Area_Id
	       AND ISNULL(@Region, SI.Sub_Company_Region_Id) = SI.Sub_Company_Region_Id
	           --AND (
	           --        (
	           --            @SITE = 0
	           --            AND TV.iSiteId IN (SELECT Site_Code
	           --                               FROM   SITE
	           --                               WHERE  (
	           --                                          (@SUBCOMPANY <> 0 AND Sub_Company_ID = @SUBCOMPANY)
	           --                                          OR (
	           --                                                 @SUBCOMPANY = 0
	           --                                                 AND Sub_Company_ID IN (SELECT
	           --                                                                               Sub_Company_ID
	           --                                                                        FROM
	           --                                                                               Sub_Company
	           --                                                                        WHERE
	           --                                                                               Company_ID =
	           --                                                                               @COMPANY)
	           --                                             )
	           --                                      ))
	           --        )
	           --        OR TV.iSiteId = (
	           --               SELECT site_code
	           --               FROM   SITE
	           --               WHERE  Site_ID = @SITE
	           --           )
	           --    )
	       AND TV.dtPaid BETWEEN @StartDate AND @EndDate
	       AND (
	               @SiteIDList IS NOT NULL
	               AND SI.Site_ID IN (SELECT DATA
	                                  FROM   fnSplit(@SiteIDList, ','))
	           )
END
GO

