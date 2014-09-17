USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Voucher]') AND name = N'IX_Voucher_SiteID')
DROP INDEX [IX_Voucher_SiteID] ON [dbo].[Voucher] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX IX_Voucher_SiteID
ON [dbo].[Voucher] ([iSiteID],[dtPrinted],[iPaySiteID])
INCLUDE ([iDeviceID],[iAmount],[strVoucherStatus])
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Voucher]') AND name = N'IX_Voucher_Status')
DROP INDEX [IX_Voucher_Status] ON [dbo].[Voucher] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [IX_Voucher_Status]
ON [dbo].[Voucher] ([strVoucherStatus],[iPaySiteID],[dtPaid])
INCLUDE ([iDeviceID],[iPayDeviceID],[iAmount])
GO


USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Voucher]') AND name = N'IDX_Voucher_Status_dtPaid')
	DROP INDEX IDX_Voucher_Status_dtPaid ON [dbo].[Voucher] WITH ( ONLINE = OFF )
GO


CREATE NONCLUSTERED INDEX IDX_Voucher_Status_dtPaid
	ON [dbo].[Voucher] ([strVoucherStatus],[dtPaid])
	INCLUDE ([iDeviceID],[iPayDeviceID],[iSiteID],[VoucherRedeemedUser])
GO

