USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Site_User_Access]') AND name = N'Site_User_Access_ID')
DROP INDEX [Site_User_Access_ID] ON [dbo].[Site_User_Access] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Site_User_Access_ID] ON [dbo].[Site_User_Access] 
(
	[Site_User_Access_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Site_User_Access]') AND name = N'Site_User_Access_Site_ID')
DROP INDEX [Site_User_Access_Site_ID] ON [dbo].[Site_User_Access] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Site_User_Access_Site_ID] ON [dbo].[Site_User_Access] 
(
	[Site_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

