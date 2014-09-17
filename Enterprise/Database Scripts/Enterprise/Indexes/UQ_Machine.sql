USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine]') AND name = N'Machine_Depot_ID')
DROP INDEX [Machine_Depot_ID] ON [dbo].[Machine] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Depot_ID] ON [dbo].[Machine] 
(
	[Depot_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine]') AND name = N'Machine_Depreciation_Policy_ID')
DROP INDEX [Machine_Depreciation_Policy_ID] ON [dbo].[Machine] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Depreciation_Policy_ID] ON [dbo].[Machine] 
(
	[Depreciation_Policy_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine]') AND name = N'Machine_ID')
DROP INDEX [Machine_ID] ON [dbo].[Machine] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE UNIQUE NONCLUSTERED INDEX [Machine_ID] ON [dbo].[Machine] 
(
	[Machine_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine]') AND name = N'Machine_Machine_Category_ID')
DROP INDEX [Machine_Machine_Category_ID] ON [dbo].[Machine] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Machine_Category_ID] ON [dbo].[Machine] 
(
	[Machine_Category_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine]') AND name = N'Machine_Machine_Class_ID')
DROP INDEX [Machine_Machine_Class_ID] ON [dbo].[Machine] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Machine_Class_ID] ON [dbo].[Machine] 
(
	[Machine_Class_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine]') AND name = N'Machine_Machine_Depot_Planned_Movement_ID')
DROP INDEX [Machine_Machine_Depot_Planned_Movement_ID] ON [dbo].[Machine] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Machine_Depot_Planned_Movement_ID] ON [dbo].[Machine] 
(
	[Machine_Depot_Planned_Movement_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine]') AND name = N'Machine_Machine_Site_Planned_Movement_ID')
DROP INDEX [Machine_Machine_Site_Planned_Movement_ID] ON [dbo].[Machine] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Machine_Site_Planned_Movement_ID] ON [dbo].[Machine] 
(
	[Machine_Site_Planned_Movement_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine]') AND name = N'Machine_Operator_ID')
DROP INDEX [Machine_Operator_ID] ON [dbo].[Machine] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Operator_ID] ON [dbo].[Machine] 
(
	[Operator_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine]') AND name = N'Machine_Staff_ID')
DROP INDEX [Machine_Staff_ID] ON [dbo].[Machine] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Staff_ID] ON [dbo].[Machine] 
(
	[Staff_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

USE [Enterprise]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Machine]') AND name = N'Machine_Terms_Profile_ID')
DROP INDEX [Machine_Terms_Profile_ID] ON [dbo].[Machine] WITH ( ONLINE = OFF )
GO

USE [Enterprise]
GO

CREATE NONCLUSTERED INDEX [Machine_Terms_Profile_ID] ON [dbo].[Machine] 
(
	[Terms_Profile_ID] ASC
)WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

