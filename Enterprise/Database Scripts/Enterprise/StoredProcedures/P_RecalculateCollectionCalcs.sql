USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RecalculateCollectionCalcs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RecalculateCollectionCalcs]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE RecalculateCollectionCalcs

  @Collection_ID INT

AS

DECLARE @Batch_ID as INT
DECLARE @Installation_ID as INT
DECLARE @Datapak_ID as INT
DECLARE @IsSAS as BIT
DECLARE @Defloat as FLOAT
DECLARE @GrossCash as FLOAT
DECLARE @ManualRefills as FLOAT
DECLARE @Refills as FLOAT
DECLARE @Refunds as FLOAT
DECLARE @HopperChange as FLOAT
DECLARE @Ticket as FLOAT
DECLARE @NetCash as FLOAT
DECLARE @CashTake as FLOAT
DECLARE @RDCCash as FLOAT
DECLARE @RDCRefill as FLOAT
DECLARE @RDCVar as FLOAT
DECLARE @MeterCash as FLOAT
DECLARE @MeterRefill as FLOAT
DECLARE @MeterVar as FLOAT
DECLARE @VTP as FLOAT
DECLARE @PercentageIn as FLOAT
DECLARE @PercentageOut as FLOAT
DECLARE @DecHandpay as FLOAT
DECLARE @RDCHandpay as FLOAT
DECLARE @MeterHandpay as FLOAT
DECLARE @Total_Fault_Events as INT
DECLARE @Total_Door_Events as INT
DECLARE @Total_Power_Events as INT
DECLARE @Collection_Total_Power_Duration as FLOAT

DECLARE @Handpay_Var as FLOAT
DECLARE @RDC_Total_Coinage_In as FLOAT
DECLARE @RDC_Total_Coinage_Out as FLOAT
DECLARE @RDC_Coins as FLOAT
DECLARE @RDC_Total_Notes_In as FLOAT
DECLARE @RDC_Total_Notes_Out as FLOAT
DECLARE @RDC_Notes as FLOAT
DECLARE @Declared_Total_Coinage as FLOAT
DECLARE @Declared_Total_Notes as FLOAT
DECLARE @DeclaredTicketValue as FLOAT
DECLARE @Note_Var as FLOAT
DECLARE @Net_Coin as FLOAT
DECLARE @Coin_Var as FLOAT
DECLARE @Tickets_Printed as FLOAT
DECLARE @Ticket_Balance as FLOAT
DECLARE @RDC_Tickets_In as FLOAT
DECLARE @RDC_Tickets_Out as FLOAT
DECLARE @RDC_Ticket_Balance as FLOAT
DECLARE @Ticket_Var as FLOAT
DECLARE @Cash_Take as FLOAT
DECLARE @RDC_Take as FLOAT
DECLARE @Take_Var as FLOAT

-- get info from collection
SELECT

@Batch_ID = VW_CalculatedData.Batch_ID,
@Installation_ID = VW_CalculatedData.Installation_ID,
@Datapak_ID = VW_CalculatedData.Datapak_ID,
@IsSAS = VW_CalculatedData.IsSAS,
@Defloat = VW_CalculatedData.Defloat,
@GrossCash = VW_CalculatedData.GrossCash,
@ManualRefills = VW_CalculatedData.ManualRefills,
@Refills = VW_CalculatedData.Refills,
@Refunds = VW_CalculatedData.Refunds,
@HopperChange = VW_CalculatedData.HopperChange,
@Ticket = VW_CalculatedData.Ticket,
@NetCash = VW_CalculatedData.NetCash,
@CashTake = VW_CalculatedData.CashTake,
@RDCCash = VW_CalculatedData.RDCCash,
@RDCRefill = VW_CalculatedData.RDCRefill,
@RDCVar = VW_CalculatedData.RDCVar,
@MeterCash = VW_CalculatedData.MeterCash,
@MeterRefill = VW_CalculatedData.MeterRefill,
@MeterVar = VW_CalculatedData.MeterVar,
@VTP = VW_CalculatedData.VTP,
@PercentageIn = VW_CalculatedData.PercentageIn,
@PercentageOut = VW_CalculatedData.PercentageOut,
@DecHandpay = VW_CalculatedData.DecHandpay,
@RDCHandpay = VW_CalculatedData.RDCHandpay,
@MeterHandpay = VW_CalculatedData.MeterHandpay,
@Total_Fault_Events = VW_CalculatedData.Total_Fault_Events,
@Total_Door_Events = VW_CalculatedData.Total_Door_Events,
@Total_Power_Events = VW_CalculatedData.Total_Power_Events,
@Collection_Total_Power_Duration = VW_CalculatedData.Collection_Total_Power_Duration,

@Handpay_Var = VW_CalculatedData.Handpay_Var,
@RDC_Total_Coinage_In = VW_CalculatedData.RDC_Total_Coinage_In,
@RDC_Total_Coinage_Out = VW_CalculatedData.RDC_Total_Coinage_Out ,
@RDC_Coins = VW_CalculatedData.RDC_Coins ,
@RDC_Total_Notes_In = VW_CalculatedData.RDC_Total_Notes_In ,
@RDC_Total_Notes_Out = VW_CalculatedData.RDC_Total_Notes_Out, 
@RDC_Notes = VW_CalculatedData.RDC_Notes ,
@Declared_Total_Coinage = VW_CalculatedData.Declared_Total_Coinage ,
@Declared_Total_Notes = VW_CalculatedData.Declared_Total_Notes ,
@DeclaredTicketValue = VW_CalculatedData.DeclaredTicketValue ,
@Note_Var = VW_CalculatedData.Note_Var ,
@Net_Coin = VW_CalculatedData.Net_Coin ,
@Coin_Var = VW_CalculatedData.Coin_Var ,
@Tickets_Printed = VW_CalculatedData.Tickets_Printed ,
@Ticket_Balance = VW_CalculatedData.Ticket_Balance ,
@RDC_Tickets_In = VW_CalculatedData.RDC_Tickets_In, 
@RDC_Tickets_Out = VW_CalculatedData.RDC_Tickets_Out, 
@RDC_Ticket_Balance = VW_CalculatedData.RDC_Ticket_Balance ,
@Ticket_Var = VW_CalculatedData.Ticket_Var ,
@Cash_Take = VW_CalculatedData.Cash_Take, 
@RDC_Take = VW_CalculatedData.RDC_Take ,
@Take_Var = VW_CalculatedData.Take_Var 

FROM VW_CalculatedData WHERE Collection_ID = @Collection_ID

IF NOT @Collection_ID IS NULL
BEGIN

	IF NOT Exists (SELECT Collection_ID FROM Collection_Calcs WHERE Collection_ID = @Collection_ID)
		BEGIN
			INSERT INTO Collection_Calcs
					(Collection_ID)
				VALUES
					(@Collection_ID)
		END
	
	UPDATE Collection_Calcs
	
	SET 
	
	Batch_ID = @Batch_ID,
	Installation_ID = @Installation_ID,
	Datapak_ID = @Datapak_ID,
	IsSAS = @IsSAS,
	Collection_Defloat = @Defloat,
	Collection_GrossCash = @GrossCash,
	Collection_Refills = @Refills,
	Collection_Refunds = @Refunds,
	Collection_ManualRefills = @ManualRefills,
	Collection_HopperChange = @HopperChange,
	Collection_Ticket = @Ticket,
	Collection_NetCash = @NetCash,
	Collection_CashTake = COALESCE ( @CashTake, 0),
	Collection_RDCCash = @RDCCash,
	Collection_RDCRefill = @RDCRefill,
	Collection_RDCVar = @RDCVar,
	Collection_MeterCash = @MeterCash,
	Collection_MeterRefill = @MeterRefill,
	Collection_MeterVar = @MeterVar,
	Collection_VTP = @VTP,
	Collection_PercentageIn = @PercentageIn,
	Collection_PercentageOut = @PercentageOut,
	Collection_DecHandpay = @DecHandpay,
	Collection_RDCHandpay = @RDCHandpay,
	Collection_MeterHandpay = @MeterHandpay,
	Collection_FaultEvents = @Total_Fault_Events,
	Collection_DoorEvents = @Total_Door_Events,
	Collection_PowerEvents = @Total_Power_Events,
	Collection_PowerDuration  = @Collection_Total_Power_Duration,
	
	Collection_Handpay_Var = @Handpay_Var,
	Collection_RDC_Coins_In = @RDC_Total_Coinage_In,
	Collection_RDC_Coins_Out = @RDC_Total_Coinage_Out,
	Collection_RDC_Coins = @RDC_Coins,
	Collection_RDC_Notes_In = @RDC_Total_Notes_In,
	Collection_RDC_Notes_Out = @RDC_Total_Notes_Out,
	Collection_RDC_Notes = @RDC_Notes,
	Collection_Declared_Coins = @Declared_Total_Coinage,
	Collection_Declared_Notes = @Declared_Total_Notes,
	Collection_Declared_Tickets = @DeclaredTicketValue,
	Collection_Note_Var = @Note_Var,
	Collection_Net_Coin = @Net_Coin,
	Collection_Coin_Var = @Coin_Var,
	Collection_Tickets_Printed = @Tickets_Printed,
	Collection_Ticket_Balance = @Ticket_Balance,
	Collection_RDC_Tickets_In = @RDC_Tickets_In,
	Collection_RDC_Tickets_Out = @RDC_Tickets_Out,
	Collection_RDC_Ticket_Balance = @RDC_Ticket_Balance,
	Collection_Ticket_Var = COALESCE ( @Ticket_Var, 0),
	Collection_Cash_Take = COALESCE ( @Cash_Take, 0),
	Collection_RDC_Take = COALESCE ( @RDC_Take, 0),
	Collection_Take_Var = COALESCE ( @Take_Var, 0)
	
	WHERE Collection_Calcs.Collection_ID = @Collection_ID
	
END




GO

