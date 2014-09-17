USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_SSRS_REPORT_SGVI_WRL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_SSRS_REPORT_SGVI_WRL]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*********************************************************************************************************
--	Description:	This report displays the collection data for the previous collection week for the 
					SGVI.
--
--	Inputs:			Set Inputs
--	Outputs:		Resultset
--	EXEC dbo.rsp_SSRS_REPORT_SGVI_WRL 1
--	======================================================================
--
--	Revision History
--
--	Sudarsan S		10-06-2008			Created
--  Sudarsan S		04-07-2008			modified the parameter and the where condition
--  C.Taylor      30-07-2008      Added grouping by subcompany, site, removed * and / on retailer/sgvi/lottery from calculation
--  C.Taylor      04-09-2008      PE.Period_End_Final_Date AS [DATE] changed to max(vw.collection_date) as [date]
*********************************************************************************************************/
CREATE PROCEDURE [dbo].[rsp_SSRS_REPORT_SGVI_WRL]
(@SubCompany int = 0,  
 @iStatement_No INT  )  
     
AS    

 WITH SSRS_Report_CTE AS ( 	    
 SELECT  max(vw.collection_Date) as [date], --PE.Period_End_Final_Date AS [DATE],    
   S.Site_Name AS SiteName,    
   SUM(VW.Cash_In) AS [In],    
   sum((VW.dechandpay + VW.ticketout)) AS [Out],    
   sum(cash_in - (VW.dechandpay + VW.ticketout) ) AS Net, 
   sum(CT.HQTerms_Location_Share) AS Retailer,    
   sum(CT.HQTerms_Supplier_Share) AS SGVI,    
   sum(CT.HQTerms_Company_Share) AS Lottery,    
   PE.Period_End_Setup_Date AS SetupDate,    
   PE.Period_End_Final_Date As EndDate,    
   PE.Statement_No as [StatementNo],    
 /* dummy column as specified in the spec  */    
   '' AS Site_Share_Perc,    
   '' AS Operator_Share_Perc,    
   '' AS Company_Share_Perc,    
 /* 3 dummy columns */    
   SC.Sub_Company_Name AS Sub_Company,  -- this value would be used to group the data in the report    
   C.Company_Name AS Company   -- this value would be used to group the data in the report    
       
    FROM dbo.VW_CollectionData VW    
     JOIN dbo.Collection_Details CD ON VW.Collection_ID = CD.Collection_ID    
LEFT JOIN dbo.Collection_Terms CT ON VW.Collection_ID = CT.Collection_ID    
     JOIN dbo.Site S ON VW.Site_ID = S.Site_ID    
     JOIN dbo.Sub_Company SC ON S.Sub_Company_ID = SC.Sub_Company_ID    
     JOIN dbo.Company C ON SC.Company_ID = C.Company_ID    
     JOIN dbo.Period_End PE ON CD.Period_End_ID = PE.Period_End_ID    
   WHERE PE.Statement_No = @iStatement_No  and SC.Sub_Company_ID = CASE WHEN (@SubCompany = 0 ) THEN SC.Sub_Company_ID ELSE @SubCompany  END
    
    
GROUP BY PE.Period_End_Final_Date ,    
       S.Site_Name,    
     PE.Period_End_Setup_Date,    
     PE.Period_End_Final_Date,    
   SC.Sub_Company_Name,  -- this value would be used to group the data in the report    
   C.Company_Name,    
  PE.Statement_No   -- this value would be used to group the data in the report    
  )
    
  SELECT * FROM SSRS_Report_CTE ORDER BY CAST([date] AS DATETIME) DESC, Sub_Company, SiteName

GO

