USE Enterprise
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects o
       WHERE  o.[name] = 'rsp_consolidated_variance'
              AND o.[type] = 'P'
   )
BEGIN
    DROP PROCEDURE rsp_consolidated_variance
END
GO

/*
EXEC rsp_consolidated_variance 
     @company = 1,
     @subcompany = 1,
     @region = 0,
     @area = 0,
     @district = 0,
     @site = 0,
     @Zone = N'ALL',
     @category = 0,
     @startdate = N'2013-12-12 19:52:19',
     @enddate = N'2013-12-19 19:52:19',
     @DateFormat = N'dd/MM/yyyy',
     @HideZeroVarianceCollections = 0,
     @GROUPBYZONE = 0
select * from company
*  exec rsp_consolidated_variance 0,0,0,0,0,0,'',0,'06 Dec 2011','13 Dec 2011','dd MMM yyyy',0
*/

CREATE PROCEDURE [dbo].[rsp_consolidated_variance]
	@Company INT = 0,
	@SubCompany INT = 0,
	@Region INT = 0,
	@Area INT = 0,
	@District INT = 0,
	@Site INT,
	@Zone INT,
	@Category INT,
	@StartDate DATETIME,
	@EndDate DATETIME,
	@DateFormat NVARCHAR(50),
	@HideZeroVarianceCollections BIT,
	@GroupByZone BIT,
	@SiteIDList VARCHAR(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.          
	SET NOCOUNT ON;          
	DECLARE @calcStart_Date  DATETIME,
	        @calcEnd_Date    DATETIME   
	
	DECLARE @sAddShortpay    VARCHAR(10)
	
	SET DATEFORMAT dmy 
	--SET @calcStart_Date=Cast(@startdate as DateTime)
	--SET @calcEnd_Date=Cast(@enddate as DateTime)    
	
	SET @calcStart_Date = CAST(CONVERT(VARCHAR(12), @startdate, 106) AS DATETIME)          
	SET @calcEnd_Date = CAST(CONVERT(VARCHAR(12), @enddate, 106) AS DATETIME)          
	
	SELECT @sAddShortpay = setting_value  
	FROM   setting  
	WHERE  setting_name = 'AddShortpayInVoucherOut' 
	
	IF @site = 0
	    SET @site = NULL
	
	IF @category = 0
	    SET @category = NULL
	
	IF @zone = 0    
		SET @zone = NULL  
     
	--IF @zone = 'ALL'
	--   OR @zone = '0'
	--   OR @zone = '--None--'
	--    SET @zone = NULL 
	
	-- Insert statements for procedure here          
	SELECT vw_cd.site_id,
	       vw_cd.machinename,
	       vw_cd.posname,
	       MACHINE.machine_id,
	       MACHINE.machine_stock_no,
	       CAST(Coin_Var AS DECIMAL(18, 2)) AS coin_var,
	       CAST(vw_cd.note_var AS DECIMAL(18, 2)) AS NOTE_VAR,
	       ticket_in_var = CAST(
	           (
	               vw_cd.declaredticketvalue -(
	                   vw_cd.rdc_tickets_in + vw_cd.rdc_tickets_inserted_noncashable_value
	               )
	           ) AS DECIMAL(18, 2)
	       ),
	       ticket_out_var = CAST(
	           (
	               (DecTicketsOut - CASE WHEN ISNULL(@sAddShortpay,'True') = 'True' THEN 0 ELSE ISNULL(shortpay,0) END) -(
	                   rdc_tickets_out + vw_cd.rdc_tickets_printed_noncashable_value
	               )
	           ) AS DECIMAL(18, 2)
	       ),
	       CAST((vw_cd.DecEftIn / 100) -(vw_cd.EftIn / 100) AS DECIMAL(18, 2)) AS EftIn_var ,
	       CAST((vw_cd.DecEftOut / 100) -(vw_cd.EftOut / 100) AS  DECIMAL(18, 2)) EftOut_var,
	       CAST(vw_cd.handpay_var AS DECIMAL(18, 2)) AS handpay_var,
	       Total_Var = (
	           CAST(Coin_Var AS DECIMAL(18, 2)) +
	           CAST(vw_cd.note_var AS DECIMAL(18, 2)) +
	           CAST(
	               (
	                   vw_cd.declaredticketvalue -(
	                       vw_cd.rdc_tickets_in + vw_cd.rdc_tickets_inserted_noncashable_value
	                   )
	               ) AS DECIMAL(18, 2)
	           )
	       ) -(
	           CAST(
	               (
	                   (DecTicketsOut - CASE WHEN ISNULL(@sAddShortpay,'True') = 'True' THEN 0 ELSE ISNULL(shortpay,0) END) -(
	                       rdc_tickets_out + vw_cd.rdc_tickets_printed_noncashable_value
	                   )
	               ) AS DECIMAL(18, 2)
	           ) +
	           CAST(vw_cd.handpay_var AS DECIMAL(18, 2))
	       ),
	       machine_type_code = CASE 
	                                WHEN cat.machine_type_code IS NOT NULL THEN 
	                                     cat.machine_type_code
	                                ELSE NULL
	                           END,
	       St.Site_Name,
	       dbo.fnFormatDate(vw_cd.Collection_Date, @DateFormat) AS Gaming_Day,
	       batch_ref = SUBSTRING(batch.batch_ref, CHARINDEX(',', batch.batch_ref) + 1, 10),
	       CAST(shortpay AS DECIMAL(18, 2)) AS SHORTPAY,
	       CAST(vw_cd.void AS DECIMAL(18, 2)) AS VOID,
	       ISNULL(Z.Zone_Name,'NOT SET') AS Zone_Name,
	       ISNULL(batch.batch_name,'None') AS batch_name
	FROM   vw_collectiondata vw_cd WITH(NOLOCK)    
        JOIN batch WITH(NOLOCK)    
             ON  vw_cd.batch_id = batch.batch_id    
        JOIN installation WITH(NOLOCK)    
             ON  vw_cd.installation_id = installation.installation_id    
        JOIN MACHINE WITH(NOLOCK)    
             ON  installation.machine_id = MACHINE.machine_id    
        LEFT JOIN machine_type cat WITH(NOLOCK)    
             ON  cat.machine_type_id = MACHINE.machine_category_id    
        LEFT JOIN [site] St WITH(NOLOCK)    
             ON  St.site_id = vw_cd.site_id    
        LEFT JOIN Sub_Company_Region sr WITH (NOLOCK)
			ON st.Sub_Company_Region_ID = sr.Sub_Company_Region_ID
		LEFT JOIN Sub_Company_Area sa WITH (NOLOCK)
			ON st.Sub_Company_Area_ID = sa.Sub_Company_Area_ID    
		LEFT JOIN Sub_Company_District  sd WITH (NOLOCK)
			ON st.Sub_Company_District_ID = sd.Sub_Company_District_ID    
        LEFT JOIN sub_company WITH(NOLOCK)    
             ON  St.sub_company_id = sub_company.sub_company_id    
        LEFT JOIN company WITH(NOLOCK)    
             ON  sub_company.company_id = company.company_id    
        JOIN Bar_Position WITH(NOLOCK)    
             ON  Bar_Position.Bar_Position_ID = vw_cd.Bar_Position_ID    
        LEFT OUTER JOIN Zone Z WITH(NOLOCK)    
             ON  Z.Zone_ID = Bar_Position.Zone_ID    
 WHERE  CAST(collection_date AS DATETIME) BETWEEN @calcStart_Date AND @calcEnd_Date    
        AND (ISNULL(@company, 0) = 0 OR @company = company.company_id)    
        AND (    
                ISNULL(@subcompany, 0) = 0    
                OR @subcompany = sub_company.sub_company_id    
            )    
        AND (ISNULL(@area, 0) = 0 OR @area = sa.Sub_Company_Area_ID)    
        AND (ISNULL(@district, 0) = 0 OR @district = sd.Sub_Company_District_ID)    
        AND (ISNULL(@region, 0) = 0 OR @region = sr.Sub_Company_Region_ID)
            
            --where CAST( collection_date + ' 00:00:00' as datetime ) between @calcStart_Date AND @calcEnd_Date    
        --AND (    
        --        (@site IS NULL)    
        --        OR (@site IS NOT NULL AND vw_cd.site_id = @site)    
        --    )  
        AND (
	               @SiteIDList IS NOT NULL
	               AND ST.Site_Id IN (SELECT DATA
	                                  FROM   dbo.fnSplit (@SiteIDList, ','))
	           )      
        AND (    
                (@category IS NULL)    
                OR (@category IS NOT NULL AND cat.machine_type_id = @category)    
            )    
        AND (    
                --(@zone IS NULL)    
                --OR (    
                --       @zone IS NOT NULL    
                --       AND Z.Zone_ID IN (    
                --               SELECT Zone_Id    
                --               FROM   Zone Z    
                --               WHERE  Z.Zone_name = @Zone    
                --           )    
                --   )   
                (@zone IS NULL)    
                        OR (@zone IS NOT NULL AND Z.Zone_ID = @zone)     
				)    
        AND (    
                (    
                    @HideZeroVarianceCollections = 1    
                    AND ((    
                            (    
                                vw_cd.coin_var +    
                                vw_cd.note_var +(vw_cd.declared_tickets - vw_cd.rdc_tickets_in)     
                                +(    
                                    (vw_cd.tickets_printed + 
                                    CASE WHEN ISNULL(@sAddShortpay,'True') = 'True' THEN ISNULL(vw_cd.shortpay,0.00)
                                     ELSE 0.00 END - vw_cd.void)     
                                    - vw_cd.rdc_tickets_out    
                                ) +    
                                vw_cd.handpay_var    
                            )) <> 0    
                           OR (CAST((vw_cd.DecEftIn / 100.00) -(vw_cd.EftIn / 100.00) AS DECIMAL(18, 2)) <>0)
						 OR  (CAST((vw_cd.DecEftOut / 100.00) -(vw_cd.EftOut / 100.00) AS  DECIMAL(18, 2))<>0) 
                        
                       ) 
                )    
                OR (@HideZeroVarianceCollections = 0)    
            )    
 ORDER BY    
        st.Site_Name,    
        cat.Machine_Type_Code,    
        MACHINE.Machine_Stock_No,    
        CAST(vw_cd.Collection_Date AS DATETIME)    
END 
GO
