USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Staff_Customer_Access]') AND name = N'Staff_Customer_Access_Customer_Access_ID')
DROP INDEX [Staff_Customer_Access_Customer_Access_ID] ON [dbo].[Staff_Customer_Access] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Staff_Customer_Access_Customer_Access_ID] ON [dbo].[Staff_Customer_Access] 
(
	[Customer_Access_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Staff_Customer_Access]') AND name = N'Staff_Customer_Access_ID')
DROP INDEX [Staff_Customer_Access_ID] ON [dbo].[Staff_Customer_Access] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Staff_Customer_Access_ID] ON [dbo].[Staff_Customer_Access] 
(
	[Staff_Customer_Access_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Staff_Customer_Access]') AND name = N'Staff_Customer_Access_Staff_ID')
DROP INDEX [Staff_Customer_Access_Staff_ID] ON [dbo].[Staff_Customer_Access] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Staff_Customer_Access_Staff_ID] ON [dbo].[Staff_Customer_Access] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

