USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ImportMaintenanceHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ImportMaintenanceHistory]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_ImportMaintenanceHistory]   
@doc xml   
AS  
BEGIN  
	DECLARE @SiteID INT
	DECLARE @handle INT
	EXEC sp_xml_preparedocument @handle OUTPUT, @doc  

	CREATE TABLE #Temp( ID INT,
						SessionID INT,
						EventID INT,
						[TimeStamp] DATETIME,
						CoinIn INT,
						CoinOut INT,
						Bill500 INT,
						Bill200 INT,
						Bill100 INT,
						Bill50 INT,
						Bill20 INT,
						Bill10 INT,
						Bill5 INT,
						Bill1 INT,
						TrueCoinIn INT,
						TrueCoinOut INT,
						[Drop] INT,
						Jackpot INT,
						CancelledCredits INT,
						HandPaidCancelledCredits INT,
						CashableTicketIn INT,
						CashableTicketOut INT,
						CashableTicketInQty INT,
						CashableTicketOutQty INT,
						ProgressiveHandPay INT,
						Code VARCHAR(50))

	SELECT	ID,SessionID,EventID,[TimeStamp],CoinIn,CoinOut,
			Bill500,Bill200,Bill100,Bill50,Bill20,Bill10,Bill5,Bill1,TrueCoinIn,
			TrueCoinOut,[Drop],Jackpot,CancelledCredits,HandPaidCancelledCredits,
			CashableTicketIn,CashableTicketOut,CashableTicketInQty,CashableTicketOutQty,
			ProgressiveHandPay,Code
	INTO #MH
	FROM OPENXML (@handle, './MaintenanceHistorys/MaintenanceHistory',2)  
    WITH #Temp

	SELECT @SiteID = Site_ID FROM [Site] WHERE Site_Code = (SELECT TOP 1 Code FROM #MH)

	IF NOT EXISTS(SELECT 1 FROM MaintenanceHistory WHERE 
													ID = (SELECT TOP 1 ID FROM #MH) 
													AND Site_ID = @SiteID)
	BEGIN
		INSERT INTO MaintenanceHistory
		SELECT	ID,SessionID,EventID,[TimeStamp],CoinIn,CoinOut,
				Bill500,Bill200,Bill100,Bill50,Bill20,Bill10,Bill5,Bill1,TrueCoinIn,
				TrueCoinOut,[Drop],Jackpot,CancelledCredits,HandPaidCancelledCredits,
				CashableTicketIn,CashableTicketOut,CashableTicketInQty,CashableTicketOutQty,
				ProgressiveHandPay,(SELECT Site_ID FROM [Site] WHERE Site_Code = Code) AS Site_ID
		FROM #MH
	END

	EXEC sp_xml_removedocument @handle
END


GO

