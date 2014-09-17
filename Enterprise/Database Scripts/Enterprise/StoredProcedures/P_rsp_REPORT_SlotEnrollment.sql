USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_REPORT_SlotEnrollment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_REPORT_SlotEnrollment]
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
--- Description: Fetches the details from Installation table  
---          
--- Inputs:           
--- Outputs:       
--- ======================================================================================================================      
---       
--- Revision History      
---       
--- Venkatesh Kumar J  19/04/12   Created    
--  Venkatesan Haridass 12/10/12 Modified to get data more than 100 days    
-- [rsp_REPORT_SlotEnrollment] 0,0,0,0,0,0,'2013-12-01','2014-01-03' 
------------------------------------------------------------------------------------------------------------------------      
      
CREATE PROCEDURE [dbo].[rsp_REPORT_SlotEnrollment]
(
    @Company     INT = 0,
    @SubCompany  INT = 0,
    @Region      INT = 0,
    @Area        INT = 0,
    @District    INT = 0,
    @Site        INT = 0,
    @StartDate   DATETIME,
    @EndDate     DATETIME,
    @SiteIDList  VARCHAR(MAX)
)
AS
BEGIN
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

      CREATE TABLE #tAllDates
      (
            SNo                 INT,
            Site_ID             INT,
            Date                DATETIME,
            NumberOfEnrollment  INT,
            [Difference]        INT
      );      

      DECLARE @tInstallations TABLE (SNo INT, Site_Id INT, No_Of_Installation INT) 

      --***** Created Common Table Expression to Insert Date between Start and End date   

      ;WITH cte_AllDates(SNo, Date) 
      AS 
      (
          SELECT 1 AS SNo,
                 @StartDate AS [DATE] 
          UNION ALL      
          SELECT SNo + 1,
                 DATEADD(dd, 1, [DATE])
          FROM cte_AllDates
          WHERE  DATEADD(dd, 1, [DATE]) <= @EndDate
      )      

      INSERT INTO #tAllDates
        (
          SNo,
          Date,
          Site_ID
        )
      SELECT SNo,
             Date,
             Site_ID
      FROM   cte_AllDates,
             [dbo].SITE 
      OPTION(MAXRECURSION 32767) 

      --******** Retreiving Installation Details  

      ; WITH CTE_GetInstallations(Installation_Start_Date, Installation_End_Date, Site_ID) 
      AS 
      (
          SELECT Installation.Installation_Start_Date,
                 Installation.Installation_End_Date,
                 SITE.Site_ID
          FROM   [dbo].SITE
                 INNER JOIN [dbo].Bar_Position
                      ON  Bar_Position.Site_ID = SITE.Site_ID
                 INNER JOIN [dbo].Installation
                      ON  Installation.Bar_Position_ID = Bar_Position.Bar_Position_ID
                 INNER JOIN [dbo].MACHINE
                      ON  MACHINE.Machine_ID = Installation.Machine_ID
          GROUP BY
                 MACHINE.Machine_ID,
                 Installation.Installation_ID, 
                 Installation.Installation_Start_Date,
                 Installation.Installation_End_Date,
                 SITE.Site_ID
      )     

      INSERT INTO @tInstallations
        (
          SNo,
          Site_ID,
          No_Of_Installation
        )
      SELECT DISTINCT SNo,
             CTE_GetInstallations.Site_ID,
             COUNT(*) NO_OF_INSTALLATIONS
      FROM   CTE_GetInstallations
             INNER JOIN #tAllDates
                  ON  CAST(#tAllDates.Date AS DATETIME) 
                      BETWEEN CAST(CTE_GetInstallations.Installation_Start_Date AS DATETIME) 
                      AND ISNULL(
                          CAST(CTE_GetInstallations.Installation_End_Date AS DATETIME),
                          GETDATE()
                      )
                  AND CTE_GetInstallations.Site_ID = #tAllDates.Site_ID
      GROUP BY
             CTE_GetInstallations.Site_ID,
             CAST(#tAllDates.Date AS DATETIME),
             SNo 

      --****** Updating Number of Installations in Temporary Table  

      UPDATE A
      SET    NumberOfEnrollment = B.No_Of_Installation
      FROM   #tAllDates A
             INNER JOIN @tInstallations B
                  ON  A.SNo = B.SNo
                  AND A.Site_ID = B.Site_ID 

      --****** Fetching values Between #tAllDates and @tInstallations to calculate Difference  

      SELECT A.SNo,
             SITE.Site_Name + ' ['+ SITE.Site_Code +']' AS [Site Code],
             A.Date,
             ISNULL(A.NumberOfEnrollment, 0) NumberOfEnrollment,
             ISNULL(
                 NumberOfEnrollment -ISNULL(
                     (
                         SELECT TOP 1 NumberOfEnrollment
                         FROM   #tAllDates B
                         WHERE  B.Site_id = A.Site_ID
                                AND B.SNo = A.SNo -1
                     ),
                     0
                 ),
                 0
             ) AS [Difference]

      FROM   #tAllDates A
             INNER JOIN [dbo].SITE
                  ON SITE.Site_ID = A.Site_ID AND LTRIM(RTRIM(ISNULL(SITE.SiteStatus,''))) = 'FULLYCONFIGURED'
             INNER JOIN [dbo].SUB_COMPANY
                  ON SITE.Sub_Company_ID=SUB_COMPANY.Sub_Company_ID 
		     WHERE
				 ( ( @company IS NULL )       
				 OR    
				   ( @company IS NOT NULL     
					 AND    
					 SUB_COMPANY.Company_ID = @company
				   )    
				 )    

			 AND ( ( @subcompany IS NULL )       
				 OR    
				   ( @subcompany IS NOT NULL     
					 AND    
					 SITE.Sub_Company_ID = @subcompany    
				   )    
				 )    

			AND ( ( @region IS NULL )       
				 OR    
				   ( @region IS NOT NULL     
					 AND    
					 SITE.sub_company_region_id = @region    
				   )    
				 )

			 AND ( ( @area IS NULL )       
				 OR    
				   ( @area IS NOT NULL     
					 AND    
					 SITE.Sub_Company_Area_ID = @area    
				   )    
				 )    

			 AND ( ( @district IS NULL )       
				 OR    
				   ( @district IS NOT NULL     
					 AND    
					 SITE.Sub_Company_District_ID = @district
				   )    
				 )    

			 AND ( ( @site IS NULL )       
				 OR    
				   ( @site IS NOT NULL     
					 AND    
					 SITE.Site_ID = @site    
				   )    
			 )
			 AND (@SiteIDList IS NOT NULL AND SITE.Site_ID IN(SELECT DATA FROM fnSplit(@SiteIDList,',')))
			AND ISNULL(A.NumberOfEnrollment, 0) > 0        

      ORDER BY
             SITE.Site_ID,
             A.SNo
END
GO
