USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Site_Visit]') AND name = N'Site_Visit_ID')
DROP INDEX [Site_Visit_ID] ON [dbo].[Site_Visit] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Site_Visit_ID] ON [dbo].[Site_Visit] 
(
	[Site_Visit_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Site_Visit]') AND name = N'Site_Visit_Site_ID')
DROP INDEX [Site_Visit_Site_ID] ON [dbo].[Site_Visit] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Site_Visit_Site_ID] ON [dbo].[Site_Visit] 
(
	[Site_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Site_Visit]') AND name = N'Site_Visit_Staff_ID')
DROP INDEX [Site_Visit_Staff_ID] ON [dbo].[Site_Visit] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Site_Visit_Staff_ID] ON [dbo].[Site_Visit] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

