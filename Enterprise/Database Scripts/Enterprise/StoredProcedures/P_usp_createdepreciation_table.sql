USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_createdepreciation_table]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_createdepreciation_table]
GO

USE [Enterprise]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE[dbo].[usp_createdepreciation_table]
AS

	CREATE TABLE [dbo].[DDP_ttk](
	[machine_type_id] [int] NULL,
	[Machine_Class_ID] [int] NULL,
	[Depot_ID] [int] NULL,
	[Supp_ID] [int] NULL,
	[Machine_Type_Name] [varchar](100) NULL,
	[Machine_Class_Name] [varchar](100) NULL,
	[Asset_No] [varchar](100) NULL,
	[Serial_No] [varchar](100) NULL,
	[Alt_serial_No] [varchar](100) NULL,
	[purchase_date] [varchar](100) NULL,
	[cost] [money] NULL,
	[NBV] [money] NULL,
	[DCP] [money] NULL,
	[TD] [money] NULL,
	[DPW] [money] NULL,
	[Current_Depot_ID] [int] NULL,
	[Current_Depot_Name] [varchar](100) NULL,
	[Stock_Status] [varchar](20) NULL,
	[PassDateFrom] [varchar](50) NULL,
	[PassDateToo] [varchar](50) NULL,
	[PassMachineType] [varchar](50) NULL,
	[PassMachineName] [varchar](50) NULL,
	[PassSupplier] [varchar](100) NULL,
	[PassDepot] [varchar](50) NULL,
	[Spare1] [varchar](10) NULL,
	[Spare2] [varchar](10) NULL,
	[Spare3] [varchar](10) NULL,
	[Spare4] [varchar](10) NULL,
	[Spare5] [varchar](10) NULL,
	[Spare6] [varchar](10) NULL,
	[Spare7] [varchar](10) NULL,
	[Spare8] [varchar](10) NULL,
	[Spare9] [varchar](10) NULL,
	[Spare10] [varchar](10) NULL,
	[Spare11] [varchar](10) NULL,
	[Spare12] [varchar](10) NULL,
	[Spare13] [varchar](10) NULL,
	[Spare14] [varchar](10) NULL,
	[Spare15] [varchar](10) NULL,
	[Spare16] [varchar](10) NULL,
	[Spare17] [varchar](10) NULL,
	[Spare18] [varchar](10) NULL,
	[Spare19] [varchar](10) NULL,
	[Spare20] [varchar](10) NULL,
	[PassPurchaseDateFrom][varchar](50) NULL,
	[PassPurchaseDateTo] [varchar](50) NULL
	) ON [PRIMARY]


GO

