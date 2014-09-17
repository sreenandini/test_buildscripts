USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Planned_Movement]') AND name = N'Planned_Movement_ID')
DROP INDEX [Planned_Movement_ID] ON [dbo].[Planned_Movement] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Planned_Movement_ID] ON [dbo].[Planned_Movement] 
(
	[Planned_Movement_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Planned_Movement]') AND name = N'Planned_Movement_Planned_Movement_From_Accessory_Installation_ID')
DROP INDEX [Planned_Movement_Planned_Movement_From_Accessory_Installation_ID] ON [dbo].[Planned_Movement] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Planned_Movement_Planned_Movement_From_Accessory_Installation_ID] ON [dbo].[Planned_Movement] 
(
	[Planned_Movement_From_Accessory_Installation_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Planned_Movement]') AND name = N'Planned_Movement_Planned_Movement_From_Bar_Position_ID')
DROP INDEX [Planned_Movement_Planned_Movement_From_Bar_Position_ID] ON [dbo].[Planned_Movement] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Planned_Movement_Planned_Movement_From_Bar_Position_ID] ON [dbo].[Planned_Movement] 
(
	[Planned_Movement_From_Bar_Position_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Planned_Movement]') AND name = N'Planned_Movement_Planned_Movement_From_Depot_ID')
DROP INDEX [Planned_Movement_Planned_Movement_From_Depot_ID] ON [dbo].[Planned_Movement] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Planned_Movement_Planned_Movement_From_Depot_ID] ON [dbo].[Planned_Movement] 
(
	[Planned_Movement_From_Depot_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Planned_Movement]') AND name = N'Planned_Movement_Planned_Movement_From_Installation_ID')
DROP INDEX [Planned_Movement_Planned_Movement_From_Installation_ID] ON [dbo].[Planned_Movement] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Planned_Movement_Planned_Movement_From_Installation_ID] ON [dbo].[Planned_Movement] 
(
	[Planned_Movement_From_Installation_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Planned_Movement]') AND name = N'Planned_Movement_Planned_Movement_From_Machine_Class_ID')
DROP INDEX [Planned_Movement_Planned_Movement_From_Machine_Class_ID] ON [dbo].[Planned_Movement] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Planned_Movement_Planned_Movement_From_Machine_Class_ID] ON [dbo].[Planned_Movement] 
(
	[Planned_Movement_From_Machine_Class_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Planned_Movement]') AND name = N'Planned_Movement_Planned_Movement_From_Machine_ID')
DROP INDEX [Planned_Movement_Planned_Movement_From_Machine_ID] ON [dbo].[Planned_Movement] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Planned_Movement_Planned_Movement_From_Machine_ID] ON [dbo].[Planned_Movement] 
(
	[Planned_Movement_From_Machine_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Planned_Movement]') AND name = N'Planned_Movement_Planned_Movement_To_Bar_Position_ID')
DROP INDEX [Planned_Movement_Planned_Movement_To_Bar_Position_ID] ON [dbo].[Planned_Movement] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Planned_Movement_Planned_Movement_To_Bar_Position_ID] ON [dbo].[Planned_Movement] 
(
	[Planned_Movement_To_Bar_Position_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Planned_Movement]') AND name = N'Planned_Movement_Planned_Movement_To_Machine_ID')
DROP INDEX [Planned_Movement_Planned_Movement_To_Machine_ID] ON [dbo].[Planned_Movement] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Planned_Movement_Planned_Movement_To_Machine_ID] ON [dbo].[Planned_Movement] 
(
	[Planned_Movement_To_Machine_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Planned_Movement]') AND name = N'Planned_Movement_Staff_ID')
DROP INDEX [Planned_Movement_Staff_ID] ON [dbo].[Planned_Movement] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Planned_Movement_Staff_ID] ON [dbo].[Planned_Movement] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

