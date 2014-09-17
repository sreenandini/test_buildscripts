USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Calcs]') AND name = N'Collection_Calcs_Batch_ID')
DROP INDEX [Collection_Calcs_Batch_ID] ON [dbo].[Collection_Calcs] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Calcs_Batch_ID] ON [dbo].[Collection_Calcs] 
(
	[Batch_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Calcs]') AND name = N'Collection_Calcs_Datapak_ID')
DROP INDEX [Collection_Calcs_Datapak_ID] ON [dbo].[Collection_Calcs] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Calcs_Datapak_ID] ON [dbo].[Collection_Calcs] 
(
	[Datapak_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Calcs]') AND name = N'Collection_Calcs_Installation_ID')
DROP INDEX [Collection_Calcs_Installation_ID] ON [dbo].[Collection_Calcs] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Calcs_Installation_ID] ON [dbo].[Collection_Calcs] 
(
	[Installation_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Calcs]') AND name = N'Collection_Calcs_Machine_ID')
DROP INDEX [Collection_Calcs_Machine_ID] ON [dbo].[Collection_Calcs] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Calcs_Machine_ID] ON [dbo].[Collection_Calcs] 
(
	[Machine_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Calcs]') AND name = N'Collection_ID')
DROP INDEX [Collection_ID] ON [dbo].[Collection_Calcs] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Collection_ID] ON [dbo].[Collection_Calcs] 
(
	[Collection_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

