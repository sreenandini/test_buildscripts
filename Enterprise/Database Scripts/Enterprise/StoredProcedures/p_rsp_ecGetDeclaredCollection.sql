USE Enterprise
GO 
-- Drop stored procedure if it already exists
IF EXISTS (
       SELECT *
       FROM   INFORMATION_SCHEMA.ROUTINES
       WHERE  SPECIFIC_SCHEMA = N'dbo'
              AND SPECIFIC_NAME = N'rsp_ecGetDeclaredCollection'
   )
    DROP PROCEDURE dbo.rsp_ecGetDeclaredCollection
GO

CREATE PROCEDURE dbo.rsp_ecGetDeclaredCollection
	@Collection_ID INT,
	@Site_ID INT
AS
	/*****************************************************************************************************
DESCRIPTION	: PROC Description  
CREATED DATE: PROC CreateDate
MODULE		: PROC used in Modules
rsp_ecGetDeclaredCollection 1,1
	
CHANGE HISTORY :
------------------------------------------------------------------------------------------------------
AUTHOR					DESCRIPTON										MODIFIED DATE
------------------------------------------------------------------------------------------------------

*****************************************************************************************************/

BEGIN
	
	DECLARE @Region VARCHAR(20)  
	
	SELECT @Region = Region
	FROM   [Site] s
	WHERE  s.Site_ID = @Site_id  
	
	SELECT Batch_ID,
	       Site_ID,
	       ISNULL(@Region,'US') Region,
	       Cash_Collected_100p,
	       Cash_Collected_200p,
	       Cash_Collected_500p,
	       Cash_Collected_1000p,
	       Cash_Collected_2000p,
	       Cash_Collected_5000p,
	       Cash_Collected_10000p,
	       Cash_Collected_20000p,
	       Cash_Collected_50000p,
	       Declared_Tickets,
	       Tickets_Printed,
	       DecHandpay,
	       Progressive_Value_Declared,
	       DecHandpay -Progressive_Value_Declared AS Hand_Pay
	FROM   VW_CollectionData
	WHERE  Collection_ID = @Collection_ID
END
GO
