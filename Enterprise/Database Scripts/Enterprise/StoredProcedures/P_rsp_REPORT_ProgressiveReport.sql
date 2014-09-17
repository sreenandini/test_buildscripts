USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_REPORT_ProgressiveReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_REPORT_ProgressiveReport]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--  
-- Description: SP is being used to see Progressive Win Values using Progressive Report  
--  
-- Revision History  
--   
-- Rakesh Marwaha     21/NOV/2007		Created  
-- Vineetha Mathew    25/06/2009		Modified	To fix the issue:Unable to open the Progressive Win Detailed Report
--												(Done casting  to include big int values in calculation)
-- Yoganandh P		  08/07/2010		Modified to display Game Name as 'Multi Game' if the game is 'Multi Game and  
--										Auto Detected' else display actual 'Game Name
  
CREATE PROCEDURE [dbo].[rsp_REPORT_ProgressiveReport]  
  
	@Company    INT = 0,  
	@SubCompany INT = 0,  
	@Region     INT = 0,  
	@Area       INT = 0,  
	@District   INT = 0,  
	@Site       INT = 0,  
	@Category   INT = 0,    
	@StartDate  DATETIME,  
	@EndDate   DATETIME, 
    @SiteIDList VARCHAR(MAX)
	AS  

  
	SET DATEFORMAT dmy  
  
  IF @company = 0    SET @company = NULL  
  IF @subcompany = 0 SET @subcompany = NULL  
  IF @region = 0     SET @region = NULL  
  IF @area = 0       SET @area = NULL  
  IF @district = 0   SET @district = NULL  
  IF @site = 0       SET @site = NULL  
  IF @category = 0   SET @category = NULL  
  SET @startdate = CAST(CONVERT(VARCHAR(12), CAST(@startdate AS DATETIME), 106)  AS DATETIME)  
  
  
  
SELECT   
	S.Site_Name,  
	CAT.Machine_Type_Code,  
	BP.Bar_Position_ID,  
	I.Installation_ID,  
	Z.Zone_Name,  
	BP.Bar_Position_Name,  
	--CASE MC.Machine_Name WHEN 'Auto Detected' THEN 'Multi Game' ELSE MC.Machine_Name END AS Machine_Name,  
	MC.machine_name AS Machine_Name,
	-- Below calculations are done because Read table shows values in Pences  
	CAST((CAST((CAST(r.progressive_win_value AS BIGINT)*I.Installation_Price_Per_Play) AS DECIMAL(20,2))/100) AS DECIMAL(20,2)) AS progressive_win_value,  
	CAST((CAST((CAST(r.progressive_win_Handpay_value AS BIGINT)*I.Installation_Price_Per_Play) AS DECIMAL(20,2))/100) AS DECIMAL(20,2)) AS progressive_win_Handpay_value  
FROM  [Read] r     
	JOIN Installation I			ON r.Installation_ID = I.Installation_ID    
	JOIN Machine M				ON I.Machine_ID = M.Machine_ID    
	JOIN Machine_class MC		ON M.Machine_Class_ID = MC.Machine_Class_ID    
	LEFT JOIN Machine_Type CAT	ON M.Machine_Category_ID = CAT.Machine_Type_ID    
	JOIN Bar_Position BP		ON I.Bar_Position_ID = BP.Bar_Position_ID    
	JOIN Site S					ON BP.Site_ID = S.Site_ID    
	JOIN Sub_Company sc			ON sc.Sub_Company_ID = S.Sub_company_ID    
	JOIN Company c				ON c.Company_ID = sc.company_ID    
	JOIN Operator o				ON o.Operator_ID = m.Operator_ID  
	JOIN Depot d				ON d.Depot_ID = m.Depot_ID  
	LEFT JOIN [Zone] Z			ON BP.Zone_ID = Z.Zone_ID    
	--where cast ( r.read_date as datetime ) between convert ( varchar(12), @calcStartDate, 106 )  and convert( varchar(12), @calcEndDate, 106 )  
WHERE CAST ( r.read_date AS DATETIME ) BETWEEN  @startdate  AND  @enddate
	AND (( @company		IS NULL )	OR ( @company		IS NOT NULL		AND c.company_id = @company  ))
	AND ( (@subcompany	IS NULL )	OR ( @subcompany	IS NOT NULL		AND s.sub_company_id = @subcompany ))  
	AND ( (@region		IS NULL )	OR ( @region		IS NOT NULL		AND s.sub_company_region_id = @region ) )  
	AND ( (@area		IS NULL )	OR ( @area			IS NOT NULL		AND s.sub_company_area_id = @area ) )  
	--AND ( (@site		IS NULL )	OR ( @site			IS NOT NULL		AND s.site_id = @site )) 
	AND (
	               @SiteIDList IS NOT NULL AND s.site_id IN (SELECT DATA    
                                              FROM   dbo.fnSplit (@SiteIDList,','))
	           )
	AND ( (@category	IS NULL )	OR ( @category		IS NOT NULL		AND cat.machine_type_id = @category ) )   
  
Order By  
    S.Site_Name ASC,  
    CAT.Machine_Type_Code ASC,  
    CAT.Machine_Type_ID ASC  

GO

