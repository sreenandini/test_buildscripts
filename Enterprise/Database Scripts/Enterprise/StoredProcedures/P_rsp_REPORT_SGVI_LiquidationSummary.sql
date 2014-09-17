USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_REPORT_SGVI_LiquidationSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_REPORT_SGVI_LiquidationSummary]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].rsp_REPORT_SGVI_LiquidationSummary   

	@Batch_No INT = 0 

AS    

SET DATEFORMAT dmy
SET NOCOUNT ON 

DECLARE @RetailNegativeNet AS FLOAT,
		@SETTINGVALUE AS FLOAT

declare @subcomp_id int
declare @sitecount int

IF @Batch_No = 0 
  RETURN (0) -- shouldn't happen

  -- get matching subcomp id
EXEC rsp_GetSetting NULL, 'SGVI_ENT_CENTRE_SUB_COMP_ID', 0, @subcomp_id OUTPUT

  -- get matching site
declare @site_id int

  SELECT @site_id = Site_ID                         
    FROM VW_Collectiondata (NOLOCK)
   WHERE Batch_ID = @batch_no

set @sitecount=(select count(*) from site Where site_id=@site_id and sub_company_id=@subcomp_id)


IF (@sitecount=0)	--Means its not Entertainment site and setting value is 0.22 for SGVI_Batch_Net_Value
	EXEC rsp_GetSetting NULL, 'SGVI_Batch_Net_Value', '0.22', @SETTINGVALUE OUTPUT
ELSE				--Means its Entertainment site 
	SET @SETTINGVALUE=0


	--Get the batch_negative value for previous batch

    
	SELECT @RetailNegativeNet = BATCH_NEGATIVE_NET FROM BATCH WHERE BATCH_ID =
	(SELECT max(Batch.BATCH_ID) from Batch,VW_Collectiondata where Batch.Batch_ID < @Batch_No
			AND  VW_Collectiondata.BATCH_ID = Batch.Batch_ID
			AND VW_Collectiondata.Site_ID= @site_id)	

	IF (@RetailNegativeNet IS NULL )
		SET @RetailNegativeNet = 0	

 SELECT Convert(varchar,convert(datetime,VWCD.[Batch_Date_Performed],103),107) AS Date_Collected,  --Batch Date 
			  SUBSTRING(Batch_ref,CHARINDEX(',',Batch_ref,2)+1,LEN(Batch_ref)) AS SiteBatchNumber, 
		    S.Site_Name AS Retailer_Name, --Retailer Name
		    SUM(VWCD.[Cash_In]) AS Gross,--1 Gross (Meters In)
		    SUM(VWCD.dechandpay+VWCD.ticketsout) AS Tickets_Expected,--2 Tickets Expected (Meters Out)
		    SUM( VWCD.[Cash_In]-(VWCD.dechandpay+VWCD.ticketsout)) AS Net ,--3 Net
		    (SUM(VWCD.[Cash_In]-(VWCD.dechandpay+VWCD.ticketsout)) * @SETTINGVALUE) AS Net_Percentage,--4 Net x Percentage
		    @SETTINGVALUE AS Percentage_Setting,
		    @RetailNegativeNet AS Retail_Negative_Net,--5 Retailers Negative Net 
		    ((SUM(VWCD.[Cash_In]-(VWCD.dechandpay+VWCD.ticketsout)) * @SETTINGVALUE)+ @RetailNegativeNet) AS Retailer_Share,--6 Retailers Share(4+5)
		    SUM(VWCD.dechandpay+VWCD.ticketsout) AS Tickets_Paid,--7 Tickets Paid(2)
	   	  COALESCE(B.Batch_Advance,0) AS Advance_To_Retailer,--8 Advance to Retailer		
		    CASE 
			    WHEN ((SUM(VWCD.[Cash_In]-(VWCD.dechandpay+VWCD.ticketsout)) * @SETTINGVALUE)+ @RetailNegativeNet)	>	0 THEN 
				    ((SUM(VWCD.[Cash_In]-(VWCD.dechandpay+VWCD.ticketsout)) * @SETTINGVALUE)+ @RetailNegativeNet)+(SUM(VWCD.dechandpay+VWCD.ticketsout)-COALESCE(B.Batch_Advance,0))
			    ELSE
				    (SUM(VWCD.dechandpay+VWCD.ticketsout)-COALESCE(B.Batch_Advance,0))
		    END  AS Retailer,--9 --if Retailer's Share>0 then Retailer=(6+7-8) else <0 then Retailer=(7-8)
		    
		    CASE 
			    WHEN ((SUM(VWCD.[Cash_In]-(VWCD.dechandpay+VWCD.ticketsout)) * @SETTINGVALUE)+ @RetailNegativeNet)	>	0 THEN --if Retailer's Share>0 =(3-4)+(8-5) else <0 =(3+8)
				    (SUM(VWCD.[Cash_In]-(VWCD.dechandpay+VWCD.ticketsout))-(SUM(VWCD.[Cash_In]-(VWCD.dechandpay+VWCD.ticketsout)) * @SETTINGVALUE))	+ (COALESCE(B.Batch_Advance,0)-@RetailNegativeNet)
			    ELSE			
				    (SUM(VWCD.[Cash_In]-(VWCD.dechandpay+VWCD.ticketsout))+COALESCE(B.Batch_Advance,0))
		    END AS Balance_Due --10 if Retailer's Share>0 then Balance_Due=(6+7-8) else <0 then Balance_Due=(7-8)

   FROM VW_Collectiondata VWCD

   JOIN Batch B 
   	 ON B.Batch_ID=VWCD.Batch_ID
   JOIN Site S 
 	   ON S.Site_Id=VWCD.Site_ID

  WHERE B.Batch_ID=@Batch_No

GROUP BY VWCD.[Batch_Date_Performed],
         S.Site_Name,
         B.Batch_Advance,
		 Batch_ref

GO

