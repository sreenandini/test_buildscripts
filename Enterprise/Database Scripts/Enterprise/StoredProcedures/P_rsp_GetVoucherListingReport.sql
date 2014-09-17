USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetVoucherListingReport]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetVoucherListingReport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--  
-- Description: Fetches the issued Voucher/ticket records
--  
-- Inputs:  StartDate,EndDate,Status,Slot
-- Outputs:       
--  
-- =======================================================================  
--   
-- Revision History  

-- Kirubakar S 11/03/2010 Created 
  
--Durga Devi M 22/06/2012 Modified
/*
***Changes***
 1. Check the condition for ignoring 'LT' status record
 2. ZoneID parameter is used instead of Zone Name
*/

  
--------------------------------------------------------------------------- 

CREATE PROCEDURE rsp_GetVoucherListingReport
	@Company		INT = 0,
	@SubCompany		INT = 0,
	@Region			INT = 0,
	@Area			INT = 0,
	@District		INT = 0,
	@Site			INT = 0,
	@Zone			INT = 0,
	@StartDate		DATETIME,
	@EndDate		DATETIME,
	@VoucherStatus	VARCHAR(25),
	@Slot			VARCHAR(50),
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
	
	IF NOT EXISTS(
	       SELECT 1
	       FROM   Zone
	       WHERE  Zone_ID = @Zone
	   )
	BEGIN
	    SET @Zone = NULL
	END
	
	SELECT DISTINCT 
	       SI.Site_Name AS SiteName,
	       --  TD.ideviceid as SLOT,       
	       TD.strSerial AS Asset,
	       TV.dtPrinted AS IssueDate,
	       TV.strBarCode AS ValidationNumber,
	       dbo.StatusCodeDescription(TV.strVoucherStatus, TV.dtExpire) AS Status,
	       dbo.compute_decimal(TV.iAmount) AS Amount,
	       CASE Ticket_Type
	            WHEN 0 THEN 'CASHABLE'
	            WHEN 1 THEN 'NON CASHABLE'
	       END AS VoucherType,
	       --  ZONE.Zone_Name     
	       [dbo].FnGetZoneNameForSerial(TD.strSerial, TD.Site_Code) AS Zone_Name
	FROM   Voucher TV WITH (NOLOCK)
	       INNER JOIN [SITE] SI WITH (NOLOCK)
	            ON  TV.iSiteid = SI.Site_Code
	       INNER JOIN Sub_Company SC
	            ON  Si.Sub_Company_ID = SC.Sub_Company_ID
	       INNER JOIN Company C
	            ON  C.Company_ID = SC.Company_ID
	       INNER JOIN Device TD WITH (NOLOCK)
	            ON  TV.ideviceid = TD.ideviceid
	            AND TV.iSiteId = TD.Site_Code
	            AND (
	                    TD.iSiteid = TD.Site_Code
	                    OR ISNULL(TD.iSiteid, 1000) = 1000
	                )
	       INNER JOIN (
	                SELECT DISTINCT(Machine_Stock_No)
	                FROM   MACHINE WITH (NOLOCK)
	            ) EM
	            ON  EM.Machine_Stock_No = TD.strSerial --AND EM.Machine_ID=I.Machine_ID
	                                                   --   INNER JOIN Installation I ON I.Machine_ID=EM.Machine_ID
	                                                   -- INNER JOIN Bar_Position BP ON BP.Bar_Position_ID=I.Bar_Position_ID
	                                                   --LEFT OUTER JOIN ZONE ON ZONE.SITE_ID = BP.SITE_ID
	WHERE  (
	           (ISNULL(@SubCompany, SI.Sub_Company_ID) = SI.Sub_Company_ID)
	           AND (ISNULL(@Company, C.Company_ID) = C.Company_ID)
	           AND (ISNULL(@Site, SI.Site_ID) = SI.Site_ID)
	       )
	       AND ISNULL(@District, SI.Sub_Company_District_Id) = SI.Sub_Company_District_Id
	       AND ISNULL(@Area, SI.Sub_Company_Area_Id) = SI.Sub_Company_Area_Id
	       AND ISNULL(@Region, SI.Sub_Company_Region_Id) = SI.Sub_Company_Region_Id
	       AND (
	               @Zone IS NULL
	               OR (
	                      [dbo].FnGetZoneIDForSerial(TD.strSerial, TD.Site_Code)
	                      = @Zone
	                  )
	           )
	       AND dtPrinted BETWEEN @StartDate AND @EndDate
	       AND (
	               (
	                   (@VoucherStatus = 'ALL' OR @VoucherStatus = '--All--')
	                   AND ISNULL(TV.strVoucherStatus, '') <> 'LT'
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
	               (
	                   (@Slot != '--ALL--' OR @Slot != 'ALL')
	                   AND TV.ideviceid IS NOT NULL
	                   AND TD.strserial = @Slot
	               )
	               OR (
	                      (@Slot = '--ALL--' OR @Slot = 'ALL')
	                      AND TV.ideviceid IS NOT NULL
	                      AND TD.strSerial IN (SELECT Machine_Stock_No
	                                           FROM   MACHINE
	                                           WHERE  machine_id IN (SELECT 
	                                                                        Machine_ID
	                                                                 FROM   
	                                                                        INSTALLATION
	                                                                 WHERE  
	                                                                        Bar_Position_ID IN (SELECT 
	                                                                                                   Bar_Position_ID
	                                                                                            FROM   
	                                                                                                   BAR_POSITION
	                                                                                            WHERE  (@Site IS NULL OR SITE_ID = @Site))))
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
	       TD.strSerial,
	       TV.dtPrinted,
	       TV.strBarCode,
	       TV.strVoucherStatus,
	       TV.dtExpire,
	       TV.iAmount,
	       TV.Ticket_Type
	       
	       --ZONE.Zone_Name
	ORDER BY
	       STATUS,
	       IssueDate
END
GO
