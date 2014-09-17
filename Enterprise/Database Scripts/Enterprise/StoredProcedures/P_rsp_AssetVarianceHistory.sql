
USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_AssetVarianceHistory]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_AssetVarianceHistory]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE rsp_AssetVarianceHistory
	@installation_id INT,
	@rows INT
AS

	DECLARE @AddShortpay VARCHAR(10)  
	
	SELECT @AddShortpay = setting_value
	FROM   setting
	WHERE  setting_name = 'AddShortpayInVoucherOut'
	
	
	IF @rows > 0
	    SET ROWCOUNT @rows    
	
	SELECT gaming_day = collection_date,
	       collection_day = collection_date_of_collection,
	       coin_Var,
	       note_var,
	       ticket_in_var = ROUND((
	           declaredticketvalue -(rdc_tickets_in + RDC_TICKETS_INSERTED_NONCASHABLE_VALUE)
	       ),2),
	       ticket_out_var = ROUND((
	           ((DecTicketsOut) - (CASE WHEN ISNULL(@AddShortpay,'TRUE') = 'TRUE'THEN 0  ELSE ShortPay  END)) -(rdc_tickets_out + RDC_TICKETS_PRINTED_NONCASHABLE_VALUE)
	       ),2),
	      ROUND(ISNULL(handpay_var, 0),2) as handpay_var,  
	       EftIn_var = (DecEftIn / 100) -(EftIn / 100),
	       EftOut_var = (DecEftOut / 100) -(EftOut / 100),
	       Progressive_Value_Variance AS Progressive_Var,
	       Total_Var = (
	           ISNULL(coin_Var, 0) +
	           ISNULL(note_var, 0) +
	           ISNULL(
	               (
	                   declaredticketvalue -(rdc_tickets_in + RDC_TICKETS_INSERTED_NONCASHABLE_VALUE)
	               ),
	               0
	           ) -
	           ISNULL(
	               (
	                   ((DecTicketsOut) - (CASE WHEN ISNULL(@AddShortpay,'TRUE') = 'TRUE'THEN 0 ELSE ShortPay END))-(rdc_tickets_out + RDC_TICKETS_PRINTED_NONCASHABLE_VALUE)
	               ),
	               0
	           ) -
	           ISNULL(handpay_var, 0) +
	           ISNULL((DecEftIn / 100) -(EftIn / 100), 0) -
	           ISNULL((DecEftOut / 100) -(EftOut / 100), 0)
	       )
	FROM   vw_collectiondata
	WHERE  Installation_ID = @Installation_id
	ORDER BY
	       Collection_ID DESC
GO

