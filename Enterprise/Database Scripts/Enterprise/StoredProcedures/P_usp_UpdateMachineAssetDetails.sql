USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateMachineAssetDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateMachineAssetDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
 * Revision History  
 *   
 * <Name of the owner>         DateCreated       Type(Created/Modified)  Description  
   Kalaiyarasan.P              07-DEC-2012         Created               This SP is used to Add/modify Machine details   
                                                                        
Exec  usp_UpdateMachineAssetDetails  
*/  
CREATE PROCEDURE dbo.usp_UpdateMachineAssetDetails
	@Machine_ID INT,
	@Machine_End_Date DATETIME,
	@Machine_Status_Flag INT,
	@Machine_Sold_To VARCHAR(50),
	@Machine_Type_Of_Sale VARCHAR(150),
	@Machine_Sale_Price MONEY,
	@Staff_ID_Deleted INT,
	@Machine_Date_Deleted DATETIME,
	@Machine_Sales_Invoice_Number VARCHAR(50)
AS
BEGIN

/*****************************************************************************************************
* (CR# 191254 : EBS Communication Service) - MODIFIED BY Venkatesh.H  (START)
*****************************************************************************************************/

DECLARE @_Modified TABLE (
        MachineId INT,
        OldFlag INT, NewFlag INT,
        OldGameID INT, NewGameID INT,
        OldCMPGameType varchar(50), NewCMPGameType varchar(50),
        OldStockNo varchar(50), NewStockNo varchar(50),
        FlagChanged AS (CASE WHEN OldFlag = NewFlag THEN 0 ELSE 1 END),
        GameIDChanged AS (CASE WHEN OldGameID = NewGameID THEN 0 ELSE 1 END),           
        CMPGameTypeChanged AS (CASE WHEN OldCMPGameType = NewCMPGameType THEN 0 ELSE 1 END),
        StockNoChanged AS (CASE WHEN OldStockNo = NewStockNo THEN 0 ELSE 1 END)
 )

	UPDATE MACHINE
	SET    Machine_End_Date = @Machine_End_Date,
	       Machine_Status_Flag = @Machine_Status_Flag,
	       Machine_Sold_To = COALESCE(@Machine_Sold_To, Machine_Sold_To),
	       Machine_Type_Of_Sale = COALESCE(@Machine_Type_Of_Sale, Machine_Type_Of_Sale),
	       Machine_Sale_Price = @Machine_Sale_Price,
	       Staff_ID_Deleted = @Staff_ID_Deleted,
	       Machine_Date_Deleted = @Machine_Date_Deleted,
	       Machine_Sales_Invoice_Number = COALESCE(
	           @Machine_Sales_Invoice_Number,
	           Machine_Sales_Invoice_Number
	       )
	OUTPUT INSERTED.Machine_ID,
           DELETED.Machine_Status_Flag, INSERTED.Machine_Status_Flag,
           DELETED.Machine_Class_ID, INSERTED.Machine_Class_ID,
           DELETED.CMPGameType, INSERTED.CMPGameType, 
           DELETED.Machine_Stock_No, INSERTED.Machine_Stock_No
    INTO @_Modified
    WHERE  Machine_ID = @Machine_ID
    
    IF EXISTS(
           SELECT 1
           FROM   @_Modified m
           WHERE  m.FlagChanged = 1 OR
			      m.GameIDChanged = 1 OR
                  m.CMPGameTypeChanged = 1 OR
                  m.StockNoChanged = 1
	)
    BEGIN
		EXEC [dbo].[usp_EBS_UpdateMachineDetails] @Machine_ID 
    END
/*****************************************************************************************************
* (CR# 191254 : EBS Communication Service) - MODIFIED BY Venkatesh.H  (END)
*****************************************************************************************************/

END

GO

