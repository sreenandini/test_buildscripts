USE Enterprise
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects o
       WHERE  o.[name] = 'rsp_Report_TotalFundsIn'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_Report_TotalFundsIn
END
GO

/*
* Revision History
* 
* Anuradha        Created                 01 March 2013
* --rsp_Report_TotalFundsIn 0,0,0,0
*/


CREATE PROCEDURE rsp_Report_TotalFundsIn(
    @Company     INT = 0,
    @SubCompany  INT = 0,
    @Region      INT = 0,
    @Area        INT = 0,
    @District    INT = 0,
    @Site        INT = 0,
    @SiteIDList  VARCHAR(MAX)
)
AS
BEGIN
	SET NOCOUNT ON
	
	IF @Company = 0
	    SET @Company = NULL
	
	IF @SubCompany = 0
	    SET @SubCompany = NULL  
	
	IF @Site = 0
	    SET @Site = NULL
	
	IF @Region = 0
	    SET @Region = NULL
	
	IF @Area = 0
	    SET @Area = NULL
	
	IF @District = 0
	    SET @District = NULL
	
	
	;WITH TotalFundsIn AS
	(
	    SELECT S.Site_name AS SiteName,
	           S.Site_Code AS SiteCode,
	           MAX(F.Last_Drop_Date) AS LastDropDate,
	           SUM(F.CASH_IN_100P) AS Bill1,
	           SUM(CAST(F.CASH_IN_500P AS BIGINT) * 5) AS Bill5,
	           SUM(CAST(F.CASH_IN_1000P AS BIGINT) * 10) AS Bill10,
	           SUM(CAST(F.CASH_IN_5000P AS BIGINT) * 20) AS Bill20,
	           SUM(CAST(F.CASH_IN_10000P AS BIGINT) * 50) AS Bill50,
	           SUM(CAST(F.CASH_IN_10000P AS BIGINT) * 100) AS Bill100,
	           (
	               SUM(CAST(F.FUND_RDC_TICKETS_INSERTED_VALUE AS BIGINT)) / 100
	           ) AS FUND_RDC_TICKETS_INSERTED_VALUE,
	           (
	               SUM(CAST(F.RDC_TICKETS_INSERTED_NONCASHABLE_VALUE AS BIGINT)) 
	               / 100
	           ) AS RDC_TICKETS_INSERTED_NONCASHABLE_VALUE
	    FROM   tblFund F
	           INNER JOIN Installation i
	                ON  i.Installation_ID = F.Installation_no
	           INNER JOIN [Machine] M
	                ON  m.Machine_ID = i.Machine_ID
	           INNER JOIN [Site] S
	                ON  S.Site_code = F.Site_Code
	           INNER JOIN Bar_Position BP
	                ON  BP.Bar_Position_ID = I.Bar_Position_Id
	           INNER JOIN Sub_Company SC
	                ON  SC.Sub_Company_ID = S.Sub_Company_ID
	           INNER JOIN Company C
	                ON  C.Company_ID = SC.Company_ID
	    WHERE  i.Installation_End_Date IS NULL
	           AND (
	                   ISNULL(@District, S.Sub_Company_District_Id) = S.Sub_Company_District_Id
	                   AND ISNULL(@Area, S.Sub_Company_Area_Id) = S.Sub_Company_Area_Id
	                   AND ISNULL(@Region, S.Sub_Company_Region_Id) = S.Sub_Company_Region_Id
	                   AND ISNULL(@SubCompany, S.Sub_Company_Id) = S.Sub_Company_Id
	                   AND ISNULL(@Company, C.Company_Id) = C.Company_Id
	                       
	                       --AND ISNULL(@Site, S.Site_Id) = S.Site_Id
	                   AND (
	                           @SiteIDList IS NOT NULL
	                           AND S.Site_Id IN (SELECT DATA
	                                             FROM   dbo.fnSplit (@SiteIDList, ','))
	                       )
	               )
	    GROUP BY
	           S.Site_Name,
	           S.site_Code
	) ,
	
	TotalFundsInResult AS
	(
	    SELECT SiteName,
	           SiteCode,
	           LastDropDate,
	           CAST(
	               (Bill1 + Bill5 + Bill10 + Bill20 + Bill50 + Bill100)AS BIGINT
	           ) AS CashIn,
	           CAST(
	               FUND_RDC_TICKETS_INSERTED_VALUE +
	               RDC_TICKETS_INSERTED_NONCASHABLE_VALUE AS BIGINT
	           ) AS VouchersIn,
	           CAST(
	               Bill1 +
	               Bill5 +
	               Bill10 +
	               Bill20 +
	               Bill50 +
	               Bill100 + (
	                   FUND_RDC_TICKETS_INSERTED_VALUE +
	                   RDC_TICKETS_INSERTED_NONCASHABLE_VALUE
	               )
	               AS BIGINT
	           ) AS TotalFundsIn
	    FROM   TotalFundsIn
	)
	
	SELECT *
	FROM   TotalFundsInResult
	ORDER BY
	       TotalFundsIn DESC
END
GO

