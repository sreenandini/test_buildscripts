USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_REPORT_SiteLicenseHistory]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_REPORT_SiteLicenseHistory]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------    
---    
--- Description: Fetches the details from Site Licensing table
---        
--- Inputs:
/*
   EXEC rsp_REPORT_SiteLicenseHistory @company = 2,
     @subcompany = 0,
     @region = 0,
     @area = 0,
     @district = 0,
     @site = 0,
     @RuleName = 'TEST',
     @startdate = '2013-01-01 12:35:33',
     @enddate = '2014-01-15 12:35:33' 
     */        
--- Outputs:     
--- ======================================================================================================================    
---     
--- Revision History    
---
--- Venkatesh Kumar J		11/04/12	  Created   
--  Aishwarrya V S			15/01/14	  Modified  
------------------------------------------------------------------------------------------------------------------------    

CREATE PROCEDURE rsp_REPORT_SiteLicenseHistory(
    @Company     INT = 0,
    @SubCompany  INT = 0,
    @Region      INT = 0,
    @Area        INT = 0,
    @District    INT = 0,
    @Site        INT = 0,
    @RuleName    VARCHAR(50) = NULL,
    @StartDate   DATETIME,
    @EndDate     DATETIME,
    @SiteIDList  VARCHAR(MAX)
)
AS
BEGIN
	SET @startdate = DATEADD(d, DATEDIFF(d, 0, @startdate), 0)
	SET @enddate = DATEADD(d, DATEDIFF(d, 0, @enddate), '23:59:59')
	
	SET NOCOUNT ON
	
	IF @company = 0
	    SET @company = NULL
	
	IF @subcompany = 0
	    SET @subcompany = NULL
	
	IF @region = 0
	    SET @region = NULL
	
	IF @area = 0
	    SET @area = NULL
	
	IF @district = 0
	    SET @district = NULL
	
	IF @site = 0
	    SET @site = NULL  
	
	IF @RuleName = '--ALL--' OR @RuleName = 'All'
	    SET @RuleName = NULL
	
	
	SELECT ST.Site_Name + ' [' + ST.Site_Code + ']' AS [Site Code],
	       [License Key] = CASE KS.KeyText 
	                            WHEN 'Active' 
	                          THEN '**********'	                        
								WHEN 'Created' 
							  THEN '*********'
	                            ELSE LI.LicenseKey
	                       END,
	       KS.KeyText AS [License Status],
	       LI.CreatedDateTime AS [Creation Date],
	       Staff.Staff_Last_name + ',' + Staff.Staff_First_name AS [Created By],
	       (CONVERT(VARCHAR(30), LI.ActivatedDateTime, 101)) AS 
	       [Activation Date],
	       LI.ExpireDate AS [Expiry Date],
	       CancelledBy = CASE 
	                          WHEN ISNULL(CancelledStaffID, '') <> '' THEN (
	                                   SELECT Staff.Staff_Last_name + ',' +
	                                          Staff.Staff_First_name AS 
	                                          CancelledName
	                                   FROM   Staff
	                                   WHERE  Staff_ID = CancelledStaffID
	                               )
	                          ELSE '-'
	                     END,
	       ActivatedBy = CASE 
	                          WHEN ISNULL(ActivatedStaffId, '') <> '' THEN (
	                                   SELECT Staff.Staff_Last_name + ',' +
	                                          Staff.Staff_First_name AS 
	                                          CancelledName
	                                   FROM   Staff
	                                   WHERE  Staff_ID = ActivatedStaffId
	                               )
	                          ELSE '-'
	                     END,
	       (CONVERT(VARCHAR(30), LI.CancelledDateTime, 101)) AS 
	       CancelledDateTime,
	       sr.RuleName
	FROM   [dbo].SL_LicenseInfo LI
	       INNER JOIN [dbo].SL_KeyStatus KS
	            ON  LI.KeyStatusID = KS.KeyStatusID
	       INNER JOIN [dbo].Site ST
	            ON  LI.Site_ID = ST.Site_ID
	       INNER JOIN SL_Rules sr
	            ON  sr.RuleID = li.RuleID
	       INNER JOIN [dbo].Sub_Company SC
	            ON  SC.Sub_Company_ID = ST.Sub_Company_ID
	       INNER JOIN [dbo].Company
	            ON  Company.Company_ID = SC.Company_ID
	       INNER JOIN [dbo].[Staff]
	            ON  Staff.Staff_ID = LI.CreatedStaffID
	WHERE  CAST(LI.UpdatedDateTime AS DATETIME) BETWEEN CONVERT(DATETIME, @startdate, 103) 
	       AND 
	       CONVERT(DATETIME, @enddate, 103)
	       AND (
	               (@company IS NULL)
	               OR (@company IS NOT NULL AND Company.Company_ID = @company)
	           )
	       AND (
	               (@subcompany IS NULL)
	               OR (
	                      @subcompany IS NOT NULL
	                      AND ST.Sub_Company_ID = @subcompany
	                  )
	           )
	       AND (
	               (@region IS NULL)
	               OR (@region IS NOT NULL AND ST.sub_company_region_id = @region)
	           )
	       AND (
	               (@area IS NULL)
	               OR (@area IS NOT NULL AND ST.Sub_Company_Area_ID = @area)
	           )
	       AND (
	               (@district IS NULL)
	               OR (
	                      @district IS NOT NULL
	                      AND ST.Sub_Company_District_ID = @district
	                  )
	           )
	           --AND (
	           --        (@site IS NULL)
	           --        OR (@site IS NOT NULL AND ST.Site_ID = @site)
	           --    )
	       AND (
	               @SiteIDList IS NOT NULL
	               AND ST.Site_Id IN (SELECT DATA
	                                  FROM   dbo.fnSplit (@SiteIDList, ','))
	           )
	       AND (
	               (@RuleName IS NULL)
	               OR (@RuleName IS NOT NULL AND SR.RuleName = @RuleName)
	           )
	ORDER BY
	       ST.Site_ID ASC,
	       LI.UpdatedDateTime ASC
END
GO


