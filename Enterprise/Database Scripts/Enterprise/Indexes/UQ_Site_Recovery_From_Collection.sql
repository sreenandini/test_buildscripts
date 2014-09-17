USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Site_Recovery_From_Collection]') AND name = N'Site_Recovery_From_Collection_Collection_ID')
DROP INDEX [Site_Recovery_From_Collection_Collection_ID] ON [dbo].[Site_Recovery_From_Collection] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Site_Recovery_From_Collection_Collection_ID] ON [dbo].[Site_Recovery_From_Collection] 
(
	[Collection_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Site_Recovery_From_Collection]') AND name = N'Site_Recovery_From_Collection_ID')
DROP INDEX [Site_Recovery_From_Collection_ID] ON [dbo].[Site_Recovery_From_Collection] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Site_Recovery_From_Collection_ID] ON [dbo].[Site_Recovery_From_Collection] 
(
	[Site_Recovery_From_Collection_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Site_Recovery_From_Collection]') AND name = N'Site_Recovery_From_Collection_Site_Recovery_ID')
DROP INDEX [Site_Recovery_From_Collection_Site_Recovery_ID] ON [dbo].[Site_Recovery_From_Collection] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Site_Recovery_From_Collection_Site_Recovery_ID] ON [dbo].[Site_Recovery_From_Collection] 
(
	[Site_Recovery_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

