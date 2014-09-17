USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ImportStackerLevelDetailsFromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ImportStackerLevelDetailsFromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_ImportStackerLevelDetailsFromXML  
 @doc VARCHAR(MAX),  
 @IsSuccess INT OUTPUT  
AS  
  
BEGIN  

DECLARE @idoc INT  
  
SET @IsSuccess = 0  

IF ISNULL(@doc,'') = ''  
  
RETURN 0  

SET @doc = '<?xml version="1.0" encoding="ISO-8859-1"?>' + @doc  
  
  EXEC sp_xml_preparedocument @idoc OUTPUT, @doc  
  
  DECLARE @HQ_Installation_No INT
  SELECT @HQ_Installation_No=HQ_Installation_No FROM OPENXML(@idoc, './/StackerLevels/StackerLevel_Info', 1) WITH  
   (  
    HQ_Installation_No int './HQ_Installation_No'
   )

	IF NOT EXISTS(SELECT Installation_No FROM StackerLevel WHERE Installation_No =@HQ_Installation_No)
	BEGIN
		INSERT INTO dbo.StackerLevel(Installation_No) Values (@HQ_Installation_No) 
	END

	UPDATE SL  
	SET SL.BillQty =A.BillQty,
		SL.TicketQty=A.TicketQty,
		SL.STMAlertProcessed=A.STMAlertProcessed,
		SL.ProcessedDateTime=A.ProcessedDateTime
	  FROM StackerLevel SL INNER JOIN OPENXML(@idoc, './/StackerLevels/StackerLevel_Info', 2) WITH  
	   ( 
		HQ_Installation_No INT './HQ_Installation_No',
		BillQty INT './BillQty',  
		TicketQty INT './TicketQty',   
		STMAlertProcessed BIT './STMAlertProcessed',
		ProcessedDateTime DATETIME './ProcessedDateTime'
	   )A ON SL.Installation_No = A.HQ_Installation_No

	 IF @@Error <> 0  
	  BEGIN  
	   SET @IsSuccess = -1 -- failed while updating the records in the StackerLevel table     
	  END 

END 

GO

