USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_REPORT_LicenseExpiry]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_REPORT_LicenseExpiry]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
  
--------------------------------------------------------------------------------------------------------------------------------------------------------------  
---------------------------------------------------------------------------------------------------------------------------  
---      
--- Description: Fetches the details from Licensing Expiry table  
---          
--- Inputs:           
--- Outputs:       
--- ======================================================================================================================      
---       
--- Revision History      
---       
--- Venkatesh Kumar J 16/04/12  Created       
------------------------------------------------------------------------------------------------------------------------    

CREATE PROCEDURE rsp_REPORT_LicenseExpiry(
    @Company     INT = 0,
    @SubCompany  INT = 0,
    @Region      INT = 0,
    @Area        INT = 0,
    @District    INT = 0,
    @Site        INT = 0,
    @StartDate   DATETIME,
    @EndDate     DATETIME,
    @SiteIDList VARCHAR(MAX)
)
AS
BEGIN
	SET @StartDate = DATEADD(d, DATEDIFF(d, 0, @startdate), 0)
	SET @EndDate = DATEADD(d, DATEDIFF(d, 0, @EndDate), '23:59:59')
	
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
	
	DECLARE @StatusId INT 
	SET @StatusId = 4
	
	SELECT ST.Site_Name + ' [' + ST.Site_Code + ']' AS [Site Code],
	       LicenseKey = CASE KS.KeyText 
	                            WHEN 'Active' 
	                          THEN '**********'	                        
								WHEN 'Created' 
							  THEN '*********'
	                            ELSE LI.LicenseKey
                              
                        END,
	       LI.StartDate,
	       LI.ExpireDate,
	       S.Staff_Last_Name + ',' + S.Staff_First_Name AS [Staff Name],
	       SR.RuleName,
	       DATEDIFF(dd, GETDATE(), LI.ExpireDate) AS DaysRemaining
	FROM   sl_LicenseInfo LI
		   INNER JOIN [dbo].SL_KeyStatus KS
				ON  LI.KeyStatusID = KS.KeyStatusID
	       INNER JOIN Staff s
	            ON  S.Staff_ID = LI.CreatedStaffID
	       INNER JOIN SL_Rules SR
	            ON  LI.RuleID = SR.RuleID
	       INNER JOIN [dbo].Site ST
	            ON  ST.Site_ID = LI.Site_ID
	       INNER JOIN [dbo].Sub_Company SC
	            ON  SC.Sub_Company_ID = ST.Sub_Company_ID
	       INNER JOIN [dbo].Company
	            ON  Company.Company_ID = SC.Company_ID
	WHERE  CAST(LI.UpdatedDateTime AS DATETIME) BETWEEN CONVERT(DATETIME, @StartDate, 103) 
	       AND 
	       CONVERT(DATETIME, @EndDate, 103)
	       AND LI.KeyStatusId <> @StatusId
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
	ORDER BY
	       ST.Site_ID ASC
END
GO

