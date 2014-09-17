USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Staff_Depot]') AND name = N'Staff_Depot_Depot_ID')
DROP INDEX [Staff_Depot_Depot_ID] ON [dbo].[Staff_Depot] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Staff_Depot_Depot_ID] ON [dbo].[Staff_Depot] 
(
	[Depot_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Staff_Depot]') AND name = N'Staff_Depot_ID')
DROP INDEX [Staff_Depot_ID] ON [dbo].[Staff_Depot] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Staff_Depot_ID] ON [dbo].[Staff_Depot] 
(
	[Staff_Depot_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Staff_Depot]') AND name = N'Staff_Depot_Staff_ID')
DROP INDEX [Staff_Depot_Staff_ID] ON [dbo].[Staff_Depot] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Staff_Depot_Staff_ID] ON [dbo].[Staff_Depot] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

