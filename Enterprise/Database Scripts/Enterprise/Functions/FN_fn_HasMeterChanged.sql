USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_HasMeterChanged]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_HasMeterChanged]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION fn_HasMeterChanged(@SessionID INT, @SiteID INT)
RETURNS BIT
AS
BEGIN

DECLARE @MAXID INT
DECLARE @MINID INT

DECLARE @CoinIn INT
DECLARE @CoinOut INT
DECLARE @Bill100 INT
DECLARE @Bill50 INT
DECLARE @Bill20 INT
DECLARE @Bill10 INT
DECLARE @Bill5 INT
DECLARE @Bill1 INT
DECLARE @TrueCoinIn INT
DECLARE @TrueCoinOut INT
DECLARE @Drop INT
DECLARE @Jackpot INT
DECLARE @CancelledCredits INT
DECLARE @HandPaidCancelledCredits INT
DECLARE @CashableTicketIn INT
DECLARE @CashableTicketOut INT
DECLARE @CashableTicketInQty INT
DECLARE @CashableTicketOutQty INT
DECLARE @ProgressiveHandPay INT

SELECT @MAXID = MAX(ID) FROM MaintenanceHistory WHERE @SessionID = SessionID AND SITE_ID = @SiteID
SELECT @MINID = MIN(ID) FROM MaintenanceHistory WHERE @SessionID = SessionID AND SITE_ID = @SiteID


IF (ISNULL(@MAXID,0) = 0)
BEGIN 
	RETURN 0
END

SELECT 
@CoinIn = CoinIn ,@CoinOut = CoinOut  ,@Bill100 = Bill100 ,@Bill50 = Bill50 ,@Bill20 = Bill20 ,@Bill10 = Bill10
,@Bill5 = Bill5 ,@Bill1 = Bill1 ,@TrueCoinIn = TrueCoinIn ,@TrueCoinOut = TrueCoinOut ,@Drop = [Drop] ,@Jackpot = Jackpot
,@CancelledCredits = CancelledCredits ,@HandPaidCancelledCredits = HandPaidCancelledCredits ,@CashableTicketIn = CashableTicketIn
,@CashableTicketOut = CashableTicketOut  ,@CashableTicketInQty = CashableTicketInQty ,@CashableTicketOutQty = CashableTicketOutQty
,@ProgressiveHandPay = ProgressiveHandPay
 FROM 
MaintenanceHistory
WHERE ID = @MAXID AND SITE_ID = @SiteID


IF EXISTS (SELECT 1 FROM MaintenanceHistory WHERE ID = @MINID AND SITE_ID = @SiteID
				AND @CoinIn = CoinIn
				AND @CoinOut = CoinOut
				AND @Bill100 = Bill100
				AND @Bill50 = Bill50
				AND @Bill20 = Bill20
				AND @Bill10 = Bill10
				AND @Bill5 = Bill5
				AND @Bill1 = Bill1
				AND @TrueCoinIn = TrueCoinIn
				AND @TrueCoinOut = TrueCoinOut
				AND @Drop = [Drop]
				AND @Jackpot = Jackpot
				AND @CancelledCredits = CancelledCredits
				AND @HandPaidCancelledCredits = HandPaidCancelledCredits
				AND @CashableTicketIn = CashableTicketIn
				AND @CashableTicketOut = CashableTicketOut
				AND @CashableTicketInQty = CashableTicketInQty
				AND @CashableTicketOutQty = CashableTicketOutQty
				AND @ProgressiveHandPay = ProgressiveHandPay)
	BEGIN
	RETURN 0
	END

RETURN 1


END
GO

