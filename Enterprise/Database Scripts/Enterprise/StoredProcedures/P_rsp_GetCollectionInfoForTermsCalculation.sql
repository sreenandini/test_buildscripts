USE [Enterprise]
GO

IF EXISTS (
       SELECT *
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[rsp_GetCollectionInfoForTermsCalculation]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[rsp_GetCollectionInfoForTermsCalculation]
GO

CREATE PROCEDURE [dbo].[rsp_GetCollectionInfoForTermsCalculation](@CollectionID INT)
AS
BEGIN
	SELECT Installation_ID,
	       CashCollected,
	       Collection_Date,
	       Previous_Collection_Date,
	       Collection_Days,
	       Collection_Prize_Value,
	       Collection_Sundries,
	       Collection_Sundries_Unsupported,
	       Collection_Supplier_Float_Recovered,
	       Collection_Non_Supplier_Float_Recovered,
	       Collection_Treasury_Defloat,
	       Down_Days,
	       CashRefills
	FROM   COLLECTION
	       INNER JOIN Collection_Details
	            ON  COLLECTION.Collection_ID = Collection_Details.Collection_ID
	WHERE  COLLECTION.Collection_ID = @CollectionID
END
GO
