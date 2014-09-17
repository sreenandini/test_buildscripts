USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_InsertCollDetailfromCollection]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_InsertCollDetailfromCollection]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---usp_InsertCollDetailfromCollection.sql
-- ===================================================================================================================================
-- usp_InsertCollDetailfromXML
-- -----------------------------------------------------------------------------------------------------------------------------------
--
-- inserts into Collection batch, collection table from XML
-- 

-- -----------------------------------------------------------------------------------------------------------------------------------
-- Revision History 
-- 
-- 16/05/2008	Sudarsan S	Created
-- 11/07/2008	Renjish N   Modified sp for 'Rerequest of Collection By Date' Requirement.
-- 24/07/2008	Sudarsan S	modified the update to save the Collection_days value not having < 0
-- 18/05/2009	Sudarsan S	modified the code
-- ===================================================================================================================================

--IF EXISTS ( SELECT * FROM Sysobjects WHERE name = 'usp_InsertCollDetailfromXML' AND xtype = 'P')
--	drop procedure [dbo].[usp_InsertCollDetailfromXML]
--GO

CREATE PROCEDURE [dbo].[usp_InsertCollDetailfromCollection]
  @pSite_Code	VARCHAR(50),
  @piCollection_ID	INT,
  @piInstallation_ID	INT,
  @IsSuccess VARCHAR(500) OUTPUT
AS

BEGIN

DECLARE @liCompany_ID	INT
DECLARE @liSub_Company_ID	INT

SET @IsSuccess = 'SUCCESS'

-- declare a table variable to hold the details to eb inserted into the collection_details table.
DECLARE @ltDetailsTable	TABLE
(
	Collection_ID	INT,
	Installation_ID	INT,
	Collection_Company_ID	INT,
	Collection_Sub_Company_ID	INT,
	Collection_Gross	REAL,
	Collection_NetEx	REAL,
	Collection_VAT_Rate	REAL,
	Collection_Days	INT,
	Period_ID	INT,
	Week_ID	INT,
	Collection_Supplier_ID	INT,
	Collection_Supplier_Depot_ID	INT
)


	INSERT INTO @ltDetailsTable(Collection_ID, Installation_ID)
	SELECT @piCollection_ID, @piInstallation_ID


--	fetch the Company_ID and the Sub_Company_ID to update the same on to the table variable
	SELECT @liCompany_ID = C.Company_ID, @liSub_Company_ID = SC.Sub_Company_ID
	FROM dbo.Site S	
	INNER JOIN Sub_Company SC ON S.Sub_Company_ID = SC.Sub_Company_ID
	INNER JOIN Company C ON SC.Company_ID = C.Company_ID
	WHERE Site_Code = @pSite_Code

/*	the column Collection_gross is a calculated value as done below. it depends on the Cash_Refills column for each type				*/
	UPDATE DT
		SET Collection_Company_ID = @liCompany_ID, 
			Collection_Sub_Company_ID = @liSub_Company_ID,
			Collection_Days = CASE WHEN ISNULL(TC.PreviousCollectionDate, '') <> ''	THEN
									DATEDIFF(day, TC.PreviousCollectionDate, TC.Collection_Date)
								ELSE
									0	END,
			Collection_Gross = ISNULL(TC.CashCollected, 0) - ISNULL(TC.Collection_Treasury_Defloat, 0) + ISNULL(TC.CashRefills, 0) + CAST(REPLACE(TC.Treasury_Repayments, 'e+000', '') AS REAL)
								-	(ISNULL(TC.CASH_REFILL_5P, 0) / 20) - (ISNULL(TC.CASH_REFILL_10P, 0) / 10) - (ISNULL(TC.CASH_REFILL_20P, 0) / 5) - (ISNULL(TC.CASH_REFILL_50P, 0) / 2)
								-	ISNULL(TC.CASH_REFILL_100P, 0) - (ISNULL(TC.CASH_REFILL_200P, 0) * 2) - (ISNULL(TC.CASH_REFILL_500P, 0) * 5) - (ISNULL(TC.CASH_REFILL_1000P, 0) * 10)
								-	(ISNULL(TC.CASH_REFILL_2000P, 0) * 20) - (ISNULL(TC.CASH_REFILL_5000P, 0) * 50) - (ISNULL(TC.CASH_REFILL_100000P, 0) * 100),
			Collection_Vat_Rate = 0.175		--	the value 0.175 is a constant value throughout
	FROM @ltDetailsTable DT 
INNER JOIN dbo.#TempCollection TC ON DT.Installation_ID = TC.Installation_No AND DT.Collection_ID = @piCollection_ID 
INNER JOIN dbo.Collection C ON C.Batch_ID = TC.Collection_Batch_No AND C.Installation_ID = TC.Installation_No AND CONVERT(VARCHAR, TC.Collection_Date, 106) = C.Collection_Date AND CONVERT(VARCHAR, TC.Collection_Date, 108) = C.Collection_Time

    WHERE TC.Installation_No = @piInstallation_ID
      AND C.Collection_Id = @piCollection_ID 


		IF @@ERROR <> 0
		BEGIN
			SET @IsSuccess = 'failed in the step to update the Gross, Collectiondays'---4		--	update failed in the initial stage for a table variable
			GOTO Err
		END


/*	Collection_Net_Ex is calculated by multiplying (1-Collection_Vat_Rate) times and the collection_days
	can not be 0, so replace the same to 1				*/
	UPDATE @ltDetailsTable 
		SET Collection_NetEx = Collection_Gross * 0.825,
			Collection_Days = CASE WHEN ISNULL(Collection_Days, 0) <= 0 THEN 1 ELSE Collection_Days END

		IF @@ERROR <> 0
		BEGIN
			SET @IsSuccess = 'Failed in the step to update the Collection_NetEX'---5	--	Failed while updating the table variable with NetEX and collection days
			GOTO Err
		END


/*	update the Supplier_ID and the Supplier_Depot_ID from the Operator and the Depot tables			*/
	UPDATE DT
		SET	Collection_Supplier_ID = ISNULL(O.Operator_ID, 0),
			Collection_Supplier_Depot_ID = ISNULL(D.Depot_ID, 0),
			Collection_Company_ID = ISNULL(C.Company_ID, 0),
			Collection_Sub_Company_ID = ISNULL(SC.Sub_Company_ID, 0)
	FROM dbo.Site S  
INNER JOIN Sub_Company SC ON S.Sub_Company_ID = SC.Sub_Company_ID
INNER JOIN Company C ON SC.Company_ID = C.Company_ID
INNER JOIN Bar_Position BP ON S.Site_ID = BP.Site_ID
INNER JOIN dbo.Installation I ON I.Bar_Position_ID = BP.Bar_Position_ID
INNER JOIN @ltDetailsTable DT ON DT.Installation_ID = I.Installation_ID
LEFT JOIN dbo.Depot D ON BP.Depot_ID = D.Depot_ID
LEFT JOIN Operator O ON D.Supplier_ID = O.Operator_ID 

		IF @@ERROR <> 0
		BEGIN
			SET @IsSuccess = 'Failed while updating the Supplier details'---6		-- Failed while updating the Supplier details'
			GOTO Err
		END

/*	once all the details are fetched, they need to be inserted into the collection_details table	*/



	------------------------------------
	----------Insert Code Start---------
	------------------------------------
	
	INSERT INTO dbo.Collection_Details(
										Collection_ID,
										Collection_Company_ID,
										Collection_Sub_Company_ID,
										Collection_Sundries,
										Collection_Expected_Bagged_Cash,
										Collection_Actual_Bagged_Cash,
										Total_Door_Events,
										Total_Power_Events,
										Total_Fault_Events,
										Collection_Total_Power_Duration,
										Collection_Gross,
										Collection_NetEx,
										Collection_VAT_Rate,
										Collection_Days,
										Collection_Supplier_ID,
										Collection_Supplier_Depot_ID,
										Period_ID,
										Week_ID)

	SELECT	@piCollection_ID,
			DT.Collection_Company_ID,
			DT.Collection_Sub_Company_ID,
			ISNULL(CAST(REPLACE(TC.Treasury_Repayments, 'e+000', '') AS REAL), 0),
			ISNULL(CAST(REPLACE(TC.ExpectedBaggedCash, 'e+000', '') AS REAL), 0),
			ISNULL(CAST(REPLACE(TC.ActualBaggedCash, 'e+000', '') AS REAL), 0),
			TC.CollectionNoDoorEvents,
			TC.CollectionNoPowerEvents,
			TC.CollectionNoFaultEvents,
			TC.CollectionTotalDurationPower,
			DT.Collection_Gross,
			DT.Collection_NetEx,
			DT.Collection_Vat_Rate,
			DT.Collection_Days,
			DT.Collection_Supplier_ID,
			DT.Collection_Supplier_Depot_ID,
			dbo.fnGetWeekorPeriod(DT.Collection_Sub_Company_ID, CONVERT(VARCHAR, TC.Collection_Date, 106), 'Period'),
			dbo.fnGetWeekorPeriod(DT.Collection_Sub_Company_ID, CONVERT(VARCHAR, TC.Collection_Date, 106), 'Week')
	FROM	dbo.#TempCollection TC 
INNER JOIN dbo.Collection C ON C.Batch_ID = TC.Collection_Batch_No AND C.Installation_ID = TC.Installation_No AND CONVERT(VARCHAR, TC.Collection_Date, 106) = C.Collection_Date AND CONVERT(VARCHAR, TC.Collection_Date, 108) = C.Collection_Time
INNER JOIN @ltDetailsTable DT ON TC.Installation_No = DT.Installation_ID AND DT.Collection_ID = @piCollection_ID AND C.Collection_Id = @piCollection_ID AND TC.Installation_No = @piInstallation_ID
 LEFT JOIN dbo.Collection_Details CD ON C.Collection_ID = CD.Collection_ID
	WHERE  C.Collection_ID = @piCollection_ID AND CD.Collection_ID IS NULL
--	WHERE @piCollection_ID NOT IN (SELECT Collection_ID FROM dbo.Collection_Details)	

	


		IF @@ERROR <> 0
		BEGIN
			SET @IsSuccess = 'Failed in Insert command for Collection_Details'---7		--	Failed in Insert command for Collection_Details'
			GOTO Err
		END

	------------------------------------
	----------Insert Code Start---------
	------------------------------------

	------------------------------------
	----------Update Code Start---------
	------------------------------------
	
	UPDATE dbo.Collection_Details
	SET Collection_Details.Collection_Company_ID = DT.Collection_Company_ID,
		Collection_Details.Collection_Sub_Company_ID = DT.Collection_Sub_Company_ID,
		Collection_Details.Collection_Sundries = ISNULL(CAST(REPLACE(TC.Treasury_Repayments, 'e+000', '') AS REAL), 0),
		Collection_Details.Collection_Expected_Bagged_Cash = ISNULL(CAST(REPLACE(TC.ExpectedBaggedCash, 'e+000', '') AS REAL), 0),
		Collection_Details.Collection_Actual_Bagged_Cash = ISNULL(CAST(REPLACE(TC.ActualBaggedCash, 'e+000', '') AS REAL), 0),
		Collection_Details.Total_Door_Events = TC.CollectionNoDoorEvents,
		Collection_Details.Total_Power_Events = TC.CollectionNoPowerEvents,
		Collection_Details.Total_Fault_Events = TC.CollectionNoFaultEvents,
		Collection_Details.Collection_Total_Power_Duration = TC.CollectionTotalDurationPower,
		Collection_Details.Collection_Gross = DT.Collection_Gross,
		Collection_Details.Collection_NetEx = DT.Collection_NetEx,
		Collection_Details.Collection_VAT_Rate = DT.Collection_Vat_Rate,
		Collection_Details.Collection_Days = DT.Collection_Days,
		Collection_Details.Collection_Supplier_ID = DT.Collection_Supplier_ID,
		Collection_Details.Collection_Supplier_Depot_ID = DT.Collection_Supplier_Depot_ID,
		Collection_Details.Period_ID = dbo.fnGetWeekorPeriod(DT.Collection_Sub_Company_ID, CONVERT(VARCHAR, TC.Collection_Date, 106), 'Period'),
		Collection_Details.Week_ID = dbo.fnGetWeekorPeriod(DT.Collection_Sub_Company_ID, CONVERT(VARCHAR, TC.Collection_Date, 106), 'Week')

	FROM	dbo.#TempCollection TC INNER JOIN @ltDetailsTable DT 
	ON TC.Installation_No = DT.Installation_ID AND DT.Collection_ID = @piCollection_ID AND TC.Installation_No = @piInstallation_ID
----	INNER JOIN Collection CO
----	ON TC.Installation_No = CO.Installation_Id AND TC.Collection_Date = CO.Collection_Date AND TC.Collection_Time = CO.Collection_Time
	INNER JOIN dbo.Collection_Details ON Collection_Details.Collection_ID = DT.Collection_ID
		IF @@ERROR <> 0
		BEGIN
			SET @IsSuccess = 'Failed in Update command for Collection_Details'---7		--	Failed in Insert command for Collection_Details'
			GOTO Err
		END

	------------------------------------
	----------Update Code Start---------
	------------------------------------

RETURN 0
		
Err:
RETURN -1

END



GO

