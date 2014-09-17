USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetDropForPeriodEndBySubCompany]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetDropForPeriodEndBySubCompany]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-------------------------------------------------------------------------- 
---
--- Description: 
---
--- Inputs:      see inputs
---
--- Outputs:     (0)   - no error .. 
---              OTHER - SQL error 
--- 
--- =======================================================================
--- 
--- Revision History
--- 
--- C.Taylor   02/07/08     Created 
--- C.Taylor   30/07/08     Tightened links.
--- C.Taylor   06/08/08     uses cash_take instead of cashtake
--------------------------------------------------------------------------- 
CREATE PROCEDURE rsp_GetDropForPeriodEndBySubCompany

  @PeriodEndDate datetime

AS

   select Sub_Company_Name, 
          sc.sub_company_id, 
          sc.company_id,  
          Period_ID = pe.period_end_id, 
          Period_Start = period_end_setup_date, 
          Period_End = period_end_final_date, 
          Total_Net = cast ( SUM(cash_in - (dechandpay + ticketout)) as decimal(20,2))
     from sub_company sc

     join company c
       on sc.company_id = c.company_id

     join collection_details cd
       on cd.collection_sub_company_id = sc.sub_company_id

     join vw_collectiondata vwcd
       on vwcd.collection_id = cd.collection_id

     JOIN Period_End pe
       ON ( pe.Period_End_ID = cd.period_end_id and pe.Sub_company_id = sc.sub_company_id )
       
    WHERE CONVERT(VARCHAR, CAST(Period_End_Final_Date AS DATETIME), 101) = CONVERT(VARCHAR, @PeriodEndDate, 101)

 group by pe.Period_End_ID, 
          Sub_Company_Name, 
          sc.sub_company_id,
          sc.company_id,  
          period_end_setup_date,
          period_end_final_date


GO

