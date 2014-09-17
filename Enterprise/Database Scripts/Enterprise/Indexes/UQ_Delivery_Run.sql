USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Delivery_Run]') AND name = N'Delivery_Run_Delivery_Run_Depot_ID')
DROP INDEX [Delivery_Run_Delivery_Run_Depot_ID] ON [dbo].[Delivery_Run] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Delivery_Run_Delivery_Run_Depot_ID] ON [dbo].[Delivery_Run] 
(
	[Delivery_Run_Depot_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Delivery_Run]') AND name = N'Delivery_Run_Delivery_Run_Depot_To_ID')
DROP INDEX [Delivery_Run_Delivery_Run_Depot_To_ID] ON [dbo].[Delivery_Run] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Delivery_Run_Delivery_Run_Depot_To_ID] ON [dbo].[Delivery_Run] 
(
	[Delivery_Run_Depot_To_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Delivery_Run]') AND name = N'Delivery_Run_Delivery_Staff_ID')
DROP INDEX [Delivery_Run_Delivery_Staff_ID] ON [dbo].[Delivery_Run] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Delivery_Run_Delivery_Staff_ID] ON [dbo].[Delivery_Run] 
(
	[Delivery_Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Delivery_Run]') AND name = N'Delivery_Run_ID')
DROP INDEX [Delivery_Run_ID] ON [dbo].[Delivery_Run] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Delivery_Run_ID] ON [dbo].[Delivery_Run] 
(
	[Delivery_Run_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Delivery_Run]') AND name = N'Delivery_Run_Staff_ID')
DROP INDEX [Delivery_Run_Staff_ID] ON [dbo].[Delivery_Run] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Delivery_Run_Staff_ID] ON [dbo].[Delivery_Run] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

