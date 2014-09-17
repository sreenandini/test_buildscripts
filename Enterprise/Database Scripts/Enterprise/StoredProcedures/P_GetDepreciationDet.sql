USE [Enterprise]
GO

IF EXISTS (
       SELECT 1
       FROM   sys.objects
       WHERE  OBJECT_ID = OBJECT_ID(N'[dbo].[GetDepreciationDet]')
              AND TYPE IN (N'P', N'PC')
   )
    DROP PROCEDURE [dbo].[GetDepreciationDet]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetDepreciationDet](@mtypeid AS INT)
AS
	IF (@mtypeid = 0)
	BEGIN
	    SELECT machine_type_id,
	           Machine_Class_ID,
	           Depot_ID,
	           Supp_ID,
	           Machine_Type_Name,
	           Machine_Class_Name,
	           Asset_No,
	           Serial_No,
	           Alt_serial_No,
	           CONVERT(VARCHAR(20), CAST(purchase_date AS DATETIME), 106) AS purchase_date,
	           cost,
	           NBV,
	           DCP,
	           TD,
	           DPW,
	           Current_Depot_ID,
	           Current_Depot_Name,
	           Stock_Status,
	           CONVERT(VARCHAR(20), CAST(PassDateFrom AS DATETIME), 106) AS PassDateFrom,
	           CONVERT(VARCHAR(20), CAST(PassDateToo AS DATETIME), 106) AS PassDateToo,
	           PassMachineType,
	           PassMachineName,
	           PassSupplier,
	           PassDepot,
	           Spare1,
	           Spare2,
	           Spare3,
	           Spare4,
	           Spare5,
	           Spare6,
	           Spare7,
	           Spare8,
	           Spare9,
	           Spare10,
	           Spare11,
	           Spare12,
	           Spare13,
	           Spare14,
	           Spare15,
	           Spare16,
	           Spare17,
	           Spare18,
	           Spare19,
	           Spare20,
	           CONVERT(VARCHAR(20), CAST(PassPurchaseDateFrom AS DATETIME), 106) AS PassPurchaseDateFrom,
	           CONVERT(VARCHAR(20), CAST(PassPurchaseDateTo AS DATETIME), 106) AS PassPurchaseDateTo
	    FROM   DDP_ttk WITH(NOLOCK)
	END
	ELSE
	BEGIN
	    SELECT machine_type_id,
	           Machine_Class_ID,
	           Depot_ID,
	           Supp_ID,
	           Machine_Type_Name,
	           Machine_Class_Name,
	           Asset_No,
	           Serial_No,
	           Alt_serial_No,
	           CONVERT(VARCHAR(20), CAST(purchase_date AS DATETIME), 106),
	           cost,
	           NBV,
	           DCP,
	           TD,
	           DPW,
	           Current_Depot_ID,
	           Current_Depot_Name,
	           Stock_Status,
	           CONVERT(VARCHAR(20), CAST(PassDateFrom AS DATETIME), 106),
	           CONVERT(VARCHAR(20), CAST(PassDateToo AS DATETIME), 106),
	           PassMachineType,
	           PassMachineName,
	           PassSupplier,
	           PassDepot,
	           Spare1,
	           Spare2,
	           Spare3,
	           Spare4,
	           Spare5,
	           Spare6,
	           Spare7,
	           Spare8,
	           Spare9,
	           Spare10,
	           Spare11,
	           Spare12,
	           Spare13,
	           Spare14,
	           Spare15,
	           Spare16,
	           Spare17,
	           Spare18,
	           Spare19,
	           Spare20,
	           CONVERT(VARCHAR(20), CAST(PassPurchaseDateFrom AS DATETIME), 106),
	           CONVERT(VARCHAR(20), CAST(PassPurchaseDateTo AS DATETIME), 106)
	    FROM   DDP_ttk WITH(NOLOCK)
	    WHERE  machine_type_id = @mtypeid
	END
GO

	   