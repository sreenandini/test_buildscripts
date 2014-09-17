USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[usp_UpdateBarPositionWithTermsInfo]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[usp_UpdateBarPositionWithTermsInfo]
GO
  
CREATE PROCEDURE [dbo].[usp_UpdateBarPositionWithTermsInfo]
	@ValidationFlag BIT,
	@PreDate DATETIME ,
	@PostDate DATETIME,
	@SiteID INT = 0,
	@CompanyID INT = 0,
	@SubCompanyID INT = 0
AS
BEGIN
	IF @SiteID = 0
	    SET @SiteID = NULL  
	
	IF @CompanyID = 0
	    SET @CompanyID = NULL  
	
	IF @SubCompanyID = 0
	    SET @SubCompanyID = NULL    
	
	DECLARE @SiteTermsGrpID AS INT   
	SELECT @SiteTermsGrpID = Terms_Group_ID
	FROM   [Site] 
	
	
	-- To update Bar Position details  
	UPDATE Bar_Position
	SET    Bar_Position_Use_Terms = @ValidationFlag,
	       Bar_Position_Rent_Past_Date = CONVERT(VARCHAR, @PreDate, 106),
	       Bar_Position_Share_Past_Date = CONVERT(VARCHAR, @PreDate, 106),
	       Bar_Position_Licence_Past_Date = CONVERT(VARCHAR, @PreDate, 106),
	       Bar_Position_Rent_Future_Date = CONVERT(VARCHAR, @PostDate, 106),
	       Bar_Position_Share_Future_Date = CONVERT(VARCHAR, @PostDate, 106),
	       Bar_Position_Licence_Future_Date = CONVERT(VARCHAR, @PostDate, 106)
	WHERE  (
	           (@SiteID IS NOT NULL AND Site_ID = @SiteID)
	           OR (
	                  @SiteID IS NULL
	                  AND (
	                          Site_ID IN (SELECT SITE_ID
	                                      FROM   SITE S
	                                             INNER JOIN Sub_Company SC
	                                                  ON  S.Sub_Company_ID = SC.Sub_Company_ID
	                                      WHERE  (
	                                                 (
	                                                     @SubCompanyID IS NOT 
	                                                     NULL
	                                                     OR SC.Sub_Company_ID =
	                                                        @SubCompanyID
	                                                 )
	                                                 OR (
	                                                        SC.Company_ID IN (SELECT 
	                                                                                 Company_ID
	                                                                          FROM   
	                                                                                 Company
	                                                                          WHERE  (@CompanyID IS NULL OR Company_ID = @CompanyID))
	                                                    )
	                                             ))
	                      )
	              )
	       ) 
	
	-- to update bar position terms group ID for which terms group is not assigned  
	UPDATE BP
	SET    BP.Terms_Group_ID = S.Terms_Group_ID
	FROM   Bar_Position BP
	       INNER JOIN SITE S
	            ON  S.Site_id = BP.Site_ID
	WHERE  BP.Terms_Group_ID IS NULL
	       OR  BP.Terms_Group_ID = 0
END
GO
