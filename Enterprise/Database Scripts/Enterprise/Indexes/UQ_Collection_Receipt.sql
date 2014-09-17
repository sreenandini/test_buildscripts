USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Receipt]') AND name = N'Collection_Receipt_Batch_ID')
DROP INDEX [Collection_Receipt_Batch_ID] ON [dbo].[Collection_Receipt] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Receipt_Batch_ID] ON [dbo].[Collection_Receipt] 
(
	[Batch_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Receipt]') AND name = N'Collection_Receipt_ID')
DROP INDEX [Collection_Receipt_ID] ON [dbo].[Collection_Receipt] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Collection_Receipt_ID] ON [dbo].[Collection_Receipt] 
(
	[Collection_Receipt_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Collection_Receipt]') AND name = N'Collection_Receipt_Site_ID')
DROP INDEX [Collection_Receipt_Site_ID] ON [dbo].[Collection_Receipt] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Collection_Receipt_Site_ID] ON [dbo].[Collection_Receipt] 
(
	[Site_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

