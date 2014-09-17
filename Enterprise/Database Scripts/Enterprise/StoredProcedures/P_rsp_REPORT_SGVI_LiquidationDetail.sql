USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_REPORT_SGVI_LiquidationDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_REPORT_SGVI_LiquidationDetail]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].rsp_REPORT_SGVI_LiquidationDetail  
 
	@Batch_No INT = 0   
	
AS    

SET DATEFORMAT dmy 
SET NOCOUNT ON 
IF @Batch_No = 0 SET @Batch_No = NULL

DECLARE 	
	@MH_ID  INT ,
	@MH_Installation_No INT,
	@MH_BILL_100 INT,
	@MH_BILL_50 INT,
	@MH_BILL_20 INT,
	@MH_BILL_10 INT,
	@MH_BILL_5 INT,
	@MH_BILL_1 INT,
	@MH_TICKET_INSERTED_VALUE BIGINT,
	@MH_TICKET_PRINTED_VALUE INT,
	@MH_HANDPAY INT,
	@MH_JACKPOT INT,
	@JACKPOTDiff INT,
	@TicketsInDiff INT,
	@TicketsOutDiff INT,
	@HandPayDiff INT,
	@Collection_No  INT,
	@CASH_IN_100P INT,
	@CASH_IN_500P INT,
	@CASH_IN_1000P INT,
	@CASH_IN_2000P INT,
	@CASH_IN_5000P INT,
	@CASH_IN_10000P INT,
	@Collected_100p INT,
	@Collected_500p INT,
	@Collected_1000p INT,
	@Collected_2000p INT,
	@Collected_5000P INT,
	@Collected_10000P INT,
	@COLLECTION_JACKPOTAct float,
	@DecTicketsInAct real,
	@DecTicketsOutAct float,
	@DecHandPayAct real,
	@Position varchar(50),
	@Asset_Number varchar(50),
	@Serial_Number varchar(50),
	@Collection_DateTime VARCHAR(100),
	@Merged_Site_Batch_No VARCHAR(50),
	@Deleted_Site_Batch_No VARCHAR(50)

--Create temp table
IF object_id('tempdb..#TempMeter') IS NULL 
	CREATE TABLE #TempMeter (
	P INT,  
	C INT ,
	MH_Installation_No INT,  
MH_Collection_No INT,
	Position varchar(50),
	Asset_Number varchar(50),
	Serial_Number varchar(50),
	Collection_DateTime VARCHAR(100),
	Site_Batch_No VARCHAR(100),
	MH_BILL_100P int,
	MH_BILL_50P int,
	MH_BILL_20P int,
	MH_BILL_10P int,
	MH_BILL_5P int,
	MH_BILL_1P int,
	TICKET_INP bigint ,
	TICKET_OUTP int,
	HANDPAYP int,
	JACKPOTP int,
	MH_BILL_100C int,
	MH_BILL_50C int,
	MH_BILL_20C int,
	MH_BILL_10C int,
	MH_BILL_5C int,
	MH_BILL_1C int,
	TICKET_INC bigint,
	TICKET_OUTC int,
	HANDPAYC int, 
	JACKPOTC int,
	MH_BILL_100DIFF int,
	MH_BILL_50DIFF int,
	MH_BILL_20DIFF int,
	MH_BILL_10DIFF int,
	MH_BILL_5DIFF int,
	MH_BILL_1DIFF int,
	TICKET_INDIFF bigint,
	TICKET_OUTDIFF int,
	HANDPAYDIFF int, 
	JACKPOTDIFF int,
	MH_BILL_100Act int,
	MH_BILL_50Act int,
	MH_BILL_20Act int,
	MH_BILL_10Act int,
	MH_BILL_5Act int,
	MH_BILL_1Act int,
	JACKPOTAct	float,
	TicketsInAct real,
	TicketsOutAct float,
	HandPayAct	real
	)  
	
	SELECT 
		@Merged_Site_Batch_No = Convert(Varchar(10),RIGHT(B.Batch_Ref, LEN(B.Batch_Ref) - LEN(LEFT(B.Batch_Ref, 5))))
	FROM Batch B 
	WHERE B.Batch_ID = @Batch_No
	
	
--Declare the cursor to loop through the collection id's for the batch no.
DECLARE Get_CollectionIdCursor CURSOR LOCAL READ_ONLY FOR

	--SELECT Collection_ID FROM COLLECTION WHERE Batch_ID=@Batch_No
	SELECT DISTINCT COLL.Collection_ID FROM COLLECTION COLL
	JOIN Meter_History MH on MH.MH_Installation_No=COLL.Installation_ID	
	WHERE COLL.Batch_ID=@Batch_No
	
--open the cursor 
OPEN Get_CollectionIdCursor

FETCH NEXT FROM Get_CollectionIdCursor INTO @Collection_No

	WHILE @@FETCH_STATUS = 0
	BEGIN
	--insert the temp table with C records for the collection number
		INSERT INTO #TempMeter
		(C,
		MH_Installation_No,
  MH_Collection_No,
		MH_BILL_100C,
		MH_BILL_50C,
		MH_BILL_20C,
		MH_BILL_10C,
		MH_BILL_5C,
		MH_BILL_1C,
		TICKET_INC,
		TICKET_OUTC,
		HANDPAYC , 
		JACKPOTC
		)
		SELECT 
		MH_ID,
		MH_Installation_No,
  Mh_Linkreference, 
		MH_BILL_100,
		MH_BILL_50,
		MH_BILL_20,
		MH_BILL_10,
		MH_BILL_5,
		MH_BILL_1,		
		MH_TICKET_INSERTED_VALUE ,
		MH_TICKET_PRINTED_VALUE,
		MH_HANDPAY,
		MH_JACKPOT

		FROM Meter_History WHERE Mh_Linkreference=@Collection_No and Mh_type='C' and Mh_Process='COLL'
		
		--select the installation number from the temptable
		SELECT @MH_Installation_No=MH_Installation_No FROM #TempMeter
		
		--Update the temp table with P records for the collection number based on the installation no.
		SELECT 
		@MH_ID=MH_ID,
		@MH_BILL_100=MH_BILL_100,
		@MH_BILL_50=MH_BILL_50,
		@MH_BILL_20=MH_BILL_20,
		@MH_BILL_10=MH_BILL_10,
		@MH_BILL_5=MH_BILL_5,
		@MH_BILL_1=MH_BILL_1,
		@MH_TICKET_INSERTED_VALUE=MH_TICKET_INSERTED_VALUE ,
		@MH_TICKET_PRINTED_VALUE =MH_TICKET_PRINTED_VALUE,
		@MH_HANDPAY =MH_HANDPAY,
		@MH_JACKPOT =MH_JACKPOT
		
		FROM Meter_History WHERE Mh_Linkreference=@Collection_No and Mh_type='P' and Mh_Process='COLL'

		UPDATE #TempMeter 
		SET 
		#TempMeter.P=@MH_ID,
		#TempMeter.MH_BILL_100P=@MH_BILL_100,
		#TempMeter.MH_BILL_50P=@MH_BILL_50,
		#TempMeter.MH_BILL_20P=@MH_BILL_20,
		#TempMeter.MH_BILL_10P=@MH_BILL_10,
		#TempMeter.MH_BILL_5P=@MH_BILL_5,
		#TempMeter.MH_BILL_1P=@MH_BILL_1,
		#TempMeter.TICKET_INP=@MH_TICKET_INSERTED_VALUE,
		#TempMeter.TICKET_OUTP=@MH_TICKET_PRINTED_VALUE,
		#TempMeter.HANDPAYP=@MH_HANDPAY,
		#TempMeter.JACKPOTP=@MH_JACKPOT
  
  WHERE #TempMeter.MH_Installation_No=@MH_Installation_No  AND #TempMeter.MH_Collection_No = @Collection_No
		
		--Clear temp variables
		set @MH_ID=''
		set @MH_BILL_100=''
		set @MH_BILL_50=''
		set @MH_BILL_20=''
		set @MH_BILL_10=''
		set @MH_BILL_5=''
		set @MH_BILL_1=''
		set @MH_TICKET_INSERTED_VALUE=''
		set @MH_TICKET_PRINTED_VALUE=''
		set @MH_HANDPAY=''
		set @MH_JACKPOT=''
		--Update the temp table with delta records for the P&C records for the collection number
		Select 
		@CASH_IN_100P=CASH_IN_100P,
		@CASH_IN_500P=CASH_IN_500P,
		@CASH_IN_1000P=CASH_IN_1000P,
		@CASH_IN_2000P=CASH_IN_2000P,
		@CASH_IN_5000P=CASH_IN_5000P,
		@CASH_IN_10000P=CASH_IN_10000P,
		@JACKPOTDiff =COLLECTION_RDC_JACKPOT,
		@TicketsInDiff =COLLECTION_RDC_TICKETS_INSERTED_VALUE,
		@TicketsOutDiff =COLLECTION_RDC_TICKETS_PRINTED_VALUE,
		@HandPayDiff =Collection_RDC_Handpay,		
		@Collection_DateTime = Collection_Date_Of_Collection
		from Collection where Collection_ID=@Collection_No

		UPDATE #TempMeter SET 
		MH_BILL_100DIFF=@CASH_IN_10000P ,
		MH_BILL_50DIFF =@CASH_IN_5000P,
		MH_BILL_20DIFF =@CASH_IN_2000P,
		MH_BILL_10DIFF =@CASH_IN_1000P,
		MH_BILL_5DIFF =@CASH_IN_500P,
		MH_BILL_1DIFF=@CASH_IN_100P,
		TICKET_INDIFF =@TicketsInDiff,
		TICKET_OUTDIFF =@TicketsOutDiff,
		HANDPAYDIFF =@HandPayDiff, 
		JACKPOTDIFF =@JACKPOTDiff,
		Collection_DateTime= @Collection_DateTime
  WHERE #TempMeter.MH_Installation_No=@MH_Installation_No  AND #TempMeter.MH_Collection_No = @Collection_No
		--Clear temp variables 
		set @CASH_IN_10000P=''
		set @CASH_IN_5000P=''
		set @CASH_IN_2000P=''
		set @CASH_IN_1000P=''
		set @CASH_IN_500P=''
		set @CASH_IN_100P=''
		set @TicketsInDiff=''
		set @TicketsOutDiff=''
		set @HandPayDiff=''
		set @JACKPOTDiff=''
		SET @Collection_DateTime = ''
	
		--Update the temp table with Actual & Machine details records for the collection number
		SELECT
		@Position=VWCD.PosName ,
		@Asset_Number=VWCD.StockNo ,
		@Serial_Number=M.Machine_Manufacturers_Serial_No ,
		@Collected_10000P=VWCD.Cash_Collected_10000p ,
		@Collected_5000P=VWCD.Cash_Collected_5000p ,
		@Collected_2000p=VWCD.Cash_Collected_2000p ,
		@Collected_1000p=VWCD.Cash_Collected_1000p ,
		@Collected_500p=VWCD.Cash_Collected_500P ,
		@Collected_100p=VWCD.Cash_Collected_100p ,
		@COLLECTION_JACKPOTAct=VWCD.COLLECTION_RDC_JACKPOT,
		@DecTicketsInAct=VWCD.Declared_Tickets,
		@DecTicketsOutAct=VWCD.TicketsOut,
		--@DecHandPayAct=VWCD.DecHandPay
		@DecHandPayAct=(CAST(C.Collection_RDC_Handpay AS FLOAT) * Installation.Installation_Price_Per_Play) / 100
		FROM VW_CollectionData VWCD
			JOIN Installation ON Installation.Installation_Id=VWCD.Installation_ID
			JOIN Collection C on C.Collection_ID=VWCD.Collection_ID
			JOIN Machine M ON Installation.Machine_ID=M.Machine_ID
		WHERE VWCD.Collection_ID=@Collection_No

		UPDATE #TempMeter SET 
		Position =@Position,
		Asset_Number =@Asset_Number,
		Serial_Number=@Serial_Number ,
		MH_BILL_100Act=@Collected_10000P ,
		MH_BILL_50Act=@Collected_5000P ,
		MH_BILL_20Act =@Collected_2000p,
		MH_BILL_10Act =@Collected_1000p,
		MH_BILL_5Act =@Collected_500p,
		MH_BILL_1Act=@Collected_100p ,
		JACKPOTAct=@COLLECTION_JACKPOTAct,
		TicketsInAct=@DecTicketsInAct,
		TicketsOutAct=@DecTicketsOutAct ,
		HandPayAct=@DecHandPayAct 
  WHERE #TempMeter.MH_Installation_No=@MH_Installation_No  AND #TempMeter.MH_Collection_No = @Collection_No
  
  
		SET @Deleted_Site_Batch_No = NULL
		SELECT 
			TOP 1 @Deleted_Site_Batch_No = SUBSTRING(Deleted_Batch_Ref, 6, LEN(Deleted_Batch_Ref)) 
		FROM Merged_Batch_Details WITH(NOLOCK) 
	    WHERE ', ' + Deleted_Collection_Nos + ',' LIKE '%, '+ CAST(@Collection_No AS VARCHAR(20)) + ',%' ORDER BY Merged_ID ASC
				
		UPDATE #TempMeter 
			SET Site_Batch_No = ISNULL(@Deleted_Site_Batch_No, @Merged_Site_Batch_No)
		WHERE #TempMeter.MH_Installation_No = @MH_Installation_No AND #TempMeter.MH_Collection_No = @Collection_No

		--Clear temp variables
		set @Position=''
		set @Asset_Number=''
		set @Serial_Number=''
		set @Collected_10000P=''
		set @Collected_5000P=''
		set @Collected_2000p=''
		set @Collected_1000p=''
		set @Collected_500p=''
		set @Collected_100p=''
		set @COLLECTION_JACKPOTAct=''
		set @DecTicketsInAct=''
		set @DecTicketsOutAct=''
		set @DecHandPayAct=''
		set @MH_Installation_No=''
		set @Collection_No=''
		
	--Loop for next record.
	FETCH NEXT FROM Get_CollectionIdCursor INTO @Collection_No
	END
--destroy cursor
CLOSE Get_CollectionIdCursor
DEALLOCATE Get_CollectionIdCursor

--select report data from temp table
SELECT 
	P ,  
	C  ,
 P as MH_Installation_No, 
	Site_Batch_No,   
	Position ,
	Asset_Number ,
	Serial_Number ,
	Collection_DateTime,
	MH_BILL_100P ,
	MH_BILL_50P ,
	MH_BILL_20P ,
	MH_BILL_10P ,
	MH_BILL_5P ,
	MH_BILL_1P ,
	TICKET_INP ,
	TICKET_OUTP,
	HANDPAYP,
	JACKPOTP,
	MH_BILL_100C ,
	MH_BILL_50C ,
	MH_BILL_20C ,
	MH_BILL_10C ,
	MH_BILL_5C ,
	MH_BILL_1C ,
	TICKET_INC ,
	TICKET_OUTC,
	HANDPAYC,
	JACKPOTC,
	MH_BILL_100DIFF ,
	MH_BILL_50DIFF ,
	MH_BILL_20DIFF ,
	MH_BILL_10DIFF ,
	MH_BILL_5DIFF ,
	MH_BILL_1DIFF ,
	TICKET_INDIFF,
	TICKET_OUTDIFF,
	HANDPAYDIFF,
	JACKPOTDIFF,
	MH_BILL_100Act ,
	MH_BILL_50Act ,
	MH_BILL_20Act ,
	MH_BILL_10Act ,
	MH_BILL_5Act ,
	MH_BILL_1Act ,
	TicketsInAct,
	TicketsOutAct ,
	HandPayAct,
	JACKPOTAct	
 FROM #TempMeter
 ORDER BY Position, Site_Batch_No
DROP TABLE  #TempMeter

GO

