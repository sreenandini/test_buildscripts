USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_CheckNUpdateSpikes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_CheckNUpdateSpikes]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE usp_CheckNUpdateSpikes
  @Process			varchar(10),
  @LReference		int,
  @VTPHour			int=-1

AS

declare @isException as bit
declare @maxSpikevalue as int
declare @ExceptionType as smallint
declare @InstallationId as int
declare @ExceptionUser as varchar(50)
set @isException=0
set @maxSpikevalue = 10000
set @ExceptionUser = 'IMPORT SYSTEM'

-- dump the C record of Meter_History into the temp table

select * into #tempExMH from Meter_History  where MH_ID in 
(Select max(MH_ID) from Meter_history where MH_Type='C' and MH_Process=@Process 
  and MH_LinkReference = @LReference
   and
 (
       @VTPHour <> -1
       and MH_Reference=@VTPHour
	   or 1=1
 )
)

-- Update the temp table with C-P(Deltas) from Meter_History

Update #tempExMH set
#tempExMH.MH_PAYOUT_FLOAT_TOKEN= #tempExMH.MH_PAYOUT_FLOAT_TOKEN - MH.MH_PAYOUT_FLOAT_TOKEN, 
#tempExMH.MH_PAYOUT_FLOAT_10P= #tempExMH.MH_PAYOUT_FLOAT_10P - MH.MH_PAYOUT_FLOAT_10P, 
#tempExMH.MH_PAYOUT_FLOAT_20P= #tempExMH.MH_PAYOUT_FLOAT_20P - MH.MH_PAYOUT_FLOAT_20P, 
#tempExMH.MH_PAYOUT_FLOAT_50P= #tempExMH.MH_PAYOUT_FLOAT_50P - MH.MH_PAYOUT_FLOAT_50P, 
#tempExMH.MH_PAYOUT_FLOAT_100P= #tempExMH.MH_PAYOUT_FLOAT_100P - MH.MH_PAYOUT_FLOAT_100P, 
#tempExMH.MH_CASH_IN_2P= #tempExMH.MH_CASH_IN_2P - MH.MH_CASH_IN_2P, 
#tempExMH.MH_CASH_IN_5P= #tempExMH.MH_CASH_IN_5P - MH.MH_CASH_IN_5P, 
#tempExMH.MH_CASH_IN_10P= #tempExMH.MH_CASH_IN_10P - MH.MH_CASH_IN_10P, 
#tempExMH.MH_CASH_IN_20P= #tempExMH.MH_CASH_IN_20P - MH.MH_CASH_IN_20P, 
#tempExMH.MH_CASH_IN_50P= #tempExMH.MH_CASH_IN_50P - MH.MH_CASH_IN_50P, 
#tempExMH.MH_CASH_IN_100P= #tempExMH.MH_CASH_IN_100P - MH.MH_CASH_IN_100P,
#tempExMH.MH_CASH_IN_200P= #tempExMH.MH_CASH_IN_200P - MH.MH_CASH_IN_200P, 
#tempExMH.MH_CASH_IN_500P= #tempExMH.MH_CASH_IN_500P - MH.MH_CASH_IN_500P, 
#tempExMH.MH_CASH_IN_1000P= #tempExMH.MH_CASH_IN_1000P - MH.MH_CASH_IN_1000P, 
#tempExMH.MH_CASH_IN_2000P= #tempExMH.MH_CASH_IN_2000P - MH.MH_CASH_IN_2000P, 
#tempExMH.MH_CASH_IN_5000P= #tempExMH.MH_CASH_IN_5000P - MH.MH_CASH_IN_5000P, 
#tempExMH.MH_CASH_IN_10000P= #tempExMH.MH_CASH_IN_10000P - MH.MH_CASH_IN_10000P, 
#tempExMH.MH_CASH_IN_20000P= #tempExMH.MH_CASH_IN_20000P - MH.MH_CASH_IN_20000P, 
#tempExMH.MH_CASH_IN_50000P= #tempExMH.MH_CASH_IN_50000P - MH.MH_CASH_IN_50000P, 
#tempExMH.MH_CASH_IN_100000P= #tempExMH.MH_CASH_IN_100000P - MH.MH_CASH_IN_100000P, 
#tempExMH.MH_TOKEN_IN_5P= #tempExMH.MH_TOKEN_IN_5P - MH.MH_TOKEN_IN_5P, 
#tempExMH.MH_TOKEN_IN_10P= #tempExMH.MH_TOKEN_IN_10P - MH.MH_TOKEN_IN_10P, 
#tempExMH.MH_TOKEN_IN_20P= #tempExMH.MH_TOKEN_IN_20P - MH.MH_TOKEN_IN_20P, 
#tempExMH.MH_TOKEN_IN_50P= #tempExMH.MH_TOKEN_IN_50P - MH.MH_TOKEN_IN_50P, 
#tempExMH.MH_TOKEN_IN_100P= #tempExMH.MH_TOKEN_IN_100P - MH.MH_TOKEN_IN_100P, 
#tempExMH.MH_TOKEN_IN_200P= #tempExMH.MH_TOKEN_IN_200P - MH.MH_TOKEN_IN_200P, 
#tempExMH.MH_TOKEN_IN_500P= #tempExMH.MH_TOKEN_IN_500P - MH.MH_TOKEN_IN_500P, 
#tempExMH.MH_TOKEN_IN_1000P= #tempExMH.MH_TOKEN_IN_1000P - MH.MH_TOKEN_IN_1000P, 
#tempExMH.MH_CASH_OUT_2P= #tempExMH.MH_CASH_OUT_2P - MH.MH_CASH_OUT_2P, 
#tempExMH.MH_CASH_OUT_5P= #tempExMH.MH_CASH_OUT_5P - MH.MH_CASH_OUT_5P, 
#tempExMH.MH_CASH_OUT_10P= #tempExMH.MH_CASH_OUT_10P - MH.MH_CASH_OUT_10P, 
#tempExMH.MH_CASH_OUT_20P= #tempExMH.MH_CASH_OUT_20P - MH.MH_CASH_OUT_20P, 
#tempExMH.MH_CASH_OUT_50P= #tempExMH.MH_CASH_OUT_50P - MH.MH_CASH_OUT_50P, 
#tempExMH.MH_CASH_OUT_100P= #tempExMH.MH_CASH_OUT_100P - MH.MH_CASH_OUT_100P, 
#tempExMH.MH_CASH_OUT_200P= #tempExMH.MH_CASH_OUT_200P - MH.MH_CASH_OUT_200P, 
#tempExMH.MH_CASH_OUT_500P= #tempExMH.MH_CASH_OUT_500P - MH.MH_CASH_OUT_500P, 
#tempExMH.MH_CASH_OUT_1000P= #tempExMH.MH_CASH_OUT_1000P - MH.MH_CASH_OUT_1000P, 
#tempExMH.MH_CASH_OUT_2000P= #tempExMH.MH_CASH_OUT_2000P - MH.MH_CASH_OUT_2000P, 
#tempExMH.MH_CASH_OUT_5000P= #tempExMH.MH_CASH_OUT_5000P - MH.MH_CASH_OUT_5000P, 
#tempExMH.MH_CASH_OUT_10000P= #tempExMH.MH_CASH_OUT_10000P - MH.MH_CASH_OUT_10000P, 
#tempExMH.MH_CASH_OUT_20000P= #tempExMH.MH_CASH_OUT_20000P - MH.MH_CASH_OUT_20000P, 
#tempExMH.MH_CASH_OUT_50000P= #tempExMH.MH_CASH_OUT_50000P - MH.MH_CASH_OUT_50000P, 
#tempExMH.MH_CASH_OUT_100000P= #tempExMH.MH_CASH_OUT_100000P - MH.MH_CASH_OUT_100000P, 
#tempExMH.MH_TOKEN_OUT_5P= #tempExMH.MH_TOKEN_OUT_5P - MH.MH_TOKEN_OUT_5P, 
#tempExMH.MH_TOKEN_OUT_10P= #tempExMH.MH_TOKEN_OUT_10P - MH.MH_TOKEN_OUT_10P, 
#tempExMH.MH_TOKEN_OUT_20P= #tempExMH.MH_TOKEN_OUT_20P - MH.MH_TOKEN_OUT_20P, 
#tempExMH.MH_TOKEN_OUT_50P= #tempExMH.MH_TOKEN_OUT_50P - MH.MH_TOKEN_OUT_50P, 
#tempExMH.MH_TOKEN_OUT_100P= #tempExMH.MH_TOKEN_OUT_100P - MH.MH_TOKEN_OUT_100P, 
#tempExMH.MH_TOKEN_OUT_200P= #tempExMH.MH_TOKEN_OUT_200P - MH.MH_TOKEN_OUT_200P, 
#tempExMH.MH_TOKEN_OUT_500P= #tempExMH.MH_TOKEN_OUT_500P - MH.MH_TOKEN_OUT_500P, 
#tempExMH.MH_TOKEN_OUT_1000P= #tempExMH.MH_TOKEN_OUT_1000P - MH.MH_TOKEN_OUT_1000P, 
#tempExMH.MH_CASH_REFILL_5P= #tempExMH.MH_CASH_REFILL_5P - MH.MH_CASH_REFILL_5P, 
#tempExMH.MH_CASH_REFILL_10P= #tempExMH.MH_CASH_REFILL_10P - MH.MH_CASH_REFILL_10P, 
#tempExMH.MH_CASH_REFILL_20P= #tempExMH.MH_CASH_REFILL_20P - MH.MH_CASH_REFILL_20P, 
#tempExMH.MH_CASH_REFILL_50P= #tempExMH.MH_CASH_REFILL_50P - MH.MH_CASH_REFILL_50P, 
#tempExMH.MH_CASH_REFILL_100P= #tempExMH.MH_CASH_REFILL_100P - MH.MH_CASH_REFILL_100P, 
#tempExMH.MH_CASH_REFILL_200P= #tempExMH.MH_CASH_REFILL_200P - MH.MH_CASH_REFILL_200P, 
#tempExMH.MH_CASH_REFILL_500P= #tempExMH.MH_CASH_REFILL_500P - MH.MH_CASH_REFILL_500P, 
#tempExMH.MH_CASH_REFILL_1000P= #tempExMH.MH_CASH_REFILL_1000P - MH.MH_CASH_REFILL_1000P, 
#tempExMH.MH_CASH_REFILL_2000P= #tempExMH.MH_CASH_REFILL_2000P - MH.MH_CASH_REFILL_2000P, 
#tempExMH.MH_CASH_REFILL_5000P= #tempExMH.MH_CASH_REFILL_5000P - MH.MH_CASH_REFILL_5000P, 
#tempExMH.MH_CASH_REFILL_10000P= #tempExMH.MH_CASH_REFILL_10000P - MH.MH_CASH_REFILL_10000P, 
#tempExMH.MH_CASH_REFILL_20000P= #tempExMH.MH_CASH_REFILL_20000P - MH.MH_CASH_REFILL_20000P, 
#tempExMH.MH_CASH_REFILL_50000P= #tempExMH.MH_CASH_REFILL_50000P - MH.MH_CASH_REFILL_50000P, 
#tempExMH.MH_CASH_REFILL_100000P= #tempExMH.MH_CASH_REFILL_100000P - MH.MH_CASH_REFILL_100000P, 
#tempExMH.MH_TOKEN_REFILL_5P= #tempExMH.MH_TOKEN_REFILL_5P - MH.MH_TOKEN_REFILL_5P, 
#tempExMH.MH_TOKEN_REFILL_10P= #tempExMH.MH_TOKEN_REFILL_10P - MH.MH_TOKEN_REFILL_10P, 
#tempExMH.MH_TOKEN_REFILL_20P= #tempExMH.MH_TOKEN_REFILL_20P - MH.MH_TOKEN_REFILL_20P, 
#tempExMH.MH_TOKEN_REFILL_50P= #tempExMH.MH_TOKEN_REFILL_50P - MH.MH_TOKEN_REFILL_50P, 
#tempExMH.MH_TOKEN_REFILL_100P= #tempExMH.MH_TOKEN_REFILL_100P - MH.MH_TOKEN_REFILL_100P, 
#tempExMH.MH_TOKEN_REFILL_200P= #tempExMH.MH_TOKEN_REFILL_200P - MH.MH_TOKEN_REFILL_200P, 
#tempExMH.MH_TOKEN_REFILL_500P= #tempExMH.MH_TOKEN_REFILL_500P - MH.MH_TOKEN_REFILL_500P, 
#tempExMH.MH_TOKEN_REFILL_1000P= #tempExMH.MH_TOKEN_REFILL_1000P - MH.MH_TOKEN_REFILL_1000P, 
#tempExMH.MH_COINS_IN= #tempExMH.MH_COINS_IN - MH.MH_COINS_IN, 
#tempExMH.MH_COINS_OUT= #tempExMH.MH_COINS_OUT - MH.MH_COINS_OUT,
#tempExMH.MH_COIN_DROP= #tempExMH.MH_COIN_DROP - MH.MH_COIN_DROP, 
#tempExMH.MH_HANDPAY= #tempExMH.MH_HANDPAY - MH.MH_HANDPAY, 
#tempExMH.MH_EXTERNAL_CREDIT= #tempExMH.MH_EXTERNAL_CREDIT - MH.MH_EXTERNAL_CREDIT, 
#tempExMH.MH_GAMES_BET= #tempExMH.MH_GAMES_BET - MH.MH_GAMES_BET, 
#tempExMH.MH_GAMES_WON= #tempExMH.MH_GAMES_WON - MH.MH_GAMES_WON, 
#tempExMH.MH_NOTES= #tempExMH.MH_NOTES - MH.MH_NOTES, 
#tempExMH.MH_VTP= #tempExMH.MH_VTP - MH.MH_VTP, 
#tempExMH.MH_CANCELLED_CREDITS= #tempExMH.MH_CANCELLED_CREDITS - MH.MH_CANCELLED_CREDITS, 
#tempExMH.MH_GAMES_LOST= #tempExMH.MH_GAMES_LOST - MH.MH_GAMES_LOST, 
#tempExMH.MH_GAMES_SINCE_POWER_UP= #tempExMH.MH_GAMES_SINCE_POWER_UP - MH.MH_GAMES_SINCE_POWER_UP, 
#tempExMH.MH_TRUE_COIN_IN= #tempExMH.MH_TRUE_COIN_IN - MH.MH_TRUE_COIN_IN, 
#tempExMH.MH_TRUE_COIN_OUT= #tempExMH.MH_TRUE_COIN_OUT - MH.MH_TRUE_COIN_OUT, 
#tempExMH.MH_CURRENT_CREDITS= #tempExMH.MH_CURRENT_CREDITS - MH.MH_CURRENT_CREDITS, 
#tempExMH.MH_JACKPOT= #tempExMH.MH_JACKPOT - MH.MH_JACKPOT, 
#tempExMH.MH_BILL_1= #tempExMH.MH_BILL_1 - MH.MH_BILL_1, 
#tempExMH.MH_BILL_2= #tempExMH.MH_BILL_2 - MH.MH_BILL_2, 
#tempExMH.MH_BILL_5= #tempExMH.MH_BILL_5 - MH.MH_BILL_5, 
#tempExMH.MH_BILL_10= #tempExMH.MH_BILL_10 - MH.MH_BILL_10, 
#tempExMH.MH_BILL_20= #tempExMH.MH_BILL_20 - MH.MH_BILL_20, 
#tempExMH.MH_BILL_50= #tempExMH.MH_BILL_50 - MH.MH_BILL_50, 
#tempExMH.MH_BILL_100= #tempExMH.MH_BILL_100 - MH.MH_BILL_100, 
#tempExMH.MH_BILL_250= #tempExMH.MH_BILL_250 - MH.MH_BILL_250, 
#tempExMH.MH_BILL_10000= #tempExMH.MH_BILL_10000 - MH.MH_BILL_10000, 
#tempExMH.MH_BILL_20000= #tempExMH.MH_BILL_20000 - MH.MH_BILL_20000, 
#tempExMH.MH_BILL_50000= #tempExMH.MH_BILL_50000 - MH.MH_BILL_50000, 
#tempExMH.MH_BILL_100000= #tempExMH.MH_BILL_100000 - MH.MH_BILL_100000, 
#tempExMH.MH_TICKET_PRINTED_QTY= #tempExMH.MH_TICKET_PRINTED_QTY - MH.MH_TICKET_PRINTED_QTY, 
#tempExMH.MH_TICKET_PRINTED_VALUE= #tempExMH.MH_TICKET_PRINTED_VALUE - MH.MH_TICKET_PRINTED_VALUE, 
#tempExMH.MH_TICKET_INSERTED_QTY= #tempExMH.MH_TICKET_INSERTED_QTY - MH.MH_TICKET_INSERTED_QTY, 
#tempExMH.MH_TICKET_INSERTED_VALUE= #tempExMH.MH_TICKET_INSERTED_VALUE - MH.MH_TICKET_INSERTED_VALUE
From Meter_History MH where MH.MH_ID in 
(Select max(MH_ID) from Meter_history where MH_Type='P' and MH_Process=@Process 
  and MH_LinkReference = @LReference
   and
 (
       @VTPHour <> -1
       and MH_Reference=@VTPHour
	or 1=1
 )
)


-- check if any column has spikes.

select  @InstallationId = MH_Installation_No, 
	    @isException    = case	 when (MH_PAYOUT_FLOAT_TOKEN > @maxSpikevalue)  then  1 
								 when (MH_PAYOUT_FLOAT_10P  > @maxSpikevalue) then  1 
								 when (MH_PAYOUT_FLOAT_20P  > @maxSpikevalue) then  1 
								 when (MH_PAYOUT_FLOAT_50P >  @maxSpikevalue) then  1 
								 when (MH_PAYOUT_FLOAT_100P >  @maxSpikevalue) then  1 
								 when (MH_CASH_IN_2P >  @maxSpikevalue) then  1 
								 when (MH_CASH_IN_5P >  @maxSpikevalue ) then  1 
								 when (MH_CASH_IN_10P >  @maxSpikevalue) then  1 
								 when (MH_CASH_IN_20P >  @maxSpikevalue) then  1 
								 when (MH_CASH_IN_50P >  @maxSpikevalue) then  1 
								 when (MH_CASH_IN_100P >  @maxSpikevalue) then  1 
								 when (MH_CASH_IN_200P >  @maxSpikevalue) then  1 
								 when (MH_CASH_IN_500P >  @maxSpikevalue) then  1 
								 when (MH_CASH_IN_1000P >  @maxSpikevalue) then  1 
								 when (MH_CASH_IN_2000P >  @maxSpikevalue) then  1 
								 when (MH_CASH_IN_5000P >  @maxSpikevalue) then  1 
								 when (MH_CASH_IN_10000P >  @maxSpikevalue) then  1 
								 when (MH_CASH_IN_20000P >  @maxSpikevalue) then  1 
								 when (MH_CASH_IN_50000P >  @maxSpikevalue) then  1 
								 when (MH_CASH_IN_100000P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_IN_5P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_IN_10P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_IN_20P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_IN_50P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_IN_100P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_IN_200P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_IN_500P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_IN_1000P >  @maxSpikevalue) then  1 
								 when (MH_CASH_OUT_2P >  @maxSpikevalue) then  1 
								 when (MH_CASH_OUT_5P >  @maxSpikevalue) then  1 
								 when (MH_CASH_OUT_10P >  @maxSpikevalue) then  1 
								 when (MH_CASH_OUT_20P >  @maxSpikevalue) then  1 
								 when (MH_CASH_OUT_50P >  @maxSpikevalue) then  1 
								 when (MH_CASH_OUT_100P >  @maxSpikevalue) then  1 
								 when (MH_CASH_OUT_200P >  @maxSpikevalue) then  1 
								 when (MH_CASH_OUT_500P >  @maxSpikevalue) then  1 
								 when (MH_CASH_OUT_1000P >  @maxSpikevalue) then  1 
								 when (MH_CASH_OUT_2000P >  @maxSpikevalue) then  1 
								 when (MH_CASH_OUT_5000P >  @maxSpikevalue) then  1 
								 when (MH_CASH_OUT_10000P >  @maxSpikevalue) then  1 
								 when (MH_CASH_OUT_20000P >  @maxSpikevalue) then  1 
								 when (MH_CASH_OUT_50000P >  @maxSpikevalue) then  1 
								 when (MH_CASH_OUT_100000P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_OUT_5P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_OUT_10P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_OUT_20P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_OUT_50P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_OUT_100P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_OUT_200P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_OUT_500P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_OUT_1000P >  @maxSpikevalue) then  1 
								 when (MH_CASH_REFILL_5P >  @maxSpikevalue) then  1 
								 when (MH_CASH_REFILL_10P >  @maxSpikevalue) then  1 
								 when (MH_CASH_REFILL_20P >  @maxSpikevalue) then  1 
								 when (MH_CASH_REFILL_50P >  @maxSpikevalue) then  1 
								 when (MH_CASH_REFILL_100P >  @maxSpikevalue) then  1 
								 when (MH_CASH_REFILL_200P >  @maxSpikevalue) then  1 
								 when (MH_CASH_REFILL_500P >  @maxSpikevalue) then  1 
								 when (MH_CASH_REFILL_1000P >  @maxSpikevalue) then  1 
								 when (MH_CASH_REFILL_2000P >  @maxSpikevalue) then  1 
								 when (MH_CASH_REFILL_5000P >  @maxSpikevalue) then  1 
								 when (MH_CASH_REFILL_10000P >  @maxSpikevalue) then  1 
								 when (MH_CASH_REFILL_20000P >  @maxSpikevalue) then  1 
								 when (MH_CASH_REFILL_50000P >  @maxSpikevalue) then  1 
								 when (MH_CASH_REFILL_100000P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_REFILL_5P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_REFILL_10P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_REFILL_20P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_REFILL_50P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_REFILL_100P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_REFILL_200P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_REFILL_500P >  @maxSpikevalue) then  1 
								 when (MH_TOKEN_REFILL_1000P >  @maxSpikevalue) then  1 
								 when (MH_COINS_IN >  @maxSpikevalue) then  1 
								 when (MH_COINS_OUT >  @maxSpikevalue) then  1 
								 when (MH_COIN_DROP >  @maxSpikevalue) then  1 
								 when (MH_HANDPAY >  @maxSpikevalue) then  1 
								 when (MH_EXTERNAL_CREDIT >  @maxSpikevalue) then  1 
								 when (MH_GAMES_BET >  @maxSpikevalue) then  1 
								 when (MH_GAMES_WON >  @maxSpikevalue) then  1 
								 when (MH_NOTES >  @maxSpikevalue) then  1 
								 when (MH_VTP >  @maxSpikevalue) then  1 
								 when (MH_CANCELLED_CREDITS >  @maxSpikevalue) then  1 
								 when (MH_GAMES_LOST >  @maxSpikevalue) then  1 
								 when (MH_GAMES_SINCE_POWER_UP >  @maxSpikevalue) then  1 
								 when (MH_TRUE_COIN_IN >  @maxSpikevalue) then  1 
								 when (MH_TRUE_COIN_OUT >  @maxSpikevalue) then  1 
								 when (MH_CURRENT_CREDITS >  @maxSpikevalue) then  1 
								 when (MH_JACKPOT >  @maxSpikevalue) then  1 
								 when (MH_BILL_1 >  @maxSpikevalue) then  1 
								 when (MH_BILL_2 >  @maxSpikevalue) then  1 
								 when (MH_BILL_5 >  @maxSpikevalue) then  1 
								 when (MH_BILL_10 >  @maxSpikevalue) then  1 
								 when (MH_BILL_20 >  @maxSpikevalue) then  1 
								 when (MH_BILL_50 >  @maxSpikevalue) then  1 
								 when (MH_BILL_100 >  @maxSpikevalue) then  1 
								 when (MH_BILL_250 >  @maxSpikevalue) then  1 
								 when (MH_BILL_10000 >  @maxSpikevalue) then  1 
								 when (MH_BILL_20000 >  @maxSpikevalue) then  1 
								 when (MH_BILL_50000 >  @maxSpikevalue) then  1 
								 when (MH_BILL_100000 >  @maxSpikevalue) then  1 
								 when (MH_TICKET_PRINTED_QTY >  @maxSpikevalue) then  1 
								 when (MH_TICKET_PRINTED_VALUE >  @maxSpikevalue) then  1 
								 when (MH_TICKET_INSERTED_QTY >  @maxSpikevalue) then  1 
								 when (MH_TICKET_INSERTED_VALUE >  @maxSpikevalue) then  1 

					  end from #tempExMH

--drop the temp table

 drop table #tempExMH

--check the process and assign exceptionType accordingly

if @Process='VTP'
	set @ExceptionType = 200
else if @Process='READ'
	set @ExceptionType = 201
else if @Process='COLL'
	set @ExceptionType = 202

--Insert into exception table if the record is an exception record

if @isException = 1
    exec usp_InsertException @InstallationId,@ExceptionType, '', @LReference, @ExceptionUser

if @@ERROR <> 0
  begin
   drop table #tempExMH
   return -1
  end

GO

