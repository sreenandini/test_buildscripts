USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[esp_InsertDepreciationDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[esp_InsertDepreciationDetails]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[esp_InsertDepreciationDetails]
(@MTID as int,@MCID as int,
@depotID as int,
@suppID as int,@mtypename  varchar(100),
@mclassname  varchar(100),
@mAsset_No  varchar(100),
@Serial_No  varchar(100),
@Alt_serial_No  varchar(100),
@purchase_date varchar(100),
@cost  money,
@NBV  money,
@DCP  money,
@TD  money,
@DPW  money,
@Current_Depot_ID  int,
@Current_Depot_Name  varchar(100),
@Stock_Status  varchar(20),
@PassDateFrom  varchar(50),
@PassDateToo  varchar(50),
@PassMachineType  varchar(50),
@PassMachineName  varchar(50),
@PassSupplier  varchar(100),
@PassDepot  varchar(50),
@Spare1  varchar(10),
@Spare2  varchar(10)
,@Spare3  varchar(10),
@Spare4  varchar(10),
@Spare5  varchar(10),
@Spare6  varchar(10),
@Spare7  varchar(10),
@Spare8  varchar(10)
,@Spare9  varchar(10),
@Spare10  varchar(10),
@Spare11  varchar(10),
@Spare12  varchar(10),
@Spare13  varchar(10),
@Spare14  varchar(10),
@Spare15  varchar(10),
@Spare16  varchar(10),
@Spare17  varchar(10),
@Spare18  varchar(10),
@Spare19  varchar(10),
@Spare20  varchar(10),
@PassPurchaseDateFrom  varchar(50),  
@PassPurchaseDateTo varchar(50))
AS

INSERT INTO DDP_ttk values
(@MTID,
@MCID,
@depotID,
@suppID,
@mtypename ,
@mclassname,
@mAsset_No
,@Serial_No,
@Alt_serial_No,
@purchase_date,
@cost,
@NBV,
@DCP,
@TD ,
@DPW,
@Current_Depot_ID ,
@Current_Depot_Name,
@Stock_Status,
@PassDateFrom,
@PassDateToo,
@PassMachineType,
@PassMachineName ,
@PassSupplier,
@PassDepot,
@Spare1,
@Spare2,
@Spare3 ,
@Spare4,
@Spare5,
@Spare6 ,
@Spare7,
@Spare8
,@Spare9,
@Spare10 ,
@Spare11,
@Spare12 ,
@Spare13,
@Spare14,
@Spare15,
@Spare16 ,
@Spare17,
@Spare18,
@Spare19,
@Spare20,
@PassPurchaseDateFrom,
@PassPurchaseDateTo)
	
GO

