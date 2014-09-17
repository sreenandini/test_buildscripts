USE Enterprise
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects o
       WHERE  o.[name] = 'rsp_Report_TotalFundsInDetails'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_Report_TotalFundsInDetails
END
GO


/*
* Revision History
* 
* Anuradha        Created                 01 March 2013
* --rsp_Report_TotalFundsInDetails 0,0,0,0
*/


CREATE PROCEDURE rsp_Report_TotalFundsInDetails(
    @Company     INT = 0,
    @SubCompany  INT = 0,
    @Region      INT = 0,
    @Area        INT = 0,
    @District    INT = 0,
    @Site        INT = 0,
    @Zone        INT = 0,
    @SiteIDList  VARCHAR(MAX)
)
AS
BEGIN

	SET NOCOUNT ON
	
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
	
	
	;WITH TotalFundsInDetails AS
	(
	    SELECT S.Site_Name AS SiteName,
	           S.Site_Code AS SiteCode,
	           Z.Zone_Name,
	           M.Machine_Stock_No AS Asset,
	           BP.Bar_Position_Name AS PosName,
	           (CAST(SUM(isnull(F.CASH_IN_100P,0)) AS FLOAT)) AS Bill1,
	           (CAST(SUM(ISNULL(F.CASH_IN_500P,0)) AS FLOAT) * 5) AS Bill5,
	           (CAST(SUM(ISNULL(F.CASH_IN_1000P,0)) AS FLOAT) * 10) AS Bill10,
	           (CAST(SUM(ISNULL(F.CASH_IN_2000P,0)) AS FLOAT) * 20) AS Bill20,
	           (CAST(SUM(ISNULL(F.CASH_IN_5000P,0)) AS FLOAT) * 50) AS Bill50,
	           (CAST(SUM(ISNULL(F.CASH_IN_10000P,0)) AS FLOAT) * 100) AS Bill100,
	           F.Last_Drop_Date AS LastDropDate,
	           (
	               CAST(SUM(ISNULL(F.FUND_RDC_TICKETS_INSERTED_VALUE,0)) AS FLOAT) / 100
	           ) AS FUND_RDC_TICKETS_INSERTED_VALUE,
	           (
	               CAST(SUM(ISNULL(F.RDC_TICKETS_INSERTED_NONCASHABLE_VALUE,0)) AS FLOAT) 
	               / 100
	           ) AS RDC_TICKETS_INSERTED_NONCASHABLE_VALUE
	    FROM   [tblFund] F
	           INNER JOIN Installation i
	                ON  i.Installation_ID = F.Installation_no
	           INNER JOIN [Machine] M
	                ON  M.Machine_ID = i.Machine_ID
	           INNER JOIN [Site] S
	                ON  S.Site_code = F.Site_Code
	           INNER JOIN Bar_Position BP
	                ON  BP.Bar_position_id = i.Bar_Position_ID
	           INNER JOIN Sub_Company SC
	                ON  SC.Sub_Company_ID = S.Sub_Company_ID
	           INNER JOIN Company C
	                ON  C.Company_ID = SC.Company_ID
	           LEFT JOIN Zone Z
	                ON  Z.Zone_ID = BP.Zone_ID
	    WHERE  i.Installation_End_Date IS NULL
	           AND (
	                   ISNULL(@Region, S.Sub_Company_REGION_ID) = S.Sub_Company_REGION_ID
	                   AND ISNULL(@Area, S.Sub_Company_Area_ID) = S.Sub_Company_Area_Id
	                   AND ISNULL(@District, S.Sub_Company_Area_ID) = S.Sub_Company_District_Id
	                   AND ISNULL(@Company, C.Company_Id) = C.Company_Id
	                   AND ISNULL(@Subcompany, S.Sub_Company_Id) = S.Sub_Company_Id
	                   AND (
	                           @SiteIDList IS NOT NULL
	                           AND S.Site_Id IN (SELECT DATA
	                                             FROM   dbo.fnSplit (@SiteIDList, ','))
	                       )
	                   AND ISNULL(@Zone, Z.Zone_ID) = Z.Zone_ID
	               )
	    GROUP BY
	           S.Site_Name,
	           F.Last_Drop_Date,
	           BP.Bar_Position_Name,
	           M.Machine_Stock_No,
	           S.Site_Code,
	           Z.Zone_Name
	)
	
	SELECT SiteName,
	       SiteCode,
	       Zone_Name,
	       Asset,
	       PosName,
	       Bill1,
	       Bill5,
	       Bill10,
	       Bill20,
	       Bill50,
	       Bill100,
	      CAST( (Bill1 + Bill5 + Bill10 + Bill20 + Bill50 + Bill100) as bigint)AS CashIn,
	       LastDropDate,
	      CAST (
	          ( FUND_RDC_TICKETS_INSERTED_VALUE + 
	           RDC_TICKETS_INSERTED_NONCASHABLE_VALUE) AS BIGINT) AS VouchersIn,
	     CAST(
	         Bill1 +
	           Bill5 +
	           Bill10 +
	           Bill20 +
	           Bill50 +
	           Bill100 +(
	               FUND_RDC_TICKETS_INSERTED_VALUE + 
	               RDC_TICKETS_INSERTED_NONCASHABLE_VALUE
	           )
	     AS BIGINT  )  AS TotalFundsIn
	FROM   TotalFundsInDetails
END
GO

