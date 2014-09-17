USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_REPORT_BankingReport_cpt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_REPORT_BankingReport_cpt]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/**
  C.Taylor  20/09/06  LEFT joined machine_type .. was missing machines from report
                      any machines not assigned to a machine type, displayed as UNCATEGORISED

**/
CREATE   PROCEDURE rsp_REPORT_BankingReport_cpt

  @company    INT = 0,
  @subcompany INT = 0,
  @region     INT = 0,
  @area       INT = 0,
  @district   INT = 0,
  @site       INT = 0,
  @category   INT = 0, 
  @startdate  datetime,
  @enddate    datetime

AS

  SET DATEFORMAT dmy
 
  IF @company = 0    SET @company = NULL
  IF @subcompany = 0 SET @subcompany = NULL
  IF @region = 0     SET @region = NULL
  IF @area = 0       SET @area = NULL
  IF @district = 0   SET @district = NULL
  IF @site = 0       SET @site = NULL
  IF @category = 0   SET @category = NULL

  DECLARE @NOTSET varchar(20)

  SET @NOTSET = 'UN-DEFINED'

  select company.company_id,
         site.sub_company_id,
         site.sub_company_region_id,
         site.sub_company_area_id,
         site.sub_company_district_id,
         site.site_id,
         site.site_name,
         machine_type_id = CASE WHEN cat.machine_type_id IS NOT NULL THEN cat.machine_type_id ELSE 0 END, 
         machine_type_code = CASE WHEN cat.machine_type_code IS NOT NULL THEN cat.machine_type_code ELSE @NOTSET END,
         machine_type_description = CASE WHEN cat.machine_type_description IS NOT NULL THEN cat.machine_type_description ELSE @NOTSET END,
         machine.machine_stock_no,

              vw_collectiondata.collection_id, 

         vw_collectiondata.machinename,
         vw_collectiondata.posname,

	 vw_collectiondata.cashcollected,
	 vw_collectiondata.Cash_collected_5000p,
  	 vw_collectiondata.Cash_collected_2000p,
	 vw_collectiondata.Cash_collected_1000p,
	 vw_collectiondata.Cash_collected_500p,

	 vw_collectiondata.declared_tickets,
         vw_collectiondata.shortpay,
         vw_collectiondata.dechandpay,
         vw_collectiondata.manualrefills,
         vw_collectiondata.refunds,
         vw_collectiondata.tickets_printed,
	 vw_collectiondata.handpay_var,
	 vw_collectiondata.note_var,
	 vw_collectiondata.coin_var,
	 vw_collectiondata.rdc_tickets_in,
	 vw_collectiondata.rdc_tickets_out,
	 vw_collectiondata.cash_take,
	 vw_collectiondata.take_var

--         vw_collectiondata.*

    from vw_collectiondata

    join installation
      on vw_collectiondata.installation_id = installation.installation_id

    join machine
      on installation.machine_id = machine.machine_id

LEFT join machine_type cat
      on cat.machine_type_id = machine.machine_category_id 

    join site
      on site.site_id = vw_collectiondata.site_id

    join sub_company
      on site.sub_company_id = sub_company.sub_company_id

    join company
      on sub_company.company_id = company.company_id

   where cast ( collection_date as datetime ) between convert ( varchar(12), @startdate, 106 )  and convert( varchar(12), @enddate, 106 )

     AND ( ( @company IS NULL )   
         OR
           ( @company IS NOT NULL 
             AND
             company.company_id = @company 
           )
         )

     AND ( ( @subcompany IS NULL )   
         OR
           ( @subcompany IS NOT NULL 
             AND
             site.sub_company_id = @subcompany
           )
         )

     AND ( ( @region IS NULL )   
         OR
           ( @region IS NOT NULL 
             AND
             site.sub_company_region_id = @region
           )
         )

     AND ( ( @area IS NULL )   
         OR
           ( @area IS NOT NULL 
             AND
             site.sub_company_area_id = @area
           )
         )

     AND ( ( @district IS NULL )   
         OR
           ( @district IS NOT NULL 
             AND
             site.sub_company_district_id = @district
           )
         )

     AND ( ( @site IS NULL )   
         OR
           ( @site IS NOT NULL 
             AND
             site.site_id = @site
           )
         )


     AND ( ( @category IS NULL )   
         OR
           ( @category IS NOT NULL
             AND
             cat.machine_type_id = @category
           )
         )

 order by company.company_id,
         site.sub_company_id,
         site.sub_company_region_id,
         site.sub_company_area_id,
         site.sub_company_district_id,
         site.site_id,
         site.site_name,
         cat.machine_type_id, 
         vw_collectiondata.posName



GO

