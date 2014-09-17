USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[VTP_Archive]') AND name = N'VTP_Archive_Date')
DROP INDEX [VTP_Archive_Date] ON [dbo].[VTP_Archive] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [VTP_Archive_Date] ON [dbo].[VTP_Archive] 
(
	[Date] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[VTP_Archive]') AND name = N'VTP_Archive_Installation_ID')
DROP INDEX [VTP_Archive_Installation_ID] ON [dbo].[VTP_Archive] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [VTP_Archive_Installation_ID] ON [dbo].[VTP_Archive] 
(
	[Installation_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[VTP_Archive]') AND name = N'VTP_Archive_Period_ID')
DROP INDEX [VTP_Archive_Period_ID] ON [dbo].[VTP_Archive] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [VTP_Archive_Period_ID] ON [dbo].[VTP_Archive] 
(
	[Period_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[VTP_Archive]') AND name = N'VTP_Archive_Read_ID')
DROP INDEX [VTP_Archive_Read_ID] ON [dbo].[VTP_Archive] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [VTP_Archive_Read_ID] ON [dbo].[VTP_Archive] 
(
	[Read_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[VTP_Archive]') AND name = N'VTP_Archive_Week_ID')
DROP INDEX [VTP_Archive_Week_ID] ON [dbo].[VTP_Archive] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [VTP_Archive_Week_ID] ON [dbo].[VTP_Archive] 
(
	[Week_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[VTP_Archive]') AND name = N'VTP_ID')
DROP INDEX [VTP_ID] ON [dbo].[VTP_Archive] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [VTP_ID] ON [dbo].[VTP_Archive] 
(
	[VTP_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

