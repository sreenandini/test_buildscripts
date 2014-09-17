USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation_Datapak]') AND name = N'Installation_Datapak_Datapak_ID')
DROP INDEX [Installation_Datapak_Datapak_ID] ON [dbo].[Installation_Datapak] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Installation_Datapak_Datapak_ID] ON [dbo].[Installation_Datapak] 
(
	[Datapak_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation_Datapak]') AND name = N'Installation_Datapak_Installation_Datapak_BarId')
DROP INDEX [Installation_Datapak_Installation_Datapak_BarId] ON [dbo].[Installation_Datapak] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Installation_Datapak_Installation_Datapak_BarId] ON [dbo].[Installation_Datapak] 
(
	[Installation_Datapak_BarId] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation_Datapak]') AND name = N'Installation_Datapak_Installation_Datapak_SecondaryMachineCode')
DROP INDEX [Installation_Datapak_Installation_Datapak_SecondaryMachineCode] ON [dbo].[Installation_Datapak] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Installation_Datapak_Installation_Datapak_SecondaryMachineCode] ON [dbo].[Installation_Datapak] 
(
	[Installation_Datapak_SecondaryMachineCode] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation_Datapak]') AND name = N'Installation_Datapak_Installation_Datapak_SiteCode')
DROP INDEX [Installation_Datapak_Installation_Datapak_SiteCode] ON [dbo].[Installation_Datapak] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Installation_Datapak_Installation_Datapak_SiteCode] ON [dbo].[Installation_Datapak] 
(
	[Installation_Datapak_SiteCode] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Installation_Datapak]') AND name = N'Installation_Datapak_Installation_ID')
DROP INDEX [Installation_Datapak_Installation_ID] ON [dbo].[Installation_Datapak] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Installation_Datapak_Installation_ID] ON [dbo].[Installation_Datapak] 
(
	[Installation_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

