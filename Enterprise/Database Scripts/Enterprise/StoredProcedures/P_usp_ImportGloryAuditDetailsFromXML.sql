USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ImportGloryAuditDetailsFromXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ImportGloryAuditDetailsFromXML]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE usp_ImportGloryAuditDetailsFromXML  
 @doc VARCHAR(MAX) 
AS  
  
BEGIN  

DECLARE @idoc INT   


IF ISNULL(@doc,'') = ''  


SET @doc = '<?xml version="1.0" encoding="ISO-8859-1"?>' + @doc  
  
  EXEC sp_xml_preparedocument @idoc OUTPUT, @doc  
  
DECLARE @TransactionStarttime DATETIME,	  
	   @TransactionType NVARCHAR(30), 
	   @TransactionEndtime DATETIME,
	   @TicketNo nvarchar(300), 
	   @ValidationNo nvarchar(400),
	   @AssetNo nvarchar(50),
	   @TransactionAmount decimal(18,2), 
	   @UserID  nvarchar(30),
	   @SessionID nvarchar(max),
	   @Device nvarchar(30), 
	   @Status bit,
	   @ErrorCode nvarchar(50)

  SELECT @TransactionStarttime=A.TransactionStarttime,
        @TicketNo=A.TicketNo,
		@TransactionType=A.TransactionType,
        @TransactionEndtime =A.TransactionEndtime,
		@ValidationNo=A.ValidationNo,
		@AssetNo=A.AssetNo,
		@TransactionAmount=A.TransactionAmount,
		@UserID=A.UserID,
		@SessionID=A.SessionID,
		@Device=A.Device,
		@Status=A.Status,
		@ErrorCode=A.ErrorCode
  FROM OPENXML(@idoc, './/Glory/GloryAudit', 2) WITH  
   (  
        TransactionStarttime DATETIME './TransactionStarttime',
	    TicketNo nvarchar(300)  './TicketNo',
        TransactionType NVARCHAR(30) './TransactionType', 
        TransactionEndtime DATETIME './TransactionEndtime',	
		ValidationNo nvarchar(400) './ValidationNo',   
		AssetNo nvarchar(50) './AssetNo', 
		TransactionAmount decimal(18,2) './TransactionAmount',
		UserID  nvarchar(30)  './UserID',
		SessionID nvarchar(max)  './SessionID',
		Device nvarchar(30) './Device', 
		Status bit  './Status',
		ErrorCode nvarchar(50)  './ErrorCode'
   ) A



	IF NOT EXISTS(SELECT id FROM Glory_CDAuditDetails WHERE ValidationNo =@ValidationNo and TicketNo=@TicketNo  )
	BEGIN
		INSERT INTO Glory_CDAuditDetails(ValidationNo,TicketNo) Values (@ValidationNo,@TicketNo) 
	END

	UPDATE Glory_CDAuditDetails 
	SET 
		TransactionStarttime=@TransactionStarttime,
		TransactionType =@TransactionType,		
		TransactionEndtime =@TransactionEndtime,		
		ValidationNo=@ValidationNo,
		AssetNo=@AssetNo,
		TransactionAmount=@TransactionAmount,
		UserID=@UserID,
		SessionID=@SessionID,
		Device=@Device,
		Status=@Status,
		ErrorCode=@ErrorCode
WHERE  ValidationNo =@ValidationNo  AND TicketNo =@TicketNo
END 

GO

