USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rsp_GetSubCoCollectionValidationDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rsp_GetSubCoCollectionValidationDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-------------------------------------------------------------------------- 
--
-- Description: Gets the required fields for the Sub Company Collection Validation.
--
-- Inputs:      See inputs
--
-- Outputs:     Bar_Position_ID,Bar_Position_Name,Site_Code,Site_Name & Site_ID 
--
-- =======================================================================
-- 
-- Revision History
-- 
-- Renjish N   24/06/08   Created 
-- 
--------------------------------------------------------------------------- 
CREATE PROCEDURE [dbo].[rsp_GetSubCoCollectionValidationDetails]
   
    @SubCompanyID  INT,
	@DaysToLookAt INT

AS 

SELECT  Bar_Position.Bar_Position_ID, 
		Collection.Collection_Date, 
		Collection.CashCollected, 
		Collection.Collection_ID, 
		Machine_Class.Machine_Name, 
		Machine_Type.Machine_Type_Code, 
		Machine.Machine_Stock_No, 
		Installation.Installation_Start_Date, 
		Collection_Details.Collection_Days, 
		Collection_Details.Period_End_ID, 
		Collection.Collection_Replacement, 
		Collection.Collection_Processed_Through_Terms, 
		Collection.Collection_Terms_Invalid, 
		Collection.Collection_Terms_Invalid_Ignore, 
		Collection_Details.EDI_Import_Log_ID
FROM 
Bar_Position 
INNER JOIN Installation ON Bar_Position.Bar_Position_ID = Installation.Bar_Position_ID
INNER JOIN Site ON Bar_Position.Site_ID = Site.Site_ID
INNER JOIN Collection ON Installation.Installation_ID = Collection.Installation_ID
INNER JOIN Collection_Details ON Collection.Collection_ID = Collection_Details.Collection_ID
INNER JOIN Machine ON Installation.Machine_ID = Machine.Machine_ID
INNER JOIN Machine_Class ON Machine.Machine_Class_ID = Machine_Class.Machine_Class_ID
INNER JOIN Machine_Type ON Machine_Class.Machine_Type_ID = Machine_Type.Machine_Type_ID
WHERE Site.Sub_Company_ID = @SubCompanyID 
AND Datediff(d, Collection_Date, GetDate()) <= @DaysToLookAt
ORDER BY Cast(Collection_Date as datetime) ASC




GO

