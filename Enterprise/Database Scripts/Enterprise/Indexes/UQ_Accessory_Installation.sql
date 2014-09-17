USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Accessory_Installation]') AND name = N'Accessory_Installation_Accessory_ID')
DROP INDEX [Accessory_Installation_Accessory_ID] ON [dbo].[Accessory_Installation] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Accessory_Installation_Accessory_ID] ON [dbo].[Accessory_Installation] 
(
	[Accessory_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Accessory_Installation]') AND name = N'Accessory_Installation_Accessory_Installation_Amedis_Import_Log_ID')
DROP INDEX [Accessory_Installation_Accessory_Installation_Amedis_Import_Log_ID] ON [dbo].[Accessory_Installation] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Accessory_Installation_Accessory_Installation_Amedis_Import_Log_ID] ON [dbo].[Accessory_Installation] 
(
	[Accessory_Installation_Amedis_Import_Log_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Accessory_Installation]') AND name = N'Accessory_Installation_Accessory_Installation_Amedis_Import_Log_Withdrawl_ID')
DROP INDEX [Accessory_Installation_Accessory_Installation_Amedis_Import_Log_Withdrawl_ID] ON [dbo].[Accessory_Installation] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Accessory_Installation_Accessory_Installation_Amedis_Import_Log_Withdrawl_ID] ON [dbo].[Accessory_Installation] 
(
	[Accessory_Installation_Amedis_Import_Log_Withdrawl_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Accessory_Installation]') AND name = N'Accessory_Installation_Bar_Position_ID')
DROP INDEX [Accessory_Installation_Bar_Position_ID] ON [dbo].[Accessory_Installation] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Accessory_Installation_Bar_Position_ID] ON [dbo].[Accessory_Installation] 
(
	[Bar_Position_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Accessory_Installation]') AND name = N'Accessory_Installation_ID')
DROP INDEX [Accessory_Installation_ID] ON [dbo].[Accessory_Installation] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Accessory_Installation_ID] ON [dbo].[Accessory_Installation] 
(
	[Accessory_Installation_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

