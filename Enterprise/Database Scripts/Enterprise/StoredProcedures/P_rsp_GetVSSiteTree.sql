/************************************************************
 * Code formatted by SoftTree SQL Assistant © v4.8.29
 * Time: 06/05/13 6:13:42 PM
 ************************************************************/

/* ================================================================================= 
 * Purpose	:	rsp_GetVSSiteTree
 * Author	:	A.Vinod Kumar
 * Created  :	25/02/2013
 * ================================================================================= 
 * Revision History :
 * ================================================================================= 
 * 25/02/2013  	A.Vinod Kumar    Initial Version
 * ================================================================================= 
*/
/*USE the DATABASE*/
USE Enterprise
GO

-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_GetVSSiteTree'
   )
    DROP PROCEDURE dbo.rsp_GetVSSiteTree
GO

--EXEC dbo.rsp_GetVSSiteTree 1, 0
CREATE PROCEDURE dbo.rsp_GetVSSiteTree
(
    @UserId                   INT,
    @ActiveSites              BIT = 0,
    @Search                   VARCHAR(512) = NULL,
    @HasSupplier              BIT = 0,
    @HasAddress               BIT = 0,
    @HasSiteRep               BIT = 0,
    @BarPositionId            INT = NULL,
    @CompanyID                INT = NULL,
    @Sub_Company_ID           INT = NULL,
    @Sub_Company_Region_ID    INT = NULL,
    @Sub_Company_Area_ID      INT = NULL,
    @Sub_Company_District_ID  INT = NULL,
    @Operator_ID              INT = NULL,
    @Depot_ID                 INT = NULL,
    @Machine_Type_ID          INT = NULL,
    @Manufacturer_ID          INT = NULL,
    @SiteRepID                INT = NULL,
    @ExcludeVacant            BIT = 0,
    @ModelSearch              VARCHAR(512) = NULL,
    @PayoutPercentageFrom     REAL = NULL,
    @PayoutPercentageTo       REAL = NULL
)
AS
BEGIN
	SET NOCOUNT ON
	SET ANSI_NULLS OFF
	
	DECLARE @HasPrefix  BIT,
	        @HasSearch  BIT
	
	SET @HasPrefix = 0
	SET @HasSearch = 0	
	
	SELECT @HasPrefix = 1
	WHERE  @HasSupplier = 1
	       OR  @HasAddress = 1
	       OR  @HasSiteRep = 1
	       OR  @BarPositionId IS NOT NULL
	
	IF (@Search = '')
	    SET @Search = NULL
	IF (@ModelSearch = '')
	    SET @ModelSearch = NULL
	
	SELECT @HasSearch = 1
	WHERE  ISNULL(@Search, '') <> ''
	
	--SELECT @HasPrefix AS '@HasPrefix',
	--       @HasSearch AS '@HasSearch',
	--       @HasSupplier AS '@HasSupplier',
	--       @HasAddress AS '@HasAddress',
	--       @HasSiteRep AS '@HasSiteRep',
	--       @BarPositionId AS '@BarPositionId'
	
	SELECT DISTINCT CC.Company_ID,
	       CC.Company_Name,
	       SC.Sub_Company_ID,
	       SC.Sub_Company_Name,
	       ST.Site_ID,
	       ST.Site_Name,
	       ST.Site_Code,
	       ST.Site_Address_1,
	       ST.Site_Address_2,
	       ST.Site_Address_3,
	       ISNULL(ST.Site_status_ID, 0) AS Site_Status_Id,
	       ST.Site_Inactive_Date,
	       ST.WebURL
	FROM   SITE ST WITH(NOLOCK)
	       INNER JOIN Sub_Company SC WITH(NOLOCK)
	            ON  ST.Sub_Company_ID = SC.Sub_Company_ID
	       INNER JOIN Company CC WITH(NOLOCK)
	            ON  SC.Company_ID = CC.Company_ID
	       LEFT JOIN Bar_Position BP WITH(NOLOCK)
	            ON  ST.Site_ID = BP.Site_ID
	       LEFT JOIN Depot DP WITH(NOLOCK)
	            ON  BP.Depot_ID = DP.Depot_ID
	       LEFT JOIN Operator OP WITH(NOLOCK)
	            ON  DP.Supplier_ID = OP.Operator_ID
	       LEFT JOIN Installation INS WITH(NOLOCK)
	            ON  BP.Bar_Position_ID = INS.Bar_Position_ID
	       LEFT JOIN MACHINE MM WITH(NOLOCK)
	            ON  INS.Machine_ID = MM.Machine_ID
	       LEFT JOIN Machine_Class MC WITH(NOLOCK)
	            ON  MM.Machine_Class_ID = MC.Machine_Class_ID
	       INNER JOIN SecurityProfile SP
	            ON  SP.SecurityProfileType_Value = ST.Site_ID
	            AND AllowUser = 1
	            AND SecurityProfileType_ID = 2
	       INNER JOIN STAFF_CUSTOMER_ACCESS SCA
	            ON  SP.Customer_Access_ID = SCA. Customer_Access_ID	            
	       INNER JOIN STAFF S
	            ON  S.staff_id = SCA.staff_id                
	                --Added below for UserBased Site Access  	                
	       INNER JOIN [USER] U
	            ON  U.SecurityUserID = S.UserTableID
	       INNER JOIN [UserRole_lnk] URL
	            ON  URL.SecurityUserID = U.SecurityUserID
	       INNER JOIN [ROLE] R
	            ON  R.SecurityRoleID = URL.SecurityRoleID	  
	WHERE  ((R.RoleName = 'SUPER USER' AND URL.SecurityUserID = @Userid)
	       OR  (
	               R.RoleName <> 'SUPER USER'
	               AND U.SecurityUserID = @Userid
	               
	           ))
	       AND ST.Site_End_Date IS NULL
	       AND (
	               @BarPositionId IS NULL
	               OR (bp.Bar_Position_ID = @BarPositionId)
	           )
	       AND (
	               0 = @HasSupplier
	               OR (
	                      ST.Site_Name LIKE @Search
	                      OR BP.Bar_Position_Supplier_Site_Code LIKE @Search
	                  )
	           )
	       AND (
	               0 = @HasAddress
	               OR (
	                      ST.Site_Name LIKE @Search
	                      OR ST.Site_Postcode LIKE @Search
	                      OR ST.Site_Address_1 LIKE @Search
	                      OR ST.Site_Address_2 LIKE @Search
	                      OR ST.Site_Address_3 LIKE @Search
	                      OR ST.Site_Address_4 LIKE @Search
	                      OR ST.Site_Address_5 LIKE @Search
	                      OR ST.Site_Phone_No LIKE @Search
	                  )
	           )
	       AND (
	               0 = @HasSiteRep
	               OR (
	                      ST.Site_Reference LIKE @Search
	                      OR ST.Site_Company_Code LIKE @Search
	                  )
	           )
	       AND (
	               (1 = @HasPrefix)
	               OR (0 = @HasSearch)
	               OR (ST.Site_Name LIKE @Search OR ST.Site_Code LIKE @Search)
	           )
	       AND (
	               0 = @ActiveSites
	               OR (
	                      ISNULL(ST.Site_Status_ID, 0) = 0
	                      OR ST.Site_Inactive_Date > GETDATE()
	                  )
	           )
	       AND (@CompanyID IS NULL OR (SC.Company_ID = @CompanyID))
	       AND (
	               @Sub_Company_ID IS NULL
	               OR (ST.Sub_Company_ID = @Sub_Company_ID)
	           )
	       AND (
	               @Sub_Company_Region_ID IS NULL
	               OR (ST.Sub_Company_Region_ID = @Sub_Company_Region_ID)
	           )
	       AND (
	               @Sub_Company_Area_ID IS NULL
	               OR (ST.Sub_Company_Area_ID = @Sub_Company_Area_ID)
	           )
	       AND (
	               @Sub_Company_District_ID IS NULL
	               OR (ST.Sub_Company_District_ID = @Sub_Company_District_ID)
	           )
	       AND (@Operator_ID IS NULL OR (OP.Operator_ID = @Operator_ID))
	       AND (@Depot_ID IS NULL OR (BP.Depot_ID = @Depot_ID))
	       AND (
	               @Machine_Type_ID IS NULL
	               OR (MC.Machine_Type_ID = @Machine_Type_ID)
	           )
	       AND (
	               @Manufacturer_ID IS NULL
	               OR (MC.Manufacturer_ID = @Manufacturer_ID)
	           )
	       AND (
	               @ExcludeVacant = 0
	               OR (
	                      ST.Site_ID IN (SELECT DISTINCT Bar_Position.Site_ID
	                                     FROM   Installation WITH(NOLOCK)
	                                            INNER JOIN Bar_Position WITH(NOLOCK)
	                                                 ON  Installation.Bar_Position_ID = Bar_Position.Bar_Position_ID
	                                     WHERE  Installation_End_Date IS NULL)
	                  )
	           )
	       AND (@SiteRepID IS NULL OR (ST.Staff_ID = @SiteRepID))
	       AND (
	               @ModelSearch IS NULL
	               OR (
	                      (
	                          (MC.Machine_Name LIKE @ModelSearch)
	                          OR (MC.Machine_Class_Model_Code LIKE @ModelSearch)
	                      )
	                      AND INS.Installation_End_Date IS NULL
	                  )
	           )
	       AND (
	               @PayoutPercentageFrom IS NULL
	               OR (
	                      Installation_Percentage_Payout > @PayoutPercentageFrom
	                      AND INS.Installation_End_Date IS NULL
	                  )
	           )
	       AND (
	               @PayoutPercentageTo IS NULL
	               OR (
	                      Installation_Percentage_Payout < @PayoutPercentageTo
	                      AND INS.Installation_End_Date IS NULL
	                  )
	           )
	ORDER BY
	       CC.Company_Name ASC,
	       CC.Company_ID ASC,
	       SC.Sub_Company_Name ASC,
	       SC.Sub_Company_ID ASC,
	       ST.Site_Name ASC,
	       ST.Site_Code ASC,
	       ST.Site_ID ASC
	
	SET ANSI_NULLS ON
	SET NOCOUNT OFF
END
GO