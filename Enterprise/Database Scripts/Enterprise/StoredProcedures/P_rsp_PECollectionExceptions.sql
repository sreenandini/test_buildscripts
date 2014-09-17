USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_PECollectionExceptions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_PECollectionExceptions]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_PECollectionExceptions

  @period_end_id int

AS

   SELECT SiteName = s.Site_Name,
	  s.Site_ID,
	  s.Site_Code,
	  sum(declared_tickets+declared_notes) CashIn,
	  sum((VW.dechandpay + VW.ticketout)) CashOut,
	  sum(declared_tickets) ticketin,
	  sum(declared_notes) bills,
	  sum(ticketout) ticketout,
	  sum(dechandpay) handpays,
	  sum((declared_tickets+declared_notes) - (VW.dechandpay + VW.ticketout)) net,
	  count(distinct stockno)  machines,
	  --count(distinct col.installation_id) machinecount,
	  count(*) collcount,
  	  count(distinct vw.batch_id) batchcount,
	  min(CD.week_id) week_id,
      max(vw.batch_id) batch_id,
      min(CW.Calendar_Week_Number) AS WeekNumber,  
	  min(CW.Calendar_Week_Start_Date) AS WeekStartDate,  
      min(CW.Calendar_Week_End_Date) AS WeekEndDate

      FROM site s

	 LEFT JOIN  dbo.VW_CollectionData VW WITH(NOLOCK)
        ON ( vw.site_id = s.site_id ) 

	 LEFT JOIN dbo.Collection_Details CD WITH(NOLOCK)
		ON VW.Collection_ID = CD.Collection_ID
       
     LEFT JOIN Calendar_Week CW WITH(NOLOCK)  
        ON  CD.Week_ID = CW.Calendar_Week_ID

     JOIN Sub_Company SC WITH(NOLOCK)
       ON S.Sub_Company_ID = SC.Sub_Company_ID

     JOIN Company C WITH(NOLOCK)
       ON SC.Company_ID = C.Company_ID

     JOIN Period_End PE WITH(NOLOCK)
       ON ( CD.Period_End_ID = PE.Period_End_ID ) -- or CD.Period_End_ID is null )

    WHERE PE.Period_End_ID = @period_end_id
      and S.Sub_Company_ID = pe.sub_company_id

 GROUP BY S.Site_Name,	  
			s.Site_ID,
			s.Site_Code

GO

