USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Site_Updates]') AND name = N'Site_Update_ID')
DROP INDEX [Site_Update_ID] ON [dbo].[Site_Updates] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Site_Update_ID] ON [dbo].[Site_Updates] 
(
	[Site_Update_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Site_Updates]') AND name = N'Site_Updates_Site_ID')
DROP INDEX [Site_Updates_Site_ID] ON [dbo].[Site_Updates] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Site_Updates_Site_ID] ON [dbo].[Site_Updates] 
(
	[Site_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

